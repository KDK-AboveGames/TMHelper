namespace TMHelper.Common.Board
{
	/// <summary>
	/// Модель для хранения состояния доски match-3.
	/// Начало координат находится в левом верхнем углу доски.
	/// Координата левого верхнего угла доски равна 1:1.
	/// Координаты только увеличиваются.
	/// </summary>
	public abstract class BoardState
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

		#region Utils

		public List<BoardCoords> GetCoordsAround(BoardCoords coords)
		{
			List<BoardCoords> coordsAround = new List<BoardCoords>();

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

		#endregion Utils
	}
}
