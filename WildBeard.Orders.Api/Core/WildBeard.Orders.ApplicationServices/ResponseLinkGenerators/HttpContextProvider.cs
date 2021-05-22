using Microsoft.AspNetCore.Http;
using System;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private readonly HttpContext _current;

        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _current = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetAppBaseUrl() => $"{_current.Request.Scheme}://{_current.Request.Host}{_current.Request.PathBase}";

        public HttpContext GetCurrentContext() => _current;
    }
}
