using static System.Console;

namespace TMHelper.Host.Console
{
	public class ConsoleWriter
	{
		public readonly struct ConsoleMessage
		{
			public readonly string Message;
			public readonly ConsoleColor Color;

			public ConsoleMessage(string message, ConsoleColor color = ConsoleColor.White)
			{
				Message = message;
				Color = color;
			}
		}

		public readonly struct ConsoleTableCell
		{
			public readonly List<ConsoleMessage> Messages;
			public readonly int Length;

			public ConsoleTableCell()
				: this(new List<ConsoleMessage>()) { }

			public ConsoleTableCell(params ConsoleMessage[] messages)
				: this(messages?.ToList() ?? new List<ConsoleMessage>()) { }

			public ConsoleTableCell(List<ConsoleMessage> messages)
			{
				ArgumentNullException.ThrowIfNull(messages);

				Messages = messages;
				Length = messages.Count > 0 ? messages.Sum(x => x.Message.Length) : 0;
			}

			public static implicit operator ConsoleTableCell(string message)
			{
				return new ConsoleTableCell(new ConsoleMessage(message));
			}
		}

		public static void WriteData(
			string[] headers,
			List<Dictionary<string, ConsoleTableCell>> dataWithHeaders,
			char columnDelimiter = '|',
			char headerDelimiter = '-',
			char? rowDelimiter = null)
		{
			ArgumentNullException.ThrowIfNull(headers);

			if (headers.Length == 0)
			{
				throw new ArgumentException("Headers is empty", nameof(headers));
			}

			string columnDelimiterStr = $" {columnDelimiter} ";

			WriteLine();

			if (dataWithHeaders == null || dataWithHeaders.Count == 0)
			{
				string headersLine = columnDelimiterStr + string.Join(columnDelimiterStr, headers) + columnDelimiterStr;
				WriteLine(headersLine);
				WriteLine(new string(headerDelimiter, headersLine.Length));
				WriteLine(columnDelimiterStr + "no data");
				return;
			}

			Dictionary<string, int> columnWidths = headers
				.ToDictionary(
					header => header,
					header => dataWithHeaders.Max(
						x => Math.Max(
							header.Length,
							x.TryGetValue(header, out ConsoleTableCell cell) ? cell.Length : 0)));


			Write(columnDelimiterStr);

			int rowWidth = columnDelimiterStr.Length;

			foreach (string header in headers)
			{
				int cellWidth = columnWidths[header];

				Write(header.PadLeft(cellWidth));
				Write(columnDelimiterStr);

				rowWidth += cellWidth + columnDelimiterStr.Length;
			}

			WriteLine();
			WriteLine(new string(headerDelimiter, rowWidth));

			foreach (Dictionary<string, ConsoleTableCell> dataRow in dataWithHeaders)
			{
				Write(columnDelimiterStr);

				foreach (string header in headers)
				{
					int cellWidth = columnWidths[header];

					if (dataRow.TryGetValue(header, out ConsoleTableCell cell))
					{
						if (cellWidth > cell.Length)
						{
							Write(new string(' ', cellWidth - cell.Length));
						}

						foreach (ConsoleMessage message in cell.Messages)
						{
							ForegroundColor = message.Color;

							Write(message.Message);

							ResetColor();
						}
					}
					else
					{
						Write(new string(' ', columnWidths[header]));
					}

					Write(columnDelimiterStr);
				}

				WriteLine();

				if (rowDelimiter.HasValue)
				{
					WriteLine(new string(rowDelimiter.Value, rowWidth));
				}
			}
		}
	}
}
