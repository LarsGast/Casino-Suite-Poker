using DeckOfPlayingCardsLibrary.Entities;
using PokerLibrary.Entities;

namespace PokerLibrary.Services
{
    /// <summary>
    /// Service class responsible for comparing two <see cref="PokerHand"/> instances to determine which one is better based on standard poker hand rankings and tiebreaker rules.
    /// </summary>
    public class PokerHandComparer
    {
        /// <summary>
        /// Checks if <paramref name="firstHand"/> wins against <paramref name="secondHand"/>.
        /// </summary>
        /// <param name="firstHand">The first <see cref="PokerHand"/> to compare.</param>
        /// <param name="secondHand">The second <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="firstHand"/> hand wins.
        /// <see langword="false"/> if <paramref name="secondHand"/> hand wins.
        /// <see langword="null"/> if it's a draw.
        /// </returns>
        public bool? WinsAgainst(PokerHand firstHand, PokerHand secondHand)
        {
            // Check if the first hand has a better hand rank than the second hand.
            var hasBetterHandRank = this.HasBetterHandRank(firstHand, secondHand);
            if (hasBetterHandRank != null)
            {
                return hasBetterHandRank;
            }

            // If hand rank is the same, check by the primary card rank.
            var hasBetterFirstCard = this.HasBetterFirstCard(firstHand, secondHand);
            if (hasBetterFirstCard != null)
            {
                return hasBetterFirstCard;
            }

            // If the primary card rank is the same, check by the secondary card rank.
            var hasBetterSecondCard = this.HasBetterSecondCard(firstHand, secondHand);
            if (hasBetterSecondCard != null)
            {
                return hasBetterSecondCard;
            }

            // If all previous comparisons result in a draw, compare by kickers.
            return this.HasBetterKickers(firstHand, secondHand);
        }

        /// <summary>
        /// Determines the best hand from a list of hands.
        /// </summary>
        /// <remarks>
        /// Only returns one hand in case of a draw.
        /// </remarks>
        /// <param name="hands">A list of <see cref="PokerHand"/> to evaluate.</param>
        /// <returns>The best <see cref="PokerHand"/> among the provided list.</returns>
        public PokerHand GetWinningHand(IEnumerable<PokerHand> hands)
        {
            // Find the highest hand rank among the provided hands.
            // If only one hand has the best hand rank, return it.
            var bestHandRank = hands.OrderByDescending(hand => hand.HandRank).First().HandRank;
            var handsBestHandRank = hands.Where(hand => hand.HandRank == bestHandRank);
            if (handsBestHandRank.Count() == 1)
            {
                return handsBestHandRank.First();
            }

            // Now compare by the primary card rank.
            // If only one hand has the best primary card rank, return it.
            var bestPrimaryCardRank = handsBestHandRank
                .OrderByDescending(hand => hand.PrimaryCardRank)
                .First()
                .PrimaryCardRank;
            var handsBestPrimaryCardRank = handsBestHandRank.Where(hand =>
                hand.PrimaryCardRank == bestPrimaryCardRank
            );
            if (handsBestPrimaryCardRank.Count() == 1)
            {
                return handsBestPrimaryCardRank.First();
            }

            // Now compare by the secondary card rank.
            // If only one hand has the best secondary card rank, return it.
            var bestSecondaryCardRank = handsBestPrimaryCardRank
                .OrderByDescending(hand => hand.SecondaryCardRank)
                .First()
                .SecondaryCardRank;
            var handsBestSecondaryCardRank = handsBestPrimaryCardRank.Where(hand =>
                hand.SecondaryCardRank == bestSecondaryCardRank
            );
            if (handsBestSecondaryCardRank.Count() == 1)
            {
                return handsBestSecondaryCardRank.First();
            }

            // Lastly, compare the kickers.
            var handsHighestKickers = this.GetWinningHandByKickers(
                handsBestSecondaryCardRank.ToList()
            );

            // If there are multiple same-quality hands (so a draw), return one of those hands.
            return handsHighestKickers.First();
        }

        /// <summary>
        /// Checks if <paramref name="firstHand"/> has a better <see cref="PokerHand.HandRank"/> than <paramref name="secondHand"/>.
        /// </summary>
        /// <param name="firstHand">The first <see cref="PokerHand"/> to compare.</param>
        /// <param name="secondHand">The second <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="firstHand"/> has a better <see cref="PokerHand.HandRank"/>.
        /// <see langword="false"/> if the <paramref name="secondHand"/> hand has a better <see cref="PokerHand.HandRank"/>.
        /// <see langword="null"/> if they have the same <see cref="PokerHand.HandRank"/>.
        /// </returns>
        private bool? HasBetterHandRank(PokerHand firstHand, PokerHand secondHand)
        {
            return firstHand.HandRank == secondHand.HandRank
                ? null
                : firstHand.HandRank > secondHand.HandRank;
        }

        /// <summary>
        /// Checks if <paramref name="firstHand"/> has a better <see cref="PokerHand.PrimaryCardRank"/> than <paramref name="secondHand"/>.
        /// </summary>
        /// <param name="firstHand">The first <see cref="PokerHand"/> to compare.</param>
        /// <param name="secondHand">The second <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="firstHand"/> has a better <see cref="PokerHand.PrimaryCardRank"/>.
        /// <see langword="false"/> if <paramref name="secondHand"/> has a better <see cref="PokerHand.PrimaryCardRank"/>.
        /// <see langword="null"/> if they have the same <see cref="PokerHand.PrimaryCardRank"/>.
        /// </returns>
        private bool? HasBetterFirstCard(PokerHand firstHand, PokerHand secondHand)
        {
            return firstHand.PrimaryCardRank == secondHand.PrimaryCardRank
                ? null
                : firstHand.PrimaryCardRank > secondHand.PrimaryCardRank;
        }

        /// <summary>
        /// Checks if <paramref name="firstHand"/> has a better <see cref="PokerHand.SecondaryCardRank"/> than <paramref name="secondHand"/>.
        /// </summary>
        /// <param name="firstHand">The first <see cref="PokerHand"/> to compare.</param>
        /// <param name="secondHand">The second <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="firstHand"/> has a better <see cref="PokerHand.SecondaryCardRank"/>.
        /// <see langword="false"/> if <paramref name="secondHand"/> has a better <see cref="PokerHand.SecondaryCardRank"/>.
        /// <see langword="null"/> if they have the same <see cref="PokerHand.SecondaryCardRank"/>.
        /// </returns>
        private bool? HasBetterSecondCard(PokerHand firstHand, PokerHand secondHand)
        {
            return firstHand.SecondaryCardRank == secondHand.SecondaryCardRank
                ? null
                : firstHand.SecondaryCardRank > secondHand.SecondaryCardRank;
        }

        /// <summary>
        /// Checks if <paramref name="firstHand"/> has a better <see cref="PokerHand.Kickers"/> than <paramref name="secondHand"/>.
        /// </summary>
        /// <param name="firstHand">The first <see cref="PokerHand"/> to compare.</param>
        /// <param name="secondHand">The second <see cref="PokerHand"/> to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="firstHand"/> has a better <see cref="PokerHand.Kickers"/>.
        /// <see langword="false"/> if <paramref name="secondHand"/> has a better <see cref="PokerHand.Kickers"/>.
        /// <see langword="null"/> if they have the same <see cref="PokerHand.Kickers"/>.
        /// </returns>
        private bool? HasBetterKickers(PokerHand firstHand, PokerHand secondHand)
        {
            for (var i = 0; i < firstHand.Kickers.Count; i++)
            {
                if (firstHand.Kickers[i].Rank > secondHand.Kickers[i].Rank)
                {
                    return true;
                }

                if (firstHand.Kickers[i].Rank < secondHand.Kickers[i].Rank)
                {
                    return false;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the best hand(s) among the given hands based solely on the <see cref="PokerHand.Kickers"/>.
        /// </summary>
        /// <param name="hands">A list of <see cref="PokerHand"/>s to evaluate.</param>
        /// <returns>A list of <see cref="PokerHand"/>s with the best <see cref="PokerHand.Kickers"/>.</returns>
        private List<PokerHand> GetWinningHandByKickers(IEnumerable<PokerHand> hands)
        {
            // Create a list of hands with the highest kickers.
            var handsWithHighestKickers = hands.ToList();

            // Iterate through each kicker position.
            for (var i = 0; i < handsWithHighestKickers.First().Kickers.Count; i++)
            {
                Card? highestKicker = null;

                // Find the highest kicker at the current index among all hands.
                foreach (var hand in hands)
                {
                    var handKicker = hand.Kickers[i];
                    if (highestKicker == null || handKicker.Rank > highestKicker.Value.Rank)
                    {
                        highestKicker = handKicker;
                    }
                }

                // Filter hands with the kicker of the current index having the same rank as the highest kicker.
                handsWithHighestKickers = hands
                    .Where(hand => hand.Kickers[i].Rank == highestKicker!.Value.Rank)
                    .ToList();

                // If there's only one hand with this kicker, the winner is found.
                if (handsWithHighestKickers.Count == 1)
                {
                    return handsWithHighestKickers.ToList();
                }
            }

            // In the case of multiple hands with the exact same kickers, return these hands.
            return handsWithHighestKickers.ToList();
        }
    }
}
