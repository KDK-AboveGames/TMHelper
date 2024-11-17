using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.BoxOfSages
{
	public sealed class BoxOfSagesBoardSolverSwapResultsPlacementTests : BoxOfSagesBoardSolverTestsBase
	{
		[Test]
		public void Complex1()
		{
			BoardActionResult<BoxOfSagesBoardState> result = DoSwapAction(
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
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1),
				new BoardGemSwap(3, 4, Right));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						P1, R3, G2, __, R1, M2, G2, B1, R1, P1,
						M1, R2, Y1, __, M1, M1, Y1, Y1, G3, G1,
						R3, B3, P1, __, B2, G1, Y1, G1, M2, M2,
						Y1, M1, P1, __, R2, B2, G3, B1, R2, P3,
						Y1, R1, Y1, B2, R1, P1, M2, M1, G1, M1,
						R1, P2, M1, G3, G1, R1, B2, Y3, B1, G2,
						B2, B1, G1, B3, G1, P2, M3, B1, R2, Y1,
						Y1, G1, R2, B1, Y1, R1, Y1, G3, M1, G1,
						Y1, P1, G1, G1, R1, R1, G1, G3, M2, P1,
						G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.RedGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.YellowGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.MaroonGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.PurpleGemsCollectedTotal, 16 },

						{ BoardActionResultDataKeys.AdditionalMoveInfo, true },
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateHasOneMove, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void Complex2()
		{
			BoardActionResult<BoxOfSagesBoardState> result = DoSwapAction(
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
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1),
				new BoardGemSwap(10, 9, Right));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						P1, G1, P3, M2, Y2, __, __, __, __, __,
						M1, Y1, Y1, B1, R1, R1, M1, __, __, G1,
						B3, B1, P1, Y1, M1, P1, P3, __, __, P1,
						Y1, M1, P1, M1, B2, R3, G2, __, __, M2,
						Y1, R1, Y1, R1, R2, B1, Y1, __, P2, P3,
						R1, P2, M1, R2, R1, M2, B2, G1, R1, M1,
						B2, B1, G1, P3, G1, B2, M3, Y3, M2, G2,
						Y1, G1, R2, G1, Y1, P1, Y1, B1, R2, Y1,
						Y1, P1, G1, G1, R1, P2, G1, G3, B1, P1,
						G1, R2, R1, B3, B1, Y1, Y1, B1, R2, G1));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.RedGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 11 },
						{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 3 },
						{ BoardActionResultDataKeys.YellowGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.MaroonGemsCollectedTotal, 4 },
						{ BoardActionResultDataKeys.PurpleGemsCollectedTotal, 0 },

						{ BoardActionResultDataKeys.AdditionalMoveInfo, false },
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateHasOneMove, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}
	}
}
