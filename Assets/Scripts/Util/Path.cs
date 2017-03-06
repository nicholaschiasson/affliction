 namespace Util
{
	public static class Path
	{
		public static string Combine(params string[] paths)
		{
			string path = null;
			foreach (string p in paths)
			{
				if (path == null)
				{
					path = p;
				}
				else
				{
					path = System.IO.Path.Combine(path, p);
				}
			}
			return path;
		}
	}
}
