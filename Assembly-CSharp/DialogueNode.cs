using System.Collections.Generic;
using System.Xml.Serialization;

public class DialogueNode
{
	[XmlElement("id")]
	public string id;

	[XmlElement("phrase")]
	public string phrase;

	[XmlArray("responses")]
	[XmlArrayItem("response")]
	public List<DialogueResponse> responses = new List<DialogueResponse>();

	public List<DialogueResponse> eligibleResponses = new List<DialogueResponse>();

	[XmlElement("editorX")]
	public float editorX;

	[XmlElement("editorY")]
	public float editorY;
}
