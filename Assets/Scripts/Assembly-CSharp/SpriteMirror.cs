public class SpriteMirror : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public float interval;

	[System.NonSerialized]
	public UnityEngine.RectTransform t;

	public void OnStart()
	{
		t = GetComponent<UnityEngine.RectTransform>();
		StartCoroutine(MirrorRoutine());
	}

	public void Mirror()
	{
		UnityEngine.Vector3 scale = t.localScale;
		t.localScale = new UnityEngine.Vector3(-scale.x, scale.y, scale.z);
	}
	public System.Collections.IEnumerator MirrorRoutine()
	{
		yield return new UnityEngine.WaitForEndOfFrame();
		while (true)
		{
			yield return new UnityEngine.WaitForSecondsRealtime(interval);
			Mirror();
		}
	}

}
