using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 形状接口
	/// </summary>
	public interface IShape
	{
		/// <summary>
		/// 顶点
		/// </summary>
		/// <value>The points.</value>
		Vector2[] Vertices { get; }
		/// <summary>
		/// 是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		bool Contains(Vector2 point);
		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="points">Points.</param>
		void SetVertices (Vector2[] vertices);
	}
}

