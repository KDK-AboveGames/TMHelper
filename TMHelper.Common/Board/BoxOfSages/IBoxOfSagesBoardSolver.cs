namespace TMHelper.Common.Board.BoxOfSages
{
	public interface IBoxOfSagesBoardSolver
	{
		List<BoardGemSwap> GetAllPossibleSwaps(BoxOfSagesBoardState boardState);

		void Solve(BoxOfSagesBoardState boardStateToUpdate, out BoardCollapseResult gemLinesCollapsed);
	}
}
