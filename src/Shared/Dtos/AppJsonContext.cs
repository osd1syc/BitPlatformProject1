using Fido2NetLib;
using Fido2NetLib.Objects;
using Project1.Shared.Dtos.Todo;
using Project1.Shared.Dtos.PushNotification;
using Project1.Shared.Dtos.Identity;
using Project1.Shared.Dtos.Statistics;

namespace Project1.Shared.Dtos;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Dictionary<string, JsonElement>))]
[JsonSerializable(typeof(Dictionary<string, string?>))]
[JsonSerializable(typeof(TimeSpan))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(Guid[]))]
[JsonSerializable(typeof(GitHubStats))]
[JsonSerializable(typeof(NugetStatsDto))]
[JsonSerializable(typeof(AppProblemDetails))]
[JsonSerializable(typeof(PushNotificationSubscriptionDto))]
[JsonSerializable(typeof(TodoItemDto))]
[JsonSerializable(typeof(PagedResult<TodoItemDto>))]
[JsonSerializable(typeof(List<TodoItemDto>))]
[JsonSerializable(typeof(AssertionOptions))]
[JsonSerializable(typeof(AuthenticatorAssertionRawResponse))]
[JsonSerializable(typeof(AuthenticatorAttestationRawResponse))]
[JsonSerializable(typeof(CredentialCreateOptions))]
[JsonSerializable(typeof(VerifyAssertionResult))]
[JsonSerializable(typeof(VerifyWebAuthnAndSignInDto))]
[JsonSerializable(typeof(WebAuthnAssertionOptionsRequestDto))]
public partial class AppJsonContext : JsonSerializerContext
{
}
