public class MenuBlock : UnityEngine.MonoBehaviour
{
	public UnityEngine.Events.UnityEvent showEvent;

	public void Start()
	{
		if (showEvent == null)
		{
			showEvent = new UnityEngine.Events.UnityEvent();
		}
		UnityEngine.RectTransform blockRect = GetComponent<UnityEngine.RectTransform>();
		UnityEngine.RectTransform parentRect = transform.parent != null ? transform.parent.GetComponent<UnityEngine.RectTransform>() : null;
		if (blockRect != null && parentRect != null)
		{
			blockRect.sizeDelta = parentRect.sizeDelta;
		}
	}
	public System.Collections.IEnumerator ShowElementsRoutine()
	{
		showEvent.Invoke();
		foreach (MenuElement element in transform.GetComponentsInChildren<MenuElement>())
		{
			if (element.gameObject.activeInHierarchy && !element.isVisible)
			{
				yield return new UnityEngine.WaitForSecondsRealtime(element.delay);
				element.ShowElement();
			}
		}
	}
	public System.Collections.IEnumerator HideElementsRoutine()
	{
		foreach (MenuElement element in transform.GetComponentsInChildren<MenuElement>())
		{
			if (element.gameObject.activeInHierarchy && element.isVisible)
			{
				element.HideElement();
			}
		}
		yield return null;
	}

}
