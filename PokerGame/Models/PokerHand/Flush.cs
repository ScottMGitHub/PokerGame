using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Any five cards of the same suit, but not in a sequence.
    public class Flush : Hand
	{
        public Flush(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.Flush) {

            // Cards of same suit
            var cardsInHand = CardsInPlay
                .GroupBy(g => g.Suit)
                .Where(g => g.Count() >= 5)
                .SelectMany(x => x)
                .ToList();

            if(cardsInHand.Count >= 5)
			{

                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);
            }
        }
    }
}
