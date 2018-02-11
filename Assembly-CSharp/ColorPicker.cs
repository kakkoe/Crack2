using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
	public Func<Color, bool> onChanged;

	private float hue = 0.1f;

	private float sat = 0.36f;

	private float val = 0.66f;

	private Vector3 v3 = default(Vector3);

	public Color color = Color.white;

	private Color vColor = Color.white;

	private Color dimColor = Color.black;

	public bool initted;

	private float hexTextDelay;

	private float RGBTextDelay;

	public List<GameObject> paletteButtons = new List<GameObject>();

	private int dragging;

	private float colorleft;

	private float colorright;

	private float colortop;

	private float colorbottom;

	private float valleft;

	private float valright;

	private float valtop;

	private float valbottom;

	private float texWidth;

	private float texHeight;

	private List<GameObject> existingDots = new List<GameObject>();

	private List<Color> existingColors = new List<Color>();

	private List<float> existingHues = new List<float>();

	private List<float> existingSats = new List<float>();

	private List<float> existingVals = new List<float>();

	private int needRedraw = 2;

	private void Start()
	{
	}

	private void OnEnable()
	{
	}

	public void render()
	{
		this.checkInit();
		if (this.texWidth < 1f)
		{
			this.needRedraw = 2;
		}
		ColorPicker.HsvToRgb((1f - this.hue) * 360f, this.sat, this.val, out this.color.r, out this.color.g, out this.color.b);
		this.v3 = Vector3.zero;
		this.v3.x = this.colorleft + (this.colorright - this.colorleft) * this.hue - this.texWidth / 2f;
		this.v3.y = 0f - (this.colortop + (this.colorbottom - this.colortop) * (1f - this.sat) - this.texHeight / 2f);
		this.v3.z = -2f;
		base.transform.Find("ColorCursor").transform.localPosition = this.v3;
		this.v3 = Vector3.zero;
		this.v3.x = (0f - this.texWidth) / 2f + this.valleft + (this.valright - this.valleft) * this.val;
		this.v3.y = this.texHeight / 2f - 379f;
		this.v3.z = -2f;
		base.transform.Find("ValueCursor").transform.localPosition = this.v3;
		((Component)base.transform.Find("ColorCursor").Find("Fill")).GetComponent<Image>().color = this.color;
		this.vColor.r = this.val;
		this.vColor.g = this.val;
		this.vColor.b = this.val;
		((Component)base.transform.Find("txtHex")).GetComponent<InputField>().text = this.ColorToHex(this.color);
		((Component)base.transform.Find("txtR")).GetComponent<InputField>().text = Mathf.Round(this.color.r * 255f) + string.Empty;
		((Component)base.transform.Find("txtG")).GetComponent<InputField>().text = Mathf.Round(this.color.g * 255f) + string.Empty;
		((Component)base.transform.Find("txtB")).GetComponent<InputField>().text = Mathf.Round(this.color.b * 255f) + string.Empty;
		this.dimColor.a = 1f - this.val;
		((Component)base.transform.Find("Dimmer")).GetComponent<Image>().color = this.dimColor;
	}

	public void hexUpdated()
	{
		if (!(this.hexTextDelay > 0f))
		{
			try
			{
				this.color = ColorPicker.HexToColor(((Component)base.transform.Find("txtHex")).GetComponent<InputField>().text);
				this.updateHSV();
				this.hexTextDelay = 2f;
				this.RGBTextDelay = 2f;
				this.render();
				if (this.onChanged != null)
				{
					this.onChanged(this.color);
				}
			}
			catch
			{
			}
		}
	}

	public void updateHSV()
	{
		ColorPicker.ColorToHSV(this.color, out this.hue, out this.sat, out this.val);
		this.hue = 360f - this.hue;
		this.hue /= 360f;
	}

	public void hexDoneEditing()
	{
		if (!(this.hexTextDelay > 0f))
		{
			try
			{
				this.color = ColorPicker.HexToColor(((Component)base.transform.Find("txtHex")).GetComponent<InputField>().text);
			}
			catch
			{
				this.hexTextDelay = 2f;
				((Component)base.transform.Find("txtHex")).GetComponent<InputField>().text = this.ColorToHex(this.color);
			}
		}
	}

	public void RGBUpdated()
	{
		if (!(this.RGBTextDelay > 0f))
		{
			try
			{
				this.color.r = float.Parse(((Component)base.transform.Find("txtR")).GetComponent<InputField>().text) / 255f;
				this.color.g = float.Parse(((Component)base.transform.Find("txtG")).GetComponent<InputField>().text) / 255f;
				this.color.b = float.Parse(((Component)base.transform.Find("txtB")).GetComponent<InputField>().text) / 255f;
				this.hexTextDelay = 1f;
				((Component)base.transform.Find("txtHex")).GetComponent<InputField>().text = this.ColorToHex(this.color);
				this.hexTextDelay = 0f;
				this.hexUpdated();
			}
			catch
			{
			}
		}
	}

	public static void ColorToHSV(Color color, out float hue, out float saturation, out float value)
	{
		float num = Mathf.Max(color.r * 255f, Mathf.Max(color.g * 255f, color.b * 255f));
		float num2 = Mathf.Min(color.r * 255f, Mathf.Min(color.g * 255f, color.b * 255f));
		hue = 0f;
		if (Mathf.Abs(num - color.r * 255f) < 0.1f)
		{
			hue = (color.g * 255f - color.b * 255f) / (num - num2);
		}
		else if (Mathf.Abs(num - color.g * 255f) < 0.1f)
		{
			hue = 2f + (color.b * 255f - color.r * 255f) / (num - num2);
		}
		else
		{
			hue = 4f + (color.r * 255f - color.g * 255f) / (num - num2);
		}
		hue *= 60f;
		if (hue < 0f)
		{
			hue += 360f;
		}
		if (float.IsNaN(hue))
		{
			hue = 0f;
		}
		saturation = ((num != 0f) ? (1f - 1f * num2 / num) : 0f);
		value = num / 255f;
	}

	private string ColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	public static Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}

	public void setExistingColors(List<Color> eColors)
	{
		this.checkInit();
		if (eColors.Count < 1)
		{
			eColors.Add(Color.white);
		}
		this.existingColors = eColors;
		this.existingHues = new List<float>();
		this.existingSats = new List<float>();
		this.existingVals = new List<float>();
		for (int i = 0; i < this.existingColors.Count; i++)
		{
			float num = default(float);
			float item = default(float);
			float item2 = default(float);
			ColorPicker.ColorToHSV(this.existingColors[i], out num, out item, out item2);
			num = 360f - num;
			num /= 360f;
			this.existingHues.Add(num);
			this.existingSats.Add(item);
			this.existingVals.Add(item2);
		}
		if (this.existingColors.Count > this.existingDots.Count)
		{
			bool activeSelf = this.existingDots[0].activeSelf;
			if (!activeSelf)
			{
				this.existingDots[0].SetActive(true);
			}
			this.existingDots.Add(UnityEngine.Object.Instantiate(this.existingDots[0]));
			this.existingDots[this.existingDots.Count - 1].transform.SetParent(this.existingDots[0].transform.parent);
			this.existingDots[this.existingDots.Count - 1].transform.localScale = Vector3.one;
			if (!activeSelf)
			{
				this.existingDots[0].SetActive(false);
			}
		}
		for (int j = 0; j < this.existingDots.Count; j++)
		{
			if (j < this.existingColors.Count)
			{
				this.existingDots[j].SetActive(true);
				this.v3 = Vector3.zero;
				this.v3.x = this.colorleft + (this.colorright - this.colorleft) * this.existingHues[j] - this.texWidth / 2f;
				this.v3.y = 0f - (this.colortop + (this.colorbottom - this.colortop) * (1f - this.existingSats[j]) - this.texHeight / 2f);
				this.v3.z = -2f;
				this.existingDots[j].transform.localPosition = this.v3;
			}
			else
			{
				this.existingDots[j].SetActive(false);
			}
		}
		while (this.existingHues.Count != this.paletteButtons.Count)
		{
			if (this.existingHues.Count > this.paletteButtons.Count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(this.paletteButtons[0]);
				gameObject.transform.SetParent(base.transform);
				this.v3 = Vector3.one;
				gameObject.transform.localScale = this.v3;
				this.paletteButtons.Add(gameObject);
			}
			else
			{
				int index = this.paletteButtons.Count - 1;
				UnityEngine.Object.Destroy(this.paletteButtons[index]);
				this.paletteButtons.RemoveAt(index);
			}
			for (int k = 0; k < this.paletteButtons.Count; k++)
			{
				this.v3 = Vector3.zero;
				ref Vector3 val = ref this.v3;
				Vector3 localPosition = this.paletteButtons[0].transform.localPosition;
				val.y = localPosition.y;
				ref Vector3 val2 = ref this.v3;
				Vector3 localPosition2 = this.paletteButtons[0].transform.localPosition;
				val2.x = localPosition2.x - (float)(17 * k);
				this.paletteButtons[k].transform.localPosition = this.v3;
			}
		}
		for (int l = 0; l < this.paletteButtons.Count; l++)
		{
			this.paletteButtons[l].GetComponent<Image>().color = this.existingColors[l];
		}
	}

	public void paletteClicked(Button cmd)
	{
		this.color = ((Component)cmd).GetComponent<Image>().color;
		this.updateHSV();
		this.render();
		if (this.onChanged != null)
		{
			this.onChanged(this.color);
		}
	}

	public void checkInit()
	{
		if (!this.initted)
		{
			this.hue = 0.07f;
			this.sat = 0.6f;
			this.val = 0.6f;
			this.colorleft = 121f;
			this.colorright = 376f;
			this.colortop = 113f;
			this.colorbottom = 368f;
			this.valleft = 121f;
			this.valright = 376f;
			this.valtop = 370f;
			this.valbottom = 388f;
			this.texWidth = 0f;
			this.texHeight = 0f;
			this.existingDots = new List<GameObject>();
			this.existingDots.Add(base.transform.Find("existingDot").gameObject);
			this.paletteButtons = new List<GameObject>();
			this.paletteButtons.Add(base.transform.Find("cmdPalette").gameObject);
			this.initted = true;
		}
	}

	private void Update()
	{
		if ((UnityEngine.Object)base.transform.Find("chkPalette") != (UnityEngine.Object)null)
		{
			((Component)base.transform.Find("chkPalette").Find("Label")).GetComponent<Text>().text = Localization.getPhrase("EDIT_PALETTE", string.Empty);
		}
		this.checkInit();
		if (this.needRedraw > 0)
		{
			this.needRedraw--;
			if (this.needRedraw == 0)
			{
				if (this.texWidth < 1f)
				{
					this.texWidth = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.width;
					this.texHeight = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.height;
				}
				this.render();
			}
		}
		if (this.hexTextDelay > 0f)
		{
			this.hexTextDelay -= 1f;
		}
		if (this.RGBTextDelay > 0f)
		{
			this.RGBTextDelay -= 1f;
		}
		if (Input.GetMouseButton(0) && base.isActiveAndEnabled)
		{
			this.hexTextDelay = 2f;
			this.RGBTextDelay = 2f;
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(Game.gameInstance.uiCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out raycastHit))
			{
				if (this.texWidth < 1f)
				{
					this.texWidth = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.width;
					this.texHeight = (float)base.GetComponentInChildren<MeshRenderer>().material.mainTexture.height;
				}
				Vector2 textureCoord = raycastHit.textureCoord;
				textureCoord.y = 1f - textureCoord.y;
				textureCoord.x *= this.texWidth;
				textureCoord.y *= this.texHeight;
				if (this.dragging == 0)
				{
					if (textureCoord.x >= this.colorleft && textureCoord.x <= this.colorright && textureCoord.y >= this.colortop && textureCoord.y <= this.colorbottom)
					{
						this.dragging = 1;
					}
					if (textureCoord.x >= this.valleft && textureCoord.x <= this.valright && textureCoord.y >= this.valtop && textureCoord.y <= this.valbottom)
					{
						this.dragging = 2;
					}
				}
				if (this.dragging == 1)
				{
					this.hue = textureCoord.x - this.colorleft;
					this.hue /= this.colorright - this.colorleft;
					if (this.hue < 0f)
					{
						this.hue = 0f;
					}
					if (this.hue > 1f)
					{
						this.hue = 1f;
					}
					this.sat = textureCoord.y - this.colortop;
					this.sat /= this.colorbottom - this.colortop;
					if (this.sat < 0f)
					{
						this.sat = 0f;
					}
					if (this.sat > 1f)
					{
						this.sat = 1f;
					}
					this.sat = 1f - this.sat;
					if (Input.GetKey(UserSettings.data.KEY_SNAP_TO_GRID))
					{
						int num = -1;
						float num2 = 0.04f;
						for (int i = 0; i < this.existingHues.Count; i++)
						{
							float f = this.existingHues[i] - this.hue;
							float f2 = this.existingSats[i] - this.sat;
							float num3 = Mathf.Sqrt(Mathf.Pow(f, 2f) + Mathf.Pow(f2, 2f));
							if (num3 < num2)
							{
								num2 = num3;
								num = i;
							}
						}
						if (num != -1)
						{
							this.hue = this.existingHues[num];
							this.sat = this.existingSats[num];
							this.val = this.existingVals[num];
						}
					}
					this.render();
				}
				if (this.dragging == 2)
				{
					this.val = textureCoord.x - this.valleft;
					this.val /= this.valright - this.valleft;
					if (this.val < 0f)
					{
						this.val = 0f;
					}
					if (this.val > 1f)
					{
						this.val = 1f;
					}
					this.render();
				}
			}
		}
		else
		{
			if (this.dragging > 0 && this.onChanged != null)
			{
				this.onChanged(this.color);
			}
			this.dragging = 0;
		}
	}

	public static Color HsvToColor(float h, float S, float V)
	{
		Color white = Color.white;
		float r = default(float);
		float g = default(float);
		float b = default(float);
		ColorPicker.HsvToRgb(h, S, V, out r, out g, out b);
		white.r = r;
		white.g = g;
		white.b = b;
		return white;
	}

	public static void HsvToRgb(float h, float S, float V, out float r, out float g, out float b)
	{
		double num;
		for (num = (double)h; num < 0.0; num += 360.0)
		{
		}
		while (num >= 360.0)
		{
			num -= 360.0;
		}
		double num4;
		double num3;
		double num2;
		if (V <= 0f)
		{
			num4 = (num3 = (num2 = 0.0));
		}
		else if (S <= 0f)
		{
			num4 = (num3 = (num2 = (double)V));
		}
		else
		{
			double num5 = num / 60.0;
			int num6 = (int)Mathf.Floor((float)num5);
			double num7 = num5 - (double)num6;
			double num8 = (double)(V * (1f - S));
			double num9 = (double)V * (1.0 - (double)S * num7);
			double num10 = (double)V * (1.0 - (double)S * (1.0 - num7));
			switch (num6)
			{
			case 0:
				num4 = (double)V;
				num3 = num10;
				num2 = num8;
				break;
			case 1:
				num4 = num9;
				num3 = (double)V;
				num2 = num8;
				break;
			case 2:
				num4 = num8;
				num3 = (double)V;
				num2 = num10;
				break;
			case 3:
				num4 = num8;
				num3 = num9;
				num2 = (double)V;
				break;
			case 4:
				num4 = num10;
				num3 = num8;
				num2 = (double)V;
				break;
			case 5:
				num4 = (double)V;
				num3 = num8;
				num2 = num9;
				break;
			case 6:
				num4 = (double)V;
				num3 = num10;
				num2 = num8;
				break;
			case -1:
				num4 = (double)V;
				num3 = num8;
				num2 = num9;
				break;
			default:
				num4 = (num3 = (num2 = (double)V));
				break;
			}
		}
		r = (float)num4;
		g = (float)num3;
		b = (float)num2;
	}
}
