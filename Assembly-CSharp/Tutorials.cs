using System.Collections.Generic;
using UnityEngine;

public class Tutorials
{
	public static List<string> newPlayerTutorials = new List<string>();

	public static bool forcedTutorialFromTooltip = false;

	public static int newPlayerTutorialNeeded = 0;

	public static bool droneFollow = false;

	public static object objectivePosition;

	public static Vector3 helperDroneHome;

	public static int tutorialDroneAnchorX = 0;

	public static int tutorialDroneAnchorY = 0;

	public static Vector3 tutorialDronePosition;

	public static int tutorialDroneAdviceStep = 0;

	public static int tutorialDroneAdviceStepB = 0;

	public static string tutorialDroneAdvice = string.Empty;

	public static Game game;

	public static RackCharacter pc;

	public static bool allowTutorialDroneAdviceContinue = false;

	public static bool tutorialDroneAdviceInstant = false;

	public static int tutorialDroneForceTextX = 0;

	public static int tutorialDroneForceTextY = 0;

	public static float tutorialDroneArrowPosition = -1f;

	public static float tutorialDronePatience = 0f;

	public static string tutorialDroneImage = string.Empty;

	public static LayoutItemSpecialProperties props = new LayoutItemSpecialProperties();

	public static void init()
	{
		Tutorials.helperDroneHome.x = -21.53f;
		Tutorials.helperDroneHome.y = 0.15f;
		Tutorials.helperDroneHome.z = -118.25f;
		Tutorials.newPlayerTutorialNeeded = 0;
		Tutorials.newPlayerTutorials = new List<string>();
		Tutorials.newPlayerTutorials.Add("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
		Tutorials.newPlayerTutorials.Add("NPT_GO_TO_TEST_ROOM");
		Tutorials.newPlayerTutorials.Add("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
		Tutorials.newPlayerTutorials.Add("NPT_GREET_THE_SUBJECT");
		Tutorials.newPlayerTutorials.Add("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES");
		Tutorials.newPlayerTutorials.Add("NPT_AROUSE_THE_SUBJECT");
		Tutorials.newPlayerTutorials.Add("NPT_BRING_THE_SUBJECT_TO_ORGASM");
		Tutorials.newPlayerTutorials.Add("NPT_BRING_YOURSELF_TO_ORGASM");
		Tutorials.newPlayerTutorials.Add("NPT_DISMISS_THE_SUBJECT");
		Tutorials.newPlayerTutorials.Add("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION");
		Tutorials.newPlayerTutorials.Add("NPT_ENTER_RESEARCH_HOLOGRAM");
		Tutorials.newPlayerTutorials.Add("NPT_ENTER_A_RESEARCH_PROJECT");
		Tutorials.newPlayerTutorials.Add("NPT_PLACE_A_RESEARCH_TILE");
		Tutorials.newPlayerTutorials.Add("NPT_FIND_ALL_THE_RED_RESEARCH_NODES");
		Tutorials.newPlayerTutorials.Add("NPT_SWITCH_RESEARCH_COLORS");
		Tutorials.newPlayerTutorials.Add("NPT_COMPLETE_A_RESEARCH_PROJECT");
		Tutorials.newPlayerTutorials.Add("NPT_SELECT_A_PAYING_CUSTOMER");
		Tutorials.newPlayerTutorials.Add("NPT_COMPLETE_CUSTOMERS_DEMANDS");
		Tutorials.newPlayerTutorials.Add("NPT_DISMISS_PAYING_CUSTOMER");
		Tutorials.newPlayerTutorials.Add("NPT_SAVE_MONEY_FOR_MSS");
		Tutorials.newPlayerTutorials.Add("NPT_ORDER_MATERIAL_SYNTHESIS_STATION");
		Tutorials.newPlayerTutorials.Add("NPT_WAIT_FOR_DELIVERY");
		Tutorials.newPlayerTutorials.Add("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION");
		Tutorials.newPlayerTutorials.Add("NPT_ENTER_LAB_EDITING_MODE");
		Tutorials.newPlayerTutorials.Add("NPT_PLACE_MATERIAL_SYNTHESIS_STATION");
		Tutorials.newPlayerTutorials.Add("NPT_COMPLETE_THE_DILDO_RESEARCH_PROJECT");
		Tutorials.newPlayerTutorials.Add("NPT_CREATE_A_DILDO");
		Tutorials.newPlayerTutorials.Add("NPT_EQUIP_DILDO");
	}

	public static void completeAllNPTtutorials()
	{
		for (int i = 0; i < Tutorials.newPlayerTutorials.Count; i++)
		{
			Tutorials.completeTutorialIfActive(Tutorials.newPlayerTutorials[i]);
		}
	}

	public static void process()
	{
		Tutorials.game = Game.gameInstance;
		Tutorials.pc = Game.gameInstance.PC();
		Tutorials.tutorialDroneAdviceInstant = false;
		if (Tutorials.forcedTutorialFromTooltip)
		{
			Tutorials.droneFollow = true;
			Tutorials.allowTutorialDroneAdviceContinue = true;
			Tutorials.tutorialDroneArrowPosition = -1f;
			Tutorials.tutorialDroneForceTextX = 0;
			Tutorials.tutorialDroneForceTextY = 0;
			if (!Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1) && !Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1_ALT) && !Input.GetMouseButtonDown(0))
			{
				return;
			}
			UISFX.clickSFX(string.Empty);
			Tutorials.forcedTutorialFromTooltip = false;
		}
		else
		{
			Tutorials.droneFollow = false;
			Tutorials.objectivePosition = null;
			Tutorials.allowTutorialDroneAdviceContinue = false;
			Tutorials.tutorialDroneAdvice = string.Empty;
			Tutorials.tutorialDroneImage = string.Empty;
			Tutorials.tutorialDroneForceTextX = 0;
			Tutorials.tutorialDroneForceTextY = 0;
			Tutorials.tutorialDroneArrowPosition = -1f;
			if (Inventory.getCharVar("tourCompleted") == 1f && Tutorials.newPlayerTutorialNeeded < Tutorials.newPlayerTutorials.Count)
			{
				if (UserSettings.needTutorial("NPT_BRING_YOURSELF_TO_ORGASM") && !UserSettings.needTutorial("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT") && Tutorials.game.currentTestSubjects.Count < 1)
				{
					UserSettings.undoTutorial("NPT_BRING_YOURSELF_TO_ORGASM");
					Objectives.uncompleteObjective("NPT_BRING_YOURSELF_TO_ORGASM");
					UserSettings.undoTutorial("NPT_BRING_THE_SUBJECT_TO_ORGASM");
					Objectives.uncompleteObjective("NPT_BRING_THE_SUBJECT_TO_ORGASM");
					UserSettings.undoTutorial("NPT_AROUSE_THE_SUBJECT");
					Objectives.uncompleteObjective("NPT_AROUSE_THE_SUBJECT");
					UserSettings.undoTutorial("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES");
					Objectives.uncompleteObjective("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES");
					UserSettings.undoTutorial("NPT_GREET_THE_SUBJECT");
					Objectives.uncompleteObjective("NPT_GREET_THE_SUBJECT");
					UserSettings.undoTutorial("NPT_GO_TO_TEST_ROOM");
					Objectives.uncompleteObjective("NPT_GO_TO_TEST_ROOM");
					UserSettings.undoTutorial("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
					Objectives.uncompleteObjective("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
					Tutorials.newPlayerTutorialNeeded = 0;
				}
				if (Tutorials.game.currentTestSubjects.Count > 0 && UserSettings.needTutorial("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT"))
				{
					Objectives.completeObjective("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
					UserSettings.completeTutorial("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
				}
				if (Tutorials.game.currentTestSubjects.Count > 0 && UserSettings.needTutorial("NPT_SELECT_A_PAYING_CUSTOMER"))
				{
					Objectives.completeObjective("NPT_SELECT_A_PAYING_CUSTOMER");
					UserSettings.completeTutorial("NPT_SELECT_A_PAYING_CUSTOMER");
					Objectives.wipeCompletedObjectives();
				}
				if (UserSettings.needTutorial(Tutorials.newPlayerTutorials[Tutorials.newPlayerTutorialNeeded]))
				{
					string text = Tutorials.newPlayerTutorials[Tutorials.newPlayerTutorialNeeded];
					if (text != null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(28);
						dictionary.Add("NPT_EQUIP_DILDO", 0);
						dictionary.Add("NPT_CREATE_A_DILDO", 1);
						dictionary.Add("NPT_COMPLETE_THE_DILDO_RESEARCH_PROJECT", 2);
						dictionary.Add("NPT_PLACE_MATERIAL_SYNTHESIS_STATION", 3);
						dictionary.Add("NPT_ENTER_LAB_EDITING_MODE", 4);
						dictionary.Add("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION", 5);
						dictionary.Add("NPT_WAIT_FOR_DELIVERY", 6);
						dictionary.Add("NPT_ORDER_MATERIAL_SYNTHESIS_STATION", 7);
						dictionary.Add("NPT_SAVE_MONEY_FOR_MSS", 8);
						dictionary.Add("NPT_DISMISS_PAYING_CUSTOMER", 9);
						dictionary.Add("NPT_COMPLETE_CUSTOMERS_DEMANDS", 10);
						dictionary.Add("NPT_SELECT_A_PAYING_CUSTOMER", 11);
						dictionary.Add("NPT_COMPLETE_A_RESEARCH_PROJECT", 12);
						dictionary.Add("NPT_SWITCH_RESEARCH_COLORS", 13);
						dictionary.Add("NPT_FIND_ALL_THE_RED_RESEARCH_NODES", 14);
						dictionary.Add("NPT_PLACE_A_RESEARCH_TILE", 15);
						dictionary.Add("NPT_ENTER_A_RESEARCH_PROJECT", 16);
						dictionary.Add("NPT_ENTER_RESEARCH_HOLOGRAM", 17);
						dictionary.Add("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION", 18);
						dictionary.Add("NPT_DISMISS_THE_SUBJECT", 19);
						dictionary.Add("NPT_BRING_YOURSELF_TO_ORGASM", 20);
						dictionary.Add("NPT_BRING_THE_SUBJECT_TO_ORGASM", 21);
						dictionary.Add("NPT_AROUSE_THE_SUBJECT", 22);
						dictionary.Add("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES", 23);
						dictionary.Add("NPT_GREET_THE_SUBJECT", 24);
						dictionary.Add("NPT_BEGIN_INTERACTING_WITH_SUBJECT", 25);
						dictionary.Add("NPT_GO_TO_TEST_ROOM", 26);
						dictionary.Add("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT", 27);
						int num = default(int);
						if (dictionary.TryGetValue(text, out num))
						{
							switch (num)
							{
							case 0:
								Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && !Tutorials.game.inResearchMode);
								for (int k = 0; k < Tutorials.game.toolHotkeys.Count; k++)
								{
									if (Tutorials.game.toolHotkeys[k] == "Dildo")
									{
										Objectives.completeObjective("NPT_EQUIP_DILDO");
										UserSettings.completeTutorial("NPT_EQUIP_DILDO");
									}
								}
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_EQUIP_DILDO.A", string.Empty));
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_EQUIP_DILDO.B", string.Empty));
									Tutorials.allowContinue();
									break;
								case 2:
									if (Inventory.weHaveItem("Dildo", out Tutorials.props, string.Empty, false) || Tutorials.game.draggingInventoryItem == "Dildo")
									{
										if (Tutorials.game.inventoryOpen)
										{
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_EQUIP_DILDO.E", string.Empty));
											if (Tutorials.game.draggingInventoryItem == "Dildo")
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = 0;
												Tutorials.tutorialDronePosition.x = 45f;
												Tutorials.tutorialDronePosition.y = -128f;
												Tutorials.tutorialDroneArrowPosition = 155f;
											}
											else
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = 0;
												Tutorials.tutorialDronePosition.x = -56f;
												Tutorials.tutorialDronePosition.y = 132f;
												Tutorials.tutorialDroneArrowPosition = 285f;
											}
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_EQUIP_DILDO.D", string.Empty));
										}
									}
									else
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_EQUIP_DILDO.C", string.Empty));
									}
									break;
								}
								break;
							case 1:
								Tutorials.droneFollow = (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom);
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.A", string.Empty));
									Tutorials.allowContinue();
									Tutorials.tutorialDroneAdviceStepB = 0;
									break;
								case 1:
									Tutorials.objectivePosition = Game.locationOfNearestLabItem("MaterialSynthesisStation", Tutorials.game.camPos_actual);
									if (!LayoutManager.allLabItemNames.Contains("MaterialSynthesisStation"))
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.G", string.Empty));
										Tutorials.objectivePosition = Vector3.zero;
									}
									else if (Inventory.data.money < 55)
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.B", string.Empty));
									}
									else if (Tutorials.game.shopOpen && Shop.currentShop == "materialsynthesisstation")
									{
										switch (Tutorials.tutorialDroneAdviceStepB)
										{
										case 0:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -369f;
											Tutorials.tutorialDronePosition.y = -58f;
											Tutorials.tutorialDroneArrowPosition = 45f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.D", string.Empty));
											if (Tutorials.game.materialGridOpen)
											{
												Tutorials.tutorialDroneAdviceStepB = 1;
											}
											if (Tutorials.game.cartContents.Count > 0)
											{
												Tutorials.tutorialDroneAdviceStepB = 2;
											}
											break;
										case 1:
											if (Tutorials.game.materialGridOpen)
											{
												Tutorials.tutorialDroneAnchorX = 1;
												Tutorials.tutorialDroneAnchorY = 1;
												Tutorials.tutorialDronePosition.x = 500f;
												Tutorials.tutorialDronePosition.y = 500f;
												Tutorials.tutorialDroneAdvice = string.Empty;
											}
											else
											{
												Tutorials.tutorialDroneAnchorX = -1;
												Tutorials.tutorialDroneAnchorY = 0;
												Tutorials.tutorialDronePosition.x = 502f;
												Tutorials.tutorialDronePosition.y = 100f;
												Tutorials.tutorialDroneArrowPosition = 280f;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.E", string.Empty));
											}
											if (Tutorials.game.cartContents.Count > 0)
											{
												Tutorials.tutorialDroneAdviceStepB = 2;
											}
											break;
										case 2:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = -1;
											Tutorials.tutorialDronePosition.x = -404f;
											Tutorials.tutorialDronePosition.y = 145f;
											Tutorials.tutorialDroneArrowPosition = 160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.F", string.Empty));
											break;
										}
									}
									else
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_CREATE_A_DILDO.C", string.Empty));
									}
									break;
								}
								break;
							case 2:
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.droneFollow = (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom);
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_THE_DILDO_RESEARCH_PROJECT.A", string.Empty));
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && !Tutorials.game.UIinUse);
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_THE_DILDO_RESEARCH_PROJECT.B", string.Empty));
									break;
								case 2:
									Tutorials.droneFollow = false;
									break;
								}
								break;
							case 3:
							{
								Tutorials.droneFollow = (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom);
								if (!TestingRoom.editingMode)
								{
									UserSettings.undoTutorial("NPT_ENTER_LAB_EDITING_MODE");
									Objectives.uncompleteObjective("NPT_ENTER_LAB_EDITING_MODE");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								string a = string.Empty;
								if (TestingRoom.currentLabItemDefinition != null)
								{
									a = TestingRoom.currentLabItemDefinition.assetName;
								}
								if (a == "MaterialSynthesisStation")
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PLACE_MATERIAL_SYNTHESIS_STATION.A", string.Empty));
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PLACE_MATERIAL_SYNTHESIS_STATION.B", string.Empty));
								}
								break;
							}
							case 4:
								Tutorials.droneFollow = (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom);
								if (TestingRoom.editingMode)
								{
									Objectives.completeObjective("NPT_ENTER_LAB_EDITING_MODE");
									UserSettings.completeTutorial("NPT_ENTER_LAB_EDITING_MODE");
								}
								if (Tutorials.game.currentZone != "LabFloor" || Tutorials.game.PC().standingOnSurface.name.IndexOf("garage") != -1)
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_LAB_EDITING_MODE.A", string.Empty));
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_LAB_EDITING_MODE.B", string.Empty));
								}
								break;
							case 5:
								Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && !Tutorials.game.UIinUse);
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION.A", string.Empty));
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PICK_UP_MATERIAL_SYNTHESIS_STATION.B", string.Empty));
									break;
								}
								Tutorials.objectivePosition = new Vector3(-46.92f, -2.07f, -100.67f);
								break;
							case 6:
								Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && !Tutorials.game.UIinUse);
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_WAIT_FOR_DELIVERY.A", string.Empty));
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_WAIT_FOR_DELIVERY.B", string.Empty));
									Tutorials.allowContinue();
									break;
								case 2:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = 500f;
									Tutorials.tutorialDronePosition.y = 500f;
									Tutorials.tutorialDroneAdvice = string.Empty;
									break;
								}
								break;
							case 7:
								Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && Tutorials.game.curDialogue == string.Empty);
								if (Tutorials.game.shopOpen)
								{
									if (Tutorials.game.cartContents.Contains("MaterialSynthesisStation"))
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = -1;
										Tutorials.tutorialDronePosition.x = -480f;
										Tutorials.tutorialDronePosition.y = 88f;
										Tutorials.tutorialDroneArrowPosition = 105f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ORDER_MATERIAL_SYNTHESIS_STATION.Check out", string.Empty));
									}
									else
									{
										Tutorials.tutorialDroneAnchorX = -1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = 491f;
										Tutorials.tutorialDronePosition.y = -188f;
										Tutorials.tutorialDroneArrowPosition = 290f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ORDER_MATERIAL_SYNTHESIS_STATION.Add to cart", string.Empty));
									}
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ORDER_MATERIAL_SYNTHESIS_STATION.Got enough", string.Empty));
								}
								Tutorials.objectivePosition = new Vector3(-21.56f, 0f, -114.79f);
								break;
							case 8:
								Tutorials.droneFollow = ((Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom) && !Tutorials.game.UIinUse);
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									if (Tutorials.game.mostRecentSatisfactionRating >= 4)
									{
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SAVE_MONEY_FOR_MSS.A Good", string.Empty));
									}
									else
									{
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SAVE_MONEY_FOR_MSS.A Bad", string.Empty));
									}
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SAVE_MONEY_FOR_MSS.B", string.Empty));
									Tutorials.allowContinue();
									break;
								case 2:
									if (Inventory.data.money >= 2250)
									{
										Objectives.completeObjective("NPT_SAVE_MONEY_FOR_MSS");
										UserSettings.completeTutorial("NPT_SAVE_MONEY_FOR_MSS");
									}
									else
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SAVE_MONEY_FOR_MSS.C", string.Empty));
									}
									break;
								}
								break;
							case 9:
								Tutorials.droneFollow = true;
								if (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom)
								{
									Objectives.completeObjective("NPT_DISMISS_PAYING_CUSTOMER");
									UserSettings.completeTutorial("NPT_DISMISS_PAYING_CUSTOMER");
									Objectives.wipeCompletedObjectives();
								}
								else if (!(Tutorials.pc.orgasming > 0f) && !(Tutorials.game.currentTestSubjects[0].orgasming > 0f) && Tutorials.game.currentInteraction == null)
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_PAYING_CUSTOMER.Complete", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										if (Tutorials.pc.interactionSubject == null)
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -100f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_THE_SUBJECT.Go back to subject", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -86f;
											Tutorials.tutorialDronePosition.y = -68f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_THE_SUBJECT.Open dismiss menu", string.Empty));
											Tutorials.tutorialDroneArrowPosition = 45f;
											Tutorials.tutorialDroneForceTextX = -1;
											Tutorials.tutorialDroneForceTextY = -1;
										}
										break;
									}
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = 500f;
									Tutorials.tutorialDronePosition.y = 500f;
									Tutorials.tutorialDroneAdvice = string.Empty;
								}
								break;
							case 10:
								Tutorials.droneFollow = true;
								if (Tutorials.game.currentTestSubjects.Count < 1)
								{
									UserSettings.undoTutorial("NPT_SELECT_A_PAYING_CUSTOMER");
									Objectives.uncompleteObjective("NPT_SELECT_A_PAYING_CUSTOMER");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else
								{
									bool flag = true;
									int num2 = 0;
									while (num2 < Tutorials.game.currentTestSubjects[0].objectives.Count)
									{
										if (Tutorials.game.currentTestSubjects[0].objectives[num2].secret || Tutorials.game.currentTestSubjects[0].objectives[num2].completed)
										{
											num2++;
											continue;
										}
										flag = false;
										break;
									}
									if (flag && Tutorials.game.currentTestSubjects[0].objectives.Count > 0)
									{
										Objectives.completeObjective("NPT_COMPLETE_CUSTOMERS_DEMANDS");
										UserSettings.completeTutorial("NPT_COMPLETE_CUSTOMERS_DEMANDS");
									}
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_CUSTOMERS_DEMANDS.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = -1;
										Tutorials.tutorialDroneAnchorY = 0;
										Tutorials.tutorialDronePosition.x = 279f;
										Tutorials.tutorialDronePosition.y = 120f;
										Tutorials.tutorialDroneArrowPosition = 255f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_CUSTOMERS_DEMANDS.B", string.Empty));
										Tutorials.allowContinue();
										break;
									case 2:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_CUSTOMERS_DEMANDS.C", string.Empty));
										Tutorials.allowContinue();
										break;
									case 3:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_CUSTOMERS_DEMANDS.D", string.Empty));
										Tutorials.allowContinue();
										break;
									case 4:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_CUSTOMERS_DEMANDS.E", string.Empty));
										Tutorials.allowContinue();
										break;
									case 5:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = 500f;
										Tutorials.tutorialDronePosition.y = 500f;
										Tutorials.tutorialDroneAdvice = string.Empty;
										break;
									}
								}
								break;
							case 11:
								Tutorials.droneFollow = !Tutorials.game.UIinUse;
								if (Tutorials.game.currentTestSubjects.Count > 0)
								{
									Objectives.completeObjective("NPT_SELECT_A_PAYING_CUSTOMER");
									UserSettings.completeTutorial("NPT_SELECT_A_PAYING_CUSTOMER");
									Objectives.wipeCompletedObjectives();
								}
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SELECT_A_PAYING_CUSTOMER.A", string.Empty));
									Tutorials.allowContinue();
									break;
								case 1:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SELECT_A_PAYING_CUSTOMER.B", string.Empty));
									Tutorials.allowContinue();
									break;
								case 2:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SELECT_A_PAYING_CUSTOMER.C", string.Empty));
									Tutorials.allowContinue();
									break;
								case 3:
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SELECT_A_PAYING_CUSTOMER.D", string.Empty));
									break;
								}
								Tutorials.objectivePosition = new Vector3(33.94f, 14.61f, -99.17f);
								break;
							case 12:
								Tutorials.droneFollow = (Tutorials.game.currentZone == "Hologram" && Tutorials.game.wasAnyResearchHotspotBeingUsed);
								if (ResearchGrid.recentlyOutOfChemicals > 0f)
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPC_RESEARCH_OUT_OF_CHEMICALS.Good", string.Empty));
									ResearchGrid.recentlyOutOfChemicals -= Time.deltaTime;
								}
								else
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_A_RESEARCH_PROJECT.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = -1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = 70f;
										Tutorials.tutorialDronePosition.y = -70f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_A_RESEARCH_PROJECT.B", string.Empty));
										Tutorials.tutorialDroneArrowPosition = 315f;
										Tutorials.allowContinue();
										break;
									case 2:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_COMPLETE_A_RESEARCH_PROJECT.C", string.Empty));
										Tutorials.allowContinue();
										break;
									case 3:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = 500f;
										Tutorials.tutorialDronePosition.y = 500f;
										Tutorials.tutorialDroneAdvice = string.Empty;
										break;
									}
								}
								break;
							case 13:
								Tutorials.droneFollow = (Tutorials.game.currentZone == "Hologram" && Tutorials.game.wasAnyResearchHotspotBeingUsed);
								if (!Tutorials.game.wasAnyResearchHotspotBeingUsed)
								{
									UserSettings.undoTutorial("NPT_ENTER_A_RESEARCH_PROJECT");
									Objectives.uncompleteObjective("NPT_ENTER_A_RESEARCH_PROJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else if (ResearchGrid.selectedColor != 0)
								{
									Objectives.completeObjective("NPT_SWITCH_RESEARCH_COLORS");
									UserSettings.completeTutorial("NPT_SWITCH_RESEARCH_COLORS");
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_SWITCH_RESEARCH_COLORS.Switch", string.Empty));
								}
								break;
							case 14:
								Tutorials.droneFollow = (Tutorials.game.currentZone == "Hologram" && Tutorials.game.wasAnyResearchHotspotBeingUsed);
								if (ResearchGrid.recentlyOutOfChemicals > 0f)
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPC_RESEARCH_OUT_OF_CHEMICALS.Good", string.Empty));
									ResearchGrid.recentlyOutOfChemicals -= Time.deltaTime;
								}
								else if (!Tutorials.game.wasAnyResearchHotspotBeingUsed)
								{
									UserSettings.undoTutorial("NPT_ENTER_A_RESEARCH_PROJECT");
									Objectives.uncompleteObjective("NPT_ENTER_A_RESEARCH_PROJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else if (ResearchGrid.curColorsRequired[0] == 0)
								{
									Objectives.completeObjective("NPT_FIND_ALL_THE_RED_RESEARCH_NODES");
									UserSettings.completeTutorial("NPT_FIND_ALL_THE_RED_RESEARCH_NODES");
								}
								else if (ResearchGrid.lastGuessWasGood)
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_FIND_ALL_THE_RED_RESEARCH_NODES.Good", string.Empty));
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_FIND_ALL_THE_RED_RESEARCH_NODES.Bad", string.Empty));
								}
								break;
							case 15:
								Tutorials.droneFollow = (Tutorials.game.currentZone == "Hologram" && Tutorials.game.wasAnyResearchHotspotBeingUsed);
								if (!Tutorials.game.wasAnyResearchHotspotBeingUsed)
								{
									UserSettings.undoTutorial("NPT_ENTER_A_RESEARCH_PROJECT");
									Objectives.uncompleteObjective("NPT_ENTER_A_RESEARCH_PROJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else
								{
									if (ResearchGrid.curColorsRequired[0] == 0)
									{
										Objectives.completeObjective("NPT_PLACE_A_RESEARCH_TILE");
										UserSettings.completeTutorial("NPT_PLACE_A_RESEARCH_TILE");
									}
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PLACE_A_RESEARCH_TILE.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PLACE_A_RESEARCH_TILE.B", string.Empty));
										Tutorials.allowContinue();
										break;
									case 2:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_PLACE_A_RESEARCH_TILE.C", string.Empty));
										Tutorials.droneFollow = false;
										break;
									}
								}
								break;
							case 16:
								Tutorials.droneFollow = (Tutorials.game.currentZone == "Hologram");
								if (!Tutorials.game.inResearchMode)
								{
									UserSettings.undoTutorial("NPT_ENTER_RESEARCH_HOLOGRAM");
									Objectives.uncompleteObjective("NPT_ENTER_RESEARCH_HOLOGRAM");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else if (Tutorials.game.wasAnyResearchHotspotBeingUsed)
								{
									Objectives.completeObjective("NPT_ENTER_A_RESEARCH_PROJECT");
									UserSettings.completeTutorial("NPT_ENTER_A_RESEARCH_PROJECT");
								}
								else
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_A_RESEARCH_PROJECT.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_A_RESEARCH_PROJECT.B", string.Empty));
										break;
									}
									Tutorials.objectivePosition = new Vector3(15.96f, -2.89f, -145.05f);
								}
								break;
							case 17:
								Tutorials.droneFollow = (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom);
								if (Tutorials.game.inResearchMode)
								{
									Objectives.completeObjective("NPT_ENTER_RESEARCH_HOLOGRAM");
									UserSettings.completeTutorial("NPT_ENTER_RESEARCH_HOLOGRAM");
								}
								else
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_RESEARCH_HOLOGRAM.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_RESEARCH_HOLOGRAM.B", string.Empty));
										Tutorials.allowContinue();
										break;
									case 2:
										if (Tutorials.game.currentZone == "LabTowerLower")
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_RESEARCH_HOLOGRAM.C", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_ENTER_RESEARCH_HOLOGRAM.D", string.Empty));
										}
										break;
									}
									Tutorials.objectivePosition = new Vector3(15.96f, -2.89f, -145.05f);
								}
								break;
							case 18:
								Tutorials.droneFollow = true;
								if (Centrifuge.anythingActuallyProcessing)
								{
									Objectives.completeObjective("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION");
									UserSettings.completeTutorial("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION");
								}
								else
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION.A", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION.B", string.Empty));
										Tutorials.allowContinue();
										break;
									case 2:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION.C", string.Empty));
										Tutorials.allowContinue();
										break;
									case 3:
										if (Tutorials.game.currentZone == "LabTowerLower")
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION.E", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DROP_OFF_SPECIMEN_FOR_CONVERSION.D", string.Empty));
										}
										break;
									}
									Tutorials.objectivePosition = new Vector3(11.137f, -2.101f, -157.08f);
								}
								break;
							case 19:
								Tutorials.droneFollow = true;
								if (Tutorials.game.currentTestSubjects.Count == 0 || !Tutorials.game.inTestRoom)
								{
									Objectives.completeObjective("NPT_DISMISS_THE_SUBJECT");
									UserSettings.completeTutorial("NPT_DISMISS_THE_SUBJECT");
									Objectives.wipeCompletedObjectives();
								}
								else if (!(Tutorials.pc.orgasming > 0f) && Tutorials.game.currentInteraction == null)
								{
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_THE_SUBJECT.Complete", string.Empty));
										Tutorials.allowContinue();
										break;
									case 1:
										if (Tutorials.pc.interactionSubject == null)
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -100f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_THE_SUBJECT.Go back to subject", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -86f;
											Tutorials.tutorialDronePosition.y = -68f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_DISMISS_THE_SUBJECT.Open dismiss menu", string.Empty));
											Tutorials.tutorialDroneArrowPosition = 45f;
											Tutorials.tutorialDroneForceTextX = -1;
											Tutorials.tutorialDroneForceTextY = -1;
										}
										break;
									}
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = 500f;
									Tutorials.tutorialDronePosition.y = 500f;
									Tutorials.tutorialDroneAdvice = string.Empty;
								}
								break;
							case 20:
								Tutorials.droneFollow = true;
								if (Tutorials.pc.interactionSubject == null)
								{
									UserSettings.undoTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Objectives.uncompleteObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else
								{
									if (Tutorials.pc.orgasming > 0f)
									{
										Objectives.completeObjective("NPT_BRING_YOURSELF_TO_ORGASM");
										UserSettings.completeTutorial("NPT_BRING_YOURSELF_TO_ORGASM");
									}
									if (Tutorials.pc.interactionSubject.orgasming > 0f)
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = 500f;
										Tutorials.tutorialDronePosition.y = 500f;
										Tutorials.tutorialDroneAdvice = string.Empty;
									}
									else
									{
										switch (Tutorials.tutorialDroneAdviceStep)
										{
										case 0:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_YOURSELF_TO_ORGASM.Self orgasm A", string.Empty));
											Tutorials.allowContinue();
											break;
										case 1:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_YOURSELF_TO_ORGASM.Self orgasm B", string.Empty));
											Tutorials.allowContinue();
											break;
										case 2:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_YOURSELF_TO_ORGASM.Self orgasm C", string.Empty));
											Tutorials.allowContinue();
											break;
										case 3:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_YOURSELF_TO_ORGASM.Self orgasm D", string.Empty));
											Tutorials.allowContinue();
											break;
										case 4:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = 500f;
											Tutorials.tutorialDronePosition.y = 500f;
											Tutorials.tutorialDroneAdvice = string.Empty;
											break;
										}
									}
								}
								break;
							case 21:
								Tutorials.droneFollow = true;
								if (Tutorials.pc.interactionSubject == null)
								{
									UserSettings.undoTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Objectives.uncompleteObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else
								{
									if (Tutorials.pc.interactionSubject.orgasming > 0f)
									{
										Objectives.completeObjective("NPT_BRING_THE_SUBJECT_TO_ORGASM");
										UserSettings.completeTutorial("NPT_BRING_THE_SUBJECT_TO_ORGASM");
									}
									if (Tutorials.game.currentInteraction != null)
									{
										if (Tutorials.pc.interactionVigor < 4f)
										{
											Tutorials.tutorialDronePatience -= Time.deltaTime;
										}
										else
										{
											Tutorials.tutorialDronePatience = 3f;
										}
										if (Tutorials.tutorialDronePatience <= 0f)
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Slow up and down", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = 200f;
											Tutorials.tutorialDronePosition.y = 200f;
										}
									}
									else if (Tutorials.game.inlineDialogueOpen)
									{
										Tutorials.tutorialDroneAdvice = string.Empty;
									}
									else
									{
										switch (Tutorials.tutorialDroneAdviceStep)
										{
										case 0:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f + Tutorials.pc.up() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms A", string.Empty));
											break;
										case 1:
											Tutorials.tutorialDroneAdviceInstant = true;
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = 0;
											Tutorials.tutorialDronePosition.x = 328f;
											Tutorials.tutorialDronePosition.y = -18f;
											if (Input.GetMouseButtonDown(0))
											{
												Debug.Log(Tutorials.tutorialDronePosition);
											}
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f + Tutorials.pc.up() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms B", string.Empty));
											Tutorials.tutorialDroneImage = "hologram_tutorial" + Game.PathDirectorySeparatorChar + "hologram_tutorial0";
											break;
										case 2:
											Tutorials.tutorialDroneAdviceInstant = true;
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = 0;
											Tutorials.tutorialDronePosition.x = 125f;
											Tutorials.tutorialDronePosition.y = 80f;
											if (Input.GetMouseButtonDown(0))
											{
												Debug.Log(Tutorials.tutorialDronePosition);
											}
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f + Tutorials.pc.up() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms C", string.Empty));
											Tutorials.tutorialDroneImage = "hologram_tutorial" + Game.PathDirectorySeparatorChar + "hologram_tutorial1";
											break;
										case 3:
											Tutorials.tutorialDroneAdviceInstant = true;
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = 0;
											Tutorials.tutorialDronePosition.x = -245f;
											Tutorials.tutorialDronePosition.y = -173f;
											if (Input.GetMouseButtonDown(0))
											{
												Debug.Log(Tutorials.tutorialDronePosition);
											}
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f + Tutorials.pc.up() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms D", string.Empty));
											Tutorials.tutorialDroneImage = "hologram_tutorial" + Game.PathDirectorySeparatorChar + "hologram_tutorial2";
											break;
										case 4:
											Tutorials.tutorialDroneAdviceInstant = true;
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = 0;
											Tutorials.tutorialDronePosition.x = -156f;
											Tutorials.tutorialDronePosition.y = -114f;
											if (Input.GetMouseButtonDown(0))
											{
												Debug.Log(Tutorials.tutorialDronePosition);
											}
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f + Tutorials.pc.up() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramRealtime.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms E", string.Empty));
											Tutorials.tutorialDroneImage = "hologram_tutorial" + Game.PathDirectorySeparatorChar + "hologram_tutorial3";
											break;
										case 5:
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.allowContinue();
											Tutorials.game.forceCam((Tutorials.pc.interactionSubject.apparatus.hologramHarvest.position + (Tutorials.pc.bones.Head.position - Tutorials.pc.forward() * 2f) * 4f) / 5f, Tutorials.pc.interactionSubject.apparatus.hologramHarvest.position);
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Holograms F", string.Empty));
											break;
										case 6:
											if (Tutorials.game.curTool != "mouth")
											{
												Tutorials.tutorialDroneAnchorX = 1;
												Tutorials.tutorialDroneAnchorY = -1;
												Tutorials.tutorialDronePosition.x = -570f;
												Tutorials.tutorialDronePosition.y = 85f;
												Tutorials.tutorialDroneArrowPosition = 100f;
												Tutorials.tutorialDroneForceTextX = -1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Choose mouth", string.Empty));
											}
											else
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = 0;
												Tutorials.tutorialDronePosition.x = 80f;
												Tutorials.tutorialDronePosition.y = 115f;
												Tutorials.tutorialDroneArrowPosition = 210f;
												Tutorials.tutorialDroneForceTextX = 1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BRING_THE_SUBJECT_TO_ORGASM.Begin interacting", string.Empty));
											}
											break;
										}
									}
								}
								break;
							case 22:
								Tutorials.droneFollow = true;
								if (Tutorials.pc.interactionSubject == null)
								{
									UserSettings.undoTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Objectives.uncompleteObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else if (Tutorials.game.currentInteraction != null)
								{
									if (Tutorials.pc.interactionSubject.arousal < 0.6f)
									{
										if (Tutorials.pc.interactionVigor < 4f)
										{
											Tutorials.tutorialDronePatience -= Time.deltaTime;
										}
										else
										{
											Tutorials.tutorialDronePatience = 3f;
										}
										if (Tutorials.tutorialDronePatience <= 0f)
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = -100f;
											Tutorials.tutorialDronePosition.y = -160f;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_AROUSE_THE_SUBJECT.Slow circles", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = 1;
											Tutorials.tutorialDronePosition.x = 200f;
											Tutorials.tutorialDronePosition.y = 200f;
										}
									}
									else
									{
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_AROUSE_THE_SUBJECT.Switch to mouth", string.Empty));
									}
								}
								else if (Tutorials.game.inlineDialogueOpen)
								{
									Tutorials.tutorialDroneAdvice = string.Empty;
								}
								else
								{
									if (Tutorials.pc.interactionSubject.arousal >= 0.6f)
									{
										Objectives.completeObjective("NPT_AROUSE_THE_SUBJECT");
										UserSettings.completeTutorial("NPT_AROUSE_THE_SUBJECT");
									}
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.tutorialDroneAnchorX = 1;
										Tutorials.tutorialDroneAnchorY = 1;
										Tutorials.tutorialDronePosition.x = -100f;
										Tutorials.tutorialDronePosition.y = -160f;
										Tutorials.allowContinue();
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_AROUSE_THE_SUBJECT.Arouse", string.Empty));
										break;
									case 1:
										if (Tutorials.game.curTool != "hand")
										{
											Tutorials.tutorialDroneAnchorX = 1;
											Tutorials.tutorialDroneAnchorY = -1;
											Tutorials.tutorialDronePosition.x = -570f;
											Tutorials.tutorialDronePosition.y = 85f;
											Tutorials.tutorialDroneArrowPosition = 100f;
											Tutorials.tutorialDroneForceTextX = -1;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_AROUSE_THE_SUBJECT.Choose hands", string.Empty));
										}
										else
										{
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = 0;
											Tutorials.tutorialDronePosition.x = 80f;
											Tutorials.tutorialDronePosition.y = 115f;
											Tutorials.tutorialDroneArrowPosition = 210f;
											Tutorials.tutorialDroneForceTextX = 1;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_AROUSE_THE_SUBJECT.Begin interacting", string.Empty));
										}
										break;
									}
								}
								break;
							case 23:
								Tutorials.droneFollow = true;
								if (Tutorials.pc.interactionSubject == null)
								{
									UserSettings.undoTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Objectives.uncompleteObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else if (Tutorials.pc.emoteTime > 0f)
								{
									Tutorials.tutorialDroneAdvice = string.Empty;
									if (Tutorials.pc.interactionSubject.helloPhase == 3)
									{
										Tutorials.tutorialDroneAdviceStep = 4;
									}
								}
								else
								{
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									Tutorials.tutorialDroneAdviceInstant = true;
									if (Tutorials.game.wasInlineDialogueOpen)
									{
										if (Tutorials.game.inlineDialogueSubmenu == string.Empty)
										{
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = -1;
											Tutorials.tutorialDronePosition.x = 50f;
											Tutorials.tutorialDronePosition.y = 100f;
											Tutorials.tutorialDroneForceTextX = 1;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES.Ask", string.Empty));
										}
										else if (Tutorials.game.inlineDialogueSubmenu == "ask")
										{
											Tutorials.tutorialDroneAnchorX = 0;
											Tutorials.tutorialDroneAnchorY = -1;
											Tutorials.tutorialDronePosition.x = 170f;
											Tutorials.tutorialDronePosition.y = 150f;
											Tutorials.tutorialDroneForceTextX = 1;
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES.Pick interests", string.Empty));
											if (Tutorials.game.last_hoverInlineDialogueSubOption != -1)
											{
												try
												{
													if (Tutorials.game.inlineDialogueSubmenuSubOptions[Tutorials.game.activeInlineDialogueSubmenu][Tutorials.game.last_hoverInlineDialogueSubOption] == "inline_ask.ask_what_they_like")
													{
														Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Let go", string.Empty));
													}
												}
												catch
												{
												}
											}
										}
										else
										{
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Wrong category", string.Empty));
										}
									}
									else
									{
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES.Open wheel", string.Empty));
									}
								}
								break;
							case 24:
								Tutorials.droneFollow = true;
								if (Tutorials.pc.interactionSubject == null)
								{
									UserSettings.undoTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Objectives.uncompleteObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									Tutorials.newPlayerTutorialNeeded = 0;
								}
								else
								{
									if (Tutorials.tutorialDroneAdviceStep < 3)
									{
										for (int l = 0; l < Tutorials.game.currentTestSubjects.Count; l++)
										{
											Tutorials.game.currentTestSubjects[l].muteEmoteTime = 0.1f;
										}
									}
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -160f;
									switch (Tutorials.tutorialDroneAdviceStep)
									{
									case 0:
										Tutorials.allowContinue();
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Open inline dialogue A", string.Empty));
										break;
									case 1:
										Tutorials.allowContinue();
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Open inline dialogue B", string.Empty));
										break;
									case 2:
										Tutorials.allowContinue();
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Open inline dialogue C", string.Empty));
										break;
									case 3:
										Tutorials.tutorialDroneAdviceInstant = true;
										if (Tutorials.pc.emoteTime > 0f)
										{
											Tutorials.tutorialDroneAdvice = string.Empty;
											if (Tutorials.pc.interactionSubject.helloPhase == 3)
											{
												Tutorials.tutorialDroneAdviceStep = 4;
											}
										}
										else if (Tutorials.game.wasInlineDialogueOpen)
										{
											if (Tutorials.game.inlineDialogueSubmenu == string.Empty)
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = -1;
												Tutorials.tutorialDronePosition.x = 50f;
												Tutorials.tutorialDronePosition.y = 100f;
												Tutorials.tutorialDroneForceTextX = 1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Converse", string.Empty));
											}
											else if (Tutorials.game.inlineDialogueSubmenu == "converse")
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = -1;
												Tutorials.tutorialDronePosition.x = 170f;
												Tutorials.tutorialDronePosition.y = 150f;
												Tutorials.tutorialDroneForceTextX = 1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Pick greet", string.Empty));
												if (Tutorials.game.last_hoverInlineDialogueSubOption != -1)
												{
													try
													{
														if (Tutorials.game.inlineDialogueSubmenuSubOptions[Tutorials.game.activeInlineDialogueSubmenu][Tutorials.game.last_hoverInlineDialogueSubOption] == "inline_converse.greet")
														{
															Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Let go", string.Empty));
														}
													}
													catch
													{
													}
												}
											}
											else
											{
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Wrong category", string.Empty));
											}
										}
										else
										{
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Open inline dialogue D", string.Empty));
										}
										break;
									case 4:
										Tutorials.tutorialDroneAdviceInstant = true;
										if (Tutorials.pc.emoteTime > 0f)
										{
											Tutorials.tutorialDroneAdvice = string.Empty;
											if (Tutorials.pc.interactionSubject.friendlinessToPlayer >= 0.4f)
											{
												Objectives.completeObjective("NPT_GREET_THE_SUBJECT");
												UserSettings.completeTutorial("NPT_GREET_THE_SUBJECT");
											}
										}
										else if (Tutorials.game.wasInlineDialogueOpen)
										{
											if (Tutorials.game.inlineDialogueSubmenu == string.Empty)
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = -1;
												Tutorials.tutorialDronePosition.x = 50f;
												Tutorials.tutorialDronePosition.y = 100f;
												Tutorials.tutorialDroneForceTextX = 1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Encourage", string.Empty));
											}
											else if (Tutorials.game.inlineDialogueSubmenu == "encourage")
											{
												Tutorials.tutorialDroneAnchorX = 0;
												Tutorials.tutorialDroneAnchorY = -1;
												Tutorials.tutorialDronePosition.x = 170f;
												Tutorials.tutorialDronePosition.y = 150f;
												Tutorials.tutorialDroneForceTextX = 1;
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Pick compliment body", string.Empty));
												if (Tutorials.game.last_hoverInlineDialogueSubOption != -1)
												{
													try
													{
														if (Tutorials.game.inlineDialogueSubmenuSubOptions[Tutorials.game.activeInlineDialogueSubmenu][Tutorials.game.last_hoverInlineDialogueSubOption] == "inline_encourage.compliment_body")
														{
															Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Let go", string.Empty));
														}
													}
													catch
													{
													}
												}
											}
											else
											{
												Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Wrong category", string.Empty));
											}
										}
										else
										{
											Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GREET_THE_SUBJECT.Now encourage", string.Empty));
										}
										break;
									}
								}
								break;
							case 25:
								Tutorials.droneFollow = true;
								for (int j = 0; j < Tutorials.game.currentTestSubjects.Count; j++)
								{
									Tutorials.game.currentTestSubjects[j].muteEmoteTime = 0.1f;
								}
								Tutorials.tutorialDroneAnchorX = 1;
								Tutorials.tutorialDroneAnchorY = 1;
								Tutorials.tutorialDronePosition.x = -100f;
								Tutorials.tutorialDronePosition.y = -100f;
								if (Tutorials.pc.interactionSubject != null)
								{
									Objectives.completeObjective("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
									UserSettings.completeTutorial("NPT_BEGIN_INTERACTING_WITH_SUBJECT");
								}
								else
								{
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_BEGIN_INTERACTING_WITH_SUBJECT.Approach", string.Empty));
								}
								break;
							case 26:
								Tutorials.droneFollow = true;
								for (int i = 0; i < Tutorials.game.currentTestSubjects.Count; i++)
								{
									Tutorials.game.currentTestSubjects[i].muteEmoteTime = 0.1f;
								}
								Tutorials.tutorialDroneAnchorX = 1;
								Tutorials.tutorialDroneAnchorY = 1;
								Tutorials.tutorialDronePosition.x = -100f;
								Tutorials.tutorialDronePosition.y = -100f;
								Tutorials.objectivePosition = new Vector3(58.11f, -0.73f, -161.63f);
								if (Tutorials.game.currentZone == "Room0")
								{
									Objectives.completeObjective("NPT_GO_TO_TEST_ROOM");
									UserSettings.completeTutorial("NPT_GO_TO_TEST_ROOM");
								}
								else
								{
									switch (Tutorials.game.currentZone)
									{
									default:
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GO_TO_TEST_ROOM.Go down to testing room", string.Empty));
										break;
									case "LabFloor":
									case "LabTowerLower":
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_GO_TO_TEST_ROOM.Go to testing room", string.Empty));
										break;
									}
								}
								break;
							case 27:
								Tutorials.droneFollow = true;
								if (Tutorials.game.currentTestSubjects.Count > 0)
								{
									Objectives.completeObjective("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
									UserSettings.completeTutorial("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT");
								}
								if (!Tutorials.game.characterSelectorOpen)
								{
									Tutorials.tutorialDroneAdviceStep = 0;
								}
								switch (Tutorials.tutorialDroneAdviceStep)
								{
								case 0:
									switch (Tutorials.game.currentZone)
									{
									default:
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Go to CMC", string.Empty));
										break;
									case "LabFloor":
									case "LabTowerLower":
									case "Room0":
									case "Room1":
									case "Room2":
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Go up to CMC", string.Empty));
										break;
									case "LabEntrance":
										Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Talk to CMC", string.Empty));
										break;
									}
									Tutorials.tutorialDroneAnchorX = 1;
									Tutorials.tutorialDroneAnchorY = 1;
									Tutorials.tutorialDronePosition.x = -100f;
									Tutorials.tutorialDronePosition.y = -100f;
									if (Tutorials.game.characterSelectorOpen)
									{
										Tutorials.tutorialDroneAdviceStep = 1;
									}
									break;
								case 1:
									Tutorials.tutorialDroneAdviceInstant = true;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Wait", string.Empty));
									Tutorials.tutorialDroneAnchorX = -1;
									Tutorials.tutorialDroneAnchorY = -1;
									Tutorials.tutorialDronePosition.x = 100f;
									Tutorials.tutorialDronePosition.y = 100f;
									if (!Tutorials.game.anythingLoading && Tutorials.game.randomCharactersForSelection.Count > 0)
									{
										Tutorials.tutorialDroneAdviceStep = 2;
									}
									break;
								case 2:
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Use arrow keys", string.Empty));
									Tutorials.tutorialDroneAnchorX = -1;
									Tutorials.tutorialDroneAnchorY = -1;
									Tutorials.tutorialDronePosition.x = 100f;
									Tutorials.tutorialDronePosition.y = 100f;
									if (Tutorials.game.characterSelector_currentSelected != 0)
									{
										Tutorials.tutorialDroneAdviceStep = 3;
									}
									try
									{
										if (Tutorials.game.subjectRoomAssignments[Tutorials.game.characterSelector_currentSelected] != -1)
										{
											Tutorials.tutorialDroneAdviceStep = 4;
										}
									}
									catch
									{
									}
									break;
								case 3:
									Tutorials.tutorialDroneArrowPosition = 75f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Assign to chair", string.Empty));
									Tutorials.tutorialDroneAnchorX = 0;
									Tutorials.tutorialDroneAnchorY = 0;
									Tutorials.tutorialDronePosition.x = -8f;
									Tutorials.tutorialDronePosition.y = -30f;
									Tutorials.tutorialDroneForceTextX = -1;
									Tutorials.tutorialDroneForceTextY = -1;
									if (Tutorials.game.subjectRoomAssignments[Tutorials.game.characterSelector_currentSelected] != -1)
									{
										Tutorials.tutorialDroneAdviceStep = 4;
									}
									break;
								case 4:
									Tutorials.tutorialDroneArrowPosition = 75f;
									Tutorials.tutorialDroneAdvice = Game.dialogueFormat(Localization.getPhrase("NPT_TALK_TO_CLIENT_MANAGEMENT_CENTER_TO_PICK_A_SUBJECT.Prepare subjects", string.Empty));
									Tutorials.tutorialDroneAnchorX = 0;
									Tutorials.tutorialDroneAnchorY = 0;
									Tutorials.tutorialDronePosition.x = 94f;
									Tutorials.tutorialDronePosition.y = -184f;
									Tutorials.tutorialDroneForceTextX = -1;
									Tutorials.tutorialDroneForceTextY = -1;
									break;
								}
								Tutorials.objectivePosition = new Vector3(33.94f, 14.61f, -99.17f);
								break;
							}
						}
					}
					if (!Objectives.haveObjective(Tutorials.newPlayerTutorials[Tutorials.newPlayerTutorialNeeded]))
					{
						Objectives.addObjective(3, Tutorials.newPlayerTutorials[Tutorials.newPlayerTutorialNeeded], Game.dialogueFormat(Localization.getPhrase(Tutorials.newPlayerTutorials[Tutorials.newPlayerTutorialNeeded], string.Empty)), 0f, null, false, false, Tutorials.objectivePosition);
						Tutorials.tutorialDroneAdviceStep = 0;
						Tutorials.tutorialDroneAdviceStepB = 0;
					}
				}
				else
				{
					Tutorials.newPlayerTutorialNeeded++;
				}
			}
			if (!Tutorials.droneFollow)
			{
				Game.gameInstance.helperDrone.manualTarget = Tutorials.helperDroneHome;
			}
			else
			{
				Game.gameInstance.helperDrone.manualTarget = Vector3.zero;
			}
		}
	}

	public static void allowContinue()
	{
		Tutorials.allowTutorialDroneAdviceContinue = true;
		if (!Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1) && !Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1_ALT) && !Input.GetMouseButtonDown(1))
		{
			return;
		}
		UISFX.clickSFX(string.Empty);
		Tutorials.tutorialDroneAdviceStep++;
	}

	public static void allowContinueB()
	{
		Tutorials.allowTutorialDroneAdviceContinue = true;
		if (!Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1) && !Input.GetKeyDown(UserSettings.data.KEY_DIALOGUE1_ALT) && !Input.GetMouseButtonDown(1))
		{
			return;
		}
		UISFX.clickSFX(string.Empty);
		Tutorials.tutorialDroneAdviceStepB++;
	}

	public static void completeTutorialIfActive(string name)
	{
		if (UserSettings.needTutorial(name))
		{
			UserSettings.completeTutorial(name);
			TutorialTooltip.recentTutorialCompletion = 3;
		}
	}
}
