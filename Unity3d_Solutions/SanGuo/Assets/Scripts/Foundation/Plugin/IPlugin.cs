using System;

namespace Foundation.Plugin
{
	/// <summary>
	/// 插件接口
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		int ID { get; }
		/// <summary>
		/// 初始化
		/// </summary>
		void Init();
		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		void Update(float dt);
		/// <summary>
		/// 销毁
		/// </summary>
		void Dispose();
	}
}

