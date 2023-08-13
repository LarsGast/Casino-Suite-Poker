using System.Collections.Generic;
using Casino_Suite_Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.ExtentionClasses;
using Poker.WinningHands;
using static Casino_Suite_Poker.Card;
using static Poker.WinningHands.PokerHand;

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

		internal static void assertThatHandWins(PokerHand winningHand, PokerHand losingHand) {
			Assert.IsTrue(winningHand.winsAgainst(losingHand), 
				$"{winningHand.handType} does not win against {losingHand.handType}\n" +
				$"First value: {winningHand.firstCardValue} vs. {losingHand.firstCardValue}\n" +
				$"Second value: {winningHand.secondCardValue} vs. {losingHand.secondCardValue}\n" +
				$"Kickers: {winningHand.kickers.getDisplayString()} vs. {losingHand.kickers.getDisplayString()}");
		}

		/// <summary>
		/// Asserts that the given hand wins against the given losinghands.
		/// </summary>
		/// <param name="winningHand"></param>
		/// <param name="losingHands"></param>
		internal static void assertThatHandWins(PokerHand winningHand, IEnumerable<PokerHand> losingHands) {

			var allhands = new List<PokerHand>() { winningHand };
			allhands.AddRange(losingHands);

			var handThatWon = PokerHand.getWinningHand(allhands);

			Assert.IsTrue(handThatWon == winningHand, "Hand that should win did not win.\n" +
				"Hand that should win vs. hand that won.\n" + 
				$"{winningHand.handType} vs. {handThatWon.handType}\n" +
				$"First value: {winningHand.firstCardValue} vs. {handThatWon.firstCardValue}\n" +
				$"Second value: {winningHand.secondCardValue} vs. {handThatWon.secondCardValue}\n" +
				$"Kickers: {winningHand.kickers.getDisplayString()} vs. {handThatWon.kickers.getDisplayString()}"
				);
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
		/// Gets the worst StraightFlush as a PokerHand.
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
		/// Gets the worst FourOfAKind as a PokerHand.
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

		/// <summary>
		/// Gets the best FullHouse as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFullHouse() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.Ace, Suit.Diamonds),
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst FullHouse as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFullHouse() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Two, Suit.Diamonds),
				new Card(CardValue.Three, Suit.Clubs),
				new Card(CardValue.Three, Suit.Hearts),
				new Card(CardValue.Five, Suit.Clubs),
				new Card(CardValue.Six, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Flush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFlush() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Hearts),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Nine, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Flush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFlush() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Three, Suit.Hearts),
				new Card(CardValue.Four, Suit.Hearts),
				new Card(CardValue.Five, Suit.Hearts),
				new Card(CardValue.Seven, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Straight as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestStraight() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Ten, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Straight as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstStraight() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Three, Suit.Diamonds),
				new Card(CardValue.Four, Suit.Clubs),
				new Card(CardValue.Five, Suit.Hearts),
				new Card(CardValue.Seven, Suit.Clubs),
				new Card(CardValue.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best ThreeOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestThreeOfAKind() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.Ace, Suit.Diamonds),
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst ThreeOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstThreeOfAKind() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Two, Suit.Diamonds),
				new Card(CardValue.Three, Suit.Clubs),
				new Card(CardValue.Four, Suit.Hearts),
				new Card(CardValue.Five, Suit.Clubs),
				new Card(CardValue.Seven, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best TwoPair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestTwoPair() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.King, Suit.Diamonds),
				new Card(CardValue.King, Suit.Clubs),
				new Card(CardValue.Queen, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst TwoPair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstTwoPair() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Three, Suit.Diamonds),
				new Card(CardValue.Three, Suit.Clubs),
				new Card(CardValue.Four, Suit.Hearts),
				new Card(CardValue.Five, Suit.Clubs),
				new Card(CardValue.Seven, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Pair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestPair() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.Ace, Suit.Spades),
				new Card(CardValue.King, Suit.Diamonds),
				new Card(CardValue.Queen, Suit.Clubs),
				new Card(CardValue.Jack, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Pair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstPair() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Two, Suit.Spades),
				new Card(CardValue.Three, Suit.Diamonds),
				new Card(CardValue.Four, Suit.Clubs),
				new Card(CardValue.Five, Suit.Hearts),
				new Card(CardValue.Seven, Suit.Clubs),
				new Card(CardValue.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best HighCard as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestHighCard() {
			var cards = new List<Card>() {
				new Card(CardValue.Ace, Suit.Hearts),
				new Card(CardValue.King, Suit.Spades),
				new Card(CardValue.Queen, Suit.Diamonds),
				new Card(CardValue.Jack, Suit.Clubs),
				new Card(CardValue.Nine, Suit.Hearts),
				new Card(CardValue.Two, Suit.Clubs),
				new Card(CardValue.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst HighCard as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstHighCard() {
			var cards = new List<Card>() {
				new Card(CardValue.Two, Suit.Hearts),
				new Card(CardValue.Three, Suit.Spades),
				new Card(CardValue.Four, Suit.Diamonds),
				new Card(CardValue.Five, Suit.Clubs),
				new Card(CardValue.Seven, Suit.Hearts),
				new Card(CardValue.Eight, Suit.Clubs),
				new Card(CardValue.Nine, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}
	}
}
