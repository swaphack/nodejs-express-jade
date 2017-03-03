using System;
using System.Collections.Generic;
using Controller.AI.AStar;
using Game.Shapes.D2;
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
		/// 多边形
		/// </summary>
		private Dictionary<int, NavigationNode> _NavNodes;

		public NavigationMesh ()
		{
			_NavNodes = new Dictionary<int, NavigationNode> ();
		}

		/// <summary>
		/// 添加形状
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="center">Center.</param>
		/// <param name="vertexes">Vertexes.</param>
		public void AddPolygon(int id, Vector3 center, Vector3[] vertexes)
		{
			if (vertexes == null || vertexes.Length < 3) {
				return;
			}

			if (_NavNodes.ContainsKey (id)) {
				Log.Warning ("Exists ID in Navigation Mesh, id : " + id);
				return;
			}

			NavigationNode item = new NavigationNode ();
			item.Position = center;
			Array.Copy (vertexes, item.Polygon.Points, vertexes.Length);

			_NavNodes.Add (id, item);
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
		public NavigationNode GetPolygon(Vector2 point)
		{
			foreach (KeyValuePair<int, NavigationNode> item in _NavNodes) {
				if (item.Value != null
					&& item.Value.Polygon != null
					&& item.Value.Polygon.Contains (point)) {
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

			// 查找初始网格，并设置开始位置
			NavigationNode startNode = GetPolygon (src);

			// 查找结束网格，并设置结束位置
			NavigationNode endNode = GetPolygon (dest);

			return FindWay (startNode, endNode);
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
		}
	}
}

