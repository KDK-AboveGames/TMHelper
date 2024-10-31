using TMHelper.Common.Board.Actions;

namespace TMHelper.Common.Board.BoxOfSages
{
	/// <summary>
	/// Модель данных с информацией о результате действий игрока на доске match-3.
	/// </summary>
	public class BoxOfSagesBoardActionResult
	{
		public readonly BoxOfSagesBoardState InitialBoardState;

		public readonly BoardGemSwapAction Action;
		public readonly int RedGemsCollected;
		public readonly int GreenGemsCollected;
		public readonly int BlueGemsCollected;
		public readonly int YellowGemsCollected;
		public readonly int MaroonGemsCollected;
		public readonly int PurpleGemsCollected;
		public readonly bool AdditionalMoveGranted;

		public readonly BoxOfSagesBoardState ResultBoardState;
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
		/// <param name="resultBoardStateAdditionalMovePotential">
		/// Есть ли в результирующем состоянии такой ход,
		/// который приведет к схлопыванию ряда из 4+ камней,
		/// благодаря чему будет получено право дополнительного хода.
		/// </param>
		public BoxOfSagesBoardActionResult(
			BoxOfSagesBoardState initialBoardState,
			BoardGemSwapAction action,
			int redGemsCollected,
			int greenGemsCollected,
			int blueGemsCollected,
			int yellowGemsCollected,
			int maroonGemsCollected,
			int purpleGemsCollected,
			bool additionalMoveGranted,
			BoxOfSagesBoardState resultBoardState,
			bool resultBoardStateAdditionalMovePotential)
		{
			InitialBoardState = initialBoardState;

			Action = action;
			RedGemsCollected = redGemsCollected;
			GreenGemsCollected = greenGemsCollected;
			BlueGemsCollected = blueGemsCollected;
			YellowGemsCollected = yellowGemsCollected;
			MaroonGemsCollected = maroonGemsCollected;
			PurpleGemsCollected = purpleGemsCollected;
			AdditionalMoveGranted = additionalMoveGranted;

			ResultBoardState = resultBoardState;
			ResultBoardStateAdditionalMovePotential = resultBoardStateAdditionalMovePotential;
		}
	}
}
