using RealityHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Domain.Entities
{
    public class Round
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndsAt { get; set; }
        public EnumRoundStatus Status { get; set; } = EnumRoundStatus.Open;

        // Navigation Properties
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
