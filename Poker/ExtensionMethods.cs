using Casino_Suite_Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker {
	public static class ExtensionMethods {

		/// <summary>
		/// Extension method.
		/// Gets the unicode value of the suit as a string for displaying.
		/// </summary>
		/// <param name="suit"></param>
		/// <returns></returns>
		public static string getDisplayString(this Card.Suit suit) { 
			switch(suit) {
				case Card.Suit.Hearts:
					return "\u2665";
				case Card.Suit.Spades:
					return "\u2660";
				case Card.Suit.Diamonds:
					return "\u2666";
				case Card.Suit.Clubs:
					return "\u2663";
			}

			return null;
		}

		/// <summary>
		/// Extension method.
		/// Gets the value of the card as a string for displaying.
		/// </summary>
		/// <param name="cardValue"></param>
		/// <returns></returns>
		public static string getDisplayString(this Card.CardValue cardValue) {
			switch (cardValue) {
				case Card.CardValue.Jack:
					return "J";
				case Card.CardValue.Queen:
					return "Q";
				case Card.CardValue.King:
					return "K";
				case Card.CardValue.Ace:
					return "A";
				default:
					return ((int)cardValue).ToString();
			}
		}
	}
}
