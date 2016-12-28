using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	/// <summary>
	/// 平台
	/// </summary>
	public class Platform
	{
		/// <summary>
		/// 按键推送
		/// </summary>
		private KeyBoard _KeyBoard;

		public Platform()
		{
			_KeyBoard = new KeyBoard ();
		}

		/// <summary>
		/// 添加返回键点击处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void AddEscKeyHandler(NotifyHandler handler) {
			if (handler == null) {
				return;
			}
			_KeyBoard.AddListener (KeyCode.Escape, handler);
		}

		/// <summary>
		/// 移除返回键点击处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void RemoveEscKeyHandler(NotifyHandler handler) {
			if (handler == null) {
				return;
			}
			_KeyBoard.RemoveListener (KeyCode.Escape, handler);
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		public void Update()
		{
			Dictionary<KeyCode, NotifyListener>.KeyCollection keys = _KeyBoard.GetKeys ();
			if (keys == null || keys.Count == 0) {
				return;
			}
			foreach (KeyCode key in keys) {
				if (Input.GetKeyDown (key) == true) {
					_KeyBoard.Notify (key);
				}
			}
		}
	}
}

