using TMHelper.Common.Board;
using TMHelper.Common.Board.Actions;

namespace TMHelper.Tests.Board
{
	public abstract class BoardSolverTestsBase
	{
		#region Data Generation Utils

		protected BoardGemSwapAction CreateSwap(int row, int column, BoardGemSwapDirections direction)
		{
			return new BoardGemSwapAction(
				new BoardCoords(row, column),
				direction);
		}

		#endregion Data Generation Utils

		#region Assertion Utils

		protected void AssertBoardStatesEqual(BoardState actual, BoardState expected, string? message = null)
		{
			Assert.Multiple(() =>
			{
				Assert.That(actual.Rows, Is.EqualTo(expected.Rows), ConcatMessages(nameof(expected.Rows), message));
				Assert.That(actual.Columns, Is.EqualTo(expected.Columns), ConcatMessages(nameof(expected.Columns), message));
			});

			Assert.Multiple(() =>
			{
				for (int i = 1; i <= expected.Rows; i++)
				{
					for (int j = 1; j <= expected.Columns; j++)
					{
						Assert.That(actual[i, j], Is.EqualTo(expected[i, j]), ConcatMessages($"Wrong gem at [{i},{j}]", message));
					}
				}
			});
		}

		#endregion Assertion Utils
	}
}
