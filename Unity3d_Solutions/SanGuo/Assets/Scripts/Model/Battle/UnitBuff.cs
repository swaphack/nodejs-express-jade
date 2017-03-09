using System;
using System.Collections.Generic;
using Game.Helper;
using Model.Base;
using Model.Buff;

namespace Model.Battle
{
	/// <summary>
	/// buff改变时处理
	/// </summary>
	public delegate void OnBuffBroadCast(BuffModel buffModel);
	/// <summary>
	/// 单位状态
	/// </summary>
	public class UnitBuff
	{
		/// <summary>
		/// 状态相关值
		/// </summary>
		public class BuffValue : CoolDown
		{

		}

		/// <summary>
		/// 状态类别集合
		/// {状态类型， 叠加数量}
		/// </summary>
		private Dictionary<BuffType, List<BuffModel>> _BuffModels;

		/// <summary>
		/// 技能值
		/// </summary>
		private Dictionary<BuffType, List<BuffValue>> _BuffValues;

		/// <summary>
		/// 状态类别集合
		/// </summary>
		public Dictionary<BuffType, List<BuffModel>> BuffModels	{
			get { 
				return _BuffModels;
			}
		}

		/// <summary>
		/// 技能值
		/// </summary>
		public Dictionary<BuffType, List<BuffValue>> BuffValues	{
			get { 
				return _BuffValues;
			}
		}

		/// <summary>
		/// 新增buff
		/// </summary>
		public event OnBuffBroadCast OnBuffAdd;
		/// <summary>
		/// 移除buff
		/// </summary>
		public event OnBuffBroadCast OnBuffRemove;

		public UnitBuff ()
		{
			_BuffModels = new Dictionary<BuffType, List<BuffModel>> ();
			_BuffValues = new Dictionary<BuffType, List<BuffValue>> ();
		}

		/// <summary>
		/// 添加状态
		/// </summary>
		/// <param name="buffModel">BuffModel.</param>
		private void AddBuffModel(BuffModel buffModel)
		{
			if (buffModel == null) {
				return;
			}

			if (!_BuffModels.ContainsKey (buffModel.BuffType)) {
				_BuffModels [buffModel.BuffType] = new List<BuffModel> ();
			}

			_BuffModels [buffModel.BuffType].Add (buffModel);
		}

		/// <summary>
		/// 移除状态
		/// </summary>
		/// <param name="buffModel">Buff model.</param>
		private void RemoveBuffModel(BuffModel buffModel)
		{  
			if (buffModel == null) {
				return;
			}

			if (!_BuffModels.ContainsKey (buffModel.BuffType)) {
				Log.Warning ("Not Exists Buff Type : " + buffModel.BuffType.ToString());
				return;
			}
			  
			_BuffModels [buffModel.BuffType].Remove (buffModel);

			if (_BuffModels [buffModel.BuffType].Count == 0) {
				_BuffModels.Remove (buffModel.BuffType);
			}
		}

		/// <summary>
		/// 获取状态索引
		/// </summary>
		/// <returns>The buff index.</returns>
		/// <param name="buffModel">Buff model.</param>
		public int GetBuffIndex(BuffModel buffModel)
		{
			if (buffModel == null) {
				return -1;
			}

			if (!_BuffModels.ContainsKey (buffModel.BuffType)) {
				return -1;
			}

			return _BuffModels [buffModel.BuffType].IndexOf(buffModel);
		}

		/// <summary>
		/// 获取状态
		/// </summary>
		/// <returns>The buff model.</returns>
		/// <param name="type">Type.</param>
		/// <param name="index">Index.</param>
		public BuffModel GetBuffModel(BuffType type, int index)
		{
			if (!_BuffModels.ContainsKey (type)) {
				return null;
			}

			if (index < 0 || index > _BuffModels.Count) {
				return null;
			}

			return _BuffModels[type][index];
		}


		/// <summary>
		/// 是否包含指定类型的状态
		/// </summary>
		/// <returns><c>true</c> if this instance has type the specified type; otherwise, <c>false</c>.</returns>
		/// <param name="type">Type.</param>
		public bool HasType(BuffType type)
		{
			bool bModelOk = _BuffModels.ContainsKey (type);
			bool bValueOk = _BuffValues.ContainsKey (type);

			return bModelOk && bValueOk;
		}

		/// <summary>
		/// 获取状态影响到的属性值
		/// 1.相同状态不叠加，取当前状态效果的最大值
		/// 2.相同状态叠加，取累计值
		/// 最终取上面两者的最大值
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="gain">增益{固定值，百分比}.</param>
		/// <param name="reduction">减益{固定值，百分比}.</param>
		public void GetEffectPropertyValue(PropertyType type, out BuffPropertyValue gain, out BuffPropertyValue reduction)
		{
			gain = new BuffPropertyValue ();
			reduction = new BuffPropertyValue ();

			if (!_BuffModels.ContainsKey (BuffType.EffectProperty)) {
				return;
			}

			// 增益叠加
			BuffPropertyValue gainSuperpose = new BuffPropertyValue();
			// 增益不叠加
			BuffPropertyValue gainNotSuperpose = new BuffPropertyValue();
			// 减益叠加
			BuffPropertyValue reductionSuperpose = new BuffPropertyValue();
			// 减益不叠加
			BuffPropertyValue reductionNotSuperpose = new BuffPropertyValue();

			BuffPropertyValue current;

			int count = _BuffModels [BuffType.EffectProperty].Count;
			BuffModel buff = null;
			for (int i = 0; i < count; i ++) {
				buff = _BuffModels [BuffType.EffectProperty] [i];
				if (buff.PropertyType != type) {
					continue;
				}
				if (buff.CanSuperpose) { // 可叠加
					if (!buff.IsDeBuff) {
						current = gainSuperpose;
					} else {
						current = reductionSuperpose;
					}
					current.Fix += buff.FixValue;
					current.Percent += buff.PercentValue;
				}
				else if (!buff.CanSuperpose) { // 不可叠加
					if (!buff.IsDeBuff) {
						current = gainNotSuperpose;
					} else {
						current = reductionNotSuperpose;	
					}
					if (buff.FixValue > current.Fix) {
						current.Fix = buff.FixValue;
					}
					if (buff.PercentValue > gain.Percent) {
						gain.Percent = buff.PercentValue;
					}
				}
			}

			gain.Fix = gainSuperpose.Fix > gainNotSuperpose.Fix ? gainSuperpose.Fix : gainNotSuperpose.Fix;
			gain.Percent = gainSuperpose.Percent > gainNotSuperpose.Percent ? gainSuperpose.Percent : gainNotSuperpose.Percent;
			reduction.Fix = reductionSuperpose.Fix > reductionNotSuperpose.Fix ? reductionSuperpose.Fix : reductionNotSuperpose.Fix;
			reduction.Percent = reductionSuperpose.Percent > reductionNotSuperpose.Percent ? reductionSuperpose.Percent : reductionNotSuperpose.Percent;
		}


		/// <summary>
		/// 添加技能参数
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		private void AddBuffValue(BuffType type, BuffValue value)
		{
			if (value == null) {
				return;
			}

			if (!_BuffValues.ContainsKey (type)) {
				_BuffValues[type] = new List<BuffValue> ();
			}

			value.Reset ();
			_BuffValues[type].Add(value);
		}

		/// <summary>
		/// 移除技能参数
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="index">Index.</param>
		private void RemoveBuffValue(BuffType type, int index)
		{
			if (!_BuffValues.ContainsKey (type)) {
				return;
			}
			_BuffValues[type].RemoveAt (index);
			if (_BuffValues [type].Count == 0) {
				_BuffValues.Remove (type);
			}
		}

		/// <summary>
		/// 获取技能值
		/// </summary>
		/// <returns>The buff value.</returns>
		/// <param name="type">Type.</param>
		/// <param name="index">Index.</param>
		public BuffValue GetBuffValue(BuffType type, int index)
		{
			if (_BuffValues.ContainsKey (type)) {
				return null;
			}

			if (index < 0 || index >= _BuffValues [type].Count) {
				return null;
			}

			return _BuffValues[type][index];
		}

		/// <summary>
		/// 清除所有状态
		/// </summary>
		public void Clear()
		{
			_BuffModels.Clear ();
			_BuffValues.Clear ();
		}


		/// <summary>
		/// 添加状态
		/// </summary>
		/// <param name="buffModel">Buff model.</param>
		public void AddBuff(BuffModel buffModel)
		{
			if (buffModel == null) {
				return;
			}

			this.AddBuffModel (buffModel);
			this.AddBuffValue (buffModel.BuffType, new BuffValue () {
				MaxValue = buffModel.Duration,
			});

			if (OnBuffAdd != null) {
				OnBuffAdd (buffModel);
			}
		}

		/// <summary>
		/// 移除状态
		/// </summary>
		/// <param name="buffModel">Buff model.</param>
		public void RemoveBuff(BuffModel buffModel)
		{
			if (buffModel == null) {
				return;
			}

			int index = this.GetBuffIndex (buffModel);

			this.RemoveBuffValue (buffModel.BuffType, index);
			this.RemoveBuffModel (buffModel);

			if (OnBuffRemove != null) {
				OnBuffRemove (buffModel);
			}
		}
	}
}

