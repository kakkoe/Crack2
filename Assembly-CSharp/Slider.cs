using System;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
	public float val;

	public float snapVal = -1f;

	public string phrase = string.Empty;

	public Func<bool> onChange;

	public bool dragging;

	public string lowPhrase = string.Empty;

	public string highPhrase = string.Empty;

	public float timeSinceChange = 999f;

	private float alph = 1f;

	private Vector3 v3 = default(Vector3);

	private float lastDragTickPos;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetMouseButton(0) && this.dragging)
		{
			Game.gameInstance.playSound("ui_enddrag", 1f, 1f);
			this.dragging = false;
		}
		if (this.dragging)
		{
			this.val -= Game.gameInstance.mouseChange.x * 0.04f;
			if (Mathf.Abs(this.val - this.lastDragTickPos) > 0.1f)
			{
				Game.gameInstance.playSound("ui_dragtick", 1f, 1f + (this.val - 0.5f));
				this.lastDragTickPos = this.val;
			}
		}
		if (this.val < 0f)
		{
			this.val = 0f;
		}
		if (this.val > 1f)
		{
			this.val = 1f;
		}
		if (this.phrase == string.Empty)
		{
			this.phrase = ((Component)base.transform.Find("txt")).GetComponent<Text>().text;
			this.lowPhrase = ((Component)base.transform.Find("txtLow")).GetComponent<Text>().text;
			this.highPhrase = ((Component)base.transform.Find("txtHigh")).GetComponent<Text>().text;
		}
		((Component)base.transform.Find("txt")).GetComponent<Text>().text = Localization.getPhrase(this.phrase, string.Empty);
		((Component)base.transform.Find("txtLow")).GetComponent<Text>().text = Localization.getPhrase(this.lowPhrase, string.Empty);
		((Component)base.transform.Find("txtHigh")).GetComponent<Text>().text = Localization.getPhrase(this.highPhrase, string.Empty);
		if (this.snapVal >= 0f)
		{
			this.v3.x = (-1f + this.snapVal) * 207f;
		}
		else
		{
			this.v3.x = (-1f + this.val) * 207f;
		}
		this.v3.y = 0f;
		this.v3.z = 0f;
		base.transform.Find("mask").Find("fill").localPosition = this.v3;
		if (this.dragging)
		{
			if (this.onChange != null)
			{
				this.onChange();
			}
			this.alph = 0.7f + Mathf.Cos(Time.time * 10f) * 0.2f;
			this.timeSinceChange = 0f;
		}
		else
		{
			this.alph = 1f;
		}
		this.timeSinceChange += Time.deltaTime;
		this.alph *= ((Component)base.transform.Find("txt")).GetComponent<CanvasRenderer>().GetAlpha();
		((Component)base.transform.Find("mask").Find("fill")).GetComponent<CanvasRenderer>().SetAlpha(this.alph);
	}

	public void startDrag()
	{
		Game.gameInstance.playSound("ui_begindrag", 1f, 1f);
		this.dragging = true;
		this.lastDragTickPos = this.val;
		Game.gameInstance.draggingUIthing = true;
	}
}
