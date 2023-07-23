using System.Runtime.CompilerServices;

namespace Casino_Suite_Poker {
	public class Card {

		/// <summary>
		/// Card value.
		/// Two is the lowest value.
		/// Ace is the highest value.
		/// </summary>
		public enum CardValue {
			Two = 2,
			Three,
			Four,
			Five,
			Six,
			Seven,
			Eight,
			Nine,
			Ten,
			Jack,
			Queen,
			King,
			Ace
		}

		/// <summary>
		/// Card suit.
		/// </summary>
		public enum Suit {
			Hearts,
			Spades,
			Diamonds,
			Clubs
		}

		/// <summary>
		/// Value of the card.
		/// This includes "Two" until "Ten", and "Jack" until "Ace".
		/// </summary>
		public CardValue cardValue { get; private set; }

		/// <summary>
		/// The suit of the card.
		/// This includes "Hearts", "Spades", "Diamonds", and "Clubs".
		/// </summary>
		public Suit suit { get; private set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="cardValue"></param>
		/// <param name="suit"></param>
		public Card(CardValue cardValue, Suit suit) {
			this.cardValue = cardValue;
			this.suit = suit;
		}
	}
}