using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 线段
	/// </summary>
	public struct Line : Shape
	{
		public Line(Vector3 point0, Vector3 point1)
		{
			_Points = new Vector3[2];
			_Points [0] = point0;
			_Points [1] = point1;
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

		/// <summary>
		/// 获取包围盒
		/// </summary>
		/// <returns>The bounds.</returns>
		public Bounds GetBounds()
		{
			Vector3 size = (this [1] - this [0]) * 0.5f;
			Vector3 center = this [0] + size;
			return new Bounds (center, size);
		}

		/// <summary>
		/// 判断是否与线段相交
		/// 另一条线段在此线段的两侧
		/// </summary>
		/// <param name="line">Line.</param>
		public bool Intersects(Line line) 
		{
			Bounds bounds0 = this.GetBounds ();
			Bounds bounds1 = line.GetBounds ();

			if (!bounds0.Intersects (bounds1)) {
				return false;
			}

			Vector3 v0 = this [0] - line [0];
			Vector3 v1 = line [1] - line [0];
			Vector3 v2 = this [1] - line [1];

			float dot00 = Vector3.Dot(v0, v1);
			float dot01 = Vector3.Dot(v1, v2);

			if (dot00 * dot01 >= 0) {
				return true;
			}
			
			return false;
		}
	}
}

