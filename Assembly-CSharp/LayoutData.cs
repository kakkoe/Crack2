using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("LayoutData")]
public class LayoutData
{
	[XmlArray("layouts")]
	[XmlArrayItem("layout")]
	public List<RoomLayout> layouts = new List<RoomLayout>();

	[XmlArray("activeLayouts")]
	[XmlArrayItem("activeLayout")]
	public string[] activeLayouts = new string[3];
}
