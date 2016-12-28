using UnityEngine;
using System.Collections;

namespace Logic
{
	/// <summary>
	/// 逻辑管理实例
	/// </summary>
	public class LogicInstance
	{
		/// <summary>
		/// 静态实例
		/// </summary>
		private static LogicInstance s_Instance;
		/// <summary>
		/// 玩家自己数据
		/// </summary>
		private Player _MainPlayer;

		private LogicInstance()
		{
			_MainPlayer = new Player ();
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		public static LogicInstance GetInstance()
		{
			if (s_Instance == null) {
				s_Instance = new LogicInstance ();
			}

			return s_Instance;
		}

		/// <summary>
		/// 获取玩家自己数据
		/// </summary>
		/// <returns>The main player.</returns>
		public Player GetMainPlayer()
		{
			return _MainPlayer;
		}
	}
}