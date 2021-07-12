using System;
using System.Collections.Generic;
using System.Linq;
using PokerGame.Enums;
using PokerGame.Models.PokerHand;

namespace PokerGame.Models
{
    public class Game
    {

        // A game deck can be comprised of multiple card decks
        public List<Card> GameCards { get; set; } = new();

        public List<Card> CommunityCards { get; set; } = new();

        public List<Player> Players { get; set; } = new();

        public GameState State { get; set;} = GameState.Idle;

        #region constructors
        public Game() { }
     
        // New Game
        public Game(int numberOfPlayers = 2)
        {

            // Generate the Deck and shuffle them 3 times
            GameCards = ShuffleCards(ShuffleCards(ShuffleCards(new CardDeck().Cards)));

            // create players
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                Players.Add(new Player(i));
            }

            // Deal a card to each player until each player has two cards
            do
            {
                foreach (var player in Players)
                {
                    var card = GameCards.FirstOrDefault();
                    player.Cards.Add(card);
                    GameCards.Remove(card);
                }
            } while (Players.Any(x => x.Cards.Count() != 2));

            // User is Player one - so cards are always open
            Players.FirstOrDefault().Cards.ForEach(x => x.CardOpen = true);

            // Set Game State
            State = GameState.NewGame;

        }

        #endregion

        #region public methods
        public void DealFlop() {
            
            if (State != GameState.NewGame)
                return;

            DealCommunityCards(3);
            State = GameState.FlopDealt;

        }

        public void DealTurn() {

            if (State != GameState.FlopDealt)
                return;

            DealCommunityCards(1);
            State = GameState.TurnDealt;
        }

        public void DealRiver() {

             if (State != GameState.TurnDealt)
                return;

            DealCommunityCards(1);
            State = GameState.RiverDealt;
            
            var royalFlush = new RoyalFlush(CommunityCards);
            var straigFlush = new StraightFlush(CommunityCards);

            // If the community cards deal a Royal flush or straight flush call the game;
            if(royalFlush.HandExists || royalFlush.HandExists) {
                CallGame();
            }

        }

        public void CallGame()
        {
            // Get all possible hands for each player
            foreach (var player in Players) {
                player.EvaluatePlayersCards(CommunityCards);
                // show all cards
                player.Cards.ForEach(x => x.CardOpen = true);
            }

            GetWinner();
            State = GameState.Called;
        }


        #endregion

        #region private methods
        private void DealCommunityCards(int numberOfCards) {
            
            // Move to method
            // Burn first card
            var burnCard = GameCards.Take(1);
            GameCards = GameCards.Except(burnCard).ToList();

            // Then deal the community cards - The Flop
            var communityCards = GameCards.Take(numberOfCards);
            CommunityCards.AddRange(communityCards);
            GameCards = GameCards.Except(communityCards).ToList();
            // Set community cards to open
            CommunityCards.ForEach(x => x.CardOpen = true);
        }

        // Map to array and back again for knuffShuffle
        private static List<Card> ShuffleCards(List<Card> cards)
        {
            return KnuthShuffle(cards.ToArray()).ToList();
        }

        // KnuthShuffle 
        // https://www.rosettacode.org/wiki/Knuth_shuffle
        private static T[] KnuthShuffle<T>(T[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                // Don't select from the entire array on subsequent loops
                int j = random.Next(i, array.Length);

                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return array;
        }

        // Determining the winner
        private void GetWinner()
        {

            // Get highest hand
            var highestHandType = Players.Select(x => x.HighestHand.HandType).Max();
            var highestHandPlayers = Players.Where(x => x.HighestHand.HandType == highestHandType).ToList();
            if (highestHandPlayers.Count() == 1)
			{
                highestHandPlayers.ForEach(x => x.HasWinningHand = true);
                return;
            }

            // If there are more than one player with the highest hand then use handvalue as tiebreaker
            var highestHandValue = highestHandPlayers.Select(y => y.HighestHand.HandValue).Max();

            highestHandPlayers.Where(x => x.HighestHand.HandValue == highestHandValue).ToList()
                .ForEach(x => x.HasWinningHand = true);

        }
    
        #endregion

    }
}