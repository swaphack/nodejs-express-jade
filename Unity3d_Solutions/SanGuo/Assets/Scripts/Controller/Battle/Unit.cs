using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Battle;
using Model.Base;

namespace Controller.Battle
{
	/// <summary>
	/// 作战单位
	/// </summary>
	public class Unit : Identifier
	{
		/// <summary>
		/// 单位信息
		/// </summary>
		private UnitModel _UnitModel;
		/// <summary>
		/// 单位属性
		/// </summary>
		private UnitProperty _UnitProperty;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		private TranformObject _TranformObject;
		/// <summary>
		/// 单位死亡处理
		/// </summary>
		public event OnUnitBroadCast OnDead;
		/// <summary>
		/// 开始播放动作
		/// </summary>
		public event OnUnitActionBroadCast OnUnitActionStart;
		/// <summary>
		/// 动作播放停止
		/// </summary>
		public event OnUnitActionBroadCast OnUnitActionEnd;
		/// <summary>
		/// 所属队伍编号
		/// </summary>
		public int TeamID;
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

		public Unit()
		{
			_TranformObject = new TranformObject ();
			_UnitProperty = new UnitProperty ();

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

			if (paper.Attributes != null) {
				foreach(KeyValuePair<PropertyType, float>  item in paper.Attributes){
					_UnitProperty.BaseProperty.SetValue (item.Key, item.Value);
				}

				foreach(KeyValuePair<PropertyType, float>  item in paper.Attributes) {
					float value = _UnitProperty.GetMaxValue (item.Key);
					_UnitProperty.CurrentProperty.SetValue (item.Key, value);
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
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			_TranformObject.Update (dt);

			if (_UnitProperty.Dead) {
				return;
			}
		}
	}
}

