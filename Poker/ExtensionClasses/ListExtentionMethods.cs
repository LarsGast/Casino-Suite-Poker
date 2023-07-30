using Casino_Suite_Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.ExtentionClasses {
	public static class ListExtentionMethods {

		/// <summary>
		/// Extension method.
		/// Check if two lists of cards are equal.
		/// </summary>
		/// <param name="cards"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool equals(this IEnumerable<Card> cards, IEnumerable<Card> other) { 
			
			// Both null.
			if (cards == null && other == null) {
				return true;
			}

			// We know they are not both null because of the check above.
			if (cards == null || other == null) {
				return false;
			}

			// Not the same amount of items
			if (cards.Count() != other.Count()) {
				return false;
			}

			// Every single card (in order) is the same.
			var orderedCards = cards.OrderBy(card => card.cardValue).ThenBy(card => card.suit).ToList();
			var orderedOther = other.OrderBy(card => card.cardValue).ThenBy(card => card.suit).ToList();
			for (var i = 0; i < cards.Count(); i++) {
				if (!orderedCards[i].equals(orderedOther[i])) {
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Extension method.
		/// Gets display string of the list of cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		public static string getDisplayString(this IEnumerable<Card> cards) {
			return String.Join(",", cards.Select(card => card.suit.getDisplayString() + card.cardValue.getDisplayString()));
		}
	}
}
