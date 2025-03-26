﻿using Project1.Client.Core.Services.HttpMessageHandlers;

namespace Project1.Client.Core.Components;

public class Parameters
{
    public const string CurrentDir = nameof(CurrentDir);
    public const string CurrentTheme = nameof(CurrentTheme);
    public const string CurrentRouteData = nameof(CurrentRouteData);

    /// <summary>
    /// Indicates the connection status, with default behavior tied to the SignalR connection status.
    /// <see cref="ExceptionDelegatingHandler"/> allows this value to be updated based on server responses:
    /// - When the first response is received from the server, this value becomes true (Online).
    /// - When a <see cref="ServerConnectionException"/> occurs, it becomes false (Offline).
    /// By default, this value is null (Unknown).
    /// </summary>
    public const string IsOnline = nameof(IsOnline);
}
