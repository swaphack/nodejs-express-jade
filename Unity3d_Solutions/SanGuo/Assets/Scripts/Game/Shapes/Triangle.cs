using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 三角形
	/// </summary>
	public struct Triangle : IShape
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

		public Triangle(Vector2 point0, Vector2 point1, Vector2 point2)
		{
			_Vertices = new Vector2[3];
			this [0] = point0;
			this [1] = point1;
			this [2] = point2;
		}

		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="points">Points.</param>
		/// <param name="vertices">Vertices.</param>
		public void SetVertices(Vector2[] vertices)
		{
			if (vertices == null || vertices.Length != 3) {
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
			Vector2 v0 = this[2] - this[0];
			Vector2 v1 = this[1] - this[0];
			Vector2 v2 = point - this[0];

			float dot00 = Vector2.Dot(v0, v0);
			float dot01 = Vector2.Dot(v0, v1);
			float dot02 = Vector2.Dot(v0, v2);
			float dot11 = Vector2.Dot(v0, v1);
			float dot12 = Vector2.Dot(v0, v2);

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

