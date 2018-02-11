using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("RacknetCache")]
public class RacknetCache
{
	[XmlArrayItem("racknetCharacter")]
	[XmlArray("racknetCharacterCache")]
	public List<string> racknetCharacterCache = new List<string>();

	[XmlElement("racknetCacheTimestamp")]
	public long racknetCacheTimestamp;
}
