using System;
using System.Collections.Generic;
using System.Linq;

namespace Casino_Suite_Poker {
	public class Deck {

		/// <summary>
		/// The 52 cards of this deck.
		/// Possibly sorted.
		/// </summary>
		public List<Card> cards {get; private set;}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="cards"></param>
		private Deck(List<Card> cards) {
			this.cards = cards;
		}

		/// <summary>
		/// Gets an (unsorted) deck of 52 cards.
		/// </summary>
		/// <returns></returns>
		public static Deck get() {
			var cards = new List<Card>();

			// Create a card for each suit-value combination.
			foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit))) {
				foreach (Card.CardValue value in Enum.GetValues(typeof(Card.CardValue))) {
					cards.Add(new Card(value, suit));
				}
			}

			return new Deck(cards);
		}

		/// <summary>
		/// Shuffles the cards in the deck.
		/// </summary>
		public void shuffle(Random random = null) {

			if (random == null) {
				random = new Random();
			}

			this.cards = this.cards.OrderBy(card => random.Next()).ToList();
		}
	}
}
