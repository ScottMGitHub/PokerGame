using PokerGame.Enums;

namespace PokerGame.Models
{
    public class Card
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
        public bool CardOpen { get; set; }
        public bool IsFaceCard {get; set;}
        public bool IsRedCard {get; set;}

        public Card(CardSuit suit, CardRank rank, bool open = false)
        {
            Suit = suit;
            Rank = rank;
            CardOpen = open;

            IsFaceCard = 
                rank == PokerGame.Enums.CardRank.Ace || rank == PokerGame.Enums.CardRank.King ||
                rank == PokerGame.Enums.CardRank.Queen || rank == PokerGame.Enums.CardRank.Jack;

            IsRedCard = 
                suit == PokerGame.Enums.CardSuit.Diamonds || suit ==PokerGame.Enums.CardSuit.Hearts;
        }

    }
}