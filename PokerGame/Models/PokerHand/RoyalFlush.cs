using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGame.Models.PokerHand
{
    // A, K, Q, J, 10, and all the same suit.
    public class RoyalFlush : Hand
	{
        public RoyalFlush(List<Card> cardsInPlay) 
            : base(cardsInPlay, Enums.HandType.RoyalFlush) {

            // Cards of royal flush rank
            var rankCards = CardsInPlay
                .Where(x => HandRanks.ROYAL_FLUSH_RANKS.Contains(x.Rank))
                .ToList();

            // Cards of same suit
            var suitCards = rankCards
                .GroupBy(g => g.Suit)
                .Where(g => g.Count() == 5)
                .SelectMany(x => x)
                .ToList();

            if (suitCards.Count == 5)
            {
                this.HandCards = GetHandCards(suitCards, this.CardsInPlay);
                this.HandExists = true;
                this.HandValue = CalculateHandValue(this.HandCards);

            }
        }
    }
}
