using TMHelper.Common.Board.Battle;
using TMHelper.Common.Board.BoxOfSages;

namespace TMHelper.Tests
{
	public static class GlobalUtils
	{
		#region Data Generation Utils

		public static BattleBoardState CreateBoardState(
			BoardGems cell11, BoardGems cell12, BoardGems cell13, BoardGems cell14, BoardGems cell15, BoardGems cell16,
			BoardGems cell21, BoardGems cell22, BoardGems cell23, BoardGems cell24, BoardGems cell25, BoardGems cell26,
			BoardGems cell31, BoardGems cell32, BoardGems cell33, BoardGems cell34, BoardGems cell35, BoardGems cell36,
			BoardGems cell41, BoardGems cell42, BoardGems cell43, BoardGems cell44, BoardGems cell45, BoardGems cell46,
			BoardGems cell51, BoardGems cell52, BoardGems cell53, BoardGems cell54, BoardGems cell55, BoardGems cell56,
			BoardGems cell61, BoardGems cell62, BoardGems cell63, BoardGems cell64, BoardGems cell65, BoardGems cell66)
		{
			BattleBoardState state = new();

			state[1, 1] = cell11;
			state[1, 2] = cell12;
			state[1, 3] = cell13;
			state[1, 4] = cell14;
			state[1, 5] = cell15;
			state[1, 6] = cell16;

			state[2, 1] = cell21;
			state[2, 2] = cell22;
			state[2, 3] = cell23;
			state[2, 4] = cell24;
			state[2, 5] = cell25;
			state[2, 6] = cell26;

			state[3, 1] = cell31;
			state[3, 2] = cell32;
			state[3, 3] = cell33;
			state[3, 4] = cell34;
			state[3, 5] = cell35;
			state[3, 6] = cell36;

			state[4, 1] = cell41;
			state[4, 2] = cell42;
			state[4, 3] = cell43;
			state[4, 4] = cell44;
			state[4, 5] = cell45;
			state[4, 6] = cell46;

			state[5, 1] = cell51;
			state[5, 2] = cell52;
			state[5, 3] = cell53;
			state[5, 4] = cell54;
			state[5, 5] = cell55;
			state[5, 6] = cell56;

			state[6, 1] = cell61;
			state[6, 2] = cell62;
			state[6, 3] = cell63;
			state[6, 4] = cell64;
			state[6, 5] = cell65;
			state[6, 6] = cell66;

			return state;
		}

		public static BoxOfSagesBoardState CreateBoardState(
			BoardGems cell11, BoardGems cell12, BoardGems cell13, BoardGems cell14, BoardGems cell15, BoardGems cell16, BoardGems cell17, BoardGems cell18, BoardGems cell19, BoardGems cell10,
			BoardGems cell21, BoardGems cell22, BoardGems cell23, BoardGems cell24, BoardGems cell25, BoardGems cell26, BoardGems cell27, BoardGems cell28, BoardGems cell29, BoardGems cell20,
			BoardGems cell31, BoardGems cell32, BoardGems cell33, BoardGems cell34, BoardGems cell35, BoardGems cell36, BoardGems cell37, BoardGems cell38, BoardGems cell39, BoardGems cell30,
			BoardGems cell41, BoardGems cell42, BoardGems cell43, BoardGems cell44, BoardGems cell45, BoardGems cell46, BoardGems cell47, BoardGems cell48, BoardGems cell49, BoardGems cell40,
			BoardGems cell51, BoardGems cell52, BoardGems cell53, BoardGems cell54, BoardGems cell55, BoardGems cell56, BoardGems cell57, BoardGems cell58, BoardGems cell59, BoardGems cell50,
			BoardGems cell61, BoardGems cell62, BoardGems cell63, BoardGems cell64, BoardGems cell65, BoardGems cell66, BoardGems cell67, BoardGems cell68, BoardGems cell69, BoardGems cell60,
			BoardGems cell71, BoardGems cell72, BoardGems cell73, BoardGems cell74, BoardGems cell75, BoardGems cell76, BoardGems cell77, BoardGems cell78, BoardGems cell79, BoardGems cell70,
			BoardGems cell81, BoardGems cell82, BoardGems cell83, BoardGems cell84, BoardGems cell85, BoardGems cell86, BoardGems cell87, BoardGems cell88, BoardGems cell89, BoardGems cell80,
			BoardGems cell91, BoardGems cell92, BoardGems cell93, BoardGems cell94, BoardGems cell95, BoardGems cell96, BoardGems cell97, BoardGems cell98, BoardGems cell99, BoardGems cell90,
			BoardGems cell01, BoardGems cell02, BoardGems cell03, BoardGems cell04, BoardGems cell05, BoardGems cell06, BoardGems cell07, BoardGems cell08, BoardGems cell09, BoardGems cell00)
		{
			BoxOfSagesBoardState state = new();

			state[1, 1] = cell11;
			state[1, 2] = cell12;
			state[1, 3] = cell13;
			state[1, 4] = cell14;
			state[1, 5] = cell15;
			state[1, 6] = cell16;
			state[1, 7] = cell17;
			state[1, 8] = cell18;
			state[1, 9] = cell19;
			state[1, 10] = cell10;

			state[2, 1] = cell21;
			state[2, 2] = cell22;
			state[2, 3] = cell23;
			state[2, 4] = cell24;
			state[2, 5] = cell25;
			state[2, 6] = cell26;
			state[2, 7] = cell27;
			state[2, 8] = cell28;
			state[2, 9] = cell29;
			state[2, 10] = cell20;

			state[3, 1] = cell31;
			state[3, 2] = cell32;
			state[3, 3] = cell33;
			state[3, 4] = cell34;
			state[3, 5] = cell35;
			state[3, 6] = cell36;
			state[3, 7] = cell37;
			state[3, 8] = cell38;
			state[3, 9] = cell39;
			state[3, 10] = cell30;

			state[4, 1] = cell41;
			state[4, 2] = cell42;
			state[4, 3] = cell43;
			state[4, 4] = cell44;
			state[4, 5] = cell45;
			state[4, 6] = cell46;
			state[4, 7] = cell47;
			state[4, 8] = cell48;
			state[4, 9] = cell49;
			state[4, 10] = cell40;

			state[5, 1] = cell51;
			state[5, 2] = cell52;
			state[5, 3] = cell53;
			state[5, 4] = cell54;
			state[5, 5] = cell55;
			state[5, 6] = cell56;
			state[5, 7] = cell57;
			state[5, 8] = cell58;
			state[5, 9] = cell59;
			state[5, 10] = cell50;

			state[6, 1] = cell61;
			state[6, 2] = cell62;
			state[6, 3] = cell63;
			state[6, 4] = cell64;
			state[6, 5] = cell65;
			state[6, 6] = cell66;
			state[6, 7] = cell67;
			state[6, 8] = cell68;
			state[6, 9] = cell69;
			state[6, 10] = cell60;

			state[7, 1] = cell71;
			state[7, 2] = cell72;
			state[7, 3] = cell73;
			state[7, 4] = cell74;
			state[7, 5] = cell75;
			state[7, 6] = cell76;
			state[7, 7] = cell77;
			state[7, 8] = cell78;
			state[7, 9] = cell79;
			state[7, 10] = cell70;

			state[8, 1] = cell81;
			state[8, 2] = cell82;
			state[8, 3] = cell83;
			state[8, 4] = cell84;
			state[8, 5] = cell85;
			state[8, 6] = cell86;
			state[8, 7] = cell87;
			state[8, 8] = cell88;
			state[8, 9] = cell89;
			state[8, 10] = cell80;

			state[9, 1] = cell91;
			state[9, 2] = cell92;
			state[9, 3] = cell93;
			state[9, 4] = cell94;
			state[9, 5] = cell95;
			state[9, 6] = cell96;
			state[9, 7] = cell97;
			state[9, 8] = cell98;
			state[9, 9] = cell99;
			state[9, 10] = cell90;

			state[10, 1] = cell01;
			state[10, 2] = cell02;
			state[10, 3] = cell03;
			state[10, 4] = cell04;
			state[10, 5] = cell05;
			state[10, 6] = cell06;
			state[10, 7] = cell07;
			state[10, 8] = cell08;
			state[10, 9] = cell09;
			state[10, 10] = cell00;

			return state;
		}

		#endregion Data Generation Utils


		public static string ConcatMessages(params string?[] messages)
		{
			return string.Join("; ", messages);
		}
	}
}
