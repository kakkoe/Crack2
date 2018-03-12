using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace TriLib
{
	public class AssetLoader : IDisposable
	{
		private MaterialData[] _materialData;

		private MeshData[] _meshData;

		private Dictionary<string, NodeData> _nodeDataDictionary;

		private int _nodeId;

		private Material _standardBaseMaterial;

		private Material _standardSpecularMaterial;

		private Texture2D _notFoundTexture;

        public event MeshCreatedHandle OnMeshCreated;

        public event MaterialCreatedHandle OnMaterialCreated;

        public event TextureLoadHandle OnTextureLoaded;

        public event AnimationClipCreatedHandle OnAnimationClipCreated;

        public event ObjectLoadedHandle OnObjectLoaded;

		public static bool IsExtensionSupported(string extension)
		{
			return AssimpInterop.ai_IsExtensionSupported(extension);
		}

		public static string GetSupportedFileExtensions()
		{
			string result = default(string);
			AssimpInterop.ai_GetExtensionList(out result);
			return result;
		}

		public void Dispose()
		{
			this.OnMeshCreated = null;
			this.OnMaterialCreated = null;
			this.OnAnimationClipCreated = null;
			this.OnTextureLoaded = null;
			this.OnObjectLoaded = null;
			if (this._nodeDataDictionary != null)
			{
				foreach (NodeData value in this._nodeDataDictionary.Values)
				{
					value.Dispose();
				}
				this._nodeDataDictionary = null;
			}
			if (this._meshData != null)
			{
				MeshData[] meshData = this._meshData;
				foreach (MeshData meshData2 in meshData)
				{
					meshData2.Dispose();
				}
				this._meshData = null;
			}
			if (this._materialData != null)
			{
				MaterialData[] materialData = this._materialData;
				foreach (MaterialData materialData2 in materialData)
				{
					materialData2.Dispose();
				}
				this._materialData = null;
			}
		}

		private bool LoadStandardMaterials()
		{
			if ((UnityEngine.Object)this._standardBaseMaterial == (UnityEngine.Object)null)
			{
				this._standardBaseMaterial = (Resources.Load("StandardMaterial") as Material);
			}
			if ((UnityEngine.Object)this._standardSpecularMaterial == (UnityEngine.Object)null)
			{
				this._standardSpecularMaterial = (Resources.Load("StandardSpecularMaterial") as Material);
			}
			return (UnityEngine.Object)this._standardBaseMaterial != (UnityEngine.Object)null && (UnityEngine.Object)this._standardSpecularMaterial != (UnityEngine.Object)null;
		}

		private bool LoadNotFoundTexture()
		{
			if ((UnityEngine.Object)this._notFoundTexture == (UnityEngine.Object)null)
			{
				this._notFoundTexture = (Resources.Load("NotFound") as Texture2D);
			}
			return (UnityEngine.Object)this._notFoundTexture != (UnityEngine.Object)null;
		}

		private void LoadContextOptions(GameObject rootGameObject, AssetLoaderOptions options)
		{
			rootGameObject.transform.rotation = Quaternion.Euler(options.RotationAngles);
			rootGameObject.transform.localScale = Vector3.one * options.Scale;
		}

		public GameObject LoadFromFile(string filename, AssetLoaderOptions options = null, GameObject wrapperGameObject = null)
		{
			if ((UnityEngine.Object)options == (UnityEngine.Object)null)
			{
				options = AssetLoaderOptions.CreateInstance();
			}
			IntPtr intPtr;
			try
			{
				intPtr = this.ImportFile(filename, options);
			}
			catch (Exception innerException)
			{
				throw new Exception(string.Format("Error parsing file: {0}", filename), innerException);
			}
			if (intPtr == IntPtr.Zero)
			{
				string arg = AssimpInterop.ai_GetErrorString();
				throw new Exception(string.Format("Error loading asset. Assimp returns: [{0}]", arg));
			}
			GameObject gameObject = null;
			try
			{
				gameObject = this.LoadInternal(filename, intPtr, options, wrapperGameObject);
			}
			catch
			{
				if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				throw;
			}
			AssimpInterop.ai_ReleaseImport(intPtr);
			if (this.OnObjectLoaded != null)
			{
				this.OnObjectLoaded(gameObject);
			}
			return gameObject;
		}

		private IntPtr ImportFile(string filename, AssetLoaderOptions options)
		{
			IntPtr result;
			if ((UnityEngine.Object)options != (UnityEngine.Object)null && options.AdvancedConfigs != null)
			{
				IntPtr intPtr = AssimpInterop.ai_CreatePropertyStore();
				foreach (AssetAdvancedConfig advancedConfig in options.AdvancedConfigs)
				{
					AssetAdvancedConfigType assetAdvancedConfigType = default(AssetAdvancedConfigType);
					string text = default(string);
					string text2 = default(string);
					string text3 = default(string);
					object obj = default(object);
					object obj2 = default(object);
					object obj3 = default(object);
					bool flag = default(bool);
					bool flag2 = default(bool);
					bool flag3 = default(bool);
					AssetAdvancedPropertyMetadata.GetOptionMetadata(advancedConfig.Key, out assetAdvancedConfigType, out text, out text2, out text3, out obj, out obj2, out obj3, out flag, out flag2, out flag3);
					switch (assetAdvancedConfigType)
					{
					case AssetAdvancedConfigType.AiComponent:
						AssimpInterop.ai_SetImportPropertyInteger(intPtr, advancedConfig.Key, advancedConfig.IntValue << 1);
						break;
					case AssetAdvancedConfigType.AiPrimitiveType:
						AssimpInterop.ai_SetImportPropertyInteger(intPtr, advancedConfig.Key, advancedConfig.IntValue << 1);
						break;
					case AssetAdvancedConfigType.AiUVTransform:
						AssimpInterop.ai_SetImportPropertyInteger(intPtr, advancedConfig.Key, advancedConfig.IntValue << 1);
						break;
					case AssetAdvancedConfigType.Bool:
						AssimpInterop.ai_SetImportPropertyInteger(intPtr, advancedConfig.Key, advancedConfig.BoolValue ? 1 : 0);
						break;
					case AssetAdvancedConfigType.Integer:
						AssimpInterop.ai_SetImportPropertyInteger(intPtr, advancedConfig.Key, advancedConfig.IntValue);
						break;
					case AssetAdvancedConfigType.Float:
						AssimpInterop.ai_SetImportPropertyFloat(intPtr, advancedConfig.Key, advancedConfig.FloatValue);
						break;
					case AssetAdvancedConfigType.String:
						AssimpInterop.ai_SetImportPropertyString(intPtr, advancedConfig.Key, advancedConfig.StringValue);
						break;
					}
				}
				result = AssimpInterop.ai_ImportFileEx(filename, (uint)options.PostProcessSteps, IntPtr.Zero, intPtr);
				AssimpInterop.ai_CreateReleasePropertyStore(intPtr);
			}
			else
			{
				result = AssimpInterop.ai_ImportFile(filename, (uint)options.PostProcessSteps);
			}
			return result;
		}

		private GameObject LoadInternal(string filename, IntPtr scene, AssetLoaderOptions options, GameObject wrapperGameObject = null)
		{
			this._nodeDataDictionary = new Dictionary<string, NodeData>();
			this._nodeId = 0;
			if (!this.LoadNotFoundTexture())
			{
				throw new Exception("Please add the \"NotFound\" asset from the source package at the project 'Resources' folder.");
			}
			if (!this.LoadStandardMaterials())
			{
				throw new Exception("Please add the \"StandardMaterial\" and \"StandardSpecularMaterial\" assets from the source package at the project 'Resources' folder.");
			}
			if (AssimpInterop.aiScene_HasMaterials(scene) && !options.DontLoadMaterials)
			{
				this._materialData = new MaterialData[AssimpInterop.aiScene_GetNumMaterials(scene)];
				this.BuildMaterials(filename, scene, options);
			}
			if (AssimpInterop.aiScene_HasMeshes(scene))
			{
				this._meshData = new MeshData[AssimpInterop.aiScene_GetNumMeshes(scene)];
				this.BuildMeshes(scene);
			}
			wrapperGameObject = this.BuildWrapperObject(scene, options, wrapperGameObject);
			if (AssimpInterop.aiScene_HasMeshes(scene))
			{
				this.BuildBones(scene);
			}
			if (AssimpInterop.aiScene_HasAnimation(scene) && !options.DontLoadAnimations)
			{
				this.BuildAnimations(wrapperGameObject, scene, options);
			}
			if (AssimpInterop.aiScene_HasCameras(scene) && !options.DontLoadCameras)
			{
				this.BuildCameras(wrapperGameObject, scene, options);
			}
			if (AssimpInterop.aiScene_HasLights(scene) && !options.DontLoadLights)
			{
				this.BuildLights(wrapperGameObject, scene, options);
			}
			this._nodeDataDictionary = null;
			this._meshData = null;
			this._materialData = null;
			return wrapperGameObject;
		}

		private void BuildMeshes(IntPtr scene)
		{
			uint num = AssimpInterop.aiScene_GetNumMeshes(scene);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num2);
				uint num3 = AssimpInterop.aiMesh_VertexCount(ptrMesh);
				Vector3[] array = new Vector3[num3];
				Vector3[] array2 = null;
				bool flag = AssimpInterop.aiMesh_HasNormals(ptrMesh);
				if (flag)
				{
					array2 = new Vector3[num3];
				}
				Vector4[] array3 = null;
				Vector4[] array4 = null;
				bool flag2 = AssimpInterop.aiMesh_HasTangentsAndBitangents(ptrMesh);
				if (flag2)
				{
					array3 = new Vector4[num3];
					array4 = new Vector4[num3];
				}
				Vector2[] array5 = null;
				bool flag3 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 0u);
				if (flag3)
				{
					array5 = new Vector2[num3];
				}
				Vector2[] array6 = null;
				bool flag4 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 1u);
				if (flag4)
				{
					array6 = new Vector2[num3];
				}
				Vector2[] array7 = null;
				bool flag5 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 2u);
				if (flag5)
				{
					array7 = new Vector2[num3];
				}
				Vector2[] array8 = null;
				bool flag6 = AssimpInterop.aiMesh_HasTextureCoords(ptrMesh, 3u);
				if (flag6)
				{
					array8 = new Vector2[num3];
				}
				Color[] array9 = null;
				bool flag7 = AssimpInterop.aiMesh_HasVertexColors(ptrMesh, 0u);
				if (flag7)
				{
					array9 = new Color[num3];
				}
				for (uint num4 = 0u; num4 < num3; num4++)
				{
					array[num4] = AssimpInterop.aiMesh_GetVertex(ptrMesh, num4);
					if (flag)
					{
						array2[num4] = AssimpInterop.aiMesh_GetNormal(ptrMesh, num4);
					}
					if (flag2)
					{
						array3[num4] = AssimpInterop.aiMesh_GetTangent(ptrMesh, num4);
						array4[num4] = AssimpInterop.aiMesh_GetBitangent(ptrMesh, num4);
					}
					if (flag3)
					{
						array5[num4] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 0u, num4);
					}
					if (flag4)
					{
						array6[num4] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 1u, num4);
					}
					if (flag5)
					{
						array7[num4] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 2u, num4);
					}
					if (flag6)
					{
						array8[num4] = AssimpInterop.aiMesh_GetTextureCoord(ptrMesh, 3u, num4);
					}
					if (flag7)
					{
						array9[num4] = AssimpInterop.aiMesh_GetVertexColor(ptrMesh, 0u, num4);
					}
				}
				string text = AssimpInterop.aiMesh_GetName(ptrMesh);
				Mesh mesh = new Mesh();
				mesh.name = ((!string.IsNullOrEmpty(text)) ? text : ("Mesh_" + StringUtils.GenerateUniqueName(num2)));
				mesh.vertices = array;
				Mesh mesh2 = mesh;
				if (flag)
				{
					mesh2.normals = array2;
				}
				if (flag2)
				{
					mesh2.tangents = array3;
				}
				if (flag3)
				{
					mesh2.uv = array5;
				}
				if (flag4)
				{
					mesh2.uv2 = array6;
				}
				if (flag5)
				{
					mesh2.uv3 = array7;
				}
				if (flag6)
				{
					mesh2.uv4 = array8;
				}
				if (flag7)
				{
					mesh2.colors = array9;
				}
				if (AssimpInterop.aiMesh_HasFaces(ptrMesh))
				{
					uint num5 = AssimpInterop.aiMesh_GetNumFaces(ptrMesh);
					int[] array10 = new int[num5 * 3];
					for (uint num6 = 0u; num6 < num5; num6++)
					{
						IntPtr ptrFace = AssimpInterop.aiMesh_GetFace(ptrMesh, num6);
						uint num7 = AssimpInterop.aiFace_GetNumIndices(ptrFace);
						if (num7 > 3)
						{
							throw new UnityException("More than three face indices is not supported. Please enable \"Triangulate\" in your \"AssetLoaderOptions\" \"PostProcessSteps\" field");
						}
						for (uint num8 = 0u; num8 < num7; num8++)
						{
							array10[num6 * 3 + num8] = (int)AssimpInterop.aiFace_GetIndex(ptrFace, num8);
						}
					}
					mesh2.SetIndices(array10, MeshTopology.Triangles, 0);
				}
				MeshData meshData = new MeshData();
				meshData.UnityMesh = mesh2;
				MeshData meshData2 = meshData;
				this._meshData[num2] = meshData2;
			}
		}

		private void BuildLights(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
		}

		private void BuildCameras(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
			for (uint num = 0u; num < AssimpInterop.aiScene_GetNumCameras(scene); num++)
			{
				IntPtr ptrCamera = AssimpInterop.aiScene_GetCamera(scene, num);
				string name = AssimpInterop.aiCamera_GetName(ptrCamera);
				Transform transform = wrapperGameObject.transform.FindDeepChild(name, false);
				if (!((UnityEngine.Object)transform == (UnityEngine.Object)null))
				{
					Camera camera = transform.gameObject.AddComponent<Camera>();
					camera.aspect = AssimpInterop.aiCamera_GetAspect(ptrCamera);
					camera.nearClipPlane = AssimpInterop.aiCamera_GetClipPlaneNear(ptrCamera);
					camera.farClipPlane = AssimpInterop.aiCamera_GetClipPlaneFar(ptrCamera);
					camera.fieldOfView = AssimpInterop.aiCamera_GetHorizontalFOV(ptrCamera);
					camera.transform.localPosition = AssimpInterop.aiCamera_GetPosition(ptrCamera);
					camera.transform.LookAt(AssimpInterop.aiCamera_GetLookAt(ptrCamera), AssimpInterop.aiCamera_GetUp(ptrCamera));
				}
			}
		}

		private void BuildMaterials(string filename, IntPtr scene, AssetLoaderOptions options)
		{
			string text = null;
			string textureFileNameWithoutExtension = null;
			if (filename != null)
			{
				FileInfo fileInfo = new FileInfo(filename);
				text = fileInfo.Directory.FullName;
				textureFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			}
			string basePath = string.IsNullOrEmpty(options.TexturesPathOverride) ? text : options.TexturesPathOverride;
			List<Material> list = options.MaterialsOverride ?? new List<Material>();
			for (uint num = 0u; num < AssimpInterop.aiScene_GetNumMaterials(scene); num++)
			{
				IntPtr ptrMat = AssimpInterop.aiScene_GetMaterial(scene, num);
				bool isOverriden;
				Material material;
				if (list.Count > num)
				{
					isOverriden = true;
					material = list[(int)num];
				}
				else
				{
					isOverriden = false;
					string name = default(string);
					if (AssimpInterop.aiMaterial_HasName(ptrMat))
					{
						if (!AssimpInterop.aiMaterial_GetName(ptrMat, out name))
						{
							name = "Material_" + StringUtils.GenerateUniqueName(num);
						}
					}
					else
					{
						name = "Material_" + StringUtils.GenerateUniqueName(num);
					}
					bool flag = AssimpInterop.aiMaterial_HasSpecular(ptrMat);
					uint num2 = AssimpInterop.aiMaterial_GetNumTextureSpecular(ptrMat);
					material = new Material((!options.UseStandardSpecularMaterial || (!flag && num2 == 0)) ? this._standardBaseMaterial : this._standardSpecularMaterial);
					material.name = name;
					float a = 1f;
					float num3 = default(float);
					if (AssimpInterop.aiMaterial_HasOpacity(ptrMat) && AssimpInterop.aiMaterial_GetOpacity(ptrMat, out num3))
					{
						a = num3;
					}
					bool flag2 = false;
					Color value = default(Color);
					if (AssimpInterop.aiMaterial_HasDiffuse(ptrMat) && AssimpInterop.aiMaterial_GetDiffuse(ptrMat, out value))
					{
						value.a = a;
						material.SetColor("_Color", value);
						flag2 = true;
					}
					if (!flag2)
					{
						material.SetColor("_Color", Color.white);
					}
					bool flag3 = false;
					uint num4 = AssimpInterop.aiMaterial_GetNumTextureDiffuse(ptrMat);
					string text2 = default(string);
					uint num5 = default(uint);
					uint num6 = default(uint);
					float num7 = default(float);
					uint num8 = default(uint);
					uint num9 = default(uint);
					if (num4 != 0 && AssimpInterop.aiMaterial_GetTextureDiffuse(ptrMat, 0u, out text2, out num5, out num6, out num7, out num8, out num9))
					{
						TextureWrapMode textureWrapMode = (TextureWrapMode)((num9 == 1) ? 1 : 0);
						string name2 = StringUtils.GenerateUniqueName(text2);
						Texture2DUtils.LoadTextureFromFile(scene, text2, name2, material, "_MainTex", textureWrapMode, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag3 = true;
					}
					if (!flag3)
					{
						material.SetTexture("_MainTex", null);
					}
					bool flag4 = false;
					Color value2 = default(Color);
					if (AssimpInterop.aiMaterial_HasEmissive(ptrMat) && AssimpInterop.aiMaterial_GetEmissive(ptrMat, out value2))
					{
						material.SetColor("_EmissionColor", value2);
						flag4 = true;
					}
					if (!flag4)
					{
						material.SetColor("_EmissionColor", Color.black);
					}
					bool flag5 = false;
					uint num10 = AssimpInterop.aiMaterial_GetNumTextureEmissive(ptrMat);
					string text3 = default(string);
					uint num11 = default(uint);
					uint num12 = default(uint);
					float num13 = default(float);
					uint num14 = default(uint);
					uint num15 = default(uint);
					if (num10 != 0 && AssimpInterop.aiMaterial_GetTextureEmissive(ptrMat, 0u, out text3, out num11, out num12, out num13, out num14, out num15))
					{
						TextureWrapMode textureWrapMode2 = (TextureWrapMode)((num15 == 1) ? 1 : 0);
						string name3 = StringUtils.GenerateUniqueName(text3);
						Texture2DUtils.LoadTextureFromFile(scene, text3, name3, material, "_EmissionMap", textureWrapMode2, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag5 = true;
					}
					if (!flag5)
					{
						material.SetTexture("_EmissionMap", null);
						if (!flag4)
						{
							material.DisableKeyword("_EMISSION");
						}
					}
					bool flag6 = false;
					Color value3 = default(Color);
					if (flag && AssimpInterop.aiMaterial_GetSpecular(ptrMat, out value3))
					{
						value3.a = a;
						material.SetColor("_SpecColor", value3);
						flag6 = true;
					}
					if (!flag6)
					{
						material.SetColor("_SpecColor", Color.black);
					}
					bool flag7 = false;
					string text4 = default(string);
					uint num16 = default(uint);
					uint num17 = default(uint);
					float num18 = default(float);
					uint num19 = default(uint);
					uint num20 = default(uint);
					if (num2 != 0 && AssimpInterop.aiMaterial_GetTextureSpecular(ptrMat, 0u, out text4, out num16, out num17, out num18, out num19, out num20))
					{
						TextureWrapMode textureWrapMode3 = (TextureWrapMode)((num20 == 1) ? 1 : 0);
						string name4 = StringUtils.GenerateUniqueName(text4);
						Texture2DUtils.LoadTextureFromFile(scene, text4, name4, material, "_SpecGlossMap", textureWrapMode3, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag7 = true;
					}
					if (!flag7)
					{
						material.SetTexture("_SpecGlossMap", null);
						material.DisableKeyword("_SPECGLOSSMAP");
					}
					bool flag8 = false;
					uint num21 = AssimpInterop.aiMaterial_GetNumTextureNormals(ptrMat);
					string text5 = default(string);
					uint num22 = default(uint);
					uint num23 = default(uint);
					float num24 = default(float);
					uint num25 = default(uint);
					uint num26 = default(uint);
					if (num21 != 0 && AssimpInterop.aiMaterial_GetTextureNormals(ptrMat, 0u, out text5, out num22, out num23, out num24, out num25, out num26))
					{
						TextureWrapMode textureWrapMode4 = (TextureWrapMode)((num26 == 1) ? 1 : 0);
						string name5 = StringUtils.GenerateUniqueName(text5);
						Texture2DUtils.LoadTextureFromFile(scene, text5, name5, material, "_BumpMap", textureWrapMode4, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, true);
						flag8 = true;
					}
					bool flag9 = false;
					uint num27 = AssimpInterop.aiMaterial_GetNumTextureHeight(ptrMat);
					string text6 = default(string);
					uint num28 = default(uint);
					uint num29 = default(uint);
					float num30 = default(float);
					uint num31 = default(uint);
					uint num32 = default(uint);
					if (num27 != 0 && AssimpInterop.aiMaterial_GetTextureHeight(ptrMat, 0u, out text6, out num28, out num29, out num30, out num31, out num32))
					{
						TextureWrapMode textureWrapMode5 = (TextureWrapMode)((num32 == 1) ? 1 : 0);
						string name6 = StringUtils.GenerateUniqueName(text6);
						Texture2DUtils.LoadTextureFromFile(scene, text6, name6, material, "_BumpMap", textureWrapMode5, basePath, this.OnTextureLoaded, options.TextureCompression, textureFileNameWithoutExtension, false);
						flag9 = true;
					}
					if (!flag9 && !flag8)
					{
						material.SetTexture("_BumpMap", null);
						material.DisableKeyword("_NORMALMAP");
					}
					bool flag10 = false;
					float num33 = default(float);
					if (AssimpInterop.aiMaterial_HasBumpScaling(ptrMat) && AssimpInterop.aiMaterial_GetBumpScaling(ptrMat, out num33))
					{
						if (num33 == 0f)
						{
							num33 = 1f;
						}
						material.SetFloat("_BumpScale", num33);
						flag10 = true;
					}
					if (!flag10)
					{
						material.SetFloat("_BumpScale", 1f);
					}
					bool flag11 = false;
					float value4 = default(float);
					if (AssimpInterop.aiMaterial_HasShininess(ptrMat) && AssimpInterop.aiMaterial_GetShininess(ptrMat, out value4))
					{
						material.SetFloat("_Glossiness", value4);
						flag11 = true;
					}
					if (!flag11)
					{
						material.SetFloat("_Glossiness", 0.5f);
					}
					bool flag12 = false;
					if (AssimpInterop.aiMaterial_HasShininessStrength(ptrMat))
					{
						float value5 = default(float);
						if (AssimpInterop.aiMaterial_GetShininessStrength(ptrMat, out value5))
						{
							material.SetFloat("_GlossMapScale", value5);
							flag12 = true;
						}
						else
						{
							material.SetFloat("_GlossMapScale", 1f);
						}
					}
					if (!flag12)
					{
						material.SetFloat("_GlossMapScale", 1f);
					}
				}
				if (!((UnityEngine.Object)material == (UnityEngine.Object)null))
				{
					MaterialData materialData = new MaterialData();
					materialData.UnityMaterial = material;
					MaterialData materialData2 = materialData;
					this._materialData[num] = materialData2;
					if (this.OnMaterialCreated != null)
					{
						this.OnMaterialCreated(num, isOverriden, material);
					}
				}
			}
		}

		private void BuildBones(IntPtr scene)
		{
			uint num = AssimpInterop.aiScene_GetNumMeshes(scene);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				MeshData meshData = this._meshData[num2];
				IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num2);
				Mesh unityMesh = meshData.UnityMesh;
				if (AssimpInterop.aiMesh_HasBones(ptrMesh))
				{
					uint num3 = AssimpInterop.aiMesh_VertexCount(ptrMesh);
					uint num4 = AssimpInterop.aiMesh_GetNumBones(ptrMesh);
					Matrix4x4[] array = new Matrix4x4[num4];
					Transform[] array2 = new Transform[num4];
					BoneWeight[] array3 = new BoneWeight[num3];
					int[] array4 = new int[num3];
					for (uint num5 = 0u; num5 < num4; num5++)
					{
						IntPtr ptrBone = AssimpInterop.aiMesh_GetBone(ptrMesh, num5);
						string key = AssimpInterop.aiBone_GetName(ptrBone);
						if (this._nodeDataDictionary.ContainsKey(key))
						{
							NodeData nodeData = this._nodeDataDictionary[key];
							GameObject gameObject = nodeData.GameObject;
							Transform transform = array2[num5] = gameObject.transform;
							Matrix4x4 matrix4x = AssimpInterop.aiBone_GetOffsetMatrix(ptrBone);
							array[num5] = matrix4x;
							uint num6 = AssimpInterop.aiBone_GetNumWeights(ptrBone);
							for (uint num7 = 0u; num7 < num6; num7++)
							{
								IntPtr ptrVweight = AssimpInterop.aiBone_GetWeights(ptrBone, num7);
								float num8 = AssimpInterop.aiVertexWeight_GetWeight(ptrVweight);
								uint num9 = AssimpInterop.aiVertexWeight_GetVertexId(ptrVweight);
								int num10 = array4[num9];
								int num11 = (int)num5;
								BoneWeight boneWeight;
								switch (num10)
								{
								case 0:
								{
									BoneWeight boneWeight2 = default(BoneWeight);
									boneWeight2.boneIndex0 = num11;
									boneWeight2.weight0 = num8;
									boneWeight = boneWeight2;
									array3[num9] = boneWeight;
									break;
								}
								case 1:
									boneWeight = array3[num9];
									boneWeight.boneIndex1 = num11;
									boneWeight.weight1 = num8;
									array3[num9] = boneWeight;
									break;
								case 2:
									boneWeight = array3[num9];
									boneWeight.boneIndex2 = num11;
									boneWeight.weight2 = num8;
									array3[num9] = boneWeight;
									break;
								case 3:
									boneWeight = array3[num9];
									boneWeight.boneIndex3 = num11;
									boneWeight.weight3 = num8;
									array3[num9] = boneWeight;
									break;
								default:
									boneWeight = array3[num9];
									boneWeight.boneIndex3 = num11;
									boneWeight.weight3 = num8;
									array3[num9] = boneWeight;
									break;
								}
								array4[num9]++;
							}
						}
					}
					SkinnedMeshRenderer skinnedMeshRenderer = meshData.SkinnedMeshRenderer;
					skinnedMeshRenderer.bones = array2;
					unityMesh.bindposes = array;
					unityMesh.boneWeights = array3;
				}
				if (this.OnMeshCreated != null)
				{
					this.OnMeshCreated(num2, unityMesh);
				}
			}
		}

		private void BuildAnimations(GameObject wrapperGameObject, IntPtr scene, AssetLoaderOptions options)
		{
			uint num = AssimpInterop.aiScene_GetNumAnimations(scene);
			AnimationClip[] array = new AnimationClip[num];
			for (uint num2 = 0u; num2 < num; num2++)
			{
				IntPtr ptrAnimation = AssimpInterop.aiScene_GetAnimation(scene, num2);
				float num3 = AssimpInterop.aiAnimation_GetTicksPerSecond(ptrAnimation);
				if (num3 <= 0f)
				{
					num3 = 60f;
				}
				string text = AssimpInterop.aiAnimation_GetName(ptrAnimation);
				AnimationClip animationClip = new AnimationClip();
				animationClip.name = ((!string.IsNullOrEmpty(text)) ? text : ("Animation_" + StringUtils.GenerateUniqueName(num2)));
				animationClip.legacy = true;
				animationClip.frameRate = num3;
				AnimationClip animationClip2 = animationClip;
				float num4 = AssimpInterop.aiAnimation_GetDuraction(ptrAnimation);
				float animationLength = num4 / num3;
				uint num5 = AssimpInterop.aiAnimation_GetNumChannels(ptrAnimation);
				for (uint num6 = 0u; num6 < num5; num6++)
				{
					IntPtr ptrNodeAnim = AssimpInterop.aiAnimation_GetAnimationChannel(ptrAnimation, num6);
					string text2 = AssimpInterop.aiNodeAnim_GetNodeName(ptrNodeAnim);
					if (!string.IsNullOrEmpty(text2) && this._nodeDataDictionary.ContainsKey(text2))
					{
						NodeData nodeData = this._nodeDataDictionary[text2];
						uint num7 = AssimpInterop.aiNodeAnim_GetNumRotationKeys(ptrNodeAnim);
						if (num7 != 0)
						{
							AnimationCurve animationCurve = new AnimationCurve();
							AnimationCurve animationCurve2 = new AnimationCurve();
							AnimationCurve animationCurve3 = new AnimationCurve();
							AnimationCurve animationCurve4 = new AnimationCurve();
							for (uint num8 = 0u; num8 < num7; num8++)
							{
								IntPtr ptrQuatKey = AssimpInterop.aiNodeAnim_GetRotationKey(ptrNodeAnim, num8);
								float time = AssimpInterop.aiQuatKey_GetTime(ptrQuatKey) / num3;
								Quaternion quaternion = AssimpInterop.aiQuatKey_GetValue(ptrQuatKey);
								animationCurve.AddKey(time, quaternion.x);
								animationCurve2.AddKey(time, quaternion.y);
								animationCurve3.AddKey(time, quaternion.z);
								animationCurve4.AddKey(time, quaternion.w);
							}
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localRotation.x", AssetLoader.FixCurve(animationLength, animationCurve));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localRotation.y", AssetLoader.FixCurve(animationLength, animationCurve2));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localRotation.z", AssetLoader.FixCurve(animationLength, animationCurve3));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localRotation.w", AssetLoader.FixCurve(animationLength, animationCurve4));
						}
						uint num9 = AssimpInterop.aiNodeAnim_GetNumPositionKeys(ptrNodeAnim);
						if (num9 != 0)
						{
							AnimationCurve animationCurve5 = new AnimationCurve();
							AnimationCurve animationCurve6 = new AnimationCurve();
							AnimationCurve animationCurve7 = new AnimationCurve();
							for (uint num10 = 0u; num10 < num9; num10++)
							{
								IntPtr ptrVectorKey = AssimpInterop.aiNodeAnim_GetPositionKey(ptrNodeAnim, num10);
								float time2 = AssimpInterop.aiVectorKey_GetTime(ptrVectorKey) / num3;
								Vector3 vector = AssimpInterop.aiVectorKey_GetValue(ptrVectorKey);
								animationCurve5.AddKey(time2, vector.x);
								animationCurve6.AddKey(time2, vector.y);
								animationCurve7.AddKey(time2, vector.z);
							}
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localPosition.x", AssetLoader.FixCurve(animationLength, animationCurve5));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localPosition.y", AssetLoader.FixCurve(animationLength, animationCurve6));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localPosition.z", AssetLoader.FixCurve(animationLength, animationCurve7));
						}
						uint num11 = AssimpInterop.aiNodeAnim_GetNumScalingKeys(ptrNodeAnim);
						if (num11 != 0)
						{
							AnimationCurve animationCurve8 = new AnimationCurve();
							AnimationCurve animationCurve9 = new AnimationCurve();
							AnimationCurve animationCurve10 = new AnimationCurve();
							for (uint num12 = 0u; num12 < num11; num12++)
							{
								IntPtr ptrVectorKey2 = AssimpInterop.aiNodeAnim_GetScalingKey(ptrNodeAnim, num12);
								float time3 = AssimpInterop.aiVectorKey_GetTime(ptrVectorKey2) / num3;
								Vector3 vector2 = AssimpInterop.aiVectorKey_GetValue(ptrVectorKey2);
								animationCurve8.AddKey(time3, vector2.x);
								animationCurve9.AddKey(time3, vector2.y);
								animationCurve10.AddKey(time3, vector2.z);
							}
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localScale.x", AssetLoader.FixCurve(animationLength, animationCurve8));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localScale.y", AssetLoader.FixCurve(animationLength, animationCurve9));
							animationClip2.SetCurve(nodeData.Path, typeof(Transform), "localScale.z", AssetLoader.FixCurve(animationLength, animationCurve10));
						}
					}
				}
				animationClip2.EnsureQuaternionContinuity();
				animationClip2.wrapMode = options.AnimationWrapMode;
				array[num2] = animationClip2;
				if (this.OnAnimationClipCreated != null)
				{
					this.OnAnimationClipCreated(num2, animationClip2);
				}
			}
			if (options.UseLegacyAnimations)
			{
				Animation animation = wrapperGameObject.GetComponent<Animation>();
				if ((UnityEngine.Object)animation == (UnityEngine.Object)null)
				{
					animation = wrapperGameObject.AddComponent<Animation>();
				}
				AnimationClip clip = null;
				for (int i = 0; i < array.Length; i++)
				{
					AnimationClip animationClip3 = array[i];
					animation.AddClip(animationClip3, animationClip3.name);
					if (i == 0)
					{
						clip = animationClip3;
					}
				}
				animation.clip = clip;
				if (options.AutoPlayAnimations)
				{
					animation.Play();
				}
			}
			else
			{
				Animator animator = wrapperGameObject.GetComponent<Animator>();
				if ((UnityEngine.Object)animator == (UnityEngine.Object)null)
				{
					animator = wrapperGameObject.AddComponent<Animator>();
				}
				if ((UnityEngine.Object)options.AnimatorController != (UnityEngine.Object)null)
				{
					animator.runtimeAnimatorController = options.AnimatorController;
				}
				if ((UnityEngine.Object)options.Avatar != (UnityEngine.Object)null)
				{
					animator.avatar = options.Avatar;
				}
			}
		}

		private GameObject BuildWrapperObject(IntPtr scene, AssetLoaderOptions options, GameObject templateObject = null)
		{
			IntPtr intPtr = AssimpInterop.aiScene_GetRootNode(scene);
			NodeData nodeData = new NodeData();
			int id = this._nodeId++;
			nodeData.Node = intPtr;
			nodeData.Id = id;
			string arg = nodeData.Path = (nodeData.Name = this.FixName(AssimpInterop.aiNode_GetName(intPtr), id));
			object gameObject = templateObject;
			if (gameObject == null)
			{
				GameObject gameObject2 = new GameObject();
				gameObject2.name = string.Format("Wrapper_{0}", arg);
				gameObject = gameObject2;
			}
			GameObject gameObject3 = (GameObject)gameObject;
			GameObject gameObject4 = this.BuildObject(scene, nodeData, options);
			this.LoadContextOptions(gameObject4, options);
			gameObject4.transform.parent = gameObject3.transform;
			return gameObject3;
		}

		private GameObject BuildObject(IntPtr scene, NodeData nodeData, AssetLoaderOptions options)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = nodeData.Name;
			GameObject gameObject2 = gameObject;
			IntPtr node = nodeData.Node;
			uint num = AssimpInterop.aiNode_GetNumMeshes(node);
			bool flag = AssimpInterop.aiScene_HasMeshes(scene);
			if (num != 0 && flag)
			{
				for (uint num2 = 0u; num2 < num; num2++)
				{
					uint num3 = AssimpInterop.aiNode_GetMeshIndex(node, num2);
					IntPtr ptrMesh = AssimpInterop.aiScene_GetMesh(scene, num3);
					uint num4 = AssimpInterop.aiMesh_GetMatrialIndex(ptrMesh);
					Material material = null;
					if (this._materialData != null)
					{
						MaterialData materialData = this._materialData[num4];
						if (materialData != null)
						{
							material = materialData.UnityMaterial;
						}
					}
					if ((UnityEngine.Object)material == (UnityEngine.Object)null)
					{
						material = this._standardBaseMaterial;
					}
					MeshData meshData = this._meshData[num3];
					Mesh unityMesh = meshData.UnityMesh;
					gameObject = new GameObject();
					gameObject.name = string.Format("<{0}:Mesh:{1}>", gameObject2.name, num2);
					GameObject gameObject3 = gameObject;
					gameObject3.transform.parent = gameObject2.transform;
					MeshFilter meshFilter = gameObject3.AddComponent<MeshFilter>();
					meshFilter.mesh = unityMesh;
					if (AssimpInterop.aiMesh_HasBones(ptrMesh))
					{
						SkinnedMeshRenderer skinnedMeshRenderer = gameObject3.AddComponent<SkinnedMeshRenderer>();
						skinnedMeshRenderer.sharedMesh = unityMesh;
						skinnedMeshRenderer.quality = SkinQuality.Bone4;
						skinnedMeshRenderer.sharedMaterial = material;
						meshData.SkinnedMeshRenderer = skinnedMeshRenderer;
					}
					else
					{
						MeshRenderer meshRenderer = gameObject3.AddComponent<MeshRenderer>();
						meshRenderer.sharedMaterial = material;
						if (options.GenerateMeshColliders)
						{
							MeshCollider meshCollider = gameObject3.AddComponent<MeshCollider>();
							meshCollider.sharedMesh = unityMesh;
							meshCollider.convex = options.ConvexMeshColliders;
						}
					}
				}
			}
			if (nodeData.ParentNodeData != null)
			{
				gameObject2.transform.parent = nodeData.ParentNodeData.GameObject.transform;
			}
			gameObject2.transform.LoadMatrix(AssimpInterop.aiNode_GetTransformation(node), true);
			nodeData.GameObject = gameObject2;
			this._nodeDataDictionary.Add(nodeData.Name, nodeData);
			uint num5 = AssimpInterop.aiNode_GetNumChildren(node);
			if (num5 != 0)
			{
				for (uint num6 = 0u; num6 < num5; num6++)
				{
					IntPtr intPtr = AssimpInterop.aiNode_GetChildren(node, num6);
					int id = this._nodeId++;
					NodeData nodeData2 = new NodeData();
					nodeData2.ParentNodeData = nodeData;
					nodeData2.Node = intPtr;
					nodeData2.Id = id;
					nodeData2.Name = this.FixName(AssimpInterop.aiNode_GetName(intPtr), id);
					NodeData nodeData3 = nodeData2;
					nodeData3.Path = string.Format("{0}/{1}", nodeData.Path, nodeData3.Name);
					this.BuildObject(scene, nodeData3, options);
				}
			}
			return gameObject2;
		}

		private string FixName(string name, int id)
		{
			return (!this._nodeDataDictionary.ContainsKey(name) && !string.IsNullOrEmpty(name)) ? name : StringUtils.GenerateUniqueName(id);
		}

		private static AnimationCurve FixCurve(float animationLength, AnimationCurve curve)
		{
			if (Mathf.Approximately(animationLength, 0f))
			{
				animationLength = 1f;
			}
			if (curve.keys.Length == 1)
			{
				curve.AddKey(new Keyframe(animationLength, curve.keys[0].value));
			}
			return curve;
		}
	}
}
