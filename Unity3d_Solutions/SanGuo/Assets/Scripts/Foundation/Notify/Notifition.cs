using System;
using System.Collections.Generic;

namespace Foundation.Notify
{
	/// <summary>
	/// 推送管理
	/// </summary>
	public class Notifition<T>
	{
		/// <summary>
		/// 推送监听器
		/// </summary>
		protected Dictionary<T, NotifyEvent> _NotifyListeners;

		public Notifition ()
		{
			_NotifyListeners = new Dictionary<T, NotifyEvent> ();
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
				_NotifyListeners [key] = new NotifyEvent ();
			}

			NotifyEvent listener = _NotifyListeners[key];

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

			NotifyEvent listener = _NotifyListeners[key];

			listener.RemoveNotify (handler);
		}

		/// <summary>
		/// 注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="handler">Handler.</param>
		public void AddListener (T key, NotifyHandlerWithParameter handler)
		{
			if (handler == null) {
				return;
			}

			if (_NotifyListeners.ContainsKey (key) == false) {
				_NotifyListeners [key] = new NotifyEvent ();
			}

			NotifyEvent listener = _NotifyListeners[key];

			listener.AddNotify (handler);
		}

		/// <summary>
		/// 移除注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="handler">Handler.</param>
		public void RemoveListener (T key, NotifyHandlerWithParameter handler)
		{
			if (handler == null) {
				return;
			}

			if (_NotifyListeners.ContainsKey (key) == false) {
				return;
			}

			NotifyEvent listener = _NotifyListeners[key];

			listener.RemoveNotify (handler);
		}

		/// <summary>
		/// 移除一类注册监听
		/// </summary>
		/// <param name="key">Key.</param>
		public void RemoveOneKindListeners (T key)
		{
			if (_NotifyListeners.ContainsKey (key)) {
				_NotifyListeners.Remove (key);
			}
		}

		/// <summary>
		/// 推送
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="parameter">Object.</param>
		public void Notify(T key, object parameter)
		{
			if (_NotifyListeners.ContainsKey (key) == false) {
				return;
			}

			_NotifyListeners [key].Dispatch(parameter);
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
		public Dictionary<T, NotifyEvent>.KeyCollection GetKeys() {
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

