using System;
using Model.Base;
using Model.Skill;

namespace Model.Sheet
{
	/// <summary>
	/// 技能表
	/// </summary>
	public class SkillSheet : ModelSheet<SkillModel>
	{
		public SkillSheet ()
			:base(ModelType.Skill)
		{
		}
	}
}

