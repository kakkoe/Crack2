using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterManager
{
	public class UpdatedEmbellishments
	{
		[XmlArray("embellishmentLayers")]
		[XmlArrayItem("embellishmentLayer")]
		public List<EmbellishmentLayer> layers;
	}

	public static CharactersData data;

	public static List<string> species = new List<string>();

	public static List<string> corruptCharacterFiles = new List<string>();

	public static bool waitingForDefinitionUpdaterUtilityCharacterToInit = false;

	public static RackCharacter definitionUpdateUtilityCharacter;

	public static void init()
	{
		CharacterManager.loadCharacterData();
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		string[] files = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty, "*.rack2species");
		CharacterManager.species = new List<string>();
		for (int i = 0; i < files.Length; i++)
		{
			CharacterManager.species.Add(files[i].Split(Game.PathDirectorySeparatorChar)[files[i].Split(Game.PathDirectorySeparatorChar).Length - 1].Split('.')[0]);
		}
	}

	public static CharacterData getCharacter(string uid)
	{
		CharacterData characterData = new CharacterData();
		for (int i = 0; i < CharacterManager.data.characters.Count; i++)
		{
			if (CharacterManager.data.characters[i].uid == uid)
			{
				return CharacterManager.data.characters[i];
			}
		}
		characterData.uid = uid;
		return characterData;
	}

	public static void updateCharacter(string uid, CharacterData cData)
	{
		for (int i = 0; i < CharacterManager.data.characters.Count; i++)
		{
			if (CharacterManager.data.characters[i].uid == uid)
			{
				CharacterManager.data.characters[i] = cData;
				return;
			}
		}
		CharacterManager.data.characters.Add(cData);
	}

	public static void loadCharacterData()
	{
		if (File.Exists(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackCharacterData"))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharactersData));
			FileStream fileStream = new FileStream(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackCharacterData", FileMode.Open);
			CharacterManager.data = (xmlSerializer.Deserialize(fileStream) as CharactersData);
			fileStream.Close();
			CharacterManager.saveCharacterData();
		}
		else
		{
			CharacterManager.data = new CharactersData();
			CharacterManager.saveCharacterData();
		}
	}

	public static string serializeCharacter(RackCharacter character)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
		StringWriter stringWriter = new StringWriter();
		character.data.colorDefinitions = new List<ColorDefinition>();
		xmlSerializer.Serialize(stringWriter, character.data);
		return stringWriter.ToString();
	}

	public static void exportCharacter(RackCharacter character, string filename)
	{
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		character.data.gameVersion = Game.gameVersion;
		character.data.colorDefinitions = new List<ColorDefinition>();
		Game.saveDataToXML(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty + filename + ".rack2character", typeof(CharacterData), character.data);
	}

	public static void deserializeCharacter(string str, RackCharacter character, string filename = "")
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
		TextReader textReader = new StringReader(str);
		try
		{
			character.data = (xmlSerializer.Deserialize(textReader) as CharacterData);
		}
		catch
		{
			if (filename == string.Empty)
			{
				CharacterManager.corruptCharacterFiles.Add(Localization.getPhrase("AN_UNKNOWN_CHARACTER_FILE", string.Empty));
			}
			else
			{
				CharacterManager.corruptCharacterFiles.Add(filename);
			}
			character.data = new CharacterData();
		}
		character.rebuildCharacter();
		character.buildTexture();
	}

	public static CharacterData deserializeCharacterData(string str, string filename = "")
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
		TextReader textReader = new StringReader(str);
		try
		{
			return xmlSerializer.Deserialize(textReader) as CharacterData;
		}
		catch
		{
			if (filename == string.Empty)
			{
				CharacterManager.corruptCharacterFiles.Add(Localization.getPhrase("AN_UNKNOWN_CHARACTER_FILE", string.Empty));
			}
			else
			{
				CharacterManager.corruptCharacterFiles.Add(filename);
			}
			return new CharacterData();
		}
	}

	public static void importCharacter(RackCharacter character, string filename, bool updateEditor = false, bool fullFilenameGiven = false)
	{
		string text = Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty + filename + ".rack2character";
		if (fullFilenameGiven)
		{
			text = filename;
		}
		if (File.Exists(text))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
			FileStream fileStream = new FileStream(text, FileMode.Open);
			try
			{
				character.data = (xmlSerializer.Deserialize(fileStream) as CharacterData);
			}
			catch
			{
				CharacterManager.corruptCharacterFiles.Add(text);
				character.data = new CharacterData();
			}
			character.data.created = true;
			character.data.customized = false;
			fileStream.Close();
			character.rebuildCharacter();
			character.buildTexture();
			if (updateEditor)
			{
				Game.gameInstance.characterCustomizationChangeMade = true;
				Game.gameInstance.undoingLock = 5;
				Game.gameInstance.needInitialValues = true;
				Game.gameInstance.closeColorPicker();
			}
		}
		else
		{
			Debug.Log("Tried to import a character from a missing file: " + filename);
		}
	}

	public static void updateDefinitionEmbellishments(string species)
	{
		CharacterData characterDefinition = CharacterManager.importCharacterData(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty + species + ".rack2species", true);
		CharacterManager.definitionUpdateUtilityCharacter = new RackCharacter(Game.gameInstance, characterDefinition, false, null, 0f, string.Empty);
		Game.gameInstance.addCharacter(CharacterManager.definitionUpdateUtilityCharacter);
		CharacterManager.waitingForDefinitionUpdaterUtilityCharacterToInit = true;
	}

	public static void processDefinitionUpdater()
	{
		if (CharacterManager.waitingForDefinitionUpdaterUtilityCharacterToInit && CharacterManager.definitionUpdateUtilityCharacter.initted)
		{
			if (CharacterManager.definitionUpdateUtilityCharacter.hadOutdatedEmbellishments)
			{
				UpdatedEmbellishments updatedEmbellishments = new UpdatedEmbellishments();
				updatedEmbellishments.layers = CharacterManager.definitionUpdateUtilityCharacter.data.embellishmentLayers;
				Game.saveDataToXML(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "speciesDefinitions" + Game.PathDirectorySeparatorChar + string.Empty + CharacterManager.species[Game.updatingEmbellishmentSpeciesID] + ".newembellishments", typeof(UpdatedEmbellishments), updatedEmbellishments);
				Game.trace("Updated embellishments for " + CharacterManager.species[Game.updatingEmbellishmentSpeciesID]);
			}
			else
			{
				Game.trace("OK: " + CharacterManager.species[Game.updatingEmbellishmentSpeciesID]);
			}
			Game.gameInstance.removeCharacter(CharacterManager.definitionUpdateUtilityCharacter);
			CharacterManager.waitingForDefinitionUpdaterUtilityCharacterToInit = false;
		}
	}

	public static CharacterData importCharacterData(string filename, bool fullFilenameGiven = false)
	{
		string text = Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty + filename + ".rack2character";
		if (fullFilenameGiven)
		{
			text = filename;
		}
		if (File.Exists(text))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
			FileStream fileStream = new FileStream(text, FileMode.Open);
			CharacterData result;
			try
			{
				result = (xmlSerializer.Deserialize(fileStream) as CharacterData);
			}
			catch
			{
				result = new CharacterData();
				CharacterManager.corruptCharacterFiles.Add(text);
			}
			fileStream.Close();
			return result;
		}
		return null;
	}

	public static byte[] getAvatarFromCharacterFile(string filename)
	{
		if (File.Exists(filename))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterData));
			FileStream fileStream = new FileStream(filename, FileMode.Open);
			CharacterData characterData;
			try
			{
				characterData = (xmlSerializer.Deserialize(fileStream) as CharacterData);
			}
			catch
			{
				characterData = new CharacterData();
				CharacterManager.corruptCharacterFiles.Add(filename);
			}
			fileStream.Close();
			return Convert.FromBase64String(characterData.avatarPixels);
		}
		return null;
	}

	public static void deleteCharacterData(string id)
	{
		try
		{
			File.Delete(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackCharacterData");
		}
		catch
		{
		}
	}

	public static void saveCharacterData()
	{
		try
		{
			for (int i = 0; i < CharacterManager.data.characters.Count; i++)
			{
				CharacterManager.data.characters[i].avatarPixels = string.Empty;
				CharacterManager.data.characters[i].gameVersion = Game.gameVersion;
				CharacterManager.data.characters[i].colorDefinitions = new List<ColorDefinition>();
				if (CharacterManager.data.characters[i].uid != CharacterManager.data.playerCharacter)
				{
					CharacterManager.data.characters.RemoveAt(i);
					i--;
				}
			}
			Game.saveDataToXML(UserSettings.saveDataDirectory + UserSettings.data.activeUser + ".rackCharacterData", typeof(CharactersData), CharacterManager.data);
		}
		catch
		{
			Debug.Log("Failed to save character data because a save is already in progress.");
		}
	}
}
