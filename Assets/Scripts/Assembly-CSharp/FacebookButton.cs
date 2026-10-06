public class FacebookButton : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.Sprite enabledSprite;

	[UnityEngine.SerializeField]
	public UnityEngine.Sprite disabledSprite;

	public void OnStart()
	{
		GetComponent<UnityEngine.UI.Image>().sprite = disabledSprite;
	}

	public void OnClick()
	{
		FacebookManager.facebookEnabled = true;
		GetComponent<UnityEngine.UI.Image>().sprite = disabledSprite;
		GetComponent<Popup>().Show();
	}

}
