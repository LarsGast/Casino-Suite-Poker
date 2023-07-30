using Casino_Suite_Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.ExtentionClasses;
using Poker.WinningHands;
using System;
using System.Collections.Generic;
using System.Linq;
using Unit_Tests.Unit_Tests_Help_Methods;
using static Casino_Suite_Poker.Card;
using static Poker.WinningHands.PokerHand;

namespace Unit_Tests {
	[TestClass]
	public class UnitTestsHandIsCorrect {

		/// <summary>
		/// Tests whether the given straightflush.
		/// </summary>
		[TestMethod]
		public void handIsStraightFlush() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Ten, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.StraightFlush;
			CardValue? firstValue = null;
			CardValue? secondValue = null;
			Suit? suit = Suit.Hearts;
			var kickers = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Ten, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a four of a kind.
		/// </summary>
		[TestMethod]
		public void handIsFourOfAKind() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Clubs),
				new Card(CardValue.Ace, Suit.Diamonds),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.FourOfAKind;
			CardValue? firstValue = CardValue.Ace;
			CardValue? secondValue = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.King, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a full house.
		/// </summary>
		[TestMethod]
		public void handIsFullHouse() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.Ace, Suit.Clubs),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.FullHouse;
			CardValue? firstValue = CardValue.Ace;
			CardValue? secondValue = CardValue.King;
			Suit? suit = null;
			var kickers = new List<Card>();

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a flush.
		/// </summary>
		[TestMethod]
		public void handIsFlush() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Nine, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.Flush;
			CardValue? firstValue = null;
			CardValue? secondValue = null;
			Suit? suit = Suit.Hearts;
			var kickers = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Nine, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a straight.
		/// </summary>
		[TestMethod]
		public void handIsStraight() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Ten, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.Straight;
			CardValue? firstValue = null;
			CardValue? secondValue = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Ten, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a three of a kind.
		/// </summary>
		[TestMethod]
		public void handIsThreeOfAKind() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.Ace, Suit.Diamonds),
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.ThreeOfAKind;
			CardValue? firstValue = CardValue.Ace;
			CardValue? secondValue = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.Queen, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a two pair.
		/// </summary>
		[TestMethod]
		public void handIsTwoPair() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.King, Suit.Diamonds),
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.TwoPair;
			CardValue? firstValue = CardValue.Ace;
			CardValue? secondValue = CardValue.King;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.Queen, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a pair.
		/// </summary>
		[TestMethod]
		public void handIsPair() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.King, Suit.Diamonds),
				new Card(CardValue.Queen, Suit.Clubs),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.Pair;
			CardValue? firstValue = CardValue.Ace;
			CardValue? secondValue = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.King, Suit.Diamonds),
				new Card(CardValue.Queen, Suit.Clubs),
				new Card(CardValue.Jack, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a high card.
		/// </summary>
		[TestMethod]
		public void handIsHighCard() {

			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Nine, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handType = HandType.HighCard;
			CardValue? firstValue = null;
			CardValue? secondValue = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Nine, Suit.Hearts),
			};

			UnitTestHelpClass.assertHand(bestHand, handType, firstValue, secondValue, suit, kickers);
		}
	}
}
