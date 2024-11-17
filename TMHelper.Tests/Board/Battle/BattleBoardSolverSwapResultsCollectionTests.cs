using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapResultsCollectionTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void Simple3InRowX2()
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

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 3 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 3 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
				});
		}

		[Test]
		public void Simple3InRowSwapWithEmpty()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, G,
					_, _, _, G, R, R),
				new BoardGemSwap(6, 4, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 0 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 3 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
				});
		}

		[Test]
		public void Simple4InRow()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, _, R, G, R, R),
				new BoardGemSwap(6, 4, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 8 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 0 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
				});
		}

		[Test]
		public void Simple5InRow()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, R, R, G, R, R),
				new BoardGemSwap(6, 4, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 15 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 0 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
				});
		}

		[Test]
		public void SimpleSwap2RowsCollapseLvled()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, R1, R1, G1,
					__, __, __, G5, G1, R3),
				new BoardGemSwap(6, 6, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 5 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 7 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
					{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 0 },
				});
		}

		[Test]
		public void SimpleSwap2RowsCollapseWithSkull5()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, B,
					_, _, _, _, R, G,
					_, _, _, S, S, G,
					_, _, _, G, G, S5),
				new BoardGemSwap(6, 6, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 1 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 4 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0 },
					{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 7 },
				});
		}

		[Test]
		public void SimpleSwap2RowsCollapseWithSkull5x2()
		{
			BoardActionResult<BattleBoardState> result = DoSwapAction(
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, B1,
					__, __, __, R1, S5, G1,
					__, __, __, S1, S1, G1,
					__, __, __, G1, G1, S5),
				new BoardGemSwap(6, 6, Up));

			AssertActionResultsDataAtLeast(
				result.ResultsData,
				new Dictionary<BoardActionResultDataKeys, object>
				{
					{ BoardActionResultDataKeys.RedGemsCollectedTotal, 1 },
					{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 4 },
					{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 1 },
					{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 12 },
				});
		}
	}
}
