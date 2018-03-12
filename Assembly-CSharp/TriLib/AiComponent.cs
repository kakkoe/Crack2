using System;

namespace TriLib
{
	[Flags]
	public enum AiComponent
	{
		Normals = 2,
		TangentsAndBitangents = 4,
		Colors = 8,
		TexCoords = 0x10,
		BoneWeights = 0x20,
		Animations = 0x40,
		Textures = 0x80,
		Lights = 0x100,
		Cameras = 0x200,
		Meshes = 0x400,
		Materials = 0x800
	}
}
