public class IntroCanvasDepth : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.Canvas canvas;

	public void Start()
	{
		canvas = GetComponent<UnityEngine.Canvas>();
		canvas.sortingOrder = 2;
	}

	public void Update()
	{
		if (GameControl.canProceed)
		{
			canvas.sortingOrder = -1;
		}
	}

}
