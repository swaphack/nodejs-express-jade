Shader "Custom/Test/SampleVertex" {
	Properties {
		_Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
		_Specular ("Specular", Color) = (1, 1, 1, 1)
		_Gloss ("Gloss", Range(8.0, 256)) = 20
	}
	SubShader {
		UsePass "Custom/Light/DiffuseVertexLevel/DIFFUSEVERTEX"
		UsePass "Custom/Light/SpecularVertexLevel/SPECULARVERTEX"
	}

	// 如果上面都不执行，调用这个
	FallBack "Diffuse"
}

