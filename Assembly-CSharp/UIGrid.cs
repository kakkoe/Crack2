using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGrid : MonoBehaviour
{
	public Func<bool> onChange;

	public bool dragging;

	public float valX;

	public float valY;

	public float lastMX;

	public float lastMY;

	public string phraseX = string.Empty;

	public string phraseY = string.Empty;

	private Vector3 v3 = default(Vector3);

	private float nubSize = 1f;

	private Vector2 lastDragTickPos = default(Vector2);

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
			this.valX -= Game.gameInstance.mouseChange.x * 0.04f;
			this.valY += Game.gameInstance.mouseChange.y * 0.04f;
			if (Input.GetKey(UserSettings.data.KEY_SNAP_TO_GRID))
			{
				if (Math.Abs(this.valX - 0.5f) < 0.05f)
				{
					this.valX = 0.5f;
				}
				if (Math.Abs(this.valY - 0.5f) < 0.05f)
				{
					this.valY = 0.5f;
				}
			}
			if (Mathf.Abs(this.valX - this.lastDragTickPos.x) > 0.05f || Mathf.Abs(this.valY - this.lastDragTickPos.y) > 0.05f)
			{
				Game.gameInstance.playSound("ui_dragtick", 1f, 1f);
				this.lastDragTickPos.x = this.valX;
				this.lastDragTickPos.y = this.valY;
			}
		}
		if (this.valX < 0f)
		{
			this.valX = 0f;
		}
		if (this.valX > 1f)
		{
			this.valX = 1f;
		}
		if (this.valY < 0f)
		{
			this.valY = 0f;
		}
		if (this.valY > 1f)
		{
			this.valY = 1f;
		}
		if (this.phraseX == string.Empty)
		{
			this.phraseX = ((Component)base.transform.Find("txtX")).GetComponent<Text>().text;
			this.phraseY = ((Component)base.transform.Find("txtY")).GetComponent<Text>().text;
		}
		((Component)base.transform.Find("txtX")).GetComponent<Text>().text = Localization.getPhrase(this.phraseX, string.Empty);
		((Component)base.transform.Find("txtY")).GetComponent<Text>().text = Localization.getPhrase(this.phraseY, string.Empty);
		this.v3.x = -64f + this.valX * 128f;
		this.v3.y = -64f + this.valY * 128f;
		this.v3.z = 0f;
		base.transform.Find("nub").localPosition = this.v3;
		if (this.dragging)
		{
			if (this.onChange != null)
			{
				this.onChange();
			}
			this.nubSize += (0.4f + Mathf.Cos(Time.time * 10f) * 0.1f - this.nubSize) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
		}
		else
		{
			this.nubSize += (0.8f - this.nubSize) * Game.cap(Time.deltaTime * 3f, 0f, 1f);
		}
		base.transform.Find("nub").localScale = Vector3.one * this.nubSize;
	}

	public void startDrag()
	{
		Game.gameInstance.playSound("ui_begindrag", 1f, 1f);
		this.dragging = true;
		Game.gameInstance.draggingUIthing = true;
		this.lastDragTickPos.x = this.valX;
		this.lastDragTickPos.y = this.valY;
	}
}
