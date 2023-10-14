using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.ExtentionClasses;

namespace Poker_Console {
	internal class Program {
		static void Main(string[] args) {

			// Get all the players.
			var players = Program.getPlayers();

			// Get the deck and shuffle it.
			var deck = Deck.get();
			deck.shuffle();

			// Draw 2 cards for each player.
			Program.drawPlayerCards(players, deck);

			// Draw 5 cards on the table.
			List<Card> tableCards = Program.drawTableCards(deck);

			// Determine the winner(s).
			List<Player> winningPlayers = getWinningPlayers(players, tableCards);

			// Display everything.
			Program.displayGame(players, tableCards, winningPlayers);

			// ReadKey so the app doesn't close.
			Console.ReadKey();
		}

		/// <summary>
		/// Define the possible players.
		/// </summary>
		/// <returns></returns>
		private static List<Player> getPlayers() {
			// Define the possible players.
			var playerIdentifiers = new List<string>() {
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
			if (numberOfPlayers > playerIdentifiers.Count) {
				throw new Exception("Not enough player identifiers");
			}

			if (numberOfPlayers < 2) {
				throw new Exception("At least 2 players should be participating.");
			}

			// Make a player for each player name.
			var players = new List<Player>();
			for (int i = 0; i < numberOfPlayers; i++) {
				players.Add(new Player(playerIdentifiers[i]));
			}

			return players;
		}

		/// <summary>
		/// Draw two cards for each player.
		/// </summary>
		/// <param name="players"></param>
		/// <param name="deck"></param>
		/// <exception cref="Exception"></exception>
		private static void drawPlayerCards(List<Player> players, Deck deck) {
			for (int i = 0; i < 2; i++) {
				foreach (var player in players) {
					var card = deck.draw();
					if (card == null) {
						throw new Exception("There are no more cards in the deck.");
					}

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
		private static List<Card> drawTableCards(Deck deck) {
			var tableCards = new List<Card>();
			for (int i = 0; i < 5; i++) {
				var card = deck.draw();
				if (card == null) {
					throw new Exception("There are no more cards in the deck.");
				}

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
			var bestHand = PokerHand.getWinningHand(players.Select(player => player.hand));

			// Get all the players that draw with the best hand.
			// More than one player can have the best hand.
			var winningPlayers = players.Where(player => player.hand.winsAgainst(bestHand) == null).ToList();
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
			Console.WriteLine(tableCards.getDisplayString());
			Console.WriteLine("");

			foreach (var player in players) {
				Console.WriteLine($"Identifier: {player.identifier}");
				Console.WriteLine($"Cards: {player.cards.getDisplayString()}");
				Console.WriteLine($"Hand Type: {player.hand.handType}");
				Console.WriteLine($"Highest value for this hand type: {(player.hand.firstCardValue?.getDisplayString())}");
				Console.WriteLine($"Second highest value for this hand type: {(player.hand.secondCardValue?.getDisplayString())}\n" +
					$"Kickers: {player.hand.kickers.getDisplayString()}");
				Console.WriteLine("");
			}

			Console.WriteLine("Winning player(s):");
			foreach (var player in winningPlayers) {
				Console.WriteLine(player.identifier);
			}
		}
	}
}
