using TMHelper.Common.Board.Battle;

namespace TMHelper.Host.Console.Battle
{
	/// <summary>
	/// Считывать ввода в консоли для создания модели состояния доски match-3.
	/// Идея заключается в том, что вполне реально за 15 секунд на NumPad'е клавиатуры
	/// ввести значения всех 36 камней на доске.
	/// </summary>
	public class BattleBoardStateConsoleProvider : IBattleBoardStateProvider
	{
		public BattleBoardState GetBoardState()
		{
			throw new NotImplementedException();
		}
	}
}
