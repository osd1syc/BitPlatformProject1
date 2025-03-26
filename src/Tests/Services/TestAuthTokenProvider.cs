using Project1.Client.Core.Services.Contracts;

namespace Project1.Tests.Services;

public partial class TestAuthTokenProvider : IAuthTokenProvider
{
    [AutoInject] private IStorageService storageService = default!;

    public async Task<string?> GetAccessToken()
    {
        return await storageService.GetItem("access_token");
    }
}
