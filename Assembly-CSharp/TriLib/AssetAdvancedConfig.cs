using System;

namespace TriLib
{
	[Serializable]
	public class AssetAdvancedConfig
	{
		public string Key;

		public int IntValue;

		public float FloatValue;

		public bool BoolValue;

		public string StringValue;

		public AssetAdvancedConfig()
		{
		}

		public AssetAdvancedConfig(string key)
		{
			this.Key = key;
		}

		public AssetAdvancedConfig(string key, int defaultValue)
		{
			this.Key = key;
			this.IntValue = defaultValue;
		}

		public AssetAdvancedConfig(string key, float defaultValue)
		{
			this.Key = key;
			this.FloatValue = defaultValue;
		}

		public AssetAdvancedConfig(string key, bool defaultValue)
		{
			this.Key = key;
			this.BoolValue = defaultValue;
		}

		public AssetAdvancedConfig(string key, string defaultValue)
		{
			this.Key = key;
			this.StringValue = defaultValue;
		}

		public AssetAdvancedConfig(string key, AiComponent defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}

		public AssetAdvancedConfig(string key, AiPrimitiveType defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}

		public AssetAdvancedConfig(string key, AiUVTransform defaultValue)
		{
			this.Key = key;
			this.IntValue = (int)defaultValue;
		}
	}
}
