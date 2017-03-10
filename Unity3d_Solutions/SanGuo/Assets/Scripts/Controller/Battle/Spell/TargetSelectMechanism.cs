using System;
using System.Collections.Generic;
using Controller.Battle;

namespace Controller.Battle.Spell
{
	/// <summary>
	/// 目标选择机制
	/// </summary>
	public partial class TargetSelectMechanism
	{
		/// <summary>
		/// 静态实例
		/// </summary>
		private static TargetSelectMechanism  _TSM;

		/// <summary>
		/// 目标选择规则
		/// </summary>
		private Dictionary<TargetSelectType, ITargetSelect> _TSRule;
		
		private TargetSelectMechanism ()
		{
			_TSRule = new  Dictionary<TargetSelectType, ITargetSelect> ();
			Init ();
		}

		/// <summary>
		/// 静态实例
		/// </summary>
		public static TargetSelectMechanism Instance {
			get {
				if (_TSM == null) {
					_TSM = new 	TargetSelectMechanism();
				}

				return _TSM;
			}
		}

		/// <summary>
		/// 查找目标
		/// </summary>
		/// <returns><c>true</c>, if targets was found, <c>false</c> otherwise.</returns>
		/// <param name="src">Source.</param>
		/// <param name="field">Field.</param>
		/// <param name="parameters">Parameters.</param>
		public SelectedTarget FindTargets(TargetSelectType type, Unit src, Field field, ISelectedTargetParameters parameters)
		{
			if (src == null || field == null) {
				return null;
			}

			if (!_TSRule.ContainsKey (type)) {
				return null;
			}

			ITargetSelect handler = _TSRule [type];
			if (!handler.FindTargets (src, field, parameters)) {
				return null;
			}

			return handler.SelectedTarget;
		}
	}
}

