using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("DialogueTree")]
public class DialogueTree
{
	[XmlArray("nodes")]
	[XmlArrayItem("node")]
	public List<DialogueNode> nodes = new List<DialogueNode>();
}
