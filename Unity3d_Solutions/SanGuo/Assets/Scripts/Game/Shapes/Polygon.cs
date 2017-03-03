using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;

namespace Game.Shapes.D2
{
	/// <summary>
	/// 2d多边形
	/// 凸多边形
	/// </summary>
	public class Polygon : IShape
	{
		/// <summary>
		/// 顶点坐标
		/// </summary>
		private Vector3[] _Points;
		/// <summary>
		/// 分解的三角形
		/// </summary>
		/// <value>The triangles.</value>
		private Triangle[] _Triangles;
		/// <summary>
		/// 顶点坐标
		/// </summary>
		/// <value>The points.</value>
		public Vector3[] Points { 
			get { 
				return _Points;
			}
			set { 
				SetPoints (value);
			}
		}

		/// <summary>
		/// 分解的三角形
		/// </summary>
		/// <value>The triangles.</value>
		public Triangle[] Triangles {
			get { 
				return _Triangles;
			}
		}

		/// <summary>
		/// 设置顶点
		/// </summary>
		/// <param name="vertexs">Vertexes.</param>
		public void SetPoints(Vector3[] vertexes)
		{
			if (vertexes == null) {
				return;
			}

			int vertexCount = vertexes.Length;
			Log.Assert (vertexCount <= 3, "Error Vertex Count in Create Polygon, count : " + vertexCount);

			// 顶点坐标
			_Points = new Vector3[vertexCount];
			Array.Copy (vertexes, _Points, vertexCount);

			// 三角形
			int triangleCount = vertexCount - 2;
			_Triangles = new Triangle[triangleCount];
			for (int i = 0; i < triangleCount; i++) {
				_Triangles [i] [0] = _Points [0];
				_Triangles [i] [1] = _Points [i + 1];
				_Triangles [i] [2] = _Points [i + 2];
			}
		}

		/// <summary>
		/// 初始化多边形
		/// </summary>
		public Polygon ()
		{
			
		}

		/// <summary>
		/// 判断是否包含点
		/// </summary>
		/// <param name="point">Point.</param>
		public bool Contains(Vector3 point)
		{
			int triangleCount = _Triangles.Length;
			for (int i = 0; i < triangleCount; i++) {
				if (_Triangles [i].Contains (point)) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 判断是否包含线段
		/// </summary>
		/// <param name="ray">Ray.</param>
		public bool Contains (Ray ray)
		{
			return false;
		}

		/// <summary>
		/// 判断是否与线段相交
		/// </summary>
		/// <param name="shape">Shape.</param>
		public bool Intersects(Ray shape)
		{
			return false;
		}

		/// <summary>
		/// 是否包含形状
		/// </summary>
		/// <param name="shape">Shape.</param>
		public bool Contains (IShape shape)
		{
			if (shape == null) {
				return false;
			}

			return false;
		}

		/// <summary>
		/// 判断两多边形是否相交
		/// </summary>
		/// <param name="shape">Shape.</param>
		public bool Intersects(IShape shape)
		{
			if (shape == null) {
				return false;
			}

			if (Points == null
				|| shape.Points == null) {
				return false;
			}

			return false;
		}

		/// <summary>
		/// 是否有相同的边
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="shape">Shape.</param>
		public bool HasCommonEdge (IShape shape)
		{
			if (shape == null) {
				return false;
			}

			if (Points == null
				|| shape.Points == null) {
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
		public List<Ray> GetCommonEdges (IShape shape)
		{
			if (shape == null) {
				return null;
			}

			if (Points == null
				|| shape.Points == null) {
				return null;
			}

			int selfVertexCount = Points.Length;
			int targetVertexCount = shape.Points.Length;

			List<KeyValuePair<int, int>> indexs = new List<KeyValuePair<int, int>>();

			for (int i = 0; i < selfVertexCount; i++) {
				for (int j = 0; j < targetVertexCount; j++) {
					if (shape.Points [j] == Points [i]) {
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
					rayAry.Add(new Ray(Points[lastKey], Points[lastKey + 1]));
				} 
				// 递归到下一个
				lastKey = indexs [i].Key;
				lastValue = indexs [i].Value;
			}

			// 循环,尾部到头部
			if ((indexs [0].Key == 0 && (lastKey == selfVertexCount - 1)) 
				&& (indexs [0].Value == 0 && (lastValue == lastValue - 1))) {
				rayAry.Add(new Ray(Points[lastKey], Points[0]));
			}

			return rayAry;
		}

		/// <summary>
		/// 是否有相同顶点
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="shape">Shape.</param>
		public bool HasCommonVertex (IShape shape)
		{
			if (shape == null) {
				return false;
			}

			if (Points == null
				|| shape.Points == null) {
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
		public List<Vector2> GetCommonVertexes (IShape shape)
		{
			if (shape == null) {
				return null;
			}

			if (Points == null
				|| shape.Points == null) {
				return null;
			}

			int selfVertexCount = Points.Length;
			int targetVertexCount = shape.Points.Length;

			List<Vector2> vertexes = new List<Vector2>();

			for (int i = 0; i < selfVertexCount; i++) {
				for (int j = 0; j < targetVertexCount; j++) {
					if (shape.Points [j] == Points [i]) {
						vertexes.Add (Points [i]);
					}
				}
			}

			return vertexes;
		}
	}
}

