using TMHelper.Common.Board;
using TMHelper.Common.Board.Actions;
using TMHelper.Common.Board.Battle;

namespace TMHelper.BoardSolving.Battle
{
	public class BattleBoardSolver : IBattleBoardSolver
	{
		private static readonly List<BoardGemSwapAction> AllSwapActionsForBoard;

		static BattleBoardSolver()
		{
			List<BoardGemSwapAction> allSwaps = new List<BoardGemSwapAction>();

			for (int row = 1; row <= 6; row++)
			{
				for (int column = 1; column <= 6; column++)
				{
					if (column < 6)
					{
						allSwaps.Add(
							new BoardGemSwapAction(new BoardCoords(row, column), BoardGemSwapDirections.Right));
					}

					if (row < 6)
					{
						allSwaps.Add(
							new BoardGemSwapAction(new BoardCoords(row, column), BoardGemSwapDirections.Down));
					}
				}
			}

			AllSwapActionsForBoard = allSwaps.Distinct()
				.OrderByDescending(
					// более быстрым будет проход по списку из правого нижнего угла, т.к. камни падают слева направо и сверху вниз
					x => x.From.Row + x.From.Column)
				.ToList();
		}


		public List<BoardGemSwapAction> GetAllPossibleSwaps(BattleBoardState boardState)
		{
			List<BoardGemSwapAction> possibleSwaps = new List<BoardGemSwapAction>();

			for (int i = 0; i < AllSwapActionsForBoard.Count; i++)
			{
				BoardGemSwapAction swap = AllSwapActionsForBoard[i];

				if (boardState[swap.From].SameTypeAs(BoardGems.Empty)
					&& boardState[swap.To].SameTypeAs(BoardGems.Empty))
				{
					continue;
				}

				BattleBoardState boardStateAfterAction = swap.CalculateNewBoardState(boardState);

				IEnumerator<SameGemsLine> gemsLinesEnumerator = FindSameGemsLines(boardStateAfterAction);
				if (gemsLinesEnumerator.MoveNext())
				{
					// Если после перемещения камней получилась хотя бы одна линия больше 3 одинаковых камней в ряд,
					// то такое действие с доской считается правильным
					possibleSwaps.Add(swap);
				}
			}

			return possibleSwaps;
		}

		public BattleBoardActionResult ApplyActionAndSolve(BattleBoardState boardState, BoardAction action)
		{
			BattleBoardState initialBoardState = boardState;
			boardState = action.CalculateNewBoardState(initialBoardState);

			// Переменная fallAndShiftRequired отвечает за то,
			// требуется ли сдвинуть все камни в правый нижний угол перед просчетом камней в ряд.
			// При перемещении камней это не требуется, т.к. камень можно переместить вверх на пустую клетку.
			// А при взрыве и пр., требуется, т.к. камни сначала должны упасть, а потом обработаться.
			bool fallAndShiftRequired = action is not BoardGemSwapAction; // todo: сделать через public virtual bool BoardAction.FirstFallAndShiftRequired?

			// Подсчитываем количество уничтоженных камней на случай если камни были уничтожены действием
			GetTotalGems(initialBoardState, out int initialRed, out int initialGreen, out int initialBlue, out int initialSkull);
			GetTotalGems(boardState, out int afterActionRed, out int afterActionGreen, out int afterActionBlue, out int afterActionSkull);
			int redCollected = initialRed > afterActionRed ? initialRed - afterActionRed : 0;
			int greenCollected = initialGreen > afterActionGreen ? initialGreen - afterActionGreen : 0;
			int blueCollected = initialBlue > afterActionBlue ? initialBlue - afterActionBlue : 0;
			int skullCollected = initialSkull > afterActionSkull ? initialSkull - afterActionSkull : 0;

			bool additionalMoveGranted = false;

			bool isBoardStateFinal;
			do
			{
				if (fallAndShiftRequired)
				{
					FallGemsOnBoard(boardState);
					ShiftGemsOnBoard(boardState);
				}

				fallAndShiftRequired = true; // далее каждый цикл перед просчетом необходимо сдвигать камни вправо вниз

				isBoardStateFinal = true;

				List<BoardCoords> gemsCoordsToRemove = new List<BoardCoords>();
				Queue<BoardCoords> skullsLvl5CoordsQueue = new Queue<BoardCoords>();

				IEnumerator<SameGemsLine> gemsLinesEnumerator = FindSameGemsLines(boardState);
				while (gemsLinesEnumerator.MoveNext())
				{
					SameGemsLine gemsLine = gemsLinesEnumerator.Current;

					isBoardStateFinal = false; // т.к. нашлась линия камней, которая схлопнется

					int rowStart = gemsLine.RowStart < gemsLine.RowEnd ? gemsLine.RowStart : gemsLine.RowEnd;
					int rowEnd = gemsLine.RowStart < gemsLine.RowEnd ? gemsLine.RowEnd : gemsLine.RowStart;
					int columnStart = gemsLine.ColumnStart < gemsLine.ColumnEnd ? gemsLine.ColumnStart : gemsLine.ColumnEnd;
					int columnEnd = gemsLine.ColumnStart < gemsLine.ColumnEnd ? gemsLine.ColumnEnd : gemsLine.ColumnStart;

					BoardGems gemsType = boardState[rowStart, columnStart];
					int gemsCount = 0;

					for (int row = rowStart; row <= rowEnd; row++)
					{
						for (int column = columnStart; column <= columnEnd; column++)
						{
							BoardCoords coords = new BoardCoords(row, column);

							if (!gemsCoordsToRemove.Contains(coords))
							{
								gemsCoordsToRemove.Add(coords);
							}

							int gemCountValue = boardState[coords].GetCountValue();

							if (gemCountValue == 5 && gemsType.SameTypeAs(BoardGems.Skull))
							{
								skullsLvl5CoordsQueue.Enqueue(coords);
							}

							gemsCount += gemCountValue;
						}
					}

					gemsCount *= gemsLine.Length switch
					{
						4 => 2, // умножение количества полученных камней в 2 раза, если собрано 4 в ряд
						5 => 3, // умножение количества полученных камней в 3 раза, если собрано 5 в ряд
						6 => 4, // умножение количества полученных камней в 4 раза, если собрано 6 в ряд
						_ => 1  // иначе количество камней остается как есть
					};

					if (gemsType.SameTypeAs(BoardGems.Red))
					{
						redCollected += gemsCount;
					}
					else if (gemsType.SameTypeAs(BoardGems.Green))
					{
						greenCollected += gemsCount;
					}
					else if (gemsType.SameTypeAs(BoardGems.Blue))
					{
						blueCollected += gemsCount;
					}
					else if (gemsType.SameTypeAs(BoardGems.Skull))
					{
						skullCollected += gemsCount;
					}

					if (gemsLine.Length >= 4)
					{
						additionalMoveGranted = true;
					}
				}

				while (skullsLvl5CoordsQueue.Count > 0)
				{
					BoardCoords skullLvl5Coords = skullsLvl5CoordsQueue.Dequeue();

					List<BoardCoords> skullLvl5CoordsAround = boardState.GetCoordsAround(skullLvl5Coords);
					for (int i = 0; i < skullLvl5CoordsAround.Count; i++)
					{
						BoardCoords coords = skullLvl5CoordsAround[i];

						BoardGems gem = boardState[coords];

						if (gem.SameTypeAs(BoardGems.Empty)
							|| gemsCoordsToRemove.Contains(coords))
						{
							continue; // дважды не собираем один и тот же камень из-за взрыва черепа
						}

						int gemCountValue = gem.GetCountValue();
						if (gem.SameTypeAs(BoardGems.Red))
						{
							redCollected += gemCountValue;
						}
						else if (gem.SameTypeAs(BoardGems.Green))
						{
							greenCollected += gemCountValue;
						}
						else if (gem.SameTypeAs(BoardGems.Blue))
						{
							blueCollected += gemCountValue;
						}
						else if (gem.SameTypeAs(BoardGems.Skull))
						{
							skullCollected += gemCountValue;

							if (gemCountValue == 5)
							{
								skullsLvl5CoordsQueue.Enqueue(coords);
							}
						}

						gemsCoordsToRemove.Add(coords);
					}
				}

				for (int i = 0; i < gemsCoordsToRemove.Count; i++)
				{
					boardState[gemsCoordsToRemove[i]] = BoardGems.Empty;
				}
			}
			while (!isBoardStateFinal);

			List<BoardGemSwapAction> resultBoardStateMoves = GetAllPossibleSwaps(boardState);
			bool resultBoardStateHasMoves = resultBoardStateMoves.Count > 0;
			bool resultBoardStateHasSeveralMoves = false;
			bool resultBoardStateAdditionalMovePotential = false;

			if (resultBoardStateHasMoves)
			{
				for (int i = 0; i < resultBoardStateMoves.Count; i++)
				{
					BoardGemSwapAction swap = resultBoardStateMoves[i];

					BattleBoardActionResult swapResult = ApplyActionAndSolve(boardState, swap); // рекурсия порождает лишние вычисления т.к. не все результаты здесь нужны - todo!

					if (swapResult.ResultBoardStateHasMoves)
					{
						resultBoardStateHasSeveralMoves = true;
					}

					if (swapResult.AdditionalMoveGranted)
					{
						resultBoardStateAdditionalMovePotential = true;
					}

					if (resultBoardStateHasSeveralMoves && resultBoardStateAdditionalMovePotential)
					{
						break; // все необходимые данные получены
					}
				}
			}

			return new BattleBoardActionResult(
				initialBoardState,
				action,
				redCollected,
				greenCollected,
				blueCollected,
				skullCollected,
				additionalMoveGranted,
				boardState,
				resultBoardStateHasMoves,
				!resultBoardStateHasSeveralMoves,
				resultBoardStateAdditionalMovePotential);
		}

		#region Utils

		/// <summary>
		/// Модель данных с информацией о нескольких одинаковых камнях в ряд.
		/// </summary>
		private struct SameGemsLine
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

		private static IEnumerator<SameGemsLine> FindSameGemsLines(BoardState boardState)
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

					if (!currentGem.SameTypeAs(prevGem)
						|| currentGem.SameTypeAs(BoardGems.Empty))
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

						if (currentGem.SameTypeAs(BoardGems.Empty))
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

					if (!currentGem.SameTypeAs(prevGem)
						|| currentGem.SameTypeAs(BoardGems.Empty))
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

						if (currentGem.SameTypeAs(BoardGems.Empty))
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
		private static void FallGemsOnBoard(BoardState boardState)
		{
			for (int column = boardState.Columns; column >= 1; column--)
			{
				int? rowCellEmpty = null;
				for (int row = boardState.Rows; row >= 1; row--)
				{
					if (!boardState[row, column].SameTypeAs(BoardGems.Empty))
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
		private static void ShiftGemsOnBoard(BoardState boardState)
		{
			for (int row = boardState.Rows; row >= 1; row--)
			{
				int? columnCellEmpty = null;
				for (int column = boardState.Columns; column >= 1; column--)
				{
					if (!boardState[row, column].SameTypeAs(BoardGems.Empty))
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

		private static void GetTotalGems(
			BoardState boardState,
			out int redTotal,
			out int greenTotal,
			out int blueTotal,
			out int skullTotal)
		{
			redTotal = 0;
			greenTotal = 0;
			blueTotal = 0;
			skullTotal = 0;

			for (int row = 1; row <= boardState.Rows; row++)
			{
				for (int column = 1; column <= boardState.Columns; column++)
				{
					BoardGems gem = boardState[row, column];

					if (gem.SameTypeAs(BoardGems.Red))
					{
						redTotal += gem.GetCountValue();
					}
					else if (gem.SameTypeAs(BoardGems.Green))
					{
						greenTotal += gem.GetCountValue();
					}
					else if (gem.SameTypeAs(BoardGems.Blue))
					{
						blueTotal += gem.GetCountValue();
					}
					else if (gem.SameTypeAs(BoardGems.Skull))
					{
						skullTotal += gem.GetCountValue();
					}
				}
			}
		}

		#endregion Utils
	}
}
