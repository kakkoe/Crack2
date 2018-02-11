using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("EmbellishmentPackage")]
public class EmbellishmentPackage
{
	[XmlElement("name")]
	public string name;

	[XmlArrayItem("requiredPart")]
	[XmlArray("requiredParts")]
	public List<string> requiredParts = new List<string>();

	[XmlArray("embellishments")]
	[XmlArrayItem("embellishment")]
	public List<EmbellishmentLayer> embellishments = new List<EmbellishmentLayer>();
}
