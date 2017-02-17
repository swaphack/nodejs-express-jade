using System;
using System.Collections;
using UnityEngine;

namespace Game.Helper
{
	/// <summary>
	/// 一些常用方法
	/// </summary>
	public class Utility
	{
		private Utility ()
		{
		}

		/// <summary>
		/// 执行协同程序
		/// </summary>
		/// <param name="handler">Handler.</param>
		public static void RunCoroutine(IEnumerator handler)
		{
			if (handler == null) {
				return;
			}
			GameInstance.GetInstance ().StartCoroutine (handler);
		}

		/// <summary>
		/// 停止协同程序
		/// </summary>
		/// <param name="handler">Handler.</param>
		public static void StopCoroutine(IEnumerator handler)
		{
			if (handler == null) {
				return;
			}

			GameInstance.GetInstance ().StopCoroutine (handler);
		}

		/// <summary>
		/// 销毁对象
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public static void Destory(GameObject gameObject)
		{
			if (gameObject == null) {
				return;
			}
			GameObject.Destroy (gameObject);
		}

		/// <summary>
		/// 克隆对象
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		public static GameObject Clone(GameObject gameObject)
		{
			if (gameObject == null) {
				return null;
			}
			return GameObject.Instantiate<GameObject> (gameObject);
		}

		/// <summary>
		/// 获取屏幕分辨率
		/// </summary>
		/// <returns>The screen size.</returns>
		public static Vector2 GetScreenSize()
		{
			return new Vector2 (Screen.width, Screen.height);
			
		}
	}
}

