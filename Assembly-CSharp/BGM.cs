using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BGM
{
	public static string[] allMP3files;

	public static AudioSource player0;

	public static AudioSource player1;

	public static int currentPlayer;

	public static int transitionStatus;

	public static List<string> playlist;

	public static int curSong;

	public static bool nextSongReady;

	public static float crossfade;

	public static float titleScreenDelay = 2f;

	public static void init(Transform musicPlayer)
	{
		BGM.player0 = ((Component)musicPlayer).GetComponent<AudioSource>();
		BGM.player1 = ((Component)musicPlayer.Find("OtherMusicPlayer")).GetComponent<AudioSource>();
		new FileInfo(Application.persistentDataPath + "/bgm/").Directory.Create();
		BGM.allMP3files = Directory.GetFiles(Application.persistentDataPath + "/bgm/", "*.ogg");
		BGM.playlist = new List<string>();
		for (int i = 0; i < BGM.allMP3files.Length; i++)
		{
			BGM.playlist.Add(BGM.allMP3files[i]);
		}
		BGM.shuffle(BGM.playlist);
		if (!UserSettings.data.acceptedTerms)
		{
			int num = 0;
			while (true)
			{
				if (num < BGM.playlist.Count)
				{
					if (BGM.playlist[num].IndexOf("title.mp3") == -1)
					{
						num++;
						continue;
					}
					break;
				}
				return;
			}
			string value = BGM.playlist[0];
			BGM.playlist[0] = BGM.playlist[num];
			BGM.playlist[num] = value;
		}
	}

	public static void shuffle(List<string> list)
	{
		for (int i = 0; i < list.Count * 3; i++)
		{
			string value = list[i % list.Count];
			int num;
			for (num = Mathf.FloorToInt(Random.value * (float)list.Count * 2f); num >= list.Count; num -= list.Count)
			{
			}
			list[i % list.Count] = list[num];
			list[num] = value;
		}
	}

	public static void loadMP3(string filename)
	{
		BGM.nextSongReady = false;
		Game.gameInstance.StartCoroutine(BGM.loadMP3file(filename));
	}

	public static IEnumerator loadMP3file(string filename)
	{
		if (!File.Exists(filename))
		{
			UserSettings.rebuildGameAssets();
		}
        using (WWW www = new WWW("file:///" + filename))
        {
            if (!www.isDone)
            {
                yield return (object)null;
            }
            yield return (object)www;
            bool flag3 = string.IsNullOrEmpty(www.error);
            if (flag3)
            {
                bool flag4 = BGM.currentPlayer == 0;
                if (flag4)
                {
                    BGM.player0.clip = www.GetAudioClip(false);
                    BGM.player0.Play();
                }
                else
                {
                    BGM.player1.clip = www.GetAudioClip(false);
                    BGM.player1.Play();
                }
                BGM.transitionStatus = 1;
                BGM.crossfade = 0f;
                BGM.nextSongReady = true;
            }
        }
		yield break;
	}

	public static void play(string songName, bool fullPathGiven = false)
	{
		BGM.transitionStatus = 0;
		BGM.currentPlayer = 1 - BGM.currentPlayer;
		if (fullPathGiven)
		{
			BGM.loadMP3(songName);
		}
		else
		{
			BGM.loadMP3(Application.persistentDataPath + "/bgm/" + songName + ".ogg");
		}
	}

	public static void playNext()
	{
		BGM.curSong++;
		if (BGM.curSong >= BGM.playlist.Count)
		{
			BGM.curSong = 0;
			BGM.shuffle(BGM.playlist);
		}
		BGM.play(BGM.playlist[BGM.curSong], true);
	}

	public static void process()
	{
		if (BGM.transitionStatus == 2)
		{
			if (BGM.currentPlayer == 0)
			{
				if (BGM.player0.time > BGM.player0.clip.length - 5f && BGM.player0.clip.length > 1f)
				{
					BGM.playNext();
				}
			}
			else if (BGM.player1.time > BGM.player1.clip.length - 5f && BGM.player1.clip.length > 1f)
			{
				BGM.playNext();
			}
		}
		if (BGM.transitionStatus == 1)
		{
			if (BGM.titleScreenDelay > 0f)
			{
				BGM.titleScreenDelay -= Time.deltaTime;
				BGM.crossfade = 0f;
				BGM.player0.volume = 0f;
				BGM.player1.volume = 0f;
			}
			else
			{
				if (BGM.crossfade < 1f)
				{
					BGM.crossfade += Time.deltaTime * 0.1f;
					if (BGM.crossfade >= 1f)
					{
						BGM.crossfade = 1f;
						BGM.transitionStatus = 2;
						if (BGM.currentPlayer == 0)
						{
							BGM.player1.Stop();
						}
						else
						{
							BGM.player0.Stop();
						}
					}
				}
				if (BGM.currentPlayer == 0)
				{
					BGM.player0.volume = BGM.crossfade;
					BGM.player1.volume = 1f - BGM.crossfade;
				}
				else
				{
					BGM.player1.volume = BGM.crossfade;
					BGM.player0.volume = 1f - BGM.crossfade;
				}
			}
		}
	}
}
