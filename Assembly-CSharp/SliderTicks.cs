using UnityEngine;
using UnityEngine.UI;

public class SliderTicks : MonoBehaviour
{
	private float lastVal;

	public static float tickDelay;

	private void Start()
	{
	}

	private void Update()
	{
		if (Game.initted && Mathf.Abs(base.GetComponent<UnityEngine.UI.Slider>().value - this.lastVal) > 0.1f)
		{
			if (SliderTicks.tickDelay <= 0f)
			{
				Game.gameInstance.playSound("ui_dragtick", 1f, 1f);
			}
			this.lastVal = base.GetComponent<UnityEngine.UI.Slider>().value;
			SliderTicks.tickDelay = 0.025f;
		}
	}
}
