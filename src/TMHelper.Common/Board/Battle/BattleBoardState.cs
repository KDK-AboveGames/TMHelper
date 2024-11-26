namespace TMHelper.Common.Board.Battle
{
	/// <summary>
	/// Модель для хранения состояния доски match-3 обычного боя.
	/// Начало координат находится в левом верхнем углу доски.
	/// Координата левого верхнего угла доски равна 1:1.
	/// Координаты только увеличиваются.
	/// </summary>
	public class BattleBoardState : BoardState
	{
		public BattleBoardState()
			: base(6, 6)
		{ }

		public override object Clone()
		{
			BattleBoardState clone = new();

			Array.Copy(Gems, clone.Gems, Gems.Length);

			return clone;
		}
	}
}
