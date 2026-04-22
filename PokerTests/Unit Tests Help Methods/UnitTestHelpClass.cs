using PokerLibrary.Services;

namespace PokerUnitTests.Unit_Tests_Help_Methods
{
    /// <summary>
    /// A class that provides helper methods for unit tests related to Poker hand evaluation.
    /// </summary>
    internal class UnitTestHelpClass
    {
        private readonly PokerHandEvaluator _pokerHandEvaluator;
        private readonly PokerHandComparer _pokerHandComparer;

        public UnitTestHelpClass()
        {
            this._pokerHandEvaluator = new PokerHandEvaluator();
            this._pokerHandComparer = new PokerHandComparer();
        }

        /// <summary>
        /// Help function to assert a PokerHand.
        /// </summary>
        /// <param name="hand">Observed hand</param>
        /// <param name="handRank">Expected handRank</param>
        /// <param name="primaryCardRank">Expected primaryCardRank</param>
        /// <param name="secondaryCardRank">Expected secondaryCardRank</param>
        /// <param name="suit">Expected suit</param>
        /// <param name="kickers">Expected kickers</param>
        internal void AssertHand(
            PokerHand hand,
            HandRanking handRank,
            CardRank? primaryCardRank,
            CardRank? secondaryCardRank,
            CardSuit? suit,
            IEnumerable<Card>? kickers
        )
        {
            Assert.Multiple(
                (TestDelegate)(
                    () =>
                    {
                        Assert.That(
                            hand.HandRank,
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
                        Assert.That(hand.HandSuit, Is.EqualTo(suit), $"CardSuit is not {suit}");
                        Assert.That(hand.Kickers, Is.EqualTo(kickers), $"Kickers are not the same");
                    }
                )
            );
        }

        /// <summary>
        /// Asserts that the given hand wins against the given losinghands.
        /// </summary>
        /// <param name="winningHand"></param>
        /// <param name="losingHands"></param>
        internal void AssertThatHandWins(PokerHand winningHand, IEnumerable<PokerHand> losingHands)
        {
            var allhands = new List<PokerHand>() { winningHand };
            allhands.AddRange(losingHands);

            var handThatWon = this._pokerHandComparer.GetWinningHand(allhands);

            Assert.That(
                handThatWon,
                Is.EqualTo(winningHand),
                "Hand that should win did not win.\n"
                    + "Hand that should win vs. hand that won.\n"
                    + $"{winningHand.HandRank} vs. {handThatWon.HandRank}\n"
                    + $"Primary card rank: {winningHand.PrimaryCardRank} vs. {handThatWon.PrimaryCardRank}\n"
                    + $"Secondary card rank: {winningHand.SecondaryCardRank} vs. {handThatWon.SecondaryCardRank}\n"
                    + $"Kickers: {winningHand.Kickers} vs. {handThatWon.Kickers}"
            );
        }

        /// <summary>
        /// Gets the best StraightFlush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestStraightFlush()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst StraightFlush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstStraightFlush()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Three, CardSuit.Hearts),
                new(CardRank.Four, CardSuit.Hearts),
                new(CardRank.Five, CardSuit.Hearts),
                new(CardRank.Seven, CardSuit.Clubs),
                new(CardRank.Eight, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best FourOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestFourOfAKind()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.Ace, CardSuit.Diamonds),
                new(CardRank.Ace, CardSuit.Clubs),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst FourOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstFourOfAKind()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Two, CardSuit.Diamonds),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Hearts),
                new(CardRank.Three, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best FullHouse as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestFullHouse()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Spades),
                new(CardRank.Ace, CardSuit.Diamonds),
                new(CardRank.King, CardSuit.Clubs),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst FullHouse as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstFullHouse()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Two, CardSuit.Diamonds),
                new(CardRank.Three, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Hearts),
                new(CardRank.Five, CardSuit.Clubs),
                new(CardRank.Six, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Flush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestFlush()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Flush as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstFlush()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Three, CardSuit.Hearts),
                new(CardRank.Four, CardSuit.Hearts),
                new(CardRank.Five, CardSuit.Hearts),
                new(CardRank.Seven, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Clubs),
                new(CardRank.Three, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Straight as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestStraight()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Straight as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstStraight()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Three, CardSuit.Diamonds),
                new(CardRank.Four, CardSuit.Clubs),
                new(CardRank.Five, CardSuit.Hearts),
                new(CardRank.Seven, CardSuit.Clubs),
                new(CardRank.Eight, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best ThreeOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestThreeOfAKind()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst ThreeOfAKind as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstThreeOfAKind()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Two, CardSuit.Diamonds),
                new(CardRank.Three, CardSuit.Clubs),
                new(CardRank.Four, CardSuit.Hearts),
                new(CardRank.Five, CardSuit.Clubs),
                new(CardRank.Seven, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best TwoPair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestTwoPair()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst TwoPair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstTwoPair()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Three, CardSuit.Diamonds),
                new(CardRank.Three, CardSuit.Clubs),
                new(CardRank.Four, CardSuit.Hearts),
                new(CardRank.Five, CardSuit.Clubs),
                new(CardRank.Seven, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best Pair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestPair()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst Pair as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstPair()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Two, CardSuit.Spades),
                new(CardRank.Three, CardSuit.Diamonds),
                new(CardRank.Four, CardSuit.Clubs),
                new(CardRank.Five, CardSuit.Hearts),
                new(CardRank.Seven, CardSuit.Clubs),
                new(CardRank.Eight, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the best HighCard as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetBestHighCard()
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

            return this._pokerHandEvaluator.GetBestHand(cards);
        }

        /// <summary>
        /// Gets the worst HighCard as a PokerHand.
        /// </summary>
        /// <returns></returns>
        internal PokerHand GetWorstHighCard()
        {
            var cards = new List<Card>()
            {
                new(CardRank.Two, CardSuit.Hearts),
                new(CardRank.Three, CardSuit.Spades),
                new(CardRank.Four, CardSuit.Diamonds),
                new(CardRank.Five, CardSuit.Clubs),
                new(CardRank.Seven, CardSuit.Hearts),
                new(CardRank.Eight, CardSuit.Clubs),
                new(CardRank.Nine, CardSuit.Spades),
            };

            return this._pokerHandEvaluator.GetBestHand(cards);
        }
    }
}
