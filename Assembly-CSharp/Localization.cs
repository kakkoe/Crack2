using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

public class Localization
{
	public static bool initted = false;

	public static LocalizationData data;

	public static Dictionary<string, string> translatedPhrases = new Dictionary<string, string>();

	public static string missingPhrase = string.Empty;

	public static bool loadingNewLocalization = false;

	public static List<string> languages;

	public static void init()
	{
		Game.gameInstance.StartCoroutine(Localization.loadLocalizationData());
	}

	public static void setLanguage(string _language)
	{
		UserSettings.data.language = _language;
		Localization.missingPhrase = string.Empty;
		UserSettings.saveSettings();
	}

	public static IEnumerator loadNewLocalizationData(string url)
	{
		WWW www = new WWW(url + "?refresh=" + Guid.NewGuid());
		yield return (object)www;
        bool flag = !string.IsNullOrEmpty(www.error);
        if (flag)
        {
            Debug.Log("Failed to load new localization");
        }
        else
        {
            Debug.Log("Saving new translations");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocalizationData));
            Localization.data = (xmlSerializer.Deserialize(new StringReader(www.text)) as LocalizationData);
            new FileInfo(string.Concat(new object[]
            {
            Game.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "translation",
            Game.PathDirectorySeparatorChar,
            string.Empty
            })).Directory.Create();
            StreamWriter streamWriter = new StreamWriter(string.Concat(new object[]
            {
            Game.persistentDataPath,
            string.Empty,
            Game.PathDirectorySeparatorChar,
            "translation",
            Game.PathDirectorySeparatorChar,
            "Localization.xml"
            }), false, new System.Text.UTF8Encoding(false));
            streamWriter.Write(www.text);
            streamWriter.Close();
            Localization.convertDataToDictionary();
            xmlSerializer = null;
            streamWriter = null;
        }
        yield break;
    }

	public static string getSubPhrase(string phraseName, string param1 = "", string param2 = "", string param3 = "")
	{
		return Localization.getPhrase(phraseName, string.Empty).Replace("[1]", param1).Replace("[2]", param2)
			.Replace("[3]", param3);
	}

	public static string getPhrase(string phraseName, string language = "")
	{
		if (phraseName == string.Empty)
		{
			return string.Empty;
		}
		if (Localization.data == null)
		{
			return phraseName;
		}
		if (Game.newsData != null && Localization.data.version < Game.newsData.localizationVersion && !Localization.loadingNewLocalization && Game.newsData != null)
		{
			Game.gameInstance.StartCoroutine(Localization.loadNewLocalizationData(Game.newsData.localizationURL));
			Localization.loadingNewLocalization = true;
		}
		if (UserSettings.data == null)
		{
			return phraseName;
		}
		if (language == string.Empty)
		{
			language = UserSettings.data.language;
		}
		if (Input.GetKey(UserSettings.data.KEY_TRANSLATE))
		{
			return phraseName;
		}
		string text = string.Empty;
		if (Localization.translatedPhrases.ContainsKey(language + ".." + phraseName))
		{
			text = Localization.translatedPhrases[language + ".." + phraseName];
		}
		if (text == null)
		{
			text = string.Empty;
		}
		if (text.Length == 0)
		{
			Localization.missingPhrase = phraseName;
			if (language != "english")
			{
				return Localization.getPhrase(phraseName, "english");
			}
			if (!Localization.loadingNewLocalization && Game.newsData != null)
			{
				Game.gameInstance.StartCoroutine(Localization.loadNewLocalizationData(Game.newsData.localizationURL));
				Localization.loadingNewLocalization = true;
			}
			return phraseName;
		}
		if (Game.daddyCummyCheat)
		{
			text = text.Replace("Specimen", "Cummies").Replace("specimen", "cummies").Replace("SPECIMEN", "CUMMIES");
		}
		return text;
	}

	public static IEnumerator loadLocalizationData()
	{
		if (!File.Exists(Game.persistentDataPath + "/translation/Localization.xml"))
		{
			UserSettings.rebuildGameAssets();
		}
		WWW www = new WWW("file:///" + Game.persistentDataPath + "/translation/Localization.xml?refresh=" + Guid.NewGuid());
		yield return (object)www;
        bool flag2 = !string.IsNullOrEmpty(www.error);
        if (flag2)
        {
            Debug.Log("failed to load translations: " + www.error);
            Localization.data = new LocalizationData();
        }
        else
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocalizationData));
            Localization.data = (xmlSerializer.Deserialize(new StringReader(www.text)) as LocalizationData);
            xmlSerializer = null;
        }
        Localization.convertDataToDictionary();
        Localization.initted = true;
        yield break;
    }

	public static void convertDataToDictionary()
	{
		Localization.languages = new List<string>();
		FieldInfo[] fields = typeof(Phrases).GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			if (fields[i].Name != "id")
			{
				Localization.languages.Add(fields[i].Name);
			}
		}
		for (int j = 0; j < Localization.data.phrases.Count; j++)
		{
			for (int k = 0; k < Localization.languages.Count; k++)
			{
				object value = Localization.data.phrases[j].GetType().GetField(Localization.languages[k]).GetValue(Localization.data.phrases[j]);
				if (!Localization.translatedPhrases.ContainsKey(Localization.languages[k] + ".." + Localization.data.phrases[j].id))
				{
					if (value == null)
					{
						Localization.translatedPhrases.Add(Localization.languages[k] + ".." + Localization.data.phrases[j].id, string.Empty);
					}
					else
					{
						Localization.translatedPhrases.Add(Localization.languages[k] + ".." + Localization.data.phrases[j].id, value.ToString());
					}
				}
			}
		}
		Localization.data.phrases = new List<Phrases>();
	}

	public static void save()
	{
		new FileInfo(Game.persistentDataPath + "/translate/").Directory.Create();
		Game.saveDataToXML(Game.persistentDataPath + "/translation/Localization.xml", typeof(LocalizationData), Localization.data);
	}
}
