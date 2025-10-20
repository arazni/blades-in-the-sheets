using Microsoft.JSInterop;
using System.Text;

namespace UI.Services;

public sealed class InterceptErrorProvider() : ILoggerProvider
{
	public event Action<Exception>? Intercept;

	public ILogger CreateLogger(string _) => new InterceptErrorLog(this);
	public void Dispose() { }

	private sealed class InterceptErrorLog(InterceptErrorProvider errorProvider) : ILogger
	{
		public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;
		public bool IsEnabled(LogLevel logLevel) => logLevel > LogLevel.Warning;
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			if (!IsEnabled(logLevel) || exception?.StackTrace == null)
				return;

			errorProvider.Intercept?.Invoke(exception);
		}
	}

	public static async Task SetErrorMessage(IJSRuntime jSRuntime, Exception exception)
	{
		StringBuilder message = new(exception.Message);
		message.AppendLine(exception.StackTrace);
		var inner = exception.InnerException;

		while (inner != null)
		{
			message.AppendLine("INNER: ");
			message.AppendLine(inner.Message);
			message.AppendLine(inner.StackTrace);
			inner = inner.InnerException;
		}

		await jSRuntime.InvokeVoidAsync("bladesStackTrace", Encoding.UTF8.GetBytes(message.ToString()));
	}
}

