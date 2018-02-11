using UnityEngine;
using UnityEngine.UI;

public class GradientEditor : MonoBehaviour
{
	public int id;

	private void Start()
	{
	}

	private void Update()
	{
		((Component)base.transform.Find("Number")).GetComponent<Text>().text = this.id + 1 + string.Empty;
	}

	public void addColor()
	{
		Debug.Log("Adding...");
		float num = 0f;
		float num2 = 0f;
		Color a = Game.gameInstance.PC().data.embellishmentColors[this.id];
		Color b = Game.gameInstance.PC().data.embellishmentColors[this.id];
		for (int i = 0; i < Game.gameInstance.PC().data.embellishmentColorGradientPoints.Count; i++)
		{
			if (Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].embellishmentID == this.id)
			{
				float num3 = 1f;
				int num4 = -1;
				for (int j = 0; j < Game.gameInstance.PC().data.embellishmentColorGradientPoints.Count; j++)
				{
					if (Game.gameInstance.PC().data.embellishmentColorGradientPoints[j].embellishmentID == this.id && j != i && Game.gameInstance.PC().data.embellishmentColorGradientPoints[j].position < Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].position && Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].position - Game.gameInstance.PC().data.embellishmentColorGradientPoints[j].position < num3)
					{
						num3 = Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].position - Game.gameInstance.PC().data.embellishmentColorGradientPoints[j].position;
						num4 = j;
					}
				}
				float num5 = 0f;
				if (num4 != -1)
				{
					num5 = Game.gameInstance.PC().data.embellishmentColorGradientPoints[num4].position;
				}
				float num6 = Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].position - num5;
				if (num6 > num)
				{
					if (num4 != -1)
					{
						a = Game.gameInstance.PC().data.embellishmentColorGradientPoints[num4].color;
					}
					b = Game.gameInstance.PC().data.embellishmentColorGradientPoints[i].color;
					num = num6;
					num2 = num5;
				}
			}
		}
		if (num == 0f && num2 == 0f)
		{
			num = 1f;
		}
		Debug.Log(num2 + " - " + num);
		Game.gameInstance.PC().data.embellishmentColorGradientPoints.Add(new EmbellishmentColorGradientPoint());
		Game.gameInstance.PC().data.embellishmentColorGradientPoints[Game.gameInstance.PC().data.embellishmentColorGradientPoints.Count - 1].color = Color.Lerp(a, b, 0.5f);
		Game.gameInstance.PC().data.embellishmentColorGradientPoints[Game.gameInstance.PC().data.embellishmentColorGradientPoints.Count - 1].position = num2 + num / 2f;
		Game.gameInstance.PC().data.embellishmentColorGradientPoints[Game.gameInstance.PC().data.embellishmentColorGradientPoints.Count - 1].embellishmentID = this.id;
		Game.gameInstance.needEmbellishmentColorRebuildDots = true;
		Game.gameInstance.needEmbellishmentColorRedraw = true;
		Game.gameInstance.needEmbellishmentColorRedrawSpecific = this.id;
	}
}
