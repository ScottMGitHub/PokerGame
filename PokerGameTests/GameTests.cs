using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Enums;
using PokerGame.Models;
using PokerGame.Models.PokerHand;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameTests
{
	[TestClass]
	public class GameTests
	{
		// https://howtoplaypokerinfo.com/kicker/


		#region Call Game Tests - Winning Hand

		[TestMethod]
		public void CallGame_HighestHandTieBeatsHighestHandValues_Player2Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.Five),
					new Card(CardSuit.Hearts, CardRank.Six),
					new Card(CardSuit.Diamonds, CardRank.Six),
					new Card(CardSuit.Hearts, CardRank.Jack),
					new Card(CardSuit.Spades, CardRank.Seven),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Two),
							new Card(CardSuit.Spades, CardRank.Jack),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Seven),
							new Card(CardSuit.Clubs, CardRank.Jack),
						}
					},
					new Player
					{
						Number = 3,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.King),
							new Card(CardSuit.Spades, CardRank.Nine),
						}
					},
					new Player
					{
						Number = 4,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ten),
							new Card(CardSuit.Hearts, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsFalse(game.Players[0].HasWinningHand);
			Assert.IsTrue(game.Players[1].HasWinningHand);
			Assert.IsFalse(game.Players[2].HasWinningHand);
			Assert.IsFalse(game.Players[3].HasWinningHand);

		}


		#endregion

		#region Call Game Tie Breaker Tests - High Card

		[TestMethod]
		public void CallGame_HighCardTie_Player1KickerWins()
		{

			// Arrange
			var game = new Game() {
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.King),
					new Card(CardSuit.Clubs, CardRank.Queen),
					new Card(CardSuit.Diamonds, CardRank.Eight),
					new Card(CardSuit.Spades, CardRank.Two),
					new Card(CardSuit.Spades, CardRank.Three),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Spades, CardRank.Ace),
							new Card(CardSuit.Clubs, CardRank.Nine),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Five),
						}
					},
					new Player
					{
						Number = 3,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Four),
							new Card(CardSuit.Diamonds, CardRank.Five),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Take(2).Any(x => x.HasWinningHand));


		}

		[TestMethod]
		public void CallGame_HighCardTie_BothPlayer1And2KickersWin()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Diamonds, CardRank.King),
					new Card(CardSuit.Clubs, CardRank.Queen),
					new Card(CardSuit.Diamonds, CardRank.Nine),
					new Card(CardSuit.Spades, CardRank.Eight),
					new Card(CardSuit.Spades, CardRank.Three),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Seven),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Ace),
							new Card(CardSuit.Hearts, CardRank.Five),
						}
					},
					new Player
					{
						Number = 3,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Four),
							new Card(CardSuit.Diamonds, CardRank.Five),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.Take(2).All(x => x.HasWinningHand));
			Assert.IsFalse(game.Players.Last().HasWinningHand);


		}


		#endregion

		#region Call Game Tie Breaker Tests - One Pair

		[TestMethod]
		public void CallGame_OnePairTie_Player1KickerWins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Diamonds, CardRank.Ten),
					new Card(CardSuit.Spades, CardRank.Seven),
					new Card(CardSuit.Spades, CardRank.Five),
					new Card(CardSuit.Diamonds, CardRank.Two),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Ace),
							new Card(CardSuit.Hearts, CardRank.King),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Queen),
						}
					},
					new Player
					{
						Number = 3,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Four),
							new Card(CardSuit.Diamonds, CardRank.Five),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Take(2).Any(x => x.HasWinningHand));


		}

		#endregion

		#region Call Game Tie Breaker Tests - Two Pair


		[TestMethod]
		public void CallGame_TwoPairTie_Player1KickerWins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Diamonds, CardRank.Ten),
					new Card(CardSuit.Spades, CardRank.Seven),
					new Card(CardSuit.Spades, CardRank.Five),
					new Card(CardSuit.Diamonds, CardRank.Five),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Ace),
							new Card(CardSuit.Hearts, CardRank.King),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Queen),
						}
					},
					new Player
					{
						Number = 3,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Four),
							new Card(CardSuit.Diamonds, CardRank.Two),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Take(2).Any(x => x.HasWinningHand));


		}


		[TestMethod]
		public void CallGame_TwoPairTie_SplitPot()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.Nine),
					new Card(CardSuit.Clubs, CardRank.Nine),
					new Card(CardSuit.Diamonds, CardRank.Jack),
					new Card(CardSuit.Clubs, CardRank.Four),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Ace),
							new Card(CardSuit.Diamonds, CardRank.Two),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Seven),
						}
					},
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.All(x => x.HasWinningHand));


		}


		#endregion

		#region Call Game Tie Breaker Tests - Three of a kind

		[TestMethod]
		public void CallGame_ThreeOfAKindTie_Player1KickerWins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Spades, CardRank.Ace),
					new Card(CardSuit.Spades, CardRank.Seven),
					new Card(CardSuit.Hearts, CardRank.Five),
					new Card(CardSuit.Diamonds, CardRank.Two),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Ace),
							new Card(CardSuit.Hearts, CardRank.King),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ace),
							new Card(CardSuit.Clubs, CardRank.Queen),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game Tie Breaker Tests - Straight

		[TestMethod]
		public void CallGame_StraightTie_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Diamonds, CardRank.Eight),
					new Card(CardSuit.Clubs, CardRank.Seven),
					new Card(CardSuit.Diamonds, CardRank.Six),
					new Card(CardSuit.Spades, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.King),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Nine),
							new Card(CardSuit.Hearts, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Five),
							new Card(CardSuit.Clubs, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game Tie Breaker Tests - Flush

		[TestMethod]
		public void CallGame_FlushTie_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.Eight),
					new Card(CardSuit.Hearts, CardRank.Seven),
					new Card(CardSuit.Hearts, CardRank.Six),
					new Card(CardSuit.Hearts, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.King),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Two),
							new Card(CardSuit.Hearts, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Three),
							new Card(CardSuit.Hearts, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game Tie Breaker Tests - Flush

		[TestMethod]
		public void CallGame_FullHouseTie_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.Eight),
					new Card(CardSuit.Spades, CardRank.Eight),
					new Card(CardSuit.Diamonds, CardRank.Eight),
					new Card(CardSuit.Hearts, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.King),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Ace),
							new Card(CardSuit.Hearts, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.King),
							new Card(CardSuit.Hearts, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game Tie Breaker Tests - Four Of A Kind

		[TestMethod]
		public void CallGame_FourOfAKindTie_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Hearts, CardRank.Eight),
					new Card(CardSuit.Spades, CardRank.Eight),
					new Card(CardSuit.Diamonds, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.Ace),
					new Card(CardSuit.Hearts, CardRank.King),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Ace),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Eight),
							new Card(CardSuit.Hearts, CardRank.Eight),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game Tie Breaker Tests - Straight Flush

		[TestMethod]
		public void CallGame_StraightFlushTie_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Eight),
					new Card(CardSuit.Clubs, CardRank.Seven),
					new Card(CardSuit.Clubs, CardRank.Six),
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Clubs, CardRank.King),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Nine),
							new Card(CardSuit.Clubs, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Clubs, CardRank.Five),
							new Card(CardSuit.Clubs, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}

		#endregion

		#region Call Game - Royal Flush 

		[TestMethod]
		public void CallGame_RoyalFlushCommunityCards_SplitPot()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Clubs, CardRank.King),
					new Card(CardSuit.Clubs, CardRank.Queen),
					new Card(CardSuit.Clubs, CardRank.Jack),
					new Card(CardSuit.Clubs, CardRank.Ten),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Nine),
							new Card(CardSuit.Hearts, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Five),
							new Card(CardSuit.Hearts, CardRank.Four),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.All(x => x.HasWinningHand));

		}

		[TestMethod]
		public void CallGame_RoyalFlushBeatsAllHands_Player1Wins()
		{

			// Arrange
			var game = new Game()
			{
				State = GameState.RiverDealt,

				CommunityCards = new List<Card>() {
					new Card(CardSuit.Clubs, CardRank.Ace),
					new Card(CardSuit.Clubs, CardRank.King),
					new Card(CardSuit.Clubs, CardRank.Queen),
					new Card(CardSuit.Clubs, CardRank.Jack),
					new Card(CardSuit.Hearts, CardRank.Ace),
				},

				Players = new List<Player>() {
					new Player
					{
						Number = 1,
						Cards = new List<Card>(){
							new Card(CardSuit.Hearts, CardRank.Nine),
							new Card(CardSuit.Clubs, CardRank.Ten),
						}
					},
					new Player
					{
						Number = 2,
						Cards = new List<Card>(){
							new Card(CardSuit.Diamonds, CardRank.Ace),
							new Card(CardSuit.Spades, CardRank.Ace),
						}
					}
				}
			};


			// Act
			game.CallGame();

			// Assert
			Assert.IsTrue(game.Players.FirstOrDefault().HasWinningHand);
			Assert.IsFalse(game.Players.Skip(1).Any(x => x.HasWinningHand));

		}


		#endregion
	
	
	}
}
