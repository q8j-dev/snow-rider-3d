public class BackgroundRoll : UnityEngine.MonoBehaviour
{
	public float delay;

	[UnityEngine.SerializeField]
	public float backAnimationTime;

	[System.NonSerialized]
	public bool isVisible;

	public void Awake()
	{
		transform.localScale = UnityEngine.Vector3.zero;
	}
	public System.Collections.IEnumerator BackgroundShowRoutine()
	{
		if (isVisible)
		{
			yield break;
		}
		isVisible = true;
		float t = 0f;
		while (t < 1f)
		{
			transform.localScale = new UnityEngine.Vector3(t, 1f, 1f);
			yield return new UnityEngine.WaitForEndOfFrame();
			t += UnityEngine.Time.unscaledDeltaTime / backAnimationTime;
		}
		transform.localScale = UnityEngine.Vector3.one;
	}
	public System.Collections.IEnumerator BackgroundHideRoutine()
	{
		if (!isVisible)
		{
			yield break;
		}
		isVisible = false;
		float t = 1f;
		while (t > 0f)
		{
			transform.localScale = new UnityEngine.Vector3(t, 1f, 1f);
			yield return new UnityEngine.WaitForEndOfFrame();
			t -= UnityEngine.Time.unscaledDeltaTime / backAnimationTime;
		}
		transform.localScale = UnityEngine.Vector3.zero;
	}

	public BackgroundRoll()
	{
		backAnimationTime = 0.3f;
	}
}
