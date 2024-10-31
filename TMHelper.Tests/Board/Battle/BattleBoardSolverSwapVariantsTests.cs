using TMHelper.Common.Board.Actions;
using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapVariantsTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void Simple3InRowX2()
		{
			List<BoardGemSwapAction> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, R, G,
					_, _, _, G, G, R));

			AssertSwapsVariantsEqual(
				swaps,
				CreateSwap(6, 6, Up));
		}

		[Test]
		public void Simple3InRowSwapWithEmpty()
		{
			List<BoardGemSwapAction> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, G,
					_, _, _, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				CreateSwap(6, 4, Up));
		}

		[Test]
		public void Simple4InRow()
		{
			List<BoardGemSwapAction> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, _, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				CreateSwap(6, 4, Up));
		}

		[Test]
		public void Simple5InRow()
		{
			List<BoardGemSwapAction> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, R, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				CreateSwap(6, 4, Up));
		}

		[Test]
		public void Simple5InRowAnd3InRow()
		{
			List<BoardGemSwapAction> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, B,
					_, _, _, R, G, G,
					_, R, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				CreateSwap(6, 4, Up),
				CreateSwap(6, 4, Right));
		}

		#region Assertion Utils

		private void AssertSwapsVariantsEqual(
			List<BoardGemSwapAction> actual,
			params BoardGemSwapAction[] expected)
		{
			foreach (BoardGemSwapAction expectedAction in expected)
			{
				Assert.That(actual, Does.Contain(expectedAction));
			}
		}

		#endregion Assertion Utils
	}
}
