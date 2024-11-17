namespace TMHelper.BoardParsing.OpenCV
{
	/// <summary>
	/// Варианты стилизации доски
	/// (картинки камушков на доске могут быть стилизованы под эвент или время года).
	/// </summary>
	public enum BoardVisualStyles
	{
		Classic,
		Halloween,
		Autumn
	}

	/// <summary>
	/// Параметры запуска парсера доски на основе OpenCV.
	/// </summary>
	public class OpenCVBoardStateParserOptions
	{
		/// <summary>
		/// Стилизация доски.
		/// </summary>
		public readonly BoardVisualStyles BoardStyle;

		/// <summary>
		/// Сохранять ли скриншот и результаты парсинга в отдельную папку для будущего возможного использования в тестах.
		/// </summary>
		public readonly bool SaveTestData;

		/// <summary>
		/// Открывать ли отладочную картинку после попытки парсинга.
		/// </summary>
		public readonly bool Debug;

		public OpenCVBoardStateParserOptions(
			BoardVisualStyles boardStyle,
			bool saveTestData = false,
			bool debug = false)
		{
			BoardStyle = boardStyle;

			SaveTestData = saveTestData;
			Debug = debug;
		}
	}
}
