namespace TriLib
{
	public static class FileUtils
	{
		public static string GetFileDirectory(string filename)
		{
			int length = filename.LastIndexOf('/');
			return filename.Substring(0, length);
		}

		public static string GetFilenameWithoutExtension(string filename)
		{
			int startIndex = filename.LastIndexOf('/');
			int length = filename.LastIndexOf('.');
			return filename.Substring(startIndex, length);
		}
	}
}
