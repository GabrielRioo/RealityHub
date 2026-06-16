using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IRoundParticipantRepository
    {
        // verifica se o participante realmente está no paredão daquele round
        Task<bool> ExistisAsync(Guid roundId, Guid participantId);
    }
}
