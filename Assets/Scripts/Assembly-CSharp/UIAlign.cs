public class UIAlign : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public float offset;

	[UnityEngine.SerializeField]
	public bool updateAlign;

	[UnityEngine.SerializeField]
	public UnityEngine.UI.Text alignText;

	[UnityEngine.SerializeField]
	public UnityEngine.Canvas canvas;

	public void OnEnable()
	{
		Align();
	}

	public void OnValidate()
	{
		Align();
	}

	public void Update()
	{
		if (updateAlign)
		{
			Align();
		}
	}

	public void Align()
	{
		UnityEngine.RectTransform rectTransform = GetComponent<UnityEngine.RectTransform>();
		float textRectWidth = alignText.rectTransform.sizeDelta.x;
		float visibleWidth = UnityEngine.Mathf.Min(alignText.preferredWidth, textRectWidth);
		float centeredRemainder = alignText.alignment == UnityEngine.TextAnchor.MiddleCenter ? (textRectWidth - visibleWidth) * 0.5f : 0f;
		UnityEngine.Vector3 displacement = new UnityEngine.Vector3(visibleWidth + centeredRemainder + offset + rectTransform.sizeDelta.x * 0.5f, 0f, 0f);
		transform.localPosition = alignText.transform.localPosition + displacement;
	}

}
