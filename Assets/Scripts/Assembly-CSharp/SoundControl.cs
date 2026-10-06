public class SoundControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip giftClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip increaseScoreClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip beatScoreClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip popupClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip sledLandClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip sledTurnClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioClip sledCrashClip;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioSource skiSource;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioSource rotateSource;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioSource windSource;

	[UnityEngine.SerializeField]
	public UnityEngine.AudioSource musicSource;

	[System.NonSerialized]
	public UnityEngine.AudioSource audioSource;

	public static SoundControl instance;

	[System.NonSerialized]
	public float prevRot;

	public void Awake()
	{
		audioSource = GetComponent<UnityEngine.AudioSource>();
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnEnd.AddListener(OnEnd);
	}

	public void OnPlay()
	{
		skiSource.Play();
		rotateSource.Play();
	}

	public void OnEnd()
	{
		skiSource.Stop();
		rotateSource.Stop();
	}

	public void Update()
	{
		if (GameControl.gameMode == GameMode.play)
		{
			if (musicSource.volume > 0f)
			{
				musicSource.volume -= UnityEngine.Time.deltaTime * 0.5f;
			}
			if (!PlayerControl.instance.isGrounded)
			{
				if (windSource.volume < 0.7f)
				{
					windSource.volume += UnityEngine.Time.deltaTime * 3.3333333f;
				}
				if (skiSource.volume > 0f)
				{
					skiSource.volume = UnityEngine.Mathf.MoveTowards(skiSource.volume, 0f, UnityEngine.Time.unscaledDeltaTime * 6f);
				}
				if (rotateSource.volume > 0f)
				{
					rotateSource.volume = UnityEngine.Mathf.MoveTowards(rotateSource.volume, 0f, UnityEngine.Time.unscaledDeltaTime * 6f);
				}
			}
			else
			{
				if (skiSource.volume < 1f && UnityEngine.Mathf.Abs(PlayerControl.instance.hRot - prevRot) < 0.1f)
				{
					skiSource.volume = UnityEngine.Mathf.MoveTowards(skiSource.volume, 1f, UnityEngine.Time.unscaledDeltaTime * 6f);
				}
				if (windSource.volume > 0.2f)
				{
					windSource.volume -= UnityEngine.Time.deltaTime * 3.3333333f;
				}
				float rotationDelta = UnityEngine.Mathf.Abs(PlayerControl.instance.hRot - prevRot);
				float skiVolume = skiSource.volume;
				if (UnityEngine.Time.deltaTime * 80f < rotationDelta)
				{
					if (skiVolume > 0f)
					{
						skiSource.volume -= UnityEngine.Time.deltaTime * 2f;
					}
					if (rotateSource.volume < 1f)
					{
						rotateSource.volume += UnityEngine.Time.deltaTime * 2f;
					}
				}
				else
				{
					if (skiVolume < 1f)
					{
						skiSource.volume += UnityEngine.Time.deltaTime * 2f;
					}
					if (rotateSource.volume > 0f)
					{
						rotateSource.volume -= UnityEngine.Time.deltaTime * 2f;
					}
				}
				prevRot = PlayerControl.instance.hRot;
			}
		}
		else if (musicSource.volume < 1f)
		{
			musicSource.volume += UnityEngine.Time.deltaTime * 0.5f;
		}
	}

	public void PlayGiftClip()
	{
		audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		audioSource.PlayOneShot(giftClip);
		audioSource.pitch = 1f;
	}

	public void PlayIncreaseScore()
	{
		audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		audioSource.PlayOneShot(increaseScoreClip);
		audioSource.pitch = 1f;
	}

	public void PlayBeatScore()
	{
		audioSource.PlayOneShot(beatScoreClip);
	}

	public void PlayPopup()
	{
		audioSource.pitch = UnityEngine.Random.Range(1.7f, 2.3f);
		audioSource.PlayOneShot(popupClip);
		audioSource.pitch = 1f;
	}

	public void PlaySledCrash()
	{
		audioSource.PlayOneShot(sledCrashClip);
	}

	public void PlaySledLand()
	{
		audioSource.PlayOneShot(sledLandClip);
	}

}
