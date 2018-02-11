using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Appendage
{
	public Transform appendageRootBone;

	public Transform attachBone;

	public GameObject appendage;

	public int color;

	public static List<AppendageBundle> assetBundles = new List<AppendageBundle>();

	public bool built;

	public string url;

	public Vector3 v3;

	public AssetBundle bundle;

	public string assetName;

	public RackCharacter owner;

	public List<Rigidbody> rigidbodies = new List<Rigidbody>();

	public List<Vector3> lastPositions = new List<Vector3>();

	private Vector3 lastAttachBonePos = default(Vector3);

	public Appendage(RackCharacter _owner, string _url, string _assetName, Transform _attachBone, int col)
	{
		this.owner = _owner;
		this.assetName = _assetName;
		this.url = _url;
		this.attachBone = _attachBone;
		this.color = col;
	}

	public bool checkLoad()
	{
		if (this.built)
		{
			return this.built;
		}
		bool flag = false;
		int index = -1;
		for (int i = 0; i < Appendage.assetBundles.Count; i++)
		{
			if (Appendage.assetBundles[i].url == this.url)
			{
				flag = true;
				index = i;
			}
		}
		if (flag)
		{
			if (Appendage.assetBundles[index].loaded)
			{
				this.bundle = Appendage.assetBundles[index].bundle;
				this.finishBuildingAppendage();
				this.built = true;
				this.setMaterials();
			}
		}
		else
		{
			Appendage.assetBundles.Add(new AppendageBundle());
			Appendage.assetBundles[Appendage.assetBundles.Count - 1].url = this.url;
			Appendage.assetBundles[Appendage.assetBundles.Count - 1].loaded = false;
			Appendage.assetBundles[Appendage.assetBundles.Count - 1].beginLoad();
		}
		if (!this.built)
		{
			goto IL_0110;
		}
		goto IL_0110;
		IL_0110:
		return this.built;
	}

	public void setMaterials()
	{
		if (this.built)
		{
			Material material = new Material(Game.gameInstance.shader);
			material.CopyPropertiesFromMaterial(Game.gameInstance.defaultMaterial);
			for (int i = 0; i < ((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
			{
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials[i].shader = Game.gameInstance.shader;
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials[i].CopyPropertiesFromMaterial(Game.gameInstance.defaultMaterial);
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials[i].SetTexture("_MainTex", ((Component)this.owner.bodyPiece).GetComponent<Renderer>().material.GetTexture("_MainTex"));
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials[i].SetTexture("_MetallicGlossMap", ((Component)this.owner.bodyPiece).GetComponent<Renderer>().material.GetTexture("_MetallicGlossMap"));
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().materials[i].SetTexture("_EmissionMap", ((Component)this.owner.bodyPiece).GetComponent<Renderer>().material.GetTexture("_EmissionMap"));
				Vector2 vector = default(Vector2);
				Vector2 vector2 = default(Vector2);
				this.owner.getEmbellishmentColorCoords(out vector, out vector2, this.color, false);
				Vector2[] uv = ((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().sharedMesh.uv;
				for (int j = 0; j < uv.Length; j++)
				{
					float y = uv[j].y;
					Vector2 zero = Vector2.zero;
					zero.x = vector.x + (vector2.x - vector.x) * y;
					zero.y = vector.y + (vector2.y - vector.y) * y;
					zero.y = 1f - zero.y;
					uv[j] = zero;
				}
				((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().sharedMesh.uv = uv;
			}
		}
	}

	public void finishBuildingAppendage()
	{
		this.appendage = (GameObject)Object.Instantiate(this.bundle.LoadAsset(this.assetName));
		((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().sharedMesh = Object.Instantiate(((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().sharedMesh);
		Object.Destroy(this.appendage.transform.Find("HEAD_canine").gameObject);
		this.appendage.transform.parent = this.owner.GO.transform;
		this.appendageRootBone = this.appendage.transform.Find("Armature").Find("Head");
		this.appendageRootBone.name = "appendageRootBoneRoot";
		this.appendageRootBone.SetParent(this.attachBone);
		this.appendageRootBone.localPosition = Vector3.zero;
		this.appendageRootBone.localRotation = Quaternion.identity;
		this.appendageRootBone.localScale = Vector3.one;
		this.rigidbodies = new List<Rigidbody>();
		this.lastPositions = new List<Vector3>();
		this.recursiveBuildAppendageBones(this.appendageRootBone, true);
		this.recursiveBuildHairColliders(this.appendageRootBone, true);
		Collider[] componentsInChildren = ((Component)this.appendageRootBone).GetComponentsInChildren<Collider>();
		Collider[] array = new Collider[0];
		if ((Object)this.owner.furniture != (Object)null)
		{
			array = ((Component)this.owner.furniture).GetComponentsInChildren<Collider>().Concat(((Component)this.owner.furniture).GetComponents<Collider>()).ToArray();
		}
		if ((Object)this.owner.apparatus != (Object)null)
		{
			array = ((Component)this.owner.apparatus).GetComponentsInChildren<Collider>().Concat(((Component)this.owner.apparatus).GetComponents<Collider>()).ToArray();
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			for (int j = i + 1; j < componentsInChildren.Length; j++)
			{
				Physics.IgnoreCollision(componentsInChildren[i], componentsInChildren[j]);
			}
			if ((Object)this.owner.furniture != (Object)null)
			{
				for (int k = 0; k < array.Length; k++)
				{
					Physics.IgnoreCollision(componentsInChildren[i], array[k]);
				}
			}
		}
		if (this.owner.isPreviewCharacter)
		{
			Game.recursiveSetLayer(this.appendage, 16);
		}
		else
		{
			Game.recursiveSetLayer(this.appendage, 2);
		}
	}

	public void recursiveBuildAppendageBones(Transform bone, bool root = false)
	{
		if (this.owner.isPreviewCharacter)
		{
			Game.recursiveSetLayer(bone.gameObject, 16);
		}
		else
		{
			Game.recursiveSetLayer(bone.gameObject, 2);
		}
		if (root)
		{
			bone.gameObject.AddComponent<Rigidbody>();
			((Component)bone).GetComponent<Rigidbody>().isKinematic = root;
			((Component)bone).GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
			((Component)bone).GetComponent<Rigidbody>().mass = 10f;
		}
		if (!root && bone.childCount > 0)
		{
			bone.gameObject.AddComponent<Rigidbody>();
			((Component)bone).GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
			((Component)bone).GetComponent<Rigidbody>().mass = 30f;
			((Component)bone).GetComponent<Rigidbody>().useGravity = false;
			this.rigidbodies.Add(((Component)bone).GetComponent<Rigidbody>());
			this.lastPositions.Add(bone.position);
			bone.gameObject.AddComponent<CharacterJoint>();
			((Component)bone).GetComponent<CharacterJoint>().autoConfigureConnectedAnchor = true;
			((Component)bone).GetComponent<CharacterJoint>().connectedBody = ((Component)bone.parent).GetComponent<Rigidbody>();
			((Component)bone).GetComponent<CharacterJoint>().connectedAnchor = ((Component)bone).GetComponent<CharacterJoint>().connectedAnchor;
			SoftJointLimitSpring twistLimitSpring = default(SoftJointLimitSpring);
			SoftJointLimitSpring swingLimitSpring = default(SoftJointLimitSpring);
			SoftJointLimit lowTwistLimit = default(SoftJointLimit);
			SoftJointLimit highTwistLimit = default(SoftJointLimit);
			SoftJointLimit swing1Limit = default(SoftJointLimit);
			SoftJointLimit swing2Limit = default(SoftJointLimit);
			lowTwistLimit.limit = -3f;
			highTwistLimit.limit = 3f;
			swing1Limit.limit = 3f;
			swing2Limit.limit = 3f;
			if (bone.name.IndexOf("floppy") != -1)
			{
				twistLimitSpring.spring = 10f;
				twistLimitSpring.damper = 1f;
				swingLimitSpring.spring = 10f;
				swingLimitSpring.damper = 1f;
				lowTwistLimit.limit = 0f;
				highTwistLimit.limit = 0f;
				swing1Limit.limit = 3f;
				swing2Limit.limit = 3f;
			}
			if (bone.name.IndexOf("stiff") != -1)
			{
				twistLimitSpring.spring = 1000f;
				twistLimitSpring.damper = 100f;
				swingLimitSpring.spring = 1000f;
				swingLimitSpring.damper = 100f;
				lowTwistLimit.limit = 0f;
				highTwistLimit.limit = 0f;
				swing1Limit.limit = 3f;
				swing2Limit.limit = 3f;
			}
			((Component)bone).GetComponent<CharacterJoint>().highTwistLimit = highTwistLimit;
			((Component)bone).GetComponent<CharacterJoint>().lowTwistLimit = lowTwistLimit;
			((Component)bone).GetComponent<CharacterJoint>().swing1Limit = swing1Limit;
			((Component)bone).GetComponent<CharacterJoint>().swing2Limit = swing2Limit;
			((Component)bone).GetComponent<CharacterJoint>().twistLimitSpring = twistLimitSpring;
			((Component)bone).GetComponent<CharacterJoint>().swingLimitSpring = swingLimitSpring;
			((Component)bone).GetComponent<CharacterJoint>().enableProjection = true;
		}
		for (int i = 0; i < bone.childCount; i++)
		{
			this.recursiveBuildAppendageBones(bone.GetChild(i), false);
		}
	}

	public void recursiveBuildHairColliders(Transform bone, bool root = false)
	{
		if (!root && bone.childCount > 0 && bone.name.ToLower().IndexOf("collide") != -1)
		{
			bone.gameObject.AddComponent<CapsuleCollider>();
			((Component)bone).GetComponent<CapsuleCollider>().direction = 0;
			((Component)bone).GetComponent<CapsuleCollider>().radius = 0.03f;
			((Component)bone).GetComponent<CapsuleCollider>().height = (bone.GetChild(0).position - bone.position).magnitude;
			this.v3 = Vector3.zero;
			this.v3.x = (0f - ((Component)bone).GetComponent<CapsuleCollider>().height) / 2f;
			((Component)bone).GetComponent<CapsuleCollider>().center = this.v3;
		}
		for (int i = 0; i < bone.childCount; i++)
		{
			this.recursiveBuildHairColliders(bone.GetChild(i), false);
		}
	}

	public void process()
	{
		this.v3 = this.lastAttachBonePos - this.attachBone.position;
		for (int i = 0; i < this.rigidbodies.Count; i++)
		{
			if (this.v3.magnitude < 2f)
			{
				this.rigidbodies[i].AddForce(this.v3 * 200f, ForceMode.Acceleration);
			}
		}
		this.lastAttachBonePos = this.attachBone.position;
	}

	public void kill()
	{
		try
		{
			if ((Object)((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>() != (Object)null)
			{
				Object.Destroy(((Component)this.appendage.transform.Find("Icosphere")).GetComponent<SkinnedMeshRenderer>().sharedMesh);
			}
			if ((Object)this.appendageRootBone != (Object)null)
			{
				Object.Destroy(this.appendageRootBone.gameObject);
			}
			if ((Object)this.appendage != (Object)null)
			{
				Object.Destroy(this.appendage);
			}
		}
		catch
		{
		}
	}
}
