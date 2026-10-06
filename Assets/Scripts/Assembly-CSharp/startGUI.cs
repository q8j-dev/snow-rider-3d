public class startGUI : UnityEngine.MonoBehaviour
{
	public void Awake()
	{
		GameControl.OnPlay.AddListener(OnPlay);
	}

	public void OnPlay()
	{
		gameObject.SetActive(false);
	}

}
