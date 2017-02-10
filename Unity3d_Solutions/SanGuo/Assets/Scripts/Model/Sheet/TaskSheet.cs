using System;
using Model.Base;
using Model.Task;

namespace Model.Sheet
{
	/// <summary>
	/// 任务
	/// </summary>
	public class TaskSheet : ModelSheet<TaskModel>
	{
		public TaskSheet() 
			:base(ModelType.Task)
		{
		}
	}
}

