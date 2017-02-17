using System;

namespace Model.Base
{
	/// <summary>
	/// 选择目标类型
	/// </summary>
	public enum TargetType
	{
		/// <summary>
		/// 无限定
		/// </summary>
		None,
		/// <summary>
		/// 自己
		/// </summary>
		Self,
		/// <summary>
		/// 本方队友
		/// </summary>
		SelfTeamMember,
		/// <summary>
		/// 非本方成员
		/// </summary>
		OtherTeamMember,
	}
}

