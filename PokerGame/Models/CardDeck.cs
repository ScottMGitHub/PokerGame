using PokerGame.Enums;
using System;
using System.Collections.Generic;
namespace PokerGame.Models
{
    public class CardDeck
    {

        public List<Card> Cards { get; set; } = new();

        public CardDeck()
        {
            // Foreach enum of type and value generate a card
            foreach (var cardSuit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (var CardRank in Enum.GetValues(typeof(CardRank)))
                {
                    Cards.Add(new Card((CardSuit)cardSuit, (CardRank)CardRank));
                }
            }
        }

    }
}