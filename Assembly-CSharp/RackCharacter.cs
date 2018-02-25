using RootMotion;
using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RackCharacter
{
	public class BlendShapeDefinition
	{
		public string name;

		public Vector3[] verts;

		public Vector3[] normals;

		public Vector3[] tangents;
	}

	public class arousingThought
	{
		public float arousal;

		public float duration;

		public float durationMidpoint;
	}

	public class clothingRefVertDefinition
	{
		public string clothingName;

		public int[] refPieces;

		public int[] refVerts;
	}

	public Game game;

	public int uid;

	public ManifestPiece[] characterPieces;

	public CharacterData data;

	public GameObject GO;

	public string manifesturl;

	public string asseturl;

	public AssetBundle assetBundle;

	public bool initted;

	public bool manifestLoaded;

	public bool referencesLoaded;

	public bool assetsLoaded;

	public bool gameObjectsBuilt;

	public static List<string> charactersReboned;

	public static List<string> allSpecies;

	public int mostRecentMeshModificationTime;

	public float loadAccountedFor;

	public NPC npcData;

	public bool followPC;

	public GameObject hairPiece;

	public Animator animator;

	public static CharacterBundle allPieceBundle;

	public static GameObject allPieces;

	public List<GameObject> parts = new List<GameObject>();

	public List<GameObject> preciseMousePickingCollider = new List<GameObject>();

	public List<Vector3[]> originalverts = new List<Vector3[]>();

	public List<int> originalVertexCounts = new List<int>();

	public Bones bones = new Bones();

	public Dictionary<string, Vector3> startRot = new Dictionary<string, Vector3>();

	public SkinnedMeshRenderer headPiece;

	public int headPieceIndex;

	public SkinnedMeshRenderer wingPiece;

	public int wingPieceIndex;

	public SkinnedMeshRenderer bodyPiece;

	public int bodyPieceIndex;

	public SkinnedMeshRenderer penisPiece;

	public int penisPieceIndex;

	public SkinnedMeshRenderer ballsPiece;

	public int ballsPieceIndex;

	public SkinnedMeshRenderer vaginaPiece;

	public int vaginaPieceIndex;

	public SkinnedMeshRenderer handsPiece;

	public int handsPieceIndex;

	public SkinnedMeshRenderer feetPiece;

	public int feetPieceIndex;

	public SkinnedMeshRenderer tailPiece;

	public int tailPieceIndex;

	public bool usingGhostMat;

	public float boobWeight;

	public List<EmbellishmentLayer> tailFurEmbellishments = new List<EmbellishmentLayer>();

	public bool hidden;

	public bool frozen;

	public bool effectivelyFrozen;

	private int freezeDelay;

	public string rightHandItem = string.Empty;

	public string leftHandItem = string.Empty;

	public string rightHandItem_current = string.Empty;

	public string leftHandItem_current = string.Empty;

	public bool rightHandItemHasFingerPoses;

	public bool leftHandItemHasFingerPoses;

	public Transform idleHandLTarget;

	public Transform idleHandRTarget;

	public float customHeadScale = 1f;

	public int tailLength_act;

	public bool useUniversalPenis;

	public bool useUniversalBalls;

	public bool useUniversalVagina;

	public bool useUniversalBody;

	public bool showBalls;

	public bool showPenis;

	public bool showVagina;

	public bool showWings;

	public float breastSize_act;

	public float breastPerk_act;

	public float height_act;

	public float bodyMass_act;

	public float adiposity_act;

	public float bodyFemininity_act;

	public float headFemininity_act;

	public float totalFemininity;

	public float totalPenisSize;

	public float totalBallSize;

	public float growerShower_act;

	public float penisCurveX_act;

	public float penisCurveY_act;

	public float penisLength_act;

	public float penisGirth_act;

	public float penisSize_act;

	public float ballSize_act;

	public float scrotumLength_act;

	public float nippleSize_act;

	public float vaginaPlumpness_act;

	public float vaginaShape_act;

	public float clitSize_act;

	public float belly_act;

	public float muscle_act;

	public float hipWidth_act;

	public float buttSize_act;

	public float shoulderWidth_act;

	public List<ChemicalCompound> chemicalsInSystem = new List<ChemicalCompound>();

	public float artificialOrgasm;

	public float orgasmPrevention;

	public float artificialSmallness;

	public float artificialBigness;

	public float artificialSizeChange;

	public static GameObject clothingContainer;

	public string[] clothingSlots;

	public string[] lastSexToySlots;

	public string[] sexToySlots;

	public string[] lastSexToySlotUIDs;

	public LayoutItemSpecialProperties[] sexToySlotProperties;

	public List<GameObject> clothingPiecesEquipped = new List<GameObject>();

	public float breastSupportFromClothing;

	public List<Mesh> clothingPieceStartPoseMesh = new List<Mesh>();

	public List<ClothingReferenceData> clothingRefData = new List<ClothingReferenceData>();

	public bool clothingRefsBuilt;

	public string facialExpression = "happy";

	public float timeSinceLastEmote;

	public float emoteTime;

	public string emoteString = string.Empty;

	public string emoteThought = string.Empty;

	public GameObject emote;

	public Transform emoteContainer;

	private UnityEngine.UI.Image emoteBG;

	private UnityEngine.UI.Image emoteBGframe;

	private Text emoteTxt;

	private Text emoteNameTxt;

	public float talkativeness = 1f;

	public float timeSinceDisobedienceOrReprimand = 100f;

	public bool feetInAir;

	public string footType = string.Empty;

	public float[] breath = new float[8];

	public float breathIntensity = 1f;

	public float breathTime;

	public Vector3 focusPoint = default(Vector3);

	public Vector3 effectiveFocusPoint = default(Vector3);

	public Vector3 focusDistraction = default(Vector3);

	public Vector3 focusVector = default(Vector3);

	public float focusDist;

	public float distractionTime;

	public Vector3 headGoal = default(Vector3);

	public Vector3 headRot = default(Vector3);

	public Vector3 headCockAnimation = default(Vector3);

	public Vector3 headCockAnimationTarget = default(Vector3);

	public float headCockChangeDelay;

	public float maxHeadSpeed = 20f;

	public float movingHead;

	public float currentThroatTightness = 0.05f;

	public float eyebrowX;

	public float eyebrowY;

	public float eyebrowR;

	public float eyeJitterX;

	public float eyeJitterY;

	public float effectiveEyeJitterX;

	public float effectiveEyeJitterY;

	public float eyeJitterDelay;

	public float blinkDelay;

	public float blinkSpeed = 0.05f;

	public float effectiveEyeOpenL = 1f;

	public float effectiveEyeOpenR = 1f;

	public Transform[] buttbones = new Transform[2];

	public float buttJiggliness = 0.5f;

	public float currentTailholeTightness = 1f;

	public Transform[] vaginabones = new Transform[5];

	public Vector3[] originalVaginaRotations = new Vector3[8];

	public Vector3[] originalVaginaPositions = new Vector3[8];

	public float currentVaginalTightness = 1f;

	public float penisScale = 1f;

	public Transform[] penisbones = new Transform[5];

	public Transform[] penisinverterbones = new Transform[4];

	public float[] penisBoneLengths = new float[5];

	public Vector3[] lastPenisbonePos = new Vector3[5];

	public static Quaternion[] originalPenisRotations;

	public float cockTwitch;

	public float cockTwitchIntensity;

	public float cockTwitchTime;

	private Vector3 v3_Finger00_R0;

	private Vector3 v3_Finger01_R0;

	private Vector3 v3_Finger02_R0;

	private Vector3 v3_Finger10_R0;

	private Vector3 v3_Finger11_R0;

	private Vector3 v3_Finger12_R0;

	private Vector3 v3_Finger20_R0;

	private Vector3 v3_Finger21_R0;

	private Vector3 v3_Finger22_R0;

	private Vector3 v3_Finger30_R0;

	private Vector3 v3_Finger31_R0;

	private Vector3 v3_Finger32_R0;

	private Vector3 v3_Thumb0_R0;

	private Vector3 v3_Thumb1_R0;

	private Vector3 v3_Thumb2_R0;

	private Vector3 v3_Finger00_R1;

	private Vector3 v3_Finger01_R1;

	private Vector3 v3_Finger02_R1;

	private Vector3 v3_Finger10_R1;

	private Vector3 v3_Finger11_R1;

	private Vector3 v3_Finger12_R1;

	private Vector3 v3_Finger20_R1;

	private Vector3 v3_Finger21_R1;

	private Vector3 v3_Finger22_R1;

	private Vector3 v3_Finger30_R1;

	private Vector3 v3_Finger31_R1;

	private Vector3 v3_Finger32_R1;

	private Vector3 v3_Thumb0_R1;

	private Vector3 v3_Thumb1_R1;

	private Vector3 v3_Thumb2_R1;

	public Transform[] wingbones = new Transform[2];

	public Vector3[] lastWingPos = new Vector3[2];

	public Transform[] ballbones = new Transform[3];

	public Vector3[] lastBallPos = new Vector3[4];

	public float ballsackLength = 1f;

	public float ballDroopiness = 0.0001f;

	public Transform[] tailbones = new Transform[9];

	public Vector3[] lastTailbonePos = new Vector3[9];

	public float tailFlickTime;

	public float tailFlickIntensity;

	public List<Objective> objectives = new List<Objective>();

	public int justOrgasmed_tracker;

	public Vector3 startPosition_handLlocal = Vector3.zero;

	public Vector3 startPosition_handRlocal = Vector3.zero;

	public Vector3 startPosition_rootlocal = Vector3.zero;

	public Vector3 startPosition_tailholelocal = Vector3.zero;

	public Vector3 startPosition_headlocal = Vector3.zero;

	public Vector3 startPosition_handL = Vector3.zero;

	public Vector3 startPosition_handR = Vector3.zero;

	public Vector3 startPosition_root = Vector3.zero;

	public Vector3 startPosition_tailhole = Vector3.zero;

	public Vector3 startPosition_head = Vector3.zero;

	public float satisfactionFromEnjoyment;

	public float dissatisfactionFromEnjoyment;

	public float satisfactionFromObjectives;

	public float dissatisfactionFromObjectives;

	public float satisfaction;

	public float sensitivity = 1f;

	public float targetStimulation = 0.5f;

	public float anticipation;

	public float arousal;

	public float orgasm;

	public float orgasming;

	public float refractory;

	public float lastArousal = 1f;

	public float stimulation;

	public float pleasure;

	public float overstimulation;

	public float discomfort;

	public float cumInAnus;

	public float cumInVagina;

	public float cumInMouth;

	public UnityEngine.Color cumInAnusColor;

	public UnityEngine.Color cumInVaginaColor;

	public UnityEngine.Color cumInMouthColor;

	public Dictionary<string, int> preferencePlayerKnowledge = new Dictionary<string, int>();

	public Dictionary<string, int> preferenceKnowledge = new Dictionary<string, int>();

	public Dictionary<string, float> preferences = new Dictionary<string, float>();

	public Dictionary<string, int> confidencePlayerKnowledge = new Dictionary<string, int>();

	public Dictionary<string, int> confidences = new Dictionary<string, int>();

	public Dictionary<string, float> inlineDialogueStaleness = new Dictionary<string, float>();

	public float playerKnowledgeOfInteractionPreferences;

	public float playerKnowledgeOfAttractionPreferences;

	public float playerKnowledgeOfExperiencePreferences;

	public float playerKnowledgeOfConfidences;

	public float playerKnowledgeOfSubmissionTraits;

	public Dictionary<string, int> commandStatus = new Dictionary<string, int>();

	public bool midair = true;

	public bool moving;

	public Vector3 targetLocation = default(Vector3);

	public float moveSpeed;

	public bool autoWalking;

	public Action autowalkCallback;

	public Vector3 autoWalkFinishAngle = default(Vector3);

	public Vector3 autoWalkLocation = default(Vector3);

	public float satisfactionValue;

	public string racknetID = string.Empty;

	public string racknetAccountID = string.Empty;

	public bool controlledByPlayer;

	public float mX;

	public float mY;

	public Rigidbody movementTarget;

	public float recentJump = 99f;

	public int lastMovingSurfaceID = -1;

	public GameObject currentMovingSurface;

	public Vector3 lastMovingSurfacePosition = default(Vector3);

	public bool ridingMovingElevator;

	public Quaternion origRot;

	public Quaternion newRot;

	public Vector3 v3 = default(Vector3);

	public Vector3 v32 = default(Vector3);

	public Vector3 v33 = default(Vector3);

	public Quaternion quat;

	public Vector3 spawnPoint;

	public float timeSpentAutowalking;

	public float autowalkTimeout;

	public float highestPreferenceCategoryValue;

	public float enjoymentWeight_sensations;

	public float enjoymentWeight_experiences;

	public float enjoymentWeight_attraction;

	public bool effectivelyPlantigrade;

	public bool forcePlantigrade;

	public float timeSincePreciseBake;

	public Vector3 walkAimer;

	public float selectedMoveSpeed;

	public float ninetyDegrees = 1.57075f;

	public bool crotchCoveredByClothing;

	public bool breastsCoveredByClothing;

	public float interactionMX = 0.5f;

	public float interactionMY = 0.5f;

	public float interactionMX_raw = 0.5f;

	public float interactionMY_raw = 0.5f;

	public Vector3 interactionMouseChange = Vector3.zero;

	public Vector3 lastInteractionMousePosition = Vector3.zero;

	public float interactionControlTime;

	public float interactionSpeed;

	public Vector3 interactionStartCenter = new Vector3(0.5f, 0.5f, 0f);

	public int controlMode;

	public float interactionHandleX = 0.5f;

	public float interactionHandleY = 0.5f;

	public float interactionVigor;

	public int postAssetLoadStep;

	public float loadStepsCompleted;

	public static int totalLoadSteps;

	public string loadProgressString = string.Empty;

	public bool isPreviewCharacter;

	private int lastMS;

	private UnityEngine.Color[][] canvasPixels = new UnityEngine.Color[6][];

	public static List<UnityEngine.Color[]> layerPixels;

	public static List<string> layerIDs;

	private UnityEngine.Color[] curLayerPixels;

	private int dif;

	private int LID;

	private UnityEngine.Color tarCol;

	private float alph;

	public bool needRedrawBecauseOfUnfinishedDecal;

	public int numUnfinishedDecals;

	public bool showTexLayerDrawTimes;

	private List<string> texturesToUnload = new List<string>();

	public Vector4[] threadedImagePixels;

	private System.Drawing.Color curPix;

	private BitmapData bitmapData;

	private Bitmap image;

	private int bytesPerPixel;

	private int byteCount;

	private byte[] pixels;

	private IntPtr ptrFirstPixel;

	private int heightInPixels;

	private int widthInBytes;

	private Thread textureDrawingThread;

	private bool customGlow;

	private float[] headFurModifierPixels = new float[1];

	private float[] bodyFurModifierPixels = new float[1];

	private float[] wingFurModifierPixels = new float[1];

	private bool haveFurModifiersBeenInitialized;

	public List<string> textureSuffixes;

	public List<string> customTexturesWeNeedToDownload = new List<string>();

	public bool alreadyCheckedForCustomTextures;

	private Texture2D headCanvas;

	private Texture2D bodyCanvas;

	private Texture2D wingCanvas;

	private Texture2D headFXCanvas;

	private Texture2D headMetalCanvas;

	private Texture2D headEmitCanvas;

	private Texture2D bodyFXCanvas;

	private Texture2D bodyMetalCanvas;

	private Texture2D bodyEmitCanvas;

	private Texture2D wingFXCanvas;

	private Texture2D wingMetalCanvas;

	private Texture2D wingEmitCanvas;

	private Texture2D headControlCanvas;

	private Texture2D bodyControlCanvas;

	private Texture2D tailControlCanvas;

	private Texture2D wingControlCanvas;

	private Texture2D tex;

	private string characterTextureDirectory;

	public bool buildingTexture;

	public bool waitingForTextureBuilder;

	public static bool TextureBuilderBusy;

	public static bool showTextureBuildTimes;

	private bool queueTextureBuild;

	private float defaultFurMapScale = 1f;

	private bool needFurLOD;

	private UnityEngine.Color[] controlPixels;

	private UnityEngine.Color[] wetPixels;

	public Rigidbody[] rigidbodies;

	public Collider[] colliders;

	public float ballsRetracting;

	private List<DateTime> timerStarted = new List<DateTime>();

	private List<string> timerNames = new List<string>();

	private DateTime profileStart;

	private bool verboseProfiling = true;

	public bool rebuilding;

	private bool queueRebuild;

	public float rebuildDelay;

	public bool needFirstFurClick;

	public List<Appendage> hairAppendages = new List<Appendage>();

	public Mesh oBody;

	public Mesh oFeet;

	public BoneWeight[] originalBodyPieceBoneWeights;

	private List<List<int>> tailRingVerts;

	private int lastTailLength = -1;

	private List<Vector3> tailRingCenters = new List<Vector3>();

	private int numTailRings;

	private int tailTipID = -1;

	private List<Vector3> sortedTailVerts = new List<Vector3>();

	private List<int> leftoverVerts = new List<int>();

	public static List<Mesh> embellishmentMeshes;

	public bool hadOutdatedEmbellishments;

	private Vector3[] newBlendshapeVerts;

	private Vector3[] newBlendshapeNormals;

	private Vector3[] newBlendshapeTangents;

	private List<Vector3> newVerts;

	private List<int> newVertReferenceVertIndices;

	private List<Vector3> newNormals;

	private List<int> newTriangles;

	private List<BoneWeight> newBoneWeights;

	private List<Vector2> newUVs;

	public static Vector3 originalEyeLPosition;

	public static Vector3 originalEyeRPosition;

	public Quaternion[] originalTongueRotation;

	public Vector3[] originalFootRotations = new Vector3[2];

	public Vector3 originalUpperEyelidPositionL;

	public Vector3 originalUpperEyelidPositionR;

	private Transform[] earbones = new Transform[12];

	private Vector3[] lastEarPos = new Vector3[12];

	private static Vector3[] originalEarPositions;

	private static Vector3[] earStartingAngles;

	private Collider[] earColliders = new Collider[12];

	private ConfigurableJoint[] earJoints = new ConfigurableJoint[12];

	private Transform[] originalEarParents = new Transform[12];

	private SoftJointLimitSpring earTwistSpring;

	private SoftJointLimitSpring earSwingSpring;

	private SoftJointLimit earS1Limit;

	private SoftJointLimit earS2Limit;

	private SoftJointLimit earHTLimit;

	private SoftJointLimit earLTLimit;

	private Rigidbody[] earRigidbodies = new Rigidbody[12];

	public float earCenter;

	private bool longEars;

	private float earStiffness = 0.8f;

	private float earForward;

	private Vector3 origAngle;

	private Vector3 earTargetVec3 = default(Vector3);

	private Quaternion earTargetQuat = default(Quaternion);

	private JointDrive earTargetDrive = default(JointDrive);

	private bool allowEarUnparenting;

	private Vector3 lastHeadPosition = default(Vector3);

	private Vector3 bellyRotation;

	private Vector3 bellyOriginalRotation;

	private Vector3 bellyOriginalPosition;

	private float lastBellyY;

	private Vector3 bellyVelocity;

	public SphereCollider BellyCollider;

	public CapsuleCollider LowerSpineCollider;

	public CapsuleCollider MiddleSpineCollider;

	public CapsuleCollider UpperSpineCollider;

	private ConfigurableJoint[] bellyJoints = new ConfigurableJoint[2];

	private Rigidbody[] bellyRigidbodies = new Rigidbody[2];

	private Vector3[] lastBellyPositions = new Vector3[2];

	private Transform[] boobbones = new Transform[2];

	private static Vector3[] boobOriginalRotations;

	private static Vector3[] boobOriginalPositions;

	private static Vector3[] originalBoobAnchors;

	private Vector3[] boobOriginalScales = new Vector3[2];

	private SoftJointLimit boobLimit = default(SoftJointLimit);

	private SoftJointLimitSpring boobLimitSpring = default(SoftJointLimitSpring);

	private ConfigurableJoint[] boobJoints = new ConfigurableJoint[4];

	private Rigidbody[] boobRigidbodies = new Rigidbody[4];

	private SphereCollider[] boobColliders = new SphereCollider[2];

	public float[] boobPushInFromArm = new float[2];

	public float[] boobPushedInFromArm = new float[2];

	public float[] boobPushingInSpeed = new float[2];

	public SexToy selectedSexToy;

	public bool waitingToUpdateClothes;

	public List<SexToy> equippedSexToys = new List<SexToy>();

	private SoftJointLimitSpring buttTwistSpring = default(SoftJointLimitSpring);

	private SoftJointLimitSpring buttSwingSpring = default(SoftJointLimitSpring);

	private SoftJointLimit buttS1Limit = default(SoftJointLimit);

	private SoftJointLimit buttS2Limit = default(SoftJointLimit);

	private SoftJointLimit buttHTLimit = default(SoftJointLimit);

	private SoftJointLimit buttLTLimit = default(SoftJointLimit);

	public GameObject tailholeEntrance;

	public static Vector3 originalAnusPosition;

	public static Vector3 originalAnusLeftPosition;

	public static Vector3 originalAnusRightPosition;

	public static Vector3 originalAnusTopPosition;

	public static Vector3 originalAnusBottomPosition;

	public static Quaternion originalAnusLeftRotation;

	public static Quaternion originalAnusRightRotation;

	public static Quaternion originalAnusTopRotation;

	public static Quaternion originalAnusBottomRotation;

	public static Quaternion originalAnusRotation;

	public static List<Quaternion> originalButtRotations;

	private ConfigurableJoint[] buttJoints = new ConfigurableJoint[2];

	private Vector3[] buttOriginalPositions = new Vector3[2];

	private Rigidbody[] buttRigidbodies = new Rigidbody[2];

	private Vector3[] lastHipPosition = new Vector3[2];

	public static bool gotOriginalButtStuff;

	public static Vector3[] originalBallPositions;

	public static Vector3[] originalBallAngles;

	private Transform[] originalBallParents = new Transform[3];

	private ConfigurableJoint[] ballJoints = new ConfigurableJoint[4];

	private SphereCollider[] ballColliders = new SphereCollider[2];

	private BoxCollider[] scrotumColliders = new BoxCollider[1];

	private Rigidbody[] ballRigidbodies = new Rigidbody[3];

	private SoftJointLimit ballLimit = default(SoftJointLimit);

	private static Vector3[] originalBallAnchors;

	private bool allowBallsUnparenting;

	private Quaternion originalClitRotation;

	public GameObject vaginaEntrance;

	public GameObject vaginaContainer;

	public Vector3 originalVaginaPosition;

	private SoftJointLimitSpring penisTwistSpring = default(SoftJointLimitSpring);

	private SoftJointLimitSpring penisSwingSpring = default(SoftJointLimitSpring);

	private SoftJointLimit penisS1Limit = default(SoftJointLimit);

	private SoftJointLimit penisS2Limit = default(SoftJointLimit);

	private SoftJointLimit penisHTLimit = default(SoftJointLimit);

	private SoftJointLimit penisLTLimit = default(SoftJointLimit);

	private SoftJointLimit penisLinearLimit = default(SoftJointLimit);

	private Transform PenisBase;

	public Quaternion originalPubicRotation;

	public Vector3 penisBaseRelativeToRoot;

	private static Vector3[] originalPenisBonePositions;

	private CapsuleCollider[] penisColliders = new CapsuleCollider[5];

	private Rigidbody[] penisRigidbodies = new Rigidbody[5];

	private ConfigurableJoint[] penisJoints = new ConfigurableJoint[5];

	private Transform[] originalPenisParents = new Transform[5];

	private SoftJointLimitSpring penisLimitSpring = default(SoftJointLimitSpring);

	public static float originalPenisLengthMinusHead = 0f;

	public static float originalPenisHeadLength = 0f;

	private bool allowPenisUnparenting;

	public Quaternion[] wingInRots;

	public Quaternion[] wingOutRots;

	public static Vector3[] tailStartingAngles;

	public static Vector3[] originalTailbonePosition;

	public List<float> originalTailCapsuleHeight;

	private Vector3 tailCurlVec3 = default(Vector3);

	private Quaternion tailCurlQuat = default(Quaternion);

	private JointDrive tailCurlDrive = default(JointDrive);

	private SoftJointLimit tailS1Limit = default(SoftJointLimit);

	private SoftJointLimit tailS2Limit = default(SoftJointLimit);

	public string furniturePose = string.Empty;

	public Furniture furniture;

	public Vector3 preFurniturePosition;

	private Transform furnitureRoot;

	private Transform delayedFurnitureUnphysics;

	private float delayedFurnitureUnphysicsTime;

	private bool aWasEnabled;

	private bool bWasEnabled;

	public BondageApparatus delayedApparatusBind;

	public float delayedApparatusTime;

	public string boundPose = string.Empty;

	public BondageApparatus apparatus;

	public bool headMovementBlockedByApparatus_back;

	public IKSolver FIKsolver;

	public Vector3 shoulderLstartPos = Vector3.zero;

	public Vector3 shoulderRstartPos = Vector3.zero;

	public Transform bodyTarget;

	public Vector3 originalBodyTargetPosition;

	public Transform elbowTargetL;

	public Transform elbowTargetR;

	public GameObject tailholeEntranceAfterIK;

	public GameObject vaginalEntranceAfterIK;

	public GameObject throatHoleAfterIK;

	public GameObject throatHoleAfterIKandSuckLock;

	public GameObject mouthOpeningAfterIK;

	public Transform headTarget;

	public GameObject throatHole;

	private bool usingDefaultBodyTarget = true;

	public bool customIdleHandLtarget;

	public bool customIdleHandRtarget;

	public float idleHandLclench;

	public float idleHandRclench;

	public float idleHandLclenchT;

	public float idleHandRclenchT;

	public GameObject rootAfterIK;

	public float mirrorSwitchDelay;

	public bool mirroredSwitch;

	public bool faceControlledByAnimation;

	public bool allowLimitedEyeMovement;

	public int animationPauseTime;

	public bool animationPaused;

	public float furnitureRootSnap;

	public int curFacialAnimation = -1;

	public Transform cameraFocusPoint;

	public Vector3 testTubeTurbulence = default(Vector3);

	private float gravityModifier;

	private int physicsTickCount;

	private bool[] originalColliderStatus;

	private bool collidersWereOn = true;

	private bool needColliderRebuild;

	private bool fadingOutCharacter;

	private bool wasFadingOutCharacter;

	private float fadeOutCharacterAmount;

	private bool wasHidden;

	private bool[] wasFaded = new bool[12];

	private bool originalMatColSaved;

	private UnityEngine.Color originalCharacterMatCol;

	private UnityEngine.Color fadeCol;

	private float[] fadeAmount = new float[12];

	private bool needFadeUpdate;

	private bool pieceHidden;

	public bool bodyGhosted;

	public bool bodySuperGhosted;

	public bool tailGhosted;

	private UnityEngine.Color rimColor;

	public float[] rimAmount = new float[12];

	public static Shader originalCharacterShader;

	public bool inFrontOfCamera;

	public float distFromCamera = 100f;

	public bool lineOfSightToCamera;

	public ChemicalData chemData = new ChemicalData();

	private float fadeRate;

	private float artificialSizeChange_target;

	private int colliderToggleDelay;

	private bool givenPlayerKnowledgeOfSelf;

	public Dictionary<string, float> thoughts = new Dictionary<string, float>();

	private float emoteDelay;

	private float highestThoughtWeight;

	private string highestThought = string.Empty;

	private float timeBetweenEmotes = 10f;

	private float adjustedThoughtWeight;

	private Vector3 ibhp;

	public Vector3 emoteOffset = default(Vector3);

	public int emoteWithName;

	public float muteEmoteTime;

	public float recentIDprompt;

	private bool justFinishedMuteTime;

	public float autoDialogueFamiliarity;

	public bool muffledSpeech;

	private bool wasNearOrgasm;

	private float timeSinceEmoteAboutPain = 99f;

	private int wasRequestingChange;

	private int requestingChange;

	private float timeSpentInteracting;

	private float timeSinceInteraction;

	private float totalSessionTimeSpentInteracting;

	public int helloPhase;

	public float precumFrequency;

	public float precumDelay = 1f;

	public CumDot currentPrecumDot;

	public float wetdropFrequency;

	public float wetdropDelay = 1f;

	public CumDot currentwetdropDot;

	public float analdripDelay = 1f;

	public CumDot currentAnaldripDot;

	public float wetness_vagina;

	public float wetness_penis;

	public float wetness_finger0;

	public float wetness_finger1;

	public float wetness_fist;

	public float wetness_muzzle;

	public float rendered_wetness_vagina;

	public float rendered_wetness_penis;

	public float rendered_wetness_finger0;

	public float rendered_wetness_finger1;

	public float rendered_wetness_fist;

	public float rendered_wetness_muzzle;

	public Vector3 up_AIK = Vector3.zero;

	public Vector3 right_AIK = Vector3.zero;

	public Vector3 forward_AIK = Vector3.zero;

	public List<arousingThought> arousingThoughts = new List<arousingThought>();

	public int numberOfStimulatingInteractions;

	public int lastKnownNumberOfStimulatingInteractions = -1;

	public bool lastKnownInPain;

	public int numberOfArousingInteractions;

	public int lastKnownNumberOfArousingInteractions;

	public bool wasOrgasming;

	public float timeSinceExpressionChange;

	public List<string> expressionOptions = new List<string>();

	public int overstimulationEmotionalState;

	public int lastOverstimulationEmotionalState;

	public float overstimulationEmotionThreshhold0 = 0.05f;

	public float overstimulationEmotionThreshhold1 = 0.25f;

	public bool inPain;

	public float timeSincePain = 100f;

	public string effectivePersonality = "happy";

	public string characterVoice = "pleasant";

	public float aggression;

	public float dominance;

	public float friendlinessToPlayer;

	public bool rebellious;

	public float[] specimenProduced = new float[6];

	public float[] stockpiledSpecimenProduced = new float[6];

	public float[] curFrameSpecimenProduced = new float[6];

	public float rateOfSpecimenProduction;

	private float specimenSpeedModifier = 0.04f;

	private float edgeTime;

	private float sessionTimeFactor;

	private float targetEdgeTime;

	private float nonLEchemicalsProducedSinceLastOrgasm;

	public float projectedLapinine;

	public float projectedEquimine;

	private float tar_enjoymentFromInteractions;

	private float tar_unenjoymentFromInteractions;

	private float tar_enjoymentFromExperience;

	private float tar_unenjoymentFromExperience;

	private float tar_enjoymentFromAttraction;

	private float tar_unenjoymentFromAttraction;

	public float enjoymentFromInteractions;

	public float unenjoymentFromInteractions;

	public float enjoymentFromExperience;

	public float unenjoymentFromExperience;

	public float enjoymentFromAttraction;

	public float unenjoymentFromAttraction;

	private float tar_secretEnjoymentFromInteractions;

	private float tar_secretEnjoymentFromExperience;

	private float tar_secretEnjoymentFromAttraction;

	public float secretEnjoymentFromInteractions;

	public float secretEnjoymentFromExperience;

	public float secretEnjoymentFromAttraction;

	public float totalEnjoymentAndUnenjoyment;

	private GameObject enjoymentUI;

	public float enjoymentPollDelay;

	public int nextEnjoymentToPoll;

	private bool ableToExperienceEnjoyment;

	private float pollTime = 0.4f;

	private int numEnjoymentPolls = 3;

	public Dictionary<string, float> experienceEnjoymentCauses = new Dictionary<string, float>();

	private Dictionary<string, float> sensationTimes = new Dictionary<string, float>();

	private Dictionary<string, float> fetishTimes = new Dictionary<string, float>();

	private float timeAtEdge;

	private float idealEdgeTime;

	private float edgeAmount;

	private float enjoymentChangeBecauseOfFastOrgasm;

	public int howManyCharactersCanSeeMe;

	public int howManyCharactersAmIinteractingWith;

	public int howManyWatchers;

	private List<int> interactingWithCharacters = new List<int>();

	private List<int> charactersIcanSee = new List<int>();

	public float cumulativePartnerSatisfaction;

	public float lastKnownCumulativePartnerSatisfaction;

	public bool readyToCum;

	public bool beingStimulatedAutomatically;

	public Dictionary<string, float> attractionEnjoymentCauses = new Dictionary<string, float>();

	private RackCharacter partner;

	public float proximityToOrgasm;

	public float currentOrgasmDuration = 1f;

	public float currentRefractoryDuration = 1f;

	public float orgasmTwitch;

	public float orgasmSoftPulse;

	public float sensitivityAdjustment = 1f;

	public float discomfortFromSourcesOtherThanStimulation;

	public float percievedStimulation;

	public float arousalLossRate = 1f;

	public int numberOfOrgasms;

	public float howMuchDoIWantToOrgasm = 1f;

	private float sensitivityAdjustmentTarget = 1f;

	private float rFactor;

	private float effectiveStamina;

	private float tarHMDIWTO;

	public float forcedPleasure;
    
	public static float targetStimulationGlobalAdjustment;
    
	public float cumIntensity;

	public bool emittingCum;

	public bool emittingSquirt;

	public float squirtDotEmitDelay;

	public float cumDotEmitDelay;

	private float cumDotEmitFrequency = 0.004f;

	public CumDot previousCumDot;

	public float currentCumEmitThickness;

	public float cumRandomVelocityX;

	public float cumRandomVelocityY;

	public float cumSpurtDelay;

	public float currentCumSpurt;

	public UnityEngine.Color col;

	private Vector3 lastPenisTip = default(Vector3);

	private Vector3 lastCumVelocity = default(Vector3);

	private Vector3 cumVelocity = default(Vector3);

	private Vector3 cumEmitPositionDifference = default(Vector3);

	private Vector3 cumEmitVelocityDifference = default(Vector3);

	private Vector3 lastSquirtTip = default(Vector3);

	private Vector3 lastSquirtVelocity = default(Vector3);

	private Vector3 squirtVelocity = default(Vector3);

	private Vector3 squirtEmitPositionDifference = default(Vector3);

	private Vector3 squirtEmitVelocityDifference = default(Vector3);

	private int nextCumSFX;

	private float cumDotsToEmit;

	private ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);

	private Vector3 grav;

	public bool wasEmittingCum;

	public bool wasEmittingSquirt;

	private float cumSpurtTrimStart;

	private float squirtSpurtTrimStart;

	private float squirtDotsToEmit;

	private float cumWobbleFactor;

	public List<string> involvedInteractions = new List<string>();

	public Dictionary<string, float> interactionsEnjoymentCauses = new Dictionary<string, float>();

	public List<string> toysAlreadyAccountedFor = new List<string>();

	public int numberOfInteractionsFromSexToys;

	private string[] interactionPref = new string[4];

	private float numberOfInteractionPrefs;

	public bool currentlyUsingPenis;

	public bool currentlyUsingVagina;

	public bool currentlyUsingMouth;

	public bool currentlyUsingButt;

	public bool interactingWithSelf;

	public bool currentlyUsingHandL;

	public bool currentlyUsingHandR;

	public Transform headInteractionMountPoint;

	public List<Interaction> currentInteractions = new List<Interaction>();

	public bool usingSexToy;

	public SexToy currentSexToy;

	public float recentlyRemovedPenisFromInteraction;

	public int curSexPose;

	public string curSexPoseName = "default";

	public RackCharacter interactionSubject;

	public BondageApparatus interactionApparatus;

	public Transform interactionPoint;

	public string lastInteractionPointName = string.Empty;

	public List<GameObject> temporaryPerformerMountPoints = new List<GameObject>();

	public bool hasFootTargets;

	public List<Transform> interactionHotspots = new List<Transform>();

	public List<InteractionTrigger> interactionHotspotTriggers = new List<InteractionTrigger>();

	public List<Collider> interactionHotspotColliders = new List<Collider>();

	public bool isInteractionSubject;

	public bool attachedToInteractionPoint;

	public float testCylinderSize = 0.1f;

	public float minimumPositionAlongPenis;

	public float maximumPositionAlongPenis;

	public float handPositionAlongPenis;

	public float lastHandPositionAlongPenis;

	private float bodyIKweight;

	public Vector3 aikPenisTip = Vector3.zero;

	private bool girthsInitted;

	public List<List<List<float>>> polledGirths = new List<List<List<float>>>();

	public int girthArousalSegments = 1;

	public int girthPollingSegments = 20;

	public bool needGirthUpdate;

	public float penisLengthInWorldUnits_tar;

	public float penisLengthInWorldUnits;

	public bool pollingPenisGirth;

	private Vector3 gpap;

	public bool penisBeingBuried;

	public Vector3 penisBuryOrifice;

	public Vector3 penisBuryTarget;

	public Vector3 penisBuryInsidePoint;

	public float buryTarDistance;

	public static float positionAlongPenetratorPenisStructureCutoff0;

	public static float positionAlongPenetratorPenisStructureCutoff1;

	public static float positionAlongPenetratorPenisStructureCutoff2;

	public static float positionAlongPenetratorPenisStructureCutoff3;

	private Vector3 dap;

	public static Vector3 pap;

	public Vector3 fingerTarget0L;

	public Vector3 fingerTarget1L;

	public Vector3 fingerTarget2L;

	public Vector3 fingerTarget3L;

	public Vector3 fingerTarget0R;

	public Vector3 fingerTarget1R;

	public Vector3 fingerTarget2R;

	public Vector3 fingerTarget3R;

	private bool aimingToy;

	private Vector3 toyAimPosition;

	public List<float> handClenchL = new List<float>();

	public List<float> handClenchR = new List<float>();

	public List<float> handClenchRootL = new List<float>();

	public List<float> handClenchRootR = new List<float>();

	public bool clenchingHandL;

	public bool clenchingHandR;

	public float handClenchHowMuchHand;

	public float adjustedClench;

	public bool grabbingPoleL;

	public bool grabbingPoleR;

	private float ccr;

	private float fingerSpreadL = 0.5f;

	private float fingerSpreadR = 0.5f;

	public static Vector3 FKToFirstKnuckleL;

	public static Vector3 FKToFirstKnuckleR;

	public static string ApplicationpersistentDataPath;

	public static List<string> customTexturesWeNeedToCheckForZeroLength;

	private List<ImperialFurLOD> furLODs = new List<ImperialFurLOD>();

	private bool hasFurLOD;

	public bool needsRandomDecals;

	public bool needsRandomize;

	public int loadedFromExternal;

	public int threadedTextureDrawingState;

	public bool busyDownloadingACustomTexture;

	public Vector3 shoulderRoll_L = default(Vector3);

	public Vector3 shoulderRoll_R = default(Vector3);

	public float hipRoll_L;

	public float hipRoll_R;

	public Vector3 tailholePositionAfterIK = default(Vector3);

	public Vector3 headPositionAfterIK = default(Vector3);

	public Vector3 headOffsetFromTarget = default(Vector3);

	public static bool allowWrongSubjectHotspots;

	public Vector3 velocityFromKinematicMovement = Vector3.zero;

	public Vector3 tailPositionBeforeForcedPositionUpdateSpecificallyForRenderFrame;

	public bool tailWasJustForcedIntoPlaceForARenderFrame;

	public Vector3 lastUpperSpinePosition;

	public float lastHumpBump;

	public float humpBumpAmount;

	public float humpBumpAmount_act;

	public float writheForward;

	public float writheRight;

	public float writheF;

	public float writheR;

	public float overstimWritheTargetF;

	public float overstimWritheTargetR;

	public float overstimWritheTargetCooldown;

	public float naturalWritheIntensity;

	public float naturalWrithe;

	public static bool testingWrithe;

	public float higherWritheFactor;

	public float writheResistance;

	public float squatAmount;

	public bool squatting;

	public float rootRoll;

	private float hipStiffness;

	private float hipStiffTime;

	private bool idleAnimation;

	private bool IKoriginalSettingsSet;

	private float original_pullBodyHorizontal;

	private float original_pullBodyVertical;

	private float original_spineStiffness;

	private float original_rightArmChainpull;

	private float original_leftArmChainpull;

	private float original_rightArmChainreach;

	private float original_leftArmChainreach;

	public Vector3 hipOffsetFromInteraction = Vector3.zero;

	private float hipShiftTime;

	private float hipShiftDelay;

	private bool aimingMainHand;

	private bool leftHandManuallyAimed;

	private bool rightHandManuallyAimed;

	private Transform originalLHET;

	private Transform originalRHET;

	private Transform manualAimLeftHandTarget;

	private Transform manualAimRightHandTarget;

	private float originalLHP;

	private float originalLHR;

	private float originalRHP;

	private float originalRHR;

	private Vector3 shoulderRollFromInteractionsL = Vector3.zero;

	private Vector3 shoulderRollFromInteractionsR = Vector3.zero;

	public float wingOutR;

	public float wingOutL;

	public Vector3 pubicAngle = default(Vector3);

	public Vector3 lastPubicPosition = default(Vector3);

	private float pubicSwaySideToSide;

	public float pubicPushAmount;

	public bool pubicBeingPushed;

	public float bellyStiffness;

	private float lastBodyMass_act = -1f;

	private float lastAdiposity_act = -1f;

	private float lastBelly_act = -1f;

	private SoftJointLimit bellyLinearLimit = default(SoftJointLimit);

	public float breastPerkiness;

	public float[] boobReturnToPositionSpeed = new float[2];

	public Vector3[] lastBoobPos = new Vector3[4];

	public float[] boobUpAmount = new float[2];

	public float[] boobSideStretchAmount = new float[2];

	public float[] boobTranslateToCenter = new float[2];

	private float boobForceUp;

	private float boobForceIn;

	public static float breastThreshhold;

	private float swap;

	private float lastBreastPerk = -1f;

	private int boobsWereBeingPhysicked = 1;

	public List<GameObject> handheldObjects = new List<GameObject>();

	private Transform ft_Finger00_R0;

	private Transform ft_Finger01_R0;

	private Transform ft_Finger02_R0;

	private Transform ft_Finger10_R0;

	private Transform ft_Finger11_R0;

	private Transform ft_Finger12_R0;

	private Transform ft_Finger20_R0;

	private Transform ft_Finger21_R0;

	private Transform ft_Finger22_R0;

	private Transform ft_Finger30_R0;

	private Transform ft_Finger31_R0;

	private Transform ft_Finger32_R0;

	private Transform ft_Thumb0_R0;

	private Transform ft_Thumb1_R0;

	private Transform ft_Thumb2_R0;

	private Transform hho_Finger00_R0;

	private Transform hho_Finger01_R0;

	private Transform hho_Finger02_R0;

	private Transform hho_Finger10_R0;

	private Transform hho_Finger11_R0;

	private Transform hho_Finger12_R0;

	private Transform hho_Finger20_R0;

	private Transform hho_Finger21_R0;

	private Transform hho_Finger22_R0;

	private Transform hho_Finger30_R0;

	private Transform hho_Finger31_R0;

	private Transform hho_Finger32_R0;

	private Transform hho_Thumb0_R0;

	private Transform hho_Thumb1_R0;

	private Transform hho_Thumb2_R0;

	private Transform hho_Finger00_R1;

	private Transform hho_Finger01_R1;

	private Transform hho_Finger02_R1;

	private Transform hho_Finger10_R1;

	private Transform hho_Finger11_R1;

	private Transform hho_Finger12_R1;

	private Transform hho_Finger20_R1;

	private Transform hho_Finger21_R1;

	private Transform hho_Finger22_R1;

	private Transform hho_Finger30_R1;

	private Transform hho_Finger31_R1;

	private Transform hho_Finger32_R1;

	private Transform hho_Thumb0_R1;

	private Transform hho_Thumb1_R1;

	private Transform hho_Thumb2_R1;

	public static Texture2D headFurControlBase;

	public static Texture2D bodyFurControlBase;

	public static Texture2D wingFurControlBase;

	public static UnityEngine.Color[] wetnessTex_vagina;

	public static UnityEngine.Color[] wetnessTex_penis;

	public static UnityEngine.Color[] wetnessTex_finger0;

	public static UnityEngine.Color[] wetnessTex_finger1;

	public static UnityEngine.Color[] wetnessTex_fist;

	public static UnityEngine.Color[] wetnessTex_muzzle;

	public static Vector4 blackV4;

	public static Transform cumEmitterTransform;

	public static ParticleSystem cumEmitter;

	public static string baseModelFilename;

	public static bool windowsOS;

	public static bool macOS;

	public static bool linuxOS;

	public static List<string> furTypes = new List<string>();

	public static List<string> skinTypes = new List<string>();

	public static Dictionary<string, Texture> furNoiseTextures = new Dictionary<string, Texture>();

	public static Dictionary<string, Texture> skinNormalTextures = new Dictionary<string, Texture>();

	public static List<string> existingTextures = new List<string>();

	public List<string> loadingClothes = new List<string>();

	private List<BoneWeight[]> originalClothingBoneWeights = new List<BoneWeight[]>();

	private List<GameObject> clothingCollisionMeshes = new List<GameObject>();

	public static List<clothingRefVertDefinition> clothingRefDefinitions;

	public bool stepLifting;

	public float stepLiftAmount;

	private Vector3 lastMTPosition = default(Vector3);

	private float distanceFromMovementTarget;

	private float horizontalDistanceFromMovementTarget;

	private float extraMoveRotation;

	private bool pressingAnyMovementKeys;

	private float maxClimb = 0.65f;

	private RaycastHit hit;

	private float climbHelper;

	private bool runSpeedCheat;

	private float timeSpentTryingToGetAutowalkAngle;

	private bool wasInRenderStudio;

	private Vector3 positionBeforeRenderStudio;

	private float rotationBeforeRenderStudio;

	private float photoDelay = 0.1f;

	private bool needPostPhotoAnimationReset;

	private bool jumping;

	private float jumpY;

	private float jumpVelY;

	private bool animatingMovement;

	private Vector3 amountMoved = default(Vector3);

	private Vector3 amountMovedHorizontal = default(Vector3);

	private Vector3 previousPosition = default(Vector3);

	private bool backpedaling;

	private float timeSinceTeleport;

	private float floorFixDelay;

	private float stepLiftVel;

	private StepLift steplift;

	private bool stepliftInitted;

	private GameObject stepLiftSource;

	private Vector3 lastKnownGoodLocation = default(Vector3);

	private float recentFloorFall;

	private Rigidbody GOrigidBody;

	public Vector3 suckLockTarget = default(Vector3);

	public bool suckLock;

	public float suckLickBump;

	private RackCharacter suckLockCharacter;

	private float rootRollTar;

	private bool rollingRoot;

	public Vector3 rootScale;

	public float lastHeight = 1f;

	public float changingFromHeight = 1f;

	public bool newHeight;

	public Vector3 headScale;

	public Quaternion shoulderRotationBeforeIKL;

	public Quaternion shoulderRotationBeforeIKR;

	public float rootGrind;

	public float swallowBulge;

	public float throatBeingFucked;

	public RackCharacter dickSucker;

	public float swallowDelay;

	public float swallowing;

	public Transform GOHandTargetL;

	public Transform GOHandTargetR;

	private Transform GOElbowTargetL;

	private Transform GOElbowTargetR;

	private Transform defaultLeftFootTarget;

	private Transform defaultRightFootTarget;

	public float backBendAmount;

	public bool bendingBack;

	public float neckBendAmount;

	public bool bendingNeck;

	public float backArchAmount;

	public bool archingBack;

	public bool grindingRoot;

	public float cumInLeftEyeTime;

	public float cumInRightEyeTime;

	public Vector3 neckScale;

	public float extraEyeOpenFromPupils;

	public float moveSpeedThresholdForHeadFocus = 0.4f;

	public float firstPersonBodySpin;

	public float maxHeadTurn = 1f;

	public float lockedPositionHeadTurn;

	public float focusedOnPC;

	public float glancingAtPCbody;

	public bool glancingAtBreasts;

	public float distFromPC;

	public float nextPCattention;

	public float PCfocusEyeSwitch;

	public bool PCfocusRightEye;

	private bool suspendedAnim;

	public bool closingEyesInPleasure;

	public float moan;

	public float moanIntensity;

	public float currentMoanLength = 1f;

	public float pleasureEyeOpen = 1f;

	public float pleasureEyeOpenAmount = 1f;

	public float pleasureEyeCheckToggleCooldown;

	public float neckRock;

	public float neckRockVel;

	public float lastHeadMovement;

	public float pupilScale = 1f;

	public Vector3 animatedFocusPoint = Vector3.zero;

	public bool mouthBeingPenetrated;

	public float mouthPenetrationAmount;

	public float mouthPenetrationDrag;

	public float tongueScale = 1f;

	public Vector3 tongueScaleV;

	public float talkingAnimationTime;

	public const float mouthClosedAngle = 246f;

	public const float mouthFullOpenAngle = 279f;

	public float mouthOpenAmount;

	public float mouthOpenAmount_animated;

	public float talkAnimPhraseTime;

	public float talkAnimPhraseTimeTotal;

	public float talkAnimPhraseOpen;

	public float talkAnimPhrase_mouthCorners;

	public float talkAnimPhrase2_nose;

	public float talkAnimPhrase2_nose_act;

	public float talkAnimPhrase2Time;

	public float talkAnimPhrase2_earL_tar;

	public float talkAnimPhrase2_earR_tar;

	public float talkAnimPhrase2_earL;

	public float talkAnimPhrase2_earR;

	public float talkAnimPhrase2_cheek;

	public float talkAnimPhrase2_headX;

	public float talkAnimPhrase2_headY;

	public float talkAnimPhrase2_headZ;

	public float talkAnimPhrase2_headX_act;

	public float talkAnimPhrase2_headY_act;

	public float talkAnimPhrase2_headZ_act;

	public float talkAnimPhrase2_jawX;

	public float mouthOpenAmount_act;

	public float pleasureGasp;

	public float holdMouthOpenTime;

	public float timeSinceBrokeMouthHoldCommand = 100f;

	public Quaternion originalTailRot;

	public float lastGlobalYAng;

	public float tailFlickAgitation = 0.35f;

	public float wagIntensity;

	public float wagIntensity_effective = 1f;

	public float wagTime;

	public Vector3[] lastTailBaseRot = new Vector3[10];

	public bool needTailRecurl = true;

	public float tailsize_act = 1f;

	public float effectiveTailCurlX;

	public float effectiveTailCurlY;

	public float lastRenderedEffectiveTailCurlY;

	public float effectiveTailLift;

	private int tailDisconnectID = 1;

	public Vector3[] tailForceFromHipMovement = new Vector3[10];

	public Rigidbody[] tailRigidBodies = new Rigidbody[10];

	public ConfigurableJoint[] tailJoints = new ConfigurableJoint[10];

	public CapsuleCollider[] tailColliders = new CapsuleCollider[10];

	public float timeSinceTailRecurl;

	private float tailboneCurlAdjustmentX;

	private float tailboneCurlAdjustmentY;

	private float tailFlickPoint = 10f;

	private float tailFlickX;

	private float tailFlickY;

	private float taperFactor = 1f;

	private float lastTailSize = -1f;

	private float tailMomentumTransferSpeed = 5f;

	private bool updatingTailSize;

	private Vector3 penisAngleTarget;

	public bool penisBeingAngled;

	public bool anglingEntirePenis;

	public Vector3 penisAngledAmount = default(Vector3);

	private Vector3 penisNudge = default(Vector3);

	public bool penisBeingNudged;

	private Vector3 clitNudge = default(Vector3);

	public bool clitBeingNudged;

	private float penisArousalScale;

	private Quaternion newPenisRotation;

	private float foreskinOverlap;

	private float ballsOverDickFixTime;

	public float penisDrag;

	public float penisDragTightness;

	private float lastArousalPoll;

	private float foreskinDrag;

	public float knotSwell;

	public float lastKnotSwellPoll;

	public GameObject previousPenisRootPosition;

	public float buryTransition;

	public float penisBuryGirthReduction = 0.5f;

	public bool penisHeadBuried;

	public RackCharacter penisBurialTarget;

	public int penisBurialOrifice;

	public Quaternion[] lastPenisRotations = new Quaternion[5];

	private bool penisKinked;

	private float penisKinkFixer;

	private float recentlyFixingPenisKink;

	public float knotBuryAmount;

	public float timeSincePenisGirthPoll;

	public float timeSinceBuriedPenis;

	private int wasProcessingPenis = -1;

	public static Vector3 originalPenisBasePosition;

	public float penisStretch = 1f;

	public bool penisBeingStretched;

	private AudioSource SFX;

	public static List<AudioClip> characterSounds;

	public static List<string> characterSoundNames;

	private int lastFootstep = -1;

	private int nextFootstep;

	private int nextFootstepSurface;

	private float ra;

	public Transform standingOnSurface;

	public bool spreadingVaginaOuter;

	public bool spreadingVaginaInner;

	public float vaginaSpreadOuter;

	public float vaginaSpreadInner;

	public float vaginaSpreadOuter_act;

	public float vaginaSpreadInner_act;

	public float extraVaginaSpread;

	public float ballRetract;

	private float bsl;

	private float ballDragDownAmount;

	private float ballDragUpAmount;

	private Transform BallCatcher;

	private bool ballsCollidingWithLegs;

	private Transform BallsackOrigin;

	private float ballUpsideDownRetract;

	private SoftJointLimitSpring ballSpringLimitSpring;

	private int ballCollisionWithLegAnimationPollDelay;

	private float unstickingBalls;

	private float invertedBallZ;

	private int ballStickCheckDelay;

	private int wasProcessingBalls = -1;

	public float anusPush;

	public bool anusBeingPushed;

	public float anusDrag;

	public float anusGape;

	public float anusPenetratedByGirth;

	public bool anusBeingPenetrated;

	public float vaginaPush;

	public bool vaginaBeingPushed;

	public float vaginaDrag;

	public float vaginaGape;

	public float vaginaPenetratedByGirth;

	public bool vaginaBeingPenetrated;

	public float vaginaPenetrationSideAmount;

	public List<Vector3> originalButtEulers = new List<Vector3>();

	public List<Vector3> originalButtPositions = new List<Vector3>();

	public Vector3 gravity_vec = default(Vector3);

	public Vector3 relativeRotation_diff;

	public Vector3 angleTo_diff;

	public Quaternion angleTo_originalRot;

	public Quaternion angleTo_newRot;

	private Vector3 axisFixVector = new Vector3(-90f, 90f, 0f);

	private float longestFurLength = 1f;

	private float footstepWeight = 1f;

	public int lastPenisType = -1;

	public float lastForeskinOverlap = -1f;

	public float lastPArousal = -1f;

	private float sheathedAmount;

	private float slitOutAmount;

	public static List<string> clipFixRegionNames;

	public Dictionary<string, float> clipFixes = new Dictionary<string, float>();

	public bool dying;

	public int fallDelay;

	public float backupCumVolume;

	public float backupCumSpurtStrength;

	public float backupCumSpurtFrequency;

	public float backupOrgasmDuration;

	public bool isPissing;

	public bool hasPissed;

	public RackCharacter(Game _game, CharacterData characterDefinition, bool playerControlled = false, object _spawnPoint = null, float spawnAngle = 0f, string buildFromURL = "")
	{
		if (_spawnPoint == null)
		{
			_spawnPoint = Vector3.zero;
		}
		this.spawnPoint = (Vector3)_spawnPoint;
		this.game = _game;
		this.controlledByPlayer = playerControlled;
		this.emote = UnityEngine.Object.Instantiate(this.game.UI.transform.Find("Emotes").Find("Emote").gameObject);
		this.emoteBG = ((Component)this.emote.transform.Find("container").Find("bg")).GetComponent<UnityEngine.UI.Image>();
		this.emoteBGframe = ((Component)this.emote.transform.Find("container").Find("bgFrame")).GetComponent<UnityEngine.UI.Image>();
		this.emoteTxt = ((Component)this.emote.transform.Find("container").Find("txt")).GetComponent<Text>();
		this.emoteNameTxt = ((Component)this.emote.transform.Find("container").Find("txtName")).GetComponent<Text>();
		this.emoteContainer = this.emote.transform.Find("container");
		this.emote.transform.SetParent(this.game.UI.transform.Find("Emotes"));
		this.talkativeness = 0.7f + UnityEngine.Random.value * 0.6f;
		if (buildFromURL == string.Empty)
		{
			this.data = characterDefinition;
			this.createManifest();
		}
		else
		{
			this.data = new CharacterData();
			this.game.StartCoroutine(RackCharacter.loadCDataFromURL(buildFromURL, this));
		}
	}

	public static IEnumerator loadCDataFromURL(string url, RackCharacter character)
	{
		WWW www = new WWW(url);
		yield return (object)www;
        character.data = CharacterManager.deserializeCharacterData(www.text, url);
        character.createManifest();
        yield break;
    }

	public void autoWalk(float x, float y, float z, float angX, float angY, float angZ, Action onArrival = null, float timeout = 999f)
	{
		this.autoWalking = true;
		this.autoWalkLocation.x = x;
		this.autoWalkLocation.y = y;
		this.autoWalkLocation.z = z;
		this.autoWalkFinishAngle.x = angX;
		this.autoWalkFinishAngle.y = angY;
		this.autoWalkFinishAngle.z = angZ;
		this.autowalkCallback = onArrival;
		this.autowalkTimeout = timeout;
	}

	public float getQuipStaleness(string subOption)
	{
		if (!this.inlineDialogueStaleness.ContainsKey(subOption))
		{
			this.inlineDialogueStaleness.Add(subOption, -1f);
		}
		return this.inlineDialogueStaleness[subOption];
	}

	public void staleQuip(string subOption, float val)
	{
		if (!this.inlineDialogueStaleness.ContainsKey(subOption))
		{
			this.inlineDialogueStaleness.Add(subOption, -1f);
		}
		if (this.inlineDialogueStaleness[subOption] < 0f)
		{
			this.inlineDialogueStaleness[subOption] = 0f;
		}
		Dictionary<string, float> dictionary;
		string key;
		(dictionary = this.inlineDialogueStaleness)[key = subOption] = dictionary[key] + val;
	}

	public int getCommandStatus(string command)
	{
		if (!this.commandStatus.ContainsKey(command))
		{
			this.commandStatus.Add(command, 0);
		}
		return this.commandStatus[command];
	}

	public void setCommandStatus(string command, int val)
	{
		if (!this.commandStatus.ContainsKey(command))
		{
			this.commandStatus.Add(command, val);
		}
		this.commandStatus[command] = val;
		if (val == -1)
		{
			this.timeSinceDisobedienceOrReprimand = 0f;
			if (command == "use_your_mouth")
			{
				this.timeSinceBrokeMouthHoldCommand = 0f;
			}
		}
	}

	public void teleport(float x, float y, float z, float heading = -999f, bool repeatedTeleport = false)
	{
		if (this.initted)
		{
			if (!repeatedTeleport)
			{
				this.resetAllFloppyBodies();
				this.timeSinceTeleport = 0f;
			}
			this.v3 = new Vector3(x, y, z) - this.GO.transform.position;
			this.lastMTPosition += this.v3;
			this.targetLocation += this.v3;
			this.movementTarget.transform.Translate(this.v3);
			Transform transform = this.GO.transform;
			transform.position += this.v3;
			if (this.controlledByPlayer)
			{
				this.game.mainCam.transform.parent.Translate(this.v3);
				Game obj = this.game;
				obj.camPos += this.v3;
				Game obj2 = this.game;
				obj2.camPos_actual += this.v3;
				Game obj3 = this.game;
				obj3.camTarget += this.v3;
				Game obj4 = this.game;
				obj4.camTarget_actual += this.v3;
			}
			if (heading != -999f)
			{
				this.v3 = this.GO.transform.localEulerAngles;
				this.v3.y = heading;
				this.GO.transform.localEulerAngles = this.v3;
			}
			Rigidbody component = ((Component)this.movementTarget).GetComponent<Rigidbody>();
			component.velocity *= 0f;
			Rigidbody component2 = ((Component)this.movementTarget).GetComponent<Rigidbody>();
			component2.angularVelocity *= 0f;
			this.floorFixDelay = 1f;
		}
	}

	public static void createDefaultEmbellishmentColors(CharacterData data)
	{
		data.embellishmentColors = new List<UnityEngine.Color>();
		UnityEngine.Color item = default(UnityEngine.Color);
		item.r = 79f;
		item.g = 65f;
		item.b = 58f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 208f;
		item.g = 193f;
		item.b = 182f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 233f;
		item.g = 199f;
		item.b = 127f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 43f;
		item.g = 41f;
		item.b = 37f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 122f;
		item.g = 109f;
		item.b = 102f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 195f;
		item.g = 195f;
		item.b = 194f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 207f;
		item.g = 95f;
		item.b = 73f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 227f;
		item.g = 172f;
		item.b = 77f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 242f;
		item.g = 230f;
		item.b = 94f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 146f;
		item.g = 242f;
		item.b = 94f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 94f;
		item.g = 209f;
		item.b = 242f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		item.r = 157f;
		item.g = 94f;
		item.b = 242f;
		item.a = 255f;
		data.embellishmentColors.Add(item);
		data.embellishmentColorGradientPoints = new List<EmbellishmentColorGradientPoint>();
		for (int i = 0; i < 12; i++)
		{
			data.embellishmentColorGradientPoints.Add(new EmbellishmentColorGradientPoint());
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].color = data.embellishmentColors[i] * 0.35f;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].embellishmentID = i;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].position = 0.01f;
			data.embellishmentColorGradientPoints.Add(new EmbellishmentColorGradientPoint());
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].color = data.embellishmentColors[i] * 0.6f;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].embellishmentID = i;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].position = 0.4f;
			data.embellishmentColorGradientPoints.Add(new EmbellishmentColorGradientPoint());
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].color = data.embellishmentColors[i];
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].embellishmentID = i;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].position = 0.6f;
			data.embellishmentColorGradientPoints.Add(new EmbellishmentColorGradientPoint());
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].color = UnityEngine.Color.Lerp(data.embellishmentColors[i], UnityEngine.Color.white * 256f, 0.7f);
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].embellishmentID = i;
			data.embellishmentColorGradientPoints[data.embellishmentColorGradientPoints.Count - 1].position = 0.95f;
		}
	}

	private void fetchPreferences()
	{
		this.confidences = new Dictionary<string, int>();
		this.confidencePlayerKnowledge = new Dictionary<string, int>();
		if (this.data.confidences == null)
		{
			this.data.confidences = new List<ConfidenceValue>();
		}
		foreach (string key in SexualPreferences.confidences.Keys)
		{
			bool flag = false;
			int num = 0;
			while (num < this.data.confidences.Count)
			{
				if (!(this.data.confidences[num].attribute == key))
				{
					num++;
					continue;
				}
				flag = true;
				break;
			}
			if (!flag)
			{
				ConfidenceValue confidenceValue = new ConfidenceValue();
				confidenceValue.attribute = key;
				confidenceValue.value = SexualPreferences.confidences[key].defaultValue;
				this.data.confidences.Add(confidenceValue);
			}
		}
		for (int i = 0; i < this.data.confidences.Count; i++)
		{
			if (this.data.confidences[i].value > 0f)
			{
				this.confidences.Add(this.data.confidences[i].attribute, 1);
			}
			else
			{
				this.confidences.Add(this.data.confidences[i].attribute, -1);
			}
			this.confidencePlayerKnowledge.Add(this.data.confidences[i].attribute, 0);
		}
		this.preferences = new Dictionary<string, float>();
		this.preferenceKnowledge = new Dictionary<string, int>();
		this.preferencePlayerKnowledge = new Dictionary<string, int>();
		foreach (string key2 in SexualPreferences.preferences.Keys)
		{
			bool flag2 = false;
			int num2 = 0;
			while (num2 < this.data.preferences.Count)
			{
				if (!(this.data.preferences[num2].preference == key2))
				{
					num2++;
					continue;
				}
				flag2 = true;
				break;
			}
			if (!flag2)
			{
				SexualPreferenceValue sexualPreferenceValue = new SexualPreferenceValue();
				sexualPreferenceValue.preference = key2;
				sexualPreferenceValue.value = SexualPreferences.preferences[key2].defaultValue;
				this.data.preferences.Add(sexualPreferenceValue);
			}
		}
		this.highestPreferenceCategoryValue = 0f;
		for (int j = 0; j < this.data.preferences.Count; j++)
		{
			if (this.data.preferences[j].preference == "category_sensations" || this.data.preferences[j].preference == "category_experiences" || this.data.preferences[j].preference == "category_attraction")
			{
				if (this.data.preferences[j].value > this.highestPreferenceCategoryValue)
				{
					this.highestPreferenceCategoryValue = this.data.preferences[j].value;
				}
				if (this.data.preferences[j].value > this.highestPreferenceCategoryValue)
				{
					this.highestPreferenceCategoryValue = this.data.preferences[j].value;
				}
			}
			this.preferences.Add(this.data.preferences[j].preference, this.data.preferences[j].value);
			this.preferenceKnowledge.Add(this.data.preferences[j].preference, this.data.preferences[j].knowledge);
			this.preferencePlayerKnowledge.Add(this.data.preferences[j].preference, 0);
		}
		this.enjoymentWeight_attraction = this.preferences["category_attraction"] / this.highestPreferenceCategoryValue;
		this.enjoymentWeight_experiences = this.preferences["category_experiences"] / this.highestPreferenceCategoryValue;
		this.enjoymentWeight_sensations = this.preferences["category_sensations"] / this.highestPreferenceCategoryValue;
	}

	private void createManifest()
	{
		if (this.data != null)
		{
			this.fetchPreferences();
			if (this.data.genitalType == 3)
			{
				this.data.ballsType = 0;
			}
			if (this.data.assetBundleName == string.Empty || this.data.assetBundleName == null)
			{
				this.data.assetBundleName = RackCharacter.baseModelFilename;
			}
			if (this.data.embellishmentColors.Count < 12)
			{
				RackCharacter.createDefaultEmbellishmentColors(this.data);
			}
			this.characterPieces = new ManifestPiece[9];
			for (int i = 0; i < this.characterPieces.Length; i++)
			{
				this.characterPieces[i] = new ManifestPiece();
				switch (i)
				{
				case 0:
					this.characterPieces[i].character = "body_universal";
					this.characterPieces[i].reference = "body_universal";
					break;
				case 1:
					if (this.data.headType == string.Empty || this.data.headType == null)
					{
						this.data.headType = "feline";
					}
					this.characterPieces[i].character = "head_" + this.data.headType;
					this.characterPieces[i].reference = "head_" + this.data.headType;
					break;
				case 2:
					if (this.data.tailLength > 1f)
					{
						this.data.tailLength = 1f;
					}
					this.tailLength_act = Mathf.FloorToInt(this.data.tailLength * 9.9f);
					if (this.data.tailFurSizes.Count < 5)
					{
						this.data.tailFurSizes = new List<float>();
						for (int j = 0; j < 5; j++)
						{
							this.data.tailFurSizes.Add(2f - (float)Mathf.Abs(3 - j) * 0.6f);
						}
					}
					this.characterPieces[i].character = "tail_" + this.tailLength_act;
					this.characterPieces[i].reference = "tail_" + this.tailLength_act;
					break;
				case 3:
					switch (this.data.specialHands)
					{
					case "webbed":
						this.characterPieces[i].character = "hands_webbed";
						this.characterPieces[i].reference = "hands_webbed";
						break;
					case "meaty":
						this.characterPieces[i].character = "hands_meaty";
						this.characterPieces[i].reference = "hands_meaty";
						break;
					default:
						switch (this.data.numFingers)
						{
						case 3:
							this.characterPieces[i].character = "hands_threefinger";
							this.characterPieces[i].reference = "hands_threefinger";
							break;
						case 4:
							this.characterPieces[i].character = "hands_fourfinger";
							this.characterPieces[i].reference = "hands_fourfinger";
							break;
						case 5:
							this.characterPieces[i].character = "hands_humanoid";
							this.characterPieces[i].reference = "hands_humanoid";
							break;
						}
						break;
					}
					break;
				case 4:
					this.effectivelyPlantigrade = (this.data.legType == 1);
					if (this.forcePlantigrade)
					{
						this.effectivelyPlantigrade = true;
						this.characterPieces[i].character = "feet_planti_fivetoe";
						this.characterPieces[i].reference = "feet_planti_fivetoe";
					}
					else
					{
						switch (this.data.specialFoot)
						{
						case "hooved":
							this.characterPieces[i].character = "feet_digi_hooved";
							this.characterPieces[i].reference = "feet_digi_hooved";
							break;
						case "slender":
							this.characterPieces[i].character = "feet_digi_fourtoe_slender";
							this.characterPieces[i].reference = "feet_digi_fourtoe_slender";
							break;
						case "meaty_d":
							this.characterPieces[i].character = "feet_digi_fivetoe_meaty";
							this.characterPieces[i].reference = "feet_digi_fivetoe_meaty";
							break;
						case "meaty_p":
							this.characterPieces[i].character = "feet_digi_fivetoe_bear";
							this.characterPieces[i].reference = "feet_digi_fivetoe_bear";
							break;
						case "webbed":
							this.characterPieces[i].character = "feet_digi_webbed";
							this.characterPieces[i].reference = "feet_digi_webbed";
							break;
						default:
						{
							string str = "feet_digi_";
							string str2 = "fourtoe";
							switch (this.data.legType)
							{
							case 0:
								str = "feet_digi_";
								break;
							case 1:
								str = "feet_planti_";
								break;
							}
							switch (this.data.numToes)
							{
							case 3:
								str2 = "threetoe";
								break;
							case 4:
								str2 = "fourtoe";
								break;
							case 5:
								str2 = "fivetoe";
								break;
							}
							this.characterPieces[i].character = str + str2;
							this.characterPieces[i].reference = str + str2;
							break;
						}
						}
					}
					this.footType = this.characterPieces[i].character;
					break;
				case 5:
					switch (this.data.wingType)
					{
					case 0:
						this.characterPieces[i].character = "wings_none";
						this.characterPieces[i].reference = "wings_none";
						break;
					case 1:
						this.characterPieces[i].character = "wings_avian";
						this.characterPieces[i].reference = "wings_avian";
						break;
					case 2:
						this.characterPieces[i].character = "wings_skin";
						this.characterPieces[i].reference = "wings_skin";
						break;
					}
					break;
				case 6:
					this.characterPieces[i].character = "penis_universal";
					this.characterPieces[i].reference = "penis_universal";
					break;
				case 7:
					if (this.data.genitalType == 2)
					{
						this.characterPieces[i].character = "crotch_neuter";
						this.characterPieces[i].reference = "crotch_neuter";
					}
					else if (this.data.ballsType == 2)
					{
						this.characterPieces[i].character = "slit_universal";
						this.characterPieces[i].reference = "slit_universal";
					}
					else
					{
						this.characterPieces[i].character = "balls_universal";
						this.characterPieces[i].reference = "balls_universal";
					}
					break;
				case 8:
					this.characterPieces[i].character = "vagina_universal";
					this.characterPieces[i].reference = "vagina_universal";
					break;
				}
			}
			if (this.data.baseColor.a < 1f)
			{
				this.data.baseColor = UnityEngine.Color.white;
				this.data.baseColor.r = 0.9411765f;
				this.data.baseColor.g = 0.9019608f;
				this.data.baseColor.b = 0.8627451f;
			}
			if (this.data.textureLayers.Count == 0)
			{
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "eyeball";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.996078432f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.996078432f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.996078432f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "iris";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.5372549f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.607843161f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.345098048f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "penis";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.827451f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.6313726f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.5372549f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "innervagina";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.827451f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.6313726f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.5372549f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "tailhole";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.827451f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.6313726f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.5372549f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "gums";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.180392161f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.156862751f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.149019614f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "teeth";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "tongue_0";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.827451f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.6313726f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.5372549f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "nose";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.180392161f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.156862751f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.149019614f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "eyelashes";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.180392161f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.156862751f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.149019614f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "eyebrows_3";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = UnityEngine.Color.white;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.r = 0.180392161f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.g = 0.156862751f;
				this.data.textureLayers[this.data.textureLayers.Count - 1].color.b = 0.149019614f;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int k = 0; k < this.data.headMorphs.Count; k++)
			{
				if (this.data.headMorphs[k].key == "Head Size")
				{
					flag = true;
				}
				if (this.data.headMorphs[k].key == "Eye Size")
				{
					flag2 = true;
				}
				if (this.data.headMorphs[k].key == "Tongue Length")
				{
					flag3 = true;
				}
			}
			if (!flag)
			{
				this.data.headMorphs.Add(new CharacterDataFloatProperty());
				this.data.headMorphs[this.data.headMorphs.Count - 1].key = "Head Size";
				this.data.headMorphs[this.data.headMorphs.Count - 1].val = 0.5f;
			}
			if (!flag2)
			{
				this.data.headMorphs.Add(new CharacterDataFloatProperty());
				this.data.headMorphs[this.data.headMorphs.Count - 1].key = "Eye Size";
				this.data.headMorphs[this.data.headMorphs.Count - 1].val = 0.5f;
			}
			if (!flag3)
			{
				this.data.headMorphs.Add(new CharacterDataFloatProperty());
				this.data.headMorphs[this.data.headMorphs.Count - 1].key = "Tongue Length";
				this.data.headMorphs[this.data.headMorphs.Count - 1].val = 0.25f;
			}
			for (int num = this.data.embellishmentLayers.Count - 1; num >= 0; num--)
			{
				if (!this.data.embellishmentLayers[num].utilityLayer)
				{
					bool flag4 = false;
					int num2 = 0;
					while (num2 < this.characterPieces.Length)
					{
						if (!(this.characterPieces[num2].character == this.data.embellishmentLayers[num].partName))
						{
							num2++;
							continue;
						}
						flag4 = true;
						break;
					}
					if (!flag4)
					{
						this.data.embellishmentLayers.RemoveAt(num);
					}
				}
			}
			this.aggression = this.data.aggression;
			this.dominance = this.data.aggression * UnityEngine.Random.value;
			if (this.data.furType != string.Empty && !RackCharacter.furTypes.Contains(this.data.furType))
			{
				this.data.furType = string.Empty;
			}
			if (this.data.furType == string.Empty)
			{
				if (RackCharacter.furTypes.Contains("velvety"))
				{
					this.data.furType = "velvety";
				}
				else
				{
					this.data.furType = RackCharacter.furTypes[0];
				}
			}
			this.mostRecentMeshModificationTime = 0;
			string fileName = Game.characterAssetDirectory + RackCharacter.baseModelFilename;
			FileInfo fileInfo = new FileInfo(fileName);
			int num3 = (int)RackCharacter.GetUnixEpoch(fileInfo.LastWriteTime);
			if (num3 > this.mostRecentMeshModificationTime)
			{
				this.mostRecentMeshModificationTime = num3;
			}
			this.manifestLoaded = true;
		}
	}

	public static double GetUnixEpoch(DateTime dateTime)
	{
		return (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}

	private void trace(object s)
	{
		Debug.Log(s);
	}

	private void traceHeirarchy(string depth, Transform t)
	{
		Debug.Log(depth + " " + t.name);
		for (int i = 0; i < t.childCount; i++)
		{
			this.traceHeirarchy(depth + " - ", t.GetChild(i));
		}
	}

	public void processRebuilding()
	{
		if (this.queueRebuild && !this.rebuilding)
		{
			this.queueRebuild = false;
			this.startRebuild();
		}
		if (!this.initted)
		{
			if (!this.dying)
			{
				this.processInit();
			}
		}
		else
		{
			if (!this.rebuilding && this.rebuildDelay > 0f)
			{
				this.rebuildDelay -= Time.deltaTime;
				if (this.rebuildDelay <= 0f)
				{
					this.rebuildCharacter();
				}
			}
			this.timeSincePreciseBake += Time.deltaTime;
		}
	}

	public void processInput()
	{
		if (this.controlledByPlayer)
		{
			this.mX = this.game.mX;
			this.mY = this.game.mY;
		}
		else
		{
			this.mX = this.game.mX;
			this.mY = this.game.mY;
		}
		switch (this.controlMode)
		{
		case 0:
		{
			float num = 1f;
			if (this.controlledByPlayer && this.game.currentInteraction != null)
			{
				num -= this.cap((Mathf.Abs(this.game.currentInteraction.pushingThroughResistance) - 0.2f) * (1f - this.game.penetrationPopTime) * 4f, 0f, 1f);
			}
			if (Input.GetMouseButton(0))
			{
				this.interactionStartCenter.x += (this.mX - this.interactionStartCenter.x) * this.cap(Time.deltaTime * 0.8f, 0f, 1f) * num;
				this.interactionStartCenter.y += (this.mY - this.interactionStartCenter.y) * this.cap(Time.deltaTime * 0.8f, 0f, 1f) * num;
			}
			this.interactionStartCenter.x = this.cap(this.interactionStartCenter.x, 0.2f, 0.8f);
			this.interactionStartCenter.y = this.cap(this.interactionStartCenter.y, 0.2f, 0.8f);
			this.interactionMX_raw += (this.cap(0.5f + (this.mX - this.interactionStartCenter.x) * UserSettings.data.interactionSensitivity, 0f, 1f) - this.interactionMX_raw) * this.cap(Time.deltaTime * 25f, 0f, 1f);
			this.interactionMY_raw += (this.cap(0.5f + (this.mY - this.interactionStartCenter.y) * UserSettings.data.interactionSensitivity, 0f, 1f) - this.interactionMY_raw) * this.cap(Time.deltaTime * 25f, 0f, 1f);
			this.interactionMX = Game.smoothLerp(this.interactionMX_raw, 2f);
			this.interactionMY = Game.smoothLerp(this.interactionMY_raw, 2f);
			break;
		}
		case 1:
			this.interactionSpeed += (this.interactionMouseChange.magnitude * UserSettings.data.interactionSensitivity * 3.5f - this.interactionSpeed) * this.cap(Time.deltaTime * UserSettings.data.interactionSensitivity * 1.2f, 0f, 1f);
			this.interactionSpeed = this.cap(this.interactionSpeed, 0f, 25f);
			this.interactionControlTime += Time.deltaTime * (Mathf.Pow(this.interactionSpeed / 35f, 1.5f) * 35f);
			this.interactionMX += (this.mX - this.interactionMX) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.interactionMY += (0.5f + Mathf.Cos(this.interactionControlTime) * this.interactionSpeed / 60f - this.interactionMY) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			break;
		case 2:
			this.interactionSpeed = this.cap((1f - this.mY) * 35f - 5f, 0f, 25f);
			this.interactionControlTime += Time.deltaTime * this.interactionSpeed;
			this.interactionMX += (this.mX - this.interactionMX) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.interactionMY += (0.5f + Mathf.Cos(this.interactionControlTime) * this.interactionSpeed / 60f - this.interactionMY) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			break;
		case 3:
			if (Input.GetMouseButton(0) && !this.game.UIinUse && !this.game.inventoryOpen)
			{
				this.interactionHandleX = this.mX;
				this.interactionHandleY = this.mY;
			}
			else if (this.interactionHandleY > 0.9f)
			{
				this.interactionHandleY = 1f;
			}
			this.interactionStartCenter.x += (this.interactionHandleX - this.interactionStartCenter.x) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.interactionStartCenter.y += (this.interactionHandleY - this.interactionStartCenter.y) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.interactionSpeed = this.cap((1f - this.interactionHandleY) * 35f - 5f, 0f, 25f);
			this.interactionControlTime += Time.deltaTime * this.interactionSpeed;
			this.interactionMX += (this.interactionHandleX - this.interactionMX) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.interactionMY += (0.5f + Mathf.Cos(this.interactionControlTime) * this.interactionSpeed / 60f - this.interactionMY) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			break;
		}
		this.interactionMouseChange.x = (this.interactionMX - this.lastInteractionMousePosition.x) * (2f + UserSettings.data.interactionSensitivity) / Time.deltaTime * 0.5f;
		this.interactionMouseChange.y = (this.interactionMY - this.lastInteractionMousePosition.y) * (2f + UserSettings.data.interactionSensitivity) / Time.deltaTime * 0.5f;
		this.interactionVigor += (this.interactionMouseChange.magnitude - this.interactionVigor) * this.cap(Time.deltaTime * 14f, 0f, 1f);
		this.lastInteractionMousePosition.x = this.interactionMX;
		this.lastInteractionMousePosition.y = this.interactionMY;
	}

	public void processInit()
	{
		this.loadStepsCompleted = 0f;
		if (!this.manifestLoaded)
		{
			this.loadProgressString = "CREATING_MANIFEST";
			return;
		}
		this.loadStepsCompleted += 1f;
		RackCharacter.initAllPieces();
		if (!RackCharacter.allPieceBundle.loaded)
		{
			this.loadProgressString = "LOADING_ASSETS";
			return;
		}
		if (!RackCharacter.allPieceBundle.seamFixesPushedBackToMeshes)
		{
			RackCharacter.allPieceBundle.pushSeamFixesBackToMeshes();
		}
		this.loadStepsCompleted += 1f;
		this.loadStepsCompleted += (float)this.postAssetLoadStep;
		if (!this.gameObjectsBuilt)
		{
			if (this.game.loadTransition != 1f && !this.isPreviewCharacter)
			{
				return;
			}
			switch (this.postAssetLoadStep)
			{
			case 0:
				if (this.loadProgressString == "BUILDING_SKELETON")
				{
					this.createSkeleton();
					this.identifyBones();
					this.prepareBoobs();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "BUILDING_SKELETON";
				break;
			case 1:
				if (this.loadProgressString == "PREPARING_INVERSE_KINEMATICS")
				{
					this.prepareIK();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "PREPARING_INVERSE_KINEMATICS";
				break;
			case 2:
				if (this.loadProgressString == "CREATING_GEOMETRY")
				{
					this.createPieces();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "CREATING_GEOMETRY";
				break;
			case 3:
				if (this.loadProgressString == "SWAPPING_SKELETON")
				{
					this.swapInMasterSkeleton();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "SWAPPING_SKELETON";
				break;
			case 4:
				if (this.loadProgressString == "PREPARING_ANIMATION")
				{
					this.prepareAnimation();
					this.preparePhysics();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "PREPARING_ANIMATION";
				break;
			case 5:
				if (this.loadProgressString == "PREPARING_PHYSICS")
				{
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "PREPARING_PHYSICS";
				break;
			case 6:
				if (this.loadProgressString == "APPLYING_CUSTOMIZATION")
				{
					this.applyCustomization();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "APPLYING_CUSTOMIZATION";
				break;
			case 7:
				if (this.loadProgressString == "BUILDING_TEXTURE")
				{
					this.buildTexture();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "BUILDING_TEXTURE";
				break;
			case 8:
				if (this.loadProgressString == "BUILDING_PRECISE_RAYCASTING_MESHES")
				{
					this.createPreciseRaycastingMesh();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "BUILDING_PRECISE_RAYCASTING_MESHES";
				break;
			case 9:
				if (this.loadProgressString == "CREATING_HAIR")
				{
					this.createHair();
					this.postAssetLoadStep++;
				}
				this.loadProgressString = "CREATING_HAIR";
				break;
			}
			if (this.postAssetLoadStep >= 10)
			{
				this.gameObjectsBuilt = true;
				goto IL_0354;
			}
			return;
		}
		goto IL_0354;
		IL_0354:
		this.loadProgressString = "APPLYING_CLOTHING";
		if (this.controlledByPlayer)
		{
			this.updateClothingBasedOnInventory();
		}
		if (this.waitingToUpdateClothes && this.loadingClothes.Count == 0 && this.game.draggingInventoryItem == string.Empty)
		{
			this.waitingToUpdateClothes = false;
			this.updateClothingBasedOnInventory();
		}
		this.teleport(this.spawnPoint.x, this.spawnPoint.y, this.spawnPoint.z, -999f, false);
		this.loadProgressString = "DRAWING_DECALS";
		if (this.needsRandomDecals)
		{
			this.needsRandomDecals = false;
			RandomCharacterGenerator.drawDecalsOnCharacter(this);
		}
		this.pollPenisGirth(false);
		this.loadProgressString = string.Empty;
		this.initted = true;
		this.applyReferenceModifications();
	}

	public static int headTypeIndex(string hT)
	{
		if (hT != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
			dictionary.Add("canine", 1);
			dictionary.Add("feline", 2);
			dictionary.Add("lizard", 3);
			dictionary.Add("cervine", 4);
			dictionary.Add("rodent", 5);
			dictionary.Add("bird", 6);
			dictionary.Add("equine", 7);
			dictionary.Add("mustelid", 8);
			dictionary.Add("bovine", 9);
			dictionary.Add("human", 10);
			dictionary.Add("shark", 11);
			dictionary.Add("sharkfinned", 12);
			int num = default(int);
			if (dictionary.TryGetValue(hT, out num))
			{
				switch (num)
				{
				case 1:
					return 0;
				case 2:
					return 1;
				case 3:
					return 2;
				case 4:
					return 3;
				case 5:
					return 4;
				case 6:
					return 5;
				case 7:
					return 6;
				case 8:
					return 7;
				case 9:
					return 8;
				case 10:
					return 9;
				case 11:
					return 10;
				case 12:
					return 11;
				}
			}
		}
		return 0;
	}

	public static float getTongueLengthForHeadType(string hT)
	{
		if (hT != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
			dictionary.Add("human", 1);
			dictionary.Add("bovine", 2);
			dictionary.Add("canine", 3);
			dictionary.Add("cervine", 4);
			dictionary.Add("equine", 5);
			dictionary.Add("feline", 6);
			dictionary.Add("lizard", 7);
			dictionary.Add("mustelid", 8);
			dictionary.Add("rodent", 9);
			dictionary.Add("shark", 10);
			dictionary.Add("sharkfinned", 11);
			int num = default(int);
			if (dictionary.TryGetValue(hT, out num))
			{
				switch (num)
				{
				case 1:
					return 0.16f;
				case 2:
					return 0.34f;
				case 3:
					return 0.35f;
				case 4:
					return 0.33f;
				case 5:
					return 0.43f;
				case 6:
					return 0.26f;
				case 7:
					return 0.4f;
				case 8:
					return 0.26f;
				case 9:
					return 0.29f;
				case 10:
					return 0.4f;
				case 11:
					return 0.4f;
				}
			}
		}
		return 0.35f;
	}

	public void drawTexLayer(int canvasID, string layer, Vector4 color, float opacity, bool additive = false, List<TextureLayerMask> masks = null, bool adjustFur = false, float adjustFurAmount = 1f)
	{
		if (opacity == 0f && !adjustFur)
		{
			return;
		}
		if (layer == "fill")
		{
			for (int i = 0; i < this.canvasPixels[canvasID].Length; i++)
			{
				this.canvasPixels[canvasID][i] = color;
			}
		}
		else
		{
			string str = string.Empty;
			switch (canvasID)
			{
			case 0:
				str = "_head";
				break;
			case 1:
				str = "_body";
				break;
			case 2:
				str = "_wings";
				break;
			case 3:
				str = "_head";
				break;
			case 4:
				str = "_body";
				break;
			case 5:
				str = "_wings";
				break;
			}
			bool flag = false;
			if (canvasID >= 3 && !File.Exists(this.characterTextureDirectory + layer.ToLower() + ".png"))
			{
				layer = layer.Replace("FX", string.Empty);
				flag = true;
			}
			int num = 0;
			string text = this.characterTextureDirectory + layer.ToLower() + ".png";
			if (File.Exists(text))
			{
				if (RackCharacter.windowsOS)
				{
					try
					{
						this.image = new Bitmap(text);
					}
					catch (Exception ex)
					{
						Debug.Log("error loading image: " + ex.Message);
						return;
					}
					this.bitmapData = this.image.LockBits(new Rectangle(0, 0, this.image.Width, this.image.Height), ImageLockMode.ReadWrite, this.image.PixelFormat);
					this.bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(this.image.PixelFormat) / 8;
					this.byteCount = this.bitmapData.Stride * this.image.Height;
					this.pixels = new byte[this.byteCount];
					this.ptrFirstPixel = this.bitmapData.Scan0;
					Marshal.Copy(this.ptrFirstPixel, this.pixels, 0, this.pixels.Length);
					this.heightInPixels = this.bitmapData.Height;
					this.widthInBytes = this.bitmapData.Width * this.bytesPerPixel;
					this.threadedImagePixels = new Vector4[this.image.Width * this.image.Height];
					for (int num2 = this.heightInPixels - 1; num2 >= 0; num2--)
					{
						int num3 = num2 * this.bitmapData.Stride;
						for (int j = 0; j < this.widthInBytes; j += this.bytesPerPixel)
						{
							this.threadedImagePixels[num].x = (float)(int)this.pixels[num3 + j] / 255f;
							this.threadedImagePixels[num].y = (float)(int)this.pixels[num3 + j + 1] / 255f;
							this.threadedImagePixels[num].z = (float)(int)this.pixels[num3 + j + 2] / 255f;
							this.threadedImagePixels[num].w = (float)(int)this.pixels[num3 + j + 3] / 255f;
							num++;
						}
					}
					this.image.Dispose();
					this.image = null;
				}
				else
				{
					Texture2D texture2D = new Texture2D(2, 2);
					texture2D.LoadImage(File.ReadAllBytes(text));
					UnityEngine.Color[] array = texture2D.GetPixels();
					this.threadedImagePixels = new Vector4[array.Length];
					for (int k = 0; k < array.Length; k++)
					{
						this.threadedImagePixels[k] = array[k];
					}
					UnityEngine.Object.Destroy(texture2D);
					texture2D = null;
					array = null;
				}
				if (masks != null)
				{
					for (int l = 0; l < masks.Count; l++)
					{
						string text2 = this.characterTextureDirectory + masks[l].texture.ToLower() + str + ".png";
						if (!File.Exists(text2))
						{
							return;
						}
						if (RackCharacter.windowsOS)
						{
							try
							{
								this.image = new Bitmap(text2);
								this.bitmapData = this.image.LockBits(new Rectangle(0, 0, this.image.Width, this.image.Height), ImageLockMode.ReadWrite, this.image.PixelFormat);
								this.bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(this.image.PixelFormat) / 8;
								this.byteCount = this.bitmapData.Stride * this.image.Height;
								this.pixels = new byte[this.byteCount];
								this.ptrFirstPixel = this.bitmapData.Scan0;
								Marshal.Copy(this.ptrFirstPixel, this.pixels, 0, this.pixels.Length);
								this.heightInPixels = this.bitmapData.Height;
								this.widthInBytes = this.bitmapData.Width * this.bytesPerPixel;
								num = 0;
								for (int num4 = this.heightInPixels - 1; num4 >= 0; num4--)
								{
									int num5 = num4 * this.bitmapData.Stride;
									for (int m = 0; m < this.widthInBytes; m += this.bytesPerPixel)
									{
										this.threadedImagePixels[num].w *= (float)(int)this.pixels[num5 + m + 3] / 255f;
										num++;
									}
								}
								this.bitmapData = null;
								this.image.Dispose();
								this.image = null;
								this.pixels = null;
							}
							catch
							{
							}
						}
						else
						{
							Texture2D texture2D2 = new Texture2D(2, 2);
							texture2D2.LoadImage(File.ReadAllBytes(text2));
							UnityEngine.Color[] array2 = texture2D2.GetPixels();
							for (int n = 0; n < array2.Length; n++)
							{
								this.threadedImagePixels[n].w *= array2[n].a;
							}
							UnityEngine.Object.Destroy(texture2D2);
							texture2D2 = null;
							array2 = null;
						}
					}
				}
				if (adjustFur)
				{
					switch (canvasID)
					{
					case 0:
						for (int num7 = 0; num7 < this.canvasPixels[canvasID].Length; num7++)
						{
							if (this.threadedImagePixels[num7].w != 0f)
							{
								this.headFurModifierPixels[num7] += (adjustFurAmount - this.headFurModifierPixels[num7]) * this.threadedImagePixels[num7].w;
							}
						}
						break;
					case 1:
						for (int num8 = 0; num8 < this.canvasPixels[canvasID].Length; num8++)
						{
							if (this.threadedImagePixels[num8].w != 0f)
							{
								this.bodyFurModifierPixels[num8] += (adjustFurAmount - this.bodyFurModifierPixels[num8]) * this.threadedImagePixels[num8].w;
							}
						}
						break;
					case 2:
						for (int num6 = 0; num6 < this.canvasPixels[canvasID].Length; num6++)
						{
							if (this.threadedImagePixels[num6].w != 0f)
							{
								this.wingFurModifierPixels[num6] += (adjustFurAmount - this.wingFurModifierPixels[num6]) * this.threadedImagePixels[num6].w;
							}
						}
						break;
					}
				}
				if (additive)
				{
					if (!flag)
					{
						for (int num9 = 0; num9 < this.canvasPixels[canvasID].Length; num9++)
						{
							if (this.threadedImagePixels[num9].w != 0f && (this.threadedImagePixels[num9].x != 0f || this.threadedImagePixels[num9].y != 0f || this.threadedImagePixels[num9].z != 0f))
							{
								this.tarCol.r = this.threadedImagePixels[num9].x * color.x;
								this.tarCol.g = this.threadedImagePixels[num9].y * color.y;
								this.tarCol.b = this.threadedImagePixels[num9].z * color.z;
								this.tarCol.a = this.threadedImagePixels[num9].w * color.w;
								ref UnityEngine.Color val = ref this.canvasPixels[canvasID][num9];
								val += this.tarCol;
							}
						}
					}
				}
				else
				{
					for (int num10 = 0; num10 < this.canvasPixels[canvasID].Length; num10++)
					{
						if (this.threadedImagePixels[num10].w != 0f)
						{
							if (flag)
							{
								this.tarCol = UnityEngine.Color.black;
							}
							else
							{
								this.tarCol.r = this.threadedImagePixels[num10].x * color.x;
								this.tarCol.g = this.threadedImagePixels[num10].y * color.y;
								this.tarCol.b = this.threadedImagePixels[num10].z * color.z;
								this.tarCol.a = this.threadedImagePixels[num10].w * color.w;
							}
							this.alph = opacity * this.threadedImagePixels[num10].w;
							if (this.alph < 1f)
							{
								this.canvasPixels[canvasID][num10].r += (this.tarCol.r - this.canvasPixels[canvasID][num10].r) * this.alph;
								this.canvasPixels[canvasID][num10].g += (this.tarCol.g - this.canvasPixels[canvasID][num10].g) * this.alph;
								this.canvasPixels[canvasID][num10].b += (this.tarCol.b - this.canvasPixels[canvasID][num10].b) * this.alph;
								this.canvasPixels[canvasID][num10].a = this.cap(this.canvasPixels[canvasID][num10].a + this.alph, 0f, 1f);
							}
							else
							{
								this.canvasPixels[canvasID][num10] = this.tarCol;
							}
						}
					}
				}
			}
		}
	}

	public void paintTexture()
	{
		this.drawTexLayer(0, "fill", this.data.baseColor, 1f, false, null, false, 1f);
		this.drawTexLayer(1, "fill", this.data.baseColor, 1f, false, null, false, 1f);
		this.drawTexLayer(2, "fill", this.data.baseColor, 1f, false, null, false, 1f);
		this.drawTexLayer(3, "fill", RackCharacter.blackV4, 1f, false, null, false, 1f);
		this.drawTexLayer(4, "fill", RackCharacter.blackV4, 1f, false, null, false, 1f);
		this.drawTexLayer(5, "fill", RackCharacter.blackV4, 1f, false, null, false, 1f);
		this.threadedTextureDrawingState = 2;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < this.data.textureLayers.Count; j++)
			{
				if (PatternIcons.getZGroup(this.data.textureLayers[j].texture.Split('_')[0]) == i)
				{
					this.customGlow = false;
					for (int k = 0; k < 6 && ((!this.customGlow && !this.data.textureLayers[j].isDecal) || k <= 2); k++)
					{
						string str = string.Empty;
						switch (k)
						{
						case 0:
							str = "_head";
							break;
						case 1:
							str = "_body";
							break;
						case 2:
							str = "_wings";
							break;
						case 3:
							str = "_headFX";
							break;
						case 4:
							str = "_bodyFX";
							break;
						case 5:
							str = "_wingsFX";
							break;
						}
						this.drawTexLayer(k, this.data.textureLayers[j].texture + str, this.data.textureLayers[j].color, this.data.textureLayers[j].opacity, false, this.data.textureLayers[j].masks, this.data.textureLayers[j].modifyFur, this.data.textureLayers[j].furLength - this.data.furLength);
						if (this.data.textureLayers[j].glow && k < 3)
						{
							this.customGlow = true;
							k += 3;
							this.drawTexLayer(k, this.data.textureLayers[j].texture + str, UnityEngine.Color.green, this.data.textureLayers[j].opacity, false, this.data.textureLayers[j].masks, false, 1f);
							k -= 3;
						}
					}
					this.threadedTextureDrawingState = 2;
				}
			}
		}
		this.threadedTextureDrawingState = 3;
	}

	public void preTexDraw()
	{
		this.characterTextureDirectory = RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + "/";
		this.longestFurLength = this.data.furLength;
		for (int i = 0; i < this.data.textureLayers.Count; i++)
		{
			if (this.data.textureLayers[i].modifyFur && this.data.textureLayers[i].furLength > this.longestFurLength)
			{
				this.longestFurLength = this.data.textureLayers[i].furLength;
			}
		}
		bool flag = this.data.genitalType == 3;
		bool flag2 = false;
		UnityEngine.Color color = UnityEngine.Color.white;
		int index = -1;
		for (int j = 0; j < this.data.textureLayers.Count; j++)
		{
			if (this.data.textureLayers[j].texture == "hermmask")
			{
				flag2 = true;
				index = j;
				if (!flag)
				{
					this.data.textureLayers.RemoveAt(j);
					j--;
				}
			}
			if (this.data.textureLayers[j].texture == "penis")
			{
				color = this.data.textureLayers[j].color;
			}
			if (this.data.textureLayers[j].texture == "nose")
			{
				bool flag3 = false;
				int index2 = -1;
				int num = 0;
				while (num < this.data.textureLayers[j].masks.Count)
				{
					if (this.data.textureLayers[j].masks[num].texture.IndexOf("nosemask_") == -1)
					{
						num++;
						continue;
					}
					flag3 = true;
					index2 = num;
					break;
				}
				if (!flag3)
				{
					this.data.textureLayers[j].masks.Add(new TextureLayerMask());
					this.data.textureLayers[j].masks[this.data.textureLayers[j].masks.Count - 1].texture = "nosemask_" + RackCharacter.headTypeIndex(this.data.headType);
				}
				else
				{
					this.data.textureLayers[j].masks[index2].texture = "nosemask_" + RackCharacter.headTypeIndex(this.data.headType);
				}
			}
			if (this.data.textureLayers[j].texture == "penis")
			{
				bool flag4 = this.data.ballsType == 0 && this.data.genitalType != 3;
				bool flag5 = false;
				int num2 = 0;
				while (num2 < this.data.textureLayers[j].masks.Count)
				{
					if (this.data.textureLayers[j].masks[num2].texture.IndexOf("penismask") == -1)
					{
						num2++;
						continue;
					}
					flag5 = true;
					if (flag4)
					{
						break;
					}
					this.data.textureLayers[j].masks.RemoveAt(num2);
					break;
				}
				if (flag4 && !flag5)
				{
					this.data.textureLayers[j].masks.Add(new TextureLayerMask());
					this.data.textureLayers[j].masks[this.data.textureLayers[j].masks.Count - 1].texture = "penismask";
				}
			}
		}
		if (flag)
		{
			if (!flag2)
			{
				this.data.textureLayers.Add(new TextureLayer());
				this.data.textureLayers[this.data.textureLayers.Count - 1].texture = "hermmask";
				this.data.textureLayers[this.data.textureLayers.Count - 1].color = color;
				this.data.textureLayers[this.data.textureLayers.Count - 1].required = true;
			}
			else
			{
				this.data.textureLayers[index].color = color;
			}
		}
		for (int k = 0; k < this.data.textureLayers.Count; k++)
		{
			for (int l = 0; l < 6; l++)
			{
				string text = string.Empty;
				switch (l)
				{
				case 0:
					text = "_head";
					break;
				case 1:
					text = "_body";
					break;
				case 2:
					text = "_wings";
					break;
				case 3:
					text = "_headFX";
					break;
				case 4:
					text = "_bodyFX";
					break;
				case 5:
					text = "_wingsFX";
					break;
				}
				if (!this.textureExists(this.data.textureLayers[k].texture.ToLower() + text) && File.Exists(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + this.data.textureLayers[k].texture.ToLower() + text + ".png"))
				{
					byte[] array = File.ReadAllBytes(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + this.data.textureLayers[k].texture.ToLower() + text + ".png");
					Texture2D texture2D = new Texture2D(4, 4);
					texture2D.LoadImage(array);
					TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
					byte[] bytes = texture2D.EncodeToPNG();
					File.WriteAllBytes(this.characterTextureDirectory + this.data.textureLayers[k].texture.ToLower() + text + ".png", bytes);
					UnityEngine.Object.Destroy(texture2D);
					RackCharacter.existingTextures.Add(this.data.textureLayers[k].texture.ToLower() + text);
					array = null;
					bytes = null;
				}
				if (this.data.textureLayers[k].masks != null)
				{
					for (int m = 0; m < this.data.textureLayers[k].masks.Count; m++)
					{
						if (!this.textureExists(this.data.textureLayers[k].masks[m].texture.ToLower() + text) && File.Exists(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + this.data.textureLayers[k].masks[m].texture.ToLower() + text + ".png"))
						{
							byte[] array2 = File.ReadAllBytes(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + this.data.textureLayers[k].masks[m].texture.ToLower() + text + ".png");
							Texture2D texture2D2 = new Texture2D(4, 4);
							texture2D2.LoadImage(array2);
							TextureScale.Bilinear(texture2D2, Mathf.RoundToInt((float)texture2D2.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D2.height * UserSettings.data.characterTextureQuality));
							byte[] bytes2 = texture2D2.EncodeToPNG();
							File.WriteAllBytes(this.characterTextureDirectory + this.data.textureLayers[k].masks[m].texture.ToLower() + text + ".png", bytes2);
							UnityEngine.Object.Destroy(texture2D2);
							RackCharacter.existingTextures.Add(this.data.textureLayers[k].masks[m].texture.ToLower() + text);
							array2 = null;
							bytes2 = null;
						}
					}
				}
			}
		}
		if ((UnityEngine.Object)this.headCanvas == (UnityEngine.Object)null)
		{
			this.headCanvas = new Texture2D(Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality));
			this.bodyCanvas = new Texture2D(Mathf.FloorToInt(2048f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(2048f * UserSettings.data.characterTextureQuality));
			this.wingCanvas = new Texture2D(Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality));
			this.headFXCanvas = new Texture2D(Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality));
			this.bodyFXCanvas = new Texture2D(Mathf.FloorToInt(2048f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(2048f * UserSettings.data.characterTextureQuality));
			this.wingFXCanvas = new Texture2D(Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality), Mathf.FloorToInt(1024f * UserSettings.data.characterTextureQuality));
			this.headControlCanvas = new Texture2D(this.headFXCanvas.width, this.headFXCanvas.height);
			this.bodyControlCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
			this.tailControlCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
			this.wingControlCanvas = new Texture2D(this.wingFXCanvas.width, this.wingFXCanvas.height);
			for (int n = 0; n < 6; n++)
			{
				switch (n)
				{
				case 0:
					this.canvasPixels[n] = this.headCanvas.GetPixels();
					break;
				case 1:
					this.canvasPixels[n] = this.bodyCanvas.GetPixels();
					break;
				case 2:
					this.canvasPixels[n] = this.wingCanvas.GetPixels();
					break;
				case 3:
					this.canvasPixels[n] = this.headFXCanvas.GetPixels();
					break;
				case 4:
					this.canvasPixels[n] = this.bodyFXCanvas.GetPixels();
					break;
				case 5:
					this.canvasPixels[n] = this.wingFXCanvas.GetPixels();
					break;
				}
			}
		}
		this.headFurModifierPixels = new float[this.headControlCanvas.width * this.headControlCanvas.height];
		for (int num3 = 0; num3 < this.headFurModifierPixels.Length; num3++)
		{
			this.headFurModifierPixels[num3] = 0f;
		}
		this.bodyFurModifierPixels = new float[this.bodyControlCanvas.width * this.bodyControlCanvas.height];
		for (int num4 = 0; num4 < this.bodyFurModifierPixels.Length; num4++)
		{
			this.bodyFurModifierPixels[num4] = 0f;
		}
		this.wingFurModifierPixels = new float[this.wingControlCanvas.width * this.wingControlCanvas.height];
		for (int num5 = 0; num5 < this.wingFurModifierPixels.Length; num5++)
		{
			this.wingFurModifierPixels[num5] = 0f;
		}
		this.haveFurModifiersBeenInitialized = true;
	}

	public void checkForCustomTextures()
	{
		if (!this.alreadyCheckedForCustomTextures)
		{
			if (this.textureSuffixes == null)
			{
				this.textureSuffixes = new List<string>();
				this.textureSuffixes.Add("_body");
				this.textureSuffixes.Add("_head");
				this.textureSuffixes.Add("_wings");
				this.textureSuffixes.Add("_bodyfx");
				this.textureSuffixes.Add("_headfx");
				this.textureSuffixes.Add("_wingsfx");
			}
			if (this.loadedFromExternal != 0 || this.isPreviewCharacter)
			{
				for (int i = 0; i < this.data.textureLayers.Count; i++)
				{
					if ((this.data.textureLayers[i].isDecal || PatternIcons.isCustom(this.data.textureLayers[i].texture.Split('_')[0])) && !this.data.textureLayers[i].texture.Contains("racknet_cache/"))
					{
						for (int j = 0; j < 6; j++)
						{
							this.customTexturesWeNeedToDownload.Add(this.racknetAccountID + this.data.textureLayers[i].texture.Replace("decal_cache/", string.Empty) + this.textureSuffixes[j]);
						}
						this.data.textureLayers[i].texture = "racknet_cache/" + this.racknetAccountID + this.data.textureLayers[i].texture.Replace("decal_cache/", string.Empty);
					}
					for (int k = 0; k < this.data.textureLayers[i].masks.Count; k++)
					{
						if (PatternIcons.isCustom(this.data.textureLayers[i].masks[k].texture) && !this.data.textureLayers[i].masks[k].texture.Contains("racknet_cache/"))
						{
							for (int l = 0; l < 6; l++)
							{
								this.customTexturesWeNeedToDownload.Add(this.racknetAccountID + this.data.textureLayers[i].masks[k].texture.Replace("decal_cache/", string.Empty) + this.textureSuffixes[l]);
							}
							this.data.textureLayers[i].masks[k].texture = "racknet_cache/" + this.racknetAccountID + this.data.textureLayers[i].masks[k].texture.Replace("decal_cache/", string.Empty);
						}
					}
				}
			}
			this.alreadyCheckedForCustomTextures = true;
		}
	}

	public void buildTexture()
	{
		if (this.threadedTextureDrawingState > 0 || this.customTexturesWeNeedToDownload.Count > 0)
		{
			this.queueTextureBuild = true;
		}
		else
		{
			this.checkForCustomTextures();
			if (this.customTexturesWeNeedToDownload.Count > 0)
			{
				this.queueTextureBuild = true;
			}
			else
			{
				this.threadedTextureDrawingState = 1;
				if (this.textureDrawingThread != null)
				{
					this.textureDrawingThread.Abort();
				}
				this.preTexDraw();
				if (RackCharacter.windowsOS)
				{
					this.textureDrawingThread = new Thread(this.paintTexture);
					this.textureDrawingThread.Start();
				}
				else
				{
					this.paintTexture();
				}
			}
		}
	}

	public void splitFXtexIntoSeparateTextures()
	{
		if (!((UnityEngine.Object)this.headFXCanvas == (UnityEngine.Object)null))
		{
			try
			{
				UnityEngine.Object.Destroy(this.headMetalCanvas);
			}
			catch
			{
			}
			this.headMetalCanvas = new Texture2D(this.headFXCanvas.width, this.headFXCanvas.height);
			UnityEngine.Color[] array = this.headFXCanvas.GetPixels();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].a = array[i].r;
				array[i].r *= 0.5f;
				array[i].g = (array[i].b = 0f);
			}
			this.headMetalCanvas.SetPixels(array);
			this.headMetalCanvas.Apply();
			try
			{
				UnityEngine.Object.Destroy(this.bodyMetalCanvas);
			}
			catch
			{
			}
			this.bodyMetalCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
			array = this.bodyFXCanvas.GetPixels();
			for (int j = 0; j < array.Length; j++)
			{
				array[j].a = array[j].r;
				array[j].r = (array[j].g = (array[j].b = 0f));
			}
			this.bodyMetalCanvas.SetPixels(array);
			this.bodyMetalCanvas.Apply();
			try
			{
				UnityEngine.Object.Destroy(this.wingMetalCanvas);
			}
			catch
			{
			}
			this.wingMetalCanvas = new Texture2D(this.wingFXCanvas.width, this.wingFXCanvas.height);
			array = this.wingFXCanvas.GetPixels();
			for (int k = 0; k < array.Length; k++)
			{
				array[k].a = array[k].r;
				array[k].r = (array[k].g = (array[k].b = 0f));
			}
			this.wingMetalCanvas.SetPixels(array);
			this.wingMetalCanvas.Apply();
			try
			{
				UnityEngine.Object.Destroy(this.headEmitCanvas);
			}
			catch
			{
			}
			this.headEmitCanvas = new Texture2D(this.headFXCanvas.width, this.headFXCanvas.height);
			array = this.headFXCanvas.GetPixels();
			UnityEngine.Color[] array2 = this.headCanvas.GetPixels();
			for (int l = 0; l < array.Length; l++)
			{
				array[l].r = array2[l].r * array[l].g;
				array[l].b = array2[l].b * array[l].g;
				array[l].g = array2[l].g * array[l].g;
			}
			this.headEmitCanvas.SetPixels(array);
			this.headEmitCanvas.Apply();
			try
			{
				UnityEngine.Object.Destroy(this.bodyEmitCanvas);
			}
			catch
			{
			}
			this.bodyEmitCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
			array = this.bodyFXCanvas.GetPixels();
			array2 = this.bodyCanvas.GetPixels();
			for (int m = 0; m < array.Length; m++)
			{
				array[m].r = array2[m].r * array[m].g;
				array[m].b = array2[m].b * array[m].g;
				array[m].g = array2[m].g * array[m].g;
			}
			this.bodyEmitCanvas.SetPixels(array);
			this.bodyEmitCanvas.Apply();
			try
			{
				UnityEngine.Object.Destroy(this.wingEmitCanvas);
			}
			catch
			{
			}
			this.wingEmitCanvas = new Texture2D(this.wingFXCanvas.width, this.wingFXCanvas.height);
			array = this.wingFXCanvas.GetPixels();
			array2 = this.wingCanvas.GetPixels();
			for (int n = 0; n < array.Length; n++)
			{
				array[n].r = array2[n].r * array[n].g;
				array[n].b = array2[n].b * array[n].g;
				array[n].g = array2[n].g * array[n].g;
			}
			this.wingEmitCanvas.SetPixels(array);
			this.wingEmitCanvas.Apply();
			if (this.haveFurModifiersBeenInitialized)
			{
				this.defaultFurMapScale = this.data.furLength / this.longestFurLength;
				if (RackCharacter.headFurControlBase.width != this.headFXCanvas.width)
				{
					RackCharacter.headFurControlBase = (Resources.Load("headFUR") as Texture2D);
					RackCharacter.bodyFurControlBase = (Resources.Load("bodyFUR") as Texture2D);
					RackCharacter.wingFurControlBase = (Resources.Load("wingFUR") as Texture2D);
					TextureScale.Bilinear(RackCharacter.headFurControlBase, this.headFXCanvas.width, this.headFXCanvas.height);
					TextureScale.Bilinear(RackCharacter.bodyFurControlBase, this.bodyFXCanvas.width, this.bodyFXCanvas.height);
					TextureScale.Bilinear(RackCharacter.wingFurControlBase, this.wingFXCanvas.width, this.wingFXCanvas.height);
				}
				try
				{
					UnityEngine.Object.Destroy(this.headControlCanvas);
				}
				catch
				{
				}
				this.headControlCanvas = new Texture2D(this.headFXCanvas.width, this.headFXCanvas.height);
				array = this.headFXCanvas.GetPixels();
				array2 = RackCharacter.headFurControlBase.GetPixels();
				for (int num = 0; num < array.Length; num++)
				{
					if (array[num].b > 0f || this.longestFurLength == 0f)
					{
						array[num].r = (array[num].g = (array[num].b = 0f));
					}
					else
					{
						array[num].r = (array[num].g = (array[num].b = array2[num].r * this.defaultFurMapScale + this.headFurModifierPixels[num]));
					}
				}
				this.headControlCanvas.SetPixels(array);
				this.headControlCanvas.Apply();
				try
				{
					UnityEngine.Object.Destroy(this.bodyControlCanvas);
				}
				catch
				{
				}
				try
				{
					UnityEngine.Object.Destroy(this.tailControlCanvas);
				}
				catch
				{
				}
				this.bodyControlCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
				this.tailControlCanvas = new Texture2D(this.bodyFXCanvas.width, this.bodyFXCanvas.height);
				array = this.bodyFXCanvas.GetPixels();
				array2 = RackCharacter.bodyFurControlBase.GetPixels();
				float num2 = 0f;
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					if (array[num3].r > 0f || this.longestFurLength == 0f)
					{
						array[num3].r = (array[num3].g = (array[num3].b = 0f));
					}
					else
					{
						if (this.bodyFurModifierPixels[num3] > num2)
						{
							num2 = this.bodyFurModifierPixels[num3];
						}
						array[num3].r = (array[num3].g = (array[num3].b = array2[num3].r * this.defaultFurMapScale + this.bodyFurModifierPixels[num3]));
					}
				}
				this.bodyControlCanvas.SetPixels(array);
				this.bodyControlCanvas.Apply();
				try
				{
					UnityEngine.Object.Destroy(this.wingControlCanvas);
				}
				catch
				{
				}
				this.wingControlCanvas = new Texture2D(this.wingFXCanvas.width, this.wingFXCanvas.height);
				array = this.wingFXCanvas.GetPixels();
				array2 = RackCharacter.wingFurControlBase.GetPixels();
				for (int num4 = 0; num4 < array.Length; num4++)
				{
					if (array[num4].b > 0f || this.longestFurLength == 0f)
					{
						array[num4].r = (array[num4].g = (array[num4].b = 0f));
					}
					else
					{
						array[num4].r = (array[num4].g = (array[num4].b = array2[num4].r * this.defaultFurMapScale + this.wingFurModifierPixels[num4]));
					}
				}
				this.wingControlCanvas.SetPixels(array);
				this.wingControlCanvas.Apply();
			}
		}
	}

	public void applyTexture()
	{
		if (!((UnityEngine.Object)this.headFXCanvas == (UnityEngine.Object)null))
		{
			this.splitFXtexIntoSeparateTextures();
			int num = 0;
			while (num < this.parts.Count)
			{
				Material material = new Material(this.game.shader);
				material.CopyPropertiesFromMaterial(this.game.defaultMaterial);
				if (num == this.wingPieceIndex)
				{
					material.SetTexture("_MainTex", this.wingCanvas);
					material.SetTexture("_SkinTex", this.wingCanvas);
					material.SetTexture("_FurMetallicMap", this.wingMetalCanvas);
					material.SetTexture("_MetallicGlossMap", this.wingMetalCanvas);
					material.SetTexture("_EmissionMap", this.wingEmitCanvas);
					material.SetTexture("_ControlTex", this.wingControlCanvas);
				}
				else if (num == this.headPieceIndex)
				{
					material.SetTexture("_MainTex", this.headCanvas);
					material.SetTexture("_SkinTex", this.headCanvas);
					material.SetTexture("_FurMetallicMap", this.headMetalCanvas);
					material.SetTexture("_MetallicGlossMap", this.headMetalCanvas);
					material.SetTexture("_EmissionMap", this.headEmitCanvas);
					material.SetTexture("_ControlTex", this.headControlCanvas);
				}
				else
				{
					material.SetTexture("_MainTex", this.bodyCanvas);
					material.SetTexture("_SkinTex", this.bodyCanvas);
					material.SetTexture("_FurMetallicMap", this.bodyMetalCanvas);
					material.SetTexture("_MetallicGlossMap", this.bodyMetalCanvas);
					material.SetTexture("_EmissionMap", this.bodyEmitCanvas);
					if (num == this.tailPieceIndex && this.defaultFurMapScale != 1f)
					{
						this.controlPixels = this.bodyControlCanvas.GetPixels();
						for (int i = 0; i < this.controlPixels.Length; i++)
						{
							ref UnityEngine.Color val = ref this.controlPixels[i];
							val /= this.defaultFurMapScale;
						}
						this.tailControlCanvas.SetPixels(this.controlPixels);
						this.tailControlCanvas.Apply();
						material.SetTexture("_ControlTex", this.tailControlCanvas);
					}
					else
					{
						material.SetTexture("_ControlTex", this.bodyControlCanvas);
					}
				}
				RackCharacter.setBlendmode(material, "cutout");
				if (num == this.tailPieceIndex)
				{
					material.SetFloat("_MaxHairLength", 0.15f * this.height_act / this.tailsize_act * this.data.tailFurLength * 3f);
				}
				else
				{
					if (this.clothingPiecesEquipped.Count > 0 && this.controlledByPlayer)
					{
						goto IL_02b3;
					}
					if (this.clothingPiecesEquipped.Count > 1 && !this.controlledByPlayer)
					{
						goto IL_02b3;
					}
					material.SetFloat("_MaxHairLength", 0.15f * this.longestFurLength * this.height_act);
				}
				goto IL_02e6;
				IL_02e6:
				if (!RackCharacter.skinTypes.Contains(this.data.skinType))
				{
					Debug.Log(this.data.name + " has an skin type that we're missing: " + this.data.skinType);
					if (RackCharacter.skinTypes.Contains("skin"))
					{
						this.data.skinType = "skin";
					}
					else if (RackCharacter.skinTypes.Count > 0)
					{
						this.data.skinType = RackCharacter.skinTypes[0];
					}
				}
				if (num == this.headPieceIndex)
				{
					material.SetFloat("_StrandThickness", 1.15f);
					material.SetTexture("_BumpMap", RackCharacter.skinNormalTextures[this.data.skinType + "_head"]);
				}
				else if (num != this.wingPieceIndex)
				{
					material.SetTexture("_BumpMap", RackCharacter.skinNormalTextures[this.data.skinType + "_body"]);
				}
				material.SetTexture("_NoiseTex", RackCharacter.furNoiseTextures[this.data.furType]);
				material.SetFloat("_EdgeFade", 0.2f);
				this.parts[num].GetComponent<Renderer>().material = material;
				if ((UnityEngine.Object)this.parts[num].GetComponent<ImperialFurPhysics>() == (UnityEngine.Object)null)
				{
					this.parts[num].AddComponent<ImperialFurPhysics>();
					this.parts[num].GetComponent<ImperialFurPhysics>().useRigidbody = false;
					this.parts[num].GetComponent<ImperialFurPhysics>().rack2CharacterHack = true;
					this.parts[num].GetComponent<ImperialFurPhysics>().usePhysicsGravity = false;
					this.parts[num].GetComponent<ImperialFurPhysics>().physicsEnabled = false;
				}
				if (num == this.tailPieceIndex)
				{
					this.v3.x = (this.data.tailLift - 0.5f) * -0.5f;
					this.v3.y = 0f;
					this.v3.z = 0.2f;
				}
				else
				{
					this.v3.x = 1f;
					this.v3.y = 0f;
					this.v3.z = 0f;
				}
				this.parts[num].GetComponent<ImperialFurPhysics>().AdditionalGravity = this.v3;
				this.parts[num].GetComponent<ImperialFurPhysics>().UpdateMaterial();
				this.parts[num].GetComponent<ImperialFurPhysics>().UpdatePhysics();
				num++;
				continue;
				IL_02b3:
				material.SetFloat("_MaxHairLength", 0f);
				goto IL_02e6;
			}
			this.needFurLOD = true;
		}
	}

	public void addWetness(string where, float howMuch)
	{
		if (where == "muzzle")
		{
			this.wetPixels = this.headMetalCanvas.GetPixels();
		}
		else
		{
			this.wetPixels = this.bodyMetalCanvas.GetPixels();
		}
		if (where != null)
		{
			if (!(where == "vagina"))
			{
				if (!(where == "penis"))
				{
					if (!(where == "finger0"))
					{
						if (!(where == "finger1"))
						{
							if (!(where == "fist"))
							{
								if (where == "muzzle")
								{
									for (int i = 0; i < RackCharacter.wetnessTex_muzzle.Length; i++)
									{
										this.wetPixels[i].a += RackCharacter.wetnessTex_muzzle[i].r * RackCharacter.wetnessTex_muzzle[i].a * howMuch;
									}
								}
							}
							else
							{
								for (int j = 0; j < RackCharacter.wetnessTex_fist.Length; j++)
								{
									this.wetPixels[j].a += RackCharacter.wetnessTex_fist[j].r * RackCharacter.wetnessTex_fist[j].a * howMuch;
								}
							}
						}
						else
						{
							for (int k = 0; k < RackCharacter.wetnessTex_finger1.Length; k++)
							{
								this.wetPixels[k].a += RackCharacter.wetnessTex_finger1[k].r * RackCharacter.wetnessTex_finger1[k].a * howMuch;
							}
						}
					}
					else
					{
						for (int l = 0; l < RackCharacter.wetnessTex_finger0.Length; l++)
						{
							this.wetPixels[l].a += RackCharacter.wetnessTex_finger0[l].r * RackCharacter.wetnessTex_finger0[l].a * howMuch;
						}
					}
				}
				else
				{
					for (int m = 0; m < RackCharacter.wetnessTex_penis.Length; m++)
					{
						this.wetPixels[m].a += RackCharacter.wetnessTex_penis[m].r * RackCharacter.wetnessTex_penis[m].a * howMuch;
					}
				}
			}
			else
			{
				for (int n = 0; n < RackCharacter.wetnessTex_vagina.Length; n++)
				{
					this.wetPixels[n].a += RackCharacter.wetnessTex_vagina[n].r * RackCharacter.wetnessTex_vagina[n].a * howMuch;
				}
			}
		}
		if (where == "muzzle")
		{
			this.headMetalCanvas.SetPixels(this.wetPixels);
			this.headMetalCanvas.Apply();
			this.parts[this.headPieceIndex].GetComponent<Renderer>().material.SetTexture("_MetallicGlossMap", this.headMetalCanvas);
		}
		else
		{
			this.bodyMetalCanvas.SetPixels(this.wetPixels);
			this.bodyMetalCanvas.Apply();
			this.parts[this.bodyPieceIndex].GetComponent<Renderer>().material.SetTexture("_MetallicGlossMap", this.bodyMetalCanvas);
		}
	}

	public void identifyColliders()
	{
		this.bones.AssholeCollider = ((Component)this.bones.Asshole).GetComponent<Collider>();
		this.bones.Hip_LCollider = ((Component)this.bones.Hip_L).GetComponent<Collider>();
		this.bones.Butt_LCollider = ((Component)this.bones.Butt_L).GetComponent<Collider>();
		this.bones.UpperLeg_LCollider = ((Component)this.bones.UpperLeg_L).GetComponent<Collider>();
		this.bones.LowerLeg_LCollider = ((Component)this.bones.LowerLeg_L).GetComponent<Collider>();
		this.bones.Foot_LCollider = ((Component)this.bones.Foot_L).GetComponent<Collider>();
		this.bones.Footpad_LCollider = ((Component)this.bones.Footpad_L).GetComponent<Collider>();
		this.bones.Hip_RCollider = ((Component)this.bones.Hip_R).GetComponent<Collider>();
		this.bones.Butt_RCollider = ((Component)this.bones.Butt_R).GetComponent<Collider>();
		this.bones.UpperLeg_RCollider = ((Component)this.bones.UpperLeg_R).GetComponent<Collider>();
		this.bones.LowerLeg_RCollider = ((Component)this.bones.LowerLeg_R).GetComponent<Collider>();
		this.bones.Foot_RCollider = ((Component)this.bones.Foot_R).GetComponent<Collider>();
		this.bones.Footpad_RCollider = ((Component)this.bones.Footpad_R).GetComponent<Collider>();
		this.bones.PubicCollider1 = ((Component)this.bones.Pubic).GetComponents<Collider>()[0];
		this.bones.PubicCollider2 = ((Component)this.bones.Pubic).GetComponents<Collider>()[1];
		this.bones.Ballsack0Collider = ((Component)this.bones.Ballsack0).GetComponent<Collider>();
		this.bones.Ballsack1Collider = ((Component)this.bones.Ballsack1).GetComponent<Collider>();
		this.bones.Nut_LCollider = ((Component)this.bones.Nut_L).GetComponent<Collider>();
		this.bones.Nut_RCollider = ((Component)this.bones.Nut_R).GetComponent<Collider>();
		this.bones.Penis0Collider = ((Component)this.bones.Penis0).GetComponent<CapsuleCollider>();
		this.bones.Penis1Collider = ((Component)this.bones.Penis1).GetComponent<CapsuleCollider>();
		this.bones.Penis2Collider = ((Component)this.bones.Penis2).GetComponent<CapsuleCollider>();
		this.bones.Penis3Collider = ((Component)this.bones.Penis3).GetComponent<CapsuleCollider>();
		this.bones.Penis4Collider = ((Component)this.bones.Penis4).GetComponent<CapsuleCollider>();
		this.bones.Penis4Collider2 = ((Component)this.bones.Penis4).GetComponent<SphereCollider>();
		this.bones.BallCatcherCollider = ((Component)this.BallCatcher).GetComponent<Collider>();
		this.bones.SpineLowerCollider1 = ((Component)this.bones.SpineLower).GetComponents<Collider>()[0];
		this.bones.SpineLowerCollider2 = ((Component)this.bones.SpineLower).GetComponents<Collider>()[1];
		this.bones.SpineMiddleCollider = ((Component)this.bones.SpineMiddle).GetComponent<Collider>();
		this.bones.SpineUpperCollider = ((Component)this.bones.SpineUpper).GetComponent<Collider>();
		this.bones.Breast_LCollider = ((Component)this.bones.Breast_L).GetComponent<Collider>();
		this.bones.Breast_RCollider = ((Component)this.bones.Breast_R).GetComponent<Collider>();
		this.bones.NeckCollider = ((Component)this.bones.Neck).GetComponent<Collider>();
		this.bones.HeadCollider1 = ((Component)this.bones.Head).GetComponents<Collider>()[0];
		this.bones.HeadCollider2 = ((Component)this.bones.Head).GetComponents<Collider>()[1];
		this.bones.HeadCollider3 = ((Component)this.bones.Head).GetComponents<Collider>()[2];
		this.bones.HeadCollider4 = ((Component)this.bones.Head).GetComponents<Collider>()[3];
		this.bones.Ear1_LCollider = ((Component)this.bones.Ear1_L).GetComponent<Collider>();
		this.bones.Ear2_LCollider = ((Component)this.bones.Ear2_L).GetComponent<Collider>();
		this.bones.Ear4_LCollider = ((Component)this.bones.Ear4_L).GetComponent<Collider>();
		this.bones.Ear1_RCollider = ((Component)this.bones.Ear1_R).GetComponent<Collider>();
		this.bones.Ear2_RCollider = ((Component)this.bones.Ear2_R).GetComponent<Collider>();
		this.bones.Ear4_RCollider = ((Component)this.bones.Ear4_R).GetComponent<Collider>();
		this.bones.Shoulder_LCollider = ((Component)this.bones.Shoulder_L).GetComponent<Collider>();
		this.bones.UpperArm_LCollider = ((Component)this.bones.UpperArm_L).GetComponent<Collider>();
		this.bones.LowerArmLCollider0 = ((Component)this.bones.LowerArm_L).GetComponents<Collider>()[0];
		this.bones.LowerArmLCollider1 = ((Component)this.bones.LowerArm_L).GetComponents<Collider>()[1];
		this.bones.Hand_LCollider = ((Component)this.bones.Hand_L).GetComponent<Collider>();
		this.bones.Finger02_LCollider = ((Component)this.bones.Finger02_L).GetComponent<Collider>();
		this.bones.Finger30_LCollider = ((Component)this.bones.Finger30_L).GetComponent<Collider>();
		this.bones.Thumb0_LCollider = ((Component)this.bones.Thumb0_L).GetComponent<Collider>();
		this.bones.Thumb2_LCollider = ((Component)this.bones.Thumb2_L).GetComponent<Collider>();
		this.bones.Shoulder_RCollider = ((Component)this.bones.Shoulder_R).GetComponent<Collider>();
		this.bones.UpperArm_RCollider = ((Component)this.bones.UpperArm_R).GetComponent<Collider>();
		this.bones.LowerArmRCollider0 = ((Component)this.bones.LowerArm_R).GetComponents<Collider>()[0];
		this.bones.LowerArmRCollider1 = ((Component)this.bones.LowerArm_R).GetComponents<Collider>()[1];
		this.bones.Hand_RCollider = ((Component)this.bones.Hand_R).GetComponent<Collider>();
		this.bones.Finger02_RCollider = ((Component)this.bones.Finger02_R).GetComponent<Collider>();
		this.bones.Finger30_RCollider = ((Component)this.bones.Finger30_R).GetComponent<Collider>();
		this.bones.Thumb0_RCollider = ((Component)this.bones.Thumb0_R).GetComponent<Collider>();
		this.bones.Thumb2_RCollider = ((Component)this.bones.Thumb2_R).GetComponent<Collider>();
		this.bones.Wing3_LCollider = ((Component)this.bones.Wing3_L).GetComponent<Collider>();
		this.bones.Wing4_LCollider = ((Component)this.bones.Wing4_L).GetComponent<Collider>();
		this.bones.Wing3_RCollider = ((Component)this.bones.Wing3_R).GetComponent<Collider>();
		this.bones.Wing4_RCollider = ((Component)this.bones.Wing4_R).GetComponent<Collider>();
	}

	public void preparePhysics()
	{
		this.identifyColliders();
		if ((UnityEngine.Object)this.movementTarget == (UnityEngine.Object)null)
		{
			this.movementTarget = ((Component)this.GO.transform.Find("MovementTarget")).GetComponent<Rigidbody>();
			this.movementTarget.mass = 1f;
			this.movementTarget.drag = 0f;
			this.movementTarget.isKinematic = false;
			((Component)this.GO.transform.Find("MovementTarget")).GetComponent<Collider>().material.staticFriction = 1f;
			((Component)this.GO.transform.Find("MovementTarget")).GetComponent<Collider>().material.dynamicFriction = 1f;
			((Component)this.GO.transform.Find("MovementTarget")).GetComponent<Collider>().material.bounciness = 0f;
		}
		this.rebuildColliders();
		this.makeAllCoreElementsNotCollideWithThemselves();
		for (int i = 0; i < this.colliders.Length; i++)
		{
			if ((UnityEngine.Object)((Component)this.colliders[i]).GetComponent<Rigidbody>() == (UnityEngine.Object)null)
			{
				this.colliders[i].gameObject.AddComponent<Rigidbody>().isKinematic = true;
			}
			if (this.colliders[i].gameObject.layer != 22 && this.colliders[i].gameObject.layer != 23 && (this.colliders[i].gameObject.layer != 21 || this.controlledByPlayer))
			{
				if (this.colliders[i].gameObject.layer == 21)
				{
					UnityEngine.Object.Destroy(this.colliders[i]);
				}
				else
				{
					this.colliders[i].gameObject.layer = 2;
				}
			}
			if (this.colliders[i].name.IndexOf("Wing") != -1)
			{
				this.colliders[i].gameObject.layer = 14;
			}
			this.SmartIgnoreCollision(this.movementTarget.gameObject.GetComponent<Collider>(), this.colliders[i], true);
			string name = this.colliders[i].name;
			if (name != null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(25);
				dictionary.Add("Tail0", 0);
				dictionary.Add("Tail1", 0);
				dictionary.Add("Tail2", 1);
				dictionary.Add("Tail3", 1);
				dictionary.Add("Tail4", 1);
				dictionary.Add("Tail5", 1);
				dictionary.Add("Tail6", 1);
				dictionary.Add("Tail7", 1);
				dictionary.Add("Butt_L", 2);
				dictionary.Add("Butt_R", 2);
				dictionary.Add("PenisClippingPreventer", 3);
				dictionary.Add("Penis0", 4);
				dictionary.Add("Penis1", 4);
				dictionary.Add("Penis2", 4);
				dictionary.Add("Penis3", 4);
				dictionary.Add("Penis4", 4);
				dictionary.Add("Penis5", 4);
				dictionary.Add("Ballsack0", 5);
				dictionary.Add("Ballsack1", 5);
				dictionary.Add("BellyCollider", 6);
				dictionary.Add("BallCatcher", 7);
				dictionary.Add("Breast_L", 8);
				dictionary.Add("Breast_R", 8);
				dictionary.Add("Nut_L", 9);
				dictionary.Add("Nut_R", 9);
				int num = default(int);
				if (dictionary.TryGetValue(name, out num))
				{
					switch (num)
					{
					case 0:
						for (int num3 = 0; num3 < this.colliders.Length; num3++)
						{
							this.SmartIgnoreCollision(this.colliders[i], this.colliders[num3], true);
						}
						break;
					case 1:
						for (int num7 = 0; num7 < this.colliders.Length; num7++)
						{
							if (this.colliders[num7].name.IndexOf("Arm") != -1 || this.colliders[num7].name.IndexOf("Hand") != -1 || this.colliders[num7].name.IndexOf("Finger") != -1 || this.colliders[num7].name.IndexOf("Thumb") != -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[num7], true);
							}
						}
						break;
					case 2:
						for (int m = 0; m < this.colliders.Length; m++)
						{
							this.SmartIgnoreCollision(this.colliders[i], this.colliders[m], true);
						}
						for (int n = 3; n < this.tailbones.Length; n++)
						{
							this.SmartIgnoreCollision(this.colliders[i], this.tailColliders[n], false);
						}
						break;
					case 3:
						for (int num5 = 0; num5 < this.colliders.Length; num5++)
						{
							if (this.colliders[num5].name.IndexOf("Penis0") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[num5], true);
							}
						}
						break;
					case 4:
						for (int k = 0; k < this.colliders.Length; k++)
						{
							if ((this.colliders[k].name.IndexOf("Nut") == -1 || this.colliders[i].name == "Penis0") && this.colliders[k].name.IndexOf("UpperLeg") == -1 && this.colliders[k].name.IndexOf("BellyCollider") == -1 && this.colliders[k].name.IndexOf("Ballsack") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[k], true);
							}
						}
						this.SmartIgnoreCollision(this.colliders[i], this.bones.PubicCollider2, false);
						break;
					case 5:
						for (int num6 = 0; num6 < this.colliders.Length; num6++)
						{
							if (this.colliders[num6].name.IndexOf("Penis") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[num6], true);
							}
						}
						break;
					case 6:
						for (int num4 = 0; num4 < this.colliders.Length; num4++)
						{
							if (this.colliders[num4].name.IndexOf("Penis") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[num4], true);
							}
						}
						break;
					case 7:
						for (int num2 = 0; num2 < this.colliders.Length; num2++)
						{
							if (this.colliders[num2].name.IndexOf("Nut") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[num2], true);
							}
						}
						break;
					case 8:
						for (int l = 0; l < this.colliders.Length; l++)
						{
							if (this.colliders[l].name.IndexOf("Arm") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[l], true);
							}
						}
						break;
					case 9:
						for (int j = 0; j < this.colliders.Length; j++)
						{
							if (this.colliders[j].name.IndexOf("BallCatcher") == -1 && this.colliders[j].name.IndexOf("UpperLeg") == -1 && this.colliders[j].name.IndexOf("Penis") == -1)
							{
								this.SmartIgnoreCollision(this.colliders[i], this.colliders[j], true);
							}
						}
						break;
					}
				}
			}
		}
		this.game.disableWorldAndCharacterPartCollisions();
		this.movementTarget.transform.parent = this.game.World.transform;
		this.rigidbodies = ((Component)this.GO.transform).GetComponentsInChildren<Rigidbody>();
		this.ignoreCollisions(this.bones.Tail0, false, this.bones.UpperArm_L);
		this.ignoreCollisions(this.bones.Tail0, false, this.bones.UpperArm_R);
		this.originalColliderStatus = new bool[this.colliders.Length];
		for (int num8 = 0; num8 < this.colliders.Length; num8++)
		{
			this.originalColliderStatus[num8] = this.colliders[num8].enabled;
		}
	}

	public void addCollisionListener(Transform transform, Func<Collision, bool, bool> callback)
	{
		transform.gameObject.AddComponent<CollisionListener>().callback = callback;
	}

	public void addTriggerListener(Transform transform, Func<Collider, bool, bool> callback)
	{
		transform.gameObject.AddComponent<TriggerListener>().callback = callback;
	}

	public bool ballsCollidedWithSomething(Collision collision, bool firstFrameOfCollision)
	{
		this.ballRetract += (collision.impulse.magnitude * 6f - this.ballRetract) * this.cap(Time.deltaTime * 12f, 0f, 1f);
		this.ballsRetracting = 0.1f;
		return true;
	}

	public void makeAllCoreElementsNotCollideWithThemselves()
	{
		List<Transform> list = new List<Transform>();
		list.Add(this.bones.SpineLower);
		list.Add(this.bones.SpineMiddle);
		list.Add(this.bones.SpineUpper);
		list.Add(this.bones.Neck);
		list.Add(this.bones.Shoulder_R);
		list.Add(this.bones.Shoulder_L);
		list.Add(this.bones.UpperArm_R);
		list.Add(this.bones.UpperArm_L);
		list.Add(this.bones.LowerArm_R);
		list.Add(this.bones.LowerArm_L);
		list.Add(this.bones.Hip_R);
		list.Add(this.bones.Hip_L);
		list.Add(this.bones.UpperLeg_R);
		list.Add(this.bones.UpperLeg_L);
		list.Add(this.bones.LowerLeg_R);
		list.Add(this.bones.LowerLeg_L);
		list.Add(this.bones.Butt_R);
		list.Add(this.bones.Butt_L);
		for (int i = 0; i < list.Count - 1; i++)
		{
			for (int j = i + 1; j < list.Count; j++)
			{
				Collider[] components = ((Component)list[i]).GetComponents<Collider>();
				Collider[] components2 = ((Component)list[j]).GetComponents<Collider>();
				for (int k = 0; k < components.Length; k++)
				{
					for (int l = 0; l < components2.Length; l++)
					{
						bool enabled = components[k].enabled;
						bool enabled2 = components2[l].enabled;
						components[k].enabled = true;
						components2[l].enabled = true;
						this.SmartIgnoreCollision(components[k], components2[l], true);
						components[k].enabled = enabled;
						components2[l].enabled = enabled2;
					}
				}
			}
		}
	}

	public void startTimer(string name)
	{
		int num = this.timerNames.IndexOf(name);
		if (num == -1)
		{
			this.timerNames.Add(name);
			this.timerStarted.Add(DateTime.Now);
		}
		else
		{
			this.timerStarted[num] = DateTime.Now;
		}
	}

	public float stopTimer(string name, bool silent = false)
	{
		int num = this.timerNames.IndexOf(name);
		if (num >= 0)
		{
			if (this.verboseProfiling && !silent)
			{
				Debug.Log("'" + this.timerNames[num] + "' time taken: " + (DateTime.Now - this.timerStarted[num]).TotalMilliseconds / 1000.0 + " seconds");
			}
			return (float)(DateTime.Now - this.timerStarted[num]).TotalMilliseconds / 1000f;
		}
		return 0f;
	}

	public void profile(Action func)
	{
		this.profileStart = DateTime.Now;
		func();
		if (this.verboseProfiling)
		{
			Debug.Log("'" + func.Method.Name + "' time taken: " + (DateTime.Now - this.profileStart).TotalMilliseconds / 1000.0 + " seconds");
		}
	}

	public void rebuildCharacter()
	{
		if (this.rebuilding)
		{
			this.queueRebuild = true;
		}
		else
		{
			this.startRebuild();
		}
	}

	public void startRebuild()
	{
		this.rebuilding = true;
		Transform transform = UnityEngine.Object.Instantiate(Game.gameInstance.masterSkeleton.transform.Find("BODY_universal"));
		transform.name = "BODY_universal_replacement";
		this.assimilatePart(transform.gameObject, "body_universal", false);
		transform.name = "BODY_universal";
		this.destroyExistingGeometry();
		this.createManifest();
		this.game.recentThinking = 0.2f;
		this.finishRebuild();
		UnityEngine.Object.Destroy(transform);
	}

	public void createPreciseRaycastingMesh()
	{
		for (int i = 0; i < this.preciseMousePickingCollider.Count; i++)
		{
			try
			{
				UnityEngine.Object.Destroy(this.preciseMousePickingCollider[i].GetComponent<MeshCollider>().sharedMesh);
			}
			catch
			{
			}
		}
		for (int j = 0; j < this.parts.Count; j++)
		{
			this.preciseMousePickingCollider[j].AddComponent<MeshCollider>().sharedMesh = new Mesh();
		}
		this.needFirstFurClick = true;
	}

	public void finishRebuild()
	{
		this.createPieces();
		this.swapInMasterSkeleton();
		this.applyCustomization();
		this.applyTexture();
		this.animator.Rebind();
		this.createPreciseRaycastingMesh();
		this.prepareTail();
		this.createHair();
		if ((UnityEngine.Object)this.furniture != (UnityEngine.Object)null)
		{
			this.animator.SetBool(this.furniturePose, true);
		}
		if (this.needsRandomDecals)
		{
			this.needsRandomDecals = false;
			RandomCharacterGenerator.drawDecalsOnCharacter(this);
			this.buildTexture();
		}
		this.pollPenisGirth(false);
		this.rebuilding = false;
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			this.apparatus.timeAlive = 0f;
		}
		this.applyReferenceModifications();
	}

	public void createHair()
	{
		for (int i = 0; i < this.hairAppendages.Count; i++)
		{
			this.hairAppendages[i].kill();
		}
		this.hairAppendages = new List<Appendage>();
		string text = "anthro/";
		if (this.data.headType == "human")
		{
			text = "human/";
		}
		this.hairAppendages.Add(new Appendage(this, Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "hairstyles" + Game.PathDirectorySeparatorChar + string.Empty + text + this.data.hairstyle + "_" + this.data.hairvariant, this.data.hairstyle + "_" + this.data.hairvariant, this.bones.Head, this.data.hairColor));
		for (int j = 0; j < this.data.hairAddons.Count; j++)
		{
			this.hairAppendages.Add(new Appendage(this, Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "hairstyles" + Game.PathDirectorySeparatorChar + string.Empty + text + this.data.hairAddons[j].style + "_" + this.data.hairAddons[j].variant, this.data.hairAddons[j].style + "_" + this.data.hairAddons[j].variant, this.bones.Head, this.data.hairAddons[j].color));
		}
	}

	public void destroyExistingGeometry()
	{
		for (int i = 0; i < this.parts.Count; i++)
		{
			UnityEngine.Object.Destroy(this.parts[i].GetComponent<SkinnedMeshRenderer>().sharedMesh);
			UnityEngine.Object.Destroy(this.parts[i]);
		}
		this.parts = new List<GameObject>();
		for (int j = 0; j < this.preciseMousePickingCollider.Count; j++)
		{
			try
			{
				UnityEngine.Object.Destroy(this.preciseMousePickingCollider[j].GetComponent<MeshCollider>().sharedMesh);
			}
			catch
			{
			}
		}
		this.preciseMousePickingCollider = new List<GameObject>();
		this.originalVertexCounts = new List<int>();
		this.originalverts = new List<Vector3[]>();
	}

	public void createSkeleton()
	{
		this.GO = this.game.createCharacterSkeleton(this);
		UnityEngine.Object.Destroy(this.GO.GetComponent<RackCharacterReference>());
		this.GO.AddComponent<RackCharacterReference>().reference = this;
		this.GO.name = this.data.uid;
		this.GO.GetComponent<CharacterModel>().owner = this;
	}

	public void createPieces()
	{
		RackCharacter.allPieces.SetActive(true);
		this.tailPieceIndex = -1;
		for (int i = 0; i < this.parts.Count; i++)
		{
			UnityEngine.Object.Destroy(this.parts[i].GetComponent<SkinnedMeshRenderer>().sharedMesh);
			UnityEngine.Object.Destroy(this.parts[i]);
		}
		this.parts = new List<GameObject>();
		this.originalVertexCounts = new List<int>();
		this.originalverts = new List<Vector3[]>();
		for (int j = 0; j < this.preciseMousePickingCollider.Count; j++)
		{
			try
			{
				UnityEngine.Object.Destroy(this.preciseMousePickingCollider[j].GetComponent<MeshCollider>().sharedMesh);
			}
			catch
			{
			}
		}
		this.preciseMousePickingCollider = new List<GameObject>();
		for (int k = 0; k < this.characterPieces.Length; k++)
		{
			this.parts.Add(UnityEngine.Object.Instantiate(RackCharacter.allPieces.transform.Find(this.characterPieces[k].character).gameObject));
			this.parts[k].name = this.characterPieces[k].character;
			GameObject gameObject = new GameObject("PreciseMousePickingCollider");
			gameObject.transform.parent = this.parts[k].transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = 12;
			this.preciseMousePickingCollider.Add(gameObject);
			if (this.characterPieces[k].reference.ToLower().IndexOf("head") != -1)
			{
				this.headPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.headPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("wings") != -1)
			{
				this.wingPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.wingPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("body") != -1)
			{
				this.bodyPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.bodyPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("penis") != -1)
			{
				this.penisPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.penisPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("balls") != -1 || this.characterPieces[k].reference.ToLower().IndexOf("slit") != -1 || this.characterPieces[k].reference.ToLower().IndexOf("crotch") != -1)
			{
				this.ballsPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.ballsPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("vagina") != -1)
			{
				this.vaginaPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.vaginaPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("hands") != -1)
			{
				this.handsPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.handsPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("feet") != -1)
			{
				this.feetPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.feetPieceIndex = k;
			}
			if (this.characterPieces[k].reference.ToLower().IndexOf("tail") != -1)
			{
				this.tailPiece = this.parts[k].GetComponent<SkinnedMeshRenderer>();
				this.tailPieceIndex = k;
			}
			Mesh targetMesh = UnityEngine.Object.Instantiate(RackCharacter.getOriginalMeshByName(this.parts[k].name));
			this.parts[k].GetComponent<SkinnedMeshRenderer>().sharedMesh = this.drawEmbellishments(targetMesh, k, this.parts[k].name, true);
			this.parts[k].GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
			this.originalverts.Add(this.parts[k].GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices);
			this.originalVertexCounts.Add(this.parts[k].GetComponent<SkinnedMeshRenderer>().sharedMesh.vertexCount);
		}
		RackCharacter.allPieces.SetActive(false);
		this.oBody = RackCharacter.getOriginalMeshByName(this.bodyPiece.name);
		this.oFeet = RackCharacter.getOriginalMeshByName(this.feetPiece.name);
		this.originalBodyPieceBoneWeights = new BoneWeight[this.parts[this.bodyPieceIndex].GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.Length];
		this.parts[this.bodyPieceIndex].GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.CopyTo(this.originalBodyPieceBoneWeights, 0);
		this.updateBoneWeights(false);
	}

	public static Mesh getOriginalMeshByName(string name)
	{
		return RackCharacter.allPieces.transform.Find(name).gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
	}

	public int getFurTypeIndex()
	{
		return RackCharacter.furTypes.IndexOf(this.data.furType);
	}

	public int getSkinTypeIndex()
	{
		return RackCharacter.skinTypes.IndexOf(this.data.skinType);
	}

	public void determineTailSizeAct()
	{
		this.tailsize_act = 1f + (this.data.tailSize - 0.5f) * 1.9f;
	}

	public void applyTailModifications(Mesh tailMesh)
	{
		this.determineTailSizeAct();
		this.tailFurEmbellishments = new List<EmbellishmentLayer>();
		int num = 11;
		float num2 = 0f;
		if (this.tailLength_act != 0)
		{
			if (this.tailLength_act != this.lastTailLength)
			{
				this.sortedTailVerts = new List<Vector3>();
				this.lastTailLength = this.tailLength_act;
				this.tailRingVerts = new List<List<int>>();
				int num3 = tailMesh.vertices.Length;
				List<Vector3> list = new List<Vector3>();
				for (int i = 0; i < num3; i++)
				{
					list.Add(tailMesh.vertices[i]);
				}
				while (list.Count > 0)
				{
					float num4 = 999f;
					int index = -1;
					for (int j = 0; j < list.Count; j++)
					{
						Vector3 vector = list[j];
						float y = vector.y;
						if (y < num4)
						{
							num4 = y;
							index = j;
						}
						if (y > num2)
						{
							num2 = y;
							this.tailTipID = j;
						}
					}
					this.sortedTailVerts.Add(list[index]);
					list.RemoveAt(index);
				}
				this.numTailRings = Mathf.FloorToInt((float)(num3 / num));
				List<int> list2 = new List<int>();
				for (int k = 0; k < this.numTailRings; k++)
				{
					this.tailRingVerts.Add(new List<int>());
					for (int l = 0; l < num; l++)
					{
						for (int m = 0; m < num3; m++)
						{
							if (list2.IndexOf(m) == -1)
							{
								float x = tailMesh.vertices[m].x;
								Vector3 vector2 = this.sortedTailVerts[0];
								if (x == vector2.x)
								{
									float y2 = tailMesh.vertices[m].y;
									Vector3 vector3 = this.sortedTailVerts[0];
									if (y2 == vector3.y)
									{
										float z = tailMesh.vertices[m].z;
										Vector3 vector4 = this.sortedTailVerts[0];
										if (z == vector4.z)
										{
											this.tailRingVerts[k].Add(m);
											list2.Add(m);
											break;
										}
									}
								}
							}
						}
						this.sortedTailVerts.RemoveAt(0);
					}
				}
				this.leftoverVerts = new List<int>();
				for (int n = 0; n < this.sortedTailVerts.Count; n++)
				{
					for (int num5 = 0; num5 < num3; num5++)
					{
						if (list2.IndexOf(num5) == -1)
						{
							float x2 = tailMesh.vertices[num5].x;
							Vector3 vector5 = this.sortedTailVerts[n];
							if (x2 == vector5.x)
							{
								float y3 = tailMesh.vertices[num5].y;
								Vector3 vector6 = this.sortedTailVerts[n];
								if (y3 == vector6.y)
								{
									float z2 = tailMesh.vertices[num5].z;
									Vector3 vector7 = this.sortedTailVerts[n];
									if (z2 == vector7.z)
									{
										this.leftoverVerts.Add(num5);
										list2.Add(num5);
										break;
									}
								}
							}
						}
					}
				}
			}
			Vector3[] vertices = tailMesh.vertices;
			this.tailRingCenters = new List<Vector3>();
			Vector3 a = Vector3.zero;
			for (int num6 = 0; num6 < this.leftoverVerts.Count; num6++)
			{
				a += vertices[this.leftoverVerts[num6]];
			}
			a /= (float)this.leftoverVerts.Count;
			for (int num7 = 0; num7 < this.leftoverVerts.Count; num7++)
			{
				float num8 = vertices[this.leftoverVerts[num7]].x - a.x;
				float num9 = vertices[this.leftoverVerts[num7]].z - a.z;
				this.v3 = vertices[this.leftoverVerts[num7]];
				this.v3.x = a.x + num8 * (1f - this.data.tailTaper * 0.95f);
				this.v3.z = a.z + num9 * (1f - this.data.tailTaper * 0.95f);
				vertices[this.leftoverVerts[num7]] = this.v3;
			}
			for (int num10 = 0; num10 < this.numTailRings; num10++)
			{
				this.v3 = Vector3.zero;
				for (int num11 = 0; num11 < this.tailRingVerts[num10].Count; num11++)
				{
					this.v3 += tailMesh.vertices[this.tailRingVerts[num10][num11]];
				}
				this.v3 /= (float)this.tailRingVerts[num10].Count;
				this.tailRingCenters.Add(this.v3);
				float num12 = ((float)num10 + 1f) / 5f;
				if (num12 > 1f)
				{
					num12 = 1f;
				}
				num12 = 1f - Mathf.Pow(1f - num12, 2f);
				if (num10 > 0)
				{
					float num13 = Mathf.Abs(this.data.tailThickness - 0.5f) * 2f / this.tailsize_act;
					float num14 = (this.data.tailThickness - 0.5f) * 2f / this.tailsize_act;
					if ((double)this.data.tailThickness < 0.5)
					{
						num13 = this.cap(Mathf.Abs(this.data.tailThickness - 0.5f) * 4f, 0f, 1f) / this.tailsize_act;
						num14 = this.cap(Mathf.Abs(this.data.tailThickness - 0.5f) * 4f - 1f, 0f, 1f) / this.tailsize_act;
					}
					if (num14 < 0f)
					{
						num14 *= -0.25f;
					}
					float num15 = 10f;
					for (int num16 = 0; num16 < this.tailRingVerts[num10].Count; num16++)
					{
						if (vertices[this.tailRingVerts[num10][num16]].z < num15)
						{
							num15 = vertices[this.tailRingVerts[num10][num16]].z;
						}
					}
					for (int num17 = 0; num17 < this.tailRingVerts[num10].Count; num17++)
					{
						bool flag = num15 == vertices[this.tailRingVerts[num10][num17]].z;
						float x3 = vertices[this.tailRingVerts[num10][num17]].x;
						Vector3 vector8 = this.tailRingCenters[num10];
						float num18 = x3 - vector8.x;
						float z3 = vertices[this.tailRingVerts[num10][num17]].z;
						Vector3 vector9 = this.tailRingCenters[num10];
						float num19 = z3 - vector9.z;
						ref Vector3 val = ref vertices[this.tailRingVerts[num10][num17]];
						Vector3 vector10 = this.tailRingCenters[num10];
						val.x = vector10.x + num18 * (1f + num13 * 2f) * (1f - Mathf.Pow(1f - (1f - (float)num10 / (float)this.tailRingVerts.Count), 3f) * this.data.tailTaper) * num12;
						float num20 = num14;
						if (num19 > 0f && (double)this.data.tailThickness < 0.5)
						{
							num20 *= 0.3f;
						}
						ref Vector3 val2 = ref vertices[this.tailRingVerts[num10][num17]];
						Vector3 vector11 = this.tailRingCenters[num10];
						val2.z = vector11.z + num19 * (1f + num20 * 2f) * (1f - Mathf.Pow(1f - (1f - (float)num10 / (float)this.tailRingVerts.Count), 3f) * this.data.tailTaper) * num12;
					}
				}
			}
			for (int num21 = 0; num21 < this.leftoverVerts.Count; num21++)
			{
				vertices[this.leftoverVerts[num21]].y += this.data.tailTaper * 0.5f;
			}
			tailMesh.vertices = vertices;
		}
	}

	public float getFurLengthAt(float pos)
	{
		float result = 0f;
		for (int i = 0; i < 4; i++)
		{
			if (pos < (float)i + 0.2f && pos >= (float)i * 0.2f)
			{
				float num = (pos - (float)i * 0.2f) / 0.2f;
				result = this.data.tailFurSizes[i] + (this.data.tailFurSizes[i + 1] - this.data.tailFurSizes[i]) * num;
			}
		}
		return result;
	}

	public Mesh drawEmbellishments(Mesh targetMesh, int refPieceIndex, string pieceName, bool checkForMeshUpdates = false)
	{
		List<BlendShapeDefinition> list = new List<BlendShapeDefinition>();
		for (int i = 0; i < targetMesh.blendShapeCount; i++)
		{
			BlendShapeDefinition blendShapeDefinition = new BlendShapeDefinition();
			blendShapeDefinition.name = targetMesh.GetBlendShapeName(i);
			blendShapeDefinition.verts = new Vector3[targetMesh.vertexCount];
			blendShapeDefinition.normals = new Vector3[targetMesh.vertexCount];
			blendShapeDefinition.tangents = new Vector3[targetMesh.vertexCount];
			targetMesh.GetBlendShapeFrameVertices(i, 0, blendShapeDefinition.verts, blendShapeDefinition.normals, blendShapeDefinition.tangents);
			list.Add(blendShapeDefinition);
		}
		this.newVerts = new List<Vector3>();
		this.newVertReferenceVertIndices = new List<int>();
		this.newNormals = new List<Vector3>();
		this.newTriangles = new List<int>();
		this.newBoneWeights = new List<BoneWeight>();
		this.newUVs = new List<Vector2>();
		int num = targetMesh.vertices.Length;
		bool flag = false;
		int num2 = 0;
		bool flag2 = false;
		bool flag3 = false;
		int num3 = 0;
		if (pieceName.IndexOf("tail") != -1)
		{
			this.applyTailModifications(targetMesh);
		}
		for (int j = 0; j < this.clothingPiecesEquipped.Count; j++)
		{
			GameObject gameObject = new GameObject();
			gameObject.layer = 17;
			gameObject.AddComponent<MeshCollider>();
			gameObject.GetComponent<MeshCollider>().sharedMesh = this.clothingPieceStartPoseMesh[j];
			gameObject.GetComponent<MeshCollider>().enabled = false;
			gameObject.GetComponent<MeshCollider>().enabled = true;
			this.clothingCollisionMeshes.Add(gameObject);
		}
		for (int k = 0; k < this.data.embellishmentLayers.Count; k++)
		{
			bool flag4 = false;
			bool flag5 = false;
			if (this.data.embellishmentLayers[k].size != 0f && !this.data.embellishmentLayers[k].utilityLayer && !this.data.embellishmentLayers[k].hidden && this.data.embellishmentLayers[k].partName.ToLower() == pieceName.ToLower())
			{
				if (this.data.embellishmentLayers[k].meshModifiedTime < this.mostRecentMeshModificationTime)
				{
					flag4 = true;
				}
				if (this.data.embellishmentLayers[k].vertexID >= targetMesh.vertexCount)
				{
					this.data.embellishmentLayers.RemoveAt(k);
					k--;
					this.game.popup("YOU_CANNOT_PLACE_AN_EMBELLISHMENT_ON_AN_EMBELLISHMENT", false, false);
				}
				else
				{
					for (int l = 0; l < RackCharacter.embellishmentMeshes.Count; l++)
					{
						if (RackCharacter.embellishmentMeshes[l].name == this.data.embellishmentLayers[k].embellishment)
						{
							Vector3 one = default(Vector3);
							if (RackCharacter.embellishmentMeshes[l].name.IndexOf("TAILFUR_0") != -1)
							{
								one.x = 0.8f + 0.2f * UnityEngine.Random.value;
								one.y = 0.7f + (0.3f + 2f / (0.7f + Mathf.Pow(this.data.embellishmentLayers[k].size, 2f))) * UnityEngine.Random.value;
								one.z = 0.8f + 0.2f * UnityEngine.Random.value;
								flag5 = true;
							}
							else if (RackCharacter.embellishmentMeshes[l].name.IndexOf("TAILFUR") != -1)
							{
								one.x = 0.8f + 0.2f * UnityEngine.Random.value;
								one.y = 0.7f + (0.3f + 0.7f / (0.7f + Mathf.Pow(this.data.embellishmentLayers[k].size, 2f))) * UnityEngine.Random.value;
								one.z = 0.8f + 0.2f * UnityEngine.Random.value;
								flag5 = true;
							}
							else
							{
								one = Vector3.one;
								flag5 = false;
							}
							flag3 = true;
							if (checkForMeshUpdates && flag4 && !flag && !this.data.embellishmentLayers[k].temporaryLayer)
							{
								if (this.data.embellishmentLayers[k].embellishmentPositionForVertexLookup != Vector3.zero)
								{
									float num4 = 99f;
									for (int m = 0; m < targetMesh.vertices.Length; m++)
									{
										float magnitude = (targetMesh.vertices[m] - this.data.embellishmentLayers[k].embellishmentPositionForVertexLookup).magnitude;
										if (magnitude < num4)
										{
											num4 = magnitude;
											this.data.embellishmentLayers[k].vertexID = m;
										}
									}
									num3++;
									this.data.embellishmentLayers[k].mirrorVertID = -1;
								}
								this.data.embellishmentLayers[k].meshModifiedTime = this.mostRecentMeshModificationTime;
							}
							int num5 = this.data.embellishmentLayers[k].vertexID;
							if (flag)
							{
								num5 = -1;
								if (this.data.embellishmentLayers[k].mirrorVertID == -1)
								{
									num2++;
									for (int n = 0; n < targetMesh.vertices.Length; n++)
									{
										if (targetMesh.vertices[n].x == 0f - targetMesh.vertices[this.data.embellishmentLayers[k].vertexID].x && targetMesh.vertices[n].y == targetMesh.vertices[this.data.embellishmentLayers[k].vertexID].y && targetMesh.vertices[n].z == targetMesh.vertices[this.data.embellishmentLayers[k].vertexID].z)
										{
											num5 = n;
											this.data.embellishmentLayers[k].mirrorVertID = num5;
											flag2 = true;
										}
									}
								}
								else
								{
									num5 = this.data.embellishmentLayers[k].mirrorVertID;
								}
								if (num5 == -1)
								{
									flag = false;
									continue;
								}
							}
							bool flag6 = false;
							if (0 == 0 && this.data.embellishmentLayers[k].embellishment.ToLower().IndexOf("antler") == -1 && this.data.embellishmentLayers[k].embellishment.ToLower().IndexOf("fin") == -1 && this.data.embellishmentLayers[k].embellishment.ToLower().IndexOf("horn") == -1)
							{
								Vector3 vector = targetMesh.vertices[num5] + targetMesh.normals[num5];
								Vector3 a = targetMesh.vertices[num5] - targetMesh.normals[num5] * 0.1f;
								flag6 = Physics.Raycast(vector, a - vector, (a - vector).magnitude, LayerMask.GetMask("ClothingColliders"));
							}
							if (!flag6)
							{
								Vector3[] vertices = RackCharacter.embellishmentMeshes[l].vertices;
								Quaternion rhs = Quaternion.AngleAxis((this.data.embellishmentLayers[k].twist - 0.5f) * 360f, Vector3.forward);
								Quaternion lhs = Quaternion.AngleAxis((this.data.embellishmentLayers[k].bend - 0.5f) * 360f, Vector3.right);
								Quaternion lhs2 = Quaternion.AngleAxis((this.data.embellishmentLayers[k].turn - 0.5f) * 360f, Vector3.forward);
								if (flag)
								{
									rhs = Quaternion.AngleAxis((1f - this.data.embellishmentLayers[k].twist - 0.5f) * 360f, Vector3.forward);
									lhs2 = Quaternion.AngleAxis((1f - this.data.embellishmentLayers[k].turn - 0.5f) * 360f, Vector3.forward);
								}
								Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, targetMesh.normals[num5]);
								bool headTexture = false;
								if (pieceName.IndexOf("head_") != -1)
								{
									headTexture = true;
								}
								Vector2 vector2 = default(Vector2);
								Vector2 vector3 = default(Vector2);
								this.getEmbellishmentColorCoords(out vector2, out vector3, this.data.embellishmentLayers[k].color, headTexture);
								if (checkForMeshUpdates && (this.data.embellishmentLayers[k].embellishmentPositionForVertexLookup == Vector3.zero || flag4) && !flag && !this.data.embellishmentLayers[k].temporaryLayer)
								{
									this.data.embellishmentLayers[k].embellishmentPositionForVertexLookup = targetMesh.vertices[num5];
									flag2 = true;
								}
								Quaternion rotation = quaternion * (lhs2 * (lhs * rhs));
								for (int num6 = 0; num6 < vertices.Length; this.newBoneWeights.Add(targetMesh.boneWeights[num5]), num6++)
								{
									if (flag)
									{
										vertices[num6].x *= -1f;
									}
									if (flag5)
									{
										vertices[num6].x *= one.x;
										vertices[num6].y *= one.y;
										vertices[num6].z *= one.z;
									}
									vertices[num6] = rotation * vertices[num6] * this.data.embellishmentLayers[k].size;
									this.newVerts.Add(vertices[num6] + targetMesh.vertices[num5]);
									this.newVertReferenceVertIndices.Add(num5);
									this.v32 = RackCharacter.embellishmentMeshes[l].normals[num6];
									if (flag)
									{
										this.v32.x *= -1f;
									}
									this.v3 = quaternion * this.v32;
									this.newNormals.Add(this.v3);
									if (RackCharacter.embellishmentMeshes[l].uv[num6].x == 0f && RackCharacter.embellishmentMeshes[l].uv[num6].y == 0f)
									{
										goto IL_0bc4;
									}
									if (this.data.embellishmentLayers[k].color == -1)
									{
										goto IL_0bc4;
									}
									float y = RackCharacter.embellishmentMeshes[l].uv[num6].y;
									Vector2 zero = Vector2.zero;
									zero.x = vector2.x + (vector3.x - vector2.x) * y;
									zero.y = vector2.y + (vector3.y - vector2.y) * y;
									zero.y = 1f - zero.y;
									this.newUVs.Add(zero);
									continue;
									IL_0bc4:
									this.newUVs.Add(targetMesh.uv[num5]);
								}
								for (int num7 = 0; num7 < RackCharacter.embellishmentMeshes[l].triangles.Length; num7 += 3)
								{
									if (flag)
									{
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7 + 2] + num);
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7 + 1] + num);
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7] + num);
									}
									else
									{
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7] + num);
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7 + 1] + num);
										this.newTriangles.Add(RackCharacter.embellishmentMeshes[l].triangles[num7 + 2] + num);
									}
								}
								num += RackCharacter.embellishmentMeshes[l].vertices.Length;
							}
							if (!flag && this.data.embellishmentLayers[k].mirror)
							{
								flag = true;
								l--;
							}
							else
							{
								flag = false;
								l = RackCharacter.embellishmentMeshes.Count;
							}
						}
					}
				}
			}
		}
		for (int num8 = 0; num8 < this.clothingCollisionMeshes.Count; num8++)
		{
			UnityEngine.Object.Destroy(this.clothingCollisionMeshes[num8]);
		}
		this.clothingCollisionMeshes = new List<GameObject>();
		for (int num9 = this.data.embellishmentLayers.Count - 1; num9 >= 0; num9--)
		{
			if (this.data.embellishmentLayers[num9].temporaryLayer)
			{
				this.data.embellishmentLayers.RemoveAt(num9);
			}
		}
		if (flag2)
		{
			this.hadOutdatedEmbellishments = true;
			this.saveMe(false);
		}
		if (!flag3)
		{
			return targetMesh;
		}
		Vector3[] array = new Vector3[targetMesh.normals.Length + this.newNormals.ToArray().Length];
		targetMesh.normals.CopyTo(array, 0);
		this.newNormals.ToArray().CopyTo(array, targetMesh.normals.Length);
		BoneWeight[] array2 = new BoneWeight[0];
		array2 = new BoneWeight[targetMesh.boneWeights.Length + this.newBoneWeights.ToArray().Length];
		targetMesh.boneWeights.CopyTo(array2, 0);
		this.newBoneWeights.ToArray().CopyTo(array2, targetMesh.boneWeights.Length);
		Vector2[] array3 = new Vector2[targetMesh.uv.Length + this.newUVs.ToArray().Length];
		targetMesh.uv.CopyTo(array3, 0);
		this.newUVs.ToArray().CopyTo(array3, targetMesh.uv.Length);
		Vector3[] array4 = new Vector3[targetMesh.vertices.Length + this.newVerts.ToArray().Length];
		targetMesh.vertices.CopyTo(array4, 0);
		this.newVerts.ToArray().CopyTo(array4, targetMesh.vertices.Length);
		targetMesh.vertices = array4;
		int[] array5 = new int[targetMesh.triangles.Length + this.newTriangles.ToArray().Length];
		targetMesh.triangles.CopyTo(array5, 0);
		this.newTriangles.ToArray().CopyTo(array5, targetMesh.triangles.Length);
		targetMesh.triangles = array5;
		targetMesh.normals = array;
		targetMesh.uv = array3;
		targetMesh.boneWeights = array2;
		targetMesh.ClearBlendShapes();
		for (int num10 = 0; num10 < list.Count; num10++)
		{
			this.newBlendshapeVerts = new Vector3[this.newVerts.Count];
			this.newBlendshapeNormals = new Vector3[this.newVerts.Count];
			this.newBlendshapeTangents = new Vector3[this.newVerts.Count];
			for (int num11 = 0; num11 < this.newBlendshapeVerts.Length; num11++)
			{
				this.newBlendshapeVerts[num11] = list[num10].verts[this.newVertReferenceVertIndices[num11]];
			}
			list[num10].verts = list[num10].verts.Concat(this.newBlendshapeVerts).ToArray();
			list[num10].normals = list[num10].normals.Concat(this.newBlendshapeNormals).ToArray();
			list[num10].tangents = list[num10].tangents.Concat(this.newBlendshapeTangents).ToArray();
			targetMesh.AddBlendShapeFrame(list[num10].name, 100f, list[num10].verts, list[num10].normals, list[num10].tangents);
		}
		this.newVerts = null;
		this.newVertReferenceVertIndices = null;
		this.newNormals = null;
		this.newTriangles = null;
		this.newBoneWeights = null;
		this.newUVs = null;
		this.newBlendshapeVerts = null;
		this.newBlendshapeNormals = null;
		this.newBlendshapeTangents = null;
		return targetMesh;
	}

	public void saveMe(bool forceSave = false)
	{
		if (this.controlledByPlayer)
		{
			CharacterManager.saveCharacterData();
		}
		else if ((UnityEngine.Object)this.npcData != (UnityEngine.Object)null)
		{
			CharacterManager.exportCharacter(this, "NPC." + Inventory.data.characterName + "." + this.npcData.handle);
		}
		else if (forceSave)
		{
			CharacterManager.exportCharacter(this, this.data.uid);
		}
	}

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		Vector3 point2 = point - pivot;
		point2 = Quaternion.Euler(angles) * point2;
		point = point2 + pivot;
		return point;
	}

	public void assignNormalMaterial()
	{
	}

	public void assignGhostMaterial()
	{
	}

	public void swapInMasterSkeleton()
	{
		if (RackCharacter.originalEyeLPosition.x == 0f)
		{
			RackCharacter.originalEyeLPosition = this.bones.Eye_L.transform.localPosition;
			RackCharacter.originalEyeRPosition = this.bones.Eye_R.transform.localPosition;
		}
		for (int i = 0; i < this.parts.Count; i++)
		{
			this.assimilatePart(this.parts[i], string.Empty, false);
		}
		RackCharacter.charactersReboned.Add(this.data.assetBundleName);
		UnityEngine.Object.Destroy(this.GO.transform.Find("BODY_universal").gameObject);
	}

	public void assimilatePart(GameObject assimilatedPart, string referencePiece = "", bool includeBoneWeights = false)
	{
		if (referencePiece == string.Empty)
		{
			referencePiece = "BODY_universal";
		}
		Transform transform = this.GO.transform.Find(referencePiece);
		BoneWeight[] array = new BoneWeight[0];
		if (includeBoneWeights)
		{
			int[] array2 = new int[assimilatedPart.GetComponent<SkinnedMeshRenderer>().bones.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				try
				{
					array2[i] = 0;
					string name = assimilatedPart.GetComponent<SkinnedMeshRenderer>().bones[i].name;
					for (int j = 0; j < transform.gameObject.GetComponent<SkinnedMeshRenderer>().bones.Length; j++)
					{
						try
						{
							if (transform.gameObject.GetComponent<SkinnedMeshRenderer>().bones[j].name == name)
							{
								array2[i] = j;
							}
						}
						catch
						{
							Debug.Log("Error at " + j);
							Debug.Log("Bone: " + transform.gameObject.GetComponent<SkinnedMeshRenderer>().bones[j]);
							Debug.Log("Bone name: " + transform.gameObject.GetComponent<SkinnedMeshRenderer>().bones[j].name);
						}
					}
				}
				catch
				{
					Debug.Log("Bone[" + i + "] of failed SMR: " + assimilatedPart.GetComponent<SkinnedMeshRenderer>().bones[i]);
				}
			}
			array = assimilatedPart.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights;
			for (int k = 0; k < array.Length; k++)
			{
				array[k].boneIndex0 = array2[array[k].boneIndex0];
				array[k].boneIndex1 = array2[array[k].boneIndex1];
				array[k].boneIndex2 = array2[array[k].boneIndex2];
				array[k].boneIndex3 = array2[array[k].boneIndex3];
			}
		}
		assimilatedPart.transform.parent = this.GO.transform;
		assimilatedPart.GetComponent<SkinnedMeshRenderer>().bones = transform.gameObject.GetComponent<SkinnedMeshRenderer>().bones;
		assimilatedPart.GetComponent<SkinnedMeshRenderer>().rootBone = transform.gameObject.GetComponent<SkinnedMeshRenderer>().rootBone;
		if (includeBoneWeights)
		{
			assimilatedPart.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights = array;
		}
		assimilatedPart.GetComponent<SkinnedMeshRenderer>().sharedMesh.bindposes = transform.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.bindposes;
	}

	public void prepareAnimation()
	{
		GameObject gameObject = new GameObject("cameraFocusPoint");
		gameObject.transform.SetParent(this.GO.transform.Find("Armature"));
		gameObject.transform.position = this.bones.SpineUpper.position;
		this.cameraFocusPoint = gameObject.transform;
		this.animator = this.GO.GetComponent<Animator>();
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		this.animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		this.animator.enabled = false;
		this.headRot = this.bones.Head.eulerAngles;
		this.focusPoint = this.bones.Pubic.position;
		for (int i = 0; i < 5; i++)
		{
			this.handClenchL.Add(0f);
			this.handClenchR.Add(0f);
			this.handClenchRootL.Add(0f);
			this.handClenchRootR.Add(0f);
		}
		this.naturalWrithe = UnityEngine.Random.value * 1000f;
		this.prepareClothing();
		this.prepareWings();
		this.prepareTail();
		this.prepareVagina();
		this.preparePenis();
		this.prepareBalls();
		this.prepareButt();
		this.prepareBelly();
		this.prepareEars();
		this.prepareEyes();
		this.prepareFeet();
		this.prepareMouth();
		this.manualAimLeftHandTarget = new GameObject("manualAimLeftHandTarget").transform;
		this.manualAimRightHandTarget = new GameObject("manualAimRightHandTarget").transform;
		this.manualAimLeftHandTarget.SetParent(this.GO.transform);
		this.manualAimRightHandTarget.SetParent(this.GO.transform);
		MeshRenderer[] componentsInChildren = this.GO.GetComponentsInChildren<MeshRenderer>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].reflectionProbeUsage = ReflectionProbeUsage.Simple;
		}
	}

	public void prepareMouth()
	{
		this.originalTongueRotation = new Quaternion[3];
		this.originalTongueRotation[0] = this.bones.Tongue0.localRotation;
		this.originalTongueRotation[1] = this.bones.Tongue1.localRotation;
		this.originalTongueRotation[2] = this.bones.Tongue2.localRotation;
	}

	public void prepareFeet()
	{
		this.originalFootRotations[0] = this.bones.Foot_L.localEulerAngles;
		this.originalFootRotations[1] = this.bones.Foot_R.localEulerAngles;
	}

	public void prepareEyes()
	{
		this.originalUpperEyelidPositionL = this.bones.UpperEyelid_L.localPosition;
		this.originalUpperEyelidPositionR = this.bones.UpperEyelid_R.localPosition;
	}

	public void prepareEars()
	{
		this.lastEarPos = new Vector3[12];
		this.earbones[0] = this.bones.Ear0_L;
		this.earbones[1] = this.bones.Ear0_R;
		this.earbones[2] = this.bones.Ear1_L;
		this.earbones[3] = this.bones.Ear1_R;
		this.earbones[4] = this.bones.Ear2_L;
		this.earbones[5] = this.bones.Ear2_R;
		this.earbones[6] = this.bones.Ear3_L;
		this.earbones[7] = this.bones.Ear3_R;
		this.earbones[8] = this.bones.Ear4_L;
		this.earbones[9] = this.bones.Ear4_R;
		this.earbones[10] = this.bones.Ear5_L;
		this.earbones[11] = this.bones.Ear5_R;
		if (RackCharacter.originalEarPositions == null)
		{
			RackCharacter.originalEarPositions = new Vector3[12];
			for (int i = 0; i < 12; i++)
			{
				RackCharacter.originalEarPositions[i] = this.earbones[i].localPosition;
				RackCharacter.earStartingAngles[i] = this.earbones[i].localEulerAngles;
			}
		}
		if ((UnityEngine.Object)this.originalEarParents[0] != (UnityEngine.Object)null)
		{
			this.reparentEars();
			for (int j = 0; j < 12; j++)
			{
				this.earbones[j].localPosition = RackCharacter.originalEarPositions[j];
				this.earbones[j].localEulerAngles = RackCharacter.earStartingAngles[j];
			}
		}
		for (int k = 0; k < 12; k++)
		{
			this.originalEarParents[k] = this.earbones[k].parent;
		}
		this.unparentEars();
		for (int l = 0; l < 12; l++)
		{
			this.lastEarPos[l] = this.earbones[l].position;
			if ((UnityEngine.Object)this.earbones[l].gameObject.GetComponent<Rigidbody>() == (UnityEngine.Object)null)
			{
				this.earbones[l].gameObject.AddComponent<Rigidbody>();
			}
			this.earRigidbodies[l] = this.earbones[l].gameObject.GetComponent<Rigidbody>();
			this.earColliders[l] = this.earbones[l].gameObject.GetComponent<Collider>();
			this.earRigidbodies[l].isKinematic = false;
			this.earRigidbodies[l].mass = 15f;
			if ((UnityEngine.Object)this.earbones[l].gameObject.GetComponent<ConfigurableJoint>() == (UnityEngine.Object)null)
			{
				this.earbones[l].gameObject.AddComponent<ConfigurableJoint>();
			}
			this.earJoints[l] = this.earbones[l].gameObject.GetComponent<ConfigurableJoint>();
			this.earJoints[l].angularXMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].angularYMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].angularZMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].xMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].yMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].zMotion = ConfigurableJointMotion.Limited;
			this.earJoints[l].projectionMode = JointProjectionMode.PositionAndRotation;
			this.earJoints[l].rotationDriveMode = RotationDriveMode.XYAndZ;
			this.earJoints[l].autoConfigureConnectedAnchor = false;
			this.earJoints[l].autoConfigureConnectedAnchor = true;
			if (l < 4)
			{
				this.earJoints[l].angularYZDrive = this.earTargetDrive;
			}
			if (l < 2)
			{
				this.earJoints[l].connectedBody = ((Component)this.bones.Head).GetComponent<Rigidbody>();
			}
			else
			{
				this.earJoints[l].connectedBody = this.earRigidbodies[l - 2];
			}
			this.earRigidbodies[l].centerOfMass = Vector3.zero;
			this.earRigidbodies[l].inertiaTensor = Vector3.one;
			this.earRigidbodies[l].useGravity = false;
		}
		this.pollEarProperties();
	}

	public void pollEarProperties()
	{
		this.earStiffness = 0.8f;
		this.earForward = 0f;
		this.longEars = false;
		for (int i = 0; i < this.data.headMorphs.Count; i++)
		{
			if (this.data.headMorphs[i].key == "Floppy Ears")
			{
				this.earStiffness = 1f - this.data.headMorphs[i].val;
			}
			if (this.data.headMorphs[i].key == "Centered Ears")
			{
				this.earCenter = this.data.headMorphs[i].val * 0.55f;
			}
			if (this.data.headMorphs[i].key == "Forward-Leaning Ears")
			{
				this.earForward = this.data.headMorphs[i].val - 0.5f;
			}
			if ((this.data.headType == "canine" || this.data.headType == "rodent") && this.data.headMorphs[i].key == "Long Ears")
			{
				this.longEars = (this.data.headMorphs[i].val > 0.5f);
			}
		}
	}

	public void reparentEars()
	{
		if (this.allowEarUnparenting)
		{
			for (int i = 0; i < this.earbones.Length; i++)
			{
				this.earbones[i].SetParent(this.originalEarParents[i]);
			}
		}
	}

	public void unparentEars()
	{
		if (this.allowEarUnparenting && ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null || this.controlledByPlayer))
		{
			for (int i = 0; i < this.earbones.Length; i++)
			{
				this.earbones[i].SetParent(this.game.World.transform);
			}
		}
	}

	public void resetEars()
	{
		this.reparentEars();
		for (int i = 0; i < this.earbones.Length; i++)
		{
			this.earbones[i].localPosition = RackCharacter.originalEarPositions[i];
			this.earbones[i].localEulerAngles = RackCharacter.earStartingAngles[i];
			this.earbones[i].localScale = Vector3.one;
			Rigidbody obj = this.earRigidbodies[i];
			obj.angularVelocity *= 0f;
			Rigidbody obj2 = this.earRigidbodies[i];
			obj2.velocity *= 0f;
			this.lastEarPos[i] = this.earbones[i].position;
		}
		this.unparentEars();
	}

	public void processEars()
	{
		for (int i = 0; i < this.earRigidbodies.Length; i++)
		{
			if (this.earRigidbodies[i].velocity.magnitude > 100f)
			{
				this.resetEars();
			}
		}
		if ((this.earbones[0].position - this.bones.Head.position).magnitude > 15f)
		{
			this.resetEars();
		}
		if (this.game.customizingCharacter && this.controlledByPlayer)
		{
			this.pollEarProperties();
		}
		if (this.earStiffness > 0.95f)
		{
			this.earS1Limit.limit = 0f;
			this.earS2Limit.limit = 0f;
		}
		else
		{
			this.earS1Limit.limit = 0f;
			this.earS2Limit.limit = 3f;
		}
		ref SoftJointLimit val = ref this.earHTLimit;
		float limit = 0f;
		this.earLTLimit.limit = limit;
		val.limit = limit;
		this.earTwistSpring.spring = 0f;
		ref SoftJointLimitSpring val2 = ref this.earTwistSpring;
		limit = 0f;
		this.earSwingSpring.damper = limit;
		val2.damper = limit;
		for (int j = 0; j < 4; j++)
		{
			if (j < 2)
			{
				this.earSwingSpring.spring = 12000f;
			}
			else
			{
				this.earSwingSpring.spring = 40f + Mathf.Pow(this.earStiffness, 4f) * 14000f;
			}
			this.earSwingSpring.damper = this.earSwingSpring.spring * 0.1f;
			this.earJoints[j].angularYZLimitSpring = this.earSwingSpring;
			this.earJoints[j].angularXLimitSpring = this.earTwistSpring;
			this.earJoints[j].angularYLimit = this.earS1Limit;
			this.earJoints[j].angularZLimit = this.earS2Limit;
			this.earJoints[j].lowAngularXLimit = this.earLTLimit;
			this.earJoints[j].highAngularXLimit = this.earHTLimit;
		}
		this.earTargetDrive.maximumForce = 1000f;
		this.earTargetDrive.positionSpring = this.earSwingSpring.spring;
		this.earTargetDrive.positionDamper = this.earSwingSpring.damper;
		if (this.longEars)
		{
			this.earSwingSpring.spring = 1f + Mathf.Pow(this.earStiffness, 7f) * 14000f;
			if (this.earStiffness > 0.95f)
			{
				this.earS1Limit.limit = 0f;
				this.earS2Limit.limit = 0f;
			}
			else
			{
				this.earS1Limit.limit = 0f;
				this.earS2Limit.limit = 3f;
			}
		}
		else
		{
			ref SoftJointLimitSpring val3 = ref this.earTwistSpring;
			limit = 0f;
			this.earSwingSpring.spring = limit;
			val3.spring = limit;
			this.earS1Limit.limit = 0f;
			this.earS2Limit.limit = 0f;
		}
		this.earSwingSpring.damper = this.earSwingSpring.spring * 0.1f;
		for (int k = 4; k < this.earbones.Length; k++)
		{
			this.earJoints[k].angularYZLimitSpring = this.earSwingSpring;
			this.earJoints[k].angularXLimitSpring = this.earTwistSpring;
			this.earJoints[k].angularYLimit = this.earS1Limit;
			this.earJoints[k].angularZLimit = this.earS2Limit;
			this.earJoints[k].lowAngularXLimit = this.earLTLimit;
			this.earJoints[k].highAngularXLimit = this.earHTLimit;
		}
		Vector3 localPosition = this.bones.EyebrowInner_L.transform.localPosition;
		float num = (localPosition.z - -0.245f) / 0.016f;
		Vector3 localEulerAngles = this.bones.EyebrowInner_L.transform.localEulerAngles;
		float num2 = num + (localEulerAngles.z - 309f) / -60f;
		Vector3 localPosition2 = this.bones.EyebrowInner_R.transform.localPosition;
		float num3 = (localPosition2.z - -0.245f) / 0.016f;
		Vector3 localEulerAngles2 = this.bones.EyebrowInner_R.transform.localEulerAngles;
		float num4 = num3 + (localEulerAngles2.z - 51f) / 60f;
		num2 += this.mouthOpenAmount_act * 0.1f;
		num4 += this.mouthOpenAmount_act * 0.1f;
		if (num4 > num2 + 0.05f)
		{
			num4 += (num4 - num2) * 0.7f;
		}
		if (num2 > num4 + 0.05f)
		{
			num2 += (num2 - num4) * 0.7f;
		}
		num2 += this.talkAnimPhrase2_earL;
		num4 += this.talkAnimPhrase2_earR;
		for (int l = 0; l < 4; l++)
		{
			this.earTargetVec3 = RackCharacter.earStartingAngles[l];
			if (l < 2)
			{
				if (l == 0)
				{
					this.earTargetVec3.x += this.earCenter * 90f;
					this.earTargetVec3.x += num2 * 50f;
				}
				else
				{
					this.earTargetVec3.x -= this.earCenter * 90f;
					this.earTargetVec3.x -= num4 * 50f;
				}
			}
			switch (l)
			{
			case 2:
				this.earTargetVec3.z -= this.earForward * 90f;
				break;
			case 3:
				this.earTargetVec3.z += this.earForward * 90f;
				break;
			}
			this.earTargetQuat = Quaternion.Euler(this.earTargetVec3);
			this.earS1Limit.limit = Mathf.Abs(this.earTargetVec3.x - RackCharacter.earStartingAngles[l].x);
			this.earS2Limit.limit = Mathf.Abs(this.earTargetVec3.z - RackCharacter.earStartingAngles[l].z);
			this.earJoints[l].angularYLimit = this.earS1Limit;
			this.earJoints[l].angularZLimit = this.earS2Limit;
			this.earJoints[l].targetRotation = this.earTargetQuat;
			this.earJoints[l].angularYZDrive = this.earTargetDrive;
		}
		this.earColliders[8].enabled = this.longEars;
		this.earColliders[9].enabled = this.longEars;
		this.v3 = this.lastHeadPosition - this.bones.Head.position;
		for (int m = 0; m < this.earRigidbodies.Length; m++)
		{
			if (this.v3.magnitude < 2f)
			{
				this.earRigidbodies[m].AddForce(this.v3 * 50f, ForceMode.Impulse);
			}
		}
		this.lastHeadPosition = this.bones.Head.position;
		if (this.newHeight)
		{
			this.resetEars();
			for (int n = 0; n < this.earJoints.Length; n++)
			{
				this.earJoints[n].autoConfigureConnectedAnchor = true;
				this.earJoints[n].connectedBody = this.earJoints[n].connectedBody;
			}
		}
	}

	public void resetBelly()
	{
		if (this.initted)
		{
			this.bones.Belly.localPosition = this.bellyOriginalPosition;
			this.bones.Belly.localEulerAngles = this.bellyOriginalRotation;
			for (int i = 0; i < this.bellyRigidbodies.Length; i++)
			{
				Rigidbody obj = this.bellyRigidbodies[i];
				obj.angularVelocity *= 0f;
				Rigidbody obj2 = this.bellyRigidbodies[i];
				obj2.velocity *= 0f;
				this.lastBellyPositions[i] = this.bellyRigidbodies[i].position;
			}
		}
	}

	public void prepareBelly()
	{
		this.bellyRotation = this.bones.Belly.localEulerAngles;
		this.bellyOriginalRotation = this.bellyRotation;
		Vector3 position = this.bones.Belly.position;
		this.lastBellyY = position.y;
		this.bellyOriginalPosition = this.bones.Belly.localPosition;
		this.bellyVelocity = Vector3.zero;
		this.BellyCollider = ((Component)this.bones.Belly.Find("BellyCollider")).GetComponent<SphereCollider>();
		this.LowerSpineCollider = ((Component)this.bones.SpineLower).GetComponents<CapsuleCollider>()[0];
		this.MiddleSpineCollider = ((Component)this.bones.SpineMiddle).GetComponent<CapsuleCollider>();
		this.UpperSpineCollider = ((Component)this.bones.SpineUpper).GetComponent<CapsuleCollider>();
		this.bellyRigidbodies[0] = ((Component)this.bones.Belly).GetComponent<Rigidbody>();
		this.bellyRigidbodies[1] = ((Component)this.bones.Belly.Find("BellyCollider")).GetComponent<Rigidbody>();
		for (int i = 0; i < this.bellyRigidbodies.Length; i++)
		{
			this.bellyRigidbodies[i].centerOfMass = Vector3.zero;
			this.bellyRigidbodies[i].inertiaTensor = Vector3.one;
			this.bellyRigidbodies[i].useGravity = false;
			this.bellyRigidbodies[i].isKinematic = false;
			this.lastBellyPositions[i] = this.bellyRigidbodies[i].position;
			this.bellyJoints[i] = ((Component)this.bellyRigidbodies[i]).GetComponent<ConfigurableJoint>();
		}
		this.v3.x = 0f;
		this.v3.y = 0f;
		this.v3.z = -1.5f;
		this.bellyRigidbodies[0].centerOfMass = this.v3;
		this.bellyJoints[0].connectedBody = ((Component)this.bones.SpineLower).GetComponent<Rigidbody>();
		this.bellyJoints[1].connectedBody = this.bellyRigidbodies[0];
	}

	public void resetBoobs()
	{
		if (this.initted)
		{
			for (int i = 0; i < this.boobbones.Length; i++)
			{
				this.boobbones[i].localPosition = RackCharacter.boobOriginalPositions[i];
				this.boobbones[i].localEulerAngles = RackCharacter.boobOriginalRotations[i];
				this.lastBoobPos[i] = this.boobbones[i].position;
			}
			for (int j = 0; j < this.boobRigidbodies.Length; j++)
			{
				Rigidbody obj = this.boobRigidbodies[j];
				obj.angularVelocity *= 0f;
				Rigidbody obj2 = this.boobRigidbodies[j];
				obj2.velocity *= 0f;
			}
		}
	}

	public void prepareBoobs()
	{
		this.boobbones[0] = this.bones.Breast_L;
		this.boobbones[1] = this.bones.Breast_R;
		for (int i = 0; i < 2; i++)
		{
			this.boobColliders[i] = ((Component)this.boobbones[i]).GetComponent<SphereCollider>();
			this.boobJoints[i] = ((Component)this.boobbones[i]).GetComponent<ConfigurableJoint>();
			this.boobRigidbodies[i] = ((Component)this.boobbones[i]).GetComponent<Rigidbody>();
			this.boobJoints[i + 2] = ((Component)this.boobbones[i].Find("boobWeight")).GetComponent<ConfigurableJoint>();
			this.boobRigidbodies[i + 2] = ((Component)this.boobbones[i].Find("boobWeight")).GetComponent<Rigidbody>();
			((Component)this.boobbones[i]).GetComponent<ConfigurableJoint>().connectedBody = ((Component)this.bones.SpineUpper).GetComponent<Rigidbody>();
			((Component)this.boobbones[i].Find("boobWeight")).GetComponent<ConfigurableJoint>().connectedBody = this.boobRigidbodies[i];
			this.boobRigidbodies[i].centerOfMass = Vector3.zero;
			this.boobRigidbodies[i].inertiaTensor = Vector3.one;
			((Component)this.boobbones[i].Find("boobWeight")).GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
			((Component)this.boobbones[i].Find("boobWeight")).GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
		}
		if (RackCharacter.originalBoobAnchors == null)
		{
			RackCharacter.originalBoobAnchors = new Vector3[2];
			for (int j = 0; j < 2; j++)
			{
				RackCharacter.originalBoobAnchors[j] = this.boobJoints[j].connectedAnchor;
				RackCharacter.boobOriginalPositions[j] = this.boobbones[j].localPosition;
				this.boobOriginalScales[j] = this.boobbones[j].localScale;
				RackCharacter.boobOriginalRotations[j] = this.boobbones[j].localEulerAngles;
			}
		}
		for (int k = 0; k < this.boobRigidbodies.Length; k++)
		{
			this.lastBoobPos[k] = this.boobRigidbodies[k].position;
			this.boobRigidbodies[k].isKinematic = false;
		}
		this.lastUpperSpinePosition = this.bones.SpineUpper.position;
		this.addCollisionListener(this.bones.Breast_L, this.boobLCollidedWithSomething);
		this.addCollisionListener(this.bones.Breast_R, this.boobRCollidedWithSomething);
	}

	public bool boobLCollidedWithSomething(Collision collision, bool firstFrameOfCollision)
	{
		if (collision.collider.name == "UpperArm_L" || collision.collider.name == "LowerArm_L")
		{
			this.boobPushingInSpeed[0] += Time.deltaTime * 15f;
			this.boobPushInFromArm[0] += Time.deltaTime * this.boobPushingInSpeed[0];
			this.boobPushedInFromArm[0] = 0.02f;
		}
		return true;
	}

	public bool boobRCollidedWithSomething(Collision collision, bool firstFrameOfCollision)
	{
		if (collision.collider.name == "UpperArm_R" || collision.collider.name == "LowerArm_R")
		{
			this.boobPushingInSpeed[1] += Time.deltaTime * 15f;
			this.boobPushInFromArm[1] -= Time.deltaTime * this.boobPushingInSpeed[1];
			this.boobPushedInFromArm[1] = 0.02f;
		}
		return true;
	}

	public static int getClothingSlotIDByName(string n)
	{
		if (n != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
			dictionary.Add("HEAD", 0);
			dictionary.Add("FACE", 1);
			dictionary.Add("TORSO", 2);
			dictionary.Add("FOREARMS", 3);
			dictionary.Add("HANDS", 4);
			dictionary.Add("CROTCH", 5);
			dictionary.Add("LEGS", 6);
			dictionary.Add("FEET", 7);
			int num = default(int);
			if (dictionary.TryGetValue(n, out num))
			{
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					return 1;
				case 2:
					return 2;
				case 3:
					return 3;
				case 4:
					return 4;
				case 5:
					return 5;
				case 6:
					return 6;
				case 7:
					return 7;
				}
			}
		}
		return 0;
	}

	public SexToy getCurrentSexToy()
	{
		for (int i = 0; i < this.equippedSexToys.Count; i++)
		{
			if (this.equippedSexToys[i].beingHeld)
			{
				return this.equippedSexToys[i];
			}
		}
		return null;
	}

	public static int getSextoySlotByName(string n)
	{
		if (n.Contains("TOOL"))
		{
			return 11 + int.Parse(n.Replace("HEX_TOOL_", string.Empty).Replace("TOOL_", string.Empty));
		}
		if (n != null)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(22);
			dictionary.Add("BALLS", 0);
			dictionary.Add("HEX_BALLS", 0);
			dictionary.Add("BREASTL", 1);
			dictionary.Add("HEX_BREASTL", 1);
			dictionary.Add("BREASTR", 2);
			dictionary.Add("HEX_BREASTR", 2);
			dictionary.Add("CLIT", 3);
			dictionary.Add("HEX_CLIT", 3);
			dictionary.Add("FOOTL", 4);
			dictionary.Add("HEX_FOOTL", 4);
			dictionary.Add("FOOTR", 5);
			dictionary.Add("HEX_FOOTR", 5);
			dictionary.Add("MOUTH", 6);
			dictionary.Add("HEX_MOUTH", 6);
			dictionary.Add("PENIS", 7);
			dictionary.Add("HEX_PENIS", 7);
			dictionary.Add("SHAFT", 8);
			dictionary.Add("HEX_SHAFT", 8);
			dictionary.Add("TAILHOLE", 9);
			dictionary.Add("HEX_TAILHOLE", 9);
			dictionary.Add("VAGINA", 10);
			dictionary.Add("HEX_VAGINA", 10);
			int num = default(int);
			if (dictionary.TryGetValue(n, out num))
			{
				switch (num)
				{
				case 0:
					return 0;
				case 1:
					return 1;
				case 2:
					return 2;
				case 3:
					return 3;
				case 4:
					return 4;
				case 5:
					return 5;
				case 6:
					return 6;
				case 7:
					return 7;
				case 8:
					return 8;
				case 9:
					return 9;
				case 10:
					return 10;
				}
			}
		}
		return 0;
	}

	public SexToy findSexToyByUID(string uid)
	{
		for (int i = 0; i < this.equippedSexToys.Count; i++)
		{
			if (this.equippedSexToys[i].properties.uid == uid)
			{
				return this.equippedSexToys[i];
			}
		}
		return null;
	}

	public void updateClothingBasedOnInventory()
	{
		if (this.loadingClothes.Count > 0 || this.game.draggingInventoryItem != string.Empty)
		{
			this.waitingToUpdateClothes = true;
		}
		else
		{
			this.breastsCoveredByClothing = false;
			this.crotchCoveredByClothing = false;
			for (int i = 0; i < this.clothingPiecesEquipped.Count; i++)
			{
				UnityEngine.Object.Destroy(this.clothingPiecesEquipped[i].GetComponent<Renderer>().material.GetTexture("_MainTex"));
				UnityEngine.Object.Destroy(this.clothingPiecesEquipped[i].GetComponent<Renderer>().material.GetTexture("_BumpMap"));
				UnityEngine.Object.Destroy(this.clothingPiecesEquipped[i].GetComponent<Renderer>().material.GetTexture("_MetallicGlossMap"));
				UnityEngine.Object.Destroy(this.clothingPiecesEquipped[i].GetComponent<Renderer>().material.GetTexture("_EmissionMap"));
				UnityEngine.Object.Destroy(this.clothingPiecesEquipped[i]);
				UnityEngine.Object.Destroy(this.clothingPieceStartPoseMesh[i]);
			}
			this.clothingRefData = new List<ClothingReferenceData>();
			this.clothingPiecesEquipped = new List<GameObject>();
			this.originalClothingBoneWeights = new List<BoneWeight[]>();
			this.clothingPieceStartPoseMesh = new List<Mesh>();
			this.breastSupportFromClothing = 0f;
			for (int j = 0; j < this.clothingSlots.Length; j++)
			{
				this.clothingSlots[j] = string.Empty;
			}
			for (int k = 0; k < this.sexToySlots.Length; k++)
			{
				this.sexToySlots[k] = string.Empty;
				this.sexToySlotProperties[k] = new LayoutItemSpecialProperties();
			}
			if (this.controlledByPlayer && 1 == 0)
			{
				goto IL_0645;
			}
			Bag bagByName = Inventory.getBagByName("CLOTHING");
			if (!this.controlledByPlayer)
			{
				bagByName = Inventory.getBagByName(this.data.uid);
			}
			if (bagByName != null)
			{
				List<SexToy> list = new List<SexToy>();
				for (int num = bagByName.contents.Count - 1; num >= 0; num--)
				{
					if (Inventory.getItemDefinition(bagByName.contents[num].itemType).category == "SEX_TOYS_AND_TOOLS")
					{
						for (int l = 0; l < bagByName.contents[num].properties.occupyingSlots.Count; l++)
						{
							this.sexToySlots[RackCharacter.getSextoySlotByName(bagByName.contents[num].properties.occupyingSlots[l].ToUpper())] = bagByName.contents[num].itemType;
							this.sexToySlotProperties[RackCharacter.getSextoySlotByName(bagByName.contents[num].properties.occupyingSlots[l].ToUpper())] = bagByName.contents[num].properties;
							if (l == 0)
							{
								SexToy sexToy = new SexToy();
								sexToy.itemID = bagByName.contents[num].itemType;
								sexToy.properties = bagByName.contents[num].properties;
								sexToy.slot = bagByName.contents[num].properties.occupyingSlots[l];
								list.Add(sexToy);
							}
						}
					}
					else if (Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots.Count != 0)
					{
						for (int m = 0; m < Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots.Count; m++)
						{
							int clothingSlotIDByName = RackCharacter.getClothingSlotIDByName(Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots[m].name);
							if (Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots[m].occupies)
							{
								this.clothingSlots[clothingSlotIDByName] = bagByName.contents[num].itemType;
								if (this.breastSupportFromClothing < Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots[m].breastSupport)
								{
									this.breastSupportFromClothing = Inventory.getItemDefinition(bagByName.contents[num].itemType).equipSlots[m].breastSupport;
								}
							}
						}
						if (bagByName.contents[num].itemType != "RackChip")
						{
							this.addClothingPiece(bagByName.contents[num].itemType);
						}
					}
				}
				for (int n = 0; n < SexToySlots.numSlots; n++)
				{
					if (this.sexToySlots[n] == string.Empty)
					{
						if (this.lastSexToySlotUIDs[n] != string.Empty)
						{
							this.killSexToyByUID(this.lastSexToySlotUIDs[n]);
						}
						this.lastSexToySlotUIDs[n] = string.Empty;
					}
					else
					{
						if (this.sexToySlotProperties[n].uid != this.lastSexToySlotUIDs[n] && this.lastSexToySlotUIDs[n] != string.Empty)
						{
							this.killSexToyByUID(this.lastSexToySlotUIDs[n]);
						}
						this.lastSexToySlotUIDs[n] = this.sexToySlotProperties[n].uid;
					}
				}
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					bool flag = false;
					int num3 = 0;
					while (num3 < this.equippedSexToys.Count)
					{
						if (!(list[num2].properties.uid == this.equippedSexToys[num3].properties.uid))
						{
							num3++;
							continue;
						}
						flag = true;
						break;
					}
					if (!flag)
					{
						list[num2].subject = this;
						this.equippedSexToys.Add(list[num2]);
					}
				}
				list = new List<SexToy>();
				goto IL_0645;
			}
		}
		return;
		IL_0645:
		if (this.controlledByPlayer)
		{
			this.game.updateToolHotkeys();
			this.game.updateToolbar();
		}
		for (int num4 = 0; num4 < RackCharacter.clipFixRegionNames.Count; num4++)
		{
			this.clipFixes[RackCharacter.clipFixRegionNames[num4]] = 0f;
		}
		for (int num5 = 0; num5 < this.clothingSlots.Length; num5++)
		{
			if (this.clothingSlots[num5] != string.Empty)
			{
				for (int num6 = 0; num6 < Inventory.getItemDefinition(this.clothingSlots[num5]).clipFixes.Count; num6++)
				{
					this.clipFixes[Inventory.getItemDefinition(this.clothingSlots[num5]).clipFixes[num6]] = 100f;
				}
			}
		}
		this.applyCustomization();
	}

	public void killSexToyByUID(string UID)
	{
		int num = 0;
		while (true)
		{
			if (num < this.equippedSexToys.Count)
			{
				if (!(this.equippedSexToys[num].properties.uid == UID))
				{
					num++;
					continue;
				}
				break;
			}
			return;
		}
		this.equippedSexToys[num].kill();
		this.equippedSexToys.RemoveAt(num);
	}

	public void prepareClothing()
	{
		for (int i = 0; i < RackCharacter.clipFixRegionNames.Count; i++)
		{
			this.clipFixes.Add(RackCharacter.clipFixRegionNames[i], 0f);
		}
		this.clothingSlots = new string[ClothingSlots.numSlots];
		for (int j = 0; j < this.clothingSlots.Length; j++)
		{
			this.clothingSlots[j] = string.Empty;
		}
		this.sexToySlots = new string[SexToySlots.numSlots];
		for (int k = 0; k < this.sexToySlots.Length; k++)
		{
			this.sexToySlots[k] = string.Empty;
		}
		this.sexToySlotProperties = new LayoutItemSpecialProperties[SexToySlots.numSlots];
		for (int l = 0; l < this.sexToySlotProperties.Length; l++)
		{
			this.sexToySlotProperties[l] = new LayoutItemSpecialProperties();
		}
		this.lastSexToySlotUIDs = new string[SexToySlots.numSlots];
		for (int m = 0; m < this.sexToySlots.Length; m++)
		{
			this.lastSexToySlotUIDs[m] = string.Empty;
		}
	}

	public void resetButt()
	{
		if (this.initted)
		{
			for (int i = 0; i < this.buttRigidbodies.Length; i++)
			{
				this.buttbones[i].localPosition = this.buttOriginalPositions[i];
				Rigidbody obj = this.buttRigidbodies[i];
				obj.angularVelocity *= 0f;
				Rigidbody obj2 = this.buttRigidbodies[i];
				obj2.velocity *= 0f;
			}
		}
	}

	public void prepareButt()
	{
		this.buttbones[0] = this.bones.Butt_L;
		this.buttbones[1] = this.bones.Butt_R;
		ref SoftJointLimit val = ref this.buttS1Limit;
		float limit = 0f;
		this.buttS2Limit.limit = limit;
		val.limit = limit;
		ref SoftJointLimit val2 = ref this.buttHTLimit;
		limit = 0f;
		this.buttLTLimit.limit = limit;
		val2.limit = limit;
		ref SoftJointLimitSpring val3 = ref this.buttTwistSpring;
		limit = 1000f;
		this.buttSwingSpring.spring = limit;
		val3.spring = limit;
		ref SoftJointLimitSpring val4 = ref this.buttTwistSpring;
		limit = 100f;
		this.buttSwingSpring.damper = limit;
		val4.damper = limit;
		for (int i = 0; i < this.buttbones.Length; i++)
		{
			this.buttJoints[i] = this.buttbones[i].gameObject.GetComponent<ConfigurableJoint>();
			this.buttRigidbodies[i] = this.buttbones[i].gameObject.GetComponent<Rigidbody>();
			this.buttJoints[i].connectedBody = ((Component)this.buttbones[i].parent).GetComponent<Rigidbody>();
			this.buttRigidbodies[i].isKinematic = false;
			this.buttOriginalPositions[i] = this.buttbones[i].localPosition;
		}
		if (!RackCharacter.gotOriginalButtStuff)
		{
			RackCharacter.originalAnusPosition = this.bones.Asshole.localPosition;
			RackCharacter.originalAnusRotation = this.bones.Asshole.localRotation;
			RackCharacter.originalAnusLeftPosition = this.bones.AssholeSide_L.localPosition;
			RackCharacter.originalAnusLeftRotation = this.bones.AssholeSide_L.localRotation;
			RackCharacter.originalAnusRightPosition = this.bones.AssholeSide_R.localPosition;
			RackCharacter.originalAnusRightRotation = this.bones.AssholeSide_R.localRotation;
			RackCharacter.originalAnusTopPosition = this.bones.AssholeTop.localPosition;
			RackCharacter.originalAnusTopRotation = this.bones.AssholeTop.localRotation;
			RackCharacter.originalAnusBottomPosition = this.bones.AssholeBottom.localPosition;
			RackCharacter.originalAnusBottomRotation = this.bones.AssholeBottom.localRotation;
			this.originalButtPositions.Add(new Vector3(-0.1013f, 0.06377f, -0.0757f));
			this.originalButtPositions.Add(new Vector3(-0.1013f, -0.06377f, -0.0757f));
			for (int j = 0; j < this.buttbones.Length; j++)
			{
				RackCharacter.originalButtRotations.Add(this.buttbones[j].localRotation);
				this.originalButtEulers.Add(this.buttbones[j].localEulerAngles);
			}
			RackCharacter.gotOriginalButtStuff = true;
		}
	}

	public void prepareBalls()
	{
		this.BallCatcher = this.bones.Pubic.Find("BallCatcher");
		this.BallsackOrigin = this.bones.Pubic.Find("BallsackOrigin");
		((Component)this.bones.Ballsack0).GetComponent<ConfigurableJoint>().connectedBody = ((Component)this.bones.Pubic).GetComponent<Rigidbody>();
		((Component)this.bones.Ballsack0).GetComponent<Rigidbody>().isKinematic = false;
		this.ballbones[0] = this.bones.Ballsack1;
		this.ballbones[1] = this.bones.Nut_L;
		this.ballbones[2] = this.bones.Nut_R;
		this.v3 = (this.bones.Ballsack0.position - this.ballbones[0].position) * 0.5f;
		Transform obj = this.ballbones[0];
		obj.position += this.v3;
		Transform obj2 = this.ballbones[1];
		obj2.position -= this.v3;
		Transform obj3 = this.ballbones[2];
		obj3.position -= this.v3;
		if (RackCharacter.originalBallAngles == null)
		{
			RackCharacter.originalBallAngles = new Vector3[this.ballbones.Length];
			RackCharacter.originalBallPositions = new Vector3[this.ballbones.Length];
			for (int i = 0; i < this.ballbones.Length; i++)
			{
				RackCharacter.originalBallAngles[i] = this.ballbones[i].localEulerAngles;
				RackCharacter.originalBallPositions[i] = this.ballbones[i].localPosition;
			}
		}
		for (int j = 0; j < this.ballbones.Length; j++)
		{
			this.originalBallParents[j] = this.ballbones[j].parent;
		}
		this.unparentBalls();
		for (int k = 0; k < this.ballbones.Length; k++)
		{
			this.ballJoints[k] = ((Component)this.ballbones[k]).GetComponent<ConfigurableJoint>();
			this.ballRigidbodies[k] = ((Component)this.ballbones[k]).GetComponent<Rigidbody>();
		}
		this.ballColliders[0] = ((Component)this.ballbones[1]).GetComponent<SphereCollider>();
		this.ballColliders[1] = ((Component)this.ballbones[2]).GetComponent<SphereCollider>();
		this.ballColliders[0].enabled = true;
		this.ballColliders[1].enabled = true;
		this.scrotumColliders[0] = ((Component)this.ballbones[0]).GetComponent<BoxCollider>();
		this.scrotumColliders[0].enabled = true;
		this.ballJoints[0].connectedBody = ((Component)this.bones.Ballsack0).GetComponent<Rigidbody>();
		this.ballJoints[1].connectedBody = ((Component)this.bones.Ballsack1).GetComponent<Rigidbody>();
		this.ballJoints[2].connectedBody = ((Component)this.bones.Ballsack1).GetComponent<Rigidbody>();
		this.ballJoints[3] = this.ballbones[2].gameObject.AddComponent<ConfigurableJoint>();
		this.ballJoints[3].connectedBody = this.ballRigidbodies[1];
		this.ballJoints[3].xMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].yMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].zMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].angularXMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].angularYMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].angularZMotion = ConfigurableJointMotion.Limited;
		this.ballJoints[3].angularXLimitSpring = this.ballJoints[2].angularXLimitSpring;
		this.ballJoints[3].angularYZLimitSpring = this.ballJoints[2].angularYZLimitSpring;
		this.ballJoints[3].linearLimitSpring = this.ballJoints[2].linearLimitSpring;
		for (int l = 0; l < this.ballRigidbodies.Length; l++)
		{
			if (!((UnityEngine.Object)this.ballRigidbodies[l] == (UnityEngine.Object)null))
			{
				this.ballRigidbodies[l].centerOfMass = Vector3.zero;
				this.ballRigidbodies[l].inertiaTensor = Vector3.one;
				this.ballRigidbodies[l].isKinematic = false;
				this.ballRigidbodies[l].useGravity = false;
			}
		}
		((Component)this.bones.Ballsack0).GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
		((Component)this.bones.Ballsack0).GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
		for (int m = 0; m < this.ballJoints.Length; m++)
		{
			this.ballJoints[m].autoConfigureConnectedAnchor = false;
		}
		if (RackCharacter.originalBallAnchors == null)
		{
			RackCharacter.originalBallAnchors = new Vector3[5];
			for (int n = 0; n < this.ballJoints.Length; n++)
			{
				RackCharacter.originalBallAnchors[n] = this.ballJoints[n].connectedAnchor;
			}
		}
		((Component)this.bones.Ballsack0).GetComponent<Joint>().autoConfigureConnectedAnchor = false;
	}

	public void reparentBalls()
	{
		if (this.allowBallsUnparenting)
		{
			for (int i = 0; i < this.ballbones.Length; i++)
			{
				this.ballbones[i].SetParent(this.originalBallParents[i]);
			}
		}
	}

	public void unparentBalls()
	{
		if (this.allowBallsUnparenting && (UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			for (int i = 0; i < this.ballbones.Length; i++)
			{
				this.ballbones[i].SetParent(this.game.World.transform);
				for (int j = 0; j < this.ballRigidbodies.Length; j++)
				{
					if (!((UnityEngine.Object)this.ballRigidbodies[j] == (UnityEngine.Object)null))
					{
						this.ballRigidbodies[j].centerOfMass = Vector3.zero;
						this.ballRigidbodies[j].inertiaTensor = Vector3.one;
						this.ballRigidbodies[j].isKinematic = false;
					}
				}
			}
		}
	}

	public void resetBalls()
	{
		if (this.initted)
		{
			this.reparentBalls();
			for (int i = 0; i < this.ballbones.Length; i++)
			{
				this.ballbones[i].localPosition = RackCharacter.originalBallPositions[i];
				this.ballbones[i].localEulerAngles = RackCharacter.originalBallAngles[i];
				this.ballbones[i].localScale = Vector3.one;
				Rigidbody obj = this.ballRigidbodies[i];
				obj.angularVelocity *= 0f;
				Rigidbody obj2 = this.ballRigidbodies[i];
				obj2.velocity *= 0f;
				this.lastBallPos[i] = this.ballbones[i].position;
			}
			this.unparentBalls();
		}
	}

	public void prepareVagina()
	{
		this.vaginaEntrance = new GameObject("VaginaEntrance");
		this.vaginaEntrance.transform.SetParent(this.bones.Pubic);
		this.v3 = Vector3.zero;
		this.v3.y = 28.96f;
		this.vaginaEntrance.transform.localEulerAngles = this.v3;
		this.v32 = Vector3.zero;
		this.v32.x = -0.049f;
		this.v32.z = -0.122f;
		this.vaginaEntrance.transform.localPosition = this.v32;
		this.vaginaContainer = new GameObject("Vagina");
		this.vaginaContainer.transform.SetParent(this.bones.Pubic);
		this.v3.x = 0.081f;
		this.v3.y = 0f;
		this.v3.z = -0.092f;
		this.vaginaContainer.transform.localPosition = this.v3;
		this.vaginaContainer.transform.rotation = this.vaginaEntrance.transform.rotation;
		this.bones.VaginaLower_L.SetParent(this.vaginaContainer.transform);
		this.bones.VaginaLower_R.SetParent(this.vaginaContainer.transform);
		this.bones.VaginaUpper_L.SetParent(this.vaginaContainer.transform);
		this.bones.VaginaUpper_R.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMajorLower_L.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMajorLower_R.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMajorUpper_L.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMajorUpper_R.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMinorLower_L.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMinorLower_R.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMinorUpper_L.SetParent(this.vaginaContainer.transform);
		this.bones.LabiaMinorUpper_R.SetParent(this.vaginaContainer.transform);
		this.originalVaginaPosition = this.vaginaContainer.transform.localPosition;
		this.originalClitRotation = this.bones.Clit.rotation;
		this.vaginabones = new Transform[9];
		this.originalVaginaRotations = new Vector3[9];
		this.originalVaginaPositions = new Vector3[9];
		this.vaginabones[0] = this.bones.LabiaMajorUpper_L;
		this.vaginabones[1] = this.bones.LabiaMajorUpper_R;
		this.vaginabones[2] = this.bones.LabiaMajorLower_L;
		this.vaginabones[3] = this.bones.LabiaMajorLower_R;
		this.vaginabones[4] = this.bones.LabiaMinorUpper_L;
		this.vaginabones[5] = this.bones.LabiaMinorUpper_R;
		this.vaginabones[6] = this.bones.LabiaMinorLower_L;
		this.vaginabones[7] = this.bones.LabiaMinorLower_R;
		this.vaginabones[8] = this.bones.VaginaRearLip;
		for (int i = 0; i < this.vaginabones.Length; i++)
		{
			this.originalVaginaRotations[i] = this.vaginabones[i].localEulerAngles;
			this.originalVaginaPositions[i] = this.vaginabones[i].localPosition;
		}
	}

	public void preparePenis()
	{
		this.previousPenisRootPosition = new GameObject("previousPenisRootPosition");
		this.previousPenisRootPosition.transform.SetParent(this.GO.transform);
		this.originalPubicRotation = this.bones.Pubic.localRotation;
		if ((UnityEngine.Object)this.PenisBase == (UnityEngine.Object)null)
		{
			this.PenisBase = this.bones.Pubic.Find("PenisBase");
		}
		((Component)this.PenisBase).GetComponent<Rigidbody>().mass = 45f;
		((Component)this.PenisBase).GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
		((Component)this.PenisBase).GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
		((Component)this.PenisBase).GetComponent<ConfigurableJoint>().connectedBody = ((Component)this.bones.Pubic).GetComponent<Rigidbody>();
		((Component)this.PenisBase).GetComponent<Rigidbody>().isKinematic = false;
		if (RackCharacter.originalPenisBasePosition.magnitude == 0f)
		{
			RackCharacter.originalPenisBasePosition = this.PenisBase.localPosition;
		}
		this.penisbones[0] = this.bones.Penis0;
		this.penisbones[1] = this.bones.Penis1;
		this.penisbones[2] = this.bones.Penis2;
		this.penisbones[3] = this.bones.Penis3;
		this.penisbones[4] = this.bones.Penis4;
		this.penisinverterbones[0] = this.bones.Penis0_inverter;
		this.penisinverterbones[1] = this.bones.Penis1_inverter;
		this.penisinverterbones[2] = this.bones.Penis2_inverter;
		this.penisinverterbones[3] = this.bones.Penis3_inverter;
		for (int i = 0; i < this.penisbones.Length; i++)
		{
			this.originalPenisParents[i] = this.penisbones[i].parent;
		}
		if (RackCharacter.originalPenisBonePositions == null)
		{
			RackCharacter.originalPenisBonePositions = new Vector3[5];
			for (int j = 0; j < 5; j++)
			{
				RackCharacter.originalPenisBonePositions[j] = this.penisbones[j].localPosition;
				RackCharacter.originalPenisRotations[j] = this.penisbones[j].localRotation;
			}
		}
		for (int k = 0; k < this.penisbones.Length; k++)
		{
			if ((UnityEngine.Object)this.penisbones[k].gameObject.GetComponent<Rigidbody>() == (UnityEngine.Object)null)
			{
				this.penisbones[k].gameObject.AddComponent<Rigidbody>();
			}
			this.penisRigidbodies[k] = this.penisbones[k].gameObject.GetComponent<Rigidbody>();
			this.lastPenisRotations[k] = this.penisbones[k].localRotation;
			Vector3 position = this.penisbones[k].position;
			position = ((k <= 0) ? this.PenisBase.position : this.penisbones[k].parent.parent.position);
			this.penisBoneLengths[k] = (position - this.penisbones[k].position).magnitude;
			if ((UnityEngine.Object)this.penisbones[k].gameObject.GetComponent<CapsuleCollider>() == (UnityEngine.Object)null)
			{
				this.penisbones[k].gameObject.AddComponent<CapsuleCollider>();
			}
			this.penisColliders[k] = this.penisbones[k].gameObject.GetComponent<CapsuleCollider>();
			this.penisColliders[k].direction = 0;
			this.penisColliders[k].radius = 0.05f;
			this.penisColliders[k].height = this.penisBoneLengths[k];
			this.v3.x = (0f - this.penisColliders[k].height) / 2f;
			this.v3.y = 0f;
			this.v3.z = 0f;
			this.penisColliders[k].center = this.v3;
			this.penisRigidbodies[k].centerOfMass = Vector3.zero;
			this.penisRigidbodies[k].inertiaTensor = Vector3.one;
		}
		this.unparentPenis();
		for (int l = 0; l < this.penisbones.Length; l++)
		{
			if ((UnityEngine.Object)this.penisbones[l].gameObject.GetComponent<ConfigurableJoint>() == (UnityEngine.Object)null)
			{
				this.penisbones[l].gameObject.AddComponent<ConfigurableJoint>();
			}
			this.penisJoints[l] = this.penisbones[l].gameObject.GetComponent<ConfigurableJoint>();
			this.penisJoints[l].xMotion = ConfigurableJointMotion.Locked;
			this.penisJoints[l].yMotion = ConfigurableJointMotion.Locked;
			this.penisJoints[l].zMotion = ConfigurableJointMotion.Locked;
			this.penisJoints[l].angularXMotion = ConfigurableJointMotion.Limited;
			this.penisJoints[l].angularYMotion = ConfigurableJointMotion.Limited;
			this.penisJoints[l].angularZMotion = ConfigurableJointMotion.Limited;
			this.penisS1Limit.limit = 3f;
			if (l == 0)
			{
				this.penisS2Limit.limit = 0f;
			}
			else
			{
				this.penisS2Limit.limit = 3f;
			}
			this.penisHTLimit.limit = 0f;
			this.penisLTLimit.limit = 0f;
			this.penisLinearLimit.limit = 0f;
			this.penisTwistSpring.damper = 0f;
			this.penisSwingSpring.damper = 0.1f;
			this.penisTwistSpring.spring = 0f;
			this.penisLinearLimit.limit = 0f;
			this.penisSwingSpring.spring = 1f + Mathf.Pow(10f, this.arousal * 100f);
			this.penisJoints[l].angularYLimit = this.penisS1Limit;
			this.penisJoints[l].angularZLimit = this.penisS2Limit;
			this.penisJoints[l].lowAngularXLimit = this.penisLTLimit;
			this.penisJoints[l].highAngularXLimit = this.penisHTLimit;
			this.penisJoints[l].angularXLimitSpring = this.penisTwistSpring;
			if (l == 0)
			{
				this.penisJoints[l].connectedBody = ((Component)this.PenisBase).GetComponent<Rigidbody>();
			}
			else
			{
				this.penisJoints[l].connectedBody = ((Component)this.penisbones[l - 1]).GetComponent<Rigidbody>();
			}
			this.penisJoints[l].projectionMode = JointProjectionMode.PositionAndRotation;
			this.penisJoints[l].projectionAngle = 180f;
			this.penisJoints[l].projectionDistance = 0.1f;
			this.penisRigidbodies[l].centerOfMass = Vector3.zero;
			this.penisRigidbodies[l].inertiaTensor = Vector3.one;
			this.penisRigidbodies[l].mass = 35f;
			this.penisRigidbodies[l].useGravity = false;
		}
		this.penisBaseRelativeToRoot = this.bones.Root.InverseTransformPoint(this.bones.Penis0.position);
		if (RackCharacter.originalPenisLengthMinusHead == 0f)
		{
			RackCharacter.originalPenisLengthMinusHead = (this.bones.Penis4.position - this.bones.Penis3.position).magnitude + (this.bones.Penis3.position - this.bones.Penis2.position).magnitude + (this.bones.Penis2.position - this.bones.Penis1.position).magnitude;
			RackCharacter.originalPenisHeadLength = (this.bones.Penis1.position - this.bones.Penis0.position).magnitude * 2.571058f;
			float num = ((this.bones.Penis4.position - this.bones.Penis3.position).magnitude + (this.bones.Penis3.position - this.bones.Penis2.position).magnitude + (this.bones.Penis2.position - this.bones.Penis1.position).magnitude + (this.bones.Penis1.position - this.bones.Penis0.position).magnitude) * 1.353161f;
			float num2 = RackCharacter.originalPenisHeadLength + RackCharacter.originalPenisLengthMinusHead;
		}
	}

	public void reparentPenis()
	{
		if (this.allowPenisUnparenting)
		{
			for (int i = 0; i < 1; i++)
			{
				this.penisbones[i].SetParent(this.originalPenisParents[i]);
			}
		}
	}

	public void unparentPenis()
	{
		if (this.allowPenisUnparenting && (UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			for (int i = 0; i < 1; i++)
			{
				this.penisbones[i].SetParent(this.game.World.transform);
			}
			for (int j = 0; j < this.penisRigidbodies.Length; j++)
			{
				if (!((UnityEngine.Object)this.penisRigidbodies[j] == (UnityEngine.Object)null))
				{
					this.penisRigidbodies[j].centerOfMass = Vector3.zero;
					this.penisRigidbodies[j].inertiaTensor = Vector3.one;
				}
			}
		}
	}

	public void resetPenis()
	{
		if (this.initted)
		{
			this.reparentPenis();
			this.PenisBase.localPosition = RackCharacter.originalPenisBasePosition;
			for (int i = 0; i < this.penisbones.Length; i++)
			{
				this.penisbones[i].localPosition = RackCharacter.originalPenisBonePositions[i];
				this.penisbones[i].localRotation = RackCharacter.originalPenisRotations[i];
				this.penisbones[i].localScale = Vector3.one;
				this.bones.Penis0_inverter.localScale = Vector3.one;
				this.bones.Penis1_inverter.localScale = Vector3.one;
				this.bones.Penis2_inverter.localScale = Vector3.one;
				this.bones.Penis3_inverter.localScale = Vector3.one;
				Rigidbody obj = this.penisRigidbodies[i];
				obj.angularVelocity *= 0f;
				Rigidbody obj2 = this.penisRigidbodies[i];
				obj2.velocity *= 0f;
				this.lastPenisbonePos[i] = this.penisbones[i].position;
			}
			this.unparentPenis();
		}
	}

	public void prepareWings()
	{
		this.wingbones = new Transform[10];
		this.wingbones[0] = this.bones.Wing0_L;
		this.wingbones[1] = this.bones.Wing1_L;
		this.wingbones[2] = this.bones.Wing2_L;
		this.wingbones[3] = this.bones.Wing3_L;
		this.wingbones[4] = this.bones.Wing4_L;
		this.wingbones[5] = this.bones.Wing0_R;
		this.wingbones[6] = this.bones.Wing1_R;
		this.wingbones[7] = this.bones.Wing2_R;
		this.wingbones[8] = this.bones.Wing3_R;
		this.wingbones[9] = this.bones.Wing4_R;
		SoftJointLimitSpring twistLimitSpring = default(SoftJointLimitSpring);
		SoftJointLimitSpring swingLimitSpring = default(SoftJointLimitSpring);
		SoftJointLimit swing1Limit = default(SoftJointLimit);
		SoftJointLimit swing2Limit = default(SoftJointLimit);
		SoftJointLimit highTwistLimit = default(SoftJointLimit);
		SoftJointLimit lowTwistLimit = default(SoftJointLimit);
		swing1Limit.limit = 0f;
		swing2Limit.limit = 3f;
		float num3 = highTwistLimit.limit = (lowTwistLimit.limit = 0f);
		num3 = (lowTwistLimit.bounciness = 0f);
		num3 = (highTwistLimit.bounciness = num3);
		num3 = (swing1Limit.bounciness = (swing2Limit.bounciness = num3));
		num3 = (twistLimitSpring.spring = (swingLimitSpring.spring = 10000f));
		num3 = (twistLimitSpring.damper = (swingLimitSpring.damper = 0f));
		for (int i = 0; i < 10; i++)
		{
			this.wingbones[i].gameObject.AddComponent<Rigidbody>();
			this.wingbones[i].gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
			this.wingbones[i].gameObject.GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
			this.wingbones[i].gameObject.GetComponent<Rigidbody>().mass = 0.5f;
			this.wingbones[i].gameObject.AddComponent<CharacterJoint>();
			switch (i)
			{
			case 0:
			case 5:
				((Component)this.wingbones[i]).GetComponent<CharacterJoint>().connectedBody = ((Component)this.bones.SpineUpper).GetComponent<Rigidbody>();
				break;
			case 4:
			case 9:
				((Component)this.wingbones[i]).GetComponent<CharacterJoint>().connectedBody = ((Component)this.wingbones[i - 2]).GetComponent<Rigidbody>();
				break;
			default:
				((Component)this.wingbones[i]).GetComponent<CharacterJoint>().connectedBody = ((Component)this.wingbones[i - 1]).GetComponent<Rigidbody>();
				break;
			}
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().enableProjection = true;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().swingLimitSpring = swingLimitSpring;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().twistLimitSpring = twistLimitSpring;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().swing1Limit = swing1Limit;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().swing2Limit = swing2Limit;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().highTwistLimit = highTwistLimit;
			((Component)this.wingbones[i]).GetComponent<CharacterJoint>().lowTwistLimit = lowTwistLimit;
		}
		this.wingInRots = new Quaternion[10];
		this.wingOutRots = new Quaternion[10];
		this.wingOutRots[0] = Quaternion.Euler(349f, 304f, 198f);
		this.wingOutRots[1] = Quaternion.Euler(345f, 103f, 18f);
		this.wingOutRots[2] = Quaternion.Euler(8f, 326f, 4f);
		this.wingOutRots[3] = Quaternion.Euler(358f, 5f, 7f);
		this.wingOutRots[4] = Quaternion.Euler(1f, 275f, 10f);
		this.wingOutRots[5] = Quaternion.Euler(349f, 304f, 198f);
		this.wingOutRots[6] = Quaternion.Euler(345f, 103f, 18f);
		this.wingOutRots[7] = Quaternion.Euler(8f, 326f, 4f);
		this.wingOutRots[8] = Quaternion.Euler(358f, 5f, 7f);
		this.wingOutRots[9] = Quaternion.Euler(1f, 275f, 10f);
		this.wingInRots[0] = Quaternion.Euler(359f, 328f, 177f);
		this.wingInRots[1] = Quaternion.Euler(7f, 150f, 358f);
		this.wingInRots[2] = Quaternion.Euler(332f, 230f, 349f);
		this.wingInRots[3] = Quaternion.Euler(356f, 320f, 355f);
		this.wingInRots[4] = Quaternion.Euler(7f, 325f, 359f);
		this.wingInRots[5] = Quaternion.Euler(359f, 328f, 177f);
		this.wingInRots[6] = Quaternion.Euler(7f, 150f, 358f);
		this.wingInRots[7] = Quaternion.Euler(332f, 230f, 349f);
		this.wingInRots[8] = Quaternion.Euler(356f, 320f, 355f);
		this.wingInRots[9] = Quaternion.Euler(7f, 325f, 359f);
		for (int j = 5; j < 10; j++)
		{
			this.wingOutRots[j].x *= -1f;
			this.wingOutRots[j].z *= -1f;
		}
		for (int k = 0; k < 5; k++)
		{
			this.wingInRots[k].x *= -1f;
			this.wingInRots[k].z *= -1f;
		}
	}

	public void prepareTail()
	{
		this.tailbones[0] = this.bones.Tail0;
		this.tailbones[1] = this.bones.Tail1;
		this.tailbones[2] = this.bones.Tail2;
		this.tailbones[3] = this.bones.Tail3;
		this.tailbones[4] = this.bones.Tail4;
		this.tailbones[5] = this.bones.Tail5;
		this.tailbones[6] = this.bones.Tail6;
		this.tailbones[7] = this.bones.Tail7;
		this.tailbones[8] = this.bones.Tail8;
		this.reparentTail();
		if (RackCharacter.tailStartingAngles == null)
		{
			RackCharacter.tailStartingAngles = new Vector3[9];
			RackCharacter.originalTailbonePosition = new Vector3[9];
			for (int i = 0; i < 9; i++)
			{
				RackCharacter.tailStartingAngles[i] = this.tailbones[i].localEulerAngles;
				RackCharacter.originalTailbonePosition[i] = this.tailbones[i].localPosition;
			}
		}
		else
		{
			for (int j = 0; j < 9; j++)
			{
				this.tailbones[j].localPosition = RackCharacter.originalTailbonePosition[j];
				this.tailbones[j].localEulerAngles = RackCharacter.tailStartingAngles[j];
			}
		}
		this.unparentTail();
		SoftJointLimitSpring softJointLimitSpring = default(SoftJointLimitSpring);
		SoftJointLimitSpring angularYZLimitSpring = default(SoftJointLimitSpring);
		this.originalTailCapsuleHeight = new List<float>();
		for (int k = 0; k < this.tailbones.Length; k++)
		{
			float num3 = softJointLimitSpring.spring = (angularYZLimitSpring.spring = Mathf.Lerp(50f, 5000f, this.data.tailStiffness + this.cap(3f - (float)k, 0f, 3f) * 0.35f));
			num3 = (softJointLimitSpring.damper = (angularYZLimitSpring.damper = angularYZLimitSpring.spring * 0.1f));
			this.tailCurlDrive.maximumForce = 100f * (10f / ((float)k + 1f));
			this.tailCurlDrive.positionSpring = angularYZLimitSpring.spring * (10f / ((float)k + 1f));
			this.tailCurlDrive.positionDamper = angularYZLimitSpring.damper * (10f / ((float)k + 1f));
			this.tailForceFromHipMovement[k] = Vector3.zero;
			if ((UnityEngine.Object)((Component)this.tailbones[k]).GetComponent<Rigidbody>() == (UnityEngine.Object)null)
			{
				this.tailbones[k].gameObject.AddComponent<Rigidbody>();
			}
			this.tailRigidBodies[k] = ((Component)this.tailbones[k]).GetComponent<Rigidbody>();
			if ((UnityEngine.Object)this.tailJoints[k] == (UnityEngine.Object)null)
			{
				this.tailbones[k].gameObject.AddComponent<ConfigurableJoint>();
			}
			this.tailJoints[k] = this.tailbones[k].gameObject.GetComponent<ConfigurableJoint>();
			this.tailJoints[k].xMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].yMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].zMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].angularXMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].angularYMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].angularZMotion = ConfigurableJointMotion.Limited;
			this.tailJoints[k].angularYZLimitSpring = angularYZLimitSpring;
			this.tailJoints[k].projectionMode = JointProjectionMode.PositionAndRotation;
			this.tailJoints[k].rotationDriveMode = RotationDriveMode.XYAndZ;
			this.tailJoints[k].angularYZDrive = this.tailCurlDrive;
			if (k == 0)
			{
				this.tailJoints[k].connectedBody = ((Component)this.bones.SpineLower).GetComponent<Rigidbody>();
			}
			else
			{
				this.tailJoints[k].connectedBody = ((Component)this.tailbones[k - 1]).GetComponent<Rigidbody>();
			}
			Vector3 position = this.tailbones[k].position;
			if (this.tailbones[k].childCount > 0)
			{
				position = this.tailbones[k].GetChild(0).position;
			}
			else
			{
				position.z += 0.4f;
			}
			float magnitude = (position - this.tailbones[k].position).magnitude;
			this.originalTailCapsuleHeight.Add(magnitude);
			if ((UnityEngine.Object)this.tailbones[k].gameObject.GetComponent<CapsuleCollider>() == (UnityEngine.Object)null)
			{
				this.tailbones[k].gameObject.AddComponent<CapsuleCollider>();
			}
			this.tailColliders[k] = this.tailbones[k].gameObject.GetComponent<CapsuleCollider>();
			this.tailColliders[k].direction = 0;
			this.tailColliders[k].radius = 0.1f;
			this.tailColliders[k].height = magnitude;
			this.v3.x = (0f - magnitude) / 2f;
			this.v3.y = 0f;
			this.v3.z = 0f;
			this.tailColliders[k].center = this.v3;
			this.tailRigidBodies[k].centerOfMass = Vector3.zero;
			this.tailRigidBodies[k].inertiaTensor = Vector3.one;
			this.tailRigidBodies[k].useGravity = false;
		}
		this.needTailRecurl = true;
	}

	public void unparentTail()
	{
		if (!((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null) && !this.controlledByPlayer)
		{
			return;
		}
		for (int i = 0; i < this.tailbones.Length; i++)
		{
			this.tailbones[i].SetParent(this.game.World.transform);
		}
	}

	public void reparentTail()
	{
		this.tailbones[0].SetParent(this.bones.SpineLower);
		for (int i = 1; i < this.tailbones.Length; i++)
		{
			this.tailbones[i].SetParent(this.tailbones[i - 1]);
		}
	}

	public void resetTail()
	{
		this.reparentTail();
		for (int i = 0; i < this.tailbones.Length; i++)
		{
			this.tailbones[i].localPosition = RackCharacter.originalTailbonePosition[i];
			if (i > 0)
			{
				this.tailbones[i].localScale = Vector3.one;
			}
			Rigidbody obj = this.tailRigidBodies[i];
			obj.angularVelocity *= 0f;
			Rigidbody obj2 = this.tailRigidBodies[i];
			obj2.velocity *= 0f;
			this.lastTailbonePos[i] = this.tailbones[i].position;
		}
		this.unparentTail();
		this.lastTailSize = -1f;
	}

	public bool tailCollision(Collision collision, bool first)
	{
		Debug.Log("Tail collided with " + collision.collider.name);
		return true;
	}

	public void updateBaseCoordinates()
	{
		this.startCoords("Eye_L", this.bones.Eye_L);
		this.startCoords("Eye_R", this.bones.Eye_R);
		this.startCoords("Head", this.bones.Head);
		this.startCoords("Nut_L", this.bones.Nut_L);
		this.startCoords("Nut_R", this.bones.Nut_R);
	}

	public void startCoords(string boneName, Transform bone)
	{
		this.startRot[boneName] = bone.localRotation.eulerAngles;
	}

	public void useFurniture(Furniture _furniture)
	{
		this.preFurniturePosition = this.GO.transform.position;
		this.furniture = _furniture;
		this.furnitureRoot = this.furniture.transform.Find("Root");
		this.ignoreCollisions(this.furniture.transform, false, null);
		Vector3 position = this.furniture.transform.position;
		float x = position.x;
		Vector3 position2 = this.furniture.transform.position;
		float y = position2.y;
		Vector3 position3 = this.furniture.transform.position;
		this.teleport(x, y, position3.z, -999f, false);
		this.v3 = this.GO.transform.eulerAngles;
		ref Vector3 val = ref this.v3;
		Vector3 eulerAngles = this.furniture.transform.eulerAngles;
		val.y = eulerAngles.y;
		this.GO.transform.eulerAngles = this.v3;
		this.furniture.occupant = this;
		this.furniture.occupied = true;
		this.furniturePose = this.furniture.animName;
		if (this.furniture.scalesToFitOccupant)
		{
			this.furniture.gameObject.transform.localScale = Vector3.one * (0.8f + this.data.height * 0.5f);
		}
		this.animator.SetBool(this.furniturePose, true);
		this.resetAllFloppyBodies();
	}

	public void leaveFurniture()
	{
		if ((UnityEngine.Object)this.furniture != (UnityEngine.Object)null)
		{
			this.delayedFurnitureUnphysics = this.furniture.transform;
			this.delayedFurnitureUnphysicsTime = 0.5f;
			this.furniture.occupied = false;
			this.furniture.occupant = null;
			this.furniture = null;
			try
			{
				this.animator.SetBool(this.furniturePose, false);
			}
			catch
			{
			}
		}
		this.furniturePose = string.Empty;
		this.teleport(this.preFurniturePosition.x, this.preFurniturePosition.y, this.preFurniturePosition.z, -999f, false);
	}

	public void unignoreCollisions(Transform with)
	{
		this.ignoreCollisions(with, true, null);
	}

	public void SmartIgnoreCollision(Collider a, Collider b, bool ignore = true)
	{
		this.aWasEnabled = a.enabled;
		this.bWasEnabled = b.enabled;
		Physics.IgnoreCollision(a, b, ignore);
		a.enabled = this.aWasEnabled;
		b.enabled = this.bWasEnabled;
	}

	public Transform recursiveFindChild(string name, Transform of = null)
	{
		if ((UnityEngine.Object)of == (UnityEngine.Object)null)
		{
			of = this.GO.transform;
		}
		for (int i = 0; i < of.childCount; i++)
		{
			if (of.GetChild(i).name == name)
			{
				return of.GetChild(i);
			}
			if (of.GetChild(i).childCount > 0)
			{
				Transform transform = this.recursiveFindChild(name, of.GetChild(i));
				if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
				{
					return transform;
				}
			}
		}
		return null;
	}

	public void ignoreCollisions(Transform with, bool unignore = false, Transform recursiveRoot = null)
	{
		this.rebuildColliders();
		Collider[] array = ((Component)with).GetComponentsInChildren<Collider>().Concat(((Component)with).GetComponents<Collider>()).ToArray();
		for (int i = 0; i < this.colliders.Length; i++)
		{
			if (this.colliders[i].name.IndexOf("PreciseMouse") == -1)
			{
				for (int j = 0; j < array.Length; j++)
				{
					this.SmartIgnoreCollision(this.colliders[i], array[j], !unignore);
					if (i == 0)
					{
						this.SmartIgnoreCollision(((Component)this.movementTarget).GetComponent<Collider>(), array[j], !unignore);
					}
				}
			}
		}
	}

	public void bindToApparatus(BondageApparatus app)
	{
		this.apparatus = app;
		this.apparatus.boundCharacter = this;
		this.ignoreCollisions(app.transform, false, null);
		Vector3 position = app.transform.position;
		float x = position.x;
		Vector3 position2 = app.transform.position;
		float y = position2.y;
		Vector3 position3 = app.transform.position;
		float z = position3.z;
		Vector3 eulerAngles = app.transform.eulerAngles;
		this.teleport(x, y, z, eulerAngles.y, false);
		this.v3 = this.GO.transform.eulerAngles;
		ref Vector3 val = ref this.v3;
		Vector3 eulerAngles2 = app.transform.eulerAngles;
		val.y = eulerAngles2.y;
		this.GO.transform.eulerAngles = this.v3;
		this.setPose(app.poseName);
		this.boundPose = app.poseName;
		this.apparatus.timeAlive = 0f;
		this.headMovementBlockedByApparatus_back = this.apparatus.blockHeadMovement_back;
		BondagePoint[] componentsInChildren = ((Component)app).GetComponentsInChildren<BondagePoint>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			string name = componentsInChildren[i].name;
			if (name != null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
				dictionary.Add("Hand_L", 0);
				dictionary.Add("Hand_R", 1);
				dictionary.Add("Foot_L", 2);
				dictionary.Add("Foot_R", 3);
				dictionary.Add("ElbowTargetL", 4);
				dictionary.Add("ElbowTargetR", 5);
				dictionary.Add("KneeTargetL", 6);
				dictionary.Add("KneeTargetR", 7);
				int num = default(int);
				if (dictionary.TryGetValue(name, out num))
				{
					switch (num)
					{
					case 0:
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 0.95f;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 0.75f;
						break;
					case 1:
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 0.95f;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 0.75f;
						break;
					case 2:
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.target = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.positionWeight = 1f;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.rotationWeight = 0.75f;
						break;
					case 3:
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.target = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight = 1f;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.rotationWeight = 0.75f;
						break;
					case 4:
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.bendGoal = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.weight = 1f;
						break;
					case 5:
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.bendGoal = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.weight = 1f;
						break;
					case 6:
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.bendGoal = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.weight = 1f;
						break;
					case 7:
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.bendGoal = componentsInChildren[i].transform;
						this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.weight = 1f;
						break;
					}
				}
			}
		}
		this.lastForeskinOverlap = -1f;
		this.resetAllFloppyBodies();
	}

	public void prepareIK()
	{
		this.tailholeEntranceAfterIK = new GameObject("TailholeEntranceAfterIK");
		this.tailholeEntranceAfterIK.transform.SetParent(this.game.World.transform);
		this.vaginalEntranceAfterIK = new GameObject("VaginalEntranceAfterIK");
		this.vaginalEntranceAfterIK.transform.SetParent(this.game.World.transform);
		this.throatHole = new GameObject("ThroatHole");
		this.throatHole.transform.SetParent(this.bones.Head);
		this.throatHole.transform.localScale = Vector3.one;
		this.v3 = Vector3.zero;
		this.v3.y = -15f;
		this.throatHole.transform.localEulerAngles = this.v3;
		this.v3.x = -0.082f;
		this.v3.y = 0f;
		this.v3.z = -0.117f;
		this.throatHole.transform.localPosition = this.v3;
		this.throatHoleAfterIK = new GameObject("ThroatHoleAfterIK");
		this.throatHoleAfterIK.transform.SetParent(this.game.World.transform);
		this.throatHoleAfterIKandSuckLock = new GameObject("throatHoleAfterIKandSuckLock");
		this.throatHoleAfterIKandSuckLock.transform.SetParent(this.game.World.transform);
		this.mouthOpeningAfterIK = new GameObject("mouthOpeningAfterIK");
		this.mouthOpeningAfterIK.transform.SetParent(this.game.World.transform);
		if (this.shoulderLstartPos.x == 0f)
		{
			this.shoulderLstartPos = this.bones.Shoulder_L.localPosition;
			this.shoulderRstartPos = this.bones.Shoulder_R.localPosition;
		}
		BipedReferences bipedReferences = new BipedReferences();
		bipedReferences.root = this.GO.transform.Find("Armature");
		bipedReferences.pelvis = this.GO.transform.Find("Armature").Find("Root");
		bipedReferences.leftThigh = this.GO.transform.Find("Armature").Find("Root").Find("Hip_L")
			.Find("UpperLeg_L");
		bipedReferences.leftCalf = this.GO.transform.Find("Armature").Find("Root").Find("Hip_L")
			.Find("UpperLeg_L")
			.Find("LowerLeg_L");
		bipedReferences.leftFoot = this.GO.transform.Find("Armature").Find("Root").Find("Hip_L")
			.Find("UpperLeg_L")
			.Find("LowerLeg_L")
			.Find("Foot_L")
			.Find("Footpad_L");
		bipedReferences.rightThigh = this.GO.transform.Find("Armature").Find("Root").Find("Hip_R")
			.Find("UpperLeg_R");
		bipedReferences.rightCalf = this.GO.transform.Find("Armature").Find("Root").Find("Hip_R")
			.Find("UpperLeg_R")
			.Find("LowerLeg_R");
		bipedReferences.rightFoot = this.GO.transform.Find("Armature").Find("Root").Find("Hip_R")
			.Find("UpperLeg_R")
			.Find("LowerLeg_R")
			.Find("Foot_R")
			.Find("Footpad_R");
		bipedReferences.leftUpperArm = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_L")
			.Find("UpperArm_L");
		bipedReferences.leftForearm = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_L")
			.Find("UpperArm_L")
			.Find("LowerArm_L");
		bipedReferences.leftHand = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_L")
			.Find("UpperArm_L")
			.Find("LowerArm_L")
			.Find("Hand_L");
		bipedReferences.rightUpperArm = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_R")
			.Find("UpperArm_R");
		bipedReferences.rightForearm = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_R")
			.Find("UpperArm_R")
			.Find("LowerArm_R");
		bipedReferences.rightHand = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Shoulder_R")
			.Find("UpperArm_R")
			.Find("LowerArm_R")
			.Find("Hand_R");
		bipedReferences.head = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Neck")
			.Find("NeckInvert")
			.Find("Head");
		bipedReferences.spine = new Transform[2];
		bipedReferences.spine[0] = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower");
		bipedReferences.spine[1] = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper");
		this.GO.GetComponent<FullBodyBipedIK>().SetReferences(bipedReferences, this.bones.Root);
		this.GO.GetComponent<FullBodyBipedIK>().fixTransforms = true;
		this.resetIdleHandTarget(false);
		this.resetIdleHandTarget(true);
		this.resetElbowTarget(false);
		this.resetElbowTarget(true);
		this.resetKneeTarget(false);
		this.resetKneeTarget(true);
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.idleHandLTarget;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 0.9f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.idleHandRTarget;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 0.9f;
		this.GO.GetComponent<FullBodyBipedIK>().enabled = false;
		this.FIKsolver = this.GO.GetComponent<FullBodyBipedIK>().solver;
		while (this.handheldObjects.Count < 2)
		{
			this.handheldObjects.Add(null);
		}
		this.usingDefaultBodyTarget = true;
		this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = this.GO.transform.Find("BodyTarget");
		this.bodyTarget = this.GO.transform.Find("BodyTarget");
		this.originalBodyTargetPosition = this.bodyTarget.localPosition;
		this.headTarget = this.GO.transform.Find("HeadTarget");
		this.headTarget.SetParent(this.game.World.transform);
		FBBIKHeadEffector.BendBone[] array = new FBBIKHeadEffector.BendBone[4]
		{
			new FBBIKHeadEffector.BendBone(),
			null,
			null,
			null
		};
		array[0].transform = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower");
		array[0].weight = 1f;
		array[1] = new FBBIKHeadEffector.BendBone();
		array[1].transform = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle");
		array[1].weight = 1f;
		array[2] = new FBBIKHeadEffector.BendBone();
		array[2].transform = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper");
		array[2].weight = 1f;
		array[3] = new FBBIKHeadEffector.BendBone();
		array[3].transform = this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
			.Find("SpineMiddle")
			.Find("SpineUpper")
			.Find("Neck");
		array[3].weight = 1f;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().bendBones = array;
		Transform[] cCDBones = new Transform[4]
		{
			this.GO.transform.Find("Armature").Find("Root").Find("SpineLower"),
			this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
				.Find("SpineMiddle"),
			this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
				.Find("SpineMiddle")
				.Find("SpineUpper"),
			this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
				.Find("SpineMiddle")
				.Find("SpineUpper")
				.Find("Neck")
		};
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().CCDBones = cCDBones;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().CCDWeight = 1f;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().roll = 0f;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().damper = 500f;
		Transform[] stretchBones = new Transform[1]
		{
			this.GO.transform.Find("Armature").Find("Root").Find("SpineLower")
				.Find("SpineMiddle")
				.Find("SpineUpper")
				.Find("Neck")
		};
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().stretchBones = stretchBones;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().stretchWeight = 1f;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().maxStretch = 0.15f;
	}

	public void solveIK(int iterations)
	{
		if (!this.initted)
		{
			return;
		}
		if (!Game.allowIK)
		{
			return;
		}
		if (!this.hidden)
		{
			if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null && this.inFrontOfCamera && this.distFromCamera < 60f && this.lineOfSightToCamera)
			{
				goto IL_0062;
			}
			if (this.controlledByPlayer)
			{
				goto IL_0062;
			}
		}
		goto IL_00d7;
		IL_0062:
		this.GO.GetComponent<FullBodyBipedIK>().solver.iterations = iterations;
		((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().enabled = this.currentlyUsingMouth;
		this.FIKsolver.Update();
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			this.apparatus.animateRestraints();
		}
		if (this.interactionSubject != null)
		{
			this.interactionSubject.apparatus.animateBasedOnPose(this, this.curSexPoseName);
		}
		goto IL_00d7;
		IL_00d7:
		if (!this.hidden)
		{
			this.afterIK();
		}
	}

	public void setIdleHandTarget(bool rightHand, Transform target, float clench = 0f, float thumbclench = 0f)
	{
		if (!rightHand)
		{
			if ((UnityEngine.Object)this.idleHandLTarget != (UnityEngine.Object)target)
			{
				this.idleHandLTarget = target;
				if (!this.currentlyUsingHandL)
				{
					this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.idleHandLTarget;
					this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
					this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 1f;
				}
			}
			this.idleHandLclench = clench;
			this.idleHandLclenchT = thumbclench;
			this.customIdleHandLtarget = true;
		}
		else
		{
			if ((UnityEngine.Object)this.idleHandRTarget != (UnityEngine.Object)target)
			{
				this.idleHandRTarget = target;
				if (!this.currentlyUsingHandR)
				{
					this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.idleHandRTarget;
					this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
					this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 1f;
				}
			}
			this.idleHandRclench = clench;
			this.idleHandRclenchT = thumbclench;
			this.customIdleHandRtarget = true;
		}
	}

	public void setElbowTarget(bool right, Transform target, float weight = 0.9f)
	{
		if (!right)
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.bendGoal = target;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.weight = weight;
		}
		else
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.bendGoal = target;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.weight = weight;
		}
	}

	public void setKneeTarget(bool right, Transform target, float weight = 0.9f)
	{
		if (!right)
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.bendGoal = target;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.weight = weight;
		}
		else
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.bendGoal = target;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.weight = weight;
		}
	}

	public void resetIdleHandTarget(bool rightHand)
	{
		if (!rightHand)
		{
			if ((UnityEngine.Object)this.idleHandLTarget != (UnityEngine.Object)this.GOHandTargetL)
			{
				this.idleHandLTarget = this.GOHandTargetL;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.idleHandLTarget;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 0.9f;
			}
			this.idleHandLclench = 0f;
			this.idleHandLclenchT = 0f;
			this.customIdleHandLtarget = false;
		}
		else
		{
			if ((UnityEngine.Object)this.idleHandRTarget != (UnityEngine.Object)this.GOHandTargetR)
			{
				this.idleHandRTarget = this.GOHandTargetR;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.idleHandRTarget;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 0.9f;
			}
			this.idleHandRclench = 0f;
			this.idleHandRclenchT = 0f;
			this.customIdleHandRtarget = false;
		}
	}

	public void resetElbowTarget(bool right)
	{
		if (!right)
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.bendGoal = null;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.weight = 0f;
		}
		else
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.bendGoal = null;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.weight = 0f;
		}
	}

	public void resetKneeTarget(bool right)
	{
		if (!right)
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.bendGoal = null;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.weight = 0f;
		}
		else
		{
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.bendGoal = null;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.weight = 0f;
		}
	}

	public void identifyBones()
	{
		this.bones.Armature = this.GO.transform.Find("Armature");
		this.bones.Root = this.GO.transform.Find("Armature").Find("Root");
		this.rootAfterIK = new GameObject("rootAfterIK");
		this.rootAfterIK.transform.parent = this.bones.Root.parent;
		this.bones.SpineLower = this.bones.Root.Find("SpineLower");
		this.bones.Belly = this.bones.SpineLower.Find("Belly");
		this.bones.SpineMiddle = this.bones.SpineLower.Find("SpineMiddle");
		this.bones.SpineUpper = this.bones.SpineMiddle.Find("SpineUpper");
		this.bones.Shoulder_L = this.bones.SpineUpper.Find("Shoulder_L");
		this.bones.UpperArm_L = this.bones.Shoulder_L.Find("UpperArm_L");
		this.bones.LowerArm_L = this.bones.UpperArm_L.Find("LowerArm_L");
		this.bones.Hand_L = this.bones.LowerArm_L.Find("Hand_L");
		this.bones.Thumb0_L = this.bones.Hand_L.Find("Thumb0_L");
		this.bones.Thumb1_L = this.bones.Thumb0_L.Find("Thumb1_L");
		this.bones.Thumb2_L = this.bones.Thumb1_L.Find("Thumb2_L");
		this.bones.Finger00_L = this.bones.Hand_L.Find("Finger00_L");
		this.bones.Finger01_L = this.bones.Finger00_L.Find("Finger01_L");
		this.bones.Finger02_L = this.bones.Finger01_L.Find("Finger02_L");
		this.bones.Finger10_L = this.bones.Hand_L.Find("Finger10_L");
		this.bones.Finger11_L = this.bones.Finger10_L.Find("Finger11_L");
		this.bones.Finger12_L = this.bones.Finger11_L.Find("Finger12_L");
		this.bones.Finger20_L = this.bones.Hand_L.Find("Finger20_L");
		if ((UnityEngine.Object)this.bones.Finger20_L != (UnityEngine.Object)null)
		{
			this.bones.Finger21_L = this.bones.Finger20_L.Find("Finger21_L");
		}
		if ((UnityEngine.Object)this.bones.Finger20_L != (UnityEngine.Object)null)
		{
			this.bones.Finger22_L = this.bones.Finger21_L.Find("Finger22_L");
		}
		this.bones.Finger30_L = this.bones.Hand_L.Find("Finger30_L");
		if ((UnityEngine.Object)this.bones.Finger30_L != (UnityEngine.Object)null)
		{
			this.bones.Finger31_L = this.bones.Finger30_L.Find("Finger31_L");
		}
		if ((UnityEngine.Object)this.bones.Finger30_L != (UnityEngine.Object)null)
		{
			this.bones.Finger32_L = this.bones.Finger31_L.Find("Finger32_L");
		}
		this.bones.Shoulder_R = this.bones.SpineUpper.Find("Shoulder_R");
		this.bones.UpperArm_R = this.bones.Shoulder_R.Find("UpperArm_R");
		this.bones.LowerArm_R = this.bones.UpperArm_R.Find("LowerArm_R");
		this.bones.Hand_R = this.bones.LowerArm_R.Find("Hand_R");
		this.bones.Thumb0_R = this.bones.Hand_R.Find("Thumb0_R");
		this.bones.Thumb1_R = this.bones.Thumb0_R.Find("Thumb1_R");
		this.bones.Thumb2_R = this.bones.Thumb1_R.Find("Thumb2_R");
		this.bones.Finger00_R = this.bones.Hand_R.Find("Finger00_R");
		this.bones.Finger01_R = this.bones.Finger00_R.Find("Finger01_R");
		this.bones.Finger02_R = this.bones.Finger01_R.Find("Finger02_R");
		this.bones.Finger10_R = this.bones.Hand_R.Find("Finger10_R");
		this.bones.Finger11_R = this.bones.Finger10_R.Find("Finger11_R");
		this.bones.Finger12_R = this.bones.Finger11_R.Find("Finger12_R");
		this.bones.Finger20_R = this.bones.Hand_R.Find("Finger20_R");
		if ((UnityEngine.Object)this.bones.Finger20_R != (UnityEngine.Object)null)
		{
			this.bones.Finger21_R = this.bones.Finger20_R.Find("Finger21_R");
		}
		if ((UnityEngine.Object)this.bones.Finger20_R != (UnityEngine.Object)null)
		{
			this.bones.Finger22_R = this.bones.Finger21_R.Find("Finger22_R");
		}
		this.bones.Finger30_R = this.bones.Hand_R.Find("Finger30_R");
		if ((UnityEngine.Object)this.bones.Finger30_R != (UnityEngine.Object)null)
		{
			this.bones.Finger31_R = this.bones.Finger30_R.Find("Finger31_R");
		}
		if ((UnityEngine.Object)this.bones.Finger30_R != (UnityEngine.Object)null)
		{
			this.bones.Finger32_R = this.bones.Finger31_R.Find("Finger32_R");
		}
		this.bones.Hip_L = this.bones.Root.Find("Hip_L");
		this.bones.UpperLeg_L = this.bones.Hip_L.Find("UpperLeg_L");
		this.bones.LowerLeg_L = this.bones.UpperLeg_L.Find("LowerLeg_L");
		this.bones.Foot_L = this.bones.LowerLeg_L.Find("Foot_L");
		this.bones.Footpad_L = this.bones.Foot_L.Find("Footpad_L");
		this.bones.Toe0_L = this.bones.Footpad_L.Find("Toe0_L");
		this.bones.Toe1_L = this.bones.Footpad_L.Find("Toe1_L");
		this.bones.Toe2_L = this.bones.Footpad_L.Find("Toe2_L");
		this.bones.Toe3_L = this.bones.Footpad_L.Find("Toe3_L");
		this.bones.Hip_R = this.bones.Root.Find("Hip_R");
		this.bones.UpperLeg_R = this.bones.Hip_R.Find("UpperLeg_R");
		this.bones.LowerLeg_R = this.bones.UpperLeg_R.Find("LowerLeg_R");
		this.bones.Foot_R = this.bones.LowerLeg_R.Find("Foot_R");
		this.bones.Footpad_R = this.bones.Foot_R.Find("Footpad_R");
		this.bones.Toe0_R = this.bones.Footpad_R.Find("Toe0_R");
		this.bones.Toe1_R = this.bones.Footpad_R.Find("Toe1_R");
		this.bones.Toe2_R = this.bones.Footpad_R.Find("Toe2_R");
		this.bones.Toe3_R = this.bones.Footpad_R.Find("Toe3_R");
		this.bones.Butt_L = this.bones.Hip_L.Find("Butt_L");
		this.bones.Butt_R = this.bones.Hip_R.Find("Butt_R");
		this.bones.Butt_R.gameObject.AddComponent<ButtBugDetector>();
		((Component)this.bones.Butt_R).GetComponent<ButtBugDetector>().owner = this;
		this.bones.Asshole = this.bones.Root.Find("Asshole");
		this.bones.AssholeTop = this.bones.Asshole.Find("AssholeTop");
		this.bones.AssholeBottom = this.bones.Asshole.Find("AssholeBottom");
		this.bones.AssholeSide_L = this.bones.Asshole.Find("AssholeSide_L");
		this.bones.AssholeSide_R = this.bones.Asshole.Find("AssholeSide_R");
		this.bones.Pubic = this.bones.Root.Find("Pubic");
		this.bones.Sheath = this.bones.Pubic.Find("Sheath");
		this.bones.Penis0 = this.bones.Pubic.Find("Penis0");
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis0_inverter = this.bones.Penis0.Find("Penis0_001");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis1 = this.bones.Penis0_inverter.Find("Penis1");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis1_inverter = this.bones.Penis1.Find("Penis1_001");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis2 = this.bones.Penis1_inverter.Find("Penis2");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis2_inverter = this.bones.Penis2.Find("Penis2_001");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis3 = this.bones.Penis2_inverter.Find("Penis3");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis3_inverter = this.bones.Penis3.Find("Penis3_001");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Penis4 = this.bones.Penis3_inverter.Find("Penis4");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Knot_L = this.bones.Penis1_inverter.Find("Knot_L");
		}
		if ((UnityEngine.Object)this.bones.Penis0 != (UnityEngine.Object)null)
		{
			this.bones.Knot_R = this.bones.Penis1_inverter.Find("Knot_R");
		}
		this.bones.Wing0_L = this.bones.SpineUpper.Find("Wing0_L");
		if ((UnityEngine.Object)this.bones.Wing0_L != (UnityEngine.Object)null)
		{
			this.bones.Wing1_L = this.bones.Wing0_L.Find("Wing1_L");
		}
		if ((UnityEngine.Object)this.bones.Wing0_L != (UnityEngine.Object)null)
		{
			this.bones.Wing2_L = this.bones.Wing1_L.Find("Wing2_L");
		}
		if ((UnityEngine.Object)this.bones.Wing0_L != (UnityEngine.Object)null)
		{
			this.bones.Wing3_L = this.bones.Wing2_L.Find("Wing3_L");
		}
		if ((UnityEngine.Object)this.bones.Wing0_L != (UnityEngine.Object)null)
		{
			this.bones.Wing4_L = this.bones.Wing2_L.Find("Wing4_L");
		}
		this.bones.Wing0_R = this.bones.SpineUpper.Find("Wing0_R");
		if ((UnityEngine.Object)this.bones.Wing0_R != (UnityEngine.Object)null)
		{
			this.bones.Wing1_R = this.bones.Wing0_R.Find("Wing1_R");
		}
		if ((UnityEngine.Object)this.bones.Wing0_R != (UnityEngine.Object)null)
		{
			this.bones.Wing2_R = this.bones.Wing1_R.Find("Wing2_R");
		}
		if ((UnityEngine.Object)this.bones.Wing0_R != (UnityEngine.Object)null)
		{
			this.bones.Wing3_R = this.bones.Wing2_R.Find("Wing3_R");
		}
		if ((UnityEngine.Object)this.bones.Wing0_R != (UnityEngine.Object)null)
		{
			this.bones.Wing4_R = this.bones.Wing2_R.Find("Wing4_R");
		}
		this.bones.UrethraTop = this.bones.Penis4.Find("UrethraTop");
		this.bones.UrethraBottom = this.bones.Penis4.Find("UrethraBottom");
		this.bones.UrethraSide_L = this.bones.Penis4.Find("UrethraSide_L");
		this.bones.UrethraSide_R = this.bones.Penis4.Find("UrethraSide_R");
		this.bones.Ballsack0 = this.bones.Pubic.Find("Ballsack0");
		this.bones.Ballsack1 = this.bones.Ballsack0.Find("Ballsack1");
		this.bones.Nut_L = this.bones.Ballsack1.Find("Nut_L");
		this.bones.Nut_R = this.bones.Ballsack1.Find("Nut_R");
		this.bones.Clit = this.bones.Pubic.Find("Clit");
		this.bones.LabiaMajorUpper_L = this.bones.Pubic.Find("LabiaMajorUpper_L");
		this.bones.LabiaMajorLower_L = this.bones.Pubic.Find("LabiaMajorLower_L");
		this.bones.LabiaMinorUpper_L = this.bones.Pubic.Find("LabiaMinorUpper_L");
		this.bones.LabiaMinorLower_L = this.bones.Pubic.Find("LabiaMinorLower_L");
		this.bones.LabiaMajorUpper_R = this.bones.Pubic.Find("LabiaMajorUpper_R");
		this.bones.LabiaMajorLower_R = this.bones.Pubic.Find("LabiaMajorLower_R");
		this.bones.LabiaMinorUpper_R = this.bones.Pubic.Find("LabiaMinorUpper_R");
		this.bones.LabiaMinorLower_R = this.bones.Pubic.Find("LabiaMinorLower_R");
		this.bones.VaginaRearLip = this.bones.Pubic.Find("VaginaRearLip");
		this.bones.VaginaUpper_L = this.bones.Pubic.Find("VaginaUpper_L");
		this.bones.VaginaLower_L = this.bones.Pubic.Find("VaginaLower_L");
		this.bones.VaginaUpper_R = this.bones.Pubic.Find("VaginaUpper_R");
		this.bones.VaginaLower_R = this.bones.Pubic.Find("VaginaLower_R");
		this.bones.Tail0 = this.bones.SpineLower.Find("Tail0");
		if ((UnityEngine.Object)this.bones.Tail0 != (UnityEngine.Object)null)
		{
			this.bones.Tail1 = this.bones.Tail0.Find("Tail1");
		}
		if ((UnityEngine.Object)this.bones.Tail1 != (UnityEngine.Object)null)
		{
			this.bones.Tail2 = this.bones.Tail1.Find("Tail2");
		}
		if ((UnityEngine.Object)this.bones.Tail2 != (UnityEngine.Object)null)
		{
			this.bones.Tail3 = this.bones.Tail2.Find("Tail3");
		}
		if ((UnityEngine.Object)this.bones.Tail3 != (UnityEngine.Object)null)
		{
			this.bones.Tail4 = this.bones.Tail3.Find("Tail4");
		}
		if ((UnityEngine.Object)this.bones.Tail4 != (UnityEngine.Object)null)
		{
			this.bones.Tail5 = this.bones.Tail4.Find("Tail5");
		}
		if ((UnityEngine.Object)this.bones.Tail5 != (UnityEngine.Object)null)
		{
			this.bones.Tail6 = this.bones.Tail5.Find("Tail6");
		}
		if ((UnityEngine.Object)this.bones.Tail6 != (UnityEngine.Object)null)
		{
			this.bones.Tail7 = this.bones.Tail6.Find("Tail7");
		}
		if ((UnityEngine.Object)this.bones.Tail7 != (UnityEngine.Object)null)
		{
			this.bones.Tail8 = this.bones.Tail7.Find("Tail8");
		}
		this.bones.Neck = this.bones.SpineUpper.Find("Neck");
		this.bones.Neck_inverter = this.bones.Neck.Find("NeckInvert");
		this.bones.Head = this.bones.Neck_inverter.Find("Head");
		this.bones.Jaw = this.bones.Head.Find("Jaw");
		this.bones.MouthCornerL = this.bones.Jaw.Find("MouthCorner_L");
		this.bones.MouthCornerR = this.bones.Jaw.Find("MouthCorner_R");
		this.bones.Tongue0 = this.bones.Jaw.Find("Tongue0");
		this.bones.Tongue1 = this.bones.Tongue0.Find("Tongue1");
		this.bones.Tongue2 = this.bones.Tongue1.Find("Tongue2");
		this.bones.TopLip = this.bones.Head.Find("TopLip");
		this.bones.Nose = this.bones.Head.Find("Nose");
		this.bones.Cheek_L = this.bones.Head.Find("Cheek_L");
		this.bones.UpperEyelid_L = this.bones.Head.Find("UpperEyelid_L");
		this.bones.LowerEyelid_L = this.bones.Head.Find("LowerEyelid_L");
		this.bones.Eye_L = this.bones.Head.Find("Eye_L");
		this.bones.Pupil_L = this.bones.Eye_L.Find("Pupil_L");
		this.bones.Ear0_R = this.bones.Head.Find("Ear0_R");
		this.bones.Ear1_R = this.bones.Ear0_R.Find("Ear1_R");
		this.bones.Ear2_R = this.bones.Ear1_R.Find("Ear2_R");
		this.bones.Ear3_R = this.bones.Ear2_R.Find("Ear3_R");
		this.bones.Ear4_R = this.bones.Ear3_R.Find("Ear4_R");
		this.bones.Ear5_R = this.bones.Ear4_R.Find("Ear5_R");
		this.bones.Ear0_L = this.bones.Head.Find("Ear0_L");
		this.bones.Ear1_L = this.bones.Ear0_L.Find("Ear1_L");
		this.bones.Ear2_L = this.bones.Ear1_L.Find("Ear2_L");
		this.bones.Ear3_L = this.bones.Ear2_L.Find("Ear3_L");
		this.bones.Ear4_L = this.bones.Ear3_L.Find("Ear4_L");
		this.bones.Ear5_L = this.bones.Ear4_L.Find("Ear5_L");
		this.bones.EyebrowInner_L = this.bones.Head.Find("EyebrowInner_L");
		this.bones.EyebrowOuter_L = this.bones.Head.Find("EyebrowOuter_L");
		this.bones.Cheek_R = this.bones.Head.Find("Cheek_R");
		this.bones.UpperEyelid_R = this.bones.Head.Find("UpperEyelid_R");
		this.bones.LowerEyelid_R = this.bones.Head.Find("LowerEyelid_R");
		this.bones.Eye_R = this.bones.Head.Find("Eye_R");
		this.bones.Pupil_R = this.bones.Eye_R.Find("Pupil_R");
		this.bones.EyebrowInner_R = this.bones.Head.Find("EyebrowInner_R");
		this.bones.EyebrowOuter_R = this.bones.Head.Find("EyebrowOuter_R");
		this.bones.Breast_R = this.bones.SpineUpper.Find("Breast_R");
		this.bones.Breast_L = this.bones.SpineUpper.Find("Breast_L");
		this.bones.InjectionPoint = this.bones.Butt_L.Find("InjectionPoint");
		this.interactionHotspots = new List<Transform>();
		this.interactionHotspotTriggers = new List<InteractionTrigger>();
		this.interactionHotspotColliders = new List<Collider>();
		this.interactionHotspots.Add(this.bones.Footpad_L.Find("InteractionTrigger_footl"));
		this.interactionHotspots.Add(this.bones.Footpad_R.Find("InteractionTrigger_footr"));
		this.interactionHotspots[0].SetParent(this.GO.transform);
		this.interactionHotspots[1].SetParent(this.GO.transform);
		this.interactionHotspots.Add(this.bones.Root.Find("InteractionTrigger_tailhole"));
		this.interactionHotspots.Add(this.bones.Ballsack1.Find("InteractionTrigger_balls"));
		this.interactionHotspots[3].SetParent(this.GO.transform);
		this.interactionHotspots.Add(this.bones.Clit.Find("InteractionTrigger_clit"));
		this.interactionHotspots.Add(this.bones.Pubic.Find("InteractionTrigger_vagina"));
		this.interactionHotspots.Add(this.bones.Penis4.Find("InteractionTrigger_penis"));
		this.interactionHotspots.Add(this.bones.Breast_L.Find("InteractionTrigger_breastl"));
		this.interactionHotspots.Add(this.bones.Breast_R.Find("InteractionTrigger_breastr"));
		this.interactionHotspots.Add(this.bones.Head.Find("InteractionTrigger_mouth"));
		for (int i = 0; i < this.interactionHotspots.Count; i++)
		{
			this.interactionHotspotTriggers.Add(((Component)this.interactionHotspots[i]).GetComponent<InteractionTrigger>());
			((Component)this.interactionHotspots[i]).GetComponent<InteractionTrigger>().character = this;
			UnityEngine.Object.Destroy(this.interactionHotspots[i].gameObject.GetComponent<Collider>());
		}
		this.GOHandTargetL = this.GO.transform.Find("HandTargetL");
		this.GOHandTargetR = this.GO.transform.Find("HandTargetR");
		this.GOElbowTargetL = this.GO.transform.Find("ElbowTargetL");
		this.GOElbowTargetR = this.GO.transform.Find("ElbowTargetR");
		this.defaultLeftFootTarget = new GameObject("defaultLeftFootTarget").transform;
		this.defaultLeftFootTarget.SetParent(this.GO.transform);
		this.defaultRightFootTarget = new GameObject("defaultRightFootTarget").transform;
		this.defaultRightFootTarget.SetParent(this.GO.transform);
		this.tailholeEntrance = new GameObject("TailholeEntrance");
		this.tailholeEntrance.transform.SetParent(this.bones.Asshole);
		this.tailholeEntrance.transform.localRotation = Quaternion.identity;
		this.tailholeEntrance.transform.localScale = Vector3.one;
		this.tailholeEntrance.transform.SetParent(this.bones.Root);
		this.v3.x = 0.038f;
		this.v3.y = 0f;
		this.v3.z = 0.096f;
		this.tailholeEntrance.transform.localPosition = this.v3;
		this.tailholeEntrance.transform.SetParent(this.bones.Asshole);
		this.startPosition_handLlocal = this.GO.transform.InverseTransformPoint(this.bones.Hand_L.position);
		this.startPosition_handRlocal = this.GO.transform.InverseTransformPoint(this.bones.Hand_R.position);
		this.startPosition_headlocal = this.GO.transform.InverseTransformPoint(this.bones.Head.position);
		this.startPosition_tailholelocal = this.GO.transform.InverseTransformPoint(this.tailholeEntrance.transform.position);
		this.startPosition_rootlocal = this.GO.transform.InverseTransformPoint(this.bones.Root.position);
	}

	public void setPose(string poseName)
	{
		this.animator.SetTrigger("Idle");
		if (poseName != "Idle")
		{
			this.animator.SetTrigger(poseName);
		}
	}

	public void updateCameraFocusPoint()
	{
		this.v3 = this.bones.Armature.InverseTransformPoint(this.bones.SpineUpper.position);
		if (this.game.firstpersonTransition > 0f)
		{
			this.v3 -= this.v3 * this.game.firstpersonTransition;
			this.v3 += this.bones.Armature.InverseTransformPoint(this.bones.Head.position) * this.game.firstpersonTransition;
		}
		Transform transform = this.cameraFocusPoint;
		transform.localPosition += (this.v3 - this.cameraFocusPoint.localPosition) * this.cap(Time.deltaTime * 12f, 0f, 1f);
	}

	public void processAnimation()
	{
		if (this.uid == this.game.headshotSubjectUID)
		{
			if (this.game.recentThinking <= 0f)
			{
				this.setPose("PhotoSmirk");
				this.curFacialAnimation = FacialExpression.get("happy");
				this.animator.SetInteger("FacialExpression", this.curFacialAnimation);
			}
		}
		else
		{
			this.curFacialAnimation = FacialExpression.get(this.facialExpression);
			this.animator.SetInteger("FacialExpression", this.curFacialAnimation);
			if (this.furniturePose == string.Empty)
			{
				this.furnitureRootSnap -= Time.deltaTime * 7f;
				if (this.delayedFurnitureUnphysicsTime > 0f)
				{
					this.delayedFurnitureUnphysicsTime -= Time.deltaTime;
					if (this.delayedFurnitureUnphysicsTime <= 0f)
					{
						this.unignoreCollisions(this.delayedFurnitureUnphysics);
					}
				}
			}
			else
			{
				this.animator.SetBool(this.furniturePose, true);
				this.furnitureRootSnap += Time.deltaTime * 7f;
			}
			if (this.boundPose != string.Empty)
			{
				this.setPose(this.boundPose);
				this.archBack(this.apparatus.backArchAmount);
				this.grindRoot(this.apparatus.rootGrindAmount);
				Transform root = this.bones.Root;
				root.position += this.apparatus.rootPoint.position - this.bones.Root.position;
			}
			else if (this.furniturePose != string.Empty)
			{
				this.setPose("Idle");
				this.furnitureRootSnap = this.cap(this.furnitureRootSnap, 0f, 1f);
				Vector3 zero = Vector3.zero;
				if (this.furniturePose == "Suspended")
				{
					zero.y = Mathf.Cos(Time.time * 0.4f) * 0.3f;
				}
				Transform root2 = this.bones.Root;
				root2.position += this.furnitureRoot.position - this.bones.Root.position + zero * this.furnitureRootSnap;
				this.v3 = this.GO.transform.eulerAngles;
				this.v3.y = this.furniture.transform.eulerAngles.y;
				this.GO.transform.eulerAngles = this.v3;
			}
			else if (this.controlledByPlayer)
			{
				if (this.mirrorSwitchDelay > 0f)
				{
					if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("previewArousal")).GetComponent<Slider>().val > 0.05f || this.interactingWithSelf)
					{
						this.mirrorSwitchDelay -= Time.deltaTime * 10f;
					}
					else
					{
						this.mirrorSwitchDelay -= Time.deltaTime;
					}
				}
				if (this.controlledByPlayer && !this.autoWalking && this.game.customizingCharacter && this.game.PC() == this.game.OC())
				{
					if (Game.gameInstance.customizeCharacterPage == 4 || Game.gameInstance.customizeCharacterPage == 51)
					{
						this.setPose("ArmsOutForEditing");
					}
					else if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("ddPose")).GetComponent<Dropdown>().value == 4)
					{
						this.setPose("ArmsOutForEditing");
					}
					else if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("ddPose")).GetComponent<Dropdown>().value == 1)
					{
						this.setPose("ShoweringOpenWide");
					}
					else if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("ddPose")).GetComponent<Dropdown>().value == 2)
					{
						this.setPose("ShoweringLegLift");
					}
					else if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("ddPose")).GetComponent<Dropdown>().value == 3)
					{
						this.setPose("Showering1Spread");
					}
					else if (this.mirrorSwitchDelay <= 0f)
					{
						this.mirrorSwitchDelay = 3f + UnityEngine.Random.value * 5f;
						this.mirroredSwitch = !this.mirroredSwitch;
						if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("previewArousal")).GetComponent<Slider>().val > 0.05f || this.interactingWithSelf)
						{
							this.setPose("Showering1");
						}
						else if (((Component)Game.gameInstance.UI.transform.Find("CustomizationCamControls").Find("previewArousal")).GetComponent<Slider>().val > 0.05f || this.interactingWithSelf)
						{
							this.setPose("Showering1");
						}
						else if (this.mirroredSwitch)
						{
							this.setPose("Showering0");
						}
						else
						{
							this.setPose("Showering0_mirror");
						}
					}
				}
			}
			else if ((UnityEngine.Object)this.npcData != (UnityEngine.Object)null && this.npcData.idleAnimation != string.Empty)
			{
				this.setPose(this.npcData.idleAnimation);
			}
		}
		this.faceControlledByAnimation = false;
		this.allowLimitedEyeMovement = false;
		this.feetInAir = false;
		this.game.lockedPosition = (this.controlledByPlayer && (this.game.waitingOnPostCustomizationSnapshot || RacknetMultiplayer.anyRacknetUIopen || this.game.characterSelectorOpen || this.game.lightingControlPanelOpen || (this.game.inventoryOpen && this.game.interactingWithBags.Count > 0) || this.game.preferencesMenuOpen || (this.game.anythingLoading && !UserSettings.data.mod_hideLoadScreens) || this.game.characterSelectorOpen || this.game.PC().interactionSubject != null || this.game.creditsOpen || this.game.shopOpen || this.game.chemicalSynthesisMenuOpen));
		if (this.controlledByPlayer && Inventory.getCharVar("startingStuffGiven") == 0f && Inventory.getCharVar("secretaryGreeted") == 1f)
		{
			this.game.lockedPosition = true;
		}
		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		if (!currentAnimatorStateInfo.IsName("Sit"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.IsName("Suspended"))
			{
				goto IL_0716;
			}
			goto IL_0729;
		}
		goto IL_0716;
		IL_08d2:
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		if (!currentAnimatorStateInfo.IsName("Showering1"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.IsName("Showering1Spread"))
			{
				goto IL_0908;
			}
			goto IL_0914;
		}
		goto IL_0908;
		IL_0716:
		this.game.lockedPosition = true;
		this.feetInAir = true;
		goto IL_0729;
		IL_0882:
		this.game.lockedPosition = true;
		this.faceControlledByAnimation = true;
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		int num;
		if (!currentAnimatorStateInfo.IsName("ShoweringLegLift"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			num = (currentAnimatorStateInfo.IsName("ShoweringOpenWide") ? 1 : 0);
		}
		else
		{
			num = 1;
		}
		this.allowLimitedEyeMovement = ((byte)num != 0);
		goto IL_08d2;
		IL_0914:
		if (!this.game.customizingCharacter)
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (!currentAnimatorStateInfo.IsName("Showering0"))
			{
				currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.IsName("Showering0_mirror"))
				{
					currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
					if (!currentAnimatorStateInfo.IsName("Showering1"))
					{
						currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
						if (!currentAnimatorStateInfo.IsName("Showering1Spread"))
						{
							currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
							if (!currentAnimatorStateInfo.IsName("ShoweringLegLift"))
							{
								currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
								if (currentAnimatorStateInfo.IsName("ShoweringOpenWide"))
								{
									goto IL_09c9;
								}
								goto IL_09d4;
							}
						}
					}
				}
			}
			goto IL_09c9;
		}
		goto IL_09d4;
		IL_0908:
		this.game.lockedPosition = true;
		goto IL_0914;
		IL_0729:
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.IsName("PhotoSmirk"))
		{
			this.game.lockedPosition = true;
			this.faceControlledByAnimation = true;
			this.allowLimitedEyeMovement = true;
		}
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		if (!currentAnimatorStateInfo.IsName("RackChair"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (!currentAnimatorStateInfo.IsName("Sideways"))
			{
				currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.IsName("Stocks"))
				{
					currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
					if (!currentAnimatorStateInfo.IsName("FacedownSwing"))
					{
						currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
						if (!currentAnimatorStateInfo.IsName("TableStraps"))
						{
							currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
							if (currentAnimatorStateInfo.IsName("UpsideDown"))
							{
								goto IL_0803;
							}
							goto IL_0816;
						}
					}
				}
			}
		}
		goto IL_0803;
		IL_0816:
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		if (!currentAnimatorStateInfo.IsName("Showering0"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (!currentAnimatorStateInfo.IsName("Showering0_mirror"))
			{
				currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
				if (!currentAnimatorStateInfo.IsName("ShoweringLegLift"))
				{
					currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.IsName("ShoweringOpenWide"))
					{
						goto IL_0882;
					}
					goto IL_08d2;
				}
			}
		}
		goto IL_0882;
		IL_09c9:
		this.setPose("Idle");
		goto IL_09d4;
		IL_0803:
		this.game.lockedPosition = true;
		this.feetInAir = true;
		goto IL_0816;
		IL_09d4:
		currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		int num2;
		if (!currentAnimatorStateInfo.IsName("Idle"))
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			num2 = (currentAnimatorStateInfo.IsName("IdlePlantigrade") ? 1 : 0);
		}
		else
		{
			num2 = 1;
		}
		this.idleAnimation = ((byte)num2 != 0);
		int num3;
		if (this.animationPauseTime > 0)
		{
			currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			num3 = (currentAnimatorStateInfo.IsName("ArmsOutForEditing") ? 1 : 0);
		}
		else
		{
			num3 = 0;
		}
		this.animationPaused = ((byte)num3 != 0);
		if (this.animationPauseTime > 0)
		{
			this.animationPauseTime--;
		}
		if (this.animationPaused)
		{
			this.animator.speed = 0f;
		}
		else
		{
			this.animator.speed = 1f;
		}
	}

	public void pauseAnimation()
	{
		this.animationPauseTime = 2;
	}

	public void processHide()
	{
		this.resetTail();
		this.resetAllFloppyBodies();
		this.reparentAllFloppyBodies();
	}

	public void unparentAllFloppyBodies()
	{
		this.unparentTail();
		this.unparentEars();
		this.unparentBalls();
		this.unparentPenis();
	}

	public void reparentAllFloppyBodies()
	{
		this.reparentTail();
		this.reparentEars();
		this.reparentBalls();
		this.reparentPenis();
	}

	public void resetAllFloppyBodies()
	{
		this.resetTail();
		this.resetEars();
		this.resetBalls();
		this.resetPenis();
		this.resetBoobs();
		this.resetBelly();
		this.resetButt();
		this.lastTailSize = -1f;
	}

	public void processUnhide()
	{
		this.resetAllFloppyBodies();
		if ((UnityEngine.Object)this.furniture != (UnityEngine.Object)null)
		{
			this.ignoreCollisions(this.furniture.transform, false, null);
		}
		if ((UnityEngine.Object)this.furniture != (UnityEngine.Object)null)
		{
			this.animator.SetBool(this.furniturePose, true);
		}
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			this.ignoreCollisions(this.apparatus.transform, false, null);
		}
	}

	public void applyGravityToAttachedFloppyParts()
	{
		if (this.suspendedAnim)
		{
			this.testTubeTurbulence.x = Mathf.Cos(Time.time * 10.54f) * 20f;
			this.testTubeTurbulence.y = Mathf.Cos(Time.time * 10.17f) * 20f + 15f;
			this.testTubeTurbulence.z = Mathf.Cos(Time.time * 10.39f) * 20f;
			this.testTubeTurbulence *= 0.05f;
			this.gravityModifier = 0.1f;
		}
		else
		{
			this.testTubeTurbulence = Vector3.zero;
			this.gravityModifier = 0.2f;
		}
		for (int i = 0; i < this.tailLength_act; i++)
		{
			this.tailRigidBodies[i].AddForce(Physics.gravity * this.gravityModifier * 6f + this.testTubeTurbulence, ForceMode.Acceleration);
		}
		for (int j = 0; j < this.earRigidbodies.Length; j++)
		{
			this.earRigidbodies[j].AddForce(Physics.gravity * this.gravityModifier * 0.7f + this.testTubeTurbulence * 0.6f, ForceMode.Acceleration);
		}
		for (int k = 0; k < this.ballRigidbodies.Length; k++)
		{
			this.ballRigidbodies[k].AddForce(Physics.gravity * this.gravityModifier + this.testTubeTurbulence, ForceMode.Acceleration);
		}
		for (int l = 0; l < this.penisRigidbodies.Length; l++)
		{
			this.penisRigidbodies[l].AddForce(Physics.gravity * this.gravityModifier * 0.75f + this.testTubeTurbulence, ForceMode.Acceleration);
		}
		for (int m = 2; m < 4; m++)
		{
			this.boobRigidbodies[m].AddForce(Physics.gravity * this.gravityModifier * 10.5f + this.testTubeTurbulence, ForceMode.Acceleration);
		}
		for (int n = 0; n < this.hairAppendages.Count; n++)
		{
			for (int num = 0; num < this.hairAppendages[n].rigidbodies.Count; num++)
			{
				this.hairAppendages[n].rigidbodies[num].AddForce(Physics.gravity * this.gravityModifier + this.testTubeTurbulence, ForceMode.Acceleration);
			}
		}
		for (int num2 = 0; num2 < this.bellyRigidbodies.Length; num2++)
		{
			this.bellyRigidbodies[num2].AddForce(Physics.gravity * this.gravityModifier * 80f + this.testTubeTurbulence, ForceMode.Acceleration);
		}
		for (int num3 = 0; num3 < this.buttRigidbodies.Length; num3++)
		{
			this.buttRigidbodies[num3].AddForce(Physics.gravity * this.gravityModifier * 0.5f + this.testTubeTurbulence, ForceMode.Acceleration);
		}
	}

	public void determineIKlevelAndSolve()
	{
		if (this.initted && !this.rebuilding)
		{
			if (this.interactionSubject != null && this.distFromCamera < 20f)
			{
				goto IL_002e;
			}
			if (this.controlledByPlayer)
			{
				goto IL_002e;
			}
			if ((this.isInteractionSubject || this.interactionSubject != null || this.controlledByPlayer) && this.distFromCamera < 40f)
			{
				this.solveIK(Mathf.RoundToInt(6f * UserSettings.data.IKquality));
				return;
			}
			if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null && this.distFromCamera < 40f)
			{
				this.solveIK(Mathf.RoundToInt(2f * UserSettings.data.IKquality));
			}
			else if (UserSettings.data.mod_fixNpcCum && this.distFromCamera < 20f)
			{
				this.solveIK(Mathf.RoundToInt(UserSettings.data.IKquality));
			}
			else if (!this.faceControlledByAnimation)
			{
				this.processEyesAndFocus(this.currentlyUsingMouth);
			}
		}
		return;
		IL_002e:
		this.solveIK(Mathf.RoundToInt(6f * UserSettings.data.IKquality));
	}

	public bool FixedUpdate()
	{
		if (Game.thereHasBeenAtLeastOneFixedUpdate)
		{
			return false;
		}
		this.turnCollidersOff();
		this.render();
		this.turnCollidersOn(false);
		return true;
	}

	public void freezePhysics(Transform transform)
	{
		((Component)transform).GetComponent<Rigidbody>().isKinematic = true;
		((Component)transform).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		((Component)transform).GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public void unfreezePhysics(Transform transform)
	{
		((Component)transform).GetComponent<Rigidbody>().isKinematic = false;
	}

	public static void setBlendmode(Material material, string blendMode)
	{
		if (blendMode != null)
		{
			if (!(blendMode == "opaque"))
			{
				if (!(blendMode == "cutout"))
				{
					if (!(blendMode == "fade"))
					{
						if (blendMode == "transparent")
						{
							material.SetInt("_SrcBlend", 1);
							material.SetInt("_DstBlend", 10);
							material.SetInt("_ZWrite", 0);
							material.DisableKeyword("_ALPHATEST_ON");
							material.DisableKeyword("_ALPHABLEND_ON");
							material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
							material.EnableKeyword("_METALLICGLOSSMAP");
							material.EnableKeyword("_NORMALMAP");
							material.EnableKeyword("_EMISSION");
							material.renderQueue = 3000;
						}
					}
					else
					{
						material.SetInt("_SrcBlend", 5);
						material.SetInt("_DstBlend", 10);
						material.SetInt("_ZWrite", 0);
						material.DisableKeyword("_ALPHATEST_ON");
						material.EnableKeyword("_ALPHABLEND_ON");
						material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
						material.EnableKeyword("_METALLICGLOSSMAP");
						material.EnableKeyword("_NORMALMAP");
						material.EnableKeyword("_EMISSION");
						material.renderQueue = 3000;
					}
				}
				else
				{
					material.SetInt("_SrcBlend", 1);
					material.SetInt("_DstBlend", 0);
					material.SetInt("_ZWrite", 1);
					material.EnableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.EnableKeyword("_METALLICGLOSSMAP");
					material.EnableKeyword("_NORMALMAP");
					material.EnableKeyword("_EMISSION");
					material.renderQueue = 2450;
				}
			}
			else
			{
				material.SetInt("_SrcBlend", 1);
				material.SetInt("_DstBlend", 0);
				material.SetInt("_ZWrite", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHABLEND_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.EnableKeyword("_METALLICGLOSSMAP");
				material.EnableKeyword("_NORMALMAP");
				material.EnableKeyword("_EMISSION");
				material.renderQueue = -1;
			}
		}
	}

	public void turnCollidersOff()
	{
		if (this.initted && !this.rebuilding && (!this.hidden || this.collidersWereOn) && this.collidersWereOn)
		{
			this.needColliderRebuild = false;
			for (int i = 0; i < this.colliders.Length; i++)
			{
				if ((UnityEngine.Object)this.colliders[i] == (UnityEngine.Object)null)
				{
					this.needColliderRebuild = true;
				}
				else
				{
					this.originalColliderStatus[i] = this.colliders[i].enabled;
					if (this.colliders[i].enabled)
					{
						this.colliders[i].enabled = false;
					}
				}
			}
			this.collidersWereOn = false;
			if (this.needColliderRebuild)
			{
				this.rebuildColliders();
				this.needColliderRebuild = false;
			}
		}
	}

	public void turnCollidersOn(bool becauseWeAreRebuildingList = false)
	{
		if (this.initted && !this.rebuilding && this.colliders != null && (becauseWeAreRebuildingList || (!this.hidden && (this.controlledByPlayer || this.interactionSubject != null || this.isInteractionSubject || UserSettings.data.mod_fixNpcCum))) && (!this.collidersWereOn | becauseWeAreRebuildingList))
		{
			this.needColliderRebuild = false;
			for (int i = 0; i < this.colliders.Length; i++)
			{
				if ((UnityEngine.Object)this.colliders[i] == (UnityEngine.Object)null)
				{
					this.needColliderRebuild = true;
				}
				else if (this.originalColliderStatus[i] && !this.colliders[i].enabled)
				{
					this.colliders[i].enabled = true;
				}
			}
			this.collidersWereOn = true;
			if (this.needColliderRebuild && !becauseWeAreRebuildingList)
			{
				this.rebuildColliders();
				this.needColliderRebuild = false;
			}
		}
	}

	public void rebuildColliders()
	{
		this.turnCollidersOn(true);
		this.colliders = this.GO.GetComponentsInChildren<Collider>();
		List<Collider> list = this.colliders.ToList();
		for (int i = 0; i < this.tailbones.Length; i++)
		{
			list.Add(this.tailColliders[i]);
		}
		for (int j = 0; j < this.earbones.Length; j++)
		{
			if ((UnityEngine.Object)this.earColliders[j] != (UnityEngine.Object)null)
			{
				list.Add(this.earColliders[j]);
			}
		}
		for (int k = 0; k < this.boobColliders.Length; k++)
		{
			if ((UnityEngine.Object)this.boobColliders[k] != (UnityEngine.Object)null)
			{
				list.Add(this.boobColliders[k]);
			}
		}
		for (int l = 0; l < this.ballColliders.Length; l++)
		{
			if ((UnityEngine.Object)this.ballColliders[l] != (UnityEngine.Object)null)
			{
				list.Add(this.ballColliders[l]);
			}
		}
		for (int m = 0; m < this.scrotumColliders.Length; m++)
		{
			if ((UnityEngine.Object)this.scrotumColliders[m] != (UnityEngine.Object)null)
			{
				list.Add(this.scrotumColliders[m]);
			}
		}
		for (int n = 0; n < this.penisColliders.Length; n++)
		{
			if ((UnityEngine.Object)this.penisColliders[n] != (UnityEngine.Object)null)
			{
				list.Add(this.penisColliders[n]);
			}
		}
		list.Add(this.penisbones[4].gameObject.GetComponent<SphereCollider>());
		for (int num = 0; num < this.hairAppendages.Count; num++)
		{
			if (this.hairAppendages[num].built)
			{
				Collider[] componentsInChildren = this.hairAppendages[num].appendage.GetComponentsInChildren<Collider>();
				for (int num2 = 0; num2 < componentsInChildren.Length; num2++)
				{
					list.Add(componentsInChildren[num2]);
				}
			}
		}
		for (int num3 = 0; num3 < list.Count; num3++)
		{
			if (list[num3].name.IndexOf("InteractionTrigger") != -1 || list[num3].name.IndexOf("MovementTarget") != -1)
			{
				list.RemoveAt(num3);
				num3--;
			}
		}
		this.colliders = list.ToArray();
		this.originalColliderStatus = new bool[this.colliders.Length];
		for (int num4 = 0; num4 < this.colliders.Length; num4++)
		{
			this.originalColliderStatus[num4] = this.colliders[num4].enabled;
		}
	}

	public void fadeOutCharacter(float amount)
	{
		this.fadingOutCharacter = true;
		this.fadeOutCharacterAmount = amount;
	}

	public void render()
	{
		if ((UnityEngine.Object)this.GO != (UnityEngine.Object)null)
		{
			this.GO.SetActive(!this.hidden);
		}
		if (this.hidden && !this.wasHidden)
		{
			this.processHide();
		}
		if (!this.hidden)
		{
			if (this.initted)
			{
				this.lineOfSightToCamera = false;
				Vector3 vector = this.game.mainCam.transform.InverseTransformPoint(this.bones.Root.position);
				this.inFrontOfCamera = (vector.z > 0f);
				if (this.inFrontOfCamera)
				{
					this.v3 = this.bones.Root.position - this.game.mainCam.transform.position;
					this.lineOfSightToCamera = !Physics.Raycast(this.game.mainCam.transform.position, this.v3.normalized, this.v3.magnitude, LayerMask.GetMask("StaticObjects"));
				}
				this.distFromCamera = (this.bones.Root.position - this.game.mainCam.transform.position).magnitude;
			}
			bool flag = false;
			this.tailGhosted = false;
			if (this.fadingOutCharacter)
			{
				this.fadingOutCharacter = false;
				this.wasFadingOutCharacter = true;
			}
			else
			{
				this.wasFadingOutCharacter = false;
			}
			for (int i = 0; i < this.parts.Count; i++)
			{
				this.parts[i].transform.localPosition = Vector3.zero;
				this.pieceHidden = (this.controlledByPlayer && this.interactionSubject != null && this.game.interactionZoom > 0.25f);
				if (this.pieceHidden && i == this.handsPieceIndex && (this.game.curTool == "hand" || this.interactingWithSelf || this.currentlyUsingHandL || this.currentlyUsingHandR))
				{
					this.pieceHidden = false;
				}
				if (this.pieceHidden && i == this.headPieceIndex && this.game.curTool == "mouth")
				{
					this.pieceHidden = false;
				}
				if (this.pieceHidden && (i == this.penisPieceIndex || i == this.ballsPieceIndex) && (this.game.curTool == "penis" || this.interactingWithSelf))
				{
					this.pieceHidden = false;
				}
				if (this.pieceHidden && i == this.vaginaPieceIndex && (this.game.curTool == "vagina" || this.interactingWithSelf))
				{
					this.pieceHidden = false;
				}
				if (this.currentlyUsingButt || this.currentlyUsingVagina)
				{
					this.pieceHidden = false;
				}
				bool flag2 = this.game.interactionZoom > 0.75f && this.controlledByPlayer && this.interactionSubject != null;
				if (flag2)
				{
					this.pieceHidden = true;
				}
				if (this.controlledByPlayer && this.interactionSubject != null && this.game.freeCam)
				{
					this.pieceHidden = false;
				}
				if (i == this.tailPieceIndex && !this.pieceHidden && UserSettings.data.ghostTailsDuringSex)
				{
					if (this.controlledByPlayer && this.interactionSubject != null)
					{
						goto IL_03cc;
					}
					if (this.isInteractionSubject)
					{
						goto IL_03cc;
					}
				}
				goto IL_0427;
				IL_03cc:
				float num = Vector3.Angle(this.bones.SpineLower.forward, this.game.mainCam.transform.position - this.bones.SpineLower.position);
				if (num < 95f)
				{
					this.pieceHidden = true;
					this.tailGhosted = true;
				}
				goto IL_0427;
				IL_0856:
				if (this.wasFadingOutCharacter)
				{
					this.rimColor *= 1f - this.fadeOutCharacterAmount;
					this.rimAmount[i] = 0f;
				}
				this.rimColor *= 0.2f + this.rimAmount[i] * 0.8f;
				this.parts[i].GetComponent<SkinnedMeshRenderer>().enabled = (this.fadeAmount[i] < 1f);
				this.parts[i].GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", this.rimColor);
				this.parts[i].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_FresnelStrength", this.rimAmount[i] * 2f);
				if (i == this.bodyPieceIndex)
				{
					for (int j = 0; j < this.clothingPiecesEquipped.Count; j++)
					{
						this.clothingPiecesEquipped[j].GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", this.rimColor);
						this.clothingPiecesEquipped[j].GetComponent<SkinnedMeshRenderer>().material.SetFloat("_FresnelStrength", this.rimAmount[i] * 2f);
						this.rimColor.a = 0f;
					}
				}
				this.needFadeUpdate = false;
				continue;
				IL_07dd:
				this.rimAmount[i] += Time.deltaTime * 0.5f;
				if (this.rimAmount[i] > 1f)
				{
					this.rimAmount[i] = 1f;
				}
				goto IL_0856;
				IL_0427:
				if (!UserSettings.data.ghostBodyDuringSex)
				{
					this.pieceHidden = false;
				}
				if (this.wasFadingOutCharacter)
				{
					this.fadeAmount[i] = this.fadeOutCharacterAmount;
					this.pieceHidden = (this.fadeOutCharacterAmount >= 1f);
					this.rimAmount[i] = 0f;
				}
				if (this.pieceHidden)
				{
					if (this.fadeAmount[i] < 0.98f)
					{
						this.fadeAmount[i] += Time.deltaTime * 2.5f;
						if (this.fadeAmount[i] >= 0.98f)
						{
							this.fadeAmount[i] = 0.98f;
						}
						this.needFadeUpdate = true;
					}
					if (i == this.bodyPieceIndex)
					{
						this.bodyGhosted = true;
					}
					if (i == this.bodyPieceIndex)
					{
						this.bodySuperGhosted = flag2;
					}
					if (i == this.headPieceIndex)
					{
						for (int k = 0; k < this.hairAppendages.Count; k++)
						{
							if (this.hairAppendages[k].built)
							{
								this.hairAppendages[k].appendage.SetActive(false);
							}
						}
					}
				}
				else
				{
					if (this.fadeAmount[i] > 0f)
					{
						this.needFadeUpdate = true;
						this.fadeAmount[i] -= Time.deltaTime * 2.5f;
						if (this.fadeAmount[i] <= 0f)
						{
							this.fadeAmount[i] = 0f;
						}
					}
					if (i == this.bodyPieceIndex)
					{
						this.bodyGhosted = false;
					}
					if (i == this.bodyPieceIndex)
					{
						this.bodySuperGhosted = false;
					}
					if (i == this.headPieceIndex)
					{
						for (int l = 0; l < this.hairAppendages.Count; l++)
						{
							if (this.hairAppendages[l].built)
							{
								this.hairAppendages[l].appendage.SetActive(true);
							}
						}
					}
				}
				if (!(this.fadeAmount[i] > 0f) && !this.needFadeUpdate)
				{
					if (this.wasFaded[i])
					{
						if (this.hasFurLOD)
						{
							for (int m = 0; m < this.furLODs.Count; m++)
							{
								this.furLODs[m].suppressed = false;
							}
						}
						this.parts[i].GetComponent<SkinnedMeshRenderer>().material.shader = RackCharacter.originalCharacterShader;
						RackCharacter.setBlendmode(this.parts[i].GetComponent<SkinnedMeshRenderer>().material, "cutout");
						this.parts[i].GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", this.originalCharacterMatCol);
						if (i == this.bodyPieceIndex)
						{
							for (int n = 0; n < this.clothingPiecesEquipped.Count; n++)
							{
								this.clothingPiecesEquipped[n].GetComponent<SkinnedMeshRenderer>().material.shader = RackCharacter.originalCharacterShader;
								RackCharacter.setBlendmode(this.clothingPiecesEquipped[n].GetComponent<SkinnedMeshRenderer>().material, "cutout");
								this.clothingPiecesEquipped[n].GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", this.originalCharacterMatCol);
							}
							flag = true;
						}
						this.wasFaded[i] = false;
						this.needFadeUpdate = false;
					}
					continue;
				}
				if (!this.originalMatColSaved)
				{
					this.originalCharacterMatCol = ((Component)this.bodyPiece).GetComponent<SkinnedMeshRenderer>().material.GetColor("_Color");
					this.originalMatColSaved = true;
				}
				if (!this.wasFaded[i])
				{
					if (this.hasFurLOD)
					{
						for (int num2 = 0; num2 < this.furLODs.Count; num2++)
						{
							this.furLODs[num2].suppressed = true;
						}
					}
					this.parts[i].GetComponent<SkinnedMeshRenderer>().material.shader = Shader.Find("Ciconia Studio/Effects/Ghost/FastGhost");
					if (i == this.bodyPieceIndex)
					{
						for (int num3 = 0; num3 < this.clothingPiecesEquipped.Count; num3++)
						{
							this.clothingPiecesEquipped[num3].GetComponent<SkinnedMeshRenderer>().material.shader = Shader.Find("Ciconia Studio/Effects/Ghost/FastGhost");
						}
						flag = true;
					}
					this.wasFaded[i] = true;
				}
				this.fadeCol = UnityEngine.Color.white * 0.16f * (1f - this.fadeAmount[i]);
				this.fadeCol.a = 1f - this.fadeAmount[i];
				this.rimColor = this.game.fakeAmbientTargetColor;
				if (this.fadeAmount[i] > 0.15f)
				{
					if (i != this.bodyPieceIndex && i != this.tailPieceIndex && i != this.wingPieceIndex)
					{
						goto IL_07dd;
					}
					if (!flag2)
					{
						goto IL_07dd;
					}
				}
				this.rimAmount[i] -= Time.deltaTime * 15f;
				if (this.rimAmount[i] < 0f)
				{
					this.rimAmount[i] = 0f;
				}
				goto IL_0856;
			}
			if (this.initted)
			{
				if (this.tailGhosted)
				{
					this.bodyPiece.SetBlendShapeWeight(this.bodyPiece.sharedMesh.GetBlendShapeIndex("GhostTail"), 100f);
				}
				else
				{
					this.bodyPiece.SetBlendShapeWeight(this.bodyPiece.sharedMesh.GetBlendShapeIndex("GhostTail"), 0f);
				}
			}
			if (flag)
			{
				this.applyReferenceModifications();
			}
			if (this.initted)
			{
				((Component)this.tailbones[0]).GetComponent<Rigidbody>().inertiaTensor = Vector3.one;
			}
			if (this.initted)
			{
				for (int num4 = 0; num4 < this.tailbones.Length; num4++)
				{
					this.tailbones[num4].gameObject.SetActive(!this.hidden);
				}
			}
			if (this.initted && !this.rebuilding && !this.hidden && !((UnityEngine.Object)this.animator == (UnityEngine.Object)null))
			{
				if (this.wasHidden)
				{
					this.processUnhide();
				}
				if (!Game.allowHandPositioning)
				{
					this.getFingerRots();
				}
				this.processFreezing();
				if (!this.effectivelyFrozen)
				{
					this.animator.Update(Time.deltaTime * 2f);
				}
				if (!Game.allowHandPositioning)
				{
					this.setFingerRots();
				}
				if (this.controlledByPlayer && this.game.firstPersonMode)
				{
					goto IL_0cc0;
				}
				if (this.wasFadingOutCharacter)
				{
					goto IL_0cc0;
				}
				this.animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
				goto IL_0cdd;
			}
			goto IL_0ec4;
		}
		return;
		IL_0cc0:
		this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		goto IL_0cdd;
		IL_0ec4:
		this.wasHidden = this.hidden;
		this.physicsTickCount = 0;
		return;
		IL_0cdd:
		this.updateBaseCoordinates();
		if (this.controlledByPlayer && TestingRoom.editingMode)
		{
			if (!this.usingGhostMat)
			{
				this.usingGhostMat = true;
				this.assignGhostMaterial();
			}
		}
		else if (this.usingGhostMat)
		{
			this.usingGhostMat = false;
			this.assignNormalMaterial();
		}
		if (this.delayedApparatusTime > 0f)
		{
			this.delayedApparatusTime -= Time.deltaTime;
			if (this.delayedApparatusTime <= 0f)
			{
				this.bindToApparatus(this.delayedApparatusBind);
			}
		}
		this.processTexture();
		this.processPleasure();
		this.processChemicalsInSystem();
		this.processPlayerKnowledgeAndInlineDialogue();
		this.processEnjoyment();
		this.processSpecimenProduction();
		this.processThoughtsAndEmotes();
		this.processInteraction();
		this.determineColliderVisibility();
		if (!this.effectivelyFrozen)
		{
			this.processAnimation();
			this.processWrithing();
			this.processBody();
			this.processBreath();
			this.processIK();
			this.processInput();
			this.processMovement();
			this.processInteractionOrigins();
			if (!this.faceControlledByAnimation)
			{
				this.processFaceAndEmotions();
			}
			if (this.faceControlledByAnimation)
			{
				this.animator.SetLayerWeight(1, 0f);
			}
			else
			{
				this.animator.SetLayerWeight(1, 1f);
			}
			this.processWings();
			this.processPubic();
			this.processVagina();
			this.processButt();
			this.processAnus();
			this.processEars();
			this.processBoobs();
			this.processBelly();
			this.processClothing();
			this.processHair();
			this.reparentPenis();
			this.reparentBalls();
			this.processBalls();
			this.processPenis();
			this.unparentBalls();
			this.unparentPenis();
			this.lastPubicPosition = this.bones.Pubic.position;
			this.processTail();
			this.processDrippingAndPrecum();
			this.processWetness();
			this.processHands();
			this.pointFingers();
			this.processHead();
			this.applyGravityToAttachedFloppyParts();
		}
		goto IL_0ec4;
	}

	public float getChemicalCompound(string name)
	{
		int num = -1;
		int num2 = 0;
		while (num2 < this.chemicalsInSystem.Count)
		{
			if (!(this.chemicalsInSystem[num2].name == name))
			{
				num2++;
				continue;
			}
			num = num2;
			break;
		}
		if (num == -1)
		{
			ChemicalCompound chemicalCompound = new ChemicalCompound();
			chemicalCompound.name = name;
			chemicalCompound.amountOwned = 0f;
			this.chemicalsInSystem.Add(chemicalCompound);
			num = this.chemicalsInSystem.Count - 1;
		}
		return this.chemicalsInSystem[num].amountOwned;
	}

	public void addChemicalCompound(string name, float amount)
	{
		int num = -1;
		int num2 = 0;
		while (num2 < this.chemicalsInSystem.Count)
		{
			if (!(this.chemicalsInSystem[num2].name == name))
			{
				num2++;
				continue;
			}
			num = num2;
			break;
		}
		if (num == -1)
		{
			ChemicalCompound chemicalCompound = new ChemicalCompound();
			chemicalCompound.name = name;
			chemicalCompound.amountOwned = 0f;
			this.chemicalsInSystem.Add(chemicalCompound);
			num = this.chemicalsInSystem.Count - 1;
		}
		this.chemicalsInSystem[num].amountOwned += amount;
		if (this.chemicalsInSystem[num].amountOwned < 0f)
		{
			this.chemicalsInSystem[num].amountOwned = 0f;
		}
	}

	public void processChemicalsInSystem()
	{
		for (int i = 0; i < this.chemicalsInSystem.Count; i++)
		{
			if (this.chemicalsInSystem[i].amountOwned > 0f)
			{
				Chemicals.processChemical(this, this.chemicalsInSystem[i].name, this.chemicalsInSystem[i].amountOwned);
			}
		}
		this.artificialSizeChange_target = this.artificialBigness - this.artificialSmallness;
		if (this.artificialSizeChange != this.artificialSizeChange_target)
		{
			this.artificialSizeChange += (this.artificialSizeChange_target - this.artificialSizeChange) * this.cap(Time.deltaTime * 3f, 0f, 1f);
			if (Mathf.Abs(this.artificialSizeChange - this.artificialSizeChange_target) < 0.01f)
			{
				this.artificialSizeChange = this.artificialSizeChange_target;
			}
			this.applyCustomization();
		}
	}

	public void processInteractionOrigins()
	{
		this.startPosition_head = this.GO.transform.TransformPoint(this.startPosition_headlocal);
		this.startPosition_handL = this.GO.transform.TransformPoint(this.startPosition_handLlocal);
		this.startPosition_handR = this.GO.transform.TransformPoint(this.startPosition_handRlocal);
		this.startPosition_tailhole = this.GO.transform.TransformPoint(this.startPosition_tailholelocal);
		this.startPosition_root = this.GO.transform.TransformPoint(this.startPosition_rootlocal);
	}

	public void processFreezing()
	{
		if (this.frozen)
		{
			this.effectivelyFrozen = true;
		}
		else
		{
			this.effectivelyFrozen = false;
		}
	}

	public void determineColliderVisibility()
	{
		for (int i = 0; i < this.interactionHotspotColliders.Count; i++)
		{
			this.interactionHotspotColliders[i].enabled = (this.isInteractionSubject && this.game.mouseMoved && this.game.currentInteraction == null);
		}
		if (this.colliderToggleDelay > 0)
		{
			this.colliderToggleDelay--;
		}
		else
		{
			this.colliderToggleDelay = 50;
			this.bones.AssholeCollider.enabled = false;
			this.bones.Hip_LCollider.enabled = false;
			this.bones.Butt_LCollider.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.UpperLeg_LCollider.enabled = this.controlledByPlayer;
			this.bones.LowerLeg_LCollider.enabled = this.controlledByPlayer;
			this.bones.Foot_LCollider.enabled = false;
			this.bones.Footpad_LCollider.enabled = false;
			this.bones.Hip_RCollider.enabled = false;
			this.bones.Butt_RCollider.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.UpperLeg_RCollider.enabled = this.controlledByPlayer;
			this.bones.LowerLeg_RCollider.enabled = this.controlledByPlayer;
			this.bones.Foot_RCollider.enabled = false;
			this.bones.Footpad_RCollider.enabled = false;
			this.bones.PubicCollider1.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.PubicCollider2.enabled = ((UnityEngine.Object)this.npcData == (UnityEngine.Object)null);
			this.bones.Ballsack1Collider.enabled = ((UnityEngine.Object)this.npcData == (UnityEngine.Object)null);
			this.bones.Nut_LCollider.enabled = ((UnityEngine.Object)this.npcData == (UnityEngine.Object)null);
			this.bones.Nut_RCollider.enabled = ((UnityEngine.Object)this.npcData == (UnityEngine.Object)null);
			this.bones.BallCatcherCollider.enabled = false;
			this.bones.SpineLowerCollider1.enabled = false;
			this.bones.SpineLowerCollider2.enabled = false;
			this.BellyCollider.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.SpineMiddleCollider.enabled = false;
			this.bones.SpineUpperCollider.enabled = false;
			this.bones.Breast_LCollider.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.Breast_RCollider.enabled = (this.controlledByPlayer || this.isInteractionSubject);
			this.bones.NeckCollider.enabled = false;
			this.bones.HeadCollider1.enabled = false;
			this.bones.HeadCollider2.enabled = false;
			this.bones.HeadCollider3.enabled = false;
			this.bones.HeadCollider4.enabled = false;
			this.bones.Ear1_LCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Ear2_LCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Ear4_LCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Ear1_RCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Ear2_RCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Ear4_RCollider.enabled = (this.longEars && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Shoulder_LCollider.enabled = false;
			this.bones.UpperArm_LCollider.enabled = false;
			this.bones.LowerArmLCollider0.enabled = false;
			this.bones.LowerArmLCollider1.enabled = false;
			this.bones.Hand_LCollider.enabled = false;
			this.bones.Finger02_LCollider.enabled = false;
			this.bones.Finger30_LCollider.enabled = false;
			this.bones.Thumb0_LCollider.enabled = false;
			this.bones.Thumb2_LCollider.enabled = false;
			this.bones.Shoulder_RCollider.enabled = false;
			this.bones.UpperArm_RCollider.enabled = false;
			this.bones.LowerArmRCollider0.enabled = false;
			this.bones.LowerArmRCollider1.enabled = false;
			this.bones.Hand_RCollider.enabled = false;
			this.bones.Finger02_RCollider.enabled = false;
			this.bones.Finger30_RCollider.enabled = false;
			this.bones.Thumb0_RCollider.enabled = false;
			this.bones.Thumb2_RCollider.enabled = false;
			this.bones.Wing3_LCollider.enabled = (this.showWings && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Wing4_LCollider.enabled = (this.showWings && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Wing3_RCollider.enabled = (this.showWings && (this.controlledByPlayer || this.isInteractionSubject));
			this.bones.Wing4_RCollider.enabled = (this.showWings && (this.controlledByPlayer || this.isInteractionSubject));
		}
	}

	public void processPlayerKnowledgeAndInlineDialogue()
	{
		if (SexualPreferences.initted && this.ableToExperienceEnjoyment)
		{
			foreach (string item in this.inlineDialogueStaleness.Keys.ToList())
			{
				if (this.inlineDialogueStaleness[item] > 0f)
				{
					Dictionary<string, float> dictionary;
					string key;
					(dictionary = this.inlineDialogueStaleness)[key = item] = dictionary[key] - Time.deltaTime;
					if (this.inlineDialogueStaleness[item] < 0f)
					{
						this.inlineDialogueStaleness[item] = 0f;
					}
				}
			}
			if (this.controlledByPlayer && !this.givenPlayerKnowledgeOfSelf)
			{
				foreach (string item2 in this.confidences.Keys.ToList())
				{
					this.confidencePlayerKnowledge[item2] = 1;
				}
				foreach (string item3 in this.preferences.Keys.ToList())
				{
					this.preferencePlayerKnowledge[item3] = 1;
				}
				this.givenPlayerKnowledgeOfSelf = true;
			}
			this.playerKnowledgeOfInteractionPreferences = 4f;
			this.playerKnowledgeOfAttractionPreferences = 4f;
			this.playerKnowledgeOfExperiencePreferences = 4f;
			this.playerKnowledgeOfSubmissionTraits = 4f;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			foreach (string item4 in this.preferences.Keys.ToList())
			{
				if (!(this.preferences[item4] >= 0.5f - SexualPreferences.preferenceIndifferenceRange) || !(this.preferences[item4] <= 0.5f + SexualPreferences.preferenceIndifferenceRange) || this.controlledByPlayer)
				{
					string category = SexualPreferences.getPreference(item4).category;
					if (this.preferencePlayerKnowledge[item4] != 0)
					{
						if (category != null)
						{
							if (!(category == "PREFERENCE_CATEGORY_ATTRACTION"))
							{
								if (!(category == "PREFERENCE_CATEGORY_EXPERIENCE"))
								{
									if (category == "PREFERENCE_CATEGORY_INTERACTIONS")
									{
										flag = true;
									}
								}
								else
								{
									if (item4 == "submission" || item4 == "degredation")
									{
										flag4 = true;
									}
									flag3 = true;
								}
							}
							else
							{
								flag2 = true;
							}
						}
					}
					else
					{
						float num = 4f;
						if (this.preferenceKnowledge[item4] == 2)
						{
							num = 0f;
						}
						if (this.preferenceKnowledge[item4] == 1)
						{
							num = 2f;
						}
						if (this.preferenceKnowledge[item4] == 0)
						{
							num = 3f;
						}
						if (category != null)
						{
							if (!(category == "PREFERENCE_CATEGORY_ATTRACTION"))
							{
								if (!(category == "PREFERENCE_CATEGORY_EXPERIENCE"))
								{
									if (category == "PREFERENCE_CATEGORY_INTERACTIONS" && num < this.playerKnowledgeOfInteractionPreferences)
									{
										this.playerKnowledgeOfInteractionPreferences = num;
									}
								}
								else
								{
									if ((item4 == "submission" || item4 == "degredation") && num < this.playerKnowledgeOfSubmissionTraits)
									{
										this.playerKnowledgeOfSubmissionTraits = num;
									}
									if (num < this.playerKnowledgeOfExperiencePreferences)
									{
										this.playerKnowledgeOfExperiencePreferences = num;
									}
								}
							}
							else if (num < this.playerKnowledgeOfAttractionPreferences)
							{
								this.playerKnowledgeOfAttractionPreferences = num;
							}
						}
					}
				}
			}
			if (this.playerKnowledgeOfAttractionPreferences == 0f && flag2)
			{
				this.playerKnowledgeOfAttractionPreferences = 1f;
			}
			if (this.playerKnowledgeOfSubmissionTraits == 0f && flag4)
			{
				this.playerKnowledgeOfSubmissionTraits = 1f;
			}
			if (this.playerKnowledgeOfExperiencePreferences == 0f && flag3)
			{
				this.playerKnowledgeOfExperiencePreferences = 1f;
			}
			if (this.playerKnowledgeOfInteractionPreferences == 0f && flag)
			{
				this.playerKnowledgeOfInteractionPreferences = 1f;
			}
			this.playerKnowledgeOfConfidences = 0f;
			foreach (string item5 in this.confidences.Keys.ToList())
			{
				if (this.confidencePlayerKnowledge[item5] != 0)
				{
					this.playerKnowledgeOfConfidences += 4f / (float)this.confidences.Keys.Count;
				}
			}
		}
	}

	public void think(string thought, float weight = 0.2f, bool allAtOnce = false, bool switchUrgently = false)
	{
		if (this.emoteThought != thought && switchUrgently)
		{
			weight = 200f;
			this.emoteTime = 0f;
		}
		if (!this.thoughts.ContainsKey(thought))
		{
			this.thoughts.Add(thought, 0f);
		}
		float num = Time.deltaTime * this.talkativeness / (1f + this.holdMouthOpenTime);
		if (allAtOnce)
		{
			num = 1f;
		}
		this.thoughts[thought] = this.cap(this.thoughts[thought] + weight * num, 0f, 1f);
	}

	public void processThoughtsAndEmotes()
	{
		this.justFinishedMuteTime = false;
		if (this.recentIDprompt > 0f)
		{
			this.recentIDprompt -= Time.deltaTime;
		}
		if (this.muteEmoteTime > 0f)
		{
			this.muteEmoteTime -= Time.deltaTime;
			if (this.muteEmoteTime < 0f)
			{
				this.emoteTime = 0f;
				this.emoteString = string.Empty;
				this.emoteThought = string.Empty;
				this.justFinishedMuteTime = true;
			}
		}
		for (int num = this.arousingThoughts.Count - 1; num >= 0; num--)
		{
			this.arousingThoughts[num].duration -= Time.deltaTime;
			this.arouse(this.arousingThoughts[num].arousal * (1f - Mathf.Pow(Mathf.Abs(this.arousingThoughts[num].duration - this.arousingThoughts[num].durationMidpoint) / this.arousingThoughts[num].durationMidpoint, 2f)));
			if (this.arousingThoughts[num].duration <= 0f)
			{
				this.arousingThoughts.RemoveAt(num);
			}
		}
		if (!this.ableToExperienceEnjoyment && !this.controlledByPlayer)
		{
			return;
		}
		if (this.ableToExperienceEnjoyment)
		{
			this.determineThoughtsFromInteractions();
		}
		if (this.timeSinceDisobedienceOrReprimand < 100f)
		{
			this.timeSinceDisobedienceOrReprimand += Time.deltaTime;
		}
		this.timeSinceLastEmote += Time.deltaTime;
		this.highestThoughtWeight = 0f;
		this.highestThought = string.Empty;
		foreach (string item in this.thoughts.Keys.ToList())
		{
			this.adjustedThoughtWeight = this.thoughts[item];
			if (item == this.emoteThought)
			{
				this.adjustedThoughtWeight *= 0.5f;
			}
			if (this.adjustedThoughtWeight > this.highestThoughtWeight)
			{
				this.highestThoughtWeight = this.adjustedThoughtWeight;
				this.highestThought = item;
			}
		}
		if (this.highestThoughtWeight > 0.1f && this.muteEmoteTime <= 0f && (this.emoteTime <= -10f + this.highestThoughtWeight * 10f || (this.emoteTime < this.highestThoughtWeight * 3f && this.highestThoughtWeight >= 0.9f)))
		{
			this.timeSinceLastEmote = 0f;
			this.emoteThought = this.highestThought;
			this.emoteString = Emotes.formEmoteFromThought(this.highestThought, this);
			string text = string.Empty;
			if (this.highestThought == "apologize_for_disobedience_in_pain")
			{
				this.agreeToAllBrokenCommands();
			}
			if (this.highestThought == "describe_a_preference")
			{
				foreach (string item2 in from item in this.preferences.Keys.ToList()
				orderby UnityEngine.Random.value
				select item)
				{
					if (!item2.Contains("category") && SexualPreferences.getPreference(item2).hideFromPreview != 1 && (SexualPreferences.getPreference(item2).hideFromPreview != 2 || this.showPenis) && (SexualPreferences.getPreference(item2).hideFromPreview != 3 || this.showVagina) && this.preferences[item2] > 0.5f + SexualPreferences.preferenceIndifferenceRange && this.preferenceKnowledge[item2] == 2 && this.preferencePlayerKnowledge[item2] == 0)
					{
						text = item2;
					}
				}
				if (UserSettings.needTutorial("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES"))
				{
					UserSettings.completeTutorial("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES");
					Objectives.completeObjective("NPT_FIND_OUT_WHAT_THE_SUBJECT_LIKES");
					text = ((this.data.genitalType != 1) ? "blowjob_receiving" : "cunnilingus_receiving");
				}
				if (text == string.Empty)
				{
					this.emoteString = Emotes.formEmoteFromThought("you_know_everything", this);
				}
				else
				{
					this.preferencePlayerKnowledge[text] = 1;
					this.emoteString = this.emoteString.Replace("[1]", Localization.getPhrase("PREFERENCE_" + text, string.Empty).ToLower());
				}
			}
			if (this.highestThought == "describe_a_secret_preference")
			{
				foreach (string item3 in from item in this.preferences.Keys.ToList()
				orderby UnityEngine.Random.value
				select item)
				{
					if (!item3.Contains("category") && SexualPreferences.getPreference(item3).hideFromPreview != 1 && (SexualPreferences.getPreference(item3).hideFromPreview != 2 || this.showPenis) && (SexualPreferences.getPreference(item3).hideFromPreview != 3 || this.showVagina) && this.preferences[item3] > 0.5f + SexualPreferences.preferenceIndifferenceRange && this.preferenceKnowledge[item3] == 1 && this.preferencePlayerKnowledge[item3] == 0)
					{
						text = item3;
					}
				}
				if (text == string.Empty)
				{
					this.emoteString = Emotes.formEmoteFromThought("you_know_everything", this);
				}
				else
				{
					this.preferencePlayerKnowledge[text] = 1;
					this.emoteString = this.emoteString.Replace("[1]", Localization.getPhrase("PREFERENCE_" + text, string.Empty).ToLower());
				}
			}
			if (this.highestThought == "describe_a_dislike")
			{
				foreach (string item4 in from item in this.preferences.Keys.ToList()
				orderby UnityEngine.Random.value
				select item)
				{
					if (!item4.Contains("category") && SexualPreferences.getPreference(item4).hideFromPreview != 1 && (SexualPreferences.getPreference(item4).hideFromPreview != 2 || this.showPenis) && (SexualPreferences.getPreference(item4).hideFromPreview != 3 || this.showVagina) && this.preferences[item4] < 0.5f + SexualPreferences.preferenceIndifferenceRange && this.preferenceKnowledge[item4] > 0 && this.preferencePlayerKnowledge[item4] == 0)
					{
						text = item4;
					}
				}
				if (text == string.Empty)
				{
					this.emoteString = Emotes.formEmoteFromThought("you_know_everything", this);
				}
				else
				{
					this.preferencePlayerKnowledge[text] = 1;
					this.emoteString = this.emoteString.Replace("[1]", Localization.getPhrase("PREFERENCE_" + text, string.Empty).ToLower());
				}
			}
			this.emoteTime = 3f + (float)this.emoteString.Length / 60f * 4f;
			if (this.emoteString.Trim() == string.Empty)
			{
				this.emoteTime = 0f;
			}
			if (this.emoteTime > 0f && this.recentIDprompt <= 0f && this.getCommandStatus("less_talking") > 0)
			{
				this.setCommandStatus("less_talking", -1);
			}
			if (this.currentlyUsingMouth || this.suckLock)
			{
				this.muffleSpeech();
			}
			if (this.muffledSpeech)
			{
				if (this.emoteString.Contains("♥♥♥"))
				{
					this.emoteString = Localization.getPhrase("EMOTES_muffled_massive_pleasure" + Mathf.FloorToInt(UnityEngine.Random.value * 100f % 4f).ToString(), string.Empty);
				}
				else if (this.emoteString.Contains("♥"))
				{
					this.emoteString = Localization.getPhrase("EMOTES_muffled_pleasure" + Mathf.FloorToInt(UnityEngine.Random.value * 100f % 4f).ToString(), string.Empty);
				}
				else if (this.emoteString.Contains("✲"))
				{
					this.emoteString = Localization.getPhrase("EMOTES_muffled_anger" + Mathf.FloorToInt(UnityEngine.Random.value * 100f % 4f).ToString(), string.Empty);
				}
				else if (this.emoteString.Contains("..."))
				{
					this.emoteString = Localization.getPhrase("EMOTES_muffled_drawn_out" + Mathf.FloorToInt(UnityEngine.Random.value * 100f % 4f).ToString(), string.Empty);
				}
				else
				{
					this.emoteString = Localization.getPhrase("EMOTES_muffled" + Mathf.FloorToInt(UnityEngine.Random.value * 100f % 4f).ToString(), string.Empty);
				}
				this.muffledSpeech = false;
			}
			this.talkingAnimationTime = this.emoteTime * 0.05f;
			this.col = (UnityEngine.Color.white + UnityEngine.Color.white + this.data.baseColor) / 3f;
			this.col.a = 1f;
			this.emoteBG.color = this.col;
			this.emoteBGframe.color = this.data.baseColor;
			this.emoteTxt.text = this.emoteString;
			this.emoteNameTxt.text = this.data.name + ":";
			this.emote.transform.localScale = Vector3.zero;
			this.emote.SetActive(true);
			this.thoughts[this.highestThought] = 0f;
			foreach (string item5 in this.thoughts.Keys.ToList())
			{
				Dictionary<string, float> dictionary;
				string key;
				(dictionary = this.thoughts)[key = item5] = dictionary[key] * 0.5f;
			}
			if (this.highestThought == "hello1")
			{
				this.helloPhase = 1;
			}
			if (this.highestThought == "hello2")
			{
				this.helloPhase = 2;
			}
			if (this.highestThought == "hello3")
			{
				this.helloPhase = 3;
			}
		}
		if (this.emoteTime > 0f)
		{
			this.v32 = this.game.worldToScreen(this.bones.Head.position, false, 200f);
			if (this.v32.z < 0f)
			{
				this.ibhp = this.game.mainCam.transform.InverseTransformPoint(this.bones.Head.position);
				this.v33.x = this.cap(this.ibhp.x * 10f, -100f, 100f);
				this.v33.y = this.cap(this.ibhp.z * 10f, -30f, -100f);
				this.v33.z = 0f;
			}
			else
			{
				this.v33 = this.v32;
				this.v33.z = 0f;
				if (this.v32.x > 0f)
				{
					this.v33.x += 150f;
				}
				else
				{
					this.v33.x -= 150f;
				}
				if (this.v32.y > 0f)
				{
					this.v33.y += 80f;
				}
				else
				{
					this.v33.y -= 80f;
				}
			}
			Vector3 localScale = this.emote.transform.localScale;
			if (localScale.x < 0.05f)
			{
				this.emote.transform.localPosition = this.v33;
			}
			else
			{
				Transform transform = this.emote.transform;
				transform.localPosition += (this.v33 - this.emote.transform.localPosition) * this.cap(Time.deltaTime * 12f, 0f, 1f);
			}
			Transform transform2 = this.emoteContainer;
			transform2.localPosition += (this.emoteOffset - this.emoteContainer.localPosition) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.v3 = Vector3.one * this.cap(this.emoteTime / 0.5f, 0f, 1f) * (0.95f - this.cap((this.bones.Head.position - this.game.mainCam.transform.position).magnitude / 25f, 0f, 0.3f) - Mathf.Abs(this.v32.x) * 0.001f);
			Transform transform3 = this.emote.transform;
			transform3.localScale += (this.v3 - this.emote.transform.localScale) * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.v3.y = this.v32.x * 0.05f;
			this.v3.z = this.v32.x * 0.02f;
			this.v3.x = 0f;
			this.emote.transform.localEulerAngles = this.v3;
		}
		this.emoteTime -= Time.deltaTime;
		if (this.emoteTime < -99f)
		{
			this.emoteTime = -99f;
		}
		this.emote.SetActive(this.emoteTime > 0f);
	}

	public void muffleSpeech()
	{
		this.muffledSpeech = true;
	}

	public void agreeToAllBrokenCommands()
	{
		foreach (string item in this.commandStatus.Keys.ToList())
		{
			if (!(item == "use_your_mouth") || !(this.timeSinceBrokeMouthHoldCommand > 20f))
			{
				if (this.getCommandStatus(item) < 0)
				{
					this.setCommandStatus(item, 2);
				}
				if (item != null)
				{
					if (!(item == "use_dom_name"))
					{
						if (!(item == "demean_self"))
						{
							if (!(item == "beg"))
							{
								if (!(item == "use_your_mouth"))
								{
									if (item == "less_talking")
									{
										this.talkativeness *= 0.3f;
									}
								}
								else
								{
									this.holdMouthOpenTime = 25f;
								}
							}
							else
							{
								this.think("beg", 1.3f, true, false);
							}
						}
						else
						{
							this.think("demean_myself", 1.5f, true, false);
						}
					}
					else
					{
						this.setCommandStatus("use_dom_name", 1);
					}
				}
			}
		}
	}

	public void determineThoughtsFromInteractions()
	{
		this.requestingChange = 0;
		if (this.lastKnownNumberOfStimulatingInteractions > 0)
		{
			this.timeSpentInteracting += Time.deltaTime;
			this.totalSessionTimeSpentInteracting += Time.deltaTime;
			if (this.timeSpentInteracting > 60f)
			{
				this.timeSpentInteracting = 60f;
			}
			this.helloPhase = 3;
			this.timeSinceInteraction = 0f;
		}
		else
		{
			this.timeSpentInteracting -= this.timeSpentInteracting;
			this.timeSinceInteraction += Time.deltaTime;
			if (this.timeSinceInteraction > 60f)
			{
				this.timeSinceInteraction = 60f;
			}
		}
		if (this.lastKnownInPain)
		{
			this.timeSinceEmoteAboutPain = 0f;
			if (this.timeSinceDisobedienceOrReprimand < 25f)
			{
				if (this.dominance > 0f)
				{
					if (this.timeSinceEmoteAboutPain < 20f)
					{
						this.think("continued_pain", 0.5f, false, false);
					}
					else
					{
						this.think("unexpected_pain", 1f, false, true);
					}
				}
				else
				{
					this.think("apologize_for_disobedience_in_pain", 0.5f, false, false);
					this.recentIDprompt = 0.5f;
				}
				float num = this.cap(0f - this.aggression, 0f, 1f);
				this.dominance -= Time.deltaTime / 30f * (1f + num * 2f);
			}
			else if (this.timeSinceEmoteAboutPain < 20f)
			{
				this.think("continued_pain", 0.5f, false, false);
			}
			else
			{
				this.think("unexpected_pain", 1f, false, true);
			}
			if (this.data.aggression > 0f)
			{
				this.aggression += Time.deltaTime / 10f;
			}
			else
			{
				this.aggression -= Time.deltaTime / 10f;
			}
			if (this.preferences["pain"] < 0.5f)
			{
				this.friendlinessToPlayer -= Time.deltaTime / 3f * (0.5f - this.preferences["pain"]);
			}
		}
		else
		{
			if (this.timeSinceEmoteAboutPain < 99f)
			{
				this.timeSinceEmoteAboutPain += Time.deltaTime;
			}
			if (this.orgasming > 0f)
			{
				this.think("orgasm", 0.5f, false, true);
				foreach (string item in this.thoughts.Keys.ToList())
				{
					this.thoughts[item] = 0f;
				}
			}
			else if (this.proximityToOrgasm > 0.9f && this.refractory <= 0f)
			{
				this.think("near_orgasm", 0.4f, false, true);
				this.wasNearOrgasm = true;
			}
			else
			{
				if (this.stimulation > this.targetStimulation + 0.35f)
				{
					this.think("too_rough", 0.35f, false, true);
					this.requestingChange = -1;
				}
				if (this.wasNearOrgasm)
				{
					if (this.readyToCum)
					{
						this.think("edged", 0.3f, false, false);
					}
					else
					{
						this.think("great_feeling", 0.3f, false, false);
					}
					this.wasNearOrgasm = false;
				}
				else if (this.lastKnownNumberOfStimulatingInteractions > 0)
				{
					if (this.wasRequestingChange != 0 && this.pleasure > 0.05f)
					{
						this.think("that_feels_better", 0.6f, false, false);
						this.wasRequestingChange = 0;
					}
					else if (this.pleasure > 0.4f)
					{
						this.think("great_feeling", 0.2f, false, false);
					}
					else if (this.pleasure > 0.05f)
					{
						this.think("good_feeling", 0.2f, false, false);
					}
					else if (this.pleasure > 0f && this.stimulation < this.targetStimulation)
					{
						if (this.timeSpentInteracting > 20f)
						{
							this.think("teased", 0.35f, false, false);
						}
						else if (this.timeSpentInteracting > 10f)
						{
							this.think("too_slow", 0.35f, false, false);
						}
						else
						{
							this.think("good_feeling", 0.35f, false, false);
						}
						this.requestingChange = 1;
					}
				}
				else if (this.wasRequestingChange != 0)
				{
					if (this.wasRequestingChange == 1)
					{
						this.think("teased", 0.35f, false, false);
					}
					else
					{
						this.think("no_need_to_stop_just_be_gentle", 0.35f, false, false);
					}
					this.wasRequestingChange = 0;
				}
				else if (this.distFromPC < 15f)
				{
					if (this.totalSessionTimeSpentInteracting > 5f)
					{
						if (this.timeSinceInteraction < 25f)
						{
							this.think("why_stop", 0.35f, false, false);
						}
						else
						{
							this.totalSessionTimeSpentInteracting = 0f;
							this.helloPhase = 1;
						}
					}
					else if (this.charactersIcanSee.Count <= 1 || this.isInteractionSubject)
					{
						switch (this.helloPhase)
						{
						case 0:
							this.think("hello1", 0.25f, false, false);
							break;
						case 1:
							this.think("hello2", 0.08f, false, false);
							break;
						case 2:
							this.think("hello3", 0.06f, false, false);
							break;
						case 3:
							this.think("awkward_silence", 0.04f, false, false);
							break;
						}
					}
					else
					{
						this.processWatchingThoughts();
					}
				}
				else if (this.charactersIcanSee.Count > 0)
				{
					this.processWatchingThoughts();
				}
				else if (this.totalSessionTimeSpentInteracting > 5f)
				{
					this.think("anyone_there", 0.05f, false, false);
					if (this.data.aggression > 0f)
					{
						if (this.aggression < 0.6f)
						{
							this.aggression += Time.deltaTime / 120f;
						}
					}
					else if (this.aggression > -0.4f)
					{
						this.aggression -= Time.deltaTime / 120f;
					}
				}
			}
		}
		if (this.requestingChange != 0)
		{
			this.wasRequestingChange = this.requestingChange;
		}
		this.aggression = this.cap(this.aggression, -1f, 1f);
		this.dominance = this.cap(this.dominance, -1f, 1f);
		if (this.aggression < 0.4f)
		{
			this.setCommandStatus("dont_be_rude", 1);
		}
		this.friendlinessToPlayer = this.cap(this.friendlinessToPlayer, 0f, 2f);
	}

	public void processWatchingThoughts()
	{
		this.think("watching", 0.05f, false, false);
	}

	public void getFingerRots()
	{
		this.v3_Finger00_R0 = this.bones.Finger00_L.localEulerAngles;
		this.v3_Finger01_R0 = this.bones.Finger01_L.localEulerAngles;
		this.v3_Finger02_R0 = this.bones.Finger02_L.localEulerAngles;
		this.v3_Finger10_R0 = this.bones.Finger10_L.localEulerAngles;
		this.v3_Finger11_R0 = this.bones.Finger11_L.localEulerAngles;
		this.v3_Finger12_R0 = this.bones.Finger12_L.localEulerAngles;
		this.v3_Finger20_R0 = this.bones.Finger20_L.localEulerAngles;
		this.v3_Finger21_R0 = this.bones.Finger21_L.localEulerAngles;
		this.v3_Finger22_R0 = this.bones.Finger22_L.localEulerAngles;
		this.v3_Finger30_R0 = this.bones.Finger30_L.localEulerAngles;
		this.v3_Finger31_R0 = this.bones.Finger31_L.localEulerAngles;
		this.v3_Finger32_R0 = this.bones.Finger32_L.localEulerAngles;
		this.v3_Thumb0_R0 = this.bones.Thumb0_L.localEulerAngles;
		this.v3_Thumb1_R0 = this.bones.Thumb1_L.localEulerAngles;
		this.v3_Thumb2_R0 = this.bones.Thumb2_L.localEulerAngles;
		this.v3_Finger00_R1 = this.bones.Finger00_R.localEulerAngles;
		this.v3_Finger01_R1 = this.bones.Finger01_R.localEulerAngles;
		this.v3_Finger02_R1 = this.bones.Finger02_R.localEulerAngles;
		this.v3_Finger10_R1 = this.bones.Finger10_R.localEulerAngles;
		this.v3_Finger11_R1 = this.bones.Finger11_R.localEulerAngles;
		this.v3_Finger12_R1 = this.bones.Finger12_R.localEulerAngles;
		this.v3_Finger20_R1 = this.bones.Finger20_R.localEulerAngles;
		this.v3_Finger21_R1 = this.bones.Finger21_R.localEulerAngles;
		this.v3_Finger22_R1 = this.bones.Finger22_R.localEulerAngles;
		this.v3_Finger30_R1 = this.bones.Finger30_R.localEulerAngles;
		this.v3_Finger31_R1 = this.bones.Finger31_R.localEulerAngles;
		this.v3_Finger32_R1 = this.bones.Finger32_R.localEulerAngles;
		this.v3_Thumb0_R1 = this.bones.Thumb0_R.localEulerAngles;
		this.v3_Thumb1_R1 = this.bones.Thumb1_R.localEulerAngles;
		this.v3_Thumb2_R1 = this.bones.Thumb2_R.localEulerAngles;
	}

	public void setFingerRots()
	{
		this.bones.Finger00_L.localEulerAngles = this.v3_Finger00_R0;
		this.bones.Finger01_L.localEulerAngles = this.v3_Finger01_R0;
		this.bones.Finger02_L.localEulerAngles = this.v3_Finger02_R0;
		this.bones.Finger10_L.localEulerAngles = this.v3_Finger10_R0;
		this.bones.Finger11_L.localEulerAngles = this.v3_Finger11_R0;
		this.bones.Finger12_L.localEulerAngles = this.v3_Finger12_R0;
		this.bones.Finger20_L.localEulerAngles = this.v3_Finger20_R0;
		this.bones.Finger21_L.localEulerAngles = this.v3_Finger21_R0;
		this.bones.Finger22_L.localEulerAngles = this.v3_Finger22_R0;
		this.bones.Finger30_L.localEulerAngles = this.v3_Finger30_R0;
		this.bones.Finger31_L.localEulerAngles = this.v3_Finger31_R0;
		this.bones.Finger32_L.localEulerAngles = this.v3_Finger32_R0;
		this.bones.Thumb0_L.localEulerAngles = this.v3_Thumb0_R0;
		this.bones.Thumb1_L.localEulerAngles = this.v3_Thumb1_R0;
		this.bones.Thumb2_L.localEulerAngles = this.v3_Thumb2_R0;
		this.bones.Finger00_R.localEulerAngles = this.v3_Finger00_R1;
		this.bones.Finger01_R.localEulerAngles = this.v3_Finger01_R1;
		this.bones.Finger02_R.localEulerAngles = this.v3_Finger02_R1;
		this.bones.Finger10_R.localEulerAngles = this.v3_Finger10_R1;
		this.bones.Finger11_R.localEulerAngles = this.v3_Finger11_R1;
		this.bones.Finger12_R.localEulerAngles = this.v3_Finger12_R1;
		this.bones.Finger20_R.localEulerAngles = this.v3_Finger20_R1;
		this.bones.Finger21_R.localEulerAngles = this.v3_Finger21_R1;
		this.bones.Finger22_R.localEulerAngles = this.v3_Finger22_R1;
		this.bones.Finger30_R.localEulerAngles = this.v3_Finger30_R1;
		this.bones.Finger31_R.localEulerAngles = this.v3_Finger31_R1;
		this.bones.Finger32_R.localEulerAngles = this.v3_Finger32_R1;
		this.bones.Thumb0_R.localEulerAngles = this.v3_Thumb0_R1;
		this.bones.Thumb1_R.localEulerAngles = this.v3_Thumb1_R1;
		this.bones.Thumb2_R.localEulerAngles = this.v3_Thumb2_R1;
	}

	public void processDrippingAndPrecum()
	{
		if (this.cumInAnus > 0f && !this.anusBeingPenetrated && !this.crotchCoveredByClothing)
		{
			if (this.currentAnaldripDot == null)
			{
				this.analdripDelay -= Time.deltaTime * this.cumInAnus * 0.3f;
				if (this.analdripDelay <= 0f)
				{
					if (Game.rainbowJizzCheat)
					{
						this.cumInAnusColor = ColorPicker.HsvToColor(Time.time * 2000f, 0.9f, 0.9f);
					}
					this.currentAnaldripDot = Cum.addDripDot(this.tailholeEntrance.transform.position, this.cap(this.cumInAnus / 2f, 0.5f, 4f), this.cumInAnusColor);
					this.currentAnaldripDot.GO.transform.SetParent(this.tailholeEntrance.transform);
					this.v3.x = 0f;
					this.v3.y = 0f;
					this.v3.z = 0f;
					this.currentAnaldripDot.GO.transform.localPosition = this.v3;
					this.currentAnaldripDot.head.localPosition = Vector3.zero;
					this.currentAnaldripDot.tail.localPosition = this.currentAnaldripDot.GO.transform.localPosition;
					this.currentAnaldripDot.slider = false;
					this.analdripDelay = 1f + UnityEngine.Random.value * 0.25f;
				}
			}
			else
			{
				this.currentAnaldripDot.thickness += Time.deltaTime * (0.35f + this.cumInAnus * 0.01f) * (1f + this.anusGape);
				if (!this.currentAnaldripDot.stillAttached)
				{
					this.currentAnaldripDot = null;
				}
			}
			this.cumInAnus -= Time.deltaTime * (1.2f + this.anusGape) * 1.1f;
		}
		if (this.showPenis && !this.crotchCoveredByClothing)
		{
			if (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f) < this.data.precumThreshold)
			{
				this.precumFrequency = 0f;
			}
			else
			{
				this.precumFrequency = 5f - (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f)) * 2f;
			}
			if (this.currentPrecumDot != null && !this.currentPrecumDot.stillAttached)
			{
				this.currentPrecumDot = null;
			}
			if (this.precumFrequency > 0f && this.currentPrecumDot == null)
			{
				this.precumDelay -= Time.deltaTime;
				if (this.precumDelay < 0f)
				{
					this.precumDelay += this.precumFrequency * (1f + UnityEngine.Random.value * 0.2f);
					this.v3 = Vector3.zero;
					this.v3.x = -0.0184f;
					this.v3.z = 0.003f;
					if (Game.rainbowJizzCheat)
					{
						this.col = ColorPicker.HsvToColor(Time.time * 2000f, 0.9f, 0.9f);
					}
					else
					{
						this.col = Cum.defaultCumColor;
					}
					this.currentPrecumDot = Cum.addDripDot(this.penisTip(true), 1f, this.col);
					this.currentPrecumDot.GO.transform.SetParent(this.bones.UrethraBottom);
					this.currentPrecumDot.GO.transform.localPosition = this.v3;
					this.currentPrecumDot.head.localPosition = Vector3.zero;
					this.currentPrecumDot.tail.localPosition = Vector3.zero;
					this.currentPrecumDot.slider = true;
				}
			}
			if (this.currentPrecumDot != null && !this.currentPrecumDot.slider)
			{
				this.currentPrecumDot.thickness += Time.deltaTime * 0.15f * (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f));
			}
		}
		if (this.showVagina && !this.crotchCoveredByClothing)
		{
			if (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f) < this.data.wetnessThreshold)
			{
				this.wetdropFrequency = 0f;
			}
			else
			{
				this.wetdropFrequency = 5f - (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f)) * 2f;
			}
			if (this.currentwetdropDot != null && !this.currentwetdropDot.stillAttached)
			{
				this.currentwetdropDot = null;
			}
			if (this.wetdropFrequency > 0f && this.currentwetdropDot == null)
			{
				this.wetdropDelay -= Time.deltaTime;
				if (this.wetdropDelay < 0f)
				{
					this.wetdropDelay += this.wetdropFrequency * (1f + UnityEngine.Random.value * 0.2f);
					if (Game.rainbowJizzCheat)
					{
						this.col = ColorPicker.HsvToColor(Time.time * 2000f, 0.9f, 0.9f);
					}
					else
					{
						this.col = Cum.defaultCumColor;
					}
					this.currentwetdropDot = Cum.addDripDot(this.bones.VaginaRearLip.position, 1f, this.col);
					this.currentwetdropDot.GO.transform.SetParent(this.bones.VaginaRearLip);
					this.v3.x = -0.05f;
					this.v3.y = 0f;
					this.v3.z = 0f;
					this.currentwetdropDot.GO.transform.localPosition = this.v3;
					this.currentwetdropDot.head.localPosition = Vector3.zero;
					this.currentwetdropDot.tail.localPosition = this.currentwetdropDot.GO.transform.localPosition;
					this.currentwetdropDot.slider = false;
				}
			}
			if (this.currentwetdropDot != null)
			{
				this.currentwetdropDot.thickness += Time.deltaTime * 0.15f * (this.orgasm + this.anticipation + this.cap(this.orgasming * 10f / this.currentOrgasmDuration, 0f, 1f));
			}
		}
	}

	public void processWetness()
	{
		if (Mathf.Abs(this.wetness_vagina - this.rendered_wetness_vagina) > 0.05f)
		{
			if (this.wetness_vagina > this.rendered_wetness_vagina)
			{
				this.addWetness("vagina", this.wetness_vagina - this.rendered_wetness_vagina);
			}
			this.rendered_wetness_vagina = this.wetness_vagina;
		}
		if (Mathf.Abs(this.wetness_penis - this.rendered_wetness_penis) > 0.05f)
		{
			if (this.wetness_penis > this.rendered_wetness_penis)
			{
				this.addWetness("penis", this.wetness_penis - this.rendered_wetness_penis);
			}
			this.rendered_wetness_penis = this.wetness_penis;
		}
		if (Mathf.Abs(this.wetness_finger0 - this.rendered_wetness_finger0) > 0.05f)
		{
			if (this.wetness_finger0 > this.rendered_wetness_finger0)
			{
				this.addWetness("finger0", this.wetness_finger0 - this.rendered_wetness_finger0);
			}
			this.rendered_wetness_finger0 = this.wetness_finger0;
		}
		if (Mathf.Abs(this.wetness_finger1 - this.rendered_wetness_finger1) > 0.05f)
		{
			if (this.wetness_finger1 > this.rendered_wetness_finger1)
			{
				this.addWetness("finger1", this.wetness_finger1 - this.rendered_wetness_finger1);
			}
			this.rendered_wetness_finger1 = this.wetness_finger1;
		}
		if (Mathf.Abs(this.wetness_fist - this.rendered_wetness_fist) > 0.05f)
		{
			if (this.wetness_fist > this.rendered_wetness_fist)
			{
				this.addWetness("fist", this.wetness_fist - this.rendered_wetness_fist);
			}
			this.rendered_wetness_fist = this.wetness_fist;
		}
		if (Mathf.Abs(this.wetness_muzzle - this.rendered_wetness_muzzle) > 0.05f)
		{
			if (this.wetness_muzzle > this.rendered_wetness_muzzle)
			{
				this.addWetness("muzzle", this.wetness_muzzle - this.rendered_wetness_muzzle);
			}
			this.rendered_wetness_muzzle = this.wetness_muzzle;
		}
	}

	public Vector3 up()
	{
		return -this.bones.Root.right;
	}

	public Vector3 right()
	{
		return -this.bones.Root.up;
	}

	public Vector3 forward()
	{
		return -this.bones.Root.forward;
	}

	public Vector3 upAfterIK()
	{
		return this.up_AIK;
	}

	public Vector3 rightAfterIK()
	{
		return this.right_AIK;
	}

	public Vector3 forwardAfterIK()
	{
		return this.forward_AIK;
	}

	public void stimulate(float amount)
	{
		this.stimulation += amount * Time.deltaTime;
		this.numberOfStimulatingInteractions++;
	}

	public void arouseByThought(float amount, float duration)
	{
		arousingThought arousingThought = new arousingThought();
		arousingThought.arousal = amount;
		arousingThought.duration = duration;
		arousingThought.durationMidpoint = duration / 2f;
		this.arousingThoughts.Add(arousingThought);
	}

	public void arouse(float amount)
	{
		this.arousal += amount * Time.deltaTime;
		this.numberOfArousingInteractions++;
	}

	public void hurt(float amount, bool allAtOnce = false)
	{
		if (allAtOnce)
		{
			this.discomfortFromSourcesOtherThanStimulation += amount;
		}
		else
		{
			this.discomfortFromSourcesOtherThanStimulation += amount * Time.deltaTime;
		}
	}

	public void processInteractionEmotions()
	{
		this.timeSinceExpressionChange -= Time.deltaTime;
		if (this.numberOfStimulatingInteractions != this.lastKnownNumberOfStimulatingInteractions)
		{
			if (this.lastKnownNumberOfStimulatingInteractions == 0 || this.numberOfStimulatingInteractions == 0)
			{
				this.timeSinceExpressionChange = 0.05f;
				this.pleasureEyeCheckToggleCooldown = 0.05f;
				if (this.lastKnownNumberOfStimulatingInteractions == 0 && this.numberOfStimulatingInteractions > 0)
				{
					this.closingEyesInPleasure = false;
				}
			}
			else
			{
				this.timeSinceExpressionChange *= 0.5f;
			}
		}
		this.inPain = (this.discomfort > this.pleasure + 0.15f || this.timeSincePain < 2f);
		if (this.discomfort > this.pleasure + 0.15f)
		{
			this.timeSincePain = 0f;
		}
		else
		{
			this.timeSincePain += Time.deltaTime;
		}
		if (this.inPain != this.lastKnownInPain)
		{
			if (!this.lastKnownInPain)
			{
				this.timeSinceExpressionChange = 0f;
				this.pleasureEyeCheckToggleCooldown = 0.05f;
			}
			else
			{
				this.timeSinceExpressionChange *= 0.8f;
			}
		}
		this.overstimulationEmotionalState = 0;
		if (this.overstimulation > this.overstimulationEmotionThreshhold0)
		{
			this.overstimulationEmotionalState = 1;
		}
		if (this.overstimulation > this.overstimulationEmotionThreshhold1)
		{
			this.overstimulationEmotionalState = 2;
		}
		if (this.overstimulationEmotionalState != this.lastOverstimulationEmotionalState)
		{
			this.timeSinceExpressionChange *= 0.1f;
			this.lastOverstimulationEmotionalState = this.overstimulationEmotionalState;
		}
		if (this.inPain)
		{
			this.wagIntensity = 0.1f;
		}
		else if (this.overstimulationEmotionalState > 0)
		{
			this.wagIntensity = 0.2f;
		}
		else if (this.orgasming > 0.01f)
		{
			this.wagIntensity = 0.9f;
		}
		else if (this.pleasure > 0.01f)
		{
			this.wagIntensity = 0.7f;
		}
		else
		{
			this.wagIntensity = 0.5f;
		}
		this.aggression = this.cap(this.aggression, -1f, 1f);
		this.dominance = this.cap(this.dominance, -1f, 1f);
		this.friendlinessToPlayer = this.cap(this.friendlinessToPlayer, 0f, 2f);
		this.rebellious = false;
		if (this.timeSinceExpressionChange <= 0f)
		{
			this.expressionOptions = new List<string>();
			if (this.aggression > 0.5f)
			{
				if (this.dominance > -0.75f)
				{
					this.effectivePersonality = "angry";
					if (this.aggression > 0.7f)
					{
						this.rebellious = true;
					}
				}
				else
				{
					this.effectivePersonality = "shy";
				}
			}
			else if (this.aggression < -0.5f)
			{
				if (this.dominance > 0.75f)
				{
					this.effectivePersonality = "happy";
				}
				else
				{
					this.effectivePersonality = "shy";
				}
			}
			else if (this.dominance > -0.25f)
			{
				this.effectivePersonality = "happy";
			}
			else
			{
				this.effectivePersonality = "shy";
			}
			if (this.overstimulationEmotionalState > 0)
			{
				if (this.overstimulationEmotionalState == 1 && this.aggression < 0.5f)
				{
					this.effectivePersonality = "shy";
				}
				if (this.overstimulationEmotionalState == 2)
				{
					this.effectivePersonality = "angry";
				}
			}
			switch (this.effectivePersonality)
			{
			case "shy":
				this.wagIntensity *= 0.6f;
				break;
			case "happy":
				this.wagIntensity *= 1.1f;
				break;
			case "angry":
				this.wagIntensity *= 0.2f;
				break;
			}
			if (this.inPain)
			{
				this.expressionOptions.Add("grumpy");
				this.expressionOptions.Add("angry");
				this.expressionOptions.Add("hurt");
				this.expressionOptions.Add("weaksmile");
				this.expressionOptions.Add("concerned");
			}
			else if (this.timeSincePain < 5f)
			{
				this.expressionOptions.Add("concerned");
				this.expressionOptions.Add("weaksmile");
				this.expressionOptions.Add("worried");
			}
			else if (this.numberOfStimulatingInteractions == 0 && this.orgasming <= this.currentOrgasmDuration * 0.5f && this.overstimulationEmotionalState == 0)
			{
				switch (this.effectivePersonality)
				{
				case "shy":
					this.expressionOptions.Add("hurt");
					this.expressionOptions.Add("concerned");
					this.expressionOptions.Add("impressed");
					this.expressionOptions.Add("pleading");
					this.expressionOptions.Add("surprised");
					this.expressionOptions.Add("weaksmile");
					this.expressionOptions.Add("worried");
					break;
				case "happy":
					this.expressionOptions.Add("confused");
					this.expressionOptions.Add("coy");
					this.expressionOptions.Add("confusedAmused");
					this.expressionOptions.Add("happy");
					this.expressionOptions.Add("seductive");
					this.expressionOptions.Add("smile");
					break;
				case "angry":
					this.expressionOptions.Add("grumpy");
					this.expressionOptions.Add("serious");
					this.expressionOptions.Add("unamused");
					break;
				}
			}
			else
			{
				if (this.anticipation < 0.25f && this.orgasming <= 0f)
				{
					goto IL_0678;
				}
				if (this.overstimulationEmotionalState > 0)
				{
					goto IL_0678;
				}
				if (this.orgasming <= 0f)
				{
					switch (this.effectivePersonality)
					{
					case "shy":
						this.expressionOptions.Add("pleasureConflicted");
						this.expressionOptions.Add("pleasure");
						this.expressionOptions.Add("weaksmile");
						this.expressionOptions.Add("confusedAmused");
						this.expressionOptions.Add("confusedSmug");
						this.expressionOptions.Add("seductive");
						this.expressionOptions.Add("impressed");
						break;
					case "happy":
						this.expressionOptions.Add("pleasure");
						this.expressionOptions.Add("pleasureEnraptured");
						this.expressionOptions.Add("coy");
						this.expressionOptions.Add("dominant");
						this.expressionOptions.Add("happy");
						this.expressionOptions.Add("seductive");
						this.expressionOptions.Add("impressed");
						break;
					case "angry":
						this.expressionOptions.Add("pleasureConflicted");
						this.expressionOptions.Add("pleasureIntense");
						this.expressionOptions.Add("dominant");
						this.expressionOptions.Add("confusedAmused");
						this.expressionOptions.Add("confusedSmug");
						this.expressionOptions.Add("grit");
						this.expressionOptions.Add("hurt");
						this.expressionOptions.Add("pleading");
						this.expressionOptions.Add("worried");
						break;
					}
				}
				else
				{
					switch (this.effectivePersonality)
					{
					case "shy":
						this.expressionOptions.Add("worried");
						this.expressionOptions.Add("pleasureConflicted");
						this.expressionOptions.Add("pleasureIntense");
						break;
					case "happy":
						this.expressionOptions.Add("pleasure");
						this.expressionOptions.Add("pleasureEnraptured");
						this.expressionOptions.Add("pleasureIntense");
						break;
					case "angry":
						this.expressionOptions.Add("grumpy");
						this.expressionOptions.Add("hurt");
						this.expressionOptions.Add("pleasureConflicted");
						this.expressionOptions.Add("pleasureIntense");
						break;
					}
				}
			}
			goto IL_0aaa;
		}
		goto IL_0b78;
		IL_0b78:
		this.lastKnownNumberOfStimulatingInteractions = this.numberOfStimulatingInteractions;
		this.numberOfStimulatingInteractions = 0;
		this.lastKnownNumberOfArousingInteractions = this.numberOfArousingInteractions;
		this.numberOfArousingInteractions = 0;
		if (!this.inPain && !(this.timeSincePain > 2f))
		{
			return;
		}
		this.lastKnownInPain = this.inPain;
		return;
		IL_0aaa:
		for (int i = 0; i < this.expressionOptions.Count; i++)
		{
			if (this.expressionOptions[i] == this.facialExpression && this.expressionOptions.Count > 1)
			{
				this.expressionOptions.RemoveAt(i);
				i--;
			}
		}
		this.facialExpression = this.expressionOptions[Mathf.RoundToInt(UnityEngine.Random.value * 1000f) % this.expressionOptions.Count];
		this.timeSinceExpressionChange = 2.5f + UnityEngine.Random.value * 8f;
		if (this.orgasming >= this.currentOrgasmDuration * 0.5f)
		{
			this.timeSinceExpressionChange *= 0.3f;
		}
		goto IL_0b78;
		IL_0678:
		switch (this.effectivePersonality)
		{
		case "shy":
			this.expressionOptions.Add("pleasureConflicted");
			this.expressionOptions.Add("pleasure");
			this.expressionOptions.Add("worried");
			this.expressionOptions.Add("weaksmile");
			break;
		case "happy":
			this.expressionOptions.Add("pleasure");
			this.expressionOptions.Add("pleasureEnraptured");
			this.expressionOptions.Add("pleasureSerious");
			this.expressionOptions.Add("confusedSmug");
			this.expressionOptions.Add("confusedAmused");
			this.expressionOptions.Add("dominant");
			this.expressionOptions.Add("happy");
			this.expressionOptions.Add("seductive");
			this.expressionOptions.Add("impressed");
			break;
		case "angry":
			this.expressionOptions.Add("concerned");
			this.expressionOptions.Add("coy");
			this.expressionOptions.Add("confusedSmug");
			this.expressionOptions.Add("angry");
			break;
		}
		goto IL_0aaa;
	}

	public void removeAnyClothesCoveringCrotch()
	{
		bool flag = false;
		for (int i = 0; i < this.clothingPiecesEquipped.Count; i++)
		{
			LabItemDefinition itemDefinition = Inventory.getItemDefinition(this.clothingPiecesEquipped[i].name);
			if (itemDefinition.equipSlots[ClothingSlots.CROTCH].occupies)
			{
				Inventory.moveItemToDifferentBag(this.clothingPiecesEquipped[i].name, "CLOTHING", "INVENTORY");
				flag = true;
				this.game.playSound("ui_clothingShuffle", 1f, 1f);
			}
		}
		if (flag)
		{
			this.updateClothingBasedOnInventory();
		}
	}

	public void maintainArousal(float amount = 0.2f)
	{
		this.arousalLossRate -= amount;
		if (this.arousalLossRate < 0f)
		{
			this.arousalLossRate = 0f;
		}
	}

	public void processSpecimenProduction()
	{
		if (this.totalEnjoymentAndUnenjoyment < 0.05f && this.totalEnjoymentAndUnenjoyment > -0.05f)
		{
			return;
		}
		if (this.ableToExperienceEnjoyment)
		{
			for (int i = 0; i < 6; i++)
			{
				this.curFrameSpecimenProduced[i] = 0f;
			}
			this.rateOfSpecimenProduction = 0f;
			this.rateOfSpecimenProduction += this.proximityToOrgasm;
			this.rateOfSpecimenProduction += this.arousal;
			this.rateOfSpecimenProduction += this.cap((float)this.lastKnownNumberOfStimulatingInteractions * 0.2f, 0f, 1f);
			if (this.currentOrgasmDuration > 0f && this.orgasming > 0f)
			{
				this.rateOfSpecimenProduction += this.orgasming / this.currentOrgasmDuration;
			}
			this.rateOfSpecimenProduction *= this.anticipation;
			this.curFrameSpecimenProduced[1] += (this.secretEnjoymentFromAttraction + this.secretEnjoymentFromInteractions + this.secretEnjoymentFromExperience) / this.totalEnjoymentAndUnenjoyment;
			this.curFrameSpecimenProduced[1] = (Mathf.Pow(1f + this.curFrameSpecimenProduced[1], 3f) - 1f) * 0.9f + 0.1f;
			this.curFrameSpecimenProduced[0] = 1f - this.curFrameSpecimenProduced[1];
			if (this.refractory <= 0f && this.orgasming <= 0f)
			{
				if (this.proximityToOrgasm > 0.5f)
				{
					this.edgeTime += Time.deltaTime;
				}
				this.targetEdgeTime = 10f + Mathf.Pow(2f, 1f + UserSettings.data.orgasmSpeed) * 10f;
				this.sessionTimeFactor = (this.cap((this.edgeTime - this.targetEdgeTime) / (this.targetEdgeTime / 2f), -1f, 1f) + 1f) / 2f;
				this.projectedEquimine = this.nonLEchemicalsProducedSinceLastOrgasm * this.sessionTimeFactor;
				this.projectedLapinine = this.nonLEchemicalsProducedSinceLastOrgasm * (1f - this.sessionTimeFactor);
			}
			else
			{
				if (this.edgeTime > 0f)
				{
					this.stockpiledSpecimenProduced[2] = this.projectedLapinine;
					this.stockpiledSpecimenProduced[4] = this.projectedEquimine;
					this.nonLEchemicalsProducedSinceLastOrgasm = 0f;
				}
				this.edgeTime = 0f;
				this.projectedLapinine = 0f;
				this.projectedEquimine = 0f;
			}
			this.curFrameSpecimenProduced[3] += (this.unenjoymentFromAttraction + this.unenjoymentFromInteractions + this.unenjoymentFromExperience) / this.totalEnjoymentAndUnenjoyment * 0.5f;
			if (this.lastKnownInPain)
			{
				this.curFrameSpecimenProduced[3] += 10f;
			}
			this.curFrameSpecimenProduced[3] += this.cap(this.overstimulation - 0.2f, 0f, 1f) * 12f;
			this.curFrameSpecimenProduced[5] += this.lastKnownCumulativePartnerSatisfaction * this.cap(this.preferences["partner_satisfaction"], 0f, 3f);
			float num = 0f;
			for (int j = 0; j < 6; j++)
			{
				if (float.IsNaN(this.curFrameSpecimenProduced[j]))
				{
					this.curFrameSpecimenProduced[j] = 0f;
				}
				if (this.curFrameSpecimenProduced[j] > num)
				{
					num = this.curFrameSpecimenProduced[j];
				}
			}
			if (num <= 1f)
			{
				num = 1f;
			}
			for (int k = 0; k < 6; k++)
			{
				this.curFrameSpecimenProduced[k] = this.cap(this.curFrameSpecimenProduced[k] / num * this.rateOfSpecimenProduction, 0f, 999999f);
			}
			for (int l = 0; l < 6; l++)
			{
				this.curFrameSpecimenProduced[l] *= Time.deltaTime * this.specimenSpeedModifier;
				float num2 = 0.8f;
				if (this.orgasming > 0f)
				{
					num2 = 0f;
				}
				else if (this.refractory > 0f)
				{
					num2 -= this.refractory / this.currentRefractoryDuration * 0.8f;
				}
				if (l == 2 || l == 4)
				{
					num2 = 1f;
				}
				if (this.orgasming > 0f)
				{
					num2 = 0f;
				}
				if (l != 2 && l != 4)
				{
					this.nonLEchemicalsProducedSinceLastOrgasm += this.curFrameSpecimenProduced[l];
				}
				this.specimenProduced[l] += this.curFrameSpecimenProduced[l] * (1f - num2);
				this.stockpiledSpecimenProduced[l] += this.curFrameSpecimenProduced[l] * num2;
				if (this.orgasming > 0f)
				{
					float num3 = this.stockpiledSpecimenProduced[l] * this.cap(Time.deltaTime, 0f, 1f);
					this.specimenProduced[l] += num3;
					this.stockpiledSpecimenProduced[l] -= num3;
				}
				this.curFrameSpecimenProduced[l] = 0f;
			}
		}
	}

	public void processEnjoyment()
	{
		this.ableToExperienceEnjoyment = (!this.controlledByPlayer && (UnityEngine.Object)this.apparatus != (UnityEngine.Object)null);
		if (this.ableToExperienceEnjoyment)
		{
			this.enjoymentPollDelay -= Time.deltaTime;
			if (this.enjoymentPollDelay <= 0f)
			{
				this.enjoymentPollDelay += this.pollTime / (float)this.numEnjoymentPolls;
				switch (this.nextEnjoymentToPoll)
				{
				case 0:
					this.pollEnjoymentFromExperience();
					break;
				case 1:
					this.pollEnjoymentFromAttraction();
					break;
				case 2:
					this.pollEnjoymentFromInteractions();
					break;
				}
				this.nextEnjoymentToPoll = (this.nextEnjoymentToPoll + 1) % this.numEnjoymentPolls;
			}
			this.enjoymentFromInteractions += (this.tar_enjoymentFromInteractions - this.enjoymentFromInteractions) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.unenjoymentFromInteractions += (this.tar_unenjoymentFromInteractions - this.unenjoymentFromInteractions) * this.cap(Time.deltaTime * 6.5f, 0f, 1f);
			this.enjoymentFromExperience += (this.tar_enjoymentFromExperience - this.enjoymentFromExperience) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.unenjoymentFromExperience += (this.tar_unenjoymentFromExperience - this.unenjoymentFromExperience) * this.cap(Time.deltaTime * 6.5f, 0f, 1f);
			this.enjoymentFromAttraction += (this.tar_enjoymentFromAttraction - this.enjoymentFromAttraction) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.unenjoymentFromAttraction += (this.tar_unenjoymentFromAttraction - this.unenjoymentFromAttraction) * this.cap(Time.deltaTime * 6.5f, 0f, 1f);
			this.secretEnjoymentFromInteractions += (this.tar_secretEnjoymentFromInteractions - this.secretEnjoymentFromInteractions) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.secretEnjoymentFromExperience += (this.tar_secretEnjoymentFromExperience - this.secretEnjoymentFromExperience) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.secretEnjoymentFromAttraction += (this.tar_secretEnjoymentFromAttraction - this.secretEnjoymentFromAttraction) * this.cap(Time.deltaTime * 1.2f, 0f, 1f);
			this.totalEnjoymentAndUnenjoyment = this.enjoymentFromAttraction + this.enjoymentFromExperience + this.enjoymentFromInteractions + this.unenjoymentFromAttraction + this.unenjoymentFromExperience + this.unenjoymentFromInteractions;
			if ((UnityEngine.Object)this.enjoymentUI == (UnityEngine.Object)null)
			{
				this.enjoymentUI = this.GO.transform.Find("EnjoymentUI").gameObject;
			}
			this.enjoymentUI.SetActive(Game.enjoymentDebugging && !this.controlledByPlayer);
			if (Game.enjoymentDebugging)
			{
				this.enjoymentUI.transform.position = this.bones.Head.position;
				string text = "aggression: " + Mathf.RoundToInt(this.aggression * 100f) + "\r\ndominance: " + Mathf.RoundToInt(this.dominance * 100f) + "\r\nfriendliness: " + Mathf.RoundToInt(this.friendlinessToPlayer * 100f) + "\r\n\r\n";
				foreach (string item in this.commandStatus.Keys.ToList())
				{
					text = text + item + ": " + this.getCommandStatus(item) + "\r\n";
				}
				text = text + "\r\n\r\nHow much do I want to cum: " + Mathf.RoundToInt(this.howMuchDoIWantToOrgasm * 100f);
				((Component)this.enjoymentUI.transform.Find("txt")).GetComponent<TextMesh>().text = text;
			}
			this.satisfactionFromEnjoyment *= 1f - this.cap(Time.deltaTime * 0.01f, 0f, 1f);
			this.dissatisfactionFromEnjoyment *= 1f - this.cap(Time.deltaTime * 0.01f, 0f, 1f);
			this.satisfactionFromEnjoyment += this.enjoymentFromAttraction * this.enjoymentWeight_attraction;
			this.satisfactionFromEnjoyment += this.enjoymentFromExperience * this.enjoymentWeight_experiences;
			this.satisfactionFromEnjoyment += this.enjoymentFromInteractions * this.enjoymentWeight_sensations;
			this.dissatisfactionFromEnjoyment += this.unenjoymentFromAttraction * this.enjoymentWeight_attraction * this.data.negativeExperienceModifier;
			this.dissatisfactionFromEnjoyment += this.unenjoymentFromExperience * this.enjoymentWeight_experiences * this.data.negativeExperienceModifier;
			this.dissatisfactionFromEnjoyment += this.unenjoymentFromInteractions * this.enjoymentWeight_sensations * this.data.negativeExperienceModifier;
			this.satisfactionFromObjectives = 0f;
			int num = 0;
			for (int i = 0; i < this.objectives.Count; i++)
			{
				if (!this.objectives[i].secret || this.objectives[i].completed)
				{
					num++;
				}
			}
			for (int j = 0; j < this.objectives.Count; j++)
			{
				if (this.objectives[j].bad)
				{
					this.satisfactionFromObjectives -= this.objectives[j].completion / (float)num;
				}
				else
				{
					this.satisfactionFromObjectives += this.objectives[j].completion / (float)num;
				}
			}
			if (num <= 0)
			{
				this.satisfactionFromObjectives = 1f;
			}
			float num2 = this.cap(this.satisfactionFromObjectives * (this.satisfactionFromEnjoyment / (this.satisfactionFromEnjoyment + this.dissatisfactionFromEnjoyment)), 0f, 1f);
			if (float.IsNaN(num2))
			{
				num2 = 0f;
			}
			if (num2 > this.satisfaction)
			{
				if (this.orgasming > 0f)
				{
					this.satisfaction += (num2 - this.satisfaction) * this.cap(Time.deltaTime * 15f, 0f, 1f);
				}
				else
				{
					this.satisfaction += (num2 - this.satisfaction) * this.cap(Time.deltaTime * 5f, 0f, 1f);
				}
			}
			else if (this.refractory > 0f)
			{
				this.satisfaction += (num2 - this.satisfaction) * this.cap(Time.deltaTime * 0.05f, 0f, 1f);
			}
			else
			{
				this.satisfaction += (num2 - this.satisfaction) * this.cap(Time.deltaTime * 5f, 0f, 1f);
			}
		}
	}

	public void addEnjoymentFromExperience(int direction, string cause, float amount, bool secret)
	{
		if (!this.experienceEnjoymentCauses.ContainsKey(cause))
		{
			this.experienceEnjoymentCauses.Add(cause, 0f);
		}
		this.experienceEnjoymentCauses[cause] = amount * (float)direction;
		if (direction == 1)
		{
			this.tar_enjoymentFromExperience += amount;
			if (secret)
			{
				this.tar_secretEnjoymentFromExperience += amount;
			}
		}
		else
		{
			this.tar_unenjoymentFromExperience += amount;
		}
	}

	public void triggerSensation(string sensation, float time)
	{
		if (!this.sensationTimes.ContainsKey(sensation))
		{
			this.sensationTimes.Add(sensation, 0f);
		}
		if (time > this.sensationTimes[sensation])
		{
			this.sensationTimes[sensation] = time;
		}
	}

	public void triggerFetishEnjoyment(string fetish, float time)
	{
		if (!this.fetishTimes.ContainsKey(fetish))
		{
			this.fetishTimes.Add(fetish, 0f);
		}
		if (time > this.fetishTimes[fetish])
		{
			this.fetishTimes[fetish] = time;
		}
	}

	public void pollEnjoymentFromExperience()
	{
		this.tar_enjoymentFromExperience = 0f;
		this.tar_secretEnjoymentFromExperience = 0f;
		this.tar_unenjoymentFromExperience = 0f;
		foreach (string item in this.experienceEnjoymentCauses.Keys.ToList())
		{
			this.experienceEnjoymentCauses[item] = 0f;
		}
		foreach (string item2 in this.fetishTimes.Keys.ToList())
		{
			if (this.fetishTimes[item2] > 0f)
			{
				Dictionary<string, float> dictionary;
				string key;
				(dictionary = this.fetishTimes)[key = item2] = dictionary[key] - this.pollTime;
				if (this.preferences[item2] > 0.5f)
				{
					this.addEnjoymentFromExperience(1, item2, this.preferences[item2] - 0.5f, this.preferenceKnowledge[item2] != 2);
				}
				else
				{
					this.addEnjoymentFromExperience(-1, item2, 0.5f - this.preferences[item2], this.preferenceKnowledge[item2] != 2);
				}
			}
			else
			{
				this.fetishTimes[item2] = 0f;
			}
		}
		if (this.overstimulation > 0f)
		{
			if (this.preferences["overstimulation"] > 0.5f)
			{
				this.addEnjoymentFromExperience(1, "overstimulation", (this.preferences["overstimulation"] - 0.5f) * this.overstimulation, this.preferenceKnowledge["overstimulation"] != 2);
			}
			else
			{
				this.addEnjoymentFromExperience(-1, "overstimulation", (0f - (0.5f - this.preferences["overstimulation"])) * this.overstimulation, this.preferenceKnowledge["overstimulation"] != 2);
			}
		}
		this.idealEdgeTime = this.preferences["edging"] * 100f;
		if (this.idealEdgeTime < 45f)
		{
			this.idealEdgeTime = 45f;
		}
		if (this.proximityToOrgasm > 0.5f && this.refractory <= 0f)
		{
			this.edgeAmount = (this.proximityToOrgasm - 0.5f) * 2f;
			this.timeAtEdge += this.pollTime;
			this.enjoymentChangeBecauseOfFastOrgasm -= this.enjoymentChangeBecauseOfFastOrgasm * this.cap(this.pollTime * 0.1f, 0f, 1f);
		}
		else
		{
			this.edgeAmount = 0f;
			if (this.refractory > 0f && this.timeAtEdge > 0f)
			{
				if (this.timeAtEdge < this.idealEdgeTime * 0.75f)
				{
					this.enjoymentChangeBecauseOfFastOrgasm = (0f - (this.idealEdgeTime * 0.75f - this.timeAtEdge) / (this.idealEdgeTime * 0.75f)) * this.preferences["edging"];
				}
				this.timeAtEdge = 0f;
			}
			if (this.proximityToOrgasm < 0.5f && this.preferences["edging"] > 0.5f)
			{
				this.timeAtEdge -= this.timeAtEdge * this.cap(this.pollTime * 0.01f, 0f, 1f);
			}
		}
		if (this.timeAtEdge < this.idealEdgeTime)
		{
			this.readyToCum = false;
			if (this.preferences["edging"] > 0f)
			{
				float num = this.cap((this.timeAtEdge - this.idealEdgeTime * 0.75f) / (this.idealEdgeTime * 0.25f), 0f, 1f);
				this.addEnjoymentFromExperience(1, "edging", this.preferences["edging"] * this.edgeAmount * (1f - num), this.preferenceKnowledge["edging"] != 2);
			}
		}
		else
		{
			this.readyToCum = true;
			if (this.preferences["edging"] > 0.5f)
			{
				this.addEnjoymentFromExperience(1, "edging", (this.preferences["edging"] - 0.5f) * this.edgeAmount * (1f - (this.timeAtEdge - this.idealEdgeTime) / this.idealEdgeTime), this.preferenceKnowledge["edging"] != 2);
			}
			else
			{
				this.addEnjoymentFromExperience(-1, "edging", 0f - (0.5f - this.preferences["edging"]) * (1f - this.edgeAmount) * (this.timeAtEdge - this.idealEdgeTime) / this.idealEdgeTime, this.preferenceKnowledge["edging"] != 2);
			}
		}
		if (this.enjoymentChangeBecauseOfFastOrgasm > 0f)
		{
			this.addEnjoymentFromExperience(1, "orgasm_timing", this.enjoymentChangeBecauseOfFastOrgasm, false);
		}
		else
		{
			this.addEnjoymentFromExperience(-1, "orgasm_timing", 0f - this.enjoymentChangeBecauseOfFastOrgasm, false);
		}
		this.howManyCharactersAmIinteractingWith = 0;
		this.interactingWithCharacters = new List<int>();
		this.charactersIcanSee = new List<int>();
		for (int i = 0; i < this.currentInteractions.Count; i++)
		{
			if (!this.interactingWithCharacters.Contains(this.currentInteractions[i].targetCharacter.uid))
			{
				this.interactingWithCharacters.Add(this.currentInteractions[i].targetCharacter.uid);
			}
		}
		for (int j = 0; j < this.game.characters.Count; j++)
		{
			if (this.game.characters[j].uid != this.uid)
			{
				for (int k = 0; k < this.game.characters[j].currentInteractions.Count; k++)
				{
					if (this.game.characters[j].currentInteractions[k].targetCharacter.uid == this.uid && !this.interactingWithCharacters.Contains(this.game.characters[j].uid))
					{
						this.interactingWithCharacters.Add(this.game.characters[j].uid);
					}
				}
			}
		}
		this.howManyCharactersAmIinteractingWith = this.interactingWithCharacters.Count;
		this.howManyCharactersCanSeeMe = 0;
		for (int l = 0; l < this.game.characters.Count; l++)
		{
			if (this.game.characters[l].initted && this.game.characters[l].uid != this.uid)
			{
				this.v3 = this.game.characters[l].bones.Head.position;
				this.v32 = this.bones.Root.position - this.v3;
				bool flag = false;
				if (this.v32.magnitude < 50f)
				{
					flag = !Physics.Raycast(this.v3, this.v32, this.v32.magnitude, LayerMask.GetMask("StaticObjects"));
				}
				if (flag)
				{
					this.howManyCharactersCanSeeMe++;
				}
				this.v3 = this.bones.Head.position;
				this.v32 = this.game.characters[l].bones.Root.position - this.v3;
				bool flag2 = false;
				if (this.v32.magnitude < 50f)
				{
					flag2 = !Physics.Raycast(this.v3, this.v32, this.v32.magnitude, LayerMask.GetMask("StaticObjects"));
				}
				if (flag2)
				{
					this.charactersIcanSee.Add(this.game.characters[l].uid);
				}
			}
		}
		this.howManyWatchers = this.howManyCharactersCanSeeMe - this.howManyCharactersAmIinteractingWith;
		if (this.howManyCharactersCanSeeMe == 1)
		{
			this.howManyWatchers = 0;
		}
		if (this.howManyCharactersAmIinteractingWith > 1)
		{
			if (this.preferences["group_sex"] > 0.5f)
			{
				this.addEnjoymentFromExperience(1, "group_sex", this.preferences["group_sex"] - 0.5f, this.preferenceKnowledge["group_sex"] != 2);
			}
			else
			{
				this.addEnjoymentFromExperience(-1, "group_sex", 0.5f - this.preferences["group_sex"], this.preferenceKnowledge["group_sex"] != 2);
			}
		}
		float num2 = 0f;
		if (this.howManyWatchers >= 25000)
		{
			num2 = 2.25f;
		}
		else if (this.howManyWatchers >= 5000)
		{
			num2 = 2f;
		}
		else if (this.howManyWatchers >= 1000)
		{
			num2 = 1.75f;
		}
		else if (this.howManyWatchers >= 500)
		{
			num2 = 1.5f;
		}
		else if (this.howManyWatchers >= 100)
		{
			num2 = 1.25f;
		}
		else if (this.howManyWatchers >= 50)
		{
			num2 = 1f;
		}
		else if (this.howManyWatchers >= 10)
		{
			num2 = 0.75f;
		}
		else if (this.howManyWatchers >= 4)
		{
			num2 = 0.5f;
		}
		else if (this.howManyWatchers >= 1)
		{
			num2 = 0.25f;
		}
		if (this.preferences["exhibitionism"] > 0.5f)
		{
			this.addEnjoymentFromExperience(1, "exhibitionism", (this.preferences["exhibitionism"] - 0.5f) * num2, this.preferenceKnowledge["exhibitionism"] != 2);
		}
		else
		{
			this.addEnjoymentFromExperience(-1, "exhibitionism", (0.5f - this.preferences["exhibitionism"]) * num2, this.preferenceKnowledge["exhibitionism"] != 2);
		}
		this.cumulativePartnerSatisfaction = 0f;
		for (int m = 0; m < this.interactingWithCharacters.Count; m++)
		{
			this.partner = this.game.getCharacterByUID(this.interactingWithCharacters[m]);
			if (this.partner != null)
			{
				float num3 = this.partner.proximityToOrgasm * 0.75f;
				if (this.partner.orgasming > 0f || this.partner.refractory > 0f)
				{
					num3 += this.partner.refractory / this.partner.currentRefractoryDuration * 0.75f;
				}
				this.cumulativePartnerSatisfaction += num3 * 1f / 1.5f;
				if (num3 > 0.5f)
				{
					if (this.preferences["partner_satisfaction"] > 0.5f)
					{
						this.addEnjoymentFromExperience(1, "partner_satisfaction", (this.preferences["partner_satisfaction"] - 0.5f) * (num3 - 0.5f), this.preferenceKnowledge["partner_satisfaction"] != 2);
					}
					else
					{
						this.addEnjoymentFromExperience(-1, "partner_satisfaction", (0.5f - this.preferences["partner_satisfaction"]) * (num3 - 0.5f), this.preferenceKnowledge["partner_satisfaction"] != 2);
					}
				}
			}
		}
		this.lastKnownCumulativePartnerSatisfaction = this.cumulativePartnerSatisfaction;
		this.beingStimulatedAutomatically = (this.interactingWithCharacters.Count == 0 && this.lastKnownNumberOfStimulatingInteractions > 0);
		if (this.beingStimulatedAutomatically)
		{
			if (this.preferences["automated_stimulation"] > 0.5f)
			{
				this.addEnjoymentFromExperience(1, "automated_stimulation", this.preferences["automated_stimulation"] - 0.5f, this.preferenceKnowledge["automated_stimulation"] != 2);
			}
			else
			{
				this.addEnjoymentFromExperience(-1, "automated_stimulation", 0.5f - this.preferences["automated_stimulation"], this.preferenceKnowledge["automated_stimulation"] != 2);
			}
		}
	}

	public void addEnjoymentFromAttraction(int direction, string cause, float amount, bool secret)
	{
		if (!this.attractionEnjoymentCauses.ContainsKey(cause))
		{
			this.attractionEnjoymentCauses.Add(cause, 0f);
		}
		this.attractionEnjoymentCauses[cause] = amount * (float)direction;
		if (direction == 1)
		{
			this.tar_enjoymentFromAttraction += amount;
			if (secret)
			{
				this.tar_secretEnjoymentFromAttraction += amount;
			}
		}
		else
		{
			this.tar_unenjoymentFromAttraction += amount;
		}
	}

	public void partnerAttraction(string key, float modifier = 1f)
	{
		if (this.preferences[key] > 0.5f)
		{
			this.addEnjoymentFromAttraction(1, key, (this.preferences[key] - 0.5f) * modifier, this.preferenceKnowledge[key] != 2);
		}
		else
		{
			this.addEnjoymentFromAttraction(-1, key, (0.5f - this.preferences[key]) * modifier, this.preferenceKnowledge[key] != 2);
		}
	}

	public void pollEnjoymentFromAttraction()
	{
		this.tar_enjoymentFromAttraction = 0f;
		this.tar_unenjoymentFromAttraction = 0f;
		foreach (string item in this.attractionEnjoymentCauses.Keys.ToList())
		{
			this.attractionEnjoymentCauses[item] = 0f;
		}
		for (int i = 0; i < this.charactersIcanSee.Count; i++)
		{
			this.partner = this.game.getCharacterByUID(this.charactersIcanSee[i]);
			if (this.partner != null)
			{
				this.judgePartnerAttraction(this.partner, 0.2f);
			}
		}
		for (int j = 0; j < this.interactingWithCharacters.Count; j++)
		{
			this.partner = this.game.getCharacterByUID(this.interactingWithCharacters[j]);
			if (this.partner != null)
			{
				this.judgePartnerAttraction(this.partner, 1f);
			}
		}
	}

	public void judgePartnerAttraction(RackCharacter partner, float modifier = 1f)
	{
		float num = 0.5f;
		if (partner.showPenis && partner.showVagina)
		{
			this.partnerAttraction("herms", modifier);
			num = 0.45f;
		}
		else if (partner.showPenis)
		{
			if (partner.breastSize_act > RackCharacter.breastThreshhold)
			{
				this.partnerAttraction("dgirls", modifier);
				num = 0.4f;
			}
			else
			{
				this.partnerAttraction("men", modifier);
				num = 0.65f;
			}
		}
		else if (partner.showVagina)
		{
			if (partner.breastSize_act > RackCharacter.breastThreshhold)
			{
				this.partnerAttraction("women", modifier);
				num = 0.35f;
			}
			else
			{
				this.partnerAttraction("cboys", modifier);
				num = 0.6f;
			}
		}
		else
		{
			this.partnerAttraction("nulls", modifier);
			num = 0.45f;
		}
		float num2 = partner.height_act - this.height_act;
		if (num2 > 0.07f)
		{
			this.partnerAttraction("larger_partners", modifier * (num2 / 0.14f));
		}
		else if (!(num2 > -0.07f))
		{
			this.partnerAttraction("smaller_partners", modifier * ((0f - num2) / 0.14f));
		}
		float num3 = partner.totalFemininity;
		float num4 = 1f - num3 - num;
		if (num < 0.49f)
		{
			num4 *= -1f;
		}
		if (num4 < 0f)
		{
			this.partnerAttraction("countermasculinity", modifier);
		}
		else if (num4 > 0.45f)
		{
			this.partnerAttraction("exaggerated_masculinity", modifier);
		}
	}

	public void processPleasure()
	{
		this.tarHMDIWTO = 1f;
		if (this.getCommandStatus("order_to_cum") > 0)
		{
			this.tarHMDIWTO += (1f - this.dominance) * 0.5f;
		}
		if (this.getCommandStatus("order_to_not_cum") > 0)
		{
			this.tarHMDIWTO -= (1f - this.dominance) * 0.5f;
		}
		this.tarHMDIWTO += this.anticipation * 0.2f;
		this.tarHMDIWTO += this.proximityToOrgasm * 0.2f;
		if (this.readyToCum)
		{
			this.tarHMDIWTO *= 1.2f;
		}
		else
		{
			this.tarHMDIWTO *= 1f - this.proximityToOrgasm * 0.9f;
		}
		this.howMuchDoIWantToOrgasm += (this.tarHMDIWTO - this.howMuchDoIWantToOrgasm) * this.cap(Time.deltaTime * 5f, 0f, 1f);
		this.effectiveStamina = this.data.stamina * this.howMuchDoIWantToOrgasm;
		if (this.interactingWithSelf && this.arousal < 0.4f)
		{
			this.arousal += this.cap(Time.deltaTime, 0f, 0.4f - this.arousal);
		}
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			this.processInteractionEmotions();
		}
		else
		{
			this.wagIntensity = 0.25f;
		}
		this.rFactor = 0f;
		if (this.refractory > 0f)
		{
			this.rFactor = this.refractory / this.currentRefractoryDuration;
		}
		this.anticipation -= this.anticipation * this.cap(Time.deltaTime * 0.005f * (1f + this.rFactor * 20f), 0f, 1f);
		this.arousal -= this.arousal * this.cap(Time.deltaTime * 0.007f * (1f + this.rFactor * 2f), 0f, 1f) * this.arousalLossRate;
		this.arousalLossRate = 1f;
		this.stimulation -= this.stimulation * this.cap(Time.deltaTime * 2f, 0f, 1f);
		this.stimulation = this.cap(this.stimulation, 0f, 3f);
		this.sensitivityAdjustmentTarget = 1f;
		if (this.orgasming > 0f)
		{
			this.sensitivityAdjustmentTarget += (this.data.orgasmSensitivity - 1f) * (this.orgasming / this.currentOrgasmDuration);
			if (this.refractory > 0f)
			{
				this.sensitivityAdjustmentTarget += (this.data.refractorySensitivity - 1f) * Mathf.Pow(1f - this.orgasming / this.currentOrgasmDuration, 3f);
			}
			this.orgasming -= Time.deltaTime;
			if (this.orgasming < 0f)
			{
				this.orgasming = 0f;
				if (this.hasPissed)
				{
					this.data.cumVolume = this.backupCumVolume;
					this.data.cumSpurtStrength = this.backupCumSpurtStrength;
					this.data.cumSpurtFrequency = this.backupCumSpurtFrequency;
					this.data.orgasmDuration = this.backupOrgasmDuration;
					this.isPissing = false;
					this.hasPissed = false;
				}
			}
		}
		else if (this.refractory > 0f)
		{
			this.sensitivityAdjustmentTarget += (this.data.refractorySensitivity - 1f) * (this.refractory / this.currentRefractoryDuration);
			this.refractory -= Time.deltaTime;
			if (this.refractory < 0f)
			{
				this.refractory = 0f;
			}
		}
		else
		{
			this.sensitivityAdjustmentTarget += (this.data.proximitySensitivity - 1f) * this.proximityToOrgasm;
		}
		this.percievedStimulation = this.stimulation * this.data.sensitivity;
		if (1f - this.cap(this.percievedStimulation - this.targetStimulation * 0.75f, 0f, 1f) > this.sensitivity)
		{
			this.sensitivity += (1f - this.cap(this.percievedStimulation - this.targetStimulation * 0.5f, 0f, 1f) - this.sensitivity) * this.cap(Time.deltaTime * 0.07f, 0f, 1f) * (1f + this.rFactor * 2f);
		}
		else
		{
			this.sensitivity += (1f - this.cap(this.percievedStimulation - this.targetStimulation * 0.5f, 0f, 1f) - this.sensitivity) * this.cap(Time.deltaTime * 0.5f, 0f, 1f) * (1f + this.rFactor * 2f);
		}
		this.sensitivityAdjustment += (this.sensitivityAdjustmentTarget - this.sensitivityAdjustment) * this.cap(Time.deltaTime * 5f, 0f, 1f);
		this.targetStimulation = this.cap((2f - this.sensitivity * this.sensitivityAdjustment) * 0.5f, 0.1f, 1f) * 0.25f + (1f - UserSettings.data.globalSensitivity) * 2f;
		float num = 0f;
		float num2 = 0f;
		if (this.refractory > 0f)
		{
			num = this.cap(this.percievedStimulation - this.targetStimulation - 0.05f, 0f, 1f);
		}
		else
		{
			num2 = this.cap(this.percievedStimulation - this.targetStimulation - 0.15f, 0f, 1f) * 0.1f + this.discomfortFromSourcesOtherThanStimulation;
		}
		this.discomfortFromSourcesOtherThanStimulation -= this.discomfortFromSourcesOtherThanStimulation * this.cap(Time.deltaTime * 2f, 0f, 1f);
		this.overstimulation += (num - this.overstimulation) * this.cap(Time.deltaTime * 15f, 0f, 1f);
		this.discomfort += (num2 - this.discomfort) * this.cap(Time.deltaTime * 15f, 0f, 1f);
		float num3 = this.cap(0.5f - Mathf.Abs(this.targetStimulation - this.percievedStimulation), 0f, 1f) * (1f + this.orgasming / this.currentOrgasmDuration);
		num3 += this.forcedPleasure;
		this.forcedPleasure = 0f;
		this.pleasure += (num3 - this.pleasure) * this.cap(Time.deltaTime * 8f, 0f, 1f);
		this.arouse(this.pleasure * 0.1f);
		this.arousal = this.cap(this.arousal, 0f, 1f);
		this.anticipation += this.arousal * 0.009f * Time.deltaTime;
		this.anticipation = this.cap(this.anticipation, 0f, 1f);
		this.orgasm += this.pleasure * (0.5f + this.anticipation * this.arousal) * 0.6f * 0.225f * Time.deltaTime * this.effectiveStamina * (UserSettings.data.orgasmSpeed + 0.65f);
		float num4 = 1f;
		if (this.controlledByPlayer)
		{
			num4 = 0.25f;
		}
		this.orgasm -= this.cap(Time.deltaTime * 0.02f, 0f, 1f) * (1f + this.rFactor * 10f) * this.effectiveStamina * num4 * (UserSettings.data.orgasmSpeed + 0.65f);
		this.orgasm = this.cap(this.orgasm, 0f, 1f);
		this.proximityToOrgasm = this.cap(this.orgasm + this.artificialOrgasm + this.pleasure * this.arousal, 0f, 1f);
		this.artificialOrgasm = 0f;
		if (this.proximityToOrgasm >= 0.99f && this.orgasmPrevention <= 0f && this.orgasming <= 0f)
		{
			this.cumIntensity = 0f;
			this.cumSpurtDelay = 0.5f + UnityEngine.Random.value * 0.5f;
			this.currentOrgasmDuration = 22f * this.data.orgasmDuration * (1f - this.data.orgasmAnticipationFactor + this.anticipation * this.data.orgasmAnticipationFactor);
			this.orgasming = this.currentOrgasmDuration;
			this.numberOfOrgasms++;
			this.timeSinceExpressionChange = 0.3f;
			this.pleasureEyeCheckToggleCooldown = 0.05f;
			this.closingEyesInPleasure = false;
			if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
			{
				this.apparatus.addOrgasmToCounter();
			}
			if (this.getCommandStatus("order_to_not_cum") > 0)
			{
				this.setCommandStatus("order_to_not_cum", -1);
			}
			if (this.getCommandStatus("order_to_cum") > 0)
			{
				this.setCommandStatus("order_to_cum", 2);
			}
		}
		this.orgasmPrevention = 0f;
		if (this.proximityToOrgasm >= 1f)
		{
			this.currentRefractoryDuration = 60f * this.data.refractoryDuration;
			this.refractory = this.currentRefractoryDuration;
		}
		this.processCum();
	}

	public void processCum()
	{
		if (this.orgasming > 0f)
		{
			this.cumIntensity += (this.data.cumVolume * Mathf.Pow(this.orgasming / this.currentOrgasmDuration, 2f) - this.cumIntensity) * this.cap(Time.deltaTime * 5f, 0f, 1f);
			if (this.cumSpurtDelay <= 0f)
			{
				this.playSound("cum" + this.nextCumSFX, Mathf.Pow(this.orgasming / this.currentOrgasmDuration, 3f), false);
				this.nextCumSFX = (this.nextCumSFX + 1) % 3;
				if (!UserSettings.data.mod_altCumStyle)
				{
					this.currentCumSpurt = (0.1f + this.cumIntensity) * this.data.cumSpurtFrequency * 0.25f;
					this.cumSpurtDelay += 1f + (this.data.cumSpurtFrequency - 1f) * 0.5f;
				}
				else
				{
					this.currentCumSpurt = this.cap(this.data.mod_cumSpurtRatio * 0.1f * this.cumIntensity + 0.1f + (this.cumIntensity + 0.1f) * this.data.cumSpurtFrequency * 0.25f, 0.02f, 9999f);
					this.cumSpurtDelay += this.cap(1.25f - this.data.mod_cumSpurtRatio + (1f - this.orgasming / this.currentOrgasmDuration) + (this.data.cumSpurtFrequency - 1f) * 0.25f, 0.1f, 2.1f);
				}
				this.cumSpurtTrimStart = this.currentCumSpurt * 0.3f;
				this.squirtSpurtTrimStart = this.currentCumSpurt * (0.3f + (0.7f - 0.7f * this.data.squirtAmount));
				this.cumWobbleFactor = 1f;
				if (this.currentPrecumDot != null)
				{
					this.currentPrecumDot.stillAttached = false;
					this.currentPrecumDot = null;
					this.precumDelay = 0f;
				}
				if (this.currentwetdropDot != null)
				{
					this.currentwetdropDot.stillAttached = false;
					this.currentwetdropDot = null;
					this.wetdropDelay = 0f;
				}
			}
		}
		if (this.currentCumSpurt > 0f)
		{
			this.currentCumSpurt -= Time.deltaTime;
			this.cumSpurtTrimStart -= Time.deltaTime;
			this.squirtSpurtTrimStart -= Time.deltaTime;
			if (this.currentCumSpurt < 0f)
			{
				this.currentCumSpurt = 0f;
			}
		}
		else
		{
			this.cumSpurtDelay -= Time.deltaTime * (1f + Mathf.Pow(this.orgasming / this.currentOrgasmDuration, 15f) * 6f);
		}
		this.orgasmTwitch += (this.cap(this.currentCumSpurt * 20f, 0f, 1f) - this.orgasmTwitch) * this.cap(Time.deltaTime * 12f * (1f + Mathf.Pow(this.orgasming / this.currentOrgasmDuration, 15f) * 8f), 0f, 1f);
		this.orgasmSoftPulse += (this.orgasmTwitch - this.orgasmSoftPulse) * this.cap(Time.deltaTime * 5f * (1f + Mathf.Pow(this.orgasming / this.currentOrgasmDuration, 15f) * 8f), 0f, 1f);
		this.emittingCum = (this.currentCumSpurt > 0f && this.cumSpurtTrimStart <= 0f && this.orgasming / this.currentOrgasmDuration > 0.3f);
		this.emittingSquirt = (this.currentCumSpurt > 0f && this.squirtSpurtTrimStart <= 0f && this.orgasming / this.currentOrgasmDuration > 0.3f);
		if (this.emittingCum)
		{
			this.cumDotEmitDelay += this.game.deltaTime;
			if (this.cumDotEmitDelay > 0f)
			{
				this.cumDotsToEmit = 0f;
				if (Game.rainbowJizzCheat)
				{
					this.col = ColorPicker.HsvToColor(Time.time * 2000f, 0.9f, 0.9f);
				}
				else
				{
					this.col = Cum.defaultCumColor;
				}
				if (this.showPenis)
				{
					this.lastCumVelocity = this.cumVelocity;
					this.cumVelocity = -this.bones.Penis4.right * this.currentCumEmitThickness * 20f / this.data.cumVolume * this.data.cumSpurtStrength * 0.8f;
					this.cumVelocity += this.bones.Penis4.up * Mathf.Cos((this.data.height + 0.75f) * Time.time * 21f) * this.cumVelocity.magnitude * 0.15f * this.cumWobbleFactor;
					this.cumVelocity += this.bones.Penis4.up * Mathf.Cos((this.data.height + 0.71f) * Time.time * 17f) * this.cumVelocity.magnitude * 0.15f * this.cumWobbleFactor;
					this.cumVelocity += this.bones.Penis4.forward * Mathf.Cos((this.data.height + 0.73f) * Time.time * 19f) * this.cumVelocity.magnitude * 0.1f;
					if (this.lastCumVelocity.magnitude == 0f || !this.wasEmittingCum)
					{
						this.lastCumVelocity = this.cumVelocity;
					}
					this.grav = Physics.gravity;
					if (!this.wasEmittingCum)
					{
						this.grav *= 0f;
					}
					this.lastPenisTip += (this.lastCumVelocity + this.grav * this.cumDotEmitDelay) * this.cumDotEmitDelay;
					if (!this.wasEmittingCum)
					{
						this.lastPenisTip = this.penisTip(true);
					}
					this.lastCumVelocity += this.grav * this.cumDotEmitDelay;
					this.cumEmitPositionDifference = this.penisTip(true) - this.lastPenisTip;
					this.cumEmitVelocityDifference = this.cumVelocity - this.lastCumVelocity;
					this.penisHeadBuried = (this.penisHeadBuried || (this.penisBurialOrifice == 2 && this.penisBeingBuried));
					if (!this.crotchCoveredByClothing)
					{
						if (this.penisHeadBuried)
						{
							switch (this.penisBurialOrifice)
							{
							case 2:
								this.penisBurialTarget.cumInMouth += this.currentCumEmitThickness * this.data.cumVolume;
								this.penisBurialTarget.cumInMouthColor = this.col;
								break;
							case 1:
								this.penisBurialTarget.cumInVagina += this.currentCumEmitThickness * this.data.cumVolume;
								this.penisBurialTarget.cumInVaginaColor = this.col;
								break;
							case 0:
								this.penisBurialTarget.cumInAnus += this.currentCumEmitThickness * this.data.cumVolume;
								this.penisBurialTarget.cumInAnusColor = this.col;
								break;
							}
						}
						else if (Game.allowCum)
						{
							this.cumDotsToEmit = Mathf.Floor(this.cumDotEmitDelay / this.cumDotEmitFrequency);
							for (float num = 0f; num < this.cumDotsToEmit; num += 1f)
							{
								this.v32 = this.lastCumVelocity + this.cumEmitVelocityDifference * num / this.cumDotsToEmit;
								this.emitParams.position = this.lastPenisTip + this.cumEmitPositionDifference * num / this.cumDotsToEmit;
								this.emitParams.velocity = this.v32;
								this.emitParams.startSize = 0.03f;
								this.emitParams.startColor = this.col;
								RackCharacter.cumEmitter.Emit(this.emitParams, 1);
								this.cumDotEmitDelay -= this.cumDotEmitFrequency;
							}
							this.lastCumVelocity = this.cumVelocity;
						}
					}
				}
			}
		}
		else
		{
			this.cumDotEmitDelay = 0f;
			this.previousCumDot = null;
		}
		if (this.emittingSquirt)
		{
			this.squirtDotEmitDelay += this.game.deltaTime;
			if (this.squirtDotEmitDelay > 0f)
			{
				this.cumDotsToEmit = 0f;
				if (Game.rainbowJizzCheat)
				{
					this.col = ColorPicker.HsvToColor(Time.time * 2000f, 0.9f, 0.9f);
				}
				else
				{
					this.col = Cum.defaultCumColor;
				}
				if (this.showVagina)
				{
					this.lastSquirtVelocity = this.squirtVelocity;
					if (this.vaginaBeingPenetrated)
					{
						this.squirtVelocity = -this.bones.VaginaLower_L.forward * this.currentCumEmitThickness * 20f / this.data.cumVolume * this.data.cumSpurtStrength * 0.8f;
					}
					else
					{
						this.squirtVelocity = -this.bones.VaginaLower_L.right * this.currentCumEmitThickness * 20f / this.data.cumVolume * this.data.cumSpurtStrength * 0.8f;
					}
					this.squirtVelocity += this.bones.VaginaLower_L.up * Mathf.Cos((this.data.height + 0.75f) * Time.time * 21f) * this.squirtVelocity.magnitude * 0.1f * this.cumWobbleFactor;
					this.squirtVelocity += this.bones.VaginaLower_L.up * Mathf.Cos((this.data.height + 0.71f) * Time.time * 17f) * this.squirtVelocity.magnitude * 0.1f * this.cumWobbleFactor;
					this.squirtVelocity += this.bones.VaginaLower_L.forward * Mathf.Cos((this.data.height + 0.73f) * Time.time * 19f) * this.squirtVelocity.magnitude * 0.05f;
					this.squirtVelocity *= 0.6f * this.data.squirtAmount;
					if (this.lastSquirtVelocity.magnitude == 0f || !this.wasEmittingSquirt)
					{
						this.lastSquirtVelocity = this.squirtVelocity;
					}
					this.grav = Physics.gravity;
					if (!this.wasEmittingSquirt)
					{
						this.grav *= 0f;
					}
					this.lastSquirtTip += (this.lastSquirtVelocity + this.grav * this.squirtDotEmitDelay) * this.squirtDotEmitDelay;
					if (!this.wasEmittingSquirt)
					{
						this.lastSquirtTip = this.bones.Clit.position;
					}
					this.lastSquirtVelocity += this.grav * this.squirtDotEmitDelay;
					this.squirtEmitPositionDifference = this.bones.Clit.position - this.lastSquirtTip;
					this.squirtEmitVelocityDifference = this.squirtVelocity - this.lastSquirtVelocity;
					if (!this.crotchCoveredByClothing && Game.allowCum)
					{
						this.squirtDotsToEmit = Mathf.Floor(this.squirtDotEmitDelay / this.cumDotEmitFrequency);
						for (float num2 = 0f; num2 < this.squirtDotsToEmit; num2 += 1f)
						{
							this.v32 = this.lastSquirtVelocity + this.squirtEmitVelocityDifference * num2 / this.squirtDotsToEmit;
							this.emitParams.position = this.lastSquirtTip + this.squirtEmitPositionDifference * num2 / this.squirtDotsToEmit;
							this.emitParams.velocity = this.v32;
							this.emitParams.startSize = 0.03f;
							this.emitParams.startColor = this.col;
							RackCharacter.cumEmitter.Emit(this.emitParams, 1);
							this.squirtDotEmitDelay -= this.cumDotEmitFrequency;
						}
						this.lastSquirtVelocity = this.squirtVelocity;
					}
				}
			}
		}
		else
		{
			this.squirtDotEmitDelay = 0f;
			this.previousCumDot = null;
		}
		if (this.emittingCum || this.emittingSquirt)
		{
			this.cumWobbleFactor -= this.cumWobbleFactor * this.cap(Time.deltaTime * 20f, 0f, 1f);
		}
		this.wasEmittingCum = this.emittingCum;
		this.wasEmittingSquirt = this.emittingSquirt;
		this.lastPenisTip = this.penisTip(true);
		if (this.currentCumSpurt > 0f)
		{
			this.currentCumEmitThickness += (this.cumIntensity * 0.5f - this.currentCumEmitThickness) * this.cap(Time.deltaTime * 15f / (1f + (this.data.cumSpurtFrequency - 1f) * 0.75f), 0f, 1f) * Cum.tScale;
		}
		else
		{
			this.currentCumEmitThickness -= Time.deltaTime * 25f / (1f + (this.data.cumSpurtFrequency - 1f) * 0.75f) * Cum.tScale;
			if (this.currentCumEmitThickness < 0f)
			{
				this.currentCumEmitThickness = 0f;
			}
		}
		if (this.penisHeadBuried)
		{
			this.arouse(1f);
		}
	}

	public void interactionStarted(string interaction)
	{
		this.involvedInteractions.Add(interaction);
	}

	public void interactionEnded(string interaction)
	{
		this.involvedInteractions.Remove(interaction);
	}

	public void addEnjoymentFromInteractions(int direction, string cause, float amount, bool secret)
	{
		if (!this.interactionsEnjoymentCauses.ContainsKey(cause))
		{
			this.interactionsEnjoymentCauses.Add(cause, 0f);
		}
		this.interactionsEnjoymentCauses[cause] = amount * (float)direction;
		if (direction == 1)
		{
			this.tar_enjoymentFromInteractions += amount;
			if (secret)
			{
				this.tar_secretEnjoymentFromInteractions += amount;
			}
		}
		else
		{
			this.tar_unenjoymentFromInteractions += amount * 0.2f;
		}
	}

	public void pollEnjoymentFromInteractions()
	{
		int num = 0;
		this.toysAlreadyAccountedFor.Clear();
		for (int i = 0; i < this.sexToySlots.Length; i++)
		{
			if (this.sexToySlots[i] != string.Empty && !this.sexToySlots[i].Contains("interaction.") && !this.toysAlreadyAccountedFor.Contains(this.sexToySlotProperties[i].uid))
			{
				this.toysAlreadyAccountedFor.Add(this.sexToySlotProperties[i].uid);
				num++;
			}
		}
		this.tar_enjoymentFromInteractions = 0f;
		this.tar_unenjoymentFromInteractions = 0f;
		this.tar_secretEnjoymentFromInteractions = 0f;
		foreach (string item in this.interactionsEnjoymentCauses.Keys.ToList())
		{
			this.interactionsEnjoymentCauses[item] = 0f;
		}
		for (int j = 0; j < this.involvedInteractions.Count; j++)
		{
			Interaction.getRelevantPreferenceFromInteraction(this.involvedInteractions[j], out this.interactionPref[0], out this.interactionPref[1], out this.interactionPref[2], out this.interactionPref[3]);
			this.numberOfInteractionPrefs = 0f;
			for (int k = 0; k < this.interactionPref.Length; k++)
			{
				if (this.interactionPref[k] != string.Empty)
				{
					this.numberOfInteractionPrefs += 1f;
				}
			}
			for (int l = 0; l < this.interactionPref.Length; l++)
			{
				float num2;
				if (this.interactionPref[l] != string.Empty)
				{
					num2 = this.preferences[this.interactionPref[l]];
					if (this.interactionPref[l].Contains("sextoy_"))
					{
						if (this.interactionPref[l] != "sextoy_general")
						{
							num2 *= this.preferences["sextoy_general"];
						}
						num++;
					}
				}
				else
				{
					num2 = 0f;
				}
				if (num2 > 0f)
				{
					this.addEnjoymentFromInteractions(1, this.interactionPref[l], num2 / this.numberOfInteractionPrefs, this.preferenceKnowledge[this.interactionPref[l]] != 2);
				}
				else if (num2 < 0f)
				{
					this.addEnjoymentFromInteractions(-1, this.interactionPref[l], (0f - num2) / this.numberOfInteractionPrefs, this.preferenceKnowledge[this.interactionPref[l]] != 2);
				}
			}
		}
		this.numberOfInteractionsFromSexToys = num;
		foreach (string item2 in this.sensationTimes.Keys.ToList())
		{
			if (this.sensationTimes[item2] > 0f)
			{
				Dictionary<string, float> dictionary;
				string key;
				(dictionary = this.sensationTimes)[key = item2] = dictionary[key] - this.pollTime;
				if (this.preferences[item2] > 0.5f)
				{
					this.addEnjoymentFromInteractions(1, item2, this.preferences[item2] - 0.5f, this.preferenceKnowledge[item2] != 2);
				}
				else
				{
					this.addEnjoymentFromInteractions(-1, item2, 0.5f - this.preferences[item2], this.preferenceKnowledge[item2] != 2);
				}
			}
			else
			{
				this.sensationTimes[item2] = 0f;
			}
		}
	}

	public void takeUpSlot(string slot, int uid)
	{
		if (!(this.sexToySlots[RackCharacter.getSextoySlotByName(slot.ToUpper())] != string.Empty))
		{
			this.sexToySlots[RackCharacter.getSextoySlotByName(slot.ToUpper())] = "interaction." + uid;
		}
	}

	public void freeUpSlot(string slot, int uid)
	{
		if (!(this.sexToySlots[RackCharacter.getSextoySlotByName(slot.ToUpper())] != "interaction." + uid))
		{
			this.sexToySlots[RackCharacter.getSextoySlotByName(slot.ToUpper())] = string.Empty;
		}
	}

	public void assignToInteraction(Interaction interaction)
	{
		this.currentInteractions.Add(interaction);
		switch (interaction.performingNode)
		{
		default:
		{
			this.currentlyUsingHandR = true;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = interaction.gizmo.transform.Find("mountPoint");
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 1f;
			string type2 = interaction.type;
			if (type2 == null || !(type2 == "fingeranus"))
			{
				this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, this.bones.Hand_R);
			}
			else
			{
				this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, this.bones.Finger00_R);
			}
			break;
		}
		case "handL":
		{
			this.currentlyUsingHandL = true;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = interaction.gizmo.transform.Find("mountPoint");
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 1f;
			string type = interaction.type;
			if (type == null || !(type == "fingeranus"))
			{
				this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, this.bones.Hand_L);
			}
			else
			{
				this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, this.bones.Finger00_L);
			}
			break;
		}
		case "penis":
			this.usingDefaultBodyTarget = false;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = interaction.gizmo.transform.Find("mountPoint");
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, null);
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.UpperLeg_L);
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.UpperLeg_R);
			this.currentlyUsingPenis = true;
			break;
		case "butt":
			this.usingDefaultBodyTarget = false;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = interaction.gizmo.transform.Find("mountPoint");
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, null);
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.Penis0);
			this.currentlyUsingButt = true;
			break;
		case "vagina":
			this.usingDefaultBodyTarget = false;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = interaction.gizmo.transform.Find("mountPoint");
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, null);
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.Penis0);
			this.currentlyUsingVagina = true;
			break;
		case "mouth":
			this.headInteractionMountPoint = interaction.gizmo.transform.Find("mountPoint");
			this.headTarget.position = interaction.gizmo.transform.Find("mountPoint").position;
			this.headTarget.rotation = interaction.gizmo.transform.Find("mountPoint").rotation;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, false, null);
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().bendWeight = 1f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().rotationWeight = 1f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().positionWeight = 0.5f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().CCDWeight = 1f;
			this.currentlyUsingMouth = true;
			break;
		}
		this.interactingWithSelf = interaction.selfInteraction;
		switch (interaction.type)
		{
		default:
			this.interactionStartCenter.x = this.mX;
			this.interactionStartCenter.y = this.mY;
			break;
		case "fuckanus":
			this.interactionStartCenter.x = this.mX;
			this.interactionStartCenter.y = this.mY - 0.25f;
			break;
		case "analridedick":
			this.interactionStartCenter.x = this.mX;
			this.interactionStartCenter.y = this.mY + 0.25f;
			break;
		}
		this.currentSexToy = this.getCurrentSexToy();
		this.usingSexToy = (this.currentSexToy != null);
	}

	public void removeFromInteraction(Interaction interaction)
	{
		switch (interaction.performingNode)
		{
		default:
			this.currentlyUsingHandR = false;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.idleHandRTarget;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 1f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.Hand_R);
			break;
		case "handL":
			this.currentlyUsingHandL = false;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.idleHandLTarget;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 1f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, this.bones.Hand_L);
			break;
		case "penis":
			this.usingDefaultBodyTarget = true;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = this.bodyTarget;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, null);
			this.recentlyRemovedPenisFromInteraction = 0.1f;
			this.currentlyUsingPenis = false;
			break;
		case "butt":
			this.usingDefaultBodyTarget = true;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = this.bodyTarget;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, null);
			this.currentlyUsingButt = false;
			break;
		case "vagina":
			this.usingDefaultBodyTarget = true;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.target = this.bodyTarget;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = 0f;
			this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = 0f;
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, null);
			this.currentlyUsingVagina = false;
			break;
		case "mouth":
			this.ignoreCollisions(interaction.targetCharacter.GO.transform, true, null);
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().bendWeight = 0f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().rotationWeight = 0f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().positionWeight = 0f;
			((Component)this.headTarget).GetComponent<FBBIKHeadEffector>().CCDWeight = 0f;
			this.currentlyUsingMouth = false;
			break;
		}
		if (interaction.selfInteraction)
		{
			this.interactingWithSelf = false;
		}
		this.currentInteractions.Remove(interaction);
		if (this.currentInteractions.Count <= 0 && this.interactionSubject != null && (UnityEngine.Object)this.interactionSubject.apparatus != (UnityEngine.Object)null && !this.interactionSubject.apparatus.publicPoseNames.Contains(this.curSexPoseName))
		{
			this.setSexPose(0);
		}
		this.currentSexToy = this.getCurrentSexToy();
		this.usingSexToy = (this.currentSexToy != null);
	}

	public void setSexPose(int p)
	{
		if (!this.interactingWithSelf)
		{
			this.curSexPose = p;
			if (p == 0)
			{
				this.interactWithTestSubject(this.interactionSubject, this.interactionSubject.apparatus.getApproachPointByName("default"), this.interactionSubject.apparatus);
				this.curSexPoseName = "default";
			}
			else
			{
				this.interactWithTestSubject(this.interactionSubject, this.interactionSubject.apparatus.getApproachPointByName(this.interactionSubject.apparatus.poseNames[p - 1].ToString()), this.interactionSubject.apparatus);
				this.curSexPoseName = this.interactionSubject.apparatus.poseNames[p - 1].ToString();
			}
			this.resetFootTargets();
			this.interactionSubject.apparatus.positionPerformerBasedOnSelectedPose(this, this.curSexPoseName);
			for (int i = 1; i < 5; i++)
			{
				this.penisbones[i].Rotate(-35f, 0f, 0f);
			}
		}
	}

	public void interactWithTestSubject(RackCharacter testSubject, Transform approachPoint, BondageApparatus onApparatus)
	{
		if (this.controlledByPlayer && this.interactionSubject == null)
		{
			if (UserSettings.needTutorial("NPT_DISMISS_THE_SUBJECT"))
			{
				this.game.interactionZoom = 0f;
				this.game.interactionCamX = 0.7f;
				this.game.interactionCamY = 0.4f;
			}
			else
			{
				this.game.interactionZoom = 0.5f;
				this.game.interactionCamX = 0f;
				this.game.interactionCamY = 0f;
			}
		}
		this.interactionSubject = testSubject;
		this.interactionApparatus = onApparatus;
		this.ignoreCollisions(this.interactionApparatus.transform, false, null);
		this.ignoreCollisions(this.interactionSubject.GO.transform, false, this.bones.Belly);
		this.interactionApparatus.createPerformerMountPoints(this);
	}

	public void leaveInteraction()
	{
		if (this.controlMode == 3 && this.currentInteractions.Count > 0)
		{
			this.controlMode = 2;
		}
		else
		{
			if (this.controlledByPlayer)
			{
				this.game.confirmingTerminate = false;
				this.game.camFollowDist = 6f;
			}
			this.interactionSubject = null;
			this.interactionPoint = null;
			this.lastInteractionPointName = string.Empty;
			this.ignoreCollisions(this.interactionApparatus.transform, true, null);
			this.resetIdleHandTarget(false);
			this.resetIdleHandTarget(true);
			this.resetElbowTarget(false);
			this.resetElbowTarget(true);
			this.resetKneeTarget(false);
			this.resetKneeTarget(true);
			this.resetFootTargets();
			this.interactionApparatus = null;
			this.curSexPose = 0;
			this.curSexPoseName = "default";
		}
		this.destroyPerformerMountPoints();
	}

	public void destroyPerformerMountPoints()
	{
		for (int i = 0; i < this.temporaryPerformerMountPoints.Count; i++)
		{
			UnityEngine.Object.Destroy(this.temporaryPerformerMountPoints[i]);
		}
		this.temporaryPerformerMountPoints = new List<GameObject>();
	}

	public void setFootTargets(Transform leftFoot, Transform rightFoot)
	{
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.target = leftFoot;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.positionWeight = 1f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.rotationWeight = 1f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.target = rightFoot;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight = 1f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.rotationWeight = 1f;
		this.hasFootTargets = true;
	}

	public void resetFootTargets()
	{
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.target = this.defaultLeftFootTarget;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.positionWeight = 0f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.rotationWeight = 0f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.target = this.defaultRightFootTarget;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight = 0f;
		this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.rotationWeight = 0f;
		this.hasFootTargets = false;
	}

	public void processInteraction()
	{
		this.isInteractionSubject = false;
		if (this.game.PC().interactionSubject != null)
		{
			this.isInteractionSubject = (this.game.PC().interactionSubject.uid == this.uid);
		}
		if ((UnityEngine.Object)this.interactionPoint != (UnityEngine.Object)null)
		{
			((Component)this.interactionPoint).GetComponent<ApproachPoint>().performerScale = this.height_act / this.interactionSubject.height_act;
			this.interactionApparatus.ongoingPositionPerformer(this, this.curSexPoseName);
			if (Input.GetKeyDown(KeyCode.KeypadMultiply))
			{
				this.debugIKtargets();
			}
		}
		if (this.currentlyUsingMouth)
		{
			this.headTarget.position = this.headInteractionMountPoint.position;
			this.headTarget.rotation = this.headInteractionMountPoint.rotation;
			this.allowLimitedEyeMovement = true;
		}
	}

	public void debugIKtargets()
	{
		Transform transform = this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target;
		string text = string.Empty;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text = "// LEFT HAND";
			text += "\r\n// Position";
			object[] obj = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition = transform.localPosition;
			obj[2] = localPosition.x;
			obj[3] = "f;";
			text = string.Concat(obj);
			object[] obj2 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition2 = transform.localPosition;
			obj2[2] = localPosition2.y;
			obj2[3] = "f;";
			text = string.Concat(obj2);
			object[] obj3 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition3 = transform.localPosition;
			obj3[2] = localPosition3.z;
			obj3[3] = "f;";
			text = string.Concat(obj3);
			text += "\r\nLHP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj4 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles = transform.localEulerAngles;
			obj4[2] = localEulerAngles.x;
			obj4[3] = "f;";
			text = string.Concat(obj4);
			object[] obj5 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles2 = transform.localEulerAngles;
			obj5[2] = localEulerAngles2.y;
			obj5[3] = "f;";
			text = string.Concat(obj5);
			object[] obj6 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles3 = transform.localEulerAngles;
			obj6[2] = localEulerAngles3.z;
			obj6[3] = "f;";
			text = string.Concat(obj6);
			text += "\r\nLHP.transform.localEulerAngles = v3;";
		}
		transform = this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text += "\r\n\r\n// RIGHT HAND";
			text += "\r\n// Position";
			object[] obj7 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition4 = transform.localPosition;
			obj7[2] = localPosition4.x;
			obj7[3] = "f;";
			text = string.Concat(obj7);
			object[] obj8 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition5 = transform.localPosition;
			obj8[2] = localPosition5.y;
			obj8[3] = "f;";
			text = string.Concat(obj8);
			object[] obj9 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition6 = transform.localPosition;
			obj9[2] = localPosition6.z;
			obj9[3] = "f;";
			text = string.Concat(obj9);
			text += "\r\nRHP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj10 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles4 = transform.localEulerAngles;
			obj10[2] = localEulerAngles4.x;
			obj10[3] = "f;";
			text = string.Concat(obj10);
			object[] obj11 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles5 = transform.localEulerAngles;
			obj11[2] = localEulerAngles5.y;
			obj11[3] = "f;";
			text = string.Concat(obj11);
			object[] obj12 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles6 = transform.localEulerAngles;
			obj12[2] = localEulerAngles6.z;
			obj12[3] = "f;";
			text = string.Concat(obj12);
			text += "\r\nRHP.transform.localEulerAngles = v3;";
		}
		transform = this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.bendConstraint.bendGoal;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text += "\r\n\r\n// LEFT ELBOW";
			text += "\r\n// Position";
			object[] obj13 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition7 = transform.localPosition;
			obj13[2] = localPosition7.x;
			obj13[3] = "f;";
			text = string.Concat(obj13);
			object[] obj14 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition8 = transform.localPosition;
			obj14[2] = localPosition8.y;
			obj14[3] = "f;";
			text = string.Concat(obj14);
			object[] obj15 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition9 = transform.localPosition;
			obj15[2] = localPosition9.z;
			obj15[3] = "f;";
			text = string.Concat(obj15);
			text += "\r\nLEP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj16 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles7 = transform.localEulerAngles;
			obj16[2] = localEulerAngles7.x;
			obj16[3] = "f;";
			text = string.Concat(obj16);
			object[] obj17 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles8 = transform.localEulerAngles;
			obj17[2] = localEulerAngles8.y;
			obj17[3] = "f;";
			text = string.Concat(obj17);
			object[] obj18 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles9 = transform.localEulerAngles;
			obj18[2] = localEulerAngles9.z;
			obj18[3] = "f;";
			text = string.Concat(obj18);
			text += "\r\nLEP.transform.localEulerAngles = v3;";
		}
		transform = this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.bendConstraint.bendGoal;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text += "\r\n\r\n// RIGHT ELBOW";
			text += "\r\n// Position";
			object[] obj19 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition10 = transform.localPosition;
			obj19[2] = localPosition10.x;
			obj19[3] = "f;";
			text = string.Concat(obj19);
			object[] obj20 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition11 = transform.localPosition;
			obj20[2] = localPosition11.y;
			obj20[3] = "f;";
			text = string.Concat(obj20);
			object[] obj21 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition12 = transform.localPosition;
			obj21[2] = localPosition12.z;
			obj21[3] = "f;";
			text = string.Concat(obj21);
			text += "\r\nREP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj22 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles10 = transform.localEulerAngles;
			obj22[2] = localEulerAngles10.x;
			obj22[3] = "f;";
			text = string.Concat(obj22);
			object[] obj23 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles11 = transform.localEulerAngles;
			obj23[2] = localEulerAngles11.y;
			obj23[3] = "f;";
			text = string.Concat(obj23);
			object[] obj24 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles12 = transform.localEulerAngles;
			obj24[2] = localEulerAngles12.z;
			obj24[3] = "f;";
			text = string.Concat(obj24);
			text += "\r\nREP.transform.localEulerAngles = v3;";
		}
		transform = this.GO.GetComponent<FullBodyBipedIK>().solver.leftLegChain.bendConstraint.bendGoal;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text += "\r\n\r\n// LEFT KNEE";
			text += "\r\n// Position";
			object[] obj25 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition13 = transform.localPosition;
			obj25[2] = localPosition13.x;
			obj25[3] = "f;";
			text = string.Concat(obj25);
			object[] obj26 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition14 = transform.localPosition;
			obj26[2] = localPosition14.y;
			obj26[3] = "f;";
			text = string.Concat(obj26);
			object[] obj27 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition15 = transform.localPosition;
			obj27[2] = localPosition15.z;
			obj27[3] = "f;";
			text = string.Concat(obj27);
			text += "\r\nLKP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj28 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles13 = transform.localEulerAngles;
			obj28[2] = localEulerAngles13.x;
			obj28[3] = "f;";
			text = string.Concat(obj28);
			object[] obj29 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles14 = transform.localEulerAngles;
			obj29[2] = localEulerAngles14.y;
			obj29[3] = "f;";
			text = string.Concat(obj29);
			object[] obj30 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles15 = transform.localEulerAngles;
			obj30[2] = localEulerAngles15.z;
			obj30[3] = "f;";
			text = string.Concat(obj30);
			text += "\r\nLKP.transform.localEulerAngles = v3;";
		}
		transform = this.GO.GetComponent<FullBodyBipedIK>().solver.rightLegChain.bendConstraint.bendGoal;
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)transform.parent == (UnityEngine.Object)null)
			{
				transform = ((Component)transform).GetComponent<GlobalFollowerPoint>().mother.transform;
			}
			text += "\r\n\r\n// RIGHT KNEE";
			text += "\r\n// Position";
			object[] obj31 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localPosition16 = transform.localPosition;
			obj31[2] = localPosition16.x;
			obj31[3] = "f;";
			text = string.Concat(obj31);
			object[] obj32 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localPosition17 = transform.localPosition;
			obj32[2] = localPosition17.y;
			obj32[3] = "f;";
			text = string.Concat(obj32);
			object[] obj33 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localPosition18 = transform.localPosition;
			obj33[2] = localPosition18.z;
			obj33[3] = "f;";
			text = string.Concat(obj33);
			text += "\r\nRKP.transform.localPosition = v3;";
			text += "\r\n// Rotation";
			object[] obj34 = new object[4]
			{
				text,
				"\r\nv3.x = ",
				null,
				null
			};
			Vector3 localEulerAngles16 = transform.localEulerAngles;
			obj34[2] = localEulerAngles16.x;
			obj34[3] = "f;";
			text = string.Concat(obj34);
			object[] obj35 = new object[4]
			{
				text,
				"\r\nv3.y = ",
				null,
				null
			};
			Vector3 localEulerAngles17 = transform.localEulerAngles;
			obj35[2] = localEulerAngles17.y;
			obj35[3] = "f;";
			text = string.Concat(obj35);
			object[] obj36 = new object[4]
			{
				text,
				"\r\nv3.z = ",
				null,
				null
			};
			Vector3 localEulerAngles18 = transform.localEulerAngles;
			obj36[2] = localEulerAngles18.z;
			obj36[3] = "f;";
			text = string.Concat(obj36);
			text += "\r\nRKP.transform.localEulerAngles = v3;";
		}
		Debug.Log(text);
	}

	public void pollPenisLength()
	{
		float num = RackCharacter.originalPenisLengthMinusHead;
		Vector3 lossyScale = this.bones.Pubic.lossyScale;
		this.penisLengthInWorldUnits_tar = num * lossyScale.x * this.penisArousalScale + RackCharacter.originalPenisHeadLength;
	}

	public Vector3 penisTip(bool afterIK = false)
	{
		if (afterIK && this.aikPenisTip != Vector3.zero && UserSettings.data.mod_fixNpcCum)
		{
			goto IL_0030;
		}
		if (afterIK && !UserSettings.data.mod_fixNpcCum)
		{
			goto IL_0030;
		}
		return this.bones.Penis4.position - this.bones.Penis4.right * 0.1856f * this.bones.Penis4.localScale.x;
		IL_0030:
		return this.aikPenisTip;
	}

	public void pollPenisGirth(bool force = false)
	{
		if (this.controlledByPlayer && this.game.customizingCharacter)
		{
			return;
		}
		if (force)
		{
			this.needGirthUpdate = true;
		}
		if (this.polledGirths.Count != 0 && !this.needGirthUpdate)
		{
			return;
		}
		if (!this.girthsInitted)
		{
			this.polledGirths = new List<List<List<float>>>();
			for (int i = 0; i < this.girthArousalSegments; i++)
			{
				this.polledGirths.Add(new List<List<float>>());
				for (int j = 0; j < this.girthPollingSegments; j++)
				{
					this.polledGirths[i].Add(new List<float>());
					for (int k = 0; k < 7; k++)
					{
						this.polledGirths[i][j].Add(0f);
					}
				}
			}
			this.girthsInitted = true;
		}
		Vector3 localPosition = this.parts[this.penisPieceIndex].transform.localPosition;
		Vector3 localScale = this.parts[this.penisPieceIndex].transform.localScale;
		this.parts[this.penisPieceIndex].transform.localPosition = Vector3.zero;
		Transform transform = this.parts[this.penisPieceIndex].transform;
		Vector3 one = Vector3.one;
		Vector3 localScale2 = this.GO.transform.localScale;
		transform.localScale = one * (1f / localScale2.x);
		this.preciseMousePickingCollider[this.penisPieceIndex].layer = 12;
		this.preciseMousePickingCollider[this.penisPieceIndex].transform.localPosition = Vector3.zero;
		this.preciseMousePickingCollider[this.penisPieceIndex].transform.localRotation = Quaternion.identity;
		this.preciseMousePickingCollider[this.penisPieceIndex].transform.localScale = Vector3.one;
		((Component)this.preciseMousePickingCollider[this.penisPieceIndex].transform.parent).GetComponent<SkinnedMeshRenderer>().BakeMesh(this.preciseMousePickingCollider[this.penisPieceIndex].GetComponent<MeshCollider>().sharedMesh);
		this.preciseMousePickingCollider[this.penisPieceIndex].GetComponent<MeshCollider>().enabled = true;
		this.preciseMousePickingCollider[this.penisPieceIndex].GetComponent<MeshCollider>().enabled = false;
		this.preciseMousePickingCollider[this.penisPieceIndex].GetComponent<MeshCollider>().enabled = true;
		this.pollingPenisGirth = true;
		this.pollPenisLength();
		for (int l = 0; l < this.girthArousalSegments; l++)
		{
			for (int m = 0; m < this.girthPollingSegments; m++)
			{
				Vector3 a = RackCharacter.positionAlongPenetrator(this.bones.Penis0, this.bones.Penis4, (float)m / (float)(this.girthPollingSegments - 1), true, this.bones.Penis0, this.bones.Penis1, this.bones.Penis2, this.bones.Penis3, this.bones.Penis4);
				for (int n = 0; n < 6; n++)
				{
					if (n == 0 || n == 3)
					{
						this.polledGirths[l][m][n] = 0f;
					}
					else
					{
						this.v3 = this.directionAlongPenetrator(n % 3, this.bones.Penis0, this.bones.Penis4, (float)m / (float)(this.girthPollingSegments - 1), true, this.bones.Penis0, this.bones.Penis1, this.bones.Penis2, this.bones.Penis3, this.bones.Penis4);
						if (n >= 3)
						{
							this.v3 *= -1f;
						}
						RaycastHit raycastHit = default(RaycastHit);
						if (Physics.Raycast(new Ray(a + this.v3, -this.v3), out raycastHit, this.v3.magnitude, LayerMask.GetMask("PreciseRaycasting")))
						{
							this.polledGirths[l][m][n] = this.v3.magnitude - raycastHit.distance;
						}
						else
						{
							this.polledGirths[l][m][n] = 0f;
						}
					}
				}
				float num = this.polledGirths[l][m][1] + this.polledGirths[l][m][4];
				if (this.polledGirths[l][m][2] + this.polledGirths[l][m][5] > num)
				{
					num = this.polledGirths[l][m][2] + this.polledGirths[l][m][5];
				}
				this.polledGirths[l][m][6] = num;
			}
		}
		this.parts[this.penisPieceIndex].transform.localPosition = localPosition;
		this.parts[this.penisPieceIndex].transform.localScale = localScale;
		this.preciseMousePickingCollider[this.penisPieceIndex].GetComponent<MeshCollider>().enabled = false;
		this.needGirthUpdate = false;
	}

	public float girthAlongPenetrator(float position, bool penisBaseLimit = true)
	{
		if (penisBaseLimit && position < 0.1f && (this.knotSwell < 0.2f || !this.data.hasKnot))
		{
			position = 0.1f;
		}
		position = this.cap(position, 0f, 1f);
		int num = Mathf.FloorToInt(position * (float)(this.girthPollingSegments - 1));
		int index = Mathf.FloorToInt(this.arousal * (float)(this.girthArousalSegments - 1));
		float num2 = this.polledGirths[index][num][6];
		if (num < this.girthPollingSegments - 1)
		{
			float num3 = position * (float)(this.girthPollingSegments - 1) - (float)num;
			num2 *= 1f - num3;
			num2 += num3 * this.polledGirths[index][num + 1][6];
		}
		if (num2 < 0.005f)
		{
			num2 = 0.005f;
		}
		return num2;
	}

	public Vector3 girthPointAlongPenetrator(int direction, Transform penetratorRootBone, Transform penetratorTipBone, float position, bool penetratorHasPenisStructure = false, Transform penis0 = null, Transform penis1 = null, Transform penis2 = null, Transform penis3 = null, Transform penis4 = null)
	{
		position = this.cap(position, 0f, 1f);
		bool flag = false;
		if (direction >= 3)
		{
			flag = true;
			direction -= 3;
		}
		this.gpap = this.directionAlongPenetrator(direction, penetratorRootBone, penetratorTipBone, position, penetratorHasPenisStructure, penis0, penis1, penis2, penis3, penis4);
		if (flag)
		{
			this.gpap *= -1f;
		}
		int num = Mathf.FloorToInt(position * (float)(this.girthPollingSegments - 1));
		int index = Mathf.FloorToInt(this.arousal * (float)(this.girthArousalSegments - 1));
		float num2 = this.polledGirths[index][num][direction];
		if (num < this.girthPollingSegments - 1)
		{
			float num3 = position * (float)(this.girthPollingSegments - 1) - (float)num;
			num2 *= 1f - num3;
			num2 += num3 * this.polledGirths[index][num + 1][direction];
		}
		if (num2 < 0.005f)
		{
			num2 = 0.005f;
		}
		return this.gpap * num2;
	}

	public void buryPenis(Vector3 orifice, Vector3 target, Vector3 insidePoint, RackCharacter targetChar, int orificeType)
	{
		this.penisBuryOrifice = orifice;
		this.penisBuryTarget = target;
		this.buryTarDistance = (this.penisBuryOrifice - this.penisBuryInsidePoint).magnitude;
		this.penisBuryInsidePoint = insidePoint;
		this.penisBeingBuried = true;
		this.penisBurialTarget = targetChar;
		this.penisBurialOrifice = orificeType;
	}

	public Vector3 directionAlongPenetrator(int direction, Transform penetratorRootBone, Transform penetratorTipBone, float position, bool penetratorHasPenisStructure = false, Transform penis0 = null, Transform penis1 = null, Transform penis2 = null, Transform penis3 = null, Transform penis4 = null)
	{
		position = this.cap(position, 0f, 1f);
		if (penetratorHasPenisStructure)
		{
			if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff3)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff3) / (1f - RackCharacter.positionAlongPenetratorPenisStructureCutoff3);
				switch (direction)
				{
				case 0:
					this.dap = -penis4.right;
					break;
				case 1:
					this.dap = -penis4.forward;
					break;
				case 2:
					this.dap = penis4.up;
					break;
				}
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff2)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff2) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff3 - RackCharacter.positionAlongPenetratorPenisStructureCutoff2);
				switch (direction)
				{
				case 0:
					this.dap = -penis3.right;
					this.dap += (-penis4.right - this.dap) * position;
					break;
				case 1:
					this.dap = penis3.forward;
					this.dap += (-penis4.forward - this.dap) * position;
					break;
				case 2:
					this.dap = -penis3.up;
					this.dap += (penis4.up - this.dap) * position;
					break;
				}
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff1)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff1) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff2 - RackCharacter.positionAlongPenetratorPenisStructureCutoff1);
				switch (direction)
				{
				case 0:
					this.dap = -penis2.right;
					this.dap += (-penis3.right - this.dap) * position;
					break;
				case 1:
					this.dap = penis2.forward;
					this.dap += (penis3.forward - this.dap) * position;
					break;
				case 2:
					this.dap = -penis2.up;
					this.dap += (-penis3.up - this.dap) * position;
					break;
				}
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff0)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff0) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff1 - RackCharacter.positionAlongPenetratorPenisStructureCutoff0);
				switch (direction)
				{
				case 0:
					this.dap = -penis1.right;
					this.dap += (-penis2.right - this.dap) * position;
					break;
				case 1:
					this.dap = penis1.forward;
					this.dap += (penis2.forward - this.dap) * position;
					break;
				case 2:
					this.dap = -penis1.up;
					this.dap += (-penis2.up - this.dap) * position;
					break;
				}
			}
			else
			{
				position /= RackCharacter.positionAlongPenetratorPenisStructureCutoff0;
				switch (direction)
				{
				case 0:
					this.dap = -penetratorRootBone.right;
					this.dap += (-penis1.right - this.dap) * position;
					break;
				case 1:
					this.dap = penetratorRootBone.forward;
					this.dap += (penis1.forward - this.dap) * position;
					break;
				case 2:
					this.dap = -penetratorRootBone.up;
					this.dap += (-penis1.up - this.dap) * position;
					break;
				}
			}
		}
		else
		{
			switch (direction)
			{
			case 0:
				this.dap = -penetratorRootBone.right;
				this.dap += (-penetratorTipBone.right - penetratorRootBone.position) * position;
				break;
			case 1:
				this.dap = penetratorRootBone.forward;
				this.dap += (penetratorTipBone.forward - penetratorRootBone.position) * position;
				break;
			case 2:
				this.dap = -penetratorRootBone.up;
				this.dap += (-penetratorTipBone.up - penetratorRootBone.position) * position;
				break;
			}
		}
		return this.dap;
	}

	public static Vector3 positionAlongPenetrator(Transform penetratorRootBone, Transform penetratorTipBone, float position, bool penetratorHasPenisStructure = false, Transform penis0 = null, Transform penis1 = null, Transform penis2 = null, Transform penis3 = null, Transform penis4 = null)
	{
		if (penetratorHasPenisStructure)
		{
			if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff3)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff3) / (1f - RackCharacter.positionAlongPenetratorPenisStructureCutoff3);
				RackCharacter.pap = penis4.position;
				RackCharacter.pap += penis4.right * -0.35f * position;
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff2)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff2) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff3 - RackCharacter.positionAlongPenetratorPenisStructureCutoff2);
				RackCharacter.pap = penis3.position;
				RackCharacter.pap += (penis4.position - RackCharacter.pap) * position;
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff1)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff1) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff2 - RackCharacter.positionAlongPenetratorPenisStructureCutoff1);
				RackCharacter.pap = penis2.position;
				RackCharacter.pap += (penis3.position - RackCharacter.pap) * position;
			}
			else if (position > RackCharacter.positionAlongPenetratorPenisStructureCutoff0)
			{
				position = (position - RackCharacter.positionAlongPenetratorPenisStructureCutoff0) / (RackCharacter.positionAlongPenetratorPenisStructureCutoff1 - RackCharacter.positionAlongPenetratorPenisStructureCutoff0);
				RackCharacter.pap = penis1.position;
				RackCharacter.pap += (penis2.position - RackCharacter.pap) * position;
			}
			else
			{
				position /= RackCharacter.positionAlongPenetratorPenisStructureCutoff0;
				RackCharacter.pap = penetratorRootBone.position;
				RackCharacter.pap += (penis1.position - RackCharacter.pap) * position;
			}
		}
		else
		{
			RackCharacter.pap = penetratorRootBone.position;
			RackCharacter.pap += (penetratorTipBone.position - penetratorRootBone.position) * position;
		}
		return RackCharacter.pap;
	}

	public void clenchHandL(float c, bool aroundPole = false, int finger = -1, float spread = 0.5f, float rootBend = -99f)
	{
		if (rootBend == -99f)
		{
			rootBend = c;
		}
		if (finger == -1)
		{
			for (int i = 0; i < this.handClenchL.Count; i++)
			{
				this.handClenchL[i] = c;
				this.handClenchRootL[i] = rootBend;
			}
		}
		else
		{
			this.handClenchL[finger] = c;
			this.handClenchRootL[finger] = rootBend;
		}
		if (spread != 0.5f)
		{
			this.fingerSpreadL = spread;
		}
		this.grabbingPoleL = (aroundPole || this.grabbingPoleL);
		this.clenchingHandL = true;
	}

	public void clenchHandR(float c, bool aroundPole = false, int finger = -1, float spread = 0.5f, float rootBend = -99f)
	{
		if (rootBend == -99f)
		{
			rootBend = c;
		}
		if (finger == -1)
		{
			for (int i = 0; i < this.handClenchR.Count; i++)
			{
				this.handClenchR[i] = c;
				this.handClenchRootR[i] = rootBend;
			}
		}
		else
		{
			this.handClenchR[finger] = c;
			this.handClenchRootR[finger] = rootBend;
		}
		if (spread != 0.5f)
		{
			this.fingerSpreadR = spread;
		}
		this.grabbingPoleR = (aroundPole || this.grabbingPoleR);
		this.clenchingHandR = true;
	}

	public void pointFingerAt(bool rightHand, int finger, Vector3 target)
	{
		switch (rightHand)
		{
		case true:
			switch (finger)
			{
			case 0:
				this.fingerTarget0R = target;
				break;
			case 1:
				this.fingerTarget1R = target;
				break;
			case 2:
				this.fingerTarget2R = target;
				break;
			case 3:
				this.fingerTarget3R = target;
				break;
			}
			break;
		case false:
			switch (finger)
			{
			case 0:
				this.fingerTarget0L = target;
				break;
			case 1:
				this.fingerTarget1L = target;
				break;
			case 2:
				this.fingerTarget2L = target;
				break;
			case 3:
				this.fingerTarget3L = target;
				break;
			}
			break;
		}
	}

	public Transform getFingerBone(int finger, int knuckle, int hand)
	{
		switch (hand)
		{
		case 0:
			switch (finger)
			{
			case 0:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger00_L;
				case 1:
					return this.bones.Finger01_L;
				case 2:
					return this.bones.Finger02_L;
				}
				break;
			case 1:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger10_L;
				case 1:
					return this.bones.Finger11_L;
				case 2:
					return this.bones.Finger12_L;
				}
				break;
			case 2:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger20_L;
				case 1:
					return this.bones.Finger21_L;
				case 2:
					return this.bones.Finger22_L;
				}
				break;
			case 3:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger30_L;
				case 1:
					return this.bones.Finger31_L;
				case 2:
					return this.bones.Finger32_L;
				}
				break;
			case 4:
				switch (knuckle)
				{
				case 0:
					return this.bones.Thumb0_L;
				case 1:
					return this.bones.Thumb1_L;
				case 2:
					return this.bones.Thumb2_L;
				}
				break;
			}
			break;
		case 1:
			switch (finger)
			{
			case 0:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger00_R;
				case 1:
					return this.bones.Finger01_R;
				case 2:
					return this.bones.Finger02_R;
				}
				break;
			case 1:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger10_R;
				case 1:
					return this.bones.Finger11_R;
				case 2:
					return this.bones.Finger12_R;
				}
				break;
			case 2:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger20_R;
				case 1:
					return this.bones.Finger21_R;
				case 2:
					return this.bones.Finger22_R;
				}
				break;
			case 3:
				switch (knuckle)
				{
				case 0:
					return this.bones.Finger30_R;
				case 1:
					return this.bones.Finger31_R;
				case 2:
					return this.bones.Finger32_R;
				}
				break;
			case 4:
				switch (knuckle)
				{
				case 0:
					return this.bones.Thumb0_R;
				case 1:
					return this.bones.Thumb1_R;
				case 2:
					return this.bones.Thumb2_R;
				}
				break;
			}
			break;
		}
		return null;
	}

	public void aimToy(Vector3 at)
	{
	}

	public void processHands()
	{
		if (this.customIdleHandLtarget && this.idleHandLclench != 0f && (UnityEngine.Object)this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target == (UnityEngine.Object)this.idleHandLTarget)
		{
			this.clenchHandL(this.idleHandLclench, false, -1, 0.5f, -99f);
		}
		if (this.customIdleHandRtarget && this.idleHandRclench != 0f && (UnityEngine.Object)this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target == (UnityEngine.Object)this.idleHandRTarget)
		{
			this.clenchHandR(this.idleHandRclench, false, -1, 0.5f, -99f);
		}
		if (Input.GetKey(KeyCode.Keypad1))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.idleHandLclench -= Time.deltaTime;
			}
			else
			{
				this.idleHandLclench += Time.deltaTime;
			}
			Debug.Log("LEFT CLENCH: " + this.idleHandLclench + ", " + this.idleHandLclenchT);
		}
		if (Input.GetKey(KeyCode.Keypad4))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.idleHandLclenchT -= Time.deltaTime;
			}
			else
			{
				this.idleHandLclenchT += Time.deltaTime;
			}
			Debug.Log("LEFT CLENCH: " + this.idleHandLclench + ", " + this.idleHandLclenchT);
		}
		if (Input.GetKey(KeyCode.Keypad2))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.idleHandRclench -= Time.deltaTime;
			}
			else
			{
				this.idleHandRclench += Time.deltaTime;
			}
			Debug.Log("RIGHT CLENCH: " + this.idleHandRclench + ", " + this.idleHandRclenchT);
		}
		if (Input.GetKey(KeyCode.Keypad5))
		{
			if (Input.GetKey(KeyCode.LeftAlt))
			{
				this.idleHandRclenchT -= Time.deltaTime;
			}
			else
			{
				this.idleHandRclenchT += Time.deltaTime;
			}
			Debug.Log("RIGHT CLENCH: " + this.idleHandRclench + ", " + this.idleHandRclenchT);
		}
		this.handClenchHowMuchHand = 1f;
		if (this.rightHandItem != this.rightHandItem_current)
		{
			if (this.rightHandItem_current != string.Empty)
			{
				this.killHeldItem(true);
			}
			this.putItemInHand(this.rightHandItem, true, string.Empty);
		}
		if (this.leftHandItem != this.leftHandItem_current)
		{
			if (this.leftHandItem_current != string.Empty)
			{
				this.killHeldItem(false);
			}
			this.putItemInHand(this.leftHandItem, false, string.Empty);
		}
		if (this.selectedSexToy != null && this.selectedSexToy.showHeld && this.interactionSubject == null && !this.aimingMainHand)
		{
			this.v3.x = 24f;
			this.v3.y = 27f;
			this.v3.z = 0f;
			this.bones.mainHand.Rotate(this.v3);
		}
		if (Game.allowHandPositioning)
		{
			if (this.leftHandItemHasFingerPoses)
			{
				this.mirrorTransform(this.hho_Finger00_R0, this.bones.Finger00_L, false);
				this.mirrorTransform(this.hho_Finger01_R0, this.bones.Finger01_L, false);
				this.mirrorTransform(this.hho_Finger02_R0, this.bones.Finger02_L, false);
				this.mirrorTransform(this.hho_Finger10_R0, this.bones.Finger10_L, false);
				this.mirrorTransform(this.hho_Finger11_R0, this.bones.Finger11_L, false);
				this.mirrorTransform(this.hho_Finger12_R0, this.bones.Finger12_L, false);
				this.mirrorTransform(this.hho_Finger20_R0, this.bones.Finger20_L, false);
				this.mirrorTransform(this.hho_Finger21_R0, this.bones.Finger21_L, false);
				this.mirrorTransform(this.hho_Finger22_R0, this.bones.Finger22_L, false);
				this.mirrorTransform(this.hho_Finger30_R0, this.bones.Finger30_L, false);
				this.mirrorTransform(this.hho_Finger31_R0, this.bones.Finger31_L, false);
				this.mirrorTransform(this.hho_Finger32_R0, this.bones.Finger32_L, false);
				this.mirrorTransform(this.hho_Thumb0_R0, this.bones.Thumb0_L, false);
				this.mirrorTransform(this.hho_Thumb1_R0, this.bones.Thumb1_L, false);
				this.mirrorTransform(this.hho_Thumb2_R0, this.bones.Thumb2_L, false);
			}
			else if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null || this.clenchingHandL)
			{
				this.v3 = Vector3.zero;
				for (int i = 0; i < 4; i++)
				{
					if (this.grabbingPoleL)
					{
						switch (i)
						{
						case 0:
							this.handClenchHowMuchHand = 1f;
							break;
						case 1:
							this.handClenchHowMuchHand = 0.93f;
							break;
						case 2:
							this.handClenchHowMuchHand = 0.92f;
							break;
						case 3:
							this.handClenchHowMuchHand = 0.89f;
							break;
						}
						if (this.handClenchL[i] > 0.6f)
						{
							this.handClenchHowMuchHand += (1f - this.handClenchHowMuchHand) * (this.handClenchL[i] - 0.6f) / 0.06f;
						}
					}
					this.adjustedClench = this.handClenchL[i] * this.cap(1f - (float)i * (1f - this.handClenchHowMuchHand), 0f, 1f);
					if (this.handClenchRootL[i] != this.handClenchL[i])
					{
						this.v3.y = this.handClenchRootL[i] * -80f;
					}
					else
					{
						this.v3.y = this.adjustedClench * -80f;
					}
					this.v3.x = (float)(-i * 10) * this.adjustedClench;
					this.v3.z = (float)(i - 1) * (1f - this.adjustedClench) * -9f;
					this.v3.z = (this.fingerSpreadL - 0.5f) * 20f * (0.5f - (float)i);
					this.getFingerBone(i, 0, 0).localEulerAngles = this.v3;
					this.v3.y = this.adjustedClench * -80f;
					this.v3.z = 0f;
					this.getFingerBone(i, 1, 0).localEulerAngles = this.v3;
					this.getFingerBone(i, 2, 0).localEulerAngles = this.v3;
					if (i == 0)
					{
						if (this.grabbingPoleL)
						{
							this.ccr = this.cap(this.handClenchL[i], 0f, 0.8f);
							this.v3 = Vector3.zero;
							this.v3.x = 62f;
							this.v3.y = -60f + 60.6f * this.ccr;
							this.bones.Thumb2_L.localEulerAngles = this.v3;
							this.bones.Thumb1_L.localEulerAngles = this.v3;
							this.v3 = Vector3.zero;
							this.v3.x = 133f;
							this.v3.y = -49f + (this.ccr - 0.688f) * 186f;
							this.bones.Thumb0_L.localEulerAngles = this.v3;
						}
						else
						{
							this.v3.x = 62f;
							this.bones.Thumb1_L.localEulerAngles = this.v3;
							this.bones.Thumb2_L.localEulerAngles = this.v3;
						}
					}
				}
				if (!this.grabbingPoleL)
				{
					this.v3 = this.bones.Thumb0_L.localEulerAngles;
					this.v3.x = 40f + this.handClenchL[0] * 20f;
					this.v3.y = 10f;
					this.v3.z = 52f;
					this.bones.Thumb0_L.localEulerAngles = this.v3;
				}
			}
		}
		if (Game.allowHandPositioning)
		{
			if (this.rightHandItemHasFingerPoses)
			{
				this.mirrorTransform(this.hho_Finger00_R1, this.bones.Finger00_R, false);
				this.mirrorTransform(this.hho_Finger01_R1, this.bones.Finger01_R, false);
				this.mirrorTransform(this.hho_Finger02_R1, this.bones.Finger02_R, false);
				this.mirrorTransform(this.hho_Finger10_R1, this.bones.Finger10_R, false);
				this.mirrorTransform(this.hho_Finger11_R1, this.bones.Finger11_R, false);
				this.mirrorTransform(this.hho_Finger12_R1, this.bones.Finger12_R, false);
				this.mirrorTransform(this.hho_Finger20_R1, this.bones.Finger20_R, false);
				this.mirrorTransform(this.hho_Finger21_R1, this.bones.Finger21_R, false);
				this.mirrorTransform(this.hho_Finger22_R1, this.bones.Finger22_R, false);
				this.mirrorTransform(this.hho_Finger30_R1, this.bones.Finger30_R, false);
				this.mirrorTransform(this.hho_Finger31_R1, this.bones.Finger31_R, false);
				this.mirrorTransform(this.hho_Finger32_R1, this.bones.Finger32_R, false);
				this.mirrorTransform(this.hho_Thumb0_R1, this.bones.Thumb0_R, false);
				this.mirrorTransform(this.hho_Thumb1_R1, this.bones.Thumb1_R, false);
				this.mirrorTransform(this.hho_Thumb2_R1, this.bones.Thumb2_R, false);
			}
			else if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null || this.clenchingHandR)
			{
				this.v3 = Vector3.zero;
				for (int j = 0; j < 4; j++)
				{
					if (this.grabbingPoleR)
					{
						switch (j)
						{
						case 0:
							this.handClenchHowMuchHand = 1f;
							break;
						case 1:
							this.handClenchHowMuchHand = 0.93f;
							break;
						case 2:
							this.handClenchHowMuchHand = 0.92f;
							break;
						case 3:
							this.handClenchHowMuchHand = 0.89f;
							break;
						}
						if (this.handClenchR[j] > 0.6f)
						{
							this.handClenchHowMuchHand += (1f - this.handClenchHowMuchHand) * (this.handClenchR[j] - 0.6f) / 0.06f;
						}
					}
					this.adjustedClench = this.handClenchR[j] * this.cap(1f - (float)j * (1f - this.handClenchHowMuchHand), 0f, 1f);
					if (this.handClenchRootR[j] != this.handClenchR[j])
					{
						this.v3.y = this.handClenchRootR[j] * -80f;
					}
					else
					{
						this.v3.y = this.adjustedClench * -80f;
					}
					this.v3.x = (float)(-j * -10) * this.adjustedClench;
					this.v3.z = (float)(j - 1) * (1f - this.adjustedClench) * 9f;
					this.v3.z = (this.fingerSpreadR - 0.5f) * -20f * (0.5f - (float)j);
					this.getFingerBone(j, 0, 1).localEulerAngles = this.v3;
					this.v3.y = this.adjustedClench * -80f;
					this.v3.z = 0f;
					this.getFingerBone(j, 1, 1).localEulerAngles = this.v3;
					this.getFingerBone(j, 2, 1).localEulerAngles = this.v3;
					if (j == 0)
					{
						if (this.grabbingPoleR)
						{
							this.ccr = this.cap(this.handClenchR[j], 0f, 0.8f);
							this.v3 = Vector3.zero;
							this.v3.x = 298f;
							this.v3.y = -60f + 60.6f * this.ccr;
							this.bones.Thumb2_R.localEulerAngles = this.v3;
							this.bones.Thumb1_R.localEulerAngles = this.v3;
							this.v3 = Vector3.zero;
							this.v3.x = -133f;
							this.v3.y = -49f + (this.ccr - 0.688f) * 186f;
							this.bones.Thumb0_R.localEulerAngles = this.v3;
						}
						else
						{
							this.v3.x = 298f;
							this.bones.Thumb1_R.localEulerAngles = this.v3;
							this.bones.Thumb2_R.localEulerAngles = this.v3;
						}
					}
				}
				if (!this.grabbingPoleR)
				{
					this.v3 = this.bones.Thumb0_R.localEulerAngles;
					this.v3.x = 320f + this.handClenchR[0] * -20f;
					this.v3.y = 10f;
					this.v3.z = 307f;
					this.bones.Thumb0_R.localEulerAngles = this.v3;
				}
			}
		}
		this.grabbingPoleL = false;
		this.grabbingPoleR = false;
		this.clenchingHandL = false;
		this.clenchingHandR = false;
		this.fingerSpreadL = 0.5f;
		this.fingerSpreadR = 0.5f;
		if (this.idleHandLclenchT != 0f && !this.currentlyUsingHandL)
		{
			this.bones.Thumb0_L.Rotate(0f, this.idleHandLclenchT * 90f, 0f);
		}
		if (this.idleHandRclenchT != 0f && !this.currentlyUsingHandR)
		{
			this.bones.Thumb0_R.Rotate(0f, this.idleHandRclenchT * 90f, 0f);
		}
	}

	public void downloadNextCustomTexture()
	{
		this.busyDownloadingACustomTexture = true;
		if (File.Exists(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "racknet_cache" + Game.PathDirectorySeparatorChar + string.Empty + this.customTexturesWeNeedToDownload[0] + ".png"))
		{
			this.finishedDownloadingCustomTexture();
		}
		else
		{
			string uriString = "http://fekrack.net/rack2/characters/customtextures/" + this.customTexturesWeNeedToDownload[0] + ".png";
			WebClient webClient = new WebClient();
			webClient.DownloadFileCompleted += delegate
			{
				this.finishedDownloadingCustomTexture();
			};
			new FileInfo(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "racknet_cache" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
			webClient.DownloadFileAsync(new Uri(uriString), RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "racknet_cache" + Game.PathDirectorySeparatorChar + string.Empty + this.customTexturesWeNeedToDownload[0] + ".png");
		}
	}

	public void finishedDownloadingCustomTexture()
	{
		RackCharacter.customTexturesWeNeedToCheckForZeroLength.Add(RackCharacter.ApplicationpersistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "racknet_cache" + Game.PathDirectorySeparatorChar + string.Empty + this.customTexturesWeNeedToDownload[0] + ".png");
		this.customTexturesWeNeedToDownload.RemoveAt(0);
		this.busyDownloadingACustomTexture = false;
		if (this.customTexturesWeNeedToDownload.Count == 0)
		{
			this.queueTextureBuild = true;
		}
		else
		{
			this.downloadNextCustomTexture();
		}
	}

	public void processTexture()
	{
		if (!this.busyDownloadingACustomTexture && this.customTexturesWeNeedToDownload.Count > 0)
		{
			this.downloadNextCustomTexture();
		}
		if (this.threadedTextureDrawingState == 2)
		{
			this.headCanvas.SetPixels(this.canvasPixels[0]);
			this.bodyCanvas.SetPixels(this.canvasPixels[1]);
			this.wingCanvas.SetPixels(this.canvasPixels[2]);
			this.headFXCanvas.SetPixels(this.canvasPixels[3]);
			this.bodyFXCanvas.SetPixels(this.canvasPixels[4]);
			this.wingFXCanvas.SetPixels(this.canvasPixels[5]);
			this.headCanvas.Apply();
			this.bodyCanvas.Apply();
			this.wingCanvas.Apply();
			this.headFXCanvas.Apply();
			this.bodyFXCanvas.Apply();
			this.wingFXCanvas.Apply();
			this.applyTexture();
			if (this.threadedTextureDrawingState == 2)
			{
				this.threadedTextureDrawingState = 1;
			}
		}
		if (this.threadedTextureDrawingState == 3)
		{
			this.headCanvas.SetPixels(this.canvasPixels[0]);
			this.bodyCanvas.SetPixels(this.canvasPixels[1]);
			this.wingCanvas.SetPixels(this.canvasPixels[2]);
			this.drawEmbellishmentColorsOnTexture();
			this.headFXCanvas.SetPixels(this.canvasPixels[3]);
			this.bodyFXCanvas.SetPixels(this.canvasPixels[4]);
			this.wingFXCanvas.SetPixels(this.canvasPixels[5]);
			this.headCanvas.Apply();
			this.bodyCanvas.Apply();
			this.wingCanvas.Apply();
			this.headFXCanvas.Apply();
			this.bodyFXCanvas.Apply();
			this.wingFXCanvas.Apply();
			this.applyTexture();
			for (int i = 0; i < this.hairAppendages.Count; i++)
			{
				this.hairAppendages[i].setMaterials();
			}
			this.threadedTextureDrawingState = 0;
		}
		while (RackCharacter.customTexturesWeNeedToCheckForZeroLength.Count > 0)
		{
			FileInfo fileInfo = new FileInfo(RackCharacter.customTexturesWeNeedToCheckForZeroLength[0]);
			try
			{
				if (fileInfo.Length < 1)
				{
					fileInfo.Delete();
				}
			}
			catch
			{
			}
			RackCharacter.customTexturesWeNeedToCheckForZeroLength.RemoveAt(0);
		}
		if (this.queueTextureBuild && this.customTexturesWeNeedToDownload.Count <= 0 && this.threadedTextureDrawingState <= 0)
		{
			this.queueTextureBuild = false;
			this.buildTexture();
		}
		if (this.needFurLOD)
		{
			if (this.isPreviewCharacter)
			{
				this.needFurLOD = false;
			}
			else
			{
				for (int j = 0; j < this.parts.Count; j++)
				{
					if ((UnityEngine.Object)this.parts[j].GetComponent<ImperialFurLOD>() != (UnityEngine.Object)null)
					{
						UnityEngine.Object.Destroy(this.parts[j].GetComponent<ImperialFurLOD>());
						this.hasFurLOD = false;
					}
					else
					{
						if (j == 0)
						{
							this.furLODs = new List<ImperialFurLOD>();
						}
						this.furLODs.Add(this.parts[j].AddComponent<ImperialFurLOD>());
						this.hasFurLOD = true;
						this.parts[j].GetComponent<ImperialFurLOD>().from40To20 = 5f;
						this.parts[j].GetComponent<ImperialFurLOD>().from20To10 = 10f;
						this.parts[j].GetComponent<ImperialFurLOD>().from10To5 = 15f;
						this.parts[j].GetComponent<ImperialFurLOD>().from5To2 = 40f;
						this.parts[j].GetComponent<ImperialFurLOD>().from2To1 = 80f;
						this.needFurLOD = false;
					}
				}
			}
		}
	}

	public void drawEmbellishmentColorsOnTexture()
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = 6;
		for (int i = 0; i < 12; i++)
		{
			Vector2 a = default(Vector2);
			Vector2 vector = default(Vector2);
			this.getEmbellishmentColorCoords(out a, out vector, i, false);
			a *= 2048f;
			if (i < 4 || i > 7)
			{
				for (int j = 0; j < 32 + num3 * 2; j++)
				{
					for (int k = 0; k < num3 * 2; k++)
					{
						num = a.x + (float)j - (float)num3;
						num2 = 2048f - (a.y - (float)k + (float)num3);
						this.bodyCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), Game.determineEmbellishmentColorAtPoint(this, i, ((float)j - (float)num3) / 32f) / 256f);
						this.col.r = 0.01f;
						this.col.g = 0.01f;
						this.col.b = 0.01f;
						this.bodyFXCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), this.bodyFXCanvas.GetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality)) + this.col);
					}
				}
			}
			else
			{
				for (int l = 0; l < num3 * 2; l++)
				{
					for (int m = 0; m < 32 + num3 * 2; m++)
					{
						num = a.x + (float)l - (float)num3;
						num2 = 2048f - (a.y - (float)m + (float)num3);
						this.bodyCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), Game.determineEmbellishmentColorAtPoint(this, i, ((float)m - (float)num3) / 32f) / 256f);
						this.col.r = 0.01f;
						this.col.g = 0.01f;
						this.col.b = 0.01f;
						this.bodyFXCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), this.bodyFXCanvas.GetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality)) + this.col);
					}
				}
			}
			this.getEmbellishmentColorCoords(out a, out vector, i, true);
			a *= 1024f;
			if (i < 4 || i > 7)
			{
				for (int n = 0; n < 32 + num3 * 2; n++)
				{
					for (int num4 = 0; num4 < num3 * 2; num4++)
					{
						num = a.x + (float)n - (float)num3;
						num2 = 1024f - (a.y - (float)num4 + (float)num3);
						this.headCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), Game.determineEmbellishmentColorAtPoint(this, i, ((float)n - (float)num3) / 32f) / 256f);
						this.col.r = 0.01f;
						this.col.g = 0.01f;
						this.col.b = 0.01f;
						this.headFXCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), this.headFXCanvas.GetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality)) + this.col);
					}
				}
			}
			else
			{
				for (int num5 = 0; num5 < num3 * 2; num5++)
				{
					for (int num6 = 0; num6 < 32 + num3 * 2; num6++)
					{
						num = a.x + (float)num5 - (float)num3;
						num2 = 1024f - (a.y - (float)num6 + (float)num3);
						this.headCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), Game.determineEmbellishmentColorAtPoint(this, i, ((float)num6 - (float)num3) / (32f + (float)num3 * 2f)) / 256f);
						this.col.r = 0.01f;
						this.col.g = 0.01f;
						this.col.b = 0.01f;
						this.headFXCanvas.SetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality), this.headFXCanvas.GetPixel(Mathf.RoundToInt(num * UserSettings.data.characterTextureQuality), Mathf.RoundToInt(num2 * UserSettings.data.characterTextureQuality)) + this.col);
					}
				}
			}
		}
	}

	public void afterIK()
	{
		if (!this.faceControlledByAnimation && !this.currentlyUsingMouth)
		{
			this.processEyesAndFocus(false);
		}
		if (this.faceControlledByAnimation && this.allowLimitedEyeMovement)
		{
			goto IL_003e;
		}
		if (this.currentlyUsingMouth)
		{
			goto IL_003e;
		}
		goto IL_0044;
		IL_0044:
		Vector3 a = this.bones.SpineUpper.InverseTransformPoint(this.bones.LowerArm_L.position);
		Vector3 localScale = this.GO.transform.localScale;
		this.shoulderRoll_L = a / localScale.x;
		Vector3 a2 = this.bones.SpineUpper.InverseTransformPoint(this.bones.LowerArm_R.position);
		Vector3 localScale2 = this.GO.transform.localScale;
		this.shoulderRoll_R = a2 / localScale2.x;
		float num = this.hipRoll_L;
		Vector3 vector = this.bones.SpineLower.InverseTransformPoint(this.bones.LowerLeg_L.position);
		float y = vector.y;
		Vector3 localScale3 = this.GO.transform.localScale;
		this.hipRoll_L = num + (y / localScale3.x * 50f - this.hipRoll_L) * this.cap(Time.deltaTime, 0f, 1f);
		float num2 = this.hipRoll_R;
		Vector3 vector2 = this.bones.SpineLower.InverseTransformPoint(this.bones.LowerLeg_R.position);
		float num3 = 0f - vector2.y;
		Vector3 localScale4 = this.GO.transform.localScale;
		this.hipRoll_R = num2 + (num3 / localScale4.x * 50f - this.hipRoll_R) * this.cap(Time.deltaTime, 0f, 1f);
		this.tailholePositionAfterIK = this.tailholeEntrance.transform.position;
		this.headPositionAfterIK = this.bones.Head.position;
		this.forward_AIK = this.forward();
		this.up_AIK = this.up();
		this.right_AIK = this.right();
		this.rootAfterIK.transform.position = this.bones.Root.position;
		this.rootAfterIK.transform.rotation = this.bones.Root.rotation;
		this.rootAfterIK.transform.localScale = this.bones.Root.localScale;
		if (this.usingSexToy && this.currentSexToy != null)
		{
			this.currentSexToy.getToyTipAfterIK();
		}
		this.previousPenisRootPosition.transform.position = this.penisbones[0].position;
		this.aikPenisTip = this.penisTip(false);
		this.fixPlantigradeFeet();
		this.updateInteractionPoints();
		if (this.suckLock)
		{
			this.bones.Head.LookAt(this.suckLockCharacter.startPosition_root, this.upAfterIK() - this.forwardAfterIK());
			this.bones.Head.Rotate(0f, 190f, 255f);
		}
		this.tailholeEntranceAfterIK.transform.position = this.tailholeEntrance.transform.position;
		this.tailholeEntranceAfterIK.transform.rotation = this.tailholeEntrance.transform.rotation;
		this.vaginalEntranceAfterIK.transform.position = this.vaginaEntrance.transform.position;
		this.vaginalEntranceAfterIK.transform.rotation = this.vaginaEntrance.transform.rotation;
		this.throatHoleAfterIK.transform.position = this.throatHole.transform.position;
		this.throatHoleAfterIK.transform.rotation = this.throatHole.transform.rotation;
		Transform transform = this.mouthOpeningAfterIK.transform;
		Vector3 position = this.throatHole.transform.position;
		Vector3 forward = this.throatHoleAfterIK.transform.forward;
		float num4 = this.tongueScale;
		Vector3 lossyScale = this.bones.Head.lossyScale;
		transform.position = position - forward * (0.375f * (num4 * lossyScale.x));
		this.mouthOpeningAfterIK.transform.rotation = this.throatHole.transform.rotation;
		this.v3 = this.bones.Head.position - this.headTarget.position;
		this.v3 = this.rootAfterIK.transform.InverseTransformVector(this.v3);
		if (this.currentlyUsingMouth)
		{
			this.headOffsetFromTarget += this.v3 * this.cap(Time.deltaTime * 8f, 0f, 1f);
		}
		else
		{
			this.headOffsetFromTarget *= 1f - this.cap(Time.deltaTime * 6f, 0f, 1f);
		}
		return;
		IL_003e:
		this.processLimitedEyeMovement();
		goto IL_0044;
	}

	public void updateInteractionPoints()
	{
		if (this.isInteractionSubject)
		{
			this.interactionHotspots[0].position = this.bones.Toe1_L.position;
			this.interactionHotspots[1].position = this.bones.Toe1_R.position;
			this.interactionHotspots[3].position = this.bones.Ballsack1.position;
			for (int i = 0; i < this.interactionHotspots.Count; i++)
			{
				this.interactionHotspots[i].gameObject.SetActive(true);
			}
			this.interactionHotspots[3].gameObject.SetActive(this.data.genitalType == 0 && (this.data.ballsType == 0 || this.data.ballsType == 1));
			this.interactionHotspots[4].gameObject.SetActive(this.data.genitalType == 1);
			this.interactionHotspots[5].gameObject.SetActive(this.data.genitalType == 1 || this.data.genitalType == 3);
			this.interactionHotspots[6].gameObject.SetActive(this.data.genitalType == 0 || this.data.genitalType == 3);
		}
		else
		{
			for (int j = 0; j < this.interactionHotspots.Count; j++)
			{
				this.interactionHotspots[j].gameObject.SetActive(false);
			}
		}
	}

	public void fixPlantigradeFeet()
	{
		if (this.effectivelyPlantigrade)
		{
			this.v3 = RackCharacter.degreeDist3(this.originalFootRotations[0], this.bones.Foot_L.localEulerAngles);
			this.v32 = RackCharacter.degreeDist3(this.originalFootRotations[1], this.bones.Foot_R.localEulerAngles);
			this.bones.Foot_L.localEulerAngles = this.originalFootRotations[0];
			this.bones.Foot_R.localEulerAngles = this.originalFootRotations[1];
			Transform footpad_L = this.bones.Footpad_L;
			footpad_L.localEulerAngles += this.v3;
			Transform footpad_R = this.bones.Footpad_R;
			footpad_R.localEulerAngles += this.v32;
			Transform foot_L = this.bones.Foot_L;
			foot_L.position += Vector3.down * this.vectorSize(this.v3) * 0.001f * this.height_act;
			Transform foot_R = this.bones.Foot_R;
			foot_R.position += Vector3.down * this.vectorSize(this.v32) * 0.001f * this.height_act;
		}
	}

	public float vectorSize(Vector3 v)
	{
		return Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z);
	}

	public void postRender()
	{
		if (this.initted)
		{
			return;
		}
	}

	public void pointFingers()
	{
		if (this.fingerTarget0L.magnitude > 0f)
		{
			this.bones.Finger00_L.LookAt(this.fingerTarget0L, this.bones.Hand_L.up - this.bones.Hand_L.right);
			this.bones.Finger00_L.Rotate(0f, 90f, 0f);
			this.bones.Finger01_L.localRotation = Quaternion.identity;
			this.bones.Finger02_L.localRotation = Quaternion.identity;
			this.fingerTarget0L = Vector3.zero;
		}
		if (this.fingerTarget1L.magnitude > 0f)
		{
			this.bones.Finger10_L.LookAt(this.fingerTarget1L, this.bones.Hand_L.up - this.bones.Hand_L.right);
			this.bones.Finger10_L.Rotate(0f, 90f, 0f);
			this.bones.Finger11_L.localRotation = Quaternion.identity;
			this.bones.Finger12_L.localRotation = Quaternion.identity;
			this.fingerTarget1L = Vector3.zero;
		}
		if (this.fingerTarget2L.magnitude > 0f)
		{
			this.bones.Finger20_L.LookAt(this.fingerTarget2L, this.bones.Hand_L.up - this.bones.Hand_L.right);
			this.bones.Finger20_L.Rotate(0f, 90f, 0f);
			this.bones.Finger21_L.localRotation = Quaternion.identity;
			this.bones.Finger22_L.localRotation = Quaternion.identity;
			this.fingerTarget2L = Vector3.zero;
		}
		if (this.fingerTarget3L.magnitude > 0f)
		{
			this.bones.Finger30_L.LookAt(this.fingerTarget3L, this.bones.Hand_L.up - this.bones.Hand_L.right);
			this.bones.Finger30_L.Rotate(0f, 90f, 0f);
			this.bones.Finger31_L.localRotation = Quaternion.identity;
			this.bones.Finger32_L.localRotation = Quaternion.identity;
			this.fingerTarget3L = Vector3.zero;
		}
		if (this.fingerTarget0R.magnitude > 0f)
		{
			this.bones.Finger00_R.LookAt(this.fingerTarget0R, this.bones.Hand_R.up + this.bones.Hand_R.right);
			this.bones.Finger00_R.Rotate(0f, 90f, 0f);
			this.bones.Finger01_R.localRotation = Quaternion.identity;
			this.bones.Finger02_R.localRotation = Quaternion.identity;
			this.fingerTarget0R = Vector3.zero;
		}
		if (this.fingerTarget1R.magnitude > 0f)
		{
			this.bones.Finger10_R.LookAt(this.fingerTarget1R, this.bones.Hand_R.up + this.bones.Hand_R.right);
			this.bones.Finger10_R.Rotate(0f, 90f, 0f);
			this.bones.Finger11_R.localRotation = Quaternion.identity;
			this.bones.Finger12_R.localRotation = Quaternion.identity;
			this.fingerTarget1R = Vector3.zero;
		}
		if (this.fingerTarget2R.magnitude > 0f)
		{
			this.bones.Finger20_R.LookAt(this.fingerTarget2R, this.bones.Hand_R.up + this.bones.Hand_R.right);
			this.bones.Finger20_R.Rotate(0f, 90f, 0f);
			this.bones.Finger21_R.localRotation = Quaternion.identity;
			this.bones.Finger22_R.localRotation = Quaternion.identity;
			this.fingerTarget2R = Vector3.zero;
		}
		if (this.fingerTarget3R.magnitude > 0f)
		{
			this.bones.Finger30_R.LookAt(this.fingerTarget3R, this.bones.Hand_R.up + this.bones.Hand_R.right);
			this.bones.Finger30_R.Rotate(0f, 90f, 0f);
			this.bones.Finger31_R.localRotation = Quaternion.identity;
			this.bones.Finger32_R.localRotation = Quaternion.identity;
			this.fingerTarget3R = Vector3.zero;
		}
	}

	public void humpBump(float h)
	{
		this.humpBumpAmount += this.cap(h, 0f, 1f);
	}

	public void processWrithing()
	{
		this.higherWritheFactor = 0f;
		if (this.boundPose != string.Empty)
		{
			if (RackCharacter.testingWrithe)
			{
				if (Input.GetKey(KeyCode.UpArrow))
				{
					this.writheForward += (1f - this.writheForward) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				else if (Input.GetKey(KeyCode.DownArrow))
				{
					this.writheForward += (-1f - this.writheForward) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				else
				{
					this.writheForward += (0f - this.writheForward) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				if (Input.GetKey(KeyCode.RightArrow))
				{
					this.writheRight += (1f - this.writheRight) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				else if (Input.GetKey(KeyCode.LeftArrow))
				{
					this.writheRight += (-1f - this.writheRight) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				else
				{
					this.writheRight += (0f - this.writheRight) * this.cap(Time.deltaTime * 8f, 0f, 1f);
				}
				this.writheF += (this.writheForward - this.writheF) * this.cap(Time.deltaTime * 4f, 0f, 1f);
				this.writheR += (this.writheRight - this.writheR) * this.cap(Time.deltaTime * 4f, 0f, 1f);
			}
			else
			{
				this.higherWritheFactor = this.overstimulation;
				if (this.inPain && this.discomfort > this.higherWritheFactor)
				{
					this.higherWritheFactor = this.discomfort;
				}
				if (this.getCommandStatus("stop_squirming") > 0)
				{
					if (this.higherWritheFactor > 0.1f)
					{
						this.writheResistance -= this.writheResistance * this.cap(Time.deltaTime * 0.1f * (1f + this.higherWritheFactor * 0.15f), 0f, 1f);
					}
					else if (this.writheResistance < 5f)
					{
						this.writheResistance += Time.deltaTime * 0.2f;
					}
					this.higherWritheFactor /= 1f + this.writheResistance;
					if (this.higherWritheFactor > 0.3f)
					{
						this.setCommandStatus("stop_squirming", -1);
					}
				}
				else
				{
					this.writheResistance -= this.writheResistance * this.cap(Time.deltaTime * 15f, 0f, 1f);
					this.higherWritheFactor /= 1f + this.writheResistance;
				}
				this.naturalWrithe += this.arousal * this.pleasure * 0.03f + this.orgasmSoftPulse * 0.1f + this.cap(this.higherWritheFactor * 0.5f, 0f, 0.05f);
				this.naturalWritheIntensity = this.arousal * this.pleasure * 0.15f + this.cap(this.higherWritheFactor * 0.7f, 0f, 0.15f);
				if (this.orgasming / this.currentOrgasmDuration > 0.5f)
				{
					this.naturalWritheIntensity += this.cap(this.cumIntensity + this.orgasmSoftPulse * 3f, 0f, 1f) * 0.1f;
				}
				if (this.refractory > 0f && this.orgasming <= 0f)
				{
					this.naturalWritheIntensity *= 1f - this.cap(this.refractory, 0f, 10f) / 10f;
				}
				this.writheForward = Mathf.Cos(this.naturalWrithe) * this.naturalWritheIntensity;
				this.writheRight = Mathf.Cos(this.naturalWrithe * 1.05f) * this.naturalWritheIntensity;
				this.overstimWritheTargetCooldown -= 0.3f + this.higherWritheFactor;
				if (this.overstimWritheTargetCooldown <= 0f)
				{
					float num = -1f + UnityEngine.Random.value * 2f;
					float num2 = -1f + UnityEngine.Random.value * 2f;
					float num3 = Mathf.Abs(this.overstimWritheTargetF - num) + Mathf.Abs(this.overstimWritheTargetR - num2);
					if (this.higherWritheFactor > 0.6f)
					{
						int num4 = 0;
						while (num3 < this.higherWritheFactor * 2f && num4 < 10)
						{
							num = -1f + UnityEngine.Random.value * 2f;
							num2 = -1f + UnityEngine.Random.value * 2f;
							num3 = Mathf.Abs(this.overstimWritheTargetF - num) + Mathf.Abs(this.overstimWritheTargetR - num2);
							num4++;
						}
					}
					if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
					{
						this.apparatus.reactToSubjectStruggle(num3 * this.higherWritheFactor);
					}
					this.overstimWritheTargetF = num;
					this.overstimWritheTargetR = num2;
					this.overstimWritheTargetCooldown = 12f + UnityEngine.Random.value * 8f;
				}
				this.writheForward += (this.overstimWritheTargetF - this.writheForward) * this.cap(this.higherWritheFactor, 0f, 0.8f);
				this.writheRight += (this.overstimWritheTargetR - this.writheRight) * this.cap(this.higherWritheFactor, 0f, 0.8f);
				if ((this.refractory <= 0f || this.orgasming > 0f) && 0.3f > this.writheForward)
				{
					this.writheForward += (0.3f - this.writheForward) * Mathf.Pow(this.proximityToOrgasm, 9f) * (1f - this.cumIntensity);
				}
				if (0.6f > this.writheForward)
				{
					this.writheForward += (0.6f - this.writheForward) * Mathf.Pow(this.cap(this.cumIntensity + this.orgasmSoftPulse * 3f, 0f, 1f) * (this.orgasming / this.currentOrgasmDuration), 4f);
				}
				this.writheF += (this.writheForward - this.writheF) * this.cap(Time.deltaTime * 4f, 0f, 1f);
				this.writheR += (this.writheRight - this.writheR) * this.cap(Time.deltaTime * 4f, 0f, 1f);
			}
		}
	}

	public void squat(float amount)
	{
		this.squatAmount = amount;
		this.squatting = true;
	}

	public void applyHeadAdjustment()
	{
		this.squat(this.cap(0f - this.headOffsetFromTarget.x, 0f, 10f) * 1f);
		Transform transform = this.bodyTarget;
		transform.position += (-this.up() - this.forward() * 0.6f) * this.squatAmount;
		if (!this.squatting)
		{
			this.squatAmount *= 1f - this.cap(Time.deltaTime * 4f, 0f, 1f);
		}
	}

	public void processIK()
	{
		if (!this.IKoriginalSettingsSet && this.initted)
		{
			this.original_pullBodyHorizontal = this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyHorizontal;
			this.original_pullBodyVertical = this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyVertical;
			this.original_spineStiffness = this.GO.GetComponent<FullBodyBipedIK>().solver.spineStiffness;
			this.original_rightArmChainpull = this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.pull;
			this.original_leftArmChainpull = this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.pull;
			this.original_rightArmChainreach = this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.reach;
			this.original_leftArmChainreach = this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.reach;
			this.IKoriginalSettingsSet = true;
		}
		if (this.IKoriginalSettingsSet)
		{
			if (this.interactingWithSelf)
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyHorizontal = 0.2f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyVertical = 0.5f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.spineStiffness = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.pull = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.pull = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.reach = 0.25f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.reach = 0.25f;
				if (this.showPenis)
				{
					this.bendBack(-0.4f);
				}
				else
				{
					this.bendBack(-0.32f);
				}
			}
			else
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyHorizontal = this.original_pullBodyHorizontal;
				this.GO.GetComponent<FullBodyBipedIK>().solver.pullBodyVertical = this.original_pullBodyVertical;
				this.GO.GetComponent<FullBodyBipedIK>().solver.spineStiffness = this.original_spineStiffness;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.pull = this.original_rightArmChainpull;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.pull = this.original_leftArmChainpull;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightArmChain.reach = this.original_rightArmChainreach;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftArmChain.reach = this.original_leftArmChainreach;
			}
		}
		this.applyShoulderFix();
		this.applyHipFix();
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			this.apparatus.animateWrithing(this);
		}
		this.hipShiftDelay -= Time.deltaTime;
		if (this.hipShiftDelay <= 0f)
		{
			this.hipShiftDelay = 0.2f + UnityEngine.Random.value * 6f;
			this.hipShiftTime = 0.45f + UnityEngine.Random.value * 0.4f;
		}
		this.hipStiffness = 0f;
		if (this.hipShiftTime > 0f)
		{
			this.hipShiftTime -= Time.deltaTime;
			this.hipStiffTime += Time.deltaTime * Mathf.Pow(0.5f + Mathf.Cos(Time.deltaTime * 0.3f) * 0.5f, 6f) * 2f;
		}
		else
		{
			this.hipStiffTime += Time.deltaTime * Mathf.Pow(0.5f + Mathf.Cos(Time.deltaTime * 0.3f) * 0.5f, 6f) * 0.5f;
		}
		if (this.interactionSubject != null)
		{
			this.hipStiffness = 0.4f + Mathf.Cos(this.hipStiffTime + 99f) * 0.1f;
		}
		else if (this.suspendedAnim)
		{
			this.hipStiffness = 0.4f + Mathf.Cos(this.hipStiffTime + 37f) * 0.1f;
		}
		else if ((UnityEngine.Object)this.furniture == (UnityEngine.Object)null && (UnityEngine.Object)this.apparatus == (UnityEngine.Object)null && this.moveSpeed == 0f)
		{
			this.hipStiffness = 0.6f + Mathf.Cos(this.hipStiffTime * 0.3f) * 0.1f;
			if (!this.idleAnimation && !this.interactingWithSelf)
			{
				this.hipStiffness = 0f;
			}
		}
		if (!this.usingDefaultBodyTarget && !this.suspendedAnim)
		{
			this.hipStiffness = 1f;
		}
		if (this.currentlyUsingMouth)
		{
			this.hipStiffness = 0.65f;
		}
		this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.positionWeight = this.hipStiffness;
		this.GO.GetComponent<FullBodyBipedIK>().solver.bodyEffector.rotationWeight = this.hipStiffness;
		if (this.currentlyUsingMouth)
		{
			this.hipStiffness = 0f;
		}
		this.v3 = this.originalBodyTargetPosition;
		this.v3.x += Mathf.Cos((this.hipStiffTime + 99f) * 1.03f) * 0.03f;
		this.v3.x += Mathf.Cos((this.hipStiffTime + 37f) * 0.91f) * 0.03f;
		this.v3.x += Mathf.Cos(this.hipStiffTime * 0.83f) * 0.03f;
		this.v3.y += -0.04f + Mathf.Cos((this.hipStiffTime + 11f) * 0.87f) * 0.01f;
		this.v32 = Vector3.zero;
		if (this.interactionSubject != null)
		{
			this.v32 += this.up() * -0.2f * (this.interactionMY - 0.5f);
			this.v32 += this.forward() * (this.interactionMY - 0.5f);
			this.v32 += this.right() * (this.interactionMX - 0.5f);
			this.v32 *= 0.25f;
		}
		this.hipOffsetFromInteraction += (this.v32 - this.hipOffsetFromInteraction) * this.cap(Time.deltaTime * 2f, 0f, 1f);
		this.v3 += this.hipOffsetFromInteraction;
		if (this.interactingWithSelf)
		{
			if (this.showPenis)
			{
				this.v3.y -= 0.13f + (this.mX - 0.5f) * 0.02f;
			}
			else
			{
				this.v3.y -= 0.09f + (this.mX - 0.5f) * 0.02f;
			}
		}
		this.bodyTarget.localPosition = this.v3;
		this.applyHeadAdjustment();
		if (!this.currentlyUsingHandL && !this.customIdleHandLtarget && !this.currentlyUsingMouth)
		{
			Transform gOHandTargetL = this.GOHandTargetL;
			gOHandTargetL.position += this.right() * this.v3.x * this.hipStiffness;
		}
		if (!this.currentlyUsingHandR && !this.customIdleHandRtarget && !this.currentlyUsingMouth)
		{
			Transform gOHandTargetR = this.GOHandTargetR;
			gOHandTargetR.position += this.right() * this.v3.x * this.hipStiffness;
		}
		this.processManualAimHandTargets(false);
		this.determineIKlevelAndSolve();
		this.processManualAimHandTargets(true);
	}

	public void manuallyPositionHand(bool rightHand, Vector3 position, Quaternion rotation)
	{
		if (rightHand)
		{
			this.rightHandManuallyAimed = true;
			this.aimingMainHand = (this.aimingMainHand || this.data.rightHanded);
			this.manualAimRightHandTarget.position = position;
			this.manualAimRightHandTarget.rotation = rotation;
		}
		else
		{
			this.leftHandManuallyAimed = true;
			this.aimingMainHand = (this.aimingMainHand || !this.data.rightHanded);
			this.manualAimLeftHandTarget.position = position;
			this.manualAimLeftHandTarget.rotation = rotation;
		}
	}

	public void manuallyAimHand(bool rightHand, Vector3 position, Vector3 target, Vector3 up)
	{
		if (rightHand)
		{
			this.rightHandManuallyAimed = true;
			this.aimingMainHand = (this.aimingMainHand || this.data.rightHanded);
			this.manualAimRightHandTarget.position = position;
			this.manualAimRightHandTarget.LookAt(target, up);
		}
		else
		{
			this.leftHandManuallyAimed = true;
			this.aimingMainHand = (this.aimingMainHand || !this.data.rightHanded);
			this.manualAimLeftHandTarget.position = position;
			this.manualAimLeftHandTarget.LookAt(target, up);
		}
	}

	public void processManualAimHandTargets(bool reset = false)
	{
		if (!reset)
		{
			if (this.leftHandManuallyAimed)
			{
				this.originalLHET = this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target;
				this.originalLHP = this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight;
				this.originalLHR = this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.manualAimLeftHandTarget;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = 1f;
			}
			if (this.rightHandManuallyAimed)
			{
				this.originalRHET = this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target;
				this.originalRHP = this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight;
				this.originalRHR = this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.manualAimRightHandTarget;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = 1f;
			}
		}
		else
		{
			if (this.leftHandManuallyAimed)
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.target = this.originalLHET;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = this.originalLHP;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftHandEffector.rotationWeight = this.originalLHR;
			}
			if (this.rightHandManuallyAimed)
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.target = this.originalRHET;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.positionWeight = this.originalRHP;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightHandEffector.rotationWeight = this.originalRHR;
			}
			this.leftHandManuallyAimed = false;
			this.rightHandManuallyAimed = false;
			this.aimingMainHand = false;
		}
	}

	public void applyHipFix()
	{
		if (this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight > 0.9f)
		{
			this.bones.Hip_L.Rotate(0f, this.hipRoll_L * 0.28f, this.hipRoll_L * -0.12f);
			this.bones.Hip_R.Rotate(0f, this.hipRoll_R * 0.28f, this.hipRoll_R * 0.12f);
		}
	}

	public void applyShoulderFix()
	{
		this.bones.Shoulder_L.Rotate(this.shoulderRollFromInteractionsL.x, this.shoulderRoll_L.x * -50f * 0.15f + this.shoulderRollFromInteractionsL.y, this.shoulderRoll_L.z * -50f * 0.15f + this.shoulderRollFromInteractionsL.z);
		this.bones.Shoulder_R.Rotate(this.shoulderRollFromInteractionsR.x, this.shoulderRoll_R.x * -50f * 0.15f + this.shoulderRollFromInteractionsR.y, this.shoulderRoll_R.z * 50f * 0.15f + this.shoulderRollFromInteractionsR.z);
		this.v3 = Vector3.zero;
		if (this.currentlyUsingHandL)
		{
			this.v3.y -= (this.interactionMY - 0.5f) * 20f;
			this.v3.z -= (this.interactionMY - 0.5f) * 50f;
		}
		this.shoulderRollFromInteractionsL += (this.v3 - this.shoulderRollFromInteractionsL) * this.cap(Time.deltaTime * 3f, 0f, 1f);
		this.v3 = Vector3.zero;
		if (this.currentlyUsingHandR)
		{
			this.v3.y -= (this.interactionMY - 0.5f) * 20f;
			this.v3.z += (this.interactionMY - 0.5f) * 50f;
		}
		this.shoulderRollFromInteractionsR += (this.v3 - this.shoulderRollFromInteractionsR) * this.cap(Time.deltaTime * 3f, 0f, 1f);
	}

	public void processWings()
	{
		if (this.showWings)
		{
			float num = (this.moveSpeed - 0.2f) * 0.3f;
			Vector3 localEulerAngles = this.bones.Shoulder_L.localEulerAngles;
			this.wingOutL = num + this.cap(Game.degreeDist(localEulerAngles.z, 300f) / (180f - 12f * this.moveSpeed), 0f, 1f) + this.breath[0] * 0.1f;
			float num2 = (this.moveSpeed - 0.2f) * 0.3f;
			Vector3 localEulerAngles2 = this.bones.Shoulder_R.localEulerAngles;
			this.wingOutR = num2 + this.cap(Game.degreeDist(60f, localEulerAngles2.z) / (180f - 12f * this.moveSpeed), 0f, 1f) + this.breath[0] * 0.1f;
			if (this.suspendedAnim)
			{
				this.wingOutL = -0.5f;
				this.wingOutR = -0.5f;
			}
			for (int i = 0; i < 10; i++)
			{
				float t = this.wingOutL;
				if (i >= 5)
				{
					t = this.wingOutR;
				}
				Vector3 localEulerAngles3 = this.wingbones[i].localEulerAngles;
				this.v3 = Vector3.zero;
				this.wingbones[i].localRotation = Quaternion.Lerp(this.wingInRots[i], this.wingOutRots[i], t);
				((Component)this.wingbones[i]).GetComponent<CharacterJoint>().autoConfigureConnectedAnchor = false;
				((Component)this.wingbones[i]).GetComponent<CharacterJoint>().connectedAnchor = ((Component)this.wingbones[i]).GetComponent<CharacterJoint>().connectedAnchor;
				this.wingbones[i].localEulerAngles = localEulerAngles3;
				if (i == 0 || i == 5)
				{
					this.wingbones[i].localScale = Vector3.one * (0.5f + this.data.wingSize * 0.5f);
				}
			}
		}
	}

	public void processPubic()
	{
		this.v3 = Vector3.one;
		if (this.showPenis)
		{
			this.v3 *= this.penisSize_act;
		}
		else
		{
			this.v3 *= 1f - this.orgasmSoftPulse * 0.3f;
		}
		this.v3.y += this.extraVaginaSpread;
		this.v3.z += this.extraVaginaSpread * 0.85f;
		this.bones.Pubic.localScale = this.v3;
		this.bones.Pubic.localRotation = this.originalPubicRotation;
		Vector3 vector = this.bones.Root.InverseTransformPoint(this.bones.LowerLeg_R.position);
		float z = vector.z;
		Vector3 vector2 = this.bones.Root.InverseTransformPoint(this.bones.LowerLeg_L.position);
		this.pubicSwaySideToSide = z - vector2.z;
		this.pubicAngle.y += this.penisDrag * 20f + this.penisAngledAmount.x * 2f + this.clitNudge.y * 0.2f - this.pubicPushAmount * 20f + this.cap(this.ballRetract * 90f, 0f, 50f) - this.pubicAngle.y;
		this.pubicAngle.z += this.penisAngledAmount.y * 2f + this.clitNudge.x * 0.2f - this.cap(this.vaginaPenetrationSideAmount * 90f, -30f, 30f) + this.pubicSwaySideToSide * 20f - this.pubicAngle.z;
		this.bones.Pubic.Rotate(this.pubicAngle * 1f);
		this.penisAngledAmount = Vector3.zero;
		if (!this.pubicBeingPushed)
		{
			this.pubicPushAmount *= 1f - this.cap(Time.deltaTime * 3f, 0f, 1f);
		}
		this.pubicBeingPushed = false;
	}

	public void pushPubic(float amount)
	{
		this.pubicPushAmount += (amount - this.pubicPushAmount) * this.cap(Time.deltaTime * 8f, 0f, 1f);
		this.pubicBeingPushed = true;
	}

	public void processBelly()
	{
		Vector3 position = this.bones.Belly.position;
		if (float.IsNaN(position.x))
		{
			this.resetBelly();
		}
		if ((this.bones.Belly.position - this.bones.SpineLower.position).magnitude > 2f)
		{
			this.resetBelly();
		}
		if (this.bodyMass_act != this.lastBodyMass_act || this.adiposity_act != this.lastAdiposity_act || this.belly_act != this.lastBelly_act)
		{
			this.UpperSpineCollider.height = 0.97f + 0.1f * this.bodyMass_act;
			this.MiddleSpineCollider.height = 0.79f + 0.25f * this.bodyMass_act + 0.34f * this.adiposity_act;
			this.MiddleSpineCollider.radius = 0.32f + 0.1f * this.bodyMass_act;
			this.v3.x = -0.24f;
			this.v3.y = 0f;
			this.v3.z = -0.12f + 0.03f * this.adiposity_act;
			this.MiddleSpineCollider.center = this.v3;
			this.LowerSpineCollider.height = 0.74f + 0.22f * this.bodyMass_act + 0.49f * this.adiposity_act;
			this.LowerSpineCollider.radius = 0.28f + 0.09f * this.bodyMass_act;
			this.v3.x = -0.28f - 0.06f * this.adiposity_act;
			this.v3.y = 0f;
			this.v3.z = -0.11f + 0.03f * this.adiposity_act;
			if (this.bodyMass_act < 0.5f)
			{
				this.v3.z += 0.04f * (0.5f - this.bodyMass_act) / 0.5f;
			}
			this.LowerSpineCollider.center = this.v3;
			this.v3.x = -0.1f;
			this.v3.x -= (this.bodyMass_act - 0.5f) * 0.1f;
			this.v3.x -= this.belly_act * 0.3f;
			this.v3.x -= this.adiposity_act * 0.1f;
			this.v3.y = 0f;
			this.v3.z = 0.06f;
			this.BellyCollider.center = this.v3;
			float num = 0f;
			if (this.bodyMass_act / 0.5f > num)
			{
				num = this.bodyMass_act / 0.5f;
			}
			if (this.belly_act / 0.25f > num)
			{
				num = this.belly_act / 0.25f;
			}
			num = this.cap(num, 0f, 1f);
			this.BellyCollider.radius = 0.27f + 0.06f * num;
			this.bellyStiffness = 1f - Mathf.Pow(this.belly_act, 2f);
			if (this.belly_act < 0.4f)
			{
				float num2 = (0.4f - this.belly_act) / 0.4f;
				this.bellyStiffness += (1f - this.bellyStiffness) * num2;
			}
			this.bellyLinearLimit.limit = this.cap(this.belly_act * 0.15f, 0f, 0.15f);
			this.bellyJoints[0].linearLimit = this.bellyLinearLimit;
			this.updateBoneWeights(false);
			this.lastBodyMass_act = this.bodyMass_act;
			this.lastAdiposity_act = this.adiposity_act;
			this.lastBelly_act = this.belly_act;
		}
		this.v3 = this.lastBellyPositions[0] - this.bellyRigidbodies[0].position;
		if (this.v3.magnitude < 2f)
		{
			this.bellyRigidbodies[1].AddForce(this.v3 * 360f, ForceMode.Impulse);
		}
		this.lastBellyPositions[0] = this.bellyRigidbodies[0].position;
	}

	public void processBoobs()
	{
		Vector3 position = this.boobbones[0].position;
		if (float.IsNaN(position.x))
		{
			this.resetBoobs();
		}
		for (int i = 0; i < this.boobRigidbodies.Length; i++)
		{
			if (this.boobRigidbodies[i].velocity.magnitude > 100f)
			{
				this.resetBoobs();
			}
		}
		for (int j = 0; j < 2; j++)
		{
			if ((this.boobbones[j].position - this.bones.SpineUpper.position).magnitude > 2f)
			{
				this.resetBoobs();
			}
		}
		float num = 0.21f + 0.03f * this.bodyMass_act + 0.01f * this.adiposity_act + 0.02f * this.muscle_act;
		this.v3.x = 0.02f - 0.04f * this.bodyMass_act - 0.06f * this.adiposity_act - 0.06f * this.muscle_act;
		this.v3.y = 0f - 0.12f * this.adiposity_act;
		this.v3.z = -0.09f - 0.06f * this.bodyMass_act - 0.01f * this.muscle_act;
		float num2 = this.cap(this.breastSize_act, 0f, 1f);
		num += num2 * -0.03f;
		this.v3.x += num2 * -0.19f;
		this.v3.y += num2 * -0.04f;
		this.v3.z += num2 * 0.08f;
		num2 = this.cap(this.breastSize_act, 1f, 2f) - 1f;
		num += num2 * 0.03f;
		this.v3.x += num2 * -0.15f;
		this.v3.y += num2 * 0.03f;
		this.v3.z += num2 * 0.02f;
		num2 = this.cap(this.breastSize_act, 2f, 3f) - 2f;
		num += num2 * 0.08f;
		this.v3.x += num2 * -0.18f;
		this.boobColliders[1].radius = num;
		this.boobColliders[1].center = this.v3;
		this.v3.y *= -1f;
		this.boobColliders[0].radius = num;
		this.boobColliders[0].center = this.v3;
		for (int k = 0; k < 2; k++)
		{
			this.boobJoints[k].connectedAnchor = RackCharacter.originalBoobAnchors[k];
		}
		if (this.breastSize_act < RackCharacter.breastThreshhold)
		{
			for (int l = 0; l < 2; l++)
			{
				if (this.boobsWereBeingPhysicked == 1)
				{
					this.boobbones[l].localEulerAngles = RackCharacter.boobOriginalRotations[l];
					this.boobbones[l].localPosition = RackCharacter.boobOriginalPositions[l];
					this.boobbones[l].localScale = this.boobOriginalScales[l];
					this.boobLimit.limit = 0f;
					this.boobJoints[l].angularYLimit = this.boobLimit;
					this.boobJoints[l].angularZLimit = this.boobLimit;
					this.boobRigidbodies[l].velocity = Vector3.zero;
					this.boobRigidbodies[l].angularVelocity = Vector3.zero;
					this.boobRigidbodies[l + 2].velocity = Vector3.zero;
					this.boobRigidbodies[l + 2].angularVelocity = Vector3.zero;
					if (l == 1)
					{
						this.updateBoneWeights(true);
					}
				}
			}
			this.boobsWereBeingPhysicked = 0;
		}
		else
		{
			for (int m = 0; m < 2; m++)
			{
				if (this.boobsWereBeingPhysicked == 0)
				{
					this.lastBreastPerk = -1f;
				}
				this.breastPerkiness = 0.2f - this.data.breastSize * 0.16f;
				this.breastPerkiness += (1f - this.breastPerkiness) * this.breastPerk_act;
				if (this.breastSize_act < RackCharacter.breastThreshhold + 0.75f)
				{
					float num3 = 1f - (this.breastSize_act - RackCharacter.breastThreshhold) / 0.75f;
					this.breastPerkiness += (1f - this.breastPerkiness) * num3;
				}
				if (this.breastSupportFromClothing > this.breastPerkiness)
				{
					this.breastPerkiness = this.breastSupportFromClothing;
				}
				if (this.breastPerkiness != this.lastBreastPerk)
				{
					this.v3 = this.boobbones[m].localEulerAngles;
					this.boobbones[m].localEulerAngles = RackCharacter.boobOriginalRotations[m];
					this.boobForceIn = 18f * this.breastPerkiness;
					this.boobForceUp = 5f * this.breastPerkiness;
					if (m == 0)
					{
						this.boobbones[m].Rotate(0f, this.boobForceUp, this.boobForceIn);
					}
					else
					{
						this.boobbones[m].Rotate(0f, this.boobForceUp, 0f - this.boobForceIn);
					}
					this.boobJoints[m].autoConfigureConnectedAnchor = false;
					this.boobJoints[m].connectedAnchor = this.boobJoints[m].connectedAnchor;
					this.boobbones[m].localEulerAngles = this.v3;
					this.boobLimit.limit = 25f - 22f * this.breastPerkiness;
					this.boobJoints[m].angularYLimit = this.boobLimit;
					this.boobLimit.limit = 25f - 22f * this.breastPerkiness;
					this.boobJoints[m].angularZLimit = this.boobLimit;
					this.boobLimitSpring.spring = 100f + 19900f * this.breastPerkiness;
					this.boobLimitSpring.damper = 10f + 1990f * this.breastPerkiness;
					this.boobJoints[m].angularYZLimitSpring = this.boobLimitSpring;
					if (m == 1)
					{
						this.updateBoneWeights(false);
					}
				}
			}
			this.boobsWereBeingPhysicked = 1;
			this.lastBreastPerk = this.breastPerkiness;
		}
		for (int n = 0; n < 2; n++)
		{
			if (this.boobPushedInFromArm[n] <= 0f)
			{
				this.boobPushingInSpeed[n] = 0f;
				if (this.boobPushInFromArm[n] > 0f)
				{
					this.boobReturnToPositionSpeed[n] += Time.deltaTime * 15f;
					this.boobPushInFromArm[n] -= Time.deltaTime * this.boobReturnToPositionSpeed[n];
					if (this.boobPushInFromArm[n] < 0f)
					{
						this.boobPushInFromArm[n] = 0f;
						this.boobReturnToPositionSpeed[n] = 0f;
					}
				}
				else if (this.boobPushInFromArm[n] < 0f)
				{
					this.boobReturnToPositionSpeed[n] += Time.deltaTime * 15f;
					this.boobPushInFromArm[n] += Time.deltaTime * this.boobReturnToPositionSpeed[n];
					if (this.boobPushInFromArm[n] > 0f)
					{
						this.boobPushInFromArm[n] = 0f;
						this.boobReturnToPositionSpeed[n] = 0f;
					}
				}
			}
			else
			{
				this.boobReturnToPositionSpeed[n] = 0f;
				this.boobPushedInFromArm[n] -= Time.deltaTime;
			}
			this.v3 = Vector3.one;
			Vector3 localEulerAngles = this.boobbones[n].localEulerAngles;
			float num4 = 0f - Game.degreeDist(localEulerAngles.y, 260f);
			num4 = ((!(num4 > 0f)) ? (num4 * 0.5f) : (num4 * 3f));
			float to = (n != 0) ? -13f : 13f;
			num4 *= 0.005f;
			this.boobUpAmount[n] += (num4 - this.boobUpAmount[n]) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			Vector3 localEulerAngles2 = this.boobbones[n].localEulerAngles;
			float num5 = Game.degreeDist(localEulerAngles2.z, to);
			if (n == 1)
			{
				num5 *= -1f;
			}
			this.boobSideStretchAmount[n] += (num5 - this.boobSideStretchAmount[n]) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			this.v3.x = 1f - this.boobUpAmount[n] - Mathf.Abs(this.boobSideStretchAmount[n] - 30f) * 0.011f - this.breastSupportFromClothing * 0.25f;
			this.v3.y = 1f + this.boobUpAmount[n] * 0.2f - this.boobSideStretchAmount[n] * 0.006f;
			this.v3.z = 1f - this.boobUpAmount[n] * 0.2f + this.boobSideStretchAmount[n] * 0.006f;
			this.v3.x = this.cap(this.v3.x, 0.85f, 1.5f);
			this.v3.y = this.cap(this.v3.y, 0.5f, 1.25f);
			this.v3.z = this.cap(this.v3.z, 0.5f, 1.25f);
			this.boobbones[n].localScale = this.v3;
			this.v3 = RackCharacter.boobOriginalPositions[n];
			this.boobTranslateToCenter[n] = this.cap((20f - this.boobSideStretchAmount[n]) * 0.04f, 0f, 1f) * 0.1f;
			this.boobTranslateToCenter[n] += 0.04f * this.breastPerkiness;
			if (n == 0)
			{
				this.boobTranslateToCenter[n] *= -1f;
			}
			this.boobTranslateToCenter[n] *= 0.4f;
			this.v3.y += this.boobTranslateToCenter[n];
			this.v3.z -= Mathf.Abs(this.boobTranslateToCenter[n]) * 1.3f + 0.025f;
			this.boobbones[n].localPosition = this.v3;
		}
		for (int num6 = 2; num6 < this.boobRigidbodies.Length; num6++)
		{
			this.v3 = this.lastBoobPos[num6 - 2] - this.boobRigidbodies[num6 - 2].position + this.amountMovedHorizontal;
			if (this.v3.magnitude < 2f)
			{
				this.boobRigidbodies[num6].AddForce(this.v3 * 120f, ForceMode.Impulse);
			}
			this.lastBoobPos[num6 - 2] = this.boobRigidbodies[num6 - 2].position;
		}
	}

	public void updateBoneWeights(bool noBoobWeight = false)
	{
		this.updateBoneWeightsForSMR(this.parts[this.bodyPieceIndex].GetComponent<SkinnedMeshRenderer>(), this.originalBodyPieceBoneWeights, noBoobWeight);
		for (int i = 0; i < this.clothingPiecesEquipped.Count; i++)
		{
			this.updateBoneWeightsForSMR(this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>(), this.originalClothingBoneWeights[i], noBoobWeight);
		}
	}

	public void updateBoneWeightsForSMR(SkinnedMeshRenderer SMR, BoneWeight[] originalBoneWeights, bool noBoobWeight = false)
	{
		BoneWeight[] array = new BoneWeight[originalBoneWeights.Length];
		originalBoneWeights.CopyTo(array, 0);
		int num = -1;
		int num2 = -1;
		int num3 = -1;
		int num4 = -1;
		int num5 = -1;
		int num6 = -1;
		int num7 = -1;
		int num8 = -1;
		int num9 = -1;
		for (int i = 0; i < SMR.bones.Length; i++)
		{
			if (SMR.bones[i].name == "Breast_L")
			{
				num = i;
			}
			if (SMR.bones[i].name == "Breast_R")
			{
				num2 = i;
			}
			if (SMR.bones[i].name == "SpineUpper")
			{
				num3 = i;
			}
			if (SMR.bones[i].name == "SpineLower")
			{
				num5 = i;
			}
			if (SMR.bones[i].name == "Belly")
			{
				num4 = i;
			}
			if (SMR.bones[i].name == "Butt_L")
			{
				num9 = i;
			}
			if (SMR.bones[i].name == "Butt_R")
			{
				num8 = i;
			}
			if (SMR.bones[i].name == "Hip_L")
			{
				num7 = i;
			}
			if (SMR.bones[i].name == "Hip_R")
			{
				num6 = i;
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			float num10 = 0f;
			float num11 = 1f - 0.8f * this.cap(this.breastSize_act * 0.25f, 0f, 1f);
			float num12 = 0f;
			float num13 = 1f - 0.8f * this.cap(this.belly_act * 2f, 0f, 1f);
			float num14 = 0f;
			float num15 = 0.4f;
			if (noBoobWeight)
			{
				num11 = 1f;
			}
			if (array[j].boneIndex0 == num || array[j].boneIndex0 == num2)
			{
				float num16 = array[j].weight0 * num11;
				num10 += num16;
				array[j].weight0 -= num16;
			}
			if (array[j].boneIndex1 == num || array[j].boneIndex1 == num2)
			{
				float num16 = array[j].weight1 * num11;
				num10 += num16;
				array[j].weight1 -= num16;
			}
			if (array[j].boneIndex2 == num || array[j].boneIndex2 == num2)
			{
				float num16 = array[j].weight2 * num11;
				num10 += num16;
				array[j].weight2 -= num16;
			}
			if (array[j].boneIndex3 == num || array[j].boneIndex3 == num2)
			{
				float num16 = array[j].weight3 * num11;
				num10 += num16;
				array[j].weight3 -= num16;
			}
			if (num10 > 0f)
			{
				if (array[j].boneIndex0 == num3)
				{
					array[j].weight0 += num10;
					num10 = 0f;
				}
				if (array[j].boneIndex1 == num3)
				{
					array[j].weight1 += num10;
					num10 = 0f;
				}
				if (array[j].boneIndex2 == num3)
				{
					array[j].weight2 += num10;
					num10 = 0f;
				}
				if (array[j].boneIndex3 == num3)
				{
					array[j].weight3 += num10;
					num10 = 0f;
				}
				if (num10 > 0f)
				{
					array[j].weight3 = num10;
					array[j].boneIndex3 = num3;
				}
			}
			if (array[j].boneIndex0 == num4)
			{
				float num17 = array[j].weight0 * num13;
				num12 += num17;
				array[j].weight0 -= num17;
			}
			if (array[j].boneIndex1 == num4)
			{
				float num17 = array[j].weight1 * num13;
				num12 += num17;
				array[j].weight1 -= num17;
			}
			if (array[j].boneIndex2 == num4)
			{
				float num17 = array[j].weight2 * num13;
				num12 += num17;
				array[j].weight2 -= num17;
			}
			if (array[j].boneIndex3 == num4)
			{
				float num17 = array[j].weight3 * num13;
				num12 += num17;
				array[j].weight3 -= num17;
			}
			if (num12 > 0f)
			{
				if (array[j].boneIndex0 == num5)
				{
					array[j].weight0 += num12;
					num12 = 0f;
				}
				if (array[j].boneIndex1 == num5)
				{
					array[j].weight1 += num12;
					num12 = 0f;
				}
				if (array[j].boneIndex2 == num5)
				{
					array[j].weight2 += num12;
					num12 = 0f;
				}
				if (array[j].boneIndex3 == num5)
				{
					array[j].weight3 += num12;
					num12 = 0f;
				}
				if (num12 > 0f)
				{
					array[j].weight3 = num12;
					array[j].boneIndex3 = num5;
				}
			}
			if (array[j].boneIndex0 == num8)
			{
				float num18 = array[j].weight0 * num15;
				num14 += num18;
				array[j].weight0 -= num18;
			}
			if (array[j].boneIndex1 == num8)
			{
				float num18 = array[j].weight1 * num15;
				num14 += num18;
				array[j].weight1 -= num18;
			}
			if (array[j].boneIndex2 == num8)
			{
				float num18 = array[j].weight2 * num15;
				num14 += num18;
				array[j].weight2 -= num18;
			}
			if (array[j].boneIndex3 == num8)
			{
				float num18 = array[j].weight3 * num15;
				num14 += num18;
				array[j].weight3 -= num18;
			}
			if (num14 > 0f)
			{
				if (array[j].boneIndex0 == num6)
				{
					array[j].weight0 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex1 == num6)
				{
					array[j].weight1 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex2 == num6)
				{
					array[j].weight2 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex3 == num6)
				{
					array[j].weight3 += num14;
					num14 = 0f;
				}
				if (num14 > 0f)
				{
					array[j].weight3 = num14;
					array[j].boneIndex3 = num6;
				}
			}
			if (array[j].boneIndex0 == num9)
			{
				float num18 = array[j].weight0 * num15;
				num14 += num18;
				array[j].weight0 -= num18;
			}
			if (array[j].boneIndex1 == num9)
			{
				float num18 = array[j].weight1 * num15;
				num14 += num18;
				array[j].weight1 -= num18;
			}
			if (array[j].boneIndex2 == num9)
			{
				float num18 = array[j].weight2 * num15;
				num14 += num18;
				array[j].weight2 -= num18;
			}
			if (array[j].boneIndex3 == num9)
			{
				float num18 = array[j].weight3 * num15;
				num14 += num18;
				array[j].weight3 -= num18;
			}
			if (num14 > 0f)
			{
				if (array[j].boneIndex0 == num7)
				{
					array[j].weight0 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex1 == num7)
				{
					array[j].weight1 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex2 == num7)
				{
					array[j].weight2 += num14;
					num14 = 0f;
				}
				if (array[j].boneIndex3 == num7)
				{
					array[j].weight3 += num14;
					num14 = 0f;
				}
				if (num14 > 0f)
				{
					array[j].weight3 = num14;
					array[j].boneIndex3 = num7;
				}
			}
		}
		SMR.sharedMesh.boneWeights = array;
	}

	public void processClothing()
	{
		for (int i = 0; i < this.equippedSexToys.Count; i++)
		{
			this.equippedSexToys[i].process();
		}
		if (this.loadingClothes.Count > 0)
		{
			this.checkClothingLoad();
		}
	}

	public void mirrorTransform(Transform from, Transform to, bool mirrorRotFix = false)
	{
		to.position = from.position;
		if (mirrorRotFix)
		{
			this.v3 = from.localEulerAngles;
			this.v3.x *= -1f;
			this.v3.z *= -1f;
			to.localEulerAngles = this.v3;
		}
		else
		{
			to.rotation = from.rotation;
		}
	}

	public void killHeldItem(bool rightHand = true)
	{
		int index;
		if (rightHand)
		{
			index = 1;
			this.rightHandItem_current = string.Empty;
			this.rightHandItemHasFingerPoses = false;
		}
		else
		{
			index = 0;
			this.leftHandItem_current = string.Empty;
			this.leftHandItemHasFingerPoses = false;
		}
		if ((UnityEngine.Object)this.handheldObjects[index] != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(this.handheldObjects[index]);
		}
		this.handheldObjects[index] = null;
	}

	public void assignFingerTargets(Transform tar)
	{
		this.rightHandItemHasFingerPoses = true;
		this.hho_Finger00_R1 = tar.Find("Finger00_R");
		this.hho_Finger01_R1 = tar.Find("Finger00_R").Find("Finger01_R");
		this.hho_Finger02_R1 = tar.Find("Finger00_R").Find("Finger01_R").Find("Finger02_R");
		this.hho_Finger10_R1 = tar.Find("Finger10_R");
		this.hho_Finger11_R1 = tar.Find("Finger10_R").Find("Finger11_R");
		this.hho_Finger12_R1 = tar.Find("Finger10_R").Find("Finger11_R").Find("Finger12_R");
		this.hho_Finger20_R1 = tar.Find("Finger20_R");
		this.hho_Finger21_R1 = tar.Find("Finger20_R").Find("Finger21_R");
		this.hho_Finger22_R1 = tar.Find("Finger20_R").Find("Finger21_R").Find("Finger22_R");
		this.hho_Finger30_R1 = tar.Find("Finger30_R");
		this.hho_Finger31_R1 = tar.Find("Finger30_R").Find("Finger31_R");
		this.hho_Finger32_R1 = tar.Find("Finger30_R").Find("Finger31_R").Find("Finger32_R");
		this.hho_Thumb0_R1 = tar.Find("Thumb0_R");
		this.hho_Thumb1_R1 = tar.Find("Thumb0_R").Find("Thumb1_R");
		this.hho_Thumb2_R1 = tar.Find("Thumb0_R").Find("Thumb1_R").Find("Thumb2_R");
	}

	public void clearFingerTargets()
	{
		this.rightHandItemHasFingerPoses = false;
	}

	public void putItemInHand(string item, bool rightHand = true, string material = "")
	{
		if (!(item == string.Empty))
		{
			int index;
			Transform parent;
			if (rightHand)
			{
				index = 1;
				this.rightHandItem_current = item;
				parent = this.bones.Hand_R;
			}
			else
			{
				index = 0;
				this.leftHandItem_current = item;
				parent = this.bones.Hand_L;
			}
			try
			{
				UnityEngine.Object.Destroy(this.handheldObjects[index]);
			}
			catch
			{
			}
			this.handheldObjects[index] = UnityEngine.Object.Instantiate(TestingRoom.labItemContainer.Find(item).gameObject);
			if (material != string.Empty)
			{
				ToyMaterials.applyMaterialToObject(this.handheldObjects[index], material);
			}
			this.handheldObjects[index].SetActive(true);
			this.handheldObjects[index].transform.parent = parent;
			this.handheldObjects[index].transform.localPosition = Vector3.zero;
			this.handheldObjects[index].transform.localEulerAngles = Vector3.zero;
			this.handheldObjects[index].transform.localScale = Vector3.one;
			if (!rightHand)
			{
				this.v3 = this.handheldObjects[index].transform.localScale;
				this.v3.x *= -1f;
				this.handheldObjects[index].transform.localScale = this.v3;
				this.v3 = this.handheldObjects[index].transform.localEulerAngles;
				this.v3.z += 180f;
				this.handheldObjects[index].transform.localEulerAngles = this.v3;
				this.leftHandItemHasFingerPoses = ((UnityEngine.Object)this.handheldObjects[index].transform.Find("Finger00_R") != (UnityEngine.Object)null);
				if (this.leftHandItemHasFingerPoses)
				{
					this.hho_Finger00_R0 = this.handheldObjects[0].transform.Find("Finger00_R");
					this.hho_Finger01_R0 = this.handheldObjects[0].transform.Find("Finger00_R").Find("Finger01_R");
					this.hho_Finger02_R0 = this.handheldObjects[0].transform.Find("Finger00_R").Find("Finger01_R").Find("Finger02_R");
					this.hho_Finger10_R0 = this.handheldObjects[0].transform.Find("Finger10_R");
					this.hho_Finger11_R0 = this.handheldObjects[0].transform.Find("Finger10_R").Find("Finger11_R");
					this.hho_Finger12_R0 = this.handheldObjects[0].transform.Find("Finger10_R").Find("Finger11_R").Find("Finger12_R");
					this.hho_Finger20_R0 = this.handheldObjects[0].transform.Find("Finger20_R");
					this.hho_Finger21_R0 = this.handheldObjects[0].transform.Find("Finger20_R").Find("Finger21_R");
					this.hho_Finger22_R0 = this.handheldObjects[0].transform.Find("Finger20_R").Find("Finger21_R").Find("Finger22_R");
					this.hho_Finger30_R0 = this.handheldObjects[0].transform.Find("Finger30_R");
					this.hho_Finger31_R0 = this.handheldObjects[0].transform.Find("Finger30_R").Find("Finger31_R");
					this.hho_Finger32_R0 = this.handheldObjects[0].transform.Find("Finger30_R").Find("Finger31_R").Find("Finger32_R");
					this.hho_Thumb0_R0 = this.handheldObjects[0].transform.Find("Thumb0_R");
					this.hho_Thumb1_R0 = this.handheldObjects[0].transform.Find("Thumb0_R").Find("Thumb1_R");
					this.hho_Thumb2_R0 = this.handheldObjects[0].transform.Find("Thumb0_R").Find("Thumb1_R").Find("Thumb2_R");
				}
			}
			else
			{
				this.rightHandItemHasFingerPoses = ((UnityEngine.Object)this.handheldObjects[index].transform.Find("Finger00_R") != (UnityEngine.Object)null);
				if (this.rightHandItemHasFingerPoses)
				{
					this.hho_Finger00_R1 = this.handheldObjects[1].transform.Find("Finger00_R");
					this.hho_Finger01_R1 = this.handheldObjects[1].transform.Find("Finger00_R").Find("Finger01_R");
					this.hho_Finger02_R1 = this.handheldObjects[1].transform.Find("Finger00_R").Find("Finger01_R").Find("Finger02_R");
					this.hho_Finger10_R1 = this.handheldObjects[1].transform.Find("Finger10_R");
					this.hho_Finger11_R1 = this.handheldObjects[1].transform.Find("Finger10_R").Find("Finger11_R");
					this.hho_Finger12_R1 = this.handheldObjects[1].transform.Find("Finger10_R").Find("Finger11_R").Find("Finger12_R");
					this.hho_Finger20_R1 = this.handheldObjects[1].transform.Find("Finger20_R");
					this.hho_Finger21_R1 = this.handheldObjects[1].transform.Find("Finger20_R").Find("Finger21_R");
					this.hho_Finger22_R1 = this.handheldObjects[1].transform.Find("Finger20_R").Find("Finger21_R").Find("Finger22_R");
					this.hho_Finger30_R1 = this.handheldObjects[1].transform.Find("Finger30_R");
					this.hho_Finger31_R1 = this.handheldObjects[1].transform.Find("Finger30_R").Find("Finger31_R");
					this.hho_Finger32_R1 = this.handheldObjects[1].transform.Find("Finger30_R").Find("Finger31_R").Find("Finger32_R");
					this.hho_Thumb0_R1 = this.handheldObjects[1].transform.Find("Thumb0_R");
					this.hho_Thumb1_R1 = this.handheldObjects[1].transform.Find("Thumb0_R").Find("Thumb1_R");
					this.hho_Thumb2_R1 = this.handheldObjects[1].transform.Find("Thumb0_R").Find("Thumb1_R").Find("Thumb2_R");
				}
			}
		}
	}

	public void processHair()
	{
		for (int i = 0; i < this.hairAppendages.Count; i++)
		{
			this.hairAppendages[i].checkLoad();
			if (this.hairAppendages[i].built)
			{
				this.hairAppendages[i].process();
			}
		}
	}

	public static void initSystem()
	{
		RackCharacter.windowsOS = (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows);
		if (RackCharacter.windowsOS)
		{
			RackCharacter.baseModelFilename = "basemodel";
		}
		RackCharacter.macOS = (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX);
		if (RackCharacter.macOS)
		{
			RackCharacter.baseModelFilename = "basemodel_mac";
		}
		RackCharacter.linuxOS = (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Linux);
		if (RackCharacter.linuxOS)
		{
			RackCharacter.baseModelFilename = "basemodel_linux";
		}
		RackCharacter.cumEmitterTransform = GameObject.Find("cumEmitter").transform;
		RackCharacter.cumEmitter = ((Component)RackCharacter.cumEmitterTransform).GetComponent<ParticleSystem>();
		RackCharacter.ApplicationpersistentDataPath = Application.persistentDataPath;
		RackCharacter.blackV4 = new Vector4(0f, 0f, 0f, 1f);
		SexualPreferences.init();
		RackCharacter.initClipFixRegions();
		RackCharacter.headFurControlBase = (Resources.Load("headFUR") as Texture2D);
		RackCharacter.bodyFurControlBase = (Resources.Load("bodyFUR") as Texture2D);
		RackCharacter.wingFurControlBase = (Resources.Load("wingFUR") as Texture2D);
		if (RackCharacter.embellishmentMeshes == null)
		{
			RackCharacter.embellishmentMeshes = new List<Mesh>();
			Transform transform = GameObject.Find("paintables").transform;
			SkinnedMeshRenderer[] componentsInChildren = ((Component)transform).GetComponentsInChildren<SkinnedMeshRenderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].name != "RefBody")
				{
					RackCharacter.embellishmentMeshes.Add(componentsInChildren[i].sharedMesh);
				}
			}
		}
		RackCharacter.allSpecies = new List<string>();
		string[] files = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty, "*.rack2species");
		for (int j = 0; j < files.Length; j++)
		{
			string item = files[j].Split(Game.PathDirectorySeparatorChar)[files[j].Split(Game.PathDirectorySeparatorChar).Length - 1].Split('.')[0];
			RackCharacter.allSpecies.Add(item);
		}
		RackCharacter.initAllPieces();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + "texturecache" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + "fur" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + "decal_cache" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + "racknet_cache" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		RackCharacter.existingTextures = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty).ToList();
		for (int k = 0; k < RackCharacter.existingTextures.Count; k++)
		{
			RackCharacter.existingTextures[k] = RackCharacter.existingTextures[k].Split(Game.PathDirectorySeparatorChar)[RackCharacter.existingTextures[k].Split(Game.PathDirectorySeparatorChar).Length - 1];
			RackCharacter.existingTextures[k] = RackCharacter.existingTextures[k].Split(new string[1]
			{
				".png"
			}, StringSplitOptions.None)[0];
		}
		RackCharacter.furTypes = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "fur" + Game.PathDirectorySeparatorChar + string.Empty).ToList();
		for (int l = 0; l < RackCharacter.furTypes.Count; l++)
		{
			RackCharacter.furTypes[l] = RackCharacter.furTypes[l].Split(Game.PathDirectorySeparatorChar)[RackCharacter.furTypes[l].Split(Game.PathDirectorySeparatorChar).Length - 1];
			RackCharacter.furTypes[l] = RackCharacter.furTypes[l].Split(new string[1]
			{
				".png"
			}, StringSplitOptions.None)[0];
			if (!RackCharacter.furNoiseTextures.ContainsKey(RackCharacter.furTypes[l]))
			{
				Texture2D value = new Texture2D(2, 2);
				value.LoadImage(File.ReadAllBytes(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "fur" + Game.PathDirectorySeparatorChar + RackCharacter.furTypes[l] + ".png"));
				RackCharacter.furNoiseTextures.Add(RackCharacter.furTypes[l], value);
			}
		}
		RackCharacter.skinTypes = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "normals" + Game.PathDirectorySeparatorChar + string.Empty).ToList();
		for (int m = 0; m < RackCharacter.skinTypes.Count; m++)
		{
			RackCharacter.skinTypes[m] = RackCharacter.skinTypes[m].Split(Game.PathDirectorySeparatorChar)[RackCharacter.skinTypes[m].Split(Game.PathDirectorySeparatorChar).Length - 1];
			if (RackCharacter.skinTypes.Contains(RackCharacter.skinTypes[m].Split('_')[0]))
			{
				RackCharacter.skinTypes.RemoveAt(m);
				m--;
			}
			else
			{
				RackCharacter.skinTypes[m] = RackCharacter.skinTypes[m].Split('_')[0];
				if (!RackCharacter.skinNormalTextures.ContainsKey(RackCharacter.skinTypes[m] + "_head"))
				{
					Texture2D texture2D = new Texture2D(2, 2);
					texture2D.LoadImage(File.ReadAllBytes(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "normals" + Game.PathDirectorySeparatorChar + RackCharacter.skinTypes[m] + "_body.png"));
					UnityEngine.Color[] array = texture2D.GetPixels();
					texture2D.SetPixels(array);
					texture2D.Apply();
					RackCharacter.skinNormalTextures.Add(RackCharacter.skinTypes[m] + "_body", texture2D);
					texture2D = new Texture2D(2, 2);
					texture2D.LoadImage(File.ReadAllBytes(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterTextures" + Game.PathDirectorySeparatorChar + "normals" + Game.PathDirectorySeparatorChar + RackCharacter.skinTypes[m] + "_head.png"));
					array = texture2D.GetPixels();
					texture2D.SetPixels(array);
					texture2D.Apply();
					RackCharacter.skinNormalTextures.Add(RackCharacter.skinTypes[m] + "_head", texture2D);
				}
			}
		}
	}

	public bool textureExists(string texName)
	{
		for (int i = 0; i < RackCharacter.existingTextures.Count; i++)
		{
			if (RackCharacter.existingTextures[i] == texName)
			{
				return true;
			}
		}
		return false;
	}

	public static void initAllPieces()
	{
		if (RackCharacter.allPieceBundle == null)
		{
			RackCharacter.allPieceBundle = new CharacterBundle();
			RackCharacter.allPieceBundle.asseturl = Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characterParts" + Game.PathDirectorySeparatorChar + RackCharacter.baseModelFilename;
			Game.gameInstance.StartCoroutine(RackCharacter.allPieceBundle.loadBundle());
		}
	}

	public static void initWetnessMaps()
	{
		Texture2D texture2D = Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_vagina") as Texture2D;
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_vagina = texture2D.GetPixels();
		texture2D = (Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_penis") as Texture2D);
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_penis = texture2D.GetPixels();
		texture2D = (Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_finger0") as Texture2D);
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_finger0 = texture2D.GetPixels();
		texture2D = (Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_finger1") as Texture2D);
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_finger1 = texture2D.GetPixels();
		texture2D = (Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_fist") as Texture2D);
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_fist = texture2D.GetPixels();
		texture2D = (Resources.Load("wetnessmaps" + Game.PathDirectorySeparatorChar + "wetness_muzzle") as Texture2D);
		TextureScale.Bilinear(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality));
		RackCharacter.wetnessTex_muzzle = texture2D.GetPixels();
	}

	public void addClothingPiece(string clothingName)
	{
		for (int i = 0; i < Inventory.getItemDefinition(clothingName).equipSlots.Count; i++)
		{
			int clothingSlotIDByName = RackCharacter.getClothingSlotIDByName(Inventory.getItemDefinition(clothingName).equipSlots[i].name);
			if (Inventory.getItemDefinition(clothingName).equipSlots[i].occupies)
			{
				if (clothingSlotIDByName == ClothingSlots.CROTCH)
				{
					this.crotchCoveredByClothing = true;
				}
				if (clothingSlotIDByName == ClothingSlots.TORSO)
				{
					this.breastsCoveredByClothing = true;
				}
			}
		}
		AssetLoadManager.loadAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + clothingName.ToLower());
		this.checkForScaledTex(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + ".png", Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + clothingName + ".png");
		AssetLoadManager.loadAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + ".png");
		this.checkForScaledTex(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "FX.png", Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "FX.png");
		AssetLoadManager.loadAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "FX.png");
		this.checkForScaledTex(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "NRM.png", Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "NRM.png");
		AssetLoadManager.loadAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + clothingName + "NRM.png");
		this.loadingClothes.Add(clothingName);
	}

	private void checkForScaledTex(string targetFilename, string fullresFilename)
	{
		if (!File.Exists(targetFilename) && File.Exists(fullresFilename))
		{
			byte[] array = File.ReadAllBytes(fullresFilename);
			Texture2D texture2D = new Texture2D(4, 4);
			texture2D.LoadImage(array);
			TextureScaler.scale(texture2D, Mathf.RoundToInt((float)texture2D.width * UserSettings.data.characterTextureQuality), Mathf.RoundToInt((float)texture2D.height * UserSettings.data.characterTextureQuality), FilterMode.Trilinear);
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(targetFilename, bytes);
			UnityEngine.Object.Destroy(texture2D);
		}
	}

	public void checkClothingLoad()
	{
		if (AssetLoadManager.isEverythingLoaded())
		{
			bool flag = false;
			for (int num = this.loadingClothes.Count - 1; num >= 0; num--)
			{
				if (this.loadingClothes[num] != "RackChip")
				{
					flag = true;
				}
				this.clothingRefsBuilt = false;
				Debug.Log(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + this.loadingClothes[num].ToLower());
				GameObject gameObject = UnityEngine.Object.Instantiate((AssetLoadManager.getAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + this.loadingClothes[num].ToLower()).assetBundle.LoadAsset(this.loadingClothes[num]) as GameObject).transform.Find(this.loadingClothes[num]).gameObject);
				gameObject.name = this.loadingClothes[num];
				gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = UnityEngine.Object.Instantiate(gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh);
				this.clothingPieceStartPoseMesh.Add(new Mesh());
				gameObject.GetComponent<SkinnedMeshRenderer>().BakeMesh(this.clothingPieceStartPoseMesh[this.clothingPieceStartPoseMesh.Count - 1]);
				this.addBodyBlendsToClothingPiece(gameObject.GetComponent<SkinnedMeshRenderer>(), this.clothingPieceStartPoseMesh[this.clothingPieceStartPoseMesh.Count - 1]);
				Material material = new Material(this.game.clothingShader);
				material.CopyPropertiesFromMaterial(this.game.defaultMaterial);
				for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
				{
					gameObject.GetComponent<Renderer>().materials[i].shader = this.game.clothingShader;
					gameObject.GetComponent<Renderer>().materials[i].CopyPropertiesFromMaterial(this.game.defaultMaterial);
					Texture2D texture2D = new Texture2D(4, 4);
					AssetLoadManager.getAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + this.loadingClothes[num] + ".png").LoadImageIntoTexture(texture2D);
					gameObject.GetComponent<Renderer>().materials[i].SetTexture("_MainTex", texture2D);
					Texture2D texture2D2 = new Texture2D(4, 4);
					AssetLoadManager.getAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + this.loadingClothes[num] + "NRM.png").LoadImageIntoTexture(texture2D2);
					UnityEngine.Color[] array = texture2D2.GetPixels();
					texture2D2.SetPixels(array);
					texture2D2.Apply();
					gameObject.GetComponent<Renderer>().materials[i].SetTexture("_BumpMap", texture2D2);
					gameObject.GetComponent<Renderer>().materials[i].EnableKeyword("_NORMALMAP");
					gameObject.GetComponent<Renderer>().materials[i].EnableKeyword("_METALLICGLOSSMAP");
					gameObject.GetComponent<Renderer>().materials[i].EnableKeyword("_EMISSION");
					gameObject.GetComponent<Renderer>().materials[i].SetColor("_EmissiveColor", UnityEngine.Color.white);
					Texture2D texture2D3 = new Texture2D(4, 4);
					AssetLoadManager.getAsset(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "clothing" + Game.PathDirectorySeparatorChar + string.Empty + Mathf.RoundToInt(UserSettings.data.characterTextureQuality * 100f) + string.Empty + Game.PathDirectorySeparatorChar + string.Empty + this.loadingClothes[num] + "FX.png").LoadImageIntoTexture(texture2D3);
					Texture2D texture2D4 = new Texture2D(texture2D3.width, texture2D3.height);
					UnityEngine.Color[] array2 = texture2D3.GetPixels();
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j].a = array2[j].r;
						array2[j].r = array2[j].b;
						array2[j].g = (array2[j].b = 0f);
					}
					texture2D4.SetPixels(array2);
					texture2D4.Apply();
					Texture2D texture2D5 = new Texture2D(texture2D3.width, texture2D3.height);
					array2 = texture2D3.GetPixels();
					UnityEngine.Color[] array3 = texture2D.GetPixels();
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k].r = array3[k].r * array2[k].g;
						array2[k].b = array3[k].b * array2[k].g;
						array2[k].g = array3[k].g * array2[k].g;
					}
					texture2D5.SetPixels(array2);
					texture2D5.Apply();
					gameObject.GetComponent<Renderer>().materials[i].SetTexture("_MetallicGlossMap", texture2D4);
					gameObject.GetComponent<Renderer>().materials[i].SetTexture("_EmissionMap", texture2D5);
				}
				this.clothingRefData.Add(new ClothingReferenceData());
				this.assimilatePart(gameObject, "body_universal", false);
				this.clothingPiecesEquipped.Add(gameObject);
				this.originalClothingBoneWeights.Add(new BoneWeight[gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.Length]);
				gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.boneWeights.CopyTo(this.originalClothingBoneWeights[this.originalClothingBoneWeights.Count - 1], 0);
				this.loadingClothes.RemoveAt(num);
			}
			if (flag)
			{
				for (int l = 0; l < this.parts.Count; l++)
				{
					UnityEngine.Object.Destroy(this.parts[l].GetComponent<SkinnedMeshRenderer>().sharedMesh);
					UnityEngine.Object.Destroy(this.parts[l]);
				}
				this.createPieces();
				for (int m = 0; m < this.parts.Count; m++)
				{
					this.assimilatePart(this.parts[m], "body_universal", false);
				}
				this.applyTexture();
				this.animator.Rebind();
				this.createPreciseRaycastingMesh();
				this.applyCustomization();
				for (int n = 0; n < this.parts.Count; n++)
				{
					if (this.wasFaded[n])
					{
						this.wasFaded[n] = false;
						this.fadeAmount[n] += (0.5f - this.fadeAmount[n]) * 0.01f;
					}
				}
			}
		}
	}

	public void addBodyBlendsToClothingPiece(SkinnedMeshRenderer clothingPiece, Mesh refMesh)
	{
		List<BlendShapeDefinition> list = new List<BlendShapeDefinition>();
		float num = 0.01f;
		int[] array = new int[clothingPiece.sharedMesh.vertexCount];
		int[] array2 = new int[clothingPiece.sharedMesh.vertexCount];
		bool flag = false;
		int num2 = 0;
		while (num2 < RackCharacter.clothingRefDefinitions.Count)
		{
			if (!(RackCharacter.clothingRefDefinitions[num2].clothingName == clothingPiece.name + this.footType))
			{
				num2++;
				continue;
			}
			flag = true;
			array = RackCharacter.clothingRefDefinitions[num2].refPieces;
			array2 = RackCharacter.clothingRefDefinitions[num2].refVerts;
			break;
		}
		Vector3[] vertices = clothingPiece.sharedMesh.vertices;
		Vector3[] vertices2 = this.oBody.vertices;
		Vector3[] vertices3 = this.feetPiece.sharedMesh.vertices;
		if (!flag)
		{
			for (int i = 0; i < vertices.Length; i++)
			{
				array2[i] = 0;
				float num3 = 99f;
				for (int j = 0; (float)j < this.cap((float)vertices2.Length, 0f, (float)this.oBody.vertexCount); j++)
				{
					float magnitude = (vertices[i] - vertices2[j]).magnitude;
					if (magnitude < num3)
					{
						num3 = magnitude;
						array[i] = 0;
						array2[i] = j;
						if (num3 <= num)
						{
							break;
						}
					}
				}
				for (int k = 0; (float)k < this.cap((float)vertices3.Length, 0f, (float)this.oFeet.vertexCount); k++)
				{
					float magnitude = (vertices[i] - vertices3[k]).magnitude;
					if (magnitude < num3)
					{
						num3 = magnitude;
						array[i] = 1;
						array2[i] = k;
						if (num3 <= num)
						{
							break;
						}
					}
				}
			}
			clothingRefVertDefinition clothingRefVertDefinition = new clothingRefVertDefinition();
			clothingRefVertDefinition.clothingName = clothingPiece.name + this.footType;
			clothingRefVertDefinition.refPieces = array;
			clothingRefVertDefinition.refVerts = array2;
			RackCharacter.clothingRefDefinitions.Add(clothingRefVertDefinition);
		}
		for (int l = 0; l < this.oBody.blendShapeCount; l++)
		{
			BlendShapeDefinition blendShapeDefinition = new BlendShapeDefinition();
			blendShapeDefinition.name = this.oBody.GetBlendShapeName(l);
			blendShapeDefinition.verts = new Vector3[this.oBody.vertexCount];
			blendShapeDefinition.normals = new Vector3[this.oBody.vertexCount];
			blendShapeDefinition.tangents = new Vector3[this.oBody.vertexCount];
			this.oBody.GetBlendShapeFrameVertices(l, 0, blendShapeDefinition.verts, blendShapeDefinition.normals, blendShapeDefinition.tangents);
			Vector3[] array3 = new Vector3[clothingPiece.sharedMesh.vertexCount];
			Vector3[] normals = new Vector3[clothingPiece.sharedMesh.vertexCount];
			Vector3[] tangents = new Vector3[clothingPiece.sharedMesh.vertexCount];
			for (int m = 0; m < array3.Length; m++)
			{
				if (array[m] == 0)
				{
					array3[m] = blendShapeDefinition.verts[array2[m]];
				}
			}
			blendShapeDefinition.verts = array3;
			blendShapeDefinition.normals = normals;
			blendShapeDefinition.tangents = tangents;
			list.Add(blendShapeDefinition);
		}
		for (int n = 0; n < this.oFeet.blendShapeCount; n++)
		{
			BlendShapeDefinition blendShapeDefinition2 = new BlendShapeDefinition();
			blendShapeDefinition2.name = this.oFeet.GetBlendShapeName(n);
			blendShapeDefinition2.verts = new Vector3[this.oFeet.vertexCount];
			blendShapeDefinition2.normals = new Vector3[this.oFeet.vertexCount];
			blendShapeDefinition2.tangents = new Vector3[this.oFeet.vertexCount];
			this.oFeet.GetBlendShapeFrameVertices(n, 0, blendShapeDefinition2.verts, blendShapeDefinition2.normals, blendShapeDefinition2.tangents);
			Vector3[] array4 = new Vector3[clothingPiece.sharedMesh.vertexCount];
			Vector3[] normals2 = new Vector3[clothingPiece.sharedMesh.vertexCount];
			Vector3[] tangents2 = new Vector3[clothingPiece.sharedMesh.vertexCount];
			for (int num4 = 0; num4 < array4.Length; num4++)
			{
				if (array[num4] == 1)
				{
					try
					{
						array4[num4] = blendShapeDefinition2.verts[array2[num4]];
					}
					catch
					{
						Debug.Log(num4 + ": " + array2[num4] + " / " + blendShapeDefinition2.verts.Length);
					}
				}
			}
			blendShapeDefinition2.verts = array4;
			blendShapeDefinition2.normals = normals2;
			blendShapeDefinition2.tangents = tangents2;
			list.Add(blendShapeDefinition2);
		}
		for (int num5 = 0; num5 < clothingPiece.sharedMesh.blendShapeCount; num5++)
		{
			BlendShapeDefinition blendShapeDefinition3 = new BlendShapeDefinition();
			blendShapeDefinition3.name = clothingPiece.sharedMesh.GetBlendShapeName(num5);
			blendShapeDefinition3.verts = new Vector3[clothingPiece.sharedMesh.vertexCount];
			blendShapeDefinition3.normals = new Vector3[clothingPiece.sharedMesh.vertexCount];
			blendShapeDefinition3.tangents = new Vector3[clothingPiece.sharedMesh.vertexCount];
			clothingPiece.sharedMesh.GetBlendShapeFrameVertices(num5, 0, blendShapeDefinition3.verts, blendShapeDefinition3.normals, blendShapeDefinition3.tangents);
			list.Add(blendShapeDefinition3);
		}
		refMesh.ClearBlendShapes();
		clothingPiece.sharedMesh.ClearBlendShapes();
		for (int num6 = 0; num6 < list.Count; num6++)
		{
			clothingPiece.sharedMesh.AddBlendShapeFrame(list[num6].name, 100f, list[num6].verts, list[num6].normals, list[num6].tangents);
			refMesh.AddBlendShapeFrame(list[num6].name, 100f, list[num6].verts, list[num6].normals, list[num6].tangents);
		}
		clothingPiece.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = clothingPiece.sharedMesh;
	}

	private ClothingReferenceData buildReferenceForClothingPiece(GameObject clothingPiece)
	{
		return new ClothingReferenceData();
	}

	public float getPreference(string preference)
	{
		if (this.preferences.ContainsKey(preference))
		{
			return this.preferences[preference];
		}
		return 0f;
	}

	public float getHeadScaleForSpecies(string species)
	{
		if (species != null)
		{
			if (!(species == "rodent"))
			{
				if (!(species == "mustelid"))
				{
					if (!(species == "lizard"))
					{
						if (!(species == "equine"))
						{
							if (!(species == "cervine"))
							{
								if (species == "bovine")
								{
									return 1.1f;
								}
								goto IL_008f;
							}
							return 1.05f;
						}
						return 0.99f;
					}
					return 1.01f;
				}
				return 1.06f;
			}
			return 1.01f;
		}
		goto IL_008f;
		IL_008f:
		return 1f;
	}

	public void stepLift(float amount)
	{
		this.stepLiftAmount += amount;
		this.stepLifting = true;
	}

	public void processMovement()
	{
		this.timeSinceTeleport += Time.deltaTime;
		if (this.timeSinceTeleport < 0.5f)
		{
			this.resetAllFloppyBodies();
		}
		this.previousPosition = this.GO.transform.position;
		if ((UnityEngine.Object)this.interactionPoint != (UnityEngine.Object)null)
		{
			if (this.stepLifting)
			{
				if (!this.stepliftInitted)
				{
					if ((UnityEngine.Object)this.stepLiftSource == (UnityEngine.Object)null)
					{
						TestingRoom.labItemContainer.gameObject.SetActive(true);
						this.stepLiftSource = GameObject.Find("LabItems").transform.Find("StepLift").gameObject;
						TestingRoom.labItemContainer.gameObject.SetActive(false);
					}
					this.steplift = UnityEngine.Object.Instantiate(this.stepLiftSource).GetComponent<StepLift>();
					this.steplift.transform.SetParent(this.game.World.transform);
					this.steplift.owner = this;
					this.stepliftInitted = true;
				}
				this.stepLifting = false;
				this.stepLiftVel = 0f;
			}
			else if (this.stepLiftAmount > 0f)
			{
				this.stepLiftVel += Time.deltaTime;
				this.stepLiftAmount -= this.stepLiftVel * Time.deltaTime;
				if (this.stepLiftAmount < 0f)
				{
					this.stepLiftAmount = 0f;
				}
			}
			if (this.stepliftInitted)
			{
				this.steplift.useLift(this.stepLiftAmount);
				this.steplift.process();
			}
			if (this.interactionSubject.apparatus.adjustingHeight)
			{
				this.fadeOutCharacter(1f);
			}
			this.moveGO(this.interactionPoint.transform.position + Vector3.up * this.stepLiftAmount - this.GO.transform.position);
			this.GO.transform.rotation = this.interactionPoint.transform.rotation;
			this.moveSpeed = 0f;
			this.moving = false;
			this.animator.SetBool("Moving", this.moving);
			this.animator.SetFloat("MoveSpeed", this.cap(this.moveSpeed / this.height_act, 0.1f, 1f));
			return;
		}
		if ((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null)
		{
			Vector3 position = this.apparatus.scaleWithCharacter.position;
			float x = position.x;
			Vector3 position2 = this.apparatus.scaleWithCharacter.position;
			float y = position2.y;
			Vector3 position3 = this.apparatus.scaleWithCharacter.position;
			float z = position3.z;
			Vector3 eulerAngles = this.apparatus.transform.eulerAngles;
			this.teleport(x, y, z, eulerAngles.y, true);
			this.movementTarget.position = this.GO.transform.position;
			this.movementTarget.gameObject.layer = 13;
			return;
		}
		if (this.boundPose != string.Empty || (UnityEngine.Object)this.furniture != (UnityEngine.Object)null)
		{
			this.movementTarget.position = this.GO.transform.position;
			this.movementTarget.gameObject.layer = 13;
		}
		else
		{
			this.movementTarget.gameObject.layer = 15;
		}
		if (this.uid == this.game.headshotSubjectUID && this.game.recentThinking <= 0f && this.game.loadTransition >= 1f)
		{
			if (!this.wasInRenderStudio)
			{
				this.positionBeforeRenderStudio = this.GO.transform.position;
				Vector3 eulerAngles2 = this.GO.transform.rotation.eulerAngles;
				this.rotationBeforeRenderStudio = eulerAngles2.y;
				Vector3 position4 = this.game.renderStudio.transform.position;
				float x2 = position4.x;
				Vector3 position5 = this.game.renderStudio.transform.position;
				float y2 = position5.y;
				Vector3 position6 = this.game.renderStudio.transform.position;
				this.teleport(x2, y2, position6.z, -999f, false);
				this.v3 = this.GO.transform.eulerAngles;
				this.v3.y = 0f;
				this.GO.transform.eulerAngles = this.v3;
				this.wasInRenderStudio = true;
			}
			if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("PhotoSmirk"))
			{
				if (this.photoDelay > 0f)
				{
					this.photoDelay -= Time.deltaTime;
				}
				else
				{
					this.game.takeSnapshot();
					this.teleport(this.positionBeforeRenderStudio.x, this.positionBeforeRenderStudio.y, this.positionBeforeRenderStudio.z, -999f, false);
					this.v3 = this.GO.transform.eulerAngles;
					this.v3.y = this.rotationBeforeRenderStudio;
					this.GO.transform.eulerAngles = this.v3;
					this.needPostPhotoAnimationReset = true;
				}
			}
			return;
		}
		if (this.uid != this.game.headshotSubjectUID)
		{
			this.wasInRenderStudio = false;
			this.photoDelay = 0.25f;
			if (this.needPostPhotoAnimationReset)
			{
				if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("PhotoSmirk"))
				{
					this.setPose("Idle");
				}
				else
				{
					this.needPostPhotoAnimationReset = false;
				}
			}
		}
		if (this.lastMovingSurfaceID != -1)
		{
			this.v3 = this.currentMovingSurface.transform.position - this.lastMovingSurfacePosition;
			this.lastMTPosition += this.v3;
			this.targetLocation += this.v3;
			this.movementTarget.transform.Translate(this.v3);
			this.moveGO(this.v3);
		}
		int num = UserSettings.data.defaultMoveMode;
		switch (UserSettings.data.defaultMoveMode)
		{
		case 0:
			if (Input.GetKey(UserSettings.data.KEY_WALK))
			{
				num = 1;
			}
			if (Input.GetKey(UserSettings.data.KEY_SPRINT))
			{
				num = 2;
			}
			break;
		case 1:
			if (Input.GetKey(UserSettings.data.KEY_WALK))
			{
				num = 0;
			}
			if (Input.GetKey(UserSettings.data.KEY_SPRINT))
			{
				num = 2;
			}
			break;
		case 2:
			if (Input.GetKey(UserSettings.data.KEY_WALK))
			{
				num = 0;
			}
			if (Input.GetKey(UserSettings.data.KEY_SPRINT))
			{
				num = 1;
			}
			break;
		}
		if (this.autoWalking)
		{
			if (this.controlledByPlayer || (UnityEngine.Object)this.npcData == (UnityEngine.Object)null)
			{
				this.selectedMoveSpeed = 1f;
			}
			else
			{
				this.selectedMoveSpeed = this.npcData.walkSpeed;
			}
		}
		else
		{
			switch (num)
			{
			case 0:
				this.selectedMoveSpeed = 5f;
				break;
			case 2:
				this.selectedMoveSpeed = 16f;
				if (this.runSpeedCheat)
				{
					this.selectedMoveSpeed = 40f;
				}
				break;
			default:
				this.selectedMoveSpeed = 12f;
				break;
			}
		}
		this.pressingAnyMovementKeys = false;
		this.backpedaling = false;
		if (this.controlledByPlayer && !this.autoWalking && !this.game.lockedPosition && !this.game.customizingCharacter)
		{
			float camFollowAngle = this.game.camFollowAngle;
			this.walkAimer.x = (this.walkAimer.z = 0f);
			if (Input.GetKey(UserSettings.data.KEY_WALK_FORWARD) && !this.game.popupOpen && !Input.GetKey(KeyCode.LeftAlt))
			{
				this.pressingAnyMovementKeys = true;
				this.walkAimer.x -= Mathf.Cos(camFollowAngle);
				this.walkAimer.z -= Mathf.Sin(camFollowAngle);
			}
			if (Input.GetKey(UserSettings.data.KEY_STRAFE_LEFT) && !this.game.popupOpen && !Input.GetKey(KeyCode.LeftAlt))
			{
				this.pressingAnyMovementKeys = true;
				this.walkAimer.x += Mathf.Cos(camFollowAngle - this.ninetyDegrees);
				this.walkAimer.z += Mathf.Sin(camFollowAngle - this.ninetyDegrees);
			}
			if (Input.GetKey(UserSettings.data.KEY_STRAFE_RIGHT) && !this.game.popupOpen && !Input.GetKey(KeyCode.LeftAlt))
			{
				this.pressingAnyMovementKeys = true;
				this.walkAimer.x += Mathf.Cos(camFollowAngle + this.ninetyDegrees);
				this.walkAimer.z += Mathf.Sin(camFollowAngle + this.ninetyDegrees);
			}
			if (Input.GetKey(UserSettings.data.KEY_WALK_BACKWARD) && !this.game.popupOpen && !Input.GetKey(KeyCode.LeftAlt))
			{
				if (this.controlledByPlayer && this.game.firstPersonMode)
				{
					this.backpedaling = !Input.GetKey(UserSettings.data.KEY_WALK_FORWARD);
				}
				this.pressingAnyMovementKeys = true;
				this.walkAimer.x += Mathf.Cos(camFollowAngle);
				this.walkAimer.z += Mathf.Sin(camFollowAngle);
			}
			this.walkAimer.Normalize();
			this.walkAimer *= Time.deltaTime * this.selectedMoveSpeed;
			this.targetLocation += (this.lastMTPosition - this.targetLocation) * 0.95f;
			this.movementTarget.transform.Translate(this.walkAimer);
			if (!this.midair && !this.jumping && Input.GetKeyDown(UserSettings.data.KEY_JUMP))
			{
				this.jumpVelY = 6f;
				this.jumping = true;
			}
		}
		if (this.controlledByPlayer && this.game.lockedPosition)
		{
			this.targetLocation = this.GO.transform.position;
		}
		if (this.autoWalking)
		{
			this.targetLocation = this.autoWalkLocation;
			this.timeSpentAutowalking += Time.deltaTime;
			if (!(this.timeSpentAutowalking > this.autowalkTimeout))
			{
				if ((UnityEngine.Object)this.npcData != (UnityEngine.Object)null)
				{
					Vector3 position7 = Game.gameInstance.PC().GO.transform.position;
					if (position7.z > -18f)
					{
						goto IL_0b80;
					}
				}
				goto IL_0bc3;
			}
			goto IL_0b80;
		}
		this.timeSpentAutowalking = 0f;
		goto IL_0bc3;
		IL_0b80:
		this.teleport(this.autoWalkLocation.x, this.autoWalkLocation.y, this.autoWalkLocation.z, this.autoWalkFinishAngle.y, false);
		goto IL_0bc3;
		IL_13ab:
		bool flag = false;
		float num2 = 0f;
		this.v3 = this.movementTarget.position + Vector3.up * 1f;
		flag = Physics.Raycast(this.v3, Vector3.down, out this.hit, 1.5f, 257);
		while (!flag && num2 < 2f)
		{
			num2 += 0.5f;
			this.v3.y += 0.5f;
			flag = Physics.Raycast(this.v3, Vector3.down, out this.hit, 0.5f, 257);
		}
		if (flag)
		{
			this.standingOnSurface = this.hit.collider.transform;
		}
		if (!this.pressingAnyMovementKeys && this.controlledByPlayer && !this.midair && this.hit.normal != Vector3.up)
		{
			this.movementTarget.transform.position = this.lastMTPosition;
		}
		this.lastMTPosition = this.movementTarget.transform.position;
		if (!flag && this.controlledByPlayer && this.recentFloorFall <= 0f && this.timeSinceTeleport > 1f)
		{
			this.midair = true;
		}
		else
		{
			this.midair = false;
		}
		if (this.midair && this.recentFloorFall <= 0f && this.timeSinceTeleport > 1f)
		{
			this.movementTarget.useGravity = true;
		}
		else
		{
			this.movementTarget.useGravity = false;
			Rigidbody rigidbody = this.movementTarget;
			rigidbody.velocity *= 0f;
		}
		bool flag2 = false;
		this.v3 = this.GO.transform.position;
		this.v3.y += 4f;
		RaycastHit raycastHit = default(RaycastHit);
		flag2 = Physics.Raycast(this.v3, Vector3.down, out raycastHit, 10.5f, 257);
		ref Vector3 val = ref this.v3;
		Vector3 position8 = this.movementTarget.position;
		val.y = position8.y;
		if (flag2)
		{
			Vector3 point = raycastHit.point;
			if (point.y < this.v3.y)
			{
				ref Vector3 val2 = ref this.v3;
				Vector3 point2 = raycastHit.point;
				val2.y = point2.y;
			}
			this.moveGO(this.v3 - this.GO.transform.position);
			this.lastKnownGoodLocation = this.v3;
			this.recentFloorFall = 0f;
		}
		else if (this.controlledByPlayer)
		{
			if (this.floorFixDelay <= 0f)
			{
				for (float num3 = 1f; num3 < 100f; num3 += num3)
				{
					if (flag2)
					{
						break;
					}
					this.v3 = this.GO.transform.position;
					this.v3.y += 5f;
					flag2 = Physics.Raycast(this.v3, Vector3.down, out raycastHit, num3 + 10f, 257);
				}
				if (!flag2)
				{
					if (this.recentFloorFall <= 0f)
					{
						this.teleport(this.lastKnownGoodLocation.x, this.lastKnownGoodLocation.y, this.lastKnownGoodLocation.z, -999f, false);
					}
					else
					{
						this.teleport(3.1f, -4f, -146.3f, 297f, false);
					}
					this.recentFloorFall = 3f;
				}
				else
				{
					Vector3 point3 = raycastHit.point;
					float x3 = point3.x;
					Vector3 point4 = raycastHit.point;
					float y3 = point4.y;
					Vector3 point5 = raycastHit.point;
					this.teleport(x3, y3, point5.z, -999f, false);
				}
			}
			else
			{
				this.floorFixDelay -= Time.deltaTime;
			}
		}
		if (this.jumping)
		{
			this.jumpVelY -= Time.deltaTime * 25f;
			this.jumpY += this.jumpVelY * 0.065f;
			if (this.jumpY < 0f)
			{
				this.jumpY = 0f;
			}
			this.GO.transform.Translate(0f, this.jumpY, 0f);
			if (this.jumpY <= 0f)
			{
				this.jumpVelY = 0f;
				this.jumping = false;
				this.movementTarget.position = this.GO.transform.position;
			}
		}
		if (this.recentJump < 99f)
		{
			this.recentJump += Time.deltaTime;
		}
		this.animator.SetBool("Moving", this.moving);
		this.animator.SetBool("Plantigrade", this.effectivelyPlantigrade);
		this.animator.SetFloat("MoveSpeed", this.cap(this.moveSpeed / this.height_act, 0.1f, 1f));
		float num4 = (this.moveSpeed - 0.13f) / 0.53f;
		if (num4 > 1f)
		{
			num4 = 1f;
		}
		this.animator.SetFloat("RunAmount", num4);
		int num5 = -1;
		this.ridingMovingElevator = false;
		if (flag2)
		{
			if (raycastHit.transform.name == "Platform" && (UnityEngine.Object)raycastHit.transform.Find("ElevatorPlatform") != (UnityEngine.Object)null)
			{
				this.ridingMovingElevator = ((Component)raycastHit.transform.parent).GetComponent<Elevator>().elevatorMoving;
			}
			if (raycastHit.transform.gameObject.tag == "MovingSurface")
			{
				num5 = raycastHit.transform.gameObject.GetInstanceID();
				this.currentMovingSurface = raycastHit.transform.gameObject;
				this.lastMovingSurfacePosition = raycastHit.transform.position;
				this.lastMovingSurfaceID = num5;
			}
			else
			{
				this.lastMovingSurfaceID = -1;
			}
		}
		else
		{
			this.lastMovingSurfaceID = -1;
		}
		if (!this.controlledByPlayer && !this.animatingMovement)
		{
			this.movementTarget.transform.position = this.GO.transform.position;
		}
		if (this.backpedaling)
		{
			this.GO.transform.Rotate(0f, 180f, 0f);
		}
		if (this.autoWalking)
		{
			this.movementTarget.position = this.GO.transform.position;
			this.lastMTPosition = this.movementTarget.position;
		}
		if ((this.targetLocation - this.GO.transform.position).magnitude < 0.35f && this.autoWalking)
		{
			this.teleport(this.targetLocation.x, this.targetLocation.y, this.targetLocation.z, -999f, false);
			this.v3 = this.GO.transform.localEulerAngles;
			this.v3.y += Game.degreeDist(this.v3.y, this.autoWalkFinishAngle.y) * Time.deltaTime * 9f;
			this.GO.transform.localEulerAngles = this.v3;
			this.timeSpentTryingToGetAutowalkAngle += Time.deltaTime;
			Vector3 localEulerAngles = this.GO.transform.localEulerAngles;
			if (Mathf.Abs(localEulerAngles.y - this.autoWalkFinishAngle.y) < 4f || this.timeSpentTryingToGetAutowalkAngle > 1f)
			{
				this.moveSpeed = 0f;
				this.autoWalking = false;
				if (this.autowalkCallback != null)
				{
					this.autowalkCallback();
				}
			}
		}
		else
		{
			this.timeSpentTryingToGetAutowalkAngle = 0f;
		}
		this.amountMoved = this.GO.transform.position - this.previousPosition;
		this.amountMovedHorizontal = this.amountMoved;
		this.amountMovedHorizontal.y = 0f;
		this.previousPosition = this.GO.transform.position;
		if (this.recentFloorFall > 0f)
		{
			this.recentFloorFall -= Time.deltaTime;
		}
		return;
		IL_0bc3:
		if (!this.controlledByPlayer && this.followPC && (this.GO.transform.position - this.game.PC().GO.transform.position).magnitude > 20f)
		{
			this.targetLocation = this.game.PC().GO.transform.position;
		}
		this.distanceFromMovementTarget = (this.targetLocation - this.GO.transform.position).magnitude;
		float y4 = this.targetLocation.y;
		ref Vector3 val3 = ref this.targetLocation;
		Vector3 position9 = this.GO.transform.position;
		val3.y = position9.y;
		this.horizontalDistanceFromMovementTarget = (this.targetLocation - this.GO.transform.position).magnitude;
		this.targetLocation.y = y4;
		if ((this.horizontalDistanceFromMovementTarget > 0.3f || this.pressingAnyMovementKeys || this.midair || this.jumping) && (UnityEngine.Object)this.furniture == (UnityEngine.Object)null && (UnityEngine.Object)this.apparatus == (UnityEngine.Object)null)
		{
			if (!this.moving)
			{
				this.animator.SetTrigger("Idle");
			}
			this.moveSpeed += (this.horizontalDistanceFromMovementTarget * 0.4f - this.moveSpeed) * 0.4f;
			if (this.moveSpeed > 1f && !this.runSpeedCheat)
			{
				this.moveSpeed = 1f;
			}
			if (this.horizontalDistanceFromMovementTarget > 0.3f)
			{
				Quaternion localRotation = this.GO.transform.localRotation;
				this.v32 = this.targetLocation;
				ref Vector3 val4 = ref this.v32;
				Vector3 position10 = this.GO.transform.position;
				val4.y = position10.y;
				this.GO.transform.LookAt(this.v32);
				if (this.controlledByPlayer && !this.autoWalking)
				{
					this.v3 = Vector3.forward * this.moveSpeed * Time.deltaTime * 19f;
					this.GO.transform.Translate(this.v3, this.GO.transform);
					Vector3 position11 = this.movementTarget.position;
					float y5 = position11.y;
					Vector3 position12 = this.GO.transform.position;
					this.climbHelper = (y5 - position12.y) * 7f / this.selectedMoveSpeed;
					if (this.climbHelper < 0f)
					{
						this.climbHelper = 0f;
					}
					this.GO.transform.localRotation = Quaternion.Slerp(localRotation, this.GO.transform.localRotation, this.cap(Time.deltaTime * 10f, 0f, 1f));
				}
				else
				{
					this.extraMoveRotation = 0f;
					if (this.distanceFromMovementTarget < 5f)
					{
						this.extraMoveRotation = 0.3f;
					}
					if (this.distanceFromMovementTarget < 3f)
					{
						this.extraMoveRotation = 0.5f;
					}
					if (this.distanceFromMovementTarget < 1f)
					{
						this.extraMoveRotation = 0.7f;
					}
					this.GO.transform.localRotation = Quaternion.Slerp(localRotation, this.GO.transform.localRotation, 0.3f + this.extraMoveRotation);
					if (this.autoWalking)
					{
						this.moveSpeed = 0.25f;
					}
					if (this.autoWalking && !this.controlledByPlayer && (UnityEngine.Object)this.npcData != (UnityEngine.Object)null)
					{
						this.moveSpeed *= this.npcData.walkSpeed;
					}
					this.v3 = Vector3.forward * this.moveSpeed * Time.deltaTime * 19f;
					this.GO.transform.Translate(this.v3, this.GO.transform);
					Vector3 position13 = this.movementTarget.position;
					float y6 = position13.y;
					Vector3 position14 = this.GO.transform.position;
					this.climbHelper = (y6 - position14.y) * 4f / this.selectedMoveSpeed;
					if (this.climbHelper < 0f)
					{
						this.climbHelper = 0f;
					}
					this.climbHelper *= 0f / Time.deltaTime;
				}
			}
			if (this.horizontalDistanceFromMovementTarget > 2f)
			{
				this.targetLocation = this.GO.transform.position + (this.targetLocation - this.GO.transform.position) * (1f / this.distanceFromMovementTarget);
				ref Vector3 val5 = ref this.v3;
				Vector3 position15 = this.GO.transform.position;
				float x4 = position15.x;
				Vector3 position16 = this.movementTarget.transform.position;
				val5.x = x4 - position16.x;
				this.v3.y = 0f;
				ref Vector3 val6 = ref this.v3;
				Vector3 position17 = this.GO.transform.position;
				float z2 = position17.z;
				Vector3 position18 = this.movementTarget.transform.position;
				val6.z = z2 - position18.z;
				this.movementTarget.transform.Translate(this.v3 * this.cap(Time.deltaTime * 8f, 0f, 1f));
			}
			if (!this.pressingAnyMovementKeys && this.distanceFromMovementTarget > 0f && !this.autoWalking)
			{
				this.targetLocation += (this.GO.transform.position - this.targetLocation) * 0.1f;
				if (this.horizontalDistanceFromMovementTarget > 0.05f)
				{
					ref Vector3 val7 = ref this.v3;
					Vector3 position19 = this.GO.transform.position;
					float x5 = position19.x;
					Vector3 position20 = this.movementTarget.transform.position;
					val7.x = x5 - position20.x;
					this.v3.y = 0f;
					ref Vector3 val8 = ref this.v3;
					Vector3 position21 = this.GO.transform.position;
					float z3 = position21.z;
					Vector3 position22 = this.movementTarget.transform.position;
					val8.z = z3 - position22.z;
					this.movementTarget.transform.Translate(this.v3 * this.cap(Time.deltaTime * 8f, 0f, 1f));
				}
				if (!this.midair)
				{
					Rigidbody rigidbody2 = this.movementTarget;
					rigidbody2.velocity *= 0.5f;
				}
			}
			Transform transform = this.GO.transform;
			Vector3 localEulerAngles2 = this.GO.transform.localEulerAngles;
			float xAngle = 0f - localEulerAngles2.x;
			Vector3 localEulerAngles3 = this.GO.transform.localEulerAngles;
			transform.Rotate(xAngle, 0f, 0f - localEulerAngles3.z);
			this.moving = true;
			goto IL_13ab;
		}
		this.moveSpeed = 0f;
		if (!this.moving)
		{
			goto IL_136e;
		}
		goto IL_136e;
		IL_136e:
		this.moving = false;
		Rigidbody rigidbody3 = this.movementTarget;
		rigidbody3.velocity *= 0f;
		this.movementTarget.position = this.GO.transform.position;
		goto IL_13ab;
	}

	public void moveGO(Vector3 amount)
	{
		if (this.initted)
		{
			if ((UnityEngine.Object)this.GOrigidBody == (UnityEngine.Object)null)
			{
				this.GOrigidBody = this.GO.GetComponent<Rigidbody>();
			}
			Transform transform = this.GO.transform;
			transform.position += amount;
		}
	}

	public void processHead()
	{
		float num = 0f;
		if (this.cumInMouth > 0f)
		{
			this.swallowDelay -= Time.deltaTime;
			if (this.swallowDelay <= 0f)
			{
				this.cumInMouth -= 1f;
				this.swallowing = 0.2f;
				this.swallowDelay += 1f;
			}
		}
		else
		{
			this.swallowDelay = 0.5f;
		}
		if (this.swallowing > 0f)
		{
			num += 0.15f;
			this.swallowing -= Time.deltaTime;
		}
		if (this.throatBeingFucked > 0f)
		{
			num += 0.25f;
			this.throatBeingFucked -= Time.deltaTime;
		}
		this.swallowBulge += (num - this.swallowBulge) * this.cap(Time.deltaTime * 10f, 0f, 1f);
		this.neckScale.x = 1f;
		this.neckScale.y = 1f + this.muscle_act * 0.4f;
		this.neckScale.z = 1f + this.muscle_act * 0.1f + this.swallowBulge;
		this.bones.Neck.localScale = this.neckScale;
		this.neckScale.x = 1f / this.neckScale.x;
		this.neckScale.y = 1f / this.neckScale.y;
		this.neckScale.z = 1f / this.neckScale.z;
		this.bones.Neck_inverter.localScale = this.neckScale;
		this.headScale = Vector3.one * (1f + (2f - this.height_act - 1f) * 0.3f) * this.customHeadScale * this.getHeadScaleForSpecies(this.data.headType);
		if (Game.bigHeadCheat)
		{
			this.headScale.x += 2f;
			this.headScale.y += 2f;
			this.headScale.z += 2f;
		}
		this.v3.x = -0.08f;
		this.v3.y = 0f;
		this.v3.z = -0.26f - (this.tongueScaleV.x - 1f) * 0.257f;
		((Component)this.bones.Head).GetComponent<CapsuleCollider>().center = this.v3;
		this.bones.Head.localScale = this.headScale;
		this.mouthPenetrationDrag = this.cap(this.mouthPenetrationDrag, -0.5f, 0.5f);
		this.tongueScaleV.x = this.tongueScale * (1f + this.mouthPenetrationDrag * 1.5f);
		if (this.mouthPenetrationAmount > 0.05f)
		{
			this.tongueScaleV.x *= 1.4f;
		}
		this.tongueScaleV.y = this.tongueScale;
		this.tongueScaleV.z = this.tongueScale;
		this.bones.Tongue0.localScale = this.tongueScaleV;
		this.bones.Tongue0.localRotation = this.originalTongueRotation[0];
		if (this.currentlyUsingMouth)
		{
			this.bones.Tongue0.Rotate(0f, (0f - this.mouthPenetrationAmount) * 11f - 8f, 0f);
		}
		else
		{
			this.bones.Tongue0.Rotate(0f, (0f - this.mouthPenetrationAmount) * 11f, 0f);
		}
		this.bones.Tongue1.localRotation = this.originalTongueRotation[1];
		this.bones.Tongue2.localRotation = this.originalTongueRotation[2];
		if (this.suckLock)
		{
			this.bones.Head.LookAt(this.suckLockTarget, this.bones.Head.up);
			this.bones.Head.Rotate(0f, 190f, 0f);
			this.suckLock = false;
		}
		this.throatHoleAfterIKandSuckLock.transform.position = this.throatHole.transform.position;
		this.throatHoleAfterIKandSuckLock.transform.rotation = this.throatHole.transform.rotation;
	}

	public void suckDickLockOn(Vector3 shaftBase, RackCharacter suckCharacter, float bump = 0f)
	{
		this.suckLockCharacter = suckCharacter;
		this.suckLockTarget = shaftBase;
		this.suckLock = true;
		this.suckLickBump += bump;
	}

	public void rollRoot(float amount = 0f)
	{
		this.rootRollTar += amount;
		this.rollingRoot = true;
	}

	public void processBody()
	{
		if (float.IsNaN(this.height_act))
		{
			this.height_act = 1f;
		}
		this.newHeight = false;
		this.GO.transform.localScale = Vector3.one * this.height_act;
		if (this.height_act != this.lastHeight)
		{
			this.changingFromHeight = this.lastHeight;
			this.newHeight = true;
			this.lastHeight = this.height_act;
		}
		this.GOHandTargetL.position = this.bones.Hand_L.position;
		this.GOHandTargetL.rotation = this.bones.Hand_L.rotation;
		this.GOHandTargetR.position = this.bones.Hand_R.position;
		this.GOHandTargetR.rotation = this.bones.Hand_R.rotation;
		if (!((UnityEngine.Object)this.apparatus != (UnityEngine.Object)null) && !this.hasFootTargets)
		{
			this.defaultLeftFootTarget.position = this.bones.Footpad_L.position;
			this.defaultLeftFootTarget.rotation = this.bones.Footpad_L.rotation;
			this.defaultRightFootTarget.position = this.bones.Footpad_R.position;
			this.defaultRightFootTarget.rotation = this.bones.Footpad_R.rotation;
			if (this.currentlyUsingPenis || this.currentlyUsingButt || this.currentlyUsingVagina)
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.rotationWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight = 1f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.rotationWeight = 1f;
			}
			else
			{
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.positionWeight = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.leftFootEffector.rotationWeight = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.positionWeight = 0f;
				this.GO.GetComponent<FullBodyBipedIK>().solver.rightFootEffector.rotationWeight = 0f;
			}
		}
		if (!this.currentlyUsingMouth)
		{
			if (this.currentlyUsingHandL)
			{
				this.GOElbowTargetL.position = this.bones.LowerArm_L.position - this.right();
				this.GOElbowTargetL.rotation = this.bones.LowerArm_L.rotation;
			}
			else
			{
				this.GOElbowTargetL.position = this.bones.LowerArm_L.position;
				this.GOElbowTargetL.rotation = this.bones.LowerArm_L.rotation;
			}
		}
		if (!this.currentlyUsingMouth)
		{
			if (this.currentlyUsingHandR || this.usingSexToy)
			{
				this.GOElbowTargetR.position = this.bones.LowerArm_R.position + this.right();
				this.GOElbowTargetR.rotation = this.bones.LowerArm_R.rotation;
			}
			else
			{
				this.GOElbowTargetR.position = this.bones.LowerArm_R.position;
				this.GOElbowTargetR.rotation = this.bones.LowerArm_R.rotation;
			}
		}
		this.shoulderRotationBeforeIKL = this.bones.Shoulder_L.localRotation;
		this.shoulderRotationBeforeIKR = this.bones.Shoulder_R.localRotation;
		float d = (-0.021f + 0.091f * this.bodyMass_act + 0.369f * this.belly_act + 0.168f * this.adiposity_act) * (this.height_act / 1.05f);
		if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Showering0"))
		{
			Transform gOHandTargetL = this.GOHandTargetL;
			gOHandTargetL.position -= this.bones.Root.forward * d;
			Transform gOHandTargetL2 = this.GOHandTargetL;
			gOHandTargetL2.position += this.bones.Root.right * this.belly_act * this.height_act * 0.1f;
		}
		if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Showering0_mirror"))
		{
			Transform gOHandTargetR = this.GOHandTargetR;
			gOHandTargetR.position -= this.bones.Root.forward * d;
			Transform gOHandTargetR2 = this.GOHandTargetR;
			gOHandTargetR2.position += this.bones.Root.right * this.belly_act * this.height_act * 0.1f;
		}
		this.v32 = this.bones.Hand_L.position;
		this.v3 = this.shoulderLstartPos;
		this.v3.y += (this.shoulderWidth_act - 1f) * 0.05f;
		this.bones.Shoulder_L.localPosition = this.v3;
		this.v3 = this.shoulderRstartPos;
		this.v3.y -= (this.shoulderWidth_act - 1f) * 0.05f;
		this.bones.Shoulder_R.localPosition = this.v3;
		this.bones.Hip_L.Rotate(this.rootRoll * 2f, 0f, 0f);
		this.bones.Hip_R.Rotate(this.rootRoll * -2f, 0f, 0f);
		if (!this.rollingRoot)
		{
			this.rootRoll -= this.rootRoll * this.cap(Time.deltaTime * 12f, 0f, 1f);
		}
		else
		{
			this.rootRoll += (this.rootRollTar - this.rootRoll) * this.cap(Time.deltaTime * 4f, 0f, 1f);
			this.rootRollTar = 0f;
			this.rollingRoot = false;
		}
		this.bones.Root.Rotate(0f, this.rootGrind, 0f);
		this.bones.SpineLower.Rotate(0f, 0f - this.rootGrind, 0f);
		if (!this.grindingRoot)
		{
			this.rootGrind -= this.rootGrind * this.cap(Time.deltaTime * 8f, 0f, 1f);
		}
		this.grindingRoot = false;
		this.bones.SpineLower.Rotate(0f, this.backArchAmount * 16f + this.backBendAmount * 6f + this.suckLickBump * -1f, 0f);
		this.bones.SpineMiddle.Rotate(0f, this.backArchAmount * -28f + this.backBendAmount * 28f + this.neckBendAmount * 20f + this.suckLickBump * -2f, 0f);
		this.bones.SpineUpper.Rotate(0f, this.backArchAmount * -4f + this.backBendAmount * 4f + this.neckBendAmount * 12f + this.suckLickBump * -4f, 0f);
		if (this.suckLock)
		{
			this.v3 = (this.suckLockCharacter.bones.SpineLower.position - this.bones.Head.position).normalized;
			Transform neck = this.bones.Neck;
			neck.position += this.v3 * this.suckLickBump * 0.15f;
			Transform head = this.bones.Head;
			head.position += this.v3 * this.suckLickBump * 0.3f;
		}
		this.bones.Neck.Rotate(0f, this.suckLickBump * 20f, 0f);
		this.suckLickBump *= 1f - this.cap(Time.deltaTime * 20f, 0f, 1f);
		if (!this.archingBack)
		{
			this.backArchAmount -= this.backArchAmount * this.cap(Time.deltaTime * 4f, 0f, 1f);
		}
		if (!this.bendingBack)
		{
			this.backBendAmount -= this.backBendAmount * this.cap(Time.deltaTime * 8f, 0f, 1f);
		}
		if (!this.bendingNeck)
		{
			this.neckBendAmount -= this.neckBendAmount * this.cap(Time.deltaTime * 8f, 0f, 1f);
		}
		this.archingBack = false;
		this.bendingBack = false;
		this.bendingNeck = false;
	}

	public void bendBack(float amount)
	{
		this.backBendAmount = amount;
		this.bendingBack = true;
	}

	public void bendNeckForBlowjob(float amount)
	{
		this.neckBendAmount = amount;
		this.bendingNeck = true;
	}

	public void archBack(float amount)
	{
		this.backArchAmount = amount;
		this.archingBack = true;
	}

	public void grindRoot(float amount)
	{
		this.rootGrind += (amount - this.rootGrind) * this.cap(Time.deltaTime * 10f, 0f, 1f);
		this.grindingRoot = true;
	}

	public void processBreath()
	{
		if (!this.currentlyUsingMouth)
		{
			this.breathIntensity = 0.5f + this.arousal + this.orgasm + this.anticipation + this.cumIntensity * 4f / this.data.cumVolume;
			if (this.suspendedAnim)
			{
				this.breathIntensity = 2f;
			}
			if (!this.animationPaused)
			{
				this.breathTime += this.breathIntensity * Time.deltaTime;
			}
			for (int i = 0; i < 7; i++)
			{
				this.breath[i] = Mathf.Cos((this.breathTime + (float)i * 0.1f) * 1f) * (1f + (this.breathIntensity - 1f) * 0.1f);
			}
			this.breath[7] = Mathf.Cos((this.breathTime + 1.5f) * 1f) * (1f + (this.breathIntensity - 1f) * 0.1f);
			this.v3.x = 0f;
			this.v3.z = 0f;
			this.v3.y = this.breath[1] * 3f;
			Transform spineLower = this.bones.SpineLower;
			spineLower.localEulerAngles += this.v3;
			this.v3.y = (0f - this.breath[2]) * 3f;
			Transform spineMiddle = this.bones.SpineMiddle;
			spineMiddle.localEulerAngles += this.v3;
			this.v3.y = (0f - this.breath[3]) * 2f;
			Transform spineUpper = this.bones.SpineUpper;
			spineUpper.localEulerAngles += this.v3;
			this.v3.y = this.breath[4] * 2f;
			Transform neck = this.bones.Neck;
			neck.localEulerAngles += this.v3;
			this.v3.y = this.breath[5] * -2f;
			Transform head = this.bones.Head;
			head.localEulerAngles += this.v3;
			this.v3.y = this.breath[7] * this.breathIntensity * 0.6f - this.breathIntensity * 1.7f;
			if (!this.suspendedAnim)
			{
				Transform jaw = this.bones.Jaw;
				jaw.localEulerAngles += this.v3;
			}
			this.v3.x = (this.v3.y = (this.v3.z = 1f + this.breath[6] * 0.05f));
			Transform nose = this.bones.Nose;
			nose.localEulerAngles += this.v3;
			if (this.feetInAir)
			{
				this.v3.y = 0f;
				this.v3.z = 0f;
				this.v3.x = this.breath[1] * 1f;
				Transform hip_L = this.bones.Hip_L;
				hip_L.localEulerAngles += this.v3;
				this.v3.x = (0f - this.breath[1]) * 1f;
				Transform hip_R = this.bones.Hip_R;
				hip_R.localEulerAngles += this.v3;
				this.v3.x = this.breath[2] * 2f;
				Transform upperLeg_L = this.bones.UpperLeg_L;
				upperLeg_L.localEulerAngles += this.v3;
				this.v3.x = (0f - this.breath[2]) * 2f;
				Transform upperLeg_R = this.bones.UpperLeg_R;
				upperLeg_R.localEulerAngles += this.v3;
				this.v3.x = this.breath[3] * -3f;
				Transform lowerLeg_L = this.bones.LowerLeg_L;
				lowerLeg_L.localEulerAngles += this.v3;
				this.v3.x = (0f - this.breath[3]) * -3f;
				Transform lowerLeg_R = this.bones.LowerLeg_R;
				lowerLeg_R.localEulerAngles += this.v3;
				this.v3.x = this.breath[4] * 2f;
				Transform foot_L = this.bones.Foot_L;
				foot_L.localEulerAngles += this.v3;
				this.v3.x = (0f - this.breath[4]) * 2f;
				Transform foot_R = this.bones.Foot_R;
				foot_R.localEulerAngles += this.v3;
			}
		}
	}

	public void processLimitedEyeMovement()
	{
		this.processEyesAndFocus(true);
	}

	public void cumInEye(bool right, float duration)
	{
		if (right)
		{
			if (duration > this.cumInRightEyeTime)
			{
				this.cumInRightEyeTime = duration;
			}
		}
		else if (duration > this.cumInLeftEyeTime)
		{
			this.cumInLeftEyeTime = duration;
		}
	}

	public void processEyesAndFocus(bool eyesOnly = false)
	{
		this.eyeJitterDelay -= Time.deltaTime;
		if (this.eyeJitterDelay <= 0f)
		{
			this.eyeJitterDelay += 0.2f + UnityEngine.Random.value * 0.8f;
			this.eyeJitterX = -4f + UnityEngine.Random.value * 8f;
			this.eyeJitterY = -4f + UnityEngine.Random.value * 8f;
		}
		this.pleasureEyeCheckToggleCooldown -= Time.deltaTime;
		if (this.pleasureEyeCheckToggleCooldown <= 0f)
		{
			this.pleasureEyeCheckToggleCooldown = 0.8f + UnityEngine.Random.value * 9f;
			if ((!(this.pleasure > 0f) || (!(this.orgasming > 0f) && this.lastKnownNumberOfStimulatingInteractions <= 0)) && !this.currentlyUsingMouth && !this.suckLock)
			{
				this.closingEyesInPleasure = false;
				goto IL_0209;
			}
			this.closingEyesInPleasure = !this.closingEyesInPleasure;
			if (UnityEngine.Random.value > 0.85f)
			{
				this.closingEyesInPleasure = true;
			}
			if (this.orgasming > this.currentOrgasmDuration * 0.8f)
			{
				this.closingEyesInPleasure = true;
				this.pleasureEyeCheckToggleCooldown = 4.8f + UnityEngine.Random.value * 5f;
			}
			if (!this.closingEyesInPleasure)
			{
				this.pleasureEyeCheckToggleCooldown *= 0.4f;
				if (this.orgasming > this.currentOrgasmDuration * 0.5f)
				{
					this.pleasureEyeCheckToggleCooldown *= 0.2f;
				}
			}
			else
			{
				this.currentMoanLength = this.pleasureEyeCheckToggleCooldown * (0.6f + UnityEngine.Random.value * 0.3f);
				this.moanIntensity = 0.05f + UnityEngine.Random.value * this.proximityToOrgasm * 0.15f;
				this.moan = this.currentMoanLength;
			}
			this.pleasureEyeOpenAmount = this.cap(-0.3f + UnityEngine.Random.value * 0.4f, 0f, 1f);
		}
		goto IL_0209;
		IL_0209:
		if (this.timeSincePain < 2f)
		{
			this.closingEyesInPleasure = true;
			this.pleasureEyeOpenAmount = 0f;
		}
		if (this.closingEyesInPleasure)
		{
			this.pleasureEyeOpen += (this.pleasureEyeOpenAmount - this.pleasureEyeOpen) * this.cap(Time.deltaTime * 5f, 0f, 1f);
		}
		else
		{
			this.pleasureEyeOpen += (1f - this.pleasureEyeOpen) * this.cap(Time.deltaTime * 5f, 0f, 1f);
		}
		this.blinkDelay -= Time.deltaTime;
		float num = Mathf.Abs(this.blinkDelay / this.blinkSpeed);
		if (num > 1f)
		{
			num = 1f;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		if (this.blinkDelay <= 0f - this.blinkSpeed)
		{
			this.blinkDelay += 0.6f + UnityEngine.Random.value * 4f;
		}
		if (this.uid == this.game.headshotSubjectUID)
		{
			num = 1f;
		}
		this.suspendedAnim = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Suspended");
		if (this.suspendedAnim)
		{
			num = 0f;
		}
		if (this.suspendedAnim)
		{
			this.focusPoint = this.bones.SpineUpper.position + this.bones.SpineUpper.forward * -3f;
			this.effectiveFocusPoint = this.focusPoint;
		}
		else if (this.isPreviewCharacter && (UnityEngine.Object)RacknetMultiplayer.previewCamera != (UnityEngine.Object)null)
		{
			this.focusPoint = RacknetMultiplayer.previewCamera.transform.position;
			this.effectiveFocusPoint = this.focusPoint;
		}
		else if (this.uid == this.game.headshotSubjectUID)
		{
			this.focusPoint = this.game.renderCam.transform.position;
			this.focusPoint += this.bones.Head.up * 0.75f;
			this.effectiveFocusPoint = this.focusPoint;
		}
		else if (this.controlledByPlayer)
		{
			if (this.game.firstPersonMode)
			{
				this.focusPoint = this.game.camTarget_actual;
				this.effectiveFocusPoint = this.focusPoint + this.game.mainCam.transform.forward;
			}
			else
			{
				if (this.moveSpeed < this.moveSpeedThresholdForHeadFocus)
				{
					if (this.interactingWithSelf)
					{
						this.focusPoint = this.bones.Penis4.position + this.forward() * 0.25f;
					}
					else if (this.game.PC().interactionSubject != null)
					{
						this.focusPoint = (this.game.PC().interactionSubject.bones.Eye_L.position + this.game.PC().interactionSubject.bones.Eye_R.position) / 2f;
					}
					else if (Game.gameInstance.curDialogue != string.Empty)
					{
						this.focusPoint = Game.dialoguePartner.bones.Head.position;
						if (!(this.furniturePose == string.Empty) || !(this.boundPose == string.Empty))
						{
							;
						}
					}
					else
					{
						bool flag = false;
						this.v3 = this.GO.transform.InverseTransformPoint(this.game.mainCam.transform.parent.position);
						if (this.v3.z < 1.3f && !eyesOnly)
						{
							this.focusPoint = this.game.mainCam.transform.parent.position + (this.bones.Head.transform.position - this.game.mainCam.transform.parent.position) * 10f;
						}
						else
						{
							this.focusPoint = this.game.mainCam.transform.parent.position;
						}
					}
				}
				else
				{
					this.focusPoint = this.game.mainCam.transform.parent.position;
				}
				this.effectiveFocusPoint += (this.focusPoint + this.focusDistraction - this.effectiveFocusPoint) * 0.5f;
			}
		}
		else
		{
			float num2 = 0.9f;
			if ((UnityEngine.Object)this.npcData != (UnityEngine.Object)null)
			{
				num2 = this.npcData.attentionToPlayer;
			}
			this.distFromPC = (this.game.PC().GO.transform.position - this.GO.transform.position).magnitude;
			if (!this.closingEyesInPleasure)
			{
				this.focusedOnPC -= Time.deltaTime;
			}
			if (this.distFromPC < 6f + num2 * 8f)
			{
				if (this.focusedOnPC <= 0f)
				{
					this.nextPCattention -= Time.deltaTime;
				}
				if (this.nextPCattention <= 0f)
				{
					this.focusedOnPC = num2 * 10f + UnityEngine.Random.value;
					this.nextPCattention = (1f - num2) * 10f + UnityEngine.Random.value;
				}
			}
			else
			{
				this.nextPCattention = UnityEngine.Random.value * (1.1f - num2);
			}
			if (Game.gameInstance.curDialogue != string.Empty && Game.dialoguePartner.uid == this.uid)
			{
				this.focusedOnPC = 3f;
				this.glancingAtPCbody = 3f;
				this.focusDistraction.x = (this.focusDistraction.y = (this.focusDistraction.z = 0f));
				this.distractionTime = 6f;
			}
			if (this.focusedOnPC > 0f)
			{
				if (!this.closingEyesInPleasure)
				{
					this.glancingAtPCbody -= Time.deltaTime;
				}
				if (this.glancingAtPCbody <= 0f)
				{
					this.glancingAtPCbody = 6f + UnityEngine.Random.value * 10f;
					this.glancingAtBreasts = (UnityEngine.Random.value > 0.5f && this.game.PC().data.breastSize > RackCharacter.breastThreshhold);
				}
				if (this.glancingAtPCbody <= 0.8f)
				{
					if (this.glancingAtBreasts)
					{
						this.focusPoint = this.game.PC().bones.SpineUpper.position;
					}
					else
					{
						this.focusPoint = this.game.PC().bones.Root.position;
					}
				}
				else
				{
					if (!this.closingEyesInPleasure)
					{
						this.PCfocusEyeSwitch -= Time.deltaTime;
					}
					if (this.PCfocusEyeSwitch <= 0f)
					{
						this.PCfocusEyeSwitch = 0.05f + UnityEngine.Random.value;
						this.PCfocusRightEye = !this.PCfocusRightEye;
					}
					if (this.PCfocusRightEye)
					{
						this.focusPoint = this.game.PC().bones.Eye_R.position;
					}
					else
					{
						this.focusPoint = this.game.PC().bones.Eye_L.position;
					}
				}
			}
			else
			{
				this.focusPoint = this.bones.Pubic.position;
				if ((UnityEngine.Object)this.npcData != (UnityEngine.Object)null && this.npcData.hasFocusObject)
				{
					this.focusPoint = this.npcData.transform.Find("focus").transform.position;
				}
			}
			this.effectiveFocusPoint += (this.focusPoint + this.focusDistraction - this.effectiveFocusPoint) * 0.5f;
		}
		this.effectiveFocusPoint += this.bones.Root.forward * this.pleasureGasp * Mathf.Cos(Time.time) * 5f;
		this.effectiveFocusPoint += this.bones.Root.up * this.pleasureGasp * Mathf.Sin(Time.time * 0.88f) * 5f;
		if ((UnityEngine.Object)this.apparatus == (UnityEngine.Object)null)
		{
			if (this.distractionTime > 0f)
			{
				if (!this.closingEyesInPleasure)
				{
					this.distractionTime -= Time.deltaTime;
				}
			}
			else if (this.focusDistraction.magnitude == 0f)
			{
				ref Vector3 val = ref this.focusVector;
				float x = this.focusPoint.x;
				Vector3 position = this.bones.Head.transform.position;
				val.x = x - position.x;
				ref Vector3 val2 = ref this.focusVector;
				float y = this.focusPoint.y;
				Vector3 position2 = this.bones.Head.transform.position;
				val2.y = y - position2.y;
				ref Vector3 val3 = ref this.focusVector;
				float z = this.focusPoint.z;
				Vector3 position3 = this.bones.Head.transform.position;
				val3.z = z - position3.z;
				this.focusDist = this.focusVector.magnitude;
				this.focusDistraction += this.bones.Root.forward * (UnityEngine.Random.value - 0.5f) * this.focusDist * 0.45f;
				this.focusDistraction += this.bones.Root.up * (UnityEngine.Random.value - 0.5f) * this.focusDist * 0.45f;
				this.distractionTime = 0.25f + UnityEngine.Random.value * 0.15f;
			}
			else
			{
				this.focusDistraction.x = (this.focusDistraction.y = (this.focusDistraction.z = 0f));
				this.distractionTime = UnityEngine.Random.value * 6f;
			}
		}
		if (!eyesOnly)
		{
			this.headPiece.enabled = (!this.controlledByPlayer || !this.game.firstPersonMode || !((double)this.game.firstPersonAccel > 0.9) || this.game.renderingHeadshot);
			if (this.animationPaused || (this.game.customizingCharacter && this.game.customizeCharacterPage == 4) || (this.game.customizingCharacter && this.game.customizeCharacterPage == 51))
			{
				this.headRot = this.constrain(this.headRot, 0f, 25f, 0f, 0f, 165f, 180f);
				this.bones.Head.localRotation = Quaternion.Euler(this.headRot);
			}
			else
			{
				if (this.moveSpeed < this.moveSpeedThresholdForHeadFocus)
				{
					this.v3 = this.relativeRotation("Head", this.bones.Head);
					if (this.headMovementBlockedByApparatus_back && this.v3.y < 5f)
					{
						this.v3.y = 5f;
					}
					this.bones.Neck.Rotate(0f, 1f * this.v3.y, 0f);
				}
				if (this.moveSpeed < this.moveSpeedThresholdForHeadFocus)
				{
					this.v3 = this.bones.Head.localEulerAngles;
					this.lookAt(this.bones.Head, this.effectiveFocusPoint, 1f, 0f, 90f, 0f, this.up());
					this.headGoal = RackCharacter.degreeDist3(this.headRot, this.bones.Head.localEulerAngles) * 0.8f;
					this.bones.Head.localEulerAngles = this.v3;
					if (this.movingHead > 0f)
					{
						if (this.closingEyesInPleasure)
						{
							this.maxHeadSpeed = 3f;
						}
						else
						{
							this.maxHeadSpeed = 45f;
						}
						if (this.headGoal.magnitude > this.maxHeadSpeed)
						{
							this.headGoal.Normalize();
							this.headGoal.x *= this.maxHeadSpeed;
							this.headGoal.y *= this.maxHeadSpeed;
							this.headGoal.z *= this.maxHeadSpeed;
						}
						this.headRot.x += this.headGoal.x * 0.3f;
						this.headRot.y += this.headGoal.y * 0.3f;
						this.headRot.z += this.headGoal.z * 0.3f;
						this.movingHead -= Time.deltaTime;
					}
					else if (this.headGoal.magnitude > 20f + this.focusDistraction.magnitude)
					{
						this.movingHead = 0.1f;
					}
					if (this.controlledByPlayer && this.game.firstPersonMode)
					{
						if (this.headRot.x > 25f)
						{
							this.GO.transform.Rotate(0f, Time.deltaTime * 250f * this.cap((this.headRot.x - 25f) / 10f, 0f, 1f), 0f);
						}
						if (this.headRot.x < -25f)
						{
							this.GO.transform.Rotate(0f, Time.deltaTime * -250f * this.cap((-25f - this.headRot.x) / 10f, 0f, 1f), 0f);
						}
					}
					this.headRot = this.constrain(this.headRot, 50f, 25f, 15f, 0f, 165f, 180f);
					this.headCockChangeDelay -= Time.deltaTime;
					if (this.headCockChangeDelay <= 0f)
					{
						this.headCockChangeDelay = 0.5f + UnityEngine.Random.value * 6f;
						this.headCockAnimationTarget.x = -10f + UnityEngine.Random.value * 20f;
						this.headCockAnimationTarget.y = -1f + UnityEngine.Random.value * 2f + 10f;
						this.headCockAnimationTarget.z = -8f + UnityEngine.Random.value * 16f;
					}
					if (this.animationPaused || this.suspendedAnim)
					{
						this.headCockAnimationTarget = Vector3.zero;
						this.headCockAnimationTarget.y += 10f;
					}
					this.headCockAnimation += (this.headCockAnimationTarget - this.headCockAnimation) * this.cap(Time.deltaTime * 5f, 0f, 1f);
					this.bones.Head.localRotation = Quaternion.Euler(this.headRot + this.headCockAnimation);
				}
			}
		}
		float num3 = this.humpBumpAmount_act;
		this.neckRockVel += ((num3 - this.lastHeadMovement) * -175f - this.neckRock) * Time.deltaTime * 3f;
		this.lastHeadMovement = this.humpBumpAmount_act;
		this.neckRock += this.neckRockVel * Time.deltaTime * 8f;
		this.neckRockVel -= this.neckRockVel * this.cap(Time.deltaTime * 5f, 0f, 1f);
		this.bones.Head.Rotate(0f, this.neckRock * 0.2f, 0f);
		this.lookAt(this.bones.Eye_L, this.effectiveFocusPoint, 1f, 90f, 0f, 0f, this.bones.Head.up);
		this.lookAt(this.bones.Eye_R, this.effectiveFocusPoint, 1f, 90f, 0f, 0f, this.bones.Head.up);
		this.effectiveEyeJitterX += (this.eyeJitterX - this.effectiveEyeJitterX) * 0.4f;
		this.effectiveEyeJitterY += (this.eyeJitterY - this.effectiveEyeJitterY) * 0.4f;
		this.bones.Eye_L.Rotate(0f, this.effectiveEyeJitterY, this.effectiveEyeJitterX);
		this.bones.Eye_R.Rotate(0f, this.effectiveEyeJitterY, this.effectiveEyeJitterX);
		this.v3 = this.bones.Eye_L.localEulerAngles;
		this.v3 = this.constrain(this.v3, 0f, 20f, 50f, 0f, 270f, 0f);
		if (this.animationPaused || (this.game.customizingCharacter && this.game.customizeCharacterPage == 4) || (this.game.customizingCharacter && this.game.customizeCharacterPage == 51))
		{
			this.v3 = this.constrain(this.v3, 0f, 0f, 0f, 0f, 270f, 0f);
		}
		this.bones.Eye_L.localEulerAngles = this.v3;
		this.v3 = this.bones.Eye_R.localEulerAngles;
		this.v3 = this.constrain(this.v3, 0f, 20f, 50f, 0f, 270f, 0f);
		if (this.animationPaused || (this.game.customizingCharacter && this.game.customizeCharacterPage == 4) || (this.game.customizingCharacter && this.game.customizeCharacterPage == 51))
		{
			this.v3 = this.constrain(this.v3, 0f, 0f, 0f, 0f, 270f, 0f);
		}
		this.bones.Eye_R.localEulerAngles = this.v3;
		this.v3 = Vector3.one * this.pupilScale;
		this.pupilScale += (1f + this.orgasming / this.currentOrgasmDuration * 0.3f + this.headFemininity_act * 0.2f - this.pupilScale) * this.cap(Time.deltaTime * 3f, 0f, 1f);
		this.bones.Pupil_L.localScale = this.v3;
		this.bones.Pupil_R.localScale = this.v3;
		if (this.animationPaused || (this.game.customizingCharacter && this.game.customizeCharacterPage == 4) || (this.game.customizingCharacter && this.game.customizeCharacterPage == 51))
		{
			this.effectiveEyeOpenL = 0f;
			this.effectiveEyeOpenR = 0f;
		}
		else
		{
			this.effectiveEyeOpenL = num * this.pleasureEyeOpen;
			this.effectiveEyeOpenR = num * this.pleasureEyeOpen;
		}
		if (this.cumInRightEyeTime > 0f)
		{
			this.cumInRightEyeTime -= Time.deltaTime;
			this.effectiveEyeOpenR *= 1f - this.cap(this.cumInRightEyeTime * 5f, 0f, 1f);
		}
		if (this.cumInLeftEyeTime > 0f)
		{
			this.cumInLeftEyeTime -= Time.deltaTime;
			this.effectiveEyeOpenL *= 1f - this.cap(this.cumInLeftEyeTime * 5f, 0f, 1f);
		}
		this.v3 = this.bones.UpperEyelid_L.localEulerAngles;
		this.v3.y += Game.degreeDist(this.v3.y, 260f) * (1f - this.effectiveEyeOpenL);
		this.bones.UpperEyelid_L.localEulerAngles = this.v3;
		this.bones.UpperEyelid_L.localPosition = this.originalUpperEyelidPositionL - Vector3.forward * (1f - this.effectiveEyeOpenL) * 0.015f;
		this.v3 = this.bones.UpperEyelid_R.localEulerAngles;
		this.v3.y += Game.degreeDist(this.v3.y, 260f) * (1f - this.effectiveEyeOpenR);
		this.bones.UpperEyelid_R.localEulerAngles = this.v3;
		this.bones.UpperEyelid_R.localPosition = this.originalUpperEyelidPositionR - Vector3.forward * (1f - this.effectiveEyeOpenR) * 0.015f;
		this.v3 = this.bones.LowerEyelid_L.localEulerAngles;
		this.v3.y += Game.degreeDist(this.v3.y, 260f) * (1f - this.effectiveEyeOpenL);
		this.bones.LowerEyelid_L.localEulerAngles = this.v3;
		this.v3 = this.bones.LowerEyelid_R.localEulerAngles;
		this.v3.y += Game.degreeDist(this.v3.y, 260f) * (1f - this.effectiveEyeOpenR);
		this.bones.LowerEyelid_R.localEulerAngles = this.v3;
		this.bones.Eye_L.localScale = Vector3.one * this.cap(this.effectiveEyeOpenL / 0.1f, 0f, 1f);
		this.bones.Eye_R.localScale = Vector3.one * this.cap(this.effectiveEyeOpenR / 0.1f, 0f, 1f);
	}

	public void penetrateMouth(float amount, float drag)
	{
		if (!(amount <= 0f))
		{
			this.mouthPenetrationAmount += (amount - this.mouthPenetrationAmount) * this.cap(Time.deltaTime * 12f, 0f, 1f);
			this.mouthPenetrationDrag += drag;
			this.mouthBeingPenetrated = true;
		}
	}

	public void processFaceAndEmotions()
	{
		if (this.timeSinceBrokeMouthHoldCommand < 100f)
		{
			this.timeSinceBrokeMouthHoldCommand += Time.deltaTime;
		}
		this.mouthPenetrationDrag += (0f - this.mouthPenetrationDrag) * this.cap(Time.deltaTime * 5f, 0f, 1f);
		if (this.mouthBeingPenetrated)
		{
			this.v3 = this.bones.Jaw.localEulerAngles;
			this.v3.y = 246f;
			this.v3.y = Mathf.Lerp(this.v3.y, 246f + -33f / (RackCharacter.getTongueLengthForHeadType(this.data.headType) / 0.35f), this.mouthPenetrationAmount * (1f - this.swallowBulge * 0.9f));
			this.bones.Jaw.localEulerAngles = this.v3;
			this.mouthBeingPenetrated = false;
		}
		else
		{
			this.mouthPenetrationAmount += (0f - this.mouthPenetrationAmount) * this.cap(Time.deltaTime * 12f, 0f, 1f);
			if (this.talkingAnimationTime > 0f)
			{
				this.talkAnimPhraseTime -= Time.deltaTime;
				this.talkAnimPhrase2Time -= Time.deltaTime;
				if (this.talkAnimPhrase2Time <= 0f)
				{
					this.talkAnimPhrase2Time = 0.45f + UnityEngine.Random.value * 0.6f;
					this.talkAnimPhrase2_earL_tar = -0.6f + UnityEngine.Random.value * 1.2f;
					this.talkAnimPhrase2_earR_tar = -0.6f + UnityEngine.Random.value * 1.2f;
					this.talkAnimPhrase2_cheek = -8f + UnityEngine.Random.value * 16f;
					this.talkAnimPhrase2_headX = -8f + UnityEngine.Random.value * 16f;
					this.talkAnimPhrase2_headY = -8f + UnityEngine.Random.value * 16f;
					this.talkAnimPhrase2_headZ = -8f + UnityEngine.Random.value * 16f;
				}
				if (this.talkAnimPhraseTime <= 0f)
				{
					if (this.talkAnimPhraseTime <= -0.05f)
					{
						this.talkAnimPhraseTime = (this.talkAnimPhraseTimeTotal = 0.1f + UnityEngine.Random.value * 0.2f);
						this.talkAnimPhraseOpen = 0.05f + UnityEngine.Random.value * 0.3f;
						this.talkAnimPhrase_mouthCorners = UnityEngine.Random.value * 30f;
						this.talkAnimPhrase2_nose = -15f + UnityEngine.Random.value * 30f;
					}
					this.mouthOpenAmount = 0f;
				}
				else
				{
					this.mouthOpenAmount = Mathf.Lerp(0f, this.talkAnimPhraseOpen, 1f - Math.Abs(this.talkAnimPhraseTime - this.talkAnimPhraseTimeTotal / 2f) / (this.talkAnimPhraseTimeTotal / 2f));
				}
				this.talkingAnimationTime -= Time.deltaTime;
			}
			else
			{
				this.talkAnimPhrase_mouthCorners *= 0.9f;
				this.talkAnimPhrase2_cheek *= 0.9f;
				this.talkAnimPhrase2_headX *= 0.9f;
				this.talkAnimPhrase2_headY *= 0.9f;
				this.talkAnimPhrase2_headZ *= 0.9f;
				this.talkAnimPhrase2_earL_tar *= 0.9f;
				this.talkAnimPhrase2_earR_tar *= 0.9f;
				this.talkAnimPhrase2_nose *= 0.9f;
				this.moan -= Time.deltaTime;
				if (!this.closingEyesInPleasure)
				{
					this.moan -= Time.deltaTime * 9f;
				}
				if (this.moan < 0f)
				{
					this.moan = 0f;
				}
				this.pleasureGasp = (1f - Mathf.Pow(Mathf.Abs(this.moan - this.currentMoanLength * 0.5f) / (this.currentMoanLength * 0.5f), 3f)) * this.moanIntensity;
				this.mouthOpenAmount = this.cap(this.pleasureGasp + this.humpBumpAmount * 0.02f, 0f, 1f);
				if (this.holdMouthOpenTime > 0f)
				{
					if (this.sexToySlots[SexToySlots.MOUTH] != string.Empty)
					{
						this.holdMouthOpenTime = 0f;
					}
					if (this.holdMouthOpenTime > 12f && this.holdMouthOpenTime - Time.deltaTime <= 12f)
					{
						this.think("are_you_gonna_use_my_mouth", 1f, true, false);
					}
					if (this.holdMouthOpenTime > 0f && this.holdMouthOpenTime - Time.deltaTime <= 0f)
					{
						this.think("closing_mouth_now", 1f, true, false);
						this.timeSinceBrokeMouthHoldCommand = 0f;
						this.setCommandStatus("use_your_mouth", -1);
					}
					this.holdMouthOpenTime -= Time.deltaTime;
					this.mouthOpenAmount = 0.4f;
				}
			}
			this.mouthOpenAmount_animated += (this.mouthOpenAmount - this.mouthOpenAmount_animated) * this.cap(Time.deltaTime * 90f, 0f, 1f);
			this.talkAnimPhrase2_earL += (this.talkAnimPhrase2_earL_tar - this.talkAnimPhrase2_earL) * this.cap(Time.deltaTime * 20f, 0f, 1f);
			this.talkAnimPhrase2_earR += (this.talkAnimPhrase2_earR_tar - this.talkAnimPhrase2_earR) * this.cap(Time.deltaTime * 20f, 0f, 1f);
			this.talkAnimPhrase2_nose_act += (this.talkAnimPhrase2_nose - this.talkAnimPhrase2_nose_act) * this.cap(Time.deltaTime * 20f, 0f, 1f);
			this.talkAnimPhrase2_headX_act += (this.talkAnimPhrase2_headX - this.talkAnimPhrase2_headX_act) * this.cap(Time.deltaTime * 2f, 0f, 1f);
			this.talkAnimPhrase2_headY_act += (this.talkAnimPhrase2_headY - this.talkAnimPhrase2_headY_act) * this.cap(Time.deltaTime * 2f, 0f, 1f);
			this.talkAnimPhrase2_headZ_act += (this.talkAnimPhrase2_headZ - this.talkAnimPhrase2_headZ_act) * this.cap(Time.deltaTime * 2f, 0f, 1f);
			this.v3 = this.bones.Jaw.localEulerAngles;
			if (this.talkingAnimationTime > 0f || this.mouthOpenAmount > 0f)
			{
				this.v3.y = 246f;
			}
			if (this.talkingAnimationTime > 0f)
			{
				this.talkAnimPhrase2_jawX += Time.deltaTime * (1f + Mathf.Cos(Time.time));
			}
			this.v3.z += Mathf.Cos(this.talkAnimPhrase2_jawX * 10f) * this.mouthOpenAmount_animated * 10f;
			this.v3.y = Mathf.Lerp(this.v3.y, 246f + -33f / (RackCharacter.getTongueLengthForHeadType(this.data.headType) / 0.35f), this.mouthOpenAmount_animated);
			this.bones.Jaw.localEulerAngles = this.v3;
			this.mouthOpenAmount_act = this.cap((this.v3.x - 246f) / 33f, 0f, 1f);
			this.bones.MouthCornerL.Rotate(0f, this.talkAnimPhrase_mouthCorners / (RackCharacter.getTongueLengthForHeadType(this.data.headType) / 0.35f), 0f);
			this.bones.MouthCornerR.Rotate(0f, this.talkAnimPhrase_mouthCorners / (RackCharacter.getTongueLengthForHeadType(this.data.headType) / 0.35f), 0f);
			if (this.talkingAnimationTime > 0f)
			{
				this.bones.Cheek_L.Rotate(0f, this.talkAnimPhrase2_cheek - this.mouthOpenAmount * 50f, 0f);
				this.bones.Cheek_R.Rotate(0f, this.talkAnimPhrase2_cheek - this.mouthOpenAmount * 50f, 0f);
			}
			this.bones.Head.Rotate(this.talkAnimPhrase2_headX_act, this.talkAnimPhrase2_headY_act, this.talkAnimPhrase2_headZ_act);
			this.bones.Nose.Rotate(this.talkAnimPhrase2_nose_act * 0.8f, this.mouthOpenAmount_act * -13f, 0f);
			this.bones.EyebrowInner_L.Rotate(this.talkAnimPhrase2_nose_act * 0.8f, 0f, 0f);
			this.bones.EyebrowOuter_L.Rotate(this.talkAnimPhrase2_nose_act * 0.8f, 0f, 0f);
			this.bones.EyebrowInner_R.Rotate(this.talkAnimPhrase2_nose_act * 0.8f, 0f, 0f);
			this.bones.EyebrowOuter_R.Rotate(this.talkAnimPhrase2_nose_act * 0.8f, 0f, 0f);
		}
		Transform eyebrowInner_L = this.bones.EyebrowInner_L;
		Vector3 vector = this.relativeRotation("Eye_L", this.bones.Eye_L);
		eyebrowInner_L.Rotate(0f, 0.25f * vector.y - this.swallowBulge * 45f, 0f);
		Transform eyebrowInner_R = this.bones.EyebrowInner_R;
		Vector3 vector2 = this.relativeRotation("Eye_R", this.bones.Eye_R);
		eyebrowInner_R.Rotate(0f, 0.25f * vector2.y - this.swallowBulge * 45f, 0f);
		Transform eyebrowOuter_L = this.bones.EyebrowOuter_L;
		Vector3 vector3 = this.relativeRotation("Eye_L", this.bones.Eye_L);
		eyebrowOuter_L.Rotate(0f, 0.2f * vector3.y - this.swallowBulge * 45f, 0f);
		Transform eyebrowOuter_R = this.bones.EyebrowOuter_R;
		Vector3 vector4 = this.relativeRotation("Eye_R", this.bones.Eye_R);
		eyebrowOuter_R.Rotate(0f, 0.2f * vector4.y - this.swallowBulge * 45f, 0f);
	}

	public void processTail()
	{
		if (!this.rebuilding && this.initted)
		{
			for (int i = 0; i < this.tailbones.Length; i++)
			{
				if (this.tailRigidBodies[i].velocity.magnitude > 100f)
				{
					this.resetTail();
				}
			}
			if ((this.tailbones[0].position - this.bones.SpineLower.position).magnitude > 1.5f)
			{
				this.resetTail();
			}
			for (int j = 0; j < this.tailbones.Length; j++)
			{
				if ((UnityEngine.Object)this.tailRigidBodies[j] != (UnityEngine.Object)null)
				{
					this.tailRigidBodies[j].isKinematic = this.animationPaused;
				}
			}
			if (this.data.tailTuck && (UnityEngine.Object)this.apparatus != (UnityEngine.Object)null && (this.inPain || (this.effectivePersonality == "shy" && this.pleasure <= 0.1f)))
			{
				goto IL_012a;
			}
			if (this.suspendedAnim)
			{
				goto IL_012a;
			}
			this.effectiveTailCurlX += (this.data.tailCurlX - this.effectiveTailCurlX) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			this.effectiveTailCurlY += (this.data.tailCurlY - this.effectiveTailCurlY) * this.cap(Time.deltaTime * 6f, 0f, 1f);
			this.effectiveTailLift += (this.data.tailLift - this.effectiveTailLift) * this.cap(Time.deltaTime * 6f, 0f, 1f);
			goto IL_032e;
		}
		return;
		IL_012a:
		if (this.suspendedAnim)
		{
			this.effectiveTailCurlX += (0.5f - this.effectiveTailCurlX) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			this.effectiveTailCurlY += (0.4f - this.effectiveTailCurlY) * this.cap(Time.deltaTime * 6f, 0f, 1f);
			this.effectiveTailLift += (0.45f - this.effectiveTailLift) * this.cap(Time.deltaTime * 6f, 0f, 1f);
		}
		else
		{
			this.effectiveTailCurlX += (0.5f - this.effectiveTailCurlX) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			this.effectiveTailCurlY += (0.35f - this.effectiveTailCurlY) * this.cap(Time.deltaTime * 6f, 0f, 1f);
			this.effectiveTailLift += (0.4f - this.effectiveTailLift) * this.cap(Time.deltaTime * 6f, 0f, 1f);
		}
		goto IL_032e;
		IL_032e:
		this.determineTailSizeAct();
		if (this.tailsize_act != this.lastTailSize || this.newHeight)
		{
			this.reparentTail();
			for (int k = 0; k < this.tailbones.Length; k++)
			{
				this.tailbones[k].localScale = Vector3.one;
			}
			this.tailbones[0].localScale = Vector3.one * this.tailsize_act;
			for (int l = 0; l < this.tailbones.Length; l++)
			{
				Vector3 localEulerAngles = this.tailbones[l].localEulerAngles;
				this.tailbones[l].localPosition = RackCharacter.originalTailbonePosition[l];
				this.v3 = RackCharacter.tailStartingAngles[l];
				this.tailbones[l].localEulerAngles = this.v3;
				this.tailJoints[l].autoConfigureConnectedAnchor = false;
				float num = this.originalTailCapsuleHeight[l];
				this.tailJoints[l].connectedAnchor = this.tailJoints[l].connectedAnchor;
				this.tailColliders[l].height = num;
				this.v3.x = (0f - num) / 2f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				this.tailColliders[l].center = this.v3;
				this.tailRigidBodies[l].centerOfMass = Vector3.zero;
				this.tailRigidBodies[l].inertiaTensor = Vector3.one;
				this.tailbones[l].localEulerAngles = localEulerAngles;
				this.taperFactor = 1f;
				this.taperFactor += (1f - (float)l / this.cap((float)this.tailLength_act, 1f, 10f) - this.taperFactor) * this.data.tailTaper;
				this.tailColliders[l].radius = (0.06f + 0.18f * Mathf.Abs(this.data.tailThickness - 0.5f)) * this.taperFactor;
			}
			this.unparentTail();
			this.lastTailSize = this.tailsize_act;
		}
		if (this.data.tailFlick && !this.suspendedAnim)
		{
			this.tailFlickIntensity = 1f + this.anticipation * 0.2f + this.aggression * 0.2f;
			if (this.moveSpeed < 0.05f && !this.animationPaused)
			{
				this.tailFlickTime -= Time.deltaTime * this.tailFlickIntensity;
			}
			this.tailFlickPoint += Time.deltaTime * 6f;
			if (this.tailFlickTime <= 0f)
			{
				this.tailFlickPoint = -5f;
				this.tailFlickX = (-1f + UnityEngine.Random.value * 2f) * 1280f;
				this.tailFlickY = UnityEngine.Random.value * 1f * 640f;
				this.tailFlickTime += 7f + UnityEngine.Random.value * 3f;
			}
		}
		for (int m = 0; m < this.tailbones.Length; m++)
		{
			if (m < this.tailLength_act - 1)
			{
				this.tailbones[m].gameObject.layer = 2;
				this.tailColliders[m].enabled = true;
			}
			else
			{
				this.tailbones[m].gameObject.layer = 23;
				this.tailColliders[m].enabled = false;
			}
		}
		for (int num2 = this.tailbones.Length - 1; num2 >= 0; num2--)
		{
			this.tailboneCurlAdjustmentX = 0f;
			this.tailboneCurlAdjustmentY = 0f;
			if (this.data.tailWag && num2 < 3 && !this.suspendedAnim)
			{
				this.wagIntensity_effective = this.wagIntensity;
				if (this.wagIntensity_effective < 0.1f)
				{
					this.wagIntensity_effective = 0f;
				}
				if (this.wagIntensity_effective > 0f && !this.moving && !this.animationPaused)
				{
					this.wagTime += this.wagIntensity_effective * Time.deltaTime * 3f;
					if (this.wagIntensity_effective > 0.8f)
					{
						this.wagIntensity_effective = 0.8f;
					}
					float num3 = this.data.tailStiffness - this.wagIntensity_effective;
					this.tailboneCurlAdjustmentX += 20f * Mathf.Cos(this.wagTime - (float)num2 / (1f + this.wagIntensity_effective * 15f));
				}
				else
				{
					this.wagTime = 0f;
				}
			}
			if (this.data.tailFlick && !this.suspendedAnim)
			{
				this.tailboneCurlAdjustmentX += this.cap(1f - 0.3f * Mathf.Abs((float)num2 - this.tailFlickPoint), 0f, 1f) * this.cap(this.tailFlickX * Mathf.Pow((float)num2, 3f), -75f, 75f);
				this.tailboneCurlAdjustmentY += this.cap(1f - 0.3f * Mathf.Abs((float)num2 - this.tailFlickPoint), 0f, 1f) * this.cap(this.tailFlickY * Mathf.Pow((float)num2, 3f), -75f, 75f);
			}
			this.tailCurlVec3.x = 0f;
			this.tailCurlVec3.y = this.cap((this.effectiveTailCurlY - 0.5f) * -170f * ((float)num2 / 4f) - this.tailboneCurlAdjustmentY, -170f, 170f);
			if (num2 == 0)
			{
				this.tailCurlVec3.y -= (this.effectiveTailLift - 0.5f) * 110f;
			}
			this.tailCurlVec3.z = this.cap((this.effectiveTailCurlX - 0.5f) * 170f * ((float)num2 / 4f) + this.tailboneCurlAdjustmentX, -170f, 170f);
			this.tailCurlQuat = Quaternion.Euler(this.tailCurlVec3);
			this.tailS1Limit.limit = this.cap(Mathf.Abs(this.tailCurlVec3.y), 3f, 180f);
			this.tailS2Limit.limit = this.cap(Mathf.Abs(this.tailCurlVec3.z), 3f, 180f);
			this.tailJoints[num2].targetRotation = this.tailCurlQuat;
			this.tailJoints[num2].angularYLimit = this.tailS1Limit;
			this.tailJoints[num2].angularZLimit = this.tailS2Limit;
			this.tailRigidBodies[num2].mass = Mathf.Lerp(20f, 1f, this.data.tailStiffness) * (0.1f + this.cap(this.tailFlickPoint - (float)num2, 0f, 1f) * 0.9f);
		}
	}

	public void setPenisAngle(Vector3 angleToward, bool entirePenis = false)
	{
		this.penisAngleTarget = angleToward;
		this.penisBeingAngled = true;
		this.anglingEntirePenis = entirePenis;
	}

	public void nudgePenis(float x, float y)
	{
		this.penisNudge.z = x;
		this.penisNudge.y = y;
		this.penisBeingNudged = true;
	}

	public void nudgeClit(float x, float y)
	{
		this.clitNudge.x = x;
		this.clitNudge.y = y;
		this.clitNudge.x = this.cap(this.clitNudge.x, -75f, 75f);
		this.clitNudge.y = this.cap(this.clitNudge.y, -45f, 45f);
		this.clitBeingNudged = true;
	}

	public void processPenis()
	{
		if (!this.showPenis)
		{
			if (this.wasProcessingPenis == 1)
			{
				this.resetPenis();
				((Component)this.PenisBase).GetComponent<Rigidbody>().isKinematic = true;
				for (int i = 0; i < this.penisRigidbodies.Length; i++)
				{
					this.penisRigidbodies[i].isKinematic = true;
				}
			}
			this.wasProcessingPenis = 0;
		}
		else
		{
			if (this.wasProcessingPenis == 0)
			{
				this.resetPenis();
				((Component)this.PenisBase).GetComponent<Rigidbody>().isKinematic = false;
				for (int j = 0; j < this.penisRigidbodies.Length; j++)
				{
					this.penisRigidbodies[j].isKinematic = false;
				}
			}
			this.wasProcessingPenis = 1;
		}
		Vector3 position = this.penisbones[0].position;
		if (float.IsNaN(position.x))
		{
			this.resetPenis();
		}
		Vector3 position2 = this.PenisBase.position;
		if (float.IsNaN(position2.x))
		{
			this.resetPenis();
		}
		for (int k = 0; k < this.penisbones.Length; k++)
		{
			if (this.penisRigidbodies[k].velocity.magnitude > 100f)
			{
				this.resetPenis();
			}
		}
		if ((this.penisbones[2].position - this.bones.Pubic.position).magnitude > 100f)
		{
			this.resetPenis();
		}
		if (!this.anglingEntirePenis)
		{
			this.penisHeadBuried = false;
		}
		if (this.recentlyRemovedPenisFromInteraction > 0f)
		{
			for (int l = 0; l < 5; l++)
			{
				this.penisbones[l].localRotation = RackCharacter.originalPenisRotations[l];
				this.lastPenisRotations[l] = this.penisbones[l].localRotation;
				Rigidbody obj = this.penisRigidbodies[l];
				obj.velocity *= 0f;
				Rigidbody obj2 = this.penisRigidbodies[l];
				obj2.angularVelocity *= 0f;
			}
			this.recentlyRemovedPenisFromInteraction -= Time.deltaTime;
		}
		else
		{
			this.penisKinked = false;
			if (this.penisKinked)
			{
				this.penisKinkFixer += Time.deltaTime * 60f;
				if (this.penisKinkFixer > 30f)
				{
					this.penisKinkFixer = 30f;
				}
				for (int m = 1; m < 5; m++)
				{
					this.penisbones[m].Rotate(0f, 0f - this.penisKinkFixer, 0f);
				}
				this.arousal -= this.penisKinkFixer * Time.deltaTime * 0.03f;
				if (this.arousal < 0f)
				{
					this.arousal = 0f;
				}
			}
			else
			{
				this.penisKinkFixer = 0f;
			}
			int num = 0;
			if (this.data.ballsType == 2)
			{
				num = 1;
			}
			this.penisLengthInWorldUnits += (this.penisLengthInWorldUnits_tar - this.penisLengthInWorldUnits) * this.cap(Time.deltaTime * 10f, 0f, 1f);
			this.arousal = this.cap(this.arousal, 0f, 1f);
			this.timeSincePenisGirthPoll += Time.deltaTime;
			if (this.arousal != this.lastArousal)
			{
				this.pollPenisLength();
			}
			if ((this.currentlyUsingPenis || (UnityEngine.Object)this.apparatus != (UnityEngine.Object)null) && (Mathf.Abs(this.arousal - this.lastArousalPoll) > 0.1f || Mathf.Abs(this.knotSwell - this.lastKnotSwellPoll) > 0.1f) && this.timeSincePenisGirthPoll > 0.5f)
			{
				this.pollPenisGirth(true);
				this.lastArousalPoll = this.arousal;
				this.lastKnotSwellPoll = this.knotSwell;
				this.timeSincePenisGirthPoll = 0f;
			}
			if (this.data.ballsType == 1 || this.data.ballsType == 2)
			{
				this.slitOutAmount = this.cap(this.arousal * 3f, 0f, 1f);
			}
			else
			{
				this.slitOutAmount = 1f;
			}
			if (!this.showPenis || this.crotchCoveredByClothing)
			{
				this.parts[this.penisPieceIndex].gameObject.SetActive(false);
				for (int n = 0; n < 5; n++)
				{
					this.penisbones[n].gameObject.layer = 13;
				}
			}
			else
			{
				this.parts[this.penisPieceIndex].gameObject.SetActive(true);
				for (int num2 = 0; num2 < 5; num2++)
				{
					this.penisbones[num2].gameObject.layer = 13;
					this.penisColliders[num2].enabled = false;
					if (num2 == 4)
					{
						this.bones.Penis4Collider2.enabled = false;
					}
				}
				if (this.data.ballsType == 1)
				{
					this.slitOutAmount = 1f;
				}
				this.cockTwitchIntensity = this.arousal + this.orgasm + this.anticipation * (1f - this.refractory / this.currentRefractoryDuration * 0.5f);
				if (!this.animationPaused)
				{
					this.cockTwitchTime -= Time.deltaTime * (1f + this.cockTwitchIntensity * 0.3f);
				}
				if (this.cockTwitchTime <= 0f)
				{
					this.cockTwitchTime += 2f + UnityEngine.Random.value * 0.3f;
				}
				if (this.orgasming > 0f)
				{
					this.cockTwitch = this.cap(this.orgasmTwitch * 20f, 0f, 1f);
				}
				else
				{
					this.cockTwitch = 1f - Math.Abs(this.cockTwitchTime - 0.2f) / 0.2f;
				}
				if (this.cockTwitch < 0f)
				{
					this.cockTwitch = 0f;
				}
				if (this.cockTwitch > 0f)
				{
					this.cockTwitch = Mathf.Pow(this.cockTwitch, 2f);
				}
				this.penisArousalScale = 0.07f + this.arousal * 0.93f;
				if (this.penisBeingAngled)
				{
					this.penisArousalScale += (1f - this.penisArousalScale) * 0.3f;
				}
				this.penisArousalScale = 1f * this.growerShower_act + this.penisArousalScale * (1f - this.growerShower_act);
				if (this.data.hasKnot)
				{
					this.penisArousalScale = 1f - this.penisArousalScale;
					this.penisArousalScale = Mathf.Pow(this.penisArousalScale, 4f);
					this.penisArousalScale = 1f - this.penisArousalScale;
				}
				for (int num3 = 0; num3 < 5; num3++)
				{
					float num4 = this.cap(0.2f + this.arousal * 0.8f, 0f, 1f);
					if (this.data.hasSheath || this.data.ballsType == 2 || this.penisBeingAngled || this.penisBeingNudged || this.anglingEntirePenis)
					{
						num4 = 1f;
					}
					this.penisSwingSpring.spring = 1f + Mathf.Pow(10f, 2f + num4 * 6f) * 0.5f;
					this.penisJoints[num3].angularYZLimitSpring = this.penisSwingSpring;
					this.penisRigidbodies[num3].isKinematic = this.anglingEntirePenis;
					if (this.anglingEntirePenis)
					{
						this.penisS1Limit.limit = 0f;
						this.penisS2Limit.limit = 0f;
						this.penisSwingSpring.damper = this.penisSwingSpring.spring * 0.1f;
						this.penisJoints[num3].angularYLimit = this.penisS1Limit;
						this.penisJoints[num3].angularZLimit = this.penisS2Limit;
						this.penisJoints[num3].angularYZLimitSpring = this.penisSwingSpring;
					}
					else
					{
						if (num3 == 0)
						{
							this.penisS2Limit.limit = 0f;
							this.penisS1Limit.limit = 3f + this.cap(0.2f - this.arousal - this.moveSpeed, 0f, 1f) * 250f;
						}
						else
						{
							this.penisS1Limit.limit = 3f;
							this.penisS2Limit.limit = 3f;
						}
						this.penisSwingSpring.damper = this.penisSwingSpring.spring * 0.1f;
						this.penisJoints[num3].angularYLimit = this.penisS1Limit;
						this.penisJoints[num3].angularZLimit = this.penisS2Limit;
						this.penisJoints[num3].angularYZLimitSpring = this.penisSwingSpring;
					}
					if (num3 < 4)
					{
						this.v3 = Vector3.one;
						this.v3.y *= this.penisGirth_act;
						this.v3.z *= this.penisGirth_act;
						this.v3.x = this.penisArousalScale;
						if (this.penisBeingStretched)
						{
							this.v3.x *= this.penisStretch;
							this.v3.y /= 1f + this.cap(this.penisStretch - 1f, -0.4f, 0.4f) * 1.2f;
							this.v3.z /= 1f + this.cap(this.penisStretch - 1f, -0.4f, 0.4f) * 1.2f;
							if (num3 == 3)
							{
								this.penisBeingStretched = false;
							}
						}
						if (this.data.penisType == 1 && num3 == 3)
						{
							this.v3.x = 1f;
						}
						if (this.v3.x > 1f)
						{
							this.v3.x = 1f;
						}
						if (this.v3.x < 0.1f)
						{
							this.v3.x = 0.1f;
						}
						this.v3.x += this.penisDrag;
						float num5 = 1f - this.sheathedAmount;
						if (num3 < 3)
						{
							this.v3.x *= 0.7f + (this.penisLength_act - 0.7f) * num5;
						}
						if (this.data.hasKnot && num3 <= 1 && this.v3.x < this.arousal)
						{
							this.v3.x = this.arousal;
						}
						if (this.v3.x < 0.1f)
						{
							this.v3.x = 0.1f;
						}
						if (this.data.hasSheath)
						{
							this.v3 *= 1f / (1f + this.sheathedAmount * ((float)num3 * 0.15f));
						}
						if (this.data.ballsType == 2)
						{
							this.v3 *= 0.01f + this.slitOutAmount * 0.99f;
						}
						if (this.data.penisType == 1 && num3 == 3)
						{
							float num6 = 0f;
							num6 = this.cap((this.foreskinOverlap - 0.4f) * 4f, 0f, 1f) * 0.3f;
							this.v3.y *= 1f - num6;
							this.v3.z *= 1f - num6;
						}
						this.penisbones[num3].localScale = this.v3;
						this.v3.x = 1f / this.v3.x;
						this.v3.y = 1f / this.v3.y;
						this.v3.z = 1f / this.v3.z;
						this.penisinverterbones[num3].localScale = this.v3;
						this.newPenisRotation = this.penisbones[num3].localRotation;
						this.penisbones[num3].localRotation = RackCharacter.originalPenisRotations[num3];
						if (this.slitOutAmount >= 0.95f && !this.anglingEntirePenis)
						{
							if (this.penisBeingAngled && num3 == num)
							{
								this.v32 = this.penisbones[num3].localEulerAngles;
								this.previousPenisRootPosition.transform.LookAt(this.penisAngleTarget, -this.bones.Root.right);
								this.previousPenisRootPosition.transform.Rotate(-90f, 0f, 90f);
								this.penisbones[num3].rotation = this.previousPenisRootPosition.transform.rotation;
								this.penisAngledAmount = this.bones.Pubic.InverseTransformVector(this.bones.Penis1.position - this.bones.Penis0.position).normalized;
								this.penisAngledAmount.y *= -1.3f;
								this.penisAngledAmount.x += 0.6f;
								this.penisAngledAmount.x *= 2.5f;
							}
							else
							{
								this.penisbones[num3].Rotate(0f, this.arousal * this.penisCurveY_act * 17f * this.slitOutAmount + this.cockTwitch * this.cockTwitchIntensity * 0.4f * (float)(num3 + 3) + this.sheathedAmount * 6f - (1f - this.slitOutAmount) * 15f, this.arousal * this.penisCurveX_act * 12f * this.slitOutAmount);
							}
						}
						if (num3 >= num)
						{
							this.penisbones[num3].Rotate(this.penisNudge);
						}
						if (!this.penisBeingNudged)
						{
							this.penisNudge -= this.penisNudge * this.cap(Time.deltaTime * 1.25f, 0f, 1f);
						}
						this.penisBeingNudged = false;
						if (num3 == 0 && this.data.hasSheath)
						{
							this.penisbones[num3].Rotate(0f, this.sheathedAmount * 22f, 0f);
						}
						if (num3 == 0 && this.data.ballsType == 2)
						{
							this.penisbones[num3].Rotate(0f, this.cap(1.3f - this.arousal, 0f, 1f) * -35f, 0f);
						}
						this.penisJoints[num3].autoConfigureConnectedAnchor = false;
						this.v3.y = (this.v3.z = 0f);
						this.v3.x = 0f - this.penisBoneLengths[num3];
						if (num3 == 0 && this.data.ballsType == 2)
						{
							this.v3.x += (1f - this.slitOutAmount) * 0.35f;
							this.v3.z += (1f - this.slitOutAmount) * 0.22f;
						}
						this.penisJoints[num3].connectedAnchor = this.v3;
						this.penisbones[num3].localRotation = this.newPenisRotation;
					}
					if (num3 == 4)
					{
						float num7 = 0f;
						if (this.data.penisType == 1)
						{
							num7 = this.cap((this.foreskinOverlap - 0.4f) * 4f, 0f, 1f) * 0.2f;
						}
						if (this.data.penisType < 2)
						{
							num7 += 0.22f;
						}
						this.v3.x = 1f + this.cockTwitch * this.cockTwitchIntensity * 0.01f;
						this.v3.y = (this.v3.z = this.penisGirth_act * 1f + this.cockTwitch * this.cockTwitchIntensity * 0.01f);
						this.v3 *= 1f - num7;
						if (this.data.hasSheath)
						{
							this.v3 *= 1f / (1f + this.sheathedAmount * ((float)num3 * 0.15f));
						}
						this.penisbones[num3].localScale = this.v3;
					}
				}
				this.v3 = Vector3.one;
				if (this.data.hasKnot || this.data.penisType == 4)
				{
					if ((this.game.customizingCharacter || true) && this.controlledByPlayer)
					{
						this.knotSwell += (this.arousal - this.knotSwell) * this.cap(Time.deltaTime * 5f, 0f, 1f);
					}
					if (this.cap((this.orgasm - 0.2f) / 1.2f, 0f, 1f) > this.knotSwell)
					{
						this.knotSwell += (this.cap((this.orgasm - 0.2f) / 1.2f, 0f, 1f) - this.knotSwell) * this.cap(Time.deltaTime * 0.9f, 0f, 1f);
					}
					if (this.orgasming / this.currentOrgasmDuration * 0.6f > this.knotSwell)
					{
						this.knotSwell += (this.orgasming / this.currentOrgasmDuration * 0.6f - this.knotSwell) * this.cap(Time.deltaTime * 0.9f, 0f, 1f);
					}
					this.v3 *= 1f + this.knotSwell * (1f - this.knotBuryAmount) * (1.15f * this.penisGirth_act);
					if (this.knotSwell > 0f && this.refractory <= 0f && this.proximityToOrgasm < 0.7f && this.anticipation < 0.3f)
					{
						this.knotSwell -= Time.deltaTime * 0.1f;
						if (this.knotSwell < 0f)
						{
							this.knotSwell = 0f;
						}
					}
				}
				if (!this.data.hasKnot)
				{
					this.v3 = Vector3.one;
				}
				this.bones.Knot_L.localScale = this.v3;
				this.bones.Knot_R.localScale = this.v3;
				this.foreskinOverlap = this.cap(0.6f - this.arousal * 0.7f + this.foreskinDrag, 0f, 1f);
			}
			this.penisDrag -= this.penisDrag * this.cap(Time.deltaTime * 2f / (this.penisDragTightness + 0.01f), 0f, 1f);
			if (this.penisDragTightness <= 0f)
			{
				this.foreskinDrag -= this.foreskinDrag * this.cap(Time.deltaTime * 4f, 0f, 1f);
			}
			this.penisDragTightness = 0f;
			this.applyPenisBlends();
			this.lastArousal = this.arousal;
			for (int num8 = 0; num8 < this.penisbones.Length; num8++)
			{
				this.penisColliders[num8].radius = 0.08f;
			}
			switch (this.data.penisType)
			{
			case 0:
			case 1:
				this.v3.x = -0.09f;
				this.v3.y = 0f;
				this.v3.z = 0.01f;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().center = this.v3;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().radius = 0.07f;
				break;
			case 2:
				this.v3.x = -0.11f;
				this.v3.y = 0f;
				this.v3.z = 0.02f;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().center = this.v3;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().radius = 0.06f;
				break;
			case 3:
				this.v3.x = -0.11f;
				this.v3.y = 0f;
				this.v3.z = 0f;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().center = this.v3;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().radius = 0.05f;
				break;
			case 4:
				this.v3.x = -0.07f;
				this.v3.y = 0f;
				this.v3.z = -0.03f;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().center = this.v3;
				((Component)this.penisbones[4]).GetComponent<SphereCollider>().radius = 0.09f;
				break;
			}
			this.penisBeingAngled = false;
			int num9 = 0;
			if (this.anglingEntirePenis)
			{
				float num10 = 1f;
				this.knotBuryAmount = 0f;
				for (int num11 = 0; num11 < 4; num11++)
				{
					if (num11 >= num9)
					{
						float magnitude = this.buryTarDistance;
						if (this.penisBeingBuried)
						{
							magnitude = (this.penisbones[num11].position - this.penisBuryInsidePoint).magnitude;
						}
						if (num11 == 4 && !this.controlledByPlayer)
						{
							Debug.Log(magnitude + " / " + this.buryTarDistance);
						}
						if (magnitude < this.buryTarDistance && num11 > num9)
						{
							if (magnitude > this.buryTarDistance - 0.05f && this.penisBurialOrifice != 2)
							{
								float num12 = (this.buryTarDistance - magnitude) / 0.05f;
								this.penisbones[num11].LookAt(this.penisBuryTarget, -this.bones.Root.right);
								this.penisbones[num11].Rotate(-90f, 0f, 90f);
								this.quat = this.penisbones[num11].rotation;
								this.penisbones[num11].rotation = Quaternion.Slerp(this.penisbones[num9].rotation, this.quat, num12);
								num10 = num12;
							}
							else
							{
								this.penisbones[num11].LookAt(this.penisBuryTarget, -this.bones.Root.right);
								this.penisbones[num11].Rotate(-90f, 0f, 90f);
								num10 = 1f;
							}
							if (num11 <= 3)
							{
								this.penisHeadBuried = true;
								if (this.penisBurialOrifice == 2 && this.dickSucker != null)
								{
									this.dickSucker.throatBeingFucked = 0.05f;
								}
							}
							if (num11 == 1)
							{
								this.knotBuryAmount = num10;
							}
						}
						else
						{
							num10 = 0f;
							if (num11 == num9)
							{
								if (num9 == 1)
								{
									this.penisbones[num11].LookAt(this.penisAngleTarget + (this.penisAngleTarget - this.penisbones[0].position), -this.bones.Root.right);
								}
								else
								{
									this.penisbones[num11].LookAt(this.penisAngleTarget, -this.bones.Root.right);
								}
								this.penisbones[num11].Rotate(-90f, 0f, 90f);
							}
							else
							{
								this.penisbones[num11].rotation = this.penisbones[num9].rotation;
							}
						}
					}
					Transform obj3 = this.penisbones[num11];
					obj3.localScale *= 1f - num10 * this.penisBuryGirthReduction;
				}
				this.penisAngledAmount = this.bones.Pubic.InverseTransformVector(this.bones.Penis1.position - this.bones.Penis0.position).normalized;
				this.penisAngledAmount.y *= -1.3f;
				this.penisAngledAmount.x += 0.6f;
				this.penisAngledAmount.x *= 2.5f;
				this.anglingEntirePenis = false;
			}
			if (this.penisHeadBuried)
			{
				this.timeSinceBuriedPenis = 0f;
			}
			else
			{
				this.timeSinceBuriedPenis += Time.deltaTime;
			}
			if (!this.anglingEntirePenis)
			{
				this.knotBuryAmount = 0f;
			}
			for (int num13 = 0; num13 < 5; num13++)
			{
				this.lastPenisRotations[num13] = this.penisbones[num13].localRotation;
			}
			this.penisBeingBuried = false;
			for (int num14 = 0; num14 < this.penisRigidbodies.Length; num14++)
			{
				this.v3 = this.lastPenisbonePos[num14] - this.penisbones[num14].position;
				if (this.v3.magnitude < 2f)
				{
					this.penisRigidbodies[num14].AddForce(this.v3 * 40f, ForceMode.Impulse);
				}
				this.lastPenisbonePos[num14] = this.penisRigidbodies[num14].position;
			}
			this.v3 = this.lastPubicPosition - this.bones.Pubic.position;
			for (int num15 = 0; num15 < this.penisRigidbodies.Length; num15++)
			{
				if (!(this.moveSpeed > 0.05f) && !(this.jumpY > 0f) && this.v3.magnitude < 2f)
				{
					this.penisRigidbodies[num15].AddForce(this.v3 * 50f * (float)num15, ForceMode.Impulse);
				}
			}
		}
	}

	public void dragPenis(float drag, float tightness = 0.5f)
	{
		this.penisDrag += drag;
		float num = Mathf.Abs(this.foreskinDrag - 0.2f);
		float num2 = Mathf.Abs(this.foreskinDrag + drag - 0.2f);
		if (num2 > num)
		{
			this.foreskinDrag += drag * 2.5f;
		}
		else
		{
			this.foreskinDrag += drag * 5f;
		}
		if (drag < 0f)
		{
			this.foreskinDrag += drag * 2f;
		}
		this.foreskinDrag = this.cap(this.foreskinDrag, 0f, 1f);
		this.penisDragTightness = tightness;
	}

	public void stretchPenis(float amount)
	{
		this.penisStretch = this.cap(amount, 0.8f, 1.1f);
		this.penisBeingStretched = true;
	}

	public void playSound(string sound, float vol = 1f, bool throughHead = false)
	{
		int num = RackCharacter.characterSoundNames.IndexOf(sound);
		if (num == -1)
		{
			RackCharacter.characterSounds.Add(Resources.Load("character_audio" + Game.PathDirectorySeparatorChar + string.Empty + sound) as AudioClip);
			RackCharacter.characterSoundNames.Add(sound);
			num = RackCharacter.characterSounds.Count - 1;
		}
		if (throughHead)
		{
			((Component)this.bones.Head).GetComponent<AudioSource>().PlayOneShot(RackCharacter.characterSounds[num], vol);
		}
		else
		{
			((Component)this.bones.Root).GetComponent<AudioSource>().PlayOneShot(RackCharacter.characterSounds[num], vol);
		}
	}

	public void footstep(int which)
	{
		if (which != this.lastFootstep)
		{
			this.lastFootstep = which;
			string text = "footstep_bare";
			if (false || this.data.specialFoot == "hooved")
			{
				text = "footstep_hard";
			}
			if (this.footstepWeight > 1.5f)
			{
				text += "heavy";
			}
			this.playSound(text + this.nextFootstep, this.moveSpeed, false);
			if ((UnityEngine.Object)this.standingOnSurface != (UnityEngine.Object)null && (UnityEngine.Object)this.standingOnSurface.Find("StepSound") != (UnityEngine.Object)null)
			{
				this.playSound("footstepsurface_" + this.standingOnSurface.Find("StepSound").GetChild(0).name + this.nextFootstepSurface, this.moveSpeed, false);
			}
			this.nextFootstep += Mathf.CeilToInt(UnityEngine.Random.value * 3f);
			this.nextFootstep %= 4;
			this.nextFootstepSurface += Mathf.CeilToInt(UnityEngine.Random.value * 3f);
			this.nextFootstepSurface %= 4;
		}
	}

	public void spreadVagina(bool inner, float amount)
	{
		if (!(amount < 0f))
		{
			if (inner)
			{
				this.vaginaSpreadInner += (amount - this.vaginaSpreadInner) * this.cap(Time.deltaTime * 12f, 0f, 1f);
				this.spreadingVaginaInner = true;
			}
			else
			{
				this.vaginaSpreadOuter += (amount - this.vaginaSpreadOuter) * this.cap(Time.deltaTime * 12f, 0f, 1f);
				this.spreadingVaginaOuter = true;
			}
		}
	}

	public void processVagina()
	{
		if (!this.showVagina || this.crotchCoveredByClothing)
		{
			this.parts[this.vaginaPieceIndex].gameObject.SetActive(false);
		}
		else
		{
			this.wetness_vagina += (this.cap(this.orgasm * 2f + this.arousal * 0.5f + this.orgasming / this.currentOrgasmDuration, 0f, 2f) - this.wetness_vagina) * this.cap(Time.deltaTime * 0.5f, 0f, 1f);
			this.parts[this.vaginaPieceIndex].gameObject.SetActive(true);
			this.v3 = this.originalVaginaPosition;
			this.v3.z -= this.cap(this.vaginaDrag + this.vaginaPush, -0.06f, 0f);
			this.v3.x -= this.cap(this.vaginaDrag + this.vaginaPush, -0.06f, 0f);
			this.vaginaDrag -= this.vaginaDrag * this.cap(Time.deltaTime * 15f, 0f, 1f);
			this.vaginaGape -= this.vaginaGape * this.cap(Time.deltaTime * 20f, 0f, 1f) * (this.currentVaginalTightness * this.data.vaginalTightness);
			if (!this.vaginaBeingPushed)
			{
				this.vaginaPush -= this.vaginaPush * this.cap(Time.deltaTime * 10f, 0f, 1f);
			}
			this.vaginaBeingPushed = false;
			this.vaginaContainer.transform.localPosition = this.v3;
			if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Showering1Spread"))
			{
				this.spreadVagina(false, 1f);
			}
			if (!this.spreadingVaginaInner)
			{
				this.vaginaSpreadInner -= this.vaginaSpreadInner * this.cap(Time.deltaTime * 10f, 0f, 1f);
			}
			if (!this.spreadingVaginaOuter)
			{
				this.vaginaSpreadOuter -= this.vaginaSpreadOuter * this.cap(Time.deltaTime * 9f, 0f, 1f);
			}
			this.vaginaSpreadInner_act = this.cap(this.vaginaSpreadInner, 0f, 1f);
			this.vaginaSpreadOuter_act = this.cap(this.vaginaSpreadOuter, 0f, 1f);
			this.extraVaginaSpread = this.cap(this.vaginaSpreadOuter - 1f, 0f, 10f);
			for (int i = 0; i < 8; i++)
			{
				this.v3 = this.originalVaginaRotations[i];
				this.v32 = this.originalVaginaPositions[i];
				if (i < 4)
				{
					if (i % 2 == 0)
					{
						this.v3.z -= this.vaginaSpreadOuter_act * 35f;
						if (i < 2)
						{
							this.v3.x += this.vaginaSpreadOuter_act * 30f;
						}
					}
					else
					{
						this.v3.z += this.vaginaSpreadOuter_act * 35f;
						if (i < 2)
						{
							this.v3.x -= this.vaginaSpreadOuter_act * 30f;
						}
					}
				}
				else
				{
					switch (i)
					{
					case 7:
						this.v3.x += this.vaginaSpreadInner_act * 65f * 0.35f * 0.4f;
						this.v32.y -= this.vaginaSpreadInner_act * 0.04f;
						break;
					case 6:
						this.v3.x -= this.vaginaSpreadInner_act * 65f * 0.35f * 0.4f;
						this.v32.y += this.vaginaSpreadInner_act * 0.04f;
						break;
					case 5:
						this.v3.z += this.vaginaSpreadInner_act * 250f * 0.35f * 0.4f;
						this.v32.y -= this.vaginaSpreadInner_act * 0.04f;
						this.v32.x -= this.vaginaSpreadInner_act * 1f * 0.04f;
						this.v32.z += this.vaginaSpreadInner_act * 0.35f * 0.04f;
						break;
					default:
						this.v3.z -= this.vaginaSpreadInner_act * 250f * 0.35f * 0.4f;
						this.v32.y += this.vaginaSpreadInner_act * 0.04f;
						this.v32.x -= this.vaginaSpreadInner_act * 1f * 0.04f;
						this.v32.z += this.vaginaSpreadInner_act * 0.35f * 0.04f;
						break;
					}
				}
				this.vaginabones[i].localEulerAngles = this.v3;
				this.vaginabones[i].localPosition = this.v32;
			}
			this.spreadingVaginaInner = false;
			this.spreadingVaginaOuter = false;
			this.bones.Clit.rotation = this.originalClitRotation;
			this.bones.Clit.Rotate(this.clitNudge * 0.3f);
			this.bones.Clit.Rotate(0f, this.vaginaPush * 90f, 0f);
			if (!this.clitBeingNudged)
			{
				this.clitNudge -= this.clitNudge * this.cap(Time.deltaTime * 3f, 0f, 1f);
			}
			this.clitBeingNudged = false;
			this.v3 = Vector3.one * this.clitSize_act;
			this.bones.Clit.localScale = this.v3;
			if (!this.vaginaBeingPenetrated)
			{
				this.vaginaPenetrationSideAmount -= this.vaginaPenetrationSideAmount * this.cap(Time.deltaTime * 10f, 0f, 1f);
			}
			this.vaginaBeingPenetrated = false;
		}
	}

	public void processBalls()
	{
		if (!this.showBalls)
		{
			if (this.wasProcessingBalls == 1)
			{
				this.resetBalls();
				((Component)this.bones.Ballsack0).GetComponent<Rigidbody>().isKinematic = true;
				for (int i = 0; i < this.ballRigidbodies.Length; i++)
				{
					this.ballRigidbodies[i].isKinematic = true;
				}
			}
			this.wasProcessingBalls = 0;
		}
		else
		{
			if (this.wasProcessingBalls == 0)
			{
				this.resetBalls();
				((Component)this.bones.Ballsack0).GetComponent<Rigidbody>().isKinematic = false;
				for (int j = 0; j < this.ballRigidbodies.Length; j++)
				{
					this.ballRigidbodies[j].isKinematic = false;
				}
			}
			this.wasProcessingBalls = 1;
		}
		if (float.IsNaN(this.ballbones[0].position.x))
		{
			this.resetBalls();
		}
		Vector3 vector;
		for (int k = 0; k < this.ballbones.Length; k++)
		{
			vector = this.ballRigidbodies[k].velocity;
			if (vector.magnitude > 100f)
			{
				this.resetBalls();
			}
		}
		vector = this.ballbones[2].position - this.bones.Pubic.position;
		if (vector.magnitude > 1f)
		{
			this.resetBalls();
		}
		this.v3 = this.bones.Penis0.localScale;
		if (this.data.hasSheath || this.data.genitalType == 3)
		{
			this.v3.x = 1f;
			if (this.v3.y < 1f)
			{
				this.v3.y = 1f;
			}
			this.bones.Sheath.rotation = Quaternion.Lerp(this.bones.Penis0.rotation, this.bones.Penis1.rotation, 0.5f);
		}
		else
		{
			this.bones.Sheath.rotation = this.bones.Penis0.rotation;
		}
		this.bones.Sheath.localScale = this.v3;
		this.bones.Sheath.position = this.bones.Penis0.position;
		vector = this.bones.LowerLeg_L.position - this.bones.LowerLeg_R.position;
		float num = this.cap((1.4f - vector.magnitude / this.GO.transform.localScale.x) / 0.94f, 0f, 1f);
		num = 1f - Mathf.Pow(1f - num, 2f);
		num += this.moveSpeed;
		this.v3.x = 0.49f - 0.03f * num;
		this.v3.y = 0f;
		this.v3.z = -0.0399f;
		this.BallCatcher.localPosition = this.v3;
		if (this.showBalls && !this.crotchCoveredByClothing)
		{
			this.applyBallBlends();
		}
		if (this.data.hasSheath)
		{
			this.ballDragDownAmount = 0f;
			this.ballUpsideDownRetract = 0f;
			this.ballDragUpAmount = 0f;
		}
		else
		{
			vector = this.BallsackOrigin.InverseTransformPoint(this.bones.Ballsack1.position);
			this.ballDragDownAmount = 0f - vector.normalized.y;
			this.ballUpsideDownRetract = this.cap((0f - this.ballDragDownAmount) * 1.5f, 0f, 1f);
			this.ballDragDownAmount = this.cap(this.ballDragDownAmount, 0f, 1f);
			this.ballDragUpAmount = this.ballDragDownAmount * (0f - this.arousal);
			this.ballDragDownAmount += this.ballDragUpAmount;
		}
		if (this.curSexPoseName == "riding" && this.showBalls)
		{
			this.ballRetract += (this.cap(this.interactionMY * 1.4f, 0f, 1f) - this.ballRetract) * this.cap(Time.deltaTime * 9f, 0f, 1f);
		}
		this.ballDragUpAmount -= this.ballRetract;
		if (!this.showBalls || this.crotchCoveredByClothing)
		{
			this.parts[this.ballsPieceIndex].gameObject.SetActive(false);
			for (int l = 0; l < this.ballbones.Length; l++)
			{
				this.ballbones[l].gameObject.layer = 13;
			}
			if (this.data.genitalType == 3)
			{
				this.bones.Sheath.localEulerAngles = this.bones.Penis0.localEulerAngles + this.bones.Penis1.localEulerAngles;
				this.bones.Sheath.localPosition = this.bones.Penis0.localPosition;
			}
			for (int m = 0; m < this.ballbones.Length; m++)
			{
				this.ballbones[m].localEulerAngles = RackCharacter.originalBallAngles[m];
			}
		}
		else
		{
			for (int n = 0; n < this.ballbones.Length; n++)
			{
				this.ballbones[n].gameObject.layer = 2;
			}
			this.parts[this.ballsPieceIndex].gameObject.SetActive(true);
			if (this.data.genitalType != 2)
			{
				this.ballsRetracting -= Time.deltaTime;
				if (this.ballsRetracting <= 0f)
				{
					this.ballRetract -= this.ballRetract * this.cap(Time.deltaTime * 4f, 0f, 1f);
				}
				this.ballRetract = this.cap(this.ballRetract, 0f, 1f);
				float num2 = this.ballSize_act;
				float num3 = Mathf.Pow(this.scrotumLength_act, 2f) * (1f - this.ballRetract * 0.8f);
				this.v3 = Vector3.one * num2;
				if (UserSettings.data.mod_ballsMoveOnCum != 0f)
				{
					num3 *= UserSettings.data.mod_ballsMoveOnCum * (1f - this.orgasmSoftPulse * 0.4f / (1f + (this.data.cumSpurtFrequency - 1f) * 0.25f));
				}
				this.ballbones[1].localScale = this.v3;
				this.ballbones[2].localScale = this.v3;
				if (this.ballStickCheckDelay <= 0)
				{
					this.invertedBallZ = this.bones.Root.InverseTransformPoint(this.bones.Ballsack1.position).z;
					this.ballStickCheckDelay += 10;
				}
				else
				{
					this.ballStickCheckDelay--;
				}
				if (this.invertedBallZ > -0.15f)
				{
					this.unstickingBalls += Time.deltaTime * 30f;
				}
				else
				{
					this.unstickingBalls -= Time.deltaTime * 5f;
				}
				this.unstickingBalls = this.cap(this.unstickingBalls, 0f, 1f);
				for (int num4 = 0; num4 < this.ballColliders.Length; num4++)
				{
					this.ballColliders[num4].radius = 0.06f * num2 * (1f - this.unstickingBalls);
				}
				for (int num5 = 0; num5 < this.ballJoints.Length; num5++)
				{
					if (num5 == 0 || num5 == 4)
					{
						this.ballJoints[num5].connectedAnchor = RackCharacter.originalBallAnchors[num5] * num3;
					}
					else
					{
						this.ballJoints[num5].connectedAnchor = RackCharacter.originalBallAnchors[num5];
					}
				}
				this.v3 = this.lastPubicPosition - this.bones.Pubic.position + this.amountMovedHorizontal;
				for (int num6 = 1; num6 < this.ballRigidbodies.Length; num6++)
				{
					if (this.moveSpeed <= 0.05f && this.jumpY <= 0f && this.v3.magnitude < 2f)
					{
						this.ballRigidbodies[num6].AddForce(this.v3 * 200f, ForceMode.Impulse);
					}
				}
			}
		}
	}

	public void pushAnus(float amount)
	{
		this.anusPush = this.cap(amount, -0.12f, 0f);
		this.hurt(Mathf.Abs(this.anusPush) * this.currentTailholeTightness * this.data.tailholeTightness * 10f, false);
		this.anusBeingPushed = true;
	}

	public void penetrateAnus(float drag, float girth)
	{
		this.anusPenetratedByGirth = girth;
		if (this.anusGape < girth)
		{
			this.anusGape = girth;
		}
		this.anusDrag += drag * girth * 0.3f * this.cap(Time.deltaTime * 30f, 0f, 1f);
		this.hurt(girth / this.height_act * this.currentTailholeTightness * this.data.tailholeTightness * 3f * Mathf.Abs(drag) * 4.5f, false);
	}

	public void pushVagina(float amount)
	{
		this.vaginaPush = this.cap(amount, -0.12f, 0f);
		this.hurt(Mathf.Abs(this.vaginaPush) * this.currentVaginalTightness * this.data.vaginalTightness * 10f, false);
		this.vaginaBeingPushed = true;
	}

	public void penetrateVagina(float drag, float girth, Vector3 penetrationSourcePoint)
	{
		this.vaginaPenetratedByGirth = girth;
		if (this.vaginaGape < girth)
		{
			this.vaginaGape = girth;
		}
		this.vaginaDrag += drag * girth * 0.3f * this.cap(Time.deltaTime * 30f, 0f, 1f);
		this.hurt(girth / this.height_act * this.currentVaginalTightness * this.data.vaginalTightness * 3f * Mathf.Abs(drag) * 4.5f, false);
		this.v3 = this.bones.Root.InverseTransformPoint(penetrationSourcePoint);
		this.vaginaPenetrationSideAmount = this.v3.y / (0.001f + this.v3.magnitude);
		this.vaginaBeingPenetrated = true;
	}

	public void processAnus()
	{
		this.v3 = RackCharacter.originalAnusPosition;
		this.v3.z += this.anusDrag + this.anusPush;
		this.v3.x += this.anusDrag + this.anusPush;
		this.anusDrag -= this.anusDrag * this.cap(Time.deltaTime * 15f, 0f, 1f);
		this.anusGape -= this.anusGape * this.cap(Time.deltaTime * 20f, 0f, 1f) * (this.currentTailholeTightness * this.data.tailholeTightness);
		if (!this.anusBeingPushed)
		{
			this.anusPush -= this.anusPush * this.cap(Time.deltaTime * 10f, 0f, 1f);
		}
		this.anusBeingPushed = false;
		this.bones.Asshole.localPosition = this.v3;
		this.bones.AssholeSide_L.localPosition = RackCharacter.originalAnusLeftPosition;
		this.bones.AssholeSide_R.localPosition = RackCharacter.originalAnusRightPosition;
		this.bones.AssholeSide_L.localRotation = RackCharacter.originalAnusLeftRotation;
		this.bones.AssholeSide_R.localRotation = RackCharacter.originalAnusRightRotation;
		Transform assholeSide_L = this.bones.AssholeSide_L;
		assholeSide_L.position -= this.right() * this.anusGape * 0.4f;
		Transform assholeSide_R = this.bones.AssholeSide_R;
		assholeSide_R.position += this.right() * this.anusGape * 0.4f;
		this.bones.AssholeSide_L.Rotate(0f, 0f, this.cap(this.anusGape * 100f, 0f, 35f));
		this.bones.AssholeSide_R.Rotate(0f, 0f, 0f - this.cap(this.anusGape * 100f, 0f, 35f));
		this.bones.AssholeBottom.localPosition = RackCharacter.originalAnusBottomPosition;
		this.bones.AssholeTop.localPosition = RackCharacter.originalAnusTopPosition;
		this.bones.AssholeBottom.Translate(0f, 0f, 0f - this.cap((this.anusGape - 0.02f) * 0.2f, 0f, 1f), this.bones.AssholeBottom.transform);
		this.bones.AssholeTop.Translate(0f, 0f, this.cap((this.anusGape - 0.02f) * 0.2f, 0f, 1f), this.bones.AssholeTop.transform);
		this.anusBeingPenetrated = (this.anusPenetratedByGirth * 1.25f > this.anusGape);
		this.anusPenetratedByGirth -= this.anusPenetratedByGirth * this.cap(Time.deltaTime * 5f, 0f, 1f);
		if (this.anusPenetratedByGirth < 0.01f)
		{
			this.anusPenetratedByGirth = 0f;
		}
	}

	public void processButt()
	{
		for (int i = 0; i < 2; i++)
		{
			if ((this.buttbones[i].position - this.bones.Root.position).magnitude > 2f)
			{
				this.resetButt();
			}
		}
		for (int j = 0; j < 2; j++)
		{
			this.v3.x = -0.06f - 0.07f * this.buttSize_act;
			this.v3.y = -0.15f;
			this.v3.z = -0.03f - 0.07f * this.adiposity_act + 0.03f * this.hipWidth_act + 0.06f * this.buttSize_act;
			float num = 0.25f + 0.04f * this.adiposity_act + 0.04f * this.hipWidth_act;
			if (this.bodyMass_act > 0.5f)
			{
				this.v3.z += (this.bodyMass_act - 0.5f) * -0.06f;
				num += (this.bodyMass_act - 0.5f) * 0.14f;
			}
			else
			{
				this.v3.z += (0.5f - this.bodyMass_act) * -0.06f;
				num += (0.5f - this.bodyMass_act) * -0.04f;
			}
			((Component)this.buttbones[j]).GetComponent<SphereCollider>().radius = num;
			((Component)this.buttbones[j]).GetComponent<SphereCollider>().center = this.v3;
		}
		for (int k = 0; k < this.buttRigidbodies.Length; k++)
		{
			if (k == 0)
			{
				this.v3 = this.lastHipPosition[k] - this.bones.Hip_L.position;
			}
			else
			{
				this.v3 = this.lastHipPosition[k] - this.bones.Hip_R.position;
			}
			if (this.v3.magnitude < 2f)
			{
				this.buttRigidbodies[k].AddForce(this.v3 * 200f, ForceMode.Impulse);
			}
			if (k == 0)
			{
				this.lastHipPosition[k] = this.bones.Hip_L.position;
			}
			else
			{
				this.lastHipPosition[k] = this.bones.Hip_R.position;
			}
		}
	}

	public Vector3 gravity(Transform bone)
	{
		ref Vector3 val = ref this.gravity_vec;
		Vector3 position = bone.position;
		val.x = position.x;
		ref Vector3 val2 = ref this.gravity_vec;
		Vector3 position2 = bone.position;
		val2.y = position2.y + 1000f;
		ref Vector3 val3 = ref this.gravity_vec;
		Vector3 position3 = bone.position;
		val3.z = position3.z;
		return this.gravity_vec;
	}

	public Vector3 relativeRotation(string boneName, Transform bone)
	{
		this.relativeRotation_diff = bone.localRotation.eulerAngles - this.startRot[boneName];
		if (this.relativeRotation_diff.x > 180f)
		{
			this.relativeRotation_diff.x -= 360f;
		}
		if (this.relativeRotation_diff.y > 180f)
		{
			this.relativeRotation_diff.y -= 360f;
		}
		if (this.relativeRotation_diff.z > 180f)
		{
			this.relativeRotation_diff.z -= 360f;
		}
		if (this.relativeRotation_diff.x < -180f)
		{
			this.relativeRotation_diff.x += 360f;
		}
		if (this.relativeRotation_diff.y < -180f)
		{
			this.relativeRotation_diff.y += 360f;
		}
		if (this.relativeRotation_diff.z < -180f)
		{
			this.relativeRotation_diff.z += 360f;
		}
		return this.relativeRotation_diff;
	}

	public Vector3 angleTo(Transform bone, Vector3 target, Vector3 startingRot, float amount = 1f, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f)
	{
		this.lookAt(bone, target, amount, offsetX, offsetY, offsetZ, null);
		this.angleTo_diff = bone.localEulerAngles - startingRot;
		if (this.angleTo_diff.x > 180f)
		{
			this.angleTo_diff.x -= 360f;
		}
		if (this.angleTo_diff.y > 180f)
		{
			this.angleTo_diff.y -= 360f;
		}
		if (this.angleTo_diff.z > 180f)
		{
			this.angleTo_diff.z -= 360f;
		}
		if (this.angleTo_diff.x < -180f)
		{
			this.angleTo_diff.x += 360f;
		}
		if (this.angleTo_diff.y < -180f)
		{
			this.angleTo_diff.y += 360f;
		}
		if (this.angleTo_diff.z < -180f)
		{
			this.angleTo_diff.z += 360f;
		}
		return this.angleTo_diff;
	}

	public Vector3 constrain(Vector3 vec, float x, float y, float z, float xOrigin = 0f, float yOrigin = 0f, float zOrigin = 0f)
	{
		float num = Game.degreeDist(vec.x, xOrigin);
		if (num > x)
		{
			vec.x = xOrigin - x;
		}
		if (num < 0f - x)
		{
			vec.x = xOrigin + x;
		}
		num = Game.degreeDist(vec.y, yOrigin);
		if (num > y)
		{
			vec.y = yOrigin - y;
		}
		if (num < 0f - y)
		{
			vec.y = yOrigin + y;
		}
		num = Game.degreeDist(vec.z, zOrigin);
		if (num > z)
		{
			vec.z = zOrigin - z;
		}
		if (num < 0f - z)
		{
			vec.z = zOrigin + z;
		}
		return vec;
	}

	public static Vector3 degreeDist3(Vector3 from, Vector3 to)
	{
		Vector3 zero = Vector3.zero;
		zero.x = Game.degreeDist(from.x, to.x);
		zero.y = Game.degreeDist(from.y, to.y);
		zero.z = Game.degreeDist(from.z, to.z);
		return zero;
	}

	public static float degreeOffset(Vector3 v)
	{
		float num = Mathf.Abs(Game.degreeDist(0f, v.x));
		num += Mathf.Abs(Game.degreeDist(0f, v.y));
		return num + Mathf.Abs(Game.degreeDist(0f, v.z));
	}

	public void lookAt(Transform bone, Vector3 target, float amount = 1f, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f, object hintVector = null)
	{
		this.origRot = bone.rotation;
		if (hintVector != null)
		{
			bone.LookAt(target, (Vector3)hintVector);
		}
		else
		{
			bone.LookAt(target);
		}
		bone.Rotate(this.axisFixVector);
		if (amount < 1f)
		{
			this.newRot = bone.rotation;
			bone.rotation = Quaternion.Slerp(this.origRot, this.newRot, amount);
		}
		bone.Rotate(offsetX * amount, offsetY * amount, offsetZ * amount);
	}

	public void applyCustomization()
	{
		switch (this.data.genitalType)
		{
		case 0:
			this.showPenis = true;
			this.showBalls = true;
			this.showVagina = false;
			break;
		case 1:
			this.showPenis = false;
			this.showBalls = false;
			this.showVagina = true;
			break;
		case 2:
			this.showPenis = false;
			this.showBalls = true;
			this.showVagina = false;
			break;
		case 3:
			this.showPenis = true;
			this.showBalls = false;
			this.showVagina = true;
			break;
		}
		if (UserSettings.data.modx_alwaysHaveBalls)
		{
			this.showBalls = true;
		}
		this.showWings = (this.data.wingType != 0);
		this.data.hasSheath = (this.data.ballsType == 1);
		this.penisSize_act = 1f + (this.data.penisSize - 0.5f) * 0.45f;
		this.penisLength_act = 0.2f + this.data.penisLength;
		if (this.data.penisType == 4)
		{
			this.penisLength_act += 0.9f;
		}
		this.penisGirth_act = 1f + (this.data.penisGirth - 0.2f) * 0.45f;
		if (this.data.penisType == 4)
		{
			this.penisGirth_act += 0.1f;
		}
		this.penisCurveX_act = (this.data.penisCurveX - 0.5f) * 2f;
		this.penisCurveY_act = (this.data.penisCurveY - 0.3f) * 2f;
		this.ballSize_act = 1f + (this.data.ballSize - 0.3f) * 0.8f;
		this.scrotumLength_act = 1f + (this.data.scrotumLength - 0.4f) * 0.56f;
		if (this.data.hasSheath)
		{
			this.scrotumLength_act *= 0.75f;
		}
		this.nippleSize_act = this.data.nippleSize;
		this.vaginaPlumpness_act = this.data.vaginaPlumpness;
		this.vaginaShape_act = this.data.vaginaShape;
		this.clitSize_act = 1f + this.data.clitSize * 1.8f;
		if (this.data.genitalType == 3)
		{
			this.clitSize_act = 1f;
		}
		this.growerShower_act = this.data.growerShower;
		if (this.data.hasSheath || this.data.ballsType == 2)
		{
			this.growerShower_act = 0f;
		}
		this.totalPenisSize = this.penisSize_act * (this.penisLength_act + this.penisGirth_act);
		this.totalBallSize = this.penisSize_act * (this.ballSize_act + this.scrotumLength_act * 0.2f);
		this.artificialSizeChange = this.modCap(this.artificialSizeChange, -1.5f, 1.5f);
		this.height_act = 0.8f + this.modCap(this.data.height + this.artificialSizeChange, -0.1f, 1.4f) * 0.5f;
		this.muscle_act = this.data.muscle;
		this.adiposity_act = this.data.adiposity;
		this.bodyMass_act = this.modCap(this.data.bodyMass - (this.height_act - 1f) * 0.2f + this.adiposity_act * 0.7f, 0f, 1f);
		this.belly_act = this.modCap(this.data.belly + this.adiposity_act * 0.2f, 0f, 1f);
		switch (this.data.genitalType)
		{
		case 0:
			this.bodyFemininity_act = 0f;
			this.headFemininity_act = 0.4f;
			break;
		case 1:
			this.bodyFemininity_act = 0.9f;
			this.headFemininity_act = 0.95f;
			break;
		case 2:
		case 3:
			this.bodyFemininity_act = 0.75f;
			this.headFemininity_act = 0.85f;
			break;
		}
		this.bodyFemininity_act += (this.data.bodyFemininity - 0.5f) * 2f;
		this.headFemininity_act += (this.data.headFemininity - 0.5f) * 2f;
		this.totalFemininity = (this.bodyFemininity_act + this.headFemininity_act * 3f) / 4f;
		this.shoulderWidth_act = 1.5f - this.bodyFemininity_act;
		this.footstepWeight = (this.data.bodyMass + 0.5f) * (this.data.height + 0.5f) + this.data.adiposity * 0.2f + this.data.muscle * 0.2f + this.data.belly * 0.2f;
		this.hipWidth_act = this.data.hipWidth + this.data.bodyFemininity * 0.1f;
		this.buttSize_act = this.data.buttSize;
		this.breastSize_act = this.data.breastSize + this.adiposity_act * 0.25f;
		this.breastPerk_act = this.data.breastPerk;
		this.needTailRecurl = true;
		if (this.data.rightHanded)
		{
			this.bones.mainHand = this.bones.Hand_R;
		}
		else
		{
			this.bones.mainHand = this.bones.Hand_L;
		}
		this.applyReferenceModifications();
	}

	public float cap(float val, float min = 0f, float max = 1f)
	{
		if (val < min)
		{
			val = min;
		}
		if (val > max)
		{
			val = max;
		}
		return val;
	}

	public void applyPenisBlends()
	{
		if (!this.rebuilding)
		{
			if (this.data.ballsType == 2 && !this.bodyGhosted)
			{
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Slitted"), 100f);
			}
			else
			{
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Slitted"), 0f);
			}
			if (this.bodyGhosted && !this.bodySuperGhosted)
			{
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 100f);
			}
			else
			{
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 0f);
			}
			switch (this.data.penisType)
			{
			case 0:
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollOntoRidge"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollToTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollPastTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadTaper"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadCanine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Equine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Flared"), 0f);
				break;
			case 1:
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollOntoRidge"), this.cap(this.foreskinOverlap / 0.4f, 0f, 1f) * 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollToTip"), this.cap((this.foreskinOverlap - 0.4f) / 0.4f, 0f, 1f) * 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollPastTip"), this.cap((this.foreskinOverlap - 0.8f) / 0.2f, 0f, 1f) * 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadTaper"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadCanine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Equine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Flared"), 0f);
				break;
			case 2:
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollOntoRidge"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollToTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollPastTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadTaper"), 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadCanine"), 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Equine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Flared"), 0f);
				break;
			case 3:
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollOntoRidge"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollToTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollPastTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadTaper"), 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadCanine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Equine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Flared"), 0f);
				break;
			case 4:
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollOntoRidge"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollToTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("RollPastTip"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadTaper"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("HeadCanine"), 0f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Equine"), 100f);
				this.penisPiece.SetBlendShapeWeight(this.penisPiece.sharedMesh.GetBlendShapeIndex("Flared"), this.cap(this.knotSwell * 120f, 0f, 100f));
				break;
			}
			for (int i = 0; i < this.clothingPiecesEquipped.Count; i++)
			{
				if (this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("ErectionBulge") != -1)
				{
					if (this.data.genitalType == 3 || this.data.genitalType == 0)
					{
						this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("ErectionBulge"), 5f + this.penisSize_act * 5f + this.arousal * (90f * this.penisSize_act * this.penisLength_act));
					}
					else
					{
						this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("ErectionBulge"), 0f);
					}
				}
			}
			this.pollPenisGirth(false);
		}
	}

	public void applyBallBlends()
	{
		if (!this.rebuilding && !((UnityEngine.Object)this.ballsPiece == (UnityEngine.Object)null) && this.data.genitalType != 2 && this.ballsPiece.sharedMesh.name.IndexOf("neuter") == -1)
		{
			if (this.ballsPiece.sharedMesh.name.IndexOf("SLIT") != -1)
			{
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Open"), this.cap(this.slitOutAmount * 1.2f - 0.1f, 0f, 1f) * 100f);
			}
			else if (this.data.hasSheath)
			{
				this.sheathedAmount = this.cap((0.3f - this.arousal) / 0.3f, 0f, 1f);
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Sheath"), 100f);
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Sheathed"), this.sheathedAmount * 100f);
			}
			else
			{
				this.sheathedAmount = 0f;
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Sheath"), 0f);
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Sheathed"), 0f);
			}
			if (this.bodyGhosted && !this.bodySuperGhosted)
			{
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 100f);
			}
			else
			{
				this.ballsPiece.SetBlendShapeWeight(this.ballsPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 0f);
			}
		}
	}

	public void fixHardcodedBodyRefVerts(Vector3[] verts)
	{
		for (int i = 0; i < verts.Length; i++)
		{
			this.v3.x = verts[i].x;
			this.v3.y = 0f - verts[i].z;
			this.v3.z = verts[i].y;
			verts[i] = this.v3;
		}
	}

	public void setBodyBlend(string name, float val)
	{
		int blendShapeIndex = this.bodyPiece.sharedMesh.GetBlendShapeIndex(name);
		this.bodyPiece.SetBlendShapeWeight(blendShapeIndex, val);
		for (int i = 0; i < this.clothingPiecesEquipped.Count; i++)
		{
			this.clothingPiecesEquipped[i].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(blendShapeIndex, val);
		}
	}

	public static void initClipFixRegions()
	{
		RackCharacter.clipFixRegionNames = new List<string>();
		RackCharacter.clipFixRegionNames.Add("underboob");
		RackCharacter.clipFixRegionNames.Add("neck");
		RackCharacter.clipFixRegionNames.Add("nipples");
		RackCharacter.clipFixRegionNames.Add("breasts");
		RackCharacter.clipFixRegionNames.Add("chest");
		RackCharacter.clipFixRegionNames.Add("shoulders");
		RackCharacter.clipFixRegionNames.Add("upperback");
		RackCharacter.clipFixRegionNames.Add("upperarms");
		RackCharacter.clipFixRegionNames.Add("elbows");
		RackCharacter.clipFixRegionNames.Add("lowerarms");
		RackCharacter.clipFixRegionNames.Add("waist");
		RackCharacter.clipFixRegionNames.Add("butt");
		RackCharacter.clipFixRegionNames.Add("upperlegs");
		RackCharacter.clipFixRegionNames.Add("belly");
	}

	public void applyReferenceModifications()
	{
		if (!this.rebuilding && this.initted)
		{
			for (int i = 0; i < RackCharacter.clipFixRegionNames.Count; i++)
			{
				this.bodyPiece.SetBlendShapeWeight(this.bodyPiece.sharedMesh.GetBlendShapeIndex("ClothingReduction_" + RackCharacter.clipFixRegionNames[i]), this.clipFixes[RackCharacter.clipFixRegionNames[i]]);
			}
			this.setBodyBlend("Feminine", this.modCap(this.bodyFemininity_act, 0f, 1f) * 100f);
			this.setBodyBlend("Breasts", this.modCap(this.breastSize_act, 0f, 1f) * 100f);
			this.setBodyBlend("Breasts2", this.modCap(this.breastSize_act - 1f, 0f, 1f) * 100f);
			this.setBodyBlend("Breasts3", this.modCap(this.breastSize_act - 2f, 0f, 1f) * 100f);
			if (this.breastsCoveredByClothing)
			{
				this.setBodyBlend("Nipples", 0f);
			}
			else
			{
				this.setBodyBlend("Nipples", this.modCap(this.nippleSize_act, 0f, 1f) * 100f);
			}
			this.setBodyBlend("Muscular", this.modCap(this.muscle_act, 0f, 1f) * 100f);
			this.setBodyBlend("Fat", this.modCap(this.adiposity_act, 0f, 1f) * 100f);
			this.setBodyBlend("Hips", this.modCap(this.hipWidth_act, 0f, 1f) * 100f);
			this.setBodyBlend("Butt", this.modCap(this.buttSize_act, 0f, 1f) * 100f);
			this.setBodyBlend("Belly", this.modCap(this.belly_act, 0f, 1f) * 100f);
			if (this.bodyMass_act >= 0.5f)
			{
				this.setBodyBlend("Lithe", 0f);
				this.setBodyBlend("Bulky", (this.modCap(this.bodyMass_act, 0f, 1f) - 0.5f) / 0.5f * 100f);
			}
			else
			{
				this.setBodyBlend("Bulky", 0f);
				this.setBodyBlend("Lithe", (0.5f - this.modCap(this.bodyMass_act, 0f, 1f)) / 0.5f * 100f);
			}
			this.feetPiece.SetBlendShapeWeight(this.feetPiece.sharedMesh.GetBlendShapeIndex("Bulk"), this.modCap(this.bodyMass_act - 0.5f, 0f, 1f) * 200f);
			for (int j = 0; j < this.clothingPiecesEquipped.Count; j++)
			{
				this.clothingPiecesEquipped[j].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[j].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Bulk"), this.modCap(this.bodyMass_act - 0.5f, 0f, 1f) * 200f);
			}
			if (this.data.specialFoot == "hooved" && this.feetPiece.sharedMesh.GetBlendShapeIndex("SmallHooves") != -1)
			{
				if (this.data.headType == "cervine")
				{
					this.feetPiece.SetBlendShapeWeight(this.feetPiece.sharedMesh.GetBlendShapeIndex("SmallHooves"), 100f);
				}
				else
				{
					this.feetPiece.SetBlendShapeWeight(this.feetPiece.sharedMesh.GetBlendShapeIndex("SmallHooves"), 20f);
				}
			}
			if (this.bodyGhosted && !this.bodySuperGhosted)
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 100f);
			}
			else
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 0f);
			}
			if (this.headFemininity_act >= 0.5f)
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Masculine"), 0f);
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Feminine"), (this.modCap(this.headFemininity_act, 0f, 1f) - 0.5f) / 0.5f * 100f);
			}
			else
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Feminine"), 0f);
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Masculine"), (0.5f - this.modCap(this.headFemininity_act, 0f, 1f)) / 0.5f * 100f);
			}
			float num = 0f;
			for (int k = 0; k < this.data.headMorphs.Count; k++)
			{
				if (this.data.headMorphs[k].key == "Head Size")
				{
					this.customHeadScale = 1f + (this.data.headMorphs[k].val - 0.5f) * 0.1f;
				}
				if (this.data.headMorphs[k].key == "Eye Size")
				{
					num = (this.data.headMorphs[k].val - 0.5f) * 2f;
				}
				if (this.data.headMorphs[k].key == "Tongue Length")
				{
					this.tongueScale = 1f + (RackCharacter.getTongueLengthForHeadType(this.data.headType) - 0.25f) * 3.7f + (this.data.headMorphs[k].val - 0.25f) * 0.8f;
				}
				if (this.headPiece.sharedMesh.GetBlendShapeIndex(this.data.headMorphs[k].key) != -1)
				{
					this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex(this.data.headMorphs[k].key), this.data.headMorphs[k].val * 100f);
				}
			}
			num += (this.headFemininity_act - 0.7f) * 0.7f;
			num = this.cap(num, -1f, 1f);
			if (num >= 0f)
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Small Eyes"), 0f);
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Big Eyes"), num * 100f);
			}
			else
			{
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Big Eyes"), 0f);
				this.headPiece.SetBlendShapeWeight(this.headPiece.sharedMesh.GetBlendShapeIndex("Small Eyes"), (0f - num) * 100f);
			}
			if (this.bodyGhosted && !this.bodySuperGhosted)
			{
				this.handsPiece.SetBlendShapeWeight(this.handsPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 100f);
			}
			else
			{
				this.handsPiece.SetBlendShapeWeight(this.handsPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 0f);
			}
			if (this.bodyGhosted && !this.bodySuperGhosted)
			{
				this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 100f);
			}
			else
			{
				this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("Ghost"), 0f);
			}
			this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("Plump"), this.modCap(this.vaginaPlumpness_act, 0f, 1f) * 100f);
			this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("LabiaMinora"), this.modCap(this.vaginaShape_act, 0f, 1f) * 100f);
			if (this.data.genitalType == 3)
			{
				this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("Herm"), 100f);
			}
			else
			{
				this.vaginaPiece.SetBlendShapeWeight(this.vaginaPiece.sharedMesh.GetBlendShapeIndex("Herm"), 0f);
			}
			for (int l = 0; l < this.clothingPiecesEquipped.Count; l++)
			{
				if (this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Digitigrade") != -1)
				{
					if (!this.effectivelyPlantigrade)
					{
						this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Digitigrade"), 100f);
					}
					else
					{
						this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Digitigrade"), 0f);
					}
					if (this.data.genitalType == 3 || this.data.genitalType == 0)
					{
						this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Bulge"), this.penisSize_act * 100f);
					}
					else
					{
						this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("Bulge"), 0f);
						this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(this.clothingPiecesEquipped[l].GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeIndex("ErectionBulge"), 0f);
					}
				}
			}
			this.lastTailSize = 1f;
		}
	}

	public void getEmbellishmentColorCoords(out Vector2 startV, out Vector2 endV, int col, bool headTexture)
	{
		startV = default(Vector2);
		endV = default(Vector2);
		if (!headTexture)
		{
			switch (col)
			{
			case 0:
				startV.x = 0.0078125f;
				startV.y = 0.9921875f;
				endV.x = 0.0229492188f;
				endV.y = 0.9926758f;
				break;
			case 1:
				startV.x = 0.0078125f;
				startV.y = 0.9824219f;
				endV.x = 0.0229492188f;
				endV.y = 0.982910156f;
				break;
			case 2:
				startV.x = 0.0078125f;
				startV.y = 0.97265625f;
				endV.x = 0.0229492188f;
				endV.y = 0.973144531f;
				break;
			case 3:
				startV.x = 0.0078125f;
				startV.y = 0.9628906f;
				endV.x = 0.0229492188f;
				endV.y = 0.9633789f;
				break;
			case 4:
				startV.x = 0.0078125f;
				startV.y = 0.953125f;
				endV.x = 0.00732421875f;
				endV.y = 0.938476563f;
				break;
			case 5:
				startV.x = 0.017578125f;
				startV.y = 0.953125f;
				endV.x = 0.0170898438f;
				endV.y = 0.938476563f;
				break;
			case 6:
				startV.x = 0.983398438f;
				startV.y = 0.953125f;
				endV.x = 0.982910156f;
				endV.y = 0.938476563f;
				break;
			case 7:
				startV.x = 0.993164063f;
				startV.y = 0.953125f;
				endV.x = 0.9926758f;
				endV.y = 0.938476563f;
				break;
			case 8:
				startV.x = 0.9785156f;
				startV.y = 0.9628906f;
				endV.x = 0.9926758f;
				endV.y = 0.9633789f;
				break;
			case 9:
				startV.x = 0.9785156f;
				startV.y = 0.97265625f;
				endV.x = 0.9926758f;
				endV.y = 0.973144531f;
				break;
			case 10:
				startV.x = 0.9785156f;
				startV.y = 0.9824219f;
				endV.x = 0.9926758f;
				endV.y = 0.982910156f;
				break;
			case 11:
				startV.x = 0.9785156f;
				startV.y = 0.9921875f;
				endV.x = 0.9926758f;
				endV.y = 0.9926758f;
				break;
			}
		}
		else
		{
			switch (col)
			{
			case 0:
				startV.x = 0.0107421875f;
				startV.y = 0.9902344f;
				endV.x = 0.0419921875f;
				endV.y = 0.991210938f;
				break;
			case 1:
				startV.x = 0.0107421875f;
				startV.y = 0.9707031f;
				endV.x = 0.0419921875f;
				endV.y = 0.9716797f;
				break;
			case 2:
				startV.x = 0.0107421875f;
				startV.y = 0.9511719f;
				endV.x = 0.0419921875f;
				endV.y = 0.952148438f;
				break;
			case 3:
				startV.x = 0.0107421875f;
				startV.y = 0.9316406f;
				endV.x = 0.0419921875f;
				endV.y = 0.9326172f;
				break;
			case 4:
				startV.x = 0.0107421875f;
				startV.y = 0.9121094f;
				endV.x = 0.009765625f;
				endV.y = 0.881835938f;
				break;
			case 5:
				startV.x = 0.0302734375f;
				startV.y = 0.9121094f;
				endV.x = 0.029296875f;
				endV.y = 0.881835938f;
				break;
			case 6:
				startV.x = 0.969726563f;
				startV.y = 0.9121094f;
				endV.x = 0.96875f;
				endV.y = 0.881835938f;
				break;
			case 7:
				startV.x = 0.9892578f;
				startV.y = 0.9121094f;
				endV.x = 0.98828125f;
				endV.y = 0.881835938f;
				break;
			case 8:
				startV.x = 0.9580078f;
				startV.y = 0.9316406f;
				endV.x = 0.98828125f;
				endV.y = 0.9326172f;
				break;
			case 9:
				startV.x = 0.9580078f;
				startV.y = 0.9511719f;
				endV.x = 0.98828125f;
				endV.y = 0.952148438f;
				break;
			case 10:
				startV.x = 0.9580078f;
				startV.y = 0.9707031f;
				endV.x = 0.98828125f;
				endV.y = 0.9716797f;
				break;
			case 11:
				startV.x = 0.9580078f;
				startV.y = 0.9902344f;
				endV.x = 0.98828125f;
				endV.y = 0.991210938f;
				break;
			}
		}
	}

	public void kill()
	{
		try
		{
			this.textureDrawingThread.Abort();
		}
		catch
		{
		}
		for (int i = 0; i < this.equippedSexToys.Count; i++)
		{
			this.equippedSexToys[i].kill();
		}
		this.dying = true;
		UnityEngine.Object.Destroy(this.emote);
		UnityEngine.Object.Destroy(this.bodyCanvas);
		UnityEngine.Object.Destroy(this.bodyFXCanvas);
		UnityEngine.Object.Destroy(this.bodyMetalCanvas);
		UnityEngine.Object.Destroy(this.bodyEmitCanvas);
		UnityEngine.Object.Destroy(this.headCanvas);
		UnityEngine.Object.Destroy(this.headFXCanvas);
		UnityEngine.Object.Destroy(this.headMetalCanvas);
		UnityEngine.Object.Destroy(this.headEmitCanvas);
		UnityEngine.Object.Destroy(this.wingCanvas);
		UnityEngine.Object.Destroy(this.wingFXCanvas);
		UnityEngine.Object.Destroy(this.wingMetalCanvas);
		UnityEngine.Object.Destroy(this.wingEmitCanvas);
		UnityEngine.Object.Destroy(this.headControlCanvas);
		UnityEngine.Object.Destroy(this.bodyControlCanvas);
		UnityEngine.Object.Destroy(this.tailControlCanvas);
		UnityEngine.Object.Destroy(this.wingControlCanvas);
		for (int j = 0; j < this.parts.Count; j++)
		{
			UnityEngine.Object.Destroy(this.parts[j].GetComponent<SkinnedMeshRenderer>().sharedMesh);
			UnityEngine.Object.Destroy(this.parts[j].GetComponent<Renderer>().material.GetTexture("_MainTex"));
			UnityEngine.Object.Destroy(this.parts[j]);
		}
		for (int k = 0; k < this.objectives.Count; k++)
		{
			this.objectives[k].dead = true;
		}
		this.initted = false;
		UnityEngine.Object.Destroy(this.GO);
	}

	static RackCharacter()
	{
		RackCharacter.charactersReboned = new List<string>();
		RackCharacter.allSpecies = new List<string>();
		RackCharacter.originalPenisRotations = new Quaternion[5];
		RackCharacter.totalLoadSteps = 12;
		RackCharacter.layerPixels = new List<UnityEngine.Color[]>();
		RackCharacter.layerIDs = new List<string>();
		RackCharacter.TextureBuilderBusy = false;
		RackCharacter.showTextureBuildTimes = false;
		RackCharacter.earStartingAngles = new Vector3[12];
		RackCharacter.boobOriginalRotations = new Vector3[2];
		RackCharacter.boobOriginalPositions = new Vector3[2];
		RackCharacter.originalButtRotations = new List<Quaternion>();
		RackCharacter.gotOriginalButtStuff = false;
		RackCharacter.targetStimulationGlobalAdjustment = 1.25f;
		RackCharacter.positionAlongPenetratorPenisStructureCutoff0 = 0.1f;
		RackCharacter.positionAlongPenetratorPenisStructureCutoff1 = 0.3f;
		RackCharacter.positionAlongPenetratorPenisStructureCutoff2 = 0.5f;
		RackCharacter.positionAlongPenetratorPenisStructureCutoff3 = 0.7f;
		RackCharacter.FKToFirstKnuckleL = new Vector3(-100.4f, 361.3f, 74.8f);
		RackCharacter.FKToFirstKnuckleR = new Vector3(259.3f, 361.3f, 106.6f);
		RackCharacter.customTexturesWeNeedToCheckForZeroLength = new List<string>();
		RackCharacter.allowWrongSubjectHotspots = false;
		RackCharacter.testingWrithe = false;
		RackCharacter.breastThreshhold = 0.2f;
		RackCharacter.existingTextures = new List<string>();
		RackCharacter.clothingRefDefinitions = new List<clothingRefVertDefinition>();
		RackCharacter.characterSounds = new List<AudioClip>();
		RackCharacter.characterSoundNames = new List<string>();
		RackCharacter.clipFixRegionNames = new List<string>();
	}

	public float modCap(float value, float min = 0f, float max = 1f)
	{
		return value;
	}
}
