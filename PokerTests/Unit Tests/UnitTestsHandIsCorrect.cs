using DeckOfPlayingCardsLibrary.Entities;
using DeckOfPlayingCardsLibrary.Enums;
using PokerLibrary;
using PokerLibrary.Enums;
using PokerUnitTests.Unit_Tests_Help_Methods;

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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Ten, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.StraightFlush;
            CardRank? primaryCardRank = null;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = CardSuit.Hearts;
            var kickers = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Ten, CardSuit.Hearts),
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Clubs),
                new(CardRank.Ace, CardSuit.Diamonds),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.FourOfAKind;
            CardRank? primaryCardRank = CardRank.Ace;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = null;
            var kickers = new List<Card>() { new(CardRank.King, CardSuit.Hearts) };

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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.Ace, CardSuit.Clubs),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.FullHouse;
            CardRank? primaryCardRank = CardRank.Ace;
            CardRank? secondaryCardRank = CardRank.King;
            CardSuit? suit = null;
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Nine, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Flush;
            CardRank? primaryCardRank = null;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = CardSuit.Hearts;
            var kickers = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Nine, CardSuit.Hearts),
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Queen, CardSuit.Diamonds),
                new(CardRank.Jack, CardSuit.Clubs),
                new(CardRank.Ten, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Straight;
            CardRank? primaryCardRank = null;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = null;
            var kickers = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Queen, CardSuit.Diamonds),
                new(CardRank.Jack, CardSuit.Clubs),
                new(CardRank.Ten, CardSuit.Hearts),
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.Ace, CardSuit.Diamonds),
                new(CardRank.King, CardSuit.Clubs),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.ThreeOfAKind;
            CardRank? primaryCardRank = CardRank.Ace;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = null;
            var kickers = new List<Card>()
            {
                new(CardRank.King, CardSuit.Clubs),
                new(CardRank.Queen, CardSuit.Hearts),
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.King, CardSuit.Diamonds),
                new(CardRank.King, CardSuit.Clubs),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.TwoPair;
            CardRank? primaryCardRank = CardRank.Ace;
            CardRank? secondaryCardRank = CardRank.King;
            CardSuit? suit = null;
            var kickers = new List<Card>() { new(CardRank.Queen, CardSuit.Hearts) };

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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.King, CardSuit.Diamonds),
                new(CardRank.Queen, CardSuit.Clubs),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.Pair;
            CardRank? primaryCardRank = CardRank.Ace;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = null;
            var kickers = new List<Card>()
            {
                new(CardRank.King, CardSuit.Diamonds),
                new(CardRank.Queen, CardSuit.Clubs),
                new(CardRank.Jack, CardSuit.Hearts),
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
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Queen, CardSuit.Diamonds),
                new(CardRank.Jack, CardSuit.Clubs),
                new(CardRank.Nine, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            var bestHand = PokerHand.GetBestHand(cards);

            var handRank = HandRanking.HighCard;
            CardRank? primaryCardRank = null;
            CardRank? secondaryCardRank = null;
            CardSuit? suit = null;
            var kickers = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Queen, CardSuit.Diamonds),
                new(CardRank.Jack, CardSuit.Clubs),
                new(CardRank.Nine, CardSuit.Hearts),
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
