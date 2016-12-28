using System;

namespace Game
{
	/// <summary>
	/// 推送处理
	/// </summary>
	public delegate void NotifyHandler();
	/// <summary>
	/// 推送监听器
	/// </summary>
	public class NotifyListener
	{
		/// <summary>
		/// 监听事件
		/// </summary>
		private event NotifyHandler _NotifyEvent;

		/// <summary>
		/// 派送
		/// </summary>
		public void Dispatch()
		{
			if (_NotifyEvent != null) {
				_NotifyEvent ();
			}
		}

		/// <summary>
		/// 添加推送处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void AddNotify(NotifyHandler handler)
		{
			if (handler != null) {
				_NotifyEvent += handler;
			}
		}

		/// <summary>
		/// 移除推送处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void RemoeNotify(NotifyHandler handler)
		{
			if (handler != null) {
				_NotifyEvent -= handler;
			}
		}
	}
}

