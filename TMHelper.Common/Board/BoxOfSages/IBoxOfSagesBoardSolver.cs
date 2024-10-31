using TMHelper.Common.Board.Actions;

namespace TMHelper.Common.Board.BoxOfSages
{
	public interface IBoxOfSagesBoardSolver
	{
		List<BoardGemSwapAction> GetAllPossibleSwaps(BoxOfSagesBoardState boardState);

		BoxOfSagesBoardActionResult ApplyActionAndSolve(BoxOfSagesBoardState boardState, BoardGemSwapAction action);
	}
}
