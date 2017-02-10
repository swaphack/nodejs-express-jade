using System;
using System.Collections.Generic;
using Game.Helper;
using Model.Base;
using Model.Buff;

namespace Model.Battle
{
	public class BuffValue
	{
		/// <summary>
		/// 固定值
		/// </summary>
		public float Fix;
		/// <summary>
		/// 百分比
		/// </summary>
		public float Percent;
	}
	/// <summary>
	/// 单位状态
	/// </summary>
	public class UnitBuff
	{
		/// <summary>
		/// 状态集合
		/// </summary>
		private Dictionary<PropertyType, HashSet<BuffModel>> _BuffModels;

		public UnitBuff ()
		{
			_BuffModels = new Dictionary<PropertyType, HashSet<BuffModel>> ();
		}

		/// <summary>
		/// 添加状态
		/// </summary>
		/// <param name="buffModel">BuffModel.</param>
		public void AddBuff(BuffModel buffModel)
		{
			if (buffModel == null) {
				return;
			}

			if (!_BuffModels.ContainsKey (buffModel.Type)) {
				_BuffModels [buffModel.Type] = new HashSet<BuffModel> ();
			}

			_BuffModels [buffModel.Type].Add (buffModel);
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

			if (!_BuffModels.ContainsKey (buffModel.Type)) {
				Log.Warning ("Not Exists Buff Type : " + buffModel.Type.ToString());
				return;
			}

			_BuffModels [buffModel.Type].Remove (buffModel);
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
		public void GetBuffMaxPercent(PropertyType type, out BuffValue gain, out BuffValue reduction)
		{
			gain = new BuffValue ();
			reduction = new BuffValue ();

			if (!_BuffModels.ContainsKey (type)) {
				return;
			}

			// 增益叠加
			BuffValue gainSuperpose = new BuffValue();
			// 增益不叠加
			BuffValue gainNotSuperpose = new BuffValue();
			// 减益叠加
			BuffValue reductionSuperpose = new BuffValue();
			// 减益不叠加
			BuffValue reductionNotSuperpose = new BuffValue();

			BuffValue current;

			foreach (BuffModel buff in _BuffModels[type]) {
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
		/// 清除所有状态
		/// </summary>
		public void Clear()
		{
			_BuffModels.Clear ();
		}
	}
}

