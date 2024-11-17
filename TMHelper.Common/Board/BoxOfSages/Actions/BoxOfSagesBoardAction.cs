namespace TMHelper.Common.Board.BoxOfSages.Actions
{
	/// <summary>
	/// Действие над доской match-3 Шкатулки Мудрецов.
	/// </summary>
	public abstract class BoxOfSagesBoardAction : BoardAction<BoxOfSagesBoardState>
	{
		protected override void ApplyActionToBoardState(
			BoxOfSagesBoardState boardState,
			out BoxOfSagesBoardState resultBoardState,
			out BoardCollapseResult resultGemsCollapsed,
			out Dictionary<BoardActionResultDataKeys, string> resultsData)
		{
			base.ApplyActionToBoardState(
				boardState,
				out resultBoardState,
				out resultGemsCollapsed,
				out resultsData);

			// Т.к. доска не заканчивается из-за механики падения камней сверху, то:
			resultsData.SetValueSafe(BoardActionResultDataKeys.ResultBoardStateHasMoves, true);
			resultsData.SetValueSafe(BoardActionResultDataKeys.ResultBoardStateHasOneMove, false);
		}
	}
}
