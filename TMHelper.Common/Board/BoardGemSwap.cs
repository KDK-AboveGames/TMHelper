namespace TMHelper.Common.Board
{
	/// <summary>
	/// Информация о смещении камней на доске.
	/// </summary>
	public readonly struct BoardGemSwap : IEquatable<BoardGemSwap>
	{
		public readonly BoardCoords From;
		public readonly BoardGemSwapDirections SwapDirection;
		public readonly BoardCoords To;

		public BoardGemSwap(int row, int column, BoardGemSwapDirections swapDirection)
			: this(new BoardCoords(row, column), swapDirection) { }

		public BoardGemSwap(BoardCoords coords, BoardGemSwapDirections swapDirection)
		{
			From = coords;
			SwapDirection = swapDirection;
			To = coords.Move(swapDirection);
		}

		public void ApplySwap(BoardState boardState)
		{
			(boardState[From], boardState[To]) = (boardState[To], boardState[From]);
		}

		#region Common Overrides

		public static bool operator ==(BoardGemSwap left, BoardGemSwap right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BoardGemSwap left, BoardGemSwap right)
		{
			return !left.Equals(right);
		}

		public override bool Equals(object? value)
		{
			if (value is not BoardGemSwap other)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(BoardGemSwap other)
		{
			if (GetHashCode() == other.GetHashCode())
			{
				return true;
			}

			return false;
		}

		public override int GetHashCode()
		{
			// Приводим параметры сдвига к нормализованному виду,
			// где сдвиг может быть только слева направо или сверху вниз.
			// Таким образом объявленные разными способами сдвиги могут оказаться одними и теми же.
			// Например: сдвиг [2,1] -> [1,1] эквивалентен [1,1] -> [2,1] т.к. камни меняются местами.

			BoardGemSwapDirections directionNormalized = SwapDirection switch
			{
				BoardGemSwapDirections.Up => BoardGemSwapDirections.Down,
				BoardGemSwapDirections.Left => BoardGemSwapDirections.Right,
				_ => SwapDirection
			};

			BoardCoords fromCoordsNormalized = SwapDirection switch
			{
				BoardGemSwapDirections.Up => new BoardCoords(From.Row - 1, From.Column),
				BoardGemSwapDirections.Left => new BoardCoords(From.Row, From.Column - 1),
				_ => From
			};

			return HashCode.Combine(fromCoordsNormalized.Row, fromCoordsNormalized.Column, directionNormalized);
		}

		public override string ToString()
		{
			return $"{From} <-> {To}";
		}

		#endregion Common Overrides
	}
}
