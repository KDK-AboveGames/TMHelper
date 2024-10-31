using TMHelper.Common.Board.Battle;
using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapResultsCollectionTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void Simple3InRowX2()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, R, G,
					_, _, _, G, G, R),
				CreateSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(3), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(3), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void Simple3InRowSwapWithEmpty()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, G, G,
					_, _, _, G, R, R),
				CreateSwap(6, 4, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(0), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(3), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void Simple4InRow()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, _, R, G, R, R),
				CreateSwap(6, 4, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(8), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(0), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void Simple5InRow()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, R, B, G,
					_, R, R, G, R, R),
				CreateSwap(6, 4, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(15), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(0), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void SimpleSwap2RowsCollapseLvled()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, R1, R1, G1,
					__, __, __, G5, G1, R3),
				CreateSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(5), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(7), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void SimpleSwap2RowsCollapseWithSkull5()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, B,
					_, _, _, _, R, G,
					_, _, _, S, S, G,
					_, _, _, G, G, S5),
				CreateSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(1), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(4), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(7), nameof(result.BlueGemsCollected));
			});
		}

		[Test]
		public void SimpleSwap2RowsCollapseWithSkull5x2()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, __, B1,
					__, __, __, R1, S5, G1,
					__, __, __, S1, S1, G1,
					__, __, __, G1, G1, S5),
				CreateSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(1), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(4), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(1), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(12), nameof(result.BlueGemsCollected));
			});
		}
	}
}
