using DeckOfCardsLibrary;
using PokerLibrary;

namespace PokerDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get all the players.
            var players = Program.GetPlayers();

            // Get the deck and shuffle it.
            var deck = Deck.get();
            deck.shuffle();

            // Draw 2 cards for each player.
            Program.DrawCardsForPlayers(players, deck);

            // Draw 5 cards on the table.
            var tableCards = Program.DrawCardsForTable(deck);

            // Determine the winner(s).
            var winningPlayers = GetWinningPlayers(players, tableCards);

            // Display everything.
            Program.DisplayGame(players, tableCards, winningPlayers);

            // ReadKey so the window doesn't instantly close.
            _ = Console.ReadKey();
        }

        /// <summary>
        /// Define the possible players.
        /// </summary>
        /// <returns></returns>
        private static List<Player> GetPlayers()
        {
            var playerNames = new List<string>()
            {
                "Luz",
                "Eda",
                "King",
                "Amity",
                "Willow",
                "Gus",
                "Hunter",
                "Vee",
            };

            // Define how many players will participate in the game.
            var numberOfPlayers = 3;

            // No more players than the number of names.
            if (numberOfPlayers > playerNames.Count)
            {
                throw new Exception("Not enough player names");
            }

            if (numberOfPlayers < 2)
            {
                throw new Exception("At least 2 players should be participating.");
            }

            // Make a player for each player name.
            var players = new List<Player>();
            for (var i = 0; i < numberOfPlayers; i++)
            {
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
        private static void DrawCardsForPlayers(List<Player> players, Deck deck)
        {
            for (var i = 0; i < 2; i++)
            {
                foreach (var player in players)
                {
                    var card =
                        deck.draw() ?? throw new Exception("There are no more cards in the deck.");

                    player.AddCard(card);
                }
            }
        }

        /// <summary>
        /// Draw five cards for the table.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static List<Card> DrawCardsForTable(Deck deck)
        {
            var tableCards = new List<Card>();
            for (var i = 0; i < 5; i++)
            {
                var card =
                    deck.draw() ?? throw new Exception("There are no more cards in the deck.");

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
        private static List<Player> GetWinningPlayers(List<Player> players, List<Card> tableCards)
        {
            // Determine the best hand for each player.
            foreach (var player in players)
            {
                player.SetPokerHand(tableCards);
            }

            // Get the best hand amongst all the players.
            var bestHand = PokerHand.GetWinningHand(players.Select(player => player.Hand!));

            // Get all the players that draw with the best hand.
            // More than one player can have the best hand.
            var winningPlayers = players
                .Where(player => player.Hand!.WinsAgainst(bestHand) == null)
                .ToList();
            return winningPlayers;
        }

        /// <summary>
        /// Display everything about the game.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="tableCards"></param>
        /// <param name="winningPlayers"></param>
        private static void DisplayGame(
            List<Player> players,
            List<Card> tableCards,
            List<Player> winningPlayers
        )
        {
            Console.WriteLine("Table:");
            Console.WriteLine(tableCards.GetDisplayString(displayTenAsT: true));
            Console.WriteLine("");
            Console.WriteLine("Players:");

            foreach (var player in players)
            {
                Console.WriteLine($"Name: {player.Name}");
                Console.WriteLine($"Cards: {player.Cards.GetDisplayString(displayTenAsT: true)}");
                Console.WriteLine($"Hand Rank: {player.Hand!.HandRank}");
                Console.WriteLine(
                    $"Highest value for this hand rank: {(player.Hand.PrimaryCardRank?.getDisplayString(displayTenAsT: true))}"
                );
                Console.WriteLine(
                    $"Second highest value for this hand rank: {(player.Hand.SecondaryCardRank?.getDisplayString(displayTenAsT: true))}\n"
                        + $"Kickers: {player.Hand.Kickers.GetDisplayString(displayTenAsT: true)}"
                );
                Console.WriteLine("");
            }

            Console.WriteLine("Winning player(s):");
            foreach (var player in winningPlayers)
            {
                Console.WriteLine(player.Name);
            }

            Console.WriteLine("");
            Console.WriteLine("(Press any key to close this window)");
        }
    }

    /// <summary>
    /// Possible Player class.
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The cards that the player gets dealt.
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// The best possible hand considering the cards the player received and the cards on the table.
        /// Null if not yet assigned.
        /// </summary>
        public PokerHand? Hand { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            this.Name = name;
            this.Cards = new List<Card>();
        }

        /// <summary>
        /// Add a card to the players hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            this.Cards.Add(card);
        }

        /// <summary>
        /// Set the _pokerHand property to the best possible hand the player can make with the given cards.
        /// </summary>
        /// <param name="tableCards"></param>
        public void SetPokerHand(List<Card> tableCards)
        {
            this.Hand = PokerHand.GetBestHand(tableCards.Union(this.Cards));
        }
    }
}
