public class GameControl
{
	public static int giftsThisGame;

	public static bool bestNow;

	public static float shadowDistStart = 30f;

	public static float shadowDistPlay = 12f;

	public static float shadowDistEnd = 4f;

	public static float startTransitionDur = 1f;

	public static float playStartTransitionDur = 0.2f;

	public static float playTransitionDur = 1f;

	public static float startTransitionDel = 3f;

	public static int score;

	public static GameMode gameMode;

	public static UnityEngine.Events.UnityEvent OnIntro = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnEndIntro = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnPlay = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnStartPlay = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnEnd = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnGenChunk = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnGetScore = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnLand = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnGetGift = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnMain = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnSleds = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnShop = new UnityEngine.Events.UnityEvent();

	public static UnityEngine.Events.UnityEvent OnSettings = new UnityEngine.Events.UnityEvent();

	public static bool canProceed;

	public static int soundEnabled
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("SoundEnabled");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("SoundEnabled", value);
		}
	}

	public static int completedTutorial
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("completedTutorial");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("completedTutorial", value);
		}
	}

	public static int currentSkin
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("CurrentSkin");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("CurrentSkin", value);
		}
	}

	public static int playsToRate
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("PlaysToRate");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("PlaysToRate", value);
		}
	}

	public static int gifts
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("Gifts");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("Gifts", value);
			OnGetGift.Invoke();
		}
	}

	public static bool tutorialCompleted
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("Tutorial") > 0;
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("Tutorial", value ? 1 : 0);
		}
	}

	public static int plays
	{
		get
		{
			return UnityEngine.PlayerPrefs.GetInt("Plays");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("Plays", value);
		}
	}

	public static ControlMode controlMode
	{
		get
		{
			return (ControlMode)UnityEngine.PlayerPrefs.GetInt("ControlMode");
		}
		set
		{
			UnityEngine.PlayerPrefs.SetInt("ControlMode", (int)value);
		}
	}

	public static void Init()
	{
		OnIntro.AddListener(Intro);
		OnStartPlay.AddListener(StartPlay);
		OnPlay.AddListener(Play);
		OnEnd.AddListener(End);
		OnMain.AddListener(Main);
		OnGenChunk.AddListener(GenChunk);
	}

	public static void Intro()
	{
		UnityEngine.Debug.Log("Start game event called");
		gameMode = GameMode.intro;
	}

	public static void StartPlay()
	{
		UnityEngine.Debug.Log("Start play game  event called");
	}

	public static void Play()
	{
		UnityEngine.Debug.Log("Play game event called");
		gameMode = GameMode.play;
		plays++;
		score = 0;
		giftsThisGame = 0;
		bestNow = false;
	}

	public static void End()
	{
		UnityEngine.Debug.Log("End game event called");
		UpdateScore();
		gameMode = GameMode.end;
	}

	public static void Main()
	{
		UnityEngine.Debug.Log("Main menu event called");
		gameMode = GameMode.main;
	}

	public static void GenChunk()
	{
		return;
	}

	public static void UpdateScore()
	{
		if (UnityEngine.PlayerPrefs.GetInt("Best") < score)
		{
			UnityEngine.PlayerPrefs.SetInt("Best", score);
			bestNow = true;
		}
	}

}
