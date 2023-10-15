using DeckOfCardsLibrary;
using static DeckOfCardsLibrary.Card;

namespace PokerLibrary {

	/// <summary>
	/// Represents a poker hand, which is a combination of playing cards used in various poker games.
	/// This class provides methods to evaluate and compare poker hands based on standard hand ranks.
	/// </summary>
	public class PokerHand {

		#region Properties

		/// <summary>
		/// Represents the rank of poker hand, ranging from High Card to Straight Flush.
		/// Excludes the Royal Flush, which is the highest form of a Straight Flush.
		/// </summary>
		public enum HandRank {
			HighCard,
			Pair,
			TwoPair,
			ThreeOfAKind,
			Straight,
			Flush,
			FullHouse,
			FourOfAKind,
			StraightFlush
		}

		/// <summary>
		/// The specific rank of the hand, such as High Card or Straight Flush.
		/// </summary>
		public HandRank handRank { get; private set; }

		/// <summary>
		/// The primary card rank relevant to this hand.
		/// For Pair and TwoPair: the rank of the highest pair.
		/// For ThreeOfAKind and FullHouse: the rank of the three of a kind.
		/// For FourOfAKind: the rank of the four of a kind.
		/// Null for other hand ranks.
		/// </summary>
		public Rank? primaryCardRank { get; private set; }

		/// <summary>
		/// The secondary card rank relevant to this hand.
		/// For TwoPair: the rank of the second-highest pair.
		/// For FullHouse: the rank of the highest pair that is not a three of a kind.
		/// Null for other hand ranks.
		/// </summary>
		public Rank? secondaryCardRank { get; private set; }

		/// <summary>
		/// The suit of the hand, relevant for Flush and StraightFlush.
		/// Null for other hand ranks.
		/// </summary>
		public Suit? suit { get; private set; }

		/// <summary>
		/// A sorted list of kickers, the cards used to enhance the hand.
		/// These are all the cards not part of the primary hand but still contribute to it.
		/// </summary>
		public List<Card> kickers {
			get {
				// Sort kickers in descending order of rank.
				var orderedKickers = this._kickers.OrderByDescending(card => card.rank).ToList();

				// If the hand is a straight or straight flush from Ace to Five, move the Ace to the end.
				// In this case, the ace is the lowest rank of the hand, not the highest.
				if (this.handRank == HandRank.Straight || this.handRank == HandRank.StraightFlush) {
					if (orderedKickers.Last().rank == Rank.Two && orderedKickers.First().rank == Rank.Ace) {
						var aceCard = orderedKickers.First();
						orderedKickers.Remove(aceCard);
						orderedKickers.Add(aceCard);
					}
				}

				return orderedKickers;
			}
		}

		/// <summary>
		/// An unsorted list of kickers.
		/// </summary>
		private IEnumerable<Card> _kickers { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for initializing a PokerHand.
		/// </summary>
		/// <param name="handRank">The rank of the hand.</param>
		/// <param name="primaryCardRank">The primary card rank.</param>
		/// <param name="secondaryCardRank">The secondary card rank.</param>
		/// <param name="suit">The suit of the hand (if relevant).</param>
		/// <param name="kickers">(Unsorted) list of kickers.</param>
		private PokerHand(HandRank handRank, Rank? primaryCardRank, Rank? secondaryCardRank, Suit? suit, IEnumerable<Card> kickers) {
			this.handRank = handRank;
			this.primaryCardRank = primaryCardRank;
			this.secondaryCardRank = secondaryCardRank;
			this.suit = suit;
			this._kickers = kickers;
		}

		#endregion

		#region Methods

		#region Public Methods

		/// <summary>
		/// Gets the best hand possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>The best PokerHand that can be formed with the given cards.</returns>
		public static PokerHand getBestHand(IEnumerable<Card> cards) {

			// Check for the best possible hand in descending order of poker hand ranks.

			var bestStraightFlush = PokerHand.getBestStraightFlush(cards);
			if (bestStraightFlush != null) {
				return bestStraightFlush;
			}

			var bestFourOfAKind = PokerHand.getBestFourOfAKind(cards);
			if (bestFourOfAKind != null) {
				return bestFourOfAKind;
			}

			var bestFullHouse = PokerHand.getBestFullHouse(cards);
			if (bestFullHouse != null) {
				return bestFullHouse;
			}

			var bestFlush = PokerHand.getBestFlush(cards);
			if (bestFlush != null) {
				return bestFlush;
			}

			var bestStraight = PokerHand.getBestStraight(cards);
			if (bestStraight != null) {
				return bestStraight;
			}

			var bestThreeOfAKind = PokerHand.getBestThreeOfAKind(cards);
			if (bestThreeOfAKind != null) {
				return bestThreeOfAKind;
			}

			var bestTwoPair = PokerHand.getBestTwoPair(cards);
			if (bestTwoPair != null) {
				return bestTwoPair;
			}

			var bestPair = PokerHand.getBestPair(cards);
			if (bestPair != null) {
				return bestPair;
			}

			return PokerHand.getBestHighCard(cards);
		}

		/// <summary>
		/// Checks if this hand wins against the other hand based on various criteria.
		/// </summary>
		/// <param name="other">The other PokerHand to compare against.</param>
		/// <returns>
		///     True if this hand wins, 
		///     False if the other hand wins, 
		///     Null if it's a draw.
		/// </returns>
		public bool? winsAgainst(PokerHand other) {

			// Check if this hand has a better hand rank than the other.
			var hasBetterHandRank = this.hasBetterHandRank(other);
			if (hasBetterHandRank != null) {
				return hasBetterHandRank;
			}

			// If hand rank is the same, check by the primary card rank.
			var hasBetterFirstCard = this.hasBetterFirstCard(other);
			if (hasBetterFirstCard != null) {
				return hasBetterFirstCard;
			}

			// If the primary card rank is the same, check by the secondary card rank.
			var hasBetterSecondCard = this.hasBetterSecondCard(other);
			if (hasBetterSecondCard != null) {
				return hasBetterSecondCard;
			}

			// If all previous comparisons result in a draw, compare by kickers.
			return this.hasBetterKickers(other);
		}

		/// <summary>
		/// Determines the best hand from a list of hands based on various criteria.
		/// Only returns one hand in case of a draw.
		/// </summary>
		/// <param name="hands">A list of PokerHands to evaluate.</param>
		/// <returns>The best PokerHand among the provided list.</returns>
		public static PokerHand getWinningHand(IEnumerable<PokerHand> hands) {

			// Find the highest hand rank among the provided hands.
			var bestHandRank = hands
				.OrderByDescending(hand => hand.handRank)
				.First()
				.handRank;

			// Filter hands with the best hand rank.
			var handsBestHandRank = hands.Where(hand => hand.handRank == bestHandRank);

			// If only one hand has the best hand rank, return it.
			if (handsBestHandRank.Count() == 1) {
				return handsBestHandRank.First();
			}

			// Continue comparing by the primary card rank.
			var bestPrimaryCardRank = handsBestHandRank
				.OrderByDescending(hand => hand.primaryCardRank)
				.First()
				.primaryCardRank;

			// Filter hands with the best primary card rank.
			var handsBestPrimaryCardRank = handsBestHandRank.Where(hand => hand.primaryCardRank == bestPrimaryCardRank);

			// If only one hand has the best primary card rank, return it.
			if (handsBestPrimaryCardRank.Count() == 1) {
				return handsBestPrimaryCardRank.First();
			}

			// Continue comparing by the secondary card rank.
			var bestSecondaryCardRank = handsBestPrimaryCardRank
				.OrderByDescending(hand => hand.secondaryCardRank)
				.First()
				.secondaryCardRank;

			// Filter hands with the best secondary card rank.
			var handsBestSecondaryCardRank = handsBestPrimaryCardRank.Where(hand => hand.secondaryCardRank == bestSecondaryCardRank);

			// If only one hand has the best secondary card rank, return it.
			if (handsBestSecondaryCardRank.Count() == 1) {
				return handsBestSecondaryCardRank.First();
			}

			// If no clear winner, determine the best hand by kickers.
			var handsHighestKickers = PokerHand.getWinningHandByKickers(handsBestSecondaryCardRank.ToList());

			// If there are multiple same-quality hands (so a draw), return one of those hands.
			return handsHighestKickers.First();
		}

		#endregion

		#region Helper methods

		#region getBestHand

		/// <summary>
		/// Gets the best straight flush possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Straight Flush PokerHand if one is possible, 
		///     or null if there is no straight flush.
		/// </returns>
		private static PokerHand? getBestStraightFlush(IEnumerable<Card> cards) {

			// Find the highest straight flush cards and determine the suit.
			var highestStraightFlushCards = PokerHand.getHighestStraightCards(cards, mustBeFlush: true);

			if (highestStraightFlushCards == null) {
				return null;
			}

			var suit = highestStraightFlushCards.First().suit;
			var kickers = highestStraightFlushCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRank.StraightFlush, null, null, suit, kickers);
		}

		/// <summary>
		/// Gets the best four of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Four of a Kind PokerHand if one is possible, 
		///     or null if there is no four of a kind.
		/// </returns>
		private static PokerHand? getBestFourOfAKind(IEnumerable<Card> cards) {

			// Only 1 four of a kind is possible, so we do not have to check if the one we find is the highest one.
			var highestFourOrAKindValue = PokerHand.getHighestOfAKindValue(cards, 4);

			if (highestFourOrAKindValue == null) {
				return null;
			}

			var kickers = cards.OrderByDescending(card => card.rank).Where(card => card.rank != highestFourOrAKindValue).Take(1);
			return new PokerHand(HandRank.FourOfAKind, highestFourOrAKindValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best full house possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Full House PokerHand if one is possible, 
		///     or null if there is no full house.
		/// </returns>
		private static PokerHand? getBestFullHouse(IEnumerable<Card> cards) {
			var highestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var secondHighestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards.Where(card => card.rank != highestThreeOfAKindValue), 3);
			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (secondHighestThreeOfAKindValue == null && highestPairValue == null) {
				return null;
			}

			var kickers = new List<Card>();
			return new PokerHand(HandRank.FullHouse, highestThreeOfAKindValue, highestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best flush possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Flush PokerHand if one is possible, 
		///     or null if there is no flush.
		/// </returns>
		private static PokerHand? getBestFlush(IEnumerable<Card> cards) {
			var flushCards = cards
				.GroupBy(card => card.suit)
				.Where(group => group.Count() >= 5)
				.SelectMany(group => group);

			if (!flushCards.Any()) {
				return null;
			}

			var flushSuit = flushCards.First().suit;
			var kickers = flushCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRank.Flush, null, null, flushSuit, kickers);
		}

		/// <summary>
		/// Gets the best straight possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Straight PokerHand if one is possible, 
		///     or null if there is no straight.
		/// </returns>
		private static PokerHand? getBestStraight(IEnumerable<Card> cards) {

			// Find the highest straight cards and return them as a PokerHand.
			var highestStraightCards = PokerHand.getHighestStraightCards(cards, mustBeFlush: false);

			if (highestStraightCards == null) {
				return null;
			}

			var kickers = highestStraightCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRank.Straight, null, null, null, kickers);
		}

		/// <summary>
		/// Gets the best three of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Three of a Kind PokerHand if one is possible, 
		///     or null if there is no three of a kind.
		/// </returns>
		private static PokerHand? getBestThreeOfAKind(IEnumerable<Card> cards) {

			// Find the highest three of a kind and the two highest kickers, then return as a PokerHand.
			var highestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var kickers = cards.Where(card => card.rank != highestThreeOfAKindValue).OrderByDescending(card => card.rank).Take(2);
			return new PokerHand(HandRank.ThreeOfAKind, highestThreeOfAKindValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best two pair possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Two Pair PokerHand if one is possible, 
		///     or null if there is no two pair.
		/// </returns>
		private static PokerHand? getBestTwoPair(IEnumerable<Card> cards) {

			// Find the two highest pairs and the highest kicker, then return as a PokerHand.
			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var secondHighestPairValue = PokerHand.getHighestOfAKindValue(cards.Where(card => card.rank != highestPairValue), 2);

			if (secondHighestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.rank != highestPairValue && card.rank != secondHighestPairValue)
				.OrderByDescending(card => card.rank)
				.Take(1);

			return new PokerHand(HandRank.TwoPair, highestPairValue, secondHighestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best pair possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Pair PokerHand if one is possible, 
		///     or null if there is no pair.
		/// </returns>
		private static PokerHand? getBestPair(IEnumerable<Card> cards) {

			// Find the highest pair and the three highest kickers, then return as a PokerHand.
			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.rank != highestPairValue)
				.OrderByDescending(card => card.rank)
				.Take(3);

			return new PokerHand(HandRank.Pair, highestPairValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best high card possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>The best High Card PokerHand that can be formed with the given cards.</returns>
		private static PokerHand getBestHighCard(IEnumerable<Card> cards) {

			// Find the five highest cards as kickers and return as a PokerHand.
			var orderedCards = cards.OrderByDescending(card => card.rank);
			var kickers = orderedCards.Take(5);
			return new PokerHand(HandRank.HighCard, null, null, null, kickers);
		}

		/// <summary>
		/// Gets the highest value of an "X of a kind" from the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <param name="numberOfAKind">The number of cards that should have the same rank.</param>
		/// <returns>The highest rank that forms an "X of a kind," or null if none is found.</returns>
		private static Rank? getHighestOfAKindValue(IEnumerable<Card> cards, int numberOfAKind) {

			// Group the cards by rank and count those that have the specified number "of a kind".
			var highestOfAKindCards = cards
				.GroupBy(card => card.rank)
				.Where(group => group.Count() == numberOfAKind);

			if (!highestOfAKindCards.Any()) {
				return null;
			}

			// Find and return the highest rank among the groups.
			var highestOfAKindRank = highestOfAKindCards.OrderByDescending(group => group.Key).First().Key;
			return highestOfAKindRank;
		}

		/// <summary>
		/// Gets the cards that form the highest possible straight (flush) from the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <param name="mustBeFlush">Specifies whether the hand must be a straight flush.</param>
		/// <returns>A list of cards that form the highest straight (flush), or an null if none is found.</returns>
		private static List<Card>? getHighestStraightCards(IEnumerable<Card> cards, bool mustBeFlush = false) {

			// The ace can be used at both ends, as the card below a 2, and the card above a King.
			// Because of this, we will sort descending and add duplicate cards of each ace at the end of the list.
			var orderedCards = cards.OrderByDescending(card => card.rank).ToList();
			if (orderedCards.Select(card => card.rank).Contains(Rank.Ace)) {
				orderedCards.AddRange(orderedCards.Where(card => card.rank == Rank.Ace).ToList());
			}

			// Look for a straight (flush) for each card, starting with the highest.
			foreach (var currentCard in orderedCards) {

				// Four (and below) cannot be the highest card in a straight.
				if (currentCard.rank == Rank.Four) {
					break;
				}

				// Get all cards that would make up a straight with the currentCard as the highest card.
				// If the current card is a Five, then the Ace can also be part of the straight.
				var cardsForStraight = cards.Where(card => PokerHand.isStraightCard(currentCard, card, mustBeFlush)).DistinctBy(card => card.rank);

				// If there are exactly 5 cards in cardsForStraight, then we have a straight.
				if (cardsForStraight.Count() == 5) {
					return cardsForStraight.ToList();
				}
			}

			// No straight (flush) found.
			return null;
		}

		/// <summary>
		/// Checks if a card is part of a straight from the highest card.
		/// </summary>
		/// <param name="highestCard">The highest card to start the straight from.</param>
		/// <param name="card">The card to check.</param>
		/// <param name="mustBeFlush">Specifies whether the hand must be a straight flush.</param>
		/// <returns>True if the card is part of a straight, false otherwise.</returns>
		private static bool isStraightCard(Card highestCard, Card card, bool mustBeFlush = false) {
			return (card.Equals(highestCard) ||
				   card.rank == highestCard.rank - 1 ||
				   card.rank == highestCard.rank - 2 ||
				   card.rank == highestCard.rank - 3 ||
				   card.rank == highestCard.rank - 4 ||
				   (highestCard.rank == Rank.Five && card.rank == Rank.Ace)) &&
				   (!mustBeFlush || card.suit == highestCard.suit);
		}

		#endregion

		#region winsAgainst

		/// <summary>
		/// Checks if this hand has a better hand rank than the other hand.
		/// </summary>
		/// <param name="other">The other hand to compare against.</param>
		/// <returns>
		///     True if this hand has a better hand rank,
		///     False if the other hand has a better hand rank,
		///     Null if they have the same hand rank.
		/// </returns>
		private bool? hasBetterHandRank(PokerHand other) {
			if (this.handRank == other.handRank) {
				return null;
			}

			return this.handRank > other.handRank;
		}

		/// <summary>
		/// Checks if this hand has a better primary card rank than the other hand.
		/// </summary>
		/// <param name="other">The other hand to compare against.</param>
		/// <returns>
		///     True if this hand has a better primary card rank,
		///     False if the other hand has a better primary card rank,
		///     Null if they have the same primary card rank.
		/// </returns>
		private bool? hasBetterFirstCard(PokerHand other) {
			if (this.primaryCardRank == other.primaryCardRank) {
				return null;
			}

			return this.primaryCardRank > other.primaryCardRank;
		}

		/// <summary>
		/// Checks if this hand has a better secondary card rank than the other hand.
		/// </summary>
		/// <param name="other">The other hand to compare against.</param>
		/// <returns>
		///     True if this hand has a better secondary card rank,
		///     False if the other hand has a better secondary card rank,
		///     Null if they have the same secondary card rank.
		/// </returns>
		private bool? hasBetterSecondCard(PokerHand other) {
			if (this.secondaryCardRank == other.secondaryCardRank) {
				return null;
			}

			return this.secondaryCardRank > other.secondaryCardRank;
		}

		/// <summary>
		/// Checks if this hand has better kickers than the other hand.
		/// </summary>
		/// <param name="other">The other hand to compare against.</param>
		/// <returns>
		///     True if this hand has better kickers,
		///     False if the other hand has better kickers,
		///     Null if they have the same kickers.
		/// </returns>
		private bool? hasBetterKickers(PokerHand other) {
			for (int i = 0; i < this.kickers.Count; i++) {
				if (this.kickers[i].rank > other.kickers[i].rank) {
					return true;
				}

				if (this.kickers[i].rank < other.kickers[i].rank) {
					return false;
				}
			}

			return null;
		}

		#endregion

		#region getWinningHand

		/// <summary>
		/// Gets the best hand(s) among the given hands based solely on the kickers.
		/// </summary>
		/// <param name="hands">A list of PokerHands to evaluate.</param>
		/// <returns>A list of PokerHands with the best kickers.</returns>
		private static List<PokerHand> getWinningHandByKickers(IEnumerable<PokerHand> hands) {

			// Create a list of hands with the highest kickers.
			var handsWithHighestKickers = hands.ToList();

			// Iterate through each kicker position.
			for (int i = 0; i < handsWithHighestKickers.First().kickers.Count; i++) {
				Card? highestKicker = null;

				// Find the highest kicker at the current index among all hands.
				foreach (var hand in hands) {
					var handKicker = hand.kickers[i];
					if (highestKicker == null || handKicker.rank > highestKicker.rank) {
						highestKicker = handKicker;
					}
				}

				// Filter hands with the kicker of the current index having the same rank as the highest kicker.
				handsWithHighestKickers = hands.Where(hand => hand.kickers[i].rank == highestKicker!.rank).ToList();

				// If there's only one hand with this kicker, the winner is found.
				if (handsWithHighestKickers.Count == 1) {
					return handsWithHighestKickers.ToList();
				}
			}

			// In the case of multiple hands with the exact same kickers, return these hands.
			return handsWithHighestKickers.ToList();
		}

		#endregion

		#endregion

		#endregion
	}
}
