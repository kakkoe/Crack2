using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	public static bool NPCsAllowed;

	public RackCharacter character;

	public string handle;

	public bool initted;

	public bool needRandomize;

	public Furniture startingFurniture;

	public bool postBuildSetupComplete;

	public float attentionToPlayer = 0.5f;

	public bool hasFocusObject;

	public string dialogueFile;

	public float talkRange = 7f;

	public string defaultFacialExpression = "seductive";

	public static bool buildingAnNPC;

	public static string curNPCbuild;

	public GameObject NPCparent;

	public string startingClothes = string.Empty;

	public string characterName = string.Empty;

	public string characterTitle = string.Empty;

	public string idleAnimation = string.Empty;

	private List<Vector3> waypoints;

	public int curWaypoint;

	public int tarWaypoint;

	public static int tourProgress;

	public static string clientManagerName;

	public static string chemistName;

	public static string requisitionsOfficerName;

	public static RackCharacter requisitionsOfficer;

	public float walkSpeed = 1f;

	public static bool waitingForBlackoutElevator;

	public string leftHandItem = string.Empty;

	public string rightHandItem = string.Empty;

	public static int queuedNPCsToBuild;

	private void Start()
	{
		if (NPC.NPCsAllowed)
		{
			this.hasFocusObject = ((Object)base.transform.Find("focus") != (Object)null);
			this.NPCparent = base.transform.parent.gameObject;
			base.transform.SetParent(GameObject.Find("World").transform);
			if (this.handle == "requisitionsofficer")
			{
				this.waypoints = new List<Vector3>();
				this.waypoints.Add(new Vector3(12f, 10.8f, -52f));
				this.waypoints.Add(new Vector3(10f, 10.8f, -79f));
				this.waypoints.Add(new Vector3(-18f, 10.8f, -79.7f));
				this.waypoints.Add(new Vector3(-24.5f, 10.85f, -87f));
				this.waypoints.Add(new Vector3(-19f, 10.7f, -99.6f));
				this.waypoints.Add(new Vector3(28f, 10.8f, -102f));
				this.waypoints.Add(new Vector3(10.7f, 10.8f, -100.7f));
				this.waypoints.Add(new Vector3(9.3f, 11f, -142.3f));
				this.waypoints.Add(new Vector3(11.27f, -4.4f, -154f));
				this.waypoints.Add(new Vector3(8.964f, -5.5f, -144f));
				this.waypoints.Add(new Vector3(8.75f, -5.5f, -133f));
				this.waypoints.Add(new Vector3(16.2f, -5.5f, -124f));
				this.waypoints.Add(new Vector3(52.24f, -4.9f, -156.6f));
				this.waypoints.Add(new Vector3(16.2f, -5.5f, -124f));
				this.waypoints.Add(new Vector3(-21f, -4.4f, -113f));
			}
		}
	}

	public bool openDialogue()
	{
		Game.gameInstance.curDialogue = this.dialogueFile;
		Game.dialoguePartner = this.character;
		Game.gameInstance.dialogueTree = Dialogue.importDialogue(this.dialogueFile, false);
		Game.gameInstance.updateDialogueResponseEligibility();
		Game.gameInstance.curDialogueNode = 0;
		return false;
	}

	private void rebuild()
	{
		Game.gameInstance.removeCharacter(this.character);
		this.initted = false;
		this.postBuildSetupComplete = false;
	}

	private void Update()
	{
		bool flag;
		if (NPC.NPCsAllowed && (Object)Game.gameInstance != (Object)null && Game.gameInstance.PC() != null)
		{
			if ((this.NPCparent.activeInHierarchy || (this.handle == "requisitionsofficer" && Inventory.getCharVar("tourCompleted") == 0f && !Game.gameInstance.customizingCharacter)) && !(Game.gameInstance.currentZone == "Room0") && !(Game.gameInstance.currentZone == "Room1") && !(Game.gameInstance.currentZone == "Room2"))
			{
				if (!this.initted)
				{
					if (!NPC.buildingAnNPC)
					{
						NPC.buildingAnNPC = true;
						NPC.curNPCbuild = this.handle;
						CharacterData nPCData = Inventory.getNPCData(this.handle);
						if (nPCData == null)
						{
							this.needRandomize = true;
							Game gameInstance = Game.gameInstance;
							CharacterData characterDefinition = new CharacterData();
							object spawnPoint = base.transform.position;
							Vector3 eulerAngles = base.transform.eulerAngles;
							this.character = new RackCharacter(gameInstance, characterDefinition, false, spawnPoint, eulerAngles.y, string.Empty);
						}
						else
						{
							Game gameInstance2 = Game.gameInstance;
							CharacterData characterDefinition2 = nPCData;
							object spawnPoint2 = base.transform.position;
							Vector3 eulerAngles2 = base.transform.eulerAngles;
							this.character = new RackCharacter(gameInstance2, characterDefinition2, false, spawnPoint2, eulerAngles2.y, string.Empty);
						}
						Game.gameInstance.addCharacter(this.character);
						this.character.npcData = this;
						this.initted = true;
					}
					else
					{
						NPC.queuedNPCsToBuild++;
					}
				}
				else if (!this.postBuildSetupComplete)
				{
					if ((Object)this.character.GO != (Object)null && this.character.initted && !this.character.rebuilding)
					{
						this.postBuildSetup();
					}
				}
				else
				{
					if (this.character.hidden)
					{
						RackCharacter rackCharacter = this.character;
						Vector3 position = base.transform.position;
						float x = position.x;
						Vector3 position2 = base.transform.position;
						float y = position2.y;
						Vector3 position3 = base.transform.position;
						rackCharacter.teleport(x, y, position3.z, -999f, false);
					}
					this.character.hidden = false;
					if (Inventory.getCharVar("secretaryAutoSpoken") == 0f && this.handle == "receptionist" && Game.gameInstance.curDialogue == string.Empty && (Game.gameInstance.PC().GO.transform.position - this.character.GO.transform.position).magnitude < 10f)
					{
						this.openDialogue();
						Game.gameInstance.PC().autoWalk(1.99f, 10.7f, -40.7f, 0f, 184f, 0f, null, 999f);
						Inventory.setCharVar("secretaryAutoSpoken", 1f);
					}
					if (this.handle == "requisitionsofficer" && Inventory.getCharVar("tourCompleted") == 0f)
					{
						if (Inventory.getCharVar("tourPhase") != 0f && Inventory.getCharVar("tourPhase") != 6f)
						{
							this.talkRange = 99999f;
						}
						if (Inventory.getCharVar("tourPhase") == 4f)
						{
							Vector3 position4 = Game.gameInstance.PC().GO.transform.position;
							if (position4.y > 0f)
							{
								this.talkRange = 1f;
							}
							else
							{
								this.talkRange = 99999f;
							}
						}
						if (Inventory.getCharVar("tourPhase") == 6f)
						{
							this.talkRange = 11f;
						}
						if (Inventory.getCharVar("secretaryGreeted") == 0f)
						{
							this.tarWaypoint = 3;
							this.curWaypoint = 3;
							this.character.teleport(-24.5f, 10.85f, -87f, -999f, true);
						}
						else
						{
							switch (NPC.tourProgress)
							{
							case 0:
								this.tarWaypoint = 0;
								break;
							case 1:
								this.tarWaypoint = 3;
								break;
							case 2:
								this.tarWaypoint = 5;
								break;
							case 3:
								this.tarWaypoint = 7;
								break;
							case 4:
								this.tarWaypoint = 8;
								break;
							case 5:
								this.tarWaypoint = 12;
								break;
							case 6:
								this.tarWaypoint = 14;
								break;
							}
						}
						if (!(Game.gameInstance.loadTransition <= 0.1f))
						{
							return;
						}
						if (Inventory.getCharVar("secretaryGreeted") != 1f)
						{
							return;
						}
						flag = ((this.character.GO.transform.position - this.waypoints[this.curWaypoint]).magnitude < 2.5f || (NPC.tourProgress == 5 && this.curWaypoint < 9));
						if (flag)
						{
							if (this.tarWaypoint != this.curWaypoint)
							{
								if (this.tarWaypoint > this.curWaypoint)
								{
									this.curWaypoint++;
								}
								else
								{
									this.curWaypoint--;
								}
								flag = false;
							}
							else if (Game.gameInstance.curDialogue == string.Empty && Inventory.getCharVar("tourPhase") != 0f && (Game.gameInstance.PC().GO.transform.position - this.character.GO.transform.position).magnitude < 10f && Inventory.getCharVar("tourPhase") != 4f)
							{
								this.openDialogue();
							}
						}
						else
						{
							Vector3 position5 = Game.gameInstance.PC().GO.transform.position;
							if (position5.z > -15f)
							{
								RackCharacter rackCharacter2 = this.character;
								Vector3 vector = this.waypoints[this.curWaypoint];
								float x2 = vector.x;
								Vector3 vector2 = this.waypoints[this.curWaypoint];
								float y2 = vector2.y;
								Vector3 vector3 = this.waypoints[this.curWaypoint];
								rackCharacter2.teleport(x2, y2, vector3.z, -999f, false);
							}
							if (this.curWaypoint == 8)
							{
								Vector3 position6 = this.character.GO.transform.position;
								if (position6.y > -3f)
								{
									if ((double)Game.gameInstance.blackAmount < 0.99)
									{
										NPC.waitingForBlackoutElevator = true;
									}
									else
									{
										NPC.waitingForBlackoutElevator = false;
										RackCharacter rackCharacter3 = this.character;
										Vector3 vector4 = this.waypoints[this.curWaypoint];
										float x3 = vector4.x;
										Vector3 vector5 = this.waypoints[this.curWaypoint];
										float y3 = vector5.y;
										Vector3 vector6 = this.waypoints[this.curWaypoint];
										rackCharacter3.teleport(x3, y3, vector6.z, -999f, false);
									}
									goto IL_07fc;
								}
							}
							RackCharacter rackCharacter4 = this.character;
							Vector3 vector7 = this.waypoints[this.curWaypoint];
							float x4 = vector7.x;
							Vector3 vector8 = this.waypoints[this.curWaypoint];
							float y4 = vector8.y;
							Vector3 vector9 = this.waypoints[this.curWaypoint];
							rackCharacter4.autoWalk(x4, y4, vector9.z, 0f, 0f, 0f, null, 999f);
						}
						goto IL_07fc;
					}
					if ((Game.gameInstance.PC().GO.transform.position - this.character.GO.transform.position).magnitude < this.talkRange && Game.gameInstance.curDialogue == string.Empty)
					{
						Game.gameInstance.context(Localization.getPhrase("TALK_TO", string.Empty) + " " + this.character.data.name, this.openDialogue, this.character.bones.Head.position, false);
					}
				}
				return;
			}
			if (this.initted)
			{
				this.character.hidden = true;
			}
		}
		return;
		IL_07fc:
		if (flag && Inventory.getCharVar("secretaryGreeted") != 0f && (Game.gameInstance.PC().GO.transform.position - this.character.GO.transform.position).magnitude < this.talkRange && Game.gameInstance.curDialogue == string.Empty)
		{
			Game.gameInstance.context(Localization.getPhrase("TALK_TO", string.Empty) + " " + this.character.data.name, this.openDialogue, this.character.bones.Head.position, false);
		}
	}

	public void postBuildSetup()
	{
		if (this.needRandomize)
		{
			switch (this.handle)
			{
			case "receptionist":
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("fox", 100f);
				RandomCharacterGenerator.addSpecies("deer", 100f);
				RandomCharacterGenerator.addSpecies("lynx", 100f);
				RandomCharacterGenerator.addSpecies("mouse", 100f);
				RandomCharacterGenerator.setGenderWeights(20f, 100f, 5f, 5f, 5f, 5f, 0f);
				RandomCharacterGenerator.setBodyTypeWeights(100f, 20f, 0f, 50f, 10f, 0f, 0f, 0f, 20f, 10f, 50f, 100f);
				break;
			case "hrmanager":
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("cat - siamese", 100f);
				RandomCharacterGenerator.addSpecies("otter", 100f);
				RandomCharacterGenerator.addSpecies("rat", 100f);
				RandomCharacterGenerator.addSpecies("wolf", 100f);
				RandomCharacterGenerator.setGenderWeights(100f, 50f, 5f, 5f, 5f, 5f, 0f);
				RandomCharacterGenerator.setBodyTypeWeights(100f, 20f, 5f, 60f, 50f, 20f, 10f, 5f, 50f, 30f, 50f, 50f);
				break;
			case "clientmanager":
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("bluejay", 100f);
				RandomCharacterGenerator.addSpecies("fennec", 100f);
				RandomCharacterGenerator.addSpecies("rabbit", 100f);
				RandomCharacterGenerator.addSpecies("tiger", 100f);
				RandomCharacterGenerator.setGenderWeights(100f, 50f, 5f, 5f, 5f, 5f, 0f);
				RandomCharacterGenerator.setBodyTypeWeights(100f, 20f, 5f, 60f, 50f, 20f, 10f, 5f, 50f, 30f, 50f, 50f);
				break;
			case "chemicalresearcher":
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("mouse", 100f);
				RandomCharacterGenerator.addSpecies("rat", 100f);
				RandomCharacterGenerator.addSpecies("cat - siamese", 100f);
				RandomCharacterGenerator.setGenderWeights(100f, 30f, 20f, 20f, 20f, 20f, 0f);
				RandomCharacterGenerator.setBodyTypeWeights(100f, 100f, 0f, 20f, 0f, 5f, 0f, 0f, 40f, 40f, 5f, 0f);
				break;
			case "requisitionsofficer":
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("bear", 100f);
				RandomCharacterGenerator.addSpecies("hyena", 100f);
				RandomCharacterGenerator.addSpecies("rat", 100f);
				RandomCharacterGenerator.setGenderWeights(100f, 10f, 5f, 5f, 5f, 5f, 0f);
				RandomCharacterGenerator.setBodyTypeWeights(100f, 5f, 0f, 20f, 10f, 30f, 10f, 0f, 40f, 30f, 5f, 0f);
				break;
			default:
				RandomCharacterGenerator.wipe();
				RandomCharacterGenerator.addSpecies("fox", 100f);
				RandomCharacterGenerator.addSpecies("deer", 100f);
				RandomCharacterGenerator.addSpecies("lynx", 100f);
				RandomCharacterGenerator.addSpecies("mouse", 100f);
				break;
			}
			RandomCharacterGenerator.randomize(this.character);
			List<string> list = new List<string>();
			switch (this.handle)
			{
			case "receptionist":
				if (!this.character.data.identifiesMale)
				{
					list.Add("Acacia");
					list.Add("Amaryllis");
					list.Add("Aster");
					list.Add("Azalea");
					list.Add("Blossom");
					list.Add("Clover");
					list.Add("Dahlia");
					list.Add("Daisy");
					list.Add("Heather");
					list.Add("Iris");
					list.Add("Ivy");
					list.Add("Jasmine");
					list.Add("Leilani");
					list.Add("Lilly");
					list.Add("Marigold");
					list.Add("Rose");
					list.Add("Violet");
					list.Add("Jade");
					list.Add("Amber");
					list.Add("Crystal");
					list.Add("Sapphira");
				}
				else
				{
					list.Add("Jasper");
					list.Add("Ari");
					list.Add("Colin");
					list.Add("Colt");
					list.Add("Aiden");
					list.Add("Jay");
					list.Add("Jonah");
					list.Add("Lionel");
					list.Add("Rudi");
					list.Add("Robin");
				}
				break;
			case "hrmanager":
				if (!this.character.data.identifiesMale)
				{
					list.Add("Ruth");
					list.Add("Ethel");
					list.Add("Marianne");
					list.Add("Lois");
					list.Add("Agatha");
					list.Add("Gwendoline");
					list.Add("Rosa");
					list.Add("Celia");
					list.Add("Elsa");
					list.Add("Edith");
					list.Add("Hazel");
					list.Add("Hermione");
					list.Add("Olive");
					list.Add("Ursula");
				}
				else
				{
					list.Add("Leonard");
					list.Add("Percival");
					list.Add("Harold");
					list.Add("Sid");
					list.Add("Alfred");
					list.Add("Barney");
					list.Add("Eugene");
					list.Add("Gus");
					list.Add("Gilbert");
					list.Add("Harvey");
					list.Add("Julius");
					list.Add("Milton");
					list.Add("Mortimer");
					list.Add("Murray");
					list.Add("Seymour");
					list.Add("Theodore");
					list.Add("Wilfred");
				}
				break;
			case "clientmanager":
				if (!this.character.data.identifiesMale)
				{
					list.Add("Beatrice");
					list.Add("Cordelia");
					list.Add("Eleanor");
					list.Add("Imogen");
					list.Add("Lydia");
					list.Add("Miriam");
					list.Add("Portia");
					list.Add("Winifred");
				}
				else
				{
					list.Add("Abraham");
					list.Add("Augustus");
					list.Add("Cornelius");
					list.Add("Gideon");
					list.Add("Leopold");
					list.Add("Solomon");
					list.Add("Truman");
				}
				break;
			case "chemicalresearcher":
				if (!this.character.data.identifiesMale)
				{
					list.Add("Hydra");
					list.Add("Lithia");
					list.Add("Nitra");
					list.Add("Fluori");
					list.Add("Mag");
					list.Add("Sili");
					list.Add("Pho");
					list.Add("Chlo");
					list.Add("Cal");
					list.Add("Scandi");
					list.Add("Vana");
				}
				else
				{
					list.Add("Helio");
					list.Add("Beryl");
					list.Add("Boro");
					list.Add("Oxy");
					list.Add("Neo");
					list.Add("Sodi");
					list.Add("Alu");
					list.Add("Sully");
					list.Add("Argo");
					list.Add("Potassi");
					list.Add("Titan");
					list.Add("Chrom");
					list.Add("Iro");
					list.Add("Coba");
					list.Add("Nick");
					list.Add("Copper");
					list.Add("Zin");
				}
				break;
			case "requisitionsofficer":
				if (!this.character.data.identifiesMale)
				{
					list.Add("Bell");
					list.Add("Hedy");
					list.Add("Bette");
					list.Add("Caresse");
					list.Add("Graves");
					list.Add("Beulah");
					list.Add("Sybilla");
				}
				else
				{
					list.Add("Edison");
					list.Add("Franklin");
					list.Add("Graham");
					list.Add("Carver");
					list.Add("Tesla");
					list.Add("Leonardo");
					list.Add("Morse");
					list.Add("Baird");
					list.Add("Archimedes");
					list.Add("Newton");
					list.Add("Watt");
					list.Add("Galilei");
					list.Add("Benz");
				}
				break;
			default:
				list.Add("Subject #" + (1000 + Mathf.FloorToInt(Random.value * 7000f)).ToString());
				break;
			}
			int index = Mathf.FloorToInt(Random.value * (float)list.Count);
			this.character.data.name = list[index];
			if (this.characterName != string.Empty)
			{
				this.character.data.name = this.characterName;
			}
			if (this.characterTitle != string.Empty)
			{
				this.character.data.title = this.characterTitle;
			}
			CharacterManager.exportCharacter(this.character, "NPC." + Inventory.data.characterName + "." + this.handle);
			Inventory.setNPCData(this.handle, "NPC." + Inventory.data.characterName + "." + this.handle);
			this.needRandomize = false;
		}
		else
		{
			string[] array = this.startingClothes.Split(',');
			if (UserSettings.data.mod_nudeNpcs)
			{
				array = new string[0];
			}
			this.character.breastsCoveredByClothing = false;
			this.character.crotchCoveredByClothing = false;
			for (int i = 0; i < array.Length; i++)
			{
				this.character.addClothingPiece(array[i]);
			}
			this.character.facialExpression = this.defaultFacialExpression;
			this.character.GO.transform.localEulerAngles = base.transform.localEulerAngles;
			if (this.handle == "clientmanager")
			{
				NPC.clientManagerName = this.character.data.name;
			}
			if (this.handle == "requisitionsofficer")
			{
				NPC.requisitionsOfficer = this.character;
				NPC.requisitionsOfficerName = this.character.data.name;
			}
			if (this.handle == "chemicalresearcher")
			{
				NPC.chemistName = this.character.data.name;
			}
			if (this.handle == "requisitionsofficer" && Inventory.getCharVar("tourCompleted") == 0f)
			{
				this.character.teleport(-24.5f, 10.85f, -87f, -999f, false);
			}
			else if ((Object)this.startingFurniture != (Object)null)
			{
				this.character.useFurniture(this.startingFurniture);
			}
			this.character.leftHandItem = this.leftHandItem;
			this.character.rightHandItem = this.rightHandItem;
			this.postBuildSetupComplete = true;
			NPC.buildingAnNPC = false;
		}
	}

	static NPC()
	{
		NPC.NPCsAllowed = true;
		NPC.curNPCbuild = string.Empty;
		NPC.clientManagerName = "Client Manager";
		NPC.chemistName = "Chemist";
		NPC.requisitionsOfficerName = "Requisitions Officer";
	}
}
