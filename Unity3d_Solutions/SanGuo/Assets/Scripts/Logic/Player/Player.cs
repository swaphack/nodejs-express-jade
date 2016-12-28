using UnityEngine;
using System.Collections;

namespace Logic
{
	/// <summary>
	/// 玩家
	/// </summary>
	public class Player
	{
		/// <summary>
		/// 货币资源
		/// </summary>
		public Currency Currency;

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init ()
		{
			Currency.Init ();
		}
	}
}
