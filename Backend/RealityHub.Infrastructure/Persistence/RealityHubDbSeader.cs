using RealityHub.Domain.Entities;
using RealityHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Infrastructure.Persistence
{
    // Criação da Seed inicial do banco de dados
    public class RealityHubDbSeader
    {
        public static async Task SeedAsync(RealityHubDbContext context)
        {
            // Se já existir algum participante, significa que já foi seedado.
            if (context.Participants.Any())
                return;

            var participants = new List<Participant>
            {
                new Participant { Id = Guid.NewGuid(), Name = "Gabriel", PhotoUrl = "https://placehold.co/200x200", IsActive = true },
                new Participant { Id = Guid.NewGuid(), Name = "Camila", PhotoUrl = "https://placehold.co/200x200", IsActive = true },
                new Participant { Id = Guid.NewGuid(), Name = "Richard", PhotoUrl = "https://placehold.co/200x200", IsActive = true },
                new Participant { Id = Guid.NewGuid(), Name = "Hime", PhotoUrl = "https://placehold.co/200x200", IsActive = true }
            };

            await context.Participants.AddRangeAsync(participants);

            var round = new Round
            {
                Id = Guid.NewGuid(),
                Title = "Paredão #1",
                StartAt = DateTime.UtcNow,
                EndsAt = DateTime.UtcNow.AddDays(7),
                Status = EnumRoundStatus.Open
            };

            await context.Rounds.AddAsync(round);

            await context.SaveChangesAsync();
        }
    }
