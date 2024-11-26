namespace TMHelper.Common.Board
{
	/// <summary>
	/// Действие над доской match-3.
	/// Действием может быть перемещение камней, сбор камней в области, сбор камней по линиям и т.п.
	/// </summary>
	public abstract class BoardAction<TBoardState>
		where TBoardState : BoardState, new()
	{
		/// <summary>
		/// Рассчет результата действия и нового состояния доски после совершения действия.
		/// </summary>
		/// <param name="currentBoardState">
		/// Текущее состояние доски (то есть состояние перед совершением действия).
		/// </param>
		/// <returns>Информация о результате действия.</returns>
		public BoardActionResult<TBoardState> DoAction(TBoardState currentBoardState)
		{
			ApplyActionToBoardState(
				currentBoardState,
				out TBoardState resultBoardState,
				out BoardCollapseResult resultGemsCollapsed,
				out Dictionary<BoardActionResultDataKeys, string> resultsData);

			return new BoardActionResult<TBoardState>(
				currentBoardState,
				this,
				resultBoardState,
				resultGemsCollapsed,
				resultsData);
		}

		protected abstract string GetFriendlyActionDescription();

		protected virtual void ApplyActionToBoardState(
			TBoardState boardState,
			out TBoardState resultBoardState,
			out BoardCollapseResult resultGemsCollapsed,
			out Dictionary<BoardActionResultDataKeys, string> resultsData)
		{
			resultBoardState = (TBoardState)boardState.Clone();

			resultGemsCollapsed = new BoardCollapseResult();

			resultsData = new Dictionary<BoardActionResultDataKeys, string>
			{
				{ BoardActionResultDataKeys.AdditionalMoveInfo, bool.FalseString },

				{ BoardActionResultDataKeys.RedGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.GreenGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.BlueGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.SkullGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.YellowGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.MaroonGemsCollectedTotal, 0.ToString() },
				{ BoardActionResultDataKeys.PurpleGemsCollectedTotal, 0.ToString() },
			};
		}

		#region Common Overrides

		public sealed override string ToString()
		{
			return GetFriendlyActionDescription();
		}

		#endregion Common Overrides
	}
}
