using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LayoutItem
{
	[XmlElement("uid")]
	public string uid = Guid.NewGuid().ToString();

	[XmlElement("name")]
	public string name;

	[XmlElement("assetName")]
	public string assetName;

	[XmlElement("position")]
	public Vector3 position;

	[XmlElement("rotation")]
	public Vector3 rotation;

	[XmlArrayItem("child")]
	[XmlArray("children")]
	public List<string> children = new List<string>();

	[XmlElement("customProperties")]
	public LayoutItemSpecialProperties customProperties = new LayoutItemSpecialProperties();
}
