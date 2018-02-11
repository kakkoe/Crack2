using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureLayerUI : MonoBehaviour
{
	public int id;

	public List<GameObject> maskButtons = new List<GameObject>();

	public bool isLayer;

	public bool ignoreSliderChanges;

	public bool isEmbellishment;

	public GameObject cmdDelete;

	private Vector3 v3 = default(Vector3);

	private Color fadeColor;

	public int autoEditDelay;

	private void Start()
	{
		if ((Object)base.transform.Find("hdBaseColor") != (Object)null)
		{
			((Component)base.transform.Find("hdBaseColor")).GetComponent<Text>().text = "BASE_COLOR";
		}
		if ((Object)base.transform.Find("hdAddMarking") != (Object)null)
		{
			((Component)base.transform.Find("hdAddMarking")).GetComponent<Text>().text = "ADD_MARKING";
		}
		if ((bool)base.transform.Find("LayerStuff").Find("hdPattern"))
		{
			((Component)base.transform.Find("LayerStuff").Find("hdPattern")).GetComponent<Text>().text = "PATTERN";
		}
		if ((bool)base.transform.Find("LayerStuff").Find("hdMask"))
		{
			((Component)base.transform.Find("LayerStuff").Find("hdMask")).GetComponent<Text>().text = "APPLIED_TO";
		}
		if ((bool)base.transform.Find("LayerStuff").Find("hdEdit"))
		{
			((Component)base.transform.Find("LayerStuff").Find("hdEdit")).GetComponent<Text>().text = "EDIT";
		}
		if ((bool)base.transform.Find("LayerStuff").Find("hdRequired"))
		{
			((Component)base.transform.Find("LayerStuff").Find("hdRequired")).GetComponent<Text>().text = "REQUIRED";
		}
		if ((bool)base.transform.Find("LayerStuff").Find("hdEmbellishment"))
		{
			((Component)base.transform.Find("LayerStuff").Find("hdEmbellishment")).GetComponent<Text>().text = "EMBELLISHMENT";
		}
		if ((Object)base.transform.Find("LayerStuff").Find("MaskButton") != (Object)null)
		{
			this.maskButtons = new List<GameObject>();
			this.maskButtons.Add(base.transform.Find("LayerStuff").Find("MaskButton").gameObject);
		}
	}

	public void slidersUpdated()
	{
		if (!this.ignoreSliderChanges && this.id != -1)
		{
			Game.gameInstance.PC().data.embellishmentLayers[this.id].size = ((Component)base.transform.Find("LayerStuff").Find("sldSize")).GetComponent<UnityEngine.UI.Slider>().value;
			Game.gameInstance.PC().data.embellishmentLayers[this.id].bend = ((Component)base.transform.Find("LayerStuff").Find("sldBend")).GetComponent<UnityEngine.UI.Slider>().value;
			Game.gameInstance.PC().data.embellishmentLayers[this.id].turn = ((Component)base.transform.Find("LayerStuff").Find("sldTurn")).GetComponent<UnityEngine.UI.Slider>().value;
			Game.gameInstance.PC().data.embellishmentLayers[this.id].twist = ((Component)base.transform.Find("LayerStuff").Find("sldTwist")).GetComponent<UnityEngine.UI.Slider>().value;
			Game.gameInstance.characterRebuildDelay = 99999999;
			Game.gameInstance.editingExistingEmbellishment = true;
			Game.gameInstance.editingEmbellishmentLayer = this.id;
			int index = 0;
			int num = 0;
			while (num < Game.gameInstance.PC().parts.Count)
			{
				if (!(Game.gameInstance.PC().parts[num].name == Game.gameInstance.PC().data.embellishmentLayers[this.id].partName))
				{
					num++;
					continue;
				}
				index = num;
				break;
			}
			Mesh sharedMesh = Game.gameInstance.PC().preciseMousePickingCollider[index].GetComponent<MeshCollider>().sharedMesh;
			if (!((Object)sharedMesh == (Object)null) && Game.gameInstance.PC().data.embellishmentLayers[this.id].vertexID < sharedMesh.vertices.Length)
			{
				Game.gameInstance.precisePickPoint = Game.gameInstance.PC().preciseMousePickingCollider[index].transform.TransformPoint(sharedMesh.vertices[Game.gameInstance.PC().data.embellishmentLayers[this.id].vertexID]);
				Game.gameInstance.precisePickNormal = Game.gameInstance.PC().preciseMousePickingCollider[index].transform.TransformVector(sharedMesh.normals[Game.gameInstance.PC().data.embellishmentLayers[this.id].vertexID]);
			}
		}
	}

	private void Update()
	{
		this.fadeColor = Color.white;
		this.fadeColor.a = 0.3f;
		if (this.isLayer)
		{
			if (this.autoEditDelay > 0)
			{
				this.autoEditDelay--;
				if (this.autoEditDelay <= 0)
				{
					this.EditClicked();
				}
			}
			if (!this.isEmbellishment)
			{
				base.transform.Find("LayerStuff").Find("cmdEdit").gameObject.SetActive(!Game.gameInstance.PC().data.textureLayers[this.id - 1].required && !Game.gameInstance.PC().data.textureLayers[this.id - 1].isDecal);
				base.transform.Find("LayerStuff").Find("hdEdit").gameObject.SetActive(!Game.gameInstance.PC().data.textureLayers[this.id - 1].required && !Game.gameInstance.PC().data.textureLayers[this.id - 1].isDecal);
				base.transform.Find("LayerStuff").Find("hdRequired").gameObject.SetActive(Game.gameInstance.PC().data.textureLayers[this.id - 1].required);
				if (Game.gameInstance.PC().data.textureLayers[this.id - 1].glow)
				{
					((Component)base.transform.Find("LayerStuff").Find("cmdGlow")).GetComponent<Image>().color = Color.white;
				}
				else
				{
					((Component)base.transform.Find("LayerStuff").Find("cmdGlow")).GetComponent<Image>().color = this.fadeColor;
				}
				int num = 1 + Game.gameInstance.PC().data.textureLayers[this.id - 1].masks.Count;
				while (this.maskButtons.Count != num)
				{
					if (num > this.maskButtons.Count)
					{
						GameObject gameObject = Object.Instantiate(this.maskButtons[0]);
						gameObject.transform.SetParent(base.transform.Find("LayerStuff"));
						this.v3 = Vector3.one;
						gameObject.transform.localScale = this.v3;
						this.maskButtons.Add(gameObject);
					}
					else
					{
						int index = this.maskButtons.Count - 1;
						Object.Destroy(this.maskButtons[index]);
						this.maskButtons.RemoveAt(index);
					}
					for (int i = 0; i < this.maskButtons.Count; i++)
					{
						this.v3 = Vector3.zero;
						ref Vector3 val = ref this.v3;
						Vector3 localPosition = this.maskButtons[0].transform.localPosition;
						val.y = localPosition.y;
						this.v3.x = (float)(-39 + 100 * i);
						if (i == this.maskButtons.Count - 1 && i > 0)
						{
							this.v3.x -= 25f;
						}
						this.maskButtons[i].transform.localPosition = this.v3;
					}
				}
				for (int j = 0; j < this.maskButtons.Count; j++)
				{
					this.maskButtons[j].GetComponent<MaskButton>().id = j;
					this.maskButtons[j].GetComponent<MaskButton>().owner = this;
					if (j == this.maskButtons.Count - 1)
					{
						this.maskButtons[j].transform.Find("cmdDelete").gameObject.SetActive(false);
						this.maskButtons[j].transform.Find("cmdNew").gameObject.SetActive(this.maskButtons.Count < 3);
						this.maskButtons[j].transform.Find("cmdMultiply").gameObject.SetActive(false);
						this.maskButtons[j].transform.Find("cmdAdd").gameObject.SetActive(false);
						this.maskButtons[j].transform.Find("cmdInvertL").gameObject.SetActive(false);
						this.maskButtons[j].transform.Find("cmdInvertR").gameObject.SetActive(false);
					}
					else
					{
						this.maskButtons[j].transform.Find("cmdDelete").gameObject.SetActive(true);
						this.maskButtons[j].transform.Find("cmdNew").gameObject.SetActive(false);
						this.maskButtons[j].transform.Find("cmdInvertL").gameObject.SetActive(j == 0);
						this.maskButtons[j].transform.Find("cmdInvertR").gameObject.SetActive(j > 0);
						if (Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[j].invert)
						{
							((Component)this.maskButtons[j].transform.Find("cmdInvertL")).GetComponent<Image>().color = Color.white;
							((Component)this.maskButtons[j].transform.Find("cmdInvertR")).GetComponent<Image>().color = Color.white;
						}
						else
						{
							((Component)this.maskButtons[j].transform.Find("cmdInvertL")).GetComponent<Image>().color = this.fadeColor;
							((Component)this.maskButtons[j].transform.Find("cmdInvertR")).GetComponent<Image>().color = this.fadeColor;
						}
						if (j == 0)
						{
							this.maskButtons[j].transform.Find("cmdMultiply").gameObject.SetActive(false);
							this.maskButtons[j].transform.Find("cmdAdd").gameObject.SetActive(false);
						}
						else if (Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[j].add)
						{
							this.maskButtons[j].transform.Find("cmdMultiply").gameObject.SetActive(false);
							this.maskButtons[j].transform.Find("cmdAdd").gameObject.SetActive(true);
						}
						else
						{
							this.maskButtons[j].transform.Find("cmdMultiply").gameObject.SetActive(true);
							this.maskButtons[j].transform.Find("cmdAdd").gameObject.SetActive(false);
						}
						string str = string.Empty;
						if (Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[j].invert)
						{
							str = "! ";
						}
						((Component)this.maskButtons[j].transform.Find("cmdDelete").Find("Text")).GetComponent<Text>().text = (str + Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[j].texture).ToUpper();
					}
				}
			}
		}
	}

	public void addMask()
	{
		if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
			TextureLayerMask textureLayerMask = new TextureLayerMask();
			textureLayerMask.texture = "entirebody";
			Game.gameInstance.PC().data.textureLayers[this.id - 1].masks.Add(textureLayerMask);
			Game.gameInstance.texturePatternMenuOpen = true;
			Game.gameInstance.editingTextureLayer = this.id;
			Game.gameInstance.editingTextureLayerMask = Game.gameInstance.PC().data.textureLayers[this.id - 1].masks.Count - 1;
			Game.gameInstance.characterRedrawDelay = 2;
		}
	}

	public void deleteMask(int mid)
	{
		if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
			Game.gameInstance.PC().data.textureLayers[this.id - 1].masks.RemoveAt(mid);
			Game.gameInstance.characterRedrawDelay = 1;
		}
	}

	public void toggleMirror()
	{
		if (this.id != -1)
		{
			Game.gameInstance.PC().data.embellishmentLayers[this.id].mirror = !Game.gameInstance.PC().data.embellishmentLayers[this.id].mirror;
		}
		else
		{
			Game.gameInstance.embellishmentBrushSetting_mirror = !Game.gameInstance.embellishmentBrushSetting_mirror;
			if (Game.gameInstance.embellishmentBrushSetting_center)
			{
				Game.gameInstance.embellishmentBrushSetting_center = false;
			}
		}
		Game.gameInstance.characterRebuildDelay = 2;
	}

	public void toggleVisible()
	{
		if (this.id != -1)
		{
			Game.gameInstance.PC().data.embellishmentLayers[this.id].hidden = !Game.gameInstance.PC().data.embellishmentLayers[this.id].hidden;
		}
		Game.gameInstance.characterRebuildDelay = 2;
	}

	public void embellishmentColorClicked(int c)
	{
		if (this.id < 0)
		{
			Game.gameInstance.embellishmentBrushSetting_color = c;
		}
		else
		{
			Game.gameInstance.PC().data.embellishmentLayers[this.id].color = c;
			Game.gameInstance.characterRebuildDelay = 2;
		}
	}

	public void toggleCenter()
	{
		if (this.id == -1)
		{
			Game.gameInstance.embellishmentBrushSetting_center = !Game.gameInstance.embellishmentBrushSetting_center;
			if (Game.gameInstance.embellishmentBrushSetting_center)
			{
				Game.gameInstance.embellishmentBrushSetting_mirror = false;
			}
		}
	}

	public void toggleMaskMode(int mid)
	{
		if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
		}
	}

	public void toggleInvertMode(int mid)
	{
		if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
			Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[mid].invert = !Game.gameInstance.PC().data.textureLayers[this.id - 1].masks[mid].invert;
			Game.gameInstance.characterRedrawDelay = 1;
		}
	}

	public void toggleGlow()
	{
		Game.gameInstance.PC().data.textureLayers[this.id - 1].glow = !Game.gameInstance.PC().data.textureLayers[this.id - 1].glow;
		Game.gameInstance.characterRedrawDelay = 2;
	}

	public void CreateClicked()
	{
		Game.gameInstance.colorPickerOpen = false;
		TextureLayer textureLayer = new TextureLayer();
		textureLayer.texture = "color";
		textureLayer.opacity = 1f;
		textureLayer.color = Color.white;
		Game.gameInstance.PC().data.textureLayers.Add(textureLayer);
		this.autoEditDelay = 1;
		Game.gameInstance.characterRedrawDelay = 2;
	}

	public void EditClicked()
	{
		if (this.isEmbellishment)
		{
			Game.gameInstance.embellishmentMenuOpen = true;
			Game.gameInstance.editingEmbellishmentLayer = this.id;
		}
		else if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
			Game.gameInstance.texturePatternMenuOpen = true;
			Game.gameInstance.editingTextureLayer = this.id;
			Game.gameInstance.editingTextureLayerMask = -1;
		}
	}

	public void opacityChanged()
	{
		if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.PC().data.textureLayers[this.id - 1].opacity = ((Component)base.transform.Find("LayerStuff").Find("sldOpacity")).GetComponent<UnityEngine.UI.Slider>().value;
			Game.gameInstance.characterRedrawDelay = 2;
		}
	}

	public void ColorClicked()
	{
		Game.gameInstance.colorPickerOpen = true;
		List<Color> list = new List<Color>();
		list.Add(Game.gameInstance.PC().data.baseColor);
		for (int i = 0; i < Game.gameInstance.PC().data.textureLayers.Count; i++)
		{
			if (list.IndexOf(Game.gameInstance.PC().data.textureLayers[i].color) == -1)
			{
				list.Add(Game.gameInstance.PC().data.textureLayers[i].color);
			}
		}
		Game.gameInstance.colorPicker.GetComponent<ColorPicker>().setExistingColors(list);
		Game.gameInstance.editingTextureLayer = this.id;
		Game.gameInstance.originalEditColor = ((Component)base.transform.Find("cmdColor")).GetComponent<Image>().color;
		Game.gameInstance.colorPicker.GetComponent<ColorPicker>().color = ((Component)base.transform.Find("cmdColor")).GetComponent<Image>().color;
		Game.gameInstance.colorPicker.GetComponent<ColorPicker>().updateHSV();
		Game.gameInstance.colorPicker.GetComponent<ColorPicker>().render();
	}

	public void DeleteClicked()
	{
		if (this.isEmbellishment)
		{
			if (this.id != -1)
			{
				Game.gameInstance.PC().data.embellishmentLayers.RemoveAt(this.id);
				Game.gameInstance.characterRebuildDelay = 1;
			}
		}
		else if (!Game.gameInstance.PC().data.textureLayers[this.id - 1].required)
		{
			Game.gameInstance.colorPickerOpen = false;
			Game.gameInstance.PC().data.textureLayers.RemoveAt(this.id - 1);
			Game.gameInstance.characterRedrawDelay = 1;
		}
	}

	public void UpClicked()
	{
		Game.gameInstance.colorPickerOpen = false;
		if (this.id - 1 < Game.gameInstance.PC().data.textureLayers.Count - 1)
		{
			TextureLayer value = Game.gameInstance.PC().data.textureLayers[this.id - 1];
			Game.gameInstance.PC().data.textureLayers[this.id - 1] = Game.gameInstance.PC().data.textureLayers[this.id];
			Game.gameInstance.PC().data.textureLayers[this.id] = value;
			Game.gameInstance.characterRedrawDelay = 1;
		}
	}

	public void DownClicked()
	{
		Game.gameInstance.colorPickerOpen = false;
		if (this.id - 1 > 0)
		{
			TextureLayer value = Game.gameInstance.PC().data.textureLayers[this.id - 1];
			Game.gameInstance.PC().data.textureLayers[this.id - 1] = Game.gameInstance.PC().data.textureLayers[this.id - 2];
			Game.gameInstance.PC().data.textureLayers[this.id - 2] = value;
			Game.gameInstance.characterRedrawDelay = 1;
		}
	}
}
