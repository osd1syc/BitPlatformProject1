﻿namespace Project1.Client.Core.Services.DiagnosticLog;

public partial class DiagnosticLogger : ILogger, IDisposable
{
    public static ConcurrentQueue<DiagnosticLog> Store { get; } = [];

    private IDictionary<string, object?>? currentState;

    public string? Category { get; set; }

    public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
    {
        if (state is IDictionary<string, object?> data)
        {
            currentState = data;
        }

        return this;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel) is false) return;

        var message = formatter(state, exception);

        if (Store.Count >= 1_000)
        {
            Store.TryDequeue(out var _);
        }

        Store.Enqueue(new()
        {
            CreatedOn = DateTimeOffset.Now,
            Level = logLevel,
            Message = message,
            Exception = exception,
            Category = Category,
            State = currentState?.ToDictionary(i => i.Key, i => i.Value?.ToString())
        });
    }

    public void Dispose()
    {

    }
}
