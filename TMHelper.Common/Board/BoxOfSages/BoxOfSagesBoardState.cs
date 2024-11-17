namespace TMHelper.Common.Board.BoxOfSages
{
	/// <summary>
	/// Модель для хранения состояния доски match-3 Шкатулки Мудрецов.
	/// Начало координат находится в левом верхнем углу доски.
	/// Координата левого верхнего угла доски равна 1:1.
	/// Координаты только увеличиваются.
	/// </summary>
	public class BoxOfSagesBoardState : BoardState
	{
		public BoxOfSagesBoardState()
			: base(10, 10)
		{ }

		public override object Clone()
		{
			BoxOfSagesBoardState clone = new();

			Array.Copy(Gems, clone.Gems, Gems.Length);

			return clone;
		}
	}
}
