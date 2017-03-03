using System;
using UnityEngine;

namespace Controller.AI.AStar
{
	/// <summary>
	/// 点节点
	/// </summary>
	public class ASPointNode : AStarNode
	{
		/// <summary>
		/// 坐标
		/// </summary>
		public Vector3 Position;

		public ASPointNode ()
		{
		}
	}
}

