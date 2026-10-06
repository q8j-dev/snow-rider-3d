public class TapToSlide : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public Popup popup;

	public void Start()
	{
		popup = GetComponent<Popup>();
	}

	public void OnEnable()
	{
		return;
	}

}
