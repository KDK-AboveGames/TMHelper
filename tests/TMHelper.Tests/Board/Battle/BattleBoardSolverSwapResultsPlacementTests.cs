using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapResultsPlacementTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void SimpleSwap2RowsCollapse()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, R, G,
					_, _, _, G, G, R),
				new BoardGemSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(result.ResultState, EmptyBoardState, nameof(result.ResultState));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void FallSimple()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, B, S,
					_, _, _, R, R, G,
					_, _, _, G, G, R),
				new BoardGemSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, B, S));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void ShiftSimple()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, R, G,
					_, _, _, B, R, G,
					_, _, _, S, G, R),
				new BoardGemSwap(6, 6, Left));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, B,
						_, _, _, _, _, S));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void ShiftFallSimple()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, S, B,
					_, _, _, _, R, G,
					_, _, _, B, R, G,
					_, _, _, S, G, R),
				new BoardGemSwap(6, 6, Left));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, B,
						_, _, _, S, S, B));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void ShiftFallComplex()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					__, __, __, __, __, __,
					__, B1, R1, B1, B1, R1,
					__, R1, R1, S5, G1, S1,
					__, G1, S5, S1, G1, G1,
					B1, R1, G1, G1, S5, S1,
					S1, B1, B1, S1, B1, R1),
				new BoardGemSwap(5, 5, Up));

			AssertBoardStatesEqual(
				result.ResultState,
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, B1, B1, R1,
					S1, B1, B1, S1, B1, R1));
		}

		[Test]
		public void SimpleSwap2RowsCollapseWithSkull5()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, B,
					_, _, _, _, B, B,
					_, _, _, S, S, G,
					_, _, _, G, G, S5),
				new BoardGemSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, B),
					nameof(result.ResultState));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },
					});
			});
		}

		[Test]
		public void ResultBoardStateHasOneMove()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, B, G,
					_, _, _, _, S, B,
					_, _, _, _, B, S,
					_, _, _, _, R, G,
					_, _, _, _, R, G,
					_, _, S, S, G, R),
				new BoardGemSwap(6, 6, Left));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, B, G,
						_, _, _, _, S, B,
						_, _, S, S, B, S));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, true },
					});
			});
		}

		[Test]
		public void ResultBoardStateAdditionalMovePotential()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, S, B,
					_, _, _, _, B, S,
					_, _, _, _, R, G,
					_, _, _, _, R, G,
					_, _, S, S, G, R),
				new BoardGemSwap(6, 6, Left));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, _, _,
						_, _, _, _, S, B,
						_, _, S, S, B, S));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, true },
					});
			});
		}


		[Test]
		public void Complex1()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					B1, B1, R1, R1, G1, B1,
					R1, R3, G3, S3, S1, B3,
					R1, G1, G3, R1, R1, G1,
					S3, B1, B1, G1, R1, R1,
					G1, S3, S5, B3, G1, G1,
					S1, S1, G1, G1, B3, S3),
				new BoardGemSwap(4, 4, Down));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, __, __,
						__, __, __, __, __, B1,
						__, __, __, __, __, G1,
						__, __, __, __, __, S1,
						__, __, __, __, G1, G1,
						S1, R1, R3, G3, B3, S3));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, false },

						{ BoardActionResultDataKeys.AdditionalMoveInfo, true },

						{ BoardActionResultDataKeys.RedGemsCollectedTotal, 11 },
						{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 9 },
						{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 10 },
						{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 18 },
					});
			});
		}

		[Test]
		public void Complex2()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					R1, G1, B1, G1, G3, S1,
					G3, R3, S1, G1, S3, R1,
					B1, G5, R1, R1, B1, G1,
					S1, B1, G1, B1, S3, R1,
					B1, R3, G1, R1, S1, S1,
					R1, B3, S1, B1, B1, S3),
				new BoardGemSwap(2, 5, Down));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, G1, S1,
						__, __, R1, B1, G1, R1,
						__, __, G3, S1, R1, G1,
						__, S1, B1, R1, G3, R1,
						B1, B1, G1, B1, B1, S1,
						R1, B3, S1, B1, B1, S3));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateHasOneMove, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, true },

						{ BoardActionResultDataKeys.RedGemsCollectedTotal, 7 },
						{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 7 },
						{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 7 },
					});
			});
		}

		[Test]
		public void Complex3()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					__, B3, G1, G1, B1, S1,
					__, B3, B1, R1, R1, G3,
					__, G1, R1, B1, B1, R3,
					G1, S1, R1, S1, G1, R1,
					R3, S1, G1, R1, S1, B1,
					B1, B1, R3, S1, G1, G1),
				new BoardGemSwap(5, 4, Left));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, __, __,
						__, __, __, __, __, S1,
						__, __, B3, G1, B1, G3,
						__, __, G1, S1, G1, R1,
						__, G1, S1, G1, S1, B1,
						R3, S1, G1, S1, G1, G1));

				AssertActionResultsDataAtLeast(
					result.ResultsData,
					new Dictionary<BoardActionResultDataKeys, object>
					{
						{ BoardActionResultDataKeys.ResultBoardStateHasMoves, true },
						{ BoardActionResultDataKeys.ResultBoardStateHasOneMove, false },
						{ BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential, true },

						{ BoardActionResultDataKeys.AdditionalMoveInfo, true },

						{ BoardActionResultDataKeys.RedGemsCollectedTotal, 17 },
						{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 0 },
						{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 8 },
						{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 0 },
					});
			});
		}
	}
}
