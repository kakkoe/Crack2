using System;
using System.Security.Cryptography;
using System.Text;

public class PlayerIOAuth
{
	public static string Create(string userId, string sharedSecret)
	{
		int num = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		byte[] bytes = new HMACSHA256(Encoding.UTF8.GetBytes(sharedSecret)).ComputeHash(Encoding.UTF8.GetBytes(num + ":" + userId));
		return num + ":" + PlayerIOAuth.toHexString(bytes);
	}

	private static string toHexString(byte[] bytes)
	{
		return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
	}
}
