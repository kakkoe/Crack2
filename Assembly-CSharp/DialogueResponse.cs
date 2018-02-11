using System.Xml.Serialization;

public class DialogueResponse
{
	[XmlElement("id")]
	public string id;

	[XmlElement("phrase")]
	public string phrase = string.Empty;

	[XmlElement("target")]
	public string target;

	[XmlElement("conditions")]
	public string conditions = string.Empty;

	[XmlElement("functions")]
	public string functions = string.Empty;
}
