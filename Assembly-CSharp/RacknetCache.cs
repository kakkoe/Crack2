using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("RacknetCache")]
public class RacknetCache
{
	[XmlArray("racknetCharacterCache")]
	[XmlArrayItem("racknetCharacter")]
	public List<string> racknetCharacterCache = new List<string>();

	[XmlElement("racknetCacheTimestamp")]
	public long racknetCacheTimestamp;
}
