using System;
using UnityEngine;

namespace Game.Shapes
{
	/// <summary>
	/// 形状组件
	/// </summary>
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	public class Form : MonoBehaviour
	{
		/// <summary>
		/// 边线材质
		/// </summary>
		private Material _EdgeMaterial;
		/// <summary>
		/// 网格
		/// </summary>
		private Mesh _Mesh;

		/// <summary>
		/// 显示边界
		/// </summary>
		/// <value><c>true</c> if show line; otherwise, <c>false</c>.</value>
		public bool EdgeVisible = true;
		/// <summary>
		/// 边界颜色
		/// </summary>
		/// <value>The color of the edge.</value>
		public Color EdgeColor = Color.red;
		/// <summary>
		/// 内部填充
		/// </summary>
		/// <value><c>true</c> if this instance is fill; otherwise, <c>false</c>.</value>
		public bool IsFill = true;
		/// <summary>
		/// 填充颜色
		/// </summary>
		/// <value>The color of the fill.</value>
		public Color FillColor = Color.green;
		/// <summary>
		/// 顶点
		/// </summary>
		public Vector3[] Points;

		public Form ()
		{
			
		}

		void Start()
		{
			_Mesh = new Mesh ();

			InitEdgeMaterial ();
			InitMeshCollider ();
		}

		void Update() 
		{
			if (IsFill) { // 内部是否可见
				DrawMesh ();
			}
		}


		void OnPostRender() 
		{
			if (EdgeVisible) { // 边界是否可见
				DrawEdgeMaterial ();
			}
		}

		/// <summary>
		/// 初始化网格
		/// </summary>
		private void DrawMesh()
		{
			if (_Mesh == null) {
				return;
			}

			if (Points == null || Points.Length < 3) {
				return;
			}

			// 顶点坐标
			int pointCount = Points.Length;
			Vector3[] points = new Vector3[pointCount];
			Array.Copy (Points, points, pointCount);

			// 顶点颜色
			Color[] colors = new Color[pointCount];
			for (int i = 0; i < pointCount; i++) {
				colors [i] = FillColor;
			}

			// 三角形
			int triangleCount = pointCount - 2;
			int[] triangles = new int[3 * triangleCount];
			for (int i = 0; i < triangleCount; i++) {
				triangles [3 * i] = 0;
				triangles [3 * i + 1] = i + 1;
				triangles [3 * i + 2] = i + 2;
			}


			_Mesh.name = "Form";
			_Mesh.vertices = points;
			_Mesh.colors = colors;
			_Mesh.triangles = triangles; 
			GetComponent<MeshFilter> ().mesh = _Mesh;
		}

		/// <summary>
		/// 初始化网格碰撞机
		/// </summary>
		private void InitMeshCollider() 
		{
		}

		/// <summary>
		/// 初始化边缘材质
		/// </summary>
		private void InitEdgeMaterial()
		{
			if (_EdgeMaterial != null) {
				return;
			}

			Shader shader = Shader.Find ("Hidden/Internal-Colored");
			_EdgeMaterial = new Material (shader);
			_EdgeMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			_EdgeMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			_EdgeMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			_EdgeMaterial.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			_EdgeMaterial.SetInt ("_ZWrite", 0);
		}

		/// <summary>
		/// 绘制边线
		/// </summary>
		private void DrawEdgeMaterial()
		{
			if (_EdgeMaterial == null) {
				return;
			}
			// Apply the line material
			_EdgeMaterial.SetPass (0);

			GL.PushMatrix ();
			// Set transformation matrix for drawing to
			// match our transform
			GL.MultMatrix (transform.localToWorldMatrix);

			// Vertex colors change from red to green
			GL.Color (EdgeColor);

			int lineCount = Points.Length;
			// Draw lines
			GL.Begin (GL.LINES);
			for (int i = 0; i < lineCount; i++)
			{
				GL.Vertex (Points [0]);
				GL.Vertex (Points [i]);
			}
			GL.End ();
			GL.PopMatrix ();
		}
	}
}

