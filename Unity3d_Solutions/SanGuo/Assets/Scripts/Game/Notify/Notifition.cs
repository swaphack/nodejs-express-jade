using System;
using System.Collections.Generic;

namespace Game
{
	/// <summary>
	/// 推送管理
	/// </summary>
	public class Notifition<T>
	{
		/// <summary>
		/// 推送监听器
		/// </summary>
		protected Dictionary<T, NotifyListener> _NotifyListeners;

		public Notifition ()
		{
			_NotifyListeners = new Dictionary<T, NotifyListener> ();
		}

		/// <summary>
		/// 注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="handler">Handler.</param>
		public void AddListener (T key, NotifyHandler handler)
		{
			if (handler == null) {
				return;
			}

			if (_NotifyListeners.ContainsKey (key) == false) {
				_NotifyListeners [key] = new NotifyListener ();
			}

			NotifyListener listener = _NotifyListeners[key];

			listener.AddNotify (handler);
		}

		/// <summary>
		/// 移除注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="handler">Handler.</param>
		public void RemoveListener (T key, NotifyHandler handler)
		{
			if (handler == null) {
				return;
			}

			if (_NotifyListeners.ContainsKey (key) == false) {
				return;
			}

			NotifyListener listener = _NotifyListeners[key];

			listener.RemoeNotify (handler);
		}

		/// <summary>
		/// 移除一类注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		public void RemoveOneKindListeners (T key)
		{
			if (_NotifyListeners.ContainsKey (key) == false) {
				return;
			}

			_NotifyListeners.Remove (key);
		}

		/// <summary>
		/// 推送
		/// </summary>
		/// <param name="key">Key.</param>
		public void Notify(T key)
		{
			if (_NotifyListeners.ContainsKey (key) == false) {
				return;
			}

			_NotifyListeners [key].Dispatch();
		}


		/// <summary>
		/// 主键
		/// </summary>
		/// <value>The keys.</value>
		public Dictionary<T, NotifyListener>.KeyCollection GetKeys() {
			return _NotifyListeners.Keys;
		}

		/// <summary>
		/// 清除
		/// </summary>
		public void Clear()
		{
			_NotifyListeners.Clear ();
		}
	}
}

