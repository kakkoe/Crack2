using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("localization")]
public class LocalizationData
{
	[XmlElement("version")]
	public int version;

	[XmlArray("phrases")]
	[XmlArrayItem("phrase")]
	public List<Phrases> phrases = new List<Phrases>();
}
