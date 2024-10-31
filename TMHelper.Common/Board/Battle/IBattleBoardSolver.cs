using TMHelper.Common.Board.Actions;

namespace TMHelper.Common.Board.Battle
{
	public interface IBattleBoardSolver
	{
		List<BoardGemSwapAction> GetAllPossibleSwaps(BattleBoardState boardState);

		BattleBoardActionResult ApplyActionAndSolve(BattleBoardState boardState, BoardAction action);
	}
}
