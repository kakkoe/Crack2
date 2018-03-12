using System;
using UnityEngine;

namespace TriLib
{
	public class MaterialData : IDisposable
	{
		public Material UnityMaterial;

		public void Dispose()
		{
			this.UnityMaterial = null;
		}
	}
}
