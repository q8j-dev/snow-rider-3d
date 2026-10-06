public class PlayerFBScore : UnityEngine.MonoBehaviour
{
	public void OnEnabled()
	{
		OnPlay();
	}

	public void OnPlay()
	{
		print("xD");
		gameObject.SetActive(false);
	}

}
