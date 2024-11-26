using TMHelper.Common.Board.Battle;
using static TMHelper.Common.Board.BoardGems;
using static TMHelper.Common.Board.BoardGemSwapDirections;

namespace TMHelper.Tests.Board.Battle
{
	public sealed class BattleBoardSolverSwapResultsCommonTests : BattleBoardSolverTestsBase
	{
		[Test]
		public void ModelStatesCorrect()
		{
			BattleBoardState initialState = CreateBoardState(
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, _, _, _,
				_, _, _, R, R, G,
				_, _, _, G, G, R);

			BoardActionResult<BattleBoardState> result = DoSwapAction(
				initialState,
				new BoardGemSwap(6, 6, Up));

			Assert.Multiple(() =>
			{
				AssertBoardStatesEqual(result.InitialBoardState, initialState, nameof(result.InitialBoardState));

				Assert.That(result.ResultState, Is.Not.Null, nameof(result.ResultState));
			});
		}

		[Test]
		public void IncorrectAction()
		{
			Assert.Throws(
				Is.TypeOf<ArgumentOutOfRangeException>(),
				() =>
				{
					BoardActionResult<BattleBoardState> result = DoSwapAction(
						CreateBoardState(
							_, _, _, _, _, _,
							_, _, _, _, _, _,
							_, _, _, _, _, _,
							_, _, _, _, _, _,
							_, _, _, R, R, G,
							_, _, _, G, G, R),
						new BoardGemSwap(7, 6, Up));
				});
		}
	}
}
