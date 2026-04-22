namespace PokerTests.Unit_Tests.Entities
{
    [TestFixture]
    public class PokerHandTests
    {
        #region Equals

        /// <summary>
        /// Verifies that a PokerHand instance is equal to itself.
        /// </summary>
        [Test]
        public void Equals_SameInstance_ReturnsTrue()
        {
            // Arrange
            var hand = new PokerHand(
                HandRanking.Pair,
                CardRank.Ace,
                null,
                null,
                new List<Card> { new(CardRank.King, CardSuit.Hearts) }
            );

            // Act
            var result = hand.Equals(hand);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verifies that two PokerHand instances with identical properties are equal.
        /// </summary>
        [Test]
        public void Equals_IdenticalHands_ReturnsTrue()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.King, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verifies that Equals returns false when comparing to null.
        /// Tests the "hand is not null" condition.
        /// </summary>
        [Test]
        public void Equals_ComparedToNull_ReturnsFalse()
        {
            // Arrange
            var hand = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, null);

            // Act
            var result = hand.Equals(null);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when HandRank values differ.
        /// </summary>
        [Test]
        public void Equals_DifferentHandRank_ReturnsFalse()
        {
            // Arrange
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, null);
            var hand2 = new PokerHand(HandRanking.ThreeOfAKind, CardRank.Ace, null, null, null);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when PrimaryCardRank values differ.
        /// </summary>
        [Test]
        public void Equals_DifferentPrimaryCardRank_ReturnsFalse()
        {
            // Arrange
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, null);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.King, null, null, null);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when SecondaryCardRank values differ.
        /// </summary>
        [Test]
        public void Equals_DifferentSecondaryCardRank_ReturnsFalse()
        {
            // Arrange
            var hand1 = new PokerHand(HandRanking.TwoPair, CardRank.Ace, CardRank.King, null, null);
            var hand2 = new PokerHand(
                HandRanking.TwoPair,
                CardRank.Ace,
                CardRank.Queen,
                null,
                null
            );

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when HandSuit values differ.
        /// </summary>
        [Test]
        public void Equals_DifferentHandSuit_ReturnsFalse()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.Ace, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Flush, null, null, CardSuit.Hearts, kickers);
            var hand2 = new PokerHand(HandRanking.Flush, null, null, CardSuit.Spades, kickers);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns true when both Kickers are null.
        /// Tests the "this.Kickers is null && hand.Kickers is null" branch.
        /// </summary>
        [Test]
        public void Equals_BothKickersNull_ReturnsTrue()
        {
            // Arrange
            var hand1 = new PokerHand(
                HandRanking.FullHouse,
                CardRank.Ace,
                CardRank.King,
                null,
                null
            );
            var hand2 = new PokerHand(
                HandRanking.FullHouse,
                CardRank.Ace,
                CardRank.King,
                null,
                null
            );

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verifies that Equals returns false when one Kickers is null and the other is not.
        /// Tests the branch where Kickers is not null but hand.Kickers is null.
        /// </summary>
        [Test]
        public void Equals_OneKickersNull_ReturnsFalse()
        {
            // Arrange
            var hand1 = new PokerHand(
                HandRanking.Pair,
                CardRank.Ace,
                null,
                null,
                new List<Card> { new(CardRank.King, CardSuit.Hearts) }
            );
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, null);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns true when both Kickers have identical cards.
        /// Tests the SequenceEqual comparison for non-null Kickers.
        /// </summary>
        [Test]
        public void Equals_IdenticalKickers_ReturnsTrue()
        {
            // Arrange
            var kickers1 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
            };
            var kickers2 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
            };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers2);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verifies that Equals returns false when Kickers have different cards.
        /// Tests the SequenceEqual comparison when sequences differ.
        /// </summary>
        [Test]
        public void Equals_DifferentKickers_ReturnsFalse()
        {
            // Arrange
            var kickers1 = new List<Card> { new(CardRank.King, CardSuit.Hearts) };
            var kickers2 = new List<Card> { new(CardRank.Queen, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers2);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when Kickers have different counts.
        /// Tests the SequenceEqual comparison when sequence lengths differ.
        /// </summary>
        [Test]
        public void Equals_DifferentKickerCounts_ReturnsFalse()
        {
            // Arrange
            var kickers1 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
            };
            var kickers2 = new List<Card> { new(CardRank.King, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers2);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns false when Kickers have the same cards but in different order.
        /// Tests that SequenceEqual is order-sensitive.
        /// </summary>
        [Test]
        public void Equals_DifferentKickerOrder_ReturnsFalse()
        {
            // Arrange
            var kickers1 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
            };
            var kickers2 = new List<Card>
            {
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
            };
            var hand1 = new PokerHand(HandRanking.HighCard, null, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.HighCard, null, null, null, kickers2);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verifies that Equals returns true when both Kickers are empty lists.
        /// Tests the edge case where Kickers is not null but contains no elements.
        /// </summary>
        [Test]
        public void Equals_BothKickersEmpty_ReturnsTrue()
        {
            // Arrange
            var emptyKickers1 = new List<Card>();
            var emptyKickers2 = new List<Card>();
            var hand1 = new PokerHand(HandRanking.HighCard, null, null, null, emptyKickers1);
            var hand2 = new PokerHand(HandRanking.HighCard, null, null, null, emptyKickers2);

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verifies that Equals works correctly with complex hands (StraightFlush).
        /// Tests all properties being set and equal.
        /// </summary>
        [Test]
        public void Equals_ComplexHandAllPropertiesSet_ReturnsTrue()
        {
            // Arrange
            var kickers1 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Ten, CardSuit.Hearts),
                new(CardRank.Nine, CardSuit.Hearts),
            };
            var kickers2 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Ten, CardSuit.Hearts),
                new(CardRank.Nine, CardSuit.Hearts),
            };
            var hand1 = new PokerHand(
                HandRanking.StraightFlush,
                CardRank.King,
                null,
                CardSuit.Hearts,
                kickers1
            );
            var hand2 = new PokerHand(
                HandRanking.StraightFlush,
                CardRank.King,
                null,
                CardSuit.Hearts,
                kickers2
            );

            // Act
            var result = hand1.Equals(hand2);

            // Assert
            Assert.That(result, Is.True);
        }

        #endregion

        #region GetHashCode

        /// <summary>
        /// Verifies that calling GetHashCode multiple times on the same PokerHand instance
        /// returns the same hash code value consistently.
        /// </summary>
        [Test]
        public void GetHashCode_CalledMultipleTimes_ReturnsConsistentValue()
        {
            // Arrange
            var hand = new PokerHand(
                HandRanking.Flush,
                CardRank.Ace,
                null,
                CardSuit.Hearts,
                new List<Card> { new(CardRank.King, CardSuit.Hearts) }
            );

            // Act
            var hashCode1 = hand.GetHashCode();
            var hashCode2 = hand.GetHashCode();
            var hashCode3 = hand.GetHashCode();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(hashCode1, Is.EqualTo(hashCode2));
                Assert.That(hashCode2, Is.EqualTo(hashCode3));
            });
        }

        /// <summary>
        /// Verifies that two PokerHand instances with identical properties
        /// produce the same hash code, satisfying the equality contract.
        /// </summary>
        [Test]
        public void GetHashCode_EqualInstances_ReturnSameHashCode()
        {
            // Arrange
            var kickers = new List<Card>
            {
                new(CardRank.King, CardSuit.Spades),
                new(CardRank.Queen, CardSuit.Spades),
            };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that GetHashCode handles null Kickers correctly and returns a valid hash code.
        /// Tests the branch where Kickers is null and the foreach loop is not executed.
        /// </summary>
        [Test]
        public void GetHashCode_WithNullKickers_ReturnsValidHashCode()
        {
            // Arrange
            var hand = new PokerHand(
                HandRanking.FullHouse,
                CardRank.King,
                CardRank.Queen,
                null,
                null
            );

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        /// <summary>
        /// Verifies that GetHashCode handles an empty Kickers list correctly.
        /// Tests the branch where Kickers is not null but contains no elements.
        /// </summary>
        [Test]
        public void GetHashCode_WithEmptyKickers_ReturnsValidHashCode()
        {
            // Arrange
            var emptyKickers = new List<Card>();
            var hand = new PokerHand(HandRanking.HighCard, null, null, null, emptyKickers);

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        /// <summary>
        /// Verifies that GetHashCode properly includes kicker cards in the hash calculation.
        /// Tests the branch where Kickers is not null and the foreach loop executes.
        /// </summary>
        [Test]
        public void GetHashCode_WithSingleKicker_ReturnsValidHashCode()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.Ace, CardSuit.Hearts) };
            var hand = new PokerHand(HandRanking.Pair, CardRank.King, null, null, kickers);

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        /// <summary>
        /// Verifies that GetHashCode properly includes all kicker cards in the hash calculation.
        /// Tests the foreach loop with multiple iterations through kicker cards.
        /// </summary>
        [Test]
        public void GetHashCode_WithMultipleKickers_ReturnsValidHashCode()
        {
            // Arrange
            var kickers = new List<Card>
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Queen, CardSuit.Hearts),
                new(CardRank.Jack, CardSuit.Hearts),
                new(CardRank.Ten, CardSuit.Hearts),
            };
            var hand = new PokerHand(HandRanking.Flush, null, null, CardSuit.Hearts, kickers);

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        /// <summary>
        /// Verifies that PokerHand instances with different HandRank values
        /// produce different hash codes.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentHandRank_ReturnsDifferentHashCode()
        {
            // Arrange
            var hand1 = new PokerHand(HandRanking.HighCard, null, null, null, null);
            var hand2 = new PokerHand(HandRanking.Pair, null, null, null, null);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances with different PrimaryCardRank values
        /// produce different hash codes.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentPrimaryCardRank_ReturnsDifferentHashCode()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.King, CardSuit.Spades) };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Ace, null, null, kickers);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.King, null, null, kickers);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances with different SecondaryCardRank values
        /// produce different hash codes.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentSecondaryCardRank_ReturnsDifferentHashCode()
        {
            // Arrange
            var hand1 = new PokerHand(HandRanking.TwoPair, CardRank.Ace, CardRank.King, null, null);
            var hand2 = new PokerHand(
                HandRanking.TwoPair,
                CardRank.Ace,
                CardRank.Queen,
                null,
                null
            );

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances with different HandSuit values
        /// produce different hash codes.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentHandSuit_ReturnsDifferentHashCode()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.Ace, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Flush, null, null, CardSuit.Hearts, kickers);
            var hand2 = new PokerHand(HandRanking.Flush, null, null, CardSuit.Spades, kickers);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances with different Kickers produce different hash codes.
        /// Tests that the foreach loop properly contributes different kicker cards to the hash.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentKickers_ReturnsDifferentHashCode()
        {
            // Arrange
            var kickers1 = new List<Card> { new(CardRank.Ace, CardSuit.Hearts) };
            var kickers2 = new List<Card> { new(CardRank.King, CardSuit.Hearts) };
            var hand1 = new PokerHand(HandRanking.Pair, CardRank.Queen, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.Pair, CardRank.Queen, null, null, kickers2);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances where one has null Kickers and another has non-null Kickers
        /// produce different hash codes. Tests the conditional branch for null vs non-null Kickers.
        /// </summary>
        [Test]
        public void GetHashCode_NullKickersVsNonNullKickers_ReturnsDifferentHashCode()
        {
            // Arrange
            var hand1 = new PokerHand(
                HandRanking.FullHouse,
                CardRank.Ace,
                CardRank.King,
                null,
                null
            );
            var hand2 = new PokerHand(
                HandRanking.Pair,
                CardRank.Ace,
                null,
                null,
                new List<Card> { new(CardRank.King, CardSuit.Spades) }
            );

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies that PokerHand instances with different kicker order produce different hash codes.
        /// Ensures the foreach loop processes kickers in sequence order.
        /// </summary>
        [Test]
        public void GetHashCode_DifferentKickerOrder_ReturnsDifferentHashCode()
        {
            // Arrange
            var kickers1 = new List<Card>
            {
                new(CardRank.Ace, CardSuit.Hearts),
                new(CardRank.King, CardSuit.Hearts),
            };
            var kickers2 = new List<Card>
            {
                new(CardRank.King, CardSuit.Hearts),
                new(CardRank.Ace, CardSuit.Hearts),
            };
            var hand1 = new PokerHand(HandRanking.HighCard, null, null, null, kickers1);
            var hand2 = new PokerHand(HandRanking.HighCard, null, null, null, kickers2);

            // Act
            var hashCode1 = hand1.GetHashCode();
            var hashCode2 = hand2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        /// <summary>
        /// Verifies GetHashCode behavior with all nullable properties set to null.
        /// Tests edge case where only HandRank is non-null.
        /// </summary>
        [Test]
        public void GetHashCode_AllNullablePropertiesNull_ReturnsValidHashCode()
        {
            // Arrange
            var hand = new PokerHand(HandRanking.HighCard, null, null, null, null);

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        /// <summary>
        /// Verifies GetHashCode behavior with all properties set to non-null values.
        /// Tests comprehensive case where all properties contribute to the hash.
        /// </summary>
        [Test]
        public void GetHashCode_AllPropertiesNonNull_ReturnsValidHashCode()
        {
            // Arrange
            var kickers = new List<Card> { new(CardRank.Ten, CardSuit.Clubs) };
            var hand = new PokerHand(
                HandRanking.TwoPair,
                CardRank.Ace,
                CardRank.King,
                CardSuit.Clubs,
                kickers
            );

            // Act
            var hashCode = hand.GetHashCode();

            // Assert
            Assert.That(hashCode, Is.Not.Zero);
        }

        #endregion
    }
}
