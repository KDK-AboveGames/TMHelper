using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.BoxOfSages
{
	public sealed class BoxOfSagesBoardSolverSwapVariantsTests : BoxOfSagesBoardSolverTestsBase
	{
		[Test]
		public void Complex1()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					P1, R3, G2, B2, R1, M2, G2, B1, R1, P1,
					M1, R2, Y1, P2, M1, M1, Y1, Y1, G3, G1,
					R3, B3, P1, B2, P2, G1, Y1, G1, M2, M2,
					Y1, M1, P1, P3, R2, B2, G3, B1, R2, P3,
					Y1, R1, Y1, P1, R1, P1, M2, M1, G1, M1,
					R1, P2, M1, G3, G1, R1, B2, Y3, B1, G2,
					B2, B1, G1, B3, G1, P2, M3, B1, R2, Y1,
					Y1, G1, R2, B1, Y1, R1, Y1, G3, M1, G1,
					Y1, P1, G1, G1, R1, R1, G1, G3, M2, P1,
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1));

			AssertSwapsVariantsEqual(
				swaps,
				new[]
				{
					new BoardGemSwap(10, 9, Right),
					new BoardGemSwap(9, 9, Down),
					new BoardGemSwap(10, 8, Right),
					new BoardGemSwap(5, 9, Right),
					new BoardGemSwap(6, 8, Down),
					new BoardGemSwap(9, 4, Down),
					new BoardGemSwap(6, 6, Down),
					new BoardGemSwap(6, 5, Right),
					new BoardGemSwap(2, 8, Down),
					new BoardGemSwap(3, 7, Down),
					new BoardGemSwap(6, 4, Down),
					new BoardGemSwap(7, 3, Right),
					new BoardGemSwap(8, 2, Right),
					new BoardGemSwap(8, 2, Down),
					new BoardGemSwap(6, 3, Down),
					new BoardGemSwap(3, 5, Down),
					new BoardGemSwap(5, 3, Right),
					new BoardGemSwap(3, 4, Right),
					new BoardGemSwap(3, 4, Down),
					new BoardGemSwap(2, 4, Down),
					new BoardGemSwap(3, 3, Right),
					new BoardGemSwap(2, 3, Right),
					new BoardGemSwap(3, 1, Right),
				});
		}

		[Test]
		public void Complex2()
		{
			List<BoardGemSwap> swaps = BoardSolver.GetAllPossibleSwaps(
				CreateBoardState(
					P1, G1, P3, M2, Y2, R1, M1, G1, P2, G1,
					M1, Y1, Y1, B1, R1, P1, P3, B1, R1, P1,
					B3, B1, P1, Y1, M1, R3, G2, B1, M2, M2,
					Y1, M1, P1, M1, B2, B1, Y1, G1, R2, P3,
					Y1, R1, Y1, R1, R2, G2, G3, B1, G1, M1,
					R1, P2, M1, R2, R1, M2, B2, Y3, B1, G2,
					B2, B1, G1, P3, G1, B2, M3, B1, R2, Y1,
					Y1, G1, R2, G1, Y1, P1, Y1, G3, M1, G1,
					Y1, P1, G1, G1, R1, P2, G1, G3, M2, P1,
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1));

			AssertSwapsVariantsEqual(
				swaps,
				new[]
				{
					new BoardGemSwap(10, 9, Right),
					new BoardGemSwap(9, 9, Down),
					new BoardGemSwap(10, 8, Right),
					new BoardGemSwap(6, 8, Right),
					new BoardGemSwap(6, 8, Down),
					new BoardGemSwap(5, 8, Right),
					new BoardGemSwap(5, 8, Down),
					new BoardGemSwap(6, 7, Right),
					new BoardGemSwap(6, 7, Down),
					new BoardGemSwap(4, 8, Down),
					new BoardGemSwap(4, 7, Right),
					new BoardGemSwap(7, 4, Right),
					new BoardGemSwap(7, 4, Down),
					new BoardGemSwap(8, 3, Right),
					new BoardGemSwap(8, 3, Down),
					new BoardGemSwap(7, 3, Right),
					new BoardGemSwap(7, 3, Down),
					new BoardGemSwap(8, 2, Right),
					new BoardGemSwap(8, 2, Down),
					new BoardGemSwap(2, 5, Right),
					new BoardGemSwap(5, 2, Right),
					new BoardGemSwap(2, 4, Down),
					new BoardGemSwap(1, 3, Down),
				});
		}
	}
}
