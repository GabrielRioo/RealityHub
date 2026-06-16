using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IParticipantRepository
    {
        // Busca um participante pelo ID
        Task<Participant?> GetByIdAsync(Guid participantId);
    }
}
