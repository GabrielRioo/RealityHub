using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IVoteRepository
    {
        // salva voto
        Task AddAsync(Vote vote);

        // pega o horário do último voto do usuário no round
        Task<DateTime?> GetLastVoteTimeAsync(Guid userId, Guid roundId);
    }
}
