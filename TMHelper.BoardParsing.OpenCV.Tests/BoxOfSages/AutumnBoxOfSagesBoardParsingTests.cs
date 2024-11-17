using TMHelper.BoardParsing.OpenCV.Tests.Properties;
using TMHelper.Common.Board.BoxOfSages;
using static TMHelper.Common.Board.BoardGems;

namespace TMHelper.BoardParsing.OpenCV.Tests.BoxOfSages
{
	internal class AutumnBoxOfSagesBoardParsingTests : BoxOfSagesBoardParsingTestsBase
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
			BoxOfSagesBoardState parsedState = Parse(Resources.Classic_BoxOfSages_01);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					P1, R3, G2, B2, R1, M2, G2, B1, R1, P1,
					M1, R2, Y1, P2, M1, M1, Y1, Y1, G3, G1,
					R3, B3, P1, B2, P2, G1, Y1, G1, M2, M2,
					Y1, M1, P1, P3, R2, B2, G3, B1, R2, P3,
					Y1, R1, Y1, P1, R1, P1, M2, M1, G1, M1,
					R1, P2, M1, G3, G1, R1, B2, Y3, B1, G2,
					B2, B1, G1, B3, G1, P2, M3, B1, R2, Y1,
					Y1, G1, R2, B1, Y1, R1, Y1, G3, M1, G1,
					Y1, P1, G1, G1, R1, R1, G1, G3, M2, P1,
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1));
		}

		[Test]
		public void ParseImage02()
		{
			BoxOfSagesBoardState parsedState = Parse(Resources.Classic_BoxOfSages_02);

			AssertBoardStatesEqual(
				parsedState,
				CreateBoardState(
					P1, G1, P3, M2, Y2, R1, M1, G1, P2, G1,
					M1, Y1, Y1, B1, R1, P1, P3, B1, R1, P1,
					B3, B1, P1, Y1, M1, R3, G2, B1, M2, M2,
					Y1, M1, P1, M1, B2, B1, Y1, G1, R2, P3,
					Y1, R1, Y1, R1, R2, G2, G3, B1, G1, M1,
					R1, P2, M1, R2, R1, M2, B2, Y3, B1, G2,
					B2, B1, G1, P3, G1, B2, M3, B1, R2, Y1,
					Y1, G1, R2, G1, Y1, P1, Y1, G3, M1, G1,
					Y1, P1, G1, G1, R1, P2, G1, G3, M2, P1,
					G1, R2, R1, B3, B1, Y1, Y1, B1, G1, M1));
		}
	}
}
