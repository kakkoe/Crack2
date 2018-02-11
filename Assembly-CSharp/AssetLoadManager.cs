using System.Collections.Generic;
using UnityEngine;

public class AssetLoadManager
{
	public static List<string> assetURLs = new List<string>();

	public static List<WWW> WWWs = new List<WWW>();

	public static List<int> thingsStillLoading = new List<int>();

	public static bool verbose = false;

	public static void loadAsset(string assetURL)
	{
		if (AssetLoadManager.assetURLs.IndexOf(assetURL) == -1)
		{
			if (AssetLoadManager.verbose)
			{
				Game.trace("Loading an asset for the first time: " + assetURL);
			}
			AssetLoadManager.assetURLs.Add(assetURL);
			AssetLoadManager.WWWs.Add(new WWW("file:///" + assetURL));
			AssetLoadManager.thingsStillLoading.Add(AssetLoadManager.WWWs.Count - 1);
		}
		else if (AssetLoadManager.verbose)
		{
			Game.trace("Using cached asset: " + assetURL);
		}
	}

	public static bool isEverythingLoaded()
	{
		bool flag = true;
		int num = 0;
		for (int num2 = AssetLoadManager.thingsStillLoading.Count - 1; num2 >= 0; num2--)
		{
			if (!AssetLoadManager.WWWs[AssetLoadManager.thingsStillLoading[num2]].isDone)
			{
				num++;
				flag = false;
			}
		}
		if (!flag && AssetLoadManager.verbose)
		{
			Game.trace("Waiting on " + num + " to load...");
		}
		else if (AssetLoadManager.verbose)
		{
			Game.trace("Everything has loaded!");
			AssetLoadManager.thingsStillLoading = new List<int>();
		}
		return flag;
	}

	public static bool assetLoaded(string assetURL)
	{
		if (AssetLoadManager.assetURLs.IndexOf(assetURL) == -1)
		{
			return false;
		}
		if (AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].error != string.Empty)
		{
			return false;
		}
		return true;
	}

	public static WWW getAsset(string assetURL)
	{
		if (AssetLoadManager.assetURLs.IndexOf(assetURL) == -1)
		{
			return null;
		}
		if (AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].error != string.Empty)
		{
			return null;
		}
		return AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)];
	}

	public static void unloadAsset(string assetURL)
	{
		if (!AssetLoadManager.isEverythingLoaded())
		{
			Game.trace("UNABLE TO DISPOSE ASSET WHILE ASSETS ARE STILL LOADING.");
		}
		else if (AssetLoadManager.assetLoaded(assetURL))
		{
			int num = AssetLoadManager.assetURLs.IndexOf(assetURL);
			if (num != -1)
			{
				if ((Object)AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].assetBundle != (Object)null)
				{
					AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].assetBundle.Unload(true);
				}
				Object.Destroy(AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].texture);
				AssetLoadManager.WWWs[AssetLoadManager.assetURLs.IndexOf(assetURL)].Dispose();
				AssetLoadManager.WWWs.RemoveAt(AssetLoadManager.assetURLs.IndexOf(assetURL));
				AssetLoadManager.assetURLs.RemoveAt(AssetLoadManager.assetURLs.IndexOf(assetURL));
				for (int i = 0; i < AssetLoadManager.thingsStillLoading.Count; i++)
				{
					if (AssetLoadManager.thingsStillLoading[i] == num)
					{
						AssetLoadManager.thingsStillLoading.RemoveAt(i);
						i--;
					}
					else if (AssetLoadManager.thingsStillLoading[i] > num)
					{
						List<int> list;
						int index;
						(list = AssetLoadManager.thingsStillLoading)[index = i] = list[index] - 1;
					}
				}
			}
		}
	}
}
