using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults
{
    public class RoundResultDto
    {
        public Guid ParticipantId { get; set; }
        public string ParticipantName { get; set; } = string.Empty;
        public int Votes { get; set; }
        public double Percentage { get; set; }
    }
}
