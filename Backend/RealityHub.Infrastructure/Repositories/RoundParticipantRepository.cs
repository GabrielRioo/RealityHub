using Microsoft.EntityFrameworkCore;
using RealityHub.Application.Interfaces;
using RealityHub.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Infrastructure.Repositories
{
    public class RoundParticipantRepository : IRoundParticipantRepository
    {
        private readonly RealityHubDbContext _context;

        public RoundParticipantRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        // Verifica se o participante está no round.
        public async Task<bool> ExistisAsync(Guid roundId, Guid participantId)
        {
            return await _context.RoundParticipants
                .AnyAsync(rp => rp.RoundId == roundId && rp.ParticipantId == participantId);
        }
    }
}
