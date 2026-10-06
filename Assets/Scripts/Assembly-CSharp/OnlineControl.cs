public class OnlineControl : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	public class ScoreInfo
	{
		public int id;

		public int rank;

		public int allRanks;

	}

	[System.Serializable]
	public class FacebookSetInfo
	{
		public int id;

	}
	public static bool gotScore;

	public string url;

	public static OnlineControl instance;

	public static bool isOnline
	{
		get
		{
			if (UnityEngine.Application.internetReachability != UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
			{
				_ = UnityEngine.Application.internetReachability;
			}
			return false;
		}
	}

	public static int facebookId
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("FacebookID");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("FacebookID", value);
		}
	}

	public static int deviceId
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("DeviceID");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("DeviceID", value);
		}
	}

	public static int worldPlace
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("WorldPlace");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("WorldPlace", value);
		}
	}

	public static int worldAllPlaces
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("WorldAllPlaces");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("WorldAllPlaces", value);
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
		GameControl.OnPlay.AddListener(OnPlay);
	}

	public void OnPlay()
	{
		return;
	}

	public void OnEnd()
	{
		SetScore();
		gotScore = false;
	}

	public void SetScore()
	{
		if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork
			|| UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork)
		{
			UnityEngine.PlayerPrefs.GetInt("Best");
		}
	}
	public System.Collections.IEnumerator SetDeviceScoreRoutine(int score)
	{
		yield break;
	}
	public System.Collections.IEnumerator GetDevicePlaceRoutine(int score)
	{
		yield break;
	}
	public System.Collections.IEnumerator SetFacebookScoreRoutine(string facebook_id, int score)
	{
		yield break;
	}

	public OnlineControl()
	{
		url = "";
	}
}
