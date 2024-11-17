using TMHelper.BoardParsing.OpenCV.Tests.Properties;
using TMHelper.Common.Board.Battle;
using static TMHelper.Common.Board.BoardGems;

namespace TMHelper.BoardParsing.OpenCV.Tests.Battle
{
	internal class AutumnBattleBoardParsingTests : BattleBoardParsingTestsBase
	{
		protected override BoardVisualStyles ParserBoardStyle
		{
			get { return BoardVisualStyles.Autumn; }
		}

		protected override bool ParserSaveTestData
		{
			get { return false; }
		}

		protected override bool ParserDebug
		{
			get { return false; }
		}

		[Test]
		public void ParseImage01()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_01);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					G1, S1, S1, R1, S1, B3,
					S1, R3, G1, B1, R1, G1,
					B1, R1, B1, B1, S5, S1,
					G1, G1, S3, R1, B1, B1,
					G1, B1, S1, R1, G1, S1,
					R1, G1, G3, B1, B1, G3));
		}

		[Test]
		public void ParseImage02()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_02);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					__, __, __, __, __, B1,
					__, __, __, __, __, G5,
					__, __, __, B1, S1, R1,
					__, __, __, G1, G1, B1,
					G1, B1, S1, B1, G1, R5,
					B3, G1, R1, G1, R1, G3));
		}

		[Test]
		public void ParseImage03()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_03);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					S1, R3, G1, G1, R1, R3,
					G1, B1, S3, S1, R1, G5,
					B3, B1, R1, S1, B1, R1,
					S1, G3, B3, G1, G1, R3,
					B1, G1, R1, B1, R1, B1,
					S1, R1, B1, S1, G5, S1));
		}

		[Test]
		public void ParseImage04()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_04);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					G1, G3, B1, G1, G3, R3,
					B1, R1, R1, B1, G1, B1,
					B1, S1, S1, G1, R1, S1,
					R1, R1, S3, B1, B1, R3,
					R3, G3, G1, S1, R1, S1,
					G1, B1, B1, S5, S1, G1));
		}

		[Test]
		public void ParseImage05()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_05);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					S1, S1, B1, S1, B1, R1,
					B3, B5, S1, G1, B1, B1,
					G3, G1, S1, R1, G3, G1,
					S3, G3, R1, G5, R5, S1,
					B1, R5, G1, S1, G1, R1,
					R1, S1, R3, R1, B1, G1));
		}

		[Test]
		public void ParseImage06()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_06);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					__, __, __, __, __, __,
					__, __, __, __, __, __,
					__, __, __, __, R1, S3,
					__, R5, B1, G1, B1, R1,
					S1, G3, S1, B1, R1, G1,
					S1, B1, R1, R1, S1, B3));
		}

		[Test]
		public void ParseImage07()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_07);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					R1, B1, R1, R1, G1, R3,
					G1, B1, R3, B5, B1, R1,
					G3, G3, S1, B1, S1, S1,
					B1, R1, G1, G1, S1, S1,
					G1, S5, R1, S3, B5, G1,
					B3, S1, R1, G1, S1, B5));
		}

		[Test]
		public void ParseImage08()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_08);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					S1, S1, R3, G3, R3, R5,
					G1, B1, S1, G1, G1, R3,
					S5, B1, R1, B3, G1, G1,
					S1, G1, S3, G1, B1, S1,
					R3, B1, R1, G1, G1, S1,
					G3, S1, B1, B1, R3, B1));
		}

		[Test]
		public void ParseImage09()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_09);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					B3, S1, B3, R1, G1, G3,
					G5, S1, G1, B3, R1, S1,
					G1, G3, R1, R3, G3, S3,
					S1, R1, G1, S1, G1, B1,
					S1, B1, G1, S1, R1, R1,
					R1, B3, R3, B1, B1, G3));
		}

		[Test]
		public void ParseImage10()
		{
			BattleBoardState parsedState = Parse(Resources.Autumn_Battle_10);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					__, __, __, S1, B2, S1,
					__, B3, G3, R2, B2, S1,
					__, S1, R2, G2, G2, R2,
					__, B2, B3, G3, B2, S1,
					__, S1, B2, R2, S1, G2,
					S3, S1, G2, B2, G2, R2));
		}
	}
}
