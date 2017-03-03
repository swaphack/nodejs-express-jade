using System;

namespace Foundation.Interface
{
	/// <summary>
	/// 定时控制
	/// </summary>
	public interface IControl
	{
		/// <summary>
		/// 开始
		/// </summary>
		void Start();
		/// <summary>
		/// 暂停
		/// </summary>
		void Pause();
		/// <summary>
		/// 恢复
		/// </summary>
		void Resume();
		/// <summary>
		/// 停止
		/// </summary>
		void Stop();
	}
}

