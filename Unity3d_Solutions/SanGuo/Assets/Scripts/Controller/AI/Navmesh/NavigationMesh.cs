using System;
using System.Collections.Generic;
using Controller.AI.AStar;
using Game.Shapes;
using UnityEngine;
using Game.Helper;

namespace Controller.AI.Navmesh
{
	/// <summary>
	/// 导航网格
	/// </summary>
	public class NavigationMesh : ASPointPath
	{
		/// <summary>
		/// 关联点
		/// </summary>
		private Dictionary<int, Dictionary<int, float>> _NeighborNodes;
		/// <summary>
		/// 多边形
		/// </summary>
		private Dictionary<int, NavigationNode> _NavNodes;

		public NavigationMesh ()
		{
			_NeighborNodes = new Dictionary<int, Dictionary<int, float>> ();
			_NavNodes = new Dictionary<int, NavigationNode> ();
		}

		/// <summary>
		/// 添加节点
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="center">Center.</param>
		/// <param name="shape">Shape.</param>
		public void AddNode(int id, Vector2 center, IShape shape)
		{
			NavigationNode item = new NavigationNode ();
			item.Position = center;
			item.Shape = shape;

			_NavNodes.Add (id, item);
		}

		/// <summary>
		/// 添加关联
		/// </summary>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		/// <param name="distance">Distance.</param>
		public void AddLink(int first, int second, float distance)
		{
			if (!_NeighborNodes.ContainsKey (first)) {
				_NeighborNodes.Add (first, new Dictionary<int, float> ());
			}

			if (!_NeighborNodes [first].ContainsKey (second)) {
				_NeighborNodes[first].Add (second, distance);
			} else {
				_NeighborNodes [first][second] = distance;
			}
		}

		/// <summary>
		/// 获取节点
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="id">Identifier.</param>
		public NavigationNode GetNode(int id)
		{
			if (!_NavNodes.ContainsKey (id)) {
				return null;
			}

			return _NavNodes [id];
		}

		/// <summary>
		/// 获取包含点的多边形
		/// </summary>
		/// <returns>The polygon.</returns>
		/// <param name="point">Point.</param>
		public NavigationNode GetContainPointShape(Vector2 point)
		{
			foreach (KeyValuePair<int, NavigationNode> item in _NavNodes) {
				if (item.Value != null
					&& item.Value.Shape.Contains (point)) {
					return item.Value;
				}
			}

			return null;
		}

		/// <summary>
		/// 查找一条从开始到结尾的路径
		/// </summary>
		/// <returns>The way.</returns>
		/// <param name="src">Source.</param>
		/// <param name="dest">Destination.</param>
		public List<AStarNode> FindWay(Vector2 src, Vector2 dest)
		{
			if (src == dest) {
				return null;
			}

			// 查找初始网格
			NavigationNode startNode = GetContainPointShape (src);

			// 查找结束网格
			NavigationNode endNode = GetContainPointShape (dest);

			return FindWay (startNode, endNode);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public override bool Init()
		{
			foreach (KeyValuePair<int, NavigationNode> item in _NavNodes) {
				item.Value.Reset ();
			}

			float distance;
			foreach (KeyValuePair<int, Dictionary<int, float>> item in _NeighborNodes) {
				foreach (KeyValuePair<int, float> item2 in item.Value) {
					NavigationNode startNode = GetNode (item.Key);
					NavigationNode endNode = GetNode (item2.Key);
					if (startNode != null && endNode != null) {
						if (item2.Value == 0) {
							distance = Vector3.Distance (startNode.Position, endNode.Position);
						} else {
							distance = item2.Value;
						}
						this.AddNeighborNode (startNode, endNode, distance);
					}
				}
			}

			return true;
		}


		/// <summary>
		/// Releases all resource used by the <see cref="Controller.AI.Navmesh.NavigationMesh"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Controller.AI.Navmesh.NavigationMesh"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Controller.AI.Navmesh.NavigationMesh"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="Controller.AI.Navmesh.NavigationMesh"/> so the garbage collector can reclaim the memory that the
		/// <see cref="Controller.AI.Navmesh.NavigationMesh"/> was occupying.</remarks>
		public override void Dispose()
		{
			base.Dispose ();
			_NavNodes.Clear ();
			_NeighborNodes.Clear ();
		}
	}
}

