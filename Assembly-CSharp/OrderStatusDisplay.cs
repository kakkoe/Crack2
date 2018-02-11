using System.Collections.Generic;
using UnityEngine;

public class OrderStatusDisplay : MonoBehaviour
{
	public string stationName = string.Empty;

	public string boxOpenSound = "boxopen";

	private List<GameObject> particleEmitters = new List<GameObject>();

	public int numBoxes = 5;

	private List<Vector3> originalScales = new List<Vector3>();

	private float lastUpdateTime;

	private float displayedBoxCount;

	private float timeSinceLastUpdate;

	private void Start()
	{
		for (int i = 0; i < this.numBoxes; i++)
		{
			GameObject gameObject = Object.Instantiate(base.transform.Find("openingParticles").gameObject);
			gameObject.transform.position = base.transform.Find("box" + i).position + Vector3.up * 0.4f;
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.rotation = base.transform.Find("openingParticles").rotation;
			gameObject.transform.localScale = Vector3.one;
			this.particleEmitters.Add(gameObject);
			this.originalScales.Add(base.transform.Find("box" + i).localScale);
		}
		this.lastUpdateTime = Time.time;
	}

	public void pickUpOrders()
	{
		for (int num = Inventory.data.orders.Count - 1; num >= 0; num--)
		{
			if (Inventory.data.orders[num].deliveryLocation == this.stationName && Inventory.data.orders[num].timeRemaining <= 0f)
			{
				Inventory.giveItem(Inventory.data.orders[num].item, Inventory.data.orders[num].properties, string.Empty, true, true, 0);
				Inventory.data.orders.RemoveAt(num);
			}
		}
		Inventory.saveInventoryData();
	}

	private void Update()
	{
		this.timeSinceLastUpdate = Time.time - this.lastUpdateTime;
		this.lastUpdateTime = Time.time;
		if (Inventory.data.freeplay)
		{
			this.timeSinceLastUpdate *= 50f;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		float num4 = 0f;
		bool flag = false;
		for (int i = 0; i < Inventory.data.orders.Count; i++)
		{
			bool flag2 = false;
			if (Inventory.data.orders[i].deliveryLocation == this.stationName)
			{
				if (Inventory.data.orders[i].timeRemaining > 0f)
				{
					flag2 = !flag;
					num++;
					num4 += Inventory.data.orders[i].timeRemaining;
				}
				else
				{
					num2++;
					num3++;
				}
			}
			if (flag2)
			{
				Inventory.data.orders[i].timeRemaining -= this.timeSinceLastUpdate;
				if (Inventory.data.orders[i].timeRemaining <= 0f || Shop.instantDelivery)
				{
					this.timeSinceLastUpdate = 0f - Inventory.data.orders[i].timeRemaining;
					Inventory.data.orders[i].timeRemaining = 0f;
				}
				else
				{
					flag = true;
				}
			}
		}
		if (num > 0)
		{
			((Component)base.transform.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("QUEUED_ITEMS", string.Empty);
			((Component)base.transform.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = num.ToString();
			((Component)base.transform.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = Game.formatTime(Mathf.CeilToInt(num4), false);
			((Component)base.transform.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 2;
		}
		else if (num2 > 0)
		{
			((Component)base.transform.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("ITEMS_READY_FOR_PICKUP", string.Empty);
			((Component)base.transform.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = num2.ToString();
			((Component)base.transform.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = string.Empty;
			base.transform.Find("processingStatus").Find("ready").gameObject.SetActive(true);
			base.transform.Find("processingStatus").Find("processing").gameObject.SetActive(false);
			((Component)base.transform.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 1;
		}
		else
		{
			((Component)base.transform.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("QUEUED_ITEMS", string.Empty);
			((Component)base.transform.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = "0";
			((Component)base.transform.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = string.Empty;
			base.transform.Find("processingStatus").Find("ready").gameObject.SetActive(false);
			base.transform.Find("processingStatus").Find("processing").gameObject.SetActive(false);
			((Component)base.transform.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 0;
		}
		if ((float)num3 > this.displayedBoxCount)
		{
			this.displayedBoxCount += Time.deltaTime * 3f;
			if (this.displayedBoxCount > (float)num3)
			{
				this.displayedBoxCount = (float)num3;
			}
		}
		if ((float)num3 < this.displayedBoxCount)
		{
			this.displayedBoxCount -= Time.deltaTime * 3f;
			if (this.displayedBoxCount < (float)num3)
			{
				this.displayedBoxCount = (float)num3;
			}
		}
		for (int j = 0; j < this.numBoxes; j++)
		{
			bool flag3 = this.displayedBoxCount > (float)j;
			if (flag3 && !base.transform.Find("box" + j).gameObject.activeSelf)
			{
				base.transform.Find("box" + j).transform.localScale = Vector3.zero;
				this.particleEmitters[j].SetActive(false);
			}
			else if (!flag3 && base.transform.Find("box" + j).gameObject.activeSelf)
			{
				Game.PlaySFXAtPoint(Resources.Load(this.boxOpenSound) as AudioClip, base.transform.Find("box" + j).position, 1f);
				this.particleEmitters[j].SetActive(true);
			}
			base.transform.Find("box" + j).gameObject.SetActive(flag3);
			if (flag3)
			{
				Transform transform = base.transform.Find("box" + j);
				transform.localScale += (this.originalScales[j] - base.transform.Find("box" + j).localScale) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
			}
		}
		base.transform.Find("pickupHotspot").gameObject.SetActive(num3 > 0);
	}
}
