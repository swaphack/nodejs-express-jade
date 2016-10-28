using System;

namespace Game
{
	/// <summary>
	/// 动作接口
	/// </summary>
	public interface IAction
	{
		/// <summary>
		/// 是否完成动作，如果完成移除
		/// </summary>
		/// <value><c>true</c> if this instance is finish; otherwise, <c>false</c>.</value>
		bool IsFinish { get; }
		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update(float dt);
	}
}

