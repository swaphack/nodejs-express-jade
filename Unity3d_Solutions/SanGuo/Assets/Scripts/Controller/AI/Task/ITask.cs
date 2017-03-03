using System;
using UnityEngine;

namespace Controller.AI.Task
{
	/// <summary>
	/// 单位任务回调
	/// </summary>
	public delegate void OnTaskCallback(ITask task);

	/// <summary>
	/// 任务
	/// </summary>
	public interface ITask
	{
		/// <summary>
		/// 开始任务
		/// </summary>
		/// <value>The on task start.</value>
		OnTaskCallback OnTaskStart { get; set; }
		/// <summary>
		/// 更新任务
		/// </summary>
		/// <value>The on task update.</value>
		OnTaskCallback OnTaskUpdate { get; set; }
		/// <summary>
		/// 结束任务
		/// </summary>
		/// <value>The on task finish.</value>
		OnTaskCallback OnTaskFinish { get; set; }
		/// <summary>
		/// 任务是否完成
		/// </summary>
		bool IsFinish { get; }
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="target">Target.</param>
		void Init(ITask task);
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update (float dt);
		/// <summary>
		/// 清空
		/// </summary>
		void Dispose();
	}
}

