using System;
using UnityEngine;
using System.Collections.Generic;
using Foundation.Notify;
using Game.Helper;
using Game.Platform;
using Foundation.Plugin;

namespace Game.Module
{
	/// <summary>
	/// 平台
	/// </summary>
	public class DevicePlugin : IPlugin
	{
		/// <summary>
		/// 按键
		/// </summary>
		private KeyBoard _KeyBoard;
		/// <summary>
		/// 触摸
		/// </summary>
		private TouchCenter _TouchCenter;

		public DevicePlugin()
		{
			_KeyBoard = new KeyBoard ();
			_TouchCenter = new TouchCenter ();
		}

		/// <summary>
		/// 添加返回键点击处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void AddEscKeyHandler(NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			AddKeyHandler (KeyCode.Escape, KeyPhase.Began, handler);
		}

		/// <summary>
		/// 移除返回键点击处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void RemoveEscKeyHandler(NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			RemoveKeyHandler (KeyCode.Escape, KeyPhase.Began, handler);
		}

		/// <summary>
		/// 添加按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="phase">KeyPhase.</param>
		/// <param name="handler">Handler.</param>
		public void AddKeyHandler(KeyCode code, KeyPhase phase, NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			KeyPress keyPress;
			keyPress.Key = code;
			keyPress.Status = phase;
			_KeyBoard.AddListener ( keyPress, handler);
		}

		/// <summary>
		/// 移除按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="phase">KeyPhase.</param>
		/// <param name="handler">Handler.</param>
		public void RemoveKeyHandler(KeyCode code, KeyPhase phase, NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			KeyPress keyPress = new KeyPress ();
			keyPress.Key = code;
			keyPress.Status = phase;
			_KeyBoard.RemoveListener (keyPress, handler);
		}

		/// <summary>
		/// 添加按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="handler">Handler.</param>
		public void AddKeyDownHandler(KeyCode code, NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			AddKeyHandler (code, KeyPhase.Began, handler);
			AddKeyHandler (code, KeyPhase.Keeped, handler);
		}

		/// <summary>
		/// 移除按键点击处理
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="handler">Handler.</param>
		public void RemoveKeyDownHandler(KeyCode code, NotifyHandler handler) 
		{
			if (handler == null) {
				return;
			}
			RemoveKeyHandler (code, KeyPhase.Began, handler);
			RemoveKeyHandler (code, KeyPhase.Keeped, handler);
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
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.Device;
			} 
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			Log.Info ("Screen.Size, width : " + Screen.width + ", height : " + Screen.height);
			Log.Info ("Application.dataPath : " + Application.dataPath);
			Log.Info ("Application.persistentDataPath : " + Application.persistentDataPath);
			Log.Info ("Application.streamingAssetsPath : " + Application.streamingAssetsPath);
			Log.Info ("Application.temporaryCachePath : " + Application.temporaryCachePath);
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		public void Update(float dt)
		{
			_KeyBoard.Update ();
			_TouchCenter.Update ();
		}

		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_KeyBoard.Clear ();
			_TouchCenter.Clear ();
		}
	}
}

