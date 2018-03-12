using System;

namespace TriLib
{
	[Flags]
	public enum AssimpPostProcessSteps
	{
		CalcTangentSpace = 1,
		JoinIdenticalVertices = 2,
		MakeLeftHanded = 4,
		Triangulate = 8,
		RemoveComponent = 0x10,
		GenNormals = 0x20,
		GenSmoothNormals = 0x40,
		SplitLargeMeshes = 0x80,
		PreTransformVertices = 0x100,
		LimitBoneWeights = 0x200,
		ValidateDataStructure = 0x400,
		ImproveCacheLocality = 0x800,
		RemoveRedundantMaterials = 0x1000,
		FixInfacingNormals = 0x2000,
		SortByPType = 0x8000,
		FindDegenerates = 0x10000,
		FindInvalidData = 0x20000,
		GenUvCoords = 0x40000,
		TransformUvCoords = 0x80000,
		FindInstances = 0x100000,
		OptimizeMeshes = 0x200000,
		OptimizeGraph = 0x400000,
		FlipUVs = 0x800000,
		FlipWindingOrder = 0x1000000,
		SplitByBoneCount = 0x2000000,
		Debone = 0x4000000
	}
}
