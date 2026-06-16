using Microsoft.EntityFrameworkCore;
using RealityHub.Application.Interfaces;
using RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults;
using RealityHub.Infrastructure.Persistence;

namespace RealityHub.Infrastructure.Repositories
{
    public class VoteReadRepository : IVoteReadRepository
    {
        private readonly RealityHubDbContext _context;

        public VoteReadRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoundResultDto>> GetCurrentRoundResultsAsync(Guid roundId)
        {
            var totalVotes = await _context.Votes
                .CountAsync(v => v.RoundId == roundId);

            var results = await _context.Votes
                .Where(v => v.RoundId == roundId)
                .GroupBy(v => new // agrupar os votos por participante
                {
                    v.ParticipantId,
                    v.Participant.Name
                })
                .Select(group => new RoundResultDto // transforma no DTO de resultado
                {
                    ParticipantId = group.Key.ParticipantId,
                    ParticipantName = group.Key.Name,
                    Votes = group.Count(),
                    Percentage = totalVotes == 0 ? 0 : (double)group.Count() / totalVotes * 100,
                })
                .OrderByDescending(x => x.Votes)
                .ToListAsync();

            return results;
        }
    }
}
