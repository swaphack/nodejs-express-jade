using UnityEngine;
using System.Collections.Generic;
using Controller.Battle;

namespace Controller.Battle.Spell
{
	/// <summary>
	/// 目标选择类型
	/// </summary>
	public enum TargetSelectType
	{
		/// <summary>
		/// 最近
		/// </summary>
		Nearest,
		/// <summary>
		/// 范围
		/// </summary>
		Range,
		/// <summary>
		/// 随机
		/// </summary>
		Random,
	}

	/// <summary>
	/// 目标选择机制
	/// </summary>
	public partial class TargetSelectMechanism
	{
		private void Init()
		{
			_TSRule.Add (TargetSelectType.Nearest, new FindTargetMethod.FindNearestTarget());
			_TSRule.Add (TargetSelectType.Range, new FindTargetMethod.FindRangeTarget());
			_TSRule.Add (TargetSelectType.Random, new FindTargetMethod.FindRandomTarget());
		}
	}
}

