namespace project_api._Util
{
	public class HelperClass
	{
		internal static object? GetPropertyValue(object obj, string propertyName)
		{
			try
			{
				if (propertyName.Contains('.'))
				{
					var temp = propertyName.Split(new char[] { '.' }, 2);
					return GetPropertyValue(GetPropertyValue(obj, temp[0])!, temp[1]);
				}
				else
				{
					var prop = obj.GetType().GetProperty(propertyName);
					return prop?.GetValue(obj, null);
				}
			}
			catch (NullReferenceException)
			{
				return null;
			}
		}
	}
}
