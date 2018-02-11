using PlayerIOClient;

internal class RacknetMP_Lobby
{
	public static long serverTime;

	public static void OnMessage(object sender, Message message)
	{
		string type = message.Type;
		if (type != null && type == "serverTime")
		{
			bool flag = RacknetMP_Lobby.serverTime == 0;
			RacknetMP_Lobby.serverTime = message.GetLong(0u);
			if (flag)
			{
				RacknetMultiplayer.updateCache();
			}
		}
	}
}
