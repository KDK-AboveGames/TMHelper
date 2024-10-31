using TMHelper.Common.Board.Battle;
using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapResultsPlacementTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void SimpleSwap2RowsCollapse()
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
				AssertBoardStatesEqual(result.ResultState, EmptyBoardState, nameof(result.ResultState));
				Assert.That(result.ResultBoardStateHasMoves, Is.False, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void FallSimple()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, B, S,
					_, _, _, R, R, G,
					_, _, _, G, G, R),
				CreateSwap(6, 6, Up));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.False, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void ShiftSimple()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, R, G,
					_, _, _, B, R, G,
					_, _, _, S, G, R),
				CreateSwap(6, 6, Left));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.False, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void ShiftFallSimple()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, S, B,
					_, _, _, _, R, G,
					_, _, _, B, R, G,
					_, _, _, S, G, R),
				CreateSwap(6, 6, Left));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.False, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void ShiftFallComplex()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					__, __, __, __, __, __,
					__, B1, R1, B1, B1, R1,
					__, R1, R1, S5, G1, S1,
					__, G1, S5, S1, G1, G1,
					B1, R1, G1, G1, S5, S1,
					S1, B1, B1, S1, B1, R1),
				CreateSwap(5, 5, Up));

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
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, B,
					_, _, _, _, B, B,
					_, _, _, S, S, G,
					_, _, _, G, G, S5),
				CreateSwap(6, 6, Up));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.False, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void ResultBoardStateHasOneMove()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, B, G,
					_, _, _, _, S, B,
					_, _, _, _, B, S,
					_, _, _, _, R, G,
					_, _, _, _, R, G,
					_, _, S, S, G, R),
				CreateSwap(6, 6, Left));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.True, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateHasOneMove, Is.True, nameof(result.ResultBoardStateHasOneMove));
			});
		}

		[Test]
		public void ResultBoardStateAdditionalMovePotential()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, S, B,
					_, _, _, _, B, S,
					_, _, _, _, R, G,
					_, _, _, _, R, G,
					_, _, S, S, G, R),
				CreateSwap(6, 6, Left));

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

				Assert.That(result.ResultBoardStateHasMoves, Is.True, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.True, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}


		[Test]
		public void Complex1()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					B1, B1, R1, R1, G1, B1,
					R1, R3, G3, S3, S1, B3,
					R1, G1, G3, R1, R1, G1,
					S3, B1, B1, G1, R1, R1,
					G1, S3, S5, B3, G1, G1,
					S1, S1, G1, G1, B3, S3),
				CreateSwap(4, 4, Down));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(11), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(9), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(10), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(18), nameof(result.SkullGemsCollected));

				Assert.That(result.AdditionalMoveGranted, Is.True, nameof(result.AdditionalMoveGranted));

				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, __, __,
						__, __, __, __, __, B1,
						__, __, __, __, __, G1,
						__, __, __, __, __, S1,
						__, __, __, __, G1, G1,
						S1, R1, R3, G3, B3, S3));

				Assert.That(result.ResultBoardStateHasMoves, Is.True, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.False, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void Complex2()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					R1, G1, B1, G1, G3, S1,
					G3, R3, S1, G1, S3, R1,
					B1, G5, R1, R1, B1, G1,
					S1, B1, G1, B1, S3, R1,
					B1, R3, G1, R1, S1, S1,
					R1, B3, S1, B1, B1, S3),
				CreateSwap(2, 5, Down));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(7), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(7), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(0), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(7), nameof(result.SkullGemsCollected));

				Assert.That(result.AdditionalMoveGranted, Is.False, nameof(result.AdditionalMoveGranted));

				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, G1, S1,
						__, __, R1, B1, G1, R1,
						__, __, G3, S1, R1, G1,
						__, S1, B1, R1, G3, R1,
						B1, B1, G1, B1, B1, S1,
						R1, B3, S1, B1, B1, S3));

				Assert.That(result.ResultBoardStateHasMoves, Is.True, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateHasOneMove, Is.False, nameof(result.ResultBoardStateHasOneMove));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.True, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}

		[Test]
		public void Complex3()
		{
			BattleBoardActionResult result = BoardSolver.ApplyActionAndSolve(
				CreateBoardState(
					__, B3, G1, G1, B1, S1,
					__, B3, B1, R1, R1, G3,
					__, G1, R1, B1, B1, R3,
					G1, S1, R1, S1, G1, R1,
					R3, S1, G1, R1, S1, B1,
					B1, B1, R3, S1, G1, G1),
				CreateSwap(5, 4, Left));

			Assert.Multiple(() =>
			{
				Assert.That(result.RedGemsCollected, Is.EqualTo(17), nameof(result.RedGemsCollected));
				Assert.That(result.GreenGemsCollected, Is.EqualTo(0), nameof(result.GreenGemsCollected));
				Assert.That(result.BlueGemsCollected, Is.EqualTo(8), nameof(result.BlueGemsCollected));
				Assert.That(result.SkullGemsCollected, Is.EqualTo(0), nameof(result.SkullGemsCollected));

				Assert.That(result.AdditionalMoveGranted, Is.True, nameof(result.AdditionalMoveGranted));

				AssertBoardStatesEqual(
					result.ResultState,
					CreateBoardState(
						__, __, __, __, __, __,
						__, __, __, __, __, S1,
						__, __, B3, G1, B1, G3,
						__, __, G1, S1, G1, R1,
						__, G1, S1, G1, S1, B1,
						R3, S1, G1, S1, G1, G1));

				Assert.That(result.ResultBoardStateHasMoves, Is.True, nameof(result.ResultBoardStateHasMoves));
				Assert.That(result.ResultBoardStateHasOneMove, Is.False, nameof(result.ResultBoardStateHasOneMove));
				Assert.That(result.ResultBoardStateAdditionalMovePotential, Is.True, nameof(result.ResultBoardStateAdditionalMovePotential));
			});
		}
	}
}
