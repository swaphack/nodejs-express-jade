using System;
using UnityEngine;

namespace Game.Controller
{
	/// <summary>
	/// 形状组件
	/// </summary>
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class ShapController : MonoBehaviour
	{
		/// <summary>
		/// 填充颜色
		/// </summary>
		/// <value>The color of the fill.</value>
		public Color FillColor = Color.green;
		/// <summary>
		/// 顶点
		/// </summary>
		public Vector3[] Vertices;
		/// <summary>
		/// 顶点索引
		/// </summary>
		public int[] Indices;

		public ShapController ()
		{
			
		}

		void Start()
		{
			InitMesh ();
		}

		void Update() 
		{
		}

		/// <summary>
		/// 初始化网格
		/// </summary>
		private void InitMesh()
		{
			if (Vertices == null || Vertices.Length < 3) {
				return;
			}

			if (Indices == null || Indices.Length < 3) {
				return;
			}

			// 顶点坐标
			Vector3[] vertices = new Vector3[Vertices.Length];
			Array.Copy (Vertices, vertices, Vertices.Length);

			// 顶点颜色
			Color[] colors = new Color[Vertices.Length];
			for (int i = 0; i < Vertices.Length; i++) {
				colors [i] = FillColor;
			}

			// 法线
			Vector3[] normals = new Vector3[vertices.Length];
			for (int i = 0; i < Vertices.Length; i++) {
				normals [i] = Vector3.up;
			}

			// 纹理uv
			Vector2[] uvs = new Vector2[vertices.Length];
			for (int i = 0; i < Vertices.Length; i++) {
				uvs [i] = Vector2.one;
			}

			// 顶点索引
			int[] indices = new int[Indices.Length];
			Array.Copy (Indices, indices, Indices.Length);


			Mesh mesh = new Mesh ();
			//mesh.name = "shape";
			mesh.vertices = vertices;
			mesh.normals = normals;
			//mesh.uv = uvs;
			mesh.colors = colors;
			mesh.triangles = indices;
			this.gameObject.GetComponent<MeshFilter> ().mesh = mesh;
		}

		/// <summary>
		/// 初始化材质
		/// </summary>
		private void InitMaterial()
		{
			//Material material = new Material ();
		}

		/// <summary>
		/// 初始化渲染脚本
		/// </summary>
		private void InitShader()
		{
			//Shader shader = new Shader ();

		}
	}
}

