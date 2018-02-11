using UnityEngine;
using UnityEngine.UI;

public class TooltipButton : MonoBehaviour
{
	private void Start()
	{
		this.leave();
	}

	private void Update()
	{
	}

	public void over()
	{
		((Component)base.transform.parent).GetComponent<Image>().enabled = true;
		((Component)base.transform.parent.Find("txt")).GetComponent<Text>().enabled = true;
	}

	public void leave()
	{
		((Component)base.transform.parent).GetComponent<Image>().enabled = false;
		((Component)base.transform.parent.Find("txt")).GetComponent<Text>().enabled = false;
	}
}
