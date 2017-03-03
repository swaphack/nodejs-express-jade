using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Game.Shapes
{
	/// <summary>
	/// 形状
	/// </summary>
	public interface IShape
	{
		/// <summary>
		/// 顶点
		/// </summary>
		Vector3[] Points { get; }
		/// <summary>
		/// 判断是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		bool Contains (Vector3 point);
	}
}

