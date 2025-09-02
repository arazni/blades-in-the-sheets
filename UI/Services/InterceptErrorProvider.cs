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
}

