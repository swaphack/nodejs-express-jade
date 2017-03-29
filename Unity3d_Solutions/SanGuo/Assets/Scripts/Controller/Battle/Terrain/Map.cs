using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Helper;
using Game.Controller;
using Controller.AI.AStar;
using Controller.Battle.Member;

namespace Controller.Battle.Terrain
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
		private List<MemberTransform> _MapItems;
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
			_MapItems = new List<MemberTransform> ();
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
		public void AddMemberTransform(MemberTransform transform)
		{
			if (transform == null || _Root == null) {
				return;
			}

			transform.Transform.SetParent (_Root);

			_MapItems.Add (transform);

			/*
			ThirdPersonController controller = _Root.gameObject.GetComponent<ThirdPersonController> ();
			if (controller == null) {
				controller = _Root.gameObject.AddComponent<ThirdPersonController> ();
			}
			controller.CameraPosition = new Vector3 (-5, 3, -6);
			*/
		}


		/// <summary>
		/// 移除对象
		/// </summary>
		/// <param name="transform">Transform.</param>
		public void RemoveMemberTransform(MemberTransform transform)
		{
			if (transform == null) {
				return;
			}

			transform.Transform.SetParent (null);

			_MapItems.Remove (transform);

			Utility.Destory (transform.Transform.gameObject);
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
		/// <param name="bCheckBox">If set to <c>true</c> b check box.</param>
		public List<Vector2> FindWay(Vector3 src, Vector3 dest, bool bCheckBox = true) 
		{
			_FindWayMethod.Reset ();
			int count = 0;

			int i;
			count = _MapItems.Count;
			for (i = 0; i < count; i++) {
				if (_MapItems[i].Position != src && _MapItems[i].Position != dest) {
					Vector2 position = MathHelp.Convert3DTo2D (_MapItems[i].Position);
					ASGridNode item = _FindWayMethod.GetGrid (position);
					if (item != null) { item.CanPass = false; }

					// 外围
					if (bCheckBox) {
						int j;
						int length = 0;
						length = _MapItems [i].ExtentsAry.Length;
						for (j = 0; j < length; j++) {
							item = _FindWayMethod.GetGrid (position + _MapItems[i].ExtentsAry[j]);
							if (item != null) { item.CanPass = false; }
						}	
					}
				}
			}



			Vector2 srcPos = MathHelp.Convert3DTo2D(src);
			Vector2 destPos = MathHelp.Convert3DTo2D(dest);

			List<AStarNode> path = _FindWayMethod.FindWay (srcPos, destPos);
			if (path == null || path.Count == 0) {
				if ((int)srcPos.x != (int)destPos.x || (int)srcPos.x != (int)destPos.x) {
					return null;
				} else if (srcPos != destPos) {
					List<Vector2> way = new List<Vector2> ();
					way.Add (srcPos);
					way.Add (destPos);
					return way;
				} else {
					return null;
				}
			} else {
				List<Vector2> way = new List<Vector2> ();
				// 开始位置
				ASGridNode firstNode = path[0] as ASGridNode;
				if (firstNode != null) {
					if (srcPos != firstNode.Position) {
						way.Add (srcPos);
					}
				}


				// 中间节点
				count = path.Count;
				for (i = 0; i < count; i++) {
					ASGridNode node = path [i] as ASGridNode;
					if (node == null) {
						return null;
					}
					way.Add (node.Position);
				}

				// 结束位置
				ASGridNode tailNode = path[count - 1] as ASGridNode;
				if (tailNode != null) {
					if (destPos != tailNode.Position) {
						way.Add (destPos);
					}
				}

				return way;
			}
		}

		/// <summary>
		/// 查找从起始到目标的路径
		/// </summary>
		/// <returns>The way.</returns>
		/// <param name="src">Source.</param>
		/// <param name="dest">Destination.</param>
		public List<Vector2> FindWay(Transform src, Transform dest)
		{
			if (src == null || dest == null) {
				return null;
			}

			return FindWay(src.position, dest.position);
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

