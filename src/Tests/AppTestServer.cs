﻿using System.Net.Sockets;
using System.Net;
using Project1.Server.Api;
using Project1.Server.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Project1.Tests;

public partial class AppTestServer : IAsyncDisposable
{
    private WebApplication? webApp;

    public WebApplication WebApp => webApp ?? throw new InvalidOperationException($"{nameof(WebApp)} is null. Call {nameof(Build)} method first.");
    public readonly Uri WebAppServerAddress = new(GenerateServerUrl());

    public AppTestServer Build(Action<IServiceCollection>? configureTestServices = null,
        Action<ConfigurationManager>? configureTestConfigurations = null)
    {
        if (webApp != null)
            throw new InvalidOperationException("Server is already built.");

        var builder = WebApplication.CreateBuilder(options: new()
        {
            EnvironmentName = Environments.Development,
            ApplicationName = typeof(Server.Web.Program).Assembly.GetName().Name
        });

        builder.Configuration["ServerAddress"] = WebAppServerAddress.ToString();
        builder.WebHost.UseUrls(WebAppServerAddress.ToString());

        AppEnvironment.Set(builder.Environment.EnvironmentName);

        builder.Configuration.AddClientConfigurations(clientEntryAssemblyName: "Project1.Client.Web");


        configureTestConfigurations?.Invoke(builder.Configuration);

        builder.AddTestProjectServices();

        configureTestServices?.Invoke(builder.Services);

        var app = webApp = builder.Build();

        app.ConfigureMiddlewares();

        return this;
    }

    public async Task Start()
    {
        await WebApp.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (webApp != null)
        {
            await webApp.StopAsync();
            await webApp.DisposeAsync();
        }
    }

    private static string GenerateServerUrl()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return $"http://127.0.0.1:{port}/";
    }
}
