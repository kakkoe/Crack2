using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("CharactersData")]
public class CharactersData
{
	[XmlArray("characters")]
	[XmlArrayItem("characters")]
	public List<CharacterData> characters = new List<CharacterData>();

	[XmlElement("playerCharacter")]
	public string playerCharacter;
}
