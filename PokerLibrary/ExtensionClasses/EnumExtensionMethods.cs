using DeckOfCardsLibrary;
using static DeckOfCardsLibrary.Card;

namespace PokerLibrary {
	public static class EnumExtensionMethods {

		/// <summary>
		/// Extension method.
		/// Gets the unicode value of the suit as a string for displaying.
		/// </summary>
		/// <param name="suit"></param>
		/// <returns></returns>
		public static string? getDisplayString(this Suit suit) {
			switch (suit) {
				case Card.Suit.Hearts:
					return "\u2665";
				case Card.Suit.Spades:
					return "\u2660";
				case Card.Suit.Diamonds:
					return "\u2666";
				case Card.Suit.Clubs:
					return "\u2663";
				default:
					return null;
			}
		}

		/// <summary>
		/// Extension method.
		/// Gets the value of the card as a string for displaying.
		/// </summary>
		/// <param name="cardValue"></param>
		/// <returns></returns>
		public static string getDisplayString(this Rank cardValue) {
			switch (cardValue) {
				case Card.Rank.Jack:
					return "J";
				case Card.Rank.Queen:
					return "Q";
				case Card.Rank.King:
					return "K";
				case Card.Rank.Ace:
					return "A";
				default:
					return ((int)cardValue).ToString();
			}
		}
	}
}
