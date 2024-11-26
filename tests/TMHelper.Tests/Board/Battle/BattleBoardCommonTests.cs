using static TMHelper.Common.Board.BoardGems;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardCommonTests : BoardTestsBase<BattleBoardState>
	{
		[Test]
		public void FallGems()
		{
			BattleBoardState state = CreateBoardState(
				_, _, _, R, R, _,
				_, R, _, R, _, _,
				_, _, _, _, _, _,
				_, R, R, _, _, _,
				R, _, R, _, _, _,
				R, R, _, _, R, _);

			state.FallGemsOnBoard();

			AssertBoardStatesEqual(
				state,
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, _, _, _, _, _,
					_, R, _, _, _, _,
					R, R, R, R, R, _,
					R, R, R, R, R, _));
		}

		[Test]
		public void ShiftGems()
		{
			BattleBoardState state = CreateBoardState(
				_, _, _, _, _, _,
				R, _, _, _, _, R,
				R, R, _, _, _, _,
				_, _, _, R, R, _,
				_, R, _, R, _, R,
				_, _, _, _, R, R);

			state.ShiftGems();

			AssertBoardStatesEqual(
				state,
				CreateBoardState(
					_, _, _, _, _, _,
					_, _, _, _, R, R,
					_, _, _, _, R, R,
					_, _, _, _, R, R,
					_, _, _, R, R, R,
					_, _, _, _, R, R));
		}
	}
}
