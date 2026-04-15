using DeckOfPlayingCardsLibrary.Entities;
using DeckOfPlayingCardsLibrary.Enums;
using PokerLibrary.Entities;
using PokerLibrary.Enums;

namespace PokerLibrary.Services
{
    /// <summary>
    /// Service class for evaluating the best possible 5-card <see cref="PokerHand"/> from a given collection of cards.
    /// </summary>
    public class PokerHandEvaluator
    {
        /// <summary>
        /// Gets the best 5-card <see cref="PokerHand"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>The best 5-card <see cref="PokerHand"/> that can be formed with the given cards.</returns>
        public PokerHand GetBestHand(IEnumerable<Card> cards)
        {
            // Check for the best possible hand in descending order of poker hand ranks.
            // Stop searching when a hand is found, as every possible hand after that will be of a lower rank.

            var bestStraightFlush = this.GetBestStraightFlush(cards);
            if (bestStraightFlush != null)
            {
                return bestStraightFlush;
            }

            var bestFourOfAKind = this.GetBestFourOfAKind(cards);
            if (bestFourOfAKind != null)
            {
                return bestFourOfAKind;
            }

            var bestFullHouse = this.GetBestFullHouse(cards);
            if (bestFullHouse != null)
            {
                return bestFullHouse;
            }

            var bestFlush = this.GetBestFlush(cards);
            if (bestFlush != null)
            {
                return bestFlush;
            }

            var bestStraight = this.GetBestStraight(cards);
            if (bestStraight != null)
            {
                return bestStraight;
            }

            var bestThreeOfAKind = this.GetBestThreeOfAKind(cards);
            if (bestThreeOfAKind != null)
            {
                return bestThreeOfAKind;
            }

            var bestTwoPair = this.GetBestTwoPair(cards);
            if (bestTwoPair != null)
            {
                return bestTwoPair;
            }

            var bestPair = this.GetBestPair(cards);
            if (bestPair != null)
            {
                return bestPair;
            }

            return this.GetBestHighCard(cards);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.StraightFlush"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.StraightFlush"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.StraightFlush"/> possible.
        /// </returns>
        private PokerHand? GetBestStraightFlush(IEnumerable<Card> cards)
        {
            // Find the highest straight flush cards and determine the suit.
            var highestStraightFlushCards = this.GetHighestStraightCards(cards, mustBeFlush: true);

            if (highestStraightFlushCards == null)
            {
                return null;
            }

            var suit = highestStraightFlushCards.First().Suit;
            var kickers = highestStraightFlushCards.OrderByDescending(card => card.Rank).ToList();

            // If the hand is a straight flush from Ace to Five, move the Ace to the end.
            // In this case, the ace is the lowest rank of the hand, not the highest.
            if (kickers.Last().Rank == CardRank.Two && kickers.First().Rank == CardRank.Ace)
            {
                var aceCard = kickers.First();
                _ = kickers.Remove(aceCard);
                kickers.Add(aceCard);
            }

            return new PokerHand(HandRanking.StraightFlush, null, null, suit, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.FourOfAKind"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.FourOfAKind"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.FourOfAKind"/> possible.
        /// </returns>
        private PokerHand? GetBestFourOfAKind(IEnumerable<Card> cards)
        {
            // Only 1 four of a kind is possible, so we do not have to check if the one we find is the highest one.
            var highestFourOrAKindValue = this.GetHighestOfAKindValue(cards, 4);

            if (highestFourOrAKindValue == null)
            {
                return null;
            }

            var kickers = cards
                .OrderByDescending(card => card.Rank)
                .Where(card => card.Rank != highestFourOrAKindValue)
                .Take(1);

            return new PokerHand(
                HandRanking.FourOfAKind,
                highestFourOrAKindValue,
                null,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.FullHouse"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.FullHouse"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.FullHouse"/> possible.
        /// </returns>
        private PokerHand? GetBestFullHouse(IEnumerable<Card> cards)
        {
            var highestThreeOfAKindValue = this.GetHighestOfAKindValue(cards, 3);

            if (highestThreeOfAKindValue == null)
            {
                return null;
            }

            var secondHighestThreeOfAKindValue = this.GetHighestOfAKindValue(
                cards.Where(card => card.Rank != highestThreeOfAKindValue),
                3
            );
            var highestPairValue = this.GetHighestOfAKindValue(cards, 2);

            if (secondHighestThreeOfAKindValue == null && highestPairValue == null)
            {
                return null;
            }

            var kickers = new List<Card>();

            return new PokerHand(
                HandRanking.FullHouse,
                highestThreeOfAKindValue,
                highestPairValue,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Flush"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Flush"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Flush"/> possible.
        /// </returns>
        private PokerHand? GetBestFlush(IEnumerable<Card> cards)
        {
            var flushCards = cards
                .GroupBy(card => card.Suit)
                .Where(group => group.Count() >= 5)
                .SelectMany(group => group);

            if (!flushCards.Any())
            {
                return null;
            }

            var flushSuit = flushCards.First().Suit;
            var kickers = flushCards.OrderByDescending(card => card.Rank);

            return new PokerHand(HandRanking.Flush, null, null, flushSuit, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Straight"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Straight"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Straight"/> possible.
        /// </returns>
        private PokerHand? GetBestStraight(IEnumerable<Card> cards)
        {
            // Find the highest straight cards and return them as a PokerHand.
            var highestStraightCards = this.GetHighestStraightCards(cards, mustBeFlush: false);

            if (highestStraightCards == null)
            {
                return null;
            }

            var kickers = highestStraightCards.OrderByDescending(card => card.Rank).ToList();

            // If the hand is a straight from Ace to Five, move the Ace to the end.
            // In this case, the ace is the lowest rank of the hand, not the highest.
            if (kickers.Last().Rank == CardRank.Two && kickers.First().Rank == CardRank.Ace)
            {
                var aceCard = kickers.First();
                _ = kickers.Remove(aceCard);
                kickers.Add(aceCard);
            }

            return new PokerHand(HandRanking.Straight, null, null, null, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.ThreeOfAKind"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.ThreeOfAKind"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.ThreeOfAKind"/> possible.
        /// </returns>
        private PokerHand? GetBestThreeOfAKind(IEnumerable<Card> cards)
        {
            // Find the highest three of a kind and the two highest kickers, then return as a PokerHand.
            var highestThreeOfAKindValue = this.GetHighestOfAKindValue(cards, 3);

            if (highestThreeOfAKindValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestThreeOfAKindValue)
                .OrderByDescending(card => card.Rank)
                .Take(2);

            return new PokerHand(
                HandRanking.ThreeOfAKind,
                highestThreeOfAKindValue,
                null,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.TwoPair"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.TwoPair"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.TwoPair"/> possible.
        /// </returns>
        private PokerHand? GetBestTwoPair(IEnumerable<Card> cards)
        {
            // Find the two highest pairs and the highest kicker, then return as a PokerHand.
            var highestPairValue = this.GetHighestOfAKindValue(cards, 2);

            if (highestPairValue == null)
            {
                return null;
            }

            var secondHighestPairValue = this.GetHighestOfAKindValue(
                cards.Where(card => card.Rank != highestPairValue),
                2
            );

            if (secondHighestPairValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestPairValue && card.Rank != secondHighestPairValue)
                .OrderByDescending(card => card.Rank)
                .Take(1);

            return new PokerHand(
                HandRanking.TwoPair,
                highestPairValue,
                secondHighestPairValue,
                null,
                kickers
            );
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.Pair"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>
        /// The best <see cref="HandRanking.Pair"/> <see cref="PokerHand"/> if one is possible.
        /// <see langword="null"/> if there is no <see cref="HandRanking.Pair"/> possible.
        /// </returns>
        private PokerHand? GetBestPair(IEnumerable<Card> cards)
        {
            // Find the highest pair and the three highest kickers, then return as a PokerHand.
            var highestPairValue = this.GetHighestOfAKindValue(cards, 2);

            if (highestPairValue == null)
            {
                return null;
            }

            var kickers = cards
                .Where(card => card.Rank != highestPairValue)
                .OrderByDescending(card => card.Rank)
                .Take(3);

            return new PokerHand(HandRanking.Pair, highestPairValue, null, null, kickers);
        }

        /// <summary>
        /// Gets the best <see cref="HandRanking.HighCard"/> possible with the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <returns>The best <see cref="HandRanking.HighCard"/> <see cref="PokerHand"/> that can be formed with the given cards.</returns>
        private PokerHand GetBestHighCard(IEnumerable<Card> cards)
        {
            // Find the five highest cards as kickers and return as a PokerHand.
            var orderedCards = cards.OrderByDescending(card => card.Rank);
            var kickers = orderedCards.Take(5);
            return new PokerHand(HandRanking.HighCard, null, null, null, kickers);
        }

        /// <summary>
        /// Gets the highest value of an "X of a kind" from the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <param name="numberOfAKind">The number of cards that should have the same rank.</param>
        /// <returns>The highest rank that forms an "X of a kind," or null if none is found.</returns>
        private CardRank? GetHighestOfAKindValue(IEnumerable<Card> cards, int numberOfAKind)
        {
            // Group the cards by rank and count those that have the specified number "of a kind".
            var highestOfAKindCards = cards
                .GroupBy(card => card.Rank)
                .Where(group => group.Count() == numberOfAKind);

            if (!highestOfAKindCards.Any())
            {
                return null;
            }

            // Find and return the highest rank among the groups.
            var highestOfAKindRank = highestOfAKindCards
                .OrderByDescending(group => group.Key)
                .First()
                .Key;

            return highestOfAKindRank;
        }

        /// <summary>
        /// Gets the cards that form the highest possible <see cref="HandRanking.Straight"/> or <see cref="HandRanking.StraightFlush"/> from the given cards.
        /// </summary>
        /// <param name="cards">A collection of cards to evaluate.</param>
        /// <param name="mustBeFlush">Specifies whether the hand must be a <see cref="HandRanking.StraightFlush"/>.</param>
        /// <returns>
        /// A list of cards that form the highest <see cref="HandRanking.Straight"/> or <see cref="HandRanking.StraightFlush"/>.
        /// <see langword="null"/> if none is found.
        /// </returns>
        private List<Card>? GetHighestStraightCards(
            IEnumerable<Card> cards,
            bool mustBeFlush = false
        )
        {
            // The ace can be used at both ends, as the card before a 2, and the card after a King.
            // Because of this, we will sort descending and add duplicate cards of each ace at the end of the list.
            var orderedCards = cards.OrderByDescending(card => card.Rank).ToList();
            if (orderedCards.Select(card => card.Rank).Contains(CardRank.Ace))
            {
                orderedCards.AddRange(
                    orderedCards.Where(card => card.Rank == CardRank.Ace).ToList()
                );
            }

            // Look for a straight (flush) for each card, starting with the highest.
            foreach (var currentCard in orderedCards)
            {
                // Four (and below) cannot be the highest card in a straight.
                if (currentCard.Rank == CardRank.Four)
                {
                    break;
                }

                // Get all cards that would make up a straight with the currentCard as the highest card.
                // If the current card is a Five, then the Ace can also be part of the straight.
                var cardsForStraight = cards
                    .Where(card => this.IsStraightCard(currentCard, card, mustBeFlush))
                    .DistinctBy(card => card.Rank);

                // If there are exactly 5 cards in cardsForStraight, then we have a straight.
                if (cardsForStraight.Count() == 5)
                {
                    return cardsForStraight.ToList();
                }
            }

            // No straight (flush) found.
            return null;
        }

        /// <summary>
        /// Checks if a card is part of a <see cref="HandRanking.Straight"/> from the highest card.
        /// </summary>
        /// <param name="highestCard">The highest card to start the <see cref="HandRanking.Straight"/> from.</param>
        /// <param name="card">The card to check.</param>
        /// <param name="mustBeFlush">Specifies whether the hand must be a <see cref="HandRanking.StraightFlush"/>.</param>
        /// <returns>True if the card is part of a <see cref="HandRanking.Straight"/>, false otherwise.</returns>
        private bool IsStraightCard(Card highestCard, Card card, bool mustBeFlush = false)
        {
            return (
                    card.Equals(highestCard)
                    || card.Rank == highestCard.Rank - 1
                    || card.Rank == highestCard.Rank - 2
                    || card.Rank == highestCard.Rank - 3
                    || card.Rank == highestCard.Rank - 4
                    || (highestCard.Rank == CardRank.Five && card.Rank == CardRank.Ace)
                ) && (!mustBeFlush || card.Suit == highestCard.Suit);
        }
    }
}
