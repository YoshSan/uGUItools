using System;
using System.Linq;
using UnityEngine;

namespace Yosh
{
	public static class EnumUtils
	{
		/// <summary>
		/// 指定した列挙体内の定数の値の配列を取得します。
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		public static TEnum[] GetValues<TEnum>() where TEnum : struct
		{
			return Enum.GetValues((typeof(TEnum))).Cast<TEnum>().ToArray();
		}

		/// <summary>
		/// 指定した列挙体内から値を一つランダムに取得します。
		/// </summary>
		public static TEnum PickUpRandom<TEnum>() where TEnum : struct
		{
			var values = GetValues<TEnum>();
			return values.ElementAtOrDefault(UnityEngine.Random.Range(0, values.Count()));
		}
	}

	public static class EnumExtensions
	{
		/// <summary>
		/// 指定した列挙体の値を文字列に変換します。
		/// </summary>
		public static string EnumToString<TEnum>(this TEnum self) where TEnum : struct
		{
			return Enum.GetName(typeof(TEnum), self);
		}

		/// <summary>
		/// 文字列を対応する列挙体の値に変換します。
		/// </summary>
		public static TEnum StringToEnum<TEnum>(this string self) where TEnum : struct
		{
			TEnum result = default(TEnum);

			try {
				result = (TEnum) Enum.Parse(typeof(TEnum), self, true);
			}
			catch (Exception e) {
				Debug.LogError(string.Format("文字列のコンバートに失敗しました {0},\n{1}", self, e));
			}

			return result;
		}

		/// <summary>
		/// 整数値を指定した列挙体の値に変換します。
		/// </summary>
		public static TEnum IntToEnum<TEnum>(this int self) where TEnum : struct
		{
			TEnum result = default(TEnum);

			try {
				result = (TEnum) Enum.ToObject(typeof(TEnum), self);
			}
			catch (Exception e) {
				Debug.LogError(string.Format("数値のコンバートに失敗しました {0},\n{1}", self, e));
			}

			return result;
		}

		/// <summary>
		/// 指定した番号に該当する値を列挙体から取得します。
		/// </summary>
		public static TEnum ElementAtEnum<TEnum>(int index) where TEnum : struct
		{
			TEnum result = default(TEnum);

			try {
				result = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ElementAtOrDefault(index);
			}
			catch (Exception e) {
				Debug.LogError(string.Format("数値のコンバートに失敗しました 型：{0}, {1}", typeof(TEnum), e));
			}

			return result;
		}

	}
}