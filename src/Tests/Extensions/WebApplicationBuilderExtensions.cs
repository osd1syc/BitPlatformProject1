using Project1.Server.Web;
using Project1.Tests.Services;
using Project1.Server.Api.Services;

namespace Microsoft.AspNetCore.Builder;

public static partial class WebApplicationBuilderExtensions
{
    public static void AddTestProjectServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.AddServerWebProjectServices();


        services.AddTransient(sp =>
        {
            return new HttpClient(sp.GetRequiredService<HttpMessageHandler>())
            {
                BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetServerAddress(), UriKind.Absolute)
            };
        });
    }
}
