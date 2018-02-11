using System.Xml.Serialization;

public class ItemOrder
{
	[XmlElement("item")]
	public string item;

	[XmlElement("properties")]
	public LayoutItemSpecialProperties properties;

	[XmlElement("timeRemaining")]
	public float timeRemaining;

	[XmlElement("deliveryLocation")]
	public string deliveryLocation;
}
