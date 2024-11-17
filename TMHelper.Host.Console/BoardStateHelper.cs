using System.Text;
using TMHelper.BoardSolving.Battle;
using TMHelper.BoardSolving.BoxOfSages;
using TMHelper.Common.Board;
using TMHelper.Common.Board.Battle;
using TMHelper.Common.Board.Battle.Actions;
using TMHelper.Common.Board.Battle.Actions.Magic;
using TMHelper.Common.Board.BoxOfSages;
using TMHelper.Common.Board.BoxOfSages.Actions;

namespace TMHelper.Host.Console
{
	public static class BoardStateHelper
	{
		private const string ActionHeader = "Действие";
		private const string MoveInfoHeader = "Ход";
		private const string GemsHeader = "Камни";
		private const string DamageHeader = "Урон";

		#region Battle Board

		private static readonly string[] BattleHeaders = new[] { ActionHeader, MoveInfoHeader, GemsHeader, DamageHeader };
		private static readonly BattleBoardSolver BattleBoardSolver = new();


		public static void PrintSuggestions(BattleBoardState boardState)
		{
			List<BoardActionResult<BattleBoardState>> actionResults = BattleBoardSolver
				.GetAllPossibleSwaps(boardState)
				.Select(swap => new BattleBoardGemSwapAction(swap, BattleBoardSolver).DoAction(boardState))
				.OrderByDescending(
					x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.AdditionalMoveInfo)
						&& x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential))
				.ThenByDescending(x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.AdditionalMoveInfo))
				.ThenByDescending(x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential))
				.ThenByDescending(x => x.ResultsData.GetGemsCollectedTotalCountSafe())
				.ToList();

			actionResults.Insert(
				0,
				new ForceAwakensAction().DoAction(boardState));

			actionResults.Insert(
				0,
				new DoubleAttackAction().DoAction(boardState));

			ConsoleWriter.WriteData(
				BattleHeaders,
				actionResults
					.Select(
						x => new Dictionary<string, ConsoleWriter.ConsoleTableCell>()
							{
								{ ActionHeader, x.Action.ToString() },
								{ MoveInfoHeader, FormatMoveInfo(x.ResultsData) },
								{ GemsHeader, FormatGemsInfo(x.ResultsData, includeSkulls: true) },
								{ DamageHeader, x.ResultsData.GetValueIntSafe(BoardActionResultDataKeys.InstantDamageMin).ToString() },
							})
					.ToList());
		}

		#endregion Battle Board

		#region BoxOfSages Board

		private static readonly string[] BoxOfSagesHeaders = new[] { ActionHeader, MoveInfoHeader, GemsHeader };
		private static readonly BoxOfSagesBoardSolver BoxOfSagesBoardSolver = new();

		public static void PrintSuggestions(BoxOfSagesBoardState boardState)
		{
			List<BoardActionResult<BoxOfSagesBoardState>> actionResults = BoxOfSagesBoardSolver
				.GetAllPossibleSwaps(boardState)
				.Select(swap => new BoxOfSagesBoardGemSwapAction(swap, BoxOfSagesBoardSolver).DoAction(boardState))
				.OrderByDescending(
					x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.AdditionalMoveInfo)
						&& x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential))
				.ThenByDescending(x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.AdditionalMoveInfo))
				.ThenByDescending(x => x.ResultsData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential))
				.ThenByDescending(x => x.ResultsData.GetGemsCollectedTotalCountSafe())
				.ToList();

			ConsoleWriter.WriteData(
				BoxOfSagesHeaders,
				actionResults
					.Select(
						x => new Dictionary<string, ConsoleWriter.ConsoleTableCell>()
							{
								{ ActionHeader, x.Action.ToString() },
								{ MoveInfoHeader, FormatMoveInfo(x.ResultsData) },
								{ GemsHeader, FormatGemsInfo(x.ResultsData, includeSkulls: false) },
							})
					.ToList());
		}

		#endregion BoxOfSages Board

		#region Utils

		private static ConsoleWriter.ConsoleTableCell FormatMoveInfo(
			Dictionary<BoardActionResultDataKeys, string> actionResultData)
		{
			bool additionalMove = actionResultData.GetValueBoolSafe(BoardActionResultDataKeys.AdditionalMoveInfo);

			bool? nextStateAdditionalMovePotential = actionResultData.ContainsKey(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential)
				? actionResultData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateAdditionalMoveSwapPotential)
				: null;

			bool? nextStateMoves = actionResultData.ContainsKey(BoardActionResultDataKeys.ResultBoardStateHasMoves)
				? actionResultData.GetValueBoolSafe(BoardActionResultDataKeys.ResultBoardStateHasMoves)
				: null;

			StringBuilder moveInfoSb = new();
			moveInfoSb.Append(additionalMove ? "+" : "-");

			if (nextStateMoves == false)
			{
				moveInfoSb.Append(" > !");
			}
			else if (nextStateAdditionalMovePotential.HasValue)
			{
				moveInfoSb.Append(nextStateAdditionalMovePotential.Value ? " > +" : " > -");
			}

			return new ConsoleWriter.ConsoleTableCell(
				new ConsoleWriter.ConsoleMessage(moveInfoSb.ToString()));
		}


		private static readonly Dictionary<BoardGems, ConsoleColor> GemsConsoleColors = new()
		{
			{ BoardGems.R, ConsoleColor.Red },
			{ BoardGems.G, ConsoleColor.Green },
			{ BoardGems.B, ConsoleColor.Cyan },
			{ BoardGems.Y, ConsoleColor.Yellow },
			{ BoardGems.M, ConsoleColor.DarkRed },
			{ BoardGems.P, ConsoleColor.Magenta },
		};

		private static readonly Dictionary<BoardGems, BoardActionResultDataKeys> GemsResultDataKeys = new()
		{
			{ BoardGems.R, BoardActionResultDataKeys.RedGemsCollectedTotal },
			{ BoardGems.G, BoardActionResultDataKeys.GreenGemsCollectedTotal },
			{ BoardGems.B, BoardActionResultDataKeys.BlueGemsCollectedTotal },
			{ BoardGems.Y, BoardActionResultDataKeys.YellowGemsCollectedTotal },
			{ BoardGems.M, BoardActionResultDataKeys.MaroonGemsCollectedTotal },
			{ BoardGems.P, BoardActionResultDataKeys.PurpleGemsCollectedTotal },
		};

		private static ConsoleWriter.ConsoleTableCell FormatGemsInfo(
			Dictionary<BoardActionResultDataKeys, string> actionResultData,
			bool includeSkulls)
		{
			return new ConsoleWriter.ConsoleTableCell(
				GemsResultDataKeys
					.Where(x => actionResultData.GetValueIntSafe(x.Value) > 0)
					.Select(
						x => new ConsoleWriter.ConsoleMessage(
							actionResultData.GetValueIntSafe(x.Value) > 0
								? $" +{actionResultData.GetValueIntSafe(x.Value)}".PadLeft(4)
								: new string(' ', 4),
							GemsConsoleColors[x.Key]))
					.Union(new[] { new ConsoleWriter.ConsoleMessage($"({actionResultData.GetGemsCollectedTotalCountSafe()})".PadLeft(5)) })
					.Union(
						includeSkulls
							? new[]
								{
									new ConsoleWriter.ConsoleMessage(
										actionResultData.GetValueIntSafe(BoardActionResultDataKeys.SkullGemsCollectedTotal) > 0
											? $" +{actionResultData.GetValueIntSafe(BoardActionResultDataKeys.SkullGemsCollectedTotal)}ч".PadLeft(5)
											: new string(' ', 5))
								}
							: new ConsoleWriter.ConsoleMessage[0])
					.ToList());
		}

		#endregion Utils
	}
}
