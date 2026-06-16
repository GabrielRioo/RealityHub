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
            if (context.Participants.Any() ||  context.Rounds.Any())
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

            var roundParticipants = new List<RoundParticipant>
            {
                new RoundParticipant { Id = Guid.NewGuid(), ParticipantId = participants[0].Id, RoundId = round.Id, JoinedAt = round.StartAt, IsEliminated = false},
                new RoundParticipant { Id = Guid.NewGuid(), ParticipantId = participants[1].Id, RoundId = round.Id, JoinedAt = round.StartAt, IsEliminated = false},
                new RoundParticipant { Id = Guid.NewGuid(), ParticipantId = participants[2].Id, RoundId = round.Id, JoinedAt = round.StartAt, IsEliminated = false},
                new RoundParticipant { Id = Guid.NewGuid(), ParticipantId = participants[3].Id, RoundId = round.Id, JoinedAt = round.StartAt, IsEliminated = true}
            };

            await context.AddRangeAsync(roundParticipants);

            await context.SaveChangesAsync();
        }
    }
}
