using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("SettingsData")]
public class SettingsData
{
	[XmlElement("gameVersion")]
	public int gameVersion;

	[XmlElement("activeUser")]
	public string activeUser;

	[XmlArray("users")]
	[XmlArrayItem("user")]
	public List<string> users = new List<string>();

	[XmlElement("acceptedTerms")]
	public bool acceptedTerms;

	[XmlArray("tutorialFlags")]
	[XmlArrayItem("tutorialFlag")]
	public List<string> tutorialFlags = new List<string>();

	[XmlArrayItem("userVar")]
	[XmlArray("userVars")]
	public List<UserVar> userVars = new List<UserVar>();

	[XmlArrayItem("cachedTexture")]
	[XmlArray("cachedTextures")]
	public List<string> cachedTextures = new List<string>();

	[XmlElement("KEY_TOGGLE_AUTO")]
	public KeyCode KEY_TOGGLE_AUTO = KeyCode.X;

	[XmlElement("KEY_WALK")]
	public KeyCode KEY_WALK = KeyCode.LeftShift;

	[XmlElement("KEY_JUMP")]
	public KeyCode KEY_JUMP = KeyCode.Space;

	[XmlElement("KEY_SPRINT")]
	public KeyCode KEY_SPRINT = KeyCode.F;

	[XmlElement("KEY_FREECAM")]
	public KeyCode KEY_FREECAM = KeyCode.F;

	[XmlElement("KEY_WALK_FORWARD")]
	public KeyCode KEY_WALK_FORWARD = KeyCode.W;

	[XmlElement("KEY_WALK_BACKWARD")]
	public KeyCode KEY_WALK_BACKWARD = KeyCode.S;

	[XmlElement("KEY_STRAFE_LEFT")]
	public KeyCode KEY_STRAFE_LEFT = KeyCode.A;

	[XmlElement("KEY_STRAFE_RIGHT")]
	public KeyCode KEY_STRAFE_RIGHT = KeyCode.D;

	[XmlElement("KEY_USE")]
	public KeyCode KEY_USE = KeyCode.E;

	[XmlElement("KEY_INTERFACE")]
	public KeyCode KEY_INTERFACE = KeyCode.Tab;

	[XmlElement("KEY_EDITMODE")]
	public KeyCode KEY_EDITMODE = KeyCode.L;

	[XmlElement("KEY_ROTATE_RIGHT")]
	public KeyCode KEY_ROTATE_RIGHT = KeyCode.RightArrow;

	[XmlElement("KEY_ROTATE_LEFT")]
	public KeyCode KEY_ROTATE_LEFT = KeyCode.LeftArrow;

	[XmlElement("KEY_ROTATE_UP")]
	public KeyCode KEY_ROTATE_UP = KeyCode.UpArrow;

	[XmlElement("KEY_ROTATE_DOWN")]
	public KeyCode KEY_ROTATE_DOWN = KeyCode.DownArrow;

	[XmlElement("KEY_SNAP_TO_GRID")]
	public KeyCode KEY_SNAP_TO_GRID = KeyCode.LeftControl;

	[XmlElement("KEY_TRANSLATE")]
	public KeyCode KEY_TRANSLATE = KeyCode.Slash;

	[XmlElement("KEY_DIALOGUE1")]
	public KeyCode KEY_DIALOGUE1 = KeyCode.Alpha1;

	[XmlElement("KEY_DIALOGUE2")]
	public KeyCode KEY_DIALOGUE2 = KeyCode.Alpha2;

	[XmlElement("KEY_DIALOGUE3")]
	public KeyCode KEY_DIALOGUE3 = KeyCode.Alpha3;

	[XmlElement("KEY_DIALOGUE4")]
	public KeyCode KEY_DIALOGUE4 = KeyCode.Alpha4;

	[XmlElement("KEY_DIALOGUE5")]
	public KeyCode KEY_DIALOGUE5 = KeyCode.Alpha5;

	[XmlElement("KEY_DIALOGUE6")]
	public KeyCode KEY_DIALOGUE6 = KeyCode.Alpha6;

	[XmlElement("KEY_DIALOGUE1_ALT")]
	public KeyCode KEY_DIALOGUE1_ALT = KeyCode.Keypad1;

	[XmlElement("KEY_DIALOGUE2_ALT")]
	public KeyCode KEY_DIALOGUE2_ALT = KeyCode.Keypad2;

	[XmlElement("KEY_DIALOGUE3_ALT")]
	public KeyCode KEY_DIALOGUE3_ALT = KeyCode.Keypad3;

	[XmlElement("KEY_DIALOGUE4_ALT")]
	public KeyCode KEY_DIALOGUE4_ALT = KeyCode.Keypad4;

	[XmlElement("KEY_DIALOGUE5_ALT")]
	public KeyCode KEY_DIALOGUE5_ALT = KeyCode.Keypad5;

	[XmlElement("KEY_DIALOGUE6_ALT")]
	public KeyCode KEY_DIALOGUE6_ALT = KeyCode.Keypad6;

	[XmlElement("KEY_SELECTITEM0")]
	public KeyCode KEY_SELECTITEM0 = KeyCode.Keypad1;

	[XmlElement("KEY_SELECTITEM1")]
	public KeyCode KEY_SELECTITEM1 = KeyCode.Keypad2;

	[XmlElement("KEY_SELECTITEM2")]
	public KeyCode KEY_SELECTITEM2 = KeyCode.Keypad3;

	[XmlElement("KEY_SELECTITEM3")]
	public KeyCode KEY_SELECTITEM3 = KeyCode.Keypad4;

	[XmlElement("KEY_SELECTITEM4")]
	public KeyCode KEY_SELECTITEM4 = KeyCode.Keypad5;

	[XmlElement("KEY_SELECTITEM5")]
	public KeyCode KEY_SELECTITEM5 = KeyCode.Keypad6;

	[XmlElement("KEY_SELECTITEM6")]
	public KeyCode KEY_SELECTITEM6 = KeyCode.Keypad7;

	[XmlElement("KEY_SELECTITEM7")]
	public KeyCode KEY_SELECTITEM7 = KeyCode.Keypad8;

	[XmlElement("KEY_SELECTITEM8")]
	public KeyCode KEY_SELECTITEM8 = KeyCode.Keypad9;

	[XmlElement("KEY_SELECTITEM9")]
	public KeyCode KEY_SELECTITEM9 = KeyCode.Keypad0;

	[XmlElement("KEY_SELECTITEM0_ALT")]
	public KeyCode KEY_SELECTITEM0_ALT = KeyCode.Alpha1;

	[XmlElement("KEY_SELECTITEM1_ALT")]
	public KeyCode KEY_SELECTITEM1_ALT = KeyCode.Alpha2;

	[XmlElement("KEY_SELECTITEM2_ALT")]
	public KeyCode KEY_SELECTITEM2_ALT = KeyCode.Alpha3;

	[XmlElement("KEY_SELECTITEM3_ALT")]
	public KeyCode KEY_SELECTITEM3_ALT = KeyCode.Alpha4;

	[XmlElement("KEY_SELECTITEM4_ALT")]
	public KeyCode KEY_SELECTITEM4_ALT = KeyCode.Alpha5;

	[XmlElement("KEY_SELECTITEM5_ALT")]
	public KeyCode KEY_SELECTITEM5_ALT = KeyCode.Alpha6;

	[XmlElement("KEY_SELECTITEM6_ALT")]
	public KeyCode KEY_SELECTITEM6_ALT = KeyCode.Alpha7;

	[XmlElement("KEY_SELECTITEM7_ALT")]
	public KeyCode KEY_SELECTITEM7_ALT = KeyCode.Alpha8;

	[XmlElement("KEY_SELECTITEM8_ALT")]
	public KeyCode KEY_SELECTITEM8_ALT = KeyCode.Alpha9;

	[XmlElement("KEY_SELECTITEM9_ALT")]
	public KeyCode KEY_SELECTITEM9_ALT = KeyCode.Alpha0;

	[XmlElement("KEY_TOGGLE_OBJECTIVES")]
	public KeyCode KEY_TOGGLE_OBJECTIVES = KeyCode.F1;

	[XmlElement("KEY_TOGGLE_GADGETS")]
	public KeyCode KEY_TOGGLE_GADGETS = KeyCode.F2;

	[XmlElement("KEY_TOGGLE_THOUGHT_GAUGE")]
	public KeyCode KEY_TOGGLE_THOUGHT_GAUGE = KeyCode.F3;

	[XmlElement("KEY_TOGGLE_HOLOGRAM_HUDS")]
	public KeyCode KEY_TOGGLE_HOLOGRAM_HUDS = KeyCode.F4;

	[XmlElement("KEY_INLINE_DIALOGUE")]
	public KeyCode KEY_INLINE_DIALOGUE = KeyCode.E;

	[XmlElement("KEY_SCREENSHOT")]
	public KeyCode KEY_SCREENSHOT = KeyCode.F12;

	[XmlElement("language")]
	public string language = "english";

	[XmlElement("textureQuality")]
	public float characterTextureQuality = 0.25f;

	[XmlElement("IKquality")]
	public float IKquality = 0.5f;

	[XmlElement("needDirectoryRebuild")]
	public bool needDirectoryRebuild;

	[XmlElement("vol")]
	public float vol = 0.9f;

	[XmlElement("volWorld")]
	public float volWorld = 0.9f;

	[XmlElement("volUI")]
	public float volUI = 0.9f;

	[XmlElement("volBGM")]
	public float volBGM = 0.6f;

	[XmlElement("sexSquishNoiseVolume")]
	public float sexSquishNoiseVolume = 0.5f;

	[XmlElement("dialogueSpeed")]
	public float dialogueSpeed = 50f;

	[XmlElement("lookSensitivity")]
	public float lookSensitivity = 1f;

	[XmlElement("interactionSensitivity")]
	public float interactionSensitivity = 1.2f;

	[XmlArray("fetishPreferences")]
	[XmlArrayItem("fetishPreference")]
	public List<FetishPref> fetishPreference = new List<FetishPref>();

	[XmlElement("stylePreference")]
	public float stylePreference;

	[XmlArray("genderPreferences")]
	[XmlArrayItem("genderPreference")]
	public List<float> genderPreferences = new List<float>();

	[XmlArray("bodyTypePreferences")]
	[XmlArrayItem("bodyTypePreference")]
	public List<float> bodyTypePreferences = new List<float>();

	[XmlArray("speciesPreferences")]
	[XmlArrayItem("speciesPreference")]
	public List<SpeciesPref> speciesPreferences = new List<SpeciesPref>();

	[XmlElement("invertY")]
	public bool invertY;

	[XmlElement("dynamicSensitivity")]
	public bool dynamicSensitivity = true;

	[XmlElement("ghostTailsDuringSex")]
	public bool ghostTailsDuringSex;

	[XmlElement("ghostBodyDuringSex")]
	public bool ghostBodyDuringSex = true;

	[XmlElement("defaultToFreeCam")]
	public bool defaultToFreeCam;

	[XmlElement("testSubjectEmotes")]
	public bool testSubjectEmotes = true;

	[XmlElement("defaultControlMode")]
	public int defaultControlMode;

	[XmlElement("defaultMoveMode")]
	public int defaultMoveMode = 1;

	[XmlElement("cameraSmoothing")]
	public float cameraSmoothing = 0.82f;

	[XmlElement("orgasmSpeed")]
	public float orgasmSpeed = 0.5f;

	[XmlElement("customCharacterFrequency")]
	public float customCharacterFrequency = 0.2f;

	[XmlElement("racknetCharacterFrequency")]
	public float racknetCharacterFrequency = 0.2f;

	[XmlElement("favoriteCharacterFrequency")]
	public float favoriteCharacterFrequency = 0.2f;

	[XmlElement("antialiasing")]
	public bool antialiasing = true;

	[XmlElement("bloom")]
	public bool bloom = true;

	[XmlElement("dof")]
	public bool dof = true;

	[XmlElement("glow")]
	public bool glow = true;

	[XmlElement("ssao")]
	public bool ssao = true;

	[XmlElement("mouseParticles")]
	public bool mouseParticles = true;

	[XmlElement("hideObjectivesDuringSex")]
	public bool hideObjectivesDuringSex = true;

	[XmlElement("freezeOtherSubjectsWhileInteracting")]
	public bool freezeOtherSubjectsWhileInteracting = true;

	[XmlElement("autoJoinRacknet")]
	public bool autoJoinRacknet = true;

	[XmlElement("autoComment")]
	public bool autoComment;

	[XmlElement("autoPhysics")]
	public bool autoPhysics = true;

	[XmlElement("physicsQuality")]
	public float physicsQuality = 0.5f;

	[XmlElement("selectedChemicalCompound")]
	public string selectedChemicalCompound = string.Empty;

	[XmlElement("mod_fixNpcCum")]
	public bool mod_fixNpcCum = true;

	[XmlElement("mod_nudeNpcs")]
	public bool mod_nudeNpcs = true;

	[XmlElement("mod_inheritRng")]
	public bool mod_inheritRng = true;

	[XmlElement("mod_rngStatColor")]
	public Color mod_rngStatColor = Color.magenta;

	[XmlElement("mod_sexToyPotency")]
	public float mod_sexToyPotency = 0.75f;

	[XmlElement("mod_extraCommands")]
	public bool mod_extraCommands = true;

	[XmlElement("mod_altCumStyle")]
	public bool mod_altCumStyle;

	[XmlElement("mod_cumLife")]
	public int mod_cumLife = 40;

	[XmlElement("mod_hideLoadScreens")]
	public bool mod_hideLoadScreens;

	[XmlIgnore]
	public bool modx_noCumException = true;

	[XmlIgnore]
	public bool modx_alwaysHaveBalls;

	[XmlElement("mod_logErrors")]
	public bool mod_logErrors = true;

	[XmlElement("mod_rngRanges")]
	public RNGPrefs mod_rngRanges = new RNGPrefs();

	[XmlElement("mod_ballsMoveOnCum")]
	public float mod_ballsMoveOnCum = 1f;

	[XmlElement("mod_ballsScaleOnCum")]
	public float mod_ballsScaleOnCum = 1f;

	[XmlElement("mod_AleksMods")]
	public bool mod_AleksMods = true;

	[XmlElement("KEY_MOD_PLEASURE_SELF")]
	public KeyCode KEY_MOD_PLEASURE_SELF = KeyCode.F5;

	[XmlElement("KEY_MOD_HANDJOB_OTHER")]
	public KeyCode KEY_MOD_HANDJOB_OTHER = KeyCode.F6;

	[XmlElement("KEY_MOD_ORAL_OTHER")]
	public KeyCode KEY_MOD_ORAL_OTHER = KeyCode.F7;

	[XmlElement("KEY_MOD_RIDE_OTHER")]
	public KeyCode KEY_MOD_RIDE_OTHER = KeyCode.F8;

	[XmlElement("KEY_MOD_FINGER_OTHER")]
	public KeyCode KEY_MOD_FINGER_OTHER = KeyCode.F9;

	[XmlElement("KEY_MOD_FUCK_OTHER")]
	public KeyCode KEY_MOD_FUCK_OTHER = KeyCode.F10;

	[XmlElement("KEY_MOD_PISS_SELF")]
	public KeyCode KEY_MOD_PISS_SELF = KeyCode.F11;

	[XmlElement("KEY_MOD_PISS_OTHER")]
	public KeyCode KEY_MOD_PISS_OTHER = KeyCode.F12;

	[XmlElement("KEY_MOD_TOOL_PREVIOUS")]
	public KeyCode KEY_MOD_TOOL_PREVIOUS = KeyCode.Equals;

	[XmlElement("KEY_MOD_TOOL_NEXT")]
	public KeyCode KEY_MOD_TOOL_NEXT = KeyCode.Minus;

	[XmlElement("KEY_MOD_STOP_LAST_INTERACTION")]
	public KeyCode KEY_MOD_STOP_LAST_INTERACTION = KeyCode.Delete;

	[XmlElement("KEY_MOD_STOP_ALL_INTERACTIONS")]
	public KeyCode KEY_MOD_STOP_ALL_INTERACTIONS = KeyCode.End;

	[XmlElement("KEY_MOD_ORGASM_SELF")]
	public KeyCode KEY_MOD_ORGASM_SELF = KeyCode.LeftBracket;

	[XmlElement("KEY_MOD_ORGASM_OTHER")]
	public KeyCode KEY_MOD_ORGASM_OTHER = KeyCode.RightBracket;

	[XmlElement("KEY_MOD_CONTROL_OTHER")]
	public KeyCode KEY_MOD_CONTROL_OTHER = KeyCode.Insert;

	[XmlElement("KEY_MOD_CONTROL_SELF")]
	public KeyCode KEY_MOD_CONTROL_SELF = KeyCode.Home;

	[XmlElement("KEY_MOD_AROUSE_ALL")]
	public KeyCode KEY_MOD_AROUSE_ALL = KeyCode.PageUp;

	[XmlElement("KEY_MOD_UNAROUSE_ALL")]
	public KeyCode KEY_MOD_UNAROUSE_ALL = KeyCode.PageDown;

	[XmlElement("KEY_MOD_CLEAN_UP_CUM")]
	public KeyCode KEY_MOD_CLEAN_UP_CUM = KeyCode.Slash;

	[XmlElement("globalSensitivity")]
	public float globalSensitivity = 0.5f;
}
