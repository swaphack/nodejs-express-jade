using System;
using System.Collections.Generic;

namespace Foundation.DataBase
{
	public class DB : IDB
	{
		/// <summary>
		/// 步骤
		/// </summary>
		private List<IDBStep> _Steps;
		/// <summary>
		/// 步骤索引
		/// </summary>
		private int _StepCursor;
		/// <summary>
		/// 表
		/// </summary>
		private Dictionary<string, IDBTable> _Tables;

		public DB ()
		{
			_Steps = new List<IDBStep> ();
			_Tables = new Dictionary<string, IDBTable> ();
			_StepCursor = 0;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			_StepCursor = 0;

			while (_StepCursor < _Steps.Count) 
			{
				IDBStep step = _Steps [_StepCursor];

				step.Load ();

				_StepCursor++;
			}
		}
		/// <summary>
		/// 清空数据
		/// </summary>
		public void Clear()
		{
			this.ClearSteps ();
			this.ClearTables ();
		}
		/// <summary>
		/// 获取表
		/// </summary>
		/// <returns>The table.</returns>
		/// <param name="name">表名称</param>
		public IDBTable GetTable (string name)
		{
			if (_Tables.ContainsKey (name) == true) 
			{
				return _Tables [name];
			}

			return null;
		}
		/// <summary>
		/// 添加表
		/// </summary>
		/// <param name="name">表名称</param>
		/// <param name="table">表</param>
		public void AddTable (string name, IDBTable table)
		{
			_Tables [name] = table;
		}
		/// <summary>
		/// 移除表
		/// </summary>
		/// <param name="name">表名称</param>
		public void RemovetTable (string name)
		{
			if (_Tables.ContainsKey (name) == true) 
			{
				_Tables.Remove (name);
			}
		}
		/// <summary>
		/// 清空表
		/// </summary>
		public void ClearTables()
		{
			_Tables.Clear ();
		}
		/// <summary>
		/// 添加加载步骤
		/// </summary>
		/// <param name="step">加载步骤</param>
		public void AddStep(IDBStep step)
		{
			if (step != null) 
			{
				_Steps.Add (step);
			}
		}
		/// <summary>
		/// 移除加载步骤
		/// </summary>
		/// <param name="name">步骤名称</param>
		public void RemoveStep(string name)
		{
			for (int i = 0; i < _Steps.Count; i++) 
			{
				if (_Steps [i].GetTableName ().Equals (name) == true) 
				{
					_Steps.RemoveAt (i);
					break;
				}
			}
		}
		/// <summary>
		/// 清除所有加载步骤
		/// </summary>
		public void ClearSteps()
		{
			_Steps.Clear ();
		}
	}
}

