using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid RoundId { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
