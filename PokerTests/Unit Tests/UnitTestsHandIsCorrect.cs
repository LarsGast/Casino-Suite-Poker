using DeckOfCardsLibrary;
using PokerLibrary;
using PokerUnitTests.Unit_Tests_Help_Methods;
using static DeckOfCardsLibrary.Card;
using static PokerLibrary.PokerHand;

namespace PokerUnitTests {
	public class UnitTestsHandIsCorrect {

		/// <summary>
		/// Tests whether the given straightflush.
		/// </summary>
		[Test]
		public void handIsStraightFlush() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Ten, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.StraightFlush;
			Rank? primaryCardRank = null;
			Rank? secondaryCardRank = null;
			Suit? suit = Suit.Hearts;
			var kickers = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Ten, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a four of a kind.
		/// </summary>
		[Test]
		public void handIsFourOfAKind() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Clubs),
				new Card(Rank.Ace, Suit.Diamonds),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.FourOfAKind;
			Rank? primaryCardRank = Rank.Ace;
			Rank? secondaryCardRank = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.King, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a full house.
		/// </summary>
		[Test]
		public void handIsFullHouse() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.Ace, Suit.Clubs),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.FullHouse;
			Rank? primaryCardRank = Rank.Ace;
			Rank? secondaryCardRank = Rank.King;
			Suit? suit = null;
			var kickers = new List<Card>();

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a flush.
		/// </summary>
		[Test]
		public void handIsFlush() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Nine, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.Flush;
			Rank? primaryCardRank = null;
			Rank? secondaryCardRank = null;
			Suit? suit = Suit.Hearts;
			var kickers = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Hearts),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Nine, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a straight.
		/// </summary>
		[Test]
		public void handIsStraight() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Ten, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.Straight;
			Rank? primaryCardRank = null;
			Rank? secondaryCardRank = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Ten, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a three of a kind.
		/// </summary>
		[Test]
		public void handIsThreeOfAKind() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.Ace, Suit.Diamonds),
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.ThreeOfAKind;
			Rank? primaryCardRank = Rank.Ace;
			Rank? secondaryCardRank = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.Queen, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a two pair.
		/// </summary>
		[Test]
		public void handIsTwoPair() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.King, Suit.Diamonds),
				new Card(Rank.King, Suit.Clubs),
				new Card(Rank.Queen, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.TwoPair;
			Rank? primaryCardRank = Rank.Ace;
			Rank? secondaryCardRank = Rank.King;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.Queen, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a pair.
		/// </summary>
		[Test]
		public void handIsPair() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.Ace, Suit.Spades),
				new Card(Rank.King, Suit.Diamonds),
				new Card(Rank.Queen, Suit.Clubs),
				new Card(Rank.Jack, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.Pair;
			Rank? primaryCardRank = Rank.Ace;
			Rank? secondaryCardRank = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.King, Suit.Diamonds),
				new Card(Rank.Queen, Suit.Clubs),
				new Card(Rank.Jack, Suit.Hearts)
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}

		/// <summary>
		/// Tests whether the given hand is a high card.
		/// </summary>
		[Test]
		public void handIsHighCard() {

			var cards = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Nine, Suit.Hearts),
				new Card(Rank.Two, Suit.Clubs),
				new Card(Rank.Three, Suit.Spades)
			};

			var bestHand = PokerHand.getBestHand(cards);

			var handRank = HandRank.HighCard;
			Rank? primaryCardRank = null;
			Rank? secondaryCardRank = null;
			Suit? suit = null;
			var kickers = new List<Card>() {
				new Card(Rank.Ace, Suit.Hearts),
				new Card(Rank.King, Suit.Spades),
				new Card(Rank.Queen, Suit.Diamonds),
				new Card(Rank.Jack, Suit.Clubs),
				new Card(Rank.Nine, Suit.Hearts),
			};

			UnitTestHelpClass.assertHand(bestHand, handRank, primaryCardRank, secondaryCardRank, suit, kickers);
		}
	}
}
