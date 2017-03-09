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
		/// 随机
		/// </summary>
		Random,
	}

	/// <summary>
	/// 目标选择机制
	/// </summary>
	public partial class TargetSelectMechanism
	{
		/// <summary>
		/// 查找最近目标
		/// </summary>
		internal class FindNearestTarget : ITargetSelect
		{
			/// <summary>
			/// 选中目标
			/// </summary>
			private SelectedTarget _SelectedTarget;
			/// <summary>
			/// 选中目标
			/// </summary>
			/// <value>The target.</value>
			public SelectedTarget SelectedTarget { 
				get  { 
					return _SelectedTarget;
				} 
			}

			/// <summary>
			/// 查找目标
			/// </summary>
			/// <returns><c>true</c>, if targets was found, <c>false</c> otherwise.</returns>
			/// <param name="src">Source.</param>
			/// <param name="field">Field.</param>
			public bool FindTargets(Unit src, Field field)
			{
				if (src == null || field == null) {
					return false;
				}

				List<Team> teams = field.GetOtherAliveTeam (src.TeamID);
				// 无目标
				if (teams == null || teams.Count == 0) {
					return false;
				}

				Unit firstTarget = null;
				float minDistance = 0;
				// 查找距离最近的单位
				foreach (Team team in teams) {
					foreach (KeyValuePair<int, Unit> item in team.AliveUnits.Units) {
						float dist = Vector3.Distance (src.MemberTransform.Position, item.Value.MemberTransform.Position);
						if (firstTarget == null) {
							firstTarget = item.Value;
							minDistance = dist;
						} else if (dist < minDistance) {
							firstTarget = item.Value;
							minDistance = dist;
						}
					}
				}

				if (firstTarget == null) {
					return false;
				}

				List<Unit> targets = new List<Unit> ();
				targets.Add (firstTarget);

				_SelectedTarget = new SelectedTarget (targets, targets[0]);

				return true;
			}
		}
		/// <summary>
		/// 查找随机目标
		/// </summary>
		internal class FindRandomTarget : ITargetSelect
		{
			/// <summary>
			/// 选中目标
			/// </summary>
			private SelectedTarget _SelectedTarget;
			/// <summary>
			/// 选中目标
			/// </summary>
			/// <value>The target.</value>
			public SelectedTarget SelectedTarget { 
				get  { 
					return _SelectedTarget;
				} 
			}

			/// <summary>
			/// 查找目标
			/// </summary>
			/// <returns><c>true</c>, if targets was found, <c>false</c> otherwise.</returns>
			/// <param name="src">Source.</param>
			/// <param name="field">Field.</param>
			public bool FindTargets(Unit src, Field field)
			{
				if (src == null || field == null) {
					return false;
				}

				List<Team> teams = field.GetOtherAliveTeam (src.TeamID);
				if (teams == null || teams.Count == 0) {
					return false;
				}

				int rand = Random.Range (0, teams.Count);
				Team targetTeam = teams [rand];
				rand = Random.Range (0, targetTeam.AliveUnits.Count);

				List<Unit> targets = targetTeam.AliveUnits.GetRandomUnits(1);
				if (targets == null || targets.Count == 0) {
					return false;
				}

				_SelectedTarget = new SelectedTarget (targets, targets[0]);

				return true;
			}
		}

		private void Init()
		{
			_TSRule.Add (TargetSelectType.Nearest, new FindNearestTarget());
			_TSRule.Add (TargetSelectType.Random, new FindRandomTarget());
		}
	}
}

