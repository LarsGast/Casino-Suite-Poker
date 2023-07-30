using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
			var worstStraightFlush = UnitTestHelpClass.getWorstStraightFlush();
			var bestFourOfAKind = UnitTestHelpClass.getBestFourOfAKind();
			var bestFullHouse = UnitTestHelpClass.getBestFullHouse();
			var bestFlush = UnitTestHelpClass.getBestFlush();
			var bestStraight = UnitTestHelpClass.getBestStraight();
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, worstStraightFlush);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestFourOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestFullHouse);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestFlush);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestStraight);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestStraightFlush, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best FourOfAKind always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void fourOfAKindWins() {

			// This hand should win agains all other hands in this method.
			var bestFourOfAKind = UnitTestHelpClass.getBestFourOfAKind();

			// All hands below should lose against the hand specified above.
			var worstFourOfAKind = UnitTestHelpClass.getWorstFourOfAKind();
			var bestFullHouse = UnitTestHelpClass.getBestFullHouse();
			var bestFlush = UnitTestHelpClass.getBestFlush();
			var bestStraight = UnitTestHelpClass.getBestStraight();
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, worstFourOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestFullHouse);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestFlush);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestStraight);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestFourOfAKind, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best FullHouse always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void fullHouseWins() {

			// This hand should win agains all other hands in this method.
			var bestFullHouse = UnitTestHelpClass.getBestFullHouse();

			// All hands below should lose against the hand specified above.
			var worstFullHouse = UnitTestHelpClass.getWorstFullHouse();
			var bestFlush = UnitTestHelpClass.getBestFlush();
			var bestStraight = UnitTestHelpClass.getBestStraight();
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, worstFullHouse);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestFlush);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestStraight);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestFullHouse, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best Flush always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void flushWins() {

			// This hand should win agains all other hands in this method.
			var bestFlush = UnitTestHelpClass.getBestFlush();

			// All hands below should lose against the hand specified above.
			var worstFlush = UnitTestHelpClass.getWorstFlush();
			var bestStraight = UnitTestHelpClass.getBestStraight();
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestFlush, worstFlush);
			UnitTestHelpClass.assertThatHandWins(bestFlush, bestStraight);
			UnitTestHelpClass.assertThatHandWins(bestFlush, bestThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestFlush, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestFlush, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestFlush, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best Straight always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void straightWins() {

			// This hand should win agains all other hands in this method.
			var bestStraight = UnitTestHelpClass.getBestStraight();

			// All hands below should lose against the hand specified above.
			var worstStraight = UnitTestHelpClass.getWorstStraight();
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestStraight, worstStraight);
			UnitTestHelpClass.assertThatHandWins(bestStraight, bestThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestStraight, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestStraight, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestStraight, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best ThreeOfAKind always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void threeOfAKindWins() {

			// This hand should win agains all other hands in this method.
			var bestThreeOfAKind = UnitTestHelpClass.getBestThreeOfAKind();

			// All hands below should lose against the hand specified above.
			var worstThreeOfAKind = UnitTestHelpClass.getWorstThreeOfAKind();
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestThreeOfAKind, worstThreeOfAKind);
			UnitTestHelpClass.assertThatHandWins(bestThreeOfAKind, bestTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestThreeOfAKind, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestThreeOfAKind, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best TwoPair always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void twoPairWins() {

			// This hand should win agains all other hands in this method.
			var bestTwoPair = UnitTestHelpClass.getBestTwoPair();

			// All hands below should lose against the hand specified above.
			var worstTwoPair = UnitTestHelpClass.getWorstTwoPair();
			var bestPair = UnitTestHelpClass.getBestPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestTwoPair, worstTwoPair);
			UnitTestHelpClass.assertThatHandWins(bestTwoPair, bestPair);
			UnitTestHelpClass.assertThatHandWins(bestTwoPair, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best Pair always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void pairWins() {

			// This hand should win agains all other hands in this method.
			var bestPair = UnitTestHelpClass.getBestPair();

			// All hands below should lose against the hand specified above.
			var worstPair = UnitTestHelpClass.getWorstPair();
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestPair, worstPair);
			UnitTestHelpClass.assertThatHandWins(bestPair, bestHighCard);
		}

		/// <summary>
		/// Asserts that the best HighCard always wins against inferior hands.
		/// </summary>
		[TestMethod]
		public void highCardWins() {

			// This hand should win agains all other hands in this method.
			var bestHighCard = UnitTestHelpClass.getBestHighCard();

			// All hands below should lose against the hand specified above.
			var worstHighCard = UnitTestHelpClass.getWorstHighCard();

			// Assert that the best hand wins.
			UnitTestHelpClass.assertThatHandWins(bestHighCard, worstHighCard);
		}
	}
}
