using Fido2NetLib;
using Project1.Shared.Dtos.Statistics;

namespace Project1.Server.Api.Services;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(NugetStatsDto))]
[JsonSerializable(typeof(GoogleRecaptchaVerificationResponse))]
[JsonSerializable(typeof(ProblemDetails))]
[JsonSerializable(typeof(AuthenticatorResponse))]
public partial class ServerJsonContext : JsonSerializerContext
{
}
