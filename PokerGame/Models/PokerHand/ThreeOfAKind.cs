using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // All four cards of the same rank.
    public class ThreeOfAKind : Hand
	{
        public ThreeOfAKind(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.ThreeOfAKind) {

            var cardsInHand = this.CardsInPlay
                           .GroupBy(x => x.Rank)
                           .Where(group => group.Count() == 3)
                           .SelectMany(group => group.Select(y => y))
                           .ToList();

            if (cardsInHand.Count == 3)
            {
                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }

        }
    }
}
