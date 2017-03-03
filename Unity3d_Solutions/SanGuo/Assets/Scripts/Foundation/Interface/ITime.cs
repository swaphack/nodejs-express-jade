using System;

namespace Foundation.Interface
{
	/// <summary>
	/// 定时器
	/// </summary>
	public interface ITime
	{
		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="delta">间隔</param>
		void Update (float delta);
	}
}

