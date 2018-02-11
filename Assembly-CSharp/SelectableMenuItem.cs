using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectableMenuItem : MonoBehaviour
{
	public Func<SelectableMenuItem, bool> onClick;

	public Func<SelectableMenuItem, bool> onDeleteClicked;

	public bool over;

	public float hoverAmount;

	public int id;

	public int val;

	public int max;

	public float highlightAmount = 1f;

	public Color highlightColor;

	private void Start()
	{
		this.highlightColor = Color.white;
		this.highlightColor.a = 0f;
	}

	private void Update()
	{
		if (this.over)
		{
			this.hoverAmount += (1f - this.hoverAmount) * Game.cap(16f * Time.deltaTime, 0f, 1f);
		}
		else
		{
			this.hoverAmount += (0f - this.hoverAmount) * Game.cap(9f * Time.deltaTime, 0f, 1f);
		}
		Color loadingOrange = Game.gameInstance.loadingOrange;
		Color loadingBlue = Game.gameInstance.loadingBlue;
		float num = Time.timeSinceLevelLoad * 3.5f;
		Vector3 position = base.transform.position;
		float num2 = num + position.x;
		Vector3 position2 = base.transform.position;
		this.highlightColor = Color.Lerp(loadingOrange, loadingBlue, 0.5f + 0.5f * Mathf.Cos(num2 + position2.y));
		this.highlightColor.a = this.highlightAmount;
		if ((UnityEngine.Object)base.transform.Find("highlight") != (UnityEngine.Object)null)
		{
			((Component)base.transform.Find("highlight")).GetComponent<Image>().color = this.highlightColor;
		}
	}

	public void mouseOver()
	{
		this.over = true;
		UISFX.playHover();
	}

	public void mouseOut()
	{
		this.over = false;
	}

	public void mouseClick()
	{
		if (this.onClick != null)
		{
			this.onClick(this);
		}
		Game.gameInstance.playSound("ui_click", 1f, 1f);
	}

	public void deleteClicked()
	{
		if (this.onDeleteClicked != null)
		{
			this.onDeleteClicked(this);
		}
	}

	public void variantBarClick()
	{
		Game.gameInstance.patternCardVariantBarClicked(this, ((Component)base.transform.Find("VariantBar").Find("BG")).GetComponent<Image>());
	}

	public void embellishmentVariantBarClick()
	{
		Game.gameInstance.embellishmentCardVariantBarClicked(this, ((Component)base.transform.Find("VariantBar").Find("BG")).GetComponent<Image>());
	}
}
