using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.Enums;
using PokerUnitTests.Unit_Tests_Help_Methods;
using static DeckOfCardsLibrary.Card;

namespace PokerUnitTests
{
    /// <summary>
    /// Contains unit tests to verify the correctness of Poker hands evaluation.
    /// </summary>
    public class UnitTestsHandIsCorrect
    {
        /// <summary>
        /// Tests whether the given hand is a straight flush.
        /// </summary>
        [Test]
        public void HandIsStraightFlush()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Hearts),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Jack, Suit.Hearts),
                new(Rank.Ten, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.StraightFlush;
            Rank? primaryCardRank = null;
            Rank? secondaryCardRank = null;
            Suit? suit = Suit.Hearts;
            var kickers = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Hearts),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Jack, Suit.Hearts),
                new(Rank.Ten, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a four of a kind.
        /// </summary>
        [Test]
        public void HandIsFourOfAKind()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Clubs),
                new(Rank.Ace, Suit.Diamonds),
                new(Rank.Ace, Suit.Spades),
                new(Rank.King, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.FourOfAKind;
            Rank? primaryCardRank = Rank.Ace;
            Rank? secondaryCardRank = null;
            Suit? suit = null;
            var kickers = new List<Card>() { new(Rank.King, Suit.Hearts) };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a full house.
        /// </summary>
        [Test]
        public void HandIsFullHouse()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.Ace, Suit.Clubs),
                new(Rank.King, Suit.Hearts),
                new(Rank.King, Suit.Spades),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.FullHouse;
            Rank? primaryCardRank = Rank.Ace;
            Rank? secondaryCardRank = Rank.King;
            Suit? suit = null;
            var kickers = new List<Card>();

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a flush.
        /// </summary>
        [Test]
        public void HandIsFlush()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Hearts),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Jack, Suit.Hearts),
                new(Rank.Nine, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Flush;
            Rank? primaryCardRank = null;
            Rank? secondaryCardRank = null;
            Suit? suit = Suit.Hearts;
            var kickers = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Hearts),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Jack, Suit.Hearts),
                new(Rank.Nine, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a straight.
        /// </summary>
        [Test]
        public void HandIsStraight()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Spades),
                new(Rank.Queen, Suit.Diamonds),
                new(Rank.Jack, Suit.Clubs),
                new(Rank.Ten, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Straight;
            Rank? primaryCardRank = null;
            Rank? secondaryCardRank = null;
            Suit? suit = null;
            var kickers = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Spades),
                new(Rank.Queen, Suit.Diamonds),
                new(Rank.Jack, Suit.Clubs),
                new(Rank.Ten, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a three of a kind.
        /// </summary>
        [Test]
        public void HandIsThreeOfAKind()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.Ace, Suit.Diamonds),
                new(Rank.King, Suit.Clubs),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.ThreeOfAKind;
            Rank? primaryCardRank = Rank.Ace;
            Rank? secondaryCardRank = null;
            Suit? suit = null;
            var kickers = new List<Card>()
            {
                new(Rank.King, Suit.Clubs),
                new(Rank.Queen, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a two pair.
        /// </summary>
        [Test]
        public void HandIsTwoPair()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.King, Suit.Diamonds),
                new(Rank.King, Suit.Clubs),
                new(Rank.Queen, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.TwoPair;
            Rank? primaryCardRank = Rank.Ace;
            Rank? secondaryCardRank = Rank.King;
            Suit? suit = null;
            var kickers = new List<Card>() { new(Rank.Queen, Suit.Hearts) };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a pair.
        /// </summary>
        [Test]
        public void HandIsPair()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.King, Suit.Diamonds),
                new(Rank.Queen, Suit.Clubs),
                new(Rank.Jack, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Pair;
            Rank? primaryCardRank = Rank.Ace;
            Rank? secondaryCardRank = null;
            Suit? suit = null;
            var kickers = new List<Card>()
            {
                new(Rank.King, Suit.Diamonds),
                new(Rank.Queen, Suit.Clubs),
                new(Rank.Jack, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }

        /// <summary>
        /// Tests whether the given hand is a high card.
        /// </summary>
        [Test]
        public void HandIsHighCard()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Spades),
                new(Rank.Queen, Suit.Diamonds),
                new(Rank.Jack, Suit.Clubs),
                new(Rank.Nine, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.HighCard;
            Rank? primaryCardRank = null;
            Rank? secondaryCardRank = null;
            Suit? suit = null;
            var kickers = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.King, Suit.Spades),
                new(Rank.Queen, Suit.Diamonds),
                new(Rank.Jack, Suit.Clubs),
                new(Rank.Nine, Suit.Hearts),
            };

            UnitTestHelpClass.AssertHand(
                bestHand,
                handRank,
                primaryCardRank,
                secondaryCardRank,
                suit,
                kickers
            );
        }
    }
}
