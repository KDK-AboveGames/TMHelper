namespace TMHelper.Common.Board.Battle.Actions.Magic
{
	public class RitualOfBloodAction : BattleBoardModificationAction
	{
		public RitualOfBloodAction(IBattleBoardSolver solver)
			: base(solver) { }

		protected override string GetFriendlyActionDescription()
		{
			return "Ритуал крови II";
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

			int redGemsCountTotal = boardState.GetGemsOnBoardTotalCount(BoardGems.Red);
			int damage = (int)Math.Floor(0.7 * redGemsCountTotal);

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMin,
				damage); // todo: применять в этом месте характеристики персонажа

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMax,
				damage); // todo: применять в этом месте характеристики персонажа

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.DamagePerMove,
				"за каждый череп на поле по 2 жизни +5% за каждую единицу базового урона"); // todo: применять в этом месте характеристики персонажа

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.DamagePerMoveDuration,
				7); // todo: применять в этом месте характеристики персонажа
		}

		/// <inheritdoc/>
		protected override void ModifyBoardState(BattleBoardState boardState)
		{
			for (int row = 1; row <= boardState.Rows; row++)
			{
				for (int column = 1; column <= boardState.Columns; column++)
				{
					BoardGems gem = boardState[row, column];

					if (gem.IsSameTypeAs(BoardGems.Skull) && gem.GetCountValue() == 1)
					{
						boardState[row, column] = BoardGems.Empty; // магия убирает с поля все черепа 1-го уровня
					}
				}
			}
		}
	}
}
