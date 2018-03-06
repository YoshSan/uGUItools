using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
namespace Yosh
{
	/// <summary>
	/// EnumをキーとするGroupButton用のReactiveProperty<bool>管理クラス
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	public class GroupButtonSource<TEnum> : IDisposable where TEnum : struct
	{
		private static GroupButtonSource<TEnum> m_instance;
		public static GroupButtonSource<TEnum> Instance
		{
			get { return m_instance ?? (m_instance = new GroupButtonSource<TEnum>()); }
		}

		//==================================================
		//	変数
		//==================================================
		private CompositeDisposable m_disposer = new CompositeDisposable();
		private Dictionary<TEnum, ReactiveProperty<bool>> m_sharedSource = new Dictionary<TEnum, ReactiveProperty<bool>>();

		//==================================================
		//	プロパティ
		//==================================================
		public int Length { get { return m_sharedSource.Count; } }

		public IReactiveProperty<bool> this[TEnum key]
		{
			get {
				if (!m_sharedSource.ContainsKey(key))
					m_sharedSource.Add(key, new ReactiveProperty<bool>(true).AddTo(m_disposer));

				return m_sharedSource[key].ToReactiveProperty();
			}
		}

		//==================================================
		//	関数
		//==================================================

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private GroupButtonSource()
		{
			TEnum[] keys = EnumUtils.GetValues<TEnum>();
			m_sharedSource = keys.ToDictionary(key => key, _ => new ReactiveProperty<bool>(true).AddTo(m_disposer));
		}

		public void Dispose()
		{
			m_disposer.Dispose();
			m_sharedSource.Clear();
		}

		/// <summary>
		/// 指定した列挙子に対応したReactiveCommandを取得します
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public ReactiveCommand ToReactiveCommand(TEnum key)
		{
			if (!m_sharedSource.ContainsKey(key))
				m_sharedSource.Add(key, new ReactiveProperty<bool>(true).AddTo(m_disposer));
			return m_sharedSource[key].ToReactiveCommand();
		}

		/// <summary>
		/// 指定した列挙子に対応したAsyncReactiveCommandを取得します
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public AsyncReactiveCommand ToAsyncReactiveCommand(TEnum key)
		{
			if (!m_sharedSource.ContainsKey(key))
				m_sharedSource.Add(key, new ReactiveProperty<bool>(true).AddTo(m_disposer));
			return m_sharedSource[key].ToAsyncReactiveCommand();
		}
		
		/// <summary>
		/// 指定した列挙子に対応したReactiveProperty<bool>の値を変更します
		/// </summary>
		/// <param name="key"></param>
		/// <param name="isEnable"></param>
		public void SetEnable(TEnum key, bool isEnable)
		{
			if (!m_sharedSource.ContainsKey(key)) return;

			m_sharedSource[key].Value = isEnable;
		}
	}

	public static class GroupButtonSourceExtensions
	{
		/// <summary>
		/// 指定した列挙子に対応したReactiveProperty<bool>の値を変更します
		/// </summary>
		public static void SetEnable<TEnum>(this TEnum self, bool isEnable) where TEnum : struct
		{
			GroupButtonSource<TEnum>.Instance.SetEnable(self, isEnable);
		}		
	}
}