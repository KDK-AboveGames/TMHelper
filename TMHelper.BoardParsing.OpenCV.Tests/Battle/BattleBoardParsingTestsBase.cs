using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TMHelper.Common.Board.Battle;

namespace TMHelper.BoardParsing.OpenCV.Tests.Battle
{
	internal abstract class BattleBoardParsingTestsBase : BoardParsingTestsBase<BattleBoardState>
	{
		private IBattleBoardStateProvider? Parser;

		protected override void SetupInternal()
		{
			base.SetupInternal();

			Parser = new OpenCVBoardStateParser(
				new OpenCVBoardStateParserOptions(ParserBoardStyle, ParserSaveTestData, ParserDebug),
				ImageProvider,
				NullLoggerFactory.Instance.CreateLogger<OpenCVBoardStateParser>());
		}

		protected BattleBoardState Parse(byte[] imageFromResources)
		{
			ImageProvider.PrepareBoardStateImagePng(imageFromResources);
			return Parser!.GetBoardState();
		}
	}
}
