using UnityEngine;

[ExecuteInEditMode]
public class Decal : Projection
{
	[SerializeField]
	private DecalType decalType;

	[SerializeField]
	private LightingModel lightingModel = LightingModel.PBR;

	[SerializeField]
	private GlossType glossType;

	[SerializeField]
	private Texture2D shapeTex;

	[SerializeField]
	private float shapeMultiplier = 1f;

	[SerializeField]
	private Texture2D albedoTex;

	[SerializeField]
	private Color albedoColor = Color.grey;

	[SerializeField]
	private Texture2D smoothnessTex;

	[SerializeField]
	private float smoothness = 0.2f;

	[SerializeField]
	private Texture2D specularTex;

	[SerializeField]
	private Color specularColor = Color.white;

	[SerializeField]
	private Texture2D metallicTex;

	[SerializeField]
	private float metallicity = 0.5f;

	[SerializeField]
	private Texture2D normalTex;

	[SerializeField]
	private float normalStrength = 1f;

	[SerializeField]
	private bool emissive;

	[SerializeField]
	private Texture2D emissionTex;

	[SerializeField]
	private Color emissionColor = Color.white;

	[SerializeField]
	private float emissionIntensity = 1f;

	[SerializeField]
	private float projectionLimit = 80f;

	private Material renderMaterial;

	private int deferredPass;

	private bool[] buffers;

	public DecalType DecalType
	{
		get
		{
			return this.decalType;
		}
		set
		{
			this.decalType = value;
			base.ReplaceMaterial();
			base.UpdateMaterial();
		}
	}

	public LightingModel LightModel
	{
		get
		{
			return this.lightingModel;
		}
		set
		{
			this.lightingModel = value;
			base.ReplaceMaterial();
			base.UpdateMaterial();
		}
	}

	public GlossType GlossType
	{
		get
		{
			return this.glossType;
		}
		set
		{
			this.glossType = value;
			base.ReplaceMaterial();
			base.UpdateMaterial();
		}
	}

	public bool Emissive
	{
		get
		{
			return this.emissive;
		}
		set
		{
			this.emissive = value;
			base.UpdateMaterial();
		}
	}

	public Texture2D ShapeMap
	{
		get
		{
			return this.shapeTex;
		}
		set
		{
			this.shapeTex = value;
			base.UpdateMaterial();
		}
	}

	public float ShapeMultiplier
	{
		get
		{
			return this.shapeMultiplier;
		}
		set
		{
			this.shapeMultiplier = value;
			base.UpdateMaterial();
		}
	}

	public Texture2D AlbedoMap
	{
		get
		{
			return this.albedoTex;
		}
		set
		{
			this.albedoTex = value;
			base.UpdateMaterial();
		}
	}

	public Color AlbedoColor
	{
		get
		{
			return this.albedoColor;
		}
		set
		{
			this.albedoColor = value;
			base.UpdateMaterial();
		}
	}

	public Texture2D SmoothnessMap
	{
		get
		{
			return this.smoothnessTex;
		}
		set
		{
			this.smoothnessTex = value;
			base.UpdateMaterial();
		}
	}

	public float Smoothness
	{
		get
		{
			return this.smoothness;
		}
		set
		{
			this.smoothness = Mathf.Clamp01(value);
			base.UpdateMaterial();
		}
	}

	public Texture2D MetallicMap
	{
		get
		{
			return this.metallicTex;
		}
		set
		{
			this.metallicTex = value;
			base.UpdateMaterial();
		}
	}

	public float Metallicity
	{
		get
		{
			return this.metallicity;
		}
		set
		{
			this.metallicity = value;
			base.UpdateMaterial();
		}
	}

	public Texture2D SpecularMap
	{
		get
		{
			return this.specularTex;
		}
		set
		{
			this.specularTex = value;
			base.UpdateMaterial();
		}
	}

	public Color SpecularColor
	{
		get
		{
			return this.specularColor;
		}
		set
		{
			this.specularColor = value;
			base.UpdateMaterial();
		}
	}

	public Texture2D NormalMap
	{
		get
		{
			return this.normalTex;
		}
		set
		{
			this.normalTex = value;
			base.UpdateMaterial();
		}
	}

	public float NormalStrength
	{
		get
		{
			return this.normalStrength;
		}
		set
		{
			this.normalStrength = Mathf.Clamp(value, 0f, 4f);
			base.UpdateMaterial();
		}
	}

	public Texture2D EmissionMap
	{
		get
		{
			return this.emissionTex;
		}
		set
		{
			this.emissionTex = value;
			base.UpdateMaterial();
		}
	}

	public Color EmissionColor
	{
		get
		{
			return this.emissionColor;
		}
		set
		{
			this.emissionColor = value;
			base.UpdateMaterial();
		}
	}

	public float EmissionIntensity
	{
		get
		{
			return this.emissionIntensity;
		}
		set
		{
			this.emissionIntensity = value;
			base.UpdateMaterial();
		}
	}

	public float ProjectionLimit
	{
		get
		{
			return this.projectionLimit;
		}
		set
		{
			this.projectionLimit = Mathf.Clamp(value, 0f, 180f);
			base.UpdateMaterial();
		}
	}

	public override Material RenderMaterial
	{
		get
		{
			return this.renderMaterial;
		}
	}

	public override int DeferredPass
	{
		get
		{
			return this.deferredPass;
		}
	}

	public override bool DeferredPrePass
	{
		get
		{
			if (this.DecalType == DecalType.Roughness)
			{
				return true;
			}
			return (byte)((base.transparencyType == TransparencyType.Blend) ? 1 : 0) != 0;
		}
	}

	protected override bool RequiresRenderer
	{
		get
		{
			return this.decalType == DecalType.Full;
		}
	}

	public override void UpdateProjection()
	{
		base.UpdateProjection();
		this.UpdateNormalFlip();
	}

	protected override void UpdateMaterialProperties()
	{
		base.UpdateMaterialProperties();
		this.UpdateProjectionClipping();
		switch (this.decalType)
		{
		case DecalType.Full:
			if (this.lightingModel == LightingModel.PBR)
			{
				this.UpdateGloss();
				this.UpdateNormal();
				this.UpdateEmissive();
			}
			this.UpdateColor();
			break;
		case DecalType.Roughness:
			this.UpdateShape();
			base.materialProperties.SetFloat("_Glossiness", this.smoothness);
			if ((Object)this.smoothnessTex != (Object)null)
			{
				base.materialProperties.SetTexture("_GlossTex", this.smoothnessTex);
			}
			break;
		case DecalType.Normal:
			this.UpdateShape();
			this.UpdateNormal();
			break;
		}
	}

	private void UpdateShape()
	{
		if ((Object)this.shapeTex != (Object)null)
		{
			base.materialProperties.SetTexture("_MainTex", this.shapeTex);
		}
		base.materialProperties.SetFloat("_Multiplier", this.shapeMultiplier * base.AlphaModifier);
	}

	private void UpdateColor()
	{
		if ((Object)this.albedoTex != (Object)null)
		{
			base.materialProperties.SetTexture("_MainTex", this.albedoTex);
		}
		Color value = this.albedoColor;
		value.a *= base.AlphaModifier;
		base.materialProperties.SetColor("_Color", value);
	}

	private void UpdateGloss()
	{
		base.materialProperties.SetFloat("_Glossiness", this.smoothness);
		switch (this.GlossType)
		{
		case GlossType.Metallic:
			if ((Object)this.metallicTex != (Object)null)
			{
				base.materialProperties.SetTexture("_MetallicGlossMap", this.metallicTex);
			}
			else
			{
				base.materialProperties.SetTexture("_MetallicGlossMap", Texture2D.whiteTexture);
			}
			base.materialProperties.SetFloat("_Metallic", this.metallicity);
			break;
		case GlossType.Specular:
			if ((Object)this.specularTex != (Object)null)
			{
				base.materialProperties.SetTexture("_SpecGlossMap", this.specularTex);
			}
			else
			{
				base.materialProperties.SetTexture("_SpecGlossMap", Texture2D.whiteTexture);
			}
			base.materialProperties.SetColor("_SpecColor", this.specularColor);
			break;
		}
	}

	private void UpdateNormal()
	{
		if ((Object)this.normalTex != (Object)null)
		{
			base.materialProperties.SetTexture("_BumpMap", this.normalTex);
		}
		base.materialProperties.SetFloat("_BumpScale", this.normalStrength);
		this.UpdateNormalFlip();
	}

	private void UpdateNormalFlip()
	{
		bool flag = false;
		Vector3 localScale = base.transform.localScale;
		if (Mathf.Sign(localScale.x) == -1f)
		{
			flag = !flag;
		}
		Vector3 localScale2 = base.transform.localScale;
		if (Mathf.Sign(localScale2.y) == -1f)
		{
			flag = !flag;
		}
		Vector3 localScale3 = base.transform.localScale;
		if (Mathf.Sign(localScale3.z) == -1f)
		{
			flag = !flag;
		}
		base.materialProperties.SetFloat("_BumpFlip", (float)(flag ? 1 : 0));
	}

	private void UpdateEmissive()
	{
		if (this.emissive)
		{
			if ((Object)this.emissionTex != (Object)null)
			{
				base.materialProperties.SetTexture("_EmissionMap", this.emissionTex);
			}
			base.materialProperties.SetColor("_EmissionColor", this.emissionColor * this.emissionIntensity);
		}
	}

	private void UpdateProjectionClipping()
	{
		float value = Mathf.Cos(0.0174532924f * this.projectionLimit);
		base.materialProperties.SetFloat("_NormalCutoff", value);
	}

	protected override void UpdateDeferredRendering()
	{
		if (this.buffers == null || this.buffers.Length != 3)
		{
			this.buffers = new bool[3];
		}
		switch (this.decalType)
		{
		case DecalType.Full:
			if (this.lightingModel == LightingModel.Unlit)
			{
				if (base.TransparencyType == TransparencyType.Blend)
				{
					this.renderMaterial = DynamicDecals.Mat_Decal_Unlit;
				}
				else
				{
					this.renderMaterial = DynamicDecals.Mat_Decal_UnlitCutout;
				}
				this.buffers[0] = true;
				this.buffers[1] = true;
				this.buffers[2] = false;
				this.deferredPass = 1;
			}
			else
			{
				if (this.GlossType == GlossType.Metallic)
				{
					if (base.TransparencyType == TransparencyType.Blend)
					{
						this.renderMaterial = DynamicDecals.Mat_Decal_Metallic;
					}
					else
					{
						this.renderMaterial = DynamicDecals.Mat_Decal_MetallicCutout;
					}
				}
				else if (base.TransparencyType == TransparencyType.Blend)
				{
					this.renderMaterial = DynamicDecals.Mat_Decal_Specular;
				}
				else
				{
					this.renderMaterial = DynamicDecals.Mat_Decal_SpecularCutout;
				}
				this.buffers[0] = true;
				this.buffers[1] = true;
				this.buffers[2] = true;
				this.deferredPass = 2;
			}
			break;
		case DecalType.Roughness:
			if (base.TransparencyType == TransparencyType.Blend)
			{
				this.renderMaterial = DynamicDecals.Mat_Decal_Roughness;
			}
			else
			{
				this.renderMaterial = DynamicDecals.Mat_Decal_RoughnessCutout;
			}
			this.buffers[0] = false;
			this.buffers[1] = true;
			this.buffers[2] = false;
			this.deferredPass = 0;
			break;
		case DecalType.Normal:
			if (base.TransparencyType == TransparencyType.Blend)
			{
				this.renderMaterial = DynamicDecals.Mat_Decal_Normal;
			}
			else
			{
				this.renderMaterial = DynamicDecals.Mat_Decal_NormalCutout;
			}
			this.buffers[0] = false;
			this.buffers[1] = false;
			this.buffers[2] = true;
			this.deferredPass = 0;
			break;
		}
		base.DeferredBuffers = this.buffers;
		base.UpdateDeferredRendering();
	}

	protected override void UpdateForwardRendering(MeshRenderer Renderer)
	{
		if (Renderer.sharedMaterials.Length != 1)
		{
			Renderer.sharedMaterials = new Material[1];
		}
		if (this.lightingModel == LightingModel.Unlit)
		{
			if (base.transparencyType == TransparencyType.Blend)
			{
				Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_Unlit);
			}
			else
			{
				Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_UnlitCutout);
			}
		}
		else
		{
			switch (this.glossType)
			{
			case GlossType.Metallic:
				if (base.transparencyType == TransparencyType.Blend)
				{
					Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_Metallic);
				}
				else
				{
					Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_MetallicCutout);
				}
				break;
			case GlossType.Specular:
				if (base.transparencyType == TransparencyType.Blend)
				{
					Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_Specular);
				}
				else
				{
					Renderer.sharedMaterial = new Material(DynamicDecals.Mat_Decal_SpecularCutout);
				}
				break;
			}
		}
		base.UpdateForwardRendering(Renderer);
	}

	public void CopyAllProperties(Decal Target, bool IncludeTextures = true)
	{
		if ((Object)Target != (Object)null)
		{
			base.CopyBaseProperties(Target);
			this.ProjectionLimit = Target.ProjectionLimit;
			this.DecalType = Target.DecalType;
			this.LightModel = Target.LightModel;
			if (IncludeTextures)
			{
				this.ShapeMap = Target.ShapeMap;
			}
			this.ShapeMultiplier = Target.ShapeMultiplier;
			this.CopyAlbedoProperties(Target, IncludeTextures);
			this.CopyGlossProperties(Target, IncludeTextures);
			this.CopyNormalProperties(Target, IncludeTextures);
			this.CopyEmissiveProperties(Target, IncludeTextures);
			base.CopyMaskProperties(Target);
		}
		else
		{
			Debug.LogWarning("No Decal found to copy from");
		}
	}

	public void CopyAlbedoProperties(Decal Target, bool IncludeTextures = true)
	{
		if (IncludeTextures)
		{
			this.AlbedoMap = Target.AlbedoMap;
		}
		this.AlbedoColor = Target.AlbedoColor;
	}

	public void CopyGlossProperties(Decal Target, bool IncludeTextures = true)
	{
		this.Smoothness = Target.Smoothness;
		this.GlossType = Target.GlossType;
		if (IncludeTextures)
		{
			this.MetallicMap = Target.MetallicMap;
		}
		if (IncludeTextures)
		{
			this.MetallicMap = Target.MetallicMap;
		}
		this.Metallicity = Target.Metallicity;
		if (IncludeTextures)
		{
			this.SpecularMap = Target.SpecularMap;
		}
		this.SpecularColor = Target.SpecularColor;
	}

	public void CopyNormalProperties(Decal Target, bool IncludeTextures = true)
	{
		if (IncludeTextures)
		{
			this.NormalMap = Target.NormalMap;
		}
		this.NormalStrength = Target.NormalStrength;
	}

	public void CopyEmissiveProperties(Decal Target, bool IncludeTextures = true)
	{
		this.Emissive = Target.Emissive;
		if (this.Emissive)
		{
			if (IncludeTextures)
			{
				this.EmissionMap = Target.EmissionMap;
			}
			this.EmissionColor = Target.EmissionColor;
			this.EmissionIntensity = Target.EmissionIntensity;
		}
	}
}
