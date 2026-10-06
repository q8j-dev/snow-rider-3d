public class PlayScore : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.UI.Text scoreText;

	public void Start()
	{
		GameControl.OnGenChunk.AddListener(AddScore);
		GameControl.OnEnd.AddListener(OnEnd);
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnMain.AddListener(OnMain);
		scoreText = GetComponent<UnityEngine.UI.Text>();
	}

	public void OnEnd()
	{
		scoreText.enabled = false;
	}

	public void OnPlay()
	{
		scoreText.enabled = true;
		UpdateText();
		scoreText.text = string.Empty;
	}

	public void OnMain()
	{
		scoreText.enabled = false;
	}

	public void UpdateText()
	{
		GetComponent<Popup>().Show();
		scoreText.text = GameControl.score.ToString();
	}

	public void AddScore()
	{
		if (PlayerControl.instance.transform.position.z > GGen.instance.genData.warmupLength * GGen.instance.genData.chunkSize.y)
		{
			GameControl.score++;
			GameControl.OnGetScore.Invoke();
			UpdateText();
			SoundControl.instance.PlayIncreaseScore();
		}
	}

}
