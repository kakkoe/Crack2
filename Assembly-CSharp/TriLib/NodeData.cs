using System;
using UnityEngine;

namespace TriLib
{
	public class NodeData : IDisposable
	{
		public GameObject GameObject;

		public string Name;

		public IntPtr Node;

		public NodeData ParentNodeData;

		public string Path;

		public int Id;

		public void Dispose()
		{
			this.ParentNodeData = null;
			this.GameObject = null;
			this.Name = null;
			this.Path = null;
		}
	}
}
