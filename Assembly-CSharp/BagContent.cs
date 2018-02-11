using System.Xml.Serialization;

[XmlRoot("BagContent")]
public class BagContent
{
	[XmlElement("itemType")]
	public string itemType;

	[XmlElement("x")]
	public int x;

	[XmlElement("y")]
	public int y;

	[XmlElement("newItem")]
	public bool newItem;

	[XmlElement("properties")]
	public LayoutItemSpecialProperties properties = new LayoutItemSpecialProperties();
}
