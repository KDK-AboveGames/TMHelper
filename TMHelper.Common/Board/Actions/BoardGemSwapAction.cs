namespace TMHelper.Common.Board.Actions
{
	/// <summary>
	/// Действие, в котором игрок совершил сдвиг камней на доске.
	/// </summary>
	public class BoardGemSwapAction : BoardAction, IEquatable<BoardGemSwapAction>
	{
		public readonly BoardCoords From;
		public readonly BoardGemSwapDirections SwapDirection;
		public readonly BoardCoords To;

		public BoardGemSwapAction(BoardCoords coords, BoardGemSwapDirections swapDirection)
		{
			From = coords;
			SwapDirection = swapDirection;
			To = coords.Move(swapDirection);
		}

		protected override void ApplyActionToBoardState(BoardState boardState)
		{
			(boardState[From], boardState[To]) = (boardState[To], boardState[From]);
		}

		protected override string GetFriendlyActionDescription()
		{
			return $"{From}<-swap->{To}";
		}

		#region Common Overrides

		public static bool operator ==(BoardGemSwapAction left, BoardGemSwapAction right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BoardGemSwapAction left, BoardGemSwapAction right)
		{
			return !left.Equals(right);
		}

		public override bool Equals(object? value)
		{
			if (value is not BoardGemSwapAction coords)
			{
				return false;
			}

			return Equals(coords);
		}

		public bool Equals(BoardGemSwapAction? other)
		{
			if (other is null)
			{
				return false;
			}

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

		#endregion Common Overrides
	}
}
