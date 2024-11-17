using TMHelper.BoardSolving.BoxOfSages;
using TMHelper.Common.Board.BoxOfSages.Actions;

namespace TMHelper.Tests.Board.BoxOfSages
{
	public abstract class BoxOfSagesBoardSolverTestsBase : BoardTestsBase<BoxOfSagesBoardState>
	{
		protected IBoxOfSagesBoardSolver BoardSolver { get; private set; }

		[SetUp]
		public virtual void Setup()
		{
			BoardSolver = new BoxOfSagesBoardSolver();
		}

		protected BoardActionResult<BoxOfSagesBoardState> DoSwapAction(BoxOfSagesBoardState state, BoardGemSwap swap)
		{
			return new BoxOfSagesBoardGemSwapAction(swap, BoardSolver)
				.DoAction(state);
		}
	}
}
