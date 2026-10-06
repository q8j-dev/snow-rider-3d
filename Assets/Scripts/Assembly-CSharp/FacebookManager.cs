public class FacebookManager : UnityEngine.MonoBehaviour
{
	public static UnityEngine.Events.UnityEvent OnConnect;

	public static FacebookManager instance;

	public static bool facebookEnabled
	{
		get
		{
			return false;
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("FacebookEnabled", value ? 1 : -1);
		}
	}

	public static string currentFacebookId
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetString("CurrentFacebookId");
		}
		set
		{
			if (UnityEngine.PlayerPrefs.GetString("CurrentFacebookId") != value)
			{
				UnityEngine.PlayerPrefs.SetString("CurrentFacebookId", value);
				UnityEngine.PlayerPrefs.SetInt("Best", 0);
			}
		}
	}

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
		GameControl.OnEnd.AddListener(OnEnd);
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
		_ = new System.Collections.Generic.List<string>();
	}

	public void OnEnd()
	{
		return;
	}

}
