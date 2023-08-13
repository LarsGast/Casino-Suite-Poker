using System;
using System.Collections.Generic;
using System.Linq;

namespace Casino_Suite_Poker {
	public class Deck {

		/// <summary>
		/// The 52 cards of this deck.
		/// Possibly sorted.
		/// </summary>
		private List<Card> _cards { get; set; }

		/// <summary>
		/// The index of the las drawn card.
		/// -1 if no card has been drawn yet.
		/// </summary>
		private int _index { get ; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="cards"></param>
		private Deck(IEnumerable<Card> cards) {
			this._cards = cards.ToList();
			this._index = -1;
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
		/// <param name="includeDrawnCards">Whether the deck will shuffle the already drawn cards back into the deck.</param>
		/// <param name="random">Random variable for drawing many cards in one instant.</param>
		public void shuffle(bool includeDrawnCards = true, Random random = null) {

			// Initiate random if not done already.
			if (random == null) {
				random = new Random();
			}

			var cardsToNotFhuffle = new List<Card>();
			var cardsToShuffle = this._cards;

			// If we don't want to include the already drawn cards, then exclude those when shuffling the deck.
			if (!includeDrawnCards) {

				cardsToNotFhuffle = this._cards
					.Where(card => this._cards.IndexOf(card) <= this._index)
					.ToList();

				cardsToShuffle = this._cards
					.Where(card => this._cards.IndexOf(card) > this._index)
					.ToList();
			}
			else {
				// Reset the index so cards get drawn from the top again.
				this.reset();
			}

			// Actually shuffle the cards.
			// Add the already drawn cards to the front of the deck (in order) if neccecary.
			var shuffledCards = cardsToShuffle.OrderBy(card => random.Next()).ToList();
			this._cards = cardsToNotFhuffle.Union(shuffledCards).ToList();
		}

		/// <summary>
		/// Draw a card from the deck by the next index.
		/// The card does not actually get removed from the deck.
		/// </summary>
		/// <returns>The card corresponding to the next index.</returns>
		public Card draw() {

			this._index++;

			if (this._index >= this._cards.Count) {
				return null;
			}

			var card = this._cards[this._index];			

			return card;
		}

		/// <summary>
		/// Resets the deck.
		/// This means adding the already drawn cards back into the deck, in the same order.
		/// This does NOT mean un-shuffling the deck.
		/// </summary>
		public void reset() {
			this._index = -1;
		}
	}
}
