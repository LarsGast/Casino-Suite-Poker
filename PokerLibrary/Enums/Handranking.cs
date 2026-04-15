using DeckOfPlayingCardsLibrary.Enums;

namespace PokerLibrary.Enums
{
    /// <summary>
    /// Represents the ranking of a poker hand, ranging from High Card to Straight Flush.
    /// </summary>
    /// <remarks>
    /// Excludes the Royal Flush, which is the highest form of a Straight Flush.
    /// Ordered from lowest to highest rank, with High Card being the weakest and Straight Flush being the strongest.
    /// </remarks>
    public enum HandRanking
    {
        /// <summary>
        /// Highest card when no other combination is possible.
        /// </summary>
        HighCard,

        /// <summary>
        /// Two cards of the same <see cref="CardRank"/>.
        /// </summary>
        Pair,

        /// <summary>
        /// Two cards of one <see cref="CardRank"/> and two of another.
        /// </summary>
        TwoPair,

        /// <summary>
        /// Three cards of the same <see cref="CardRank"/>.
        /// </summary>
        ThreeOfAKind,

        /// <summary>
        /// Five consecutive cards of different <see cref="CardSuit"/>s.
        /// </summary>
        /// <remarks>
        /// If all cards are of the same <see cref="CardSuit"/>, it would be a <see cref="StraightFlush"/>.
        /// </remarks>
        Straight,

        /// <summary>
        /// Five cards of the same <see cref="CardSuit"/> in any order.
        /// </summary>
        Flush,

        /// <summary>
        /// Three cards of the same <see cref="CardRank"/> and two of another.
        /// </summary>
        FullHouse,

        /// <summary>
        /// Four cards of the same <see cref="CardRank"/>.
        /// </summary>
        FourOfAKind,

        /// <summary>
        /// Five consecutive cards in the same <see cref="CardSuit"/>.
        /// </summary>
        StraightFlush,
    }
}
