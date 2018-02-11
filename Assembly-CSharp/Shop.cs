using System.Collections.Generic;
using UnityEngine;

public class Shop
{
	public static string currentShop;

	public static List<string> shopItems;

	public static bool instantDelivery;

	public static void populateShopItems(string shopID)
	{
		Shop.currentShop = shopID;
		Shop.shopItems = new List<string>();
		for (int i = 0; i < Inventory.itemData.items.Count; i++)
		{
			if (Inventory.itemData.items[i].shops.Contains(Shop.currentShop) && (!Inventory.itemData.items[i].requiresResearch || Inventory.data.completedResearch.Contains(Inventory.itemData.items[i].assetName)))
			{
				Shop.shopItems.Add(Inventory.itemData.items[i].assetName);
			}
		}
		Shop.shopItems.Sort(delegate(string p1, string p2)
		{
			int num = Shop.colorNumber(Inventory.getItemDefinition(p1).bagData.color).CompareTo(Shop.colorNumber(Inventory.getItemDefinition(p2).bagData.color));
			if (num == 0)
			{
				return p1.CompareTo(p2);
			}
			return num;
		});
	}

	public static int colorNumber(string col)
	{
		if (col != null)
		{
			if (!(col == "yellow"))
			{
				if (!(col == "pink"))
				{
					if (!(col == "purple"))
					{
						if (!(col == "green"))
						{
							if (!(col == "blue"))
							{
								if (!(col == "grey"))
								{
									goto IL_006b;
								}
								return 6;
							}
							return 5;
						}
						return 4;
					}
					return 3;
				}
				return 2;
			}
			return 1;
		}
		goto IL_006b;
		IL_006b:
		return 0;
	}

	public static void processDeliveries()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		float num4 = 0f;
		for (int i = 0; i < Inventory.data.orders.Count; i++)
		{
			bool flag = false;
			string deliveryLocation = Inventory.data.orders[i].deliveryLocation;
			if (deliveryLocation != null && deliveryLocation == "garage")
			{
				flag = true;
				if (Inventory.data.orders[i].timeRemaining > 0f)
				{
					num++;
					if (Inventory.data.orders[i].timeRemaining > num4)
					{
						num4 = Inventory.data.orders[i].timeRemaining;
					}
				}
				else
				{
					num2++;
					if (UserSettings.needTutorial("NPT_WAIT_FOR_DELIVERY"))
					{
						Objectives.completeObjective("NPT_WAIT_FOR_DELIVERY");
						UserSettings.completeTutorial("NPT_WAIT_FOR_DELIVERY");
					}
					num3 += Inventory.getItemDefinition(Inventory.data.orders[i].item).bagData.scale + 1;
				}
			}
			if (flag)
			{
				if (Inventory.data.freeplay)
				{
					Inventory.data.orders[i].timeRemaining -= Time.deltaTime * 50f;
				}
				else
				{
					Inventory.data.orders[i].timeRemaining -= Time.deltaTime;
				}
				if (Inventory.data.orders[i].timeRemaining <= 0f || Shop.instantDelivery)
				{
					Inventory.data.orders[i].timeRemaining = 0f;
				}
			}
		}
		((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("DeliveredBoxes")).GetComponent<DeliveredBoxes>().boxCount = (float)num3;
		if (num > 0)
		{
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("PENDING_ORDERS", string.Empty);
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = num.ToString();
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = Game.formatTime(Mathf.CeilToInt(num4), false);
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 2;
		}
		else if (num2 > 0)
		{
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("ORDERS_READY_FOR_PICKUP", string.Empty);
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = num2.ToString();
			((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = string.Empty;
			Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
				.Find("panel (0)")
				.Find("processingStatus")
				.Find("ready")
				.gameObject.SetActive(true);
				Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
					.Find("panel (0)")
					.Find("processingStatus")
					.Find("processing")
					.gameObject.SetActive(false);
					((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
						.Find("panel (0)")
						.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 1;
				}
				else
				{
					((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
						.Find("panel (0)")
						.Find("txtOrdersPending")).GetComponent<TextMesh>().text = Localization.getPhrase("PENDING_ORDERS", string.Empty);
					((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
						.Find("panel (0)")
						.Find("txtOrdersPendingC")).GetComponent<TextMesh>().text = "0";
					((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
						.Find("panel (0)")
						.Find("txtOrdersPendingT")).GetComponent<TextMesh>().text = string.Empty;
					Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
						.Find("panel (0)")
						.Find("processingStatus")
						.Find("ready")
						.gameObject.SetActive(false);
						Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
							.Find("panel (0)")
							.Find("processingStatus")
							.Find("processing")
							.gameObject.SetActive(false);
							((Component)Game.gameInstance.World.transform.Find("Lab").Find("Garage").Find("MainGarageDoor")
								.Find("panel (0)")
								.Find("processingStatus")).GetComponent<ProcessingStatusIndicator>().status = 0;
						}
					}
				}
