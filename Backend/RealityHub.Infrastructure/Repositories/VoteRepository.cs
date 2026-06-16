using Microsoft.EntityFrameworkCore;
using RealityHub.Application.Interfaces;
using RealityHub.Domain.Entities;
using RealityHub.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly RealityHubDbContext _context;

        public VoteRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        // Salva o voto
        public async Task AddAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        // Pega o último horario de voto do usuario naquele round.
        public async Task<DateTime?> GetLastVoteTimeAsync(Guid userId, Guid roundId)
        {
            return await _context.Votes
                .Where(v => v.UserId == userId && v.RoundId == roundId)
                .OrderByDescending(v => v.CreatedAt)
                .Select(v => (DateTime?)v.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
