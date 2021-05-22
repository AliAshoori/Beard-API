using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Buffers;

namespace WildBeard.Orders.Api.Utils
{
    public class HateosOutputFormatter : NewtonsoftJsonOutputFormatter
    {
        public HateosOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool, MvcOptions mvcOptions) : base(serializerSettings, charPool, mvcOptions)
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/wildbeard.api.hateoas+json"));
        }
    }
}
