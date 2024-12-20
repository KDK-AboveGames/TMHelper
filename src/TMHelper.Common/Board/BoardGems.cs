﻿using System.Reflection;

namespace TMHelper.Common.Board
{
	/// <summary>
	/// Камни на игровой match-3 доске.
	/// Имена значений перечисления используются только для удобного отображения в коде.
	/// Для обозначения реальной значимости и типа камня используется атрибут <see cref="BoardGemInfoAttribute"/>
	/// </summary>
	public enum BoardGems
	{
		/// <summary>
		/// Пустая ячейка.
		/// </summary>
		[BoardGemInfo(_, 0)] _,
		[BoardGemInfo(_, 0)] __ = _,
		[BoardGemInfo(_, 0)] Empty = _,

		/// <summary>
		/// Красный (red) камень уровня 1.
		/// </summary>
		[BoardGemInfo(R, 1)] R,
		[BoardGemInfo(R, 1)] Red = R,
		[BoardGemInfo(R, 1)] R1 = R,
		[BoardGemInfo(R, 2)] R2,
		[BoardGemInfo(R, 3)] R3,
		[BoardGemInfo(R, 4)] R4,
		[BoardGemInfo(R, 5)] R5,
		[BoardGemInfo(R, 6)] R6,

		/// <summary>
		/// Зеленый (green) камень уровня 1.
		/// </summary>
		[BoardGemInfo(G, 1)] G,
		[BoardGemInfo(G, 1)] Green = G,
		[BoardGemInfo(G, 1)] G1 = G,
		[BoardGemInfo(G, 2)] G2,
		[BoardGemInfo(G, 3)] G3,
		[BoardGemInfo(G, 4)] G4,
		[BoardGemInfo(G, 5)] G5,
		[BoardGemInfo(G, 6)] G6,

		/// <summary>
		/// Синий камень (blue) уровня 1.
		/// </summary>
		[BoardGemInfo(B, 1)] B,
		[BoardGemInfo(B, 1)] Blue = B,
		[BoardGemInfo(B, 1)] B1 = B,
		[BoardGemInfo(B, 2)] B2,
		[BoardGemInfo(B, 3)] B3,
		[BoardGemInfo(B, 4)] B4,
		[BoardGemInfo(B, 5)] B5,
		[BoardGemInfo(B, 6)] B6,

		/// <summary>
		/// Череп (skull) уровня 1.
		/// </summary>
		[BoardGemInfo(S, 1)] S,
		[BoardGemInfo(S, 1)] Skull = S,
		[BoardGemInfo(S, 1)] S1 = S,
		[BoardGemInfo(S, 2)] S2,
		[BoardGemInfo(S, 3)] S3,
		[BoardGemInfo(S, 4)] S4,
		[BoardGemInfo(S, 5)] S5,
		[BoardGemInfo(S, 6)] S6,

		/// <summary>
		/// Желтый (yellow) камень уровня 1.
		/// </summary>
		[BoardGemInfo(Y, 1)] Y,
		[BoardGemInfo(Y, 1)] Yellow = Y,
		[BoardGemInfo(Y, 1)] Y1 = Y,
		[BoardGemInfo(Y, 2)] Y2,
		[BoardGemInfo(Y, 3)] Y3,
		[BoardGemInfo(Y, 4)] Y4,
		[BoardGemInfo(Y, 5)] Y5,
		[BoardGemInfo(Y, 6)] Y6,

		/// <summary>
		/// Бардовый (maroon) камень уровня 1.
		/// </summary>
		[BoardGemInfo(M, 1)] M,
		[BoardGemInfo(M, 1)] Maroon = M,
		[BoardGemInfo(M, 1)] M1 = M,
		[BoardGemInfo(M, 2)] M2,
		[BoardGemInfo(M, 3)] M3,
		[BoardGemInfo(M, 4)] M4,
		[BoardGemInfo(M, 5)] M5,
		[BoardGemInfo(M, 6)] M6,

		/// <summary>
		/// Фиолетовый (purple) камень уровня 1.
		/// </summary>
		[BoardGemInfo(P, 1)] P,
		[BoardGemInfo(P, 1)] Purple = P,
		[BoardGemInfo(P, 1)] P1 = P,
		[BoardGemInfo(P, 2)] P2,
		[BoardGemInfo(P, 3)] P3,
		[BoardGemInfo(P, 4)] P4,
		[BoardGemInfo(P, 5)] P5,
		[BoardGemInfo(P, 6)] P6,
	}

	/// <summary>
	/// Атрибут для хранения информации о типе и ценности камня на доске match-3.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class BoardGemInfoAttribute : Attribute
	{
		public readonly BoardGems GemBase;
		public readonly int Value;

		/// <param name="gemBaseType">Базовый тип камня.</param>
		/// <param name="gemValueNumber">Сколько камней в одном камне.</param>
		public BoardGemInfoAttribute(BoardGems gemBaseType, int gemValueNumber)
		{
			GemBase = gemBaseType;
			Value = gemValueNumber;
		}
	}

	public static class BoardGemsExtensions
	{
		private static readonly Dictionary<BoardGems, BoardGems> GemsBaseTypes;
		private static readonly Dictionary<BoardGems, int> GemsValues;
		private static readonly Dictionary<BoardGems, Dictionary<int, BoardGems>> GemsWithValuesByTypeAndValue;
		private static readonly Dictionary<BoardGems, string> GemsFriendlyStrings;

		static BoardGemsExtensions()
		{
			GemsBaseTypes = new Dictionary<BoardGems, BoardGems>();
			GemsValues = new Dictionary<BoardGems, int>();
			GemsWithValuesByTypeAndValue = new Dictionary<BoardGems, Dictionary<int, BoardGems>>();
			GemsFriendlyStrings = new Dictionary<BoardGems, string>();

			Type gemInfoAttributeType = typeof(BoardGemInfoAttribute);
			Type gemsEnumType = typeof(BoardGems);
			foreach (BoardGems gemsEnumValue in Enum.GetValues(gemsEnumType))
			{
				if (GemsBaseTypes.ContainsKey(gemsEnumValue))
				{
					continue;
				}

				string memberName = Enum.GetName(gemsEnumType, gemsEnumValue)!;
				MemberInfo memberInfo = gemsEnumType.GetMember(memberName)[0];

				BoardGemInfoAttribute? infoAttribute = memberInfo.GetCustomAttributes(gemInfoAttributeType, false)
					.Cast<BoardGemInfoAttribute>()
					.FirstOrDefault();

				if (infoAttribute is null)
				{
					throw new Exception(
						$"Отсутствует атрибут {nameof(BoardGemInfoAttribute)} у {nameof(BoardGems)}.{gemsEnumValue}");
				}

				GemsBaseTypes.Add(gemsEnumValue, infoAttribute.GemBase);
				GemsValues.Add(gemsEnumValue, infoAttribute.Value);

				if (!GemsWithValuesByTypeAndValue.TryGetValue(infoAttribute.GemBase, out Dictionary<int, BoardGems>? gemsByValue))
				{
					gemsByValue = new Dictionary<int, BoardGems>();
					GemsWithValuesByTypeAndValue.Add(infoAttribute.GemBase, gemsByValue);
				}

				gemsByValue.Add(infoAttribute.Value, gemsEnumValue);

				GemsFriendlyStrings.Add(
					gemsEnumValue,
					gemsEnumValue.IsSameTypeAs(BoardGems.Empty)
						? nameof(BoardGems.__)
						: (gemsEnumValue.ToString().Substring(0, 1) + infoAttribute.Value));
			}
		}

		/// <summary>
		/// Получает тип камня по значению перечисления.
		/// Например, у камня с обозначем R3 тип R.
		/// </summary>
		public static bool IsSameTypeAs(this BoardGems gem, BoardGems other)
		{
			return GemsBaseTypes[gem] == GemsBaseTypes[other];
		}

		/// <summary>
		/// Получает значению базовый тип камня.
		/// </summary>
		public static BoardGems GetBaseType(this BoardGems gem)
		{
			return GemsBaseTypes[gem];
		}

		/// <summary>
		/// Получает значению, показывающее, является ли камень базовым (1-ый уровень, кол-во внутри = 1).
		/// </summary>
		public static bool IsBaseType(this BoardGems gem)
		{
			return gem == GemsBaseTypes[gem];
		}

		/// <summary>
		/// Получает значение количества камней в одной ячейке с камнем.
		/// Обычно бывает х1, х3 (т.е. 3) и х5 (т.е. 5).
		/// </summary>
		public static int GetCountValue(this BoardGems gem)
		{
			return GemsValues[gem];
		}

		/// <summary>
		/// Получает вариант камня, в котором хранится информация о кол-ве камней внутри.
		/// </summary>
		public static BoardGems GetGemWithCountValue(this BoardGems gemType, int countValue)
		{
			return GemsWithValuesByTypeAndValue.TryGetValue(GemsBaseTypes[gemType], out Dictionary<int, BoardGems>? gemsByValue)
				&& gemsByValue.TryGetValue(countValue, out BoardGems gem)
				? gem
				: throw new ArgumentOutOfRangeException(nameof(countValue), $"{gemType},{countValue}");
		}

		public static string ToStringFriendly(this BoardGems gem)
		{
			return GemsFriendlyStrings[gem];
		}
	}
}
