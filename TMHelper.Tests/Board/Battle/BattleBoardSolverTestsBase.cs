using TMHelper.BoardSolving.Battle;
using TMHelper.Common.Board.Battle.Actions;

namespace TMHelper.Tests.Board.Battle
{
	public abstract class BattleBoardSolverTestsBase : BoardTestsBase<BattleBoardState>
	{
		protected IBattleBoardSolver BoardSolver { get; private set; }

		[SetUp]
		public virtual void Setup()
		{
			BoardSolver = new BattleBoardSolver();
		}

		protected BoardActionResult<BattleBoardState> DoSwapAction(BattleBoardState state, BoardGemSwap swap)
		{
			return new BattleBoardGemSwapAction(swap, BoardSolver)
				.DoAction(state);
		}

		#region Assertion Utils

		protected void AssertActionResultsEqual(
			BoardActionResult<BattleBoardState> actual,
			BoardActionResult<BattleBoardState> expected,
			string? message = null)
		{
			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(
					actual.InitialBoardState,
					expected.InitialBoardState,
					ConcatMessages(nameof(expected.InitialBoardState), message));

				AssertBoardStatesEqual(
					actual.ResultState,
					expected.ResultState,
					ConcatMessages(nameof(expected.ResultState), message));

				Assert.That(
					actual.ResultGemsCollapsed.Stages.SelectMany(x => x.CollapsedLines),
					Is.EquivalentTo(expected.ResultGemsCollapsed.Stages.SelectMany(x => x.CollapsedLines)));

				Assert.That(
					actual.ResultsData.Count,
					Is.EqualTo(expected.ResultsData.Count),
					ConcatMessages(nameof(expected.ResultsData), nameof(expected.ResultsData.Count), message));

				AssertActionResultsDataAtLeast(
					actual.ResultsData,
					expected.ResultsData.ToDictionary(x => x.Key, x => (object)x.Value),
					ConcatMessages(nameof(expected.ResultsData), message));
			});
		}

		#endregion Assertion Utils
	}
}
