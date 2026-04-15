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
    }
}
