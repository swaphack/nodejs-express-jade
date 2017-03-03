using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;
using Controller.AI.AStar;

namespace Controller.Battle
{	
	/// <summary>
	/// 地图
	/// </summary>
	public class Map
	{
		/// <summary>
		/// 根节点
		/// </summary>
		private Transform _Root;
		/// <summary>
		/// 寻路方式
		/// </summary>
		private ASGridPath _FindWayMethod;
		/// <summary>
		/// 地图项
		/// </summary>
		private List<Transform> _MapItems;
		/// <summary>
		/// 地图大小
		/// </summary>
		private Vector2 _MapSize;

		/// <summary>
		/// 根节点
		/// </summary>
		/// <value>The root.</value>
		public Transform Root {
			get { 
				return _Root;
			}
			set { 
				_Root = value;
			}
		}

		public Map()
		{
			_FindWayMethod = new ASGridPath ();
			_MapItems = new List<Transform> ();
			_MapSize = Vector2.zero;
		}


		/// <summary>
		/// 设置地图大小
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void SetSize(int width, int height) 
		{
			if (width <= 0 || height <= 0) {
				return;
			}
			_MapSize.x = width;
			_MapSize.y = height;
		}


		/// <summary>
		/// 添加对象
		/// </summary>
		/// <param name="transform">Transform.</param>
		public void AddTransform(Transform transform)
		{
			if (transform == null || _Root == null) {
				return;
			}

			transform.SetParent (_Root);

			_MapItems.Add (transform);
		}


		/// <summary>
		/// 移除对象
		/// </summary>
		/// <param name="transform">Transform.</param>
		public void RemoveTransform(Transform transform)
		{
			if (transform == null) {
				return;
			}

			transform.SetParent (null);

			_MapItems.Remove (transform);

			Utility.Destory (transform.gameObject);
		}

		/// <summary>
		/// 查找从起始到目标的路径
		/// </summary>
		/// <returns>The way.</returns>
		/// <param name="src">Source.</param>
		/// <param name="dest">Destination.</param>
		public List<Vector2> FindWay(Transform src, Transform dest) {
			if (src == null || dest == null) {
				return null;
			}

			_FindWayMethod.SetSize ((int)_MapSize.x, (int)_MapSize.y);
			if (!_FindWayMethod.Init ()) {
				return null;
			}

			foreach (Transform target in _MapItems) {
				if (target != src && target !=  dest) {
					Vector2 position = MathHelp.Convert3DTo2D (target.position);
					ASGridNode item = _FindWayMethod.GetGrid (position);
					item.CanPass = false;
				}
			}

			Vector2 srcPos = MathHelp.Convert3DTo2D(src.position);
			Vector2 destPos = MathHelp.Convert3DTo2D(dest.position);
					
			List<AStarNode> path = _FindWayMethod.FindWay (srcPos, destPos);
			if (path == null) {
				return null;
			}

			List<Vector2> way = new List<Vector2> ();
			foreach (AStarNode item in path) {
				ASGridNode node = item as ASGridNode;
				if (node == null) {
					return null;
				}
				way.Add (node.Position);
			}

			return way;
		}

		/// <summary>
		/// 销毁地图
		/// </summary>
		public void Dispose()
		{
			if (_Root == null) {
				return;
			}

			_Root.SetParent (null);
			Utility.Destory (_Root.gameObject);
			_Root = null;
		}
	}
}

