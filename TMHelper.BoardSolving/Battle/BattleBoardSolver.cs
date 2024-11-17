using TMHelper.Common.Board;
using TMHelper.Common.Board.Battle;

namespace TMHelper.BoardSolving.Battle
{
	public class BattleBoardSolver : BoardSolverBase<BattleBoardState>, IBattleBoardSolver
	{
		public void Solve(BattleBoardState boardStateToUpdate, out BoardCollapseResult gemLinesCollapsed)
		{
			gemLinesCollapsed = new BoardCollapseResult();

			bool isBoardStateFinal;
			do
			{
				isBoardStateFinal = true;

				List<BoardCoords> gemsCoordsToRemove = new();
				Queue<BoardCoords> skullsLvl5CoordsQueue = new();

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

					BoardGems gemsType = boardStateToUpdate[rowStart, columnStart];

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

							// Специальная обработка сбора взрывающегося черепа
							if (gemsType.IsSameTypeAs(BoardGems.Skull) && gem.GetCountValue() == 5)
							{
								skullsLvl5CoordsQueue.Enqueue(coords);
							}
						}
					}

					collapseStage.CollapsedLines.Add(
						new BoardCollapseResult.GemsLine(gemsLineList));
				}

				while (skullsLvl5CoordsQueue.Count > 0)
				{
					BoardCoords skullLvl5Coords = skullsLvl5CoordsQueue.Dequeue();

					List<BoardCoords> skullLvl5CoordsAround = boardStateToUpdate.GetCoordsAround(skullLvl5Coords);
					for (int i = 0; i < skullLvl5CoordsAround.Count; i++)
					{
						BoardCoords coords = skullLvl5CoordsAround[i];
						BoardGems gem = boardStateToUpdate[coords];

						if (gem.IsSameTypeAs(BoardGems.Empty)
							|| gemsCoordsToRemove.Contains(coords))
						{
							continue; // дважды не собираем один и тот же камень из-за взрыва черепа
						}

						collapseStage.CollapsedLines.Add(
							new BoardCollapseResult.GemsLine(gem));

						// Специальная обработка сбора взрывающегося черепа
						if (gem.IsSameTypeAs(BoardGems.Skull) && gem.GetCountValue() == 5)
						{
							skullsLvl5CoordsQueue.Enqueue(coords);
						}

						gemsCoordsToRemove.Add(coords);
					}
				}

				for (int i = 0; i < gemsCoordsToRemove.Count; i++)
				{
					boardStateToUpdate[gemsCoordsToRemove[i]] = BoardGems.Empty;
				}

				gemLinesCollapsed.Stages.Add(collapseStage);

				FallGemsOnBoard(boardStateToUpdate);
				ShiftGemsOnBoard(boardStateToUpdate);
			}
			while (!isBoardStateFinal);
		}
	}
}
