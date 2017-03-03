using System;
using UnityEngine;

namespace Game.Shapes.D2
{
	/// <summary>
	/// 三角形
	/// </summary>
	public struct Triangle : Shape
	{
		public Triangle(Vector3 point0, Vector3 point1, Vector3 point2)
		{
			_Points = new Vector3[3];
			this [0] = point0;
			this [1] = point1;
			this [2] = point2;
		}

		/// <summary>
		/// 判断是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		public bool Contains(Vector3 point)
		{
			Vector3 v0 = this[2] - this[0];
			Vector3 v1 = this[1] - this[0];
			Vector3 v2 = point - this[0];

			float dot00 = Vector3.Dot(v0, v0);
			float dot01 = Vector3.Dot(v0, v1);
			float dot02 = Vector3.Dot(v0, v2);
			float dot11 = Vector3.Dot(v0, v1);
			float dot12 = Vector3.Dot(v0, v2);

			float inverDeno = 1 / (dot00 * dot11 - dot01 * dot01);

			float u = (dot11 * dot02 - dot01 * dot12) * inverDeno;
			if (u < 0 || u > 1) { // if u out of range, return directly
				return false;
			}

			float v = (dot00 * dot12 - dot01 * dot02) * inverDeno;
			if (v < 0 || v > 1) { // if v out of range, return directly
				return false;
			}

			return u + v <= 1;
		}
	}
}

