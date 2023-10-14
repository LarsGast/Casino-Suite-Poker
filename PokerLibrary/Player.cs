using DeckOfCardsLibrary;

namespace PokerLibrary {

	public class Player {

		/// <summary>
		/// Unique identifier.
		/// E.g. name of player number.
		/// </summary>
		public string identifier { get; set; }

		/// <summary>
		/// The cards that the player gets dealt.
		/// </summary>
		public List<Card> cards { get; set; }

		/// <summary>
		/// The best possible hand considering the cards the player received and the cards on the table.
		/// Null if not yet assigned.
		/// </summary>
		private PokerHand? _hand { get; set; }

		/// <summary>
		/// The best possible hand considering the cards the player received and the cards on the table.
		/// </summary>
		public PokerHand hand {
			get {
				if (this._hand == null) {
					throw new Exception("No valid hand");
				}

				return this._hand;
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="identifier"></param>
		public Player(string identifier) {
			this.identifier = identifier;
			this.cards = new List<Card>();
		}

		/// <summary>
		/// Add a card to the players hand.
		/// </summary>
		/// <param name="card"></param>
		public void addCard(Card card) {
			this.cards.Add(card);
		}

		/// <summary>
		/// Set the _pokerHand property to the best possible hand the player can make with the given cards.
		/// </summary>
		/// <param name="tableCards"></param>
		public void setPokerHand(List<Card> tableCards) {
			this._hand = PokerHand.getBestHand(tableCards.Union(this.cards));
		}
	}
}
