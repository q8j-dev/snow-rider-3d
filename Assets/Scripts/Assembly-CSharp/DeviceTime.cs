public class DeviceTime
{
	public static int Now()
	{
		System.DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		return (int)(System.DateTime.UtcNow - epoch).TotalSeconds;
	}

}
