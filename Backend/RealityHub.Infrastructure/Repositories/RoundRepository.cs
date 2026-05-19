using Microsoft.EntityFrameworkCore;
using RealityHub.Application.Interfaces;
using RealityHub.Domain.Entities;
using RealityHub.Domain.Enums;
using RealityHub.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Infrastructure.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly RealityHubDbContext _context;

        public RoundRepository(RealityHubDbContext context)
        {
            _context = context;
        }

        // Busca o round aberto, se tiver mais de um aberto, pega o mais recente.
        public async Task<Round?> GetCurrentOpenRoundAsync()
        {
            return await _context.Rounds
                .Where(r => r.Status == EnumRoundStatus.Open)
                .OrderByDescending(r => r.StartAt)
                .FirstOrDefaultAsync();

        }
    }
}
