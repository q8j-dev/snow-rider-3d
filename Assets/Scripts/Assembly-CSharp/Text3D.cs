public class Text3D : UnityEngine.MonoBehaviour
{
	public Text3DData data;

	public string text;

	public float scale;

	public float gapDistance;

	[System.NonSerialized]
	public float offset;

	[System.NonSerialized]
	public System.Collections.Generic.List<UnityEngine.GameObject> letters;

	public void Awake()
	{
		letters = new System.Collections.Generic.List<UnityEngine.GameObject>();
		Show();
	}

	public void Show(string str)
	{
		if (letters.Count > 0)
		{
			foreach (UnityEngine.GameObject letterObject in letters)
			{
				UnityEngine.Object.Destroy(letterObject);
			}
			letters.Clear();
		}
		if (str != null)
		{
			text = str;
		}
		for (int characterIndex = 0; characterIndex < text.Length; characterIndex++)
		{
			for (int letterIndex = 0; letterIndex < data.letter.Count; letterIndex++)
			{
				Letter letter = data.letter[letterIndex];
				if (letter.ascii != text[characterIndex])
				{
					continue;
				}
				UnityEngine.GameObject letterObject = UnityEngine.Object.Instantiate(
					letter.obj,
					transform.position + UnityEngine.Vector3.right * offset,
					transform.rotation);
				letterObject.transform.parent = transform;
				letterObject.transform.localScale = UnityEngine.Vector3.one * scale;
				if (characterIndex < text.Length - 1)
				{
					offset += letterObject.GetComponent<UnityEngine.Renderer>().bounds.size.x * scale + gapDistance;
				}
				letters.Add(letterObject);
				break;
			}
		}
		offset = -offset * 0.5f;
		foreach (UnityEngine.GameObject letterObject in letters)
		{
			letterObject.transform.position += UnityEngine.Vector3.right * offset;
		}
	}

	public void Show()
	{
		Show(null);
	}

}
