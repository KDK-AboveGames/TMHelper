using TMHelper.BoardSolving.Battle;
using TMHelper.Common.Board;
using TMHelper.Common.Board.Battle;

namespace TMHelper.Tests.Board.Battle
{
	public abstract class BattleBoardSolverTestsBase : BoardSolverTestsBase
	{
		protected IBattleBoardSolver BoardSolver { get; private set; }

		[SetUp]
		public virtual void Setup()
		{
			BoardSolver = new BattleBoardSolver();
		}

		#region Data Generation Utils

		protected BattleBoardState EmptyBoardState
		{
			get { return new BattleBoardState(); }
		}

		/// <summary>
		/// TODO
		/// </summary>
		protected BattleBoardState CreateRandomStableBoardState()
		{
			throw new NotImplementedException();
		}

		protected BattleBoardState CreateBoardState(
			BoardGems cell11, BoardGems cell12, BoardGems cell13, BoardGems cell14, BoardGems cell15, BoardGems cell16,
			BoardGems cell21, BoardGems cell22, BoardGems cell23, BoardGems cell24, BoardGems cell25, BoardGems cell26,
			BoardGems cell31, BoardGems cell32, BoardGems cell33, BoardGems cell34, BoardGems cell35, BoardGems cell36,
			BoardGems cell41, BoardGems cell42, BoardGems cell43, BoardGems cell44, BoardGems cell45, BoardGems cell46,
			BoardGems cell51, BoardGems cell52, BoardGems cell53, BoardGems cell54, BoardGems cell55, BoardGems cell56,
			BoardGems cell61, BoardGems cell62, BoardGems cell63, BoardGems cell64, BoardGems cell65, BoardGems cell66)
		{
			BattleBoardState state = new BattleBoardState();

			state[1, 1] = cell11;
			state[1, 2] = cell12;
			state[1, 3] = cell13;
			state[1, 4] = cell14;
			state[1, 5] = cell15;
			state[1, 6] = cell16;

			state[2, 1] = cell21;
			state[2, 2] = cell22;
			state[2, 3] = cell23;
			state[2, 4] = cell24;
			state[2, 5] = cell25;
			state[2, 6] = cell26;

			state[3, 1] = cell31;
			state[3, 2] = cell32;
			state[3, 3] = cell33;
			state[3, 4] = cell34;
			state[3, 5] = cell35;
			state[3, 6] = cell36;

			state[4, 1] = cell41;
			state[4, 2] = cell42;
			state[4, 3] = cell43;
			state[4, 4] = cell44;
			state[4, 5] = cell45;
			state[4, 6] = cell46;

			state[5, 1] = cell51;
			state[5, 2] = cell52;
			state[5, 3] = cell53;
			state[5, 4] = cell54;
			state[5, 5] = cell55;
			state[5, 6] = cell56;

			state[6, 1] = cell61;
			state[6, 2] = cell62;
			state[6, 3] = cell63;
			state[6, 4] = cell64;
			state[6, 5] = cell65;
			state[6, 6] = cell66;

			return state;
		}

		#endregion Data Generation Utils

		#region Assertion Utils

		protected void AssertActionResultsEqual(BattleBoardActionResult actual, BattleBoardActionResult expected, string? message = null)
		{
			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(expected.InitialBoardState, actual.InitialBoardState, ConcatMessages(nameof(expected.InitialBoardState), message));

				Assert.That(actual.RedGemsCollected, Is.EqualTo(expected.RedGemsCollected), nameof(expected.RedGemsCollected));
				Assert.That(actual.GreenGemsCollected, Is.EqualTo(expected.GreenGemsCollected), nameof(expected.GreenGemsCollected));
				Assert.That(actual.BlueGemsCollected, Is.EqualTo(expected.BlueGemsCollected), nameof(expected.BlueGemsCollected));
				Assert.That(actual.SkullGemsCollected, Is.EqualTo(expected.SkullGemsCollected), nameof(expected.SkullGemsCollected));
				Assert.That(actual.AdditionalMoveGranted, Is.EqualTo(expected.AdditionalMoveGranted), nameof(expected.AdditionalMoveGranted));

				AssertBoardStatesEqual(expected.ResultState, actual.ResultState, ConcatMessages(nameof(expected.ResultState), message));

				Assert.That(actual.ResultBoardStateHasMoves, Is.EqualTo(expected.ResultBoardStateHasMoves), nameof(expected.ResultBoardStateHasMoves));
				Assert.That(actual.ResultBoardStateAdditionalMovePotential, Is.EqualTo(expected.ResultBoardStateAdditionalMovePotential), nameof(expected.ResultBoardStateAdditionalMovePotential));
			});
		}

		#endregion Assertion Utils
	}
}
