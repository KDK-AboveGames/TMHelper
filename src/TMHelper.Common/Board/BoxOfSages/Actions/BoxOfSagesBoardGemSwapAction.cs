namespace TMHelper.Common.Board.BoxOfSages.Actions
{
	/// <summary>
	/// Действие, в котором игрок совершил сдвиг камней на доске.
	/// </summary>
	public class BoxOfSagesBoardGemSwapAction : BoxOfSagesBoardModificationAction
	{
		public readonly BoardGemSwap Swap;

		public BoxOfSagesBoardGemSwapAction(BoardGemSwap swap, IBoxOfSagesBoardSolver solver)
			: base(solver)
		{
			Swap = swap;
		}

		protected override string GetFriendlyActionDescription()
		{
			return Swap.ToString();
		}

		protected override void ModifyBoardState(BoxOfSagesBoardState boardState)
		{
			Swap.ApplySwap(boardState);
		}
	}
}
