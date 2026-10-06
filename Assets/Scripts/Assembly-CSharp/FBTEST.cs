public class FBTEST : UnityEngine.MonoBehaviour
{
	public void Awake()
	{
		return;
	}

	public void InitCallback()
	{
		return;
	}

	public void OnHideUnity(bool isGameShown)
	{
		UnityEngine.Time.timeScale = isGameShown ? 1f : 0f;
	}

	public void Login()
	{
		return;
	}

}
