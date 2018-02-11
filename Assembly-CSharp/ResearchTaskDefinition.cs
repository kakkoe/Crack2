using System.Xml.Serialization;

public class ResearchTaskDefinition
{
	[XmlAttribute("id")]
	public string id;

	[XmlAttribute("category")]
	public string category;

	[XmlAttribute("fetish")]
	public string fetish;

	[XmlAttribute("prerequisite")]
	public string prerequisite;

	[XmlAttribute("difficulty")]
	public float difficulty;
}
