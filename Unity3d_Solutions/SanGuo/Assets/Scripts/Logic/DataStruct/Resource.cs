using UnityEngine;
using System.Collections;

namespace Logic
{
	/// <summary>
	/// 货币资源
	/// </summary>
	public struct Currency
	{
		/// <summary>
		/// 木材
		/// </summary>
		public int Wood;
		/// <summary>
		/// 食物
		/// </summary>
		public int Food;
		/// <summary>
		/// 矿物
		/// </summary>
		public int Iron;
		/// <summary>
		/// 人口
		/// </summary>
		public int People;

		public void Init()
		{
			Wood = 0;
			Food = 0;
			Iron = 0;
			People = 0;
		}
	}

}