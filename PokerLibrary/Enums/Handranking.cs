using static DeckOfCardsLibrary.Card;

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
        /// Two cards of the same <see cref="Rank"/>.
        /// </summary>
        Pair,

        /// <summary>
        /// Two cards of one <see cref="Rank"/> and two of another.
        /// </summary>
        TwoPair,

        /// <summary>
        /// Three cards of the same <see cref="Rank"/>.
        /// </summary>
        ThreeOfAKind,

        /// <summary>
        /// Five consecutive cards of different <see cref="Suit"/>s.
        /// </summary>
        Straight,

        /// <summary>
        /// Five cards of the same <see cref="Suit"/> in any order.
        /// </summary>
        Flush,

        /// <summary>
        /// Three cards of the same <see cref="Rank"/> and two of another.
        /// </summary>
        FullHouse,

        /// <summary>
        /// Four cards of the same <see cref="Rank"/>.
        /// </summary>
        FourOfAKind,

        /// <summary>
        /// Five consecutive cards in the same <see cref="Suit"/>.
        /// </summary>
        StraightFlush,
    }
}
