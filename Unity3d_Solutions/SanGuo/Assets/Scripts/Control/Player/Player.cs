using UnityEngine;
using System.Collections;
using Game;

namespace Control
{
	/// <summary>
	/// 玩家
	/// </summary>
	public class Player
	{
		/// <summary>
		/// 资源
		/// </summary>
		private Resource _Resource;

		/// <summary>
		/// 资源
		/// </summary>
		public Resource Resource {
			get { 
				return _Resource;
			}
		}

		/// <summary>
		/// 当前玩家
		/// </summary>
		private static MainPlayer mainPlayer;

		public static MainPlayer MainPlayer
		{
			get { 
				if (mainPlayer == null) {
					mainPlayer = new MainPlayer ();
				}

				return mainPlayer;
			}
		}

		public Player()
		{
			_Resource = new Resource ();			
		}
	}
}
