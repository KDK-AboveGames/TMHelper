namespace TMHelper.Common.Board
{
	/// <summary>
	/// Модель данных с информацией о результате действий игрока на доске match-3.
	/// </summary>
	public class BoardActionResult<TBoardState>
		where TBoardState : BoardState, new()
	{
		/// <summary>
		/// Состояние доски перед выполнением действия.
		/// </summary>
		public readonly TBoardState InitialBoardState;

		/// <summary>
		/// Действие с доской.
		/// </summary>
		public readonly BoardAction<TBoardState> Action;

		/// <summary>
		/// Состояние доски после выполнения действия,
		/// а так же после каскадного схлопывания камней на доске
		/// вплоть до стабилизации состояния доски.
		/// </summary>
		public readonly TBoardState ResultState;

		/// <summary>
		/// Сколлапсировавшие (=собранные) камни в результате действия с доской.
		/// </summary>
		public readonly BoardCollapseResult ResultGemsCollapsed;

		/// <summary>
		/// Дополнительные данные о результате действия с доской.
		/// </summary>
		public readonly Dictionary<BoardActionResultDataKeys, string> ResultsData;

		public BoardActionResult(
			TBoardState initialBoardState,
			BoardAction<TBoardState> action,
			TBoardState resultState,
			BoardCollapseResult resultGemsCollapsed,
			Dictionary<BoardActionResultDataKeys, string> resultsData)
		{
			InitialBoardState = initialBoardState
				?? throw new ArgumentNullException(nameof(initialBoardState));

			Action = action
				?? throw new ArgumentNullException(nameof(action));

			ResultState = resultState
				?? throw new ArgumentNullException(nameof(resultState));

			ResultGemsCollapsed = resultGemsCollapsed
				?? throw new ArgumentNullException(nameof(resultGemsCollapsed));

			ResultsData = resultsData
				?? throw new ArgumentNullException(nameof(resultsData));
		}
	}
}
