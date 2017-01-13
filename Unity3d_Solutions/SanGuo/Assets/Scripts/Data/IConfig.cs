using System;

namespace Data
{
	/// <summary>
	/// 配置接口
	/// </summary>
	public interface IConfig
	{
		/// <summary>
		/// 配置路径
		/// </summary>
		/// <value>The config path.</value>
		string ConfigPath { get; }
		/// <summary>
		/// 加载
		/// </summary>
		bool Load ();
		/// <summary>
		/// 清空
		/// </summary>
		bool Clear ();
	}
}

