using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 形状
	/// </summary>
	public struct Shape
	{
		/// <summary>
		/// 三角形顶点
		/// </summary>
		protected Vector3[] _Points;

		/// <summary>
		/// 顶点
		/// </summary>
		/// <value>The points.</value>
		public Vector3[] Points {
			get { 
				return _Points;
			}
		}

		/// <summary>
		/// 索引器
		/// </summary>
		/// <value>The first.</value>
		public Vector3 this[int index] {
			get { 
				if (_Points == null
					|| index < 0 
					|| index >= _Points.Length) {
					return default(Vector2);
				}
				return _Points [index];
			} 
			set {
				if (_Points == null
					|| index < 0 
					|| index >= _Points.Length) {
					return;
				}
				_Points [index] = value;
			}
		}
	}
}

