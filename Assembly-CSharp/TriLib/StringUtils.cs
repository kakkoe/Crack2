using System.Globalization;

namespace TriLib
{
	public class StringUtils
	{
		public static string GenerateUniqueName(object id)
		{
			return id.GetHashCode().ToString(CultureInfo.InvariantCulture);
		}
	}
}
