using Casino_Suite_Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.WinningHands;
using System;
using static Casino_Suite_Poker.Card;
using static Poker.WinningHands.PokerHand;
using System.Collections.Generic;
using Poker.ExtentionClasses;

namespace Unit_Tests.Unit_Tests_Help_Methods {
	internal static class UnitTestHelpClass {

		/// <summary>
		/// Help function to assert a PokerHand.
		/// </summary>
		/// <param name="hand">Observed hand</param>
		/// <param name="handType">Expected handType</param>
		/// <param name="firstCardValue">Expected firstCardValue</param>
		/// <param name="secondCardValue">Expected secondCardValue</param>
		/// <param name="suit">Expected suit</param>
		/// <param name="kickers">Expected kickers</param>
		internal static void assertHand(PokerHand hand, HandType handType, CardValue? firstCardValue, CardValue? secondCardValue, Suit? suit, IEnumerable<Card> kickers) {
			Assert.IsTrue(hand.handType == handType, $"Hand is not a {handType}");
			Assert.IsTrue(hand.firstCardValue == firstCardValue, $"Highest value is not {firstCardValue}");
			Assert.IsTrue(hand.secondCardValue == secondCardValue, $"Second highest value is not {secondCardValue}");
			Assert.IsTrue(hand.suit == suit, $"Suit is not {suit}");
			Assert.IsTrue(hand.kickers.equals(kickers), $"Kickers are not the same. Oberved: {hand.kickers.getDisplayString()}. Expected: {kickers.getDisplayString()}");
		}

		internal static void assertThatHandWins(PokerHand hand, PokerHand other) {
			Assert.IsTrue(hand.winsAgainst(other), 
				$"{hand.handType} does not win against {other.handType}\n" +
				$"First value: {hand.firstCardValue}\n" +
				$"Second value: {hand.secondCardValue}\n" +
				$"Kickers: {hand.kickers.getDisplayString()}");
		}

		/// <summary>
		/// Gets the best StraightFlush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestStraightFlush() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Ten, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best StraightFlush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstStraightFlush() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Three, Suit.Hearts),
				new Card(CardValue.Four, Suit.Hearts),
				new Card(CardValue.Five, Suit.Hearts),
				new Card(CardValue.Seven, Suit.Clubs),
				new Card(CardValue.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best FourOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFourOfAKind() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.Ace, Suit.Diamonds),
				new Card(CardValue.Ace, Suit.Clubs),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best FourOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFourOfAKind() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Two, Suit.Diamonds),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Hearts),
				new Card(CardValue.Three, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}
	}
}
