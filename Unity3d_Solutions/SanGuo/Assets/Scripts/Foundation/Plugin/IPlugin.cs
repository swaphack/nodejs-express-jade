using System;

namespace Foundation.Plugin
{
	/// <summary>
	/// 插件接口
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// 初始化
		/// </summary>
		void Init();
		/// <summary>
		/// 定时更新
		/// </summary>
		void Update();
	}
}

