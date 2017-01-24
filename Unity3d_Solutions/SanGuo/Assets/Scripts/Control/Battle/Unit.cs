using System;
using UnityEngine;
using Model.Battle;

namespace Control.Battle
{
	/// <summary>
	/// 作战单位
	/// </summary>
	public class Unit : Identifier
	{
		/// <summary>
		/// 对象
		/// </summary>
		private GameObject _GameObject;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		private TranformObject _TranformObject;
		/// <summary>
		/// 布阵信息
		/// </summary>
		private UnitModel _UnitModel;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		public TranformObject TranformObject {
			get { 
				return _TranformObject;
			}
		}

		public Unit()
		{
			_TranformObject = new TranformObject ();
		}

		/// <summary>
		/// 设置对象
		/// </summary>
		/// <param name="gameObj">Game object.</param>
		/// <param name="paper">UnitModel.</param>
		public void SetObject(GameObject gameObj)
		{
			if (gameObj == null) {
				return;
			}

			_TranformObject.SetTranform (gameObj.transform);
		}

		/// <summary>
		/// 设置模型数据
		/// </summary>
		/// <param name="paper">Paper.</param>
		public void SetModel(UnitModel paper)
		{
			_UnitModel = paper;
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="name">Name.</param>
		public void RunAction(string name)
		{
			//_TranformObject.Transform.GetComponent<> ();
		}

		/// <summary>
		/// 定时更新
		/// </summary>
		/// <param name="delta">Delta.</param>
		public void Update(float dt)
		{
			
		}
	}
}

