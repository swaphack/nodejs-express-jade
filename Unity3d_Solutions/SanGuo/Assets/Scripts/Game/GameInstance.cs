using System;
using UnityEngine;

using Foundation.Net;

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
		public const string SERVER_IP = "10.0.22.63";
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
		/// The touch controller.
		/// </summary>
		private TouchCenter _TouchCenter;
		/// <summary>
		/// 文本
		/// </summary>
		private Message _Text;
		/// <summary>
		/// 游戏静态实例
		/// </summary>
		private static GameInstance s_GameInstance;
	
		/// <summary>
		/// 网络中心
		/// </summary>
		public NetCenter Net { get { return _NetCenter; } }

		/// <summary>
		/// 点击派发中心
		/// </summary>
		public TouchCenter Touch { get { return _TouchCenter; } }
		/// <summary>
		/// 文本
		/// </summary>
		public Message Text { get { return _Text; } }
		
		public GameInstance ()
		{
			_NetCenter = new NetCenter ();
			_NetCenter.TryConnectMaxCount = TryConnectCount;
			_NetCenter.FinalConnectHandler = FinalConnectHandler;
			_NetCenter.Client.OnConnect += OnConnectState;

			_TouchCenter = new TouchCenter ();

			_Text = new Message ();

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
		public void Start()
		{			
			_NetCenter.Init (SERVER_IP, SERVER_PORT);

			_Text.InitConfig ();
			_Text.SetLanguage (LanguagueType.CHINA);
		}
		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			//_NetCenter.Update ();
			_TouchCenter.Update ();
		}
		/// <summary>
		/// 连接状态回调处理
		/// </summary>
		/// <param name="state">连接状态</param>
		private void OnConnectState(ConnectState state)
		{
			if (state == ConnectState.Disconnected) 
			{
				Debug.Log ("Disconnect Server");
			} 
			else
			{
				Debug.Log ("Connect Server");
			}
		}

		/// <summary>
		/// 尝试连接失败回调处理
		/// </summary>
		private void FinalConnectHandler()
		{
			Debug.Log ("Over Connect Server Count");
		}
	}
}

