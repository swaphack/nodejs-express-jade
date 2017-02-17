using System;
using UnityEngine;

namespace Controller.AI
{
	/// <summary>
	/// 任务
	/// </summary>
	public interface ITask
	{
		/// <summary>
		/// 执行者
		/// </summary>
		/// <value>The target.</value>
		Transform Target { get; }
		/// <summary>
		/// 任务是否完成
		/// </summary>
		bool IsFinish { get; }
		/// <summary>
		/// 初始化
		/// </summary>
		void Init();
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update (float dt);
	}
}

