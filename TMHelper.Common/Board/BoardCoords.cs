namespace TMHelper.Common.Board
{
	/// <summary>
	/// Модель для хранения координат на доске match-3.
	/// </summary>
	public struct BoardCoords : IEquatable<BoardCoords>
	{
		public readonly int Row;
		public readonly int Column;

		public BoardCoords(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public readonly BoardCoords Move(BoardGemSwapDirections direction)
		{
			return direction switch
			{
				BoardGemSwapDirections.Up => new BoardCoords(Row - 1, Column),
				BoardGemSwapDirections.Down => new BoardCoords(Row + 1, Column),
				BoardGemSwapDirections.Left => new BoardCoords(Row, Column - 1),
				BoardGemSwapDirections.Right => new BoardCoords(Row, Column + 1),

				_ => throw new ArgumentOutOfRangeException(nameof(direction)),
			};
		}

		#region Common Overrides

		public static bool operator ==(BoardCoords left, BoardCoords right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BoardCoords left, BoardCoords right)
		{
			return !left.Equals(right);
		}

		public override bool Equals(object? value)
		{
			if (value is not BoardCoords other)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(BoardCoords other)
		{
			if (GetHashCode() == other.GetHashCode())
			{
				return true;
			}

			return false;
		}

		public override string ToString()
		{
			return $"{Row},{Column}";
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Row, Column);
		}

		#endregion Common Overrides
	}
}
