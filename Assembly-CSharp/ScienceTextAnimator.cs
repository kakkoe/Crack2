using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceTextAnimator : MonoBehaviour
{
	public string finalText = string.Empty;

	private float animProg;

	public static bool charsInitted;

	public static string[] chars;

	private int nextRand;

	private int advancer = 1;

	public static float tickSFXdelay;

	public static bool needTickSFX;

	private float animSpeed = 1f;

	private void Start()
	{
		if (!ScienceTextAnimator.charsInitted)
		{
			ScienceTextAnimator.charsInitted = true;
			List<string> list = new List<string>();
			list.Add("A");
			list.Add("B");
			list.Add("C");
			list.Add("D");
			list.Add("E");
			list.Add("F");
			list.Add("G");
			list.Add("H");
			list.Add("I");
			list.Add("J");
			list.Add("K");
			list.Add("L");
			list.Add("M");
			list.Add("N");
			list.Add("O");
			list.Add("P");
			list.Add("Q");
			list.Add("R");
			list.Add("S");
			list.Add("T");
			list.Add("U");
			list.Add("V");
			list.Add("W");
			list.Add("X");
			list.Add("Y");
			list.Add("Z");
			list.Add("a");
			list.Add("b");
			list.Add("c");
			list.Add("d");
			list.Add("e");
			list.Add("f");
			list.Add("g");
			list.Add("h");
			list.Add("i");
			list.Add("j");
			list.Add("k");
			list.Add("l");
			list.Add("m");
			list.Add("n");
			list.Add("o");
			list.Add("p");
			list.Add("q");
			list.Add("r");
			list.Add("s");
			list.Add("t");
			list.Add("u");
			list.Add("v");
			list.Add("w");
			list.Add("x");
			list.Add("y");
			list.Add("z");
			list.Add("0");
			list.Add("1");
			list.Add("2");
			list.Add("3");
			list.Add("4");
			list.Add("5");
			list.Add("6");
			list.Add("7");
			list.Add("8");
			list.Add("9");
			list.Add(".");
			list.Add("-");
			list.Add("_");
			list.Add("#");
			ScienceTextAnimator.chars = list.ToArray();
		}
		this.nextRand = Mathf.RoundToInt(Random.value * 1000f % (float)ScienceTextAnimator.chars.Length);
		this.advancer = 3 + Mathf.RoundToInt(Random.value * 25f);
	}

	private void Update()
	{
		if (ScienceTextAnimator.charsInitted && this.finalText != null)
		{
			string text = string.Empty;
			if (this.finalText.Length > 0)
			{
				if (this.animProg < (float)this.finalText.Length + 8.1f && !Game.gameInstance.anythingLoading && Game.gameInstance.loadTransition < 0.1f)
				{
					ScienceTextAnimator.needTickSFX = true;
					this.animProg += Time.deltaTime * this.animSpeed * 25f;
					for (int i = 0; i < this.finalText.Length; i++)
					{
						if ((float)i < this.animProg - 8f)
						{
							text += this.finalText.Substring(i, 1);
						}
						else if ((float)i < this.animProg)
						{
							if (this.nextRand < ScienceTextAnimator.chars.Length)
							{
								text += ScienceTextAnimator.chars[this.nextRand];
							}
							else
							{
								this.nextRand = Mathf.RoundToInt(Random.value * 1000f % (float)ScienceTextAnimator.chars.Length);
								this.advancer = 3 + Mathf.RoundToInt(Random.value * 25f);
							}
							this.nextRand = (this.nextRand + this.advancer) % ScienceTextAnimator.chars.Length;
						}
					}
					base.GetComponent<Text>().text = text;
				}
			}
			else
			{
				base.GetComponent<Text>().text = string.Empty;
			}
		}
	}

	public static void updateSFX()
	{
		if (ScienceTextAnimator.needTickSFX)
		{
			ScienceTextAnimator.tickSFXdelay -= Time.deltaTime;
			if (ScienceTextAnimator.tickSFXdelay <= 0f)
			{
				Game.gameInstance.playSound("ui_dragtick", 1f, 1f);
				ScienceTextAnimator.tickSFXdelay = 0.05f;
			}
		}
		ScienceTextAnimator.needTickSFX = false;
	}

	public void setText(string s, float delay = 0f, float speed = 1f, bool ignoreIfAlreadySet = false)
	{
		if (this.finalText == s && ignoreIfAlreadySet)
		{
			return;
		}
		this.finalText = s;
		this.animSpeed = speed;
		this.animProg = 0f - delay * this.animSpeed;
	}
}
