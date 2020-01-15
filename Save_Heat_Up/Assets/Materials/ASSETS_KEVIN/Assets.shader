// Upgrade NOTE: upgraded instancing buffer 'Assets' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Assets"
{
	Properties
	{
		_uvtout4("uv tout 4", 2D) = "white" {}
		_emissive_assets2("emissive_assets2", 2D) = "white" {}
		[HDR]_RED("RED", Color) = (2.828427,0.06655122,0,0)
		[HDR]_GREEN("GREEN", Color) = (2.828427,0.06655122,0,0)
		_EggColor("Egg Color", Color) = (0,0,0,0)
		_Chauffe("_Chauffe", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _EggColor;
		uniform sampler2D _emissive_assets2;
		uniform sampler2D _uvtout4;
		uniform float4 _RED;
		uniform float4 _GREEN;

		UNITY_INSTANCING_BUFFER_START(Assets)
			UNITY_DEFINE_INSTANCED_PROP(float4, _emissive_assets2_ST)
#define _emissive_assets2_ST_arr Assets
			UNITY_DEFINE_INSTANCED_PROP(float4, _uvtout4_ST)
#define _uvtout4_ST_arr Assets
			UNITY_DEFINE_INSTANCED_PROP(float, _Chauffe)
#define _Chauffe_arr Assets
		UNITY_INSTANCING_BUFFER_END(Assets)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _emissive_assets2_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_emissive_assets2_ST_arr, _emissive_assets2_ST);
			float2 uv_emissive_assets2 = i.uv_texcoord * _emissive_assets2_ST_Instance.xy + _emissive_assets2_ST_Instance.zw;
			float4 tex2DNode5 = tex2D( _emissive_assets2, uv_emissive_assets2 );
			float4 _uvtout4_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_uvtout4_ST_arr, _uvtout4_ST);
			float2 uv_uvtout4 = i.uv_texcoord * _uvtout4_ST_Instance.xy + _uvtout4_ST_Instance.zw;
			float4 blendOpSrc20 = ( _EggColor * pow( tex2DNode5.b , 0.7 ) );
			float4 blendOpDest20 = tex2D( _uvtout4, uv_uvtout4 );
			float4 lerpBlendMode20 = lerp(blendOpDest20,( blendOpSrc20 * blendOpDest20 ),( 1.0 - step( tex2DNode5.b , 0.0 ) ));
			o.Albedo = ( saturate( lerpBlendMode20 )).rgb;
			float _Chauffe_Instance = UNITY_ACCESS_INSTANCED_PROP(_Chauffe_arr, _Chauffe);
			o.Emission = ( ( ( _RED * tex2DNode5.r ) * _Chauffe_Instance ) + ( tex2DNode5.g * _GREEN ) ).rgb;
			o.Smoothness = 0.2;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
204;7;1389;922;1542.365;188.518;1.3;True;True
Node;AmplifyShaderEditor.SamplerNode;5;-1200,208;Inherit;True;Property;_emissive_assets2;emissive_assets2;1;0;Create;True;0;0;False;0;7658522576e564747a009b6fcf53c86a;7658522576e564747a009b6fcf53c86a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-1184,16;Inherit;False;Property;_RED;RED;2;1;[HDR];Create;True;0;0;False;0;2.828427,0.06655122,0,0;23.96863,0.627451,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;21;-782.8572,-116.5629;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.7;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;-1066.167,-208.4747;Inherit;False;Property;_EggColor;Egg Color;4;0;Create;True;0;0;False;0;0,0,0,0;0.4379669,0.6981132,0.6787298,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-827.8207,347.8131;Float;False;InstancedProperty;_Chauffe;_Chauffe;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;22;-399.9873,-90.96875;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-800,64;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;-1158.979,795.3132;Inherit;False;Property;_GREEN;GREEN;3;1;[HDR];Create;True;0;0;False;0;2.828427,0.06655122,0,0;2.996078,0.8627451,0.04705882,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-608.1992,-225.944;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;23;-244.3533,-212.7524;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-916.7,-457.4;Inherit;True;Property;_uvtout4;uv tout 4;0;0;Create;True;0;0;False;0;None;77acae13fe6c8fb46abf100a1a29abc2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-845.4683,748.7551;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-548.8398,140.0495;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;20;-68.35913,-409.9662;Inherit;True;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SinTimeNode;6;-1184,496;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;2;-25.4644,234.4762;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-330.6642,547.6281;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;414.3807,92.69043;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Assets;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;5;3
WireConnection;22;0;5;3
WireConnection;8;0;11;0
WireConnection;8;1;5;1
WireConnection;17;0;18;0
WireConnection;17;1;21;0
WireConnection;23;0;22;0
WireConnection;14;0;5;2
WireConnection;14;1;13;0
WireConnection;7;0;8;0
WireConnection;7;1;24;0
WireConnection;20;0;17;0
WireConnection;20;1;1;0
WireConnection;20;2;23;0
WireConnection;16;0;7;0
WireConnection;16;1;14;0
WireConnection;0;0;20;0
WireConnection;0;2;16;0
WireConnection;0;4;2;0
ASEEND*/
//CHKSM=C8BF39808D93345CC8999FD0D1144A971226BB36