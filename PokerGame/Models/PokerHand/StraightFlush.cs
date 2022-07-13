using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Five cards in a sequence, all in the same suit.
    public class StraightFlush : Hand
	{
        public StraightFlush(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.StraightFlush) {

            // top 5 cards of same suit
            var suitCards = CardsInPlay.GroupBy(g => g.Suit)
                .Where(g => g.Count() >= 5)
                .SelectMany(x => x)
                .ToList();

            var consecutiveCards = base.ConsecutiveSequenceOfCards(suitCards);

            if (suitCards.Count >= 5 && consecutiveCards.Count() >= 5)
            {
                this.HandCards = GetHandCards(consecutiveCards, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }
        }
    }
}
