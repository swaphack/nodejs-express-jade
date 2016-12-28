using UnityEngine;
using System.Collections;

namespace Foundation.DataBase
{
	/// <summary>
	/// 数据库
	/// </summary>
	public interface IDataBase
	{
		/// <summary>
		/// 初始化
		/// </summary>
		void Init ();

		/// <summary>
		/// 清空数据
		/// </summary>
		void Clear ();

		/// <summary>
		/// 获取表
		/// </summary>
		/// <returns>表</returns>
		/// <param name="name">表名称</param>
		IDataTable GetTable (string name);

		/// <summary>
		/// 添加表
		/// </summary>
		/// <param name="name">表名称</param>
		/// <param name="table">表</param>
		void AddTable (string name, IDataTable table);

		/// <summary>
		/// 移除表
		/// </summary>
		/// <param name="name">表名称</param>
		void RemoveTable (string name);

		/// <summary>
		/// 清空表
		/// </summary>
		void ClearTables ();

		/// <summary>
		/// 添加加载步骤
		/// </summary>
		/// <param name="step">加载步骤</param>
		void AddStep (IDataLoadStep step);

		/// <summary>
		/// 移除加载步骤
		/// </summary>
		/// <param name="name">步骤名称</param>
		void RemoveStep (string name);

		/// <summary>
		/// 清除所有加载步骤
		/// </summary>
		void ClearSteps ();
	}
}