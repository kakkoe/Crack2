using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class UserSettings
{
	public static SettingsData data;

	public static RacknetCache RNcacheData;

	public static string saveDataDirectory;

	public static bool rebuilding;

    public static bool doesCharacterNeedRedraw(RackCharacter character)
    {
        bool flag = UserSettings.data.cachedTextures.Contains(character.data.uid);
        bool flag2 = flag;
        if (flag2)
        {
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-head.png"
            })));
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-body.png"
            })));
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-wing.png"
            })));
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-headFX.png"
            })));
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-bodyFX.png"
            })));
            flag = (flag && File.Exists(string.Concat(new object[]
            {
            Application.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "characters",
            Game.PathDirectorySeparatorChar,
            "texturecache",
            Game.PathDirectorySeparatorChar,
            string.Empty,
            character.data.uid,
            "-wingFX.png"
            })));
            bool flag3 = !flag;
            if (flag3)
            {
                Debug.Log("Uncaching " + character.data.name + " because one or more of their cached textures is missing.");
            }
        }
        return !flag;
    }

    public static float getFetishSetting(string fetish)
	{
		for (int i = 0; i < UserSettings.data.fetishPreference.Count; i++)
		{
			if (UserSettings.data.fetishPreference[i].fetish == fetish)
			{
				return UserSettings.data.fetishPreference[i].pref;
			}
		}
		return 0f;
	}

	public static void dirtyCharacterTexture(RackCharacter character)
	{
		if (UserSettings.data.cachedTextures.Contains(character.data.uid))
		{
			UserSettings.data.cachedTextures.Remove(character.data.uid);
			UserSettings.saveSettings();
		}
	}

	public static void cacheCharacterTexture(RackCharacter character)
	{
		if (!UserSettings.data.cachedTextures.Contains(character.data.uid))
		{
			UserSettings.data.cachedTextures.Add(character.data.uid);
			UserSettings.saveSettings();
		}
	}

	public static bool needTutorial(string flag)
	{
		if (UserSettings.data == null)
		{
			return false;
		}
		return UserSettings.data.tutorialFlags.IndexOf(flag) == -1;
	}

	public static void undoTutorial(string flag)
	{
		if (UserSettings.data.tutorialFlags.IndexOf(flag) != -1)
		{
			UserSettings.data.tutorialFlags.RemoveAt(UserSettings.data.tutorialFlags.IndexOf(flag));
			UserSettings.saveSettings();
		}
	}

	public static void completeTutorial(string flag)
	{
		if (UserSettings.data.tutorialFlags.IndexOf(flag) == -1)
		{
			UserSettings.data.tutorialFlags.Add(flag);
			UserSettings.saveSettings();
			TutorialTooltip.recentTutorialCompletion = 3;
		}
	}

	public static void init()
	{
		Fetishes.init();
		UserSettings.loadSettings();
		UserSettings.cleanTextureCache();
	}

	public static void cleanTextureCache()
	{
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		string[] files = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + string.Empty, "*.rack2character");
		List<string> list = new List<string>();
		for (int i = 0; i < files.Length; i++)
		{
			CharacterData characterData = CharacterManager.importCharacterData(files[i], true);
			list.Add(characterData.uid);
		}
		for (int j = 0; j < UserSettings.data.cachedTextures.Count; j++)
		{
			if (!list.Contains(UserSettings.data.cachedTextures[j]))
			{
				UserSettings.data.cachedTextures.RemoveAt(j);
			}
		}
		new FileInfo(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + "texturecache" + Game.PathDirectorySeparatorChar + string.Empty).Directory.Create();
		string[] files2 = Directory.GetFiles(Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "characters" + Game.PathDirectorySeparatorChar + "texturecache", "*.png");
		for (int k = 0; k < files2.Length; k++)
		{
			bool flag = false;
			for (int l = 0; l < list.Count; l++)
			{
				if (files2[k].IndexOf(list[l]) != -1)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				File.Delete(files2[k]);
			}
		}
		UserSettings.saveSettings();
	}

	public static float getUserVar(string label)
	{
		for (int i = 0; i < UserSettings.data.userVars.Count; i++)
		{
			if (UserSettings.data.userVars[i].label == label)
			{
				return UserSettings.data.userVars[i].val;
			}
		}
		return 0f;
	}

	public static void setUserVar(string label, float val)
	{
		bool flag = false;
		for (int i = 0; i < UserSettings.data.userVars.Count; i++)
		{
			if (UserSettings.data.userVars[i].label == label)
			{
				flag = true;
				UserSettings.data.userVars[i].val = val;
				Debug.Log("Updated user var '" + label + "' to: " + val);
				UserSettings.saveSettings();
			}
		}
		if (!flag)
		{
			UserSettings.data.userVars.Add(new UserVar());
			UserSettings.data.userVars[UserSettings.data.userVars.Count - 1].label = label;
			UserSettings.data.userVars[UserSettings.data.userVars.Count - 1].val = val;
			Debug.Log("Created user var '" + label + "' with value: " + val);
			UserSettings.saveSettings();
		}
	}

	public static void deleteSave(string id)
	{
		File.Delete(UserSettings.saveDataDirectory + id + ".rackCharacterData");
		File.Delete(UserSettings.saveDataDirectory + id + ".rackInventoryData");
		File.Delete(UserSettings.saveDataDirectory + id + ".rackLayoutData");
		UserSettings.data.users.Remove(id);
		UserSettings.data.activeUser = string.Empty;
		UserSettings.saveSettings();
	}

	public static void loadSettings()
	{
		UserSettings.data = new SettingsData();
		UserSettings.saveDataDirectory = Application.persistentDataPath + string.Empty + Game.PathDirectorySeparatorChar + "savedata" + Game.PathDirectorySeparatorChar + string.Empty;
		if (File.Exists(UserSettings.saveDataDirectory + "rackNetCache.rncache"))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RacknetCache));
			FileStream fileStream = new FileStream(UserSettings.saveDataDirectory + "rackNetCache.rncache", FileMode.Open);
			UserSettings.RNcacheData = (xmlSerializer.Deserialize(fileStream) as RacknetCache);
			fileStream.Close();
		}
		else
		{
			UserSettings.RNcacheData = new RacknetCache();
		}
		if (File.Exists(UserSettings.saveDataDirectory + "user.rackSettings"))
		{
			XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(SettingsData));
			FileStream fileStream2 = new FileStream(UserSettings.saveDataDirectory + "user.rackSettings", FileMode.Open);
			UserSettings.data = (xmlSerializer2.Deserialize(fileStream2) as SettingsData);
			fileStream2.Close();
			if (UserSettings.data.gameVersion < Game.gameVersion)
			{
				Game.readyToProceedToGameInit = false;
			}
			else
			{
				UserSettings.saveSettings();
				Game.readyToProceedToGameInit = true;
			}
		}
		else
		{
			UserSettings.data = new SettingsData();
			UserSettings.data.gameVersion = Game.gameVersion;
			UserSettings.saveSettings();
			Game.readyToProceedToGameInit = true;
		}
	}

	public static void rebuildGameAssets()
	{
		UserSettings.rebuilding = true;
		try
		{
			Directory.Delete(Application.persistentDataPath, true);
		}
		catch
		{
			Debug.Log("Unable to delete old game files, possibly because they don't exist.");
		}
		try
		{
			FileInfo fileInfo = new FileInfo(Application.persistentDataPath);
			while (fileInfo.Exists)
			{
				Thread.Sleep(100);
				fileInfo.Refresh();
			}
			try
			{
				UserSettings.copyDirectory(Application.dataPath + string.Empty + Game.PathDirectorySeparatorChar + "Rack 2_ Furry Science", Application.persistentDataPath);
				Debug.Log("Successfully rebuild game directory.");
				Game.gameInstance.closePopup(null);
				UserSettings.loadSettings();
			}
			catch (Exception ex)
			{
				Debug.Log("Failed to copy directory.");
				Debug.Log("Source: " + Application.dataPath + string.Empty + Game.PathDirectorySeparatorChar + "Rack 2_ Furry Science");
				Debug.Log("Destination: " + Application.persistentDataPath);
				Debug.Log(ex.Message);
				Debug.Log(ex.StackTrace);
				Game.gameInstance.popup("ASSET_FOLDER_UPDATE_FAILED", false, false);
			}
		}
		catch
		{
			Debug.Log("Unable to create new game files");
		}
		UserSettings.rebuilding = false;
	}

	public static List<SaveFile> getSaveFiles(bool freeplay)
	{
		List<SaveFile> list = new List<SaveFile>();
		string activeUser = UserSettings.data.activeUser;
		for (int i = 0; i < UserSettings.data.users.Count; i++)
		{
			UserSettings.data.activeUser = UserSettings.data.users[i];
			Inventory.loadInventoryData();
			SaveFile saveFile = new SaveFile();
			saveFile.name = UserSettings.data.activeUser;
			saveFile.freeplay = Inventory.data.freeplay;
			saveFile.fekels = Inventory.data.money;
			saveFile.specimen = Mathf.RoundToInt(Inventory.data.totalSpecimen);
			saveFile.secondsPlayed = Inventory.data.secondsPlayed;
			saveFile.avatarURL = Inventory.data.profilePic;
			saveFile.researchCompletion = Inventory.data.researchCompletion;
			if (saveFile.freeplay == freeplay)
			{
				list.Add(saveFile);
			}
		}
		UserSettings.data.activeUser = activeUser;
		Inventory.data = null;
		return list;
	}

	public static void checkForMissingValues()
	{
		if (UserSettings.data.genderPreferences.Count < 7)
		{
			UserSettings.data.genderPreferences.Add(3f);
			UserSettings.data.genderPreferences.Add(3f);
			UserSettings.data.genderPreferences.Add(1f);
			UserSettings.data.genderPreferences.Add(1f);
			UserSettings.data.genderPreferences.Add(1f);
			UserSettings.data.genderPreferences.Add(1f);
			UserSettings.data.genderPreferences.Add(0f);
		}
		if (UserSettings.data.bodyTypePreferences.Count < 4)
		{
			UserSettings.data.bodyTypePreferences.Add(3f);
			UserSettings.data.bodyTypePreferences.Add(1f);
			UserSettings.data.bodyTypePreferences.Add(0f);
			UserSettings.data.bodyTypePreferences.Add(1f);
		}
		if (UserSettings.data.fetishPreference.Count < Fetishes.fetishes.Count)
		{
			for (int i = 0; i < Fetishes.fetishes.Count; i++)
			{
				if (Fetishes.fetishes[i].IndexOf("CATEGORY") == -1)
				{
					bool flag = false;
					for (int j = 0; j < UserSettings.data.fetishPreference.Count; j++)
					{
						if (UserSettings.data.fetishPreference[j].fetish == Fetishes.fetishes[i])
						{
							flag = true;
						}
					}
					if (!flag)
					{
						FetishPref fetishPref = new FetishPref();
						fetishPref.fetish = Fetishes.fetishes[i];
						fetishPref.pref = Fetishes.fetishDefaultValues[i];
						UserSettings.data.fetishPreference.Add(fetishPref);
					}
				}
			}
		}
		for (int k = 0; k < RackCharacter.allSpecies.Count; k++)
		{
			bool flag2 = false;
			for (int l = 0; l < UserSettings.data.speciesPreferences.Count; l++)
			{
				if (UserSettings.data.speciesPreferences[l].species == RackCharacter.allSpecies[k])
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				SpeciesPref speciesPref = new SpeciesPref();
				speciesPref.species = RackCharacter.allSpecies[k];
				speciesPref.pref = 3f;
				if (speciesPref.species.ToUpper() == "HUMAN")
				{
					speciesPref.pref = 0f;
				}
				UserSettings.data.speciesPreferences.Add(speciesPref);
			}
		}
	}

	public static float getSpeciesPreference(string s)
	{
		for (int i = 0; i < UserSettings.data.speciesPreferences.Count; i++)
		{
			if (UserSettings.data.speciesPreferences[i].species == s)
			{
				return UserSettings.data.speciesPreferences[i].pref;
			}
		}
		return 0f;
	}

	public static void saveSettings()
	{
		UserSettings.checkForMissingValues();
		try
		{
			new FileInfo(UserSettings.saveDataDirectory).Directory.Create();
			Game.saveDataToXML(UserSettings.saveDataDirectory + "user.rackSettings", typeof(SettingsData), UserSettings.data);
			Game.saveDataToXML(UserSettings.saveDataDirectory + "rackNetCache.rncache", typeof(RacknetCache), UserSettings.RNcacheData);
		}
		catch
		{
			Debug.Log("Failed to save setttings.");
		}
	}

	public static void copyDirectory(string strSource, string strDestination)
	{
		if (!Directory.Exists(strDestination))
		{
			Directory.CreateDirectory(strDestination);
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(strSource);
		FileInfo[] files = directoryInfo.GetFiles();
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			if (!File.Exists(Path.Combine(strDestination, fileInfo.Name)))
			{
				fileInfo.CopyTo(Path.Combine(strDestination, fileInfo.Name));
			}
			FileInfo fileInfo2 = new FileInfo(Path.Combine(strDestination, fileInfo.Name));
			while (!fileInfo2.Exists)
			{
				Thread.Sleep(1);
				fileInfo2.Refresh();
			}
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		DirectoryInfo[] array2 = directories;
		foreach (DirectoryInfo directoryInfo2 in array2)
		{
			UserSettings.copyDirectory(Path.Combine(strSource, directoryInfo2.Name), Path.Combine(strDestination, directoryInfo2.Name));
		}
	}
}
