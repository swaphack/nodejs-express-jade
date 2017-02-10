using UnityEngine;
using System.Collections;
using Game;

namespace Controller.Role
{
	/// <summary>
	/// 玩家
	/// </summary>
	public class Player
	{
		/// <summary>
		/// 资源
		/// </summary>
		private Resource _Resource;

		/// <summary>
		/// 角色信息
		/// </summary>
		private RoleInfo _Role;

		/// <summary>
		/// 资源
		/// </summary>
		public Resource Resource {
			get { 
				return _Resource;
			}
		}

		/// <summary>
		/// 角色信息
		/// </summary>
		/// <value>The role.</value>
		public RoleInfo Role {
			get { 
				return _Role;
			}
		}

		/// <summary>
		/// 当前玩家
		/// </summary>
		private static MainPlayer mainPlayer;

		public static MainPlayer MainPlayer
		{
			get { 
				if (mainPlayer == null) {
					mainPlayer = new MainPlayer ();
				}

				return mainPlayer;
			}
		}

		public Player()
		{
			_Resource = new Resource ();	
			_Role = new RoleInfo ();
		}
	}
}
