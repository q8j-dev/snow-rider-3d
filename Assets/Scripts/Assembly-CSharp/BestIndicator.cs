public class BestIndicator : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject score;

	[System.NonSerialized]
	public UnityEngine.UI.Image image;

	public void Awake()
	{
		GameControl.OnEnd.AddListener(OnEnd);
		GameControl.OnMain.AddListener(OnMain);
		image = GetComponent<UnityEngine.UI.Image>();
	}

	public void OnEnd()
	{
		image.enabled = false;
	}

	public void OnMain()
	{
		image.enabled = false;
	}

	public void Update()
	{
		if (image.enabled)
		{
			float scoreHalfWidth = score.GetComponent<UnityEngine.UI.Text>().preferredWidth * 0.5f;
			float starHalfWidth = GetComponent<UnityEngine.RectTransform>().rect.width * 0.5f;
			transform.localPosition = score.transform.localPosition + new UnityEngine.Vector3(0f - scoreHalfWidth - starHalfWidth - 6f, 0f, 0f);
			transform.localScale = score.transform.localScale;
		}
		else if (UnityEngine.PlayerPrefs.GetInt("Best") > 0 && UnityEngine.PlayerPrefs.GetInt("Best") < GameControl.score)
		{
			image.enabled = true;
			SoundControl.instance.PlayBeatScore();
		}
	}

}
