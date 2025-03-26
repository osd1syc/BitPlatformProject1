﻿
namespace Project1.Client.Maui;

public partial class MainPage
{
    public MainPage(ClientMauiSettings clientMauiSettings)
    {
        InitializeComponent();
        AppWebView.RootComponents.Insert(0, new()
        {
            ComponentType = typeof(BlazorApplicationInsights.ApplicationInsightsInit),
            // The App Insights JS SDK is already included in index.html. Use `IsWasmStandalone` to prevent reloading scripts.
            Parameters = new Dictionary<string, object?> { { nameof(BlazorApplicationInsights.ApplicationInsightsInit.IsWasmStandalone), true } },
            Selector = "head::after"
        });
    }
}
