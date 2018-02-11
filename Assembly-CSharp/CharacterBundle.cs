using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CharacterBundle
{
	public static List<string> headTypes = new List<string>();

	public string id = string.Empty;

	public bool loaded;

	public bool seamFixesPushedBackToMeshes;

	public string asseturl;

	public AssetBundle bundle;

	public Thread seamFixThread;

	public List<int> bodySeamVerts = new List<int>();

	public Vector3[] bodyVerts;

	public Vector3[] bodyNormals;

	public Dictionary<string, Vector3[]> pieceVerts = new Dictionary<string, Vector3[]>();

	public Dictionary<string, Vector3[]> pieceNormals = new Dictionary<string, Vector3[]>();

	public void fixSeams()
	{
		this.determineSeamVerts();
		int num = 0;
		int count = this.pieceVerts.Keys.Count;
		foreach (string item in this.pieceVerts.Keys.ToList())
		{
			for (int i = 0; i < this.pieceVerts[item].Length; i++)
			{
				for (int j = 0; j < this.bodySeamVerts.Count; j++)
				{
					if ((this.pieceVerts[item][i] - this.bodyVerts[this.bodySeamVerts[j]]).magnitude < 0.01f)
					{
						this.pieceVerts[item][i] = this.bodyVerts[this.bodySeamVerts[j]];
						this.pieceNormals[item][i] = this.bodyNormals[this.bodySeamVerts[j]];
					}
				}
			}
			num++;
		}
		this.loaded = true;
	}

	public void determineSeamVerts()
	{
		this.findSeams("head_feline");
		this.findSeams("tail_0");
		this.findSeams("hands_humanoid");
		this.findSeams("feet_digi_hooved");
		this.findSeams("crotch_neuter");
	}

	public void findSeams(string piece)
	{
		for (int i = 0; i < this.pieceVerts[piece].Length; i++)
		{
			for (int j = 0; j < this.bodyVerts.Length; j++)
			{
				if (this.pieceVerts[piece][i] == this.bodyVerts[j])
				{
					this.bodySeamVerts.Add(j);
				}
			}
		}
	}

	public void pushSeamFixesBackToMeshes()
	{
		if (!this.seamFixesPushedBackToMeshes)
		{
			for (int i = 0; i < RackCharacter.allPieces.transform.childCount; i++)
			{
				if (RackCharacter.allPieces.transform.GetChild(i).name != "Armature" && RackCharacter.allPieces.transform.GetChild(i).name != "body_universal")
				{
					((Component)RackCharacter.allPieces.transform.GetChild(i)).GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices = this.pieceVerts[RackCharacter.allPieces.transform.GetChild(i).name];
					((Component)RackCharacter.allPieces.transform.GetChild(i)).GetComponent<SkinnedMeshRenderer>().sharedMesh.normals = this.pieceNormals[RackCharacter.allPieces.transform.GetChild(i).name];
				}
			}
			this.bodyVerts = null;
			this.bodyNormals = null;
			this.bodySeamVerts = null;
			this.pieceVerts.Clear();
			this.pieceNormals.Clear();
			this.seamFixesPushedBackToMeshes = true;
		}
	}

	public IEnumerator loadBundle()
	{
		if (!((Object)this.bundle != (Object)null))
		{
			float attempts2 = 0f;
			if (!Caching.ready && attempts2 < 10f)
			{
				Thread.Sleep(25);
				yield return (object)null;
				/*Error: Unable to find new state assignment for yield return*/;
			}
			if (!File.Exists(this.asseturl))
			{
				if (this.asseturl.Contains("/home"))
				{
					UserSettings.rebuildGameAssets();
				}
				else
				{
					this.asseturl = "/home+" + this.asseturl;
					if (!File.Exists(this.asseturl))
					{
						UserSettings.rebuildGameAssets();
					}
				}
			}
            using (WWW www = new WWW("file:///" + this.asseturl))
            {
                attempts2 = 0f;
                if (!www.isDone && attempts2 < 10f)
                {
                    Thread.Sleep(25);
                    Game.gameInstance.recentThinking = 1f;
                    yield return (object)null;
                    /*Error: Unable to find new state assignment for yield return*/
                    ;
                }
                yield return (object)www;
                /*Error: Unable to find new state assignment for yield return*/
                ;
                if (!string.IsNullOrEmpty(www.error))
                {
                    Game.gameInstance.popup("IMPORTANT_ASSET_MISSING", true, false);
                    Game.trace("Asset load fail message: " + www.error);
                    UserSettings.data.needDirectoryRebuild = true;
                    UserSettings.saveSettings();
                    throw new System.Exception(string.Concat(new string[]
                    {
                "Error downloading character assets for '",
                this.id,
                "' - '",
                this.asseturl,
                "': ",
                www.error
                    }));
                }
                this.bundle = www.assetBundle;
                CharacterBundle.headTypes = new List<string>();
                GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.bundle.LoadAsset(this.bundle.GetAllAssetNames()[0]));
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    if (gameObject.transform.GetChild(i).name.IndexOf("HEAD_") != -1)
                    {
                        CharacterBundle.headTypes.Add(gameObject.transform.GetChild(i).name.Split(new string[]
                        {
                        "HEAD_"
                        }, System.StringSplitOptions.None)[1]);
                    }
                }
                UnityEngine.Object.Destroy(gameObject);
                RackCharacter.allPieces = (UnityEngine.Object.Instantiate(this.bundle.LoadAsset("basemodel.fbx")) as GameObject);
                RackCharacter.allPieces.name = "AllPieces";
                for (int j = 0; j < RackCharacter.allPieces.transform.childCount; j++)
                {
                    Transform child = RackCharacter.allPieces.transform.GetChild(j);
                    if (child.name != "Armature")
                    {
                        child.name = child.name.ToLower();
                    }
                }
                RackCharacter.allPieces.SetActive(false);
                for (int k = 0; k < RackCharacter.allPieces.transform.childCount; k++)
                {
                    if (RackCharacter.allPieces.transform.GetChild(k).name == "body_universal")
                    {
                        this.bodyVerts = RackCharacter.allPieces.transform.GetChild(k).GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices;
                        this.bodyNormals = RackCharacter.allPieces.transform.GetChild(k).GetComponent<SkinnedMeshRenderer>().sharedMesh.normals;
                    }
                    else if (RackCharacter.allPieces.transform.GetChild(k).name != "Armature")
                    {
                        this.pieceVerts.Add(RackCharacter.allPieces.transform.GetChild(k).name, RackCharacter.allPieces.transform.GetChild(k).GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices);
                        this.pieceNormals.Add(RackCharacter.allPieces.transform.GetChild(k).name, RackCharacter.allPieces.transform.GetChild(k).GetComponent<SkinnedMeshRenderer>().sharedMesh.normals);
                    }
                }
                this.seamFixThread = new Thread(new ThreadStart(this.fixSeams));
                this.seamFixThread.Start();
            }
		}
		yield break;
		IL_0582:
		/*Error near IL_0583: Unexpected return in MoveNext()*/;
		IL_0579:;
	}
}
