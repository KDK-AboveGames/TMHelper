using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapVariantsTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void Simple3InRowX2()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, R, G,
					_, _, _, G, G, R));

			AssertSwapsVariantsEqual(
				swaps,
				new[] { new BoardGemSwap(6, 6, Up) });
		}

		[Test]
		public void Simple3InRowSwapWithEmpty()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, G,
					_, _, _, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				new[] { new BoardGemSwap(6, 4, Up) });
		}

		[Test]
		public void Simple4InRow()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, _, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				new[] { new BoardGemSwap(6, 4, Up) });
		}

		[Test]
		public void Simple5InRow()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, R, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				new[] { new BoardGemSwap(6, 4, Up) });
		}

		[Test]
		public void Simple5InRowAnd3InRow()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, B,
					_, _, _, R, G, G,
					_, R, R, G, R, R));

			AssertSwapsVariantsEqual(
				swaps,
				new[]
				{
					new BoardGemSwap(6, 4, Up),
					new BoardGemSwap(6, 4, Right),
				});
		}
	}
}
