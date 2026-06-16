using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityHub.Application.Interfaces
{
    public interface IRequestMetadataService
    {
        // identifica o usuário na rede
        string GetIpAddress();

        // identifica navegador/dispositivo
        string GetUserAgent();
    }
}
