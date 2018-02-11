using System.Xml.Serialization;

public class LabItemEquipmentSlot
{
	[XmlAttribute("name")]
	public string name;

	[XmlAttribute("occupies")]
	public bool occupies;

	[XmlAttribute("coversUnderboob")]
	public bool coversUnderboob;

	[XmlAttribute("breastSupport")]
	public float breastSupport;
}
