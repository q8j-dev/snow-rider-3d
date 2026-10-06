public class CanvasControl : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public System.Collections.Generic.List<CanvasElement> elements;

	public float activeElements;
	public System.Collections.IEnumerator Show()
	{
		if (elements.Count < 1)
		{
			GetElements();
		}
		activeElements = 0f;
		foreach (CanvasElement element in elements)
		{
			StartCoroutine(element.Show(this));
		}
		while (activeElements < elements.Count)
		{
			yield return new UnityEngine.WaitForEndOfFrame();
		}
		yield return 0;
	}
	public System.Collections.IEnumerator Hide()
	{
		if (elements.Count < 1)
		{
			GetElements();
		}
		activeElements = elements.Count;
		foreach (CanvasElement element in elements)
		{
			StartCoroutine(element.Hide(this));
		}
		while (activeElements > 0f)
		{
			yield return new UnityEngine.WaitForEndOfFrame();
		}
		yield return 0;
	}

	public void GetElements()
	{
		foreach (UnityEngine.Transform child in transform)
		{
			CanvasElement element = child.GetComponent<CanvasElement>();
			if (element != null)
			{
				elements.Add(child.GetComponent<CanvasElement>());
			}
		}
	}

	public CanvasControl()
	{
		elements = new System.Collections.Generic.List<CanvasElement>();
	}
}
