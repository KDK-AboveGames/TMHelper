namespace TMHelper.Host.Console
{
	internal static class Utils
	{
		public static string GetAllInnerExceptionMessage(this Exception ex)
		{
			return string.Join(Environment.NewLine, GetAllInnerExceptionMessages(ex));
		}

		private static List<string> GetAllInnerExceptionMessages(Exception currentException)
		{
			List<string> result = new() { currentException.Message };

			while (currentException.InnerException != null)
			{
				result.Add(currentException.InnerException.Message);
				currentException = currentException.InnerException;
			}

			return result;
		}
	}
}
