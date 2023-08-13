using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.WinningHands;
using System;
using System.Collections.Generic;
using Unit_Tests.Unit_Tests_Help_Methods;

namespace Unit_Tests.Unit_Tests {
	[TestClass]
	public class UnitTestsHandsWin {

		/// <summary>
		/// Asserts that the best StraightFlush always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void straightFlushWins() {

			// This hand should win agains all other hands in this method.
			var bestStraightFlush = UnitTestHelpClass.getBestStraightFlush();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstStraightFlush(),
				UnitTestHelpClass.getBestFourOfAKind(),
				UnitTestHelpClass.getBestFullHouse(),
				UnitTestHelpClass.getBestFlush(),
				UnitTestHelpClass.getBestStraight(),
				UnitTestHelpClass.getBestThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};
			
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, losingHands);
		}

		/// <summary>
		/// Asserts that the best FourOfAKind always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void fourOfAKindWins() {

			// This hand should win agains all other hands in this method.
			var bestFourOfAKind = UnitTestHelpClass.getBestFourOfAKind();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstFourOfAKind(),
				UnitTestHelpClass.getBestFullHouse(),
				UnitTestHelpClass.getBestFlush(),
				UnitTestHelpClass.getBestStraight(),
				UnitTestHelpClass.getBestThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, losingHands);
		}

		/// <summary>
		/// Asserts that the best FullHouse always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void fullHouseWins() {

			// This hand should win agains all other hands in this method.
			var bestFullHouse = UnitTestHelpClass.getBestFullHouse();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstFullHouse(),
				UnitTestHelpClass.getBestFlush(),
				UnitTestHelpClass.getBestStraight(),
				UnitTestHelpClass.getBestThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestFullHouse, losingHands);
		}

		/// <summary>
		/// Asserts that the best Flush always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void flushWins() {

			// This hand should win agains all other hands in this method.
			var bestFlush = UnitTestHelpClass.getBestFlush();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstFlush(),
				UnitTestHelpClass.getBestStraight(),
				UnitTestHelpClass.getBestThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestFlush, losingHands);
		}

		/// <summary>
		/// Asserts that the best Straight always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void straightWins() {

			// This hand should win agains all other hands in this method.
			var bestStraight = UnitTestHelpClass.getBestStraight();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstStraight(),
				UnitTestHelpClass.getBestThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestStraight, losingHands);
		}

		/// <summary>
		/// Asserts that the best ThreeOfAKind always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void threeOfAKindWins() {

			// This hand should win agains all other hands in this method.
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstThreeOfAKind(),
				UnitTestHelpClass.getBestTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestThreeOfAKind, losingHands);
		}

		/// <summary>
		/// Asserts that the best TwoPair always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void twoPairWins() {

			// This hand should win agains all other hands in this method.
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstTwoPair(),
				UnitTestHelpClass.getBestPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestTwoPair, losingHands);
		}

		/// <summary>
		/// Asserts that the best Pair always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void pairWins() {

			// This hand should win agains all other hands in this method.
			var bestPair = UnitTestHelpClass.getBestPair();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstPair(),
				UnitTestHelpClass.getBestHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestPair, losingHands);
		}

		/// <summary>
		/// Asserts that the best HighCard always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void highCardWins() {

			// This hand should win agains all other hands in this method.
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// All hands below should lose against the hand specified above.
			var losingHands = new List<PokerHand>() {
				UnitTestHelpClass.getWorstHighCard()
			};

			UnitTestHelpClass.assertThatHandWins(bestHighCard, losingHands);
		}
	}
}
