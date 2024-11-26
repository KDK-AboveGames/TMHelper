namespace TMHelper.Common.Board.Battle.Actions
{
	/// <summary>
	/// Действие, в котором игрок каким-то образом повлиял на состав камней на доске.
	/// </summary>
	public abstract class BattleBoardModificationAction : BattleBoardAction
	{
		protected readonly IBattleBoardSolver BoardSolver;

		protected BattleBoardModificationAction(IBattleBoardSolver boardSolver)
		{
			BoardSolver = boardSolver;
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

			ModifyBoardState(resultBoardState);
			if (FallShiftGemsAfterModification)
			{
				resultBoardState.FallGemsOnBoard();
				resultBoardState.ShiftGems();
			}

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

			int skullGemsCollected = resultsData.GetValueIntSafe(BoardActionResultDataKeys.SkullGemsCollectedTotal);
			if (skullGemsCollected > 0)
			{
				resultsData.AppendValueSafe(
					BoardActionResultDataKeys.InstantDamageMin,
					skullGemsCollected); // todo: применять в этом месте характеристики персонажа

				resultsData.AppendValueSafe(
					BoardActionResultDataKeys.InstantDamageMax,
					skullGemsCollected); // todo: применять в этом месте характеристики персонажа
			}

			List<BoardGemSwap> resultBoardStateSwaps = BoardSolver.GetAllPossibleSwaps(resultBoardState);

			bool resultBoardStateHasSeveralMoves = false;
			bool resultBoardStateAdditionalMoveSwapPotential = false;

			for (int k = 0; k < resultBoardStateSwaps.Count; k++)
			{
				BattleBoardState resultBoardStateClone = (BattleBoardState)resultBoardState.Clone();
				resultBoardStateSwaps[k].ApplySwap(resultBoardStateClone);

				BoardSolver.Solve(resultBoardStateClone, out BoardCollapseResult collapseResult);

				if (BoardSolver.GetAllPossibleSwaps(resultBoardStateClone).Count > 0)
				{
					resultBoardStateHasSeveralMoves = true;
				}

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

				if (resultBoardStateHasSeveralMoves && resultBoardStateAdditionalMoveSwapPotential)
				{
					break; // дальше нет смысла перебирать т.к. все необходимые данные собраны
				}
			}

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.ResultBoardStateHasMoves,
				resultBoardStateSwaps.Count > 0);

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.ResultBoardStateHasOneMove,
				!resultBoardStateHasSeveralMoves);

			resultsData.SetValueSafe(
				BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential,
				resultBoardStateAdditionalMoveSwapPotential);
		}

		/// <summary>
		/// Обновляет состояние доски в соответствие с действием.
		/// </summary>
		protected abstract void ModifyBoardState(BattleBoardState boardState);

		/// <summary>
		/// Сдвинуть ли камни к краю доски после модификации состоянии доски.
		/// </summary>
		protected virtual bool FallShiftGemsAfterModification
		{
			get { return true; }
		}
	}
}
