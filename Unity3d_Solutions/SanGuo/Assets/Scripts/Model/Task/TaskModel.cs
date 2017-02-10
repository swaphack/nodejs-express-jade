using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model.Base;

namespace Model.Task
{
	/// <summary>
	/// 任务
	/// </summary>
	public class TaskModel : ModelItem
	{
		/// <summary>
		/// 任务所需条件
		/// </summary>
		public List<TaskCondition> Conditions;
		/// <summary>
		/// 前置任务
		/// </summary>
		public List<int> PredecessorTasks;
		/// <summary>
		/// 后续任务
		/// </summary>
		public List<int> SuccessorTasks;

		public TaskModel()
		{
			Conditions = new List<TaskCondition> ();
			PredecessorTasks = new List<int> ();
			SuccessorTasks = new List<int> ();
		}
	} 
}