﻿namespace Project1.Shared.Controllers.Diagnostics;

[Route("api/[controller]/[action]/")]
public interface IDiagnosticsController : IAppController
{
    [HttpPost("{?signalRConnectionId,pushNotificationSubscriptionDeviceId}")]
    Task<string> PerformDiagnostics(string? signalRConnectionId, string? pushNotificationSubscriptionDeviceId, CancellationToken cancellationToken);
}
