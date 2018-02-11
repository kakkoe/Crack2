using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class DynamicDecals
{
	internal class CameraData
	{
		public RenderingMethod method;

		public CommandBuffer maskBuffer;

		public CommandBuffer projectionBuffer;

		public CullingGroup maskCulling;

		public CullingGroup projectionCulling;

		public bool sceneCamera;

		public bool previewCamera;

		public CustomDepthTextureMode customDTM;

		public DepthTextureMode? originalDTM;

		public DepthTextureMode? desiredDTM;

		public CameraData(Camera Camera, RenderingMethod Method, CommandBuffer MaskBuffer, CommandBuffer ProjectionBuffer, CullingGroup MaskCulling, CullingGroup ProjectionCulling)
		{
			this.method = Method;
			this.maskBuffer = MaskBuffer;
			this.projectionBuffer = ProjectionBuffer;
			this.maskCulling = MaskCulling;
			this.projectionCulling = ProjectionCulling;
			this.sceneCamera = (Camera.name == "SceneCamera");
			this.previewCamera = (Camera.name == "Preview Camera");
		}
	}

	private static DynamicDecals system;

	private DynamicDecalSettings settings;

	private static RenderingMethod renderingMethod;

	private static Camera customCamera;

	private bool sceneFocus;

	private bool cameraClipping;

	private Shader normalShader;

	private Material deferredBlit;

	private Material mask;

	private Material vertExMask;

	private Material metallic;

	private Material metallicCutout;

	private Material specular;

	private Material specularCutout;

	private Material unlit;

	private Material unlitCutout;

	private Material roughness;

	private Material roughnessCutout;

	private Material normal;

	private Material normalCutout;

	private Material omnidecal;

	private Material omnidecalCutout;

	private Material eraser;

	private Material eraserCutout;

	private Material eraserGrab;

	private Mesh cube;

	private Mesh cameraBlit;

	private bool sort;

	private List<Projection> projections;

	private BoundingSphere[] projectionSpheres;

	private int staticCount;

	private List<ProjectionMask> masks;

	private BoundingSphere[] maskSpheres;

	internal static Dictionary<Camera, CameraData> cameraData = new Dictionary<Camera, CameraData>();

	public static Rect FullRect = new Rect(0f, 0f, 1f, 1f);

	private static Dictionary<int, ProjectionPool> Pools;

	private bool initialized;

	private bool updated;

	private static RenderTargetIdentifier[] one = new RenderTargetIdentifier[1];

	private static RenderTargetIdentifier[] two = new RenderTargetIdentifier[2];

	private static RenderTargetIdentifier[] three = new RenderTargetIdentifier[3];

	private static RenderTargetIdentifier[] four = new RenderTargetIdentifier[4];

	private static int[] staticRts;

	private static int[] dynamicRts;

	[CompilerGenerated]
	private static Camera.CameraCallback _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Camera.CameraCallback _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static Camera.CameraCallback _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static Camera.CameraCallback _003C_003Ef__mg_0024cache3;

	private static DynamicDecals System
	{
		get
		{
			if (DynamicDecals.system == null)
			{
				DynamicDecals.system = new DynamicDecals();
			}
			return DynamicDecals.system;
		}
	}

	public static DynamicDecalSettings Settings
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.settings == (UnityEngine.Object)null)
			{
				DynamicDecals.System.settings = Resources.Load<DynamicDecalSettings>("Settings");
			}
			if ((UnityEngine.Object)DynamicDecals.System.settings == (UnityEngine.Object)null)
			{
				DynamicDecals.System.settings = ScriptableObject.CreateInstance<DynamicDecalSettings>();
			}
			return DynamicDecals.System.settings;
		}
	}

	public static RenderingMethod RenderingMethod
	{
		get
		{
			return DynamicDecals.renderingMethod;
		}
		private set
		{
			DynamicDecals.renderingMethod = value;
		}
	}

	private static Camera CustomCamera
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.customCamera == (UnityEngine.Object)null)
			{
				DynamicDecals.customCamera = new GameObject("Custom Camera").AddComponent<Camera>();
				DynamicDecals.customCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
				DynamicDecals.customCamera.enabled = false;
			}
			return DynamicDecals.customCamera;
		}
	}

	private static CameraEvent DeferredMaskEvent
	{
		get
		{
			return CameraEvent.BeforeReflections;
		}
	}

	private static CameraEvent DeferredProjectionEvent
	{
		get
		{
			return CameraEvent.BeforeReflections;
		}
	}

	public static Shader NormalShader
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.normalShader == (UnityEngine.Object)null)
			{
				DynamicDecals.System.normalShader = Shader.Find("Decal/Internal/DepthTexture/Normal");
			}
			return DynamicDecals.System.normalShader;
		}
	}

	public static Material Mat_DeferredBlit
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.deferredBlit == (UnityEngine.Object)null)
			{
				DynamicDecals.System.deferredBlit = new Material(Shader.Find("Decal/Internal/Blit"));
			}
			return DynamicDecals.System.deferredBlit;
		}
	}

	public static Material Mat_Mask
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.mask == (UnityEngine.Object)null)
			{
				DynamicDecals.System.mask = new Material(Shader.Find("Decal/Internal/Mask"));
			}
			return DynamicDecals.System.mask;
		}
	}

	public static Material Mat_VertExMask
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.vertExMask == (UnityEngine.Object)null)
			{
				DynamicDecals.System.vertExMask = new Material(Shader.Find("Decal/Internal/VertExmotion/Mask"));
			}
			return DynamicDecals.System.vertExMask;
		}
	}

	public static Material Mat_Decal_Metallic
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.metallic == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Metallic");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.metallic = new Material(shader);
				DynamicDecals.System.metallic.DisableKeyword("_AlphaTest");
				DynamicDecals.System.metallic.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.metallic;
		}
	}

	public static Material Mat_Decal_MetallicCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.metallicCutout == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Metallic");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.metallicCutout = new Material(shader);
				DynamicDecals.System.metallicCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.metallicCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.metallicCutout;
		}
	}

	public static Material Mat_Decal_Specular
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.specular == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Specular");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.specular = new Material(shader);
				DynamicDecals.System.specular.DisableKeyword("_AlphaTest");
				DynamicDecals.System.specular.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.specular;
		}
	}

	public static Material Mat_Decal_SpecularCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.specularCutout == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Specular");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.specularCutout = new Material(shader);
				DynamicDecals.System.specularCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.specularCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.specularCutout;
		}
	}

	public static Material Mat_Decal_Unlit
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.unlit == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Unlit");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.unlit = new Material(shader);
				DynamicDecals.System.unlit.DisableKeyword("_AlphaTest");
				DynamicDecals.System.unlit.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.unlit;
		}
	}

	public static Material Mat_Decal_UnlitCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.unlitCutout == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Unlit");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.unlitCutout = new Material(shader);
				DynamicDecals.System.unlitCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.unlitCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.unlitCutout;
		}
	}

	public static Material Mat_Decal_Roughness
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.roughness == (UnityEngine.Object)null)
			{
				DynamicDecals.System.roughness = new Material(Shader.Find("Decal/Roughness"));
				DynamicDecals.System.roughness.DisableKeyword("_AlphaTest");
				DynamicDecals.System.roughness.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.roughness;
		}
	}

	public static Material Mat_Decal_RoughnessCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.roughnessCutout == (UnityEngine.Object)null)
			{
				DynamicDecals.System.roughnessCutout = new Material(Shader.Find("Decal/Roughness"));
				DynamicDecals.System.roughnessCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.roughnessCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.roughnessCutout;
		}
	}

	public static Material Mat_Decal_Normal
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.normal == (UnityEngine.Object)null)
			{
				DynamicDecals.System.normal = new Material(Shader.Find("Decal/Normal"));
				DynamicDecals.System.normal.DisableKeyword("_AlphaTest");
				DynamicDecals.System.normal.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.normal;
		}
	}

	public static Material Mat_Decal_NormalCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.normalCutout == (UnityEngine.Object)null)
			{
				DynamicDecals.System.normalCutout = new Material(Shader.Find("Decal/Normal"));
				DynamicDecals.System.normalCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.normalCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.normalCutout;
		}
	}

	public static Material Mat_OmniDecal
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.omnidecal == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/OmniDecal");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.omnidecal = new Material(shader);
				DynamicDecals.System.omnidecal.DisableKeyword("_AlphaTest");
				DynamicDecals.System.omnidecal.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.omnidecal;
		}
	}

	public static Material Mat_OmniDecalCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.omnidecalCutout == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/OmniDecal");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.omnidecalCutout = new Material(shader);
				DynamicDecals.System.omnidecalCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.omnidecalCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.omnidecalCutout;
		}
	}

	public static Material Mat_Eraser
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.eraser == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Eraser/Read");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.eraser = new Material(shader);
				DynamicDecals.System.eraser.DisableKeyword("_AlphaTest");
				DynamicDecals.System.eraser.EnableKeyword("_Blend");
			}
			return DynamicDecals.System.eraser;
		}
	}

	public static Material Mat_EraserCutout
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.eraserCutout == (UnityEngine.Object)null)
			{
				Shader shader = Shader.Find("Decal/Eraser/Read");
				if (DynamicDecals.Settings.forceForward)
				{
					shader.maximumLOD = 0;
				}
				else
				{
					shader.maximumLOD = 1000;
				}
				DynamicDecals.System.eraserCutout = new Material(shader);
				DynamicDecals.System.eraserCutout.EnableKeyword("_AlphaTest");
				DynamicDecals.System.eraserCutout.DisableKeyword("_Blend");
			}
			return DynamicDecals.System.eraserCutout;
		}
	}

	public static Material Mat_EraserGrab
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.eraserGrab == (UnityEngine.Object)null)
			{
				DynamicDecals.System.eraserGrab = new Material(Shader.Find("Decal/Eraser/Write"));
			}
			return DynamicDecals.System.eraserGrab;
		}
	}

	public static Mesh Cube
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.cube == (UnityEngine.Object)null)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
				DynamicDecals.System.cube = gameObject.GetComponent<MeshFilter>().sharedMesh;
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			return DynamicDecals.System.cube;
		}
	}

	public static Mesh CameraBlit
	{
		get
		{
			if ((UnityEngine.Object)DynamicDecals.System.cameraBlit == (UnityEngine.Object)null)
			{
				DynamicDecals.System.cameraBlit = UnityEngine.Object.Instantiate(DynamicDecals.Cube);
				DynamicDecals.ScaleMesh(DynamicDecals.System.cameraBlit, 100f);
			}
			return DynamicDecals.System.cameraBlit;
		}
	}

	public static bool StaticPass
	{
		get
		{
			return (byte)((DynamicDecals.System.staticCount > 0) ? 1 : 0) != 0;
		}
	}

	~DynamicDecals()
	{
		if (DynamicDecals.system.initialized)
		{
			Debug.LogWarning("System shutting down via Deconstructor. Forcing Uninitialization");
		}
		DynamicDecals.Uninitialize(true);
	}

	private static void CheckRenderingPath()
	{
		Camera camera = null;
		if ((UnityEngine.Object)Camera.main != (UnityEngine.Object)null)
		{
			camera = Camera.main;
		}
		else if ((UnityEngine.Object)Camera.current != (UnityEngine.Object)null)
		{
			camera = Camera.current;
		}
		if ((UnityEngine.Object)camera != (UnityEngine.Object)null)
		{
			if (camera.actualRenderingPath == RenderingPath.Forward || camera.actualRenderingPath == RenderingPath.DeferredShading)
			{
				if (camera.actualRenderingPath == RenderingPath.DeferredShading)
				{
					if (DynamicDecals.Settings.forceForward)
					{
						DynamicDecals.RenderingMethod = RenderingMethod.ForwardForced;
					}
					else
					{
						DynamicDecals.RenderingMethod = RenderingMethod.Deferred;
					}
				}
				else if (DynamicDecals.Settings.highPrecision)
				{
					DynamicDecals.RenderingMethod = RenderingMethod.ForwardHigh;
				}
				else
				{
					DynamicDecals.RenderingMethod = RenderingMethod.ForwardLow;
				}
			}
			else
			{
				Debug.LogWarning("Current Rendering Path not supported! Please use either Forward or Deferred");
			}
		}
	}

	private static void UpdateRenderingPath(Camera Camera, CameraData Data)
	{
		if (Data.method != DynamicDecals.RenderingMethod)
		{
			Camera.RemoveAllCommandBuffers();
			Data.method = DynamicDecals.RenderingMethod;
			DynamicDecals.SetRenderingMethod(Camera, Data);
		}
	}

	private static void SetRenderingMethod(Camera Camera, CameraData Data)
	{
		RenderingMethod renderingMethod = Data.method;
		if (renderingMethod == RenderingMethod.ForwardLow && (Data.sceneCamera || Data.previewCamera))
		{
			renderingMethod = RenderingMethod.ForwardHigh;
		}
		switch (renderingMethod)
		{
		case RenderingMethod.ForwardLow:
			Camera.AddCommandBuffer(CameraEvent.AfterDepthNormalsTexture, Data.maskBuffer);
			Data.customDTM = CustomDepthTextureMode.None;
			Data.desiredDTM = DepthTextureMode.DepthNormals;
			DynamicDecals.SetDepthTextureMode(Camera, Data);
			break;
		case RenderingMethod.ForwardHigh:
			Camera.AddCommandBuffer(CameraEvent.AfterDepthTexture, Data.maskBuffer);
			Data.customDTM = CustomDepthTextureMode.Normal;
			Data.desiredDTM = DepthTextureMode.Depth;
			DynamicDecals.SetDepthTextureMode(Camera, Data);
			break;
		case RenderingMethod.ForwardForced:
			Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, Data.maskBuffer);
			Data.customDTM = CustomDepthTextureMode.Normal;
			Data.desiredDTM = DepthTextureMode.Depth;
			DynamicDecals.SetDepthTextureMode(Camera, Data);
			break;
		case RenderingMethod.Deferred:
			Camera.AddCommandBuffer(DynamicDecals.DeferredMaskEvent, Data.maskBuffer);
			Camera.AddCommandBuffer(DynamicDecals.DeferredProjectionEvent, Data.projectionBuffer);
			Data.customDTM = CustomDepthTextureMode.None;
			DynamicDecals.RestoreDepthTextureMode(Camera, Data);
			break;
		}
	}

	private static void SetDepthTextureMode(Camera Camera, CameraData CameraData)
	{
		if (CameraData.desiredDTM.HasValue)
		{
			if (Camera.depthTextureMode != CameraData.desiredDTM)
			{
				if (!CameraData.originalDTM.HasValue)
				{
					CameraData.originalDTM = Camera.depthTextureMode;
				}
				else
				{
					Camera.depthTextureMode = CameraData.originalDTM.Value;
				}
				Camera.depthTextureMode |= CameraData.desiredDTM.Value;
			}
		}
		else
		{
			DynamicDecals.RestoreDepthTextureMode(Camera, CameraData);
		}
	}

	private static void RestoreDepthTextureMode(Camera Camera, CameraData CameraData)
	{
		if (CameraData.originalDTM.HasValue)
		{
			Camera.depthTextureMode = CameraData.originalDTM.Value;
		}
	}

	public static void RestoreDepthTextureModes()
	{
		for (int i = 0; i < DynamicDecals.cameraData.Count; i++)
		{
			Camera key = DynamicDecals.cameraData.ElementAt(i).Key;
			if ((UnityEngine.Object)key != (UnityEngine.Object)null)
			{
				DynamicDecals.RestoreDepthTextureMode(key, DynamicDecals.cameraData.ElementAt(i).Value);
			}
		}
	}

	public static void UpdateLODs()
	{
		Shader.Find("Decal/Metallic").maximumLOD = ((!DynamicDecals.Settings.forceForward) ? 1000 : 0);
		Shader.Find("Decal/Specular").maximumLOD = ((!DynamicDecals.Settings.forceForward) ? 1000 : 0);
		Shader.Find("Decal/Unlit").maximumLOD = ((!DynamicDecals.Settings.forceForward) ? 1000 : 0);
		Shader.Find("Decal/OmniDecal").maximumLOD = ((!DynamicDecals.Settings.forceForward) ? 1000 : 0);
		Shader.Find("Decal/Eraser/Read").maximumLOD = ((!DynamicDecals.Settings.forceForward) ? 1000 : 0);
	}

	private static void ScaleMesh(Mesh Mesh, float Scale)
	{
		Vector3[] array = new Vector3[Mesh.vertices.Length];
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 vector = Mesh.vertices[i];
			vector.x *= Scale;
			vector.y *= Scale;
			vector.z *= Scale;
			array[i] = vector;
		}
		Mesh.vertices = array;
		Mesh.RecalculateBounds();
	}

	public static void AddProjection(Projection Projection)
	{
		DynamicDecals.Initialize();
		if (DynamicDecals.System.projections == null)
		{
			DynamicDecals.System.projections = new List<Projection>();
		}
		if (!DynamicDecals.system.projections.Contains(Projection))
		{
			if (DynamicDecals.system.projections.Count == 0)
			{
				DynamicDecals.system.projections.Add(Projection);
			}
			else
			{
				int num = 0;
				while (num < DynamicDecals.system.projections.Count)
				{
					if (Projection.Priority >= DynamicDecals.system.projections[num].Priority)
					{
						if (Projection.Priority == DynamicDecals.system.projections[num].Priority && Projection.timeID < DynamicDecals.system.projections[num].timeID)
						{
							DynamicDecals.system.projections.Insert(num, Projection);
							break;
						}
						if (num == DynamicDecals.system.projections.Count - 1)
						{
							DynamicDecals.system.projections.Add(Projection);
							break;
						}
						num++;
						continue;
					}
					DynamicDecals.system.projections.Insert(num, Projection);
					break;
				}
			}
			if (Projection.GetType() == typeof(Eraser))
			{
				DynamicDecals.system.staticCount++;
			}
		}
	}

	public static void RemoveProjection(Projection Projection)
	{
		if (DynamicDecals.system.projections.Remove(Projection) && Projection.GetType() == typeof(Eraser))
		{
			DynamicDecals.system.staticCount = Mathf.Clamp(DynamicDecals.system.staticCount - 1, 0, 10000000);
		}
		DynamicDecals.Uninitialize(false);
	}

	public static void Sort()
	{
		DynamicDecals.system.sort = true;
	}

	private static void ReorderProjections()
	{
		if (DynamicDecals.System.sort && DynamicDecals.RenderingMethod == RenderingMethod.Deferred)
		{
			DynamicDecals.System.projections.Sort(delegate(Projection x, Projection y)
			{
				if (x.Priority > y.Priority)
				{
					return 1;
				}
				if (x.Priority < y.Priority)
				{
					return -1;
				}
				if (x.timeID > y.timeID)
				{
					return 1;
				}
				if (x.timeID < y.timeID)
				{
					return -1;
				}
				return 0;
			});
			DynamicDecals.System.sort = false;
		}
	}

	private static void UpdateProjections()
	{
		for (int i = 0; i < DynamicDecals.system.projections.Count; i++)
		{
			DynamicDecals.system.projections[i].UpdateProjection();
		}
	}

	public static void AddMask(ProjectionMask Mask)
	{
		if (DynamicDecals.System.masks == null)
		{
			DynamicDecals.System.masks = new List<ProjectionMask>();
		}
		if (!DynamicDecals.system.masks.Contains(Mask))
		{
			DynamicDecals.system.masks.Add(Mask);
		}
	}

	public static void RemoveMask(ProjectionMask Mask)
	{
		if (DynamicDecals.System.masks == null)
		{
			DynamicDecals.System.masks = new List<ProjectionMask>();
		}
		DynamicDecals.system.masks.Remove(Mask);
	}

	internal static CameraData GetData(Camera Camera)
	{
		CameraData cameraData = default(CameraData);
		if ((UnityEngine.Object)((Component)Camera).GetComponent<ProjectionBlocker>() == (UnityEngine.Object)null)
		{
			if (!DynamicDecals.cameraData.TryGetValue(Camera, out cameraData))
			{
				RenderingPath actualRenderingPath = Camera.actualRenderingPath;
				RenderingMethod renderingMethod = RenderingMethod.Deferred;
				renderingMethod = (RenderingMethod)((actualRenderingPath != RenderingPath.DeferredShading) ? (DynamicDecals.Settings.highPrecision ? 1 : 0) : ((!DynamicDecals.Settings.forceForward) ? 3 : 2));
				CommandBuffer commandBuffer = new CommandBuffer();
				commandBuffer.name = "Dynamic Decals - Masking";
				CommandBuffer commandBuffer2 = new CommandBuffer();
				commandBuffer2.name = "Dynamic Decals - Projection";
				CullingGroup cullingGroup = new CullingGroup();
				CullingGroup cullingGroup2 = new CullingGroup();
				cullingGroup.targetCamera = Camera;
				cullingGroup2.targetCamera = Camera;
				cameraData = new CameraData(Camera, renderingMethod, commandBuffer, commandBuffer2, cullingGroup, cullingGroup2);
				DynamicDecals.SetRenderingMethod(Camera, cameraData);
				DynamicDecals.cameraData[Camera] = cameraData;
			}
		}
		else if (DynamicDecals.cameraData.TryGetValue(Camera, out cameraData))
		{
			DynamicDecals.RestoreDepthTextureMode(Camera, cameraData);
			if ((UnityEngine.Object)Camera != (UnityEngine.Object)null)
			{
				Camera.RemoveAllCommandBuffers();
			}
			if (cameraData.maskCulling != null)
			{
				cameraData.maskCulling.Dispose();
				cameraData.maskCulling = null;
			}
			if (cameraData.projectionCulling != null)
			{
				cameraData.projectionCulling.Dispose();
				cameraData.maskCulling = null;
			}
			DynamicDecals.cameraData.Remove(Camera);
			cameraData = null;
		}
		return cameraData;
	}

	internal static ProjectionPool PoolFromInstance(PoolInstance Instance)
	{
		if (DynamicDecals.Pools == null)
		{
			DynamicDecals.Pools = new Dictionary<int, ProjectionPool>();
		}
		ProjectionPool projectionPool = default(ProjectionPool);
		if (!DynamicDecals.Pools.TryGetValue(Instance.id, out projectionPool))
		{
			projectionPool = new ProjectionPool(Instance);
			DynamicDecals.Pools.Add(Instance.id, projectionPool);
		}
		return projectionPool;
	}

	public static ProjectionPool GetPool(string Title)
	{
		for (int i = 0; i < DynamicDecals.Settings.pools.Length; i++)
		{
			if (DynamicDecals.system.settings.pools[i].title == Title)
			{
				return DynamicDecals.PoolFromInstance(DynamicDecals.system.settings.pools[i]);
			}
		}
		Debug.LogWarning("No valid pool with the title : " + Title + " found. Returning default pool");
		return DynamicDecals.PoolFromInstance(DynamicDecals.system.settings.pools[0]);
	}

	public static ProjectionPool GetPool(int ID)
	{
		for (int i = 0; i < DynamicDecals.Settings.pools.Length; i++)
		{
			if (DynamicDecals.system.settings.pools[i].id == ID)
			{
				return DynamicDecals.PoolFromInstance(DynamicDecals.system.settings.pools[i]);
			}
		}
		Debug.LogWarning("No valid pool with the ID : " + ID + " found. Returning default pool");
		return DynamicDecals.PoolFromInstance(DynamicDecals.system.settings.pools[0]);
	}

	public static void Initialize()
	{
		if (!DynamicDecals.System.initialized)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			DynamicDecals.System.cube = gameObject.GetComponent<MeshFilter>().sharedMesh;
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			Camera.onPreCull = (Camera.CameraCallback)Delegate.Combine(Camera.onPreCull, new Camera.CameraCallback(DynamicDecals.CullProjections));
			Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(DynamicDecals.RenderProjections));
			DynamicDecals.System.initialized = true;
		}
	}

	public static void Uninitialize(bool Forced = false)
	{
		if (!Forced)
		{
			if (!DynamicDecals.System.initialized)
			{
				return;
			}
			if (DynamicDecals.system.projections.Count != 0)
			{
				return;
			}
		}
		Camera.onPreCull = (Camera.CameraCallback)Delegate.Remove(Camera.onPreCull, new Camera.CameraCallback(DynamicDecals.CullProjections));
		Camera.onPreRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPreRender, new Camera.CameraCallback(DynamicDecals.RenderProjections));
		foreach (KeyValuePair<Camera, CameraData> cameraDatum in DynamicDecals.cameraData)
		{
			if ((UnityEngine.Object)cameraDatum.Key != (UnityEngine.Object)null)
			{
				cameraDatum.Key.RemoveAllCommandBuffers();
			}
			if (cameraDatum.Value.maskCulling != null)
			{
				cameraDatum.Value.maskCulling.Dispose();
				cameraDatum.Value.maskCulling = null;
			}
			if (cameraDatum.Value.projectionCulling != null)
			{
				cameraDatum.Value.projectionCulling.Dispose();
				cameraDatum.Value.maskCulling = null;
			}
		}
		DynamicDecals.cameraData.Clear();
		DynamicDecals.System.initialized = false;
	}

	private static void CullProjections(Camera Camera)
	{
		CameraData data = DynamicDecals.GetData(Camera);
		if (DynamicDecals.System.initialized && !DynamicDecals.System.updated)
		{
			if (DynamicDecals.Pools != null && DynamicDecals.Pools.Count > 0)
			{
				for (int i = 0; i < DynamicDecals.Pools.Count; i++)
				{
					DynamicDecals.Pools.ElementAt(i).Value.Update(Time.deltaTime);
				}
			}
			DynamicDecals.CheckRenderingPath();
			DynamicDecals.ReorderProjections();
			DynamicDecals.UpdateProjections();
			DynamicDecals.RequestCullUpdate();
			DynamicDecals.System.updated = true;
		}
		if (data != null)
		{
			if (!data.sceneCamera && !data.previewCamera && !Camera.isActiveAndEnabled)
			{
				return;
			}
			DynamicDecals.UpdateRenderingPath(Camera, data);
			if (data.method == RenderingMethod.ForwardForced || data.method == RenderingMethod.ForwardHigh || data.method == RenderingMethod.ForwardLow)
			{
				DynamicDecals.SetDepthTextureMode(Camera, data);
			}
			if (DynamicDecals.System.initialized && DynamicDecals.system.masks != null && DynamicDecals.system.masks.Count > 0)
			{
				data.maskCulling.SetBoundingSpheres(DynamicDecals.system.maskSpheres);
			}
			if (DynamicDecals.System.initialized && DynamicDecals.RenderingMethod == RenderingMethod.Deferred && DynamicDecals.system.projections != null && DynamicDecals.system.projections.Count > 0)
			{
				data.projectionCulling.SetBoundingSpheres(DynamicDecals.system.projectionSpheres);
			}
		}
	}

	private static void RenderProjections(Camera Camera)
	{
		DynamicDecals.System.updated = false;
		CameraData data = DynamicDecals.GetData(Camera);
		if (data != null)
		{
			if (!data.sceneCamera && !data.previewCamera && !Camera.isActiveAndEnabled)
			{
				return;
			}
			if (!DynamicDecals.system.sceneFocus && data.sceneCamera && Camera.farClipPlane > 100000f)
			{
				Debug.LogWarning("Scene Camera Far Clipping Plane too far out - Projections in the scene view may appear strange or not at all. To fix this, simply focus on an object with a reasonable scale (Select then F key), or scroll in with the mouse wheel. This occurs as Unity sets its far clipping plane absurdly high when focusing large objects, or scrolling out with the mouse-wheel. As the depthbuffer is stored as a value between the near and far clipping planes, when the far clipping plane is set to high the depthbuffer becomes very inprecise. As a result, the position and Uv's of our projections also become very inprecise.");
				DynamicDecals.system.sceneFocus = true;
			}
			if (!DynamicDecals.system.cameraClipping && !data.sceneCamera && !data.previewCamera && Camera.farClipPlane > 1000000f)
			{
				Debug.LogWarning("Cameras far clipping plane is too high to maintain an accurate Depth Buffer - Projections may appear strange or not at all. You'll also have a host of other issues, z-fighting among your objects etc.");
				DynamicDecals.system.cameraClipping = true;
			}
			DynamicDecals.UpdateMaskBuffer(Camera, data);
			DynamicDecals.UpdateProjectionBuffer(Camera, data);
			DynamicDecals.CustomDepthNormals(Camera, data);
		}
	}

	internal static void RequestCullUpdate()
	{
		if (DynamicDecals.system.masks != null && DynamicDecals.system.masks.Count > 0)
		{
			if (DynamicDecals.system.maskSpheres == null || DynamicDecals.system.maskSpheres.Length < DynamicDecals.system.masks.Count)
			{
				DynamicDecals.system.maskSpheres = new BoundingSphere[DynamicDecals.system.masks.Count * 2];
			}
			for (int i = 0; i < DynamicDecals.system.masks.Count; i++)
			{
				if (DynamicDecals.system.masks[i].Enabled)
				{
					Bounds bounds = DynamicDecals.system.masks[i].Bounds;
					DynamicDecals.system.maskSpheres[i].position = bounds.center;
					ref BoundingSphere val = ref DynamicDecals.system.maskSpheres[i];
					Vector3 size = bounds.size;
					float x = size.x;
					Vector3 size2 = bounds.size;
					float y = size2.y;
					Vector3 size3 = bounds.size;
					val.radius = Mathf.Max(x, Mathf.Max(y, size3.z)) * 1.5f;
				}
			}
		}
		if (DynamicDecals.RenderingMethod == RenderingMethod.Deferred && DynamicDecals.system.projections != null && DynamicDecals.system.projections.Count > 0)
		{
			if (DynamicDecals.system.projectionSpheres == null || DynamicDecals.system.projectionSpheres.Length < DynamicDecals.system.projections.Count)
			{
				DynamicDecals.system.projectionSpheres = new BoundingSphere[DynamicDecals.system.projections.Count * 2];
			}
			for (int j = 0; j < DynamicDecals.system.projections.Count; j++)
			{
				Transform transform = DynamicDecals.system.projections[j].transform;
				Vector3 lossyScale = transform.lossyScale;
				DynamicDecals.system.projectionSpheres[j].position = transform.position;
				DynamicDecals.system.projectionSpheres[j].radius = Mathf.Max(lossyScale.x, Mathf.Max(lossyScale.y, lossyScale.z));
			}
		}
	}

	private static void CustomDepthNormals(Camera Camera, CameraData Data)
	{
		if (Data.customDTM == CustomDepthTextureMode.Normal)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(Camera.pixelWidth, Camera.pixelHeight, 24, RenderTextureFormat.ARGB2101010);
			DynamicDecals.CustomCamera.CopyFrom(Camera);
			DynamicDecals.CustomCamera.targetTexture = temporary;
			DynamicDecals.CustomCamera.renderingPath = RenderingPath.Forward;
			DynamicDecals.CustomCamera.clearFlags = CameraClearFlags.Color;
			DynamicDecals.CustomCamera.rect = DynamicDecals.FullRect;
			DynamicDecals.CustomCamera.RenderWithShader(DynamicDecals.NormalShader, "RenderType");
			temporary.SetGlobalShaderProperty("_CameraNormalTexture");
			RenderTexture.ReleaseTemporary(temporary);
			Shader.DisableKeyword("_LowPrecision");
			Shader.EnableKeyword("_HighPrecision");
		}
		else
		{
			Shader.DisableKeyword("_HighPrecision");
			Shader.EnableKeyword("_LowPrecision");
		}
	}

	private static void UpdateMaskBuffer(Camera Camera, CameraData Data)
	{
		Data.maskBuffer.Clear();
		switch (Camera.actualRenderingPath)
		{
		case RenderingPath.Forward:
			DynamicDecals.DrawMasksForward(Camera, Data.maskBuffer, Data.maskCulling);
			break;
		case RenderingPath.DeferredShading:
			DynamicDecals.DrawMasksDeferrred(Camera, Data.maskBuffer, Data.maskCulling);
			break;
		}
	}

	private static void DrawMasksForward(Camera Camera, CommandBuffer Buffer, CullingGroup MasksCullingGroup)
	{
		int nameID = Shader.PropertyToID("_MaskBuffer");
		Buffer.GetTemporaryRT(nameID, -1, -1);
		Buffer.SetRenderTarget(nameID, BuiltinRenderTextureType.CurrentActive);
		Buffer.ClearRenderTarget(false, true, Color.clear);
		List<ProjectionMask> list = DynamicDecals.system.masks;
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				try
				{
					if (MasksCullingGroup.IsVisible(i))
					{
						DynamicDecals.DrawMask(Camera, Buffer, list[i]);
					}
				}
				catch (IndexOutOfRangeException)
				{
					DynamicDecals.DrawMask(Camera, Buffer, list[i]);
				}
			}
		}
		Buffer.ReleaseTemporaryRT(nameID);
	}

	private static void DrawMasksDeferrred(Camera Camera, CommandBuffer Buffer, CullingGroup MasksCullingGroup)
	{
		int nameID = Shader.PropertyToID("_MaskBuffer");
		Buffer.GetTemporaryRT(nameID, -1, -1);
		Buffer.SetRenderTarget(nameID, BuiltinRenderTextureType.CameraTarget);
		Buffer.ClearRenderTarget(false, true, Color.clear);
		List<ProjectionMask> list = DynamicDecals.system.masks;
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				try
				{
					if (MasksCullingGroup.IsVisible(i))
					{
						DynamicDecals.DrawMask(Camera, Buffer, list[i]);
					}
				}
				catch (IndexOutOfRangeException)
				{
					DynamicDecals.DrawMask(Camera, Buffer, list[i]);
				}
			}
		}
		Buffer.ReleaseTemporaryRT(nameID);
	}

	private static void DrawMask(Camera Camera, CommandBuffer Buffer, ProjectionMask Mask)
	{
		if (Mask.Enabled)
		{
			for (int i = 0; i < Mask.Mesh.subMeshCount; i++)
			{
				if (Mask.VertExMotion)
				{
					Buffer.DrawMesh(Mask.Mesh, Mask.Matrix, DynamicDecals.Mat_VertExMask, i, 0, Mask.Properties);
				}
				else
				{
					Buffer.DrawMesh(Mask.Mesh, Mask.Matrix, DynamicDecals.Mat_Mask, i, 0, Mask.Properties);
				}
			}
		}
	}

	private static void UpdateProjectionBuffer(Camera Camera, CameraData Data)
	{
		Data.projectionBuffer.Clear();
		List<Projection> list = DynamicDecals.system.projections;
		if (list != null && list.Count > 0)
		{
			RenderingMethod method = Data.method;
			if (method == RenderingMethod.Deferred)
			{
				if (DynamicDecals.staticRts == null)
				{
					DynamicDecals.staticRts = new int[4];
				}
				if (DynamicDecals.StaticPass)
				{
					DynamicDecals.staticRts[0] = Shader.PropertyToID("_StcAlbedo");
					DynamicDecals.staticRts[1] = Shader.PropertyToID("_StcGloss");
					DynamicDecals.staticRts[2] = Shader.PropertyToID("_StcNormal");
					DynamicDecals.staticRts[3] = Shader.PropertyToID("_StcAmbient");
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[0], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[1], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[2], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB2101010);
					if (Camera.hdr)
					{
						Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[3], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB2101010);
					}
					else
					{
						Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[3], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGBHalf);
					}
					DynamicDecals.MultiChannelFullScreenBlit(Camera, Data.projectionBuffer, DynamicDecals.staticRts);
				}
				else
				{
					DynamicDecals.staticRts[2] = Shader.PropertyToID("_StcNormal");
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.staticRts[2], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB2101010);
					DynamicDecals.StaticNormalFullScreenBlit(Camera, Data.projectionBuffer, DynamicDecals.staticRts[2]);
				}
				if (DynamicDecals.dynamicRts == null)
				{
					DynamicDecals.dynamicRts = new int[4];
				}
				DynamicDecals.dynamicRts[0] = Shader.PropertyToID("_DynAlbedo");
				DynamicDecals.dynamicRts[1] = Shader.PropertyToID("_DynGloss");
				DynamicDecals.dynamicRts[2] = Shader.PropertyToID("_DynNormal");
				DynamicDecals.dynamicRts[3] = Shader.PropertyToID("_DynAmbient");
				Data.projectionBuffer.GetTemporaryRT(DynamicDecals.dynamicRts[0], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
				Data.projectionBuffer.GetTemporaryRT(DynamicDecals.dynamicRts[1], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
				Data.projectionBuffer.GetTemporaryRT(DynamicDecals.dynamicRts[2], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB2101010);
				if (Camera.hdr)
				{
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.dynamicRts[3], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGB2101010);
				}
				else
				{
					Data.projectionBuffer.GetTemporaryRT(DynamicDecals.dynamicRts[3], -1, -1, 0, FilterMode.Point, RenderTextureFormat.ARGBHalf);
				}
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						if (Data.projectionCulling.IsVisible(i) && (Camera.cullingMask & 1 << list[i].gameObject.layer) != 0)
						{
							DynamicDecals.DrawDeferredProjection(Camera, Data.projectionBuffer, list[i], DynamicDecals.dynamicRts);
							list[i].SetVisibility(true);
						}
						else
						{
							list[i].SetVisibility(false);
						}
					}
					catch (IndexOutOfRangeException)
					{
						DynamicDecals.DrawDeferredProjection(Camera, Data.projectionBuffer, list[i], DynamicDecals.dynamicRts);
					}
				}
				if (DynamicDecals.StaticPass)
				{
					Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.staticRts[0]);
					Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.staticRts[1]);
					Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.staticRts[2]);
					Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.staticRts[3]);
				}
				else
				{
					Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.staticRts[2]);
				}
				Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.dynamicRts[0]);
				Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.dynamicRts[1]);
				Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.dynamicRts[2]);
				Data.projectionBuffer.ReleaseTemporaryRT(DynamicDecals.dynamicRts[3]);
			}
		}
	}

	private static void DrawDeferredProjection(Camera Camera, CommandBuffer Buffer, Projection Projection, int[] Rts)
	{
		if (Projection.isActiveAndEnabled && (UnityEngine.Object)Projection.RenderMaterial != (UnityEngine.Object)null && Projection.DeferredBuffers != null && Projection.DeferredBuffers.Length > 0)
		{
			if (Projection.DeferredPrePass)
			{
				DynamicDecals.MultiChannelBlit(Camera, Buffer, Projection, Rts);
			}
			if (Camera.hdr)
			{
				Buffer.SetRenderTarget(Projection.DeferredHDRTargets, BuiltinRenderTextureType.CameraTarget);
			}
			else
			{
				Buffer.SetRenderTarget(Projection.DeferredTargets, BuiltinRenderTextureType.CameraTarget);
			}
			Buffer.DrawMesh(DynamicDecals.Cube, Projection.RenderMatrix, Projection.RenderMaterial, 0, Projection.DeferredPass, Projection.MaterialProperties);
		}
	}

	public static RenderTargetIdentifier[] PassesToTargets(bool[] Channels, bool HDR)
	{
		int num = 0;
		if (Channels[0])
		{
			num += 2;
		}
		if (Channels[1])
		{
			num++;
		}
		if (Channels[2])
		{
			num++;
		}
		RenderTargetIdentifier[] array = null;
		switch (num)
		{
		case 0:
			return null;
		case 1:
			array = DynamicDecals.one;
			break;
		case 2:
			array = DynamicDecals.two;
			break;
		case 3:
			array = DynamicDecals.three;
			break;
		case 4:
			array = DynamicDecals.four;
			break;
		}
		num = 0;
		if (Channels[0])
		{
			array[num] = BuiltinRenderTextureType.GBuffer0;
			num++;
		}
		if (Channels[1])
		{
			array[num] = BuiltinRenderTextureType.GBuffer1;
			num++;
		}
		if (Channels[2])
		{
			array[num] = BuiltinRenderTextureType.GBuffer2;
			num++;
		}
		if (Channels[0])
		{
			if (HDR)
			{
				array[num] = BuiltinRenderTextureType.CameraTarget;
				num++;
			}
			else
			{
				array[num] = BuiltinRenderTextureType.GBuffer3;
				num++;
			}
		}
		return array;
	}

	private static void MultiChannelBlit(Camera Camera, CommandBuffer Buffer, Projection Projection, int[] Rts)
	{
		if (Projection.DeferredBuffers[0] && Projection.DeferredBuffers[1] && Projection.DeferredBuffers[2])
		{
			DynamicDecals.four[0] = Rts[0];
			DynamicDecals.four[1] = Rts[1];
			DynamicDecals.four[2] = Rts[2];
			DynamicDecals.four[3] = Rts[3];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.four, 6, Projection);
		}
		else if (Projection.DeferredBuffers[1] && Projection.DeferredBuffers[2])
		{
			DynamicDecals.two[0] = Rts[1];
			DynamicDecals.two[1] = Rts[2];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.two, 5, Projection);
		}
		else if (Projection.DeferredBuffers[0] && Projection.DeferredBuffers[2])
		{
			DynamicDecals.three[0] = Rts[0];
			DynamicDecals.three[1] = Rts[2];
			DynamicDecals.three[2] = Rts[3];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.three, 4, Projection);
		}
		else if (Projection.DeferredBuffers[0] && Projection.DeferredBuffers[1])
		{
			DynamicDecals.three[0] = Rts[0];
			DynamicDecals.three[1] = Rts[1];
			DynamicDecals.three[2] = Rts[3];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.three, 3, Projection);
		}
		else if (Projection.DeferredBuffers[2])
		{
			DynamicDecals.one[0] = Rts[2];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.one, 2, Projection);
		}
		else if (Projection.DeferredBuffers[1])
		{
			DynamicDecals.one[0] = Rts[1];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.one, 1, Projection);
		}
		else
		{
			DynamicDecals.two[0] = Rts[0];
			DynamicDecals.two[1] = Rts[3];
			DynamicDecals.DrawPrePass(Camera, Buffer, DynamicDecals.two, 0, Projection);
		}
	}

	private static void DrawPrePass(Camera Camera, CommandBuffer Buffer, RenderTargetIdentifier[] Rts, int Pass, Projection Projection)
	{
		if (Rts.Length > 0)
		{
			Buffer.SetRenderTarget(Rts, BuiltinRenderTextureType.CameraTarget);
			Buffer.DrawMesh(DynamicDecals.Cube, Projection.RenderMatrix, DynamicDecals.Mat_DeferredBlit, 0, Pass);
		}
	}

	private static void StaticNormalFullScreenBlit(Camera Camera, CommandBuffer Buffer, int Rt)
	{
		Buffer.SetRenderTarget(Rt, BuiltinRenderTextureType.CameraTarget);
		Buffer.DrawMesh(DynamicDecals.CameraBlit, Camera.transform.localToWorldMatrix, DynamicDecals.Mat_DeferredBlit, 0, 2);
	}

	private static void MultiChannelFullScreenBlit(Camera Camera, CommandBuffer Buffer, int[] Rts)
	{
		DynamicDecals.four[0] = Rts[0];
		DynamicDecals.four[1] = Rts[1];
		DynamicDecals.four[2] = Rts[2];
		DynamicDecals.four[3] = Rts[3];
		Buffer.SetRenderTarget(DynamicDecals.four, BuiltinRenderTextureType.CameraTarget);
		Buffer.DrawMesh(DynamicDecals.CameraBlit, Camera.transform.localToWorldMatrix, DynamicDecals.Mat_DeferredBlit, 0, 6);
	}
}
