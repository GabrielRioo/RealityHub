using RealityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IRoundRepository
    {
        // retorna o round aberto atual.
        Task<Round?> GetCurrentOpenRoundAsync();
    }
}
