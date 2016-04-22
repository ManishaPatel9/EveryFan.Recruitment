using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// Winner takes all payout calculator, the winner recieves the entire prize pool. In the event of a tie for the winning position the
    /// prize pool is split equally between the tied players.
    /// </summary>
    public class WinnerTakesAllPayoutCalculator : BasePayoutCalculator
    {
        private bool CheckSplitwinning(Tournament tournament)
        {
            bool isSplitWinnig = false;
            var firstchip = tournament.Entries.FirstOrDefault();

            isSplitWinnig = tournament.Entries.Skip(1).Any(s => s.Chips == firstchip.Chips);
            return isSplitWinnig;

        }
        public  override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            List<PayingPosition> payingposition = new List<PayingPosition>();
            if (CheckSplitwinning(tournament))
            {
                payingposition.Add(new PayingPosition() { Position = 1, Payout = (tournament.PrizePool / 2) });
                payingposition.Add(new PayingPosition() { Position = 2, Payout = (tournament.PrizePool / 2) });
            }
            else
            {
              
                payingposition.Add(new PayingPosition() { Position = 1, Payout = tournament.PrizePool });
            }
            return payingposition as IReadOnlyList<PayingPosition>;
           
        }

        
    }
}
