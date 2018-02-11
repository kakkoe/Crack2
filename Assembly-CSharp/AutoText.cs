using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{
	public string phrase = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
		if (this.phrase == string.Empty)
		{
			this.phrase = base.GetComponent<Text>().text;
		}
		base.GetComponent<Text>().text = Localization.getPhrase(this.phrase, string.Empty);
	}
}
