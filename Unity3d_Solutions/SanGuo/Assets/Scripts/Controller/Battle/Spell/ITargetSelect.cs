using Controller.Battle;

namespace Controller.Battle.Spell
{
	/// <summary>
	/// 目标选择
	/// </summary>
	public interface ITargetSelect
	{
		/// <summary>
		/// 选中目标
		/// </summary>
		/// <value>The target.</value>
		SelectedTarget SelectedTarget { get; }
		/// <summary>
		/// 查找目标
		/// </summary>
		/// <returns><c>true</c>, if targets was found, <c>false</c> otherwise.</returns>
		/// <param name="src">Source.</param>
		/// <param name="field">Field.</param>
		/// <param name="parameters">Parameters.</param>
		bool FindTargets(Unit src, Field field, ISelectedTargetParameters parameters);
	}
}

