using System;
using System.Collections.Generic;
using UnityEngine;
using Controller.AI.AStar;
using Game.Shapes.D2;

namespace Controller.AI.Navmesh
{
	/// <summary>
	/// 导航节点
	/// Position 为多边形的中心点
	/// </summary>
	public class NavigationNode : ASPointNode
	{
		/// <summary>
		/// 编号
		/// </summary>
		public int ID;
		/// <summary>
		/// 多边形
		/// </summary>
		private Polygon _Polygon;
		/// <summary>
		/// 多边形
		/// </summary>
		public Polygon Polygon {
			get { 
				return _Polygon;
			}
		}
		
		public NavigationNode ()
		{
			_Polygon = new Polygon ();
		}
	}
}

