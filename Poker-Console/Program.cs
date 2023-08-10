using Casino_Suite_Poker;
using Poker;
using Poker.ExtentionClasses;
using Poker.WinningHands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Console {
	internal class Program {
		static void Main(string[] args) {
			
			// Get a deck and shuffle it.
			var deck = Deck.get();
			deck.shuffle();

			// Initiate some variables.
			var pocketPlayerOne = new List<Card>();
			var pocketPlayerTwo = new List<Card>();
			var pocketPlayerThree = new List<Card>();
			var table = new List<Card>();

			// Deal cards to the three players.
			for (int i = 0; i < 2; i++) {
				pocketPlayerOne.Add(deck.draw());
				pocketPlayerTwo.Add(deck.draw());
				pocketPlayerThree.Add(deck.draw());
			}

			// Burn a card.
			deck.draw();

			// Deal the flop.
			table.Add(deck.draw());
			table.Add(deck.draw());
			table.Add(deck.draw());

			// Burn a card.
			deck.draw();

			// Deal the turn.
			table.Add(deck.draw());

			// Burn a card.
			deck.draw();

			// Deal the river.
			table.Add(deck.draw());

			// Display what each player has and what is on the table.
			Console.WriteLine($"Player 1: {pocketPlayerOne.getDisplayString()}");
			Console.WriteLine($"Player 2: {pocketPlayerTwo.getDisplayString()}");
			Console.WriteLine($"Player 3: {pocketPlayerThree.getDisplayString()}");
			Console.WriteLine($"Table: {table.getDisplayString()}");

			// Determine the best hand each player can make.
			var handPlayerOne = PokerHand.getBestHand(table.Union(pocketPlayerOne));
			var handPlayerTwo = PokerHand.getBestHand(table.Union(pocketPlayerTwo));
			var handPlayerThree = PokerHand.getBestHand(table.Union(pocketPlayerThree));

			Console.WriteLine("");

			// Display the best hand each player can make.
            Console.WriteLine($"Hand player 1:\n" +
				$"\tHand Type: {handPlayerOne.handType}\n" +
				$"\tHighest value for this hand type: {(handPlayerOne.firstCardValue != null ? handPlayerOne.firstCardValue.Value.getDisplayString() : null)}\n" +
				$"\tSecond highest value for this hand type: {(handPlayerOne.secondCardValue != null ? handPlayerOne.secondCardValue.Value.getDisplayString(): null)}\n" +
				$"\tKickers: {handPlayerOne.kickers.getDisplayString()}"
			);

			Console.WriteLine("");

			Console.WriteLine($"Hand player 2:\n" +
				$"\tHand Type: {handPlayerTwo.handType}\n" +
				$"\tHighest value for this hand type: {(handPlayerTwo.firstCardValue != null ? handPlayerTwo.firstCardValue.Value.getDisplayString() : null)}\n" +
				$"\tSecond highest value for this hand type: {(handPlayerTwo.secondCardValue != null ? handPlayerTwo.secondCardValue.Value.getDisplayString() : null)}\n" +
				$"\tKickers: {handPlayerTwo.kickers.getDisplayString()}"
			);

			Console.WriteLine("");

			Console.WriteLine($"Hand player 3:\n" +
				$"\tHand Type: {handPlayerThree.handType}\n" +
				$"\tHighest value for this hand type: {(handPlayerThree.firstCardValue != null ? handPlayerThree.firstCardValue.Value.getDisplayString() : null)}\n" +
				$"\tSecond highest value for this hand type: {(handPlayerThree.secondCardValue != null ? handPlayerThree.secondCardValue.Value.getDisplayString() : null)}\n" +
				$"\tKickers: {handPlayerThree.kickers.getDisplayString()}"
			);

			Console.WriteLine("");

			// Determine who wins.
			// ToDo: Refactor this in a static method in PokerHand.cs
			var playerOneWinsAgainstPlayerTwo = handPlayerOne.winsAgainst(handPlayerTwo);
			if (playerOneWinsAgainstPlayerTwo == true) {
				var playerOneWinsAgainstPlayerThree = handPlayerOne.winsAgainst(handPlayerThree);

				if (playerOneWinsAgainstPlayerThree == true) {
					Console.WriteLine("Player 1 wins!");
				}
				else if (playerOneWinsAgainstPlayerThree == false) {
					Console.WriteLine("Player 3 wins!");
				}
				else if (playerOneWinsAgainstPlayerThree == null) {
					Console.WriteLine("It's a draw between players 1 en 3!");
				}
			}
			else if (playerOneWinsAgainstPlayerTwo == false) {
				var playerTwoWinsAgainstPlayerThree = handPlayerTwo.winsAgainst(handPlayerThree);

				if (playerTwoWinsAgainstPlayerThree == true) {
					Console.WriteLine("Player 2 wins!");
				}
				else if (playerTwoWinsAgainstPlayerThree == false) {
					Console.WriteLine("Player 3 wins!");
				}
				else if (playerTwoWinsAgainstPlayerThree == null) {
					Console.WriteLine("It's a draw between players 2 en 3!");
				}
			}
			else if (playerOneWinsAgainstPlayerTwo == null) {
				var playerThreeWinsAgainstPlayerOne = handPlayerThree.winsAgainst(handPlayerOne);

				if (playerThreeWinsAgainstPlayerOne == true) {
					Console.WriteLine("Player 3 wins!");
				}
				else if (playerThreeWinsAgainstPlayerOne == false) {
					Console.WriteLine("It's a draw between players 1 en 2!");
				}
				else if (playerThreeWinsAgainstPlayerOne == null) {
					Console.WriteLine("It's a draw between players 1, 2, en 3!");
				}
			}

			Console.ReadKey();
		}
	}
}
