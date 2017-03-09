using System;
using Game;
using Game.Helper;
using Controller.Container;
using Model.Item;
using Model.Task;

namespace Controller.Role
{
	public class MainPlayer : Player
	{
		/// <summary>
		/// 是否初始化
		/// </summary>
		private bool _bInit;
		/// <summary>
		/// 任务
		/// </summary>
		private UserTask _Task;
		/// <summary>
		/// 背包
		/// </summary>
		private UserBag _Bag;
		/// <summary>
		/// 任务
		/// </summary>
		public UserTask Task {
			get { 
				return _Task;
			}
		}
		/// <summary>
		/// 背包
		/// </summary>
		public UserBag Bag {
			get { 
				return _Bag;
			}
		}

		public MainPlayer ()
		{
			_Task = new UserTask ();
			_Bag = new UserBag ();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Init()
		{
			if (_bInit) {
				return;
			}

			InitResource ();

			InitTask ();
				
			_bInit = true;
		}

		/// <summary>
		/// 初始化资源
		/// </summary>
		private void InitResource()
		{
			Resource.Food = UserDefault.GetInstance ().GetInteger ("Food");
			Resource.Wood = UserDefault.GetInstance ().GetInteger ("Wood");
			Resource.Iron = UserDefault.GetInstance ().GetInteger ("Iron");

			Resource.AddChangedNotify (ResType.Food, () => {
				UserDefault.GetInstance ().Set ("Food", Resource.Food.ToString());
			});

			Resource.AddChangedNotify (ResType.Wood, () => {
				UserDefault.GetInstance ().Set ("Wood",Resource.Wood.ToString());
			});

			Resource.AddChangedNotify (ResType.Iron, () => {
				UserDefault.GetInstance ().Set ("Food", Resource.Iron.ToString());
			});
		}

		/// <summary>
		/// 初始化任务
		/// </summary>
		private void InitTask()
		{
			// 判断物品数量是否满足
			Task.AddCheckFinishTaskHandler(TaskConditionType.Item, (TaskCondition condition) => {
				return Bag.GetItemCount(condition.TargetID) >= condition.Number;
			});

			// 判断角色等级是否满足
			Task.AddCheckFinishTaskHandler(TaskConditionType.Level, (TaskCondition condition) => {
				return Role.Level >= condition.Number;
			});
		}
	}
}

