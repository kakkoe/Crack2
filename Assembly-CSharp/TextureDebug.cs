using UnityEngine;
using UnityEngine.UI;

public class TextureDebug : MonoBehaviour
{
	public static bool initted;

	public static GameObject debugDisplay;

	public static bool showing;

	private void Start()
	{
		if (!TextureDebug.initted)
		{
			TextureDebug.debugDisplay = GameObject.Find("TextureDebug");
			TextureDebug.initted = true;
			TextureDebug.debugDisplay.transform.localPosition = Vector3.zero;
			TextureDebug.debugDisplay.SetActive(false);
		}
	}

	private void Update()
	{
		if (TextureDebug.showing && Input.GetKeyDown(KeyCode.F1))
		{
			TextureDebug.debugDisplay.SetActive(false);
			TextureDebug.showing = false;
		}
	}

	public static void showTex(Texture2D tex)
	{
		TextureDebug.debugDisplay.GetComponent<RawImage>().texture = tex;
		TextureDebug.debugDisplay.SetActive(true);
		TextureDebug.showing = true;
	}

	public static void hide()
	{
		TextureDebug.debugDisplay.SetActive(false);
		TextureDebug.showing = false;
	}
}
