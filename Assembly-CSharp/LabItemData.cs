using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("LabItems")]
public class LabItemData
{
	[XmlArray("items")]
	[XmlArrayItem("item")]
	public List<LabItemDefinition> items = new List<LabItemDefinition>();

	[XmlArray("groups")]
	[XmlArrayItem("group")]
	public List<LabItemGroupDefinition> groups = new List<LabItemGroupDefinition>();
}
