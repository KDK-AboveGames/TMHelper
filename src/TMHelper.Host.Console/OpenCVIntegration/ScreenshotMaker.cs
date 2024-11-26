using System.Drawing.Imaging;
using TMHelper.BoardParsing.OpenCV;

namespace TMHelper.Host.Console.OpenCVIntegration
{
	internal class ScreenshotMaker : IOpenCVBoardStateImageProvider
	{
		private readonly Rectangle CropArea;

		public ScreenshotMaker(int startX, int startY, int width, int height)
		{
			CropArea = new Rectangle(startX, startY, width, height);
		}

		public byte[] GetBoardStateImagePng()
		{
			Rectangle totalScreenBounds = Rectangle.Empty;
			foreach (Screen screen in Screen.AllScreens)
			{
				totalScreenBounds = Rectangle.FromLTRB(
					Math.Min(totalScreenBounds.Left, screen.Bounds.Left),
					Math.Min(totalScreenBounds.Top, screen.Bounds.Top),
					Math.Max(totalScreenBounds.Right, screen.Bounds.Right),
					Math.Max(totalScreenBounds.Bottom, screen.Bounds.Bottom));
			}

			using Bitmap screenshot = new(totalScreenBounds.Width, totalScreenBounds.Height);
			using Graphics graphics = Graphics.FromImage(screenshot);

			foreach (Screen screen in Screen.AllScreens)
			{
				graphics.CopyFromScreen(
					screen.Bounds.Left,
					screen.Bounds.Top,
					screen.Bounds.Left - totalScreenBounds.Left,
					screen.Bounds.Top - totalScreenBounds.Top,
					screen.Bounds.Size,
					CopyPixelOperation.SourceCopy);
			}

			using Bitmap crop = screenshot.Clone(CropArea, screenshot.PixelFormat);

			using MemoryStream memoryStream = new();
			crop.Save(memoryStream, ImageFormat.Png);

			return memoryStream.ToArray();
		}
	}
}
