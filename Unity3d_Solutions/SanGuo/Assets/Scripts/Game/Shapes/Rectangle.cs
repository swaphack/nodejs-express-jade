using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 矩形
	/// </summary>
	public struct Rectangle : Shape
	{
		public Rectangle(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3)
		{
			_Points = new Vector3[4];
			this [0] = point0;
			this [1] = point1;
			this [2] = point2;
			this [3] = point3;
		}

		/// <summary>
		/// 判断是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		public bool Contains(Vector3 point)
		{
			Vector3 v0 = point - this[0];
			Vector3 v1 = this[1] - this[0];

			float dot01 = Vector3.Dot(v0, v1);
			if (dot01 != 0) 
			{
				return false;
			}

			return Math.Min(this[0].x, this[1].x) <= point.x && point.x <= Math.Max(this[0].x, this[1].x)
				&& Math.Min(this[0].y, this[1].y) <= point.y && point.y <= Math.Max(this[0].y, this[1].y)
				&& Math.Min(this[0].z, this[1].z) <= point.z && point.z <= Math.Max(this[0].z, this[1].z);
		}
	}
}

