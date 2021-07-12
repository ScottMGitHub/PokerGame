using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // Three of a kind and a pair
    public class FullHouse : Hand
	{
        public FullHouse(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.FullHouse) {

            var threeOfAKind = new ThreeOfAKind(this.CardsInPlay);
            var pair = new Pair(this.CardsInPlay);

            if (threeOfAKind.HandExists && pair.HandExists)
            {
                var cardsInHand = new List<Card>(threeOfAKind.HandCards);
                cardsInHand.AddRange(pair.HandCards);

                this.HandCards = GetHandCards(cardsInHand, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }
        }
    }
}
