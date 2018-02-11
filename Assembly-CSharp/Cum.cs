using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cum
{
	public static List<CumDot> cumDots = new List<CumDot>();

	public static List<CumDot> pooledCumDots = new List<CumDot>();

	public static List<CumDot> dripDots = new List<CumDot>();

	public static List<CumDot> pooledDripDots = new List<CumDot>();

	public static float interpolationFrequency = 0.1f;

	public static GameObject CumTemplate;

	public static GameObject CumContainer;

	public static float tScale = 0.8f;

	public static float cumTravelSpeed = 0.9f;

	public static float oldY;

	public static Vector3 rv;

	public static Vector3 cv;

	public static RaycastHit hInfo;

	public static RackCharacter characterReference;

	public static float consolidationRange;

	public static int msk = -99;

	public static float maxCumLife = 40f;

	public static Vector3 v3;

	public static Color defaultCumColor;

	public static void init()
	{
		Cum.defaultCumColor = new Color(0.870588243f, 0.8745098f, 0.784313738f);
		Cum.CumTemplate = GameObject.Find("cum");
		Cum.CumContainer = GameObject.Find("cumContainer");
		for (int i = 0; i < 256; i++)
		{
			Cum.addCumDotToPool();
		}
	}

	public static void addCumDotToPool()
	{
		CumDot cumDot = new CumDot();
		cumDot.position = Vector3.zero;
		cumDot.velocity = Vector3.zero;
		cumDot.GO = Object.Instantiate(Cum.CumTemplate);
		MeshRenderer[] componentsInChildren = cumDot.GO.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].reflectionProbeUsage = ReflectionProbeUsage.Simple;
		}
		cumDot.head = cumDot.GO.transform.Find("Armature").Find("B0");
		cumDot.tail = cumDot.GO.transform.Find("Armature").Find("B0").Find("B1");
		cumDot.tail.SetParent(cumDot.head.parent);
		cumDot.head.localScale = Vector3.one * 0.05f;
		cumDot.tail.localScale = Vector3.one * 0.05f;
		cumDot.GO.SetActive(false);
		cumDot.GO.transform.SetParent(Cum.CumContainer.transform);
		cumDot.GO.transform.localPosition = Vector3.zero;
		cumDot.mat = cumDot.GO.GetComponentInChildren<Renderer>().material;
		Cum.pooledCumDots.Add(cumDot);
	}

	public static void addDripToPool()
	{
		CumDot cumDot = new CumDot();
		cumDot.position = Vector3.zero;
		cumDot.velocity = Vector3.zero;
		cumDot.tailvelocity = Vector3.zero;
		cumDot.GO = Object.Instantiate(Cum.CumTemplate);
		MeshRenderer[] componentsInChildren = cumDot.GO.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].reflectionProbeUsage = ReflectionProbeUsage.Simple;
		}
		cumDot.head = cumDot.GO.transform.Find("Armature").Find("B0");
		cumDot.tail = cumDot.GO.transform.Find("Armature").Find("B0").Find("B1");
		cumDot.tail.SetParent(cumDot.head.parent);
		cumDot.head.localScale = Vector3.one * 0.05f;
		cumDot.tail.localScale = Vector3.one * 0.05f;
		cumDot.GO.SetActive(false);
		cumDot.GO.transform.SetParent(Cum.CumContainer.transform);
		cumDot.GO.transform.localPosition = Vector3.zero;
		cumDot.mat = cumDot.GO.GetComponentInChildren<Renderer>().material;
		Cum.pooledDripDots.Add(cumDot);
	}

	public static Transform FindArmature(Transform bone)
	{
		if ((Object)bone == (Object)null)
		{
			return null;
		}
		if (bone.name == "Armature")
		{
			return bone;
		}
		return Cum.FindArmature(bone.parent);
	}

	public static void process()
	{
		for (int num = Cum.cumDots.Count - 1; num >= 0; num--)
		{
			Cum.processDot(Cum.cumDots[num], num, true);
		}
		for (int num2 = Cum.dripDots.Count - 1; num2 >= 0; num2--)
		{
			Cum.processDot(Cum.dripDots[num2], num2, false);
		}
	}

	public static void processDot(CumDot dot, int c, bool isCum = true)
	{
		if ((Object)dot.head == (Object)null)
		{
			if (isCum)
			{
				Cum.pooledCumDots.Add(dot);
				Cum.cumDots.RemoveAt(c);
			}
			else
			{
				Cum.pooledDripDots.Add(dot);
				Cum.dripDots.RemoveAt(c);
			}
		}
		else
		{
			if (!dot.finishedTraveling)
			{
				dot.raycastDelay = 0f;
				if (dot.raycastDelay <= 0f)
				{
					dot.lastPos = dot.position;
				}
				dot.position += dot.velocity * Time.deltaTime * Cum.cumTravelSpeed * Cum.tScale;
				if (dot.raycastDelay <= 0f && isCum)
				{
					Cum.v3 = Cum.detectCollision(dot);
					dot.raycastDelay = 1f;
				}
				else
				{
					Cum.v3 = Vector3.zero;
				}
				if (Cum.v3 != Vector3.zero)
				{
					dot.position = Cum.v3;
					dot.head.position = dot.position;
					dot.finishedTraveling = true;
					if (dot.breaksRemaining > 2)
					{
						dot.breaksRemaining = 2;
					}
				}
				else if (isCum)
				{
					dot.velocity.y -= Time.deltaTime * Cum.cumTravelSpeed * 25f * Cum.tScale;
					dot.head.position = dot.position;
				}
				dot.raycastDelay -= Time.deltaTime;
			}
			if (dot.finishedTraveling)
			{
				dot.life -= Time.deltaTime * Cum.tScale * 10f;
			}
			else
			{
				dot.life -= Time.deltaTime * Cum.tScale;
			}
			if ((Object)dot.link != (Object)null)
			{
				if (dot.linkedCumDot != null)
				{
					if (dot.linkedCumDot.life <= 0f)
					{
						dot.link = null;
					}
				}
				else
				{
					dot.link = null;
				}
			}
			if ((Object)dot.link != (Object)null)
			{
				dot.tail.position = dot.link.position;
				dot.tail.rotation = dot.link.rotation;
				dot.tail.localScale = dot.link.localScale;
				if (!dot.finishedTraveling)
				{
					Cum.oldY = dot.velocity.y;
					dot.velocity += (dot.link.position - dot.head.position - dot.velocity) * Game.cap(Time.deltaTime * (3.5f - (float)dot.breaksRemaining) * 0.5f, 0f, 1f);
					dot.velocity.y = Cum.oldY;
				}
				if (!dot.finishedTraveling || !dot.linkedCumDot.finishedTraveling)
				{
					if (!dot.slider)
					{
						dot.head.LookAt(dot.tail, dot.velocity);
						dot.head.Rotate(90f, 0f, 0f);
					}
					if ((dot.tail.position - dot.head.position).magnitude > 0.35f)
					{
						if (isCum)
						{
							if (dot.breaksRemaining < 1)
							{
								dot.link = null;
								dot.linkedCumDot = null;
							}
							else if (dot.breaksRemaining == 3)
							{
								Cum.v3 = Cum.randomV3() * 0.038f;
								CumDot cumDot = Cum.addCumDot(dot.head.position + (dot.link.position - dot.head.position) * 0.5f + Cum.v3, dot.velocity + Cum.v3 * 10f, dot.thickness * 0.9f, dot.color);
								cumDot.link = dot.link;
								cumDot.linkedCumDot = dot;
								cumDot.breaksRemaining = dot.breaksRemaining - 1;
								dot.link = cumDot.head;
								dot.linkedCumDot = cumDot;
								dot.breaksRemaining--;
							}
							else
							{
								Cum.v3 = Cum.randomV3() * 0.038f;
								CumDot cumDot2 = Cum.addCumDot(dot.head.position + (dot.link.position - dot.head.position) * 0.5f + Cum.v3, dot.velocity * 0.9f + Cum.v3 * 10f, dot.thickness * 0.9f, dot.color);
								cumDot2.link = dot.link;
								cumDot2.linkedCumDot = dot;
								cumDot2.breaksRemaining = dot.breaksRemaining - 1;
								CumDot cumDot3 = Cum.addCumDot(dot.head.position + (dot.link.position - dot.head.position) * 0.5f + Cum.v3, dot.velocity * 1.1f + Cum.v3 * 10f, dot.thickness * 0.9f, dot.color);
								cumDot3.link = null;
								cumDot3.linkedCumDot = null;
								cumDot3.breaksRemaining = dot.breaksRemaining - 1;
								dot.link = cumDot3.head;
								dot.linkedCumDot = cumDot3;
								dot.breaksRemaining--;
							}
						}
						else
						{
							dot.linkedCumDot.life = 0.01f;
						}
					}
				}
			}
			else if (isCum)
			{
				dot.tail.rotation = dot.head.rotation;
				dot.tail.position = dot.head.position;
				dot.tail.localScale = Vector3.zero;
			}
			else
			{
				dot.scaleIn += (1f - dot.scaleIn) * Game.cap(Time.deltaTime * 11f, 0f, 1f);
				if (dot.slider)
				{
					dot.head.localScale = Vector3.one * 0.016f * dot.scaleIn;
					dot.tail.localScale = Vector3.one * 0.016f * dot.scaleIn;
					dot.tail.rotation = dot.head.rotation;
					dot.tail.position = dot.head.position;
					if (dot.scaleIn > 0.9f)
					{
						bool flag = false;
						if (Physics.Raycast(dot.head.position + Vector3.up * 0.01f, Vector3.down * 12f + -dot.slideNormal, out Cum.hInfo, 0.1f, LayerMask.GetMask("Ignore Raycast")) && Cum.hInfo.collider.gameObject.name == "Penis4")
						{
							Cum.v3 = Cum.hInfo.normal;
							dot.slideNormal = Cum.hInfo.normal;
							if (Cum.v3.y > 0f)
							{
								Cum.v3.y = 0f;
							}
							dot.head.position = Cum.hInfo.point + Cum.hInfo.normal * 0.0025f;
							dot.head.LookAt(Cum.hInfo.point + Cum.hInfo.normal, Vector3.forward);
							dot.head.Rotate(90f, 0f, 0f);
							flag = true;
						}
						if (!flag)
						{
							Transform head = dot.head;
							head.position += dot.slideNormal * 0.005f;
							dot.head.localScale = Vector3.one * 0.01f;
							dot.slider = false;
							dot.lastTailPos = dot.tail.position;
						}
					}
				}
				else
				{
					dot.tailvelocity.y -= Time.deltaTime * Cum.cumTravelSpeed * 0.5f * Cum.tScale;
					if (!dot.stillAttached)
					{
						dot.velocity.y -= Time.deltaTime * Cum.cumTravelSpeed * 0.5f * Cum.tScale;
						Transform head2 = dot.head;
						head2.position += dot.velocity;
					}
					Cum.v3 = dot.head.position - dot.tail.position;
					float magnitude = Cum.v3.magnitude;
					if (magnitude >= 0.2f)
					{
						dot.stillAttached = false;
						dot.GO.transform.SetParent(Cum.CumContainer.transform);
					}
					Cum.v3 = Cum.v3.normalized;
					dot.tailvelocity += Cum.v3 * Mathf.Pow(magnitude * (10f - Game.cap(dot.thickness, 0f, 1f) * 9f), 3f) * Time.deltaTime * Cum.cumTravelSpeed * Cum.tScale * 0.5f;
					dot.tailvelocity -= dot.tailvelocity * Game.cap(Time.deltaTime * 2f, 0f, 1f);
					Transform tail = dot.tail;
					tail.position += dot.tailvelocity;
					if (Physics.Raycast(dot.lastTailPos, dot.tail.position - dot.lastTailPos, out Cum.hInfo, (dot.tail.position - dot.lastTailPos).magnitude * 1.15f, LayerMask.GetMask("Ignore Raycast")))
					{
						if (Cum.hInfo.collider.gameObject.name.IndexOf("Penis") == -1)
						{
							dot.stillAttached = false;
							dot.GO.transform.SetParent(Cum.CumContainer.transform);
						}
						else
						{
							dot.tail.position = Cum.hInfo.point;
							dot.lastTailPos = Cum.hInfo.point + Cum.hInfo.normal * 0.34f;
						}
					}
					else
					{
						dot.lastTailPos = dot.tail.position;
					}
					if (dot.thickness < 0.4f)
					{
						Transform tail2 = dot.tail;
						tail2.position += (dot.head.position - dot.tail.position) * Game.cap(Time.deltaTime * (0.4f - dot.thickness) * 100f, 0f, 1f);
						dot.tailvelocity -= dot.tailvelocity * Game.cap(Time.deltaTime * (0.4f - dot.thickness) * 30f, 0f, 1f);
					}
					if (!dot.slider)
					{
						dot.head.LookAt(dot.tail, Vector3.forward);
						dot.head.Rotate(90f, 0f, 0f);
					}
					dot.tail.rotation = dot.head.rotation;
					Transform head3 = dot.head;
					head3.localScale += (Vector3.one * 0.009f - dot.head.localScale) * Game.cap(Time.deltaTime * 3f, 0f, 1f);
					Transform tail3 = dot.tail;
					tail3.localScale += (dot.head.localScale * dot.thickness * 2f - dot.tail.localScale) * Game.cap(Time.deltaTime * 3f, 0f, 1f);
					Vector3 position = dot.tail.position;
					if (position.y < -10f)
					{
						dot.life = 0f;
					}
				}
			}
			if (dot.life < Cum.maxCumLife * 0.1f && isCum)
			{
				dot.head.localScale = Vector3.one * 0.07f * dot.thickness * (dot.life / (Cum.maxCumLife * 0.1f));
			}
			if (dot.life <= 0f)
			{
				dot.GO.transform.SetParent(Cum.CumContainer.transform);
				dot.GO.transform.localScale = Vector3.one;
				dot.GO.SetActive(false);
				if (isCum)
				{
					Cum.pooledCumDots.Add(dot);
					Cum.cumDots.RemoveAt(c);
				}
				else
				{
					Cum.pooledDripDots.Add(dot);
					Cum.dripDots.RemoveAt(c);
				}
			}
			else
			{
				dot.GO.SetActive(true);
			}
		}
	}

	public static Vector3 randomV3()
	{
		Cum.rv.x = -1f + Random.value * 2f;
		Cum.rv.y = -1f + Random.value * 2f;
		Cum.rv.z = -1f + Random.value * 2f;
		return Cum.rv.normalized;
	}

	public static Vector3 detectCollision(CumDot cd)
	{
		if (Cum.msk == -99)
		{
			Cum.msk = LayerMask.GetMask("StaticPreciseRaycasting", "Ignore Raycast", "StaticObjects", "LabSurfaces");
		}
		cd.parent = null;
		Cum.cv = Vector3.zero;
		if (Physics.Raycast(cd.lastPos, cd.position - cd.lastPos, out Cum.hInfo, (cd.position - cd.lastPos).magnitude, Cum.msk))
		{
			cd.parent = Cum.hInfo.collider.transform;
			cd.GO.transform.SetParent(cd.parent);
			if (Cum.hInfo.collider.transform.gameObject.layer != 2)
			{
				Cum.cv = Cum.hInfo.point;
				Cum.cv += (cd.lastPos - Cum.cv).normalized * 0.05f;
				Cum.consolidationRange = 0.03f;
			}
			else
			{
				Cum.cv = Cum.hInfo.point;
				if (Cum.hInfo.collider.name.Substring(0, 4) != "Tail")
				{
					Cum.characterReference = Cum.FindArmature(cd.parent).parent.gameObject.GetComponent<RackCharacterReference>().reference;
					if ((Cum.hInfo.point - Cum.characterReference.bones.Eye_L.position).magnitude < 0.12f)
					{
						Cum.characterReference.cumInEye(false, cd.life);
					}
					if ((Cum.hInfo.point - Cum.characterReference.bones.Eye_R.position).magnitude < 0.12f)
					{
						Cum.characterReference.cumInEye(true, cd.life);
					}
				}
				Cum.consolidationRange = 0f;
				string name = Cum.hInfo.collider.gameObject.name;
				if (name.IndexOf("Hand") != -1 || name.IndexOf("Finger") != -1 || name.IndexOf("Thumb") != -1)
				{
					cd.life = Game.cap(cd.life, 0f, 1f);
				}
			}
			for (int i = 0; i < Cum.cumDots.Count; i++)
			{
				if (Cum.cumDots[i] != cd && Cum.cumDots[i].life > 0f && (Object)Cum.cumDots[i].head != (Object)null && Cum.cumDots[i].finishedTraveling && (Cum.hInfo.point - Cum.cumDots[i].head.position).magnitude < Cum.consolidationRange)
				{
					Cum.cumDots[i].thickness += cd.thickness * 0.2f;
					Cum.cumDots[i].head.localScale = Vector3.one * 0.07f * Cum.cumDots[i].thickness * Game.cap(Cum.cumDots[i].life / (Cum.maxCumLife * 0.1f), 0f, 1f);
					cd.life = 0f;
				}
			}
			cd.impactNormal = Cum.hInfo.normal;
			if ((Object)cd.link == (Object)null)
			{
				cd.head.LookAt(Cum.hInfo.point + cd.impactNormal, Vector3.forward);
				cd.head.Rotate(90f, 0f, 0f);
			}
			cd.head.localScale = Vector3.one * cd.thickness * 0.07f;
			return Cum.cv;
		}
		if (cd.position.y < -10f)
		{
			Cum.cv = cd.position;
			Cum.cv.y = -10f;
			return Cum.cv;
		}
		return Cum.cv;
	}

	public static CumDot addDripDot(Vector3 position, float thickness, Color col)
	{
		while (Cum.pooledDripDots.Count < 1)
		{
			Cum.addDripToPool();
		}
		while ((Object)Cum.pooledDripDots[0].head == (Object)null)
		{
			Cum.pooledDripDots.RemoveAt(0);
			Cum.addDripToPool();
		}
		Cum.pooledDripDots[0].lastPos = position;
		Cum.pooledDripDots[0].position = position;
		Cum.pooledDripDots[0].head.position = position;
		Cum.pooledDripDots[0].tail.position = position;
		Cum.pooledDripDots[0].lastTailPos = Cum.pooledDripDots[0].tail.position;
		Cum.pooledDripDots[0].velocity = Vector3.zero;
		Cum.pooledDripDots[0].tailvelocity = Vector3.zero;
		Cum.pooledDripDots[0].finishedTraveling = false;
		Cum.pooledDripDots[0].position = position;
		Cum.pooledDripDots[0].life = Cum.maxCumLife;
		Cum.pooledDripDots[0].link = null;
		Cum.pooledDripDots[0].thickness = 0.1f;
		Cum.pooledDripDots[0].breaksRemaining = 3;
		Cum.pooledDripDots[0].scaleIn = 0f;
		Cum.pooledDripDots[0].raycastDelay = 0f;
		Cum.pooledDripDots[0].stillAttached = true;
		Cum.pooledDripDots[0].head.localScale = Vector3.one * 0.01f;
		Cum.pooledDripDots[0].slider = false;
		Cum.pooledDripDots[0].slideNormal = Vector3.up;
		Cum.pooledDripDots[0].color = col;
		Cum.pooledDripDots[0].mat.SetColor("_Color", col);
		Cum.pooledDripDots[0].mat.SetColor("_HColor", col * 0.5f);
		Cum.dripDots.Add(Cum.pooledDripDots[0]);
		Cum.pooledDripDots.RemoveAt(0);
		return Cum.dripDots[Cum.dripDots.Count - 1];
	}

	public static CumDot addCumDot(Vector3 position, Vector3 velocity, float thickness, Color col)
	{
		while (Cum.pooledCumDots.Count < 1)
		{
			Cum.addCumDotToPool();
		}
		while ((Object)Cum.pooledCumDots[0].head == (Object)null)
		{
			Cum.pooledCumDots.RemoveAt(0);
			Cum.addCumDotToPool();
		}
		Cum.pooledCumDots[0].lastPos = position;
		Cum.pooledCumDots[0].position = position;
		Cum.pooledCumDots[0].head.position = Cum.pooledCumDots[0].position;
		Cum.pooledCumDots[0].tail.position = Cum.pooledCumDots[0].position;
		Cum.pooledCumDots[0].velocity = velocity;
		Cum.pooledCumDots[0].finishedTraveling = false;
		Cum.pooledCumDots[0].position = position;
		Cum.pooledCumDots[0].life = Cum.maxCumLife;
		Cum.pooledCumDots[0].link = null;
		Cum.pooledCumDots[0].linkedCumDot = null;
		thickness *= 0.6f;
		Cum.pooledCumDots[0].thickness = thickness;
		Cum.pooledCumDots[0].breaksRemaining = 3;
		Cum.pooledCumDots[0].raycastDelay = 0f;
		Cum.pooledCumDots[0].head.localScale = Vector3.one * (0.02f + Random.value * 0.04f) * Game.cap(thickness, 0.3f, 999f);
		Cum.pooledCumDots[0].color = col;
		Cum.pooledCumDots[0].mat.SetColor("_Color", col);
		Cum.pooledCumDots[0].mat.SetColor("_HColor", col * 0.5f);
		Cum.cumDots.Add(Cum.pooledCumDots[0]);
		Cum.pooledCumDots.RemoveAt(0);
		return Cum.cumDots[Cum.cumDots.Count - 1];
	}
}
