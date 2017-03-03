using System;
using System.Collections.Generic;

namespace Controller.AI.Task
{
	/// <summary>
	/// 任务列表
	/// </summary>
	public class TaskQueue
	{
		/// <summary>
		/// 任务队列
		/// </summary>
		private Queue<ITask> _Tasks;
		/// <summary>
		/// 上一次任务
		/// </summary>
		private ITask _LastTask;

		public TaskQueue ()
		{
			_Tasks = new Queue<ITask> ();
		}

		/// <summary>
		/// 任务数量
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get { 
				return _Tasks.Count;
			}
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
				task.Dispose ();

				_Tasks.Dequeue ();
				ITask newTask = _Tasks.Peek ();
				if (newTask != null) {
					newTask.Init (task);
				}
				return;
			}

			task.Update (dt);
		}

		/// <summary>
		/// 清空队列所有任务
		/// </summary>
		public void Clear()
		{
			_Tasks.Clear ();
		}
	}
}

