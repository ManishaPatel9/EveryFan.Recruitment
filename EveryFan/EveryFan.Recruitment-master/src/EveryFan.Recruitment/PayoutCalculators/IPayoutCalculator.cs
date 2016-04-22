using System.Collections.Generic;

namespace EveryFan.Recruitment.PayoutCalculators
{
    public interface IPayoutCalculator
    {
        IReadOnlyList<TournamentPayout> Calculate(Tournament tournament);
    }
}
