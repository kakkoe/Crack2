using System.Xml.Serialization;

public class SexualPreferenceValue
{
	[XmlAttribute("preference")]
	public string preference;

	[XmlAttribute("value")]
	public float value;

	[XmlAttribute("knowledge")]
	public int knowledge;

	[XmlAttribute("playerKnowledge")]
	public float playerKnowledge;
}
