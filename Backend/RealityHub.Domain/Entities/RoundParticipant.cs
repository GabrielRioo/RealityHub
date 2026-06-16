using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Domain.Entities
{
    public class RoundParticipant
    {
        public Guid Id { get; set; }
        public Guid RoundId { get; set; }
        public Guid ParticipantId { get; set; }
        public bool IsEliminated { get; set; } = false;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
