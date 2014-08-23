Shader "Custom/TerrainShader" {
    Properties {
       _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
       Pass {
           Lighting Off
           ColorMaterial AmbientAndDiffuse

       }
    }
	FallBack "Diffuse"
}
