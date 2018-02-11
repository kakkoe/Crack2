using System.Xml.Serialization;

public class BagData
{
	[XmlElement("slots")]
	public string slots;

	[XmlElement("scale")]
	public int scale;

	[XmlElement("color")]
	public string color;
}
