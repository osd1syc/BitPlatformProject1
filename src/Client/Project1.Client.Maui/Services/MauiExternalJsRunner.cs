﻿using EmbedIO.WebSockets;
using Microsoft.JSInterop;

namespace Project1.Client.Maui.Services;

/// <summary>
/// Checkout external-js-runner.html
/// </summary>
public class MauiExternalJsRunner : WebSocketModule
{
    public static Func<JsonDocument, Task<JsonDocument>>? RequestToBeSent { get; private set; }

    TaskCompletionSource<JsonDocument>? responseTcs;
    TaskCompletionSource<IWebSocketContext> webSocketTcs = new();

    public MauiExternalJsRunner()
        : base("/external-js-runner", true)
    {
        RequestToBeSent += SendRequest;
    }

    private async Task<JsonDocument> SendRequest(JsonDocument document)
    {
        responseTcs = new();
        var webSocket = await webSocketTcs.Task;
        await SendAsync(webSocket, document.RootElement.GetRawText());
        return await responseTcs.Task;
    }

    protected override async Task OnMessageReceivedAsync(IWebSocketContext context, byte[] buffer, IWebSocketReceiveResult result)
    {
        var response = JsonSerializer.Deserialize<ExternalJsRunnerResponse>(buffer, JsonSerializerOptions.Web)!;
        if (string.IsNullOrEmpty(response.Error) is false)
        {
            responseTcs!.SetException(new JSException(response.Error));
        }
        else
        {
            responseTcs!.SetResult(response.Body!);
        }
    }

    protected override async Task OnClientConnectedAsync(IWebSocketContext context)
    {
        await base.OnClientConnectedAsync(context);
        webSocketTcs.SetResult(context);
    }

    protected override async Task OnClientDisconnectedAsync(IWebSocketContext context)
    {
        webSocketTcs.TrySetCanceled();
        webSocketTcs = new();
        await base.OnClientDisconnectedAsync(context);
    }

    protected override void Dispose(bool disposing)
    {
        RequestToBeSent -= SendRequest;
        base.Dispose(disposing);
    }
}

public class ExternalJsRunnerResponse
{
    public JsonDocument? Body { get; set; }

    public string? Error { get; set; }
}
