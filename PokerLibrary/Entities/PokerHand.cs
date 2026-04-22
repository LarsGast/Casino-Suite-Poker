using DeckOfPlayingCardsLibrary.Entities;
using DeckOfPlayingCardsLibrary.Enums;
using PokerLibrary.Enums;

namespace PokerLibrary.Entities
{
    /// <summary>
    /// Represents a 5-card poker hand, which is a combination of playing cards used in various poker games.
    /// </summary>
    /// <remarks>
    /// This record provides an immutable representation of a poker hand evaluated from a collection of cards.
    /// </remarks>
    public sealed record PokerHand
    {
        /// <summary>
        /// The specific ranking of the hand, such as <see cref="HandRanking.HighCard"/> or <see cref="HandRanking.StraightFlush"/>.
        /// </summary>
        public HandRanking HandRank { get; }

        /// <summary>
        /// The primary card <see cref="CardRank"/> relevant to this hand.
        /// </summary>
        /// <value>
        /// <list type="bullet">
        ///     <item>For <see cref="HandRanking.Pair"/> and <see cref="HandRanking.TwoPair"/>: the rank of the highest pair.</item>
        ///     <item>For <see cref="HandRanking.ThreeOfAKind"/> and <see cref="HandRanking.FullHouse"/>: the rank of the highest three of a kind.</item>
        ///     <item>For <see cref="HandRanking.FourOfAKind"/>: the rank of the four of a kind.</item>
        ///     <item><see langword="null"/> for other hand ranks.</item>
        /// </list>
        /// </value>
        public CardRank? PrimaryCardRank { get; }

        /// <summary>
        /// The secondary card <see cref="CardRank"/> relevant to this hand.
        /// </summary>
        /// <value>
        /// <list type="bullet">
        ///     <item>For <see cref="HandRanking.TwoPair"/>: the rank of the second-highest pair.</item>
        ///     <item>For <see cref="HandRanking.FullHouse"/>: the rank of the highest pair that is not part of the three of a kind.</item>
        ///     <item><see langword="null"/> for other hand ranks.</item>
        /// </list>
        /// </value>
        public CardRank? SecondaryCardRank { get; }

        /// <summary>
        /// The <see cref="CardSuit"/> of the hand.
        /// </summary>
        /// <value>
        /// Relevant for <see cref="HandRanking.Flush"/> and <see cref="HandRanking.StraightFlush"/>.
        /// <see langword="null"/> for other hand ranks.
        /// </value>
        public CardSuit? HandSuit { get; }

        /// <summary>
        /// A sorted read-only list of kickers, the cards used to enhance the hand.
        /// </summary>
        /// <remarks>
        /// <para>
        /// These are all the cards not part of the primary hand but still contribute to it.
        /// </para>
        /// <para>
        /// For <see cref="HandRanking.Straight"/> and <see cref="HandRanking.StraightFlush"/> hands with an Ace-low straight (A-2-3-4-5),
        /// the Ace is positioned last in the list since it acts as the low card.
        /// </para>
        /// <para>
        /// For all other hands, kickers are sorted in descending order by rank.
        /// </para>
        /// </remarks>
        /// <value>
        /// <see langword="null"/> for <see cref="HandRanking.FullHouse"/>.
        /// </value>
        public IReadOnlyList<Card>? Kickers { get; }

        /// <summary>
        /// <inheritdoc cref="PokerHand"/>
        /// </summary>
        /// <param name="handRank"><inheritdoc cref="HandRank"/></param>
        /// <param name="primaryCardRank"><inheritdoc cref="PrimaryCardRank"/></param>
        /// <param name="secondaryCardRank"><inheritdoc cref="SecondaryCardRank"/></param>
        /// <param name="handSuit"><inheritdoc cref="HandSuit"/></param>
        /// <param name="kickers"><inheritdoc cref="Kickers"/></param>
        public PokerHand(
            HandRanking handRank,
            CardRank? primaryCardRank,
            CardRank? secondaryCardRank,
            CardSuit? handSuit,
            IReadOnlyList<Card>? kickers
        )
        {
            this.HandRank = handRank;
            this.PrimaryCardRank = primaryCardRank;
            this.SecondaryCardRank = secondaryCardRank;
            this.HandSuit = handSuit;
            this.Kickers = kickers;
        }

        /// <inheritdoc/>
        public bool Equals(PokerHand? hand)
        {
            return hand is not null
                && this.HandRank == hand.HandRank
                && this.PrimaryCardRank == hand.PrimaryCardRank
                && this.SecondaryCardRank == hand.SecondaryCardRank
                && this.HandSuit == hand.HandSuit
                && (
                    this.Kickers is null && hand.Kickers is null
                    || this.Kickers is not null
                        && hand.Kickers is not null
                        && this.Kickers.SequenceEqual(hand.Kickers)
                );
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(this.EqualityContract);
            hashCode.Add(this.HandRank);
            hashCode.Add(this.PrimaryCardRank);
            hashCode.Add(this.SecondaryCardRank);
            hashCode.Add(this.HandSuit);

            if (this.Kickers is not null)
            {
                foreach (var kicker in this.Kickers)
                {
                    hashCode.Add(kicker);
                }
            }

            return hashCode.ToHashCode();
        }
    }
}
