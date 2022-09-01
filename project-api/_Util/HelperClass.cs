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
					var propertySegment = propertyName.Split(new char[] { '.' }, 2);
					return GetPropertyValue(GetPropertyValue(obj, propertySegment[0])!, propertySegment[1]);
				}
				else
				{
					var prop = obj.GetType().GetProperty(propertyName);
					return prop?.GetValue(obj);
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
