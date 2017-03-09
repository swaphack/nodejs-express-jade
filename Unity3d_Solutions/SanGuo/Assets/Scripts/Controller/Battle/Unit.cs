using System.Collections.Generic;
using UnityEngine;
using Model.Battle;
using Model.Base;
using Model.Skill;
using Model.Buff;
using Controller.AI;
using Controller.Battle.AI;
using Game.Helper;
using Controller.Battle.Member;

namespace Controller.Battle
{
	/// <summary>
	/// 作战单位
	/// </summary>
	public class Unit : Identifier
	{
		/// <summary>
		/// 队伍编号
		/// </summary>
		private int _TeamID;
		/// <summary>
		/// 单位信息
		/// </summary>
		private UnitModel _UnitModel;
		/// <summary>
		/// 单位属性
		/// </summary>
		private UnitProperty _UnitProperty;
		/// <summary>
		/// 单位技能
		/// </summary>
		private UnitSkill _UnitSkill;
		/// <summary>
		/// 单位状态
		/// </summary>
		private UnitBuff _UnitBuff;
		/// <summary>
		/// 游戏对象
		/// </summary>
		private GameObject _GameObject;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		private MemberTransform _MemberTranform;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		private MemberModel _MemberModel;
		/// <summary>
		/// 旅行者
		/// </summary>
		private Traveler _Walker;
		/// <summary>
		/// 单位行为
		/// </summary>
		private UnitBehaviour _UnitBehaviour;
		/// <summary>
		/// 是否是碰撞体
		/// </summary>
		private bool _IsCollider;
		/// <summary>
		/// 单位死亡处理
		/// </summary>
		public event OnUnitBroadcast OnDead;
		/// <summary>
		/// 开始播放动作
		/// </summary>
		public event OnUnitActionBroadcast OnUnitActionStart;
		/// <summary>
		/// 动作播放停止
		/// </summary>
		public event OnUnitActionBroadcast OnUnitActionEnd;
		/// <summary>
		/// 单位碰撞处理
		/// </summary>
		public event OnUnitCollisonBroadcast OnUnitCollision;
		/// <summary>
		/// 所属队伍编号
		/// </summary>
		public int TeamID {
			get { 
				return _TeamID;
			}
			set { 
				_TeamID = value;
			}
		}

		/// <summary>
		/// 获取单位拥有的技能
		/// </summary>
		/// <value>The skills.</value>
		public UnitSkill Skill {
			get { 
				return _UnitSkill;
			}
		}

		/// <summary>
		/// 获取挂在单位身上状态
		/// </summary>
		/// <value>The suffs.</value>
		public UnitBuff Buff {
			get { 
				return _UnitBuff;
			}
		}

		/// <summary>
		/// 单位属性
		/// </summary>
		/// <value>The properties.</value>
		public UnitProperty Property {
			get { 
				return _UnitProperty;
			}
		}
		/// <summary>
		/// 空间变换对象
		/// </summary>
		public MemberTransform MemberTransform {
			get { 
				return _MemberTranform;
			}
		}
		/// <summary>
		/// 单位模型
		/// </summary>
		public MemberModel MemberModel {
			get { 
				return _MemberModel;
			}
		}
		/// <summary>
		/// 对象
		/// </summary>
		public GameObject GameObject {
			get { 
				return _GameObject;
			}
		}

		/// <summary>
		/// 旅行者
		/// </summary>
		/// <value>The walker.</value>
		public Traveler Walker {
			get { 
				return _Walker;
			}
		}

		public UnitBehaviour UnitBehaviour {
			get { 
				return _UnitBehaviour;
			}
		}

		/// <summary>
		/// 是否是碰撞体
		/// </summary>
		/// <value><c>true</c> if this instance is collider; otherwise, <c>false</c>.</value>
		public bool IsCollider {
			get { 
				return _IsCollider;
			}
			set { 
				_IsCollider = value;
			}
		}

		public Unit()
		{
			_MemberTranform = new MemberTransform ();
			_MemberModel = new MemberModel ();

			_UnitProperty = new UnitProperty ();
			_UnitSkill = new UnitSkill ();
			_UnitBuff = new UnitBuff ();

			_Walker = new Traveler ();

			_UnitBehaviour = new UnitBehaviour ();
			_UnitBehaviour.Field = BattleHelp.Field;
			_UnitBehaviour.Target = this;
			_UnitBehaviour.Init ();

			//IsCollider = true;

			_UnitProperty.OnCurrentPropertyChanged += OnPropertyChanged;
		}

		/// <summary>
		/// 设置对象
		/// </summary>
		/// <param name="gameObj">Game object.</param>
		/// <param name="paper">UnitModel.</param>
		public void SetObject(GameObject gameObj)
		{
			if (gameObj == null) {
				return;
			}

			_GameObject = gameObj;

			_MemberTranform.SetTranform (gameObj.transform);
			_MemberModel.SetTranform (gameObj.transform);

			_MemberModel.OnActionStart += OnStartAction;
			_MemberModel.OnActionEnd += OnEndAction;
		}

		/// <summary>
		/// 重置对象
		/// </summary>
		public void RestObject()
		{
			_GameObject = null;

			_MemberTranform.SetTranform (null);
			_MemberModel.SetTranform (null);
		}

		/// <summary>
		///  播放动作
		/// </summary>
		/// <param name="tag">Tag.</param>
		private void OnStartAction(int tag)
		{
			if (OnUnitActionStart != null) {
				OnUnitActionStart (this, tag);	
			}
		}

		/// <summary>
		///  停止动作
		/// </summary>
		/// <param name="tag">Tag.</param>
		private void OnEndAction(int tag) 
		{
			if (OnUnitActionEnd != null) {
				OnUnitActionEnd (this, tag);
			}
		}

		/// <summary>
		/// 设置模型数据
		/// </summary>
		/// <param name="paper">Paper.</param>
		public void SetModel(UnitModel paper)
		{
			if (paper == null) {
				return;
			}

			_UnitModel = paper;

			// 初始化属性
			if (paper.Attributes != null) {
				foreach(KeyValuePair<PropertyType, float>  item in paper.Attributes){
					_UnitProperty.BaseProperty.SetValue (item.Key, item.Value);
				}

				foreach(KeyValuePair<PropertyType, float>  item in paper.Attributes) {
					float value = _UnitProperty.GetMaxValue (item.Key);
					_UnitProperty.CurrentProperty.SetValue (item.Key, value);
				}
			}

			// 初始化技能
			if (paper.Skills != null) {
				int index = (int)SkillIndex.NormalAttack;
				int count = paper.Skills.Count;
				for (int i = 0; i < count; i++) {
					_UnitSkill.AddSkillModel ((SkillIndex)index, paper.Skills [i]);

					UnitSkill.SkillValue value = new UnitSkill.SkillValue ();
					value.SetMaxValue (paper.Skills [i].CoolDown);
					_UnitSkill.AddSkillValue ((SkillIndex)index, value);
					index++;
				}
			}
		}

		/// <summary>
		/// 属性改变时通知
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		private void OnPropertyChanged(PropertyType type, float value)
		{
			if (type == PropertyType.HitPoints) {
				if (value <= 0) {
					_UnitBehaviour.PlayDie ();
				}
			}
		}

		/// <summary>
		/// 派发死亡事件
		/// </summary>
		public void RunDeadEvent()
		{
			if (OnDead != null) {
				OnDead (this);
			}
		}

		/// <summary>
		/// 显示位置
		/// </summary>
		private void ShowPosition()
		{
			Vector3 position = MemberTransform.Position;
			Log.Info (string.Format ("Position({0},{1},{2})", position.x, position.y, position.z));
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			if (_UnitProperty.Dead) {
				UpdateDeadEvent (dt);
			} else {
				UpdateAliveEvent (dt);
			}
		}

		/// <summary>
		/// 更新活着的事件
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateAliveEvent(float dt)
		{
			_MemberModel.Update (dt);

			// 更新状态
			UpdateBuff (dt);

			// 更新技能
			UpdateSkill (dt);

			// 行走
			UpdateWalk(dt);

			// 更新行为
			UpdateBehaviour (dt);
		}

		/// <summary>
		/// 更新死亡事件
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateDeadEvent(float dt)
		{
			if (_GameObject == null) {
				return;
			}

			_MemberModel.Update (dt);
		}

		/// <summary>
		/// 更新技能
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateSkill(float dt) {
			foreach (KeyValuePair<SkillIndex, UnitSkill.SkillValue> item in _UnitSkill.SkillValues) {
				if (item.Value.CurrentValue > 0) {
					item.Value.CurrentValue -= dt;
				}
			}
		}

		/// <summary>
		/// 更新状态
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateBuff(float dt) {
			if (_UnitBuff.BuffValues.Count == 0) {
				return;
			}
			List<KeyValuePair<BuffType, int>> removeBuffs = new List<KeyValuePair<BuffType, int>>();
			foreach (KeyValuePair<BuffType, List<UnitBuff.BuffValue>> item in _UnitBuff.BuffValues) {
				for (int i = 0, max = item.Value.Count; i < max; i++) {
					item.Value[i].CurrentValue -= dt;
					if (item.Value[i].CurrentValue < 0) {
						removeBuffs.Add (new KeyValuePair<BuffType, int> (item.Key, i));
					}
				}
			}

			if (removeBuffs.Count == 0) {
				return;
			}

			removeBuffs.Reverse ();

			foreach (KeyValuePair<BuffType, int> item in removeBuffs) {
				BuffModel buffModel = _UnitBuff.GetBuffModel (item.Key, item.Value);
				Buff.RemoveBuff (buffModel);
			}
		}

		/// <summary>
		/// 是否在行走中
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateWalk(float dt) {
			// 静止行走的buff
			if (Buff.HasType (BuffType.ForbiddenWalk)) {
				return;
			}

			// 死亡
			if (Property.Dead) {
				return;
			}

			// 禁止行走
			if (CheckNeedStopWalk ()) {
				StopWalk ();
				return;
			}

			// 碰撞检查
			if (IsCollider && IsCollison()) {
				StopWalk ();
				return;
			}

			// 抵达目标
			if (!RunningMove (dt)) { 
				StopWalk ();
				return;
			}
		}

		/// <summary>
		/// 检查是否需停止行走
		/// </summary>
		/// <returns><c>true</c>, if need stop walk was checked, <c>false</c> otherwise.</returns>
		private bool CheckNeedStopWalk() {
			// 播放动作
			if (MemberModel.IsPlay (UnitAction.t_attack_01)
			    || MemberModel.IsPlay (UnitAction.t_attack_02)
			    || MemberModel.IsPlay (UnitAction.t_attack_03)
			    || MemberModel.IsPlay (UnitAction.t_getHit)
				|| MemberModel.IsPlay (UnitAction.t_defend)
				|| MemberModel.IsPlay (UnitAction.t_die)
				|| MemberModel.IsPlay (UnitAction.t_jump)
				|| MemberModel.IsPlay (UnitAction.t_taunt)) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// 碰撞检测
		/// </summary>
		/// <returns><c>true</c> if this instance is collison; otherwise, <c>false</c>.</returns>
		private bool IsCollison()
		{
			if (GameObject == null) {
				return false;
			}
			Field field = BattleHelp.Field;
			Collider collider0 = GameObject.GetComponent<Collider> ();
			if (collider0 == null) {
				return false;
			}
				
			// 先搜索范围内的敌人，再判断碰撞
			float distance = 0;
			float boxRadius = 0;
			Bounds bounds0 = collider0.bounds;
			float maxRadius0 = Mathf.Max (bounds0.extents.x, bounds0.extents.y, bounds0.extents.z);
			Collider collider1 = null;
			Bounds bounds1;

			List<Unit> nearUnits = new List<Unit>();
			foreach (KeyValuePair<int, Team> item in field.AliveTeams) {
				UnitSheet aliveUnits = item.Value.AliveUnits;
				foreach (KeyValuePair<int, Unit> item2 in aliveUnits.Units) {
					if (ID != item2.Key && item2.Value.IsCollider && !item2.Value.UnitBehaviour.IsWalk) {
						collider1 = item2.Value.GameObject.GetComponent<Collider> ();
						if (collider1 != null) {
							bounds1 = collider1.bounds;
							distance = Vector3.Distance (MemberTransform.Position, item2.Value.MemberTransform.Position);
							boxRadius = Mathf.Max (bounds1.extents.x, bounds1.extents.y, bounds1.extents.z) + maxRadius0;
							if (distance <= boxRadius) {
								if (bounds0.Intersects (bounds1)) {
									return true;
								}
							}
						}

					}
				}
			}

			return false;
		}

		/// <summary>
		/// 行走
		/// </summary>
		/// <param name="dt">Dt.</param>
		private bool RunningMove(float dt) 
		{
			// 停止走路，站立
			if (Walker.Empty) {
				return false;
			}

			Vector3 nextPosition;
			if (!Walker.GetNextStation (dt, out nextPosition)) {
				return false;
			}

			MemberModel.PlayWalk ();
			MemberModel.LookAt (nextPosition);
			MemberTransform.WalkTo (nextPosition);

			return true;
		}

		/// <summary>
		/// 更新行为
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateBehaviour(float dt) {
			if (_UnitBehaviour.RunTask) {
				_UnitBehaviour.Update (dt);
			} else {
				_UnitBehaviour.Switch (UnitStateType.PlaySpell);
			}
		}

		/// <summary>
		/// 停止行走
		/// </summary>
		public void StopWalk()
		{
			_Walker.Clear ();
			if (MemberModel.IsPlay (UnitAction.t_walk)) {
				MemberModel.PlayIdle ();
			}
		}
	}
}

