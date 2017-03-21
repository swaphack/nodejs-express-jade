using Controller.AI.AStar;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.AI.Movement
{
	/// <summary>
	/// 指挥官
	/// </summary>
	public interface ICommander
	{
		/// <summary>
		/// 单位
		/// </summary>
		/// <value>The unit.</value>
		IMoveUnit Unit { get; set; }
	}
}

