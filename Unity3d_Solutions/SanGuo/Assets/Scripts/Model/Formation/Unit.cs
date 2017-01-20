using System;
using UnityEngine;
using Game;
using Model.Paper;

namespace Model.Formation
{
	/// <summary>
	/// 作战单位
	/// </summary>
	public class Unit
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
		public void SetObject(GameObject gameObj)
		{
			if (gameObj == null) {
				return;
			}

			_TranformObject.SetTranform (gameObj.transform);
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

