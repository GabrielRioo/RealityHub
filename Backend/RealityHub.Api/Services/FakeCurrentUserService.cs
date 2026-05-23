using RealityHub.Application.Interfaces;

namespace RealityHub.Api.Services
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public Guid GetUserId()
        {
            // Sempre retorna o mesmo usuário fake
            return Guid.Parse("11111111-1111-1111-1111-111111111111");
        }
    }
}
