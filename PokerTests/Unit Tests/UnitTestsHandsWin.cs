using PokerUnitTests.Unit_Tests_Help_Methods;

namespace PokerUnitTests
{
    public class UnitTestsHandsWin
    {
        private readonly UnitTestHelpClass _unitTestHelpClass;

        public UnitTestsHandsWin()
        {
            this._unitTestHelpClass = new UnitTestHelpClass();
        }

        /// <summary>
        /// Asserts that the best StraightFlush always wins against inferior hands.
        /// </summary>
        [Test]
        public void StraightFlushWins()
        {
            // This hand should win agains all other hands in this method.
            var bestStraightFlush = this._unitTestHelpClass.GetBestStraightFlush();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstStraightFlush(),
                this._unitTestHelpClass.GetBestFourOfAKind(),
                this._unitTestHelpClass.GetBestFullHouse(),
                this._unitTestHelpClass.GetBestFlush(),
                this._unitTestHelpClass.GetBestStraight(),
                this._unitTestHelpClass.GetBestThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestStraightFlush, losingHands);
        }

        /// <summary>
        /// Asserts that the best FourOfAKind always wins against inferior hands.
        /// </summary>
        [Test]
        public void FourOfAKindWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFourOfAKind = this._unitTestHelpClass.GetBestFourOfAKind();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstFourOfAKind(),
                this._unitTestHelpClass.GetBestFullHouse(),
                this._unitTestHelpClass.GetBestFlush(),
                this._unitTestHelpClass.GetBestStraight(),
                this._unitTestHelpClass.GetBestThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestFourOfAKind, losingHands);
        }

        /// <summary>
        /// Asserts that the best FullHouse always wins against inferior hands.
        /// </summary>
        [Test]
        public void FullHouseWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFullHouse = this._unitTestHelpClass.GetBestFullHouse();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstFullHouse(),
                this._unitTestHelpClass.GetBestFlush(),
                this._unitTestHelpClass.GetBestStraight(),
                this._unitTestHelpClass.GetBestThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestFullHouse, losingHands);
        }

        /// <summary>
        /// Asserts that the best Flush always wins against inferior hands.
        /// </summary>
        [Test]
        public void FlushWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFlush = this._unitTestHelpClass.GetBestFlush();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstFlush(),
                this._unitTestHelpClass.GetBestStraight(),
                this._unitTestHelpClass.GetBestThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestFlush, losingHands);
        }

        /// <summary>
        /// Asserts that the best Straight always wins against inferior hands.
        /// </summary>
        [Test]
        public void StraightWins()
        {
            // This hand should win agains all other hands in this method.
            var bestStraight = this._unitTestHelpClass.GetBestStraight();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstStraight(),
                this._unitTestHelpClass.GetBestThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestStraight, losingHands);
        }

        /// <summary>
        /// Asserts that the best ThreeOfAKind always wins against inferior hands.
        /// </summary>
        [Test]
        public void ThreeOfAKindWins()
        {
            // This hand should win agains all other hands in this method.
            var bestThreeOfAKind = this._unitTestHelpClass.GetBestThreeOfAKind();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstThreeOfAKind(),
                this._unitTestHelpClass.GetBestTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestThreeOfAKind, losingHands);
        }

        /// <summary>
        /// Asserts that the best TwoPair always wins against inferior hands.
        /// </summary>
        [Test]
        public void TwoPairWins()
        {
            // This hand should win agains all other hands in this method.
            var bestTwoPair = this._unitTestHelpClass.GetBestTwoPair();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstTwoPair(),
                this._unitTestHelpClass.GetBestPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestTwoPair, losingHands);
        }

        /// <summary>
        /// Asserts that the best Pair always wins against inferior hands.
        /// </summary>
        [Test]
        public void PairWins()
        {
            // This hand should win agains all other hands in this method.
            var bestPair = this._unitTestHelpClass.GetBestPair();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                this._unitTestHelpClass.GetWorstPair(),
                this._unitTestHelpClass.GetBestHighCard(),
            };

            this._unitTestHelpClass.AssertThatHandWins(bestPair, losingHands);
        }

        /// <summary>
        /// Asserts that the best HighCard always wins against inferior hands.
        /// </summary>
        [Test]
        public void HighCardWins()
        {
            // This hand should win agains all other hands in this method.
            var bestHighCard = this._unitTestHelpClass.GetBestHighCard();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>() { this._unitTestHelpClass.GetWorstHighCard() };

            this._unitTestHelpClass.AssertThatHandWins(bestHighCard, losingHands);
        }
    }
}
