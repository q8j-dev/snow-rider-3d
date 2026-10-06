public class OpenUrls : UnityEngine.MonoBehaviour
{
	public static void OpenLink(string url)
	{
		openWindow(url);
	}

	[System.Runtime.InteropServices.PreserveSig]
	public static extern void openWindow(string url);

}
