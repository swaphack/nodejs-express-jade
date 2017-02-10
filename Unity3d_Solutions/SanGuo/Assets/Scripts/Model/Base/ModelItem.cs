using System;

namespace Model.Base
{
	/// <summary>
	/// 模型项
	/// </summary>
	public class ModelItem : IModelItem
	{
		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		private int _ID;
		/// <summary>
		/// 名称
		/// </summary>
		/// <value>The name.</value>
		private int _Name;
		/// <summary>
		/// 说明
		/// </summary>
		/// <value>The describe.</value>
		private int _Describe;

		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		public int ID { 
			get { 
				return _ID; 
			} 
			set { 
				_ID = value;
			}
		}
		/// <summary>
		/// 名称
		/// </summary>
		/// <value>The name.</value>
		public int Name { 
			get { 
				return _Name; 
			} 
			set { 
				_Name = value;
			}
		}
		/// <summary>
		/// 说明
		/// </summary>
		/// <value>The describe.</value>
		public int Describe { 
			get { 
				return _Describe; 
			} 
			set { 
				_Describe = value;
			}
		}

		public ModelItem()
		{
		}
	}
}

