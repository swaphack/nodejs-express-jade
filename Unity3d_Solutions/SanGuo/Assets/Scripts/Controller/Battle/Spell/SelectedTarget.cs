using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Battle.Spell
{
	/// <summary>
	/// 选中的目标
	/// </summary>
	public class SelectedTarget
	{
		/// <summary>
		/// 目标
		/// </summary>
		private List<Unit> _Targets;
		/// <summary>
		/// 第一目标
		/// </summary>
		private Unit _FirstTarget;
		/// <summary>
		/// 目标
		/// </summary>
		/// <value>The targets.</value>
		public List<Unit> Targets { 
			get {
				return _Targets;		
			} 
		}

		/// <summary>
		/// 第一目标
		/// </summary>
		/// <value>The first target.</value>
		public Unit FirstTarget { 
			get { 
				return _FirstTarget;
			}
		}

		/// <summary>
		/// 第一目标是否可用
		/// </summary>
		/// <value><c>true</c> if this instance is first target enabled; otherwise, <c>false</c>.</value>
		public bool IsFirstTargetEnabled {
			get { 
				return _FirstTarget != null && !_FirstTarget.Property.Dead;
			}
		}

		public SelectedTarget (List<Unit> targets, Unit firstTarget )
		{
			if (targets == null || firstTarget == null) {
				return;
			}
			_Targets = targets;
			_FirstTarget = firstTarget;
		}

		/// <summary>
		/// 选择最近目标
		/// </summary>
		/// <returns><c>true</c>, if nearest target was selected, <c>false</c> otherwise.</returns>
		/// <param name="src">Source.</param>
		public bool SelectNearestTarget(Unit src)
		{
			if (src == null) {
				return false;
			}

			// 最近选择得目标
			if (_FirstTarget == null || _FirstTarget.Property.Dead) {
				_FirstTarget = FindNearestTarget (src);
			}

			return _FirstTarget != null;
		}

		/// <summary>
		/// 查找最近敌人
		/// </summary>
		/// <returns>The nearest target.</returns>
		/// <param name="src">Source.</param>
		private Unit FindNearestTarget(Unit src)
		{
			if (src == null || _Targets == null || _Targets.Count == 0) {
				return null;
			}
			Unit target = null;
			float minDistance = 0;
			int count = _Targets.Count;
			Unit item = null;
			for (int i = 0; i < count; i++) {
				item = _Targets [i];
				// 目标死亡
				if (item.Property.Dead) {
					continue;
				}
				float dist = Vector3.Distance (src.MemberTransform.Position, item.MemberTransform.Position);
				if (target == null) {
					target = item;
					minDistance = dist;
				} else if (dist < minDistance) {
					target = item;
					minDistance = dist;
				}
			}

			return target;
		} 

		/// <summary>
		/// 重置第一目标
		/// </summary>
		public void ResetFirstTarget()
		{
			_FirstTarget = null;
		}
	}
}

