namespace TMHelper.Common.Board.Actions
{
	/// <summary>
	/// Действие над доской match-3.
	/// Действием может быть перемещение камней, сбор камней в области, сбор камней по линиям и т.п.
	/// </summary>
	public abstract class BoardAction
	{
		/// <summary>
		/// Рассчет нового состояния доски после совершения действия
		/// </summary>
		/// <param name="currentBoardState">
		/// Текущее состояние доски (то есть состояние перед совершением действия).
		/// </param>
		/// <returns>Состояние доски после совершения действия</returns>
		public TBoardState CalculateNewBoardState<TBoardState>(TBoardState currentBoardState)
			where TBoardState : BoardState, ICloneable
		{
			TBoardState newBoardState = (TBoardState)currentBoardState.Clone();

			ApplyActionToBoardState(newBoardState);

			return newBoardState;
		}

		protected abstract void ApplyActionToBoardState(BoardState boardState);


		public sealed override string ToString()
		{
			return GetFriendlyActionDescription();
		}

		protected abstract string GetFriendlyActionDescription();
	}
}
