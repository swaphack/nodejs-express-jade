using System;
using System.Collections.Generic;

namespace Foundation.Plugin
{
	/// <summary>
	/// 插件中心
	/// </summary>
	public class PluginCenter
	{
		/// <summary>
		/// 插件集
		/// </summary>
		private List<IPlugin> _Plugins;

		/// <summary>
		/// 构造函数
		/// </summary>
		public PluginCenter ()
		{
			_Plugins = new List<IPlugin> ();
		}

		/// <summary>
		/// 添加插件
		/// </summary>
		/// <param name="plugin">Plugin.</param>
		public void AddPlugin(IPlugin plugin)
		{
			if (plugin == null) {
				return;
			}

			this.RemovePlugin (plugin);

			plugin.Init ();

			_Plugins.Add (plugin);
		}

		/// <summary>
		/// 移除插件
		/// </summary>
		/// <param name="plugin">Plugin.</param>
		public void RemovePlugin(IPlugin plugin) 
		{
			if (plugin == null) {
				return;
			}

			_Plugins.Remove (plugin);
		}

		/// <summary>
		/// 移除所有插件
		/// </summary>
		public void RemoveAllPlugins()
		{
			_Plugins.Clear ();
		}

		/// <summary>
		/// 获取插件
		/// </summary>
		/// <returns>The plugin.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetPlugin<T>() where T : IPlugin
		{
			for (int i = 0; i < _Plugins.Count; i++) {
				if (_Plugins [i].GetType () == typeof(T)) {
					return (T)_Plugins [i];
				}
			}

			return default(T);
		}


		/// <summary>
		/// 定时更新
		/// </summary>
		public void Update()
		{
			for (int i = 0; i < _Plugins.Count; i++) {
				_Plugins [i].Update ();
			}
		}
	}
}

