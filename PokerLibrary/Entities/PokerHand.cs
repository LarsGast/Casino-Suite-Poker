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
        public PokerHand(
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
