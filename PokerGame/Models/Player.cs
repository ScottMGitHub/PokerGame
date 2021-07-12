using PokerGame.Models.PokerHand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame.Models
{
    public class Player
    {


        public int Number { get; set; }
        public List<Card> Cards { get; set; } = new();
        public Hand HighestHand { get; set; }
        public Card HighestCard { get; set; }
        public bool HasWinningHand { get; set; }

        public Player() { }

        public Player(int number)
        {
            Number = number;
        }

        public void EvaluatePlayersCards(List<Card> communityCards)
        {

            var cardsInPlay = new List<Card>(communityCards);
            cardsInPlay.AddRange(Cards);

            // Not optimized, instantiating each of these one by one
            List<Hand> possibleHands = new List<Hand>() {
                new RoyalFlush(cardsInPlay),
                new StraightFlush(cardsInPlay),
                new FourOfAKind(cardsInPlay),
                new FullHouse(cardsInPlay),
                new Flush(cardsInPlay),
                new Straight(cardsInPlay),
                new ThreeOfAKind(cardsInPlay),
                new TwoPair(cardsInPlay),
                new Pair(cardsInPlay),
                new HighCard(cardsInPlay),
            };

            // Order descending for highest hand
            HighestHand = possibleHands.Where(x => x.HandExists).OrderByDescending(x => (int)x.HandType).FirstOrDefault();
            // Hand cards should already be order from lowest to highest
            HighestCard = HighestHand.HandCards.Last();

        }
    
    }
}