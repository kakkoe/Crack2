using System.Collections.Generic;
using System.Xml.Serialization;

public class RoomLayout
{
	[XmlElement("name")]
	public string name;

	[XmlElement("isPit")]
	public bool isPit;

	[XmlArray("items")]
	[XmlArrayItem("item")]
	public List<LayoutItem> items = new List<LayoutItem>();
}
