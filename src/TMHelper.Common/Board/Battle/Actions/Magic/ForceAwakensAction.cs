namespace TMHelper.Common.Board.Battle.Actions.Magic
{
	public class ForceAwakensAction : BattleBoardAction
	{
		protected override string GetFriendlyActionDescription()
		{
			return "Пробуждение Силы II";
		}

		protected override void ApplyActionToBoardState(
			BattleBoardState boardState,
			out BattleBoardState resultBoardState,
			out BoardCollapseResult resultGemsCollapsed,
			out Dictionary<BoardActionResultDataKeys, string> resultsData)
		{
			base.ApplyActionToBoardState(
				boardState,
				out resultBoardState,
				out resultGemsCollapsed,
				out resultsData);

			for (int row = 1; row <= boardState.Rows; row++)
			{
				for (int column = 1; column <= boardState.Columns; column++)
				{
					BoardGems gem = boardState[row, column];

					if (!gem.IsSameTypeAs(BoardGems.Skull) && gem.GetCountValue() >= 4)
					{
						return;
					}
				}
			}

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.AdditionalMoveInfo,
				true); // магия не передает ход, если на поле нету камня с уровнем >= 4
		}
	}
}
