public class ControlChange : UnityEngine.MonoBehaviour
{
	public void Awake()
	{
		if (!GameControl.tutorialCompleted)
		{
			gameObject.SetActive(false);
		}
	}

	public void OnClick()
	{
		GUIControl.instance.ChangeCanvas(GUIControl.instance.controlSelectCanvasPrefab);
	}

}
