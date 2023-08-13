using Casino_Suite_Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using Accord.Statistics.Distributions.Univariate;
using System.Linq;

namespace Unit_Tests {
	[TestClass]
	public class UnitTestsDeck {

		private const int NumberOfShuffles = 1000; // Number of times to shuffle the deck

		/// <summary>
		/// Tests whether the deck shuffle is actually random.
		/// So: the distribution of cards is uniform.
		/// </summary>
		[TestMethod]
		public void shuffleIsRandom() {

			// Initiate some variables.
			var random = new Random();
			var deck = Deck.get();

			// Dictionary to store the frequency of each card after shuffling.
			var cardFrequency = new Dictionary<Card, int>();

			while (true) {
				var card = deck.draw();

				if (card == null) {
					break;
				}

				cardFrequency[card] = 0;
			}

			// Perform shuffling multiple times and count the frequency of each card.
			for (int i = 0; i < NumberOfShuffles; i++) {

				// Include the random variable so that the default doesn't take the same seed.
				deck.shuffle(random: random);

				// Store which index the each card is shuffled.
				int cardIndex = 0;

				while (true) {
					var card = deck.draw();

					if (card == null) {
						break;
					}

					cardFrequency[card] = cardFrequency[card] + cardIndex;
					cardIndex++;
				}
			}

			// Calculate the expected average card index after shuffling (assuming a uniform distribution).
			double averageIndex = (52 - 1) / 2.0;

			// Calculate the chi-square statistic
			double chiSquare = 0;

			while (true) {
				var card = deck.draw();

				if (card == null) {
					break;
				}

				// The observer average index is the observed indexes summed up divided by the number of shuffles.
				double observedAverageIndex = (double)cardFrequency[card] / NumberOfShuffles;

				// Calculate the chi-square contribution for this card.
				double chiSquareContribution = Math.Pow(observedAverageIndex - averageIndex, 2) / averageIndex;
				chiSquare += chiSquareContribution;
			}

			// Degrees of freedom for a deck of cards is (number of cards - 1).
			int degreesOfFreedom = 52 - 1;

			// Set the significance level (alpha) for the chi-square test.
			// 0.05 here means we want to be 95% certain the distibution is uniform.
			double alpha = 0.05;

			// Use chi-square distribution table to get the critical value for the given significance level and degrees of freedom.
			ChiSquared chiSquaredDist = new ChiSquared(degreesOfFreedom);
			double criticalValue = chiSquaredDist.InverseCumulativeDistribution(1 - alpha);

			// Perform the chi-square test.
			Assert.IsFalse(chiSquare >= criticalValue, "The deck shuffle is not random.");
		}

		[TestMethod]
		public void drawnCardsDoNotShuffle() {
			var deck = Deck.get();
			deck.shuffle();
			var numberOfCards = 10;

			var firstTenCards = new List<Card>();

			// Draw 10 cards.
			for (int i = 0; i < numberOfCards; i++) {
				firstTenCards.Add(deck.draw());
			}

			// Reset the deck so the already drawn cards go back onto the top.
			deck.reset();

			for (int i = 0; i < numberOfCards / 2; i++) {
				deck.draw();
			}

			// Shuffle the remaining cards in the deck.
			deck.shuffle(includeDrawnCards: false);

			// Reset the deck so the already drawn cards go back onto the top.
			deck.reset();

			// The first five cards should be the same.
			for (int i = 0; i < numberOfCards / 2; i++) {
				Assert.IsTrue(firstTenCards[i].equals(deck.draw()), $"One of the first {numberOfCards / 2} cards is not the same");
			}

			var numberOfCardsDifferent = 0;

			// The second five cards should not be the same.
			for (int i = numberOfCards / 2; i < numberOfCards; i++) {
				if (!firstTenCards[i].equals(deck.draw())){
					numberOfCardsDifferent++;
				}
			}

			var threshold = 4;

			Assert.IsTrue(numberOfCardsDifferent >= threshold, $"Less than {threshold} cards are different.");
		}
	}
}
