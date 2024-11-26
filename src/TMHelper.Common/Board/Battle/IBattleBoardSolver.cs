namespace TMHelper.Common.Board.Battle
{
	public interface IBattleBoardSolver
	{
		List<BoardGemSwap> GetAllPossibleSwaps(BattleBoardState boardState);

		void Solve(BattleBoardState boardStateToUpdate, out BoardCollapseResult gemLinesCollapsed);
	}
}
