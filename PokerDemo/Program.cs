using DeckOfCardsLibrary;
using PokerLibrary;
using PokerLibrary.ExtentionClasses;

namespace Poker_Console {
	internal class Program {
		static void Main(string[] args) {

			// Define the possible players.
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

			// Get the deck and shuffle it.
			var deck = Deck.get();
			deck.shuffle();

			// Draw 2 cards for each player.
			for (int i = 0; i < 2; i++) {
				foreach (var player in players) {
					player.addCard(deck.draw());
				}
			}

			// Draw 5 cards on the table.
			var tableCards = new List<Card>();
			for (int i = 0; i < 5; i++) {
				tableCards.Add(deck.draw());
			}

			// Determine the best hand for each player.
			foreach (var player in players) {
				player.setPokerHand(tableCards);
			}

			// Get the best hand amongst all the players.
			var bestHand = PokerHand.getWinningHand(players.Select(player => player.hand));

			// Get all the players that draw with the best hand.
			// More than one player can have the best hand.
			var winningPlayers = players.Where(player => player.hand.winsAgainst(bestHand) == null).ToList();

			// Display everything.
			Console.WriteLine("Table:");
			Console.WriteLine(tableCards.getDisplayString());
			Console.WriteLine("");

			foreach (var player in players) {
				Console.WriteLine($"Name: {player.name}");
				Console.WriteLine($"Cards: {player.cards.getDisplayString()}");
				Console.WriteLine($"Hand Type: {player.hand.handType}");
				Console.WriteLine($"Highest value for this hand type: {(player.hand.firstCardValue != null ? player.hand.firstCardValue.Value.getDisplayString() : null)}");
				Console.WriteLine($"Second highest value for this hand type: {(player.hand.secondCardValue != null ? player.hand.secondCardValue.Value.getDisplayString() : null)}\n" +
					$"Kickers: {player.hand.kickers.getDisplayString()}");
				Console.WriteLine("");
			}

			Console.WriteLine("Winning player(s):");
			foreach (var player in winningPlayers) {
				Console.WriteLine(player.name);
			}

			// ReadKey so the app doesn't close.
			Console.ReadKey();
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
		/// </summary>
		public PokerHand hand { get; set; }

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
		/// Set the pokerHand property to the best possible hand the player can make.
		/// </summary>
		/// <param name="tableCards"></param>
		public void setPokerHand(List<Card> tableCards) {
			this.hand = PokerHand.getBestHand(tableCards.Union(this.cards));
		}
	}
}
