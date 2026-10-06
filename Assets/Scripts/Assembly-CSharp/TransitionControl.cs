public class TransitionControl : UnityEngine.MonoBehaviour
{
	public UnityEngine.UI.Image image;

	public float blackScreenDuration;

	public float transitionTime;

	public float currentA;

	public static TransitionControl instance;

	public void Awake()
	{
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnMain.AddListener(OnMain);
		image = GetComponent<UnityEngine.UI.Image>();
		StartCoroutine(TransitionRoutine(1f, 0f, GameControl.startTransitionDur, GameControl.startTransitionDel));
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}
	public System.Collections.IEnumerator IntroTransition()
	{
		GameControl.OnEndIntro.Invoke();
		yield return StartCoroutine(TransitionRoutine(0f, 1f, GameControl.playStartTransitionDur, 0f));
		CampControl.instance.OnMain();
		GameControl.OnMain.Invoke();
		GameControl.OnSleds.Invoke();
	}
	public System.Collections.IEnumerator EndTransition()
	{
		yield return StartCoroutine(TransitionRoutine(0f, 1f, GameControl.playStartTransitionDur, 0f));
		CampControl.instance.OnMain();
		GameControl.OnMain.Invoke();
		GameControl.OnSleds.Invoke();
	}

	public void OnPlay()
	{
		StartCoroutine(TransitionRoutine(1f, 0f, GameControl.playTransitionDur, 0f));
	}

	public void OnMain()
	{
		StartCoroutine(TransitionRoutine(1f, 0f, GameControl.playTransitionDur, 0f));
	}
	public System.Collections.IEnumerator TransitionRoutine(float a, float b, float time, float delay)
	{
		StopCoroutine("TransitionRoutine");
		image.enabled = true;
		if (delay != 0f)
		{
			image.color = new UnityEngine.Color(0f, 0f, 0f, a);
			yield return new UnityEngine.WaitForSecondsRealtime(delay);
		}
		float t = (currentA - a) / (b - a);
		while (t < 1f)
		{
			currentA = UnityEngine.Mathf.Lerp(a, b, t);
			image.color = new UnityEngine.Color(0f, 0f, 0f, currentA);
			yield return new UnityEngine.WaitForEndOfFrame();
			t += (UnityEngine.Time.deltaTime / UnityEngine.Time.timeScale) / time;
		}
		if (b <= 0f)
		{
			image.enabled = false;
		}
		currentA = b;
	}

	public TransitionControl()
	{
		blackScreenDuration = 2f;
		transitionTime = 1f;
		currentA = 1f;
	}
}
