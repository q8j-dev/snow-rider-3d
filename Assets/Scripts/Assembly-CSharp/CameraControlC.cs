public class CameraControlC : UnityEngine.MonoBehaviour
{
	public static CameraControlC instance;

	[System.NonSerialized]
	public PlayerControl sledgeControl;

	[UnityEngine.SerializeField]
	public UnityEngine.Vector3 rotationAmplify;

	[UnityEngine.SerializeField]
	public UnityEngine.Vector3 positionOffset;

	public UnityEngine.Vector3 pivotTargetPos;

	public UnityEngine.Vector3 camTargetPos;

	public UnityEngine.Quaternion pivotTargetRot;

	public UnityEngine.Quaternion camTargetRot;

	public UnityEngine.Vector3 camOffsetPos;

	[System.NonSerialized]
	public UnityEngine.Transform cam;

	[System.NonSerialized]
	public UnityEngine.Transform target;

	[System.NonSerialized]
	public bool isLocked;

	[System.NonSerialized]
	public float currentRotX;

	[System.NonSerialized]
	public float currentRotY;

	[System.NonSerialized]
	public float rotDirection;

	[System.NonSerialized]
	public int currentMenu;

	[System.NonSerialized]
	public int menuCount;

	[System.NonSerialized]
	public float menuRotAngle;

	[System.NonSerialized]
	public float targetMenuAngle;

	public UnityEngine.Vector2 startMousePos;

	[System.NonSerialized]
	public UnityEngine.Vector2 endMousePos;

	[System.NonSerialized]
	public float minSwipeDist;

	public float spd;

	[System.NonSerialized]
	public float startDistance;

	[System.NonSerialized]
	public UnityEngine.Quaternion startRot;

	[System.NonSerialized]
	public UnityEngine.Vector3 startPos;

	[System.NonSerialized]
	public float startTime;

	[System.NonSerialized]
	public float startPressTime;

	public void Awake()
	{
		UnityEngine.AudioListener.volume = GameControl.soundEnabled == 0 ? 1f : 0f;
		GameControl.Init();
		GameControl.OnIntro.AddListener(OnIntro);
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnEnd.AddListener(OnEnd);
		GameControl.OnLand.AddListener(OnLand);
		GameControl.OnMain.AddListener(OnMain);
		GameControl.OnSettings.AddListener(OnSettings);
		GameControl.OnSleds.AddListener(OnSleds);
		GameControl.OnShop.AddListener(OnShop);
		cam = UnityEngine.Camera.main.transform;
		UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cameraData = cam.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
		if (cameraData != null)
		{
			cameraData.renderPostProcessing = true;
			UnityEngine.Rendering.Volume volume = cam.GetComponent<UnityEngine.Rendering.Volume>();
			if (volume == null)
			{
				volume = cam.gameObject.AddComponent<UnityEngine.Rendering.Volume>();
			}
			volume.isGlobal = true;
			volume.priority = 100f;
			UnityEngine.Rendering.VolumeProfile profile = UnityEngine.ScriptableObject.CreateInstance<UnityEngine.Rendering.VolumeProfile>();
			UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments = profile.Add<UnityEngine.Rendering.Universal.ColorAdjustments>();
			colorAdjustments.saturation.Override(12f);
			volume.profile = profile;
		}
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public void Start()
	{
		GameControl.OnIntro.Invoke();
	}

	public void LateUpdate()
	{
		switch (GameControl.gameMode)
		{
		case GameMode.intro:
			StartControl();
			break;
		case GameMode.main:
			MainControl();
			break;
		case GameMode.play:
			PlayControl();
			break;
		case GameMode.end:
			EndControl();
			break;
		}
		transform.position = pivotTargetPos;
		transform.rotation = pivotTargetRot;
		cam.localPosition = camTargetPos + camOffsetPos;
		cam.localRotation = camTargetRot;
	}

	public void OnIntro()
	{
		pivotTargetPos = new UnityEngine.Vector3(2.5f, 10f, 10f);
		pivotTargetRot = UnityEngine.Quaternion.Euler(50f, 75f, 0f);
		camTargetPos = new UnityEngine.Vector3(-10f, 0f, 0f);
		UnityEngine.QualitySettings.shadowDistance = GameControl.shadowDistStart;
	}

	public void OnPlay()
	{
		isLocked = false;
		target = UnityEngine.GameObject.FindGameObjectWithTag("SledCamPoint").transform;
		sledgeControl = UnityEngine.Object.FindAnyObjectByType<PlayerControl>();
		UnityEngine.QualitySettings.shadowDistance = GameControl.shadowDistPlay;
		startPos = transform.position;
		startRot = transform.rotation;
		startTime = UnityEngine.Time.time;
		camTargetRot = UnityEngine.Quaternion.identity;
	}

	public void OnEnd()
	{
		StartCoroutine(EndRoutine());
		UnityEngine.QualitySettings.shadowDistance = GameControl.shadowDistEnd;
	}
	public System.Collections.IEnumerator EndRoutine()
	{
		yield return new UnityEngine.WaitForEndOfFrame();
		target = UnityEngine.GameObject.FindGameObjectWithTag("PhysicsBody").transform;
		ChooseAngle();
		camTargetPos = UnityEngine.Camera.main.transform.forward * -2f;
		yield return new UnityEngine.WaitForEndOfFrame();
		target.LookAt(transform);
		camTargetRot = target.localRotation;
	}

	public void OnLand()
	{
		return;
	}

	public void OnMain()
	{
		UnityEngine.QualitySettings.shadowDistance = GameControl.shadowDistEnd;
		instance.pivotTargetPos = UnityEngine.GameObject.FindGameObjectWithTag("CameraPoint").transform.position;
		instance.pivotTargetRot = UnityEngine.GameObject.FindGameObjectWithTag("CameraPoint").transform.rotation;
		instance.camTargetPos = UnityEngine.Vector3.forward * 2f;
		instance.camTargetRot = UnityEngine.Quaternion.identity;
		instance.startMousePos = UnityEngine.Camera.main.ScreenToViewportPoint(GameInput.PointerPosition);
	}

	public void StartControl()
	{
		pivotTargetPos += UnityEngine.Vector3.forward * spd * UnityEngine.Time.deltaTime;
	}

	public void PlayControl()
	{
		UnityEngine.Vector3 targetRotation = new UnityEngine.Vector3(
			-12f + sledgeControl.vRot * rotationAmplify.x,
			-sledgeControl.hRot * rotationAmplify.y,
			-sledgeControl.hRot * rotationAmplify.z);
		if (isLocked)
		{
			pivotTargetPos = target.position;
			camTargetPos = positionOffset;
			pivotTargetRot = UnityEngine.Quaternion.Lerp(
				pivotTargetRot,
				target.rotation * UnityEngine.Quaternion.Euler(15f, 0f, 0f),
				1f);
			camTargetRot = UnityEngine.Quaternion.Lerp(
				camTargetRot,
				UnityEngine.Quaternion.Euler(targetRotation),
				FrameRateControl.Blend(0.1f));
		}
		else
		{
			camTargetPos = positionOffset;
			float progress = (UnityEngine.Time.time - startTime) * 2f;
			camTargetRot = UnityEngine.Quaternion.Lerp(
				camTargetRot,
				UnityEngine.Quaternion.Euler(targetRotation),
				progress);
			pivotTargetRot = UnityEngine.Quaternion.Lerp(
				startRot,
				target.rotation * UnityEngine.Quaternion.Euler(15f, 0f, 0f),
				progress);
			pivotTargetPos = UnityEngine.Vector3.Lerp(startPos, target.position, progress);
			if (progress > 1f)
			{
				isLocked = true;
			}
		}
	}

	public void EndControl()
	{
		if (target != null && target.CompareTag("PhysicsBody"))
		{
			if (currentRotY > -70f && currentRotY < 70f)
			{
				currentRotY += 30f * rotDirection * UnityEngine.Time.deltaTime;
			}
			else
			{
				ChooseAngle();
			}
			pivotTargetRot = UnityEngine.Quaternion.Euler(currentRotX, currentRotY, 0f);
			pivotTargetPos = target.position;
		}
	}

	public void OnSettings()
	{
		MenuRot(0);
	}

	public void OnSleds()
	{
		MenuRot(1);
	}

	public void OnShop()
	{
		MenuRot(2);
	}

	public void MainControl()
	{
		pivotTargetRot = UnityEngine.Quaternion.Lerp(
			pivotTargetRot,
			UnityEngine.Quaternion.Euler(0f, targetMenuAngle, 0f),
			FrameRateControl.Blend(0.1f));
	}

	public void SwipeControl()
	{
		UnityEngine.Vector2 swipe = endMousePos - startMousePos;
		if (UnityEngine.Mathf.Abs(swipe.x) > minSwipeDist)
		{
			currentMenu = swipe.x < 0f ? 1 : -1;
			targetMenuAngle = (currentMenu - 1) * menuRotAngle - 6f;
		}
	}

	public void MenuRot(int targetMenu)
	{
		currentMenu = targetMenu;
		targetMenuAngle = (currentMenu - 1) * menuRotAngle - 6f;
	}

	public void ChooseAngle()
	{
		currentRotY = UnityEngine.Random.Range(-60, 60);
		currentRotX = UnityEngine.Random.Range(50, 75);
		rotDirection = currentRotY < 0f ? 1f : -1f;
	}

	public CameraControlC()
	{
		currentMenu = 1;
		menuCount = 3;
		menuRotAngle = 72f;
		minSwipeDist = 0.1f;
	}
}
