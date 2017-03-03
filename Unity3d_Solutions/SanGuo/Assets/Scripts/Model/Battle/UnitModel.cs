using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Base;
using Model.Skill;

namespace Model.Battle
{
	/// <summary>
	/// 单位说明
	/// </summary>
	public class UnitModel
	{
		/// <summary>
		/// 编号
		/// </summary>
		public int ID;
		/// <summary>
		/// 名称
		/// </summary>
		public string Name;
		/// <summary>
		/// 资源包路径
		/// </summary>
		public string AssetBundlePath;
		/// <summary>
		/// 文件名称
		/// </summary>
		public string FileName;
		/// <summary>
		/// 位置
		/// </summary>
		public Vector3 Position;
		/// <summary>
		/// 旋转方向
		/// </summary>
		public Vector3 Rotation;
		/// <summary>
		/// 缩放比例
		/// </summary>
		public Vector3 Scale;
		/// <summary>
		/// 单位体积
		/// </summary>
		public Vector3 Volume;
		/// <summary>
		/// 属性
		/// </summary>
		public Dictionary<PropertyType, float> Attributes;
		/// <summary>
		/// 技能
		/// </summary>
		public List<SkillModel> Skills;

		public UnitModel()
		{
			Attributes = new Dictionary<PropertyType, float> ();
			Skills = new List<SkillModel> ();
		}
	}
}

