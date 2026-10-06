public class GiftsGUI : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.UI.Text text;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject image;

	public void Awake()
	{
		GameControl.OnGetGift.AddListener(OnGet);
		text.text = GameControl.gifts.ToString();
	}

	public void OnGet()
	{
		text.text = GameControl.gifts.ToString();
		GetComponent<Popup>().Show();
		SoundControl.instance.PlayGiftClip();
	}

}
