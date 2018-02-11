using System.Collections;
using UnityEngine;

public class HUDFPS : MonoBehaviour
{
	public Rect startRect = new Rect(10f, 10f, 75f, 50f);

	public bool updateColor = true;

	public bool allowDrag;

	public float frequency = 0.5f;

	public int nbDecimal = 1;

	private double lastInterval;

	private int frames;

	private Color color = Color.white;

	private string sFPS = string.Empty;

	private GUIStyle style;

	private void Start()
	{
		this.lastInterval = (double)Time.realtimeSinceStartup;
		this.frames = 0;
		base.StartCoroutine(this.FPS());
	}

	private void Update()
	{
		this.frames++;
	}

    private IEnumerator FPS()
    {
        for (; ; )
        {
            float timeNow = Time.realtimeSinceStartup;
            if ((double)timeNow > this.lastInterval + (double)this.frequency)
            {
                double num = (double)this.frames / ((double)timeNow - this.lastInterval);
                this.frames = 0;
                this.lastInterval = (double)timeNow;
                this.sFPS = num.ToString("f" + Mathf.Clamp(this.nbDecimal, 0, 10));
                this.color = ((num < 30.0) ? ((num >= 10.0) ? Color.yellow : Color.red) : Color.green);
            }
            yield return new WaitForSeconds(this.frequency);
        }
        yield break;
    }

    private void OnGUI()
	{
		if (this.style == null)
		{
			this.style = new GUIStyle(GUI.skin.label);
			this.style.normal.textColor = Color.white;
			this.style.alignment = TextAnchor.MiddleCenter;
		}
		GUI.color = ((!this.updateColor) ? Color.white : this.color);
		this.startRect = GUI.Window(0, this.startRect, this.DoMyWindow, string.Empty);
	}

	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, this.startRect.width, this.startRect.height), this.sFPS + " FPS", this.style);
		if (this.allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}
}
