using System.Collections.Generic;
using Controller.Battle;
using UnityEngine;
using Game.Helper;

namespace Controller.Battle.Spell
{
	/// <summary>
	/// 查找目标方式
	/// </summary>
	public class FindTargetMethod
	{
		/// <summary>
		/// 查找最近目标
		/// </summary>
		public class FindNearestTarget : ITargetSelect
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
			/// <returns>true</returns>
			/// <c>false</c>
			/// <param name="src">Source.</param>
			/// <param name="field">Field.</param>
			/// <param name="parameters">Parameters.</param>
			public bool FindTargets(Unit src, Field field, ISelectedTargetParameters parameters)
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
				float area = 0;
				// 查找距离最近的单位
				foreach (Team team in teams) {
					foreach (KeyValuePair<int, Unit> item in team.AliveUnits.Units) {
						area = MathHelp.Area2D (src.MemberTransform.Position, item.Value.MemberTransform.Position);
						if (firstTarget == null) {
							firstTarget = item.Value;
							minDistance = area;
						} else if (area < minDistance) {
							firstTarget = item.Value;
							minDistance = area;
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
		public class FindRandomTarget : ITargetSelect
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
			/// <returns>true</returns>
			/// <c>false</c>
			/// <param name="src">Source.</param>
			/// <param name="field">Field.</param>
			/// <param name="parameters">Parameters.</param>
			public bool FindTargets(Unit src, Field field, ISelectedTargetParameters parameters)
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

		public class FindRangeTargetParameters : ISelectedTargetParameters
		{
			/// <summary>
			/// 范围值
			/// </summary>
			public float Range;
		}


		/// <summary>
		/// 查找在范围附近的目标
		/// </summary>
		public class FindRangeTarget : ITargetSelect
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
			/// <returns>true</returns>
			/// <c>false</c>
			/// <param name="src">Source.</param>
			/// <param name="field">Field.</param>
			/// <param name="parameters">Parameters.</param>
			public bool FindTargets(Unit src, Field field, ISelectedTargetParameters parameters)
			{
				if (src == null || field == null || parameters == null) {
					return false;
				}

				FindRangeTargetParameters parameter = parameters as FindRangeTargetParameters;
				if (parameter == null || parameter.Range <= 0) {
					return false;
				}

				List<Team> teams = field.GetOtherAliveTeam (src.TeamID);
				// 无目标
				if (teams == null || teams.Count == 0) {
					return false;
				}

				Unit firstTarget = null;
				float area = parameter.Range * parameter.Range;
				float minDistance = 0;
				float curArea = 0;
				// 查找距离最近的单位
				foreach (Team team in teams) {
					foreach (KeyValuePair<int, Unit> item in team.AliveUnits.Units) {
						curArea = MathHelp.Area2D (src.MemberTransform.Position, item.Value.MemberTransform.Position);
						if (curArea <= area) {
							if (firstTarget == null) {
								firstTarget = item.Value;
								minDistance = curArea;
							} else if (curArea < minDistance) {
								firstTarget = item.Value;
								minDistance = curArea;
							}
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
	}
}

