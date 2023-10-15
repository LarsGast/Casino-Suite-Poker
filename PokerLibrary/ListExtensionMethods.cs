using DeckOfCardsLibrary;

namespace PokerLibrary {

	/// <summary>
	/// Provides extension methods for working with lists of cards.
	/// </summary>
	public static class ListExtensionMethods {

        /// <summary>
        /// Extension method.
        /// Checks if two lists of cards are equal.
        /// </summary>
        /// <param name="cards">The first list of cards.</param>
        /// <param name="other">The second list of cards.</param>
        /// <returns>True if the lists are equal, otherwise false.</returns>
        public static bool equals(this IEnumerable<Card> cards, IEnumerable<Card> other) {

            // Both lists are null.
            if (cards == null && other == null) {
                return true;
            }

            // Only one of the lists is null and the other isn't.
            if (cards == null || other == null) {
                return false;
            }

            // Different number of cards.
            if (cards.Count() != other.Count()) {
                return false;
            }

            // Every single card (in order) is the same.
            var orderedCards = cards.OrderBy(card => card.rank).ThenBy(card => card.suit).ToList();
            var orderedOther = other.OrderBy(card => card.rank).ThenBy(card => card.suit).ToList();
            for (var i = 0; i < cards.Count(); i++) {
                if (!orderedCards[i].equals(orderedOther[i])) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Extension method.
        /// Gets the display string of the list of cards.
        /// </summary>
        /// <param name="cards">The list of cards.</param>
        /// <param name="displayTenAsT">Whether the rank "Ten" should be displayed as "T". Default is false (displays "10").</param>
        /// <returns>A string representation of the cards.</returns>
        public static string getDisplayString(this IEnumerable<Card> cards, bool displayTenAsT = false) {
            return string.Join(",", cards.Select(card => card.getDisplayString(displayTenAsT: displayTenAsT)));
        }
    }
}
