using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LayoutItemSpecialProperties
{
	[XmlElement("uid")]
	public string uid = Guid.NewGuid().ToString();

	[XmlArray("occupyingSlots")]
	[XmlArrayItem("occupyingSlot")]
	public List<string> occupyingSlots = new List<string>();

	[XmlElement("enabled")]
	public bool enabled = true;

	[XmlElement("power")]
	public float power = 0.5f;

	[XmlElement("color")]
	public Color color = Color.white;

	[XmlElement("material")]
	public string material = string.Empty;
}
