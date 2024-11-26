namespace TMHelper.Common.Board.BoxOfSages.Actions
{
	/// <summary>
	/// Действие, в котором игрок каким-то образом повлиял на состав камней на доске.
	/// </summary>
	public abstract class BoxOfSagesBoardModificationAction : BoxOfSagesBoardAction
	{
		protected readonly IBoxOfSagesBoardSolver BoardSolver;

		protected BoxOfSagesBoardModificationAction(IBoxOfSagesBoardSolver boardSolver)
		{
			BoardSolver = boardSolver;
		}

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

			ModifyBoardState(resultBoardState);

			BoardSolver.Solve(resultBoardState, out BoardCollapseResult solveResultGemsCollapsed);

			resultGemsCollapsed.Stages.AddRange(solveResultGemsCollapsed.Stages);

			for (int i = 0; i < resultGemsCollapsed.Stages.Count; i++)
			{
				BoardCollapseResult.CollapseStage collapseStage = resultGemsCollapsed.Stages[i];

				for (int j = 0; j < collapseStage.CollapsedLines.Count; j++)
				{
					BoardCollapseResult.GemsLine gemLine = collapseStage.CollapsedLines[j];

					resultsData.AppendValuesGemLineCollectedSafe(gemLine);

					if (gemLine.Length >= 4)
					{
						resultsData.SetValueSafe(
							BoardActionResultDataKeys.AdditionalMoveInfo,
							true);
					}
				}
			}

			List<BoardGemSwap> resultBoardStateSwaps = BoardSolver.GetAllPossibleSwaps(resultBoardState);

			bool resultBoardStateAdditionalMoveSwapPotential = false;

			for (int k = 0; k < resultBoardStateSwaps.Count; k++)
			{
				BoxOfSagesBoardState resultBoardStateClone = (BoxOfSagesBoardState)resultBoardState.Clone();
				resultBoardStateSwaps[k].ApplySwap(resultBoardStateClone);

				BoardSolver.Solve(resultBoardStateClone, out BoardCollapseResult collapseResult);

				for (int i = 0; i < collapseResult.Stages.Count; i++)
				{
					BoardCollapseResult.CollapseStage collapseStage = collapseResult.Stages[i];

					for (int j = 0; j < collapseStage.CollapsedLines.Count; j++)
					{
						if (collapseStage.CollapsedLines[j].Length >= 4)
						{
							resultBoardStateAdditionalMoveSwapPotential = true;
							break;
						}
					}

					if (resultBoardStateAdditionalMoveSwapPotential)
					{
						break;
					}
				}

				if (resultBoardStateAdditionalMoveSwapPotential)
				{
					break; // дальше нет смысла перебирать т.к. все необходимые данные собраны
				}
			}

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential,
				resultBoardStateAdditionalMoveSwapPotential);
		}

		protected abstract void ModifyBoardState(BoxOfSagesBoardState boardState);
	}
}
