using DeckOfPlayingCardsLibrary.Entities;
using DeckOfPlayingCardsLibrary.Enums;
using PokerLibrary.Enums;

namespace PokerLibrary.Entities
{
    /// <summary>
    /// Represents a 5-card poker hand, which is a combination of playing cards used in various poker games.
    /// </summary>
    /// <remarks>
    /// This class provides methods to evaluate and compare poker hands based on standard hand ranks.
    /// </remarks>
    public class PokerHand
    {
        #region Properties

        /// <summary>
        /// The specific ranking of the hand, such as High Card or Straight Flush.
        /// </summary>
        public HandRanking HandRank { get; }

        /// <summary>
        /// The primary card <see cref="CardRank"/> relevant to this hand.
        /// </summary>
        /// <value>
        /// For <see cref="HandRanking.Pair"/> and <see cref="HandRanking.TwoPair"/>: the rank of the highest pair.
        /// For <see cref="HandRanking.ThreeOfAKind"/> and <see cref="HandRanking.FullHouse"/>: the rank of the three of a kind.
        /// For <see cref="HandRanking.FourOfAKind"/>: the rank of the four of a kind.
        /// <see langword="null"/> for other hand ranks.
        /// </value>
        public CardRank? PrimaryCardRank { get; }

        /// <summary>
        /// The secondary card <see cref="CardRank"/> relevant to this hand.
        /// </summary>
        /// <value>
        /// For <see cref="HandRanking.TwoPair"/>: the rank of the second-highest pair.
        /// For <see cref="HandRanking.FullHouse"/>: the rank of the highest pair that is not a three of a kind.
        /// <see langword="null"/> for other hand ranks.
        /// </value>
        public CardRank? SecondaryCardRank { get; }

        /// <summary>
        /// The <see cref="CardSuit"/> of the hand.
        /// </summary>
        /// <value>
        /// Relevant for <see cref="HandRanking.Flush"/> and <see cref="HandRanking.StraightFlush"/>
        /// <see langword="null"/> for other hand ranks.
        /// </value>
        public CardSuit? HandSuit { get; }

        /// <summary>
        /// A sorted list of kickers, the cards used to enhance the hand.
        /// </summary>
        /// <remarks>
        /// These are all the cards not part of the primary hand but still contribute to it.
        /// </remarks>
        public List<Card> Kickers
        {
            get
            {
                // Sort kickers in descending order of rank.
                var orderedKickers = this._kickers.OrderByDescending(card => card.Rank).ToList();

                // If the hand is a straight or straight flush from Ace to Five, move the Ace to the end.
                // In this case, the ace is the lowest rank of the hand, not the highest.
                if (
                    this.HandRank == HandRanking.Straight
                    || this.HandRank == HandRanking.StraightFlush
                )
                {
                    if (
                        orderedKickers.Last().Rank == CardRank.Two
                        && orderedKickers.First().Rank == CardRank.Ace
                    )
                    {
                        var aceCard = orderedKickers.First();
                        _ = orderedKickers.Remove(aceCard);
                        orderedKickers.Add(aceCard);
                    }
                }

                return orderedKickers;
            }
        }

        /// <summary>
        /// An unsorted list of kickers.
        /// </summary>
        private readonly IEnumerable<Card> _kickers;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for initializing a PokerHand.
        /// </summary>
        /// <param name="handRank"><inheritdoc cref="HandRank" path="/summary"/></param>
        /// <param name="primaryCardRank"><inheritdoc cref="PrimaryCardRank" path="/summary"/></param>
        /// <param name="secondaryCardRank"><inheritdoc cref="SecondaryCardRank" path="/summary"/></param>
        /// <param name="handSuit"><inheritdoc cref="HandSuit" path="/summary"/></param>
        /// <param name="kickers"><inheritdoc cref="Kickers" path="/summary"/></param>
        private PokerHand(
            HandRanking handRank,
            CardRank? primaryCardRank,
            CardRank? secondaryCardRank,
            CardSuit? handSuit,
            IEnumerable<Card> kickers
        )
        {
            this.HandRank = handRank;
            this.PrimaryCardRank = primaryCardRank;
            this.SecondaryCardRank = secondaryCardRank;
            this.HandSuit = handSuit;
            this._kickers = kickers;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Gets the best 5-card poker hand possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>The best 5-card <see cref="PokerHand"/> that can be formed with the given cards.</returns>
        public static PokerHand GetBestHand(IEnumerable<Card> cards)
        {
            // Check for the best possible hand in descending order of poker hand ranks.
            // Stop searching when a hand is found, as every possible hand after that will be of a lower rank.

            var bestStraightFlush = PokerHand.GetBestStraightFlush(cards);
            if (bestStraightFlush != null)
            {
                return bestStraightFlush;
            }

            var bestFourOfAKind = PokerHand.GetBestFourOfAKind(cards);
            if (bestFourOfAKind != null)
            {
                return bestFourOfAKind;
            }

            var bestFullHouse = PokerHand.GetBestFullHouse(cards);
            if (bestFullHouse != null)
            {
                return bestFullHouse;
            }

            var bestFlush = PokerHand.GetBestFlush(cards);
            if (bestFlush != null)
            {
                return bestFlush;
            }

            var bestStraight = PokerHand.GetBestStraight(cards);
            if (bestStraight != null)
            {
                return bestStraight;
            }

            var bestThreeOfAKind = PokerHand.GetBestThreeOfAKind(cards);
            if (bestThreeOfAKind != null)
            {
                return bestThreeOfAKind;
            }

            var bestTwoPair = PokerHand.GetBestTwoPair(cards);
            if (bestTwoPair != null)
            {
                return bestTwoPair;
            }

            var bestPair = PokerHand.GetBestPair(cards);
            if (bestPair != null)
            {
                return bestPair;
            }

            return PokerHand.GetBestHighCard(cards);
        }

        /// <summary>
        /// Checks if this hand wins against the <paramref name="other"/> hand.
        /// </summary>
        /// <param name="other">The other <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <see langword="this"/> hand wins.
        /// <see langword="false"/> if the <paramref name="other"/> hand wins.
        /// <see langword="null"/> if it's a draw.
        /// </returns>
        public bool? WinsAgainst(PokerHand other)
        {
            // Check if this hand has a better hand rank than the other.
            var hasBetterHandRank = this.HasBetterHandRank(other);
            if (hasBetterHandRank != null)
            {
                return hasBetterHandRank;
            }

            // If hand rank is the same, check by the primary card rank.
            var hasBetterFirstCard = this.HasBetterFirstCard(other);
            if (hasBetterFirstCard != null)
            {
                return hasBetterFirstCard;
            }

            // If the primary card rank is the same, check by the secondary card rank.
            var hasBetterSecondCard = this.HasBetterSecondCard(other);
            if (hasBetterSecondCard != null)
            {
                return hasBetterSecondCard;
            }

            // If all previous comparisons result in a draw, compare by kickers.
            return this.HasBetterKickers(other);
        }

        /// <summary>
        /// Determines the best hand from a list of hands.
        /// </summary>
        /// <remarks>
        /// Only returns one hand in case of a draw.
        /// </remarks>
        /// <param name="hands">A list of <see cref="PokerHand"/> to evaluate.</param>
        /// <returns>The best <see cref="PokerHand"/> among the provided list.</returns>
        public static PokerHand GetWinningHand(IEnumerable<PokerHand> hands)
        {
            // Find the highest hand rank among the provided hands.
            // If only one hand has the best hand rank, return it.
            var bestHandRank = hands.OrderByDescending(hand => hand.HandRank).First().HandRank;
            var handsBestHandRank = hands.Where(hand => hand.HandRank == bestHandRank);
            if (handsBestHandRank.Count() == 1)
            {
                return handsBestHandRank.First();
            }

            // Now compare by the primary card rank.
            // If only one hand has the best primary card rank, return it.
            var bestPrimaryCardRank = handsBestHandRank
                .OrderByDescending(hand => hand.PrimaryCardRank)
                .First()
                .PrimaryCardRank;
            var handsBestPrimaryCardRank = handsBestHandRank.Where(hand =>
                hand.PrimaryCardRank == bestPrimaryCardRank
            );
            if (handsBestPrimaryCardRank.Count() == 1)
            {
                return handsBestPrimaryCardRank.First();
            }

            // Now compare by the secondary card rank.
            // If only one hand has the best secondary card rank, return it.
            var bestSecondaryCardRank = handsBestPrimaryCardRank
                .OrderByDescending(hand => hand.SecondaryCardRank)
                .First()
                .SecondaryCardRank;
            var handsBestSecondaryCardRank = handsBestPrimaryCardRank.Where(hand =>
                hand.SecondaryCardRank == bestSecondaryCardRank
            );
            if (handsBestSecondaryCardRank.Count() == 1)
            {
                return handsBestSecondaryCardRank.First();
            }

            // Lastly, compare the kickers.
            var handsHighestKickers = PokerHand.GetWinningHandByKickers(
                handsBestSecondaryCardRank.ToList()
            );

            // If there are multiple same-quality hands (so a draw), return one of those hands.
            return handsHighestKickers.First();
        }

        #endregion

        #region Helper methods

        #region GetBestHand

        /// <summary>
        /// Gets the best <see cref="HandRanking.StraightFlush"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.StraightFlush"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.StraightFlush"/> possible.
        /// </returns>
        private static PokerHand? GetBestStraightFlush(IEnumerable<Card> cards)
        {
            // Find the highest straight flush cards and determine the suit.
            var highestStraightFlushCards = PokerHand.GetHighestStraightCards(
                cards,
                mustBeFlush: true
            );

            if (highestStraightFlushCards == null)
            {
                return null;
            }

            var suit = highestStraightFlushCards.First().Suit;
            var kickers = highestStraightFlushCards.OrderByDescending(card => card.Rank);

            return new PokerHand(HandRanking.StraightFlush, null, null, suit, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.FourOfAKind"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.FourOfAKind"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.FourOfAKind"/> possible.
        /// </returns>
        private static PokerHand? GetBestFourOfAKind(IEnumerable<Card> cards)
        {
            // Only 1 four of a kind is possible, so we do not have to check if the one we find is the highest one.
            var highestFourOrAKindValue = PokerHand.GetHighestOfAKindValue(cards, 4);

            if (highestFourOrAKindValue == null)
            {
                return null;
            }

            var kickers = cards
                .OrderByDescending(card => card.Rank)
                .Where(card => card.Rank != highestFourOrAKindValue)
                .Take(1);

            return new PokerHand(
                HandRanking.FourOfAKind,
                highestFourOrAKindValue,
                null,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.FullHouse"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.FullHouse"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.FullHouse"/> possible.
        /// </returns>
        private static PokerHand? GetBestFullHouse(IEnumerable<Card> cards)
        {
            var highestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(cards, 3);

            if (highestThreeOfAKindValue == null)
            {
                return null;
            }

            var secondHighestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(
                cards.Where(card => card.Rank != highestThreeOfAKindValue),
                3
            );
            var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

            if (secondHighestThreeOfAKindValue == null && highestPairValue == null)
            {
                return null;
            }

            var kickers = new List<Card>();

            return new PokerHand(
                HandRanking.FullHouse,
                highestThreeOfAKindValue,
                highestPairValue,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Flush"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Flush"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Flush"/> possible.
        /// </returns>
        private static PokerHand? GetBestFlush(IEnumerable<Card> cards)
        {
            var flushCards = cards
                .GroupBy(card => card.Suit)
                .Where(group => group.Count() >= 5)
                .SelectMany(group => group);

            if (!flushCards.Any())
            {
                return null;
            }

            var flushSuit = flushCards.First().Suit;
            var kickers = flushCards.OrderByDescending(card => card.Rank);

            return new PokerHand(HandRanking.Flush, null, null, flushSuit, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Straight"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Straight"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Straight"/> possible.
        /// </returns>
        private static PokerHand? GetBestStraight(IEnumerable<Card> cards)
        {
            // Find the highest straight cards and return them as a PokerHand.
            var highestStraightCards = PokerHand.GetHighestStraightCards(cards, mustBeFlush: false);

            if (highestStraightCards == null)
            {
                return null;
            }

            var kickers = highestStraightCards.OrderByDescending(card => card.Rank);

            return new PokerHand(HandRanking.Straight, null, null, null, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.ThreeOfAKind"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.ThreeOfAKind"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.ThreeOfAKind"/> possible.
        /// </returns>
        private static PokerHand? GetBestThreeOfAKind(IEnumerable<Card> cards)
        {
            // Find the highest three of a kind and the two highest kickers, then return as a PokerHand.
            var highestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(cards, 3);

            if (highestThreeOfAKindValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestThreeOfAKindValue)
                .OrderByDescending(card => card.Rank)
                .Take(2);

            return new PokerHand(
                HandRanking.ThreeOfAKind,
                highestThreeOfAKindValue,
                null,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.TwoPair"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.TwoPair"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.TwoPair"/> possible.
        /// </returns>
        private static PokerHand? GetBestTwoPair(IEnumerable<Card> cards)
        {
            // Find the two highest pairs and the highest kicker, then return as a PokerHand.
            var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

            if (highestPairValue == null)
            {
                return null;
            }

            var secondHighestPairValue = PokerHand.GetHighestOfAKindValue(
                cards.Where(card => card.Rank != highestPairValue),
                2
            );

            if (secondHighestPairValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestPairValue && card.Rank != secondHighestPairValue)
                .OrderByDescending(card => card.Rank)
                .Take(1);

            return new PokerHand(
                HandRanking.TwoPair,
                highestPairValue,
                secondHighestPairValue,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Pair"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Pair"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Pair"/> possible.
        /// </returns>
        private static PokerHand? GetBestPair(IEnumerable<Card> cards)
        {
            // Find the highest pair and the three highest kickers, then return as a PokerHand.
            var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

            if (highestPairValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestPairValue)
                .OrderByDescending(card => card.Rank)
                .Take(3);

            return new PokerHand(HandRanking.Pair, highestPairValue, null, null, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.HighCard"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>The best <see cref="HandRanking.HighCard"/> <see cref="PokerHand"/> that can be formed with the given cards.</returns>
        private static PokerHand GetBestHighCard(IEnumerable<Card> cards)
        {
            // Find the five highest cards as kickers and return as a PokerHand.
            var orderedCards = cards.OrderByDescending(card => card.Rank);
            var kickers = orderedCards.Take(5);
            return new PokerHand(HandRanking.HighCard, null, null, null, kickers);
        }

        /// <summary>
        /// Gets the highest value of an "X of a kind" from the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <param name="numberOfAKind">The number of cards that should have the same rank.</param>
        /// <returns>The highest rank that forms an "X of a kind," or null if none is found.</returns>
        private static CardRank? GetHighestOfAKindValue(IEnumerable<Card> cards, int numberOfAKind)
        {
            // Group the cards by rank and count those that have the specified number "of a kind".
            var highestOfAKindCards = cards
                .GroupBy(card => card.Rank)
                .Where(group => group.Count() == numberOfAKind);

            if (!highestOfAKindCards.Any())
            {
                return null;
            }

            // Find and return the highest rank among the groups.
            var highestOfAKindRank = highestOfAKindCards
                .OrderByDescending(group => group.Key)
                .First()
                .Key;

            return highestOfAKindRank;
        }

        /// <summary>
        /// Gets the cards that form the highest possible <see cref="HandRanking.Straight"/> or <see cref="HandRanking.StraightFlush"/> from the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <param name="mustBeFlush">Specifies whether the hand must be a <see cref="HandRanking.StraightFlush"/>.</param>
        /// <returns>
        /// A list of cards that form the highest <see cref="HandRanking.Straight"/> or <see cref="HandRanking.StraightFlush"/>.
        /// <see langword="null"/> if none is found.
        /// </returns>
        private static List<Card>? GetHighestStraightCards(
            IEnumerable<Card> cards,
            bool mustBeFlush = false
        )
        {
            // The ace can be used at both ends, as the card before a 2, and the card after a King.
            // Because of this, we will sort descending and add duplicate cards of each ace at the end of the list.
            var orderedCards = cards.OrderByDescending(card => card.Rank).ToList();
            if (orderedCards.Select(card => card.Rank).Contains(CardRank.Ace))
            {
                orderedCards.AddRange(
                    orderedCards.Where(card => card.Rank == CardRank.Ace).ToList()
                );
            }

            // Look for a straight (flush) for each card, starting with the highest.
            foreach (var currentCard in orderedCards)
            {
                // Four (and below) cannot be the highest card in a straight.
                if (currentCard.Rank == CardRank.Four)
                {
                    break;
                }

                // Get all cards that would make up a straight with the currentCard as the highest card.
                // If the current card is a Five, then the Ace can also be part of the straight.
                var cardsForStraight = cards
                    .Where(card => PokerHand.IsStraightCard(currentCard, card, mustBeFlush))
                    .DistinctBy(card => card.Rank);

                // If there are exactly 5 cards in cardsForStraight, then we have a straight.
                if (cardsForStraight.Count() == 5)
                {
                    return cardsForStraight.ToList();
                }
            }

            // No straight (flush) found.
            return null;
        }

        /// <summary>
        /// Checks if a card is part of a <see cref="HandRanking.Straight"/> from the highest card.
        /// </summary>
        /// <param name="highestCard">The highest card to start the <see cref="HandRanking.Straight"/> from.</param>
        /// <param name="card">The card to check.</param>
        /// <param name="mustBeFlush">Specifies whether the hand must be a <see cref="HandRanking.StraightFlush"/>.</param>
        /// <returns>True if the card is part of a <see cref="HandRanking.Straight"/>, false otherwise.</returns>
        private static bool IsStraightCard(Card highestCard, Card card, bool mustBeFlush = false)
        {
            return (
                    card.Equals(highestCard)
                    || card.Rank == highestCard.Rank - 1
                    || card.Rank == highestCard.Rank - 2
                    || card.Rank == highestCard.Rank - 3
                    || card.Rank == highestCard.Rank - 4
                    || (highestCard.Rank == CardRank.Five && card.Rank == CardRank.Ace)
                ) && (!mustBeFlush || card.Suit == highestCard.Suit);
        }

        #endregion

        #region winsAgainst

        /// <summary>
        /// Checks if <see langword="this"/> <see cref="PokerHand"/> has a better <see cref="HandRank"/> than the <paramref name="other"/> hand.
        /// </summary>
        /// <param name="other">The other hand to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if this hand has a better <see cref="HandRank"/>.
        /// <see langword="false"/> if the other hand has a better <see cref="HandRank"/>.
        /// <see langword="null"/> if they have the same <see cref="HandRank"/>.
        /// </returns>
        private bool? HasBetterHandRank(PokerHand other)
        {
            return this.HandRank == other.HandRank ? null : this.HandRank > other.HandRank;
        }

        /// <summary>
        /// Checks if <see langword="this"/> <see cref="PokerHand"/> has a better <see cref="PrimaryCardRank"/> than the <paramref name="other"/> hand.
        /// </summary>
        /// <param name="other">The other hand to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if this hand has a better <see cref="PrimaryCardRank"/>.
        /// <see langword="false"/> if the other hand has a better <see cref="PrimaryCardRank"/>.
        /// <see langword="null"/> if they have the same <see cref="PrimaryCardRank"/>.
        /// </returns>
        private bool? HasBetterFirstCard(PokerHand other)
        {
            return this.PrimaryCardRank == other.PrimaryCardRank
                ? null
                : this.PrimaryCardRank > other.PrimaryCardRank;
        }

        /// <summary>
        /// Checks if <see langword="this"/> <see cref="PokerHand"/> has a better <see cref="SecondaryCardRank"/> than the <paramref name="other"/> hand.
        /// </summary>
        /// <param name="other">The other hand to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if this hand has a better <see cref="SecondaryCardRank"/>.
        /// <see langword="false"/> if the other hand has a better <see cref="SecondaryCardRank"/>.
        /// <see langword="null"/> if they have the same <see cref="SecondaryCardRank"/>.
        /// </returns>
        private bool? HasBetterSecondCard(PokerHand other)
        {
            return this.SecondaryCardRank == other.SecondaryCardRank
                ? null
                : this.SecondaryCardRank > other.SecondaryCardRank;
        }

        /// <summary>
        /// Checks if <see langword="this"/> <see cref="PokerHand"/> has a better <see cref="Kickers"/> than the <paramref name="other"/> hand.
        /// </summary>
        /// <param name="other">The other hand to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if this hand has a better <see cref="Kickers"/>.
        /// <see langword="false"/> if the other hand has a better <see cref="Kickers"/>.
        /// <see langword="null"/> if they have the same <see cref="Kickers"/>.
        /// </returns>
        private bool? HasBetterKickers(PokerHand other)
        {
            for (var i = 0; i < this.Kickers.Count; i++)
            {
                if (this.Kickers[i].Rank > other.Kickers[i].Rank)
                {
                    return true;
                }

                if (this.Kickers[i].Rank < other.Kickers[i].Rank)
                {
                    return false;
                }
            }

            return null;
        }

        #endregion

        #region getWinningHand

        /// <summary>
        /// Gets the best hand(s) among the given hands based solely on the <see cref="Kickers"/>.
        /// </summary>
        /// <param name="hands">A list of <see cref="PokerHand"/>s to evaluate.</param>
        /// <returns>A list of <see cref="PokerHand"/>s with the best <see cref="Kickers"/>.</returns>
        private static List<PokerHand> GetWinningHandByKickers(IEnumerable<PokerHand> hands)
        {
            // Create a list of hands with the highest kickers.
            var handsWithHighestKickers = hands.ToList();

            // Iterate through each kicker position.
            for (var i = 0; i < handsWithHighestKickers.First().Kickers.Count; i++)
            {
                Card? highestKicker = null;

                // Find the highest kicker at the current index among all hands.
                foreach (var hand in hands)
                {
                    var handKicker = hand.Kickers[i];
                    if (highestKicker == null || handKicker.Rank > highestKicker.Value.Rank)
                    {
                        highestKicker = handKicker;
                    }
                }

                // Filter hands with the kicker of the current index having the same rank as the highest kicker.
                handsWithHighestKickers = hands
                    .Where(hand => hand.Kickers[i].Rank == highestKicker!.Value.Rank)
                    .ToList();

                // If there's only one hand with this kicker, the winner is found.
                if (handsWithHighestKickers.Count == 1)
                {
                    return handsWithHighestKickers.ToList();
                }
            }

            // In the case of multiple hands with the exact same kickers, return these hands.
            return handsWithHighestKickers.ToList();
        }

        #endregion

        #endregion

        #endregion
    }
}
