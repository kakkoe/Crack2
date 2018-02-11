using System.Xml.Serialization;

public class ColorAltDefinition
{
	public string name;

	public float r;

	public float g;

	public float b;

	[XmlAttribute("oddity")]
	public int oddity;

	[XmlAttribute("alt")]
	public int alt;
}
