using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Battle;
using Model.Base;
using Model.Skill;
using Controller.AI;
using Controller.Battle.AI;
using Game.Helper;

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
		/// 空间变换对象
		/// </summary>
		private TranformObject _TranformObject;
		/// <summary>
		/// 旅行者
		/// </summary>
		private Traveler _Walker;
		/// <summary>
		/// 单位行为
		/// </summary>
		private UnitBehaviour _UnitBehaviour;
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
		public TranformObject TranformObject {
			get { 
				return _TranformObject;
			}
		}
		/// <summary>
		/// 对象
		/// </summary>
		public GameObject GameObject {
			get { 
				if (_TranformObject.Transform == null) {
					return null;
				}
				return _TranformObject.Transform.gameObject;
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

		public Unit()
		{
			_TranformObject = new TranformObject ();
			_UnitProperty = new UnitProperty ();
			_UnitSkill = new UnitSkill ();
			_UnitBuff = new UnitBuff ();
			_Walker = new Traveler ();

			_UnitBehaviour = new UnitBehaviour ();
			_UnitBehaviour.Field = BattleHelp.Field;
			_UnitBehaviour.Target = this;
			_UnitBehaviour.Init ();

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

			_TranformObject.SetTranform (gameObj.transform);
			_TranformObject.OnActionStart += OnStartAction;
			_TranformObject.OnActionEnd += OnEndAction;
		}

		/// <summary>
		///  播放动作
		/// </summary>
		/// <param name="tag">Tag.</param>
		private void OnStartAction(string tag) {
			OnUnitActionStart (this, tag);	
		}

		/// <summary>
		///  停止动作
		/// </summary>
		/// <param name="tag">Tag.</param>
		private void OnEndAction(string tag) {
			OnUnitActionEnd (this, tag);
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
				foreach (SkillModel item in paper.Skills) {
					_UnitSkill.AddSkillModel ((SkillIndex)index, item);

					UnitSkill.SkillValue value = new UnitSkill.SkillValue ();
					value.SetMaxValue (item.CoolDown);
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
					OnDead (this);
					_TranformObject.PlayDead ();
				}
			}
		}

		/// <summary>
		/// 显示位置
		/// </summary>
		private void ShowPosition()
		{
			Vector3 position = TranformObject.Position;
			Log.Info (string.Format ("Position({0},{1},{2})", position.x, position.y, position.z));
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			_TranformObject.Update (dt);
			UpdateBuff (dt);
			if (_UnitProperty.Dead) {
				return;
			}
			UpdateSkill (dt);
			UpdateBehaviour (dt);
		}

		/// <summary>
		/// 更新技能
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateSkill(float dt) {
			foreach (KeyValuePair<SkillIndex, UnitSkill.SkillValue> item in _UnitSkill.SkillValues) {
				item.Value.CurrentValue -= dt;
			}
		}

		/// <summary>
		/// 更新状态
		/// </summary>
		/// <param name="dt">Dt.</param>
		private void UpdateBuff(float dt) {
			foreach (KeyValuePair<BuffType, List<UnitBuff.BuffValue>> item in _UnitBuff.BuffValues) {
				foreach (UnitBuff.BuffValue value in item.Value) {
					value.CurrentValue -= dt;
				}
			}
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
	}
}

