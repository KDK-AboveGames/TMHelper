using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.Logging;
using TMHelper.BoardParsing.OpenCV;

namespace TMHelper.Host.Console
{
	internal class LaunchOptions
	{
		internal enum BoardTypes { Battle, BoxOfSages }

		[Option(
			"board",
			Required = true,
			HelpText = "Вид доски для распознавания")]
		public BoardTypes? Board { get; set; }


		[Option(
			"opencv",
			Required = true,
			HelpText = "Способ распознавания доски с помощью OpenCV",
			SetName = nameof(OpenCV))]
		public bool OpenCV { get; set; }

		[Option(
			"opencv-boardstyle",
			Default = BoardVisualStyles.Classic,
			Required = false,
			HelpText = "Визуальный стиль доски",
			SetName = nameof(OpenCV))]
		public BoardVisualStyles OpenCV_BoardStyle { get; set; }

		[Option(
			"opencv-startx",
			Required = true,
			HelpText = "Координата X (по горизонтали) левого верхнего угла доски",
			SetName = nameof(OpenCV))]
		public int OpenCV_LeftTopBoardX { get; set; }

		[Option(
			"opencv-starty",
			Required = true,
			HelpText = "Координата Y (по вертикали) левого верхнего угла доски",
			SetName = nameof(OpenCV))]
		public int OpenCV_LeftTopBoardY { get; set; }

		[Option(
			"opencv-endx",
			Required = true,
			HelpText = "Координата X (по горизонтали) правого нижнего угла доски",
			SetName = nameof(OpenCV))]
		public int OpenCV_RightBottomBoardX { get; set; }

		[Option(
			"opencv-endy",
			Required = true,
			HelpText = "Координата Y (по вертикали) правого нижнего угла доски",
			SetName = nameof(OpenCV))]
		public int OpenCV_RightBottomBoardY { get; set; }


		[Option(
			"opencv-savetest",
			Default = false,
			Required = false,
			HelpText = "Сохранять ли скриншот и результаты парсинга в отдельную папку для будущего возможного использования в тестах",
			SetName = nameof(OpenCV))]
		public bool OpenCV_SaveTestData { get; set; }

		[Option(
			"opencv-debug",
			Default = false,
			Required = false,
			HelpText = "Открывать ли картинку с отладочной информацией после попытки парсинга",
			SetName = nameof(OpenCV))]
		public bool OpenCV_Debug { get; set; }


		[Option(
			"log-level",
			Default = LogLevel.Warning,
			Required = false,
			HelpText = "Минимальный уровень логирования")]
		public LogLevel LogLevel { get; set; }


		[Usage(ApplicationAlias = "TMHelper.Host.Console.exe")]
		public static IEnumerable<Example> Examples
		{
			get
			{
				yield return new Example(
					"Распознавание обычной доски",
					new LaunchOptions
					{
						Board = BoardTypes.Battle,

						OpenCV = true,
						OpenCV_BoardStyle = BoardVisualStyles.Autumn,

						OpenCV_LeftTopBoardX = 424,
						OpenCV_LeftTopBoardY = 171,
						OpenCV_RightBottomBoardX = 854,
						OpenCV_RightBottomBoardY = 599,
					});

				yield return new Example(
					"Распознавание доски \"Шкатулка Мудрецов\"",
					new LaunchOptions
					{
						Board = BoardTypes.BoxOfSages,

						OpenCV = true,
						OpenCV_BoardStyle = BoardVisualStyles.Autumn,

						OpenCV_LeftTopBoardX = 283,
						OpenCV_LeftTopBoardY = 75,
						OpenCV_RightBottomBoardX = 1002,
						OpenCV_RightBottomBoardY = 769,
					});
			}
		}
	}
}
