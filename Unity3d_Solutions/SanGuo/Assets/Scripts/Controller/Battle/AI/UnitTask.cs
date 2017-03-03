using System;
using Controller.AI.Task;

namespace Controller.Battle.AI
{
	
	/// <summary>
	/// 单位任务
	/// </summary>
	public class UnitTask : ITask
	{
		/// <summary>
		/// 任务是否完成
		/// </summary>
		private bool _IsFinish;
		/// <summary>
		/// 施法单位
		/// </summary>
		private Unit _Src;
		/// <summary>
		/// 战场
		/// </summary>
		private Field _Field;
		/// <summary>
		/// 开始任务
		/// </summary>
		private OnTaskCallback _OnTaskStart;

		/// <summary>
		/// 更新任务
		/// </summary>
		private OnTaskCallback _OnTaskUpdate;

		/// <summary>
		/// 结束任务
		/// </summary>
		private OnTaskCallback _OnTaskFinish;

		/// <summary>
		/// 施法单位
		/// </summary>
		/// <value>The unit.</value>
		public Unit Src {
			get { 
				return _Src;
			}
			set { 
				_Src = value;
			}
		}

		/// <summary>
		/// 战场
		/// </summary>
		/// <value>The field.</value>
		public Field Field {
			get { 
				return _Field;
			}
			set { 
				_Field = value;
			}
		}

		/// <summary>
		/// 任务是否完成
		/// </summary>
		public bool IsFinish { 
			get { 
				return _IsFinish;
			}
			protected set {
				_IsFinish = value;
			}
		}

		/// <summary>
		/// 开始任务
		/// </summary>
		/// <value>The on task start.</value>
		public OnTaskCallback OnTaskStart { 
			get { 
				return _OnTaskStart;
			}
			set { 
				_OnTaskStart = value;
			}
		}
		/// <summary>
		/// 更新任务
		/// </summary>
		/// <value>The on task update.</value>
		public OnTaskCallback OnTaskUpdate { 
			get { 
				return _OnTaskUpdate;
			}
			set { 
				_OnTaskUpdate = value;
			}
		}
		/// <summary>
		/// 结束任务
		/// </summary>
		/// <value>The on task finish.</value>
		public OnTaskCallback OnTaskFinish { 
			get { 
				return _OnTaskFinish;
			}
			set { 
				_OnTaskFinish = value;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init(ITask task)
		{
			_IsFinish = false;

			if (_OnTaskStart != null) {
				_OnTaskStart (this);
			}

			InitTask (task as UnitTask);
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		/// <param name="task">Task.</param>
		protected virtual void InitTask(UnitTask task) 
		{
			
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public virtual void Update (float dt)
		{
			if (_OnTaskUpdate != null) {
				_OnTaskUpdate (this);
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		public virtual void Dispose()
		{
			if (_OnTaskFinish != null) {
				_OnTaskFinish (this);
			}

			_OnTaskStart = null;
			_OnTaskUpdate = null;
			_OnTaskFinish = null;
		}

		/// <summary>
		/// 获取装换后的任务
		/// </summary>
		/// <returns>The unit task.</returns>
		/// <param name="task">Task.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected T GetUnitTask<T>(UnitTask task) where T : UnitTask
		{
			return task as T;
		}
	}
}

