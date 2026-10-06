public class MenuElement : UnityEngine.MonoBehaviour
{
	public float delay;

	public bool isVisible;

	public UnityEngine.Events.UnityEvent showEvent;

	public void Awake()
	{
		if (showEvent == null)
		{
			showEvent = new UnityEngine.Events.UnityEvent();
		}
	}

	public void ShowElement()
	{
		gameObject.SetActive(true);
		isVisible = true;
		Popup popup = GetComponent<Popup>();
		if (popup == null)
		{
			transform.localScale = UnityEngine.Vector3.one;
		}
		else
		{
			popup.Show();
		}
		showEvent.Invoke();
	}

	public void HideElement()
	{
		Popup popup = GetComponent<Popup>();
		if (popup == null || !isVisible)
		{
			transform.localScale = UnityEngine.Vector3.zero;
			gameObject.SetActive(false);
		}
		else
		{
			isVisible = false;
			popup.Hide();
		}
	}

	public MenuElement()
	{
		delay = 0.15f;
	}
}
