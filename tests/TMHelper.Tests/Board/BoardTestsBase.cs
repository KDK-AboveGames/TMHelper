namespace TMHelper.Tests.Board
{
	public abstract class BoardTestsBase<TBoardState>
		where TBoardState : BoardState, new()
	{
		#region Data Generation Utils

		protected TBoardState EmptyBoardState
		{
			get { return new TBoardState(); }
		}

		#endregion Data Generation Utils

		#region Assertion Utils

		protected void AssertBoardStatesEqual(BoardState actual, BoardState expected, string? message = null)
		{
			Assert.Multiple(() =>
			{
				Assert.That(actual.Rows, Is.EqualTo(expected.Rows), ConcatMessages(nameof(expected.Rows), message));
				Assert.That(actual.Columns, Is.EqualTo(expected.Columns), ConcatMessages(nameof(expected.Columns), message));
			});

			Assert.Multiple(() =>
			{
				for (int i = 1; i <= expected.Rows; i++)
				{
					for (int j = 1; j <= expected.Columns; j++)
					{
						Assert.That(actual[i, j], Is.EqualTo(expected[i, j]), ConcatMessages($"Wrong gem at [{i},{j}]", message));
					}
				}
			});
		}

		protected void AssertActionResultsDataAtLeast(
			Dictionary<BoardActionResultDataKeys, string> actual,
			Dictionary<BoardActionResultDataKeys, object> expectedMinimal,
			string? message = null)
		{
			Assert.Multiple(() =>
			{
				Assert.That(actual.Count, Is.AtLeast(expectedMinimal.Count));

				foreach (BoardActionResultDataKeys key in expectedMinimal.Keys)
				{
					Assert.That(
						actual.GetValueStringSafe(key),
						Is.EqualTo(expectedMinimal[key].ToString()),
						ConcatMessages(key.ToString(), message));
				}
			});
		}

		protected void AssertSwapsVariantsEqual(
			List<BoardGemSwap> actual,
			BoardGemSwap[] expected,
			string? message = null)
		{
			Assert.Multiple(() =>
			{
				foreach (BoardGemSwap expectedAction in expected)
				{
					Assert.That(
						actual,
						Does.Contain(expectedAction),
						message);
				}
			});
		}

		#endregion Assertion Utils
	}
}
