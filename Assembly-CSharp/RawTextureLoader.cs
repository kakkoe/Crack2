using UnityEngine;

public class RawTextureLoader : MonoBehaviour
{
	private Material mat;

	public string tex = string.Empty;

	private string lastTex = string.Empty;

	private Texture textu;

	public bool albedo = true;

	public bool emission;

	private void Start()
	{
		this.mat = Object.Instantiate(base.GetComponent<Renderer>().material);
		base.GetComponent<Renderer>().material = this.mat;
	}

	private void Update()
	{
		if (this.tex != this.lastTex)
		{
			if (this.tex != string.Empty)
			{
				this.textu = (Resources.Load(this.tex.Replace('/', Game.PathDirectorySeparatorChar)) as Texture);
				if (this.albedo)
				{
					this.mat.SetTexture("_MainTex", this.textu);
				}
				if (this.emission)
				{
					this.mat.SetTexture("_EmissionMap", this.textu);
				}
				base.GetComponent<Renderer>().material = this.mat;
			}
			this.lastTex = this.tex;
		}
	}
}
