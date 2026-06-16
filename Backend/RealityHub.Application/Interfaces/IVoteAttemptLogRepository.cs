using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IVoteAttemptLogRepository
    {
        // salva auditoria sempre que alguém tenta votar
        Task AddAsync(VoteAttemptLog log);
    }
}
