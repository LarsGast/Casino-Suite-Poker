using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.Enums;
using static DeckOfCardsLibrary.Card;

namespace PokerUnitTests.Unit_Tests_Help_Methods
{
    /// <summary>
    /// A static class that provides helper methods for unit tests related to Poker hand evaluation.
    /// </summary>
    internal static class UnitTestHelpClass
    {
        /// <summary>
        /// Help function to assert a PokerHand.
        /// </summary>
        /// <param name="hand">Observed hand</param>
        /// <param name="handRank">Expected handRank</param>
        /// <param name="primaryCardRank">Expected primaryCardRank</param>
        /// <param name="secondaryCardRank">Expected secondaryCardRank</param>
        /// <param name="suit">Expected suit</param>
        /// <param name="kickers">Expected kickers</param>
        internal static void AssertHand(
            PokerHand hand,
            HandRanking handRank,
            Rank? primaryCardRank,
            Rank? secondaryCardRank,
            Suit? suit,
            IEnumerable<Card> kickers
        )
        {
            Assert.Multiple(
                (TestDelegate)(
                    () =>
                    {
                        Assert.That(
                            (HandRanking)hand.HandRank,
                            Is.EqualTo(handRank),
                            $"Hand is not a {handRank}"
                        );
                        Assert.That(
                            hand.PrimaryCardRank,
                            Is.EqualTo(primaryCardRank),
                            $"Primary card rank is not {primaryCardRank}"
                        );
                        Assert.That(
                            hand.SecondaryCardRank,
                            Is.EqualTo(secondaryCardRank),
                            $"Secondary card rank is not {secondaryCardRank}"
                        );
                        Assert.That(hand.Suit, Is.EqualTo(suit), $"Suit is not {suit}");
                        Assert.That(
                            ListExtensionMethods.Equals(hand.Kickers, kickers),
                            Is.True,
                            $"Kickers are not the same. Oberved: {(hand.Kickers.GetDisplayString(displayTenAsT: true))}. Expected: {(kickers.GetDisplayString(displayTenAsT: true))}"
                        );
                    }
                )
            );
        }

        /// <summary>
        /// Asserts that the given hand wins against the given losinghands.
        /// </summary>
        /// <param name="winningHand"></param>
        /// <param name="losingHands"></param>
        internal static void AssertThatHandWins(
            PokerHand winningHand,
            IEnumerable<PokerHand> losingHands
        )
        {
            var allhands = new List<PokerHand>() { winningHand };
            allhands.AddRange(losingHands);

            var handThatWon = PokerHand.GetWinningHand(allhands);

            Assert.That(
                handThatWon,
                Is.EqualTo(winningHand),
                "Hand that should win did not win.\n"
                    + "Hand that should win vs. hand that won.\n"
                    + $"{winningHand.HandRank} vs. {handThatWon.HandRank}\n"
                    + $"Primary card rank: {winningHand.PrimaryCardRank} vs. {handThatWon.PrimaryCardRank}\n"
                    + $"Secondary card rank: {winningHand.SecondaryCardRank} vs. {handThatWon.SecondaryCardRank}\n"
                    + $"Kickers: {winningHand.Kickers.GetDisplayString(displayTenAsT: true)} vs. {handThatWon.Kickers.GetDisplayString(displayTenAsT: true)}"
            );
        }

        /// <summary>
        /// Gets the best StraightFlush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestStraightFlush()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst StraightFlush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstStraightFlush()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Two, Suit.Hearts),
                new(Rank.Three, Suit.Hearts),
                new(Rank.Four, Suit.Hearts),
                new(Rank.Five, Suit.Hearts),
                new(Rank.Seven, Suit.Clubs),
                new(Rank.Eight, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best FourOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestFourOfAKind()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.Ace, Suit.Diamonds),
                new(Rank.Ace, Suit.Clubs),
                new(Rank.King, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst FourOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstFourOfAKind()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Two, Suit.Diamonds),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Hearts),
                new(Rank.Three, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best FullHouse as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestFullHouse()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Ace, Suit.Spades),
                new(Rank.Ace, Suit.Diamonds),
                new(Rank.King, Suit.Clubs),
                new(Rank.King, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst FullHouse as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstFullHouse()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Two, Suit.Diamonds),
                new(Rank.Three, Suit.Clubs),
                new(Rank.Three, Suit.Hearts),
                new(Rank.Five, Suit.Clubs),
                new(Rank.Six, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Flush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestFlush()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Flush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstFlush()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Three, Suit.Hearts),
                new(Rank.Four, Suit.Hearts),
                new(Rank.Five, Suit.Hearts),
                new(Rank.Seven, Suit.Hearts),
                new(Rank.Two, Suit.Clubs),
                new(Rank.Three, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Straight as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestStraight()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Straight as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstStraight()
        {
            var cards = new List<Card>()
            {
                new(Rank.Ace, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Three, Suit.Diamonds),
                new(Rank.Four, Suit.Clubs),
                new(Rank.Five, Suit.Hearts),
                new(Rank.Seven, Suit.Clubs),
                new(Rank.Eight, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best ThreeOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestThreeOfAKind()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst ThreeOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstThreeOfAKind()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Two, Suit.Diamonds),
                new(Rank.Three, Suit.Clubs),
                new(Rank.Four, Suit.Hearts),
                new(Rank.Five, Suit.Clubs),
                new(Rank.Seven, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best TwoPair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestTwoPair()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst TwoPair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstTwoPair()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Three, Suit.Diamonds),
                new(Rank.Three, Suit.Clubs),
                new(Rank.Four, Suit.Hearts),
                new(Rank.Five, Suit.Clubs),
                new(Rank.Seven, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Pair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestPair()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Pair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand getWorstPair()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Two, Suit.Spades),
                new(Rank.Three, Suit.Diamonds),
                new(Rank.Four, Suit.Clubs),
                new(Rank.Five, Suit.Hearts),
                new(Rank.Seven, Suit.Clubs),
                new(Rank.Eight, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best HighCard as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetBestHighCard()
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

            return PokerHand.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst HighCard as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal static PokerHand GetWorstHighCard()
        {
            var cards = new List<Card>()
            {
                new(Rank.Two, Suit.Hearts),
                new(Rank.Three, Suit.Spades),
                new(Rank.Four, Suit.Diamonds),
                new(Rank.Five, Suit.Clubs),
                new(Rank.Seven, Suit.Hearts),
                new(Rank.Eight, Suit.Clubs),
                new(Rank.Nine, Suit.Spades),
            };

            return PokerHand.GetBestHand(cards);
        }
    }
}
