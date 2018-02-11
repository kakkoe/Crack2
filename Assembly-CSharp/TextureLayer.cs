using System.Collections.Generic;
using UnityEngine;

public class TextureLayer
{
	public string texture = string.Empty;

	public Color color;

	public float opacity = 1f;

	public List<TextureLayerMask> masks = new List<TextureLayerMask>();

	public bool required;

	public bool isDecal;

	public bool glow;
}
