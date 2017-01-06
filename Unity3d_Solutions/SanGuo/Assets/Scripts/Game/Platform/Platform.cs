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
		/// 按键
		/// </summary>
		private KeyBoard _KeyBoard;
		/// <summary>
		/// 触摸
		/// </summary>
		private TouchCenter _TouchCenter;

		public Platform()
		{
			_KeyBoard = new KeyBoard ();
			_TouchCenter = new TouchCenter ();
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
		/// 添加按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="handler">Handler.</param>
		public void AddKeyHandler(KeyCode code,NotifyHandler handler) {
			if (handler == null) {
				return;
			}
			_KeyBoard.AddListener (KeyCode.Escape, handler);
		}

		/// <summary>
		/// 移除按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="handler">Handler.</param>
		public void RemoveKeyHandler(KeyCode code, NotifyHandler handler) {
			if (handler == null) {
				return;
			}
			_KeyBoard.RemoveListener (code, handler);
		}

		/// <summary>
		/// 添加点击派发
		/// </summary>
		/// <param name="dispatcher">Dispatcher.</param>
		public void AddTouchDispatcher (ITouchDispatcher dispatcher)
		{
			if (dispatcher == null) {
				return;
			}

			_TouchCenter.AddDispatcher (dispatcher);
		}
		/// <summary>
		/// 移除点击派发
		/// </summary>
		/// <param name="dispatcher">Dispatcher.</param>
		public void RemoveTouchDispatcher (ITouchDispatcher dispatcher)
		{
			if (dispatcher == null) {
				return;
			}

			_TouchCenter.RemoveDispatcher (dispatcher);
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		public void Update()
		{
			_KeyBoard.Update ();
			_TouchCenter.Update ();
		}
	}
}

