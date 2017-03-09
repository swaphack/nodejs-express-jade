using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Game.Shapes
{
	/// <summary>
	/// 2d多边形
	/// 凸多边形
	/// </summary>
	public struct Polygon : IShape
	{
		/// <summary>
		/// 顶点
		/// </summary>
		private Vector2[] _Vertices;

		/// <summary>
		/// 顶点
		/// </summary>
		/// <value>The Vertices.</value>
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

		public Polygon(Vector2[] vertexes)
		{
			if (vertexes == null || vertexes.Length == 0) {
				_Vertices = new Vector2[0];
				return;
			}
			int vertexCount = vertexes.Length;
			Log.Assert (vertexCount <= 3, "Error Vertex Count in Create Polygon, count : " + vertexCount);

			// 顶点坐标
			_Vertices = new Vector2[vertexCount];
			Array.Copy (vertexes, _Vertices, vertexCount);
		}

		/// <summary>
		/// 设置点
		/// </summary>
		/// <param name="Vertices">Vertices.</param>
		/// <param name="vertices">Vertices.</param>
		public void SetVertices(Vector2[] vertices)
		{
			if (vertices == null || vertices.Length < 3) {
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
			int sum = 0;
			Vector2 p0;
			Vector2 p1;
			int count = _Vertices.Length;
			for (int i = 0; i < count; i++)
			{		
				p0 = this[i];
				p1 = this[(i + 1) % count];

				// 水平
				if (p0.y == p1.y) continue;

				// 在其延长线上
				if (point.y < Math.Min(p0.y, p1.y)) 
					continue;
				if (point.y >= Math.Max(p0.y, p1.y)) 
					continue;

				// 求解 y=point.y 与 p0p1 的交点
				float x = (point.y - p0.y) * (p1.x - p0.x) / (p1.y - p0.y) + p0.x;
				if (x > point.x) sum++;
			}

			return sum % 2 == 1;
		}

		/// <summary>
		/// 是否有相同的边
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="shape">Shape.</param>
		public bool HasCommonEdge (Polygon shape)
		{
			if (Vertices == null
				|| shape.Vertices == null) {
				return false;
			}

			List<Ray> result = GetCommonEdges (shape);
			if (result == null || result.Count == 0) {
				return false;
			}

			return true;
		}

		/// <summary>
		/// 获取共边
		/// </summary>
		/// <returns>The common edge.</returns>
		/// <param name="shape">Shape.</param>
		public List<Ray> GetCommonEdges (Polygon shape)
		{
			if (Vertices == null
				|| shape.Vertices == null) {
				return null;
			}

			int selfVertexCount = Vertices.Length;
			int targetVertexCount = shape.Vertices.Length;

			List<KeyValuePair<int, int>> indexs = new List<KeyValuePair<int, int>>();

			for (int i = 0; i < selfVertexCount; i++) {
				for (int j = 0; j < targetVertexCount; j++) {
					if (shape.Vertices [j] == Vertices [i]) {
						indexs.Add (new KeyValuePair<int, int> (i, j));
					}
				}
			}

			if (indexs.Count < 2) {
				return null;
			}

			int lastKey = indexs [0].Key;
			int lastValue = indexs [0].Value;

			List<Ray> rayAry = new List<Ray> ();

			for (int i = 1; i < indexs.Count; i++) {
				if ((indexs [i].Key - lastKey == 1) && (indexs [i].Value - lastValue == 1)) {
					rayAry.Add(new Ray(Vertices[lastKey], Vertices[lastKey + 1]));
				} 
				// 递归到下一个
				lastKey = indexs [i].Key;
				lastValue = indexs [i].Value;
			}

			// 循环,尾部到头部
			if ((indexs [0].Key == 0 && (lastKey == selfVertexCount - 1)) 
				&& (indexs [0].Value == 0 && (lastValue == lastValue - 1))) {
				rayAry.Add(new Ray(Vertices[lastKey], Vertices[0]));
			}

			return rayAry;
		}

		/// <summary>
		/// 是否有相同顶点
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="shape">Shape.</param>
		public bool HasCommonVertexes (Polygon shape)
		{
			if (Vertices == null
				|| shape.Vertices == null) {
				return false;
			}

			List<Vector2> vertexes = GetCommonVertexes (shape);
			if (vertexes == null) {
				return false;
			}

			return vertexes.Count > 0;
		}

		/// <summary>
		/// 获取共点
		/// </summary>
		/// <returns>The common vertex.</returns>
		/// <param name="shape">Shape.</param>
		public List<Vector2> GetCommonVertexes (Polygon shape)
		{
			if (Vertices == null
				|| shape.Vertices == null) {
				return null;
			}

			int selfVertexCount = Vertices.Length;
			int targetVertexCount = shape.Vertices.Length;

			List<Vector2> vertexes = new List<Vector2>();

			for (int i = 0; i < selfVertexCount; i++) {
				for (int j = 0; j < targetVertexCount; j++) {
					if (shape.Vertices [j] == Vertices [i]) {
						vertexes.Add (Vertices [i]);
					}
				}
			}

			return vertexes;
		}
	}
}

