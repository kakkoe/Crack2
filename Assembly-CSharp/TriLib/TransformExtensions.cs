using System;
using System.Collections;
using UnityEngine;

namespace TriLib
{
	public static class TransformExtensions
	{
		public static void LoadMatrix(this Transform transform, Matrix4x4 matrix, bool local = true)
		{
			if (local)
			{
				transform.localScale = matrix.ExtractScale();
				transform.localRotation = matrix.ExtractRotation();
				transform.localPosition = matrix.ExtractPosition();
			}
			else
			{
				transform.rotation = matrix.ExtractRotation();
				transform.position = matrix.ExtractPosition();
			}
		}

		public static Bounds EncapsulateBounds(this Transform transform)
		{
			Bounds result = default(Bounds);
			Renderer[] componentsInChildren = ((Component)transform).GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				Renderer[] array = componentsInChildren;
				foreach (Renderer renderer in array)
				{
					result.Encapsulate(renderer.bounds);
				}
			}
			return result;
		}

		public static Transform FindDeepChild(this Transform transform, string name, bool endsWith = false)
		{
			if ((!endsWith) ? transform.name.EndsWith(name) : (transform.name == name))
			{
				return transform;
			}
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform2 = (Transform)enumerator.Current;
					Transform transform3 = transform2.FindDeepChild(name, false);
					if ((UnityEngine.Object)transform3 != (UnityEngine.Object)null)
					{
						return transform3;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return null;
		}

		public static void DestroyChildren(this Transform transform, bool destroyImmediate = false)
		{
			for (int num = transform.childCount - 1; num >= 0; num--)
			{
				Transform child = transform.GetChild(num);
				if (destroyImmediate)
				{
					UnityEngine.Object.DestroyImmediate(child.gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
		}
	}
}
