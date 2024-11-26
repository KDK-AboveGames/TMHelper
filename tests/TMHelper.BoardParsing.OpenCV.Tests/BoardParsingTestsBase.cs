using TMHelper.Common.Board;
using TMHelper.Tests.Board;

namespace TMHelper.BoardParsing.OpenCV.Tests
{
	internal abstract class BoardParsingTestsBase<TBoardState> : BoardTestsBase<TBoardState>
		where TBoardState : BoardState, new()
	{
		protected abstract BoardVisualStyles ParserBoardStyle { get; }

		/// <summary>
		/// ���� true, �� ��������� ��������� ������������ ����� � ����.
		/// ������ ��� ��������� � ���������� ����-������.
		/// </summary>
		protected abstract bool ParserSaveTestData { get; }

		/// <summary>
		/// ���� true, �� ��������� ���������� �������� ����� ���������� �����.
		/// ������ ��� ������� ����-������.
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
