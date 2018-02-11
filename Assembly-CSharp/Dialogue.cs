using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Dialogue
{
	public static void exportDialogue(DialogueTree tree, string filename)
	{
		new FileInfo(Application.persistentDataPath + "/dialogue/").Directory.Create();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(DialogueTree));
		FileStream fileStream = new FileStream(Application.persistentDataPath + "/dialogue/" + filename + ".rack2dialogue", FileMode.Create);
		xmlSerializer.Serialize(fileStream, tree);
		fileStream.Close();
	}

	public static DialogueTree importDialogue(string filename, bool fullFilenameGiven = false)
	{
		DialogueTree result = new DialogueTree();
		string path = Application.persistentDataPath + "/dialogue/" + filename + ".rack2dialogue";
		if (fullFilenameGiven)
		{
			path = filename;
		}
		if (File.Exists(path))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DialogueTree));
			FileStream fileStream = new FileStream(path, FileMode.Open);
			result = (xmlSerializer.Deserialize(fileStream) as DialogueTree);
			fileStream.Close();
		}
		return result;
	}
}
