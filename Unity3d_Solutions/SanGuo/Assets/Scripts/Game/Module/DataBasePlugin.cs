using System;
using Foundation.Plugin;
using Foundation.DataBase;
using Game.Table;
using Data;

namespace Game.Module
{
	/// <summary>
	/// 数据库插件
	/// </summary>
	public class DataBasePlugin : IPlugin
	{
		/// <summary>
		/// 数据库
		/// </summary>
		private DataBase _DataBase;

		/// <summary>
		/// 数据库
		/// </summary>
		/// <value>The data base.</value>
		public DataBase DataBase {
			get { 
				return _DataBase;
			}
		}

		public DataBasePlugin ()
		{
			_DataBase = new DataBase ();
		}

		/// <summary>
		/// 获取表
		/// </summary>
		/// <returns>The table.</returns>
		/// <param name="name">表名称</param>
		public IDataTable GetTable (string name)
		{
			if (string.IsNullOrEmpty (name)) {
				return null;
			}
			return _DataBase.GetTable (name);
		}

		/// <summary>
		/// 加载xml文件
		/// </summary>
		/// <param name="xmlFileName">Xml file name.</param>
		/// <param name="xmlFilePath">Xml file path.</param>
		public void AddXmlFile(string xmlFileName, string xmlFilePath)
		{
			if (string.IsNullOrEmpty (xmlFileName) || string.IsNullOrEmpty (xmlFilePath)) {
				return;
			}

			_DataBase.AddStep (new TableConfig(xmlFileName, xmlFilePath));
		}

		/// <summary>
		/// 插件标识
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get {
				return (int)ModuleType.DataBase;		
			} 
		}
		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			AddXmlFile(XmlFileName.DataBaseDataAdminLevel, XmlFilePath.DataBaseDataAdminLevel);
			AddXmlFile(XmlFileName.DataBaseDataBuildLevel, XmlFilePath.DataBaseDataBuildLevel);
			AddXmlFile(XmlFileName.DataBaseDataBuildProperty, XmlFilePath.DataBaseDataBuildProperty);
			AddXmlFile(XmlFileName.DataBaseDataBuildType, XmlFilePath.DataBaseDataBuildType);
			AddXmlFile(XmlFileName.DataBaseDataResourceType, XmlFilePath.DataBaseDataResourceType);
			AddXmlFile(XmlFileName.DataBaseDataRoleLevel, XmlFilePath.DataBaseDataRoleLevel);
		}
		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(float dt)
		{
			_DataBase.LoadNextStep ();
		}
		/// <summary>
		/// 销毁
		/// </summary>
		public void Dispose()
		{
			_DataBase.Clear ();
		}
	}
}

