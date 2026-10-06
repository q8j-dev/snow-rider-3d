public class OptimiseControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject clouds;

	[System.NonSerialized]
	public int lagFrameCount;

	[System.NonSerialized]
	public int level;

	[System.NonSerialized]
	public float lagDuration;

	public void Awake()
	{
		GameControl.OnPlay.AddListener(OnPlay);
	}

	public void OnPlay()
	{
		enabled = true;
	}

	public void Update()
	{
		int framesPerSecond = (int)(1f / UnityEngine.Time.unscaledDeltaTime);
		if (framesPerSecond < 55)
		{
			lagDuration += UnityEngine.Time.unscaledDeltaTime;
			lagFrameCount = (int)(lagDuration * FrameRateControl.ReferenceFrameRate);
		}
		else
		{
			lagDuration = 0f;
			lagFrameCount = 0;
		}
		if (lagDuration > 11f * FrameRateControl.ReferenceFrameDuration)
		{
			level++;
			lagDuration = 0f;
			lagFrameCount = 0;
		}
	}

	public void DisableVignete()
	{
		return;
	}

	public void LowerResolution()
	{
		return;
	}

	public void DisableShadows()
	{
		return;
	}

	public void DisableClouds()
	{
		clouds.SetActive(false);
	}

}
