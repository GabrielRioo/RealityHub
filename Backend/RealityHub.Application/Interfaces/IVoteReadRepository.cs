using RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IVoteReadRepository
    {
        Task<List<RoundResultDto>> GetCurrentRoundResultsAsync(Guid roundId);
    }
}
