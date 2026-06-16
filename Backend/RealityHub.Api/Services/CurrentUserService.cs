using RealityHub.Application.Interfaces;
using System.Security.Claims;

namespace RealityHub.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Obtem o UserId do token JWT. Se nao tiver, retorna erro.
        public Guid GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new Exception("Usuário não autenticado.");

            return Guid.Parse(userIdClaim);
        }
    }
}
