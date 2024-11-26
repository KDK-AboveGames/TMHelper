namespace TMHelper.Common.Board.Battle.Actions.Magic
{
	public class DoubleAttackAction : BattleBoardAction
	{
		protected override string GetFriendlyActionDescription()
		{
			return "Двойная атака II";
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

			int damage1 = (int)Math.Floor(
				boardState.GetGemsOnBoardTotalCount(BoardGems.Green)
					* 2.5); // todo: применять в этом месте характеристики персонажа

			int damage2 = (int)Math.Floor(
				boardState.GetGemsOnBoardTotalCount(BoardGems.Blue)
					* 2.5); // todo: применять в этом месте характеристики персонажа

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMin,
				damage1 + damage2); // todo: применять в этом месте характеристики персонажа

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMax,
				damage1 + damage2); // todo: применять в этом месте характеристики персонажа
		}
	}
}
