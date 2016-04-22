using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// FiftyFifty payout calculator. The 50/50 payout scheme returns double the tournament buyin to people
    /// who finish in the top half of the table. If the number of runners is odd the player in the middle position
    /// should get their stake back. Any tied positions should have the sum of the amount due to those positions
    /// split equally among them.
    /// </summary>
    public class FiftyFiftyPayoutCalculator : BasePayoutCalculator
    {
      
        private bool CheckSplitwinning(Tournament tournament)
        {
            bool isSplitWinnig = false;
            var firstchip = tournament.Entries.FirstOrDefault();
            isSplitWinnig = tournament.Entries.Skip(1).Any(s => s.Chips == firstchip.Chips);
            return isSplitWinnig;
           
        }

        private bool CheckSplitWinnings_TiedLastEntries(Tournament tournament)
        {
            bool isSplitlastEntryTiedWinnig = false;
            var Lastchip = tournament.Entries.Reverse().FirstOrDefault();
            isSplitlastEntryTiedWinnig = tournament.Entries.Reverse().Skip(1).Any(s => s.Chips == Lastchip.Chips);
            return isSplitlastEntryTiedWinnig;

        }
        public override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            // Logic is implemented by understanding

            List<PayingPosition> payingposition = new List<PayingPosition>();
            if (tournament.Entries.Count  == 2) 
            {

                payingposition.Add(new PayingPosition() { Position = 1, Payout = tournament.PrizePool });
               
            }
            else if (CheckSplitWinnings_TiedLastEntries(tournament))
            {
               
                var Lastchip = tournament.Entries.Reverse().FirstOrDefault();
                var lastEntiesTied = tournament.Entries.Reverse().Where(t => t.Chips == Lastchip.Chips).Count();
              
                var totaltieamount = 0;
                for (int j = 1; j <= lastEntiesTied; j++)                {
                    
                    totaltieamount += tournament.BuyIn / lastEntiesTied;
                }
                payingposition.Add(new PayingPosition() { Position = 0, Payout = (tournament.PrizePool - totaltieamount) });
                   for (int j = 1; j <= lastEntiesTied; j++)
                   {
                       payingposition.Add(new PayingPosition()
                       {
                           Position = j,
                           Payout = tournament.BuyIn/lastEntiesTied
                       });                      
                   }

                
            }
            else if (tournament.Entries.Count % 2 != 0 )
            {

                if (CheckSplitwinning(tournament))  //Splitwinning
                {
                    payingposition.Add(new PayingPosition() { Position = 1, Payout = (tournament.PrizePool/2) });
                    payingposition.Add(new PayingPosition() { Position = 2, Payout = (tournament.PrizePool/2) });
                }
                else //middleone get stake back
                {

                    payingposition.Add(new PayingPosition() { Position = 1, Payout = (tournament.PrizePool - tournament.BuyIn) });
                    payingposition.Add(new PayingPosition()
                    {
                        Position = tournament.Entries.Count / 2,
                        Payout = tournament.BuyIn
                    });
                }
            }
            else 
            {
                payingposition.Add(new PayingPosition() { Position = 1, Payout = tournament.PrizePool/2 });
                payingposition.Add(new PayingPosition() { Position = 2, Payout = tournament.PrizePool /2 });

            }
            return payingposition as IReadOnlyList<PayingPosition>;
        }

       
    }
}
