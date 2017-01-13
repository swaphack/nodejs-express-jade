using System;
using Foundation.DataBase;

namespace Game
{
	public class XmlHelp
	{
		private XmlHelp ()
		{
		}

		/// <summary>
		/// 加载xml配置表
		/// 配置格式为： <Text ID="101">伐木场</Text>
		/// 不能含有子项
		/// </summary>
		/// <returns>The xml.</returns>
		/// <param name="configPath">配置路径</param>
		/// <param name="tableName">表名称</param>
		public static DataTable LoadSimpleXml(string configPath, string tableName = "")
		{
			DataTable table = new DataTable (tableName);
			DataLoadStep loadStep = new DataLoadStep (tableName, configPath, table, delegate (string filepath) {
				return FilePathHelp.GetXmlFileData(filepath);
			});
			if (loadStep.Load () == false) {
				return null;
			}

			return table;
		}
	}
}

