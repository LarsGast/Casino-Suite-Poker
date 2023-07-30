using Casino_Suite_Poker;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using static Casino_Suite_Poker.Card;

namespace Poker.WinningHands {
	internal class PokerHand {

		#region Properties

		/// <summary>
		/// Type of hand.
		/// Ranging from high card to straight flush.
		/// A royal flush is the highest straight flush, so it is not included here.
		/// </summary>
		public enum HandType {
			HighCard,
			Pair,
			TwoPair,
			ThreeOfAKind,
			Straight,
			Flush,
			FullHouse,
			FourOrAKind,
			StraightFlush
		}

		/// <summary>
		/// Type of the hand.
		/// Ranging from high card to straight flush.
		/// </summary>
		public HandType handType { get; private set; }

		/// <summary>
		/// The card that matters first for each hand.
		/// Card with value of the highest participating card for Straight and StraightFlush.
		/// Card with value of the highest pair for Pair and TwoPair.
		/// Card with value of the three of a kind for ThreeOfAKind and FullHouse.
		/// Card with value of the four of a kind for FourOfAKind.
		/// Null for HighCard.
		/// </summary>
		public CardValue? firstCardValue { get; private set; }

		/// <summary>
		/// The card that matters after the first card for each hand.
		/// Card with value of the second highest pair for TwoPair.
		/// Card with value of the highest pair for FullHouse.
		/// Otherwise null.
		/// </summary>
		public CardValue? secondCardValue { get; private set; }

		/// <summary>
		/// The suit of the hand, if it matters.
		/// It matters for Flush and StraightFlush.
		/// Otherwise null.
		/// </summary>
		public Suit? suit { get; private set; }

		/// <summary>
		/// The (sorted, from high to low values) kickers of the hand.
		/// These are all the cards/ values not present in the first and second value, but still used to make the hand.
		/// </summary>
		public List<Card> kickers {
			get {
				return _kickers.OrderByDescending(card => card.cardValue).ToList();
			}
		}

		/// <summary>
		/// Unsorted list of kickers.
		/// </summary>
		private IEnumerable<Card> _kickers { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="handType"></param>
		/// <param name="firstCard"></param>
		/// <param name="secondCard"></param>
		/// <param name="suit"></param>
		/// <param name="kickers"></param>
		private PokerHand(HandType handType, CardValue? firstCard, CardValue? secondCard, Suit? suit, IEnumerable<Card> kickers) {
			this.handType = handType;
			this.firstCardValue = firstCard;
			this.secondCardValue = secondCard;
			this.suit = suit;
			this._kickers = kickers;
		}

		#endregion

		#region Functions

		#region Winning hand

		/// <summary>
		/// Checks if this hand wins against the other hand.
		/// True if this wins.
		/// False if other wins.
		/// Null if it is a draw.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool? winsAgainst(PokerHand other) {

			var hasBetterHandType = this.hasBetterHandType(other);
			if (hasBetterHandType != null) {
				return hasBetterHandType;
			}

			var hasBetterFirstCard = this.hasBetterFirstCard(other);
			if (hasBetterFirstCard != null) {
				return hasBetterFirstCard;
			}

			var hasBetterSecondCard = this.hasBetterSecondCard(other);
			if (hasBetterSecondCard != null) {
				return hasBetterSecondCard;
			}

			return this.hasBetterKickers(other);
		}

		/// <summary>
		/// Checks if this hand has a better handType than the other hand.
		/// True if this wins.
		/// False if other wins.
		/// Null if it is a draw.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		private bool? hasBetterHandType (PokerHand other) {
			if (this.handType > other.handType) {
				return true;
			}

			if (this.handType < other.handType) {
				return false;
			}

			return null;
		}

		/// <summary>
		/// Checks if this hand has a better firstCard than the other hand.
		/// True if this wins.
		/// False if other wins.
		/// Null if it is a draw.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		private bool? hasBetterFirstCard(PokerHand other) {
			if (this.firstCardValue > other.firstCardValue) {
				return true;
			}

			if (this.firstCardValue < other.firstCardValue) {
				return false;
			}

			return null;
		}

		/// <summary>
		/// Checks if this hand has a better secondCard than the other hand.
		/// True if this wins.
		/// False if other wins.
		/// Null if it is a draw.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		private bool? hasBetterSecondCard(PokerHand other) {
			if (this.secondCardValue > other.secondCardValue) {
				return true;
			}

			if (this.secondCardValue < other.secondCardValue) {
				return false;
			}

			return null;
		}

		/// <summary>
		/// Checks if this hand wins has better kickers than the other hand.
		/// True if this wins.
		/// False if other wins.
		/// Null if it is a draw.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		private bool? hasBetterKickers(PokerHand other) {
			for (int i = 0; i < this.kickers.Count; i++) { 
				if (this.kickers[i].cardValue > other.kickers[i].cardValue) {
					return true;
				}

				if (this.kickers[i].cardValue < other.kickers[i].cardValue) {
					return false;
				}
			}

			return null;
		}

		#endregion

		#region Get best hand

		/// <summary>
		/// Gets the best hand possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		public static PokerHand getBestHand(IEnumerable<Card> cards) {

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
		/// Gets the best straight flush possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no straight flush.</returns>
		private static PokerHand getBestStraightFlush(IEnumerable<Card> cards) {

			var highestStraightFlushCards = PokerHand.getHighestStraightCards(cards, mustBeFlush: true).OrderByDescending(card => card.cardValue);

			if (highestStraightFlushCards.Count() == 0) {
				return null;
			}

			var suit = highestStraightFlushCards.First().suit;

			return new PokerHand(HandType.StraightFlush, null, null, suit, highestStraightFlushCards);
		}

		/// <summary>
		/// Gets the best four of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no four of a kind.</returns>
		private static PokerHand getBestFourOfAKind(IEnumerable<Card> cards) {

			// Only 1 four of a kind is possible, so we do not have to check if the one we find is the highest one.
			var highestFourOrAKindValue = PokerHand.getHighestOfAKindValue(cards, 4);

			if (highestFourOrAKindValue == null) {
				return null;
			}

			var kickers = cards.OrderByDescending(card => card.cardValue).Where(card => card.cardValue != highestFourOrAKindValue).Take(1);
			return new PokerHand(HandType.FourOrAKind, highestFourOrAKindValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best full house possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no full house.</returns>
		private static PokerHand getBestFullHouse(IEnumerable<Card> cards) {

			var highestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var secondHighestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards.Where(card => card.cardValue != highestThreeOfAKindValue), 3);
			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (secondHighestThreeOfAKindValue == null && highestPairValue == null) {
				return null;
			}

			var kickers = new List<Card>();
			return new PokerHand(HandType.FullHouse, highestThreeOfAKindValue, highestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best flush possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no flush.</returns>
		private static PokerHand getBestFlush(IEnumerable<Card> cards) {
			var flushCards = cards
				.GroupBy(card => card.suit)
				.Where(group => group.Count() >= 5)
				.SelectMany(group => group);

			if (flushCards.Count() == 0) {
				return null;
			}

			var flushSuit = flushCards.First().suit;
			var kickers = flushCards.OrderByDescending(card => card.cardValue);
			return new PokerHand(HandType.Flush, null, null, flushSuit, kickers);
		}

		/// <summary>
		/// Gets the best straight possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no straight.</returns>
		private static PokerHand getBestStraight(IEnumerable<Card> cards) {

			var highestStraightCards = PokerHand.getHighestStraightCards(cards, mustBeFlush: false).OrderByDescending(card => card.cardValue);

			if (highestStraightCards.Count() == 0) {
				return null;
			}

			return new PokerHand(HandType.Straight, null, null, null, highestStraightCards);
		}

		/// <summary>
		/// Gets the best three of a kind possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no three of a kind.</returns>
		private static PokerHand getBestThreeOfAKind(IEnumerable<Card> cards) {

			var highestThreeOfAKindValue = PokerHand.getHighestOfAKindValue(cards, 3);

			if (highestThreeOfAKindValue == null) {
				return null;
			}

			var kickers = cards.Where(card => card.cardValue != highestThreeOfAKindValue).OrderByDescending(card => card.cardValue).Take(2);
			return new PokerHand(HandType.ThreeOfAKind, highestThreeOfAKindValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best two pair possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no two pair.</returns>
		private static PokerHand getBestTwoPair(IEnumerable<Card> cards) {

			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var secondHighestPairValue = PokerHand.getHighestOfAKindValue(cards.Where(card => card.cardValue != highestPairValue), 2);

			if (secondHighestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.cardValue != highestPairValue && card.cardValue != secondHighestPairValue)
				.OrderByDescending(card => card.cardValue)
				.Take(1);

			return new PokerHand(HandType.TwoPair, highestPairValue, secondHighestPairValue, null, kickers);
		}

		/// <summary>
		/// Gets the best pair possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns>null if there is no pair.</returns>
		private static PokerHand getBestPair(IEnumerable<Card> cards) {

			var highestPairValue = PokerHand.getHighestOfAKindValue(cards, 2);

			if (highestPairValue == null) {
				return null;
			}

			var kickers = cards
				.Where(card => card.cardValue != highestPairValue)
				.OrderByDescending(card => card.cardValue)
				.Take(3);

			return new PokerHand(HandType.Pair, highestPairValue, null, null, kickers);
		}

		/// <summary>
		/// Gets the best hight card possible with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		private static PokerHand getBestHighCard(IEnumerable<Card> cards) {
			var orderedCards = cards.OrderByDescending(card => card.cardValue);
			var kickers = orderedCards.Take(5);
			return new PokerHand(HandType.HighCard, null, null, null, kickers);
		}

		#region Help Functions

		/// <summary>
		/// Gets the highest "X of a kind" value of the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <param name="numberOfAKind"></param>
		/// <returns>null if there is no "X of a kind" value.</returns>
		private static CardValue? getHighestOfAKindValue(IEnumerable<Card> cards, int numberOfAKind) {

			var highestOfAKindCards = cards
				.GroupBy(card => card.cardValue)
				.Where(group => group.Count() == numberOfAKind);

			if (highestOfAKindCards.Count() == 0) {
				return null;
			}

			var highestOfAKindValue = highestOfAKindCards.OrderByDescending(group => group.Key).First().Key;

			return highestOfAKindValue;
		}

		/// <summary>
		/// Gets the cards that make up the highset possible straight (flush) with the given cards.
		/// </summary>
		/// <param name="cards"></param>
		/// <param name="mustBeFlush">If the hand has to be a straight flush or just a straight</param>
		/// <returns></returns>
		private static List<Card> getHighestStraightCards(IEnumerable<Card> cards, bool mustBeFlush = false) {

			// The ace can be used at both ends, as the card below a 2, and the card above a King.
			// Because of this, we will sort descending and add duplicate cards of each ace at the end of the list.
			var orderedCards = cards.OrderByDescending(card => card.cardValue).ToList();
			if (orderedCards.Select(card => card.cardValue).Contains(CardValue.Ace)) {
				orderedCards.AddRange(orderedCards.Where(card => card.cardValue == CardValue.Ace).ToList());
			}

			// Look for a straight (flush) for each card, starting with the highest.
			var highestStraightCards = new List<Card>();
			foreach (var currentCard in orderedCards) {

				// Four (and below) cannot be the highest card in a straight.
				if (currentCard.cardValue == CardValue.Four) {
					break;
				}

				// Get all cards that would make up a straight with currentCard as the highest card.
				// If the current card is a Five, then the Ace can also be part of the straight.
				var cardsForStraight = cards.Where(card => 
					(card == currentCard ||
					card.cardValue == currentCard.cardValue - 1 ||
					card.cardValue == currentCard.cardValue - 2 ||					
					card.cardValue == currentCard.cardValue - 3 ||					
					card.cardValue == currentCard.cardValue - 4 ||
					(currentCard.cardValue == CardValue.Five && card.cardValue == CardValue.Ace)) &&
					(!mustBeFlush || card.suit == currentCard.suit)
				);

				// There may be multiple cards with the same value.
				// Remove duplicates in that case.
				if (cardsForStraight.Count() > 5) {
					if (mustBeFlush) {
						cardsForStraight = cardsForStraight.Where(card => card.suit != currentCard.suit);
					}
					else {
						cardsForStraight = cardsForStraight.DistinctBy(card => card.cardValue);
					}
				}

				// Is there are exactly 5 cards in cardsForStraight, then we have a straight.
				if (cardsForStraight.Count() == 5) {
					highestStraightCards = cardsForStraight.ToList();
					break;
				}
			}

			return highestStraightCards;
		}

		#endregion

		#endregion

		#endregion
	}
}
