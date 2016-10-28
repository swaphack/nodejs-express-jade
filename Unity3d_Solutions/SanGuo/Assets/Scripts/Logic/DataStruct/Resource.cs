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
		/// The food.
		/// </summary>
		public int Food;
		/// <summary>
		/// The iron.
		/// </summary>
		public int Iron;
		/// <summary>
		/// The people.
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