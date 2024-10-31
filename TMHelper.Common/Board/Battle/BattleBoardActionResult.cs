using TMHelper.Common.Board.Actions;

namespace TMHelper.Common.Board.Battle
{
	/// <summary>
	/// Модель данных с информацией о результате действий игрока на доске match-3.
	/// </summary>
	public class BattleBoardActionResult
	{
		public readonly BattleBoardState InitialBoardState;

		public readonly BoardAction Action;
		public readonly int RedGemsCollected;
		public readonly int GreenGemsCollected;
		public readonly int BlueGemsCollected;
		public readonly int SkullGemsCollected;
		public readonly bool AdditionalMoveGranted;

		public readonly BattleBoardState ResultState;
		public readonly bool ResultBoardStateHasMoves;
		public readonly bool ResultBoardStateHasOneMove;
		public readonly bool ResultBoardStateAdditionalMovePotential;

		/// <param name="initialBoardState">
		/// Состояние доски перед выполнением действия.
		/// </param>
		/// <param name="action">
		/// Действие с доской.
		/// </param>
		/// <param name="additionalMoveGranted">
		/// Привело ли действие к схлопыванию ряда из 4+ камней,
		/// благодаря чему дается право дополнительного хода.
		/// </param>
		/// <param name="resultBoardState">
		/// Состояние доски после выполнения действия,
		/// а так же после каскадного схлопывания камней на доске
		/// вплоть до стабилизации состояния доски.
		/// </param>
		/// <param name="resultBoardStateHasMoves">
		/// Есть ли в результирующем состоянии хоть один возможный ход.
		/// В случае, когда ходов нет, произойдет смена доски.
		/// </param>
		/// <param name="resultBoardStateHasOneMove">
		/// В результирующем состоянии любой ход приведет к завершению текущей доски.
		/// Эквивалентно <see cref="resultBoardStateHasMoves"/>=True для любого перемещения камней
		/// на результирующем состоянии доски.
		/// </param>
		/// <param name="resultBoardStateAdditionalMovePotential">
		/// Есть ли в результирующем состоянии такой ход,
		/// который приведет к схлопыванию ряда из 4+ камней,
		/// благодаря чему будет получено право дополнительного хода.
		/// </param>
		public BattleBoardActionResult(
			BattleBoardState initialBoardState,
			BoardAction action,
			int redGemsCollected,
			int greenGemsCollected,
			int blueGemsCollected,
			int skullGemsCollected,
			bool additionalMoveGranted,
			BattleBoardState resultBoardState,
			bool resultBoardStateHasMoves,
			bool resultBoardStateHasOneMove,
			bool resultBoardStateAdditionalMovePotential)
		{
			InitialBoardState = initialBoardState;

			Action = action;
			RedGemsCollected = redGemsCollected;
			GreenGemsCollected = greenGemsCollected;
			BlueGemsCollected = blueGemsCollected;
			SkullGemsCollected = skullGemsCollected;
			AdditionalMoveGranted = additionalMoveGranted;

			ResultState = resultBoardState;
			ResultBoardStateHasMoves = resultBoardStateHasMoves;
			ResultBoardStateHasOneMove = resultBoardStateHasOneMove;
			ResultBoardStateAdditionalMovePotential = resultBoardStateAdditionalMovePotential;
		}
	}
}
