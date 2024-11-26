namespace TMHelper.BoardParsing.OpenCV.Tests
{
	internal class TestBoardStateImageProvider : IOpenCVBoardStateImageProvider
	{
		private byte[]? StoredImage;

		public void PrepareBoardStateImagePng(byte[] image)
		{
			StoredImage = image;
		}

		public byte[] GetBoardStateImagePng()
		{
			return StoredImage ?? throw new InvalidOperationException();
		}
	}
}
