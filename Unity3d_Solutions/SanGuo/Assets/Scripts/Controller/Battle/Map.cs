using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;
using Game.Controller;
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
		/// 方位偏移
		/// </summary>
		/// <value>The root.</value>
		private Vector2[] _DirectionOffsets;

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

			_DirectionOffsets = new Vector2[4];
			_DirectionOffsets [0] = new Vector2 (-0.5f, -0.5f);
			_DirectionOffsets [1] = new Vector2 (-0.5f, 0.5f);
			_DirectionOffsets [2] = new Vector2 (0.5f, -0.5f);
			_DirectionOffsets [3] = new Vector2 (0.5f, 0.5f);
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

			ThirdPersonController controller = _Root.gameObject.GetComponent<ThirdPersonController> ();
			if (controller == null) {
				controller = _Root.gameObject.AddComponent<ThirdPersonController> ();
			}
			controller.CameraPosition = new Vector3 (-5, 3, -6);
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
		/// 初始化
		/// </summary>
		public bool Init()
		{
			if (_FindWayMethod.Loaded) {
				return true;
			}
			_FindWayMethod.SetSize ((int)_MapSize.x, (int)_MapSize.y);
			if (!_FindWayMethod.Init ()) {
				return false;
			}

			return true;
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

			_FindWayMethod.Reset ();
			int count = 0;

			count = _MapItems.Count;
			for (int i = 0; i < count; i++) {
				if (_MapItems[i] != src) {
					Vector2 position = MathHelp.Convert3DTo2D (_MapItems[i].position);
					ASGridNode item = _FindWayMethod.GetGrid (position);
					if (item != null) {
						item.CanPass = false;
					}
				}
			}

			Vector2 srcPos = MathHelp.Convert3DTo2D(src.position);
			Vector2 destPos = MathHelp.Convert3DTo2D(dest.position);
					
			List<AStarNode> path = _FindWayMethod.FindWay (srcPos, destPos);
			if (path == null || path.Count == 0) {
				return null;
			}

			List<Vector2> way = new List<Vector2> ();
			/*
			ASGridNode firstNode = path[0] as ASGridNode;
			if (firstNode != null) {
				if (srcPos != firstNode.Position) {
					way.Add (srcPos);
				}
			}
			*/
			count = path.Count;
			for (int i = 0; i < count; i++) {
				ASGridNode node = path[i] as ASGridNode;
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

