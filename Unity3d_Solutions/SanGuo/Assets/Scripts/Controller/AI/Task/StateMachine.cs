using System;
using System.Collections.Generic;

namespace Controller.AI.Task
{
	/// <summary>
	/// 状态机
	/// </summary>
	public class StateMachine<T>
	{
		/// <summary>
		/// 状态集合
		/// </summary>
		private Dictionary<T, ITask> _Tasks;

		/// <summary>
		/// 当前任务
		/// </summary>
		private ITask _CurrentTask;

		/// <summary>
		/// 状态集合
		/// </summary>
		public Dictionary<T, ITask> Tasks {
			get { 
				return _Tasks;
			}
		}

		public StateMachine ()
		{
			_Tasks = new Dictionary<T, ITask> ();
		}

		/// <summary>
		/// 是否正在执行任务
		/// </summary>
		/// <value><c>true</c> if this instance run task; otherwise, <c>false</c>.</value>
		public bool RunTask {
			get { 
				return _CurrentTask != null;
			}
		}

		/// <summary>
		/// 当前任务
		/// </summary>
		/// <value>The current task.</value>
		protected ITask CurrentTask {
			get { 
				return _CurrentTask;
			} 
			set { 
				_CurrentTask = value;
			}
		}

		/// <summary>
		/// 添加状态
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="task">Task.</param>
		public void AddTask(T state, ITask task)
		{
			if (_Tasks.ContainsKey (state) || task == null) {
				return;
			}

			_Tasks.Add (state, task);
		}

		/// <summary>
		/// 移除状态
		/// </summary>
		/// <param name="state">State.</param>
		public void RemoveTask(T state)
		{
			if (!_Tasks.ContainsKey (state)) {
				return;
			}

			_Tasks.Remove (state);
		}

		/// <summary>
		/// 清空所有状态
		/// </summary>
		public void Clear()
		{
			_Tasks.Clear ();
			_CurrentTask = null;
		}

		/// <summary>
		/// 切换状态
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="target">Target.</param>
		public void Switch(T state)
		{
			if (!_Tasks.ContainsKey (state)) {
				return;
			}

			ITask task = _Tasks[state];
			if (_CurrentTask == task) {
				return;
			}

			_CurrentTask = task;

			if (_CurrentTask != null) {
				_CurrentTask.Init (null);
			}
		}

		/// <summary>
		/// 更新任务
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			if (_CurrentTask == null) {
				return;
			}
			if (_CurrentTask.IsFinish) {
				_CurrentTask.Dispose ();
				_CurrentTask = null;
				return;
			}

			_CurrentTask.Update (dt);
		}

		/// <summary>
		/// 重置
		/// </summary>
		public void Reset()
		{
			if (_CurrentTask != null) {
				_CurrentTask.Dispose ();
			}
			_CurrentTask = null;
		}
	}
}

