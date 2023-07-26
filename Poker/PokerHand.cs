using Casino_Suite_Poker;
using System;
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
		/// Card with value of the highest participating card for Straight, Flush, and StraightFlush.
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
		/// These are all the cards/ values not present in the first and second value.
		/// </summary>
		public List<Card> kickers {
			get {
				return _kickers.OrderByDescending(card => card.cardValue).ToList();
			}
		}

		/// <summary>
		/// Unsorted list of kickers.
		/// </summary>
		private List<Card> _kickers { get; set; }

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
		private PokerHand(HandType handType, CardValue? firstCard, CardValue? secondCard, Suit? suit, List<Card> kickers) {
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

		#endregion
	}
}
