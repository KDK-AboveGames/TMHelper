namespace TMHelper.Tests
{
	public static class GlobalUtils
	{
		public static string ConcatMessages(params string?[] messages)
		{
			return string.Join("; ", messages);
		}
	}
}
