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
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly RealityHubDbContext _context;

        public ParticipantRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        // Busca o participante no banco pelo ID.
        public async Task<Participant?> GetByIdAsync(Guid participantId)
        {
            return await _context.Participants
                .FirstOrDefaultAsync(p => p.Id == participantId);

        }
    }
}
