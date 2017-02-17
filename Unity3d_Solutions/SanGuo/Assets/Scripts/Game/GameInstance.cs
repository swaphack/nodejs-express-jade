using System;
using System.Collections;
using UnityEngine;
using Game.Module;
using Foundation.Plugin;
using Controller.Role;
using Game.Helper;

namespace Game
{
	/// <summary>
	/// 游戏实例
	/// </summary>
	public class GameInstance : MonoBehaviour
	{
		/// <summary>
		/// 上次更新时间
		/// </summary>
		private DateTime _LastUpdateTime;
		/// <summary>
		/// 插件管理中心
		/// </summary>
		private PluginCenter _PluginCenter;
		/// <summary>
		/// 临时插件管理中心
		/// </summary>
		private PluginCenter _TempPluginCenter;

		/// <summary>
		/// 游戏静态实例
		/// </summary>
		private static GameInstance s_GameInstance;

		public GameInstance ()
		{
			// 更新时间
			_LastUpdateTime = DateTime.MinValue;

			s_GameInstance = this;

			_PluginCenter = new PluginCenter ();
			_TempPluginCenter = new PluginCenter ();

			// 注册模块
			RegisterModules ();
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
			_PluginCenter.Init ();

			// 日志
			Log.Init();

			// 玩家信息
			Player.MainPlayer.Init ();
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update()
		{
			float dt = GetDeltaTime ();
			_PluginCenter.Update (dt);
			_TempPluginCenter.Update (dt);
		}

		void OnDestory()
		{
			_PluginCenter.Dispose ();
			_TempPluginCenter.Dispose ();
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
		/// 获取插件
		/// </summary>
		/// <returns>The plugin.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetPlugin<T>() where T : IPlugin
		{
			return _PluginCenter.GetPlugin<T>();
		}

		/// <summary>
		/// 添加临时插件
		/// </summary>
		/// <param name="plugin">Plugin.</param>
		public void AddTempPlugin(IPlugin plugin)
		{
			if (plugin == null) {
				return;
			}

			_TempPluginCenter.AddPlugin (plugin);
		}

		/// <summary>
		/// 移除临时插件
		/// </summary>
		/// <param name="plugin">Plugin.</param>
		public void RemoveTempPlugin(IPlugin plugin)
		{
			if (plugin == null) {
				return;
			}

			_TempPluginCenter.RemovePlugin (plugin);
		}

		/// <summary>
		/// 注册模块
		/// </summary>
		private void RegisterModules()
		{
			_PluginCenter.AddPlugin (new NetPlugin ());
			_PluginCenter.AddPlugin (new TextPlugin ());
			_PluginCenter.AddPlugin (new ActionPlugin ());
			_PluginCenter.AddPlugin (new DevicePlugin ());
			_PluginCenter.AddPlugin (new DataBasePlugin ());
			_PluginCenter.AddPlugin (new UIPlugin ());
		}

		/// <summary>
		/// 网络
		/// </summary>
		public NetPlugin Net { 
			get { 
				return GetPlugin<NetPlugin>(); 
			} 
		}

		/// <summary>
		/// 文本
		/// </summary>
		public TextPlugin Text { 
			get { 
				return GetPlugin<TextPlugin>(); 
			} 
		}

		/// <summary>
		/// 动作
		/// </summary>
		/// <value>The action.</value>
		public ActionPlugin Action { 
			get {  
				return GetPlugin<ActionPlugin>(); 
			} 
		}

		/// <summary>
		/// 设备平台
		/// </summary>
		/// <value>The device.</value>
		public DevicePlugin Device { 
			get { 
				return GetPlugin<DevicePlugin>(); 
			} 
		}
	}
}

