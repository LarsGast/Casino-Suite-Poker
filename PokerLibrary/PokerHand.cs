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
		public enum HandRankEnum {
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
		public HandRankEnum HandRank { get; private set; }

		/// <summary>
		/// The primary card rank relevant to this hand.
		/// For Pair and TwoPair: the rank of the highest pair.
		/// For ThreeOfAKind and FullHouse: the rank of the three of a kind.
		/// For FourOfAKind: the rank of the four of a kind.
		/// Null for other hand ranks.
		/// </summary>
		public Rank? PrimaryCardRank { get; private set; }

		/// <summary>
		/// The secondary card rank relevant to this hand.
		/// For TwoPair: the rank of the second-highest pair.
		/// For FullHouse: the rank of the highest pair that is not a three of a kind.
		/// Null for other hand ranks.
		/// </summary>
		public Rank? SecondaryCardRank { get; private set; }

		/// <summary>
		/// The suit of the hand, relevant for Flush and StraightFlush.
		/// Null for other hand ranks.
		/// </summary>
		public Suit? Suit { get; private set; }

		/// <summary>
		/// A sorted list of kickers, the cards used to enhance the hand.
		/// These are all the cards not part of the primary hand but still contribute to it.
		/// </summary>
		public List<Card> Kickers {
			get {
				// Sort kickers in descending order of rank.
				var orderedKickers = this._kickers.OrderByDescending(card => card.rank).ToList();

				// If the hand is a straight or straight flush from Ace to Five, move the Ace to the end.
				// In this case, the ace is the lowest rank of the hand, not the highest.
				if (this.HandRank == HandRankEnum.Straight || this.HandRank == HandRankEnum.StraightFlush) {
					if (orderedKickers.Last().rank == Rank.Two && orderedKickers.First().rank == Rank.Ace) {
						var aceCard = orderedKickers.First();
                        _ = orderedKickers.Remove(aceCard);
						orderedKickers.Add(aceCard);
					}
				}

				return orderedKickers;
			}
		}

        /// <summary>
        /// An unsorted list of kickers.
        /// </summary>
        private readonly IEnumerable<Card> _kickers;

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
        private PokerHand(HandRankEnum handRank, Rank? primaryCardRank, Rank? secondaryCardRank, Suit? suit, IEnumerable<Card> kickers) {
			this.HandRank = handRank;
			this.PrimaryCardRank = primaryCardRank;
			this.SecondaryCardRank = secondaryCardRank;
			this.Suit = suit;
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
		public static PokerHand GetBestHand(IEnumerable<Card> cards) {

			// Check for the best possible hand in descending order of poker hand ranks.

			var bestStraightFlush = PokerHand.GetBestStraightFlush(cards);
			if (bestStraightFlush != null) {
				return bestStraightFlush;
			}

			var bestFourOfAKind = PokerHand.GetBestFourOfAKind(cards);
			if (bestFourOfAKind != null) {
				return bestFourOfAKind;
			}

			var bestFullHouse = PokerHand.GetBestFullHouse(cards);
			if (bestFullHouse != null) {
				return bestFullHouse;
			}

			var bestFlush = PokerHand.GetBestFlush(cards);
			if (bestFlush != null) {
				return bestFlush;
			}

			var bestStraight = PokerHand.GetBestStraight(cards);
			if (bestStraight != null) {
				return bestStraight;
			}

			var bestThreeOfAKind = PokerHand.GetBestThreeOfAKind(cards);
			if (bestThreeOfAKind != null) {
				return bestThreeOfAKind;
			}

			var bestTwoPair = PokerHand.GetBestTwoPair(cards);
			if (bestTwoPair != null) {
				return bestTwoPair;
			}

			var bestPair = PokerHand.GetBestPair(cards);
			if (bestPair != null) {
				return bestPair;
			}

			return PokerHand.GetBestHighCard(cards);
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
		public bool? WinsAgainst(PokerHand other) {

			// Check if this hand has a better hand rank than the other.
			var hasBetterHandRank = this.HasBetterHandRank(other);
			if (hasBetterHandRank != null) {
				return hasBetterHandRank;
			}

			// If hand rank is the same, check by the primary card rank.
			var hasBetterFirstCard = this.HasBetterFirstCard(other);
			if (hasBetterFirstCard != null) {
				return hasBetterFirstCard;
			}

			// If the primary card rank is the same, check by the secondary card rank.
			var hasBetterSecondCard = this.HasBetterSecondCard(other);
			if (hasBetterSecondCard != null) {
				return hasBetterSecondCard;
			}

			// If all previous comparisons result in a draw, compare by kickers.
			return this.HasBetterKickers(other);
		}

		/// <summary>
		/// Determines the best hand from a list of hands based on various criteria.
		/// Only returns one hand in case of a draw.
		/// </summary>
		/// <param name="hands">A list of PokerHands to evaluate.</param>
		/// <returns>The best PokerHand among the provided list.</returns>
		public static PokerHand GetWinningHand(IEnumerable<PokerHand> hands) {

			// Find the highest hand rank among the provided hands.
			var bestHandRank = hands
				.OrderByDescending(hand => hand.HandRank)
				.First()
				.HandRank;

			// Filter hands with the best hand rank.
			var handsBestHandRank = hands.Where(hand => hand.HandRank == bestHandRank);

			// If only one hand has the best hand rank, return it.
			if (handsBestHandRank.Count() == 1) {
				return handsBestHandRank.First();
			}

			// Continue comparing by the primary card rank.
			var bestPrimaryCardRank = handsBestHandRank
				.OrderByDescending(hand => hand.PrimaryCardRank)
				.First()
				.PrimaryCardRank;

			// Filter hands with the best primary card rank.
			var handsBestPrimaryCardRank = handsBestHandRank.Where(hand => hand.PrimaryCardRank == bestPrimaryCardRank);

			// If only one hand has the best primary card rank, return it.
			if (handsBestPrimaryCardRank.Count() == 1) {
				return handsBestPrimaryCardRank.First();
			}

			// Continue comparing by the secondary card rank.
			var bestSecondaryCardRank = handsBestPrimaryCardRank
				.OrderByDescending(hand => hand.SecondaryCardRank)
				.First()
				.SecondaryCardRank;

			// Filter hands with the best secondary card rank.
			var handsBestSecondaryCardRank = handsBestPrimaryCardRank.Where(hand => hand.SecondaryCardRank == bestSecondaryCardRank);

			// If only one hand has the best secondary card rank, return it.
			if (handsBestSecondaryCardRank.Count() == 1) {
				return handsBestSecondaryCardRank.First();
			}

			// If no clear winner, determine the best hand by kickers.
			var handsHighestKickers = PokerHand.GetWinningHandByKickers(handsBestSecondaryCardRank.ToList());

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
        private static PokerHand? GetBestStraightFlush(IEnumerable<Card> cards) {

			// Find the highest straight flush cards and determine the suit.
			var highestStraightFlushCards = PokerHand.GetHighestStraightCards(cards, mustBeFlush: true);

			if (highestStraightFlushCards == null) {
				return null;
			}

			var suit = highestStraightFlushCards.First().suit;
			var kickers = highestStraightFlushCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRankEnum.StraightFlush, null, null, suit, kickers);
		}

		/// <summary>
		/// Gets the best four of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Four of a Kind PokerHand if one is possible, 
		///     or null if there is no four of a kind.
		/// </returns>
		private static PokerHand? GetBestFourOfAKind(IEnumerable<Card> cards) {

			// Only 1 four of a kind is possible, so we do not have to check if the one we find is the highest one.
			var highestFourOrAKindValue = PokerHand.GetHighestOfAKindValue(cards, 4);

			if (highestFourOrAKindValue == null) {
				return null;
			}

			var kickers = cards.OrderByDescending(card => card.rank).Where(card => card.rank != highestFourOrAKindValue).Take(1);
			return new PokerHand(HandRankEnum.FourOfAKind, highestFourOrAKindValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best full house possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Full House PokerHand if one is possible, 
		///     or null if there is no full house.
		/// </returns>
		private static PokerHand? GetBestFullHouse(IEnumerable<Card> cards) {
			var highestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var secondHighestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(cards.Where(card => card.rank != highestThreeOfAKindValue), 3);
			var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

			if (secondHighestThreeOfAKindValue == null && highestPairValue == null) {
				return null;
			}

			var kickers = new List<Card>();
			return new PokerHand(HandRankEnum.FullHouse, highestThreeOfAKindValue, highestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best flush possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Flush PokerHand if one is possible, 
		///     or null if there is no flush.
		/// </returns>
		private static PokerHand? GetBestFlush(IEnumerable<Card> cards) {
			var flushCards = cards
				.GroupBy(card => card.suit)
				.Where(group => group.Count() >= 5)
				.SelectMany(group => group);

			if (!flushCards.Any()) {
				return null;
			}

			var flushSuit = flushCards.First().suit;
			var kickers = flushCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRankEnum.Flush, null, null, flushSuit, kickers);
		}

		/// <summary>
		/// Gets the best straight possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Straight PokerHand if one is possible, 
		///     or null if there is no straight.
		/// </returns>
		private static PokerHand? GetBestStraight(IEnumerable<Card> cards) {

			// Find the highest straight cards and return them as a PokerHand.
			var highestStraightCards = PokerHand.GetHighestStraightCards(cards, mustBeFlush: false);

			if (highestStraightCards == null) {
				return null;
			}

			var kickers = highestStraightCards.OrderByDescending(card => card.rank);
			return new PokerHand(HandRankEnum.Straight, null, null, null, kickers);
		}

		/// <summary>
		/// Gets the best three of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Three of a Kind PokerHand if one is possible, 
		///     or null if there is no three of a kind.
		/// </returns>
		private static PokerHand? GetBestThreeOfAKind(IEnumerable<Card> cards) {

			// Find the highest three of a kind and the two highest kickers, then return as a PokerHand.
			var highestThreeOfAKindValue = PokerHand.GetHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var kickers = cards.Where(card => card.rank != highestThreeOfAKindValue).OrderByDescending(card => card.rank).Take(2);
			return new PokerHand(HandRankEnum.ThreeOfAKind, highestThreeOfAKindValue, null, null, kickers);
		}

        /// <summary>
        /// Gets the best two pair possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        ///     The best Two Pair PokerHand if one is possible, 
        ///     or null if there is no two pair.
        /// </returns>
        private static PokerHand? GetBestTwoPair(IEnumerable<Card> cards) {

			// Find the two highest pairs and the highest kicker, then return as a PokerHand.
			var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var secondHighestPairValue = PokerHand.GetHighestOfAKindValue(cards.Where(card => card.rank != highestPairValue), 2);

			if (secondHighestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.rank != highestPairValue && card.rank != secondHighestPairValue)
				.OrderByDescending(card => card.rank)
				.Take(1);

			return new PokerHand(HandRankEnum.TwoPair, highestPairValue, secondHighestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best pair possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>
		///     The best Pair PokerHand if one is possible, 
		///     or null if there is no pair.
		/// </returns>
		private static PokerHand? GetBestPair(IEnumerable<Card> cards) {

			// Find the highest pair and the three highest kickers, then return as a PokerHand.
			var highestPairValue = PokerHand.GetHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.rank != highestPairValue)
				.OrderByDescending(card => card.rank)
				.Take(3);

			return new PokerHand(HandRankEnum.Pair, highestPairValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best high card possible with the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <returns>The best High Card PokerHand that can be formed with the given cards.</returns>
		private static PokerHand GetBestHighCard(IEnumerable<Card> cards) {

			// Find the five highest cards as kickers and return as a PokerHand.
			var orderedCards = cards.OrderByDescending(card => card.rank);
			var kickers = orderedCards.Take(5);
			return new PokerHand(HandRankEnum.HighCard, null, null, null, kickers);
		}

		/// <summary>
		/// Gets the highest value of an "X of a kind" from the given cards.
		/// </summary>
		/// <param name="cards">A collection of cards to evaluate.</param>
		/// <param name="numberOfAKind">The number of cards that should have the same rank.</param>
		/// <returns>The highest rank that forms an "X of a kind," or null if none is found.</returns>
		private static Rank? GetHighestOfAKindValue(IEnumerable<Card> cards, int numberOfAKind) {

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
		private static List<Card>? GetHighestStraightCards(IEnumerable<Card> cards, bool mustBeFlush = false) {

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
				var cardsForStraight = cards.Where(card => PokerHand.IsStraightCard(currentCard, card, mustBeFlush)).DistinctBy(card => card.rank);

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
		private static bool IsStraightCard(Card highestCard, Card card, bool mustBeFlush = false) {
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
        private bool? HasBetterHandRank(PokerHand other) {
            return this.HandRank == other.HandRank ? null : this.HandRank > other.HandRank;
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
        private bool? HasBetterFirstCard(PokerHand other) {
            return this.PrimaryCardRank == other.PrimaryCardRank ? null : this.PrimaryCardRank > other.PrimaryCardRank;
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
        private bool? HasBetterSecondCard(PokerHand other) {
            return this.SecondaryCardRank == other.SecondaryCardRank ? null : this.SecondaryCardRank > other.SecondaryCardRank;
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
        private bool? HasBetterKickers(PokerHand other) {
			for (var i = 0; i < this.Kickers.Count; i++) {
				if (this.Kickers[i].rank > other.Kickers[i].rank) {
					return true;
				}

				if (this.Kickers[i].rank < other.Kickers[i].rank) {
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
		private static List<PokerHand> GetWinningHandByKickers(IEnumerable<PokerHand> hands) {

			// Create a list of hands with the highest kickers.
			var handsWithHighestKickers = hands.ToList();

			// Iterate through each kicker position.
			for (var i = 0; i < handsWithHighestKickers.First().Kickers.Count; i++) {
				Card? highestKicker = null;

				// Find the highest kicker at the current index among all hands.
				foreach (var hand in hands) {
					var handKicker = hand.Kickers[i];
					if (highestKicker == null || handKicker.rank > highestKicker.rank) {
						highestKicker = handKicker;
					}
				}

				// Filter hands with the kicker of the current index having the same rank as the highest kicker.
				handsWithHighestKickers = hands.Where(hand => hand.Kickers[i].rank == highestKicker!.rank).ToList();

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
