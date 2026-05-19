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
    public class VoteAttemptLogRepository : IVoteAttemptLogRepository
    {
        private readonly RealityHubDbContext _context;

        public VoteAttemptLogRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        // Registra logs de tentativas (sucesso ou falha)
        public async Task AddAsync(VoteAttemptLog log)
        {
            await _context.VoteAttemptLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
