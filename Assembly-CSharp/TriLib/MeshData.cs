using System;
using UnityEngine;

namespace TriLib
{
	public class MeshData : IDisposable
	{
		public SkinnedMeshRenderer SkinnedMeshRenderer;

		public Mesh UnityMesh;

		public void Dispose()
		{
			this.UnityMesh = null;
			this.SkinnedMeshRenderer = null;
		}
	}
}
