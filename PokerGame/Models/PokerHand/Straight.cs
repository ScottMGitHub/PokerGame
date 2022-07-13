using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    public class Straight : Hand
	{
        public Straight(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.Straight) {

            var cardsInHand = base.ConsecutiveSequenceOfCards(CardsInPlay);

            if(cardsInHand.Count >= 5) {
                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }
        }
    }
}
