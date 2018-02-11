using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class EmbellishmentPackageManager
{
	public static EmbellishmentPackage savingPackage = new EmbellishmentPackage();

	public static List<EmbellishmentPackage> packages = new List<EmbellishmentPackage>();

	public static void loadPackages()
	{
		EmbellishmentPackageManager.packages = new List<EmbellishmentPackage>();
		new FileInfo(Application.persistentDataPath + "/embellishmentPackages/").Directory.Create();
		string[] files = Directory.GetFiles(Application.persistentDataPath + "/embellishmentPackages/", "*.embellishmentPackage");
		for (int i = 0; i < files.Length; i++)
		{
			if (File.Exists(files[i]))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(EmbellishmentPackage));
				FileStream fileStream = new FileStream(files[i], FileMode.Open);
				EmbellishmentPackage embellishmentPackage = xmlSerializer.Deserialize(fileStream) as EmbellishmentPackage;
				embellishmentPackage.name = files[i].Split('/')[files[i].Split('/').Length - 1].Split('.')[0];
				fileStream.Close();
				EmbellishmentPackageManager.createPackage(embellishmentPackage, true);
			}
		}
		Game.needEmbellishmentPackageMenuRebuild = true;
	}

	public static void createPackage(EmbellishmentPackage package, bool andSave = true)
	{
		bool flag = false;
		for (int i = 0; i < EmbellishmentPackageManager.packages.Count; i++)
		{
			if (EmbellishmentPackageManager.packages[i].name == package.name)
			{
				EmbellishmentPackageManager.packages[i] = package;
				flag = true;
			}
		}
		if (!flag)
		{
			EmbellishmentPackageManager.packages.Add(package);
		}
		Game.needEmbellishmentPackageMenuRebuild = true;
		if (andSave)
		{
			EmbellishmentPackageManager.savePackage(package);
		}
	}

	public static void savePackage(EmbellishmentPackage package)
	{
		EmbellishmentPackageManager.savingPackage = package;
		new FileInfo(Application.persistentDataPath + "/embellishmentPackages/").Directory.Create();
		try
		{
			Game.saveDataToXML(Application.persistentDataPath + "/embellishmentPackages/" + package.name + ".embellishmentPackage", typeof(EmbellishmentPackage), EmbellishmentPackageManager.savingPackage);
		}
		catch
		{
			Debug.Log("Failed to save embellishment package because the file was in use.");
		}
	}
}
