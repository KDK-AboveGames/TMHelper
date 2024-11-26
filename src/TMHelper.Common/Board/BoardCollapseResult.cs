namespace TMHelper.Common.Board
{
	/// <summary>
	/// Модель для хранения информации и схлопнутых линиях камней на доске.
	/// </summary>
	public class BoardCollapseResult
	{
		/// <summary>
		/// Стадии схлопывания доски в хронологичеком порядке.
		/// Так как при схлопывании линии камней, остальные камни сдвигаются и могут тоже схлопнуться,
		/// если образуют линию длинной больше 3, таким образом доска может схлопываться несколько раз подряд.
		/// </summary>
		public List<CollapseStage> Stages { get; } = new List<CollapseStage>();

		public class CollapseStage
		{
			public List<GemsLine> CollapsedLines { get; } = new List<GemsLine>();
		}

		public readonly struct GemsLine : IEquatable<GemsLine>
		{
			public readonly List<BoardGems> Gems;
			public readonly BoardGems GemBaseType;
			public readonly int GemsTotalCount;

			public GemsLine(BoardGems singleGem)
				: this(new List<BoardGems> { singleGem }) { }

			public GemsLine(List<BoardGems> gemsLine)
			{
				if (gemsLine == null)
				{
					throw new ArgumentNullException(nameof(gemsLine));
				}

				if (gemsLine.Count == 0)
				{
					throw new ArgumentException("Line of gems is empty.", nameof(gemsLine));
				}

				Gems = gemsLine;
				GemBaseType = gemsLine[0].GetBaseType();

				int gemsCount = 0;

				for (int i = 0; i < gemsLine.Count; i++)
				{
					gemsCount += gemsLine[i].GetCountValue();
				}

				gemsCount *= gemsLine.Count switch
				{
					4 => 2, // умножение количества полученных камней в 2 раза, если собрано 4 в ряд
					5 => 3, // умножение количества полученных камней в 3 раза, если собрано 5 в ряд
					6 => 4, // умножение количества полученных камней в 4 раза, если собрано 6 в ряд
					_ => 1  // иначе количество камней остается как есть
				};

				GemsTotalCount = gemsCount;
			}

			public readonly int Length
			{
				get { return Gems.Count; }
			}

			#region Common Overrides

			public static bool operator ==(GemsLine left, GemsLine right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(GemsLine left, GemsLine right)
			{
				return !left.Equals(right);
			}

			public override bool Equals(object? value)
			{
				if (value is not GemsLine other)
				{
					return false;
				}

				return Equals(other);
			}

			public bool Equals(GemsLine other)
			{
				if (GetHashCode() == other.GetHashCode())
				{
					return true;
				}

				return false;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine((int)GemBaseType, Length, GemsTotalCount);
			}

			#endregion Common Overrides
		}
	}
}
