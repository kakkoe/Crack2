using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("DialogueTree")]
public class DialogueTree
{
	[XmlArrayItem("node")]
	[XmlArray("nodes")]
	public List<DialogueNode> nodes = new List<DialogueNode>();
}
