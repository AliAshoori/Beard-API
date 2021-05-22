using Microsoft.AspNetCore.Http;

namespace WildBeard.Orders.ApplicationServices.ResponseLinkGenerators
{
    public interface IHttpContextProvider
    {
        string GetAppBaseUrl();

        HttpContext GetCurrentContext();
    }
}
