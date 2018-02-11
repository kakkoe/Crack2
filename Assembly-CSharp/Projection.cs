using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public abstract class Projection : MonoBehaviour
{
	[SerializeField]
	private int priority;

	[SerializeField]
	protected TransparencyType transparencyType;

	[SerializeField]
	private float cutoff = 0.2f;

	[SerializeField]
	private MaskMethod maskMethod;

	[SerializeField]
	private bool[] masks = new bool[4];

	private Color MaskLayers;

	private PoolItem poolItem;

	private float scaleModifier = 1f;

	private float alphaModifier = 1f;

	private Visibility visibility;

	protected Transform forwardRenderer;

	private RenderTargetIdentifier[] deferredTargets;

	private RenderTargetIdentifier[] deferredHDRTargets;

	private bool replaceDeferred = true;

	private bool replaceForward = true;

	private bool updateMaterial = true;

	protected MaterialPropertyBlock materialProperties;

	public int Priority
	{
		get
		{
			return this.priority;
		}
		set
		{
			this.priority = value;
			this.Reprioritise();
		}
	}

	public TransparencyType TransparencyType
	{
		get
		{
			return this.transparencyType;
		}
		set
		{
			this.transparencyType = value;
			switch (this.transparencyType)
			{
			case TransparencyType.Cutout:
				this.cutoff = 0.2f;
				break;
			}
			this.ReplaceMaterial();
			this.UpdateMaterial();
		}
	}

	public float AlphaCutoff
	{
		get
		{
			return this.cutoff;
		}
		set
		{
			this.cutoff = Mathf.Clamp01(value);
			this.UpdateMaterial();
		}
	}

	public MaskMethod MaskMethod
	{
		get
		{
			return this.maskMethod;
		}
		set
		{
			this.maskMethod = value;
			this.UpdateMaterial();
		}
	}

	public bool MaskLayer1
	{
		get
		{
			return this.masks[0];
		}
		set
		{
			this.masks[0] = value;
			this.UpdateMaterial();
		}
	}

	public bool MaskLayer2
	{
		get
		{
			return this.masks[1];
		}
		set
		{
			this.masks[1] = value;
			this.UpdateMaterial();
		}
	}

	public bool MaskLayer3
	{
		get
		{
			return this.masks[2];
		}
		set
		{
			this.masks[2] = value;
			this.UpdateMaterial();
		}
	}

	public bool MaskLayer4
	{
		get
		{
			return this.masks[3];
		}
		set
		{
			this.masks[3] = value;
			this.UpdateMaterial();
		}
	}

	public PoolItem PoolItem
	{
		get
		{
			return this.poolItem;
		}
		set
		{
			this.poolItem = value;
		}
	}

	public float ScaleModifier
	{
		get
		{
			return this.scaleModifier;
		}
		set
		{
			this.scaleModifier = value;
		}
	}

	public float AlphaModifier
	{
		get
		{
			return this.alphaModifier;
		}
		set
		{
			this.alphaModifier = Mathf.Clamp01(value);
			this.UpdateMaterial();
		}
	}

	public bool Visible
	{
		get
		{
			if ((UnityEngine.Object)this != (UnityEngine.Object)null && DynamicDecals.RenderingMethod != RenderingMethod.Deferred)
			{
				MeshRenderer component = base.GetComponent<MeshRenderer>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					return component.isVisible;
				}
				return true;
			}
			switch (this.visibility)
			{
			case Visibility.Visible:
				return true;
			case Visibility.NotVisible:
				return false;
			default:
				return true;
			}
		}
	}

	public Matrix4x4 RenderMatrix
	{
		get
		{
			if (this.scaleModifier == 1f)
			{
				return base.transform.localToWorldMatrix;
			}
			return base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(this.scaleModifier, this.scaleModifier, this.scaleModifier));
		}
	}

	public abstract Material RenderMaterial
	{
		get;
	}

	public abstract int DeferredPass
	{
		get;
	}

	public abstract bool DeferredPrePass
	{
		get;
	}

	public bool[] DeferredBuffers
	{
		get;
		set;
	}

	public RenderTargetIdentifier[] DeferredTargets
	{
		get
		{
			if (this.deferredTargets == null || this.deferredTargets.Length < 1)
			{
				this.deferredTargets = (RenderTargetIdentifier[])DynamicDecals.PassesToTargets(this.DeferredBuffers, false).Clone();
			}
			return this.deferredTargets;
		}
		set
		{
			this.deferredTargets = value;
		}
	}

	public RenderTargetIdentifier[] DeferredHDRTargets
	{
		get
		{
			if (this.deferredHDRTargets == null || this.deferredHDRTargets.Length < 1)
			{
				this.deferredHDRTargets = (RenderTargetIdentifier[])DynamicDecals.PassesToTargets(this.DeferredBuffers, true).Clone();
			}
			return this.deferredHDRTargets;
		}
		set
		{
			this.deferredHDRTargets = value;
		}
	}

	public float timeID
	{
		get;
		private set;
	}

	public MaterialPropertyBlock MaterialProperties
	{
		get
		{
			if (this.updateMaterial)
			{
				this.UpdateMaterialProperties();
			}
			this.updateMaterial = false;
			return this.materialProperties;
		}
	}

	protected virtual bool RequiresRenderer
	{
		get
		{
			return true;
		}
	}

	private void Start()
	{
		this.UpdateMaterialImmeditately();
		this.UpdateRenderer();
		this.Register();
	}

	private void OnEnable()
	{
		this.UpdateMaterialImmeditately();
		this.UpdateRenderer();
		this.Register();
	}

	private void OnDisable()
	{
		this.DestroyRenderer(false);
		this.Deregister();
	}

	private void Register()
	{
		this.timeID = Time.timeSinceLevelLoad;
		DynamicDecals.AddProjection(this);
	}

	private void Deregister()
	{
		DynamicDecals.RemoveProjection(this);
	}

	public void Reprioritise()
	{
		this.Reprioritise(false);
	}

	public void Reprioritise(bool DelayedSort)
	{
		if (DelayedSort)
		{
			DynamicDecals.Sort();
		}
		else
		{
			this.Deregister();
			this.Register();
		}
	}

	public virtual void UpdateProjection()
	{
		if ((UnityEngine.Object)base.gameObject != (UnityEngine.Object)null)
		{
			this.visibility = Visibility.Unknown;
			if (!this.UpdateRenderer())
			{
				this.DestroyRenderer(true);
			}
		}
	}

	public void ReplaceMaterial()
	{
		this.replaceDeferred = true;
		this.replaceForward = true;
	}

	public void UpdateMaterialImmeditately()
	{
		this.UpdateMaterialProperties();
		this.updateMaterial = false;
	}

	public void UpdateMaterial()
	{
		this.updateMaterial = true;
	}

	protected virtual void UpdateMaterialProperties()
	{
		if (this.materialProperties == null)
		{
			this.materialProperties = new MaterialPropertyBlock();
		}
		else
		{
			this.materialProperties.Clear();
		}
		this.UpdateTransparency();
		this.UpdateMasking();
		if (this.replaceDeferred)
		{
			this.UpdateDeferredRendering();
			this.replaceDeferred = false;
		}
	}

	private void UpdateTransparency()
	{
		this.materialProperties.SetFloat("_Cutoff", this.cutoff);
	}

	private void UpdateMasking()
	{
		switch (this.maskMethod)
		{
		case MaskMethod.DrawOnEverythingExcept:
			this.materialProperties.SetFloat("_MaskBase", 1f);
			this.MaskLayers.r = ((!this.masks[0]) ? 0.5f : 0f);
			this.MaskLayers.g = ((!this.masks[1]) ? 0.5f : 0f);
			this.MaskLayers.b = ((!this.masks[2]) ? 0.5f : 0f);
			this.MaskLayers.a = ((!this.masks[3]) ? 0.5f : 0f);
			this.materialProperties.SetVector("_MaskLayers", this.MaskLayers);
			break;
		case MaskMethod.OnlyDrawOn:
			this.materialProperties.SetFloat("_MaskBase", 0f);
			this.MaskLayers.r = ((!this.masks[0]) ? 0.5f : 1f);
			this.MaskLayers.g = ((!this.masks[1]) ? 0.5f : 1f);
			this.MaskLayers.b = ((!this.masks[2]) ? 0.5f : 1f);
			this.MaskLayers.a = ((!this.masks[3]) ? 0.5f : 1f);
			this.materialProperties.SetVector("_MaskLayers", this.MaskLayers);
			break;
		}
	}

	protected virtual void UpdateDeferredRendering()
	{
		this.DeferredTargets = (RenderTargetIdentifier[])DynamicDecals.PassesToTargets(this.DeferredBuffers, false).Clone();
		this.DeferredHDRTargets = (RenderTargetIdentifier[])DynamicDecals.PassesToTargets(this.DeferredBuffers, true).Clone();
	}

	protected virtual void UpdateForwardRendering(MeshRenderer Renderer)
	{
		Renderer.sharedMaterial.renderQueue = 2455 + this.Priority;
	}

	private bool UpdateRenderer()
	{
		if (DynamicDecals.RenderingMethod != RenderingMethod.Deferred && this.RequiresRenderer)
		{
			MeshRenderer meshRenderer = null;
			if ((UnityEngine.Object)this.forwardRenderer == (UnityEngine.Object)null)
			{
				IEnumerator enumerator = base.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						if (transform.name == "Forward Renderer")
						{
							this.forwardRenderer = transform;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				if ((UnityEngine.Object)this.forwardRenderer == (UnityEngine.Object)null)
				{
					this.forwardRenderer = new GameObject("Forward Renderer").transform;
					this.forwardRenderer.SetParent(base.transform, false);
					this.forwardRenderer.gameObject.layer = base.gameObject.layer;
					MeshFilter meshFilter = this.forwardRenderer.gameObject.AddComponent<MeshFilter>();
					meshFilter.mesh = DynamicDecals.Cube;
					meshRenderer = this.forwardRenderer.gameObject.AddComponent<MeshRenderer>();
					meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			if ((UnityEngine.Object)meshRenderer == (UnityEngine.Object)null)
			{
				meshRenderer = ((Component)this.forwardRenderer).GetComponent<MeshRenderer>();
			}
			if (meshRenderer.sharedMaterials.Length == 0 || (UnityEngine.Object)meshRenderer.sharedMaterial == (UnityEngine.Object)null || this.replaceForward)
			{
				this.DestroyMaterials(meshRenderer);
				this.UpdateForwardRendering(meshRenderer);
				this.replaceForward = false;
			}
			meshRenderer.SetPropertyBlock(this.MaterialProperties);
			float num = Mathf.Clamp(this.scaleModifier, 1E-08f, 1E+09f);
			this.forwardRenderer.localScale = new Vector3(num, num, num);
			return true;
		}
		return false;
	}

	private void DestroyRenderer(bool ForceDestroy = true)
	{
		if ((UnityEngine.Object)this.forwardRenderer != (UnityEngine.Object)null)
		{
			this.DestroyMaterials(((Component)this.forwardRenderer).GetComponent<MeshRenderer>());
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this.forwardRenderer.gameObject);
			}
			else if (ForceDestroy)
			{
				UnityEngine.Object.DestroyImmediate(this.forwardRenderer.gameObject, true);
			}
		}
	}

	private void DestroyMaterials(MeshRenderer Renderer)
	{
		if (Renderer.sharedMaterials.Length > 0)
		{
			for (int i = 0; i < Renderer.sharedMaterials.Length; i++)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(Renderer.sharedMaterials[i]);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(Renderer.sharedMaterials[i], true);
				}
			}
		}
	}

	public void SetVisibility(bool Visible)
	{
		if (!Visible && this.visibility == Visibility.Unknown)
		{
			this.visibility = Visibility.NotVisible;
		}
		if (Visible)
		{
			this.visibility = Visibility.Visible;
		}
	}

	public void Fade(FadeMethod Method, float InDuration, float Delay, float OutDuration)
	{
		if (this.poolItem != null)
		{
			this.poolItem.Fade(Method, InDuration, Delay, OutDuration);
			float num = 1f;
			num = ((!(InDuration > 0f)) ? 1f : 0f);
			if (Method == FadeMethod.Alpha || Method == FadeMethod.Both)
			{
				this.AlphaModifier = num;
			}
			if (Method == FadeMethod.Scale || Method == FadeMethod.Both)
			{
				this.ScaleModifier = num;
			}
			this.UpdateMaterialImmeditately();
		}
	}

	public void Culled(CullMethod Method, float Duration)
	{
		if (this.poolItem != null)
		{
			this.poolItem.Culled(Method, Duration);
		}
	}

	public void Return()
	{
		if (this.poolItem != null)
		{
			this.poolItem.Return();
		}
	}

	public float CheckIntersecting(Vector3 Point)
	{
		Vector3 vector = base.transform.InverseTransformPoint(Point);
		return Mathf.Clamp01(2f * (0.5f - Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z))));
	}

	public void CopyBaseProperties(Projection Target)
	{
		this.Priority = Target.Priority;
		base.transform.localScale = Target.transform.localScale;
		this.TransparencyType = Target.TransparencyType;
		this.AlphaCutoff = Target.AlphaCutoff;
	}

	public void CopyMaskProperties(Projection Target)
	{
		this.MaskMethod = Target.MaskMethod;
		this.MaskLayer1 = Target.MaskLayer1;
		this.MaskLayer2 = Target.MaskLayer2;
		this.MaskLayer3 = Target.MaskLayer3;
		this.MaskLayer4 = Target.MaskLayer4;
	}
}
