using System;
using UnityEngine;

namespace Game
{
	/// <summary>
	/// 点击派发器
	/// </summary>
	public interface ITouchDispatcher
	{
		/// <summary>
		/// 点击对象
		/// </summary>
		/// <value>The target.</value>
		GameObject Target { get; set; }

		/// <summary>
		/// 点击生效
		/// </summary>
		/// <param name="state">点击状态</param>
		/// <param name="vector">点击点</param>
		void OnDispatchTouch (TouchPhase state, Vector2 vector);
	}
}

