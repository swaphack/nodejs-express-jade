using System.Collections.Generic;
using Controller.AI.Task;
using Model.Base;
using UnityEngine;
using Controller.Battle.Task;

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

		/// <summary>
		/// 是否能行走
		/// </summary>
		/// <value><c>true</c> if this instance is can walk; otherwise, <c>false</c>.</value>
		public bool IsCanWalk {
			get { 
				if (Target.MemberModel.IsPlay (UnitAction.t_attack_01)
					|| Target.MemberModel.IsPlay (UnitAction.t_attack_02)
					|| Target.MemberModel.IsPlay (UnitAction.t_attack_03)
					|| Target.MemberModel.IsPlay (UnitAction.t_getHit)) {
					return false;
				}
				return !Target.MemeberMovement.Empty;
			}
		}

		/// <summary>
		/// 是否正在攻击
		/// </summary>
		/// <value><c>true</c> if this instance is play attack; otherwise, <c>false</c>.</value>
		public bool IsPlayAttack {
			get { 
				if (Target.MemberModel.IsPlay (UnitAction.t_attack_01)
				    || Target.MemberModel.IsPlay (UnitAction.t_attack_02)
				    || Target.MemberModel.IsPlay (UnitAction.t_attack_03)) {
					return true;
				}

				SpellCaster task = CurrentTask as SpellCaster;
				if (task == null) {
					return false;
				}
				return task.IsRunningSKill;
			}
		}

		/// <summary>
		/// 攻击
		/// </summary>
		public void PlayAttack()
		{
			Target.StopWalk (true);
			int i = (int)Random.Range (0, 3);
			if (i == 0) {
				Target.MemberModel.PlayAttack01 ();
			} else if (i == 1) {
				Target.MemberModel.PlayAttack02 ();
			} else {
				Target.MemberModel.PlayAttack03 ();
			}
		}

		/// <summary>
		/// 受击
		/// </summary>
		public void PlayGetHit()
		{
			CurrentTask = null;
			Target.StopWalk ();
			Target.MemberModel.PlayGetHit ();
		}

		/// <summary>
		/// 模拟死亡
		/// </summary>
		public void PlaySimDead()
		{
			if (Target.Property.Dead) {
				return;
			}
			Target.Property.AddHP(-Target.Property.GetValue(PropertyType.CurrentHitPoints));
		}

		/// <summary>
		/// 死亡
		/// </summary>
		public void PlayDie()
		{
			CurrentTask = null;
			Target.StopWalk (true);
			Target.RunDeadEvent ();
			Target.MemberModel.PlayDie ();
		}

		/// <summary>
		/// 胜利
		/// </summary>
		public void PlayWin()
		{
			Target.StopWalk (true);
			int i = (int)Random.Range (0, 2);
			if (i == 0) {
				Target.MemberModel.PlayJump ();
			} else {
				Target.MemberModel.PlayTaunt ();
			}
		}
	}
}

