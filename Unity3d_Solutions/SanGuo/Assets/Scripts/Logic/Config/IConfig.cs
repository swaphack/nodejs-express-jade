using System;

namespace Logic
{
	/// <summary>
	/// 配置
	/// </summary>
	public interface IConfig
	{
		/// <summary>
		/// 配置文件路径
		/// </summary>
		/// <value>The filepath.</value>
		string FilePath { get; }

		/// <summary>
		/// 是否已加载
		/// </summary>
		/// <value><c>true</c> if this instance is load; otherwise, <c>false</c>.</value>
		bool IsLoad { get; }

		/// <summary>
		/// 配置是否有错误
		/// </summary>
		/// <value><c>true</c> if this instance is error; otherwise, <c>false</c>.</value>
		bool IsError { get; }

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