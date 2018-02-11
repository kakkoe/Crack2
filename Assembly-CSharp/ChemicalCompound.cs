using System.Xml.Serialization;

public class ChemicalCompound
{
	[XmlElement("name")]
	public string name;

	[XmlElement("amountOwned")]
	public float amountOwned;
}
