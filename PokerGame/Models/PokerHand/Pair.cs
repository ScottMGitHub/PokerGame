using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Two cards of the same suit
    public class Pair : Hand
	{
        public Pair(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.Pair) {

            var cardsInHand = this.CardsInPlay
                    .GroupBy(x => x.Rank)
                    .Where(group => group.Count() == 2)
                    .SelectMany(group => group.Select(y => y))
                    .ToList();

            if (cardsInHand.Count >= 2)
            {

                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay); 
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }
        }
    }
}
