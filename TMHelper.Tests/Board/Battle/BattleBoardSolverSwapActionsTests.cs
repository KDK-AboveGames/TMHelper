using TMHelper.Common.Board.Battle;
using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapActionsTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void UpDownSwapsEqual()
		{
			BattleBoardState state = CreateBoardState(
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, R, R, G,
				_, _, _, G, G, R);

			AssertActionResultsEqual(
				DoSwapAction(state, new BoardGemSwap(6, 6, Up)),
				DoSwapAction(state, new BoardGemSwap(5, 6, Down)));
		}

		[Test]
		public void LeftRightSwapsEqual()
		{
			BattleBoardState state = CreateBoardState(
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, R, G,
				_, _, _, _, R, G,
				_, _, _, _, G, R);

			AssertActionResultsEqual(
				DoSwapAction(state, new BoardGemSwap(6, 5, Right)),
				DoSwapAction(state, new BoardGemSwap(6, 6, Left)));
		}
	}
}
