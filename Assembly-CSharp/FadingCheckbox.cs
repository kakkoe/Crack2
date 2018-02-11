using UnityEngine;
using UnityEngine.UI;

public class FadingCheckbox : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.GetComponent<Toggle>().isOn)
		{
			((Component)base.transform.Find("Label")).GetComponent<CanvasRenderer>().SetAlpha(1f);
		}
		else
		{
			((Component)base.transform.Find("Label")).GetComponent<CanvasRenderer>().SetAlpha(0.3f);
		}
	}
}
