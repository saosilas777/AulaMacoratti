
namespace APICatalogo.Logging
{
	public class CustomerLogger : ILogger
	{
		readonly string loggerName;
		readonly CustomLoggerProviderConfiguration loggerConfig;

		public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
		{
			loggerName = name;
			loggerConfig = config;
		}
		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel == loggerConfig.LogLevel;
		}
		public IDisposable? BeginScope<TState>(TState state)
		{
			return null;
		}



		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		{
			string message = $"{logLevel.ToString()}: {eventId} - {formatter(state, exception)}";

			FileWriter(message);

		}

		private void FileWriter(string message)
		{

			string filePath = @"C:\Users\saosi\Documents\Log\log.txt";
			using (StreamWriter streamWriter = new StreamWriter(filePath, true))
			{
				try
				{
					streamWriter.WriteLine(message);
					streamWriter.Close();
				}
				catch (Exception)
				{
					throw;
				}
			}
		}



	}
}

