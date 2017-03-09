using System;
using System.Collections.Generic;
using UnityEngine;
using Controller.AI.AStar;
using Game.Shapes;

namespace Controller.AI.Navmesh
{
	/// <summary>
	/// 导航节点
	/// Position 为多边形的中心点
	/// </summary>
	public class NavigationNode : AStarNode
	{
		/// <summary>
		/// 编号
		/// </summary>
		public int ID;
		/// <summary>
		/// 位置
		/// </summary>
		public Vector3 Position;
		/// <summary>
		/// 形状
		/// </summary>
		public IShape Shape;
		
		public NavigationNode ()
		{
		}
	}
}

