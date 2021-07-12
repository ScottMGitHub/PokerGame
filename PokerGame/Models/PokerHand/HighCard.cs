using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Two cards of the same suit
    public class HighCard : Hand
	{
        public HighCard(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.HighCard) {

            this.HandCards = GetHandCards(cardsInPlay, this.CardsInPlay);
            this.HandExists = true;
            this.HandValue = CalculateHandValue(this.HandCards);

        }
    }
}
