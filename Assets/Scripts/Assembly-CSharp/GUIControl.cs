public class GUIControl : UnityEngine.MonoBehaviour
{
	public UnityEngine.GameObject introCanvasPrefab;

	public UnityEngine.GameObject playCanvasPrefab;

	public UnityEngine.GameObject endCanvasPrefab;

	public UnityEngine.GameObject sledsCanvasPrefab;

	public UnityEngine.GameObject shopCanvasPrefab;

	public UnityEngine.GameObject controlSelectCanvasPrefab;

	public UnityEngine.GameObject tutorialCanvasPrefab;

	public UnityEngine.GameObject rateCanvasPrefab;

	public UnityEngine.GameObject likeCanvasPrefab;

	public UnityEngine.GameObject pauseCanvasPrefab;

	public UnityEngine.GameObject settingsCanvasPrefab;

	[System.NonSerialized]
	public UnityEngine.GameObject currentCanvas;

	public static GUIControl instance;

	[System.NonSerialized]
	public bool paused;

	public void Awake()
	{
		GameControl.controlMode = ControlMode.sided;
		GameControl.OnIntro.AddListener(OnIntro);
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnEnd.AddListener(OnEnd);
		GameControl.OnMain.AddListener(OnMain);
		GameControl.OnSleds.AddListener(OnSleds);
		GameControl.OnShop.AddListener(OnShop);
		GameControl.OnSettings.AddListener(OnSettings);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
		UnityEngine.PlayerPrefs.SetInt("Rated", 1);
	}

	public void OnIntro()
	{
		ChangeCanvas(introCanvasPrefab);
	}

	public void OnPlay()
	{
		if (GameControl.completedTutorial < 1)
		{
			ChangeCanvas(null);
			GameControl.completedTutorial = 1;
			StartCoroutine(TutorialRoutine());
		}
		else
		{
			ChangeCanvas(playCanvasPrefab);
		}
	}
	public System.Collections.IEnumerator TutorialRoutine()
	{
		yield return new UnityEngine.WaitForSecondsRealtime(0.1f);
		ChangeCanvas(tutorialCanvasPrefab);
	}
	public System.Collections.IEnumerator SlowMotionRoutine()
	{
		yield return new UnityEngine.WaitForSeconds(3f);
		ChangeCanvas(controlSelectCanvasPrefab);
		SlowMotion.instance.Apply(1f, 0.05f);
	}

	public void OnEnd()
	{
		GameControl.canProceed = false;
		ChangeCanvas(endCanvasPrefab);
	}

	public void OnSleds()
	{
		ChangeCanvas(sledsCanvasPrefab);
		if (UnityEngine.PlayerPrefs.GetInt("Best") > 14 && GameControl.bestNow && UnityEngine.PlayerPrefs.GetInt("Rated") == 0 && GameControl.plays > 5 && GameControl.playsToRate < 1)
		{
			ChangeCanvas(rateCanvasPrefab);
			return;
		}
		if (GameControl.plays > 50 && UnityEngine.PlayerPrefs.GetInt("Rated") == 0 && GameControl.playsToRate < 1)
		{
			ChangeCanvas(rateCanvasPrefab);
		}
	}

	public void OnShop()
	{
		ChangeCanvas(shopCanvasPrefab);
	}

	public void OnSettings()
	{
		ChangeCanvas(settingsCanvasPrefab);
	}

	public void OnMain()
	{
		return;
	}

	public void OnTutorial()
	{
		ChangeCanvas(tutorialCanvasPrefab);
	}

	public void LateUpdate()
	{
		if (GameControl.gameMode == GameMode.play)
		{
			PlayControl();
		}
	}

	public void IntroControl()
	{
		return;
	}

	public void MainControl()
	{
		return;
	}

	public void EndControl()
	{
		return;
	}

	public void PlayControl()
	{
		if (GameInput.EscapePressedThisFrame &&
			!paused && UnityEngine.Time.timeScale == 1f)
		{
			ChangeCanvas(pauseCanvasPrefab);
			SlowMotion.instance.Apply(1f, 0.02f);
			paused = true;
		}
		if (GameInput.PrimaryPressedThisFrame &&
			paused && UnityEngine.Time.timeScale < 0.1f)
		{
			ChangeCanvas(playCanvasPrefab);
			SlowMotion.instance.Apply(1f, 1f);
			paused = false;
		}
	}

	public void OnClick()
	{
		if (!GameControl.canProceed)
		{
			return;
		}
		switch (GameControl.gameMode)
		{
		case GameMode.intro:
			OnClickIntro();
			break;
		case GameMode.main:
			OnClickMain();
			break;
		case GameMode.end:
			OnClickEnd();
			break;
		}
	}

	public void OnClickIntro()
	{
		StartCoroutine(TransitionControl.instance.IntroTransition());
	}

	public void OnClickPlay()
	{
		return;
	}

	public void OnClickMain()
	{
		if (currentCanvas.name.Equals(sledsCanvasPrefab.name))
		{
			GameControl.OnPlay.Invoke();
		}
	}

	public void OnClickEnd()
	{
		StartCoroutine(TransitionControl.instance.EndTransition());
	}

	public void ChangeCanvas(UnityEngine.GameObject canvas)
	{
		StopCoroutine("ChangeRoutine");
		StartCoroutine(ChangeRoutine(canvas));
	}
	public System.Collections.IEnumerator ChangeRoutine(UnityEngine.GameObject canvas)
	{
		if (currentCanvas != null)
		{
			MenuControler menu = currentCanvas.GetComponent<MenuControler>();
			if (menu != null)
			{
				yield return StartCoroutine(menu.MenuHideRoutine());
			}
		}
		UnityEngine.Object.Destroy(currentCanvas);
		if (canvas == null)
		{
			UnityEngine.Object.Destroy(currentCanvas);
			currentCanvas = null;
			yield break;
		}
		currentCanvas = UnityEngine.Object.Instantiate(canvas);
		currentCanvas.transform.SetParent(transform);
		currentCanvas.name = canvas.name;
		yield return StartCoroutine(currentCanvas.GetComponent<MenuControler>().MenuShowRoutine());
	}

}
