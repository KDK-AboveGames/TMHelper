using System.Text;

namespace TMHelper.Common.Board
{
	/// <summary>
	/// Модель для хранения состояния доски match-3.
	/// Начало координат находится в левом верхнем углу доски.
	/// Координата левого верхнего угла доски равна 1:1.
	/// Координаты только увеличиваются.
	/// </summary>
	public abstract class BoardState : ICloneable
	{
		public readonly int Rows;
		public readonly int Columns;

		public readonly BoardGems[] Gems;

		protected BoardState(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			Gems = new BoardGems[rows * columns];
		}

		/// <summary>
		/// Получает или задает вид камня на доске по координатам на доске.
		/// </summary>
		public BoardGems this[int row, int column]
		{
			get
			{
				AssertCoordsValid(row, column);

				return Gems[(row - 1) * Columns + (column - 1)];
			}
			set
			{
				AssertCoordsValid(row, column);

				Gems[(row - 1) * Columns + (column - 1)] = value;
			}
		}

		/// <summary>
		/// Получает или задает вид камня на доске по координатам на доске.
		/// </summary>
		public BoardGems this[BoardCoords coords]
		{
			get { return this[coords.Row, coords.Column]; }
			set { this[coords.Row, coords.Column] = value; }
		}

		/// <summary>
		/// Получает или задает вид камня на доске по индексу.
		/// Индекс начинается с 0 и берет отсчет от левого верхнего угла доски,
		/// и идет по строчкам слева направо.
		/// </summary>
		public BoardGems this[int index]
		{
			get
			{
				AssertIndexValid(index);

				return Gems[index];
			}
			set
			{
				AssertIndexValid(index);

				Gems[index] = value;
			}
		}

		public bool IsEmpty
		{
			get
			{
				for (int i = 0; i < Gems.Length; i++)
				{
					if (!Gems[i].IsSameTypeAs(BoardGems.Empty))
					{
						return false;
					}
				}

				return true;
			}
		}

		#region Utils

		/// <summary>
		/// Обновляет переданное состояние доски match-3,
		/// сдвигая все камни по столбцам так, чтобы не оставалось пустых клеток
		/// между камнями и нижней стенкой доски,
		/// а так же между самими камнями в стобце.
		/// </summary>
		public void FallGemsOnBoard()
		{
			for (int column = Columns; column >= 1; column--)
			{
				int rowSlowP = Rows;
				int rowFastP = Rows;

				while (rowFastP > 0)
				{
					if (!this[rowFastP, column].IsSameTypeAs(BoardGems.Empty))
					{
						this[rowSlowP, column] = this[rowFastP, column];

						rowSlowP--;
					}

					rowFastP--;
				}

				while (rowSlowP > 0)
				{
					this[rowSlowP, column] = BoardGems.Empty;
					rowSlowP--;
				}
			}
		}

		/// <summary>
		/// Обновляет переданное состояние доски match-3,
		/// сдвигая все камни по строкам так, чтобы не оставалось пустых клеток
		/// между камнями и правой стенкой доски,
		/// а так же между самими камнями в строке.
		/// </summary>
		public void ShiftGems()
		{
			for (int row = Rows; row >= 1; row--)
			{
				int columnSlowP = Columns;
				int columnFastP = Columns;

				while (columnFastP > 0)
				{
					if (!this[row, columnFastP].IsSameTypeAs(BoardGems.Empty))
					{
						this[row, columnSlowP] = this[row, columnFastP];

						columnSlowP--;
					}

					columnFastP--;
				}

				while (columnSlowP > 0)
				{
					this[row, columnSlowP] = BoardGems.Empty;
					columnSlowP--;
				}
			}
		}


		public int GetGemsOnBoardTotalCount(BoardGems gemBaseType)
		{
			int totalCount = 0;

			for (int row = 1; row <= Rows; row++)
			{
				for (int column = 1; column <= Columns; column++)
				{
					BoardGems gem = this[row, column];

					if (gem.IsSameTypeAs(gemBaseType))
					{
						totalCount += gem.GetCountValue();
					}
				}
			}

			return totalCount;
		}

		public List<BoardCoords> GetCoordsAround(BoardCoords coords)
		{
			List<BoardCoords> coordsAround = new();

			int row;
			int column;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
					{
						continue;
					}

					row = coords.Row + x;
					column = coords.Column + y;

					if (1 <= row && row <= Rows
						&& 1 <= column && column <= Columns)
					{
						coordsAround.Add(new BoardCoords(row, column));
					}
				}
			}

			return coordsAround;
		}

		private void AssertCoordsValid(int row, int column)
		{
			if (row < 1 || row > Rows)
			{
				throw new ArgumentOutOfRangeException(nameof(row));
			}

			if (column < 1 || column > Columns)
			{
				throw new ArgumentOutOfRangeException(nameof(column));
			}
		}

		private void AssertIndexValid(int index)
		{
			if (index < 0 || index >= Gems.Length)
			{
				throw new IndexOutOfRangeException(nameof(index));
			}
		}

		public abstract object Clone();

		public override string ToString()
		{
			StringBuilder sb = new();

			for (int i = 0; i < Gems.Length; i++)
			{
				BoardGems gem = Gems[i];

				sb.Append(gem.ToStringFriendly());

				if ((i + 1) % Columns != 0) // если не последняя ячейка в строке
				{
					sb.Append(", ");
				}
				else if (i < Gems.Length - 1) // если не последняя ячейка во всей таблице
				{
					sb.AppendLine(",");
				}
			}

			return sb.ToString();
		}

		#endregion Utils
	}
}
