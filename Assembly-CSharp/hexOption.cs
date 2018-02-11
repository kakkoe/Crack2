using UnityEngine;
using UnityEngine.UI;

public class hexOption : MonoBehaviour
{
	public HexOptionSystem system;

	public string tooltipPhrase = string.Empty;

	public bool selected;

	public bool isModButton;

	private void Start()
	{
		this.system = ((Component)base.transform.parent).GetComponent<HexOptionSystem>();
	}

	private void Update()
	{
		if ((Object)base.transform.Find("selectionTrim") != (Object)null)
		{
			base.transform.Find("selectionTrim").gameObject.SetActive(this.selected);
		}
	}

	public void mouseOver()
	{
		base.transform.Find("tooltipBG").gameObject.SetActive(true);
		base.transform.Find("tooltipText").gameObject.SetActive(true);
		if (this.tooltipPhrase == string.Empty)
		{
			this.tooltipPhrase = base.name;
		}
		((Component)base.transform.Find("tooltipText")).GetComponent<Text>().text = Localization.getPhrase(this.tooltipPhrase, string.Empty);
		UISFX.playHover();
	}

	public void mouseOut()
	{
		base.transform.Find("tooltipBG").gameObject.SetActive(false);
		base.transform.Find("tooltipText").gameObject.SetActive(false);
	}

	public void clicked()
	{
		Game.gameInstance.playSound("ui_click", 1f, 1f);
		if (this.isModButton)
		{
			Game.OpenInFileBrowser(Application.persistentDataPath);
		}
		else
		{
			this.system.clearAll();
			this.system.changed();
			this.selected = true;
		}
	}
}
