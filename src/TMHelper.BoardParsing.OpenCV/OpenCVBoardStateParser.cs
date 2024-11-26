using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.Extensions.Logging;
using TMHelper.BoardParsing.OpenCV.Properties;
using TMHelper.Common.Board;
using TMHelper.Common.Board.Battle;
using TMHelper.Common.Board.BoxOfSages;

namespace TMHelper.BoardParsing.OpenCV
{
	public class OpenCVBoardStateParser : IBattleBoardStateProvider, IBoxOfSagesBoardStateProvider
	{
		private const string TestDataDirectoryName = "_TestData";
		private readonly string? TestDataDirectory;

		private const string DebugDataDirectoryName = "_DebugData";
		private readonly string? DebugDataDirectory;

		private readonly OpenCVBoardStateParserOptions Options;
		private readonly IOpenCVBoardStateImageProvider ImageProvider;

		private readonly ILogger Logger;

		public OpenCVBoardStateParser(
			OpenCVBoardStateParserOptions options,
			IOpenCVBoardStateImageProvider imageProvider,
			ILogger logger)
		{
			Options = options
				?? throw new ArgumentNullException(nameof(options));

			if (!SupportedBoardStyles.Contains(options.BoardStyle))
			{
				throw new NotSupportedException($"Board style {options.BoardStyle} is not supported.");
			}

			ImageProvider = imageProvider
				?? throw new ArgumentNullException(nameof(imageProvider));

			Logger = logger
				?? throw new ArgumentNullException(nameof(logger));

			string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

			if (options.SaveTestData)
			{
				TestDataDirectory = Path.Combine(assemblyLocation, TestDataDirectoryName);
				try
				{
					if (!Directory.Exists(TestDataDirectory))
					{
						Directory.CreateDirectory(TestDataDirectory);
					}
				}
				catch (Exception ex)
				{
					// Логируем, а не пробрасываем исключение, потому что основная функциональность важнее
					Logger.LogError($"Не удалось создать директорию [{TestDataDirectory}] для сохранения тестовых данных. {ex.Message}");
				}
			}

			if (options.Debug)
			{
				DebugDataDirectory = Path.Combine(assemblyLocation, DebugDataDirectoryName);
				try
				{
					if (!Directory.Exists(DebugDataDirectory))
					{
						Directory.CreateDirectory(DebugDataDirectory);
					}
				}
				catch (Exception ex)
				{
					// Логируем, а не пробрасываем исключение, потому что основная функциональность важнее
					Logger.LogError($"Не удалось создать директорию [{DebugDataDirectory}] для отладочных изображений. {ex.Message}");
				}
			}
		}

		#region Resources Initialization

		private readonly struct GemInfo
		{
			public readonly double TemplateRecognitionThreshold = 0.85;
			public readonly Mat TemplateImage;
			public readonly BoardGems Gem;
			public readonly Color DebugColor;

			public GemInfo(byte[] gemImageFromResources, BoardGems gem, Color debugColor)
			{
				TemplateImage = GetMatFromBitmap(gemImageFromResources);
				Gem = gem;
				DebugColor = debugColor;
			}
		}

		private readonly struct GemModifierInfo
		{
			public readonly double TemplateRecognitionThreshold = 0.9;
			public readonly Mat TemplateImage;
			public readonly int GemCountModifierValue;

			public GemModifierInfo(byte[] gemModifierImageFromResources, int gemCountModifierValue = 1)
			{
				TemplateImage = GetMatFromBitmap(gemModifierImageFromResources);
				GemCountModifierValue = gemCountModifierValue;
			}

			public BoardGems GetModifiedGem(BoardGems initialGem)
			{
				if (GemCountModifierValue > 0)
				{
					return initialGem.GetGemWithCountValue(GemCountModifierValue);
				}

				throw new NotSupportedException(); // todo: здесь должны позже применяться модификаторы "взрыв по линии" и т.п.
			}

			public override string ToString()
			{
				return $"x{GemCountModifierValue}";
			}
		}

		private readonly struct BoardInfo
		{
			public readonly List<GemInfo> GemsParsingInfo;
			public readonly List<GemModifierInfo> GemModifiersParsingInfo;

			public BoardInfo(List<GemInfo> gemsParsingInfo, List<GemModifierInfo> gemModifiersParsingInfo)
			{
				GemsParsingInfo = gemsParsingInfo;
				GemModifiersParsingInfo = gemModifiersParsingInfo;
			}
		}

		private static readonly List<BoardVisualStyles> SupportedBoardStyles;

		private static readonly Dictionary<BoardVisualStyles, BoardInfo> BattleBoardInfo;
		private static readonly Dictionary<BoardVisualStyles, BoardInfo> BoxOfSagesBoardInfo;

		static OpenCVBoardStateParser()
		{
			SupportedBoardStyles = new List<BoardVisualStyles>
			{
				BoardVisualStyles.Autumn
			};


			List<GemModifierInfo> gemModifiersBattle = new()
			{
				new GemModifierInfo(Resources.Autumn_Battle_2_1, 2),
				new GemModifierInfo(Resources.Autumn_Battle_2_2, 2),
				new GemModifierInfo(Resources.Autumn_Battle_2_3, 2),
				new GemModifierInfo(Resources.Autumn_Battle_2_4, 2),

				new GemModifierInfo(Resources.Autumn_Battle_3_1, 3),
				new GemModifierInfo(Resources.Autumn_Battle_3_2, 3),
				new GemModifierInfo(Resources.Autumn_Battle_3_3, 3),
				new GemModifierInfo(Resources.Autumn_Battle_3_4, 3),
				new GemModifierInfo(Resources.Autumn_Battle_3_5, 3),
				new GemModifierInfo(Resources.Autumn_Battle_3_6, 3),

				new GemModifierInfo(Resources.Autumn_Battle_5_1, 5),
				new GemModifierInfo(Resources.Autumn_Battle_5_2, 5),
				new GemModifierInfo(Resources.Autumn_Battle_5_3, 5),

				// (!)
				// при появлении новых визуальных стилей досок
				// шаблоны модификаторов добавлять в этот общий список
			};

			BattleBoardInfo = new Dictionary<BoardVisualStyles, BoardInfo>
			{
				{
					BoardVisualStyles.Autumn,
					new BoardInfo(
						new List<GemInfo>
						{
							new GemInfo(Resources.Autumn_Battle_Red, BoardGems.R, Color.Red),
							new GemInfo(Resources.Autumn_Battle_Green, BoardGems.G, Color.LightGreen),
							new GemInfo(Resources.Autumn_Battle_Blue, BoardGems.B, Color.Cyan),
							new GemInfo(Resources.Autumn_Battle_Skull, BoardGems.S, Color.White),
						},
						gemModifiersBattle)
				}
			};


			List<GemModifierInfo> gemModifiersBoxOfSages = new()
			{
				new GemModifierInfo(Resources.Classic_BoxOfSages_2_1, 2),
				new GemModifierInfo(Resources.Classic_BoxOfSages_2_2, 2),
				new GemModifierInfo(Resources.Classic_BoxOfSages_2_3, 2),

				new GemModifierInfo(Resources.Classic_BoxOfSages_3_1, 3),
				new GemModifierInfo(Resources.Classic_BoxOfSages_3_2, 3),
				new GemModifierInfo(Resources.Classic_BoxOfSages_3_3, 3),

				// (!)
				// при появлении новых визуальных стилей досок
				// шаблоны модификаторов добавлять в этот общий список
			};

			BoardInfo classicBoxOfSagesBoardInfo = new(
				new List<GemInfo>
				{
					new GemInfo(Resources.Classic_BoxOfSages_Red, BoardGems.R, Color.OrangeRed),
					new GemInfo(Resources.Classic_BoxOfSages_Green, BoardGems.G, Color.LightGreen),
					new GemInfo(Resources.Classic_BoxOfSages_Blue, BoardGems.B, Color.Cyan),
					new GemInfo(Resources.Classic_BoxOfSages_Yellow, BoardGems.Y, Color.Yellow),
					new GemInfo(Resources.Classic_BoxOfSages_Maroon, BoardGems.M, Color.Crimson),
					new GemInfo(Resources.Classic_BoxOfSages_Purple, BoardGems.P, Color.Magenta),
				},
				gemModifiersBoxOfSages);

			BoxOfSagesBoardInfo = new Dictionary<BoardVisualStyles, BoardInfo>
			{
				{ BoardVisualStyles.Classic, classicBoxOfSagesBoardInfo },
				{ BoardVisualStyles.Autumn, classicBoxOfSagesBoardInfo },
			};
		}

		#endregion Resources Initialization

		BattleBoardState IBattleBoardStateProvider.GetBoardState()
		{
			return RecognizeBoardState<BattleBoardState>(BattleBoardInfo[Options.BoardStyle]);
		}

		BoxOfSagesBoardState IBoxOfSagesBoardStateProvider.GetBoardState()
		{
			return RecognizeBoardState<BoxOfSagesBoardState>(BoxOfSagesBoardInfo[Options.BoardStyle]);
		}

		private TBoardState RecognizeBoardState<TBoardState>(BoardInfo templatesInfo)
			where TBoardState : BoardState, new()
		{
			byte[] boardImagePng = ImageProvider.GetBoardStateImagePng();

			Mat boardImage = new();
			CvInvoke.Imdecode(boardImagePng, ImreadModes.Color, boardImage);
			Mat targetImage = boardImage.Clone();

			TBoardState boardState = new();

			float cellWidth = (float)boardImage.Width / boardState.Columns;
			float cellHeight = (float)boardImage.Height / boardState.Rows;

			foreach (GemInfo templateInfo in templatesInfo.GemsParsingInfo)
			{
				foreach (Point foundPoint in RecognizeTemplate(boardImage, templateInfo.TemplateImage, templateInfo.TemplateRecognitionThreshold))
				{
					int row = 1 + (int)Math.Floor(foundPoint.Y / cellHeight);
					int column = 1 + (int)Math.Floor(foundPoint.X / cellWidth);

					if (Options.Debug)
					{
						if (!boardState[row, column].IsSameTypeAs(BoardGems.Empty)
							&& boardState[row, column] != templateInfo.Gem)
						{
							Logger.LogWarning($"Множественное распознавание на [{row},{column}]: {boardState[row, column]} != {templateInfo.Gem}");
						}

						MarkTemplate(
							targetImage,
							templateInfo.TemplateImage,
							new Point(
								(int)((column - 1) * cellWidth),
								(int)((row - 1) * cellHeight)),
							templateInfo.DebugColor);
					}

					boardState[row, column] = templateInfo.Gem;
				}
			}

			foreach (GemModifierInfo templateInfo in templatesInfo.GemModifiersParsingInfo)
			{
				foreach (Point foundPoint in RecognizeTemplate(boardImage, templateInfo.TemplateImage, templateInfo.TemplateRecognitionThreshold))
				{
					int row = 1 + (int)Math.Floor(foundPoint.Y / cellHeight);
					int column = 1 + (int)Math.Floor(foundPoint.X / cellWidth);

					BoardGems gemToModify = boardState[row, column];

					BoardGems modifiedGem;
					if (gemToModify.IsSameTypeAs(BoardGems.Empty)) // произошла ошибка распознавания модификатора
					{
						if (Options.Debug)
						{
							Logger.LogError($"Распознавание модификатора [{templateInfo}] на пустой клетке [{row},{column}]");
						}

						modifiedGem = gemToModify;
					}
					else
					{
						modifiedGem = templateInfo.GetModifiedGem(gemToModify);
					}

					if (Options.Debug)
					{
						if (!gemToModify.IsBaseType() && gemToModify != modifiedGem)
						{
							Logger.LogWarning($"Множественное распознавание модификатора [{templateInfo}] на [{row},{column}]: {gemToModify} != {modifiedGem}");
						}

						MarkTemplate(
							targetImage,
							templateInfo.TemplateImage,
							foundPoint,
							Color.White,
							templateInfo.GemCountModifierValue);
					}

					boardState[row, column] = modifiedGem;
				}
			}

			string resultName = DateTime.Now.ToString("yy.MM.dd_hh.mm.ss.fff");

			if (Options.Debug)
			{
				string debugImagePath = Path.Combine(DebugDataDirectory!, resultName + ".jpg");
				try
				{
					targetImage.Save(debugImagePath);
					Logger.LogInformation("Изображение с отладочной информацией сохранено в {debugImagePath}", debugImagePath);

					if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					{
						using Process fileopener = new();
						fileopener.StartInfo.FileName = "explorer";
						fileopener.StartInfo.Arguments = "\"" + debugImagePath + "\"";
						fileopener.Start();
					}
				}
				catch (Exception ex)
				{
					// Логируем, а не пробрасываем исключение, потому что основная функциональность важнее
					Logger.LogError($"Не удалось сохранить отладочной изображение в [{debugImagePath}]. {ex.Message}");
				}
			}

			if (boardState.IsEmpty)
			{
				Logger.LogError("Не удалось распознать доску");
			}
			else if (Options.SaveTestData)
			{
				string resultImageFilePath = Path.Combine(TestDataDirectory!, resultName + ".png");
				try
				{
					File.WriteAllBytes(resultImageFilePath, boardImagePng);
					Logger.LogInformation("Изображение для тестов сохранено в {resultImageFilePath}", resultImageFilePath);
				}
				catch (Exception ex)
				{
					// Логируем, а не пробрасываем исключение, потому что основная функциональность важнее
					Logger.LogError($"Не удалось сохранить данные для тестов в [{resultImageFilePath}]. {ex.Message}");
				}

				string resultStateFilePath = Path.Combine(TestDataDirectory!, resultName + ".txt");
				try
				{
					File.WriteAllText(resultStateFilePath, boardState.ToString());
					Logger.LogInformation("Текст с состоянием для тестов сохранен в {resultStateFilePath}", resultStateFilePath);
				}
				catch (Exception ex)
				{
					// Логируем, а не пробрасываем исключение, потому что основная функциональность важнее
					Logger.LogError($"Не удалось сохранить данные для тестов в [{resultStateFilePath}]. {ex.Message}");
				}
			}

			return boardState;
		}

		private Point[] RecognizeTemplate(
			Mat sourceImage,
			Mat templateImage,
			double threshold)
		{
			Mat result = new();
			CvInvoke.MatchTemplate(sourceImage, templateImage, result, TemplateMatchingType.CcoeffNormed);

			Mat thresholdedResult = new();
			CvInvoke.Threshold(result, thresholdedResult, threshold, 1.0, ThresholdType.Binary);

			using VectorOfPoint locations = new();
			CvInvoke.FindNonZero(thresholdedResult, locations);
			return locations.ToArray();
		}

		private void MarkTemplate(
			Mat targetImage,
			Mat templateImage,
			Point markPosition,
			Color markColor,
			int markThickness = 1)
		{
			MCvScalar color = new(markColor.B, markColor.G, markColor.R);

			int x = markPosition.X;
			int y = markPosition.Y;

			for (int i = 0; i < markThickness; i++)
			{
				int shift = i * 3;

				Rectangle rect = new(
					new Point(x - shift, y - shift),
					new Size(templateImage.Width + shift * 2, templateImage.Height + shift * 2));

				CvInvoke.Rectangle(targetImage, rect, color, 1);
			}
		}

		#region Utils

		private static Mat GetMatFromBitmap(byte[] imageFromResources)
		{
			Mat image = new();
			CvInvoke.Imdecode(imageFromResources, ImreadModes.Color, image);
			return image;
		}

		#endregion Utils
	}
}
