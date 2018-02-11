using UnityEngine;

public class UISFX : MonoBehaviour
{
	public static float hoverDelay;

	public static int nextHover;

	private void Start()
	{
	}

	private void Update()
	{
		if (UISFX.hoverDelay > 0f)
		{
			UISFX.hoverDelay -= Time.deltaTime / (0.01f + Time.timeScale);
		}
		if (SliderTicks.tickDelay > 0f)
		{
			SliderTicks.tickDelay -= Time.deltaTime;
		}
	}

	public void hover()
	{
		if (Game.hasFocus && !(UISFX.hoverDelay > 0f))
		{
			Game.gameInstance.playSound("ui_hoverTick", 1f, 1f);
			Game.gameInstance.playSound("ui_hover" + UISFX.nextHover, 1f, 1f);
			UISFX.nextHover += Mathf.FloorToInt(Random.value * 7f);
			UISFX.nextHover %= 8;
			UISFX.hoverDelay = 0.025f;
		}
	}

	public static void playHover()
	{
		if (!(UISFX.hoverDelay > 0f))
		{
			Game.gameInstance.playSound("ui_hoverTick", 1f, 1f);
			Game.gameInstance.playSound("ui_hover" + UISFX.nextHover, 1f, 1f);
			UISFX.nextHover += Mathf.FloorToInt(Random.value * 7f);
			UISFX.nextHover %= 8;
			UISFX.hoverDelay = 0.025f;
		}
	}

	public void click(string sound = "")
	{
		UISFX.clickSFX(sound);
	}

	public static void clickSFX(string sound = "")
	{
		if (!(UISFX.hoverDelay > 0f))
		{
			if (sound == string.Empty)
			{
				sound = "ui_click";
			}
			Game.gameInstance.playSound(sound, 1f, 1f);
			UISFX.hoverDelay = 0.025f;
		}
	}
}
