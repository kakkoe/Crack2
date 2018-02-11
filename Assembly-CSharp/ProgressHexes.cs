using UnityEngine;

public class ProgressHexes : MonoBehaviour
{
	public float overallAlpha = 1f;

	public float progress;

	public int numImages = 12;

	public CanvasRenderer[] images;

	private float a;

	private void Start()
	{
		this.images = new CanvasRenderer[this.numImages];
		this.images[0] = ((Component)base.transform.Find("wire0")).GetComponent<CanvasRenderer>();
		this.images[1] = ((Component)base.transform.Find("wire1")).GetComponent<CanvasRenderer>();
		this.images[2] = ((Component)base.transform.Find("wire2")).GetComponent<CanvasRenderer>();
		this.images[3] = ((Component)base.transform.Find("wire3")).GetComponent<CanvasRenderer>();
		this.images[4] = ((Component)base.transform.Find("wire4")).GetComponent<CanvasRenderer>();
		this.images[5] = ((Component)base.transform.Find("wire5")).GetComponent<CanvasRenderer>();
		this.images[6] = ((Component)base.transform.Find("fill0")).GetComponent<CanvasRenderer>();
		this.images[7] = ((Component)base.transform.Find("fill1")).GetComponent<CanvasRenderer>();
		this.images[8] = ((Component)base.transform.Find("fill2")).GetComponent<CanvasRenderer>();
		this.images[9] = ((Component)base.transform.Find("fill3")).GetComponent<CanvasRenderer>();
		this.images[10] = ((Component)base.transform.Find("fill4")).GetComponent<CanvasRenderer>();
		this.images[11] = ((Component)base.transform.Find("fill5")).GetComponent<CanvasRenderer>();
	}

	private void Update()
	{
		for (int i = 0; i < this.images.Length; i++)
		{
			this.a = this.progress * (float)this.numImages - (float)i;
			if (this.a < 0f)
			{
				this.a = 0f;
			}
			if (i < 6)
			{
				this.a += 0.1f;
			}
			if (this.a > 1f)
			{
				this.a = 1f;
			}
			this.images[i].SetAlpha(this.a * this.overallAlpha);
		}
	}
}
