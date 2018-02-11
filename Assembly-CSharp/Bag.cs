using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("Bag")]
public class Bag
{
	[XmlElement("name")]
	public string name;

	[XmlElement("uid")]
	public string uid = string.Empty;

	[XmlElement("onPerson")]
	public bool onPerson;

	[XmlElement("scale")]
	public int scale;

	[XmlElement("sizeX")]
	public int sizeX;

	[XmlElement("sizeY")]
	public int sizeY;

	[XmlElement("unlimited")]
	public bool unlimited;

	[XmlArray("contents")]
	[XmlArrayItem("bagContent")]
	public List<BagContent> contents = new List<BagContent>();

	public bool waitingForDragToUpdate;

	public bool eligiblePlacement(LabItemDefinition item, int posX, int posY)
	{
		if (item == null)
		{
			return false;
		}
		if (item.bagData == null)
		{
			return false;
		}
		if (item.bagData.scale != this.scale && item.bagData.scale != -1 && this.scale != -1)
		{
			return false;
		}
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < this.contents.Count; i++)
		{
			list = list.Concat(Bag.tilesPossessedByItem(Inventory.getItemDefinition(this.contents[i].itemType), this.contents[i].x, this.contents[i].y)).ToList();
		}
		List<Vector2> list2 = Bag.tilesPossessedByItem(item, posX, posY);
		for (int j = 0; j < list2.Count; j++)
		{
			Vector2 vector = list2[j];
			if (vector.x < 0f)
			{
				return false;
			}
			Vector2 vector2 = list2[j];
			if (vector2.y < 0f)
			{
				return false;
			}
			Vector2 vector3 = list2[j];
			if (vector3.x >= (float)this.sizeX)
			{
				return false;
			}
			Vector2 vector4 = list2[j];
			if (vector4.y >= (float)this.sizeY)
			{
				return false;
			}
			for (int k = 0; k < list.Count; k++)
			{
				Vector2 vector5 = list2[j];
				float x = vector5.x;
				Vector2 vector6 = list[k];
				if (x == vector6.x)
				{
					Vector2 vector7 = list2[j];
					float y = vector7.y;
					Vector2 vector8 = list[k];
					if (y == vector8.y)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public static List<Vector2> tilesPossessedByItem(LabItemDefinition item, int posX, int posY)
	{
		List<Vector2> list = new List<Vector2>();
		if (item == null)
		{
			return list;
		}
		string[] array = item.bagData.slots.Split(' ');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(',');
			int num = 0;
			if (int.Parse(array2[1]) % 2 == 1 && posY % 2 == 1)
			{
				num = 1;
			}
			list.Add(new Vector2((float)(posX + int.Parse(array2[0]) + num), (float)(posY + int.Parse(array2[1]))));
		}
		return list;
	}

	public void update()
	{
		if (Game.gameInstance.draggingInventoryItem != string.Empty)
		{
			this.waitingForDragToUpdate = true;
		}
		else
		{
			this.waitingForDragToUpdate = false;
			if (this.unlimited)
			{
				int num = 0;
				List<Vector2> list = new List<Vector2>();
				for (int i = 0; i < this.contents.Count; i++)
				{
					list = list.Concat(Bag.tilesPossessedByItem(Inventory.getItemDefinition(this.contents[i].itemType), this.contents[i].x, this.contents[i].y)).ToList();
				}
				for (int j = 0; j < list.Count; j++)
				{
					Vector2 vector = list[j];
					if (vector.y > (float)num)
					{
						Vector2 vector2 = list[j];
						num = Mathf.RoundToInt(vector2.y);
					}
				}
				if (this.sizeY != num + 4)
				{
					this.sizeY = num + 4;
					Inventory.saveInventoryData();
					for (int k = 0; k < Game.gameInstance.displayedBags.Count; k++)
					{
						Object.Destroy(Game.gameInstance.displayedBags[k]);
					}
					Game.gameInstance.displayedBags = new List<GameObject>();
					Game.gameInstance.needInventoryRefresh = true;
				}
			}
			if (this.name == "CLOTHING")
			{
				Game.gameInstance.needInventoryRefresh = true;
				Game.gameInstance.PC().updateClothingBasedOnInventory();
			}
		}
	}
}
