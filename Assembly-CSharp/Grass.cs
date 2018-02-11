using UnityEngine;

public class Grass : MonoBehaviour
{
	public Material[] mats;

	private Renderer render;

	private int matIndex;

	private void Start()
	{
		this.render = base.gameObject.GetComponent<Renderer>();
	}

	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.matIndex = 1 - this.matIndex;
			this.render.material = this.mats[this.matIndex];
		}
	}
}
