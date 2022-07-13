using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Two pairs
    public class TwoPair : Hand
	{
        public TwoPair(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.TwoPair) {

            var cardsInHand = this.CardsInPlay
                    .GroupBy(x => x.Rank)
                    .Where(group => group.Count() == 2)
                    .SelectMany(group => group.Select(y => y))
                    .ToList();

            // Pairs will have 4 cards
            if (cardsInHand.Count >= 4)
            {
                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);
            }
        }
    }
}
