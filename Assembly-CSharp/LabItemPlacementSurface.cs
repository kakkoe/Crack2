using System.Xml.Serialization;

public class LabItemPlacementSurface
{
	[XmlAttribute("name")]
	public string name;

	[XmlAttribute("valid")]
	public bool valid;
}
