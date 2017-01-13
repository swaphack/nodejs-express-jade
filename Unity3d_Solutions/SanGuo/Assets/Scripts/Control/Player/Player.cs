using UnityEngine;
using System.Collections;
using Game;
using Foundation.Notify;

namespace Control
{
	/// <summary>
	/// 资源类型
	/// </summary>
	public enum ResType
	{
		Food,
		Wood,
		Iron,
	}
	/// <summary>
	/// 玩家
	/// </summary>
	public class Player
	{
		/// <summary>
		/// 食物
		/// </summary>
		public int Food;
		/// <summary>
		/// 木材
		/// </summary>
		public int Wood;
		/// <summary>
		/// 矿石
		/// </summary>
		public int Iron;

		/// <summary>
		/// 资源改变时的处理
		/// </summary>
		private Notifition<ResType> _ResChangeNotifition;

		public Player()
		{
			_ResChangeNotifition = new Notifition<ResType> ();
		}



		/// <summary>
		/// 当前玩家
		/// </summary>
		private static Player mainPlayer;

		public static Player MainPlayer
		{
			get { 
				if (mainPlayer == null) {
					mainPlayer = new Player ();
				}

				return mainPlayer;
			}
		}
	}
}
