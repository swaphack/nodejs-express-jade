using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 线段
	/// </summary>
	public struct Line : IShape
	{
		/// <summary>
		/// 顶点
		/// </summary>
		private Vector2[] _Vertices;

		/// <summary>
		/// 顶点
		/// </summary>
		/// <value>The points.</value>
		public Vector2[] Vertices {
			get { 
				return _Vertices;
			}
		}

		/// <summary>
		/// 索引器
		/// </summary>
		/// <value>The first.</value>
		public Vector2 this[int index] {
			get { 
				if (_Vertices == null
					|| index < 0 
					|| index >= _Vertices.Length) {
					return default(Vector2);
				}
				return _Vertices [index];
			} 
			set {
				if (_Vertices == null
					|| index < 0 
					|| index >= _Vertices.Length) {
					return;
				}
				_Vertices [index] = value;
			}
		}

		public Line(Vector2 point0, Vector2 point1)
		{
			_Vertices = new Vector2[2];
			_Vertices [0] = point0;
			_Vertices [1] = point1;
		}

		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="points">Points.</param>
		/// <param name="vertices">Vertices.</param>
		public void SetVertices(Vector2[] vertices)
		{
			if (vertices == null || vertices.Length != 2) {
				return;
			}
			_Vertices = new Vector2[2];
			Array.Copy (vertices, _Vertices, vertices.Length);
		}


		/// <summary>
		/// 判断是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		public bool Contains(Vector2 point)
		{
			Vector3 v0 = point - this[0];
			Vector3 v1 = this[1] - this[0];

			float dot01 = Vector3.Dot(v0, v1);
			if (dot01 != 0) 
			{
				return false;
			}

			return Math.Min(this[0].x, this[1].x) <= point.x && point.x <= Math.Max(this[0].x, this[1].x)
				&& Math.Min(this[0].y, this[1].y) <= point.y && point.y <= Math.Max(this[0].y, this[1].y);
		}

		/// <summary>
		/// 获取包围盒
		/// </summary>
		/// <returns>The bounds.</returns>
		public Bounds GetBounds()
		{
			Vector2 size = (this [1] - this [0]) * 0.5f;
			Vector2 center = this [0] + size;
			return new Bounds (new Vector3(center.x, center.y, 0), new Vector3(size.x, size.y, 0));
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

			Vector2 v0 = this [0] - line [0];
			Vector2 v1 = line [1] - line [0];
			Vector2 v2 = this [1] - line [1];

			float dot00 = Vector2.Dot(v0, v1);
			float dot01 = Vector2.Dot(v1, v2);

			if (dot00 * dot01 >= 0) {
				return true;
			}
			
			return false;
		}

		/// <summary>
		/// 最近点
		/// </summary>
		/// <returns>The point.</returns>
		/// <param name="point">Point.</param>
		public Vector2 closestPoint(Vector2 point)
		{
			Vector2 result;

			Vector2 src = this [0];
			Vector2 dest = this [1];

			float x0 = dest.x - src.x;
			float y0 = dest.y - src.y;

			if (y0 == 0 && x0 == 0)	{// 线段为点
				return src;
			} else if (x0 == 0)	{ // 平行于y轴
				result.y = point.y > Math.Max(src.y, dest.y) ? Math.Max(src.y, dest.y) : point.y;
				result.y = point.y < Math.Min(src.y, dest.y) ? Math.Min(src.y, dest.y) : point.y;
				result.x = src.x;
			} else if (y0 == 0)	{// 平行于x轴
				result.x = point.x > Math.Max(src.x, dest.x) ? Math.Max(src.x, dest.x) : point.x;
				result.x = point.x < Math.Min(src.x, dest.x) ? Math.Min(src.x, dest.x) : point.x;
				result.y = src.y;
			} else {
				float k = (float)(-((src.x - point.x) * x0 + (src.y - point.y) * y0)
					/ (Math.Pow(x0, 2) + Math.Pow(y0, 2)));

				result.x = k * x0 + src.x;
				result.y = k * y0 + src.y;

				if (!this.Contains(result))	{
					if (Vector2.Distance(src, result) < Vector2.Distance(dest, result))	{
						result = src;
					} else {
						result = dest;
					}
				}
			}

			return result;
		}
	}
}

