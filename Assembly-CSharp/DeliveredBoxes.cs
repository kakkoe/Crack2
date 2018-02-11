using System.Collections.Generic;
using UnityEngine;

public class DeliveredBoxes : MonoBehaviour
{
	public float boxCount;

	private float boxCount_anim;

	private int lastBoxCountDisplayed;

	private List<int> boxThreshholds = new List<int>();

	private List<Vector3> originalBoxScales = new List<Vector3>();

	private List<GameObject> particleEmitters = new List<GameObject>();

	private void Start()
	{
		this.boxThreshholds = new List<int>();
		this.boxThreshholds.Add(0);
		this.boxThreshholds.Add(1);
		this.boxThreshholds.Add(2);
		this.boxThreshholds.Add(4);
		this.boxThreshholds.Add(6);
		this.boxThreshholds.Add(8);
		this.boxThreshholds.Add(10);
		this.boxThreshholds.Add(13);
		this.boxThreshholds.Add(16);
		this.boxThreshholds.Add(18);
		this.boxThreshholds.Add(20);
		this.boxThreshholds.Add(25);
		this.boxThreshholds.Add(30);
		this.boxThreshholds.Add(40);
		this.originalBoxScales = new List<Vector3>();
		for (int i = 0; i < this.boxThreshholds.Count; i++)
		{
			this.originalBoxScales.Add(base.transform.Find("box" + this.boxThreshholds[i]).localScale);
			GameObject gameObject = Object.Instantiate(base.transform.Find("openingParticles").gameObject);
			gameObject.transform.position = base.transform.Find("box" + this.boxThreshholds[i]).position + Vector3.up * 0.9f;
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.rotation = base.transform.Find("openingParticles").rotation;
			gameObject.transform.localScale = Vector3.one;
			this.particleEmitters.Add(gameObject);
		}
	}

	public bool openBoxes()
	{
		for (int num = Inventory.data.orders.Count - 1; num >= 0; num--)
		{
			if (Inventory.data.orders[num].deliveryLocation == "garage" && Inventory.data.orders[num].timeRemaining <= 0f)
			{
				Inventory.giveItem(Inventory.data.orders[num].item, Inventory.data.orders[num].properties, string.Empty, true, true, 0);
				if (Inventory.data.orders[num].item == "MaterialSynthesisStation" && UserSettings.needTutorial("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION"))
				{
					Objectives.completeObjective("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION");
					UserSettings.completeTutorial("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION");
				}
				Inventory.data.orders.RemoveAt(num);
			}
		}
		Inventory.saveInventoryData();
		return true;
	}

	private void Update()
	{
		if (this.boxCount > this.boxCount_anim)
		{
			this.boxCount_anim += Time.deltaTime * 5f;
			if (this.boxCount_anim > this.boxCount)
			{
				this.boxCount_anim = this.boxCount;
			}
		}
		if (this.boxCount < this.boxCount_anim)
		{
			this.boxCount_anim -= Time.deltaTime * 15f;
			if (this.boxCount_anim < this.boxCount)
			{
				this.boxCount_anim = this.boxCount;
			}
			if (this.boxCount_anim < 3f && this.boxCount == 0f)
			{
				this.boxCount_anim = 0f;
			}
		}
		int num = Mathf.FloorToInt(this.boxCount_anim);
		base.transform.Find("CollectableAura").gameObject.SetActive(num > 0);
		if (this.boxCount > 0f && (Object)Game.gameInstance != (Object)null && Game.gameInstance.PC() != null && (base.transform.position - Game.gameInstance.PC().GO.transform.position).magnitude < 9f)
		{
			Game.gameInstance.context(Localization.getPhrase("OPEN_BOXES", string.Empty), this.openBoxes, base.transform.position, false);
		}
		for (int i = 0; i < this.boxThreshholds.Count; i++)
		{
			if (num != this.lastBoxCountDisplayed)
			{
				if (num > this.boxThreshholds[i])
				{
					if (this.lastBoxCountDisplayed <= this.boxThreshholds[i])
					{
						base.transform.Find("box" + this.boxThreshholds[i]).localScale = Vector3.one * 0.001f;
						this.particleEmitters[i].SetActive(false);
					}
					base.transform.Find("box" + this.boxThreshholds[i]).gameObject.SetActive(true);
				}
				else
				{
					if (this.lastBoxCountDisplayed > this.boxThreshholds[i])
					{
						Game.PlaySFXAtPoint(Resources.Load("boxopen") as AudioClip, base.transform.Find("box" + this.boxThreshholds[i]).position, 1f);
						this.particleEmitters[i].SetActive(true);
					}
					base.transform.Find("box" + this.boxThreshholds[i]).gameObject.SetActive(false);
				}
			}
			Transform transform = base.transform.Find("box" + this.boxThreshholds[i]);
			transform.localScale += (this.originalBoxScales[i] - base.transform.Find("box" + this.boxThreshholds[i]).localScale) * Game.cap(Time.deltaTime * 15f, 0f, 1f);
		}
		this.lastBoxCountDisplayed = num;
	}
}
