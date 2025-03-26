﻿using Project1.Shared.Dtos.Identity;
using Project1.Shared.Controllers.Identity;

namespace Project1.Client.Core.Components.Pages.Authorized.Settings;

public partial class SettingsPage
{
    protected override string? Title => Localizer[nameof(AppStrings.Settings)];
    protected override string? Subtitle => string.Empty;


    private bool showPasswordless;


    [Parameter] public string? Section { get; set; }


    [AutoInject] protected HttpClient HttpClient = default!;
    [AutoInject] private IUserController userController = default!;
    [AutoInject] private IWebAuthnService webAuthnService = default!;


    private UserDto? user;
    private bool isLoading;
    private string? openedAccordion;


    protected override async Task OnInitAsync()
    {
        openedAccordion = Section?.ToLower();

        isLoading = true;

        try
        {
            user = (await PrerenderStateService.GetValue(() => HttpClient.GetFromJsonAsync("api/User/GetCurrentUser", JsonSerializerOptions.GetTypeInfo<UserDto>(), CurrentCancellationToken)))!;
            if (InPrerenderSession is false)
            {
                showPasswordless = await webAuthnService.IsWebAuthnAvailable();
            }
        }
        finally
        {
            isLoading = false;
        }

        await base.OnInitAsync();
    }
}
