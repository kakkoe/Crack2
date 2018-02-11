using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class PatternIcons
{
	public static patternDefinitionData data;

	public static IEnumerator init()
	{
		if (!File.Exists(Application.persistentDataPath + "/characterTextures/_definitions.xml"))
		{
			UserSettings.rebuildGameAssets();
		}
		WWW www = new WWW("file:///" + Application.persistentDataPath + "/characterTextures/_definitions.xml");
		yield return (object)www;
        bool flag2 = !string.IsNullOrEmpty(www.error);
        if (flag2)
        {
            Debug.Log("Error loading pattern definitions!");
            Debug.Log(www.error);
            PatternIcons.data = new patternDefinitionData();
        }
        else
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(patternDefinitionData));
            PatternIcons.data = (xmlSerializer.Deserialize(new StringReader(www.text)) as patternDefinitionData);
            int num;
            for (int i = 0; i < PatternIcons.data.patterns.Count; i = num + 1)
            {
                PatternIcons.data.patterns[i].searchWords = PatternIcons.data.patterns[i].keywords.Split(new char[]
                {
                ','
                });
                PatternIcons.data.patterns[i].allRequiredParts = PatternIcons.data.patterns[i].requiredPartString.Split(new char[]
                {
                ','
                });
                num = i;
            }
            xmlSerializer = null;
        }
        yield break;
    }

	public static bool isCustom(string patternName)
	{
		for (int i = 0; i < PatternIcons.data.patterns.Count; i++)
		{
			if (PatternIcons.data.patterns[i].name == patternName)
			{
				return false;
			}
		}
		return true;
	}

	public static string getIcon(string patternName)
	{
		for (int i = 0; i < PatternIcons.data.patterns.Count; i++)
		{
			if (PatternIcons.data.patterns[i].name == patternName)
			{
				return PatternIcons.data.patterns[i].icon;
			}
		}
		return "custom";
	}

	public static string[] getKeywords(string patternName)
	{
		for (int i = 0; i < PatternIcons.data.patterns.Count; i++)
		{
			if (PatternIcons.data.patterns[i].name == patternName)
			{
				return PatternIcons.data.patterns[i].searchWords;
			}
		}
		return new string[0];
	}

	public static string[] getRequiredParts(string patternName)
	{
		for (int i = 0; i < PatternIcons.data.patterns.Count; i++)
		{
			if (PatternIcons.data.patterns[i].name == patternName)
			{
				return PatternIcons.data.patterns[i].allRequiredParts;
			}
		}
		return new string[0];
	}

	public static int getZGroup(string patternName)
	{
		for (int i = 0; i < PatternIcons.data.patterns.Count; i++)
		{
			if (PatternIcons.data.patterns[i].name == patternName)
			{
				return PatternIcons.data.patterns[i].zgroup;
			}
		}
		return 0;
	}
}
