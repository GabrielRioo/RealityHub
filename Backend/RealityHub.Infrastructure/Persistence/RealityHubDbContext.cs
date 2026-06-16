using Microsoft.EntityFrameworkCore;
using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Infrastructure.Persistence
{
    public class RealityHubDbContext : DbContext
    {
        public RealityHubDbContext(DbContextOptions<RealityHubDbContext> options) : base(options) { }

        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Round> Rounds => Set<Round>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RoundParticipant> RoundParticipants => Set<RoundParticipant>();

        public DbSet<VoteAttemptLog> VoteAttemptLogs => Set<VoteAttemptLog>();
    }
}
