using System.Collections.Generic;
using UnityEngine;

public class SexToy
{
	public RackCharacter subject;

	public int uid;

	public static int nextUID;

	public string itemID;

	public string slot;

	public LayoutItemSpecialProperties properties = new LayoutItemSpecialProperties();

	private bool needInit = true;

	private int invertX = -1;

	private int invertY = -1;

	private int invertZ = -1;

	private bool hasInversions;

	private bool hasGadget;

	private float randPitch;

	private GameObject GO;

	private Transform parent;

	private Vector3 v3;

	private Vector3 v32;

	private Vector3 v33;

	public bool on;

	public float power = 0.15f;

	private Vector3 originalPosition;

	private Vector3 originalRotation;

	private AudioSource audioSource;

	private float blink;

	private float blinkTime;

	private Material blinkingLightMaterial;

	private MeshRenderer blinkingLight;

	private Color originalBlinkingLightColor;

	public bool isAHeldItem;

	public bool beingHeld;

	private bool hasInUseLight;

	private Light inUseLight;

	private float originalIULintensity;

	public float inUseLightIntensity = 1f;

	public bool showInUseLight;

	private bool hasStartLoopStopSFX;

	private AudioClip startSFX;

	private AudioClip stopSFX;

	public bool playSLS;

	public bool wasPlayingSLS;

	public float SLSvol = 1f;

	public float SLSpitch = 1f;

	private float originalSLSpitch;

	private float originalSLSvol;

	public float penetratorLength = 0.5f;

	private Transform Base;

	private Transform Tip;

	private Transform Mount;

	private List<float> girths;

	private int girthPollingSegments = 20;

	private Vector3 aimTar;

	private Vector3 aimPos;

	public Vector3 toyTipAfterIK;

	public bool showHeld;

	private bool lastShowHeld = true;

	public void init()
	{
		this.randPitch = -0.05f + Random.value * 0.1f;
		this.uid = SexToy.nextUID;
		SexToy.nextUID++;
		switch (this.itemID)
		{
		case "ChemicalGun":
			this.makeGO(string.Empty);
			this.attachGO(this.subject.bones.mainHand, new Vector3(-0.388f, 0.093f, -0.239f), 85.882f, -73.55f, -303.095f, -1, -1, 0);
			this.initHeldItem();
			this.prepareAimedToy();
			this.prepareStartLoopStopSFX("chemicalgun_start", "chemicalgun_stop");
			this.prepareInUseLight();
			break;
		case "Dildo":
			this.makeGO(string.Empty);
			this.attachGO(this.subject.bones.mainHand, new Vector3(-0.383f, 0.126f, -0.164f), -13.572f, -133.38f, -52.181f, -1, -1, 0);
			this.initHeldItem();
			this.prepareAimedToy();
			this.preparePenetrator();
			break;
		case "CockRing":
			this.makeGO(string.Empty);
			this.attachGO(this.subject.bones.Penis1, new Vector3(0.059f, 0.013f, -0.011f), 0f, -90f, -15f, -1, -1, 0);
			this.addGadget();
			break;
		case "VibratingCockRing":
			this.makeGO(string.Empty);
			switch (this.slot)
			{
			case "shaft":
				this.attachGO(this.subject.bones.Penis1, new Vector3(0.059f, 0.013f, -0.011f), 0f, -90f, -15f, -1, -1, 0);
				break;
			case "penis":
				this.attachGO(this.subject.bones.Penis3, new Vector3(-0.054f, -0.013f, -0.009f), 0f, -90f, -205f, -1, -1, 0);
				break;
			}
			this.findBlinkingLight();
			this.addGadget();
			break;
		}
		this.needInit = false;
	}

	public void process()
	{
		if (this.needInit)
		{
			this.init();
		}
		this.processScaleInversion();
		this.processHeldItem();
		this.processStartLoopStop();
		this.processInUseLight();
		string text = this.itemID;
		if (text != null)
		{
			if (!(text == "CockRing"))
			{
				if (text == "VibratingCockRing")
				{
					this.subject.maintainArousal(0.2f);
					this.processBlinkingLight();
					this.vibrate();
				}
			}
			else
			{
				this.subject.maintainArousal(0.2f);
			}
		}
	}

	public void prepareInUseLight()
	{
		this.inUseLight = this.GO.GetComponentInChildren<Light>();
		this.originalIULintensity = this.inUseLight.intensity;
		this.hasInUseLight = true;
	}

	public void processInUseLight()
	{
		if (this.hasInUseLight)
		{
			this.inUseLight.enabled = this.showInUseLight;
			this.inUseLight.intensity = this.originalIULintensity * this.inUseLightIntensity;
		}
	}

	public void prepareStartLoopStopSFX(string start, string stop)
	{
		this.startSFX = (Resources.Load(start) as AudioClip);
		this.stopSFX = (Resources.Load(stop) as AudioClip);
		this.originalSLSpitch = this.audioSource.pitch;
		this.originalSLSvol = this.audioSource.volume;
		this.hasStartLoopStopSFX = true;
	}

	public void processStartLoopStop()
	{
		if (this.hasStartLoopStopSFX)
		{
			if (this.playSLS)
			{
				if (!this.wasPlayingSLS)
				{
					this.audioSource.pitch = this.originalSLSpitch;
					this.audioSource.volume = this.originalSLSvol;
					this.audioSource.PlayOneShot(this.startSFX, 1f);
					this.audioSource.Play();
					this.wasPlayingSLS = true;
				}
				this.audioSource.pitch = this.originalSLSpitch * this.SLSpitch;
				this.audioSource.volume = this.originalSLSvol * this.SLSvol;
			}
			else if (this.wasPlayingSLS)
			{
				this.audioSource.Stop();
				this.audioSource.pitch = this.originalSLSpitch;
				this.audioSource.volume = this.originalSLSvol;
				this.audioSource.PlayOneShot(this.stopSFX, 1f);
				this.wasPlayingSLS = false;
			}
		}
	}

	public void prepareAimedToy()
	{
		this.Base = this.GO.transform.Find("base");
		this.Tip = this.Base.Find("tip");
		this.Mount = this.Base.Find("Hand_R");
		this.penetratorLength = (this.Tip.position - this.Base.position).magnitude;
	}

	public void preparePenetrator()
	{
		GameObject gameObject = Object.Instantiate(this.GO);
		gameObject.layer = 12;
		gameObject.transform.SetParent(Game.gameInstance.World.transform);
		gameObject.transform.position = Vector3.zero;
		gameObject.transform.rotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<MeshCollider>();
		this.girths = new List<float>();
		for (int i = 0; i < this.girthPollingSegments; i++)
		{
			float num = 0f;
			float num2 = 0f;
			Vector3 a = gameObject.transform.Find("base").position + (gameObject.transform.Find("base").Find("tip").position - gameObject.transform.Find("base").position) * ((float)i / ((float)this.girthPollingSegments - 1f));
			for (int j = 0; j < 4; j++)
			{
				switch (j)
				{
				case 0:
					this.v3 = Vector3.up;
					break;
				case 1:
					this.v3 = -Vector3.up;
					break;
				case 2:
					this.v3 = Vector3.right;
					break;
				case 3:
					this.v3 = -Vector3.right;
					break;
				}
				RaycastHit raycastHit = default(RaycastHit);
				if (Physics.Raycast(new Ray(a + this.v3, -this.v3), out raycastHit, this.v3.magnitude, LayerMask.GetMask("PreciseRaycasting")))
				{
					switch (j)
					{
					case 0:
						num2 += this.v3.magnitude - raycastHit.distance;
						break;
					case 1:
						num2 += this.v3.magnitude - raycastHit.distance;
						break;
					case 2:
						num += this.v3.magnitude - raycastHit.distance;
						break;
					case 3:
						num += this.v3.magnitude - raycastHit.distance;
						break;
					}
				}
			}
			if (num > num2)
			{
				this.girths.Add(num);
			}
			else
			{
				this.girths.Add(num2);
			}
		}
		Object.Destroy(gameObject);
	}

	public void aimAt(Vector3 target, Vector3 position, Transform mount = null, bool holderUp = false)
	{
		this.Base.position = position;
		if (holderUp)
		{
			this.Base.LookAt(target, this.subject.upAfterIK());
		}
		else
		{
			this.Base.LookAt(target, position - this.subject.bones.SpineUpper.position);
		}
		if ((Object)mount != (Object)null)
		{
			mount.position = this.Mount.position;
			mount.rotation = this.Mount.rotation;
		}
	}

	public void getToyTipAfterIK()
	{
		this.toyTipAfterIK = this.Tip.position;
	}

	public void processScaleInversion()
	{
		if (this.hasInversions)
		{
			this.v3 = Vector3.one;
			if (this.invertX > -1)
			{
				switch (this.invertX)
				{
				case 0:
				{
					ref Vector3 val3 = ref this.v3;
					Vector3 localScale3 = this.parent.localScale;
					val3.x = 1f / localScale3.x;
					break;
				}
				case 1:
				{
					ref Vector3 val2 = ref this.v3;
					Vector3 localScale2 = this.parent.localScale;
					val2.x = 1f / localScale2.y;
					break;
				}
				case 2:
				{
					ref Vector3 val = ref this.v3;
					Vector3 localScale = this.parent.localScale;
					val.x = 1f / localScale.z;
					break;
				}
				}
			}
			if (this.invertY > -1)
			{
				switch (this.invertY)
				{
				case 0:
				{
					ref Vector3 val6 = ref this.v3;
					Vector3 localScale6 = this.parent.localScale;
					val6.y = 1f / localScale6.x;
					break;
				}
				case 1:
				{
					ref Vector3 val5 = ref this.v3;
					Vector3 localScale5 = this.parent.localScale;
					val5.y = 1f / localScale5.y;
					break;
				}
				case 2:
				{
					ref Vector3 val4 = ref this.v3;
					Vector3 localScale4 = this.parent.localScale;
					val4.y = 1f / localScale4.z;
					break;
				}
				}
			}
			if (this.invertZ > -1)
			{
				switch (this.invertZ)
				{
				case 0:
				{
					ref Vector3 val9 = ref this.v3;
					Vector3 localScale9 = this.parent.localScale;
					val9.z = 1f / localScale9.x;
					break;
				}
				case 1:
				{
					ref Vector3 val8 = ref this.v3;
					Vector3 localScale8 = this.parent.localScale;
					val8.z = 1f / localScale8.y;
					break;
				}
				case 2:
				{
					ref Vector3 val7 = ref this.v3;
					Vector3 localScale7 = this.parent.localScale;
					val7.z = 1f / localScale7.z;
					break;
				}
				}
			}
			this.GO.transform.localScale = this.v3;
		}
	}

	public float girthAlongPenetrator(float position)
	{
		position = 1f - Game.cap(position, 0f, 1f);
		int num = Mathf.FloorToInt(position * (float)(this.girthPollingSegments - 1));
		float num2 = this.girths[num];
		if (num < this.girthPollingSegments - 1)
		{
			float num3 = position * (float)(this.girthPollingSegments - 1) - (float)num;
			num2 *= 1f - num3;
			num2 += num3 * this.girths[num];
		}
		if (num2 < 0.005f)
		{
			num2 = 0.005f;
		}
		return num2 * this.subject.height_act;
	}

	public void aimToyHand(Vector3 target)
	{
		if (this.subject.data.rightHanded)
		{
			this.v3 = this.subject.bones.UpperArm_R.position;
		}
		else
		{
			this.v3 = this.subject.bones.UpperArm_L.position;
		}
		this.v3 += (this.subject.forward() - this.subject.up() * 0.8f) * 0.6f * this.subject.height_act;
		this.aimAt(target, this.v3, null, true);
		this.subject.manuallyPositionHand(this.subject.data.rightHanded, this.Mount.position, this.Mount.rotation);
	}

	public void processHeldItem()
	{
		if (this.isAHeldItem)
		{
			this.showHeld = this.beingHeld;
			if (this.subject.currentInteractions.Count == 0 && this.showHeld && this.subject.interactionSubject != null)
			{
				this.aimToyHand(this.subject.bones.SpineUpper.position + (this.subject.forward() - this.subject.up() * 0.2f) * this.subject.height_act * 0.9f);
			}
			if (this.showHeld != this.lastShowHeld)
			{
				this.GO.SetActive(this.showHeld);
				if (this.showHeld)
				{
					this.subject.assignFingerTargets(this.Base);
				}
				this.lastShowHeld = this.showHeld;
			}
		}
	}

	public void vibrate()
	{
		this.v3 = this.originalPosition;
		this.v32 = this.originalRotation;
		if (this.on)
		{
			this.v3.x += (-0.5f + Random.value) * this.power * 0.006f;
			this.v3.y += (-0.5f + Random.value) * this.power * 0.006f;
			this.v3.z += (-0.5f + Random.value) * this.power * 0.006f;
			this.v32.x += (-0.5f + Random.value) * this.power * 5f;
			this.v32.y += (-0.5f + Random.value) * this.power * 5f;
			this.v32.z += (-0.5f + Random.value) * this.power * 5f;
			this.parent.Rotate(this.v32 * Time.deltaTime * 0.35f);
			switch (this.slot)
			{
			case "shaft":
				this.subject.stimulate(0.1f + this.power * 0.2f);
				this.subject.arouse(0.05f + this.power * 0.05f);
				this.subject.triggerSensation("sextoy_general", 0.1f);
				this.subject.triggerSensation("sextoy_penis", 0.1f);
				this.subject.triggerSensation("vibration", 0.1f);
				break;
			case "penis":
				this.subject.stimulate(0.2f + this.power * 0.8f);
				this.subject.arouse(0.05f + this.power * 0.05f);
				this.subject.triggerSensation("sextoy_general", 0.1f);
				this.subject.triggerSensation("sextoy_penis", 0.1f);
				this.subject.triggerSensation("vibration", 0.1f);
				break;
			}
		}
		this.audioSource.enabled = this.on;
		this.audioSource.pitch = 0.7f + 0.3f * this.power + this.randPitch;
		this.audioSource.volume = 0.01f + 0.04f * this.power;
		this.GO.transform.localPosition = this.v3;
		this.GO.transform.localEulerAngles = this.v32;
	}

	public void processBlinkingLight()
	{
		if (this.on)
		{
			this.blinkTime += Time.deltaTime * this.power * 6f;
			this.blink = Mathf.Pow(1f - this.blinkTime % 1f, 4f);
			this.blinkingLightMaterial.SetColor("_EmissionColor", this.originalBlinkingLightColor * this.blink);
		}
		else
		{
			this.blinkingLightMaterial.SetColor("_EmissionColor", Color.black);
		}
		this.blinkingLight.material = this.blinkingLightMaterial;
	}

	public void addGadget()
	{
		this.hasGadget = true;
		Game.gameInstance.addGadget(this.itemID + "." + this.uid, false, this);
	}

	public void findBlinkingLight()
	{
		MeshRenderer[] componentsInChildren = this.GO.GetComponentsInChildren<MeshRenderer>();
		int num = 0;
		while (true)
		{
			if (num < componentsInChildren.Length)
			{
				if (componentsInChildren[num].name.IndexOf("light") == -1)
				{
					num++;
					continue;
				}
				break;
			}
			return;
		}
		this.blinkingLight = componentsInChildren[num];
		this.blinkingLightMaterial = componentsInChildren[num].material;
		this.originalBlinkingLightColor = this.blinkingLightMaterial.GetColor("_EmissionColor");
	}

	public void initHeldItem()
	{
		this.isAHeldItem = true;
		this.beingHeld = false;
	}

	public void hideOnCharacter()
	{
	}

	public void attachGO(Transform to, Vector3 pos, float rotateX, float rotateY, float rotateZ, int _invertX, int _invertY, int _invertZ)
	{
		this.GO.transform.SetParent(to);
		this.parent = to;
		this.GO.transform.localPosition = pos;
		this.GO.transform.localEulerAngles = new Vector3(rotateX, rotateY, rotateZ);
		this.GO.transform.localScale = Vector3.one;
		this.invertX = _invertX;
		this.invertY = _invertY;
		this.invertZ = _invertZ;
		this.hasInversions = (this.invertX > -1 || this.invertY > -1 || this.invertZ > -1);
		this.originalRotation = this.GO.transform.localEulerAngles;
		this.originalPosition = this.GO.transform.localPosition;
	}

	public void makeGO(string labItemName = "")
	{
		if (labItemName == string.Empty)
		{
			labItemName = this.itemID;
		}
		this.GO = Object.Instantiate(TestingRoom.labItemContainer.Find(labItemName).gameObject);
		this.GO.SetActive(true);
		if (this.properties.material != string.Empty)
		{
			ToyMaterials.applyMaterialToObject(this.GO, this.properties.material);
		}
		this.audioSource = this.GO.GetComponentInChildren<AudioSource>();
	}

	public void kill()
	{
		if ((Object)this.GO != (Object)null)
		{
			Object.Destroy(this.GO);
			this.GO = null;
		}
		if (this.hasGadget)
		{
			Game.gameInstance.killGadget(this.itemID + "." + this.uid);
		}
		if (this.beingHeld)
		{
			this.subject.clearFingerTargets();
		}
		this.needInit = true;
	}
}
