using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriLib
{
	[Serializable]
	public class AssetLoaderOptions : ScriptableObject
	{
		public bool DontLoadAnimations;

		public bool DontLoadLights = true;

		public bool DontLoadCameras = true;

		public bool AutoPlayAnimations;

		public WrapMode AnimationWrapMode = WrapMode.Loop;

		public bool UseLegacyAnimations = true;

		public RuntimeAnimatorController AnimatorController;

		public Avatar Avatar;

		public bool DontLoadMaterials;

		public bool UseStandardSpecularMaterial;

		public bool GenerateMeshColliders;

		public bool ConvexMeshColliders;

		public List<Material> MaterialsOverride = new List<Material>();

		public Vector3 RotationAngles = new Vector3(0f, 180f, 0f);

		public float Scale = 1f;

		public AssimpPostProcessSteps PostProcessSteps = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.MakeLeftHanded | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ValidateDataStructure | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords | AssimpPostProcessSteps.FindInstances | AssimpPostProcessSteps.OptimizeMeshes | AssimpPostProcessSteps.FlipWindingOrder;

		public string TexturesPathOverride;

		public TextureCompression TextureCompression = TextureCompression.NormalQuality;

		public List<AssetAdvancedConfig> AdvancedConfigs = new List<AssetAdvancedConfig>
		{
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.SplitLargeMeshesVertexLimit), 65000),
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.FBXImportSearchEmbeddedTextures), true),
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.FBXImportReadLights), false),
			new AssetAdvancedConfig(AssetAdvancedPropertyMetadata.GetConfigKey(AssetAdvancedPropertyClassNames.FBXImportReadCameras), false)
		};

		public static AssetLoaderOptions CreateInstance()
		{
			return ScriptableObject.CreateInstance<AssetLoaderOptions>();
		}

		public void Deserialize(string json)
		{
			JsonUtility.FromJsonOverwrite(json, this);
		}

		public string Serialize()
		{
			return JsonUtility.ToJson(this);
		}
	}
}
