using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
	public string bagUID = string.Empty;

	public Bag bag;

	public List<GameObject> tiles = new List<GameObject>();

	public static List<string> tileColors = new List<string>();

	public int delayedFirstUpdate = 2;

	public static Color normalTile;

	public static Color fadedTile;

	public static Color invisibleTile;

	public const int linesPerContentItem = 4;

	public int clothingIconsLoading;

	public List<GameObject> indexLines = new List<GameObject>();

	public List<GameObject> indexTexts = new List<GameObject>();

	public int hoveringOverContentID = -1;

	public static List<GameObject> linePool = new List<GameObject>();

	public static Texture2D blankWhiteTex;

	private void Start()
	{
		if (BagUI.tileColors.Count == 0)
		{
			BagUI.tileColors.Add("blue");
			BagUI.tileColors.Add("green");
			BagUI.tileColors.Add("grey");
			BagUI.tileColors.Add("orange");
			BagUI.tileColors.Add("pink");
			BagUI.tileColors.Add("purple");
			BagUI.tileColors.Add("red");
			BagUI.tileColors.Add("white");
			BagUI.tileColors.Add("yellow");
		}
		this.delayedFirstUpdate = 2;
		BagUI.normalTile = Color.white;
		BagUI.normalTile.a = 0.35f;
		BagUI.fadedTile = Color.white;
		BagUI.fadedTile.a = 0.1f;
		BagUI.invisibleTile = Color.white;
		BagUI.invisibleTile.a = 0f;
	}

	private void Update()
	{
		if (this.delayedFirstUpdate > 0)
		{
			this.delayedFirstUpdate--;
			if (this.delayedFirstUpdate <= 0)
			{
				this.updateBag();
			}
		}
		for (int i = 0; i < this.indexTexts.Count; i++)
		{
			Color color = this.indexTexts[i].GetComponent<Text>().color;
			if (this.hoveringOverContentID == i || this.hoveringOverContentID == -1)
			{
				color.a = 1f;
			}
			else
			{
				color.a = 0.2f;
			}
			this.indexTexts[i].GetComponent<Text>().color = color;
			for (int j = 0; j < 4; j++)
			{
				color = this.indexLines[i * 4 + j].GetComponent<RawImage>().color;
				if (this.hoveringOverContentID == i)
				{
					color.a = 1f;
				}
				else
				{
					color.a = 0.2f;
				}
				this.indexLines[i * 4 + j].GetComponent<RawImage>().color = color;
			}
		}
	}

	public void updateBag()
	{
		if (this.bag != null && this.bag.contents != null)
		{
			this.bag.contents = (from content in this.bag.contents
			orderby content.y, content.x
			select content).ToList();
			for (int i = 0; i < this.tiles.Count; i++)
			{
				string b = string.Empty;
				string text = string.Empty;
				this.tiles[i].GetComponent<BagTile>().bagContentID = -1;
				this.tiles[i].GetComponent<BagTile>().bag = this.bag;
				this.tiles[i].GetComponent<BagTile>().container = this;
				this.tiles[i].transform.Find("txtNew").gameObject.SetActive(false);
				for (int j = 0; j < this.bag.contents.Count; j++)
				{
					List<Vector2> list = Bag.tilesPossessedByItem(Inventory.getItemDefinition(this.bag.contents[j].itemType), this.bag.contents[j].x, this.bag.contents[j].y);
					for (int k = 0; k < list.Count; k++)
					{
						Vector2 vector = list[k];
						float x = vector.x;
						Vector2 vector2 = list[k];
						int num = Mathf.RoundToInt(x + vector2.y * (float)this.bag.sizeX);
						if (num == i)
						{
							b = Inventory.getItemDefinition(this.bag.contents[j].itemType).bagData.color;
							this.tiles[i].GetComponent<BagTile>().bagContentID = j;
							for (int l = 0; l < 6; l++)
							{
								this.tiles[i].GetComponent<BagTile>().connections[l] = false;
								this.tiles[i].transform.Find("txtNew").gameObject.SetActive(k == 0 && this.bag.contents[j].newItem);
							}
						}
					}
				}
				if (this.bag.uid == Game.gameInstance.inventoryDragHoverBagUID && this.bag.eligiblePlacement(Inventory.getItemDefinition(Game.gameInstance.draggingInventoryItem), Game.gameInstance.inventoryDragHoverBagX, Game.gameInstance.inventoryDragHoverBagY))
				{
					List<Vector2> list2 = Bag.tilesPossessedByItem(Inventory.getItemDefinition(Game.gameInstance.draggingInventoryItem), Game.gameInstance.inventoryDragHoverBagX, Game.gameInstance.inventoryDragHoverBagY);
					for (int m = 0; m < list2.Count; m++)
					{
						Vector2 vector3 = list2[m];
						float x2 = vector3.x;
						Vector2 vector4 = list2[m];
						int num2 = Mathf.RoundToInt(x2 + vector4.y * (float)this.bag.sizeX);
						if (num2 == i)
						{
							text = Inventory.getItemDefinition(Game.gameInstance.draggingInventoryItem).bagData.color;
						}
					}
				}
				for (int n = 0; n < BagUI.tileColors.Count; n++)
				{
					this.tiles[i].transform.Find("hex_" + BagUI.tileColors[n]).gameObject.SetActive(BagUI.tileColors[n] == b);
				}
				this.tiles[i].transform.Find("border").gameObject.SetActive(text != string.Empty);
				if (text != string.Empty)
				{
					((Component)this.tiles[i].transform.Find("border")).GetComponent<Image>().color = Game.getUIcolorByName(text);
				}
				if (this.bag.unlimited && i >= this.tiles.Count - this.bag.sizeX * 3)
				{
					if (Game.gameInstance.draggingInventoryItem == string.Empty)
					{
						this.tiles[i].GetComponent<Image>().color = BagUI.invisibleTile;
					}
					else
					{
						this.tiles[i].GetComponent<Image>().color = BagUI.fadedTile;
					}
				}
				else
				{
					this.tiles[i].GetComponent<Image>().color = BagUI.normalTile;
				}
			}
			for (int num3 = 0; num3 < this.tiles.Count; num3++)
			{
				for (int num4 = 0; num4 < num3; num4++)
				{
					if (this.tiles[num3].GetComponent<BagTile>().bagContentID == this.tiles[num4].GetComponent<BagTile>().bagContentID && this.tiles[num3].GetComponent<BagTile>().bagContentID != -1)
					{
						if (this.tiles[num4].GetComponent<BagTile>().xx == this.tiles[num3].GetComponent<BagTile>().xx - 1 && this.tiles[num4].GetComponent<BagTile>().yy == this.tiles[num3].GetComponent<BagTile>().yy)
						{
							this.tiles[num4].GetComponent<BagTile>().connections[0] = true;
							this.tiles[num3].GetComponent<BagTile>().connections[3] = true;
						}
						int num5 = 0;
						if (this.tiles[num4].GetComponent<BagTile>().yy % 2 == 1)
						{
							num5 = 1;
						}
						if (this.tiles[num4].GetComponent<BagTile>().xx + num5 == this.tiles[num3].GetComponent<BagTile>().xx && this.tiles[num4].GetComponent<BagTile>().yy + 1 == this.tiles[num3].GetComponent<BagTile>().yy)
						{
							this.tiles[num4].GetComponent<BagTile>().connections[1] = true;
							this.tiles[num3].GetComponent<BagTile>().connections[4] = true;
						}
						if (this.tiles[num4].GetComponent<BagTile>().xx + num5 == this.tiles[num3].GetComponent<BagTile>().xx + 1 && this.tiles[num4].GetComponent<BagTile>().yy + 1 == this.tiles[num3].GetComponent<BagTile>().yy)
						{
							this.tiles[num4].GetComponent<BagTile>().connections[2] = true;
							this.tiles[num3].GetComponent<BagTile>().connections[5] = true;
						}
					}
				}
			}
			for (int num6 = 0; num6 < this.tiles.Count; num6++)
			{
				for (int num7 = 0; num7 < 6; num7++)
				{
					this.tiles[num6].transform.Find("connector" + num7).gameObject.SetActive(this.tiles[num6].GetComponent<BagTile>().connections[num7] && this.tiles[num6].GetComponent<BagTile>().bagContentID != -1);
				}
			}
			((Component)base.transform.Find("inventoryLabels")).GetComponent<Text>().text = string.Empty;
			for (int num8 = 0; num8 < this.indexLines.Count; num8++)
			{
				BagUI.disposeLine(this.indexLines[num8]);
			}
			for (int num9 = 0; num9 < this.indexTexts.Count; num9++)
			{
				Object.Destroy(this.indexTexts[num9]);
			}
			this.indexLines = new List<GameObject>();
			this.indexTexts = new List<GameObject>();
			for (int num10 = 0; num10 < this.bag.contents.Count; num10++)
			{
				this.indexTexts.Add(Object.Instantiate(base.transform.Find("inventoryLabels").gameObject));
				this.indexTexts[this.indexTexts.Count - 1].transform.SetParent(base.transform.Find("inventoryLabels").parent);
				this.indexTexts[this.indexTexts.Count - 1].transform.localScale = Vector3.one;
				this.indexTexts[this.indexTexts.Count - 1].GetComponent<Text>().color = Game.getUIcolorByName(Inventory.getItemDefinition(this.bag.contents[num10].itemType).bagData.color);
				Vector3 localPosition = base.transform.Find("inventoryLabels").localPosition;
				localPosition.x -= 10f;
				localPosition.y -= 11f * (float)num10;
				this.indexTexts[this.indexTexts.Count - 1].transform.localPosition = localPosition;
				string text2 = Localization.getPhrase(Inventory.getItemDefinition(this.bag.contents[num10].itemType).displayName, string.Empty);
				if (this.bag.contents[num10].properties.material.Length > 0)
				{
					text2 = text2 + " (" + Game.unCamelCase(this.bag.contents[num10].properties.material) + ")";
				}
				this.indexTexts[this.indexTexts.Count - 1].GetComponent<Text>().text = ((Component)base.transform.Find("inventoryLabels")).GetComponent<Text>().text + text2 + "\r\n";
				localPosition.x = -51f;
				localPosition.y = 31f - 11f * (float)num10;
				localPosition.z = 0f;
				Vector3 localPosition2 = this.tiles[this.bag.contents[num10].x + this.bag.contents[num10].y * this.bag.sizeX].transform.localPosition;
				float x3 = localPosition2.x;
				float y = localPosition2.y;
				float f = localPosition2.y - localPosition.y;
				float num11 = Mathf.Abs(f);
				localPosition2.y = localPosition.y;
				localPosition2.x -= num11;
				if (localPosition2.x < localPosition.x)
				{
					localPosition2.x = x3;
					if (y > localPosition.y)
					{
						localPosition2.y = localPosition.y + (localPosition2.x - localPosition.x);
					}
					else
					{
						localPosition2.y = localPosition.y - (localPosition2.x - localPosition.x);
					}
				}
				Vector3 start = default(Vector3);
				start.x = localPosition2.x;
				start.y = localPosition2.y;
				start.z = localPosition2.z;
				Vector3 localPosition3 = this.tiles[this.bag.contents[num10].x + this.bag.contents[num10].y * this.bag.sizeX].transform.localPosition;
				this.indexLines.Add(BagUI.drawLine(base.transform, localPosition, localPosition2, Color.black, 3f));
				this.indexLines.Add(BagUI.drawLine(base.transform, start, localPosition3, Color.black, 3f));
				this.indexLines.Add(BagUI.drawLine(base.transform, localPosition, localPosition2, Game.getUIcolorByName(Inventory.getItemDefinition(this.bag.contents[num10].itemType).bagData.color), 1.5f));
				this.indexLines.Add(BagUI.drawLine(base.transform, start, localPosition3, Game.getUIcolorByName(Inventory.getItemDefinition(this.bag.contents[num10].itemType).bagData.color), 1.5f));
			}
		}
	}

	public static void disposeLine(GameObject line)
	{
		line.transform.SetParent(Game.gameInstance.UI.transform);
		line.SetActive(false);
		BagUI.linePool.Add(line);
	}

	public static void drawScienceLine(List<GameObject> container, Transform parent, Vector3 endpoint1, Vector3 endpoint2)
	{
		Vector3 start;
		Vector3 end;
		if (endpoint1.x > endpoint2.x)
		{
			start = endpoint2;
			end = endpoint1;
		}
		else
		{
			end = endpoint2;
			start = endpoint1;
		}
		float x = end.x;
		float y = end.y;
		float f = end.y - start.y;
		float num = Mathf.Abs(f);
		end.y = start.y;
		end.x -= num;
		if (end.x < start.x)
		{
			end.x = x;
			if (y > start.y)
			{
				end.y = start.y + (end.x - start.x);
			}
			else
			{
				end.y = start.y - (end.x - start.x);
			}
		}
		Vector3 start2 = default(Vector3);
		start2.x = end.x;
		start2.y = end.y;
		start2.z = end.z;
		Vector3 end2 = (!(endpoint1.x > endpoint2.x)) ? endpoint2 : endpoint1;
		container.Add(BagUI.drawLine(parent, start, end, ColorPicker.HexToColor("40e0c1"), 1.1f));
		container.Add(BagUI.drawLine(parent, start2, end2, ColorPicker.HexToColor("40e0c1"), 1.1f));
	}

	public static void updateScienceLine(GameObject line1, GameObject line2, Vector3 endpoint1, Vector3 endpoint2)
	{
		Vector3 start;
		Vector3 end;
		if (endpoint1.x > endpoint2.x)
		{
			start = endpoint2;
			end = endpoint1;
		}
		else
		{
			end = endpoint2;
			start = endpoint1;
		}
		float x = end.x;
		float y = end.y;
		float f = end.y - start.y;
		float num = Mathf.Abs(f);
		end.y = start.y;
		end.x -= num;
		if (end.x < start.x)
		{
			end.x = x;
			if (y > start.y)
			{
				end.y = start.y + (end.x - start.x);
			}
			else
			{
				end.y = start.y - (end.x - start.x);
			}
		}
		Vector3 start2 = default(Vector3);
		start2.x = end.x;
		start2.y = end.y;
		start2.z = end.z;
		Vector3 end2 = (!(endpoint1.x > endpoint2.x)) ? endpoint2 : endpoint1;
		BagUI.updateLine(line1, start, end, 1.1f);
		BagUI.updateLine(line2, start2, end2, 1.1f);
	}

	public static GameObject drawLine(Transform parent, Vector3 start, Vector3 end, Color color, float width = 1f)
	{
		if ((Object)BagUI.blankWhiteTex == (Object)null)
		{
			BagUI.blankWhiteTex = (Resources.Load("blankwhite") as Texture2D);
		}
		GameObject gameObject = null;
		if (BagUI.linePool.Count > 0)
		{
			gameObject = BagUI.linePool[0];
			BagUI.linePool.RemoveAt(0);
			gameObject.SetActive(true);
		}
		else
		{
			gameObject = new GameObject();
			gameObject.AddComponent<RawImage>();
			gameObject.GetComponent<RawImage>().texture = BagUI.blankWhiteTex;
			gameObject.GetComponent<RawImage>().SetNativeSize();
			gameObject.GetComponent<RawImage>().raycastTarget = false;
		}
		gameObject.transform.SetParent(parent);
		gameObject.transform.localPosition = (start + end) / 2f;
		gameObject.GetComponent<RawImage>().color = color;
		Vector3 vector = Vector3.one / 16f;
		vector.x = width / 16f;
		vector.y = (end - start).magnitude / 16f;
		gameObject.transform.localScale = vector;
		vector = Vector3.zero;
		vector.z = Mathf.Atan2(end.y - start.y, end.x - start.x) * 180f / 3.1415f - 90f;
		gameObject.transform.localEulerAngles = vector;
		return gameObject;
	}

	public static void updateLine(GameObject line, Vector3 start, Vector3 end, float width = 1f)
	{
		line.transform.localPosition = (start + end) / 2f;
		Vector3 vector = Vector3.one / 16f;
		vector.x = width / 16f;
		vector.y = (end - start).magnitude / 16f;
		line.transform.localScale = vector;
		vector = Vector3.zero;
		vector.z = Mathf.Atan2(end.y - start.y, end.x - start.x) * 180f / 3.1415f - 90f;
		line.transform.localEulerAngles = vector;
	}
}
