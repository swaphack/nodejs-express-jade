﻿using System;
using System.Collections;
using UnityEngine;
using Foundation.Net;
using Control;

namespace Game
{
	/// <summary>
	/// 游戏实例
	/// </summary>
	public class GameInstance : MonoBehaviour
	{
		/// <summary>
		/// 服务器地址
		/// </summary>
		public const string SERVER_IP = "127.0.0.1";
		/// <summary>
		/// 服务器端口
		/// </summary>
		public const int SERVER_PORT = 9547;
		/// <summary>
		/// 最大尝试连接次数
		/// </summary>
		public const int TryConnectCount = -1;

		/// <summary>
		/// 网络中心
		/// </summary>
		private NetCenter _NetCenter;
		/// <summary>
		/// 网络是否可用
		/// </summary>
		private bool _IsNetEnable;

		/// <summary>
		/// 文本
		/// </summary>
		private Message _Text;
		/// <summary>
		/// 平台
		/// </summary>
		private Platform _Platform;
		/// <summary>
		/// 动作
		/// </summary>
		private ActionCenter _Action;
		/// <summary>
		/// 上次更新时间
		/// </summary>
		private DateTime _LastUpdateTime;

		/// <summary>
		/// 游戏静态实例
		/// </summary>
		private static GameInstance s_GameInstance;
	
		/// <summary>
		/// 网络中心
		/// </summary>
		public NetCenter Net { get { return _NetCenter; } }
		/// <summary>
		/// 网络是否可用
		/// </summary>
		/// <value><c>true</c> if this instance is net enable; otherwise, <c>false</c>.</value>
		public bool IsNetEnable { get { return _IsNetEnable; } set { _IsNetEnable = value;} }
		/// <summary>
		/// 文本
		/// </summary>
		public Message Text { get { return _Text; } }
		/// <summary>
		/// 动作
		/// </summary>
		/// <value>The action.</value>
		public ActionCenter Action { get {  return _Action; } }

		public Platform Platform { 
			get { 
				return _Platform;
			}
		}
		
		public GameInstance ()
		{
			// 网络
			_NetCenter = new NetCenter ();
			_NetCenter.TryConnectMaxCount = TryConnectCount;
			_NetCenter.FinalConnectHandler = FinalConnectHandler;
			_NetCenter.AddConnectStateListener (OnConnectState);

			IsNetEnable = true;

			// 语言文本
			_Text = new Message ();
			// 动作
			_Action = new ActionCenter ();
			// 平台
			_Platform = new Platform ();
			// 日志
			Log.Init();

			// 更新时间
			_LastUpdateTime = DateTime.MinValue;

			s_GameInstance = this;
		}
		/// <summary>
		/// 游戏实例
		/// </summary>
		/// <returns>The instance.</returns>
		public static GameInstance GetInstance()
		{
			return s_GameInstance;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void Start()
		{
			ServerConfig config = new ServerConfig ();
			if (config.Load () == true) {
				IsNetEnable = config.IsSocketEnable;
				if (IsNetEnable)
					_NetCenter.Init (config.IPAddress, config.Port);
			}

			_Text.InitConfig ();
			_Text.SetLanguage (LanguagueType.CHINA);

			FilePathHelp.ShowPath ();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
			if (IsNetEnable == true) {
				_NetCenter.Update ();
			}

			_Platform.Update ();
			_Action.Update ( GetDeltaTime() );

		}

		/// <summary>
		/// 获取时间间隔
		/// </summary>
		/// <returns>The delta time.</returns>
		private float GetDeltaTime() 
		{
			DateTime dateTime = DateTime.Now;
			if (_LastUpdateTime == DateTime.MinValue) {
				_LastUpdateTime = dateTime;
			}
			TimeSpan timeSpane = dateTime - _LastUpdateTime;
			float dt = (float)(timeSpane.TotalMilliseconds / 1000);
			_LastUpdateTime = dateTime;
			return dt;
		}

		/// <summary>
		/// 连接状态回调处理
		/// </summary>
		/// <param name="state">连接状态</param>
		private void OnConnectState(ConnectState state)
		{
			if (state == ConnectState.Disconnected) 
			{
				//Log.Info ("Disconnect Server");
			} 
			else
			{
				//Log.Info ("Connect Server");
			}
		}

		/// <summary>
		/// 尝试连接失败回调处理
		/// </summary>
		private void FinalConnectHandler()
		{
			//Log.Info ("Over Connect Server Count");
		}
	}
}

