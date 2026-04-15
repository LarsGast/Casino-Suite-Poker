using PokerUnitTests.Unit_Tests_Help_Methods;

namespace PokerUnitTests
{
    public class UnitTestsHandsWin
    {
        /// <summary>
        /// Asserts that the best StraightFlush always wins against inferior hands.
        /// </summary>
        [Test]
        public void StraightFlushWins()
        {
            // This hand should win agains all other hands in this method.
            var bestStraightFlush = UnitTestHelpClass.GetBestStraightFlush();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstStraightFlush(),
                UnitTestHelpClass.GetBestFourOfAKind(),
                UnitTestHelpClass.GetBestFullHouse(),
                UnitTestHelpClass.GetBestFlush(),
                UnitTestHelpClass.GetBestStraight(),
                UnitTestHelpClass.GetBestThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestStraightFlush, losingHands);
        }

        /// <summary>
        /// Asserts that the best FourOfAKind always wins against inferior hands.
        /// </summary>
        [Test]
        public void FourOfAKindWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFourOfAKind = UnitTestHelpClass.GetBestFourOfAKind();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstFourOfAKind(),
                UnitTestHelpClass.GetBestFullHouse(),
                UnitTestHelpClass.GetBestFlush(),
                UnitTestHelpClass.GetBestStraight(),
                UnitTestHelpClass.GetBestThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestFourOfAKind, losingHands);
        }

        /// <summary>
        /// Asserts that the best FullHouse always wins against inferior hands.
        /// </summary>
        [Test]
        public void FullHouseWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFullHouse = UnitTestHelpClass.GetBestFullHouse();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstFullHouse(),
                UnitTestHelpClass.GetBestFlush(),
                UnitTestHelpClass.GetBestStraight(),
                UnitTestHelpClass.GetBestThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestFullHouse, losingHands);
        }

        /// <summary>
        /// Asserts that the best Flush always wins against inferior hands.
        /// </summary>
        [Test]
        public void FlushWins()
        {
            // This hand should win agains all other hands in this method.
            var bestFlush = UnitTestHelpClass.GetBestFlush();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstFlush(),
                UnitTestHelpClass.GetBestStraight(),
                UnitTestHelpClass.GetBestThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestFlush, losingHands);
        }

        /// <summary>
        /// Asserts that the best Straight always wins against inferior hands.
        /// </summary>
        [Test]
        public void StraightWins()
        {
            // This hand should win agains all other hands in this method.
            var bestStraight = UnitTestHelpClass.GetBestStraight();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstStraight(),
                UnitTestHelpClass.GetBestThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestStraight, losingHands);
        }

        /// <summary>
        /// Asserts that the best ThreeOfAKind always wins against inferior hands.
        /// </summary>
        [Test]
        public void ThreeOfAKindWins()
        {
            // This hand should win agains all other hands in this method.
            var bestThreeOfAKind = UnitTestHelpClass.GetBestThreeOfAKind();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstThreeOfAKind(),
                UnitTestHelpClass.GetBestTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestThreeOfAKind, losingHands);
        }

        /// <summary>
        /// Asserts that the best TwoPair always wins against inferior hands.
        /// </summary>
        [Test]
        public void TwoPairWins()
        {
            // This hand should win agains all other hands in this method.
            var bestTwoPair = UnitTestHelpClass.GetBestTwoPair();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.GetWorstTwoPair(),
                UnitTestHelpClass.GetBestPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestTwoPair, losingHands);
        }

        /// <summary>
        /// Asserts that the best Pair always wins against inferior hands.
        /// </summary>
        [Test]
        public void PairWins()
        {
            // This hand should win agains all other hands in this method.
            var bestPair = UnitTestHelpClass.GetBestPair();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>()
            {
                UnitTestHelpClass.getWorstPair(),
                UnitTestHelpClass.GetBestHighCard(),
            };

            UnitTestHelpClass.AssertThatHandWins(bestPair, losingHands);
        }

        /// <summary>
        /// Asserts that the best HighCard always wins against inferior hands.
        /// </summary>
        [Test]
        public void HighCardWins()
        {
            // This hand should win agains all other hands in this method.
            var bestHighCard = UnitTestHelpClass.GetBestHighCard();

            // All hands below should lose against the hand specified above.
            var losingHands = new List<PokerHand>() { UnitTestHelpClass.GetWorstHighCard() };

            UnitTestHelpClass.AssertThatHandWins(bestHighCard, losingHands);
        }
    }
}
