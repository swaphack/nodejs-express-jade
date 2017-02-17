using UnityEngine;
using System.Collections;
using Model.Battle;
using Controller.Battle;

namespace Controller.AI
{
	/// <summary>
	/// 施法者
	/// </summary>
	public class Spellcaster
	{
		/// <summary>
		/// 施法单位
		/// </summary>
		private Unit _Unit;
		/// <summary>
		/// 战场
		/// </summary>
		private Field _Field;
		/// <summary>
		/// 任务列表
		/// </summary>
		private TaskSheet _TaskSheet;

		public Spellcaster()
		{
			_TaskSheet = new TaskSheet ();
		}


		/// <summary>
		/// 设置施法单位
		/// </summary>
		/// <param name="unit">Unit.</param>
		public void SetUnit(Unit unit)
		{
			_Unit = unit;
		}

		/// <summary>
		/// 设置战场
		/// </summary>
		/// <param name="field">Field.</param>
		public void SetField(Field field)
		{
			_Field = field;
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			_TaskSheet.Update (dt);
		}
	}
}