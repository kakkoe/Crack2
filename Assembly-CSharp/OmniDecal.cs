using UnityEngine;

[ExecuteInEditMode]
public class OmniDecal : Projection
{
	[SerializeField]
	public Texture2D mainTex;

	[SerializeField]
	public Color color = Color.white;

	private bool[] buffers = new bool[4]
	{
		true,
		true,
		false,
		true
	};

	public Texture2D MainTex
	{
		get
		{
			return this.mainTex;
		}
		set
		{
			this.mainTex = value;
			base.UpdateMaterial();
		}
	}

	public Color Color
	{
		get
		{
			return this.color;
		}
		set
		{
			this.color = value;
			base.UpdateMaterial();
		}
	}

	public override Material RenderMaterial
	{
		get
		{
			return (base.transparencyType != TransparencyType.Blend) ? DynamicDecals.Mat_OmniDecalCutout : DynamicDecals.Mat_OmniDecal;
		}
	}

	public override int DeferredPass
	{
		get
		{
			return 1;
		}
	}

	public override bool DeferredPrePass
	{
		get
		{
			return base.transparencyType == TransparencyType.Blend;
		}
	}

	protected override void UpdateMaterialProperties()
	{
		base.UpdateMaterialProperties();
		this.UpdateColor();
	}

	private void UpdateColor()
	{
		if ((Object)this.mainTex != (Object)null)
		{
			base.materialProperties.SetTexture("_MainTex", this.mainTex);
		}
		Color value = this.color;
		value.a *= base.AlphaModifier;
		base.materialProperties.SetColor("_Color", value);
	}

	protected override void UpdateDeferredRendering()
	{
		base.DeferredBuffers = this.buffers;
		base.UpdateDeferredRendering();
	}

	protected override void UpdateForwardRendering(MeshRenderer Renderer)
	{
		if (Renderer.sharedMaterials.Length != 1)
		{
			Renderer.sharedMaterials = new Material[1];
		}
		if (base.transparencyType == TransparencyType.Blend)
		{
			Renderer.sharedMaterial = new Material(DynamicDecals.Mat_OmniDecal);
		}
		else
		{
			Renderer.sharedMaterial = new Material(DynamicDecals.Mat_OmniDecalCutout);
		}
		base.UpdateForwardRendering(Renderer);
	}

	public void CopyAllProperties(OmniDecal Target, bool IncludeTextures = true)
	{
		if ((Object)Target != (Object)null)
		{
			base.CopyBaseProperties(Target);
			this.CopyProperties(Target, IncludeTextures);
			base.CopyMaskProperties(Target);
		}
		else
		{
			Debug.LogWarning("No Decal found to copy from");
		}
	}

	public void CopyProperties(OmniDecal Target, bool IncludeTextures = true)
	{
		if (IncludeTextures)
		{
			this.MainTex = Target.MainTex;
		}
		this.Color = Target.Color;
	}
}
