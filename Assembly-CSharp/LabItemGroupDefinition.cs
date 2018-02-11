using System.Xml.Serialization;

public class LabItemGroupDefinition
{
	[XmlElement("name")]
	public string name;

	[XmlElement("limitInRoom")]
	public int limitInRoom;

	[XmlElement("limitInPit")]
	public int limitInPit;

	[XmlElement("limitOverall")]
	public int limitOverall;
}
