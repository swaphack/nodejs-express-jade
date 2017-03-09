using System;
using System.Collections.Generic;
using Model.Skill;
using Model.Base;

namespace Model.Battle
{
	/// <summary>
	/// 单位技能
	/// </summary>
	public class UnitSkill
	{
		/// <summary>
		/// 技能相关值
		/// </summary>
		public class SkillValue : CoolDown
		{

		}
		/// <summary>
		/// 单位技能数据
		/// </summary>
		private Dictionary<SkillIndex, SkillModel> _SkillModels;
		/// <summary>
		/// 技能值
		/// </summary>
		private Dictionary<SkillIndex, SkillValue> _SkillValues;

		/// <summary>
		/// 单位技能数据
		/// </summary>
		public Dictionary<SkillIndex, SkillModel> SkillModels	{
			get { 
				return _SkillModels;
			}
		}
		/// <summary>
		/// 技能值
		/// </summary>
		public Dictionary<SkillIndex, SkillValue> SkillValues	{
			get { 
				return _SkillValues;
			}
		}

		public UnitSkill ()
		{
			_SkillModels = new Dictionary<SkillIndex, SkillModel> ();
			_SkillValues = new Dictionary<SkillIndex, SkillValue> ();
		}

		/// <summary>
		/// 添加技能
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="skillModel">Skill model.</param>
		public void AddSkillModel(SkillIndex index, SkillModel skillModel)
		{
			if (skillModel == null) {
				return;
			}

			_SkillModels.Add (index, skillModel);
		}

		/// <summary>
		/// 移除技能模型
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveSkillModel(SkillIndex index)
		{
			_SkillModels.Remove (index);
		}

		/// <summary>
		/// 获取技能模型
		/// </summary>
		/// <returns>The skill model.</returns>
		/// <param name="index">Index.</param>
		public SkillModel GetSkillModel(SkillIndex index)
		{
			if (_SkillModels.ContainsKey (index)) {
				return _SkillModels [index];
			}

			return null;
		}

		/// <summary>
		/// 添加技能参数
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="value">Value.</param>
		public void AddSkillValue(SkillIndex index, SkillValue value)
		{
			if (value == null) {
				return;
			}

			_SkillValues.Add (index, value);
		}

		/// <summary>
		/// 移除技能参数
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveSkillValue(SkillIndex index)
		{
			_SkillValues.Remove (index);
		}

		/// <summary>
		/// 获取技能值
		/// </summary>
		/// <returns>The skill value.</returns>
		/// <param name="index">Index.</param>
		public SkillValue GetSkillValue(SkillIndex index)
		{
			if (_SkillValues.ContainsKey (index)) {
				return _SkillValues [index];
			}

			return null;
		}

		/// <summary>
		/// 重置技能时间
		/// </summary>
		/// <param name="index">Index.</param>
		public void ResetSkillValue(SkillIndex index)
		{
			if (!_SkillValues.ContainsKey (index)) {
				return;
			}

			_SkillValues [index].Reset ();
		}

		/// <summary>
		/// 清空数据
		/// </summary>
		public void Clear()
		{
			_SkillModels.Clear ();
			_SkillValues.Clear ();
		}
	}
}

