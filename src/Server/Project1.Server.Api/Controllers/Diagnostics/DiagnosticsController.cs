﻿using System.Text;
using Microsoft.AspNetCore.SignalR;
using Project1.Server.Api.SignalR;
using Project1.Server.Api.Services;
using Project1.Server.Api.Models.Identity;
using Project1.Shared.Controllers.Diagnostics;

namespace Project1.Server.Api.Controllers.Diagnostics;

[ApiController, AllowAnonymous]
[Route("api/[controller]/[action]")]
public partial class DiagnosticsController : AppControllerBase, IDiagnosticsController
{
    [AutoInject] private PushNotificationService pushNotificationService = default!;
    [AutoInject] private IHubContext<AppHub> appHubContext = default!;

    [HttpPost]
    public async Task<string> PerformDiagnostics([FromQuery] string? signalRConnectionId, [FromQuery] string? pushNotificationSubscriptionDeviceId, CancellationToken cancellationToken)
    {
        StringBuilder result = new();

        result.AppendLine($"Client IP: {HttpContext.Connection.RemoteIpAddress}");

        result.AppendLine($"Trace => {Request.HttpContext.TraceIdentifier}");

        var isAuthenticated = User.IsAuthenticated();
        Guid? userSessionId = null;
        UserSession? userSession = null;

        if (isAuthenticated)
        {
            userSessionId = User.GetSessionId();
            userSession = await DbContext
                .UserSessions.SingleAsync(us => us.Id == userSessionId, cancellationToken);
        }

        result.AppendLine($"IsAuthenticated: {isAuthenticated.ToString().ToLowerInvariant()}");

        if (string.IsNullOrEmpty(pushNotificationSubscriptionDeviceId) is false)
        {
            var subscription = await DbContext.PushNotificationSubscriptions.Include(us => us.UserSession)
                .FirstOrDefaultAsync(d => d.DeviceId == pushNotificationSubscriptionDeviceId, cancellationToken);

            result.AppendLine($"Subscription exists: {(subscription is not null).ToString().ToLowerInvariant()}");

            await pushNotificationService.RequestPush("Test Push", DateTimeOffset.Now.ToString("HH:mm:ss"), "Test action", userRelatedPush: false, u => u.DeviceId == pushNotificationSubscriptionDeviceId, cancellationToken);
        }

        if (string.IsNullOrEmpty(signalRConnectionId) is false)
        {
            await appHubContext.Clients.Client(signalRConnectionId).SendAsync(SignalREvents.SHOW_MESSAGE, DateTimeOffset.Now.ToString("HH:mm:ss"), cancellationToken);
        }

        result.AppendLine($"Culture => C: {CultureInfo.CurrentCulture.Name}, UC: {CultureInfo.CurrentUICulture.Name}");

        result.AppendLine();

        foreach (var header in Request.Headers.OrderBy(h => h.Key))
        {
            result.AppendLine($"{header.Key}: {header.Value}");
        }

        return result.ToString();
    }
}
