﻿using Fido2NetLib;

namespace Project1.Client.Maui.Services;

public partial class MauiWebAuthnService : WebAuthnServiceBase
{
    [AutoInject] private ILocalHttpServer localHttpServer = default!;
    [AutoInject] private IExternalNavigationService externalNavigationService = default!;

    public override async ValueTask<AuthenticatorAssertionRawResponse> GetWebAuthnCredential(AssertionOptions options, CancellationToken cancellationToken)
    {
        try
        {
            await externalNavigationService.NavigateToAsync($"{localHttpServer.Origin}/external-js-runner.html");

            var result = (await MauiExternalJsRunner.RequestToBeSent!.Invoke(JsonSerializer.SerializeToDocument(new { Type = "getCredential", Options = options }, JsonSerializerOptions.Web)))
                .Deserialize<AuthenticatorAssertionRawResponse>(JsonSerializerOptions.Web)!;

            return result ?? throw new TaskCanceledException();
        }
        finally
        {
            await CloseExternalBrowser();
        }
    }

    private static async Task CloseExternalBrowser()
    {
        await MauiExternalJsRunner.RequestToBeSent!.Invoke(JsonSerializer.SerializeToDocument(new { Type = "close" }, JsonSerializerOptions.Web));

        if (AppPlatform.IsIOS)
        {
            // SocialSignedInPage.razor's `window.close()` does NOT work on iOS's in app browser.
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
#if iOS
                if (UIKit.UIApplication.SharedApplication.KeyWindow?.RootViewController?.PresentedViewController is SafariServices.SFSafariViewController controller)
                {
                    controller.DismissViewController(animated: true, completionHandler: null);
                }
#endif
            });
        }
    }

    public override async ValueTask<AuthenticatorAttestationRawResponse> CreateWebAuthnCredential(CredentialCreateOptions options)
    {
        try
        {
            await externalNavigationService.NavigateToAsync($"{localHttpServer.Origin}/external-js-runner.html");

            return (await MauiExternalJsRunner.RequestToBeSent!.Invoke(JsonSerializer.SerializeToDocument(new { Type = "createCredential", Options = options }, JsonSerializerOptions.Web)))
                .Deserialize<AuthenticatorAttestationRawResponse>(JsonSerializerOptions.Web)!;
        }
        finally
        {
            await CloseExternalBrowser();
        }
    }

    public override async ValueTask<bool> IsWebAuthnAvailable()
    {
        var osVersion = Environment.OSVersion.Version;

        return OperatingSystem.IsWindowsVersionAtLeast(10, 0, 18362)
            || true /* Checkout SupportedOSPlatformVersion in Directory.Build.props */;
    }
}
