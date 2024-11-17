namespace TMHelper.Common.Board
{
	/// <summary>
	/// Перечисление идентификаторов дополнительных данных
	/// о результате действия с доской.
	/// </summary>
	public enum BoardActionResultDataKeys
	{
		/// <summary>
		/// Информация о том, привело ли действие к праву совершения еще одного действия.
		/// Например, к схлопыванию ряда из 4+ камней.
		/// Или, например, действие изначально не требовало передачи хода.
		/// </summary>
		AdditionalMoveInfo,


		/// <summary>
		/// Общее количество собранных красных камней (с учетом их уровней).
		/// </summary>
		RedGemsCollectedTotal,
		GreenGemsCollectedTotal,
		BlueGemsCollectedTotal,
		SkullGemsCollectedTotal,
		YellowGemsCollectedTotal,
		MaroonGemsCollectedTotal,
		PurpleGemsCollectedTotal,


		/// <summary>
		/// Есть ли в результирующем состоянии хоть один возможный ход.
		/// В случае, когда ходов нет, произойдет смена доски.
		/// </summary>
		ResultBoardStateHasMoves,

		/// <summary>
		/// В результирующем состоянии любой ход приведет к завершению текущей доски.
		/// Эквивалентно <see cref="resultBoardStateHasMoves"/>=True для любого перемещения камней
		/// на результирующем состоянии доски.
		/// </summary>
		ResultBoardStateHasOneMove,

		/// <summary>
		/// Есть ли в результирующем состоянии такое смещение камней,
		/// который приведет к схлопыванию ряда из 4+ камней,
		/// благодаря чему будет получено право дополнительного хода.
		/// </summary>
		ResultBoardStateAdditionalMoveSwapPotential,


		/// <summary>
		/// Минимальный урон, который вследствие действия наносится противнику сразу.
		/// </summary>
		InstantDamageMin,

		/// <summary>
		/// Максимальный урон, который вследствие действия наносится противнику сразу.
		/// Дополнительно в максимальном уроне должны учитываться шансы на яростный и бронейбоный удары.
		/// </summary>
		InstantDamageMax,

		/// <summary>
		/// Урон, который вследствие действия будет наноситься противнику раз в ход
		/// в течение следующих нескольких ходов.
		/// </summary>
		DamagePerMove,

		/// <summary>
		/// Сколько ходов будет наноситься периодический урон.
		/// </summary>
		DamagePerMoveDuration,


		/// <summary>
		/// Лечение, которое вследствие действия применяется к игроку сразу.
		/// </summary>
		InstantHeal,

		/// <summary>
		/// Лечение, которое вследствие действия будет применяться к игроку раз в ход
		/// в течение следующих нескольких ходов.
		/// </summary>
		HealPerMove,

		/// <summary>
		/// Сколько ходов будет применяться лечение.
		/// </summary>
		HealPerMoveDuration,
	}
}
