using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace TriLib
{
	public static class AssimpInterop
	{
		public const string DllPath = "assimp";

		private const int MaxStringLength = 1024;

		private const int MaxInputStringLength = 2048;

		private static readonly bool Is32Bits = IntPtr.Size == 4;

		private static readonly int IntSize = (!AssimpInterop.Is32Bits) ? 8 : 4;

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCreatePropertyStore")]
		public static extern IntPtr _aiCreatePropertyStore();

		public static IntPtr ai_CreatePropertyStore()
		{
			return AssimpInterop._aiCreatePropertyStore();
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiReleasePropertyStore")]
		public static extern void _aiReleasePropertyStore(IntPtr ptrPropertyStore);

		public static void ai_CreateReleasePropertyStore(IntPtr ptrPropertyStore)
		{
			AssimpInterop._aiReleasePropertyStore(ptrPropertyStore);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFileExWithProperties")]
		public static extern IntPtr _aiImportFileEx(string filename, uint flags, IntPtr ptrFS, IntPtr ptrProps);

		public static IntPtr ai_ImportFileEx(string filename, uint flags, IntPtr ptrFS, IntPtr ptrProp)
		{
			return AssimpInterop._aiImportFileEx(filename, flags, ptrFS, ptrProp);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyInteger")]
		public static extern IntPtr _aiSetImportPropertyInteger(IntPtr ptrStore, IntPtr name, int value);

		public static IntPtr ai_SetImportPropertyInteger(IntPtr ptrStore, string name, int value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr result = AssimpInterop._aiSetImportPropertyInteger(ptrStore, stringBuffer.AddrOfPinnedObject(), value);
			stringBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyFloat")]
		public static extern IntPtr _aiSetImportPropertyFloat(IntPtr ptrStore, IntPtr name, float value);

		public static IntPtr ai_SetImportPropertyFloat(IntPtr ptrStore, string name, float value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr result = AssimpInterop._aiSetImportPropertyFloat(ptrStore, stringBuffer.AddrOfPinnedObject(), value);
			stringBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiSetImportPropertyString")]
		public static extern IntPtr _aiSetImportPropertyString(IntPtr ptrStore, IntPtr name, IntPtr ptrValue);

		public static IntPtr ai_SetImportPropertyString(IntPtr ptrStore, string name, string value)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(name);
			IntPtr assimpStringBuffer = AssimpInterop.GetAssimpStringBuffer(value);
			IntPtr result = AssimpInterop._aiSetImportPropertyString(ptrStore, stringBuffer.AddrOfPinnedObject(), assimpStringBuffer);
			stringBuffer.Free();
			Marshal.FreeHGlobal(assimpStringBuffer);
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiImportFile")]
		public static extern IntPtr _aiImportFile(string filename, uint flags);

		public static IntPtr ai_ImportFile(string filename, uint flags)
		{
			return AssimpInterop._aiImportFile(filename, flags);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiReleaseImport")]
		public static extern void _aiReleaseImport(IntPtr scene);

		public static void ai_ReleaseImport(IntPtr scene)
		{
			AssimpInterop._aiReleaseImport(scene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiGetExtensionList")]
		public static extern void _aiGetExtensionList(IntPtr ptrExtensionList);

		public static void ai_GetExtensionList(out string strExtensionList)
		{
			byte[] array = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out array);
			AssimpInterop._aiGetExtensionList(newStringBuffer.AddrOfPinnedObject());
			newStringBuffer.Free();
			long num = (!AssimpInterop.Is32Bits) ? BitConverter.ToInt64(array, 0) : BitConverter.ToInt32(array, 0);
			strExtensionList = Encoding.UTF8.GetString(array, AssimpInterop.IntSize, (int)num);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiGetErrorString")]
		public static extern IntPtr _aiGetErrorString();

		public static string ai_GetErrorString()
		{
			IntPtr ptr = AssimpInterop._aiGetErrorString();
			return Marshal.PtrToStringAnsi(ptr);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiIsExtensionSupported")]
		public static extern bool _aiIsExtensionSupported(IntPtr strExtension);

		public static bool ai_IsExtensionSupported(string strExtension)
		{
			GCHandle stringBuffer = AssimpInterop.GetStringBuffer(strExtension);
			bool result = AssimpInterop._aiIsExtensionSupported(stringBuffer.AddrOfPinnedObject());
			stringBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasMaterials")]
		private static extern bool _aiScene_HasMaterials(IntPtr ptrScene);

		public static bool aiScene_HasMaterials(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasMaterials(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumMaterials")]
		private static extern uint _aiScene_GetNumMaterials(IntPtr ptrScene);

		public static uint aiScene_GetNumMaterials(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumMaterials(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumMeshes")]
		private static extern uint _aiScene_GetNumMeshes(IntPtr ptrScene);

		public static uint aiScene_GetNumMeshes(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumMeshes(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumAnimations")]
		private static extern uint _aiScene_GetNumAnimations(IntPtr ptrScene);

		public static uint aiScene_GetNumAnimations(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumAnimations(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumCameras")]
		private static extern uint _aiScene_GetNumCameras(IntPtr ptrScene);

		public static uint aiScene_GetNumCameras(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumCameras(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetNumLights")]
		private static extern uint _aiScene_GetNumLights(IntPtr ptrScene);

		public static uint aiScene_GetNumLights(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetNumLights(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasMeshes")]
		private static extern bool _aiScene_HasMeshes(IntPtr ptrScene);

		public static bool aiScene_HasMeshes(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasMeshes(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasAnimation")]
		private static extern bool _aiScene_HasAnimation(IntPtr ptrScene);

		public static bool aiScene_HasAnimation(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasAnimation(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasCameras")]
		private static extern bool _aiScene_HasCameras(IntPtr ptrScene);

		public static bool aiScene_HasCameras(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasCameras(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_HasLights")]
		private static extern bool _aiScene_HasLights(IntPtr ptrScene);

		public static bool aiScene_HasLights(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_HasLights(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetRootNode")]
		private static extern IntPtr _aiScene_GetRootNode(IntPtr ptrScene);

		public static IntPtr aiScene_GetRootNode(IntPtr ptrScene)
		{
			return AssimpInterop._aiScene_GetRootNode(ptrScene);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetMaterial")]
		private static extern IntPtr _aiScene_GetMaterial(IntPtr ptrScene, uint uintIndex);

		public static IntPtr aiScene_GetMaterial(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetMaterial(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetMesh")]
		private static extern IntPtr _aiScene_GetMesh(IntPtr ptrScene, uint uintIndex);

		public static IntPtr aiScene_GetMesh(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetMesh(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetAnimation")]
		private static extern IntPtr _aiScene_GetAnimation(IntPtr ptrScene, uint uintIndex);

		public static IntPtr aiScene_GetAnimation(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetAnimation(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetCamera")]
		private static extern IntPtr _aiScene_GetCamera(IntPtr ptrScene, uint uintIndex);

		public static IntPtr aiScene_GetCamera(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetCamera(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiScene_GetLight")]
		private static extern IntPtr _aiScene_GetLight(IntPtr ptrScene, uint uintIndex);

		public static IntPtr aiScene_GetLight(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiScene_GetLight(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetName")]
		private static extern IntPtr _aiNode_GetName(IntPtr ptrNode);

		public static string aiNode_GetName(IntPtr ptrNode)
		{
			IntPtr pointer = AssimpInterop._aiNode_GetName(ptrNode);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetNumChildren")]
		private static extern uint _aiNode_GetNumChildren(IntPtr ptrNode);

		public static uint aiNode_GetNumChildren(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetNumChildren(ptrNode);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetNumMeshes")]
		private static extern uint _aiNode_GetNumMeshes(IntPtr ptrNode);

		public static uint aiNode_GetNumMeshes(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetNumMeshes(ptrNode);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetChildren")]
		private static extern IntPtr _aiNode_GetChildren(IntPtr ptrNode, uint uintIndex);

		public static IntPtr aiNode_GetChildren(IntPtr ptrNode, uint uintIndex)
		{
			return AssimpInterop._aiNode_GetChildren(ptrNode, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetMeshIndex")]
		private static extern uint _aiNode_GetMeshIndex(IntPtr ptrNode, uint uintIndex);

		public static uint aiNode_GetMeshIndex(IntPtr ptrNode, uint uintIndex)
		{
			return AssimpInterop._aiNode_GetMeshIndex(ptrNode, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetParent")]
		private static extern IntPtr _aiNode_GetParent(IntPtr ptrNode);

		public static IntPtr aiNode_GetParent(IntPtr ptrNode)
		{
			return AssimpInterop._aiNode_GetParent(ptrNode);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNode_GetTransformation")]
		private static extern IntPtr _aiNode_GetTransformation(IntPtr ptrNode);

		public static Matrix4x4 aiNode_GetTransformation(IntPtr ptrNode)
		{
			IntPtr pointer = AssimpInterop._aiNode_GetTransformation(ptrNode);
			float[] newFloat16Array = AssimpInterop.GetNewFloat16Array(pointer);
			return AssimpInterop.LoadMatrix4x4FromArray(newFloat16Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_IsEmbeddedTextureCompressed")]
		private static extern bool _aiMaterial_IsEmbeddedTextureCompressed(IntPtr ptrScene, uint uintIndex);

		public static bool aiMaterial_IsEmbeddedTextureCompressed(IntPtr ptrScene, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_IsEmbeddedTextureCompressed(ptrScene, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmbeddedTextureDataSize")]
		private static extern uint _aiMaterial_GetEmbeddedTextureDataSize(IntPtr ptrScene, uint uintIndex, bool boolCompressed);

		public static uint aiMaterial_GetEmbeddedTextureDataSize(IntPtr ptrScene, uint uintIndex, bool boolCompressed)
		{
			return AssimpInterop._aiMaterial_GetEmbeddedTextureDataSize(ptrScene, uintIndex, boolCompressed);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmbeddedTextureData")]
		private static extern void _aiMaterial_GetEmbeddedTextureData(IntPtr ptrScene, IntPtr ptrData, uint uintIndex, uint uintSize);

		public static byte[] aiMaterial_GetEmbeddedTextureData(IntPtr ptrScene, uint uintIndex, uint uintSize)
		{
			byte[] array = new byte[uintSize];
			GCHandle gCHandle = AssimpInterop.LockGc(array);
			AssimpInterop._aiMaterial_GetEmbeddedTextureData(ptrScene, gCHandle.AddrOfPinnedObject(), uintIndex, uintSize);
			gCHandle.Free();
			return array;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureCount")]
		private static extern uint _aiMaterial_GetTextureCount(IntPtr ptrMat, uint uintType);

		public static uint aiMaterial_GetTextureCount(IntPtr ptrMat, uint uintType)
		{
			return AssimpInterop._aiMaterial_GetTextureCount(ptrMat, uintType);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureDiffuse")]
		private static extern bool _aiMaterial_HasTextureDiffuse(IntPtr ptrMat, uint uintType);

		public static bool aiMaterial_HasTextureDiffuse(IntPtr ptrMat, uint uintType)
		{
			return AssimpInterop._aiMaterial_HasTextureDiffuse(ptrMat, uintType);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureDiffuse")]
		private static extern bool _aiMaterial_GetTextureDiffuse(IntPtr ptrMat, uint uintType, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		public static bool aiMaterial_GetTextureDiffuse(IntPtr ptrMat, uint uintType, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureDiffuse(ptrMat, uintType, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureDiffuse")]
		private static extern uint _aiMaterial_GetNumTextureDiffuse(IntPtr ptrMat);

		public static uint aiMaterial_GetNumTextureDiffuse(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureDiffuse(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureEmissive")]
		private static extern bool _aiMaterial_HasTextureEmissive(IntPtr ptrMat, uint uintIndex);

		public static bool aiMaterial_HasTextureEmissive(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureEmissive(ptrMat, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureEmissive")]
		private static extern bool _aiMaterial_GetTextureEmissive(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		public static bool aiMaterial_GetTextureEmissive(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureEmissive(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureEmissive")]
		private static extern uint _aiMaterial_GetNumTextureEmissive(IntPtr ptrMat);

		public static uint aiMaterial_GetNumTextureEmissive(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureEmissive(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureSpecular")]
		private static extern bool _aiMaterial_HasTextureSpecular(IntPtr ptrMat, uint uintIndex);

		public static bool aiMaterial_HasTextureSpecular(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureSpecular(ptrMat, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureSpecular")]
		private static extern bool _aiMaterial_GetTextureSpecular(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		public static bool aiMaterial_GetTextureSpecular(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureSpecular(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureSpecular")]
		private static extern uint _aiMaterial_GetNumTextureSpecular(IntPtr ptrMat);

		public static uint aiMaterial_GetNumTextureSpecular(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureSpecular(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureNormals")]
		private static extern bool _aiMaterial_HasTextureNormals(IntPtr ptrMat, uint uintIndex);

		public static bool aiMaterial_HasTextureNormals(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureNormals(ptrMat, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureNormals")]
		private static extern bool _aiMaterial_GetTextureNormals(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		public static bool aiMaterial_GetTextureNormals(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureNormals(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureNormals")]
		private static extern uint _aiMaterial_GetNumTextureNormals(IntPtr ptrMat);

		public static uint aiMaterial_GetNumTextureNormals(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureNormals(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasTextureHeight")]
		private static extern bool _aiMaterial_HasTextureHeight(IntPtr ptrMat, uint uintIndex);

		public static bool aiMaterial_HasTextureHeight(IntPtr ptrMat, uint uintIndex)
		{
			return AssimpInterop._aiMaterial_HasTextureHeight(ptrMat, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetTextureHeight")]
		private static extern bool _aiMaterial_GetTextureHeight(IntPtr ptrMat, uint uintIndex, IntPtr strPath, IntPtr uintMapping, IntPtr uintUvIndex, IntPtr floatBlend, IntPtr uintOp, IntPtr uintMapMode);

		public static bool aiMaterial_GetTextureHeight(IntPtr ptrMat, uint uintIndex, out string strPath, out uint uintMapping, out uint uintUvIndex, out float floatBlend, out uint uintOp, out uint uintMapMode)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			GCHandle newUIntBuffer = AssimpInterop.GetNewUIntBuffer(out uintMapping);
			GCHandle newUIntBuffer2 = AssimpInterop.GetNewUIntBuffer(out uintUvIndex);
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatBlend);
			GCHandle newUIntBuffer3 = AssimpInterop.GetNewUIntBuffer(out uintOp);
			GCHandle newUIntBuffer4 = AssimpInterop.GetNewUIntBuffer(out uintMapMode);
			bool result = AssimpInterop._aiMaterial_GetTextureHeight(ptrMat, uintIndex, newStringBuffer.AddrOfPinnedObject(), newUIntBuffer.AddrOfPinnedObject(), newUIntBuffer2.AddrOfPinnedObject(), newFloatBuffer.AddrOfPinnedObject(), newUIntBuffer3.AddrOfPinnedObject(), newUIntBuffer4.AddrOfPinnedObject());
			strPath = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			newUIntBuffer.Free();
			newUIntBuffer2.Free();
			newFloatBuffer.Free();
			newUIntBuffer3.Free();
			newUIntBuffer4.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetNumTextureHeight")]
		private static extern uint _aiMaterial_GetNumTextureHeight(IntPtr ptrMat);

		public static uint aiMaterial_GetNumTextureHeight(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_GetNumTextureHeight(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasAmbient")]
		private static extern bool _aiMaterial_HasAmbient(IntPtr ptrMat);

		public static bool aiMaterial_HasAmbient(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasAmbient(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetAmbient")]
		private static extern bool _aiMaterial_GetAmbient(IntPtr ptrMat, IntPtr colorOut);

		public static bool aiMaterial_GetAmbient(IntPtr ptrMat, out Color colorOut)
		{
			float[] array = default(float[]);
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetAmbient(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasDiffuse")]
		private static extern bool _aiMaterial_HasDiffuse(IntPtr ptrMat);

		public static bool aiMaterial_HasDiffuse(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasDiffuse(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetDiffuse")]
		private static extern bool _aiMaterial_GetDiffuse(IntPtr ptrMat, IntPtr colorOut);

		public static bool aiMaterial_GetDiffuse(IntPtr ptrMat, out Color colorOut)
		{
			float[] array = default(float[]);
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetDiffuse(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasSpecular")]
		private static extern bool _aiMaterial_HasSpecular(IntPtr ptrMat);

		public static bool aiMaterial_HasSpecular(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasSpecular(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetSpecular")]
		private static extern bool _aiMaterial_GetSpecular(IntPtr ptrMat, IntPtr colorOut);

		public static bool aiMaterial_GetSpecular(IntPtr ptrMat, out Color colorOut)
		{
			float[] array = default(float[]);
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetSpecular(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasEmissive")]
		private static extern bool _aiMaterial_HasEmissive(IntPtr ptrMat);

		public static bool aiMaterial_HasEmissive(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasEmissive(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetEmissive")]
		private static extern bool _aiMaterial_GetEmissive(IntPtr ptrMat, IntPtr colorOut);

		public static bool aiMaterial_GetEmissive(IntPtr ptrMat, out Color colorOut)
		{
			float[] array = default(float[]);
			GCHandle newFloat4Buffer = AssimpInterop.GetNewFloat4Buffer(out array);
			bool result = AssimpInterop._aiMaterial_GetEmissive(ptrMat, newFloat4Buffer.AddrOfPinnedObject());
			colorOut = AssimpInterop.LoadColorFromArray(array);
			newFloat4Buffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasName")]
		private static extern bool _aiMaterial_HasName(IntPtr ptrMat);

		public static bool aiMaterial_HasName(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasName(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetName")]
		private static extern bool _aiMaterial_GetName(IntPtr ptrMat, IntPtr strName);

		public static bool aiMaterial_GetName(IntPtr ptrMat, out string strName)
		{
			byte[] value = default(byte[]);
			GCHandle newStringBuffer = AssimpInterop.GetNewStringBuffer(out value);
			bool result = AssimpInterop._aiMaterial_GetName(ptrMat, newStringBuffer.AddrOfPinnedObject());
			strName = AssimpInterop.ByteArrayToString(value);
			newStringBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasBumpScaling")]
		private static extern bool _aiMaterial_HasBumpScaling(IntPtr ptrMat);

		public static bool aiMaterial_HasBumpScaling(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasBumpScaling(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetBumpScaling")]
		private static extern bool _aiMaterial_GetBumpScaling(IntPtr ptrMat, IntPtr floatOut);

		public static bool aiMaterial_GetBumpScaling(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetBumpScaling(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasShininess")]
		private static extern bool _aiMaterial_HasShininess(IntPtr ptrMat);

		public static bool aiMaterial_HasShininess(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasShininess(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetShininess")]
		private static extern bool _aiMaterial_GetShininess(IntPtr ptrMat, IntPtr floatOut);

		public static bool aiMaterial_GetShininess(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetShininess(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasShininessStrength")]
		private static extern bool _aiMaterial_HasShininessStrength(IntPtr ptrMat);

		public static bool aiMaterial_HasShininessStrength(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasShininessStrength(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetShininessStrength")]
		private static extern bool _aiMaterial_GetShininessStrength(IntPtr ptrMat, IntPtr floatOut);

		public static bool aiMaterial_GetShininessStrength(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetShininessStrength(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_HasOpacity")]
		private static extern bool _aiMaterial_HasOpacity(IntPtr ptrMat);

		public static bool aiMaterial_HasOpacity(IntPtr ptrMat)
		{
			return AssimpInterop._aiMaterial_HasOpacity(ptrMat);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMaterial_GetOpacity")]
		private static extern bool _aiMaterial_GetOpacity(IntPtr ptrMat, IntPtr floatOut);

		public static bool aiMaterial_GetOpacity(IntPtr ptrMat, out float floatOut)
		{
			GCHandle newFloatBuffer = AssimpInterop.GetNewFloatBuffer(out floatOut);
			bool result = AssimpInterop._aiMaterial_GetOpacity(ptrMat, newFloatBuffer.AddrOfPinnedObject());
			newFloatBuffer.Free();
			return result;
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_VertexCount")]
		private static extern uint _aiMesh_VertexCount(IntPtr ptrMesh);

		public static uint aiMesh_VertexCount(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_VertexCount(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasNormals")]
		private static extern bool _aiMesh_HasNormals(IntPtr ptrMesh);

		public static bool aiMesh_HasNormals(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasNormals(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasTangentsAndBitangents")]
		private static extern bool _aiMesh_HasTangentsAndBitangents(IntPtr ptrMesh);

		public static bool aiMesh_HasTangentsAndBitangents(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasTangentsAndBitangents(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasTextureCoords")]
		private static extern bool _aiMesh_HasTextureCoords(IntPtr ptrMesh, uint uintIndex);

		public static bool aiMesh_HasTextureCoords(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_HasTextureCoords(ptrMesh, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasVertexColors")]
		private static extern bool _aiMesh_HasVertexColors(IntPtr ptrMesh, uint uintIndex);

		public static bool aiMesh_HasVertexColors(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_HasVertexColors(ptrMesh, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetVertex")]
		private static extern IntPtr _aiMesh_GetVertex(IntPtr ptrMesh, uint uintIndex);

		public static Vector3 aiMesh_GetVertex(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetVertex(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNormal")]
		private static extern IntPtr _aiMesh_GetNormal(IntPtr ptrMesh, uint uintIndex);

		public static Vector3 aiMesh_GetNormal(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetNormal(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetTangent")]
		private static extern IntPtr _aiMesh_GetTangent(IntPtr ptrMesh, uint uintIndex);

		public static Vector3 aiMesh_GetTangent(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetTangent(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetBitangent")]
		private static extern IntPtr _aiMesh_GetBitangent(IntPtr ptrMesh, uint uintIndex);

		public static Vector3 aiMesh_GetBitangent(IntPtr ptrMesh, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetBitangent(ptrMesh, uintIndex);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetTextureCoord")]
		private static extern IntPtr _aiMesh_GetTextureCoord(IntPtr ptrMesh, uint uintChannel, uint uintIndex);

		public static Vector2 aiMesh_GetTextureCoord(IntPtr ptrMesh, uint uintChannel, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetTextureCoord(ptrMesh, uintChannel, uintIndex);
			float[] newFloat2Array = AssimpInterop.GetNewFloat2Array(pointer);
			return AssimpInterop.LoadVector2FromArray(newFloat2Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetVertexColor")]
		private static extern IntPtr _aiMesh_GetVertexColor(IntPtr ptrMesh, uint uintChannel, uint uintIndex);

		public static Color aiMesh_GetVertexColor(IntPtr ptrMesh, uint uintChannel, uint uintIndex)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetVertexColor(ptrMesh, uintChannel, uintIndex);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetMatrialIndex")]
		private static extern uint _aiMesh_GetMatrialIndex(IntPtr ptrMesh);

		public static uint aiMesh_GetMatrialIndex(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetMatrialIndex(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetName")]
		private static extern IntPtr _aiMesh_GetName(IntPtr ptrMesh);

		public static string aiMesh_GetName(IntPtr ptrMesh)
		{
			IntPtr pointer = AssimpInterop._aiMesh_GetName(ptrMesh);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasFaces")]
		private static extern bool _aiMesh_HasFaces(IntPtr ptrMesh);

		public static bool aiMesh_HasFaces(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasFaces(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNumFaces")]
		private static extern uint _aiMesh_GetNumFaces(IntPtr ptrMesh);

		public static uint aiMesh_GetNumFaces(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetNumFaces(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetFace")]
		private static extern IntPtr _aiMesh_GetFace(IntPtr ptrMesh, uint uintIndex);

		public static IntPtr aiMesh_GetFace(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_GetFace(ptrMesh, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_HasBones")]
		private static extern bool _aiMesh_HasBones(IntPtr ptrMesh);

		public static bool aiMesh_HasBones(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_HasBones(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetNumBones")]
		private static extern uint _aiMesh_GetNumBones(IntPtr ptrMesh);

		public static uint aiMesh_GetNumBones(IntPtr ptrMesh)
		{
			return AssimpInterop._aiMesh_GetNumBones(ptrMesh);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiMesh_GetBone")]
		private static extern IntPtr _aiMesh_GetBone(IntPtr ptrMesh, uint uintIndex);

		public static IntPtr aiMesh_GetBone(IntPtr ptrMesh, uint uintIndex)
		{
			return AssimpInterop._aiMesh_GetBone(ptrMesh, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiFace_GetNumIndices")]
		private static extern uint _aiFace_GetNumIndices(IntPtr ptrFace);

		public static uint aiFace_GetNumIndices(IntPtr ptrFace)
		{
			return AssimpInterop._aiFace_GetNumIndices(ptrFace);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiFace_GetIndex")]
		private static extern uint _aiFace_GetIndex(IntPtr ptrFace, uint uintIndex);

		public static uint aiFace_GetIndex(IntPtr ptrFace, uint uintIndex)
		{
			return AssimpInterop._aiFace_GetIndex(ptrFace, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetName")]
		private static extern IntPtr _aiBone_GetName(IntPtr ptrBone);

		public static string aiBone_GetName(IntPtr ptrBone)
		{
			IntPtr pointer = AssimpInterop._aiBone_GetName(ptrBone);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetNumWeights")]
		private static extern uint _aiBone_GetNumWeights(IntPtr ptrBone);

		public static uint aiBone_GetNumWeights(IntPtr ptrBone)
		{
			return AssimpInterop._aiBone_GetNumWeights(ptrBone);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetWeights")]
		private static extern IntPtr _aiBone_GetWeights(IntPtr ptrBone, uint uintIndex);

		public static IntPtr aiBone_GetWeights(IntPtr ptrBone, uint uintIndex)
		{
			return AssimpInterop._aiBone_GetWeights(ptrBone, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiBone_GetOffsetMatrix")]
		private static extern IntPtr _aiBone_GetOffsetMatrix(IntPtr ptrBone);

		public static Matrix4x4 aiBone_GetOffsetMatrix(IntPtr ptrBone)
		{
			IntPtr pointer = AssimpInterop._aiBone_GetOffsetMatrix(ptrBone);
			float[] newFloat16Array = AssimpInterop.GetNewFloat16Array(pointer);
			return AssimpInterop.LoadMatrix4x4FromArray(newFloat16Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVertexWeight_GetWeight")]
		private static extern float _aiVertexWeight_GetWeight(IntPtr ptrVweight);

		public static float aiVertexWeight_GetWeight(IntPtr ptrVweight)
		{
			return AssimpInterop._aiVertexWeight_GetWeight(ptrVweight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVertexWeight_GetVertexId")]
		private static extern uint _aiVertexWeight_GetVertexId(IntPtr ptrVweight);

		public static uint aiVertexWeight_GetVertexId(IntPtr ptrVweight)
		{
			return AssimpInterop._aiVertexWeight_GetVertexId(ptrVweight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetName")]
		private static extern IntPtr _aiAnimation_GetName(IntPtr ptrAnimation);

		public static string aiAnimation_GetName(IntPtr ptrAnimation)
		{
			IntPtr pointer = AssimpInterop._aiAnimation_GetName(ptrAnimation);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetDuraction")]
		private static extern float _aiAnimation_GetDuraction(IntPtr ptrAnimation);

		public static float aiAnimation_GetDuraction(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetDuraction(ptrAnimation);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetTicksPerSecond")]
		private static extern float _aiAnimation_GetTicksPerSecond(IntPtr ptrAnimation);

		public static float aiAnimation_GetTicksPerSecond(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetTicksPerSecond(ptrAnimation);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumChannels")]
		private static extern uint _aiAnimation_GetNumChannels(IntPtr ptrAnimation);

		public static uint aiAnimation_GetNumChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumChannels(ptrAnimation);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumMorphChannels")]
		private static extern uint _aiAnimation_GetNumMorphChannels(IntPtr ptrAnimation);

		public static uint aiAnimation_GetNumMorphChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumMorphChannels(ptrAnimation);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetNumMeshChannels")]
		private static extern uint _aiAnimation_GetNumMeshChannels(IntPtr ptrAnimation);

		public static uint aiAnimation_GetNumMeshChannels(IntPtr ptrAnimation)
		{
			return AssimpInterop._aiAnimation_GetNumMeshChannels(ptrAnimation);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiAnimation_GetAnimationChannel")]
		private static extern IntPtr _aiAnimation_GetAnimationChannel(IntPtr ptrAnimation, uint uintIndex);

		public static IntPtr aiAnimation_GetAnimationChannel(IntPtr ptrAnimation, uint uintIndex)
		{
			return AssimpInterop._aiAnimation_GetAnimationChannel(ptrAnimation, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNodeName")]
		private static extern IntPtr _aiNodeAnim_GetNodeName(IntPtr ptrNodeAnim);

		public static string aiNodeAnim_GetNodeName(IntPtr ptrNodeAnim)
		{
			IntPtr pointer = AssimpInterop._aiNodeAnim_GetNodeName(ptrNodeAnim);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumPositionKeys")]
		private static extern uint _aiNodeAnim_GetNumPositionKeys(IntPtr ptrNodeAnim);

		public static uint aiNodeAnim_GetNumPositionKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumPositionKeys(ptrNodeAnim);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumRotationKeys")]
		private static extern uint _aiNodeAnim_GetNumRotationKeys(IntPtr ptrNodeAnim);

		public static uint aiNodeAnim_GetNumRotationKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumRotationKeys(ptrNodeAnim);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetNumScalingKeys")]
		private static extern uint _aiNodeAnim_GetNumScalingKeys(IntPtr ptrNodeAnim);

		public static uint aiNodeAnim_GetNumScalingKeys(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetNumScalingKeys(ptrNodeAnim);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPostState")]
		private static extern uint _aiNodeAnim_GetPostState(IntPtr ptrNodeAnim);

		public static uint aiNodeAnim_GetPostState(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetPostState(ptrNodeAnim);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPreState")]
		private static extern uint _aiNodeAnim_GetPreState(IntPtr ptrNodeAnim);

		public static uint aiNodeAnim_GetPreState(IntPtr ptrNodeAnim)
		{
			return AssimpInterop._aiNodeAnim_GetPreState(ptrNodeAnim);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetPositionKey")]
		private static extern IntPtr _aiNodeAnim_GetPositionKey(IntPtr ptrNodeAnim, uint uintIndex);

		public static IntPtr aiNodeAnim_GetPositionKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetPositionKey(ptrNodeAnim, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetRotationKey")]
		private static extern IntPtr _aiNodeAnim_GetRotationKey(IntPtr ptrNodeAnim, uint uintIndex);

		public static IntPtr aiNodeAnim_GetRotationKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetRotationKey(ptrNodeAnim, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiNodeAnim_GetScalingKey")]
		private static extern IntPtr _aiNodeAnim_GetScalingKey(IntPtr ptrNodeAnim, uint uintIndex);

		public static IntPtr aiNodeAnim_GetScalingKey(IntPtr ptrNodeAnim, uint uintIndex)
		{
			return AssimpInterop._aiNodeAnim_GetScalingKey(ptrNodeAnim, uintIndex);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVectorKey_GetTime")]
		private static extern float _aiVectorKey_GetTime(IntPtr ptrVectorKey);

		public static float aiVectorKey_GetTime(IntPtr ptrVectorKey)
		{
			return AssimpInterop._aiVectorKey_GetTime(ptrVectorKey);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiVectorKey_GetValue")]
		private static extern IntPtr _aiVectorKey_GetValue(IntPtr ptrVectorKey);

		public static Vector3 aiVectorKey_GetValue(IntPtr ptrVectorKey)
		{
			IntPtr pointer = AssimpInterop._aiVectorKey_GetValue(ptrVectorKey);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiQuatKey_GetTime")]
		private static extern float _aiQuatKey_GetTime(IntPtr ptrQuatKey);

		public static float aiQuatKey_GetTime(IntPtr ptrQuatKey)
		{
			return AssimpInterop._aiQuatKey_GetTime(ptrQuatKey);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiQuatKey_GetValue")]
		private static extern IntPtr _aiQuatKey_GetValue(IntPtr ptrQuatKey);

		public static Quaternion aiQuatKey_GetValue(IntPtr ptrQuatKey)
		{
			IntPtr pointer = AssimpInterop._aiQuatKey_GetValue(ptrQuatKey);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadQuaternionFromArray(newFloat4Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetAspect")]
		private static extern float _aiCamera_GetAspect(IntPtr ptrCamera);

		public static float aiCamera_GetAspect(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetAspect(ptrCamera);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetClipPlaneFar")]
		private static extern float _aiCamera_GetClipPlaneFar(IntPtr ptrCamera);

		public static float aiCamera_GetClipPlaneFar(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetClipPlaneFar(ptrCamera);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetClipPlaneNear")]
		private static extern float _aiCamera_GetClipPlaneNear(IntPtr ptrCamera);

		public static float aiCamera_GetClipPlaneNear(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetClipPlaneNear(ptrCamera);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetHorizontalFOV")]
		private static extern float _aiCamera_GetHorizontalFOV(IntPtr ptrCamera);

		public static float aiCamera_GetHorizontalFOV(IntPtr ptrCamera)
		{
			return AssimpInterop._aiCamera_GetHorizontalFOV(ptrCamera);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetLookAt")]
		private static extern IntPtr _aiCamera_GetLookAt(IntPtr ptrCamera);

		public static Vector3 aiCamera_GetLookAt(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetLookAt(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetName")]
		private static extern IntPtr _aiCamera_GetName(IntPtr ptrCamera);

		public static string aiCamera_GetName(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetName(ptrCamera);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetPosition")]
		private static extern IntPtr _aiCamera_GetPosition(IntPtr ptrCamera);

		public static Vector3 aiCamera_GetPosition(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetPosition(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiCamera_GetUp")]
		private static extern IntPtr _aiCamera_GetUp(IntPtr ptrCamera);

		public static Vector3 aiCamera_GetUp(IntPtr ptrCamera)
		{
			IntPtr pointer = AssimpInterop._aiCamera_GetUp(ptrCamera);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAngleInnerCone")]
		private static extern float _aiLight_GetAngleInnerCone(IntPtr ptrLight);

		public static float aiLight_GetAngleInnerCone(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAngleInnerCone(ptrLight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAngleOuterCone")]
		private static extern float _aiLight_GetAngleOuterCone(IntPtr ptrLight);

		public static float aiLight_GetAngleOuterCone(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAngleOuterCone(ptrLight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationConstant")]
		private static extern float _aiLight_GetAttenuationConstant(IntPtr ptrLight);

		public static float aiLight_GetAttenuationConstant(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationConstant(ptrLight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationLinear")]
		private static extern float _aiLight_GetAttenuationLinear(IntPtr ptrLight);

		public static float aiLight_GetAttenuationLinear(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationLinear(ptrLight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetAttenuationQuadratic")]
		private static extern float _aiLight_GetAttenuationQuadratic(IntPtr ptrLight);

		public static float aiLight_GetAttenuationQuadratic(IntPtr ptrLight)
		{
			return AssimpInterop._aiLight_GetAttenuationQuadratic(ptrLight);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorAmbient")]
		private static extern IntPtr _aiLight_GetColorAmbient(IntPtr ptrLight);

		public static Color aiLight_GetColorAmbient(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorAmbient(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorDiffuse")]
		private static extern IntPtr _aiLight_GetColorDiffuse(IntPtr ptrLight);

		public static Color aiLight_GetColorDiffuse(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorDiffuse(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetColorSpecular")]
		private static extern IntPtr _aiLight_GetColorSpecular(IntPtr ptrLight);

		public static Color aiLight_GetColorSpecular(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetColorSpecular(ptrLight);
			float[] newFloat4Array = AssimpInterop.GetNewFloat4Array(pointer);
			return AssimpInterop.LoadColorFromArray(newFloat4Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetDirection")]
		private static extern IntPtr _aiLight_GetDirection(IntPtr ptrLight);

		public static Vector3 aiLight_GetDirection(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetDirection(ptrLight);
			float[] newFloat3Array = AssimpInterop.GetNewFloat3Array(pointer);
			return AssimpInterop.LoadVector3FromArray(newFloat3Array);
		}

		[DllImport("assimp", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "aiLight_GetName")]
		private static extern IntPtr _aiLight_GetName(IntPtr ptrLight);

		public static string aiLight_GetName(IntPtr ptrLight)
		{
			IntPtr pointer = AssimpInterop._aiLight_GetName(ptrLight);
			return AssimpInterop.ReadStringFromPointer(pointer);
		}

		public static byte[] StringToByteArray(string str, int length)
		{
			return Encoding.ASCII.GetBytes(str.PadRight(length, '\0'));
		}

		public static GCHandle LockGc(object value)
		{
			return GCHandle.Alloc(value, GCHandleType.Pinned);
		}

		public static GCHandle GetStringBuffer(string value)
		{
			byte[] value2 = AssimpInterop.StringToByteArray(value, 1024);
			return AssimpInterop.LockGc(value2);
		}

		public static string ByteArrayToString(byte[] value)
		{
			int num = Array.IndexOf(value, (byte)0, 0);
			if (num < 0)
			{
				num = value.Length;
			}
			return Encoding.ASCII.GetString(value, 0, num);
		}

		public static IntPtr GetAssimpStringBuffer(string value)
		{
			int num = (!AssimpInterop.Is32Bits) ? 8 : 4;
			IntPtr intPtr = Marshal.AllocHGlobal(num + value.Length);
			if (AssimpInterop.Is32Bits)
			{
				Marshal.WriteInt32(intPtr, value.Length);
			}
			Marshal.WriteInt64(intPtr, value.Length);
			byte[] bytes = Encoding.ASCII.GetBytes(value);
			Marshal.Copy(bytes, 0, new IntPtr((!AssimpInterop.Is32Bits) ? (intPtr.ToInt64() + num) : intPtr.ToInt32()), value.Length);
			return intPtr;
		}

		private static GCHandle GetNewStringBuffer(out byte[] byteArray)
		{
			byteArray = new byte[2048];
			return AssimpInterop.LockGc(byteArray);
		}

		private static GCHandle GetNewFloatBuffer(out float value)
		{
			value = 0f;
			return AssimpInterop.LockGc(value);
		}

		private static GCHandle GetNewFloat2Buffer(out float[] array)
		{
			array = new float[2];
			return AssimpInterop.LockGc(array);
		}

		private static GCHandle GetNewFloat3Buffer(out float[] array)
		{
			array = new float[3];
			return AssimpInterop.LockGc(array);
		}

		private static GCHandle GetNewFloat4Buffer(out float[] array)
		{
			array = new float[4];
			return AssimpInterop.LockGc(array);
		}

		private static GCHandle GetNewFloat16Buffer(out float[] array)
		{
			array = new float[16];
			return AssimpInterop.LockGc(array);
		}

		private static GCHandle GetNewUIntBuffer(out uint value)
		{
			value = 0u;
			return AssimpInterop.LockGc(value);
		}

		private static float[] GetNewFloat2Array(IntPtr pointer)
		{
			float[] array = new float[2];
			Marshal.Copy(pointer, array, 0, 2);
			return array;
		}

		private static float[] GetNewFloat3Array(IntPtr pointer)
		{
			float[] array = new float[3];
			Marshal.Copy(pointer, array, 0, 3);
			return array;
		}

		private static float[] GetNewFloat4Array(IntPtr pointer)
		{
			float[] array = new float[4];
			Marshal.Copy(pointer, array, 0, 4);
			return array;
		}

		private static float[] GetNewFloat16Array(IntPtr pointer)
		{
			float[] array = new float[16];
			Marshal.Copy(pointer, array, 0, 16);
			return array;
		}

		private static string ReadStringFromPointer(IntPtr pointer)
		{
			return Marshal.PtrToStringAnsi(pointer);
		}

		private static Vector2 LoadVector2FromArray(float[] array)
		{
			return new Vector2(array[0], array[1]);
		}

		private static Vector3 LoadVector3FromArray(float[] array)
		{
			return new Vector3(array[0], array[1], array[2]);
		}

		private static Color LoadColorFromArray(float[] array)
		{
			return new Color(array[0], array[1], array[2], array[3]);
		}

		private static Quaternion LoadQuaternionFromArray(float[] array)
		{
			return new Quaternion(array[1], array[2], array[3], array[0]);
		}

		private static Matrix4x4 LoadMatrix4x4FromArray(float[] array)
		{
			Matrix4x4 result = default(Matrix4x4);
			result[0] = array[0];
			result[1] = array[4];
			result[2] = array[8];
			result[3] = array[12];
			result[4] = array[1];
			result[5] = array[5];
			result[6] = array[9];
			result[7] = array[13];
			result[8] = array[2];
			result[9] = array[6];
			result[10] = array[10];
			result[11] = array[14];
			result[12] = array[3];
			result[13] = array[7];
			result[14] = array[11];
			result[15] = array[15];
			return result;
		}
	}
}
