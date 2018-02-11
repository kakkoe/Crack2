using System;
using System.Collections.Generic;
using UnityEngine;

public class BondageApparatus : MonoBehaviour
{
	private Game game;

	public int uid;

	public static int nextUID;

	public RackCharacter pc;

	public float triggerDistance = 10f;

	public float backArchAmount;

	public float rootGrindAmount;

	public RackCharacter boundCharacter;

	public string poseName;

	public bool hipConstraint;

	public Vector3 originalHandLPosition;

	public Quaternion originalHandLRotation;

	public Vector3 originalHandRPosition;

	public Quaternion originalHandRRotation;

	public Vector3 originalFootLPosition;

	public Vector3 originalFootLRotation;

	public Vector3 originalFootRPosition;

	public Vector3 originalFootRRotation;

	public Transform moveWithHandL;

	public Transform moveWithHandR;

	public Transform moveWithAnkleL;

	public Transform moveWithAnkleR;

	private BondagePoint[] bondagePoints;

	public int sessionOrgasms;

	public float[] sessionSpecimenAnticipated;

	public float[] sessionSpecimenCollected;

	public DateTime sessionStart;

	public float totalSessionSpecimenCollected;

	public static Color c1_g;

	public static Color c2_g;

	public static Color c3_g;

	public static Color c1_r;

	public static Color c2_r;

	public static Color c3_r;

	public static Color c1_w;

	public static Color c2_w;

	public static Color c3_w;

	public float[] arousalHistory;

	public string poseName0 = string.Empty;

	public string poseName1 = string.Empty;

	public string poseName2 = string.Empty;

	public string poseName3 = string.Empty;

	public string poseName4 = string.Empty;

	public string poseName5 = string.Empty;

	public List<string> poseNames = new List<string>();

	public List<string> publicPoseNames = new List<string>();

	public string apparatusType;

	public bool hologramsInitialized;

	public ApproachPoint[] approachPoints;

	public Transform rootPoint;

	public float timeAlive;

	private float sessionOrgasmDisplayTime;

	public string interactionPointName;

	private float backBendFromPerformance;

	public Vector3 positioner = default(Vector3);

	private bool lastEffectivelyPlantigrade = true;

	private int nextRock;

	private bool inRange;

	private bool hadBoundSubject;

	private Vector3 v3;

	private Vector3 v32;

	private Vector3 v33;

	private float highestSpecimenCollection;

	private float sensitivityPulse;

	private float anticipationPulse;

	private float arousalHistoryTick;

	private float orgasmPulse;

	private float heatPulse;

	private float refractoryColor;

	private float painSmoother;

	private float heightAdjustment;

	private float heightAdjustmentVelocity;

	public bool adjustingHeight;

	private bool wasAdjustingHeight;

	public bool showingHologramRealtime;

	private bool wasShowingHologramRealtime;

	public bool showingHologramHarvest;

	private bool wasShowingHologramHarvest;

	private RackCharacter performingCharacter;

	public static int lastClosestID = -1;

	public static int closestID = -1;

	public static float closestDist = 15f;

	private float performingPriority;

	public static bool showHologramHUDs = true;

	public Transform hologramHarvest;

	public Transform hologramRealtime;

	public Transform hologramHarvestContents;

	public Transform hologramRealtimeContents;

	public Transform HologramHarvestTemplate;

	public Transform HologramRealtimeTemplate;

	private Transform RackChair_stirrupL;

	private Transform RackChair_stirrupR;

	private Transform headrest;

	private Vector3 headrestAngle;

	private AudioSource audioSource;

	public Transform scaleWithCharacter;

	private TextMesh txtScience0;

	private TextMesh txtScience1;

	private TextMesh txtName;

	private TextMesh txtOrgasms;

	private TextMesh txtSpecimen;

	private TextMesh txtSpecimenCount;

	private TextMesh txtSpecimenCount2;

	private TextMesh txtSamples;

	private TextMesh txtSessionDuration;

	private Transform[] specimenBars = new Transform[6];

	private Transform[] specimenFrames = new Transform[6];

	private TextMesh txtSensitivity;

	private Transform sensitivityIndicator;

	private Transform sensitivityHeat;

	private MeshRenderer sensitivityHeatMR;

	private TextMesh txtAnticipation;

	private Transform anticipationHeart;

	private Transform anticipationHeart1;

	private MeshRenderer anticipationHeartMR;

	private MeshRenderer anticipationHeart1MR;

	private Transform[] arousalHistories = new Transform[15];

	private MeshRenderer[] arousalHistoriesMR = new MeshRenderer[15];

	private TextMesh txtArousal;

	private TextMesh txtClimax;

	private SkinnedMeshRenderer orgasmMeterCurve;

	private SkinnedMeshRenderer pleasureMeterCurve;

	private Vector3 originalRackChairstirrupLposition;

	private Vector3 originalRackChairstirrupRposition;

	private bool hideHologramsForTutorialReasons = true;

	private float undergroundAmount;

	public float undergroundThreshold = 0.4f;

	public bool blockHeadMovement_back;

	private void Start()
	{
		this.uid = BondageApparatus.nextUID;
		BondageApparatus.nextUID++;
		this.sessionOrgasms = 0;
		this.sessionSpecimenAnticipated = new float[6];
		this.sessionSpecimenCollected = new float[6];
		this.bondagePoints = base.GetComponentsInChildren<BondagePoint>();
		for (int i = 0; i < this.bondagePoints.Length; i++)
		{
			if (this.bondagePoints[i].name == "Foot_L")
			{
				this.originalFootLRotation = this.bondagePoints[i].transform.localEulerAngles;
			}
			if (this.bondagePoints[i].name == "Foot_R")
			{
				this.originalFootRRotation = this.bondagePoints[i].transform.localEulerAngles;
			}
			if (this.bondagePoints[i].name == "Root")
			{
				this.rootPoint = this.bondagePoints[i].transform;
			}
		}
		BondageApparatus.c1_r = ColorPicker.HexToColor("FF7900");
		BondageApparatus.c2_r = ColorPicker.HexToColor("FF5F4C");
		BondageApparatus.c3_r = ColorPicker.HexToColor("812020");
		BondageApparatus.c1_g = ColorPicker.HexToColor("22F394");
		BondageApparatus.c2_g = ColorPicker.HexToColor("3AF32B");
		BondageApparatus.c3_g = ColorPicker.HexToColor("379700");
		BondageApparatus.c1_w = ColorPicker.HexToColor("FFFFFF");
		BondageApparatus.c2_w = ColorPicker.HexToColor("56C8F7");
		BondageApparatus.c3_w = ColorPicker.HexToColor("FFFFFF");
		this.poseNames = new List<string>();
		if (this.poseName0.Contains("*"))
		{
			this.poseName0 = this.poseName0.Remove(this.poseName0.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName0);
		}
		if (this.poseName1.Contains("*"))
		{
			this.poseName1 = this.poseName1.Remove(this.poseName1.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName1);
		}
		if (this.poseName2.Contains("*"))
		{
			this.poseName2 = this.poseName2.Remove(this.poseName2.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName2);
		}
		if (this.poseName3.Contains("*"))
		{
			this.poseName3 = this.poseName3.Remove(this.poseName3.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName3);
		}
		if (this.poseName4.Contains("*"))
		{
			this.poseName4 = this.poseName4.Remove(this.poseName4.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName4);
		}
		if (this.poseName5.Contains("*"))
		{
			this.poseName5 = this.poseName5.Remove(this.poseName5.IndexOf('*'), 1);
			this.publicPoseNames.Add(this.poseName5);
		}
		while (this.publicPoseNames.Count < 6)
		{
			this.publicPoseNames.Add(string.Empty);
		}
		this.poseNames.Add(this.poseName0);
		this.poseNames.Add(this.poseName1);
		this.poseNames.Add(this.poseName2);
		this.poseNames.Add(this.poseName3);
		this.poseNames.Add(this.poseName4);
		this.poseNames.Add(this.poseName5);
		this.approachPoints = base.GetComponentsInChildren<ApproachPoint>();
	}

	public void addOrgasmToCounter()
	{
		this.sessionOrgasms++;
		this.sessionOrgasmDisplayTime = 5f;
		Game.PlaySFXAtPoint(Resources.Load("orgasm_beep") as AudioClip, this.hologramHarvest.gameObject.transform.position, 1f);
	}

	public void terminate()
	{
		this.sessionOrgasms = 0;
		for (int i = 0; i < this.sessionSpecimenCollected.Length; i++)
		{
			this.sessionSpecimenCollected[i] = 0f;
		}
		this.hadBoundSubject = true;
		this.totalSessionSpecimenCollected = 0f;
		this.arousalHistory = new float[15];
		this.boundCharacter = null;
	}

	public Transform getApproachPointByName(string name)
	{
		for (int i = 0; i < this.approachPoints.Length; i++)
		{
			if (this.approachPoints[i].name == "ApproachPoint." + name)
			{
				return this.approachPoints[i].transform;
			}
		}
		Debug.Log("WARNING: Failed to find approach point with name '" + name + "'!");
		return this.approachPoints[0].transform;
	}

	private bool interactWithSubject()
	{
		Game.gameInstance.PC().interactWithTestSubject(this.boundCharacter, this.getApproachPointByName("default"), this);
		Game.gameInstance.PC().setSexPose(0);
		return true;
	}

	public void OnEnable()
	{
		if (this.boundCharacter != null && this.boundCharacter.initted)
		{
			this.boundCharacter.ignoreCollisions(base.transform, false, null);
		}
	}

	private string timeformat(TimeSpan span)
	{
		string text = span.Milliseconds.ToString();
		string text2 = span.Seconds.ToString();
		string text3 = span.Minutes.ToString();
		string text4 = span.Hours.ToString();
		while (text.Length < 3)
		{
			text = "0" + text;
		}
		while (text2.Length < 2)
		{
			text2 = "0" + text2;
		}
		while (text3.Length < 2)
		{
			text3 = "0" + text3;
		}
		return text4 + ":" + text3 + ":" + text2 + "." + text;
	}

	public void createPerformerMountPoints(RackCharacter performer)
	{
		if (performer.temporaryPerformerMountPoints.Count <= 0)
		{
			switch (this.poseName)
			{
			case "RackChair":
			{
				string str = "DEFAULT";
				GameObject gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject.transform.SetParent(this.boundCharacter.bones.UpperLeg_R);
				GameObject gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.UpperLeg_L);
				GameObject gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				GameObject gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				GameObject gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				GameObject gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.518f;
				this.v3.y = 0.01f;
				this.v3.z = 0.199f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 14.03857f;
				this.v3.y = 346.5793f;
				this.v3.z = 162.0554f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.508f;
				this.v3.y = -0.143f;
				this.v3.z = 0.22f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 16.47517f;
				this.v3.y = 353.5221f;
				this.v3.z = 212.8844f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0.07f;
				this.v3.y = 1.93f;
				this.v3.z = 0.88f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = -0.15f;
				this.v3.y = -2.03f;
				this.v3.z = 0.62f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 0.54f;
				this.v3.y = 2.04f;
				this.v3.z = -2.95f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 0.22f;
				this.v3.y = -1.211f;
				this.v3.z = -3.094f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "HIGHLIFT";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject.transform.SetParent(this.boundCharacter.bones.UpperLeg_R);
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.UpperLeg_L);
				this.v3.x = -0.234f;
				this.v3.y = 0.359f;
				this.v3.z = -0.181f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 287.7924f;
				this.v3.y = 318.4449f;
				this.v3.z = 118.3134f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.26f;
				this.v3.y = -0.43f;
				this.v3.z = -0.149f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 61.03807f;
				this.v3.y = 322.2742f;
				this.v3.z = 249.4643f;
				gameObject2.transform.localEulerAngles = this.v3;
				str = "RIDING";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject.transform.SetParent(this.boundCharacter.bones.SpineUpper);
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.SpineUpper);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.236f;
				this.v3.y = -0.458f;
				this.v3.z = -0.369f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 15.90605f;
				this.v3.y = 215.9374f;
				this.v3.z = 197.4012f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.25f;
				this.v3.y = 0.364f;
				this.v3.z = -0.323f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 342.2589f;
				this.v3.y = 198.3958f;
				this.v3.z = 188.1647f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0.763f;
				this.v3.z = 0f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = -0.863f;
				this.v3.z = 0f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 0.54f;
				this.v3.y = 2.04f;
				this.v3.z = -2.95f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 0.22f;
				this.v3.y = -1.211f;
				this.v3.z = -3.094f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "FUCK";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject.transform.SetParent(this.boundCharacter.bones.Hip_R);
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_L);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.54f;
				this.v3.y = 0.265f;
				this.v3.z = 0.19f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 2.449998f;
				this.v3.y = 296.186f;
				this.v3.z = 105.66f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.503f;
				this.v3.y = -0.369f;
				this.v3.z = 0.209f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 6.510003f;
				this.v3.y = 302.56f;
				this.v3.z = 262.304f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = 1.36f;
				this.v3.z = -0.72f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.72f;
				this.v3.y = -1.95f;
				this.v3.z = -0.45f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 2.13f;
				this.v3.y = 1.23f;
				this.v3.z = -2.4f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 2.1f;
				this.v3.y = -0.79f;
				this.v3.z = -2.94f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				break;
			}
			case "Stocks":
			{
				string str = "DEFAULT";
				GameObject gameObject = new GameObject(this.poseName + "." + str + ".handL");
				GameObject gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_R);
				gameObject.transform.SetParent(this.boundCharacter.bones.Hip_L);
				GameObject gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				GameObject gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				GameObject gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				GameObject gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.41f;
				this.v3.y = 0.415f;
				this.v3.z = 0.224f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 353.289f;
				this.v3.y = 317.433f;
				this.v3.z = 95.602f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.41f;
				this.v3.y = -0.415f;
				this.v3.z = 0.224f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 6.711001f;
				this.v3.y = 317.433f;
				this.v3.z = 264.398f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0.75f;
				this.v3.y = 1.29f;
				this.v3.z = -0.28f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.8f;
				this.v3.y = -1.01f;
				this.v3.z = -0.39f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 1.82f;
				this.v3.y = 0.86f;
				this.v3.z = -2.83f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 1.81f;
				this.v3.y = -0.66f;
				this.v3.z = -3.45f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "FUCK";
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				this.v3.x = 0.75f;
				this.v3.y = 0.941f;
				this.v3.z = 1.554f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.8f;
				this.v3.y = -1.354f;
				this.v3.z = 1.227f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				str = "FACEFUCK";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.Head);
				gameObject.transform.SetParent(this.boundCharacter.bones.Head);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.44f;
				this.v3.y = -0.295f;
				this.v3.z = -0.111f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 44.9784f;
				this.v3.y = 320.3191f;
				this.v3.z = 229.6641f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.478f;
				this.v3.y = 0.307f;
				this.v3.z = -0.145f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 325.0858f;
				this.v3.y = 307.4168f;
				this.v3.z = 151.0075f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = -0.66f;
				this.v3.y = 1.465f;
				this.v3.z = 1.97f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = -0.56f;
				this.v3.y = -1.11f;
				this.v3.z = 1.639f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				this.v3.x = 0.84f;
				this.v3.y = 1.74f;
				this.v3.z = -4.2f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 1.16f;
				this.v3.y = -0.32f;
				this.v3.z = -3.91f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "HIGHLIFTBEHIND";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_R);
				gameObject.transform.SetParent(this.boundCharacter.bones.Hip_L);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				this.v3.x = -0.562f;
				this.v3.y = 0.235f;
				this.v3.z = -0.313f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = -47f;
				this.v3.y = -86f;
				this.v3.z = 194f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.58f;
				this.v3.y = -0.209f;
				this.v3.z = -0.325f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 44f;
				this.v3.y = -91f;
				this.v3.z = -203f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 1.52f;
				this.v3.y = 0.69f;
				this.v3.z = -0.26f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.8f;
				this.v3.y = 0.31f;
				this.v3.z = -1.78f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				str = "HIGHLIFT";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.UpperLeg_L);
				gameObject.transform.SetParent(this.boundCharacter.bones.UpperLeg_R);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.175f;
				this.v3.y = 0.159f;
				this.v3.z = -0.433f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 33.659f;
				this.v3.y = 180.652f;
				this.v3.z = 89.992f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.072f;
				this.v3.y = -0.054f;
				this.v3.z = -0.429f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 313.207f;
				this.v3.y = 164.305f;
				this.v3.z = 257.753f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0.91f;
				this.v3.y = 0.29f;
				this.v3.z = -1.51f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 1.4f;
				this.v3.y = -0.5f;
				this.v3.z = -1.663f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 2.14f;
				this.v3.y = 0.76f;
				this.v3.z = -3.71f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 2.64f;
				this.v3.y = -0.57f;
				this.v3.z = -2.68f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				break;
			}
			case "TableStraps":
			{
				string str = "DEFAULT";
				GameObject gameObject = new GameObject(this.poseName + "." + str + ".handL");
				GameObject gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_R);
				gameObject.transform.SetParent(this.boundCharacter.bones.LowerArm_R);
				GameObject gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				GameObject gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				GameObject gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				GameObject gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.225f;
				this.v3.y = -0.142f;
				this.v3.z = 0.164f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 340.9062f;
				this.v3.y = 348.9904f;
				this.v3.z = 281.9327f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.584f;
				this.v3.y = -0.048f;
				this.v3.z = 0.271f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 328.009f;
				this.v3.y = 277.118f;
				this.v3.z = 285.878f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 1.13f;
				this.v3.y = 1.15f;
				this.v3.z = 1.26f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.181f;
				this.v3.y = -1.456f;
				this.v3.z = 0f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 0.15f;
				this.v3.y = 1.7f;
				this.v3.z = -3.45f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 0.17f;
				this.v3.y = -0.85f;
				this.v3.z = -3.37f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "MISSIONARY";
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				this.v3.x = 1.75f;
				this.v3.y = 1.5f;
				this.v3.z = 0f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 1.72f;
				this.v3.y = -1.83f;
				this.v3.z = 0f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				str = "SIXTYNINE";
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				this.v3.x = 1.36f;
				this.v3.y = 2.15f;
				this.v3.z = -0.4f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 1.63f;
				this.v3.y = -1.93f;
				this.v3.z = -0.45f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				str = "RIDING";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject.transform.SetParent(performer.bones.UpperLeg_L);
				performer.temporaryPerformerMountPoints.Add(gameObject);
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(performer.bones.UpperLeg_R);
				performer.temporaryPerformerMountPoints.Add(gameObject2);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				this.v3.x = 0.093f;
				this.v3.y = 0.354f;
				this.v3.z = -0.017f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 285.99f;
				this.v3.y = 28.005f;
				this.v3.z = 349.805f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = 0.026f;
				this.v3.y = -0.282f;
				this.v3.z = -0.125f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 87.03707f;
				this.v3.y = 103.1201f;
				this.v3.z = 63.20007f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0.55f;
				this.v3.y = 1.36f;
				this.v3.z = 0.97f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.54f;
				this.v3.y = -1.15f;
				this.v3.z = 0.96f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				str = "FACEFUCK";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject2.transform.SetParent(this.boundCharacter.bones.SpineUpper);
				gameObject.transform.SetParent(this.boundCharacter.bones.SpineUpper);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.78f;
				this.v3.y = 0.28f;
				this.v3.z = -0.18f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 341.9517f;
				this.v3.y = 220.6618f;
				this.v3.z = 354.9203f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.798f;
				this.v3.y = -0.272f;
				this.v3.z = -0.064f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 24.72046f;
				this.v3.y = 226.6102f;
				this.v3.z = 12.00988f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0.55f;
				this.v3.y = 1.36f;
				this.v3.z = 0.97f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0.54f;
				this.v3.y = -1.15f;
				this.v3.z = 0.96f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 0.15f;
				this.v3.y = 1.7f;
				this.v3.z = -3.45f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 0.17f;
				this.v3.y = -0.85f;
				this.v3.z = -3.37f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				break;
			}
			case "UpsideDown":
			{
				string str = "FACEFUCK";
				GameObject gameObject = new GameObject(this.poseName + "." + str + ".handL");
				GameObject gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject.transform.SetParent(this.boundCharacter.bones.SpineMiddle);
				gameObject2.transform.SetParent(this.boundCharacter.bones.SpineMiddle);
				this.v3.x = 0.046f;
				this.v3.y = 0.525f;
				this.v3.z = -0.273f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 292.4869f;
				this.v3.y = 18.18147f;
				this.v3.z = 49.71112f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = 0.05f;
				this.v3.y = -0.444f;
				this.v3.z = -0.391f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 78.99493f;
				this.v3.y = 57.49492f;
				this.v3.z = 336.2512f;
				gameObject2.transform.localEulerAngles = this.v3;
				str = "DEFAULT";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject.transform.SetParent(this.boundCharacter.bones.Hip_L);
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_R);
				GameObject gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				GameObject gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				GameObject gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				GameObject gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.612f;
				this.v3.y = -0.236f;
				this.v3.z = -0.339f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 13.59f;
				this.v3.y = 177.73f;
				this.v3.z = 307.798f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.745f;
				this.v3.y = 0.244f;
				this.v3.z = -0.318f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = 341.605f;
				this.v3.y = 186.002f;
				this.v3.z = 48.77f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = 1.99f;
				this.v3.z = 0f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = -2.34f;
				this.v3.z = 0f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 2.53f;
				this.v3.y = 0.96f;
				this.v3.z = -3.48f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 2.38f;
				this.v3.y = -0.87f;
				this.v3.z = -2.45f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				str = "BEHIND";
				gameObject = new GameObject(this.poseName + "." + str + ".handL");
				gameObject2 = new GameObject(this.poseName + "." + str + ".handR");
				gameObject.transform.SetParent(this.boundCharacter.bones.Hip_R);
				gameObject2.transform.SetParent(this.boundCharacter.bones.Hip_L);
				gameObject3 = new GameObject(this.poseName + "." + str + ".elbowL");
				gameObject3.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject3);
				gameObject4 = new GameObject(this.poseName + "." + str + ".elbowR");
				gameObject4.transform.SetParent(performer.bones.SpineUpper);
				performer.temporaryPerformerMountPoints.Add(gameObject4);
				gameObject5 = new GameObject(this.poseName + "." + str + ".kneeL");
				gameObject5.transform.SetParent(performer.bones.SpineUpper);
				gameObject6 = new GameObject(this.poseName + "." + str + ".kneeR");
				gameObject6.transform.SetParent(performer.bones.SpineUpper);
				this.v3.x = -0.44f;
				this.v3.y = -0.434f;
				this.v3.z = -0.299f;
				gameObject.transform.localPosition = this.v3;
				this.v3.x = 2.99f;
				this.v3.y = 197.74f;
				this.v3.z = -60.38f;
				gameObject.transform.localEulerAngles = this.v3;
				this.v3.x = -0.404f;
				this.v3.y = 0.521f;
				this.v3.z = -0.159f;
				gameObject2.transform.localPosition = this.v3;
				this.v3.x = -28.77f;
				this.v3.y = -177.66f;
				this.v3.z = 60.28f;
				gameObject2.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = 1.99f;
				this.v3.z = 0f;
				gameObject3.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject3.transform.localEulerAngles = this.v3;
				this.v3.x = 0f;
				this.v3.y = -2.34f;
				this.v3.z = 0f;
				gameObject4.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject4.transform.localEulerAngles = this.v3;
				this.v3.x = 2.53f;
				this.v3.y = 0.96f;
				this.v3.z = -3.48f;
				gameObject5.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject5.transform.localEulerAngles = this.v3;
				this.v3.x = 2.38f;
				this.v3.y = -0.87f;
				this.v3.z = -2.45f;
				gameObject6.transform.localPosition = this.v3;
				this.v3.x = 0f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				gameObject6.transform.localEulerAngles = this.v3;
				break;
			}
			}
			for (int i = 0; i < performer.temporaryPerformerMountPoints.Count; i++)
			{
				performer.temporaryPerformerMountPoints[i].AddComponent<GlobalFollower>();
				performer.temporaryPerformerMountPoints[i].GetComponent<GlobalFollower>().checkInit();
			}
		}
	}

	public void positionPerformerBasedOnSelectedPose(RackCharacter performer, string pose)
	{
		this.interactionPointName = this.poseName + "." + pose;
		if (performer.lastInteractionPointName != this.interactionPointName)
		{
			performer.interactionPoint = this.getApproachPointByName(pose);
			performer.lastInteractionPointName = this.interactionPointName;
			performer.stepLiftAmount = 0f;
			performer.stepLifting = false;
		}
		string text = this.interactionPointName;
		if (text != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(21);
			dictionary.Add("RackChair.default", 0);
			dictionary.Add("RackChair.lifted", 1);
			dictionary.Add("RackChair.eyelevel", 2);
			dictionary.Add("RackChair.highlift", 3);
			dictionary.Add("RackChair.fuck", 4);
			dictionary.Add("RackChair.riding", 5);
			dictionary.Add("Stocks.default", 6);
			dictionary.Add("Stocks.lifted", 7);
			dictionary.Add("Stocks.fuck", 8);
			dictionary.Add("Stocks.highliftBehind", 9);
			dictionary.Add("Stocks.facefuck", 10);
			dictionary.Add("Stocks.highlift", 11);
			dictionary.Add("TableStraps.default", 12);
			dictionary.Add("TableStraps.missionary", 13);
			dictionary.Add("TableStraps.sixtynine", 14);
			dictionary.Add("TableStraps.facefuck", 15);
			dictionary.Add("TableStraps.riding", 16);
			dictionary.Add("UpsideDown.default", 17);
			dictionary.Add("UpsideDown.lowered", 18);
			dictionary.Add("UpsideDown.behind", 19);
			dictionary.Add("UpsideDown.facefuck", 20);
			int num = default(int);
			if (dictionary.TryGetValue(text, out num))
			{
				switch (num)
				{
				case 0:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handL", null), 0.22f, 0.38f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handR", null), 0.22f, 0.38f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeR", null), 0.9f);
					break;
				case 1:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeR", null), 0.9f);
					break;
				case 2:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.DEFAULT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeR", null), 0.9f);
					break;
				case 3:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.HIGHLIFT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.HIGHLIFT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.DEFAULT.kneeR", null), 0.9f);
					break;
				case 4:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.FUCK.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.FUCK.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.FUCK.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.FUCK.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.FUCK.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.FUCK.kneeR", null), 0.9f);
					break;
				case 5:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("RackChair.RIDING.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("RackChair.RIDING.handR", null), 0.22f, 0f);
					performer.setFootTargets(this.scaleWithCharacter.Find("ridingMountPoints").Find("FootL"), this.scaleWithCharacter.Find("ridingMountPoints").Find("FootR"));
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("RackChair.RIDING.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("RackChair.RIDING.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("RackChair.RIDING.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("RackChair.RIDING.kneeR", null), 0.9f);
					break;
				case 6:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handL", null), 0.22f, 0.5f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handR", null), 0.22f, 0.5f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeR", null), 0.9f);
					break;
				case 7:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handL", null), 0.22f, 0.5f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handR", null), 0.22f, 0.5f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeR", null), 0.9f);
					break;
				case 8:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handL", null), 0.22f, 0.5f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.DEFAULT.handR", null), 0.22f, 0.5f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.FUCK.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.FUCK.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.DEFAULT.kneeR", null), 0.9f);
					break;
				case 9:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.handL", null), 0.22f, 0.5f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.handR", null), 0.22f, 0.5f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFTBEHIND.kneeR", null), 0.9f);
					break;
				case 10:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.FACEFUCK.handL", null), 0.22f, 0.5f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.FACEFUCK.handR", null), 0.22f, 0.5f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.FACEFUCK.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.FACEFUCK.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.FACEFUCK.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.FACEFUCK.kneeR", null), 0.9f);
					break;
				case 11:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("Stocks.HIGHLIFT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("Stocks.HIGHLIFT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("Stocks.HIGHLIFT.kneeR", null), 0.9f);
					break;
				case 12:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("TableStraps.DEFAULT.handL", null), 0.35f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("TableStraps.DEFAULT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("TableStraps.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("TableStraps.DEFAULT.kneeR", null), 0.9f);
					break;
				case 13:
					performer.setIdleHandTarget(false, this.scaleWithCharacter.Find("ridingMountPoints").Find("HandL"), 0.05f, 0f);
					performer.setIdleHandTarget(true, this.scaleWithCharacter.Find("ridingMountPoints").Find("HandR"), 0.05f, 0f);
					performer.setFootTargets(this.scaleWithCharacter.Find("ridingMountPoints").Find("FootL"), this.scaleWithCharacter.Find("ridingMountPoints").Find("FootR"));
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.MISSIONARY.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.MISSIONARY.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.scaleWithCharacter.Find("ridingMountPoints").Find("MISSIONARY.kneeL"), 0.9f);
					performer.setKneeTarget(true, this.scaleWithCharacter.Find("ridingMountPoints").Find("MISSIONARY.kneeR"), 0.9f);
					break;
				case 14:
					performer.setIdleHandTarget(false, this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.handL"), 0.05f, 0f);
					performer.setIdleHandTarget(true, this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.handR"), 0.05f, 0f);
					performer.setFootTargets(this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.footL"), this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.footR"));
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.SIXTYNINE.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.SIXTYNINE.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.kneeL"), 0.9f);
					performer.setKneeTarget(true, this.scaleWithCharacter.Find("ridingMountPoints").Find("SIXTYNINE.kneeR"), 0.9f);
					break;
				case 15:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("TableStraps.FACEFUCK.handL", null), 0.35f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("TableStraps.FACEFUCK.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.FACEFUCK.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.FACEFUCK.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("TableStraps.FACEFUCK.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("TableStraps.FACEFUCK.kneeR", null), 0.9f);
					break;
				case 16:
					performer.setIdleHandTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.RIDING.handL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.25f, 0f);
					performer.setIdleHandTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.RIDING.handR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.25f, 0f);
					performer.setFootTargets(this.scaleWithCharacter.Find("ridingMountPoints").Find("FootLriding"), this.scaleWithCharacter.Find("ridingMountPoints").Find("FootRriding"));
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.RIDING.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("TableStraps.RIDING.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.scaleWithCharacter.Find("ridingMountPoints").Find("RIDING.kneeL"), 0.9f);
					performer.setKneeTarget(true, this.scaleWithCharacter.Find("ridingMountPoints").Find("RIDING.kneeR"), 0.9f);
					break;
				case 17:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("UpsideDown.DEFAULT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("UpsideDown.DEFAULT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeR", null), 0.9f);
					break;
				case 18:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("UpsideDown.DEFAULT.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("UpsideDown.DEFAULT.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeR", null), 0.9f);
					break;
				case 19:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("UpsideDown.BEHIND.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("UpsideDown.BEHIND.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.BEHIND.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.BEHIND.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("UpsideDown.BEHIND.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("UpsideDown.BEHIND.kneeR", null), 0.9f);
					break;
				case 20:
					performer.setIdleHandTarget(false, this.boundCharacter.recursiveFindChild("UpsideDown.FACEFUCK.handL", null), 0.22f, 0f);
					performer.setIdleHandTarget(true, this.boundCharacter.recursiveFindChild("UpsideDown.FACEFUCK.handR", null), 0.22f, 0f);
					performer.setElbowTarget(false, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowL", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setElbowTarget(true, ((Component)this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.elbowR", null)).GetComponent<GlobalFollower>().globalPointTransform, 0.9f);
					performer.setKneeTarget(false, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeL", null), 0.9f);
					performer.setKneeTarget(true, this.performingCharacter.recursiveFindChild("UpsideDown.DEFAULT.kneeR", null), 0.9f);
					break;
				}
			}
		}
	}

	public void ongoingPositionPerformer(RackCharacter performer, string pose)
	{
		this.backBendFromPerformance += (0.3f * Game.cap(performer.interactionMX + performer.interactionMY - 1f, -0.5f, 0.5f) - this.backBendFromPerformance) * BondageApparatus.cap(Time.deltaTime * 2f, 0f, 1f);
		string text = this.poseName + "." + pose;
		if (text != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(20);
			dictionary.Add("TableStraps.default", 1);
			dictionary.Add("TableStraps.riding", 2);
			dictionary.Add("TableStraps.sixtynine", 3);
			dictionary.Add("TableStraps.facefuck", 4);
			dictionary.Add("UpsideDown.default", 5);
			dictionary.Add("UpsideDown.behind", 6);
			dictionary.Add("UpsideDown.close", 7);
			dictionary.Add("UpsideDown.facefuck", 8);
			dictionary.Add("RackChair.default", 9);
			dictionary.Add("RackChair.lifted", 10);
			dictionary.Add("RackChair.eyelevel", 11);
			dictionary.Add("RackChair.highlift", 12);
			dictionary.Add("RackChair.fuck", 13);
			dictionary.Add("RackChair.riding", 14);
			dictionary.Add("Stocks.default", 15);
			dictionary.Add("Stocks.lifted", 16);
			dictionary.Add("Stocks.fuck", 17);
			dictionary.Add("Stocks.highlift", 18);
			dictionary.Add("Stocks.highliftBehind", 19);
			dictionary.Add("Stocks.facefuck", 20);
			int num = default(int);
			if (dictionary.TryGetValue(text, out num))
			{
				switch (num)
				{
				case 0:
					break;
				case 1:
					performer.bendBack(-0.85f + this.backBendFromPerformance);
					break;
				case 2:
					performer.bendBack(-0.3f + this.backBendFromPerformance);
					break;
				case 3:
					performer.bendBack(-0.1f + this.backBendFromPerformance);
					break;
				case 4:
					performer.bendBack(-0.8f + this.backBendFromPerformance);
					break;
				case 5:
					performer.bendBack(0.148f + this.backBendFromPerformance);
					break;
				case 6:
					performer.bendBack(0.148f + this.backBendFromPerformance);
					break;
				case 7:
					performer.bendBack(0.148f + this.backBendFromPerformance);
					break;
				case 8:
					performer.bendBack(0.148f + this.backBendFromPerformance);
					break;
				case 9:
					performer.bendBack(-0.2f + this.backBendFromPerformance);
					break;
				case 10:
					performer.bendBack(-0.2f + this.backBendFromPerformance);
					break;
				case 11:
					performer.bendBack(-0.2f + this.backBendFromPerformance);
					break;
				case 12:
					performer.bendBack(-0.2f + this.backBendFromPerformance);
					break;
				case 13:
					performer.bendBack(-0.05f + this.backBendFromPerformance);
					break;
				case 14:
					performer.bendBack(-0.12f + this.backBendFromPerformance);
					break;
				case 15:
					performer.bendBack(-0.21f + this.backBendFromPerformance);
					break;
				case 16:
					performer.bendBack(-0.4f + this.backBendFromPerformance);
					break;
				case 17:
					performer.bendBack(-0.31f + this.backBendFromPerformance);
					break;
				case 18:
					performer.bendBack(-0.4f + this.backBendFromPerformance);
					break;
				case 19:
					performer.bendBack(-0.4f + this.backBendFromPerformance);
					break;
				case 20:
					performer.bendBack(-0.31f + this.backBendFromPerformance);
					break;
				}
			}
		}
	}

	public void animateBasedOnPose(RackCharacter performer, string pose)
	{
		string text = this.poseName;
		if (text != null)
		{
			if (!(text == "TableStraps"))
			{
				if (text == "RackChair")
				{
					this.RackChair_stirrupL.localScale = this.game.PC().bones.Footpad_L.lossyScale;
					this.RackChair_stirrupR.localScale = this.game.PC().bones.Footpad_R.lossyScale;
					if (this.originalRackChairstirrupLposition.magnitude == 0f)
					{
						this.originalRackChairstirrupLposition = this.RackChair_stirrupL.position;
						this.originalRackChairstirrupRposition = this.RackChair_stirrupR.position;
					}
					if (pose == null || !(pose == "riding"))
					{
						Transform rackChair_stirrupL = this.RackChair_stirrupL;
						rackChair_stirrupL.position += (this.originalRackChairstirrupLposition - this.RackChair_stirrupL.position) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
						Transform rackChair_stirrupR = this.RackChair_stirrupR;
						rackChair_stirrupR.position += (this.originalRackChairstirrupRposition - this.RackChair_stirrupR.position) * Game.cap(Time.deltaTime * 5f, 0f, 1f);
					}
					else
					{
						this.RackChair_stirrupL.position = performer.bones.Footpad_L.position;
						this.RackChair_stirrupL.rotation = performer.bones.Footpad_L.rotation;
						this.RackChair_stirrupL.Rotate(6f, -5f, -100f);
						this.RackChair_stirrupR.position = performer.bones.Footpad_R.position;
						this.RackChair_stirrupR.rotation = performer.bones.Footpad_R.rotation;
						this.RackChair_stirrupR.Rotate(6f, -5f, -100f);
					}
				}
			}
			else
			{
				if (pose == null || !(pose == "facefuck"))
				{
					this.headrestAngle.x += (0f - this.headrestAngle.x) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
				}
				else
				{
					this.headrestAngle.x += (-85f - this.headrestAngle.x) * Game.cap(Time.deltaTime * 10f, 0f, 1f);
				}
				this.headrest.localEulerAngles = this.headrestAngle;
			}
		}
	}

	public void animateRestraints()
	{
		if (this.boundCharacter.effectivelyPlantigrade != this.lastEffectivelyPlantigrade)
		{
			for (int i = 0; i < this.bondagePoints.Length; i++)
			{
				if (this.bondagePoints[i].name == "Foot_L")
				{
					this.bondagePoints[i].transform.localEulerAngles = this.originalFootLRotation;
					if (!this.boundCharacter.effectivelyPlantigrade)
					{
						this.bondagePoints[i].transform.Rotate(0f, 20f, 0f);
					}
				}
				if (this.bondagePoints[i].name == "Foot_R")
				{
					this.bondagePoints[i].transform.localEulerAngles = this.originalFootRRotation;
					if (!this.boundCharacter.effectivelyPlantigrade)
					{
						this.bondagePoints[i].transform.Rotate(0f, 20f, 0f);
					}
				}
			}
			this.lastEffectivelyPlantigrade = this.boundCharacter.effectivelyPlantigrade;
		}
		for (int j = 0; j < this.bondagePoints.Length; j++)
		{
			switch (this.bondagePoints[j].name)
			{
			case "Hand_L":
				if ((UnityEngine.Object)this.moveWithHandL != (UnityEngine.Object)null)
				{
					this.moveWithHandL.position = this.boundCharacter.bones.Hand_L.position;
					this.moveWithHandL.rotation = this.boundCharacter.bones.Hand_L.rotation;
					this.moveWithHandL.Rotate(0f, 0f, 90f);
				}
				break;
			case "Hand_R":
				if ((UnityEngine.Object)this.moveWithHandR != (UnityEngine.Object)null)
				{
					this.moveWithHandR.position = this.boundCharacter.bones.Hand_R.position;
					this.moveWithHandR.rotation = this.boundCharacter.bones.Hand_R.rotation;
					this.moveWithHandR.Rotate(0f, 0f, 90f);
				}
				break;
			case "Foot_L":
				if ((UnityEngine.Object)this.moveWithAnkleL != (UnityEngine.Object)null)
				{
					this.moveWithAnkleL.position = this.boundCharacter.bones.Footpad_L.position;
					this.moveWithAnkleL.rotation = this.boundCharacter.bones.Footpad_L.rotation;
					this.moveWithAnkleL.Rotate(0f, 0f, 90f);
				}
				break;
			case "Foot_R":
				if ((UnityEngine.Object)this.moveWithAnkleR != (UnityEngine.Object)null)
				{
					this.moveWithAnkleR.position = this.boundCharacter.bones.Footpad_R.position;
					this.moveWithAnkleR.rotation = this.boundCharacter.bones.Footpad_R.rotation;
					this.moveWithAnkleR.Rotate(0f, 0f, 90f);
				}
				break;
			}
		}
	}

	public void reactToSubjectStruggle(float struggleAmount)
	{
		if (struggleAmount > 1f)
		{
			this.audioSource.PlayOneShot(Resources.Load("rock" + this.nextRock) as AudioClip, (struggleAmount - 1f) * 0.3f);
			int num;
			for (num = this.nextRock; num == this.nextRock; num = Mathf.FloorToInt(UnityEngine.Random.value * 1000f) % 6)
			{
			}
			this.nextRock = num;
		}
	}

	public static void process()
	{
		BondageApparatus.lastClosestID = BondageApparatus.closestID;
		BondageApparatus.closestDist = 15f;
	}

	public void Update()
	{
		if (this.inRange)
		{
			this.game.context(Localization.getPhrase("INTERACT_WITH_SUBJECT", string.Empty), this.interactWithSubject, base.transform.position, false);
		}
	}

	public void initHolograms()
	{
		this.v3 = Vector3.one;
		this.v3.x = -1f;
		TestingRoom.labItemContainer.gameObject.SetActive(true);
		if ((UnityEngine.Object)this.HologramHarvestTemplate == (UnityEngine.Object)null)
		{
			this.HologramHarvestTemplate = GameObject.Find("LabItems").transform.Find("HologramHarvest");
			this.HologramRealtimeTemplate = GameObject.Find("LabItems").transform.Find("HologramRealtime");
		}
		this.HologramHarvestTemplate.gameObject.SetActive(true);
		GameObject gameObject = UnityEngine.Object.Instantiate(this.HologramHarvestTemplate.Find("contents").gameObject);
		gameObject.name = "contents";
		gameObject.transform.SetParent(base.gameObject.transform.Find("HologramHarvest"));
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localScale = this.v3;
		base.gameObject.transform.Find("HologramHarvest").Find("hologramScreen").SetParent(base.gameObject.transform.Find("HologramHarvest").Find("contents"));
		this.HologramHarvestTemplate.gameObject.SetActive(false);
		this.HologramRealtimeTemplate.gameObject.SetActive(true);
		gameObject = UnityEngine.Object.Instantiate(this.HologramRealtimeTemplate.Find("contents").gameObject);
		gameObject.name = "contents";
		gameObject.transform.SetParent(base.gameObject.transform.Find("HologramRealtime"));
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localEulerAngles = Vector3.zero;
		gameObject.transform.localScale = this.v3;
		base.gameObject.transform.Find("HologramRealtime").Find("hologramScreen").SetParent(base.gameObject.transform.Find("HologramRealtime").Find("contents"));
		this.HologramRealtimeTemplate.gameObject.SetActive(false);
		TestingRoom.labItemContainer.gameObject.SetActive(false);
		this.hologramHarvest = base.gameObject.transform.Find("HologramHarvest");
		this.hologramRealtime = base.gameObject.transform.Find("HologramRealtime");
		this.hologramHarvestContents = this.hologramHarvest.Find("contents");
		this.hologramRealtimeContents = this.hologramRealtime.Find("contents");
		this.scaleWithCharacter = base.gameObject.transform.Find("scaleWithCharacter");
		this.RackChair_stirrupL = base.gameObject.transform.Find("scaleWithCharacter").Find("RackChair_stirrupL");
		this.RackChair_stirrupR = base.gameObject.transform.Find("scaleWithCharacter").Find("RackChair_stirrupR");
		this.headrest = base.gameObject.transform.Find("scaleWithCharacter").Find("headrest");
		this.headrestAngle = default(Vector3);
		this.audioSource = ((Component)base.transform.Find("sfx")).GetComponent<AudioSource>();
		this.txtScience0 = ((Component)this.hologramHarvestContents.Find("txtScience0")).GetComponent<TextMesh>();
		this.txtScience1 = ((Component)this.hologramHarvestContents.Find("txtScience1")).GetComponent<TextMesh>();
		this.txtName = ((Component)this.hologramHarvestContents.Find("txtName")).GetComponent<TextMesh>();
		this.txtOrgasms = ((Component)this.hologramHarvestContents.Find("txtOrgasms")).GetComponent<TextMesh>();
		this.txtSpecimen = ((Component)this.hologramHarvestContents.Find("txtSpecimen")).GetComponent<TextMesh>();
		this.txtSpecimenCount = ((Component)this.hologramHarvestContents.Find("txtSpecimenCount")).GetComponent<TextMesh>();
		this.txtSpecimenCount2 = ((Component)this.hologramHarvestContents.Find("txtSpecimenCount2")).GetComponent<TextMesh>();
		this.txtSamples = ((Component)this.hologramHarvestContents.Find("txtSamples")).GetComponent<TextMesh>();
		this.txtSessionDuration = ((Component)this.hologramHarvestContents.Find("txtSessionDuration")).GetComponent<TextMesh>();
		for (int i = 0; i < 6; i++)
		{
			this.specimenBars[i] = this.hologramHarvestContents.Find("specimenBar" + i);
			this.specimenFrames[i] = this.hologramHarvestContents.Find("specimenFrame" + i);
		}
		this.txtSensitivity = ((Component)this.hologramRealtimeContents.Find("txtSensitivity")).GetComponent<TextMesh>();
		this.sensitivityIndicator = this.hologramRealtimeContents.Find("sensitivityIndicator");
		this.sensitivityHeat = this.hologramRealtimeContents.Find("sensitivityHeat");
		this.sensitivityHeatMR = ((Component)this.sensitivityHeat).GetComponent<MeshRenderer>();
		this.txtAnticipation = ((Component)this.hologramRealtimeContents.Find("txtAnticipation")).GetComponent<TextMesh>();
		this.anticipationHeart = this.hologramRealtimeContents.Find("anticipationHeart").Find("anticipationHeart");
		this.anticipationHeart1 = this.hologramRealtimeContents.Find("anticipationHeart").Find("anticipationHeart.001");
		this.anticipationHeartMR = ((Component)this.anticipationHeart).GetComponent<MeshRenderer>();
		this.anticipationHeart1MR = ((Component)this.anticipationHeart1).GetComponent<MeshRenderer>();
		for (int j = 0; j < 15; j++)
		{
			this.arousalHistories[j] = this.hologramRealtimeContents.Find("arousalHistory").Find("arousalMeter (" + j + ")");
			this.arousalHistoriesMR[j] = ((Component)this.arousalHistories[j]).GetComponent<MeshRenderer>();
		}
		this.txtArousal = ((Component)this.hologramRealtimeContents.Find("txtArousal")).GetComponent<TextMesh>();
		this.txtClimax = ((Component)this.hologramRealtimeContents.Find("txtClimax")).GetComponent<TextMesh>();
		this.orgasmMeterCurve = ((Component)this.hologramRealtimeContents.Find("orgasmMeter").Find("curveMeter")).GetComponent<SkinnedMeshRenderer>();
		this.pleasureMeterCurve = ((Component)this.hologramRealtimeContents.Find("pleasureMeter").Find("curveMeter")).GetComponent<SkinnedMeshRenderer>();
		this.hologramsInitialized = true;
	}

	private void FixedUpdate()
	{
		if ((UnityEngine.Object)this.game == (UnityEngine.Object)null)
		{
			this.game = Game.gameInstance;
		}
		else if (this.game.PC() != null)
		{
			if (!this.hologramsInitialized)
			{
				this.initHolograms();
			}
			else
			{
				if (this.hideHologramsForTutorialReasons && !UserSettings.needTutorial("NPT_AROUSE_THE_SUBJECT"))
				{
					this.hideHologramsForTutorialReasons = false;
				}
				float num = (this.game.PC().GO.transform.position - (this.hologramHarvest.gameObject.transform.position + this.hologramRealtime.gameObject.transform.position) / 2f).magnitude;
				if ((UnityEngine.Object)this.game.PC().interactionApparatus != (UnityEngine.Object)null && this.game.PC().interactionApparatus.uid == this.uid)
				{
					num = 0f;
				}
				if (num < BondageApparatus.closestDist)
				{
					BondageApparatus.closestID = this.uid;
					BondageApparatus.closestDist = num;
				}
				this.showingHologramHarvest = (this.boundCharacter != null && this.uid == BondageApparatus.lastClosestID && BondageApparatus.showHologramHUDs && !this.hideHologramsForTutorialReasons);
				if (this.showingHologramHarvest)
				{
					this.hologramHarvest.gameObject.SetActive(true);
					this.v3.x = -1f;
					this.v3.y = 1f;
					this.v3.z = 1f;
					if (this.performingPriority <= 0f)
					{
						this.v3 *= 0.5f;
					}
					Transform transform = this.hologramHarvestContents;
					transform.localScale += (this.v3 - this.hologramHarvestContents.localScale) * BondageApparatus.cap(Time.deltaTime * 6f, 0f, 1f);
					if (!this.wasShowingHologramHarvest)
					{
						Game.PlaySFXAtPoint(Resources.Load("hologram_on") as AudioClip, this.hologramHarvest.gameObject.transform.position, 1f);
					}
				}
				else
				{
					this.v3.x = 0f;
					this.v3.y = 1f;
					this.v3.z = 1f;
					Transform transform2 = this.hologramHarvestContents;
					transform2.localScale += (this.v3 - this.hologramHarvestContents.localScale) * BondageApparatus.cap(Time.deltaTime * 16f, 0f, 1f);
					Vector3 localScale = this.hologramHarvestContents.localScale;
					if (localScale.x > -0.01f)
					{
						this.hologramHarvest.gameObject.SetActive(false);
					}
					if (this.wasShowingHologramHarvest)
					{
						Game.PlaySFXAtPoint(Resources.Load("hologram_off") as AudioClip, this.hologramHarvest.gameObject.transform.position, 1f);
					}
				}
				this.wasShowingHologramHarvest = this.showingHologramHarvest;
				num = (this.game.PC().GO.transform.position - this.hologramRealtime.gameObject.transform.position).magnitude;
				this.showingHologramRealtime = (this.boundCharacter != null && this.uid == BondageApparatus.lastClosestID && BondageApparatus.showHologramHUDs && !this.hideHologramsForTutorialReasons);
				if (this.showingHologramRealtime)
				{
					this.hologramRealtime.gameObject.SetActive(true);
					this.v3.x = -1f;
					this.v3.y = 1f;
					this.v3.z = 1f;
					if (this.performingPriority <= 0f)
					{
						this.v3 *= 0.5f;
					}
					Transform transform3 = this.hologramRealtimeContents;
					transform3.localScale += (this.v3 - this.hologramRealtimeContents.localScale) * BondageApparatus.cap(Time.deltaTime * 6f, 0f, 1f);
					if (!this.wasShowingHologramRealtime)
					{
						Game.PlaySFXAtPoint(Resources.Load("hologram_on") as AudioClip, this.hologramRealtime.gameObject.transform.position, 1f);
					}
				}
				else
				{
					this.v3.x = 0f;
					this.v3.y = 1f;
					this.v3.z = 1f;
					Transform transform4 = this.hologramRealtimeContents;
					transform4.localScale += (this.v3 - this.hologramRealtimeContents.localScale) * BondageApparatus.cap(Time.deltaTime * 16f, 0f, 1f);
					Vector3 localScale2 = this.hologramRealtimeContents.localScale;
					if (localScale2.x > -0.01f)
					{
						this.hologramRealtime.gameObject.SetActive(false);
					}
					if (this.wasShowingHologramRealtime)
					{
						Game.PlaySFXAtPoint(Resources.Load("hologram_off") as AudioClip, this.hologramRealtime.gameObject.transform.position, 1f);
					}
				}
				this.wasShowingHologramRealtime = this.showingHologramRealtime;
				if (this.boundCharacter != null)
				{
					for (int i = 0; i < 6; i++)
					{
						float num2 = this.boundCharacter.specimenProduced[i];
						this.boundCharacter.specimenProduced[i] = 0f;
						this.sessionSpecimenCollected[i] += num2;
						Inventory.addSpecimenByType(i, num2, false);
						this.sessionSpecimenAnticipated[i] = this.boundCharacter.stockpiledSpecimenProduced[i];
						if (i == 2)
						{
							this.sessionSpecimenAnticipated[i] = this.boundCharacter.projectedLapinine;
						}
						if (i == 4)
						{
							this.sessionSpecimenAnticipated[i] = this.boundCharacter.projectedEquimine;
						}
					}
					this.inRange = false;
					this.pc = this.game.PC();
					if (this.pc.interactionSubject == null && this.pc != null)
					{
						float magnitude = (this.pc.GO.transform.position - base.transform.position).magnitude;
						this.inRange = (magnitude < this.triggerDistance);
					}
					if (this.boundCharacter.initted)
					{
						if (!this.hadBoundSubject)
						{
							this.sessionStart = DateTime.Now;
							this.sessionOrgasms = 0;
							for (int j = 0; j < this.sessionSpecimenCollected.Length; j++)
							{
								this.sessionSpecimenCollected[j] = 0f;
							}
							this.hadBoundSubject = true;
							this.totalSessionSpecimenCollected = 0f;
							this.arousalHistory = new float[15];
						}
						this.totalSessionSpecimenCollected = 0f;
						this.highestSpecimenCollection = 0.05f;
						for (int k = 0; k < this.sessionSpecimenCollected.Length; k++)
						{
							this.totalSessionSpecimenCollected += this.sessionSpecimenCollected[k];
							if (this.sessionSpecimenCollected[k] + this.sessionSpecimenAnticipated[k] > this.highestSpecimenCollection)
							{
								this.highestSpecimenCollection = this.sessionSpecimenCollected[k] + this.sessionSpecimenAnticipated[k];
							}
						}
						this.heightAdjustment = 0f;
						this.adjustingHeight = false;
						this.performingCharacter = this.game.PC();
						this.performingPriority = 0f;
						for (int l = 0; l < this.game.characters.Count; l++)
						{
							if ((UnityEngine.Object)this.game.characters[l].interactionApparatus != (UnityEngine.Object)null && this.game.characters[l].interactionApparatus.uid == this.uid)
							{
								float num3 = (float)this.game.characters[l].uid * 0.001f;
								if (this.game.characters[l].currentlyUsingHandL)
								{
									num3 += 20f;
								}
								if (this.game.characters[l].currentlyUsingHandR)
								{
									num3 += 20f;
								}
								if (this.game.characters[l].currentlyUsingPenis)
								{
									num3 += 90f;
								}
								if (this.game.characters[l].currentlyUsingVagina)
								{
									num3 += 100f;
								}
								if (num3 > this.performingPriority)
								{
									this.performingCharacter = this.game.characters[l];
									this.performingPriority = num3;
								}
							}
						}
						if (num < 50f)
						{
							this.timeAlive += Time.deltaTime;
						}
						Transform transform5 = this.scaleWithCharacter;
						Vector3 one = Vector3.one;
						Vector3 localScale3 = this.boundCharacter.GO.transform.localScale;
						transform5.localScale = one * localScale3.x;
						if (this.performingPriority > 0f || (this.timeAlive < 3f && num < 50f) || this.wasAdjustingHeight)
						{
							switch (this.poseName)
							{
							case "RackChair":
								switch (this.performingCharacter.curSexPoseName)
								{
								case "default":
								{
									Vector3 position35 = this.performingCharacter.GO.transform.position;
									float num21 = position35.y + 2.8f * this.performingCharacter.height_act;
									Vector3 position36 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num21 - position36.y;
									break;
								}
								case "lifted":
								{
									Vector3 position33 = this.performingCharacter.GO.transform.position;
									float num20 = position33.y + 3.2f * this.performingCharacter.height_act;
									Vector3 position34 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num20 - position34.y;
									break;
								}
								case "eyelevel":
								{
									Vector3 position31 = this.performingCharacter.GO.transform.position;
									float num19 = position31.y + 3.11f * this.performingCharacter.height_act;
									Vector3 position32 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num19 - position32.y;
									break;
								}
								case "highlift":
								{
									Vector3 position29 = this.performingCharacter.GO.transform.position;
									float num18 = position29.y + 4.6f * this.performingCharacter.height_act;
									Vector3 position30 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num18 - position30.y;
									break;
								}
								case "fuck":
								{
									Vector3 position27 = this.performingCharacter.GO.transform.position;
									float num17 = position27.y + 2.05f * this.performingCharacter.height_act;
									Vector3 position28 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num17 - position28.y;
									break;
								}
								}
								break;
							case "Stocks":
								switch (this.performingCharacter.curSexPoseName)
								{
								case "default":
								{
									Vector3 position25 = this.performingCharacter.GO.transform.position;
									float num16 = position25.y + 2.8f * this.performingCharacter.height_act;
									Vector3 position26 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num16 - position26.y;
									break;
								}
								case "lifted":
								{
									Vector3 position23 = this.performingCharacter.GO.transform.position;
									float num15 = position23.y + 3.7f * this.performingCharacter.height_act;
									Vector3 position24 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num15 - position24.y;
									break;
								}
								case "highlift":
								{
									Vector3 position21 = this.performingCharacter.GO.transform.position;
									float num14 = position21.y + 4.8f * this.performingCharacter.height_act;
									Vector3 position22 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num14 - position22.y;
									break;
								}
								case "highliftBehind":
								{
									Vector3 position19 = this.performingCharacter.GO.transform.position;
									float num13 = position19.y + 4.4f * this.performingCharacter.height_act;
									Vector3 position20 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num13 - position20.y;
									break;
								}
								case "fuck":
								{
									Vector3 position17 = this.performingCharacter.GO.transform.position;
									float num12 = position17.y + 1.9f * this.performingCharacter.height_act;
									Vector3 position18 = this.boundCharacter.bones.Pubic.position;
									this.heightAdjustment = num12 - position18.y;
									break;
								}
								case "facefuck":
								{
									Vector3 position15 = this.performingCharacter.GO.transform.position;
									float num11 = position15.y + 2f * this.performingCharacter.height_act;
									Vector3 position16 = this.boundCharacter.bones.Head.position;
									this.heightAdjustment = num11 - position16.y;
									break;
								}
								}
								break;
							case "TableStraps":
								switch (this.performingCharacter.curSexPoseName)
								{
								case "default":
								{
									Vector3 position13 = this.performingCharacter.GO.transform.position;
									float num10 = position13.y + 2.7f * this.performingCharacter.height_act;
									Vector3 position14 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num10 - position14.y;
									break;
								}
								case "facefuck":
								{
									Vector3 position11 = this.performingCharacter.GO.transform.position;
									float num9 = position11.y + 2f * this.performingCharacter.height_act;
									Vector3 position12 = this.boundCharacter.bones.Head.position;
									this.heightAdjustment = num9 - position12.y;
									break;
								}
								case "fuck":
								{
									Vector3 position9 = this.performingCharacter.GO.transform.position;
									float num8 = position9.y + 2f * this.performingCharacter.height_act;
									Vector3 position10 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num8 - position10.y;
									break;
								}
								}
								break;
							case "UpsideDown":
								switch (this.performingCharacter.curSexPoseName)
								{
								default:
								{
									Vector3 position7 = this.performingCharacter.GO.transform.position;
									float num7 = position7.y + 4.138f * this.performingCharacter.height_act;
									Vector3 position8 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num7 - position8.y;
									break;
								}
								case "facefuck":
								{
									Vector3 position5 = this.performingCharacter.GO.transform.position;
									float num6 = position5.y + 1.9f * this.performingCharacter.height_act;
									Vector3 position6 = this.boundCharacter.bones.Head.position;
									this.heightAdjustment = num6 - position6.y;
									break;
								}
								case "behind":
								{
									Vector3 position3 = this.performingCharacter.GO.transform.position;
									float num5 = position3.y + 4.138f * this.performingCharacter.height_act;
									Vector3 position4 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num5 - position4.y;
									break;
								}
								case "lowered":
								{
									Vector3 position = this.performingCharacter.GO.transform.position;
									float num4 = position.y + 2.9f * this.performingCharacter.height_act;
									Vector3 position2 = this.boundCharacter.bones.Root.position;
									this.heightAdjustment = num4 - position2.y;
									break;
								}
								}
								break;
							}
							this.heightAdjustmentVelocity += this.heightAdjustment * Time.deltaTime;
							this.heightAdjustmentVelocity = BondageApparatus.cap(this.heightAdjustmentVelocity, -4.5f, 4.5f);
							if (Mathf.Abs(this.heightAdjustment) < 0.05f || (this.heightAdjustment > 0f && this.heightAdjustmentVelocity < 0f) || (this.heightAdjustment < 0f && this.heightAdjustmentVelocity > 0f))
							{
								this.heightAdjustmentVelocity -= this.heightAdjustmentVelocity * BondageApparatus.cap(Time.deltaTime * 8f, 0f, 1f);
							}
							this.adjustingHeight = (Mathf.Abs(this.heightAdjustment) > 0.29f);
							this.v3.x = 0f;
							this.v3.y = this.heightAdjustmentVelocity * Time.deltaTime;
							this.v3.z = 0f;
							Vector3 position37 = base.transform.position;
							float y = position37.y;
							Vector3 position38 = this.scaleWithCharacter.position;
							this.undergroundAmount = y - position38.y;
							float num22 = this.undergroundAmount / this.boundCharacter.height_act - this.undergroundThreshold;
							if (num22 > 0f)
							{
								if (this.v3.y > 0f)
								{
									Transform transform6 = this.scaleWithCharacter;
									transform6.position += this.v3;
									this.performingCharacter.stepLift(0f);
								}
								else
								{
									this.performingCharacter.stepLift(Time.deltaTime * 1f);
									this.heightAdjustmentVelocity = 0f;
								}
							}
							else
							{
								Transform transform7 = this.scaleWithCharacter;
								transform7.position += this.v3;
								if (num22 > -0.15f)
								{
									this.performingCharacter.stepLift(0f);
								}
							}
							if (this.adjustingHeight)
							{
								if (!this.wasAdjustingHeight)
								{
									this.audioSource.Play();
									this.audioSource.PlayOneShot(Resources.Load("servo_apparatus_start") as AudioClip);
								}
							}
							else if (this.wasAdjustingHeight)
							{
								this.audioSource.Stop();
								this.audioSource.PlayOneShot(Resources.Load("servo_apparatus_stop") as AudioClip, 0.7f);
							}
							this.wasAdjustingHeight = this.adjustingHeight;
						}
						bool flag = false;
						if ((UnityEngine.Object)this.game.PC().interactionApparatus != (UnityEngine.Object)null && this.game.PC().interactionApparatus.uid == this.uid)
						{
							flag = true;
						}
						if ((UnityEngine.Object)this.scaleWithCharacter != (UnityEngine.Object)null && this.hologramsInitialized)
						{
							if ((UnityEngine.Object)this.hologramHarvest != (UnityEngine.Object)null)
							{
								if (this.sessionOrgasmDisplayTime % 0.75f > 0.2f && this.sessionOrgasmDisplayTime % 0.75f < 0.45f)
								{
									this.txtScience0.text = Localization.getPhrase("SCIENCE", string.Empty);
								}
								else
								{
									this.txtScience0.text = string.Empty;
								}
								if (this.sessionOrgasmDisplayTime % 0.75f > 0.05f && this.sessionOrgasmDisplayTime % 0.75f < 0.3f)
								{
									this.txtScience1.text = Localization.getPhrase("SCIENCE", string.Empty);
								}
								else
								{
									this.txtScience1.text = string.Empty;
								}
								if (this.sessionOrgasmDisplayTime > 0f)
								{
									this.sessionOrgasmDisplayTime -= Time.deltaTime * 0.8f;
								}
								if (this.sessionOrgasmDisplayTime < 0f)
								{
									this.sessionOrgasmDisplayTime = 0f;
								}
								this.txtName.text = this.boundCharacter.data.name;
								this.txtOrgasms.text = Localization.getPhrase("ORGASMS:", string.Empty) + this.sessionOrgasms;
								this.txtSpecimen.text = Localization.getPhrase("SPECIMEN_HARVESTED", string.Empty);
								this.txtSpecimenCount.text = Mathf.FloorToInt(this.totalSessionSpecimenCollected).ToString();
								this.txtSpecimenCount2.text = Mathf.FloorToInt(this.totalSessionSpecimenCollected * 10f % 10f).ToString();
								this.txtSamples.text = Localization.getPhrase("SAMPLES", string.Empty);
								this.txtSessionDuration.text = this.timeformat(DateTime.Now - this.sessionStart);
								for (int m = 0; m < 6; m++)
								{
									this.v3 = Vector3.one * (0.03f + this.sessionSpecimenCollected[m] / this.highestSpecimenCollection * 0.09f);
									this.specimenBars[m].localScale = this.v3;
									this.v3 = Vector3.one * (0.03f + (this.sessionSpecimenCollected[m] + this.sessionSpecimenAnticipated[m]) / this.highestSpecimenCollection * 0.09f);
									this.specimenFrames[m].localScale = this.v3;
								}
								if (flag)
								{
									this.v32 = this.hologramHarvestContents.position;
									this.v32.y = (this.game.camTarget_actual.y + this.game.camPos_actual.y) * 0.5f;
									this.hologramHarvestContents.position = this.v32;
								}
								else
								{
									this.v32 = this.hologramHarvestContents.position;
									ref Vector3 val = ref this.v32;
									Vector3 position39 = this.game.PC().bones.Head.position;
									val.y = position39.y * 0.5f;
									this.hologramHarvestContents.position = this.v32;
								}
								this.hologramHarvestContents.LookAt(this.game.mainCam.transform);
							}
							if ((UnityEngine.Object)this.hologramRealtime != (UnityEngine.Object)null)
							{
								float num23 = this.boundCharacter.percievedStimulation / this.boundCharacter.targetStimulation;
								if (this.boundCharacter.timeSincePain < 0.5f)
								{
									this.painSmoother += Time.deltaTime;
									if (this.painSmoother > 1f)
									{
										this.painSmoother = 1f;
									}
									num23 += (this.boundCharacter.discomfort - this.boundCharacter.pleasure) * 80f * (0.5f - this.boundCharacter.timeSincePain) * this.painSmoother;
								}
								else
								{
									this.painSmoother = 0f;
								}
								this.txtSensitivity.text = Localization.getPhrase("STIMULATION", string.Empty).ToUpper();
								this.sensitivityPulse += Time.deltaTime * num23 * 5f;
								this.anticipationPulse += Time.deltaTime * (3f + this.boundCharacter.anticipation * 5f);
								this.v3 = Vector3.one * (0.1f + Mathf.Cos(this.sensitivityPulse) * 0.02f * BondageApparatus.cap(num23, 0f, 2.5f));
								this.sensitivityIndicator.localScale = this.v3;
								this.v3 = Vector3.one * (0.06f + BondageApparatus.cap(0.5f * (1f - this.boundCharacter.sensitivity), 0f, 0.14f) * (0.95f + Mathf.Sin(this.sensitivityPulse) * 0.05f));
								float num24 = BondageApparatus.cap(1f - this.boundCharacter.sensitivity, 0f, 0.5f) / 0.5f;
								this.heatPulse += num24 * 0.2f;
								this.sensitivityHeat.localScale = this.v3;
								this.sensitivityHeatMR.material.SetColor("_EmissionColor", 0.9f * ((BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24)) * (0.85f + Mathf.Cos(this.heatPulse) * 0.15f) * (this.v3.x / 0.2f)));
								this.sensitivityHeatMR.material.SetColor("_Color", (BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24)) * (0.85f + Mathf.Cos(this.heatPulse) * 0.15f) * (this.v3.x / 0.2f));
								num24 = BondageApparatus.cap(Mathf.Abs(num23 - 1f), 0f, 1f);
								((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24)));
								((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24));
								if (num23 >= 1f)
								{
									((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100f * BondageApparatus.cap(num23 - 1f, 0f, 1f));
									((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 0f);
								}
								else
								{
									((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, 100f * BondageApparatus.cap(1f - num23, 0f, 1f));
									((Component)this.sensitivityIndicator).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
								}
								this.txtAnticipation.text = Localization.getPhrase("ANTICIPATION", string.Empty).ToUpper();
								this.v3 = Vector3.one * (this.boundCharacter.anticipation + Mathf.Pow(Mathf.Cos(this.anticipationPulse), 4f) * this.boundCharacter.anticipation * 0.2f);
								this.anticipationHeart.localScale = this.v3;
								this.v3 = Vector3.one * BondageApparatus.cap(this.v3.x, 1f, 2f);
								this.anticipationHeart1.localScale = this.v3;
								num24 = BondageApparatus.cap((this.v3.x - 1f) * 8f, 0f, 1f);
								this.anticipationHeartMR.material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24)));
								this.anticipationHeartMR.material.SetColor("_Color", BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24));
								this.refractoryColor += (BondageApparatus.cap(this.boundCharacter.refractory * 2f / this.boundCharacter.currentRefractoryDuration, 0f, 1f) - this.refractoryColor) * BondageApparatus.cap(Time.deltaTime * 2f, 0f, 1f);
								this.anticipationHeart1MR.material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_r * this.refractoryColor + BondageApparatus.c2_w * (1f - this.refractoryColor)));
								this.anticipationHeart1MR.material.SetColor("_Color", BondageApparatus.c2_r * this.refractoryColor + BondageApparatus.c2_w * (1f - this.refractoryColor));
								this.v3.z = 1f;
								this.arousalHistoryTick -= Time.deltaTime;
								if (this.arousalHistoryTick <= 0f)
								{
									this.arousalHistoryTick += 0.05f;
									for (int num25 = this.arousalHistory.Length - 1; num25 >= 0; num25--)
									{
										if (num25 == 0)
										{
											this.arousalHistory[0] = this.boundCharacter.arousal + this.boundCharacter.cockTwitch * 0.5f + this.boundCharacter.breath[0] * 0.2f;
										}
										else
										{
											this.arousalHistory[num25] = this.arousalHistory[num25 - 1] * 0.97f;
										}
										this.v3.x = 2f + (1f - this.arousalHistory[num25]) * 2f;
										this.v3.y = 0.5f + BondageApparatus.cap(this.arousalHistory[num25], 0f, 1f) * 11.5f;
										num24 = BondageApparatus.cap(1f - this.arousalHistory[num25], 0f, 1f);
										this.arousalHistories[this.arousalHistory.Length - 1 - num25].localScale = this.v3;
										this.arousalHistoriesMR[this.arousalHistory.Length - 1 - num25].material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24)));
										this.arousalHistoriesMR[this.arousalHistory.Length - 1 - num25].material.SetColor("_Color", BondageApparatus.c2_r * num24 + BondageApparatus.c2_g * (1f - num24));
									}
								}
								this.txtArousal.text = Localization.getPhrase("AROUSAL", string.Empty).ToUpper();
								this.txtClimax.text = Localization.getPhrase("CLIMAX", string.Empty).ToUpper();
								this.orgasmPulse += Time.deltaTime * (4f + 15f * Mathf.Pow(BondageApparatus.cap(this.boundCharacter.orgasm + this.boundCharacter.pleasure * this.boundCharacter.arousal, 0f, 1f), 5f));
								num24 = 1f - Mathf.Pow(Mathf.Cos(this.orgasmPulse), 3f);
								this.orgasmMeterCurve.material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_w * num24 + BondageApparatus.c2_g * (1f - num24)));
								this.orgasmMeterCurve.material.SetColor("_Color", BondageApparatus.c2_w * num24 + BondageApparatus.c2_g * (1f - num24));
								this.pleasureMeterCurve.material.SetColor("_EmissionColor", 0.9f * (BondageApparatus.c2_w * num24 + BondageApparatus.c2_g * (1f - num24)));
								this.pleasureMeterCurve.material.SetColor("_Color", BondageApparatus.c2_w * num24 + BondageApparatus.c2_g * (1f - num24));
								this.orgasmMeterCurve.SetBlendShapeWeight(0, 100f - BondageApparatus.cap(this.boundCharacter.orgasm / 0.25f, 0f, 1f) * 100f);
								this.orgasmMeterCurve.SetBlendShapeWeight(1, 100f - BondageApparatus.cap(this.boundCharacter.orgasm / 0.25f - 1f, 0f, 1f) * 100f);
								this.orgasmMeterCurve.SetBlendShapeWeight(2, 100f - BondageApparatus.cap(this.boundCharacter.orgasm / 0.25f - 2f, 0f, 1f) * 100f);
								this.orgasmMeterCurve.SetBlendShapeWeight(3, 100f - BondageApparatus.cap(this.boundCharacter.orgasm / 0.25f - 3f, 0f, 1f) * 100f);
								this.pleasureMeterCurve.SetBlendShapeWeight(0, 100f - BondageApparatus.cap(this.boundCharacter.pleasure * this.boundCharacter.arousal / 0.25f, 0f, 1f) * 100f);
								this.pleasureMeterCurve.SetBlendShapeWeight(1, 100f - BondageApparatus.cap(this.boundCharacter.pleasure * this.boundCharacter.arousal / 0.25f - 1f, 0f, 1f) * 100f);
								this.pleasureMeterCurve.SetBlendShapeWeight(2, 100f - BondageApparatus.cap(this.boundCharacter.pleasure * this.boundCharacter.arousal / 0.25f - 2f, 0f, 1f) * 100f);
								this.pleasureMeterCurve.SetBlendShapeWeight(3, 100f - BondageApparatus.cap(this.boundCharacter.pleasure * this.boundCharacter.arousal / 0.25f - 3f, 0f, 1f) * 100f);
								if (flag)
								{
									this.v32 = this.hologramRealtimeContents.position;
									this.v32.y = (this.game.camTarget_actual.y + this.game.camPos_actual.y) * 0.5f;
									this.hologramRealtimeContents.position = this.v32;
								}
								else
								{
									this.v32 = this.hologramRealtimeContents.position;
									ref Vector3 val2 = ref this.v32;
									Vector3 position40 = this.game.PC().bones.Head.position;
									val2.y = position40.y * 0.5f;
									this.hologramRealtimeContents.position = this.v32;
								}
								this.hologramRealtimeContents.LookAt(this.game.mainCam.transform);
							}
						}
					}
				}
			}
			this.hadBoundSubject = (this.boundCharacter != null);
			if (this.boundCharacter != null)
			{
				this.hadBoundSubject = this.boundCharacter.initted;
			}
		}
	}

	public void animateWrithing(RackCharacter character)
	{
		switch (this.poseName)
		{
		case "RackChair":
		{
			this.boundCharacter.clenchHandL(0.6f + this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.boundCharacter.clenchHandR(0.6f - this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.v3 = this.boundCharacter.bones.Root.localPosition;
			this.v32 = this.boundCharacter.bones.Root.localEulerAngles;
			this.v3.x += this.boundCharacter.writheR * 0.2f * (1f - BondageApparatus.cap(this.boundCharacter.writheF, 0f, 1f));
			this.v3.z += Mathf.Abs(this.boundCharacter.writheR) * 0.15f * (1f - BondageApparatus.cap(Math.Abs(this.boundCharacter.writheF * 2f), 0f, 1f));
			this.v3.y += Mathf.Abs(this.boundCharacter.writheR) * BondageApparatus.cap(0f - this.boundCharacter.writheF, 0f, 1f) * -0.15f;
			this.v32.z += this.boundCharacter.writheR * -5f;
			this.v32.y += this.boundCharacter.writheR * 25f;
			if (this.boundCharacter.writheF < 0f)
			{
				this.v3.z += this.boundCharacter.writheF * -0.55f;
			}
			this.v32.x += this.boundCharacter.writheF * -10f;
			this.boundCharacter.bones.Root.localPosition = this.v3;
			this.boundCharacter.bones.Root.localEulerAngles = this.v32;
			Transform transform = null;
			for (int k = 0; k < 3; k++)
			{
				switch (k)
				{
				case 0:
					transform = this.boundCharacter.bones.SpineLower;
					break;
				case 1:
					transform = this.boundCharacter.bones.SpineMiddle;
					break;
				case 2:
					transform = this.boundCharacter.bones.SpineUpper;
					break;
				}
				this.v32 = transform.localEulerAngles;
				if (this.boundCharacter.writheF < 0f)
				{
					this.boundCharacter.writheR *= 1f + this.boundCharacter.writheF * 0.3f;
				}
				this.v32.x += this.boundCharacter.writheR * 1.1f * (float)(5 + k) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.z += (0f - this.boundCharacter.writheR) * 1.9f * (float)(5 + k) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.y += this.boundCharacter.writheF * -1.05f * (float)(5 + k);
				transform.localEulerAngles = this.v32;
			}
			Transform root3 = this.boundCharacter.bones.Root;
			root3.position += this.boundCharacter.up() * this.boundCharacter.humpBumpAmount_act * 0.2f;
			break;
		}
		case "Stocks":
		{
			this.boundCharacter.clenchHandL(0.6f + this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.boundCharacter.clenchHandR(0.6f - this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.v3 = this.boundCharacter.bones.Root.localPosition;
			this.v32 = this.boundCharacter.bones.Root.localEulerAngles;
			this.v3.x += this.boundCharacter.writheR * 0.2f * (1f - BondageApparatus.cap(this.boundCharacter.writheF, 0f, 1f));
			this.v3.z += Mathf.Abs(this.boundCharacter.writheR) * 0.15f * (1f - BondageApparatus.cap(Math.Abs(this.boundCharacter.writheF * 2f), 0f, 1f));
			this.v3.y += Mathf.Abs(this.boundCharacter.writheR) * BondageApparatus.cap(0f - this.boundCharacter.writheF, 0f, 1f) * -0.15f;
			this.v32.z += this.boundCharacter.writheR * -5f;
			this.v32.y += this.boundCharacter.writheR * 25f;
			this.v3.z += this.boundCharacter.writheF * -0.15f;
			this.v32.x += this.boundCharacter.writheF * 10f;
			this.boundCharacter.bones.Root.localPosition = this.v3;
			this.boundCharacter.bones.Root.localEulerAngles = this.v32;
			Transform transform = null;
			for (int j = 0; j < 3; j++)
			{
				switch (j)
				{
				case 0:
					transform = this.boundCharacter.bones.SpineLower;
					break;
				case 1:
					transform = this.boundCharacter.bones.SpineMiddle;
					break;
				case 2:
					transform = this.boundCharacter.bones.SpineUpper;
					break;
				}
				this.v32 = transform.localEulerAngles;
				if (this.boundCharacter.writheF < 0f)
				{
					this.boundCharacter.writheR *= 1f + this.boundCharacter.writheF * 0.3f;
				}
				this.v32.x += this.boundCharacter.writheR * 0.7f * (float)(5 + j);
				this.v32.z += (0f - this.boundCharacter.writheR) * 1.5f * (float)(5 + j);
				this.v32.y += this.boundCharacter.writheF * 1f * (float)(5 + j);
				transform.localEulerAngles = this.v32;
			}
			Transform root2 = this.boundCharacter.bones.Root;
			root2.position += this.boundCharacter.up() * this.boundCharacter.humpBumpAmount_act * 0.2f;
			break;
		}
		case "TableStraps":
		{
			this.boundCharacter.clenchHandL(0.1f + this.boundCharacter.writheR * 0.1f + this.boundCharacter.writheF * 0.05f, false, -1, 0.5f, -99f);
			this.boundCharacter.clenchHandR(0.1f - this.boundCharacter.writheR * 0.1f + this.boundCharacter.writheF * 0.05f, false, -1, 0.5f, -99f);
			this.v3 = this.boundCharacter.bones.Root.localPosition;
			this.v32 = this.boundCharacter.bones.Root.localEulerAngles;
			this.v32.z += this.boundCharacter.writheR * -5f;
			this.v32.y += this.boundCharacter.writheR * 25f;
			if (this.boundCharacter.writheF < 0f)
			{
				this.v3.z += this.boundCharacter.writheF * -0.35f;
			}
			this.v32.x += this.boundCharacter.writheF * -10f;
			this.boundCharacter.bones.Root.localPosition = this.v3;
			this.boundCharacter.bones.Root.localEulerAngles = this.v32;
			Transform transform = null;
			for (int l = 0; l < 3; l++)
			{
				switch (l)
				{
				case 0:
					transform = this.boundCharacter.bones.SpineLower;
					break;
				case 1:
					transform = this.boundCharacter.bones.SpineMiddle;
					break;
				case 2:
					transform = this.boundCharacter.bones.SpineUpper;
					break;
				}
				this.v32 = transform.localEulerAngles;
				if (this.boundCharacter.writheF < 0f)
				{
					this.boundCharacter.writheR *= 1f + this.boundCharacter.writheF * 0.3f;
				}
				this.v32.x += this.boundCharacter.writheR * 2.1f * (float)(5 + l) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.z += (0f - this.boundCharacter.writheR) * 0.85f * (float)(5 + l) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.y += this.boundCharacter.writheF * -1.05f * (float)(5 + l);
				transform.localEulerAngles = this.v32;
			}
			this.boundCharacter.bones.Neck.Rotate(0f, Mathf.Abs(this.boundCharacter.writheF * 22f), 0f);
			if (this.boundCharacter.writheF < 0f)
			{
				this.boundCharacter.bones.Shoulder_L.Rotate(0f, 0f, this.boundCharacter.writheF * -20f);
				this.boundCharacter.bones.Shoulder_R.Rotate(0f, 0f, this.boundCharacter.writheF * 20f);
			}
			Transform root4 = this.boundCharacter.bones.Root;
			root4.position += this.boundCharacter.up() * this.boundCharacter.humpBumpAmount_act * 0.2f;
			break;
		}
		case "UpsideDown":
		{
			this.boundCharacter.clenchHandL(0.6f + this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.boundCharacter.clenchHandR(0.6f - this.boundCharacter.writheR * 0.2f + this.boundCharacter.writheF * 0.2f, false, -1, 0.5f, -99f);
			this.v3 = this.boundCharacter.bones.Root.localPosition;
			this.v32 = this.boundCharacter.bones.Root.localEulerAngles;
			this.v32.z += this.boundCharacter.writheR * -5f;
			this.v32.y += this.boundCharacter.writheR * 5f;
			this.v32.x += this.boundCharacter.writheF * -10f;
			this.boundCharacter.bones.Root.localPosition = this.v3;
			this.boundCharacter.bones.Root.localEulerAngles = this.v32;
			Transform transform = null;
			for (int i = 0; i < 3; i++)
			{
				switch (i)
				{
				case 0:
					transform = this.boundCharacter.bones.SpineLower;
					break;
				case 1:
					transform = this.boundCharacter.bones.SpineMiddle;
					break;
				case 2:
					transform = this.boundCharacter.bones.SpineUpper;
					break;
				}
				this.v32 = transform.localEulerAngles;
				if (this.boundCharacter.writheF < 0f)
				{
					this.boundCharacter.writheR *= 1f + this.boundCharacter.writheF * 0.3f;
				}
				this.v32.x += this.boundCharacter.writheR * 1.4f * (float)(5 + i) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.z += (0f - this.boundCharacter.writheR) * 0.8f * (float)(5 + i) * BondageApparatus.cap(this.boundCharacter.writheF + 1f, 0f, 1f);
				this.v32.y += this.boundCharacter.writheF * -1.35f * (float)(5 + i);
				transform.localEulerAngles = this.v32;
			}
			Transform root = this.boundCharacter.bones.Root;
			root.position += this.boundCharacter.up() * this.boundCharacter.humpBumpAmount_act * 0.2f;
			break;
		}
		}
		this.boundCharacter.humpBumpAmount = BondageApparatus.cap(this.boundCharacter.humpBumpAmount, 0f, 1f);
		this.boundCharacter.humpBumpAmount_act += (this.boundCharacter.humpBumpAmount - this.boundCharacter.humpBumpAmount_act) * BondageApparatus.cap(Time.deltaTime * 9f, 0f, 1f);
		this.boundCharacter.humpBumpAmount -= this.boundCharacter.humpBumpAmount * BondageApparatus.cap(Time.deltaTime * 10f, 0f, 1f);
	}

	public static float cap(float num, float low = 0f, float high = 1f)
	{
		return Game.cap(num, low, high);
	}
}
