using System;
using UnityEngine;

public class SelfLoadingObject : MonoBehaviour
{
	public Action onLoad;

	public string oFile;

	public string tFile;

	public string fFile;

	public bool waitingOnLoad;

	public GameObject loadedGO;

	private void Start()
	{
	}

	private void Update()
	{
		if (this.waitingOnLoad && AssetLoadManager.isEverythingLoaded())
		{
			bool flag = false;
			if (AssetLoadManager.assetLoaded(this.oFile))
			{
				flag = true;
			}
			if (this.tFile != string.Empty && !AssetLoadManager.assetLoaded(this.tFile))
			{
				flag = false;
			}
			if (this.fFile != string.Empty && !AssetLoadManager.assetLoaded(this.fFile))
			{
				flag = false;
			}
			if (flag)
			{
				GameObject original = AssetLoadManager.getAsset(this.oFile).assetBundle.LoadAsset(AssetLoadManager.getAsset(this.oFile).assetBundle.GetAllAssetNames()[0]) as GameObject;
				this.loadedGO = UnityEngine.Object.Instantiate(original);
				this.loadedGO.transform.SetParent(base.transform);
				this.loadedGO.transform.localPosition = Vector3.zero;
				this.loadedGO.transform.localScale = Vector3.one;
				if (this.tFile != string.Empty)
				{
					Texture2D texture2D = new Texture2D(4, 4);
					AssetLoadManager.getAsset(this.tFile).LoadImageIntoTexture(texture2D);
					TextureScale.Bilinear(texture2D, Mathf.FloorToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.FloorToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
					this.loadedGO.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", texture2D);
				}
				if (this.fFile != string.Empty)
				{
					Texture2D texture2D2 = new Texture2D(4, 4);
					AssetLoadManager.getAsset(this.fFile).LoadImageIntoTexture(texture2D2);
					TextureScale.Bilinear(texture2D2, Mathf.FloorToInt((float)texture2D2.width * UserSettings.data.characterTextureQuality), Mathf.FloorToInt((float)texture2D2.height * UserSettings.data.characterTextureQuality));
					this.loadedGO.GetComponentInChildren<Renderer>().material.SetTexture("_Mask1", texture2D2);
				}
				AssetLoadManager.unloadAsset(this.oFile);
				if (this.tFile != string.Empty)
				{
					AssetLoadManager.unloadAsset(this.tFile);
				}
				if (this.fFile != string.Empty)
				{
					AssetLoadManager.unloadAsset(this.fFile);
				}
				this.waitingOnLoad = false;
				if (this.onLoad != null)
				{
					this.onLoad();
				}
			}
			else
			{
				AssetLoadManager.loadAsset(this.oFile);
				if (this.tFile != string.Empty)
				{
					AssetLoadManager.loadAsset(this.tFile);
				}
				if (this.fFile != string.Empty)
				{
					AssetLoadManager.loadAsset(this.fFile);
				}
			}
		}
	}

	public void load(string objectFilename, string textureFilename = "", string fxFilename = "", Action _onLoad = null)
	{
		this.oFile = objectFilename;
		this.tFile = textureFilename;
		this.fFile = fxFilename;
		AssetLoadManager.loadAsset(this.oFile);
		if (this.tFile != string.Empty)
		{
			AssetLoadManager.loadAsset(this.tFile);
		}
		if (this.fFile != string.Empty)
		{
			AssetLoadManager.loadAsset(this.fFile);
		}
		this.onLoad = _onLoad;
		this.waitingOnLoad = true;
	}
}
