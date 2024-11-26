
namespace TMHelper.Common.Board.Battle.Actions.Magic
{
	public class LordOfElementsSwapAction : BattleBoardGemSwapAction
	{
		public LordOfElementsSwapAction(BoardGemSwap swap, IBattleBoardSolver solver)
			: base(swap, solver) { }

		protected override string GetFriendlyActionDescription()
		{
			return "П.С. + " + base.GetFriendlyActionDescription();
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

			int damage = 0;

			for (int i = 0; i < resultGemsCollapsed.Stages.Count; i++)
			{
				BoardCollapseResult.CollapseStage collapseStage = resultGemsCollapsed.Stages[i];

				for (int j = 0; j < collapseStage.CollapsedLines.Count; j++)
				{
					BoardCollapseResult.GemsLine gemLine = collapseStage.CollapsedLines[j];

					damage += gemLine.GemsTotalCount * 4; // todo: применять в этом месте характеристики персонажа
				}
			}

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMin,
				damage); // todo: применять в этом месте характеристики персонажа

			resultsData.AppendValueSafe(
				BoardActionResultDataKeys.InstantDamageMax,
				damage); // todo: применять в этом месте характеристики персонажа
		}
	}
}
