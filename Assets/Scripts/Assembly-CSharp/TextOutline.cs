public class TextOutline : UnityEngine.MonoBehaviour
{
	public float pixelSize;

	public UnityEngine.Color outlineColor;

	public bool resolutionDependant;

	public int doubleResolution;

	[System.NonSerialized]
	public UnityEngine.TextMesh textMesh;

	[System.NonSerialized]
	public UnityEngine.MeshRenderer meshRenderer;

	public void Start()
	{
		textMesh = GetComponent<UnityEngine.TextMesh>();
		meshRenderer = GetComponent<UnityEngine.MeshRenderer>();
		for (int i = 0; i < 8; i++)
		{
			UnityEngine.GameObject outline = new UnityEngine.GameObject("outline", typeof(UnityEngine.TextMesh));
			outline.transform.parent = transform;
			outline.transform.localScale = UnityEngine.Vector3.one;
			UnityEngine.MeshRenderer renderer = outline.GetComponent<UnityEngine.MeshRenderer>();
			renderer.material = new UnityEngine.Material(meshRenderer.material);
			renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			renderer.receiveShadows = false;
			renderer.sortingLayerID = meshRenderer.sortingLayerID;
			renderer.sortingLayerName = meshRenderer.sortingLayerName;
		}
	}

	public void LateUpdate()
	{
		UnityEngine.Camera camera = UnityEngine.Camera.main;
		UnityEngine.Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
		UnityEngine.Color color = outlineColor;
		color.a = textMesh.color.a * textMesh.color.a;
		for (int i = 0; i < transform.childCount; i++)
		{
			UnityEngine.TextMesh outline = transform.GetChild(i).GetComponent<UnityEngine.TextMesh>();
			outline.color = color;
			outline.text = textMesh.text;
			outline.font = textMesh.font;
			outline.fontSize = textMesh.fontSize;
			outline.fontStyle = textMesh.fontStyle;
			outline.offsetZ = textMesh.offsetZ;
			outline.alignment = textMesh.alignment;
			outline.anchor = textMesh.anchor;
			outline.characterSize = textMesh.characterSize;
			outline.lineSpacing = textMesh.lineSpacing;
			outline.tabSize = textMesh.tabSize;
			outline.richText = textMesh.richText;
			float size = resolutionDependant && (UnityEngine.Screen.width > doubleResolution || UnityEngine.Screen.height > doubleResolution) ? pixelSize * 2f : pixelSize;
			outline.transform.position = camera.ScreenToWorldPoint(screenPosition + GetOffset(i) * size);
			UnityEngine.MeshRenderer renderer = transform.GetChild(i).GetComponent<UnityEngine.MeshRenderer>();
			renderer.material = meshRenderer.material;
			renderer.sortingLayerID = meshRenderer.sortingLayerID;
		}
	}

	public UnityEngine.Vector3 GetOffset(int i)
	{
		switch (i % 8)
		{
		case 0:
			return new UnityEngine.Vector3(0f, 1f, 0f);
		case 1:
			return new UnityEngine.Vector3(1f, 1f, 0f);
		case 2:
			return new UnityEngine.Vector3(1f, 0f, 0f);
		case 3:
			return new UnityEngine.Vector3(1f, -1f, 0f);
		case 4:
			return new UnityEngine.Vector3(0f, -1f, 0f);
		case 5:
			return new UnityEngine.Vector3(-1f, -1f, 0f);
		case 6:
			return new UnityEngine.Vector3(-1f, 0f, 0f);
		case 7:
			return new UnityEngine.Vector3(-1f, 1f, 0f);
		default:
			return UnityEngine.Vector3.zero;
		}
	}

	public TextOutline()
	{
		pixelSize = 1f;
		outlineColor = UnityEngine.Color.white;
		doubleResolution = 1024;
	}
}
