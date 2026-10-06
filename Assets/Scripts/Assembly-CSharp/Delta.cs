public class Delta : UnityEngine.MonoBehaviour
{
	public UnityEngine.UI.Text textObj;

	public int bufferLength;

	[System.NonSerialized]
	public int i;

	[System.NonSerialized]
	public int num;

	public int[] frames;

	public void Update()
	{
		int fps = (int)(1f / UnityEngine.Time.unscaledDeltaTime);
		num -= frames[i];
		frames[i] = fps;
		num += fps;
		i++;
		if (i >= bufferLength)
		{
			i = 0;
		}
		textObj.text = "FPS : " + GetFps();
	}

	public int GetFps()
	{
		return bufferLength == 0 ? 0 : num / bufferLength;
	}

	public Delta()
	{
		bufferLength = 60;
		frames = new int[60];
	}
}
