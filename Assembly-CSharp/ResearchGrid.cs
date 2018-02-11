using System;
using UnityEngine;
using UnityEngine.Rendering;

public class ResearchGrid
{
	public static string curID = string.Empty;

	public static string curCategory = string.Empty;

	public static float curCompletion;

	public string taskID;

	public string taskCategory;

	public float taskCompletion;

	public int taskValue;

	public int taskType;

	public bool gridInUse;

	public bool gridVisible = true;

	public float gridUseAnimator;

	public float guaAccel;

	public float gridY;

	public static int selectedColor;

	public static GameObject BlankHexTile;

	public Game game;

	public GameObject GO;

	public GameObject beacon;

	public GameObject[] tiles;

	public int[] tileTypes;

	public int[] tileSolutions;

	public int[] tileGuesses;

	public static int[] curColorsRequired;

	public bool[] finishedWithColor;

	public bool finishedWithAllColors;

	public float updateDelay;

	public GameObject audioSource;

	public float cols = 32f;

	public float rows = 4f;

	public float sphereRadius = 6.7f;

	public float tileHeight = 2.15f;

	private Vector3 v3 = default(Vector3);

	public GameObject tileContainer;

	public bool colorSet;

	public ParticleSystem.MainModule beaconMain;

	public bool beaconMainSet;

	public float timeSinceSFX;

	public bool lastGIU;

	public int lastCTTC = -1;

	public static bool madeAnyGuesses;

	private float approach;

	private bool lastST = true;

	private Vector3 v32;

	public Transform pointer;

	public static bool lastGuessWasGood;

	public static float recentlyOutOfChemicals;

	public ResearchGrid(Game _game)
	{
		this.finishedWithColor = new bool[6];
		ResearchGrid.curColorsRequired = new int[6];
		this.game = _game;
		this.GO = new GameObject("ResearchGrid");
		this.audioSource = UnityEngine.Object.Instantiate(this.game.defaultAudioSource);
		this.audioSource.transform.SetParent(this.GO.transform);
		this.audioSource.transform.localPosition = Vector3.zero;
		this.audioSource.GetComponent<AudioSource>().loop = true;
		this.audioSource.GetComponent<AudioSource>().maxDistance = 20f;
		this.audioSource.GetComponent<AudioSource>().clip = (Resources.Load("buzzingforcefield") as AudioClip);
		this.audioSource.GetComponent<AudioSource>().volume = 0.01f;
		this.audioSource.GetComponent<AudioSource>().Play();
		this.v3.x = (this.v3.y = (this.v3.z = 0.55f));
		this.gridY = 7.5f;
		this.GO.transform.localScale = this.v3;
		this.tileContainer = new GameObject("tileContainer");
		this.tileContainer.transform.SetParent(this.GO.transform);
		this.tileContainer.transform.localScale = Vector3.one * 1.2f;
		this.buildTileDefinitions();
		this.buildTiles();
		this.tileContainer.gameObject.SetActive(false);
	}

	public void buildTileDefinitions()
	{
		if ((UnityEngine.Object)ResearchGrid.BlankHexTile == (UnityEngine.Object)null)
		{
			ResearchGrid.BlankHexTile = GameObject.Find("HexTile");
			ResearchGrid.BlankHexTile.SetActive(false);
		}
	}

	public void buildTiles()
	{
		this.tiles = new GameObject[(int)(this.cols * this.rows)];
		this.tileTypes = new int[this.tiles.Length];
		this.tileSolutions = new int[this.tiles.Length];
		this.tileGuesses = new int[this.tiles.Length];
		for (int i = 0; i < this.tiles.Length; i++)
		{
			this.tiles[i] = UnityEngine.Object.Instantiate(ResearchGrid.BlankHexTile);
			MeshRenderer[] componentsInChildren = this.tiles[i].GetComponentsInChildren<MeshRenderer>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].reflectionProbeUsage = ReflectionProbeUsage.Simple;
			}
			this.tiles[i].transform.parent = this.tileContainer.transform;
			this.tiles[i].transform.Find("blank_hex").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_FE").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_CA").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_AV").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_EQ").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_SC").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_LA").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_FEg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_CAg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_AVg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_EQg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_SCg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_LAg").Rotate(90f, 0f, 0f);
			this.tiles[i].transform.Find("hex_selector").Rotate(90f, 0f, 0f);
			this.tiles[i].SetActive(true);
			this.setTile(i, 0);
		}
		this.processTileAnimation();
	}

	public void setTile(int t, int val)
	{
		this.tiles[t].transform.Find("blank_hex").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_FE").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_CA").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_AV").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_EQ").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_SC").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_LA").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_FEg").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_CAg").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_AVg").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_EQg").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_SCg").gameObject.SetActive(false);
		this.tiles[t].transform.Find("hex_LAg").gameObject.SetActive(false);
		this.tileTypes[t] = val;
		switch (this.tileTypes[t])
		{
		case 0:
			this.tiles[t].transform.Find("blank_hex").gameObject.SetActive(true);
			break;
		case 1:
			this.tiles[t].transform.Find("hex_CA").gameObject.SetActive(true);
			break;
		case 2:
			this.tiles[t].transform.Find("hex_FE").gameObject.SetActive(true);
			break;
		case 3:
			this.tiles[t].transform.Find("hex_LA").gameObject.SetActive(true);
			break;
		case 4:
			this.tiles[t].transform.Find("hex_SC").gameObject.SetActive(true);
			break;
		case 5:
			this.tiles[t].transform.Find("hex_EQ").gameObject.SetActive(true);
			break;
		case 6:
			this.tiles[t].transform.Find("hex_AV").gameObject.SetActive(true);
			break;
		case 7:
		{
			this.tiles[t].transform.Find("hex_CAg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_CAg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_CAg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_CAg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		case 8:
		{
			this.tiles[t].transform.Find("hex_FEg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_FEg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_FEg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_FEg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		case 9:
		{
			this.tiles[t].transform.Find("hex_LAg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_LAg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_LAg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_LAg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		case 10:
		{
			this.tiles[t].transform.Find("hex_SCg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_SCg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_SCg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_SCg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		case 11:
		{
			this.tiles[t].transform.Find("hex_EQg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_EQg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_EQg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_EQg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		case 12:
		{
			this.tiles[t].transform.Find("hex_AVg").gameObject.SetActive(true);
			string text = this.getDistToClosest(t, this.tileTypes[t] - 6).ToString();
			if (text == "99")
			{
				text = string.Empty;
				this.tiles[t].transform.Find("hex_AVg").Find("hex_pointer").localScale = Vector3.one * 0.3f;
			}
			else
			{
				this.tiles[t].transform.Find("hex_AVg").Find("hex_pointer").localScale = Vector3.one;
			}
			((Component)this.tiles[t].transform.Find("hex_AVg").Find("txtDistance")).GetComponent<TextMesh>().text = text;
			break;
		}
		}
	}

	public int getDistToClosest(int t, int c)
	{
		int num = t % 32;
		float num2 = (float)Mathf.FloorToInt((float)t / 32f);
		float num3 = 99f;
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (this.tileSolutions[i + j * 32] == c && this.tileSolutions[i + j * 32] != this.tileGuesses[i + j * 32])
				{
					float num4 = (float)Mathf.Abs(num - i);
					if (num4 > 16f)
					{
						num4 = 16f - (num4 - 16f);
					}
					float num5 = (float)j;
					float num6 = num2;
					if (i % 2 == 1)
					{
						num5 += 0.5f;
					}
					if (num % 2 == 1)
					{
						num6 += 0.5f;
					}
					float num7 = Mathf.Abs(num5 - num6);
					num7 -= 0.5f * num4;
					if (num7 < 0f)
					{
						num7 = 0f;
					}
					num4 += num7;
					if (num4 < num3)
					{
						num3 = num4;
					}
				}
			}
		}
		return Mathf.FloorToInt(num3);
	}

	public void setTileSolution(int t, int val, bool settingBulk = false)
	{
		this.tileSolutions[t] = val;
		if (!settingBulk)
		{
			this.updateTiles();
		}
	}

	public void setGuess(int t, int val, bool settingBulk = false)
	{
		this.tileGuesses[t] = val;
		if (!settingBulk)
		{
			this.updateTiles();
		}
	}

	public void updateTiles()
	{
		this.finishedWithAllColors = true;
		for (int i = 0; i < 6; i++)
		{
			this.finishedWithColor[i] = true;
			ResearchGrid.curColorsRequired[i] = 0;
		}
		for (int j = 0; j < this.tiles.Length; j++)
		{
			if (this.tileSolutions[j] == this.tileGuesses[j])
			{
				this.setTile(j, this.tileSolutions[j]);
			}
			else
			{
				if (this.tileSolutions[j] > 0)
				{
					this.finishedWithColor[this.tileSolutions[j] - 1] = false;
					ResearchGrid.curColorsRequired[this.tileSolutions[j] - 1]++;
					this.finishedWithAllColors = false;
				}
				if (this.tileGuesses[j] != 0)
				{
					this.setTile(j, this.tileGuesses[j] + 6);
				}
				else
				{
					this.setTile(j, 0);
				}
			}
		}
	}

	public void process()
	{
		this.gridVisible = (this.taskID == "MaterialSynthesisStation" || !UserSettings.needTutorial("NPT_COMPLETE_A_RESEARCH_PROJECT"));
		if (this.gridVisible)
		{
			if (!this.beaconMainSet)
			{
				this.beaconMain = ((Component)this.beacon.transform.Find("ParticleSystem")).GetComponent<ParticleSystem>().main;
				this.beaconMainSet = true;
			}
			this.GO.SetActive(true);
			if (this.taskID == "ChemicalConversion" && !this.colorSet)
			{
				switch (this.taskType)
				{
				case 0:
					this.beaconMain.startColor = ColorPicker.HexToColor("FF5151");
					break;
				case 1:
					this.beaconMain.startColor = ColorPicker.HexToColor("F68135");
					break;
				case 2:
					this.beaconMain.startColor = ColorPicker.HexToColor("F7DC43");
					break;
				case 3:
					this.beaconMain.startColor = ColorPicker.HexToColor("85F842");
					break;
				case 4:
					this.beaconMain.startColor = ColorPicker.HexToColor("4253F8");
					break;
				case 5:
					this.beaconMain.startColor = ColorPicker.HexToColor("A342F8");
					break;
				}
				this.colorSet = true;
			}
			this.processTileAnimation();
		}
		else
		{
			this.GO.SetActive(false);
		}
	}

	public void playSFX(string sfx, float v)
	{
		if (!(Time.time - this.timeSinceSFX > 0.05f) && !(sfx == "hologram_placement_good") && !(sfx == "research_color_complete"))
		{
			return;
		}
		this.audioSource.GetComponent<AudioSource>().PlayOneShot(Resources.Load(sfx) as AudioClip, v);
		this.timeSinceSFX = Time.time;
	}

	public void processTileAnimation()
	{
		float num = 99f;
		if (this.game.PC() != null)
		{
			num = (this.game.PC().GO.transform.position - this.GO.transform.position).magnitude;
			this.gridInUse = (num <= this.sphereRadius && !this.game.anyResearchHotspotBeingUsed);
		}
		else
		{
			this.gridInUse = false;
		}
		this.approach = Game.cap((24f - num) * 0.1f, 0f, 1f);
		if (this.lastGIU != this.gridInUse)
		{
			if (this.gridInUse)
			{
				this.updateTiles();
				this.playSFX("researchhotspot_on", 75f);
				this.beacon.GetComponentInChildren<ParticleSystem>().Stop();
				ResearchGrid.curID = this.taskID;
				ResearchGrid.curCategory = this.taskCategory;
				ResearchGrid.curCompletion = this.taskCompletion;
			}
			else
			{
				this.playSFX("researchhotspot_off", 435f);
				this.beacon.GetComponentInChildren<ParticleSystem>().Play();
			}
			this.lastGIU = this.gridInUse;
		}
		if (this.gridInUse)
		{
			this.gridUseAnimator += (1f - this.gridUseAnimator) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
			if ((UnityEngine.Object)this.beacon != (UnityEngine.Object)null)
			{
				this.beacon.transform.Find("panel").gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)this.beacon != (UnityEngine.Object)null)
			{
				((Component)this.beacon.transform.Find("panel").Find("txtComplete")).GetComponent<TextMesh>().text = Mathf.FloorToInt(this.taskCompletion * 100f) + "%";
			}
		}
		else
		{
			this.gridUseAnimator += (0f - this.gridUseAnimator) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			if ((UnityEngine.Object)this.beacon != (UnityEngine.Object)null)
			{
				this.beacon.transform.Find("panel").gameObject.SetActive(true);
			}
		}
		if ((UnityEngine.Object)this.pointer != (UnityEngine.Object)null && (UnityEngine.Object)this.beacon != (UnityEngine.Object)null)
		{
			this.pointer.LookAt(this.beacon.transform.position);
			this.pointer.gameObject.SetActive(!this.game.wasAnyResearchHotspotBeingUsed && this.taskID != "ChemicalConversion");
			this.v3 = this.pointer.localEulerAngles;
			this.v3.x = 0f;
			this.v3.z = 0f;
			this.pointer.localEulerAngles = this.v3;
			this.pointer.localScale = Vector3.one * (1f - Game.cap((25f - num) * 0.4f, 0f, 1f));
			((Component)this.pointer.Find("txt")).GetComponent<TextMesh>().text = Localization.getPhrase(Inventory.getItemDefinition(this.taskID).displayName, string.Empty);
			this.v3 = this.pointer.InverseTransformPoint(this.game.mainCam.transform.position);
			this.v32.y = 0.1950975f;
			this.v32.z = 0.1950975f;
			if (this.v3.x > 0f)
			{
				this.v32.x = 0.1950975f;
				this.pointer.Find("txt").localScale = this.v32;
				((Component)this.pointer.Find("txt")).GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;
			}
			else
			{
				this.v32.x = -0.1950975f;
				this.pointer.Find("txt").localScale = this.v32;
				((Component)this.pointer.Find("txt")).GetComponent<TextMesh>().anchor = TextAnchor.MiddleRight;
			}
		}
		bool flag = true;
		if (num >= 24f)
		{
			flag = false;
		}
		if (this.game.wasAnyResearchHotspotBeingUsed && !this.gridInUse)
		{
			flag = false;
		}
		if ((UnityEngine.Object)this.beacon != (UnityEngine.Object)null)
		{
			this.beacon.gameObject.SetActive(!this.game.wasAnyResearchHotspotBeingUsed || this.gridInUse);
		}
		this.tileContainer.gameObject.SetActive(flag);
		if (flag != this.lastST)
		{
			for (int i = 0; i < this.tiles.Length; i++)
			{
				if (flag)
				{
					this.tiles[i].transform.parent = this.tileContainer.transform;
				}
				else
				{
					this.tiles[i].transform.parent = this.game.World.transform.Find("UnusedResearchHexContainer");
				}
				this.tiles[i].SetActive(flag);
			}
			this.lastST = flag;
		}
		if (flag)
		{
			float num2 = 360f / this.cols;
			this.sphereRadius = 2.5f + 10f * this.gridUseAnimator;
			this.updateDelay = 0.05f;
			int num3 = -1;
			float num4 = 1250f;
			for (int j = 0; j < this.tiles.Length; j++)
			{
				float num5 = (float)j % this.cols;
				float num6 = Mathf.Floor((float)j / this.cols);
				float num7 = 0f;
				if (num5 % 2f == 1f)
				{
					num7 = 0.5f;
				}
				float num8 = num2 * (0f - num5);
				float num9 = 0.02f * Game.degreeDist(num8 + num6 * -9f, Time.timeSinceLevelLoad * 55f % 360f);
				float num10 = Math.Abs(num9 / 3.6f) - 0.5f;
				float num11 = 1f;
				if (num10 < 0f)
				{
					num11 = -1f;
				}
				num10 = (1f - Mathf.Pow(1f - Math.Abs(num10 * 2f), 2f)) * num11;
				float num12 = num10 * (1f - this.gridUseAnimator * 0.5f);
				this.v3.x = (this.sphereRadius + num12) * Mathf.Cos(num8 * 3.1415f / 180f) * (this.approach * this.approach * this.approach);
				this.v3.y = ((0f - this.tileHeight) * ((0f - (this.rows - 1f)) / 2f + num6 + num7) + this.gridY) * (1.5f - this.gridUseAnimator * 0.5f) * (this.approach * this.approach);
				this.v3.z = (this.sphereRadius + num12) * Mathf.Sin(num8 * 3.1415f / 180f) * (this.approach * this.approach * this.approach);
				this.tiles[j].transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f - num8 - 90f;
				this.v3.z = 270f;
				this.tiles[j].transform.localEulerAngles = this.v3;
				if (this.tileTypes[j] == 0 || !this.gridInUse)
				{
					this.v3.x = (this.v3.y = (this.v3.z = 0.45f + num10 * 0.2f));
					if (this.lastCTTC != -1)
					{
						this.v3 *= 0.3f;
					}
					if (!this.gridInUse)
					{
						this.v3 *= 0.5f;
					}
					Transform transform = this.tiles[j].transform;
					transform.localScale += (this.v3 - this.tiles[j].transform.localScale) * Game.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				else
				{
					this.v3.x = (this.v3.y = (this.v3.z = 0.9f + num10 * 0.1f));
					Transform transform2 = this.tiles[j].transform;
					transform2.localScale += (this.v3 - this.tiles[j].transform.localScale) * Game.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				if (this.gridInUse)
				{
					this.v3 = this.game.mainCam.transform.InverseTransformPoint(this.tiles[j].transform.position);
					if (this.v3.magnitude > this.game.camFollowDist && this.v3.z > 0f)
					{
						this.v3 = Game.gameInstance.worldToScreen(this.tiles[j].transform.position, false, 3000f);
						float num13 = (this.v3.x - 0.5f) * (this.v3.x - 0.5f) + (this.v3.y - 0.5f) * (this.v3.y - 0.5f);
						if (num13 < num4)
						{
							num4 = num13;
							num3 = j;
						}
					}
				}
				this.tiles[j].transform.Find("hex_selector").gameObject.SetActive(j == this.lastCTTC);
			}
			if (num3 != -1)
			{
				this.tiles[num3].transform.localScale = Vector3.one;
				if (Input.GetMouseButton(0))
				{
					if (Inventory.data.chemicals[ResearchGrid.selectedColor] >= 1f)
					{
						if (this.tileGuesses[num3] != ResearchGrid.selectedColor + 1 && (this.tileGuesses[num3] != this.tileSolutions[num3] || this.tileSolutions[num3] == 0))
						{
							bool flag2 = false;
							ResearchGrid.madeAnyGuesses = true;
							this.tileGuesses[num3] = ResearchGrid.selectedColor + 1;
							this.updateTiles();
							ResearchGrid.lastGuessWasGood = false;
							if (this.tileGuesses[num3] == this.tileSolutions[num3])
							{
								if (this.finishedWithAllColors)
								{
									flag2 = true;
								}
								else if (this.finishedWithColor[this.tileGuesses[num3] - 1])
								{
									this.playSFX("research_color_complete", 135f);
									flag2 = true;
									ResearchGrid.lastGuessWasGood = true;
								}
								else
								{
									this.playSFX("hologram_placement_good", 135f);
									ResearchGrid.lastGuessWasGood = true;
								}
							}
							else if (this.finishedWithColor[this.tileGuesses[num3] - 1])
							{
								this.playSFX("hologram_placement_bad", 135f);
								flag2 = true;
							}
							else
							{
								this.playSFX("hologram_placement_lead", 135f);
							}
							if (!flag2)
							{
								Inventory.data.chemicals[ResearchGrid.selectedColor] -= 1f;
								if (Inventory.data.chemicals[ResearchGrid.selectedColor] < 0f)
								{
									Inventory.data.chemicals[ResearchGrid.selectedColor] = 0f;
								}
							}
							Objectives.completeObjective("NPT_PLACE_A_RESEARCH_TILE");
							UserSettings.completeTutorial("NPT_PLACE_A_RESEARCH_TILE");
						}
					}
					else
					{
						if (Input.GetMouseButtonDown(0))
						{
							this.game.playSound("ui_error", 1f, 1f);
						}
						ResearchGrid.recentlyOutOfChemicals = 10f;
					}
				}
				else if (ResearchGrid.madeAnyGuesses)
				{
					int num14 = 0;
					while (num14 < Inventory.data.researchTasks.Count)
					{
						if (!(Inventory.data.researchTasks[num14].id == ResearchGrid.curID) || Inventory.data.researchTasks[num14].type != this.taskType)
						{
							num14++;
							continue;
						}
						int num15 = 0;
						int num16 = 0;
						for (int k = 0; k < this.tileGuesses.Length; k++)
						{
							Inventory.data.researchTasks[num14].guesses[k] = this.tileGuesses[k] - 1;
							if (this.tileSolutions[k] != 0)
							{
								num16++;
								if (this.tileGuesses[k] == this.tileSolutions[k])
								{
									num15++;
								}
							}
						}
						Inventory.data.researchTasks[num14].completionAmount = (float)num15 / (float)num16;
						Inventory.data.researchTasks[num14].completed = (num15 == num16);
						ResearchGrid.curCompletion = (this.taskCompletion = Inventory.data.researchTasks[num14].completionAmount);
						break;
					}
					Inventory.saveInventoryData();
					ResearchGrid.madeAnyGuesses = false;
				}
			}
			if (num3 != this.lastCTTC)
			{
				UISFX.playHover();
				this.lastCTTC = num3;
			}
		}
	}
}
