using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
[RequireComponent(typeof(Camera))]
[HelpURL("http://www.thomashourdel.com/ssaopro/doc/")]
[AddComponentMenu("Image Effects/SSAO Pro")]
public class SSAOPro : MonoBehaviour
{
	public enum BlurMode
	{
		None,
		Gaussian,
		HighQualityBilateral
	}

	public enum SampleCount
	{
		VeryLow,
		Low,
		Medium,
		High,
		Ultra
	}

	protected enum Pass
	{
		Clear,
		GaussianBlur = 5,
		HighQualityBilateralBlur,
		Composite
	}

	public Texture2D NoiseTexture;

	public bool UseHighPrecisionDepthMap;

	public SampleCount Samples = SampleCount.Medium;

	[Range(1f, 4f)]
	public int Downsampling = 1;

	[Range(0.01f, 1.25f)]
	public float Radius = 0.12f;

	[Range(0f, 16f)]
	public float Intensity = 2.5f;

	[Range(0f, 10f)]
	public float Distance = 1f;

	[Range(0f, 1f)]
	public float Bias = 0.1f;

	[Range(0f, 1f)]
	public float LumContribution = 0.5f;

	[ColorUsage(false)]
	public Color OcclusionColor = Color.black;

	public float CutoffDistance = 150f;

	public float CutoffFalloff = 50f;

	public BlurMode Blur = BlurMode.HighQualityBilateral;

	public bool BlurDownsampling;

	[Range(1f, 4f)]
	public int BlurPasses = 1;

	[Range(1f, 20f)]
	public float BlurBilateralThreshold = 10f;

	public bool DebugAO;

	protected Shader m_ShaderSSAO;

	protected Material m_Material;

	protected Camera m_Camera;

	public Material Material
	{
		get
		{
			if ((Object)this.m_Material == (Object)null)
			{
				this.m_Material = new Material(this.ShaderSSAO)
				{
					hideFlags = HideFlags.HideAndDontSave
				};
			}
			return this.m_Material;
		}
	}

	public Shader ShaderSSAO
	{
		get
		{
			if ((Object)this.m_ShaderSSAO == (Object)null)
			{
				this.m_ShaderSSAO = Shader.Find("Hidden/SSAO Pro V2");
			}
			return this.m_ShaderSSAO;
		}
	}

	private void OnEnable()
	{
		this.m_Camera = base.GetComponent<Camera>();
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarning("Image Effects are not supported on this device.");
			base.enabled = false;
		}
		else if ((Object)this.ShaderSSAO == (Object)null)
		{
			Debug.LogWarning("Missing shader (SSAO).");
			base.enabled = false;
		}
		else if (!this.ShaderSSAO.isSupported)
		{
			Debug.LogWarning("Unsupported shader (SSAO).");
			base.enabled = false;
		}
		else if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			Debug.LogWarning("Depth textures aren't supported on this device.");
			base.enabled = false;
		}
	}

	private void OnPreRender()
	{
		this.m_Camera.depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.DepthNormals);
	}

	private void OnDisable()
	{
		if ((Object)this.m_Material != (Object)null)
		{
			Object.DestroyImmediate(this.m_Material);
		}
		this.m_Material = null;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if ((Object)this.ShaderSSAO == (Object)null || Mathf.Approximately(this.Intensity, 0f))
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.Material.shaderKeywords = null;
			switch (this.Samples)
			{
			case SampleCount.Low:
				this.Material.EnableKeyword("SAMPLES_LOW");
				break;
			case SampleCount.Medium:
				this.Material.EnableKeyword("SAMPLES_MEDIUM");
				break;
			case SampleCount.High:
				this.Material.EnableKeyword("SAMPLES_HIGH");
				break;
			case SampleCount.Ultra:
				this.Material.EnableKeyword("SAMPLES_ULTRA");
				break;
			}
			int num = 0;
			if ((Object)this.NoiseTexture != (Object)null)
			{
				num = 1;
			}
			if (!Mathf.Approximately(this.LumContribution, 0f))
			{
				num += 2;
			}
			num++;
			this.Material.SetMatrix("_InverseViewProject", (this.m_Camera.projectionMatrix * this.m_Camera.worldToCameraMatrix).inverse);
			this.Material.SetMatrix("_CameraModelView", this.m_Camera.cameraToWorldMatrix);
			this.Material.SetTexture("_NoiseTex", this.NoiseTexture);
			this.Material.SetVector("_Params1", new Vector4((!((Object)this.NoiseTexture == (Object)null)) ? ((float)this.NoiseTexture.width) : 0f, this.Radius, this.Intensity, this.Distance));
			this.Material.SetVector("_Params2", new Vector4(this.Bias, this.LumContribution, this.CutoffDistance, this.CutoffFalloff));
			this.Material.SetColor("_OcclusionColor", this.OcclusionColor);
			if (this.Blur == BlurMode.None)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(source.width / this.Downsampling, source.height / this.Downsampling, 0, RenderTextureFormat.ARGB32);
				Graphics.Blit(temporary, temporary, this.Material, 0);
				if (this.DebugAO)
				{
					Graphics.Blit(source, temporary, this.Material, num);
					Graphics.Blit(temporary, destination);
					RenderTexture.ReleaseTemporary(temporary);
				}
				else
				{
					Graphics.Blit(source, temporary, this.Material, num);
					this.Material.SetTexture("_SSAOTex", temporary);
					Graphics.Blit(source, destination, this.Material, 7);
					RenderTexture.ReleaseTemporary(temporary);
				}
			}
			else
			{
				Pass pass = (Pass)((this.Blur != BlurMode.HighQualityBilateral) ? 5 : 6);
				int num2 = (!this.BlurDownsampling) ? 1 : this.Downsampling;
				RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.ARGB32);
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / this.Downsampling, source.height / this.Downsampling, 0, RenderTextureFormat.ARGB32);
				Graphics.Blit(temporary2, temporary2, this.Material, 0);
				Graphics.Blit(source, temporary2, this.Material, num);
				this.Material.SetFloat("_BilateralThreshold", this.BlurBilateralThreshold * 5f);
				for (int i = 0; i < this.BlurPasses; i++)
				{
					this.Material.SetVector("_Direction", new Vector2(1f / (float)source.width, 0f));
					Graphics.Blit(temporary2, temporary3, this.Material, (int)pass);
					temporary2.DiscardContents();
					this.Material.SetVector("_Direction", new Vector2(0f, 1f / (float)source.height));
					Graphics.Blit(temporary3, temporary2, this.Material, (int)pass);
					temporary3.DiscardContents();
				}
				if (!this.DebugAO)
				{
					this.Material.SetTexture("_SSAOTex", temporary2);
					Graphics.Blit(source, destination, this.Material, 7);
				}
				else
				{
					Graphics.Blit(temporary2, destination);
				}
				RenderTexture.ReleaseTemporary(temporary2);
				RenderTexture.ReleaseTemporary(temporary3);
			}
		}
	}
}
