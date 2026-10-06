public class SoundButton : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.UI.Image indicatorImage;

	[UnityEngine.SerializeField]
	public UnityEngine.Sprite onSprite;

	[UnityEngine.SerializeField]
	public UnityEngine.Sprite offSprite;

	public void OnStart()
	{
		ChangeSprite();
	}

	public void OnClick()
	{
		GameControl.soundEnabled = GameControl.soundEnabled == 0 ? 1 : 0;
		UnityEngine.AudioListener.volume = GameControl.soundEnabled != 0 ? 0f : 1f;
		ChangeSprite();
	}

	public void ChangeSprite()
	{
		indicatorImage.sprite = GameControl.soundEnabled != 0 ? offSprite : onSprite;
		indicatorImage.rectTransform.sizeDelta = GameControl.soundEnabled != 0
			? new UnityEngine.Vector2(100f, 100f)
			: new UnityEngine.Vector2(150f, 150f);
		GetComponent<Popup>().Show();
	}

}
