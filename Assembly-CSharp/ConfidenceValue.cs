using System.Xml.Serialization;

public class ConfidenceValue
{
	[XmlAttribute("attribute")]
	public string attribute;

	[XmlAttribute("value")]
	public float value;

	[XmlAttribute("playerKnowledge")]
	public float playerKnowledge;
}
