using System;
using System.Collections.Generic;

namespace  Controller.AI
{
	/// <summary>
	/// 任务列表
	/// </summary>
	public class TaskSheet
	{
		/// <summary>
		/// 任务队列
		/// </summary>
		private Queue<ITask> _Tasks;

		public TaskSheet ()
		{
			_Tasks = new Queue<ITask> ();
		}

		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="task">Task.</param>
		public void AddTask(ITask task)
		{
			if (task == null) {
				return;
			}

			_Tasks.Enqueue (task);
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (_Tasks.Count == 0) {
				return;
			}

			ITask task = _Tasks.Peek ();
			if (task.IsFinish) {
				_Tasks.Dequeue ();
				return;
			}

			task.Update (dt);
		}

		/// <summary>
		/// 清空所有任务队列
		/// </summary>
		public void Clear()
		{
			_Tasks.Clear ();
		}
	}
}

