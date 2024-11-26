using CommandLine;
using Microsoft.Extensions.Logging;
using TMHelper.BoardParsing.OpenCV;
using TMHelper.Common.Board.Battle;
using TMHelper.Common.Board.BoxOfSages;
using TMHelper.Host.Console.OpenCVIntegration;

namespace TMHelper.Host.Console
{
	internal class Program
	{
		private static int Main(string[] args)
		{
			return CommandLine.Parser.Default.ParseArguments<LaunchOptions>(args)
				.MapResult(
					(LaunchOptions options) =>
					{
						try
						{
							using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
							{
								builder
									.AddConsole()
									.SetMinimumLevel(options.LogLevel);
							});

							if (options.OpenCV)
							{
								OpenCVBoardStateParser parser = new(
									new OpenCVBoardStateParserOptions(
										options.OpenCV_BoardStyle,
										options.OpenCV_SaveTestData,
										options.OpenCV_Debug),
									new ScreenshotMaker(
										options.OpenCV_LeftTopBoardX,
										options.OpenCV_LeftTopBoardY,
										options.OpenCV_RightBottomBoardX - options.OpenCV_LeftTopBoardX,
										options.OpenCV_RightBottomBoardY - options.OpenCV_LeftTopBoardY),
									loggerFactory.CreateLogger<OpenCVBoardStateParser>());

								System.Console.WriteLine("ESC - выход");
								System.Console.WriteLine("Любая другая клавиша - начать распознавание доски");

								while (true)
								{
									ConsoleKeyInfo key = System.Console.ReadKey(true);
									if (key.Key == ConsoleKey.Escape)
									{
										return 0;
									}

									System.Console.Clear();

									try
									{
										switch (options.Board)
										{
											case LaunchOptions.BoardTypes.Battle:
												BoardStateHelper.PrintSuggestions(((IBattleBoardStateProvider)parser).GetBoardState());
												break;

											case LaunchOptions.BoardTypes.BoxOfSages:
												BoardStateHelper.PrintSuggestions(((IBoxOfSagesBoardStateProvider)parser).GetBoardState());
												break;

											default:
												throw new NotSupportedException("Вариант доски не поддерживается.");
										}
									}
									catch (Exception ex)
									{
										System.Console.WriteLine("Ошибка!");
										System.Console.WriteLine(ex.GetAllInnerExceptionMessage());
									}

									System.Console.WriteLine();
									System.Console.WriteLine();
									System.Console.WriteLine("ESC - выход");
									System.Console.WriteLine("Любая другая клавиша - начать распознавание новой доски");
								}
							}
							else
							{
								throw new NotSupportedException("Вариант парсера не поддерживается.");
							}
						}
						catch (Exception ex)
						{
							System.Console.WriteLine("Ошибка!");
							System.Console.WriteLine(ex.GetAllInnerExceptionMessage());

							System.Console.WriteLine();
							System.Console.WriteLine();
							System.Console.WriteLine("Для выхода нажмите любую клавишу");
							System.Console.ReadKey(true);
							return 2;
						}
					},
					_ =>
					{
						System.Console.WriteLine();
						System.Console.WriteLine();
						System.Console.WriteLine("Для выхода нажмите любую клавишу");
						System.Console.ReadKey(true);
						return 1;
					});
		}
	}
}
