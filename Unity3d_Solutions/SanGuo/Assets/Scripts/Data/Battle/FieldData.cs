using System;
using System.Collections.Generic;
using Game.Helper;
using System.Xml;
using Model.Battle;
using Model.Base;
using UnityEngine;
using Model.Skill;

namespace Data.Battle
{
	/// <summary>
	/// 战场数据
	/// </summary>
	public class FieldData
	{
		/// <summary>
		/// 地图信息项
		/// </summary>
		public class MapItem
		{
			/// <summary>
			/// 宽度
			/// </summary>
			public int Width;
			/// <summary>
			/// 高度
			/// </summary>
			public int Height;
		}
		/// <summary>
		/// 原型项
		/// </summary>
		public class ResourceItem
		{
			/// <summary>
			/// 资源路径
			/// </summary>
			public string AssetBundlePath;
			/// <summary>
			/// 资源名称
			/// </summary>
			public string Name;
		}

		/// <summary>
		/// 属性项
		/// </summary>
		public class PropertyItem
		{
			public Dictionary<PropertyType, float> Values;

			public PropertyItem()
			{
				Values = new Dictionary<PropertyType, float>();
			}
		}

		/// <summary>
		/// 空间项
		/// </summary>
		public class TransformItem
		{
			/// <summary>
			/// 位置
			/// </summary>
			public Vector3 Position;
			/// <summary>
			/// 旋转
			/// </summary>
			public Vector3 Rotation;
			/// <summary>
			/// 缩放
			/// </summary>
			public Vector3 Scale;
			/// <summary>
			/// 体积
			/// </summary>
			public Vector3 Volume;
			/// <summary>
			/// 中心点
			/// </summary>
			public Vector3 Center;
		}

		/// <summary>
		/// 技能
		/// </summary>
		public class SkillItem
		{
			/// <summary>
			/// 冷却CD
			/// </summary>
			public float CoolDown;
			/// <summary>
			/// 消耗魔法
			/// </summary>
			public int CostMana;
			/// <summary>
			/// 选择半径
			/// </summary>
			public float Radius;
			/// <summary>
			/// 选择半径
			/// </summary>
			public TargetType TargetType;
		}

		/// <summary>
		/// 单位项
		/// </summary>
		public class UnitItem
		{
			/// <summary>
			/// 名称
			/// </summary>
			public string Name;
			/// <summary>
			/// 资源编号
			/// </summary>
			public int ResourceID;
			/// <summary>
			/// 属性编号
			/// </summary>
			public int PropertyID;
			/// <summary>
			/// 空间编号
			/// </summary>
			public int TransformID;
			/// <summary>
			/// 技能配置
			/// </summary>
			public int SkillID;
		}

		/// <summary>
		/// 队伍项
		/// </summary>
		public class TeamItem
		{
			public List<int> UnitIDs;

			public TeamItem()
			{
				UnitIDs = new List<int>();
			}
		}

		/// <summary>
		/// 地图信息项
		/// </summary>
		private MapItem _MapItem;

		/// <summary>
		/// 资源包路径
		/// </summary>
		private Dictionary<int, ResourceItem> _ResourceItems;
		/// <summary>
		/// 属性
		/// </summary>
		private Dictionary<int, PropertyItem> _Properties;
		/// <summary>
		/// 空间
		/// </summary>
		private Dictionary<int, TransformItem> _TransformItems;
		/// <summary>
		/// 技能
		/// </summary>
		private Dictionary<int, SkillItem> _SkillItems;
		/// <summary>
		/// 单位
		/// </summary>
		private Dictionary<int, UnitItem> _UnitItems;
		/// <summary>
		/// 队伍
		/// </summary>
		private Dictionary<int, TeamItem> _TeamItems;


		/// <summary>
		/// 地图信息项
		/// </summary>
		public MapItem MapInfo {
			get { 
				return _MapItem;
			}
		}

		/// <summary>
		/// 资源包路径
		/// </summary>
		public Dictionary<int, ResourceItem> ResourceItems { 
			get { 
				return _ResourceItems; 
			}
		}
		/// <summary>
		/// 属性
		/// </summary>
		public Dictionary<int, PropertyItem> Properties { 
			get { 
				return _Properties; 
			}
		}
		/// <summary>
		/// 空间
		/// </summary>
		public Dictionary<int, TransformItem> TransformItems { 
			get { 
				return _TransformItems; 
			}
		}
		/// <summary>
		/// 技能
		/// </summary>
		public Dictionary<int, SkillItem> SkillItems { 
			get { 
				return _SkillItems; 
			}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public Dictionary<int, UnitItem> UnitItems { 
			get { 
				return _UnitItems; 
			}
		}
		/// <summary>
		/// 队伍
		/// </summary>
		public Dictionary<int, TeamItem> TeamItems { 
			get { 
				return _TeamItems; 
			}
		}

		public FieldData ()
		{
			_MapItem = new MapItem ();
			_ResourceItems = new Dictionary<int, ResourceItem> ();
			_Properties = new Dictionary<int, PropertyItem> ();
			_TransformItems = new Dictionary<int, TransformItem> ();
			_SkillItems = new Dictionary<int, SkillItem> ();
			_UnitItems = new Dictionary<int, UnitItem> ();
			_TeamItems = new Dictionary<int, TeamItem> ();
		}


		/// <summary>
		/// 获取资源配置
		/// </summary>
		/// <returns>The resource item.</returns>
		/// <param name="id">Identifier.</param>
		public ResourceItem GetResourceItem(int id)
		{
			if (!_ResourceItems.ContainsKey (id)) {
				return null;
			}

			return _ResourceItems [id];
		}

		/// <summary>
		/// 获取属性配置
		/// </summary>
		/// <returns>The property item.</returns>
		/// <param name="id">Identifier.</param>
		public PropertyItem GetPropertyItem(int id)
		{
			if (!_Properties.ContainsKey (id)) {
				return null;
			}

			return _Properties [id];
		}

		/// <summary>
		/// 获取控件配置
		/// </summary>
		/// <returns>The transform item.</returns>
		/// <param name="id">Identifier.</param>
		public TransformItem GetTransformItem(int id)
		{
			if (!_TransformItems.ContainsKey (id)) {
				return null;
			}

			return _TransformItems [id];
		}

		/// <summary>
		/// 获取技能配置
		/// </summary>
		/// <returns>The skill item.</returns>
		/// <param name="id">Identifier.</param>
		public SkillItem GetSkillItem(int id)
		{
			if (!_SkillItems.ContainsKey (id)) {
				return null;
			}

			return _SkillItems [id];
		}

		/// <summary>
		/// 获取单位配置
		/// </summary>
		/// <returns>The unit item.</returns>
		/// <param name="id">Identifier.</param>
		public UnitItem GetUnitItem(int id)
		{
			if (!_UnitItems.ContainsKey (id)) {
				return null;
			}

			return _UnitItems [id];
		}

		/// <summary>
		/// 加载地图配置
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadMap(XmlNode node)
		{
			if (node == null) {
				return;
			}
			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				_MapItem.Width = Int32.Parse (element.GetAttribute ("width"));
				_MapItem.Height = Int32.Parse (element.GetAttribute ("height"));
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载模型数据
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadResource(XmlNode node)
		{
			if (node == null) {
				return;
			}
			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				int id = Int32.Parse (element.GetAttribute ("ID"));
				ResourceItem item = new ResourceItem ();
				item.AssetBundlePath = element.GetAttribute ("AssetBundlePath");
				item.Name = element.GetAttribute ("Name");
				_ResourceItems.Add (id, item);
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载属性数据
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadProperty(XmlNode node)
		{
			if (node == null) {
				return;
			}

			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				int id = Int32.Parse (element.GetAttribute ("ID"));
				PropertyItem item = new PropertyItem ();
				int value = 0;
				if (Int32.TryParse (element.GetAttribute ("HP"), out value)) {
					item.Values.Add (PropertyType.HitPoints, value);
					item.Values.Add (PropertyType.CurrentHitPoints, value);
				}
				if (Int32.TryParse (element.GetAttribute ("Attack"), out value)) {
					item.Values.Add (PropertyType.AttactDamage, value);
				}
				_Properties.Add (id, item);
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载空间数据
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadTransform(XmlNode node)
		{
			if (node == null) {
				return;
			}

			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				int id = Int32.Parse (element.GetAttribute ("ID"));
				TransformItem item = new TransformItem ();
				item.Position = StringHelp.ConvertToVector3 (element.GetAttribute ("Position"));
				item.Rotation = StringHelp.ConvertToVector3 (element.GetAttribute ("Rotation"));
				item.Scale = StringHelp.ConvertToVector3 (element.GetAttribute ("Scale"));
				item.Center = StringHelp.ConvertToVector3 (element.GetAttribute ("Center"));
				item.Volume = StringHelp.ConvertToVector3 (element.GetAttribute ("Volume"));
				_TransformItems.Add (id, item);
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载技能
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadSkill(XmlNode node)
		{
			if (node == null) {
				return;
			}

			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				int id = Int32.Parse (element.GetAttribute ("ID"));
				SkillItem item = new SkillItem ();
				item.CoolDown = float.Parse (element.GetAttribute ("CoolDown"));
				item.CostMana = Int32.Parse (element.GetAttribute ("CostMana"));
				item.Radius = float.Parse (element.GetAttribute ("Radius"));
				item.TargetType = (TargetType)Int32.Parse (element.GetAttribute ("TargetType"));
				_SkillItems.Add (id, item);
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载单位数据
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadUnit(XmlNode node)
		{
			if (node == null) {
				return;
			}
			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				XmlElement element = (XmlElement)itemNode;
				int id = Int32.Parse (element.GetAttribute ("ID"));
				UnitItem item = new UnitItem ();
				item.Name = element.GetAttribute ("Name");
				item.ResourceID = Int32.Parse (element.GetAttribute ("ResourceID"));
				item.PropertyID = Int32.Parse (element.GetAttribute ("PropertyID"));
				item.TransformID = Int32.Parse (element.GetAttribute ("TransformID"));
				item.SkillID = Int32.Parse (element.GetAttribute ("SkillID"));
				_UnitItems.Add (id, item);
				itemNode = itemNode.NextSibling;
			}
		}

		/// <summary>
		/// 加载队伍数据
		/// </summary>
		/// <param name="node">Node.</param>
		private void LoadTeam(XmlNode node)
		{
			if (node == null) {
				return;
			}

			TeamItem item = new TeamItem ();
			XmlElement element = (XmlElement)node;
			int id = Int32.Parse (element.GetAttribute ("ID"));
			XmlNode itemNode = node.FirstChild;
			while (itemNode != null) {
				element = (XmlElement)itemNode;
				int unitID = Int32.Parse (element.GetAttribute ("ID"));
				item.UnitIDs.Add (unitID);
				itemNode = itemNode.NextSibling;
			}
			_TeamItems.Add (id, item);
		}


		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="filename">Filename.</param>
		public bool Load(string filename)
		{
			XmlNode node = XmlHelp.LoadXMlRoot (filename);
			if (node == null) {
				return false;
			}

			this.Clear ();

			while (node != null) {
				if (node.Name == "Map") {
					LoadMap (node);
				} else if (node.Name == "Resource") {
					LoadResource (node);
				} else if (node.Name == "Property") {
					LoadProperty (node);
				} else if (node.Name == "Transform") {
					LoadTransform (node);
				} else if (node.Name == "Skill") {
					LoadSkill (node);
				} else if (node.Name == "Unit") {
					LoadUnit (node);
				} else if (node.Name == "Team") {
					LoadTeam (node);
				}
				
				node = node.NextSibling;
			}

			return true;
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear ()
		{
			_ResourceItems.Clear ();
			_Properties.Clear ();
			_TransformItems.Clear ();
			_SkillItems.Clear ();
			_UnitItems.Clear ();
			_TeamItems.Clear ();
		}
	}
}

