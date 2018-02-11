using System;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
	public static int nextUID = 0;

	public int uid;

	public Game game;

	public SexToy sexToy;

	public RackCharacter targetCharacter;

	public string targetNode;

	public bool hasPerformer;

	public bool requiresPCinteraction;

	public bool requiresMouseDown;

	public RackCharacter performingCharacter;

	public string performingNode;

	public bool alive = true;

	public string type;

	public GameObject gizmo;

	public Transform mountPoint;

	public bool isCurrentInteraction;

	public Vector3 v3 = default(Vector3);

	public Vector3 v32 = default(Vector3);

	public Vector3 gizmoStartPoint = default(Vector3);

	public float timeSpentInteracting;

	public float gizmoPosition = 1f;

	public float lastGizmoPosition = 1f;

	public float lastGizmoPositionWithinBounds;

	public float minGizmoPosition;

	public float maxGizmoPosition = 1f;

	private float angleBetween;

	private bool invertedHandPosition;

	private float inverter;

	private float iMY;

	private float handScale;

	private float extraDistanceFromTip;

	private Vector3 positioner = new Vector3(0f, 0f, 0f);

	public float gizmoPositionWithinBounds;

	public Vector3 v33 = default(Vector3);

	public float howFarIn;

	public float howFarInWorldUnits;

	private Vector3 angleOut;

	public float resistance;

	public float gizmoTargetPosition;

	public float pushingThroughResistance;

	public float penetrationGirth;

	public float previousGirth;

	public bool inside;

	public float staticResistance;

	public float penetrationSpeed = 6f;

	public float insertionOffset;

	public float forcedResistanceDelay;

	public float checkInvertPositionDelay;

	public float insideTime;

	public bool selfInteraction;

	public bool audioInitted;

	public float firstFrameAccelerator = 1f;

	public static string ineligibleReason = string.Empty;

	public AudioClip upSFX;

	public AudioClip downSFX;

	public AudioClip[] reverseUTDSFX;

	public AudioClip[] reverseDTUSFX;

	public int nextreverseUTDSFX;

	public int nextreverseDTUSFX;

	public bool reverseOnUpToDown;

	public bool reverseOnDownToUp;

	public float volMultiplier = 1f;

	public float rubSpeed;

	public float rubPos = 50f;

	public bool rubbingDown;

	public float rubDownVol;

	public float rubUpVol;

	public float UTDvolMultiplier = 1f;

	public float DTUvolMultiplier = 1f;

	public float oneShotVol;

	public static List<ToolMode> toolModes = new List<ToolMode>();

	public static bool stickyInteractions = false;

	public static float lastiMY = 0f;

	private float amountAvailable;

	public static float chemicalInjectionRate = 0f;

	public float lickOpenAmount;

	public float xFactor;

	private float insideAmount;

	private bool readyForResistanceSFX = true;

	public bool hilted;

	public float resetForcedResistanceDelay;

	public float girthAllowed = 0.08f;

	public static bool modeEligibleWithInteraction(Interaction interaction, int mode, out string butYouWillNeedToChangePoseToThis, bool switchingModes = false)
	{
		return Interaction.modeEligible(interaction.targetCharacter, interaction.performingCharacter, interaction.targetNode, interaction.performingNode, mode, out butYouWillNeedToChangePoseToThis, switchingModes, interaction.selfInteraction, interaction.uid);
	}

	public static bool modeEligible(RackCharacter targetCharacter, RackCharacter performingCharacter, string targetNode, string performingNode, int mode, out string butYouWillNeedToChangePoseToThis, bool switchingModes = false, bool selfInteraction = false, int uid = -1)
	{
		butYouWillNeedToChangePoseToThis = string.Empty;
		if ((UnityEngine.Object)targetCharacter.apparatus == (UnityEngine.Object)null)
		{
			return true;
		}
		if (performingCharacter == null)
		{
			return true;
		}
		bool result = true;
		string str = targetCharacter.apparatus.poseName + ".";
		str = ((performingCharacter.curSexPose != 0) ? (str + targetCharacter.apparatus.poseNames[performingCharacter.curSexPose - 1]) : (str + "default"));
		ToolMode mode2 = Interaction.getMode(performingNode, targetNode, mode);
		if (mode2 == null)
		{
			if (butYouWillNeedToChangePoseToThis == string.Empty)
			{
				Interaction.ineligibleReason = "INCOMPATIBLE_WITH_BONDAGE_APPARATUS";
				return false;
			}
		}
		else if (!mode2.compatiblePoses.Contains(str))
		{
			string poseName = performingCharacter.interactionApparatus.poseName;
			int num = 0;
			while (num < mode2.compatiblePoses.Count)
			{
				if (!(mode2.compatiblePoses[num].Split('.')[0] == poseName))
				{
					num++;
					continue;
				}
				butYouWillNeedToChangePoseToThis = mode2.compatiblePoses[num].Split('.')[1];
				break;
			}
			if (butYouWillNeedToChangePoseToThis == string.Empty)
			{
				Interaction.ineligibleReason = "INCOMPATIBLE_WITH_BONDAGE_APPARATUS";
				return false;
			}
		}
		if (!switchingModes)
		{
			int num2 = default(int);
			if (performingNode != null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
				dictionary.Add("footl", 0);
				dictionary.Add("footr", 1);
				dictionary.Add("balls", 2);
				dictionary.Add("clit", 3);
				dictionary.Add("penis", 4);
				dictionary.Add("vagina", 5);
				dictionary.Add("breastl", 6);
				dictionary.Add("breastr", 7);
				dictionary.Add("mouth", 8);
				dictionary.Add("tailhole", 9);
				if (dictionary.TryGetValue(performingNode, out num2))
				{
					switch (num2)
					{
					case 0:
						if (!(performingCharacter.sexToySlots[SexToySlots.FOOTL] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.FOOTL] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_FOOT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 1:
						if (!(performingCharacter.sexToySlots[SexToySlots.FOOTR] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.FOOTR] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_FOOT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 2:
						if (!(performingCharacter.sexToySlots[SexToySlots.BALLS] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.BALLS] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_BALLS_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 3:
						if (!(performingCharacter.sexToySlots[SexToySlots.CLIT] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.CLIT] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_CLIT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 4:
						if (!(performingCharacter.sexToySlots[SexToySlots.PENIS] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.PENIS] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_PENIS_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 5:
						if (!(performingCharacter.sexToySlots[SexToySlots.VAGINA] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.VAGINA] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_VAGINA_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 6:
						if (!(performingCharacter.sexToySlots[SexToySlots.BREASTL] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.BREASTL] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_BREAST_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 7:
						if (!(performingCharacter.sexToySlots[SexToySlots.BREASTR] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.BREASTR] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_BREAST_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 8:
						if (!(performingCharacter.sexToySlots[SexToySlots.MOUTH] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.MOUTH] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_MOUTH_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 9:
						if (!(performingCharacter.sexToySlots[SexToySlots.TAILHOLE] != string.Empty))
						{
							break;
						}
						if (!(performingCharacter.sexToySlots[SexToySlots.TAILHOLE] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "YOUR_TAILHOLE_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					}
				}
			}
			if (targetNode != null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
				dictionary.Add("footl", 0);
				dictionary.Add("footr", 1);
				dictionary.Add("balls", 2);
				dictionary.Add("clit", 3);
				dictionary.Add("penis", 4);
				dictionary.Add("vagina", 5);
				dictionary.Add("breastl", 6);
				dictionary.Add("breastr", 7);
				dictionary.Add("mouth", 8);
				dictionary.Add("tailhole", 9);
				if (dictionary.TryGetValue(targetNode, out num2))
				{
					switch (num2)
					{
					case 0:
						if (!(targetCharacter.sexToySlots[SexToySlots.FOOTL] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.FOOTL] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_FOOT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 1:
						if (!(targetCharacter.sexToySlots[SexToySlots.FOOTR] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.FOOTR] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_FOOT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 2:
						if (!(targetCharacter.sexToySlots[SexToySlots.BALLS] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.BALLS] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_BALLS_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 3:
						if (!(targetCharacter.sexToySlots[SexToySlots.CLIT] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.CLIT] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_CLIT_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 4:
						if (!(targetCharacter.sexToySlots[SexToySlots.PENIS] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.PENIS] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_PENIS_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 5:
						if (!(targetCharacter.sexToySlots[SexToySlots.VAGINA] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.VAGINA] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_VAGINA_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 6:
						if (!(targetCharacter.sexToySlots[SexToySlots.BREASTL] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.BREASTL] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_BREAST_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 7:
						if (!(targetCharacter.sexToySlots[SexToySlots.BREASTR] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.BREASTR] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_BREAST_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 8:
						if (!(targetCharacter.sexToySlots[SexToySlots.MOUTH] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.MOUTH] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_MOUTH_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					case 9:
						if (!(targetCharacter.sexToySlots[SexToySlots.TAILHOLE] != string.Empty))
						{
							break;
						}
						if (!(targetCharacter.sexToySlots[SexToySlots.TAILHOLE] != "interaction." + uid))
						{
							break;
						}
						Interaction.ineligibleReason = "THAT_TAILHOLE_ALREADY_BEING_USED_BY_SOMETHING_ELSE";
						return false;
					}
				}
			}
		}
		Interaction.ineligibleReason = "YOU_CANNOT_DO_THAT_RIGHT_NOW";
		switch (performingNode)
		{
		default:
			return true;
		case "handL":
		case "handR":
			if (targetNode != null && targetNode == "penis")
			{
				if (mode == Interaction.getModeIndexByName("handjob"))
				{
					result = (targetCharacter.arousal >= 0.3f || selfInteraction);
					Interaction.ineligibleReason = "YOU_MUST_AROUSE_THE_SUBJECT_FIRST";
					return result;
				}
				if (mode != Interaction.getModeIndexByName("polishcock"))
				{
					break;
				}
				result = (targetCharacter.targetStimulation < 0.3f || targetCharacter.orgasming > 0f || targetCharacter.refractory > targetCharacter.currentRefractoryDuration * 0.5f);
				Interaction.ineligibleReason = "SUBJECT_MUST_BE_SENSITIVE";
				return result;
			}
			break;
		case "butt":
			Interaction.removePlayerPants(performingCharacter);
			if (targetNode != null && targetNode == "penis" && mode == Interaction.getModeIndexByName("analridedick"))
			{
				result = (targetCharacter.arousal >= 0.6f);
				Interaction.ineligibleReason = "YOU_MUST_AROUSE_THE_SUBJECT_FIRST";
				return result;
			}
			break;
		case "mouth":
			if (targetNode != null && targetNode == "penis" && mode == Interaction.getModeIndexByName("blowjob"))
			{
				return true;
			}
			break;
		case "vagina":
			Interaction.removePlayerPants(performingCharacter);
			if (targetNode != null && targetNode == "penis" && mode == Interaction.getModeIndexByName("vaginalridedick"))
			{
				result = (targetCharacter.arousal >= 0.6f);
				Interaction.ineligibleReason = "YOU_MUST_AROUSE_THE_SUBJECT_FIRST";
				return result;
			}
			break;
		case "penis":
			if (!performingCharacter.showPenis)
			{
				Interaction.ineligibleReason = "YOU_DONT_HAVE_THE_CORRECT_ANATOMY_FOR_THAT";
				return false;
			}
			Interaction.removePlayerPants(performingCharacter);
			switch (targetNode)
			{
			case "tailhole":
				if (mode != Interaction.getModeIndexByName("fuckanus"))
				{
					break;
				}
				result = true;
				if (performingCharacter.arousal <= 0.8f)
				{
					performingCharacter.arousal = 0.8f;
				}
				return result;
			case "vagina":
				if (mode != Interaction.getModeIndexByName("fuckvagina"))
				{
					break;
				}
				result = true;
				if (performingCharacter.arousal <= 0.8f)
				{
					performingCharacter.arousal = 0.8f;
				}
				return result;
			case "mouth":
				if (mode != Interaction.getModeIndexByName("fuckmouth"))
				{
					break;
				}
				result = true;
				if (performingCharacter.arousal <= 0.8f)
				{
					performingCharacter.arousal = 0.8f;
				}
				return result;
			}
			break;
		}
		return result;
	}

	public static void removePlayerPants(RackCharacter character)
	{
		if (character.crotchCoveredByClothing && character.controlledByPlayer)
		{
			character.removeAnyClothesCoveringCrotch();
		}
	}

	public static int getModeIndexByName(string name)
	{
		for (int i = 0; i < Interaction.toolModes.Count; i++)
		{
			if (Interaction.toolModes[i].interaction == name)
			{
				return Interaction.toolModes[i].index;
			}
		}
		return 0;
	}

	public static ToolMode getMode(string tool, string node, int index)
	{
		if (tool == "handL" || tool == "handR")
		{
			tool = "hand";
		}
		if (node == "breastL" || node == "breastR")
		{
			tool = "breast";
		}
		if (node == "footL" || node == "footR")
		{
			tool = "foot";
		}
		for (int i = 0; i < Interaction.toolModes.Count; i++)
		{
			if (Interaction.toolModes[i].tool == tool && Interaction.toolModes[i].node == node && Interaction.toolModes[i].index == index)
			{
				return Interaction.toolModes[i];
			}
		}
		return null;
	}

	public static string getModeName(string tool, string node, int index)
	{
		for (int i = 0; i < Interaction.toolModes.Count; i++)
		{
			if (Interaction.toolModes[i].tool == tool && Interaction.toolModes[i].node == node && Interaction.toolModes[i].index == index)
			{
				return Interaction.toolModes[i].interaction;
			}
		}
		return string.Empty;
	}

	public static int getNumberOfModes(string tool, string node)
	{
		int num = 0;
		for (int i = 0; i < Interaction.toolModes.Count; i++)
		{
			if (Interaction.toolModes[i].tool == tool && Interaction.toolModes[i].node == node)
			{
				num++;
			}
		}
		return num;
	}

	public void initAudio(string _upSFX, string _downSFX, string _reverseUTDSFX, int _reverseUTDSFXvarieties, string _reverseDTUSFX, int _reverseDTUSFXvarieties, float vol, float UTDvol = 1f, float DTUvol = 1f, bool _reverseOnUpToDown = true, bool _reverseOnDownToUp = true)
	{
		this.UTDvolMultiplier = UTDvol;
		this.DTUvolMultiplier = DTUvol;
		this.reverseOnUpToDown = _reverseOnUpToDown;
		if (this.reverseOnUpToDown && _reverseUTDSFXvarieties > 0)
		{
			this.reverseUTDSFX = new AudioClip[_reverseUTDSFXvarieties];
			for (int i = 0; i < this.reverseUTDSFX.Length; i++)
			{
				this.reverseUTDSFX[i] = (Resources.Load(_reverseUTDSFX + i) as AudioClip);
			}
		}
		this.reverseOnDownToUp = _reverseOnDownToUp;
		if (this.reverseOnDownToUp && _reverseDTUSFXvarieties > 0)
		{
			this.reverseDTUSFX = new AudioClip[_reverseDTUSFXvarieties];
			for (int j = 0; j < this.reverseDTUSFX.Length; j++)
			{
				this.reverseDTUSFX[j] = (Resources.Load(_reverseDTUSFX + j) as AudioClip);
			}
		}
		this.volMultiplier = vol;
		this.gizmo.GetComponents<AudioSource>()[0].volume = 0f;
		this.gizmo.GetComponents<AudioSource>()[0].Stop();
		if (_downSFX != string.Empty)
		{
			this.downSFX = (Resources.Load(_downSFX) as AudioClip);
			this.gizmo.GetComponents<AudioSource>()[0].clip = this.downSFX;
			this.gizmo.GetComponents<AudioSource>()[0].Play();
		}
		this.gizmo.GetComponents<AudioSource>()[1].volume = 0f;
		this.gizmo.GetComponents<AudioSource>()[1].Stop();
		if (_upSFX != string.Empty)
		{
			this.upSFX = (Resources.Load(_upSFX) as AudioClip);
			this.gizmo.GetComponents<AudioSource>()[1].clip = this.upSFX;
			this.gizmo.GetComponents<AudioSource>()[1].Play();
			this.audioInitted = true;
		}
	}

	public void processAudio(float rubOutAmount, bool bidirectional = true)
	{
		rubOutAmount *= 100f;
		this.rubSpeed += (Mathf.Abs(rubOutAmount - this.rubPos) - this.rubSpeed) * Game.cap(Time.deltaTime * 16f, 0f, 1f);
		this.gizmo.GetComponents<AudioSource>()[2].volume = this.oneShotVol;
		if (!this.rubbingDown || !bidirectional)
		{
			this.gizmo.GetComponents<AudioSource>()[1].volume += (this.rubSpeed * this.volMultiplier * UserSettings.data.sexSquishNoiseVolume * 0.04f - this.gizmo.GetComponents<AudioSource>()[1].volume) * Game.cap(Time.deltaTime * 16f, 0f, 1f);
			this.gizmo.GetComponents<AudioSource>()[0].volume += (0f - this.gizmo.GetComponents<AudioSource>()[0].volume) * Game.cap(Time.deltaTime * 12f, 0f, 1f);
			this.oneShotVol += (this.gizmo.GetComponents<AudioSource>()[1].volume - this.oneShotVol) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			if (rubOutAmount > this.rubPos || !bidirectional)
			{
				this.rubPos = rubOutAmount;
			}
			if (rubOutAmount < this.rubPos - 5f)
			{
				if (this.reverseOnUpToDown && this.reverseUTDSFX != null)
				{
					this.gizmo.GetComponents<AudioSource>()[2].PlayOneShot(this.reverseUTDSFX[this.nextreverseUTDSFX], this.UTDvolMultiplier);
					if (this.reverseUTDSFX.Length > 1)
					{
						int num = this.nextreverseUTDSFX;
						while (this.nextreverseUTDSFX == num)
						{
							this.nextreverseUTDSFX = Mathf.FloorToInt(UnityEngine.Random.value * 1000f) % this.reverseUTDSFX.Length;
						}
					}
				}
				this.rubbingDown = true;
				this.rubPos = rubOutAmount;
			}
		}
		else
		{
			this.gizmo.GetComponents<AudioSource>()[0].volume += (this.rubSpeed * this.volMultiplier * UserSettings.data.sexSquishNoiseVolume * 0.04f - this.gizmo.GetComponents<AudioSource>()[0].volume) * Game.cap(Time.deltaTime * 16f, 0f, 1f);
			this.gizmo.GetComponents<AudioSource>()[1].volume += (0f - this.gizmo.GetComponents<AudioSource>()[1].volume) * Game.cap(Time.deltaTime * 12f, 0f, 1f);
			this.oneShotVol += (this.gizmo.GetComponents<AudioSource>()[0].volume - this.oneShotVol) * Game.cap(Time.deltaTime * 2f, 0f, 1f);
			if (rubOutAmount < this.rubPos)
			{
				this.rubPos = rubOutAmount;
			}
			if (rubOutAmount > this.rubPos + 5f)
			{
				if (this.reverseOnDownToUp && this.reverseDTUSFX != null)
				{
					this.gizmo.GetComponents<AudioSource>()[2].PlayOneShot(this.reverseDTUSFX[this.nextreverseDTUSFX], this.DTUvolMultiplier);
					if (this.reverseDTUSFX.Length > 1)
					{
						int num2 = this.nextreverseDTUSFX;
						while (this.nextreverseDTUSFX == num2)
						{
							this.nextreverseDTUSFX = Mathf.FloorToInt(UnityEngine.Random.value * 1000f) % this.reverseDTUSFX.Length;
						}
					}
				}
				this.rubbingDown = false;
				this.rubPos = rubOutAmount;
			}
		}
	}

	public static void getRelevantPreferenceFromInteraction(string interaction, out string pref1, out string pref2, out string pref3, out string pref4)
	{
		pref1 = string.Empty;
		pref2 = string.Empty;
		pref3 = string.Empty;
		pref4 = string.Empty;
		if (interaction != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(32);
			dictionary.Add("rubcocktipwithfingers_performing", 1);
			dictionary.Add("rubcocktipwithfingers_receiving", 2);
			dictionary.Add("handjob_performing", 3);
			dictionary.Add("handjob_receiving", 4);
			dictionary.Add("polishcock_performing", 5);
			dictionary.Add("polishcock_receiving", 6);
			dictionary.Add("clitrub_performing", 7);
			dictionary.Add("clitrub_receiving", 8);
			dictionary.Add("fingervagina_performing", 9);
			dictionary.Add("fingervagina_receiving", 10);
			dictionary.Add("fingeranus_performing", 11);
			dictionary.Add("fingeranus_receiving", 12);
			dictionary.Add("fuckanus_performing", 13);
			dictionary.Add("fuckanus_receiving", 14);
			dictionary.Add("analridedick_performing", 15);
			dictionary.Add("analridedick_receiving", 16);
			dictionary.Add("fuckvagina_performing", 17);
			dictionary.Add("fuckvagina_receiving", 18);
			dictionary.Add("vaginalridedick_performing", 19);
			dictionary.Add("vaginalridedick_receiving", 20);
			dictionary.Add("blowjob_performing", 21);
			dictionary.Add("blowjob_receiving", 22);
			dictionary.Add("dildoanus_performing", 23);
			dictionary.Add("dildoanus_receiving", 24);
			dictionary.Add("chemicalinjection_performing", 25);
			dictionary.Add("chemicalinjection_receiving", 26);
			dictionary.Add("cunnilingus_performing", 27);
			dictionary.Add("cunnilingus_receiving", 28);
			dictionary.Add("rimming_performing", 29);
			dictionary.Add("rimming_receiving", 30);
			dictionary.Add("fuckmouth_performing", 31);
			dictionary.Add("fuckmouth_receiving", 32);
			int num = default(int);
			if (dictionary.TryGetValue(interaction, out num))
			{
				switch (num)
				{
				case 1:
					pref1 = "handjob_giving";
					return;
				case 2:
					pref1 = "handjob_receiving";
					return;
				case 3:
					pref1 = "handjob_giving";
					return;
				case 4:
					pref1 = "handjob_receiving";
					return;
				case 5:
					pref1 = "handjob_giving";
					return;
				case 6:
					pref1 = "handjob_receiving";
					return;
				case 7:
					pref1 = "clitrub_giving";
					return;
				case 8:
					pref1 = "clitrub_receiving";
					return;
				case 9:
					pref1 = "vaginalfingering_giving";
					return;
				case 10:
					pref1 = "vaginalfingering_receiving";
					return;
				case 11:
					pref1 = "analfingering_giving";
					return;
				case 12:
					pref1 = "analfingering_receiving";
					return;
				case 13:
					pref1 = "analfucking_giving";
					return;
				case 14:
					pref1 = "analfucking_receiving";
					return;
				case 15:
					pref1 = "analfucking_receiving";
					return;
				case 16:
					pref1 = "analfucking_giving";
					return;
				case 17:
					pref1 = "vaginalfucking_giving";
					return;
				case 18:
					pref1 = "vaginalfucking_receiving";
					return;
				case 19:
					pref1 = "vaginalfucking_receiving";
					return;
				case 20:
					pref1 = "vaginalfucking_giving";
					return;
				case 21:
					pref1 = "blowjob_giving";
					return;
				case 22:
					pref1 = "blowjob_receiving";
					return;
				case 23:
					pref1 = "sextoy_general";
					return;
				case 24:
					pref1 = "sextoy_general";
					pref2 = "sextoy_anal";
					return;
				case 25:
					pref1 = "mad_science";
					return;
				case 26:
					pref1 = "mad_science";
					return;
				case 27:
					pref1 = "cunnilingus_giving";
					return;
				case 28:
					pref1 = "cunnilingus_receiving";
					return;
				case 29:
					pref1 = "rimming_giving";
					return;
				case 30:
					pref1 = "rimming_receiving";
					return;
				case 31:
					pref1 = "blowjob_receiving";
					return;
				case 32:
					pref1 = "blowjob_giving";
					return;
				}
			}
		}
		Debug.Log("Preference not found for interaction '" + interaction + "'");
	}

	public static void initSystem()
	{
		Interaction.toolModes = new List<ToolMode>();
		Interaction.addToolMode("hand", "penis", "rubcocktipwithfingers");
		Interaction.addCompatiblePose("RackChair.default", -1);
		Interaction.addCompatiblePose("Stocks.lifted", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("hand", "penis", "handjob");
		Interaction.addCompatiblePose("RackChair.lifted", -1);
		Interaction.addCompatiblePose("Stocks.lifted", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("hand", "penis", "polishcock");
		Interaction.addCompatiblePose("RackChair.default", -1);
		Interaction.addCompatiblePose("Stocks.lifted", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("hand", "clit", "clitrub");
		Interaction.addCompatiblePose("RackChair.default", -1);
		Interaction.addCompatiblePose("Stocks.lifted", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("hand", "vagina", "fingervagina");
		Interaction.addCompatiblePose("RackChair.lifted", -1);
		Interaction.addCompatiblePose("Stocks.default", -1);
		Interaction.addCompatiblePose("UpsideDown.lowered", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addToolMode("hand", "tailhole", "fingeranus");
		Interaction.addCompatiblePose("RackChair.lifted", -1);
		Interaction.addCompatiblePose("Stocks.default", -1);
		Interaction.addCompatiblePose("UpsideDown.lowered", -1);
		Interaction.addToolMode("penis", "tailhole", "fuckanus");
		Interaction.addCompatiblePose("RackChair.fuck", -1);
		Interaction.addCompatiblePose("Stocks.fuck", -1);
		Interaction.addToolMode("butt", "penis", "analridedick");
		Interaction.addCompatiblePose("RackChair.riding", -1);
		Interaction.addCompatiblePose("TableStraps.riding", -1);
		Interaction.addToolMode("penis", "vagina", "fuckvagina");
		Interaction.addCompatiblePose("RackChair.fuck", -1);
		Interaction.addCompatiblePose("Stocks.fuck", -1);
		Interaction.addCompatiblePose("TableStraps.missionary", -1);
		Interaction.addToolMode("penis", "mouth", "fuckmouth");
		Interaction.addCompatiblePose("UpsideDown.facefuck", -1);
		Interaction.addCompatiblePose("TableStraps.facefuck", -1);
		Interaction.addCompatiblePose("Stocks.facefuck", -1);
		Interaction.addToolMode("vagina", "penis", "vaginalridedick");
		Interaction.addCompatiblePose("RackChair.riding", -1);
		Interaction.addCompatiblePose("TableStraps.missionary", -1);
		Interaction.addToolMode("mouth", "penis", "blowjob");
		Interaction.addCompatiblePose("RackChair.eyelevel", -1);
		Interaction.addCompatiblePose("Stocks.highlift", -1);
		Interaction.addCompatiblePose("TableStraps.sixtynine", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("mouth", "vagina", "cunnilingus");
		Interaction.addCompatiblePose("RackChair.eyelevel", -1);
		Interaction.addCompatiblePose("Stocks.highliftBehind", -1);
		Interaction.addCompatiblePose("TableStraps.sixtynine", -1);
		Interaction.addCompatiblePose("UpsideDown.default", -1);
		Interaction.addToolMode("mouth", "tailhole", "rimming");
		Interaction.addCompatiblePose("RackChair.highlift", -1);
		Interaction.addCompatiblePose("Stocks.highliftBehind", -1);
		Interaction.addCompatiblePose("UpsideDown.behind", -1);
		Interaction.addToolMode("Dildo", "tailhole", "dildoanus");
		Interaction.addCompatiblePose("RackChair.lifted", -1);
		Interaction.addCompatiblePose("Stocks.default", -1);
		Interaction.addToolMode("ChemicalGun", "tailhole", "chemicalinjection");
		Interaction.addCompatiblePose("RackChair.lifted", -1);
		Interaction.addCompatiblePose("Stocks.default", -1);
		Interaction.addCompatiblePose("TableStraps.default", -1);
		Interaction.addCompatiblePose("UpsideDown.lowered", -1);
	}

	public static void addCompatiblePose(string pose, int toolMode = -1)
	{
		if (toolMode == -1)
		{
			toolMode = Interaction.toolModes.Count - 1;
		}
		Interaction.toolModes[toolMode].compatiblePoses.Add(pose);
	}

	public static void addToolMode(string tool, string node, string interaction)
	{
		int num = 0;
		for (int i = 0; i < Interaction.toolModes.Count; i++)
		{
			if (Interaction.toolModes[i].tool == tool && Interaction.toolModes[i].node == node)
			{
				num++;
			}
		}
		ToolMode toolMode = new ToolMode();
		toolMode.tool = tool;
		toolMode.node = node;
		toolMode.interaction = interaction;
		toolMode.index = num;
		Interaction.toolModes.Add(toolMode);
	}

	public static Interaction addInteraction(RackCharacter character, string node, string type)
	{
		if (type == string.Empty)
		{
			return null;
		}
		Interaction interaction = new Interaction();
		interaction.uid = Interaction.nextUID;
		Interaction.nextUID++;
		interaction.targetCharacter = character;
		interaction.targetNode = node;
		interaction.game = Game.gameInstance;
		interaction.type = type;
		interaction.game.interactions.Add(interaction);
		interaction.gizmo = UnityEngine.Object.Instantiate(TestingRoom.labItemContainer.Find("InteractionGizmo").gameObject);
		interaction.mountPoint = interaction.gizmo.transform.Find("mountPoint");
		interaction.gizmo.transform.SetParent(interaction.game.World.transform);
		interaction.gizmo.SetActive(true);
		return interaction;
	}

	public void claim(RackCharacter character, string node)
	{
		this.hasPerformer = true;
		this.performingCharacter = character;
		this.selfInteraction = (this.performingCharacter.uid == this.targetCharacter.uid);
		this.requiresPCinteraction = character.controlledByPlayer;
		if (node == "penis")
		{
			this.performingCharacter.pollPenisGirth(true);
		}
		this.performingNode = node;
		this.performingCharacter.assignToInteraction(this);
		this.performingCharacter.takeUpSlot(this.performingNode, this.uid);
		this.targetCharacter.takeUpSlot(this.targetNode, this.uid);
		this.performingCharacter.interactionStarted(this.type + "_performing");
		this.targetCharacter.interactionStarted(this.type + "_receiving");
		switch (node)
		{
		default:
			this.gizmoStartPoint = this.performingCharacter.bones.Hand_R.position;
			break;
		case "handL":
			this.gizmoStartPoint = this.performingCharacter.bones.Hand_L.position;
			break;
		case "penis":
			this.gizmoStartPoint = this.performingCharacter.bones.Root.position;
			break;
		case "butt":
			this.gizmoStartPoint = this.performingCharacter.tailholeEntrance.transform.position;
			break;
		case "mouth":
			this.gizmoStartPoint = this.performingCharacter.bones.Head.position;
			break;
		}
		this.sexToy = this.performingCharacter.getCurrentSexToy();
	}

	public void tracePositioner()
	{
		if (Input.GetKey(KeyCode.Y))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.positioner.x -= Time.deltaTime * 20f;
			}
			else
			{
				this.positioner.x += Time.deltaTime * 20f;
			}
		}
		if (Input.GetKey(KeyCode.U))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.positioner.y -= Time.deltaTime * 20f;
			}
			else
			{
				this.positioner.y += Time.deltaTime * 20f;
			}
		}
		if (Input.GetKey(KeyCode.I))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.positioner.z -= Time.deltaTime * 20f;
			}
			else
			{
				this.positioner.z += Time.deltaTime * 20f;
			}
		}
		Debug.Log(Math.Round((double)this.positioner.x) + "," + Math.Round((double)this.positioner.y) + "," + Math.Round((double)this.positioner.z));
	}

	public void process()
	{
		if (this.hasPerformer)
		{
			if (this.requiresMouseDown && !Interaction.stickyInteractions && !Input.GetMouseButton(0) && (this.game.PC().controlMode != 3 || !(this.game.PC().interactionHandleY < 1f)))
			{
				this.alive = false;
				this.game.PC().pleasureEyeCheckToggleCooldown = 0.25f;
				return;
			}
			if (this.requiresPCinteraction && this.targetCharacter == null)
			{
				this.alive = false;
				return;
			}
			if (this.performingCharacter == null)
			{
				this.alive = false;
				this.hasPerformer = false;
				return;
			}
			if ((UnityEngine.Object)this.performingCharacter.GO == (UnityEngine.Object)null)
			{
				this.alive = false;
				this.hasPerformer = false;
				return;
			}
		}
		if (this.alive)
		{
			switch (this.performingNode)
			{
			default:
				this.gizmoStartPoint = this.performingCharacter.startPosition_handR;
				break;
			case "handL":
				this.gizmoStartPoint = this.performingCharacter.startPosition_handL;
				break;
			case "penis":
				this.gizmoStartPoint = this.performingCharacter.startPosition_root;
				break;
			case "butt":
				this.gizmoStartPoint = this.performingCharacter.startPosition_tailhole;
				break;
			case "mouth":
				this.gizmoStartPoint = this.performingCharacter.startPosition_head;
				break;
			}
			string text = this.type;
			if (text != null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(17);
				dictionary.Add("rubcocktipwithfingers", 0);
				dictionary.Add("clitrub", 1);
				dictionary.Add("handjob", 2);
				dictionary.Add("polishcock", 3);
				dictionary.Add("spreadvagina", 4);
				dictionary.Add("fingeranus", 5);
				dictionary.Add("fingervagina", 6);
				dictionary.Add("fuckanus", 7);
				dictionary.Add("fuckvagina", 8);
				dictionary.Add("fuckmouth", 9);
				dictionary.Add("analridedick", 10);
				dictionary.Add("vaginalridedick", 11);
				dictionary.Add("blowjob", 12);
				dictionary.Add("dildoanus", 13);
				dictionary.Add("chemicalinjection", 14);
				dictionary.Add("cunnilingus", 15);
				dictionary.Add("rimming", 16);
				int num = default(int);
				if (dictionary.TryGetValue(text, out num))
				{
					switch (num)
					{
					case 0:
						this.processCockTeaseWithFingers();
						break;
					case 1:
						this.processClitRub();
						break;
					case 2:
						this.processHandjob();
						break;
					case 3:
						this.processCockPolishing();
						break;
					case 4:
						this.processVaginaSpread();
						break;
					case 5:
						this.processAnalFingering();
						break;
					case 6:
						this.processVaginalFingering();
						break;
					case 7:
						this.processFucking(0, this.targetCharacter.tailholeEntranceAfterIK.transform, this.targetCharacter.tailholeEntrance.transform.right, ref this.targetCharacter.currentTailholeTightness, this.targetCharacter.data.tailholeTightness, false);
						break;
					case 8:
						this.processFucking(1, this.targetCharacter.vaginalEntranceAfterIK.transform, this.targetCharacter.vaginaEntrance.transform.forward, ref this.targetCharacter.currentVaginalTightness, this.targetCharacter.data.vaginalTightness, false);
						break;
					case 9:
						this.processFucking(2, this.targetCharacter.throatHoleAfterIK.transform, this.targetCharacter.throatHoleAfterIK.transform.forward, ref this.targetCharacter.currentThroatTightness, 0f, false);
						break;
					case 10:
						this.processDickRiding(0, this.performingCharacter.tailholeEntranceAfterIK.transform, ref this.performingCharacter.currentTailholeTightness, this.performingCharacter.data.tailholeTightness);
						break;
					case 11:
						this.processDickRiding(1, this.performingCharacter.vaginalEntranceAfterIK.transform, ref this.performingCharacter.currentVaginalTightness, this.performingCharacter.data.vaginalTightness);
						break;
					case 12:
						this.processBlowjob();
						break;
					case 13:
						this.processFucking(0, this.targetCharacter.tailholeEntranceAfterIK.transform, this.targetCharacter.tailholeEntrance.transform.right, ref this.targetCharacter.currentTailholeTightness, this.targetCharacter.data.tailholeTightness, true);
						break;
					case 14:
						this.processChemicalInjection();
						break;
					case 15:
						this.processCunnilingus();
						break;
					case 16:
						this.processRimming();
						break;
					}
				}
			}
		}
		this.lastGizmoPosition = this.gizmoPosition;
		this.lastGizmoPositionWithinBounds = this.gizmoPositionWithinBounds;
		Interaction.lastiMY = this.iMY;
		this.timeSpentInteracting += Time.deltaTime;
		this.firstFrameAccelerator = 1f;
	}

	public void processChemicalInjection()
	{
		this.sexToy.playSLS = false;
		Interaction.chemicalInjectionRate = 0f;
		if (this.timeSpentInteracting > 0.1f && !Game.selectedChemicalWasJustForciblyChanged && UserSettings.data.selectedChemicalCompound != string.Empty)
		{
			this.amountAvailable = Inventory.getChemicalCompound(UserSettings.data.selectedChemicalCompound);
			if (this.amountAvailable > 0f)
			{
				Interaction.chemicalInjectionRate = Game.cap(1f - this.performingCharacter.interactionMY - 0.51f, 0f, 1f);
				float num = Interaction.chemicalInjectionRate * Time.deltaTime;
				if (num > this.amountAvailable)
				{
					num = this.amountAvailable;
				}
				this.targetCharacter.addChemicalCompound(UserSettings.data.selectedChemicalCompound, num);
				Inventory.addChemicalCompound(UserSettings.data.selectedChemicalCompound, 0f - num, false);
				this.sexToy.playSLS = (Interaction.chemicalInjectionRate > 0f);
				this.sexToy.SLSpitch = 1f + Interaction.chemicalInjectionRate * 0.5f;
				this.sexToy.SLSvol = 0.5f + Interaction.chemicalInjectionRate * 0.7f;
				this.sexToy.inUseLightIntensity = 0.4f + Interaction.chemicalInjectionRate * 1f;
			}
		}
		this.sexToy.showInUseLight = this.sexToy.playSLS;
		this.v3 = this.targetCharacter.bones.InjectionPoint.position;
		this.angleOut = (this.gizmoStartPoint - this.v3).normalized / 3f;
		this.v33 = this.v3 + this.angleOut.normalized * (this.sexToy.penetratorLength * this.performingCharacter.height_act) * (1.3f - Game.cap(1f - this.performingCharacter.interactionMY - 0.51f, -0.3f, 0f));
		this.sexToy.aimAt(this.v3, this.v33, this.mountPoint, true);
	}

	public void processBlowjob()
	{
		this.checkInvertPositionDelay -= Time.deltaTime;
		if (this.checkInvertPositionDelay <= 0f)
		{
			this.checkInvertPositionDelay += 3f;
			this.angleBetween = Vector3.Angle(-this.performingCharacter.bones.Root.right, this.targetCharacter.forward());
			this.invertedHandPosition = (this.angleBetween > 89f);
		}
		this.inverter = 1f;
		if (this.invertedHandPosition)
		{
			this.inverter = -1f;
		}
		this.iMY = this.performingCharacter.interactionMY;
		if (this.invertedHandPosition)
		{
			this.iMY = 1f - this.performingCharacter.interactionMY;
		}
		this.angleOut = this.targetCharacter.forwardAfterIK();
		this.v32 = this.targetCharacter.bones.Penis0.position + this.angleOut * this.gizmoPosition;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v32 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.gizmo.transform.rotation = this.targetCharacter.bones.Penis1.rotation;
		this.v3 = this.targetCharacter.bones.Penis1.InverseTransformPoint(this.performingCharacter.startPosition_head);
		Vector3 normalized = this.v3.normalized;
		Vector3 lossyScale = this.performingCharacter.bones.Head.lossyScale;
		this.v3 = normalized * lossyScale.x * 0.076f;
		this.mountPoint.localPosition = this.v3;
		this.mountPoint.LookAt(this.targetCharacter.bones.Penis0.position, -this.performingCharacter.bones.Neck.right);
		this.mountPoint.Rotate(180f, 0f, 90f + (this.performingCharacter.interactionMX - 0.5f) * 40f);
		float tongueScale = this.performingCharacter.tongueScale;
		Vector3 lossyScale2 = this.performingCharacter.bones.Head.lossyScale;
		this.minGizmoPosition = 0.375f * (tongueScale * lossyScale2.x);
		this.maxGizmoPosition = this.minGizmoPosition + this.targetCharacter.penisLengthInWorldUnits * 0.75f;
		if (this.maxGizmoPosition > 0.76f * this.performingCharacter.height_act)
		{
			this.maxGizmoPosition = 0.76f * this.performingCharacter.height_act;
		}
		if (this.maxGizmoPosition <= this.minGizmoPosition + 0.2f)
		{
			this.maxGizmoPosition = this.minGizmoPosition + 0.2f;
		}
		this.gizmoPosition = this.minGizmoPosition + Game.cap(1.5f - this.iMY * 2f, 0f, 1f) * (this.maxGizmoPosition - this.minGizmoPosition);
		this.gizmoPositionWithinBounds = Game.cap((this.gizmoPosition - this.minGizmoPosition) / (this.maxGizmoPosition - this.minGizmoPosition), 0f, 1f);
		float num = this.targetCharacter.girthAlongPenetrator(Game.cap((this.gizmoPosition - (this.minGizmoPosition + 0.01f)) / this.targetCharacter.penisLengthInWorldUnits, 0.1f, 1f), true) * 2.3f;
		Vector3 lossyScale3 = this.performingCharacter.bones.Head.lossyScale;
		float num2 = num / lossyScale3.x;
		this.mountPoint.Rotate(0f, num2 * 30f, 0f);
		float num3 = this.gizmoPosition - this.lastGizmoPosition;
		if (num3 < 0f)
		{
			num3 *= 0.3f;
		}
		this.performingCharacter.penetrateMouth(num2, num3);
		this.performingCharacter.bendNeckForBlowjob(this.iMY * -0.8f);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.65f);
		this.targetCharacter.dragPenis((this.gizmoPosition - this.lastGizmoPosition) * 2f, 0.5f);
		this.targetCharacter.setPenisAngle(this.performingCharacter.throatHoleAfterIK.transform.position, true);
		RackCharacter rackCharacter = this.targetCharacter;
		Vector3 position = this.performingCharacter.throatHoleAfterIKandSuckLock.transform.position;
		Vector3 a = this.angleOut * 0.11f;
		Vector3 lossyScale4 = this.performingCharacter.bones.Head.lossyScale;
		rackCharacter.buryPenis(position - a * lossyScale4.x, this.performingCharacter.bones.Belly.position, this.performingCharacter.throatHoleAfterIKandSuckLock.transform.position - this.performingCharacter.forward() + this.performingCharacter.up(), this.performingCharacter, 2);
		this.targetCharacter.dickSucker = this.performingCharacter;
		this.targetCharacter.wetness_penis += (1f - this.targetCharacter.wetness_penis) * Game.cap(Time.deltaTime * 0.3f, 0f, 1f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, string.Empty, 0, 0.9f, 1f, 1.9f, false, true);
		}
		this.processAudio(this.gizmoPositionWithinBounds, true);
	}

	public void processLickingMotion()
	{
		this.v3 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.startPosition_head) + (-this.targetCharacter.forwardAfterIK() - this.targetCharacter.upAfterIK() * 0.65f) * (0.25f + this.lickOpenAmount);
		Vector3 normalized = this.v3.normalized;
		float tongueScale = this.performingCharacter.tongueScale;
		Vector3 lossyScale = this.performingCharacter.bones.Head.lossyScale;
		this.v3 = normalized * (tongueScale * lossyScale.x) * (0.45f - this.lickOpenAmount * 0.15f);
		this.mountPoint.localPosition = this.v3;
		this.mountPoint.LookAt(this.gizmo.transform.position, -this.performingCharacter.bones.Neck.right);
		this.mountPoint.Rotate(180f, 0f, 90f);
		this.minGizmoPosition = 0f;
		this.maxGizmoPosition = 1f;
		this.gizmoPosition = this.minGizmoPosition + Game.cap(1.5f - this.iMY * 2f, 0f, 1f) * (this.maxGizmoPosition - this.minGizmoPosition);
		this.gizmoPositionWithinBounds = Game.cap((this.gizmoPosition - this.minGizmoPosition) / (this.maxGizmoPosition - this.minGizmoPosition), 0f, 1f);
		if (this.iMY < Interaction.lastiMY)
		{
			this.lickOpenAmount -= (this.iMY - Interaction.lastiMY) * 1.5f;
		}
		else
		{
			this.lickOpenAmount -= (this.iMY - Interaction.lastiMY) * 3f;
		}
		this.lickOpenAmount = Game.cap(this.lickOpenAmount, 0f, 0.45f);
		this.lickOpenAmount *= 1f - Game.cap(Time.deltaTime * 4f, 0f, 1f);
		this.performingCharacter.penetrateMouth(this.lickOpenAmount, 0f);
		this.mountPoint.Rotate((this.performingCharacter.interactionMX - 0.5f) * 30f, (1f - this.iMY - 0.5f) * 60f + this.lickOpenAmount * 20f + 14f, (this.performingCharacter.interactionMX - 0.5f) * 40f);
	}

	public void processCunnilingus()
	{
		this.iMY = this.performingCharacter.interactionMY;
		this.angleOut = (this.targetCharacter.forwardAfterIK() - this.targetCharacter.upAfterIK()).normalized;
		this.v32 = this.targetCharacter.vaginalEntranceAfterIK.transform.position + (-this.targetCharacter.forwardAfterIK() - this.targetCharacter.upAfterIK() * 0.65f) * this.targetCharacter.height_act * 0.03f;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v32 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.processLickingMotion();
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.45f);
		this.targetCharacter.spreadVagina(false, this.lickOpenAmount);
		this.targetCharacter.spreadVagina(true, this.lickOpenAmount);
		this.targetCharacter.nudgeClit(this.performingCharacter.interactionMX * 50f, this.lickOpenAmount * -30f);
		this.targetCharacter.wetness_vagina += (1f - this.performingCharacter.wetness_muzzle) * Game.cap(Time.deltaTime * 0.05f, 0f, 1f);
		this.performingCharacter.wetness_muzzle += (1f - this.performingCharacter.wetness_muzzle) * Game.cap(Time.deltaTime * 0.05f, 0f, 1f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, string.Empty, 0, 0.9f, 1f, 1.9f, false, true);
		}
		this.processAudio(this.gizmoPositionWithinBounds, true);
	}

	public void processRimming()
	{
		this.iMY = this.performingCharacter.interactionMY;
		this.angleOut = (this.targetCharacter.forwardAfterIK() - this.targetCharacter.upAfterIK()).normalized;
		this.v32 = this.targetCharacter.tailholeEntranceAfterIK.transform.position + (-this.targetCharacter.forwardAfterIK() - this.targetCharacter.upAfterIK() * 0.65f) * this.targetCharacter.height_act * 0.03f;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v32 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.processLickingMotion();
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.1f * this.targetCharacter.data.analPleasure);
		this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.01f * this.targetCharacter.data.analPleasure);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, string.Empty, 0, 0.9f, 1f, 1.9f, false, true);
		}
		this.processAudio(this.gizmoPositionWithinBounds, true);
	}

	public void processFucking(int orificeType, Transform orifice, Vector3 angOut, ref float dynamicTightness, float staticTightness, bool withToy = false)
	{
		this.iMY = this.performingCharacter.interactionMY;
		this.gizmoTargetPosition = 0.5f + (this.iMY - 0.6f + this.insertionOffset) * 2.5f;
		this.v3 = orifice.position;
		this.v32 = this.v3;
		this.v33 = Vector3.zero;
		if (orificeType == 2)
		{
			this.v33 = -this.performingCharacter.upAfterIK() * 4f;
			this.xFactor = 0.4f;
		}
		else if (withToy)
		{
			this.xFactor = 0.8f;
		}
		else
		{
			this.xFactor = 1.25f;
		}
		this.angleOut = (this.gizmoStartPoint + this.v33 - this.v3 - angOut * 4f).normalized;
		if (withToy)
		{
			this.angleOut = ((this.gizmoStartPoint - this.v3 + this.performingCharacter.right() * (this.performingCharacter.interactionMX - 0.5f) * this.xFactor) * Game.cap(0.3f + this.gizmoPosition * 1.1f, 0f, 0.85f) - angOut * 1.5f).normalized;
		}
		if (withToy)
		{
			this.determineHowFarInside(this.performingCharacter, this.targetCharacter, orifice, orificeType, true, this.sexToy.toyTipAfterIK, this.sexToy.penetratorLength);
		}
		else if (orificeType == 2)
		{
			this.determineHowFarInside(this.performingCharacter, this.targetCharacter, this.targetCharacter.throatHoleAfterIKandSuckLock.transform, orificeType, false, default(Vector3), 1f);
		}
		else
		{
			this.determineHowFarInside(this.performingCharacter, this.targetCharacter, orifice, orificeType, false, default(Vector3), 1f);
		}
		this.determineResistance(this.performingCharacter, ref dynamicTightness, staticTightness, 1f + this.performingCharacter.height_act - this.targetCharacter.height_act, this.performingCharacter.controlledByPlayer || this.targetCharacter.controlledByPlayer, this.sexToy);
		this.gizmoPosition += (this.gizmoTargetPosition - this.gizmoPosition) * Game.cap(Time.deltaTime * this.penetrationSpeed, 0f, 1f);
		float num = Game.cap(this.gizmoPosition, 0.01f + (this.performingCharacter.height_act - 0.8f) * 0.03f, 1.5f + (this.performingCharacter.height_act - 0.8f));
		if (withToy)
		{
			this.v3 -= this.angleOut * (1f - num * 0.65f) * this.sexToy.penetratorLength;
		}
		else
		{
			num *= this.performingCharacter.penisLengthInWorldUnits;
			if (num > 1.2f)
			{
				num = 1.2f;
			}
			if (num > this.performingCharacter.penisLengthInWorldUnits * 0.9f)
			{
				num += (this.performingCharacter.penisLengthInWorldUnits * 0.9f - num) * Game.cap(this.insideTime, 0f, 1f);
			}
			if (orificeType == 2)
			{
				float num2 = num;
				float tongueScale = this.targetCharacter.tongueScale;
				Vector3 lossyScale = this.targetCharacter.bones.Head.lossyScale;
				num = num2 + (0.335f * (tongueScale * lossyScale.x) - 0.18f);
			}
			if (num < 0.05f)
			{
				num = 0.05f;
			}
			this.v3 += this.angleOut * num;
			this.v3 += this.performingCharacter.rightAfterIK() * num * (this.performingCharacter.interactionMX - 0.5f) * this.xFactor;
		}
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		if (withToy)
		{
			this.v33 = this.v3 + this.angleOut.normalized * (this.sexToy.penetratorLength * this.performingCharacter.height_act);
			this.sexToy.aimAt(this.v3, this.v33, this.mountPoint, false);
		}
		else
		{
			this.v33 = this.v3 + (this.performingCharacter.bones.Root.position - this.performingCharacter.bones.Penis1.position);
			this.mountPoint.position = this.v33;
		}
		this.gizmo.transform.LookAt(this.v33, -this.targetCharacter.bones.Root.forward);
		if (!withToy)
		{
			this.performingCharacter.rollRoot((this.performingCharacter.interactionMY - 0.5f) * 24f);
			this.performingCharacter.archBack((this.performingCharacter.interactionMY - 0.5f) * -2f);
		}
		float num3 = 0f;
		if (this.inside)
		{
			num3 = 1f;
		}
		switch (orificeType)
		{
		case 0:
			this.targetCharacter.penetrateAnus((this.gizmoPosition - this.lastGizmoPosition) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f) * num3, this.penetrationGirth);
			this.targetCharacter.pushAnus(this.pushingThroughResistance * 0.25f * (1f - this.targetCharacter.currentTailholeTightness * 0.8f));
			this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.21f * num3 * this.targetCharacter.data.analPleasure);
			this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.1f * num3);
			if (!withToy)
			{
				this.performingCharacter.setPenisAngle(orifice.position + (this.targetCharacter.bones.SpineUpper.position - orifice.position).normalized * Game.cap(this.howFarIn - 0.35f, 0f, 1f) * 0.5f, true);
			}
			this.performingCharacter.stretchPenis(1f + this.pushingThroughResistance * (1f - this.targetCharacter.currentTailholeTightness * 0.8f) * 2f);
			break;
		case 1:
			if (this.howFarInWorldUnits > -0.01f)
			{
				float num6 = this.performingCharacter.girthAlongPenetrator(1f - (this.howFarIn + 0.01f / this.performingCharacter.penisLengthInWorldUnits), true);
				this.targetCharacter.spreadVagina(false, 5f * (num6 / this.targetCharacter.height_act));
				this.targetCharacter.spreadVagina(true, 7.5f * (num6 / this.targetCharacter.height_act));
			}
			this.targetCharacter.penetrateVagina((this.gizmoPosition - this.lastGizmoPosition) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f) * num3, this.penetrationGirth, this.performingCharacter.bones.Penis0.position);
			this.targetCharacter.pushVagina(this.pushingThroughResistance * 0.25f * (1f - this.targetCharacter.currentVaginalTightness * 0.8f));
			this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.91f * num3);
			this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.35f * num3);
			if (!withToy)
			{
				this.performingCharacter.setPenisAngle(orifice.position - this.angleOut * Game.cap(this.howFarIn - 0.35f, 0f, 1f) * 0.5f, true);
			}
			this.performingCharacter.stretchPenis(1f + this.pushingThroughResistance * (1f - this.targetCharacter.currentVaginalTightness * 0.8f) * 2f);
			break;
		case 2:
		{
			float num4 = this.performingCharacter.girthAlongPenetrator(Game.cap((this.howFarInWorldUnits - 0.1f) / this.performingCharacter.penisLengthInWorldUnits, 0.1f, 1f), true) * 2.45f;
			Vector3 lossyScale2 = this.targetCharacter.bones.Head.lossyScale;
			float amount = num4 / lossyScale2.x;
			float num5 = this.gizmoPosition - this.lastGizmoPosition;
			this.targetCharacter.penetrateMouth(amount, num5);
			this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.05f * num3);
			this.targetCharacter.suckDickLockOn(this.performingCharacter.bones.Penis0.position, this.performingCharacter, num5);
			if (!withToy)
			{
				this.performingCharacter.setPenisAngle(orifice.position + (this.targetCharacter.bones.SpineUpper.position - orifice.position).normalized * Game.cap(this.howFarIn - 0.35f, 0f, 1f) * 0.5f, true);
				this.performingCharacter.stimulate(this.performingCharacter.interactionVigor * 0.65f);
				this.performingCharacter.dragPenis((this.gizmoPosition - this.lastGizmoPosition) * 0.2f, 0.5f);
				this.performingCharacter.wetness_penis += (1f - this.performingCharacter.wetness_penis) * Game.cap(Time.deltaTime * 0.3f, 0f, 1f);
			}
			this.performingCharacter.stretchPenis(1f + this.pushingThroughResistance * (1f - this.targetCharacter.currentThroatTightness * 0.8f) * 2f);
			break;
		}
		}
		if (withToy)
		{
			if (this.inside)
			{
				this.targetCharacter.humpBump(this.performingCharacter.interactionMouseChange.y * 0.04f * this.performingCharacter.height_act / this.targetCharacter.height_act);
			}
		}
		else if (orificeType == 2)
		{
			this.performingCharacter.buryPenis(this.targetCharacter.throatHoleAfterIK.transform.position, this.targetCharacter.bones.Belly.position, this.targetCharacter.bones.Belly.position, this.targetCharacter, 2);
			this.performingCharacter.dickSucker = this.targetCharacter;
		}
		else
		{
			this.performingCharacter.buryPenis(orifice.position, this.targetCharacter.bones.SpineUpper.position, this.targetCharacter.bones.SpineUpper.position, this.targetCharacter, orificeType);
			if (this.inside)
			{
				this.performingCharacter.stimulate(this.performingCharacter.interactionVigor * 0.27f * num3);
				this.targetCharacter.humpBump(this.performingCharacter.interactionMouseChange.y * -0.15f * this.performingCharacter.height_act / this.targetCharacter.height_act);
				this.performingCharacter.wetness_penis += (1f - this.performingCharacter.wetness_penis) * Game.cap(Time.deltaTime * 0.2f, 0f, 1f);
			}
		}
		this.previousGirth = this.penetrationGirth;
		if (this.performingCharacter.showBalls && this.performingCharacter.data.ballsType != 2 && !withToy)
		{
			if (!this.audioInitted)
			{
				this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, "softslap", 3, 0.5f, 1f, 1.2f, false, true);
			}
		}
		else if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, "slurpOut", 5, 0.5f, 1f, 1.8f, false, true);
		}
		this.processAudio(num, true);
	}

	public void determineHowFarInside(RackCharacter topCharacter, RackCharacter bottomCharacter, Transform orifice, int orificeType, bool toy = false, Vector3 toyTip = default(Vector3), float toyLength = 1f)
	{
		if (!toy)
		{
			toyTip = topCharacter.penisTip(true);
			toyLength = topCharacter.penisLengthInWorldUnits;
		}
		float magnitude = (toyTip - orifice.position).magnitude;
		switch (orificeType)
		{
		case 0:
		{
			Vector3 vector3 = orifice.InverseTransformPoint(toyTip);
			this.insideAmount = vector3.x;
			break;
		}
		case 1:
		{
			Vector3 vector2 = orifice.InverseTransformPoint(toyTip);
			this.insideAmount = vector2.z;
			break;
		}
		case 2:
		{
			Vector3 vector = orifice.InverseTransformPoint(toyTip);
			this.insideAmount = vector.z;
			break;
		}
		}
		this.inside = (this.insideAmount > 0f);
		this.howFarInWorldUnits = magnitude * Game.cap(this.insideAmount / 0.05f, 0f, 1f);
		this.howFarIn += (this.howFarInWorldUnits / toyLength * 1.1f - this.howFarIn) * Game.cap(Time.deltaTime * 12f, 0f, 1f);
		if (this.howFarIn >= 0.26f)
		{
			if (topCharacter.interactionMY > 0.99f)
			{
				if (this.insideTime > 1f)
				{
					this.insideTime = 1f;
				}
				this.insideTime -= Time.deltaTime;
			}
			else
			{
				this.insideTime += Time.deltaTime;
			}
		}
		else
		{
			if (this.insideTime > 1f)
			{
				this.insideTime = 1f;
			}
			this.insideTime -= Time.deltaTime;
		}
		if (this.insideTime < 0f)
		{
			this.insideTime = 0f;
		}
		this.howFarInWorldUnits = magnitude;
	}

	public void determineResistance(RackCharacter topCharacter, ref float dynamicTightness, float staticTightness, float sizeRatioTopOverBottom, bool playerIsInteracting, SexToy toy = null)
	{
		if (toy == null)
		{
			this.penetrationGirth = topCharacter.girthAlongPenetrator(1f - this.howFarIn, true);
		}
		else
		{
			this.penetrationGirth = toy.girthAlongPenetrator(1f - this.howFarIn);
		}
		if (this.penetrationGirth < 0.06f)
		{
			this.penetrationGirth = 0f;
		}
		staticTightness = 0f;
		if (this.inside)
		{
			this.staticResistance -= 0.3f;
			if (this.resistance != 0f)
			{
				goto IL_009e;
			}
		}
		else
		{
			this.staticResistance = this.staticResistance;
		}
		goto IL_009e;
		IL_009e:
		float num = this.penetrationGirth;
		num = ((!(this.gizmoTargetPosition > this.gizmoPosition)) ? ((toy != null) ? toy.girthAlongPenetrator(0.96f - this.howFarIn) : topCharacter.girthAlongPenetrator(0.96f - this.howFarIn, true)) : ((toy != null) ? toy.girthAlongPenetrator(1.04f - this.howFarIn) : topCharacter.girthAlongPenetrator(1.04f - this.howFarIn, true)));
		if (num < 0.06f)
		{
			num = 0f;
		}
		this.resistance = (Mathf.Pow(1f + num, 5f) - Mathf.Pow(1f + this.penetrationGirth, 5f)) * dynamicTightness * staticTightness * sizeRatioTopOverBottom * 35f;
		if (this.penetrationGirth < 0.01f)
		{
			this.resistance = 0f;
		}
		if (this.staticResistance == 999f)
		{
			this.resistance += 0.01f;
		}
		if (num > this.girthAllowed && this.inside)
		{
			if (this.forcedResistanceDelay <= 0f)
			{
				this.forcedResistanceDelay = 1f;
			}
		}
		else if (this.gizmoTargetPosition > this.gizmoPosition + 0.1f)
		{
			this.forcedResistanceDelay = 0f;
		}
		if (this.forcedResistanceDelay > 0f)
		{
			if (Mathf.Abs(this.pushingThroughResistance) > 0.1f && this.readyForResistanceSFX)
			{
				Game.PlaySFXAtPoint(Resources.Load("fleshyresistance") as AudioClip, this.gizmo.transform.position, 0.25f);
				this.readyForResistanceSFX = false;
			}
			this.forcedResistanceDelay -= Game.cap(Mathf.Abs(this.pushingThroughResistance) * 0.1f, 0f, Time.deltaTime * 2f);
			if (this.forcedResistanceDelay <= 0f)
			{
				Game.PlaySFXAtPoint(Resources.Load("resistancepop") as AudioClip, this.gizmo.transform.position, 0.02f);
				this.forcedResistanceDelay = -1f;
				this.readyForResistanceSFX = true;
				if (Mathf.Abs(this.pushingThroughResistance) > 0.1f && num + 0.1f > this.girthAllowed)
				{
					this.girthAllowed = num + 0.1f;
					dynamicTightness *= 0.75f;
				}
				if (playerIsInteracting && this.timeSpentInteracting > 0.25f)
				{
					this.game.popPenetration();
				}
			}
		}
		if (Mathf.Abs(this.gizmoTargetPosition - this.gizmoPosition) >= this.resistance + this.staticResistance + this.forcedResistanceDelay * 125f)
		{
			this.penetrationSpeed += (15f - this.penetrationSpeed) * Game.cap(Time.deltaTime * 25f, 0f, 1f);
			this.pushingThroughResistance -= this.pushingThroughResistance * Game.cap(Time.deltaTime * 5f, 0f, 1f);
			this.staticResistance = 0f;
		}
		else
		{
			this.pushingThroughResistance += (this.gizmoTargetPosition - this.gizmoPosition - this.pushingThroughResistance) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
			this.gizmoTargetPosition = this.lastGizmoPosition;
			this.penetrationSpeed += (0f - this.penetrationSpeed) * Game.cap(Time.deltaTime * 25f, 0f, 1f);
			this.staticResistance = 0.2f;
		}
		if (this.pushingThroughResistance > -0.01f && !this.inside)
		{
			dynamicTightness += (1f - dynamicTightness) * Game.cap(Time.deltaTime * 0.04f, 0f, 1f);
		}
		else
		{
			dynamicTightness += (0.2f - dynamicTightness) * Game.cap(Time.deltaTime * 0.04f, 0f, 1f);
		}
	}

	public void processVaginalFingering()
	{
		this.checkInvertPositionDelay -= Time.deltaTime;
		if (this.checkInvertPositionDelay <= 0f)
		{
			this.checkInvertPositionDelay += 3f;
			this.angleBetween = Vector3.Angle(this.performingCharacter.up(), (this.targetCharacter.forward() - this.targetCharacter.up()).normalized);
			this.invertedHandPosition = (this.angleBetween > 90f);
		}
		this.inverter = 1f;
		if (this.invertedHandPosition)
		{
			this.inverter = -1f;
		}
		this.iMY = this.performingCharacter.interactionMY;
		if (this.invertedHandPosition)
		{
			this.iMY = 1f - this.performingCharacter.interactionMY;
		}
		this.gizmoPosition = 0.082f + this.iMY * 0.079f;
		this.gizmoPositionWithinBounds = (this.gizmoPosition - 0.082f) / 0.079f;
		this.v3 = this.targetCharacter.vaginalEntranceAfterIK.transform.position;
		this.angleOut = (this.gizmoStartPoint - this.v3 - this.targetCharacter.up() * 1f + this.targetCharacter.forward() * 1f).normalized;
		this.v3 += this.angleOut * this.gizmoPosition;
		this.v3 += this.performingCharacter.right() * (this.performingCharacter.interactionMX - 0.5f) * 0.2f * this.gizmoPositionWithinBounds;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.gizmo.transform.LookAt(this.targetCharacter.vaginalEntranceAfterIK.transform.position - this.angleOut * 0.001f, -this.targetCharacter.bones.Root.forward);
		this.mountPoint.position = this.gizmo.transform.position + this.angleOut * (this.performingCharacter.bones.Finger00_R.localPosition.magnitude * this.performingCharacter.height_act);
		this.mountPoint.LookAt(this.gizmo.transform.position, -this.targetCharacter.bones.Root.forward);
		if (this.invertedHandPosition)
		{
			this.mountPoint.Rotate(0f, 0f, 180f);
		}
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(180f, 90f, 0f);
		}
		else
		{
			this.mountPoint.Rotate(0f, 90f, 0f);
		}
		if (this.performingNode == "handL")
		{
			if (this.invertedHandPosition)
			{
				this.tracePositioner();
				this.gizmo.transform.Rotate(-58f, 113f, -4f - (1f - this.gizmoPositionWithinBounds) * 20f);
			}
			else
			{
				this.gizmo.transform.Rotate(30f, -33f, 25f + (1f - this.gizmoPositionWithinBounds) * 20f);
			}
			this.performingCharacter.clenchHandL(0.9f, false, -1, 0.5f, -99f);
		}
		else
		{
			if (this.invertedHandPosition)
			{
				this.gizmo.transform.Rotate(-118f, 118f, -181f - (1f - this.gizmoPositionWithinBounds) * 20f);
			}
			else
			{
				this.gizmo.transform.Rotate(30f, 33f, -25f - (1f - this.gizmoPositionWithinBounds) * 20f);
			}
			this.performingCharacter.clenchHandR(0.9f, false, -1, 0.5f, -99f);
		}
		this.performingCharacter.pointFingerAt(this.performingNode == "handR", 0, (this.targetCharacter.vaginalEntranceAfterIK.transform.position + this.targetCharacter.bones.SpineLower.position) / 2f);
		this.performingCharacter.pointFingerAt(this.performingNode == "handR", 1, (this.targetCharacter.vaginalEntranceAfterIK.transform.position + this.targetCharacter.bones.SpineLower.position) / 2f);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.015f);
		this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.01f);
		this.targetCharacter.spreadVagina(false, 0.3f);
		this.targetCharacter.spreadVagina(true, 0.6f);
		this.targetCharacter.penetrateVagina((this.gizmoPositionWithinBounds - this.lastGizmoPositionWithinBounds) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f), 0.06f, (!(this.performingNode == "handR")) ? this.performingCharacter.bones.Finger00_L.position : this.performingCharacter.bones.Finger00_R.position);
		this.targetCharacter.pushPubic(this.gizmoPositionWithinBounds);
		this.performingCharacter.wetness_finger1 += (1f - this.performingCharacter.wetness_finger1) * Game.cap(Time.deltaTime * 0.2f, 0f, 1f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_in", string.Empty, 0, string.Empty, 0, 0.025f, 1f, 1f, false, false);
		}
		this.processAudio(this.performingCharacter.interactionVigor, true);
	}

	public void processAnalFingering()
	{
		this.iMY = this.performingCharacter.interactionMY;
		this.gizmoPosition = 0.082f + this.iMY * 0.079f;
		this.gizmoPositionWithinBounds = (this.gizmoPosition - 0.082f) / 0.079f;
		this.v3 = this.targetCharacter.tailholeEntrance.transform.position;
		this.angleOut = (this.gizmoStartPoint - this.v3 - this.targetCharacter.tailholeEntrance.transform.right * 4f).normalized;
		this.v3 += this.angleOut * this.gizmoPosition;
		this.v3 += this.performingCharacter.right() * (this.performingCharacter.interactionMX - 0.5f) * 0.2f * this.gizmoPositionWithinBounds;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.gizmo.transform.LookAt(this.targetCharacter.tailholeEntrance.transform.position - this.angleOut * 0.001f, -this.targetCharacter.bones.Root.forward);
		this.mountPoint.position = this.gizmo.transform.position + this.angleOut * (this.performingCharacter.bones.Finger00_R.localPosition.magnitude * this.performingCharacter.height_act);
		this.mountPoint.LookAt(this.gizmo.transform.position, -this.targetCharacter.bones.Root.forward);
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(180f, 90f, 0f);
		}
		else
		{
			this.mountPoint.Rotate(0f, 90f, 0f);
		}
		if (this.performingNode == "handL")
		{
			this.gizmo.transform.Rotate(30f, -33f, 25f + (1f - this.gizmoPositionWithinBounds) * 20f);
			this.performingCharacter.clenchHandL(0.9f, false, -1, 0.5f, -99f);
		}
		else
		{
			this.gizmo.transform.Rotate(30f, 33f, -25f - (1f - this.gizmoPositionWithinBounds) * 20f);
			this.performingCharacter.clenchHandR(0.9f, false, -1, 0.5f, -99f);
		}
		this.performingCharacter.pointFingerAt(this.performingNode == "handR", 0, (this.targetCharacter.bones.AssholeBottom.position + this.targetCharacter.bones.AssholeTop.position + this.targetCharacter.bones.Asshole.position) / 3f);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.015f * this.targetCharacter.data.analPleasure);
		this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.01f * this.targetCharacter.data.analPleasure);
		this.targetCharacter.penetrateAnus((this.gizmoPositionWithinBounds - this.lastGizmoPositionWithinBounds) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f), 0.06f);
		this.targetCharacter.pushPubic(this.gizmoPositionWithinBounds);
		this.performingCharacter.wetness_finger0 += (1f - this.performingCharacter.wetness_finger0) * Game.cap(Time.deltaTime * 0.2f, 0f, 1f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_in", string.Empty, 0, string.Empty, 0, 0.025f, 1f, 1f, false, false);
		}
		this.processAudio(this.performingCharacter.interactionVigor, true);
	}

	public void processCockTeaseWithFingers()
	{
		this.gizmoPosition = 0.25f;
		this.v3 = RackCharacter.positionAlongPenetrator(this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, this.gizmoPosition, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4);
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		this.v33 = Vector3.zero;
		this.v33 += this.performingCharacter.right() * this.performingCharacter.interactionMX;
		this.v33 += this.performingCharacter.forward() * this.performingCharacter.interactionMY * 0.3f;
		if (this.performingNode == "handL")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(-this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		if (this.performingNode == "handR")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		this.mountPoint.localPosition = this.v32.normalized * 0.495f * this.performingCharacter.height_act;
		this.v32 = this.targetCharacter.directionAlongPenetrator(0, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, this.gizmoPosition, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4);
		this.mountPoint.LookAt(this.v3, this.v32);
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(-145.2f, 68.3f, 2.8f);
		}
		else
		{
			this.mountPoint.Rotate(-47.1f, 115.3f, -4f);
		}
		if (this.performingNode == "handL")
		{
			this.performingCharacter.clenchHandL(0.1f + (1f - this.targetCharacter.arousal) * 0.15f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 0, 0.5f, -99f);
			this.performingCharacter.clenchHandL(0.1f + (1f - this.targetCharacter.arousal) * 0.15f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 1, 0.5f, -99f);
			this.performingCharacter.clenchHandL(0.85f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 2, 0.5f, -99f);
			this.performingCharacter.clenchHandL(0.85f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 3, 0.5f, -99f);
		}
		if (this.performingNode == "handR")
		{
			this.performingCharacter.clenchHandR(0.1f + (1f - this.targetCharacter.arousal) * 0.15f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 0, 0.5f, -99f);
			this.performingCharacter.clenchHandR(0.1f + (1f - this.targetCharacter.arousal) * 0.15f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 1, 0.5f, -99f);
			this.performingCharacter.clenchHandR(0.85f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 2, 0.5f, -99f);
			this.performingCharacter.clenchHandR(0.85f + 0.15f * Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.mY - 1f, 0f, 1f), false, 3, 0.5f, -99f);
		}
		this.targetCharacter.nudgePenis((1f - this.performingCharacter.interactionMX - 0.5f) * 16f, (1f - this.performingCharacter.interactionMY - 0.5f) * 16f);
		this.targetCharacter.pushPubic(this.performingCharacter.interactionMY);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.01f);
		this.targetCharacter.arouse(this.performingCharacter.interactionVigor * 0.042f);
		if (!this.audioInitted)
		{
			this.initAudio("rub_in", "rub_in", string.Empty, 0, string.Empty, 0, 0.01f, 1f, 1f, false, false);
		}
		this.processAudio(this.performingCharacter.interactionVigor, true);
	}

	public void processVaginaSpread()
	{
	}

	public void processCockPolishing()
	{
		this.v3 = RackCharacter.positionAlongPenetrator(this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, 0.85f - (this.performingCharacter.interactionMY - 0.5f) * 0.3f, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4);
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		if (this.performingNode == "handL")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(-this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		if (this.performingNode == "handR")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		this.mountPoint.localPosition = this.v32.normalized * 0.24f * this.performingCharacter.height_act;
		Transform transform2 = this.mountPoint;
		transform2.position += this.performingCharacter.right() * (this.performingCharacter.interactionMX - 0.5f) * 0.4f;
		this.v32 = this.targetCharacter.directionAlongPenetrator(0, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, this.gizmoPosition, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4);
		this.mountPoint.LookAt(this.v3, this.v32);
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(-78.9f + (this.performingCharacter.interactionMY - 0.5f) * 50f, 181.4f + (this.performingCharacter.interactionMX - 0.5f) * 10f, -91.7f);
		}
		else
		{
			this.mountPoint.Rotate(-79.3f + (this.performingCharacter.interactionMY - 0.5f) * 50f, 176.3f + (this.performingCharacter.interactionMX - 0.5f) * 10f, -84.7f);
		}
		if (this.performingNode == "handL")
		{
			this.performingCharacter.clenchHandL(0.65f - Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 0.8f, 0f, 1f) * 0.5f, false, -1, 0.5f, -99f);
		}
		if (this.performingNode == "handR")
		{
			this.performingCharacter.clenchHandR(0.65f - Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 0.8f, 0f, 1f) * 0.5f, false, -1, 0.5f, -99f);
		}
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 3f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_in", string.Empty, 0, string.Empty, 0, 0.025f, 1f, 1f, false, false);
		}
		this.processAudio(this.performingCharacter.interactionVigor, true);
	}

	public void processClitRub()
	{
		this.v3 = this.targetCharacter.bones.Clit.position + -this.targetCharacter.bones.Clit.right * 0.016f + this.targetCharacter.bones.Clit.forward * 0.016f;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		if (this.performingNode == "handL")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(-this.performingCharacter.right() * 0.5f + -this.performingCharacter.right() * 2f * (this.performingCharacter.interactionMX - 0.5f) + -this.performingCharacter.up() * 1.4f * (this.performingCharacter.interactionMY - 0.5f) + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		if (this.performingNode == "handR")
		{
			this.v32 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.right() * 0.5f + -this.performingCharacter.right() * 2f * (this.performingCharacter.interactionMX - 0.5f) + -this.performingCharacter.up() * 1.4f * (this.performingCharacter.interactionMY - 0.5f) + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
		}
		if (this.selfInteraction)
		{
			this.v32 += this.performingCharacter.forward() * 0.55f;
		}
		float num = 0f;
		if (this.selfInteraction)
		{
			num = 0.038f;
		}
		this.mountPoint.localPosition = this.v32.normalized * (0.56f + num) * this.performingCharacter.height_act;
		this.mountPoint.LookAt(this.v3, this.targetCharacter.forward());
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(-74f, 185f, -100f);
		}
		else
		{
			this.mountPoint.Rotate(-71.3f, 165.3f, -69.7f);
		}
		if (this.performingNode == "handL")
		{
			this.performingCharacter.clenchHandL(0.1f, false, 0, 0.1f, -99f);
			this.performingCharacter.clenchHandL(0.1f, false, 1, 0.5f, -99f);
			this.performingCharacter.clenchHandL(0.85f, false, 2, 0.5f, -99f);
			this.performingCharacter.clenchHandL(0.85f, false, 3, 0.5f, -99f);
		}
		if (this.performingNode == "handR")
		{
			this.performingCharacter.clenchHandR(0.1f, false, 0, 0.1f, -99f);
			this.performingCharacter.clenchHandR(0.1f, false, 1, 0.5f, -99f);
			this.performingCharacter.clenchHandR(0.85f, false, 2, 0.5f, -99f);
			this.performingCharacter.clenchHandR(0.85f, false, 3, 0.5f, -99f);
		}
		this.targetCharacter.nudgeClit((1f - this.performingCharacter.interactionMX - 0.5f) * 252f, (this.iMY - 0.5f) * -252f);
		this.targetCharacter.pushPubic(this.performingCharacter.interactionMY * 2f);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.39f);
		this.performingCharacter.wetness_finger0 += (this.targetCharacter.wetness_vagina * 0.25f - this.performingCharacter.wetness_finger0) * Game.cap(Time.deltaTime * 0.5f, 0f, 1f);
		this.performingCharacter.wetness_finger1 += (this.targetCharacter.wetness_vagina * 0.25f - this.performingCharacter.wetness_finger1) * Game.cap(Time.deltaTime * 0.5f, 0f, 1f);
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_in", string.Empty, 0, string.Empty, 0, 0.01f, 1f, 1f, false, false);
		}
		this.processAudio(this.performingCharacter.interactionVigor, true);
	}

	public void processHandjob()
	{
		this.checkInvertPositionDelay -= Time.deltaTime;
		if (this.checkInvertPositionDelay <= 0f)
		{
			this.checkInvertPositionDelay += 3f;
			this.angleBetween = Vector3.Angle(this.performingCharacter.up(), this.targetCharacter.forward());
			this.invertedHandPosition = (this.angleBetween > 89f);
		}
		if (this.selfInteraction)
		{
			this.invertedHandPosition = false;
		}
		this.inverter = 1f;
		if (this.invertedHandPosition)
		{
			this.inverter = -1f;
		}
		this.iMY = this.performingCharacter.interactionMY;
		if (this.invertedHandPosition)
		{
			this.iMY = 1f - this.performingCharacter.interactionMY;
		}
		this.v3 = RackCharacter.positionAlongPenetrator(this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, this.gizmoPosition, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4);
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 45f + this.firstFrameAccelerator, 0f, 1f);
		if (this.selfInteraction)
		{
			this.v33 = this.performingCharacter.right();
			if (this.performingNode == "handL")
			{
				this.v33 = -this.performingCharacter.right();
			}
			this.v33 *= this.performingCharacter.height_act * 6f;
			this.v32 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.bones.SpineUpper.position + this.v33);
		}
		else
		{
			if (this.performingNode == "handL")
			{
				this.v32 = this.gizmo.transform.InverseTransformPoint(-this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
			}
			if (this.performingNode == "handR")
			{
				this.v32 = this.gizmo.transform.InverseTransformPoint(this.performingCharacter.right() + (this.v3 + this.performingCharacter.bones.SpineUpper.position * 3f) / 4f + this.v33);
			}
		}
		this.mountPoint.localPosition = this.v32.normalized * 0.33f * this.performingCharacter.height_act;
		this.mountPoint.LookAt(this.v3, this.targetCharacter.directionAlongPenetrator(0, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis4, this.gizmoPosition * 0.321f, true, this.targetCharacter.bones.Penis0, this.targetCharacter.bones.Penis1, this.targetCharacter.bones.Penis2, this.targetCharacter.bones.Penis3, this.targetCharacter.bones.Penis4));
		this.handScale = this.performingCharacter.height_act / this.targetCharacter.penisLengthInWorldUnits * 0.6f;
		if (this.invertedHandPosition)
		{
			this.minGizmoPosition = 0.15f;
			this.maxGizmoPosition = 0.8f;
		}
		else
		{
			this.minGizmoPosition = this.handScale * 0.177f;
			this.maxGizmoPosition = 0.85f;
		}
		if (this.minGizmoPosition > 0.85f || this.minGizmoPosition < 0f)
		{
			this.minGizmoPosition = 0.85f;
		}
		if (this.maxGizmoPosition < this.minGizmoPosition + 0.1f)
		{
			this.maxGizmoPosition = this.minGizmoPosition + 0.1f;
		}
		this.gizmoPosition = this.minGizmoPosition + Game.cap(1.5f - this.iMY * 2f, 0f, 1f) * (this.maxGizmoPosition - this.minGizmoPosition);
		this.gizmoPositionWithinBounds = Game.cap((this.gizmoPosition - this.minGizmoPosition) / (this.maxGizmoPosition - this.minGizmoPosition), 0f, 1f);
		this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.45f);
		this.targetCharacter.dragPenis(this.gizmoPosition - this.lastGizmoPosition, 0.5f);
		if (this.invertedHandPosition)
		{
			this.mountPoint.Rotate(0f, 0f, 180f);
		}
		if (this.performingNode == "handL")
		{
			this.mountPoint.Rotate(0f, 0f, 180f);
		}
		this.mountPoint.Rotate(1.4f, 110.9f + 55f * this.targetCharacter.girthAlongPenetrator(this.gizmoPosition - -0.5f * (this.handScale * 0.061f) * this.inverter, true), 0.8f);
		if (this.selfInteraction)
		{
			this.mountPoint.Rotate(0f, 9f, 0f);
		}
		for (int i = 0; i < 4; i++)
		{
			if (this.performingNode == "handL")
			{
				this.performingCharacter.clenchHandL(0.0688f / (0.1f + this.targetCharacter.girthAlongPenetrator(this.gizmoPosition - (-0.5f + (float)i / 3f) * (this.handScale * 0.061f) * this.inverter, true) * 0.321f), true, i, 0.5f, -99f);
			}
			if (this.performingNode == "handR")
			{
				this.performingCharacter.clenchHandR(0.0688f / (0.1f + this.targetCharacter.girthAlongPenetrator(this.gizmoPosition - (-0.5f + (float)i / 3f) * (this.handScale * 0.061f) * this.inverter, true) * 0.321f), true, i, 0.5f, -99f);
			}
		}
		if (!this.audioInitted)
		{
			this.initAudio(string.Empty, string.Empty, "rub_in", 4, "rub_out", 4, 0.45f, 1f, 1f, true, true);
		}
		this.processAudio(this.gizmoPositionWithinBounds, true);
	}

	public void processDickRiding(int orificeType, Transform orifice, ref float dynamicTightness, float staticTightness)
	{
		this.gizmoTargetPosition = 0.5f + (1f - this.performingCharacter.interactionMY - 0.55f + this.insertionOffset * 0.4f) * 1.5f;
		this.v3 = this.targetCharacter.bones.Penis0.position;
		this.angleOut = this.targetCharacter.forward().normalized;
		this.determineHowFarInside(this.targetCharacter, this.performingCharacter, orifice, 0, false, default(Vector3), 1f);
		this.determineResistance(this.targetCharacter, ref dynamicTightness, staticTightness, 1f + this.targetCharacter.height_act - this.performingCharacter.height_act, this.performingCharacter.controlledByPlayer || this.targetCharacter.controlledByPlayer, (SexToy)null);
		this.gizmoPosition += (this.gizmoTargetPosition - this.gizmoPosition) * Game.cap(Time.deltaTime * this.penetrationSpeed * 2f, 0f, 1f);
		float num = Game.cap(this.gizmoPosition, 0.18f + (this.targetCharacter.height_act - 0.8f) * 0.03f, 1.5f + (this.targetCharacter.height_act - 0.8f));
		num *= this.targetCharacter.penisLengthInWorldUnits;
		if (num > 0.8f)
		{
			num = 0.8f;
		}
		if (orificeType == 1)
		{
			num *= 1.2f;
		}
		if (num > this.targetCharacter.penisLengthInWorldUnits)
		{
			num += (this.targetCharacter.penisLengthInWorldUnits - num) * Game.cap(this.insideTime, 0f, 1f);
		}
		this.v3 += this.angleOut * num;
		this.v3 -= orifice.position - this.performingCharacter.bones.Root.position;
		Transform transform = this.gizmo.transform;
		transform.position += (this.v3 - this.gizmo.transform.position) * Game.cap(Time.deltaTime * 20f + this.firstFrameAccelerator, 0f, 1f);
		this.mountPoint.localPosition = Vector3.zero;
		this.targetCharacter.setPenisAngle(orifice.position, true);
		this.targetCharacter.pushPubic((this.performingCharacter.interactionMY - 0.5f) * 2f);
		float num2 = 0f;
		if (this.inside)
		{
			num2 = 1f;
		}
		switch (orificeType)
		{
		case 0:
			this.performingCharacter.rollRoot(Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 1f, -0.5f, 0.5f) * -24f);
			this.performingCharacter.archBack(Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 1f, -0.5f, 0.5f) * 2f);
			this.performingCharacter.stimulate(this.targetCharacter.interactionVigor * 0.21f * num2 * this.performingCharacter.data.analPleasure);
			this.performingCharacter.arouse(this.targetCharacter.interactionVigor * 0.1f * num2);
			this.performingCharacter.penetrateAnus((this.gizmoPosition - this.lastGizmoPosition) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f) * num2, this.penetrationGirth);
			this.performingCharacter.pushAnus(this.pushingThroughResistance * 0.25f * (1f - this.performingCharacter.currentTailholeTightness * 0.8f));
			this.targetCharacter.stretchPenis(1f + this.pushingThroughResistance * (1f - this.performingCharacter.currentTailholeTightness * 0.8f) * 2f);
			break;
		case 1:
			this.performingCharacter.grindRoot(-25f);
			this.performingCharacter.rollRoot(Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 1f, -0.5f, 0.5f) * -24f);
			this.performingCharacter.archBack(Game.cap(this.performingCharacter.interactionMX + this.performingCharacter.interactionMY - 1f, -0.5f, 0.5f) * 2f);
			this.performingCharacter.stimulate(this.targetCharacter.interactionVigor * 1f * num2);
			this.performingCharacter.arouse(this.targetCharacter.interactionVigor * 0.1f * num2);
			if (this.howFarInWorldUnits > -0.01f)
			{
				float num3 = this.targetCharacter.girthAlongPenetrator(1f - (this.howFarIn + 0.01f / this.targetCharacter.penisLengthInWorldUnits), true);
				this.performingCharacter.spreadVagina(false, 5f * (num3 / this.performingCharacter.height_act));
				this.performingCharacter.spreadVagina(true, 7.5f * (num3 / this.performingCharacter.height_act));
			}
			this.performingCharacter.penetrateVagina((this.gizmoPosition - this.lastGizmoPosition) * Game.cap(this.timeSpentInteracting - 1f, 0f, 1f) * num2, this.penetrationGirth, this.targetCharacter.bones.Penis0.position);
			this.performingCharacter.pushVagina(this.pushingThroughResistance * 0.25f * (1f - this.performingCharacter.currentTailholeTightness * 0.8f));
			this.targetCharacter.stretchPenis(1f + this.pushingThroughResistance * (1f - this.performingCharacter.currentVaginalTightness * 0.8f) * 2f);
			break;
		}
		if (this.inside)
		{
			this.targetCharacter.stimulate(this.performingCharacter.interactionVigor * 0.63f * num2);
			this.targetCharacter.humpBump(this.performingCharacter.interactionMouseChange.y * 0.15f * this.performingCharacter.height_act / this.targetCharacter.height_act);
		}
		this.targetCharacter.buryPenis(orifice.position, this.performingCharacter.bones.SpineUpper.position, this.performingCharacter.bones.SpineUpper.position, this.performingCharacter, 0);
		this.previousGirth = this.penetrationGirth;
		if (this.inside)
		{
			this.targetCharacter.wetness_penis += (1f - this.targetCharacter.wetness_penis) * Game.cap(Time.deltaTime * 0.2f, 0f, 1f);
		}
		if (!this.audioInitted)
		{
			this.initAudio("wetrub_in", "wetrub_out", string.Empty, 0, "slurpOut", 5, 0.5f, 1f, 1.8f, false, true);
		}
		this.processAudio(num, true);
	}

	public void kill()
	{
		if (this.type == "chemicalinjection")
		{
			this.sexToy.playSLS = false;
			this.sexToy.showInUseLight = this.sexToy.playSLS;
			Interaction.chemicalInjectionRate = 0f;
		}
		UnityEngine.Object.Destroy(this.gizmo);
		if (this.isCurrentInteraction)
		{
			this.game.currentInteraction = null;
		}
		this.targetCharacter.freeUpSlot(this.targetNode, this.uid);
		this.targetCharacter.interactionEnded(this.type + "_receiving");
		if (this.hasPerformer)
		{
			this.performingCharacter.removeFromInteraction(this);
			this.performingCharacter.freeUpSlot(this.performingNode, this.uid);
			this.performingCharacter.interactionEnded(this.type + "_performing");
		}
	}
}
