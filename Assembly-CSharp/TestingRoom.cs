using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingRoom : MonoBehaviour
{
	public string layoutName;

	private GameObject GO;

	private Game game;

	private RackCharacter pc;

	public static Transform labItemContainer;

	private bool playerInRoom;

	private Vector3 playerPositionInRoom;

	public static bool editingMode = false;

	public static string editItem = "Grabber";

	public static string lastEditItem = string.Empty;

	public static GameObject editItemBrush;

	public static Vector3 editItemOriginalRotation;

	public static int editOutVector;

	public static int editUpVector;

	public static LabItemDefinition currentLabItemDefinition;

	public static List<string> acceptablePlacementSurfacesForCurrentItem;

	private int id;

	public static Material bbMaterial;

	public static Material wireMaterial;

	public static Color goodColor = default(Color);

	public static Color badColor = default(Color);

	public static Color bbColor = default(Color);

	public static Color wireColor = default(Color);

	public static bool materialsInitialized = false;

	public static List<Renderer> colliderVisualizations;

	public static List<Renderer> brushVisualizations;

	public string badPlacementReason = string.Empty;

	public static float brushRotation = 0f;

	public static float effectiveBrushRotation = 0f;

	public static bool snapToGrid = false;

	public static bool inAnyRooms = false;

	public static string currentRoomLayoutName = string.Empty;

	private Vector3 v3;

	public static GameObject rotationIndicator;

	private float brushRotationVelocity = 5f;

	private int numAlreadyInRoom;

	private int numAlreadyInEntireLab;

	private bool overRoomBudget;

	private bool overTotalBuget;

	private int parentForWorkspaceObject = -1;

	private void Start()
	{
		if (!TestingRoom.materialsInitialized)
		{
			Shader shader = Shader.Find("Particles/Additive");
			TestingRoom.bbMaterial = new Material(shader);
			TestingRoom.wireMaterial = new Material(shader);
			TestingRoom.goodColor.r = 0.054f;
			TestingRoom.goodColor.g = 0.443f;
			TestingRoom.goodColor.b = 0.572f;
			TestingRoom.goodColor.a = 1f;
			TestingRoom.badColor.r = 1f;
			TestingRoom.badColor.g = 0.309f;
			TestingRoom.badColor.b = 0f;
			TestingRoom.badColor.a = 1f;
			TestingRoom.bbColor.r = 1f;
			TestingRoom.bbColor.g = 1f;
			TestingRoom.bbColor.b = 1f;
			TestingRoom.bbColor.a = 1f;
			TestingRoom.bbMaterial.SetTexture("_MainTex", Resources.Load("boundingBoxTexture") as Texture);
			TestingRoom.wireMaterial.SetTexture("_MainTex", Resources.Load("boundingBoxTexture") as Texture);
			TestingRoom.materialsInitialized = true;
		}
	}

	public static int getRecursiveLayoutItemIndex(Transform t, int attempts = 0)
	{
		if (attempts < 50 && !((Object)t == (Object)null))
		{
			if ((Object)((Component)t).GetComponent<LabItemInWorld>() != (Object)null)
			{
				return ((Component)t).GetComponent<LabItemInWorld>().index;
			}
			return TestingRoom.getRecursiveLayoutItemIndex(t.parent, attempts + 1);
		}
		Debug.Log("COULDN'T FIND RECURSIVE LAYOUT ITEM INDEX!");
		return -1;
	}

	private void Update()
	{
		this.badPlacementReason = string.Empty;
		if (TestingRoom.snapToGrid && !Input.GetKey(UserSettings.data.KEY_SNAP_TO_GRID))
		{
			TestingRoom.brushRotation = TestingRoom.effectiveBrushRotation;
		}
		TestingRoom.snapToGrid = Input.GetKey(UserSettings.data.KEY_SNAP_TO_GRID);
		if ((Object)this.game == (Object)null)
		{
			this.game = Game.gameInstance;
			this.GO = base.gameObject;
			if (this.GO.name == "Room0")
			{
				this.id = 0;
			}
			if (this.GO.name == "Room1")
			{
				this.id = 1;
			}
			if (this.GO.name == "Room2")
			{
				this.id = 2;
			}
			if (this.GO.name == "Pit")
			{
				this.id = 3;
			}
			return;
		}
		this.pc = this.game.PC();
		if (this.pc == null)
		{
			return;
		}
		this.playerPositionInRoom = base.transform.InverseTransformPoint(this.pc.GO.transform.position);
		this.playerInRoom = false;
		if (this.id < 3)
		{
			if (this.playerPositionInRoom.z < 20f && this.playerPositionInRoom.x < 40f && this.playerPositionInRoom.x > 0f && this.playerPositionInRoom.z > 0f)
			{
				this.playerInRoom = true;
			}
		}
		else
		{
			this.playerInRoom = (this.pc.standingOnSurface.name == "PitFloor" || this.pc.standingOnSurface.name == "Ramp" || this.pc.standingOnSurface.name == "LabRoomEntryway");
		}
		if (!this.playerInRoom)
		{
			return;
		}
		TestingRoom.currentRoomLayoutName = this.layoutName;
		TestingRoom.inAnyRooms = true;
		if (TestingRoom.editItem == "Grabber")
		{
			TestingRoom.editItem = string.Empty;
		}
		if (TestingRoom.editItem != TestingRoom.lastEditItem)
		{
			this.numAlreadyInEntireLab = 0;
			this.numAlreadyInRoom = 0;
			if (TestingRoom.editItem == string.Empty)
			{
				this.overRoomBudget = false;
				this.overTotalBuget = false;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					RoomLayout layoutByName = LayoutManager.getLayoutByName(LayoutManager.data.activeLayouts[i]);
					for (int j = 0; j < layoutByName.items.Count; j++)
					{
						if (layoutByName.items[j].assetName == TestingRoom.editItem || (Inventory.getItemDefinition(layoutByName.items[j].assetName).limitGroup == Inventory.getItemDefinition(TestingRoom.editItem).limitGroup && Inventory.getItemDefinition(TestingRoom.editItem).limitGroup != string.Empty))
						{
							if (i == this.id)
							{
								this.numAlreadyInRoom++;
							}
							this.numAlreadyInEntireLab++;
						}
					}
				}
				if (this.id == 3)
				{
					this.overRoomBudget = (this.numAlreadyInRoom >= Inventory.getItemDefinition(TestingRoom.editItem).limitInPit && Inventory.getItemDefinition(TestingRoom.editItem).limitInPit != 0);
				}
				else
				{
					this.overRoomBudget = (this.numAlreadyInRoom >= Inventory.getItemDefinition(TestingRoom.editItem).limitInRoom && Inventory.getItemDefinition(TestingRoom.editItem).limitInRoom != 0);
				}
				this.overTotalBuget = (this.numAlreadyInEntireLab >= Inventory.getItemDefinition(TestingRoom.editItem).limitOverall && Inventory.getItemDefinition(TestingRoom.editItem).limitOverall != 0);
			}
			if ((Object)TestingRoom.editItemBrush != (Object)null)
			{
				Object.Destroy(TestingRoom.editItemBrush);
				TestingRoom.editItemBrush = null;
			}
			if (TestingRoom.editItem != string.Empty)
			{
				TestingRoom.labItemContainer.Find(TestingRoom.editItem).gameObject.SetActive(true);
				TestingRoom.editItemBrush = Object.Instantiate(TestingRoom.labItemContainer.Find(TestingRoom.editItem).gameObject);
				TestingRoom.colliderVisualizations = new List<Renderer>();
				TestingRoom.brushVisualizations = new List<Renderer>();
				Collider[] componentsInChildren = TestingRoom.editItemBrush.GetComponentsInChildren<Collider>();
				for (int k = 0; k < componentsInChildren.Length; k++)
				{
					if (componentsInChildren[k].gameObject.layer != 10)
					{
						Object.Destroy(componentsInChildren[k]);
					}
				}
				ParticleSystem[] componentsInChildren2 = TestingRoom.editItemBrush.GetComponentsInChildren<ParticleSystem>();
				for (int l = 0; l < componentsInChildren2.Length; l++)
				{
					Object.Destroy(componentsInChildren2[l]);
				}
				for (int m = 0; m < TestingRoom.editItemBrush.GetComponentsInChildren<Renderer>().Length; m++)
				{
					TestingRoom.brushVisualizations.Add(TestingRoom.editItemBrush.GetComponentsInChildren<Renderer>()[m]);
				}
				TestingRoom.rotationIndicator = Object.Instantiate(TestingRoom.labItemContainer.Find("RotateRing").gameObject);
				TestingRoom.rotationIndicator.transform.parent = TestingRoom.editItemBrush.transform;
				TestingRoom.editItemOriginalRotation = TestingRoom.editItemBrush.transform.localRotation.eulerAngles;
				TestingRoom.labItemContainer.Find(TestingRoom.editItem).gameObject.SetActive(false);
				TestingRoom.editItemBrush.layer = 2;
				for (int n = 0; n < TestingRoom.editItemBrush.GetComponents<Collider>().Length; n++)
				{
					TestingRoom.editItemBrush.GetComponents<Collider>()[n].isTrigger = true;
				}
				for (int num = 0; num < TestingRoom.editItemBrush.GetComponentsInChildren<Light>().Length; num++)
				{
					if (TestingRoom.editItemBrush.GetComponentsInChildren<Light>()[num].intensity > 0.1f)
					{
						TestingRoom.editItemBrush.GetComponentsInChildren<Light>()[num].intensity = 0.1f;
					}
				}
				TestingRoom.editItemBrush.transform.Find("PlacementBounds").gameObject.AddComponent<HypotheticalItem>();
				TestingRoom.editItemBrush.transform.localPosition = Vector3.zero;
				int num2 = 0;
				while (num2 < Inventory.itemData.items.Count)
				{
					if (!(Inventory.itemData.items[num2].assetName == TestingRoom.editItem))
					{
						num2++;
						continue;
					}
					TestingRoom.currentLabItemDefinition = Inventory.itemData.items[num2];
					TestingRoom.acceptablePlacementSurfacesForCurrentItem = new List<string>();
					for (int num3 = 0; num3 < Inventory.itemData.items[num2].validSurfaces.Count; num3++)
					{
						if (Inventory.itemData.items[num2].validSurfaces[num3].valid)
						{
							TestingRoom.acceptablePlacementSurfacesForCurrentItem.Add(Inventory.itemData.items[num2].validSurfaces[num3].name);
						}
					}
					break;
				}
				BoxCollider[] components = TestingRoom.editItemBrush.GetComponents<BoxCollider>();
				for (int num4 = 0; num4 < components.Length; num4++)
				{
					GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gameObject.transform.parent = TestingRoom.editItemBrush.transform;
					gameObject.transform.localPosition = components[num4].center;
					gameObject.transform.localScale = components[num4].size;
					gameObject.GetComponent<Renderer>().material = TestingRoom.bbMaterial;
					TestingRoom.colliderVisualizations.Add(gameObject.GetComponent<Renderer>());
					Object.Destroy(gameObject.GetComponent<Collider>());
				}
			}
			else
			{
				TestingRoom.currentLabItemDefinition = null;
				TestingRoom.acceptablePlacementSurfacesForCurrentItem = new List<string>();
			}
			TestingRoom.lastEditItem = TestingRoom.editItem;
		}
		if (!TestingRoom.editingMode)
		{
			return;
		}
		if ((Object)TestingRoom.editItemBrush != (Object)null)
		{
			bool flag = true;
			if ((Object)TestingRoom.editItemBrush.transform.parent != (Object)null && TestingRoom.editItemBrush.transform.parent.name == base.transform.name)
			{
				flag = false;
			}
			if (flag)
			{
				TestingRoom.editItemBrush.transform.SetParent(base.transform);
			}
		}
		bool flag2;
		if (TestingRoom.editItem != string.Empty)
		{
			if (Input.GetKey(UserSettings.data.KEY_ROTATE_RIGHT) || (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(UserSettings.data.KEY_STRAFE_RIGHT)))
			{
				TestingRoom.brushRotation -= Time.deltaTime * this.brushRotationVelocity * 30f;
				Game.gameInstance.labEditRotateTickDelay -= Time.deltaTime;
			}
			else if (Input.GetKey(UserSettings.data.KEY_ROTATE_LEFT) || (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(UserSettings.data.KEY_STRAFE_LEFT)))
			{
				TestingRoom.brushRotation += Time.deltaTime * this.brushRotationVelocity * 30f;
				Game.gameInstance.labEditRotateTickDelay -= Time.deltaTime;
			}
			if (TestingRoom.brushRotation > TestingRoom.currentLabItemDefinition.rotationAllowed && TestingRoom.currentLabItemDefinition.rotationAllowed != 180f)
			{
				TestingRoom.brushRotation = TestingRoom.currentLabItemDefinition.rotationAllowed;
			}
			if (TestingRoom.brushRotation < 0f - TestingRoom.currentLabItemDefinition.rotationAllowed && TestingRoom.currentLabItemDefinition.rotationAllowed != 180f)
			{
				TestingRoom.brushRotation = 0f - TestingRoom.currentLabItemDefinition.rotationAllowed;
			}
			if (TestingRoom.snapToGrid)
			{
				TestingRoom.effectiveBrushRotation = Mathf.Round((TestingRoom.brushRotation + 360f) / 15f) * 15f - 360f;
			}
			else
			{
				TestingRoom.effectiveBrushRotation = TestingRoom.brushRotation;
			}
			flag2 = false;
			int num5 = 0;
			int num6 = 8;
			int num7 = 11;
			int num8 = 1 << num5;
			int num9 = 1 << num6;
			int num10 = 1 << num7;
			int layerMask = num8 | num9 | num10;
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.v3 = Input.mousePosition;
			}
			else
			{
				this.v3.x = (float)Screen.width * 0.6f;
				this.v3.y = (float)Screen.height / 2f;
				this.v3.z = 0f;
			}
			RaycastHit raycastHit = default(RaycastHit);
			if (Physics.Raycast(Game.gameInstance.mainCam.GetComponent<Camera>().ScreenPointToRay(this.v3), out raycastHit, 999f, layerMask))
			{
				TestingRoom.editItemBrush.transform.position = raycastHit.point;
				this.v3 = TestingRoom.editItemBrush.transform.localPosition;
				if (TestingRoom.snapToGrid)
				{
					if (raycastHit.collider.name != "CloseSideWall" && raycastHit.collider.name != "FarSideWall")
					{
						this.v3.x = Mathf.Round(this.v3.x);
					}
					if (raycastHit.collider.name != "Floor" && raycastHit.collider.name != "Ceiling")
					{
						this.v3.y = Mathf.Round(this.v3.y);
					}
					if (raycastHit.collider.name != "WallByDoor" && raycastHit.collider.name != "WindowWall" && raycastHit.collider.name != "BackWall")
					{
						this.v3.z = Mathf.Round(this.v3.z);
					}
				}
				TestingRoom.editItemBrush.transform.localPosition = this.v3;
				Vector3 worldUp = base.transform.up;
				if (raycastHit.collider.name == "Ceiling" || raycastHit.collider.name == "Floor" || raycastHit.collider.name == "PitFloor")
				{
					worldUp = base.transform.forward;
				}
				if (raycastHit.collider.name == "WorkspaceSurface")
				{
					this.parentForWorkspaceObject = TestingRoom.getRecursiveLayoutItemIndex(raycastHit.collider.transform, 0);
				}
				else
				{
					this.parentForWorkspaceObject = -1;
				}
				TestingRoom.editItemBrush.transform.LookAt(TestingRoom.editItemBrush.transform.position + raycastHit.normal, worldUp);
				TestingRoom.editItemBrush.transform.Rotate(0f, 0f, 0f - TestingRoom.effectiveBrushRotation);
				if (TestingRoom.acceptablePlacementSurfacesForCurrentItem.Contains(raycastHit.rigidbody.gameObject.name))
				{
					flag2 = true;
				}
			}
			if (flag2 && raycastHit.collider.name == "WorkspaceSurface" && Vector3.Angle(Vector3.up, raycastHit.normal) > 1f)
			{
				flag2 = false;
				this.badPlacementReason = Localization.getPhrase("THAT_ITEM_MUST_BE_PLACED", string.Empty) + " " + Localization.getPhrase(TestingRoom.currentLabItemDefinition.placementDescription, string.Empty);
			}
			if (!flag2)
			{
				this.badPlacementReason = Localization.getPhrase("THAT_ITEM_MUST_BE_PLACED", string.Empty) + " " + Localization.getPhrase(TestingRoom.currentLabItemDefinition.placementDescription, string.Empty);
			}
			if (flag2)
			{
				flag2 = (TestingRoom.editItemBrush.GetComponentInChildren<HypotheticalItem>().touchingThings == TestingRoom.currentLabItemDefinition.intersectionsAllowed);
				if (!flag2)
				{
					this.badPlacementReason = Localization.getPhrase("REQUIRES_MORE_SPACE", string.Empty);
				}
			}
			if (flag2 && raycastHit.collider.name != "WorkspaceSurface")
			{
				if (this.id == 3 && raycastHit.collider.name != "PitFloor")
				{
					goto IL_0ed7;
				}
				if (this.id != 3 && raycastHit.collider.name == "PitFloor")
				{
					goto IL_0ed7;
				}
			}
			goto IL_0eef;
		}
		goto IL_15bf;
		IL_0eef:
		if (flag2 && this.overRoomBudget)
		{
			flag2 = false;
			this.badPlacementReason = Localization.getPhrase("YOU_CANNOT_HAVE_ANY_MORE_OF_THESE_IN_THIS_ROOM", string.Empty);
		}
		if (flag2 && this.overTotalBuget)
		{
			flag2 = false;
			this.badPlacementReason = Localization.getPhrase("YOU_CANNOT_HAVE_ANY_MORE_OF_THESE_IN_THE_LABORATORY", string.Empty);
		}
		if (flag2)
		{
			if (Input.GetMouseButtonDown(0))
			{
				LayoutItem layoutItem = new LayoutItem();
				layoutItem.name = Localization.getPhrase(TestingRoom.currentLabItemDefinition.displayName, string.Empty);
				layoutItem.assetName = TestingRoom.currentLabItemDefinition.assetName;
				layoutItem.position = TestingRoom.editItemBrush.transform.localPosition;
				layoutItem.rotation = TestingRoom.editItemBrush.transform.localEulerAngles;
				switch (layoutItem.assetName)
				{
				case "MaterialSynthesisStation":
					Objectives.completeObjective("NPT_PLACE_MATERIAL_SYNTHESIS_STATION");
					UserSettings.completeTutorial("NPT_PLACE_MATERIAL_SYNTHESIS_STATION");
					break;
				case "CeilingLight":
				case "Spotlight":
					layoutItem.customProperties = new LayoutItemSpecialProperties();
					layoutItem.customProperties.color = default(Color);
					layoutItem.customProperties.color.r = 0.85f;
					layoutItem.customProperties.color.g = 0.95f;
					layoutItem.customProperties.color.b = 1f;
					break;
				case "ColoredCeilingLight":
				case "ColoredSpotlight":
					layoutItem.customProperties = new LayoutItemSpecialProperties();
					layoutItem.customProperties.color = default(Color);
					layoutItem.customProperties.color.r = 1f;
					layoutItem.customProperties.color.g = 0.2f;
					layoutItem.customProperties.color.b = 0f;
					break;
				}
				LayoutManager.addItemToLayout(layoutItem, this.layoutName, this.parentForWorkspaceObject);
				TestingRoom.lastEditItem = string.Empty;
				Game.gameInstance.needLabEditRebuild = true;
				Game.gameInstance.playSound("labEditPlaceItem", 1f, 1f);
				if (Game.gameInstance.itemsAvailableForLabEditingPlacementCounts[Game.gameInstance.curLabEditItem] == 1)
				{
					Game.gameInstance.curLabEditItem = 0;
				}
				Game.gameInstance.recentItemPlacement = 0f;
				return;
			}
			TestingRoom.bbColor.r += (TestingRoom.goodColor.r - TestingRoom.bbColor.r) * 0.1f;
			TestingRoom.bbColor.g += (TestingRoom.goodColor.g - TestingRoom.bbColor.g) * 0.1f;
			TestingRoom.bbColor.b += (TestingRoom.goodColor.b - TestingRoom.bbColor.b) * 0.1f;
			TestingRoom.rotationIndicator.gameObject.SetActive(true);
			TestingRoom.rotationIndicator.transform.Find("arrowsL").Rotate(0f, 0f, (0f - Time.deltaTime) * 85f);
			TestingRoom.rotationIndicator.transform.Find("arrowsR").Rotate(0f, 0f, (0f - Time.deltaTime) * 85f);
			TestingRoom.rotationIndicator.transform.Find("billboardL").LookAt(this.game.mainCam.transform.position);
			TestingRoom.rotationIndicator.transform.Find("billboardR").LookAt(this.game.mainCam.transform.position);
			this.v3.x = (this.v3.y = (this.v3.z = 0.7f + Mathf.Cos(Time.time * 5f) * 0.1f));
			TestingRoom.rotationIndicator.transform.Find("billboardL").localScale = this.v3;
			TestingRoom.rotationIndicator.transform.Find("billboardR").localScale = this.v3;
			TestingRoom.rotationIndicator.transform.LookAt(this.game.mainCam.transform.position);
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				Game.gameInstance.playSound("ui_error", 1f, 1f);
			}
			TestingRoom.bbColor.r += (TestingRoom.badColor.r - TestingRoom.bbColor.r) * 0.1f;
			TestingRoom.bbColor.g += (TestingRoom.badColor.g - TestingRoom.bbColor.g) * 0.1f;
			TestingRoom.bbColor.b += (TestingRoom.badColor.b - TestingRoom.bbColor.b) * 0.1f;
			TestingRoom.rotationIndicator.gameObject.SetActive(false);
		}
		TestingRoom.bbColor.a = (0.5f + Mathf.Cos(Time.time * 2f) / 3f) * 0.1f;
		for (int num11 = 0; num11 < TestingRoom.colliderVisualizations.Count; num11++)
		{
			TestingRoom.bbMaterial.SetColor("_TintColor", TestingRoom.bbColor);
			TestingRoom.colliderVisualizations[num11].material = TestingRoom.bbMaterial;
			TestingRoom.colliderVisualizations[num11].material.mainTextureOffset = new Vector2(Time.time * 0.025f, Time.time * 0.03f);
		}
		TestingRoom.wireColor.r = TestingRoom.bbColor.r;
		TestingRoom.wireColor.g = TestingRoom.bbColor.g;
		TestingRoom.wireColor.b = TestingRoom.bbColor.b;
		TestingRoom.wireColor.a = 0.75f + Mathf.Cos(Time.time * 2f) / 4f;
		for (int num12 = 0; num12 < TestingRoom.brushVisualizations.Count; num12++)
		{
			TestingRoom.wireMaterial.SetColor("_TintColor", TestingRoom.wireColor);
			TestingRoom.brushVisualizations[num12].material = TestingRoom.wireMaterial;
			TestingRoom.brushVisualizations[num12].material.mainTextureOffset = new Vector2(Time.time * 0.025f, Time.time * 0.03f);
		}
		goto IL_15bf;
		IL_0ed7:
		flag2 = false;
		this.badPlacementReason = Localization.getPhrase("ITEM_MUST_BE_PLACED_IN_THE_ROOM_YOURE_IN", string.Empty);
		goto IL_0eef;
		IL_15bf:
		if (this.badPlacementReason != string.Empty)
		{
			this.game.UI.transform.Find("txtPlacementNote").gameObject.SetActive(true);
			((Component)this.game.UI.transform.Find("txtPlacementNote")).GetComponent<Text>().text = this.badPlacementReason;
		}
	}
}
