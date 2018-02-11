using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
	public bool hovering;

	public float hoverAnim;

	public string textKey;

	public string forcedTextKey = string.Empty;

	public bool selected;

	public bool fullHover;

	public bool alwaysShowBG;

	public int id1;

	public int id2;

	public Func<MenuOption, bool> callback;

	private void Start()
	{
		this.textKey = ((Component)base.transform.Find("Text")).GetComponent<Text>().text;
		((Component)base.transform.Find("Text")).GetComponent<Text>().text = Localization.getPhrase(this.textKey, string.Empty);
		((Component)base.transform.Find("Text")).GetComponent<Text>().text = Localization.getPhrase(this.textKey, string.Empty);
	}

	public void OnEnable()
	{
		this.hovering = false;
		this.selected = false;
	}

	public void setHover(bool h)
	{
		this.hovering = h;
	}

	public void clicked()
	{
		if (this.callback != null)
		{
			this.callback(this);
		}
	}

	private void Update()
	{
		if (this.forcedTextKey != string.Empty)
		{
			this.textKey = this.forcedTextKey;
		}
		if (this.selected || (this.hovering && this.fullHover))
		{
			this.hoverAnim += (1f - this.hoverAnim) * Game.cap(16f * Time.deltaTime, 0f, 1f);
		}
		else if (this.hovering || this.alwaysShowBG)
		{
			this.hoverAnim += (0.2f - this.hoverAnim) * Game.cap(16f * Time.deltaTime, 0f, 1f);
		}
		else
		{
			this.hoverAnim += (0f - this.hoverAnim) * Game.cap(8f * Time.deltaTime, 0f, 1f);
		}
		((Component)base.transform.Find("selectionBG")).GetComponent<CanvasRenderer>().SetAlpha(this.hoverAnim);
		((Component)base.transform.Find("Text")).GetComponent<Text>().text = Localization.getPhrase(this.textKey, string.Empty);
	}
}
