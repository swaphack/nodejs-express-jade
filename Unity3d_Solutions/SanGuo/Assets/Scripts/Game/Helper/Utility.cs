using System;
using System.Collections;
using UnityEngine;

namespace Game
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
			GameInstance.GetInstance ().StartCoroutine (handler);
		}

		/// <summary>
		/// 停止协同程序
		/// </summary>
		/// <param name="handler">Handler.</param>
		public static void StopCoroutine(IEnumerator handler)
		{
			GameInstance.GetInstance ().StopCoroutine (handler);
		}
	}
}

