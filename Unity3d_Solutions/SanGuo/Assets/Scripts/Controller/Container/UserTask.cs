using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Model;
using Model.Task;
using Controller.Role;

namespace Controller.Container
{
	/// <summary>
	/// 检查是否可完成任务
	/// </summary>
	public delegate bool OnCheckCanFinishTask(TaskCondition condition);
	/// <summary>
	/// 玩家任务
	/// </summary>
	public class UserTask
	{
		/// <summary>
		/// 已完成任务
		/// </summary>
		private HashSet<int> _FinishedTasks;
		/// <summary>
		/// 当前任务
		/// </summary>
		private HashSet<int> _CurrentTasks;

		/// <summary>
		/// 检查是否完成任务的处理
		/// </summary>
		private Dictionary<TaskConditionType, OnCheckCanFinishTask> _CheckCanFinishTaskHandlers;
		
		/// <summary>
		/// 已完成任务
		/// </summary>
		public HashSet<int> FinishedTasks
		{
			get { 
				return _FinishedTasks;
			}
		}
		/// <summary>
		/// 当前任务
		/// </summary>
		public HashSet<int> CurrentTasks{
			get { 
				return _CurrentTasks;
			}
		}

		public UserTask()
		{
			_FinishedTasks = new HashSet<int> ();
			_CurrentTasks = new HashSet<int> ();

			_CheckCanFinishTaskHandlers = new Dictionary<TaskConditionType, OnCheckCanFinishTask> ();
		}

		/// <summary>
		/// 添加检查完成任务的处理
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="handler">Handler.</param>
		public void AddCheckFinishTaskHandler(TaskConditionType type, OnCheckCanFinishTask handler)
		{
			if (handler == null) {
				return;
			}

			_CheckCanFinishTaskHandlers [type] = handler;
		}

		/// <summary>
		/// 检查是否可完成任务
		/// </summary>
		/// <returns><c>true</c>, if can finish task was checked, <c>false</c> otherwise.</returns>
		/// <param name="taskID">Task I.</param>
		public bool CheckCanFinishTask(int taskID)
		{
			TaskModel model = Models.GetInstance ().Task.Find (taskID);
			if (model == null) {
				return false;
			}

			foreach (TaskCondition item in model.Conditions) {
				TaskConditionType type = (TaskConditionType)item.Type;
				if (!_CheckCanFinishTaskHandlers.ContainsKey (type)) {
					return false;
				}

				if (!_CheckCanFinishTaskHandlers [type] (item)) {
					return false;
				}
			}

			return false;
		}

		/// <summary>
		/// 完成任务
		/// </summary>
		/// <param name="taskID">Task I.</param>
		public bool FinishTask(int taskID)
		{
			if (!CheckCanFinishTask (taskID)) {
				return false;
			}

			if (!_CurrentTasks.Contains (taskID)) {
				return false;
			}

			_CurrentTasks.Remove (taskID);
			_FinishedTasks.Add (taskID);

			CheckOpenNextTask (taskID);

			return true;
		}

		/// <summary>
		/// 检查开启下个任务
		/// </summary>
		/// <param name="taskID">Task I.</param>
		private void CheckOpenNextTask(int taskID)
		{
			TaskModel model = Models.GetInstance ().Task.Find (taskID);
			if (model == null) {
				return;
			}
			foreach (int item in model.SuccessorTasks) {
				// 已完成
				if (_FinishedTasks.Contains (item)) {
					continue;
				}
				// 正在进行
				if (_CurrentTasks.Contains (item)) {
					continue;
				}

				// 可领取下个任务检查
				TaskModel nextTask = Models.GetInstance().Task.Find(item);
				if (nextTask != null) {
					bool canTake = true;
					foreach (int item2 in nextTask.PredecessorTasks) {
						// 前置任务未完成
						if (!_FinishedTasks.Contains (item2)) {
							canTake = false;
							break;
						}
					}

					// 可开启任务
					if (canTake) {
						_CurrentTasks.Add (nextTask.ID);
					}
				}
			}
		}
	}
}
