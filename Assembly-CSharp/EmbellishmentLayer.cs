using System.Xml.Serialization;
using UnityEngine;

public class EmbellishmentLayer
{
	[XmlElement("embellishment")]
	public string embellishment = string.Empty;

	[XmlElement("color")]
	public int color = -1;

	[XmlElement("mirror")]
	public bool mirror;

	[XmlElement("partName")]
	public string partName;

	[XmlElement("meshModifiedTime")]
	public int meshModifiedTime;

	[XmlElement("vertexID")]
	public int vertexID;

	[XmlElement("embellishmentPositionForVertexLookup")]
	public Vector3 embellishmentPositionForVertexLookup;

	[XmlElement("size")]
	public float size = 1f;

	[XmlElement("bend")]
	public float bend = 0.5f;

	[XmlElement("turn")]
	public float turn = 0.5f;

	[XmlElement("twist")]
	public float twist = 0.5f;

	[XmlElement("hidden")]
	public bool hidden;

	[XmlElement("utilityLayer")]
	public bool utilityLayer;

	[XmlElement("mirrorVertID")]
	public int mirrorVertID = -1;

	[XmlElement("temporaryLayer")]
	public bool temporaryLayer;
}
