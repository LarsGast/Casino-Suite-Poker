using DeckOfCardsLibrary;

namespace PokerLibrary {
	internal class Table {

		/// <summary>
		/// The four standard rounds of a poker game
		/// </summary>
		private enum Round { 
			preFlop,
			flop,
			turn,
			river
		}

		/// <summary>
		/// The deck used for this game of poker.
		/// </summary>
		private Deck _deck { get; set; }

		/// <summary>
		/// The players present at this game of poker.
		/// </summary>
		private List<Player> _players { get; set; }

		/// <summary>
		/// The cards that are dealt on the table.
		/// </summary>
		private List<Card> _tableCards { get; set; }

		/// <summary>
		/// The current round of this game of poker.
		/// </summary>
		private Round _currentRound { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="players"></param>
		private Table(List<Player> players) {
			this._players = players;
			this._deck = Deck.get();
			this._tableCards = new List<Card>();
			this._currentRound = Round.preFlop;
		}

		/// <summary>
		/// Create a new table based on the given player identifiers.
		/// The player identifiers will become Player classes.
		/// The deck of cards used in the game will be initialised in the constructor.
		/// </summary>
		/// <param name="playerIdentifiers"></param>
		/// <returns></returns>
		public static Table get(List<string> playerIdentifiers) {

			var players = new List<Player>();

			foreach(var playerIdentifier in playerIdentifiers) {
				players.Add(new Player(playerIdentifier));
			}

			return new Table(players);
		}

		/// <summary>
		/// Play a round of poker.
		/// Which round is played is based on the _currentRound property.
		/// </summary>
		/// <exception cref="Exception"></exception>
		public void playRound() {
			switch (this._currentRound) {
				case Round.preFlop:
					this.playPreFlop(); 
					break;
				case Round.flop:
					this.playFlop();
					break;
				case Round.turn:
					this.playTurn();
					break;
				case Round.river:
					this.playRiver();
					break;
				default:
					throw new Exception("The river is already played.");
			}

			this._currentRound++;
		}

		/// <summary>
		/// Play the pre flop.
		/// So give every player two cards.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void playPreFlop() {

			this._deck.shuffle();

            foreach (var player in this._players) {
				var card = this._deck.draw();

				if (card == null) {
					throw new Exception("Not enough cards in the deck");
				}

				player.addCard(card);
            }
        }

		/// <summary>
		/// Play the flop.
		/// So draw three cards for the table.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void playFlop() {
			for (int i = 0;  i < 3; i++) {
				var card = this._deck.draw();

				if (card == null) {
					throw new Exception("Not enough cards in the deck");
				}

				this._tableCards.Add(card);
			}
		}

		/// <summary>
		/// Play the turn.
		/// So draw one card for the table.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void playTurn() {
			var card = this._deck.draw();

			if (card == null) {
				throw new Exception("Not enough cards in the deck");
			}

			this._tableCards.Add(card);
		}

		/// <summary>
		/// Play the river.
		/// So draw one card for the table.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void playRiver() {
			var card = this._deck.draw();

			if (card == null) {
				throw new Exception("Not enough cards in the deck");
			}

			this._tableCards.Add(card);
		}

		/// <summary>
		/// Get the winners of this game of poker.
		/// </summary>
		/// <returns>The player(s) with the best hand of the game</returns>
		public List<Player> getWinners() {

			foreach (var player in this._players) {
				player.setPokerHand(this._tableCards);
			}

			// Get the best hand amongst all the players.
			var bestHand = PokerHand.getWinningHand(this._players.Select(player => player.hand));

			// Get all the players that draw with the best hand.
			// More than one player can have the best hand.
			var winningPlayers = this._players.Where(player => player.hand.winsAgainst(bestHand) == null).ToList();

			return winningPlayers;
		}
	}
}
