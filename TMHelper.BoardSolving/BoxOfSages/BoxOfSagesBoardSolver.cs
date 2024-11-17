using TMHelper.Common.Board;
using TMHelper.Common.Board.BoxOfSages;

namespace TMHelper.BoardSolving.BoxOfSages
{
	public class BoxOfSagesBoardSolver : BoardSolverBase<BoxOfSagesBoardState>, IBoxOfSagesBoardSolver
	{
		public void Solve(BoxOfSagesBoardState boardStateToUpdate, out BoardCollapseResult gemLinesCollapsed)
		{
			gemLinesCollapsed = new BoardCollapseResult();

			bool isBoardStateFinal;
			do
			{
				isBoardStateFinal = true;

				List<BoardCoords> gemsCoordsToRemove = new();

				BoardCollapseResult.CollapseStage collapseStage = new();

				IEnumerator<SameGemsLine> gemsLinesEnumerator = FindSameGemsLines(boardStateToUpdate);
				while (gemsLinesEnumerator.MoveNext())
				{
					SameGemsLine gemsLine = gemsLinesEnumerator.Current;
					List<BoardGems> gemsLineList = new();

					isBoardStateFinal = false; // т.к. нашлась линия камней, которая схлопнется

					int rowStart = gemsLine.RowStart < gemsLine.RowEnd ? gemsLine.RowStart : gemsLine.RowEnd;
					int rowEnd = gemsLine.RowStart < gemsLine.RowEnd ? gemsLine.RowEnd : gemsLine.RowStart;
					int columnStart = gemsLine.ColumnStart < gemsLine.ColumnEnd ? gemsLine.ColumnStart : gemsLine.ColumnEnd;
					int columnEnd = gemsLine.ColumnStart < gemsLine.ColumnEnd ? gemsLine.ColumnEnd : gemsLine.ColumnStart;

					for (int row = rowStart; row <= rowEnd; row++)
					{
						for (int column = columnStart; column <= columnEnd; column++)
						{
							BoardCoords coords = new(row, column);
							BoardGems gem = boardStateToUpdate[coords];

							gemsLineList.Add(gem);

							if (!gemsCoordsToRemove.Contains(coords))
							{
								gemsCoordsToRemove.Add(coords);
							}
						}
					}

					collapseStage.CollapsedLines.Add(
						new BoardCollapseResult.GemsLine(gemsLineList));
				}

				for (int i = 0; i < gemsCoordsToRemove.Count; i++)
				{
					boardStateToUpdate[gemsCoordsToRemove[i]] = BoardGems.Empty;
				}

				gemLinesCollapsed.Stages.Add(collapseStage);

				FallGemsOnBoard(boardStateToUpdate);
			}
			while (!isBoardStateFinal);
		}
	}
}
