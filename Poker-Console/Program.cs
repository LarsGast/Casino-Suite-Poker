using Casino_Suite_Poker;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Console {
	internal class Program {
		static void Main(string[] args) {
			
			// Get a deck, shuffle it, and display each card from top to bottom.
			var deck = Deck.get();
			deck.shuffle();

			var cardCount = 1;
			foreach(var card in deck.cards) {
				Console.WriteLine($"{cardCount}: {card.suit.getDisplayString()}{card.cardValue.getDisplayString()}");
				cardCount++;
			}

			Console.ReadKey();
		}
	}
}
