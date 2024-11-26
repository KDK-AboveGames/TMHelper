using TMHelper.Common.Board;

namespace TMHelper.BoardSolving
{
	public abstract class BoardSolverBase<TBoardState>
		where TBoardState : BoardState, ICloneable, new()
	{
		protected static readonly List<BoardGemSwap> AllSwapActionsForBoard;

		static BoardSolverBase()
		{
			TBoardState boardState = new();

			List<BoardGemSwap> allSwaps = new();

			for (int row = 1; row <= boardState.Rows; row++)
			{
				for (int column = 1; column <= boardState.Columns; column++)
				{
					if (column < boardState.Columns)
					{
						allSwaps.Add(
							new BoardGemSwap(new BoardCoords(row, column), BoardGemSwapDirections.Right));
					}

					if (row < boardState.Rows)
					{
						allSwaps.Add(
							new BoardGemSwap(new BoardCoords(row, column), BoardGemSwapDirections.Down));
					}
				}
			}

			AllSwapActionsForBoard = allSwaps.Distinct()
				.OrderByDescending(
					// более быстрым будет проход по списку из правого нижнего угла, т.к. камни падают слева направо и сверху вниз
					x => x.From.Row + x.From.Column)
				.ToList();
		}


		public List<BoardGemSwap> GetAllPossibleSwaps(TBoardState boardState)
		{
			List<BoardGemSwap> possibleSwaps = new();

			for (int i = 0; i < AllSwapActionsForBoard.Count; i++)
			{
				BoardGemSwap swap = AllSwapActionsForBoard[i];

				if (boardState[swap.From].IsSameTypeAs(BoardGems.Empty)
					&& boardState[swap.To].IsSameTypeAs(BoardGems.Empty))
				{
					continue;
				}

				TBoardState boardStateClone = (TBoardState)boardState.Clone();
				swap.ApplySwap(boardStateClone);

				IEnumerator<SameGemsLine> gemsLinesEnumerator = FindSameGemsLines(boardStateClone);
				if (gemsLinesEnumerator.MoveNext())
				{
					// Если после перемещения камней получилась хотя бы одна линия больше 3 одинаковых камней в ряд,
					// то такое действие с доской считается правильным
					possibleSwaps.Add(swap);
				}
			}

			return possibleSwaps;
		}

		#region Utils

		/// <summary>
		/// Модель данных с информацией о нескольких одинаковых камнях в ряд.
		/// </summary>
		protected struct SameGemsLine
		{
			public readonly int RowStart;
			public readonly int ColumnStart;

			public readonly int RowEnd;
			public readonly int ColumnEnd;

			public readonly int Length;
			public readonly BoardGems Gem;

			public SameGemsLine(
				int rowStart,
				int columnStart,
				int rowEnd,
				int columnEnd,
				BoardGems gem)
			{
				RowStart = rowStart;
				ColumnStart = columnStart;
				RowEnd = rowEnd;
				ColumnEnd = columnEnd;

				Length = 1 + Math.Abs(rowEnd - rowStart + columnEnd - columnStart);

				Gem = gem;
			}
		}

		protected static IEnumerator<SameGemsLine> FindSameGemsLines(BoardState boardState)
		{
			BoardGems currentGem;
			BoardGems prevGem;
			int sameGemsInLineCount;
			int sameGemsInLineStartRow;
			int sameGemsInLineStartColumn;
			int sameGemsInLineEndRow;
			int sameGemsInLineEndColumn;

			// Проверяем столбцы снизу свверх справа налево, т.к. в противоположном углу доски могут быть преимущественно пустые клетки
			for (int column = boardState.Columns; column >= 1; column--)
			{
				sameGemsInLineStartColumn = column;
				sameGemsInLineEndColumn = column;

				sameGemsInLineStartRow = boardState.Rows;
				sameGemsInLineCount = 0;
				prevGem = BoardGems.Empty;

				for (int row = boardState.Rows; row >= 1; row--)
				{
					currentGem = boardState[row, column];

					if (!currentGem.IsSameTypeAs(prevGem)
						|| currentGem.IsSameTypeAs(BoardGems.Empty))
					{
						if (sameGemsInLineCount >= 3)
						{
							yield return new SameGemsLine(
								sameGemsInLineStartRow,
								sameGemsInLineStartColumn,
								row + 1, // окончание линии на предыдущей строке
								sameGemsInLineEndColumn,
								prevGem);
						}

						if (currentGem.IsSameTypeAs(BoardGems.Empty))
						{
							break; // дальше проверять нет смысла, т.к. там будут одни пустые клетки
						}

						sameGemsInLineStartRow = row;
						sameGemsInLineCount = 0;
					}

					sameGemsInLineCount++;

					if (row == 1) // последняя строка, а значит и окончание линии одинаковых камней подряд
					{
						if (sameGemsInLineCount >= 3)
						{
							yield return new SameGemsLine(
								sameGemsInLineStartRow,
								sameGemsInLineStartColumn,
								row, // окончание линии на этой строке
								sameGemsInLineEndColumn,
								prevGem);
						}
					}

					prevGem = currentGem;
				}
			}

			// Проверяем строки справа налево снизу свверх, т.к. в противоположном углу доски могут быть преимущественно пустые клетки
			for (int row = boardState.Rows; row >= 1; row--)
			{
				sameGemsInLineStartRow = row;
				sameGemsInLineEndRow = row;

				sameGemsInLineStartColumn = boardState.Columns;
				sameGemsInLineCount = 0;
				prevGem = BoardGems.Empty;

				for (int column = boardState.Columns; column >= 1; column--)
				{
					currentGem = boardState[row, column];

					if (!currentGem.IsSameTypeAs(prevGem)
						|| currentGem.IsSameTypeAs(BoardGems.Empty))
					{
						if (sameGemsInLineCount >= 3)
						{
							yield return new SameGemsLine(
								sameGemsInLineStartRow,
								sameGemsInLineStartColumn,
								sameGemsInLineEndRow,
								column + 1, // окончание линии на предыдущем столбце
								prevGem);
						}

						if (currentGem.IsSameTypeAs(BoardGems.Empty))
						{
							break; // дальше проверять нет смысла, т.к. там будут одни пустые клетки
						}

						sameGemsInLineStartColumn = column;
						sameGemsInLineCount = 0;
					}

					sameGemsInLineCount++;

					if (column == 1) // последний стобец, а значит и окончание линии одинаковых камней подряд
					{
						if (sameGemsInLineCount >= 3)
						{
							yield return new SameGemsLine(
								sameGemsInLineStartRow,
								sameGemsInLineStartColumn,
								sameGemsInLineEndRow,
								column, // окончание линии на этом столбце
								prevGem);
						}
					}

					prevGem = currentGem;
				}
			}
		}

		/// <summary>
		/// Обновляет переданное состояние доски match-3,
		/// сдвигая все камни по столбцам так, чтобы не оставалось пустых клеток
		/// между камнями и нижней стенкой доски,
		/// а так же между самими камнями в стобце.
		/// </summary>
		protected static void FallGemsOnBoard(TBoardState boardState)
		{
			for (int column = boardState.Columns; column >= 1; column--)
			{
				int? rowCellEmpty = null;
				for (int row = boardState.Rows; row >= 1; row--)
				{
					if (!boardState[row, column].IsSameTypeAs(BoardGems.Empty))
					{
						if (rowCellEmpty.HasValue)
						{
							boardState[rowCellEmpty.Value, column] = boardState[row, column];
							rowCellEmpty = rowCellEmpty.Value - 1;
						}
					}
					else if (!rowCellEmpty.HasValue)
					{
						rowCellEmpty = row;
					}
				}

				if (rowCellEmpty.HasValue)
				{
					for (int rowToClear = rowCellEmpty.Value; rowToClear >= 1; rowToClear--)
					{
						boardState[rowToClear, column] = BoardGems.Empty;
					}
				}
			}
		}

		/// <summary>
		/// Обновляет переданное состояние доски match-3,
		/// сдвигая все камни по строкам так, чтобы не оставалось пустых клеток
		/// между камнями и правой стенкой доски,
		/// а так же между самими камнями в строке.
		/// </summary>
		protected static void ShiftGemsOnBoard(TBoardState boardState)
		{
			for (int row = boardState.Rows; row >= 1; row--)
			{
				int? columnCellEmpty = null;
				for (int column = boardState.Columns; column >= 1; column--)
				{
					if (!boardState[row, column].IsSameTypeAs(BoardGems.Empty))
					{
						if (columnCellEmpty.HasValue)
						{
							boardState[row, columnCellEmpty.Value] = boardState[row, column];
							columnCellEmpty = columnCellEmpty.Value - 1;
						}
					}
					else if (!columnCellEmpty.HasValue)
					{
						columnCellEmpty = column;
					}
				}

				if (columnCellEmpty.HasValue)
				{
					for (int columntToClear = columnCellEmpty.Value; columntToClear >= 1; columntToClear--)
					{
						boardState[row, columntToClear] = BoardGems.Empty;
					}
				}
			}
		}

		#endregion Utils
	}
}
