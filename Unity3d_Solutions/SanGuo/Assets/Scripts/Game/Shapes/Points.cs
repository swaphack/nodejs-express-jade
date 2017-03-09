using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 离散的点
	/// </summary>
	public struct Points : IShape
	{
		/// <summary>
		/// 点
		/// </summary>
		private Vector2[] _Vertices;

		/// <summary>
		/// 点
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

		/// <summary>
		/// 是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		public bool Contains(Vector2 point)
		{
			int length = _Vertices.Length;
			for (int i = 0; i < length; i++) {
				if (_Vertices[i] == point) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="points">Points.</param>
		/// <param name="vertices">Vertices.</param>
		public void SetVertices(Vector2[] vertices)
		{
			if (vertices == null || vertices.Length == 0) {
				return;
			}
			_Vertices = new Vector2[2];
			Array.Copy (vertices, _Vertices, vertices.Length);
		}
	}
}

