using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame.Enums;
using PokerGame.Models;
using PokerGame.Models.PokerHand;
using System.Collections.Generic;
using System.Linq;

namespace PokerGameTests
{
	[TestClass]
	public class HandTests
	{

		#region Royal Flush

		[TestMethod]
		public void Hand_CheckIsRoyalFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Ace),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.King),
				new Card(CardSuit.Diamonds, CardRank.Five),
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Queen),	
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new RoyalFlush(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);


		}

		[TestMethod]
		public void Hand_SameSuitNotCorrectRank_CheckIsNotRoyalFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Nine),
				new Card(CardSuit.Hearts, CardRank.Ace),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.King),
				new Card(CardSuit.Diamonds, CardRank.Five),
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Queen),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new RoyalFlush(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}

		#endregion

		#region Straight Flush

		[TestMethod]
		public void Hand_AceToFiveOfHearts_CheckIsStraightFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ace),
				new Card(CardSuit.Hearts, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new StraightFlush(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_NineToKingOfHearts_CheckIsStraightFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Nine),
				new Card(CardSuit.Hearts, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ten),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Queen),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);


			// Act
			var hand = new StraightFlush(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_ConsecutiveNotSameSuit_CheckIsNotStraightFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Nine),
				new Card(CardSuit.Diamonds, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ten),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Hearts, CardRank.King),
				new Card(CardSuit.Diamonds, CardRank.Queen),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new StraightFlush(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}

		[TestMethod]
		public void Hand_SameSuitNotConsecutive_CheckIsNotStraightFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Nine),
				new Card(CardSuit.Hearts, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ten),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Hearts, CardRank.Five),
				new Card(CardSuit.Diamonds, CardRank.Queen),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new StraightFlush(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}

		[TestMethod]
		public void Hand_LastCardKingNoAce_CheckIsNotStraightFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Jack),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.King),
				new Card(CardSuit.Diamonds, CardRank.Queen),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new StraightFlush(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}


		#endregion

		#region Four of a kind

		[TestMethod]
		public void Hand_CheckIsFourOfAKind()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Seven),
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new FourOfAKind(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_CheckIsNotFourOfAKind()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Seven),
				new Card(CardSuit.Hearts, CardRank.Eight),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new FourOfAKind(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);
		}

		#endregion

		#region Full house

		[TestMethod]
		public void Hand_CheckIsFullHouse()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.Jack),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.King),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new FullHouse(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_CheckIsNotFullHouse()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.Jack),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.King),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new FullHouse(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);
		}

		#endregion

		#region Flush

		[TestMethod]
		public void Hand_AceToFiveOffHearts_CheckIsFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ace),
				new Card(CardSuit.Hearts, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Flush(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_AceToFiveOffHearts_CheckIsNotFlush()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Ace),
				new Card(CardSuit.Hearts, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Diamonds, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Flush(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);
		}

		#endregion

		#region Straight

		[TestMethod]
		public void Hand_AceToFive_CheckIsStraight()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Spades, CardRank.Ace),
				new Card(CardSuit.Diamonds, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Straight(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_FourTo_CheckIsStraight()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Spades, CardRank.Nine),
				new Card(CardSuit.Diamonds, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Spades, CardRank.Five),
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.Six),
				new Card(CardSuit.Clubs, CardRank.Jack),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Straight(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);
		}

		[TestMethod]
		public void Hand_AceToFive_CheckIsNotStraight()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Spades, CardRank.Ace),
				new Card(CardSuit.Diamonds, CardRank.Four),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Diamonds, CardRank.Ten),
				new Card(CardSuit.Hearts, CardRank.Three),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Four),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Straight(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);
		}

		#endregion

		#region Three of a kind

		[TestMethod]
		public void Hand_CheckIsThreeOfAKind()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Seven),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new ThreeOfAKind(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);

		}

		[TestMethod]
		public void Hand_CheckIsNotThreeOfAKind()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Four),
				new Card(CardSuit.Hearts, CardRank.Eight),
				new Card(CardSuit.Spades, CardRank.King),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new ThreeOfAKind(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);
		}

		#endregion

		#region Two Pair

		[TestMethod]
		public void Hand_CheckIsTwoPair()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Four),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.King),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new TwoPair(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);

		}


		[TestMethod]
		public void Hand_CheckIsNotTwoPair()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Four),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new TwoPair(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}

		#endregion

		#region Pair

		[TestMethod]
		public void Hand_CheckIsPair()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Seven),
				new Card(CardSuit.Diamonds, CardRank.Four),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Pair(cardsInPlay);

			// Assert
			Assert.IsTrue(hand.HandExists);

		}

		[TestMethod]
		public void Hand_CheckIsNotPair()
		{
			// Arrange
			List<Card> playerCards = new List<Card>() {
				new Card(CardSuit.Hearts, CardRank.Seven),
				new Card(CardSuit.Clubs, CardRank.King),
			};

			List<Card> communityCards = new List<Card>() {
				new Card(CardSuit.Clubs, CardRank.Four),
				new Card(CardSuit.Diamonds, CardRank.Three),
				new Card(CardSuit.Hearts, CardRank.Two),
				new Card(CardSuit.Spades, CardRank.Queen),
				new Card(CardSuit.Hearts, CardRank.Five),
			};

			var cardsInPlay = new List<Card>(communityCards);
			cardsInPlay.AddRange(playerCards);

			// Act
			var hand = new Pair(cardsInPlay);

			// Assert
			Assert.IsFalse(hand.HandExists);

		}

		#endregion

	}
}
