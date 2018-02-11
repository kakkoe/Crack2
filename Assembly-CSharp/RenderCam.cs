using System.Collections.Generic;
using UnityEngine;

public class RenderCam : MonoBehaviour
{
	private List<int> originalLayers;

	public Texture2D snapshot;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnPreCull()
	{
		if ((Object)this.snapshot == (Object)null)
		{
			this.snapshot = new Texture2D(base.GetComponent<Camera>().targetTexture.width, base.GetComponent<Camera>().targetTexture.height);
		}
		this.originalLayers = new List<int>();
		for (int i = 0; i < Game.gameInstance.headshotSubject.GO.GetComponentsInChildren<SkinnedMeshRenderer>().Length; i++)
		{
			this.originalLayers.Add(Game.gameInstance.headshotSubject.GO.GetComponentsInChildren<SkinnedMeshRenderer>()[i].gameObject.layer);
			Game.gameInstance.headshotSubject.GO.GetComponentsInChildren<SkinnedMeshRenderer>()[i].gameObject.layer = 16;
		}
	}

	public void OnPostRender()
	{
		for (int i = 0; i < Game.gameInstance.headshotSubject.GO.GetComponentsInChildren<SkinnedMeshRenderer>().Length; i++)
		{
			Game.gameInstance.headshotSubject.GO.GetComponentsInChildren<SkinnedMeshRenderer>()[i].gameObject.layer = this.originalLayers[i];
		}
		this.snapshot.ReadPixels(new Rect(0f, 0f, (float)this.snapshot.width, (float)this.snapshot.height), 0, 0);
	}
}
