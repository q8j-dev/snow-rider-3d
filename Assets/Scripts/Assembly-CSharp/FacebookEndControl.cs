public class FacebookEndControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject connectBlock;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject friendsBlock;

	public void Start()
	{
		ShowNone();
	}

	public void ShowConnect()
	{
		BigBackground();
		connectBlock.SetActive(true);
		friendsBlock.SetActive(false);
	}

	public void ShowFriends()
	{
		BigBackground();
		connectBlock.SetActive(false);
		friendsBlock.SetActive(true);
	}

	public void ShowNone()
	{
		friendsBlock.SetActive(false);
		connectBlock.SetActive(false);
		SmallBack();
	}

	public void SmallBack()
	{
		UnityEngine.RectTransform rect = GetComponent<MenuControler>().back.GetComponent<UnityEngine.RectTransform>();
		rect.localPosition = new UnityEngine.Vector3(0f, -150f, 0f);
		rect.sizeDelta = new UnityEngine.Vector2(2200f, 265f);
	}

	public void BigBackground()
	{
		UnityEngine.RectTransform rect = GetComponent<MenuControler>().back.GetComponent<UnityEngine.RectTransform>();
		rect.localPosition = new UnityEngine.Vector3(0f, -220f, 0f);
		rect.sizeDelta = new UnityEngine.Vector2(2200f, 400f);
	}

}
