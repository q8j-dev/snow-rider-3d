public class BinarySerializationUtil : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	private class ListData<T>
	{
		public System.Collections.Generic.List<T> items;
	}

	public static void Write<T>(string path, ref System.Collections.Generic.List<T> data)
	{
		var wrapper = new ListData<T> { items = data };
		System.IO.File.WriteAllText(path, UnityEngine.JsonUtility.ToJson(wrapper));
	}

	public static void Read<T>(string path, ref System.Collections.Generic.List<T> data)
	{
		data.Clear();
		if (System.IO.File.Exists(path))
		{
			ListData<T> wrapper = null;
			try
			{
				wrapper = UnityEngine.JsonUtility.FromJson<ListData<T>>(System.IO.File.ReadAllText(path));
			}
			catch (System.ArgumentException)
			{
			}
			if (wrapper != null && wrapper.items != null)
			{
				data = wrapper.items;
			}
			else
			{
				UnityEngine.Debug.LogWarning(
					"Unreadable data found at location : " + path + " aborting read process !");
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning(
				"No data found at location : " + path + " aborting read process !");
		}
	}

}
