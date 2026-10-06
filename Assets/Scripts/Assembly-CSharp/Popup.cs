public class Popup : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.Vector3 startScale;

	public float startSize;

	public float popTime;

	[UnityEngine.SerializeField]
	public bool playSound;

	public void Awake()
	{
		startScale = transform.localScale;
		transform.localScale = UnityEngine.Vector3.zero;
	}

	public void Show()
	{
		gameObject.SetActive(true);
		StartCoroutine(ShowRoutine());
	}
	public System.Collections.IEnumerator ShowRoutine()
	{
		if (playSound)
		{
			SoundControl.instance.PlayPopup();
		}
		transform.localScale = startScale * startSize;
		float t = 0f;
		while (t < 1f)
		{
			transform.localScale = startScale * UnityEngine.Mathf.Lerp(startSize, 1f, t);
			yield return new UnityEngine.WaitForEndOfFrame();
			t += UnityEngine.Time.unscaledDeltaTime / popTime;
		}
		transform.localScale = startScale;
	}

	public void Hide()
	{
		if (gameObject.activeInHierarchy)
		{
			StartCoroutine(HideRoutine());
		}
	}
	public System.Collections.IEnumerator HideRoutine()
	{
		float t = 0f;
		while (t < 1f)
		{
			transform.localScale = startScale * UnityEngine.Mathf.Lerp(1f, 0f, t);
			yield return new UnityEngine.WaitForEndOfFrame();
			t += UnityEngine.Time.unscaledDeltaTime / popTime;
		}
		transform.localScale = UnityEngine.Vector3.zero;
	}

	public Popup()
	{
		startSize = 1.5f;
		popTime = 0.3f;
		playSound = true;
	}
}
