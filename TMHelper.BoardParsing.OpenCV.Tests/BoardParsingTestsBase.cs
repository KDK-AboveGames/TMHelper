using TMHelper.Common.Board;
using TMHelper.Tests.Board;

namespace TMHelper.BoardParsing.OpenCV.Tests
{
	internal abstract class BoardParsingTestsBase<TBoardState> : BoardTestsBase<TBoardState>
		where TBoardState : BoardState, new()
	{
		protected abstract BoardVisualStyles ParserBoardStyle { get; }

		/// <summary>
		/// ≈сли true, то сохран€ет состо€ние распознанной доски в файл.
		/// ”добно дл€ генерации и заполнени€ тест-кейсов.
		/// </summary>
		protected abstract bool ParserSaveTestData { get; }

		/// <summary>
		/// ≈сли true, то открывает отладочную картинку после выполнени€ теста.
		/// ”добно дл€ отладки тест-кейсов.
		/// </summary>
		protected abstract bool ParserDebug { get; }

		protected TestBoardStateImageProvider ImageProvider { get; private set; }

		[SetUp]
		public void Setup()
		{
			SetupInternal();
		}

		protected virtual void SetupInternal()
		{
			ImageProvider = new TestBoardStateImageProvider();
		}
	}
}
