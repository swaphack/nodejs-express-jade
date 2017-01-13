using System;

namespace Foundation.Notify
{
	/// <summary>
	/// 推送项
	/// </summary>
	public class NotifyPair
	{
		/// <summary>
		/// 参数类型
		/// </summary>
		public Type ParamType;
		/// <summary>
		/// 推送处理
		/// </summary>
		public NotifyHandler Handler;
	}
}

