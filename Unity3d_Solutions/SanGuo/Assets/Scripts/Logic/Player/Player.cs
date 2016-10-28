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
		private Currency _Currency;

		/// <summary>
		/// 木材
		/// </summary>
		public int Wood { get { return _Currency.Wood; } set { _Currency.Wood = value; } }

		/// <summary>
		/// The food.
		/// </summary>
		public int Food { get { return _Currency.Food; } set { _Currency.Food = value; } }

		/// <summary>
		/// The iron.
		/// </summary>
		public int Iron { get { return _Currency.Iron; } set { _Currency.Iron = value; } }

		/// <summary>
		/// The people.
		/// </summary>
		public int People { get { return _Currency.People; } set { _Currency.People = value; } }

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init ()
		{
			_Currency.Init ();
		}
	}
}
