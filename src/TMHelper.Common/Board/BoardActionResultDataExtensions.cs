namespace TMHelper.Common.Board
{
	public static class BoardActionResultDataExtensions
	{
		public static bool GetValueBoolSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardActionResultDataKeys key,
			bool defaultValue = false)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data.TryGetValue(key, out string? valueStr)
					&& !string.IsNullOrEmpty(valueStr)
					&& bool.TryParse(valueStr, out bool value)
				? value
				: defaultValue;
		}

		public static int GetValueIntSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardActionResultDataKeys key,
			int defaultValue = 0)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data.TryGetValue(key, out string? valueStr)
					&& !string.IsNullOrEmpty(valueStr)
					&& int.TryParse(valueStr, out int value)
				? value
				: defaultValue;
		}

		public static string? GetValueStringSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardActionResultDataKeys key,
			string? defaultValue = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data.TryGetValue(key, out string? value)
				? value
				: defaultValue;
		}


		public static void SetValueSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardActionResultDataKeys key,
			object value)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (!data.ContainsKey(key))
			{
				if (value is not null)
				{
					data.Add(key, value.ToString());
				}
			}
			else
			{
				if (value is not null)
				{
					data[key] = value.ToString();
				}
				else
				{
					data.Remove(key);
				}
			}
		}

		public static void AppendValueSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardActionResultDataKeys key,
			int value)
		{
			data.SetValueSafe(
				key,
				data.GetValueIntSafe(key) + value);
		}

		public static void AppendValuesGemLineCollectedSafe(
			this Dictionary<BoardActionResultDataKeys, string> data,
			BoardCollapseResult.GemsLine gemsLine)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			BoardActionResultDataKeys gemsTypeKey = gemsLine.GemBaseType switch
			{
				BoardGems.Red => BoardActionResultDataKeys.RedGemsCollectedTotal,
				BoardGems.Green => BoardActionResultDataKeys.GreenGemsCollectedTotal,
				BoardGems.Blue => BoardActionResultDataKeys.BlueGemsCollectedTotal,
				BoardGems.Skull => BoardActionResultDataKeys.SkullGemsCollectedTotal,
				BoardGems.Yellow => BoardActionResultDataKeys.YellowGemsCollectedTotal,
				BoardGems.Maroon => BoardActionResultDataKeys.MaroonGemsCollectedTotal,
				BoardGems.Purple => BoardActionResultDataKeys.PurpleGemsCollectedTotal,
				_ => throw new NotSupportedException("Unknown gem type: " + gemsLine.GemBaseType)
			};

			data.AppendValueSafe(gemsTypeKey, gemsLine.GemsTotalCount);
		}

		/// <summary>
		/// Получает сумму всех собранных камней.
		/// Черепа не учитываются.
		/// </summary>
		public static int GetGemsCollectedTotalCountSafe(
			this Dictionary<BoardActionResultDataKeys, string> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			return data.GetValueIntSafe(BoardActionResultDataKeys.RedGemsCollectedTotal)
				+ data.GetValueIntSafe(BoardActionResultDataKeys.GreenGemsCollectedTotal)
				+ data.GetValueIntSafe(BoardActionResultDataKeys.BlueGemsCollectedTotal)
				+ data.GetValueIntSafe(BoardActionResultDataKeys.YellowGemsCollectedTotal)
				+ data.GetValueIntSafe(BoardActionResultDataKeys.MaroonGemsCollectedTotal)
				+ data.GetValueIntSafe(BoardActionResultDataKeys.PurpleGemsCollectedTotal);
		}
	}
}
