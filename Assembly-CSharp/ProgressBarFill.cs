using UnityEngine;
using UnityEngine.UI;

public class ProgressBarFill : MonoBehaviour
{
	public float val;

	private GameObject msk;

	private float origWidth;

	private bool hasSpark;

	public Transform spark;

	private float sparkMax = 0.99f;

	private Vector3 v3;

	private void Start()
	{
		this.msk = new GameObject();
		this.msk.transform.SetParent(base.transform);
		this.msk.transform.localScale = Vector3.one;
		this.msk.transform.localPosition = Vector3.zero;
		this.msk.transform.localEulerAngles = Vector3.zero;
		this.msk.transform.SetParent(base.transform.parent);
		base.transform.SetParent(this.msk.transform);
		this.msk.AddComponent<Image>();
		this.msk.AddComponent<Mask>().showMaskGraphic = false;
		this.msk.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
		this.origWidth = base.GetComponent<Image>().rectTransform.rect.width;
		if ((Object)base.transform.Find("spark") != (Object)null)
		{
			this.hasSpark = true;
			this.spark = base.transform.Find("spark");
			this.spark.SetParent(this.msk.transform.parent);
		}
	}

	private void Update()
	{
		this.msk.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.origWidth * this.val);
		if (this.hasSpark)
		{
			this.v3 = this.msk.transform.localPosition;
			this.v3.x += this.origWidth * Game.cap(this.val, 0f, this.sparkMax);
			this.spark.localPosition = this.v3;
			this.v3 = Vector3.one * Game.cap(this.val * 20f, 0f, 1f);
			this.spark.localScale = this.v3;
		}
	}
}
