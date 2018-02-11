using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("LabItems")]
public class LabItemData
{
	[XmlArrayItem("item")]
	[XmlArray("items")]
	public List<LabItemDefinition> items = new List<LabItemDefinition>();

	[XmlArrayItem("group")]
	[XmlArray("groups")]
	public List<LabItemGroupDefinition> groups = new List<LabItemGroupDefinition>();
}
