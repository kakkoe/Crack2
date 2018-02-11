using System.Xml.Serialization;

[XmlRoot("versionInfo")]
public class NewsAndVersionData
{
	[XmlElement("latestVersion")]
	public int latestVersion;

	[XmlElement("downloadLink")]
	public string downloadLink;

	[XmlElement("localizationVersion")]
	public int localizationVersion;

	[XmlElement("localizationURL")]
	public string localizationURL;

	[XmlElement("newsTitle")]
	public string newsTitle;

	[XmlElement("newsContent")]
	public string newsContent;
}
