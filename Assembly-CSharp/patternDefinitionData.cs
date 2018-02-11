using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("patternDefinitionData")]
public class patternDefinitionData
{
	[XmlArray("patterns")]
	[XmlArrayItem("pattern")]
	public List<Pattern> patterns = new List<Pattern>();
}
