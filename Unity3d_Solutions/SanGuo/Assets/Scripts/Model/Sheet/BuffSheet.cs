using System;
using Model.Base;
using Model.Buff;

namespace Model.Sheet
{
	/// <summary>
	/// 状态表
	/// </summary>
	public class BuffSheet : ModelSheet<BuffModel>
	{
		public BuffSheet ()
			:base(ModelType.Buff)
		{
		}
	}
}

