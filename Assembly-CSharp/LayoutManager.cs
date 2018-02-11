using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class LayoutManager
{
	public const int numRooms = 4;

	public static LayoutData data;

	public static TestingRoom[] rooms;

	public static List<GameObject> allLabItems = new List<GameObject>();

	public static List<string> allLabItemNames = new List<string>();

	public static int specimenCapacity = 0;

	public static bool needSpecimenCapacityRecalculation = true;

	public static void init()
	{
		TestingRoom.labItemContainer = GameObject.Find("LabItems").transform;
		TestingRoom.labItemContainer.gameObject.SetActive(false);
		LayoutManager.getRoomReferences();
	}

	public static RoomLayout getLayoutByName(string name)
	{
		for (int i = 0; i < LayoutManager.data.layouts.Count; i++)
		{
			if (name == LayoutManager.data.layouts[i].name)
			{
				return LayoutManager.data.layouts[i];
			}
		}
		return LayoutManager.data.layouts[0];
	}

	public static RoomLayout getLayoutByRoomID(int id)
	{
		return LayoutManager.getLayoutByName(LayoutManager.data.activeLayouts[id]);
	}

	public static void getRoomReferences()
	{
		LayoutManager.rooms = new TestingRoom[4];
		for (int i = 0; i < 3; i++)
		{
			LayoutManager.rooms[i] = ((Component)GameObject.Find("World").transform.Find("Rooms").Find("Room" + i)).GetComponent<TestingRoom>();
		}
		LayoutManager.rooms[3] = ((Component)GameObject.Find("World").transform.Find("Rooms").Find("Pit")).GetComponent<TestingRoom>();
	}

	public static void applyLayouts()
	{
		for (int i = 0; i < 4; i++)
		{
			LayoutManager.applyLayout(i);
		}
		Game.gameInstance.updateRoomReflections();
	}

	public static void applyLayout(int r)
	{
		for (int num = LayoutManager.rooms[r].transform.childCount - 1; num >= 0; num--)
		{
			Object.Destroy(LayoutManager.rooms[r].transform.GetChild(num).gameObject);
		}
		LayoutManager.allLabItems = new List<GameObject>();
		LayoutManager.allLabItemNames = new List<string>();
		Bag bagByName = Inventory.getBagByName("TESTING_ROOM_" + (r + 1));
		for (int num2 = bagByName.contents.Count - 1; num2 >= 0; num2--)
		{
			string destinationBag = "GARAGE";
			if (Inventory.getItemDefinition(bagByName.contents[num2].itemType).bagData.scale == 1)
			{
				destinationBag = "STORAGE";
			}
			if (Inventory.getItemDefinition(bagByName.contents[num2].itemType).bagData.scale == 0)
			{
				destinationBag = "LOCKER_SHELF";
			}
			Inventory.moveItemToDifferentBag(bagByName.contents[num2].itemType, "TESTING_ROOM_" + (r + 1), destinationBag);
		}
		RoomLayout layoutByName = LayoutManager.getLayoutByName(LayoutManager.data.activeLayouts[r]);
		LayoutManager.rooms[r].layoutName = layoutByName.name;
		for (int i = 0; i < layoutByName.items.Count; i++)
		{
			if (Inventory.moveItemToDifferentBag(layoutByName.items[i].assetName, string.Empty, "TESTING_ROOM_" + (r + 1)))
			{
				LayoutManager.applyItemToRoom(layoutByName.items[i], r, i, false);
			}
			else
			{
				Debug.Log("Unable to put " + layoutByName.items[i].assetName + " in the laboratory because you don't have one available.");
			}
		}
	}

	public static void addItemToLayout(LayoutItem item, string name, int parent = -1)
	{
		for (int i = 0; i < LayoutManager.data.layouts.Count; i++)
		{
			if (LayoutManager.data.layouts[i].name == name)
			{
				if (parent != -1)
				{
					LayoutManager.data.layouts[i].items[parent].children.Add(item.uid);
				}
				LayoutManager.data.layouts[i].items.Add(item);
				for (int j = 0; j < 4; j++)
				{
					if (LayoutManager.rooms[j].layoutName == name)
					{
						LayoutManager.applyLayout(j);
					}
				}
			}
		}
		LayoutManager.somethingInLayoutHasChanged();
		LayoutManager.saveLayoutData();
	}

	public static void removeItemFromLayoutByUID(int l, string uid)
	{
		for (int i = 0; i < LayoutManager.data.layouts[l].items.Count; i++)
		{
			if (LayoutManager.data.layouts[l].items[i].uid == uid)
			{
				for (int j = 0; j < LayoutManager.data.layouts[l].items[i].children.Count; j++)
				{
					LayoutManager.removeItemFromLayoutByUID(l, LayoutManager.data.layouts[l].items[i].children[j]);
				}
				LayoutManager.data.layouts[l].items.RemoveAt(i);
			}
		}
	}

	public static void removeItemFromLayout(string itemName, Vector3 itemLocalPosition, string name)
	{
		for (int i = 0; i < LayoutManager.data.layouts.Count; i++)
		{
			if (LayoutManager.data.layouts[i].name == name)
			{
				for (int j = 0; j < LayoutManager.data.layouts[i].items.Count; j++)
				{
					if (LayoutManager.data.layouts[i].items[j].assetName == itemName && (LayoutManager.data.layouts[i].items[j].position - itemLocalPosition).magnitude < 0.01f)
					{
						for (int k = 0; k < LayoutManager.data.layouts[i].items[j].children.Count; k++)
						{
							LayoutManager.removeItemFromLayoutByUID(i, LayoutManager.data.layouts[i].items[j].children[k]);
						}
						LayoutManager.data.layouts[i].items.RemoveAt(j);
						LayoutManager.somethingInLayoutHasChanged();
						LayoutManager.saveLayoutData();
						for (int l = 0; l < 4; l++)
						{
							if (LayoutManager.rooms[l].layoutName == name)
							{
								LayoutManager.applyLayout(l);
							}
						}
						return;
					}
				}
			}
		}
	}

	public static void applyItemToRoom(LayoutItem item, int roomNumber, int index, bool partOfBatch = false)
	{
		TestingRoom.labItemContainer.gameObject.SetActive(true);
		GameObject.Find("LabItems").transform.Find(item.assetName).gameObject.SetActive(true);
		GameObject gameObject = Object.Instantiate(GameObject.Find("LabItems").transform.Find(item.assetName).gameObject);
		MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].reflectionProbeUsage = ReflectionProbeUsage.Simple;
		}
		Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			if ((Object)((Component)componentsInChildren2[j].transform).GetComponent<Rigidbody>() == (Object)null)
			{
				componentsInChildren2[j].gameObject.AddComponent<Rigidbody>().isKinematic = true;
			}
		}
		LayoutManager.recursiveSetLayer(gameObject, 11);
		GameObject.Find("LabItems").transform.Find(item.assetName).gameObject.SetActive(false);
		gameObject.AddComponent<LabItemInWorld>().roomID = roomNumber;
		gameObject.GetComponent<LabItemInWorld>().index = index;
		gameObject.GetComponent<LabItemInWorld>().layoutData = item;
		gameObject.name = item.assetName;
		gameObject.transform.parent = LayoutManager.rooms[roomNumber].transform;
		gameObject.transform.localPosition = item.position;
		gameObject.transform.localEulerAngles = item.rotation;
		LayoutManager.allLabItems.Add(gameObject);
		LayoutManager.allLabItemNames.Add(gameObject.name);
		TestingRoom.labItemContainer.gameObject.SetActive(false);
		if (!partOfBatch)
		{
			LayoutManager.somethingInLayoutHasChanged();
		}
	}

	public static void recursiveSetLayer(GameObject GO, int l)
	{
		if (GO.layer != 22)
		{
			GO.layer = l;
		}
		if (GO.name == "PlacementBounds")
		{
			GO.layer = 13;
		}
		for (int i = 0; i < GO.transform.childCount; i++)
		{
			LayoutManager.recursiveSetLayer(GO.transform.GetChild(i).gameObject, l);
		}
	}

	public static void removeItemFromRoom()
	{
		LayoutManager.somethingInLayoutHasChanged();
	}

	public static void somethingInLayoutHasChanged()
	{
		LayoutManager.needSpecimenCapacityRecalculation = true;
	}

	public static void loadLayouts()
	{
		if (File.Exists(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackLayoutData"))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(LayoutData));
			FileStream fileStream = new FileStream(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackLayoutData", FileMode.Open);
			LayoutManager.data = (xmlSerializer.Deserialize(fileStream) as LayoutData);
			fileStream.Close();
			LayoutManager.saveLayoutData();
			LayoutManager.applyLayouts();
		}
		else
		{
			LayoutManager.data = new LayoutData();
			RoomLayout roomLayout = new RoomLayout();
			LayoutItem layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("COLORED_SPOT_LIGHT", string.Empty);
			layoutItem.assetName = "ColoredSpotlight";
			layoutItem.position = new Vector3(39f, 15.1434412f, 9f);
			layoutItem.rotation = new Vector3(90f, 225f, 0f);
			layoutItem.customProperties = new LayoutItemSpecialProperties();
			layoutItem.customProperties.enabled = true;
			layoutItem.customProperties.power = 0.5f;
			layoutItem.customProperties.color = new Color(0.07316f, 0.628f, 1f, 1f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("COLORED_SPOT_LIGHT", string.Empty);
			layoutItem.assetName = "ColoredSpotlight";
			layoutItem.position = new Vector3(2f, 15.1434412f, 11f);
			layoutItem.rotation = new Vector3(90f, 225f, 0f);
			layoutItem.customProperties = new LayoutItemSpecialProperties();
			layoutItem.customProperties.enabled = true;
			layoutItem.customProperties.power = 0.5f;
			layoutItem.customProperties.color = new Color(1f, 0.6002f, 0.1895f, 1f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("CEILING_LIGHT", string.Empty);
			layoutItem.assetName = "CeilingLight";
			layoutItem.position = new Vector3(20f, 15.1434412f, 2f);
			layoutItem.rotation = new Vector3(90f, -4.829f, 0f);
			layoutItem.customProperties = new LayoutItemSpecialProperties();
			layoutItem.customProperties.enabled = false;
			layoutItem.customProperties.power = 0.66f;
			layoutItem.customProperties.color = new Color(0.85f, 0.95f, 1f, 1f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("LIGHTING_CONTROL_PANEL", string.Empty);
			layoutItem.assetName = "LightingControlPanel";
			layoutItem.position = new Vector3(36.41f, 3.6349f, 19.689f);
			layoutItem.rotation = new Vector3(0f, 180f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("CORNER_DESK", string.Empty);
			layoutItem.assetName = "CornerDesk";
			layoutItem.position = new Vector3(37.01f, -0.5228f, 4.228f);
			layoutItem.rotation = new Vector3(270f, 178.2f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("OFFICE_CHAIR", string.Empty);
			layoutItem.assetName = "OfficeChair";
			layoutItem.position = new Vector3(37.091f, -0.5228f, 3.9456f);
			layoutItem.rotation = new Vector3(270f, 303.99f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("WALL_SHELF", string.Empty);
			layoutItem.assetName = "WallShelf";
			layoutItem.position = new Vector3(36.452f, 7.225f, -0.52682f);
			layoutItem.rotation = new Vector3(0f, 0f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("WALL_SHELF", string.Empty);
			layoutItem.assetName = "WallShelf";
			layoutItem.position = new Vector3(-0.41193f, 6.25153f, 6.667f);
			layoutItem.rotation = new Vector3(0f, 90f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("SHELF", string.Empty);
			layoutItem.assetName = "Shelf";
			layoutItem.position = new Vector3(39f, -0.5228f, 14f);
			layoutItem.rotation = new Vector3(270f, 90f, 0f);
			roomLayout.items.Add(layoutItem);
			layoutItem = new LayoutItem();
			layoutItem.name = Localization.getPhrase("RACK_CHAIR", string.Empty);
			layoutItem.assetName = "RackChair";
			layoutItem.position = new Vector3(5.39121f, -0.5228f, 11.1726f);
			layoutItem.rotation = new Vector3(270f, 61.2753f, 0f);
			roomLayout.items.Add(layoutItem);
			roomLayout.name = Localization.getPhrase("BLUE_LAYOUT", string.Empty);
			LayoutManager.data.layouts.Add(roomLayout);
			RoomLayout roomLayout2 = new RoomLayout();
			roomLayout2.name = Localization.getPhrase("WHITE_LAYOUT", string.Empty);
			LayoutManager.data.layouts.Add(roomLayout2);
			RoomLayout roomLayout3 = new RoomLayout();
			roomLayout3.name = Localization.getPhrase("ORANGE_LAYOUT", string.Empty);
			LayoutManager.data.layouts.Add(roomLayout3);
			LayoutManager.data.activeLayouts = new string[3];
			RoomLayout roomLayout4 = new RoomLayout();
			roomLayout4.name = Localization.getPhrase("PIT_LAYOUT", string.Empty);
			roomLayout4.isPit = true;
			LayoutManager.data.layouts.Add(roomLayout4);
			LayoutManager.data.activeLayouts = new string[4];
			LayoutManager.data.activeLayouts[0] = Localization.getPhrase("BLUE_LAYOUT", string.Empty);
			LayoutManager.data.activeLayouts[1] = Localization.getPhrase("WHITE_LAYOUT", string.Empty);
			LayoutManager.data.activeLayouts[2] = Localization.getPhrase("ORANGE_LAYOUT", string.Empty);
			LayoutManager.data.activeLayouts[3] = Localization.getPhrase("PIT_LAYOUT", string.Empty);
			LayoutManager.saveLayoutData();
			LayoutManager.applyLayouts();
		}
	}

	public static int determineSpecimenCapacity()
	{
		if (LayoutManager.needSpecimenCapacityRecalculation)
		{
			LayoutManager.specimenCapacity = 6;
			for (int i = 0; i < 4; i++)
			{
				RoomLayout layoutByName = LayoutManager.getLayoutByName(LayoutManager.data.activeLayouts[i]);
				for (int j = 0; j < layoutByName.items.Count; j++)
				{
					string assetName = layoutByName.items[j].assetName;
					if (assetName != null && assetName == "SpecimenCoolerSmall")
					{
						LayoutManager.specimenCapacity += 21;
					}
				}
			}
			LayoutManager.needSpecimenCapacityRecalculation = false;
		}
		return LayoutManager.specimenCapacity;
	}

	public static void deleteLayoutData(string id)
	{
		try
		{
			File.Delete(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackLayoutData");
		}
		catch
		{
		}
	}

	public static void saveLayoutData()
	{
		Game.saveDataToXML(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackLayoutData", typeof(LayoutData), LayoutManager.data);
	}
}
