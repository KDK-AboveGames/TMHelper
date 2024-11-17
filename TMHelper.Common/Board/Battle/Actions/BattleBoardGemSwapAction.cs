namespace TMHelper.Common.Board.Battle.Actions
{
	/// <summary>
	/// Действие, в котором игрок совершил сдвиг камней на доске.
	/// </summary>
	public class BattleBoardGemSwapAction : BattleBoardModificationAction
	{
		public readonly BoardGemSwap Swap;

		public BattleBoardGemSwapAction(BoardGemSwap swap, IBattleBoardSolver solver)
			: base(solver)
		{
			Swap = swap;
		}

		protected override string GetFriendlyActionDescription()
		{
			return Swap.ToString();
		}

		/// <inheritdoc/>
		protected override void ModifyBoardState(BattleBoardState boardState)
		{
			Swap.ApplySwap(boardState);
		}

		/// <inheritdoc/>
		protected override bool FallShiftGemsAfterModification
		{
			// Не сдвигаем камни после обновления состояния потому что механика позволяет поменять камни местами с пустой клеткой,
			// и, если сразу сдвинуть камни то этот обмен местами может быть сразу отменен.
			get { return false; }
		}
	}
}
