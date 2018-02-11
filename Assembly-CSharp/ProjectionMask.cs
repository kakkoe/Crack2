using UnityEngine;

[ExecuteInEditMode]
public class ProjectionMask : MonoBehaviour
{
	[SerializeField]
	private bool[] layers = new bool[4];

	private Mesh mesh;

	private Bounds bounds;

	private Renderer source;

	private bool skinned;

	private bool? vertExMotion;

	private Matrix4x4 skinnedMatrix;

	private Vector3 skinnedScale;

	private MaterialPropertyBlock properties;

	private bool changed = true;

	public bool Enabled
	{
		get
		{
			if ((Object)this.source != (Object)null)
			{
				return this.source.enabled;
			}
			return false;
		}
	}

	public bool Layer1
	{
		get
		{
			return this.layers[0];
		}
		set
		{
			this.layers[0] = value;
			this.changed = true;
		}
	}

	public bool Layer2
	{
		get
		{
			return this.layers[1];
		}
		set
		{
			this.layers[1] = value;
			this.changed = true;
		}
	}

	public bool Layer3
	{
		get
		{
			return this.layers[2];
		}
		set
		{
			this.layers[2] = value;
			this.changed = true;
		}
	}

	public bool Layer4
	{
		get
		{
			return this.layers[3];
		}
		set
		{
			this.layers[3] = value;
			this.changed = true;
		}
	}

	public Mesh Mesh
	{
		get
		{
			if (this.skinned)
			{
				if ((Object)this.mesh == (Object)null)
				{
					this.mesh = new Mesh();
				}
				((SkinnedMeshRenderer)this.source).BakeMesh(this.mesh);
			}
			return this.mesh;
		}
	}

	public Bounds Bounds
	{
		get
		{
			return (!((Object)this.source != (Object)null)) ? default(Bounds) : this.source.bounds;
		}
	}

	public Matrix4x4 Matrix
	{
		get
		{
			if (!this.skinned)
			{
				return base.transform.localToWorldMatrix;
			}
			if (this.skinnedScale != base.transform.lossyScale)
			{
				this.skinnedMatrix = Matrix4x4.Inverse(Matrix4x4.Scale(base.transform.lossyScale));
				this.skinnedScale = base.transform.lossyScale;
			}
			return base.transform.localToWorldMatrix * this.skinnedMatrix;
		}
	}

	public bool Skinned
	{
		get
		{
			return this.skinned;
		}
	}

	public bool VertExMotion
	{
		get
		{
			if (!this.vertExMotion.HasValue)
			{
				this.vertExMotion = false;
			}
			return this.vertExMotion.Value;
		}
	}

	public MaterialPropertyBlock Properties
	{
		get
		{
			this.UpdateProperties();
			return this.properties;
		}
	}

	public void Update()
	{
		this.changed = true;
	}

	private void UpdateProperties()
	{
		if (this.properties == null)
		{
			this.properties = new MaterialPropertyBlock();
		}
		if (this.changed || this.VertExMotion)
		{
			if (this.VertExMotion)
			{
				this.source.GetPropertyBlock(this.properties);
			}
			else
			{
				this.properties.Clear();
			}
			this.properties.SetFloat("_Layer1", (float)(this.layers[0] ? 1 : 0));
			this.properties.SetFloat("_Layer2", (float)(this.layers[1] ? 1 : 0));
			this.properties.SetFloat("_Layer3", (float)(this.layers[2] ? 1 : 0));
			this.properties.SetFloat("_Layer4", (float)(this.layers[3] ? 1 : 0));
		}
		this.changed = false;
	}

	private void Start()
	{
		this.Initialize();
		this.Register();
	}

	private void OnEnable()
	{
		this.Initialize();
		this.Register();
	}

	private void OnDisable()
	{
		this.Deregister();
	}

	private void Initialize()
	{
		if ((Object)base.GetComponent<MeshRenderer>() != (Object)null)
		{
			this.mesh = base.GetComponent<MeshFilter>().sharedMesh;
			this.source = base.GetComponent<MeshRenderer>();
			this.skinned = false;
		}
		if ((Object)base.GetComponent<SkinnedMeshRenderer>() != (Object)null)
		{
			this.source = base.GetComponent<SkinnedMeshRenderer>();
			this.skinned = true;
		}
	}

	private void Register()
	{
		DynamicDecals.AddMask(this);
	}

	private void Deregister()
	{
		DynamicDecals.RemoveMask(this);
	}
}
