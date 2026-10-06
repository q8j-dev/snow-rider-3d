public class CanvasManager : UnityEngine.MonoBehaviour
{
	public static CanvasManager instance;

	public static UnityEngine.Events.UnityEvent OnHide = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnShow = new UnityEngine.Events.UnityEvent();

	[System.NonSerialized]
	public CanvasControl currCanvas;

	public void Start()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			instance = this;
		}
	}
	public System.Collections.IEnumerator ChangeCanvas(CanvasControl canvas)
	{
		if (currCanvas != null)
		{
			OnHide.Invoke();
			yield return StartCoroutine(currCanvas.Hide());
		}
		currCanvas = canvas;
		if (currCanvas != null)
		{
			OnShow.Invoke();
			yield return StartCoroutine(currCanvas.Show());
		}
		yield return 0;
	}

}
