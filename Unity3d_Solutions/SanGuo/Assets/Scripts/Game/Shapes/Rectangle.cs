using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 矩形
	/// </summary>
	public struct Rectangle : IShape
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

		public Rectangle(Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3)
		{
			_Vertices = new Vector2[4];
			this [0] = point0;
			this [1] = point1;
			this [2] = point2;
			this [3] = point3;
		}

		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="points">Points.</param>
		/// <param name="vertices">Vertices.</param>
		public void SetVertices(Vector2[] vertices)
		{
			if (vertices == null || vertices.Length != 4) {
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
			Vector2 v0 = point - this[0];
			Vector2 v1 = this[1] - this[0];

			float dot01 = Vector2.Dot(v0, v1);
			if (dot01 != 0) 
			{
				return false;
			}

			return Math.Min(this[0].x, this[1].x) <= point.x && point.x <= Math.Max(this[0].x, this[1].x)
				&& Math.Min(this[0].y, this[1].y) <= point.y && point.y <= Math.Max(this[0].y, this[1].y);
		}
	}
}

