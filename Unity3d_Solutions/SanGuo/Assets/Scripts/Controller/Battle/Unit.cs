using System;
using UnityEngine;
using Model.Battle;

namespace Controller.Battle
{
	/// <summary>
	/// 单位死亡处理
	/// </summary>
	public delegate void OnUnitDeadCallback(Unit unit);
	/// <summary>
	/// 作战单位
	/// </summary>
	public class Unit : Identifier
	{
		/// <summary>
		/// 单位信息
		/// </summary>
		private UnitModel _UnitModel;
		/// <summary>
		/// 单位属性
		/// </summary>
		private UnitProperty _UnitProperty;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		private TranformObject _TranformObject;
		/// <summary>
		/// 单位死亡处理
		/// </summary>
		private OnUnitDeadCallback _OnDeadHandler;
		/// <summary>
		/// 空间变换对象
		/// </summary>
		public TranformObject TranformObject {
			get { 
				return _TranformObject;
			}
		}
		/// <summary>
		/// 对象
		/// </summary>
		public GameObject GameObject {
			get { 
				if (_TranformObject.Transform == null) {
					return null;
				}
				return _TranformObject.Transform.gameObject;
			}
		}

		public Unit()
		{
			_TranformObject = new TranformObject ();
			_UnitProperty = new UnitProperty ();
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
			if (paper == null) {
				return;
			}

			_UnitModel = paper;
		}

		/// <summary>
		/// 设置单位死亡处理
		/// </summary>
		/// <param name="handler">Handler.</param>
		public void SetDeadHandler(OnUnitDeadCallback handler)
		{
			_OnDeadHandler = handler;
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

