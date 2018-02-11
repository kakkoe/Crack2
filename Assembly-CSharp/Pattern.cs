using System.Xml.Serialization;

[XmlRoot("Pattern")]
public class Pattern
{
	[XmlElement("name")]
	public string name;

	[XmlElement("icon")]
	public string icon;

	[XmlElement("zgroup")]
	public int zgroup;

	[XmlElement("keywords")]
	public string keywords = string.Empty;

	public string[] searchWords;

	[XmlElement("requiredPart")]
	public string requiredPartString = string.Empty;

	public string[] allRequiredParts;
}
