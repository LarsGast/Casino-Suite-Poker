using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.ExtensionClasses;

namespace Poker_Console {
	internal class Program {
		static void Main(string[] args) {

			// Get all the players.
			var players = Program.getPlayers();

			// Get the deck and shuffle it.
			var deck = Deck.get();
			deck.shuffle();

			// Draw 2 cards for each player.
			Program.drawCardsForPlayers(players, deck);

			// Draw 5 cards on the table.
			List<Card> tableCards = Program.drawCardsForTable(deck);

			// Determine the winner(s).
			List<Player> winningPlayers = getWinningPlayers(players, tableCards);

			// Display everything.
			Program.displayGame(players, tableCards, winningPlayers);

			// ReadKey so the window doesn't instantly close.
			Console.ReadKey();
		}

		/// <summary>
		/// Define the possible players.
		/// </summary>
		/// <returns></returns>
		private static List<Player> getPlayers() {
			var playerNames = new List<string>() {
				"Luz",
				"Eda",
				"King",
				"Amity",
				"Willow",
				"Gus",
				"Hunter",
				"Vee"
			};

			// Define how many players will participate in the game.
			var numberOfPlayers = 3;

			// No more players than the number of names.
			if (numberOfPlayers > playerNames.Count) {
				throw new Exception("Not enough player names");
			}

			if (numberOfPlayers < 2) {
				throw new Exception("At least 2 players should be participating.");
			}

			// Make a player for each player name.
			var players = new List<Player>();
			for (int i = 0; i < numberOfPlayers; i++) {
				players.Add(new Player(playerNames[i]));
			}

			return players;
		}

		/// <summary>
		/// Draw two cards for each player.
		/// </summary>
		/// <param name="players"></param>
		/// <param name="deck"></param>
		/// <exception cref="Exception"></exception>
		private static void drawCardsForPlayers(List<Player> players, Deck deck) {
			for (int i = 0; i < 2; i++) {
				foreach (var player in players) {
					var card = deck.draw() ?? throw new Exception("There are no more cards in the deck.");

					player.addCard(card);
				}
			}
		}

		/// <summary>
		/// Draw five cards for the table.
		/// </summary>
		/// <param name="deck"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private static List<Card> drawCardsForTable(Deck deck) {
			var tableCards = new List<Card>();
			for (int i = 0; i < 5; i++) {
				var card = deck.draw() ?? throw new Exception("There are no more cards in the deck.");

				tableCards.Add(card);
			}

			return tableCards;
		}

		/// <summary>
		/// Gets a list of players with the best hand.
		/// Only contains more than one player if the players with the best hand tie.
		/// </summary>
		/// <param name="players"></param>
		/// <param name="tableCards"></param>
		/// <returns></returns>
		private static List<Player> getWinningPlayers(List<Player> players, List<Card> tableCards) {
			// Determine the best hand for each player.
			foreach (var player in players) {
				player.setPokerHand(tableCards);
			}

			// Get the best hand amongst all the players.
			var bestHand = PokerHand.getWinningHand(players.Select(player => player.hand!));

			// Get all the players that draw with the best hand.
			// More than one player can have the best hand.
			var winningPlayers = players.Where(player => player.hand!.winsAgainst(bestHand) == null).ToList();
			return winningPlayers;
		}

		/// <summary>
		/// Display everything about the game.
		/// </summary>
		/// <param name="players"></param>
		/// <param name="tableCards"></param>
		/// <param name="winningPlayers"></param>
		private static void displayGame(List<Player> players, List<Card> tableCards, List<Player> winningPlayers) {
			Console.WriteLine("Table:");
			Console.WriteLine(tableCards.getDisplayString(displayTenAsT: true));
			Console.WriteLine("");
			Console.WriteLine("Players:");

			foreach (var player in players) {
				Console.WriteLine($"Name: {player.name}");
				Console.WriteLine($"Cards: {player.cards.getDisplayString(displayTenAsT: true)}");
				Console.WriteLine($"Hand Type: {player.hand!.handType}");
				Console.WriteLine($"Highest value for this hand type: {(player.hand.firstCardValue?.getDisplayString(displayTenAsT: true))}");
				Console.WriteLine($"Second highest value for this hand type: {(player.hand.secondCardValue?.getDisplayString(displayTenAsT: true))}\n" +
					$"Kickers: {player.hand.kickers.getDisplayString(displayTenAsT: true)}");
				Console.WriteLine("");
			}

			Console.WriteLine("Winning player(s):");
			foreach (var player in winningPlayers) {
				Console.WriteLine(player.name);
			}

			Console.WriteLine("");
			Console.WriteLine("(Press any key to close this window)");
		}
	}

	/// <summary>
	/// Possible Player class.
	/// </summary>
	internal class Player {
		
		/// <summary>
		/// Name of the player.
		/// </summary>
		public string name { get; set; }

		/// <summary>
		/// The cards that the player gets dealt.
		/// </summary>
		public List<Card> cards { get; set; }

		/// <summary>
		/// The best possible hand considering the cards the player received and the cards on the table.
		/// Null if not yet assigned.
		/// </summary>
		public PokerHand? hand { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name"></param>
		public Player(string name) {
			this.name = name;
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
			this.hand = PokerHand.getBestHand(tableCards.Union(this.cards));
		}
	}
}
