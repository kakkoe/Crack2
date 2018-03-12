using System;
using System.IO;
using UnityEngine;

namespace TriLib
{
	public static class Texture2DUtils
	{
		public static void LoadTextureFromFile(IntPtr scene, string path, string name, Material material, string propertyName, TextureWrapMode textureWrapMode = TextureWrapMode.Repeat, string basePath = null, TextureLoadHandle onTextureLoaded = null, TextureCompression textureCompression = TextureCompression.None, string textureFileNameWithoutExtension = null, bool isNormalMap = false)
		{
			bool flag;
			byte[] array;
			string text;
			if (!string.IsNullOrEmpty(path))
			{
				if (path[0] == '*')
				{
					uint uintIndex = default(uint);
					if (!uint.TryParse(path.Substring(1), out uintIndex))
					{
						return;
					}
					flag = !AssimpInterop.aiMaterial_IsEmbeddedTextureCompressed(scene, uintIndex);
					uint uintSize = AssimpInterop.aiMaterial_GetEmbeddedTextureDataSize(scene, uintIndex, !flag);
					array = AssimpInterop.aiMaterial_GetEmbeddedTextureData(scene, uintIndex, uintSize);
					text = StringUtils.GenerateUniqueName(path);
					goto IL_00b7;
				}
				text = path;
				if (!File.Exists(text) && basePath != null)
				{
					text = Path.Combine(basePath, path);
				}
				if (!File.Exists(text))
				{
					string fileName = Path.GetFileName(path);
					if (basePath != null)
					{
						text = Path.Combine(basePath, fileName);
					}
				}
				if (File.Exists(text))
				{
					array = File.ReadAllBytes(text);
					flag = false;
					goto IL_00b7;
				}
			}
			return;
			IL_00b7:
			Texture2D texture2D;
			bool flag2;
			if (flag)
			{
				int num = Mathf.FloorToInt(Mathf.Sqrt((float)(array.Length / 4)));
				texture2D = new Texture2D(num, num, TextureFormat.ARGB32, false);
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
				flag2 = true;
			}
			else
			{
				texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, false);
				flag2 = texture2D.LoadImage(array);
			}
			texture2D.name = name;
			texture2D.wrapMode = textureWrapMode;
			if (flag2)
			{
				Color32[] pixels = texture2D.GetPixels32();
				Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, false);
				if (isNormalMap)
				{
					for (int i = 0; i < pixels.Length; i++)
					{
						Color32 color = pixels[i];
						color.a = color.r;
						color.r = 0;
						color.b = 0;
						pixels[i] = color;
					}
					texture2D2.SetPixels32(pixels);
					texture2D2.Apply();
				}
				else
				{
					texture2D2.SetPixels32(pixels);
					texture2D2.Apply();
					if (textureCompression != 0)
					{
						texture2D.Compress(textureCompression == TextureCompression.HighQuality);
					}
				}
				material.SetTexture(propertyName, texture2D2);
				if (onTextureLoaded != null)
				{
					onTextureLoaded(text, material, propertyName, texture2D2);
				}
			}
		}
	}
}
