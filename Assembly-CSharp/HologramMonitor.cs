using System.Collections.Generic;
using UnityEngine;

public class HologramMonitor : MonoBehaviour
{
	public int curFrame;

	public const float numFrames = 5f;

	public float frameChangeDelay;

	public List<GameObject> frames;

	private void Start()
	{
		this.frames = new List<GameObject>();
		for (int i = 0; (float)i < 5f; i++)
		{
			this.frames.Add(base.transform.Find("HologramMonitor" + i).gameObject);
		}
		base.GetComponentInChildren<Animator>().speed = 0.9f + Random.value * 0.4f;
	}

	private void Update()
	{
		this.frameChangeDelay -= Time.deltaTime;
		if (this.frameChangeDelay <= 0f)
		{
			this.curFrame = Mathf.FloorToInt(Random.value * 5f);
			this.frameChangeDelay += 0.25f + Random.value * 8f;
			for (int i = 0; (float)i < 5f; i++)
			{
				this.frames[i].SetActive(i == this.curFrame);
			}
		}
	}
}
