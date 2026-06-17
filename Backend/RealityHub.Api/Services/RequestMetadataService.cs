using RealityHub.Application.Interfaces;

namespace RealityHub.Api.Services
{
    public class RequestMetadataService : IRequestMetadataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestMetadataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Obtem o IP do request
        public string GetIpAddress()
        {
            var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            return ip ?? "Desconhecido";
        }

        // Obtem o navegador/dispositivo
        public string GetUserAgent()
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
            return string.IsNullOrEmpty(userAgent) ? "Desconhecido" : userAgent;
        }
    }
}