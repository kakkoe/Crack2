using UnityEngine;

[ExecuteInEditMode]
public class Eraser : Projection
{
	[SerializeField]
	private Texture2D mainTex;

	[SerializeField]
	private float multiplier = 1f;

	[SerializeField]
	private float projectionLimit = 80f;

	private Material renderMaterial;

	private int deferredPass;

	private bool[] buffers;

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

	public float AlphaMultiplier
	{
		get
		{
			return this.multiplier;
		}
		set
		{
			this.multiplier = Mathf.Clamp01(value);
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
			return 1;
		}
	}

	public override bool DeferredPrePass
	{
		get
		{
			return (byte)((base.transparencyType == TransparencyType.Blend) ? 1 : 0) != 0;
		}
	}

	protected override void UpdateMaterialProperties()
	{
		base.UpdateMaterialProperties();
		this.UpdateShape();
		this.UpdateProjectionClipping();
	}

	private void UpdateShape()
	{
		if ((Object)this.mainTex != (Object)null)
		{
			base.materialProperties.SetTexture("_MainTex", this.mainTex);
		}
		base.materialProperties.SetFloat("_Multiplier", this.multiplier * base.AlphaModifier);
	}

	private void UpdateProjectionClipping()
	{
		float value = Mathf.Cos(0.0174532924f * this.projectionLimit);
		base.materialProperties.SetFloat("_NormalCutoff", value);
	}

	protected override void UpdateDeferredRendering()
	{
		if (base.transparencyType == TransparencyType.Blend)
		{
			this.renderMaterial = DynamicDecals.Mat_Eraser;
		}
		else
		{
			this.renderMaterial = DynamicDecals.Mat_EraserCutout;
		}
		if (this.buffers == null || this.buffers.Length != 3)
		{
			this.buffers = new bool[3];
			this.buffers[0] = true;
			this.buffers[1] = true;
			this.buffers[2] = true;
		}
		base.DeferredBuffers = this.buffers;
		base.UpdateDeferredRendering();
	}

	protected override void UpdateForwardRendering(MeshRenderer Renderer)
	{
		Material[] array = new Material[2];
		if (base.transparencyType == TransparencyType.Blend)
		{
			array[0] = new Material(DynamicDecals.Mat_Eraser);
		}
		else
		{
			array[0] = new Material(DynamicDecals.Mat_EraserCutout);
		}
		array[1] = new Material(DynamicDecals.Mat_EraserGrab);
		Renderer.sharedMaterials = array;
		base.UpdateForwardRendering(Renderer);
	}

	public void CopyAllProperties(Eraser Target)
	{
		if ((Object)Target != (Object)null)
		{
			base.CopyBaseProperties(Target);
			this.ProjectionLimit = Target.ProjectionLimit;
			this.mainTex = Target.mainTex;
			this.multiplier = Target.multiplier;
			base.CopyMaskProperties(Target);
		}
		else
		{
			Debug.LogWarning("No Eraser found to copy from");
		}
	}
}
