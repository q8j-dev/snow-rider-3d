public class TextTransparency : UnityEngine.MonoBehaviour
{
	public float frequency;

	public float amplitude;

	public float averageAlpha;

	[System.NonSerialized]
	public UnityEngine.UI.Text text;

	public void Start()
	{
		text = GetComponent<UnityEngine.UI.Text>();
	}

	public void Update()
	{
		UnityEngine.Color color = text.color;
		color.a = averageAlpha + UnityEngine.Mathf.Sin(UnityEngine.Time.time * frequency) * amplitude * 0.5f;
		text.color = color;
	}

	public TextTransparency()
	{
		frequency = 5f;
		amplitude = 0.3f;
		averageAlpha = 0.7f;
	}
}
