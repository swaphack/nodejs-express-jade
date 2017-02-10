using System;
using Game.Table;
using Foundation.DataBase;

namespace Game.Helper
{
	public class XmlHelp
	{
		private XmlHelp ()
		{
		}

		/// <summary>
		/// 加载xml配置表
		/// 配置格式为：
		///  	<Text ID="101">伐木场</Text>
		/// 	<Text ID="101">伐木场</Text>
		/// 不能含有子项
		/// </summary>
		/// <returns>The xml.</returns>
		/// <param name="configPath">配置路径</param>
		/// <param name="tableName">表名称</param>
		public static IDataTable LoadSimpleXml(string configPath, string tableName = "")
		{
			TableConfig loadStep = new TableConfig (tableName, configPath);
			if (loadStep.Load () == false) {
				return null;
			}
			return loadStep.TableData;
		}
	}
}

