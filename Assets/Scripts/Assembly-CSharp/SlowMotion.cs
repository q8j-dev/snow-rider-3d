public class SlowMotion : UnityEngine.MonoBehaviour
{
	public static SlowMotion instance;

	public void Awake()
	{
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnEnd.AddListener(OnEnd);
		GameControl.OnMain.AddListener(OnMain);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public void OnPlay()
	{
		Apply(0f, 1f);
	}

	public void OnMain()
	{
		Apply(0f, 1f);
	}

	public void OnEnd()
	{
		Apply(0.3f, 0.05f);
	}

	public void setSpeed(float amount)
	{
		UnityEngine.Time.timeScale = amount;
		UnityEngine.Time.fixedDeltaTime = UnityEngine.Time.timeScale * 0.02f;
	}

	public void Apply(float time, float amount)
	{
		StopCoroutine("ApplyRoutine");
		StartCoroutine(ApplyRoutine(time, amount));
	}
	public System.Collections.IEnumerator ApplyRoutine(float time, float amount)
	{
		float startScale = UnityEngine.Time.timeScale;
		float progress = 0f;
		while (progress < 1f)
		{
			setSpeed(UnityEngine.Mathf.Lerp(startScale, amount, progress));
			yield return new UnityEngine.WaitForFixedUpdate();
			progress += (UnityEngine.Time.deltaTime / UnityEngine.Time.timeScale) / time;
		}
		setSpeed(amount);
	}

}
