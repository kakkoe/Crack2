using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Inventory
{
	public static InventoryData data;

	public static LabItemData itemData;

	public static int numberOfDecayTicksSinceLastInventorySave;

	public static float timeSinceLastInventorySave;

	public static void init()
	{
		Inventory.loadLabItems();
	}

	public static float getCharVar(string label)
	{
		for (int i = 0; i < Inventory.data.charVars.Count; i++)
		{
			if (Inventory.data.charVars[i].label == label)
			{
				return Inventory.data.charVars[i].val;
			}
		}
		return 0f;
	}

	public static void setCharVar(string label, float val)
	{
		bool flag = false;
		for (int i = 0; i < Inventory.data.charVars.Count; i++)
		{
			if (Inventory.data.charVars[i].label == label)
			{
				flag = true;
				Inventory.data.charVars[i].val = val;
				Inventory.saveInventoryData();
			}
		}
		if (!flag)
		{
			Inventory.data.charVars.Add(new UserVar());
			Inventory.data.charVars[Inventory.data.charVars.Count - 1].label = label;
			Inventory.data.charVars[Inventory.data.charVars.Count - 1].val = val;
			Inventory.saveInventoryData();
		}
	}

	public static CharacterData getNPCData(string handle)
	{
		CharacterData result = null;
		for (int i = 0; i < Inventory.data.NPCs.Count; i++)
		{
			if (Inventory.data.NPCs[i].handle == handle)
			{
				result = CharacterManager.importCharacterData(Inventory.data.NPCs[i].filename, false);
			}
		}
		return result;
	}

	public static void setNPCData(string handle, string filename)
	{
		bool flag = false;
		for (int i = 0; i < Inventory.data.NPCs.Count; i++)
		{
			if (Inventory.data.NPCs[i].handle == handle)
			{
				Inventory.data.NPCs[i].filename = filename;
				flag = false;
			}
		}
		if (!flag)
		{
			Inventory.data.NPCs.Add(new NPCAssignment());
			Inventory.data.NPCs[Inventory.data.NPCs.Count - 1].handle = handle;
			Inventory.data.NPCs[Inventory.data.NPCs.Count - 1].filename = filename;
		}
		Inventory.saveInventoryData();
	}

	public static LabItemDefinition getItemDefinition(string itemName)
	{
		for (int i = 0; i < Inventory.itemData.items.Count; i++)
		{
			if (Inventory.itemData.items[i].assetName == itemName)
			{
				return Inventory.itemData.items[i];
			}
		}
		return null;
	}

	public static bool weHaveItem(string itemID, out LayoutItemSpecialProperties props, string originalBag = "", bool removeOriginal = false)
	{
		bool flag = false;
		props = null;
		for (int i = 0; i < Inventory.data.bags.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (Inventory.data.bags[i].name == originalBag || (originalBag == string.Empty && Inventory.data.bags[i].name.IndexOf("TESTING_ROOM_") == -1))
			{
				int num = 0;
				while (num < Inventory.data.bags[i].contents.Count)
				{
					if (!(Inventory.data.bags[i].contents[num].itemType == itemID))
					{
						num++;
						continue;
					}
					flag = true;
					props = Inventory.data.bags[i].contents[num].properties;
					if (!removeOriginal)
					{
						break;
					}
					Inventory.data.bags[i].contents.RemoveAt(num);
					break;
				}
			}
		}
		return flag;
	}

	public static bool moveItemToDifferentBag(string itemID, string originalBag, string destinationBag)
	{
		LayoutItemSpecialProperties properties = null;
		if (!Inventory.weHaveItem(itemID, out properties, originalBag, true))
		{
			return false;
		}
		return Inventory.giveItem(itemID, properties, destinationBag, false, false, 0);
	}

	public static bool moveItemToDifferentBagByIndex(int c, string originalBag, string destinationBag)
	{
		bool flag = false;
		string itemID = string.Empty;
		LayoutItemSpecialProperties properties = null;
		int num = 0;
		while (num < Inventory.data.bags.Count && !flag)
		{
			if (!(Inventory.data.bags[num].name == originalBag) && (!(originalBag == string.Empty) || Inventory.data.bags[num].name.IndexOf("TESTING_ROOM_") != -1))
			{
				num++;
				continue;
			}
			flag = true;
			itemID = Inventory.data.bags[num].contents[c].itemType;
			properties = Inventory.data.bags[num].contents[c].properties;
			Inventory.data.bags[num].contents.RemoveAt(c);
			break;
		}
		if (!flag)
		{
			return false;
		}
		return Inventory.giveItem(itemID, properties, destinationBag, false, false, 0);
	}

	public static bool giveItem(string itemID, LayoutItemSpecialProperties properties = null, string bagName = "", bool newItem = false, bool andSave = true, int unlimitedExpandAttempts = 0)
	{
		if (properties == null)
		{
			properties = new LayoutItemSpecialProperties();
		}
		bool flag = false;
		LabItemDefinition item = new LabItemDefinition();
		int num = 0;
		for (int i = 0; i < Inventory.itemData.items.Count; i++)
		{
			if (Inventory.itemData.items[i].assetName == itemID)
			{
				flag = true;
				num = Inventory.itemData.items[i].bagData.scale;
				item = Inventory.itemData.items[i];
			}
		}
		if (!flag)
		{
			return false;
		}
		int num2 = -1;
		int posX = -1;
		int posY = -1;
		if (bagName != string.Empty)
		{
			for (int j = 0; j < Inventory.data.bags.Count; j++)
			{
				bool flag2 = false;
				if (Inventory.data.bags[j].name == bagName && (Inventory.data.bags[j].scale == num || Inventory.data.bags[j].scale == -1))
				{
					for (int k = 0; k < Inventory.data.bags[j].sizeY; k++)
					{
						for (int l = 0; l < Inventory.data.bags[j].sizeX; l++)
						{
							if (Inventory.data.bags[j].eligiblePlacement(item, l, k))
							{
								flag2 = true;
								posX = l;
								posY = k;
								l = Inventory.data.bags[j].sizeX;
								k = Inventory.data.bags[j].sizeY;
							}
						}
					}
					if (flag2)
					{
						num2 = j;
						j = Inventory.data.bags.Count;
						break;
					}
					if (Inventory.data.bags[j].unlimited)
					{
						if (unlimitedExpandAttempts < 10)
						{
							Inventory.data.bags[j].sizeY++;
							return Inventory.giveItem(itemID, properties, bagName, newItem, andSave, unlimitedExpandAttempts + 1);
						}
						return false;
					}
				}
			}
			if (num2 != -1)
			{
				goto IL_0254;
			}
		}
		goto IL_0254;
		IL_0254:
		if (num2 == -1)
		{
			for (int m = 0; m < Inventory.data.bags.Count; m++)
			{
				if ((Inventory.data.bags[m].scale == num || Inventory.data.bags[m].scale == -1) && Inventory.data.bags[m].name != "CLOTHING" && Inventory.data.bags[m].name != "COMPONENTS" && Inventory.data.bags[m].name != "COMPONENTS_CONFIRMED")
				{
					bool flag3 = false;
					for (int n = 0; n < Inventory.data.bags[m].sizeY; n++)
					{
						for (int num3 = 0; num3 < Inventory.data.bags[m].sizeX; num3++)
						{
							if (Inventory.data.bags[m].eligiblePlacement(item, num3, n))
							{
								flag3 = true;
								posX = num3;
								posY = n;
								num3 = Inventory.data.bags[m].sizeX;
								n = Inventory.data.bags[m].sizeY;
							}
						}
					}
					if (flag3)
					{
						num2 = m;
						m = Inventory.data.bags.Count;
						break;
					}
				}
			}
		}
		if (num2 != -1)
		{
			Inventory.putItemInBag(itemID, properties, Inventory.data.bags[num2], posX, posY, newItem, andSave);
			return true;
		}
		return false;
	}

	public static void putItemInBag(string item, LayoutItemSpecialProperties properties, Bag bag, int posX, int posY, bool newItem = false, bool andSave = true)
	{
		BagContent bagContent = new BagContent();
		bagContent.itemType = item;
		bagContent.x = posX;
		bagContent.y = posY;
		bagContent.newItem = newItem;
		bagContent.properties = properties;
		bag.contents.Add(bagContent);
		Game.gameInstance.needInventoryRefresh = true;
		bag.update();
		if (andSave)
		{
			Inventory.saveInventoryData();
		}
		if (newItem)
		{
			Game.gameInstance.addToast(0, Game.getUIcolorByName(Inventory.getItemDefinition(item).bagData.color), Inventory.getItemDefinition(item).displayName, bag.name);
		}
	}

	public static Bag getBagByName(string n)
	{
		for (int i = 0; i < Inventory.data.bags.Count; i++)
		{
			if (Inventory.data.bags[i].name == n)
			{
				return Inventory.data.bags[i];
			}
		}
		return null;
	}

	public static void updateResearchTasks()
	{
		bool flag = false;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < ResearchList.allTasksAvailable.Count; i++)
		{
			bool flag2 = false;
			if (Inventory.data.completedResearch.Contains(ResearchList.allTasksAvailable[i].id))
			{
				flag2 = true;
				int num3 = 0;
				while (num3 < Inventory.data.researchTasks.Count)
				{
					if (!(Inventory.data.researchTasks[num3].id == ResearchList.allTasksAvailable[i].id))
					{
						num3++;
						continue;
					}
					Inventory.data.researchTasks.RemoveAt(num3);
					flag = true;
					break;
				}
			}
			else
			{
				int num4 = 0;
				while (num4 < Inventory.data.researchTasks.Count)
				{
					if (!(Inventory.data.researchTasks[num4].id == ResearchList.allTasksAvailable[i].id))
					{
						num4++;
						continue;
					}
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				bool flag3 = true;
				if (ResearchList.allTasksAvailable[i].fetish != string.Empty && UserSettings.getFetishSetting(ResearchList.allTasksAvailable[i].fetish) <= 0f)
				{
					flag3 = false;
				}
				if (flag3 && ResearchList.allTasksAvailable[i].prerequisite != string.Empty)
				{
					string[] array = ResearchList.allTasksAvailable[i].prerequisite.Split(',');
					for (int j = 0; j < array.Length; j++)
					{
						if (!Inventory.data.completedResearch.Contains(array[j]))
						{
							bool flag4 = false;
							int num5 = 0;
							while (num5 < Inventory.data.researchTasks.Count)
							{
								if (!(Inventory.data.researchTasks[num5].id == array[j]))
								{
									num5++;
									continue;
								}
								flag4 = true;
								if (!Inventory.data.researchTasks[num5].completed && ResearchList.allTasksAvailable[i].id == "VibratingCockRing")
								{
									Debug.Log("Incomplete: " + array[j]);
								}
								flag3 = (flag3 && Inventory.data.researchTasks[num5].completed);
								break;
							}
							if (!flag4 && ResearchList.allTasksAvailable[i].id == "VibratingCockRing")
							{
								Debug.Log("Couldn't find prereq " + array[j]);
							}
							flag3 = (flag3 && flag4);
						}
					}
					if (!flag3)
					{
						num2++;
					}
				}
				if (flag3)
				{
					ResearchTask researchTask = ResearchList.createResearchTaskFromDefinition(ResearchList.allTasksAvailable[i].id, ResearchList.allTasksAvailable[i].difficulty, ResearchList.allTasksAvailable[i].fetish, ResearchList.allTasksAvailable[i].category, -1, -2);
					researchTask.globePositionX = UnityEngine.Random.value * 360f;
					researchTask.globePositionY = -50f + UnityEngine.Random.value * 100f;
					int num6 = 0;
					while (!Inventory.validTaskPosition(researchTask) && num6 < 500)
					{
						num6++;
						researchTask.globePositionX = UnityEngine.Random.value * 360f;
						researchTask.globePositionY = -50f + UnityEngine.Random.value * 100f;
					}
					Inventory.data.researchTasks.Add(researchTask);
					flag = true;
				}
			}
		}
		for (int k = 0; k < Inventory.data.completedResearch.Count; k++)
		{
			num++;
			num2++;
		}
		for (int l = 0; l < Inventory.data.researchTasks.Count; l++)
		{
			if (Inventory.data.researchTasks[l].completed)
			{
				if (Inventory.data.researchTasks[l].category == "REPEATABLE_RESEARCH")
				{
					Inventory.data.researchTasks.RemoveAt(l);
					l--;
				}
				else
				{
					if (!Inventory.data.completedResearch.Contains(Inventory.data.researchTasks[l].id))
					{
						Inventory.data.completedResearch.Add(Inventory.data.researchTasks[l].id);
						flag = true;
					}
					num++;
					num2++;
				}
			}
			else
			{
				num2++;
			}
		}
		bool flag5 = false;
		bool[] array2 = new bool[6];
		for (int m = 0; m < Inventory.data.researchTasks.Count; m++)
		{
			if (Inventory.data.researchTasks[m].id == "PaidResearch")
			{
				flag5 = true;
			}
			if (Inventory.data.researchTasks[m].id == "ChemicalConversion")
			{
				array2[Inventory.data.researchTasks[m].type] = true;
			}
		}
		Inventory.data.researchCompletion = (float)num / (float)num2;
		if (!flag5)
		{
			ResearchTask researchTask2 = ResearchList.createResearchTaskFromDefinition("PaidResearch", UnityEngine.Random.value, string.Empty, "REPEATABLE_RESEARCH", -1, -2);
			researchTask2.globePositionX = UnityEngine.Random.value * 360f;
			researchTask2.globePositionY = -50f + UnityEngine.Random.value * 100f;
			int num7 = 0;
			while (!Inventory.validTaskPosition(researchTask2) && num7 < 500)
			{
				num7++;
				researchTask2.globePositionX = UnityEngine.Random.value * 360f;
				researchTask2.globePositionY = -50f + UnityEngine.Random.value * 100f;
			}
			Inventory.data.researchTasks.Add(researchTask2);
			flag = true;
		}
		for (int n = 0; n < 6; n++)
		{
			if (!array2[n])
			{
				ResearchTask researchTask3 = ResearchList.createResearchTaskFromDefinition("ChemicalConversion", UnityEngine.Random.value, string.Empty, "REPEATABLE_RESEARCH", n, n);
				researchTask3.globePositionX = UnityEngine.Random.value * 360f;
				researchTask3.globePositionY = -50f + UnityEngine.Random.value * 100f;
				int num8 = 0;
				while (!Inventory.validTaskPosition(researchTask3) && num8 < 500)
				{
					num8++;
					researchTask3.globePositionX = UnityEngine.Random.value * 360f;
					researchTask3.globePositionY = -50f + UnityEngine.Random.value * 100f;
				}
				Inventory.data.researchTasks.Add(researchTask3);
				flag = true;
			}
		}
		if (flag)
		{
			Inventory.saveInventoryData();
		}
	}

	public static bool validTaskPosition(ResearchTask newTask)
	{
		for (int i = 0; i < Inventory.data.researchTasks.Count; i++)
		{
			if (!Inventory.data.researchTasks[i].completed)
			{
				float num = 20f;
				if (Mathf.Abs(Game.degreeDist(newTask.globePositionX, Inventory.data.researchTasks[i].globePositionX) + Mathf.Abs(Game.degreeDist(newTask.globePositionY, Inventory.data.researchTasks[i].globePositionY))) < num)
				{
					return false;
				}
			}
		}
		return true;
	}

	public static void emptyBag(string bagName, string into = "")
	{
		List<BagContent> contents = Inventory.getBagByName(bagName).contents;
		List<string> list = new List<string>();
		for (int i = 0; i < contents.Count; i++)
		{
			list.Add(contents[i].itemType);
		}
		for (int j = 0; j < list.Count; j++)
		{
			Inventory.moveItemToDifferentBag(list[j], bagName, into);
		}
	}

	public static void deleteEverythingInBag(string bagName)
	{
		Inventory.getBagByName(bagName).contents = new List<BagContent>();
		Inventory.saveInventoryData();
	}

	public static void loadInventoryData()
	{
		if (File.Exists(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackInventoryData"))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryData));
			FileStream fileStream = new FileStream(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackInventoryData", FileMode.Open);
			Inventory.data = (xmlSerializer.Deserialize(fileStream) as InventoryData);
			fileStream.Close();
		}
		else
		{
			Inventory.data = new InventoryData();
		}
		if (Inventory.data.politeName == string.Empty)
		{
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			Inventory.data.politeName = "Dr. " + textInfo.ToTitleCase(UserSettings.data.activeUser);
		}
		if (Inventory.data.domName == string.Empty)
		{
			Inventory.data.domName = Localization.getPhrase("DefaultDomTitle_male", string.Empty);
		}
		if (Inventory.data.characterName == string.Empty)
		{
			TextInfo textInfo2 = new CultureInfo("en-US", false).TextInfo;
			Inventory.data.characterName = textInfo2.ToTitleCase(UserSettings.data.activeUser);
		}
		while (Inventory.data.hotkeyItems.Count < 21)
		{
			Inventory.data.hotkeyItems.Add(string.Empty);
		}
		if (Inventory.data.specimen == null)
		{
			Inventory.data.specimen = new float[6];
		}
		if (Inventory.data.specimen.Length < 6)
		{
			Inventory.data.specimen = new float[6];
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		bool flag7 = false;
		bool flag8 = false;
		bool flag9 = false;
		bool flag10 = false;
		bool flag11 = false;
		bool flag12 = false;
		bool flag13 = false;
		for (int i = 0; i < Inventory.data.bags.Count; i++)
		{
			if (Inventory.data.bags[i].name == "WALLET")
			{
				flag = true;
			}
			if (Inventory.data.bags[i].name == "CARRYING")
			{
				flag2 = true;
			}
			if (Inventory.data.bags[i].name == "LOCKER_SHELF")
			{
				flag3 = true;
			}
			if (Inventory.data.bags[i].name == "LOCKER")
			{
				flag4 = true;
			}
			if (Inventory.data.bags[i].name == "STORAGE")
			{
				flag5 = true;
			}
			if (Inventory.data.bags[i].name == "GARAGE")
			{
				flag6 = true;
			}
			if (Inventory.data.bags[i].name == "CLOTHING")
			{
				flag7 = true;
			}
			if (Inventory.data.bags[i].name == "TESTING_ROOM_1")
			{
				flag8 = true;
			}
			if (Inventory.data.bags[i].name == "TESTING_ROOM_2")
			{
				flag9 = true;
			}
			if (Inventory.data.bags[i].name == "TESTING_ROOM_3")
			{
				flag10 = true;
			}
			if (Inventory.data.bags[i].name == "TESTING_ROOM_4")
			{
				flag11 = true;
			}
			if (Inventory.data.bags[i].name == "COMPONENTS")
			{
				flag12 = true;
			}
			if (Inventory.data.bags[i].name == "COMPONENTS_CONFIRMED")
			{
				flag13 = true;
			}
		}
		if (!flag7)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "CLOTHING";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
			BagContent bagContent = new BagContent();
			bagContent.itemType = "RackChip";
			bagContent.x = 0;
			bagContent.y = 0;
			bagContent.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent);
		}
		if (!flag)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "WALLET";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 0;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 3;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 2;
			Inventory.data.bags[Inventory.data.bags.Count - 1].onPerson = true;
		}
		if (!flag2)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "CARRYING";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].onPerson = true;
		}
		if (!flag3)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "LOCKER_SHELF";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 0;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 9;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag4)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "LOCKER";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag5)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "STORAGE";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag6)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "GARAGE";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = 2;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 5;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 3;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
			BagContent bagContent2 = new BagContent();
			bagContent2.itemType = "RackChair";
			bagContent2.x = 0;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "LightingControlPanel";
			bagContent2.x = 2;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "CeilingLight";
			bagContent2.x = 0;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "CeilingLight";
			bagContent2.x = 0;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "CeilingLight";
			bagContent2.x = 0;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "ColoredSpotlight";
			bagContent2.x = 1;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "ColoredSpotlight";
			bagContent2.x = 2;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "CornerDesk";
			bagContent2.x = 3;
			bagContent2.y = 0;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "OfficeChair";
			bagContent2.x = 0;
			bagContent2.y = 1;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "WallShelf";
			bagContent2.x = 0;
			bagContent2.y = 2;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "WallShelf";
			bagContent2.x = 2;
			bagContent2.y = 2;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
			bagContent2 = new BagContent();
			bagContent2.itemType = "Shelf";
			bagContent2.x = 3;
			bagContent2.y = 2;
			bagContent2.newItem = false;
			Inventory.data.bags[Inventory.data.bags.Count - 1].contents.Add(bagContent2);
		}
		if (!flag8)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "TESTING_ROOM_1";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag9)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "TESTING_ROOM_2";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag10)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "TESTING_ROOM_3";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag11)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "TESTING_ROOM_4";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag12)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "COMPONENTS";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		if (!flag13)
		{
			Inventory.data.bags.Add(new Bag());
			Inventory.data.bags[Inventory.data.bags.Count - 1].name = "COMPONENTS_CONFIRMED";
			Inventory.data.bags[Inventory.data.bags.Count - 1].scale = -1;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeX = 6;
			Inventory.data.bags[Inventory.data.bags.Count - 1].sizeY = 4;
			Inventory.data.bags[Inventory.data.bags.Count - 1].unlimited = true;
		}
		for (int j = 0; j < Inventory.data.bags.Count; j++)
		{
			if (Inventory.data.bags[j].uid == string.Empty)
			{
				Inventory.data.bags[j].uid = Guid.NewGuid().ToString();
			}
		}
		if (Inventory.data.draggingItem != string.Empty)
		{
			Inventory.data.draggingItem = string.Empty;
		}
		NPC.tourProgress = Mathf.RoundToInt(Inventory.getCharVar("tourPhase"));
		Inventory.saveInventoryData();
	}

	public static void addMoney(int money)
	{
		Inventory.data.money += money;
		if (Inventory.data.money < 0)
		{
			Inventory.data.money = 0;
		}
		if (Inventory.data.money > 1000000000)
		{
			Inventory.data.money = 1000000000;
		}
	}

	public static void addSpecimen(float specimenR, float specimenO, float specimenY, float specimenG, float specimenB, float specimenP)
	{
		Inventory.addSpecimenByType(0, specimenR, false);
		Inventory.addSpecimenByType(1, specimenO, false);
		Inventory.addSpecimenByType(2, specimenY, false);
		Inventory.addSpecimenByType(3, specimenG, false);
		Inventory.addSpecimenByType(4, specimenB, false);
		Inventory.addSpecimenByType(5, specimenP, false);
	}

	public static void addSpecimenByType(int type, float amount, bool andSave = false)
	{
		Inventory.data.specimen[type] += amount;
		Inventory.data.totalSpecimen = 0f;
		for (int i = 0; i < 6; i++)
		{
			if (Inventory.data.specimen[i] < 0f)
			{
				Inventory.data.specimen[i] = 0f;
			}
			Inventory.data.totalSpecimen += Inventory.data.specimen[i];
		}
		float num = (float)LayoutManager.determineSpecimenCapacity();
		if (Inventory.data.totalSpecimen > num)
		{
			float num2 = num / Inventory.data.totalSpecimen;
			Inventory.data.totalSpecimen = num;
			for (int j = 0; j < 6; j++)
			{
				Inventory.data.specimen[j] *= num2;
			}
			Game.gameInstance.timeSinceWastedSpecimen = 0f;
		}
		if (andSave)
		{
			Inventory.saveInventoryData();
		}
	}

	public static float getChemicalCompound(string name)
	{
		int num = -1;
		int num2 = 0;
		while (num2 < Inventory.data.chemicalcompounds.Count)
		{
			if (!(Inventory.data.chemicalcompounds[num2].name == name))
			{
				num2++;
				continue;
			}
			num = num2;
			break;
		}
		if (num == -1)
		{
			ChemicalCompound chemicalCompound = new ChemicalCompound();
			chemicalCompound.name = name;
			chemicalCompound.amountOwned = 0f;
			Inventory.data.chemicalcompounds.Add(chemicalCompound);
			num = Inventory.data.chemicalcompounds.Count - 1;
		}
		return Inventory.data.chemicalcompounds[num].amountOwned;
	}

	public static void addChemicalCompound(string name, float amount, bool saveImmediately = false)
	{
		int num = -1;
		int num2 = 0;
		while (num2 < Inventory.data.chemicalcompounds.Count)
		{
			if (!(Inventory.data.chemicalcompounds[num2].name == name))
			{
				num2++;
				continue;
			}
			num = num2;
			break;
		}
		if (num == -1)
		{
			ChemicalCompound chemicalCompound = new ChemicalCompound();
			chemicalCompound.name = name;
			chemicalCompound.amountOwned = 0f;
			Inventory.data.chemicalcompounds.Add(chemicalCompound);
			num = Inventory.data.chemicalcompounds.Count - 1;
		}
		Inventory.data.chemicalcompounds[num].amountOwned += amount;
		if (Inventory.data.chemicalcompounds[num].amountOwned < 0f)
		{
			Inventory.data.chemicalcompounds[num].amountOwned = 0f;
		}
		Game.gameInstance.updateChemicalUI();
		if (saveImmediately)
		{
			Inventory.saveInventoryData();
		}
	}

	public static void addChemical(int chemical, int amount)
	{
		Inventory.data.chemicals[chemical] += (float)amount;
		if (Inventory.data.chemicals[chemical] < 0f)
		{
			Inventory.data.chemicals[chemical] = 0f;
		}
		Inventory.saveInventoryData();
	}

	public static void process()
	{
		if (Inventory.data != null)
		{
			return;
		}
	}

	public static void deleteInventoryData(string id)
	{
		try
		{
			File.Delete(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackInventoryData");
		}
		catch
		{
		}
	}

	public static void saveInventoryData()
	{
		if (Game.gameInstance.draggingInventoryItemOriginalBag != "CLOTHING")
		{
			Inventory.data.draggingItem = Game.gameInstance.draggingInventoryItem;
		}
		else
		{
			Inventory.data.draggingItem = string.Empty;
		}
		Inventory.numberOfDecayTicksSinceLastInventorySave = 0;
		Inventory.data.totalSpecimen = 0f;
		if (Inventory.data.specimen.Length < 6)
		{
			Inventory.data.specimen = new float[6];
		}
		for (int i = 0; i < 6; i++)
		{
			Inventory.data.totalSpecimen += Inventory.data.specimen[i];
		}
		ToyMaterials.update();
		Game.saveDataToXML(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackInventoryData", typeof(InventoryData), Inventory.data);
		Inventory.timeSinceLastInventorySave = 0f;
	}

	public static void loadLabItems()
	{
		TextAsset textAsset = Resources.Load("LabItems") as TextAsset;
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(LabItemData));
		Inventory.itemData = (xmlSerializer.Deserialize(new StringReader(textAsset.text)) as LabItemData);
	}
}
