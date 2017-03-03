using System;
using System.Collections.Generic;
using Controller.AI.Task;
using Model.Base;

namespace Controller.Battle.AI
{
	/// <summary>
	/// 单位行为
	/// </summary>
	public class UnitBehaviour : StateMachine<UnitStateType>
	{
		/// <summary>
		/// 施法单位
		/// </summary>
		private Unit _Target;
		/// <summary>
		/// 战场
		/// </summary>
		private Field _Field;

		/// <summary>
		/// 施法单位
		/// </summary>
		/// <value>The unit.</value>
		public Unit Target {
			get { 
				return _Target;
			}
			set { 
				_Target = value;
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

		public UnitBehaviour ()
		{
		}

		/// <summary>
		/// 添加单位任务
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="task">Task.</param>
		public void AddUnitTask(UnitStateType state, UnitTask task) 
		{
			if (task == null) {
				return;
			}

			task.Field = Field;
			task.Src = Target;

			AddTask (state, task);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			AddUnitTask (UnitStateType.PlaySpell, new SpellCaster ());
		}
	}
}

