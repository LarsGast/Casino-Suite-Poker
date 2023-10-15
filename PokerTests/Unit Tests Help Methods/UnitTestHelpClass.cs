using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.ExtensionClasses;
using static DeckOfCardsLibrary.Card;
using static PokerLibrary.PokerHand;

namespace PokerUnitTests.Unit_Tests_Help_Methods {
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
		internal static void assertHand(PokerHand hand, HandType handType, Rank? firstCardValue, Rank? secondCardValue, Suit? suit, IEnumerable<Card> kickers) {
			Assert.Multiple(() => {
				Assert.That(hand.handType, Is.EqualTo(handType), $"Hand is not a {handType}");
				Assert.That(hand.primaryCardRank, Is.EqualTo(firstCardValue), $"Highest value is not {firstCardValue}");
				Assert.That(hand.secondaryCardRank, Is.EqualTo(secondCardValue), $"Second highest value is not {secondCardValue}");
				Assert.That(hand.suit, Is.EqualTo(suit), $"Suit is not {suit}");
				Assert.That(hand.kickers.equals(kickers), Is.True, $"Kickers are not the same. Oberved: {hand.kickers.getDisplayString(displayTenAsT: true)}. Expected: {kickers.getDisplayString(displayTenAsT: true)}");
			});
		}

		internal static void assertThatHandWins(PokerHand winningHand, PokerHand losingHand) {
			Assert.That(winningHand.winsAgainst(losingHand), Is.True, 
				$"{winningHand.handType} does not win against {losingHand.handType}\n" +
				$"First value: {winningHand.primaryCardRank} vs. {losingHand.primaryCardRank}\n" +
				$"Second value: {winningHand.secondaryCardRank} vs. {losingHand.secondaryCardRank}\n" +
				$"Kickers: {winningHand.kickers.getDisplayString(displayTenAsT: true)} vs. {losingHand.kickers.getDisplayString(displayTenAsT: true)}");
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

			Assert.That(handThatWon, Is.EqualTo(winningHand), "Hand that should win did not win.\n" +
				"Hand that should win vs. hand that won.\n" + 
				$"{winningHand.handType} vs. {handThatWon.handType}\n" +
				$"First value: {winningHand.primaryCardRank} vs. {handThatWon.primaryCardRank}\n" +
				$"Second value: {winningHand.secondaryCardRank} vs. {handThatWon.secondaryCardRank}\n" +
				$"Kickers: {winningHand.kickers.getDisplayString(displayTenAsT: true)} vs. {handThatWon.kickers.getDisplayString(displayTenAsT: true)}"
				);
		}

		/// <summary>
		/// Gets the best StraightFlush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestStraightFlush() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Ten, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst StraightFlush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstStraightFlush() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Three, Suit.Hearts),
				new Card(Rank.Four, Suit.Hearts),
				new Card(Rank.Five, Suit.Hearts),
				new Card(Rank.Seven, Suit.Clubs),
				new Card(Rank.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best FourOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFourOfAKind() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.Ace, Suit.Diamonds),
				new Card(Rank.Ace, Suit.Clubs),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst FourOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFourOfAKind() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Two, Suit.Diamonds),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Hearts),
				new Card(Rank.Three, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best FullHouse as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFullHouse() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.Ace, Suit.Diamonds),
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst FullHouse as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFullHouse() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Two, Suit.Diamonds),
				new Card(Rank.Three, Suit.Clubs),
				new Card(Rank.Three, Suit.Hearts),
				new Card(Rank.Five, Suit.Clubs),
				new Card(Rank.Six, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Flush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestFlush() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Nine, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Flush as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstFlush() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Three, Suit.Hearts),
				new Card(Rank.Four, Suit.Hearts),
				new Card(Rank.Five, Suit.Hearts),
				new Card(Rank.Seven, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Straight as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestStraight() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Ten, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Straight as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstStraight() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Three, Suit.Diamonds),
				new Card(Rank.Four, Suit.Clubs),
				new Card(Rank.Five, Suit.Hearts),
				new Card(Rank.Seven, Suit.Clubs),
				new Card(Rank.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best ThreeOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestThreeOfAKind() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.Ace, Suit.Diamonds),
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst ThreeOfAKind as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstThreeOfAKind() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Two, Suit.Diamonds),
				new Card(Rank.Three, Suit.Clubs),
				new Card(Rank.Four, Suit.Hearts),
				new Card(Rank.Five, Suit.Clubs),
				new Card(Rank.Seven, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best TwoPair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestTwoPair() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.King, Suit.Diamonds),
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst TwoPair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstTwoPair() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Three, Suit.Diamonds),
				new Card(Rank.Three, Suit.Clubs),
				new Card(Rank.Four, Suit.Hearts),
				new Card(Rank.Five, Suit.Clubs),
				new Card(Rank.Seven, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best Pair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestPair() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.King, Suit.Diamonds),
				new Card(Rank.Queen, Suit.Clubs),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst Pair as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstPair() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Two, Suit.Spades),
				new Card(Rank.Three, Suit.Diamonds),
				new Card(Rank.Four, Suit.Clubs),
				new Card(Rank.Five, Suit.Hearts),
				new Card(Rank.Seven, Suit.Clubs),
				new Card(Rank.Eight, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the best HighCard as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getBestHighCard() {
			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Nine, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}

		/// <summary>
		/// Gets the worst HighCard as a PokerHand.
		/// </summary>
		/// <returns></returns>
		internal static PokerHand getWorstHighCard() {
			var cards = new List<Card>() {
				new Card(Rank.Two, Suit.Hearts),
				new Card(Rank.Three, Suit.Spades),
				new Card(Rank.Four, Suit.Diamonds),
				new Card(Rank.Five, Suit.Clubs),
				new Card(Rank.Seven, Suit.Hearts),
				new Card(Rank.Eight, Suit.Clubs),
				new Card(Rank.Nine, Suit.Spades)
			};

			return PokerHand.getBestHand(cards);
		}
	}
}
