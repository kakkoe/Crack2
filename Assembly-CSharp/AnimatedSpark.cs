using UnityEngine;

public class AnimatedSpark : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Find("spark0").Rotate(0f, 0f, Time.deltaTime * 363f);
		base.transform.Find("spark0").localScale = Vector3.one * (0.8f + 0.2f * Mathf.Cos(Time.time * 2.77f));
		base.transform.Find("spark1").Rotate(0f, 0f, (0f - Time.deltaTime) * 450f);
		base.transform.Find("spark1").localScale = Vector3.one * (0.8f + 0.2f * Mathf.Cos(Time.time * 3.1f));
	}
}
