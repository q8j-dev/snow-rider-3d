public class CanvasElement : UnityEngine.MonoBehaviour
{
	public System.Collections.IEnumerator Show(CanvasControl parent)
	{
		Popup popup = GetComponent<Popup>();
		if (popup)
		{
			StartCoroutine(popup.ShowRoutine());
		}
		else
		{
			transform.localScale = UnityEngine.Vector3.one;
		}
		parent.activeElements += 1f;
		yield return 0;
	}
	public System.Collections.IEnumerator Hide(CanvasControl parent)
	{
		Popup popup = GetComponent<Popup>();
		if (popup)
		{
			StartCoroutine(popup.HideRoutine());
		}
		else
		{
			transform.localScale = UnityEngine.Vector3.zero;
		}
		parent.activeElements -= 1f;
		yield return 0;
	}

}
