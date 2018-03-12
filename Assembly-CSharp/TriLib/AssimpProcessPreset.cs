namespace TriLib
{
	public class AssimpProcessPreset
	{
		public const AssimpPostProcessSteps ConvertToLeftHanded = AssimpPostProcessSteps.MakeLeftHanded | AssimpPostProcessSteps.FlipWindingOrder;

		public const AssimpPostProcessSteps TargetRealtimeFast = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenNormals | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.GenUvCoords;

		public const AssimpPostProcessSteps TargetRealtimeQuality = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords;

		public const AssimpPostProcessSteps TargetRealtimeMaxQuality = AssimpPostProcessSteps.CalcTangentSpace | AssimpPostProcessSteps.JoinIdenticalVertices | AssimpPostProcessSteps.Triangulate | AssimpPostProcessSteps.GenSmoothNormals | AssimpPostProcessSteps.SplitLargeMeshes | AssimpPostProcessSteps.LimitBoneWeights | AssimpPostProcessSteps.ValidateDataStructure | AssimpPostProcessSteps.ImproveCacheLocality | AssimpPostProcessSteps.RemoveRedundantMaterials | AssimpPostProcessSteps.SortByPType | AssimpPostProcessSteps.FindInvalidData | AssimpPostProcessSteps.GenUvCoords | AssimpPostProcessSteps.FindInstances | AssimpPostProcessSteps.OptimizeMeshes;
	}
}
