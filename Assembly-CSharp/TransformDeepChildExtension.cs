using System;
using System.Collections;
using UnityEngine;

public static class TransformDeepChildExtension
{
	public static Transform FindDeepChild(this Transform aParent, string aName)
	{
		Transform transform = aParent.Find(aName);
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			return transform;
		}
		IEnumerator enumerator = aParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform aParent2 = (Transform)enumerator.Current;
				transform = aParent2.FindDeepChild(aName);
				if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
				{
					return transform;
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
}
