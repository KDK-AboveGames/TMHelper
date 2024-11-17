using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TMHelper.Common.Board.BoxOfSages;

namespace TMHelper.BoardParsing.OpenCV.Tests.BoxOfSages
{
	internal abstract class BoxOfSagesBoardParsingTestsBase : BoardParsingTestsBase<BoxOfSagesBoardState>
	{
		private IBoxOfSagesBoardStateProvider? Parser;

		protected override void SetupInternal()
		{
			base.SetupInternal();

			Parser = new OpenCVBoardStateParser(
				new OpenCVBoardStateParserOptions(ParserBoardStyle, ParserSaveTestData, ParserDebug),
				ImageProvider,
				NullLoggerFactory.Instance.CreateLogger<OpenCVBoardStateParser>());
		}

		protected BoxOfSagesBoardState Parse(byte[] imageFromResources)
		{
			ImageProvider.PrepareBoardStateImagePng(imageFromResources);
			return Parser!.GetBoardState();
		}
	}
}
