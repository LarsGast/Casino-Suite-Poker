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
    public sealed record PokerHand(
        HandRanking HandRank,
        CardRank? PrimaryCardRank,
        CardRank? SecondaryCardRank,
        CardSuit? HandSuit,
        IReadOnlyList<Card> Kickers
    )
    {
        /// <summary>
        /// The specific ranking of the hand, such as <see cref="HandRanking.HighCard"/> or <see cref="HandRanking.StraightFlush"/>.
        /// </summary>
        public HandRanking HandRank { get; } = HandRank;

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
        public CardRank? PrimaryCardRank { get; } = PrimaryCardRank;

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
        public CardRank? SecondaryCardRank { get; } = SecondaryCardRank;

        /// <summary>
        /// The <see cref="CardSuit"/> of the hand.
        /// </summary>
        /// <value>
        /// Relevant for <see cref="HandRanking.Flush"/> and <see cref="HandRanking.StraightFlush"/>.
        /// <see langword="null"/> for other hand ranks.
        /// </value>
        public CardSuit? HandSuit { get; } = HandSuit;

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
        public IReadOnlyList<Card> Kickers { get; } = Kickers;
    }
}
