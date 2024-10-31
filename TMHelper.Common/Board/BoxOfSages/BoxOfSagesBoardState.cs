namespace TMHelper.Common.Board.BoxOfSages
{
	/// <summary>
	/// Модель для хранения состояния доски match-3 шкатулки мудрецов.
	/// Начало координат находится в левом верхнем углу доски.
	/// Координата левого верхнего угла доски равна 1:1.
	/// Координаты только увеличиваются.
	/// </summary>
	public class BoxOfSagesBoardState : BoardState, ICloneable
	{
		public BoxOfSagesBoardState()
			: base(8, 8)
		{ }

		public object Clone()
		{
			BoxOfSagesBoardState clone = new BoxOfSagesBoardState();

			Array.Copy(Gems, clone.Gems, Gems.Length);

			return clone;
		}
	}
}
