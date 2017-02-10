using System;
using System.Collections.Generic;

namespace Foundation.Plugin
{
	/// <summary>
	/// 插件中心
	/// </summary>
	public class PluginCenter : IDisposable
	{
		/// <summary>
		/// 插件集
		/// </summary>
		private HashSet<IPlugin> _Plugins;

		/// <summary>
		/// 构造函数
		/// </summary>
		public PluginCenter ()
		{
			_Plugins = new HashSet<IPlugin> ();
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
		/// 获取插件
		/// </summary>
		/// <returns>The plugin.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetPlugin<T>() where T : IPlugin
		{
			foreach (IPlugin plugin in _Plugins) {
				if (plugin.GetType () == typeof(T)) {
					return (T)plugin;
				}
			}

			return default(T);
		}

		/// <summary>
		/// 获取插件
		/// </summary>
		/// <returns>The plugin.</returns>
		/// <param name="pluginID">Plugin I.</param>
		public IPlugin GetPlugin(int pluginID)
		{
			foreach (IPlugin plugin in _Plugins) {
				if (plugin.ID == pluginID) {
					return plugin;
				}
			}

			return null;
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			foreach (IPlugin plugin in _Plugins) {
				plugin.Init ();
			}
		}


		/// <summary>
		/// 定时更新
		/// </summary>
		public void Update(float dt)
		{
			foreach (IPlugin plugin in _Plugins) {
				plugin.Update (dt);
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Foundation.Plugin.PluginCenter"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Foundation.Plugin.PluginCenter"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Foundation.Plugin.PluginCenter"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the <see cref="Foundation.Plugin.PluginCenter"/>
		/// so the garbage collector can reclaim the memory that the <see cref="Foundation.Plugin.PluginCenter"/> was occupying.</remarks>
		public void Dispose()
		{
			foreach (IPlugin plugin in _Plugins) {
				plugin.Dispose ();
			}

			_Plugins.Clear ();
		}
	}
}

