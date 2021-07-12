using PokerGame.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class HandRanks {
    public static readonly IList<CardRank> ROYAL_FLUSH_RANKS = new ReadOnlyCollection<CardRank>(
        new List<CardRank> {
            CardRank.Ace,
            CardRank.King,
            CardRank.Queen,
            CardRank.Jack,
            CardRank.Ten
        }
    );
}