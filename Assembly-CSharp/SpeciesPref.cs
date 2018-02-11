using System.Xml.Serialization;

public class SpeciesPref
{
	[XmlElement("species")]
	public string species;

	[XmlElement("pref")]
	public float pref;
}
