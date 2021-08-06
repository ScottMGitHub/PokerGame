namespace PokerGame.Enums
{
    public enum CardRank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }

    public enum CardRankAceHigh
	{
        // Give Ace arbitrarily high value of 100.
        // 14 will not work when calulating total hand value
        // as it could lead to an equal total hand value with none ace values
        Ace = 100,
    }
}
