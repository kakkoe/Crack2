using System;
using UnityEngine;

namespace TriLib.Samples
{
	public class LoadSample : MonoBehaviour
	{
		protected void Start()
		{
			using (AssetLoader assetLoader = new AssetLoader())
			{
				try
				{
					AssetLoaderOptions assetLoaderOptions = AssetLoaderOptions.CreateInstance();
					assetLoaderOptions.RotationAngles = new Vector3(90f, 180f, 0f);
					assetLoaderOptions.AutoPlayAnimations = true;
					GameObject gameObject = assetLoader.LoadFromFile(Application.dataPath + "/TriLib/TriLib/Samples/Models/Bouncing.fbx", assetLoaderOptions, null);
					gameObject.transform.position = new Vector3(128f, 0f, 0f);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.ToString());
				}
			}
		}
	}
}
