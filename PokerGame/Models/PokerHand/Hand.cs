using PokerGame.Enums;
using System.Collections.Generic;
using System.Linq;

// TO DO Refactor this class - should be an abstract class with handtypes inherited
// Distinguish between Hands as an abstract class and players hands, the hands a player holds
namespace PokerGame.Models.PokerHand
{
    public abstract class Hand
    {
        private const int NUMBER_OF_CARDS_IN_HAND = 5;

        public List<Card> CardsInPlay { get; set; } = new();

        public List<Card> HandCards { get; set; } = new();

        public HandType HandType { get; set; }

        public bool HandExists { get; set; }

        public Card HighCard { get; set; }

        public int HandValue { get; set; }

        public Hand(List<Card> cardsInPlay, HandType handType)
        {
            CardsInPlay = cardsInPlay;
            HandType = handType;
            HighCard = OrderCards(cardsInPlay, 5).FirstOrDefault();
        }

        public List<Card> ConsecutiveSequenceOfCards(List<Card> cards, int minNumberOfSequentialCards = 5)
        {

            List<Card> consecutiveCards = new List<Card>();
 
            // Order by rank from lowest up
            var cardsOrdered = cards.OrderBy(x => (int)x.Rank).ToList();

            // Check for ACE
            var aces = cardsOrdered.Where(x => x.Rank == CardRank.Ace).ToList();

            // Check consecutive sequence
            for (int i = 0; i < cardsOrdered.Count(); i++) {


                // If the minimum number of consecutive cards is reached then exit
                if (consecutiveCards.Count == minNumberOfSequentialCards)
                    break;

                // If first item 
                if (i == 0) {
                    consecutiveCards.Add(cardsOrdered[i]);
                    continue;
                }

                // if empty list add item
                if (consecutiveCards.Count() == 0) {
                    consecutiveCards.Add(cardsOrdered[i]);
                    continue;
                }

                // If remainder of 1 then current card is in sequence with last card
                if ((int)cardsOrdered[i].Rank - (int)cardsOrdered[i - 1].Rank == 1) {

                    // If has Ace and current item is king then add both to list and exit
                    if (aces.Count() > 0 && cardsOrdered[i].Rank == CardRank.King)
                    {
                        consecutiveCards.Add(cardsOrdered[i]);
                        consecutiveCards.Add(aces.FirstOrDefault());
                        break;
                    }

                    consecutiveCards.Add(cardsOrdered[i]);
                    continue;
                }

                // Else re-initialize list with new item
                consecutiveCards = new List<Card>() { 
                    cardsOrdered[i]
                };

            }

            return consecutiveCards;

        }
    
        public List<Card> OrderCards(List<Card> cards, int numberOfCardsToTake)
		{
            // If cards has Ace skip ace and add at the end of list
            var numberOfAces = cards.Count(x => x.Rank == Enums.CardRank.Ace);
            if (numberOfAces > 0) {

                // Get by highest rank by descending, excluding ace, then order by ascending
                var orderedCards = cards
                    .Where(x => x.Rank != CardRank.Ace)
                    .Select(x => x)
					.OrderByDescending(x => x.Rank)
                    .Take(numberOfCardsToTake - numberOfAces)
                    .OrderBy(x => x.Rank)
                    .ToList();

                orderedCards.AddRange(cards.Where(x => x.Rank == Enums.CardRank.Ace).ToList());

                return orderedCards;

            }

            // Get by highest rank the order by ascending
            return cards.OrderByDescending(x => x.Rank)
                .Take(numberOfCardsToTake)
                .OrderBy(x => x.Rank)
                .ToList();

		}

        public int CalculateHandValue(List<Card> cards)
        {
            // If cards has Ace skip Ace and add at the end
            if (cards.Any(x => x.Rank == Enums.CardRank.Ace))
            {
                var valueExcludingAce = cards.Where(x => x.Rank != CardRank.Ace).Sum(x => (int)x.Rank);
                return valueExcludingAce + (int)CardRankAceHigh.Ace * cards.Count(x => x.Rank == Enums.CardRank.Ace);
            }

            // Get by highest rank the order by ascending
            return cards.Sum(x => (int)x.Rank);

        }
        
        public List<Card> GetHandCards(List<Card> handTypeCards, List<Card> cardsInPlay)
		{

            // Must always return 5 cards total, add range of cards excluding the cards already in hand
            var numberOfAdditionalCards = NUMBER_OF_CARDS_IN_HAND - handTypeCards.Count;
            if(numberOfAdditionalCards > 0)
			{
                var additionalCards = OrderCards(cardsInPlay.Except(handTypeCards).ToList(), numberOfAdditionalCards);
                handTypeCards.AddRange(additionalCards);
            }

            return OrderCards(handTypeCards, NUMBER_OF_CARDS_IN_HAND);

        }
    }
}
