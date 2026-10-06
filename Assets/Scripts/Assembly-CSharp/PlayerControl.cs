public class PlayerControl : UnityEngine.MonoBehaviour
{
	public SledgeData data;

	public GenData genData;

	public SkinData skinData;

	[UnityEngine.SerializeField]
	public SledgePoint collisionPoint;

	[UnityEngine.SerializeField]
	public UnityEngine.Transform sledgeModel;

	[UnityEngine.SerializeField]
	public System.Collections.Generic.List<CollisionRay> collisionRays;

	[UnityEngine.SerializeField]
	public ScriptableObj physicsSledgePrefab;

	[UnityEngine.SerializeField]
	public UnityEngine.ParticleSystem rearParticles;

	[System.NonSerialized]
	public UnityEngine.GameObject physicsSledge;

	[UnityEngine.HideInInspector]
	public float hRot;

	[UnityEngine.HideInInspector]
	public float vRot;

	[System.NonSerialized]
	public float moveSpeed;

	public float currMoveSpeed;

	public float maxTouchJumpDelta;

	[System.NonSerialized]
	public float touchLeftTime;

	[System.NonSerialized]
	public float touchRightTime;

	[System.NonSerialized]
	public bool touchLeftPressed;

	[System.NonSerialized]
	public bool touchRightPressed;

	[System.NonSerialized]
	public bool touchLeftThisFrame;

	[System.NonSerialized]
	public bool touchRightThisFrame;

	public float jumpStartTime;

	public bool alreadyJumped;

	public bool isGrounded;

	public static PlayerControl instance;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject scoreCanvasPrefab;

	public UnityEngine.GameObject scoreCanvas;

	[UnityEngine.SerializeField]
	public UnityEngine.Material menuMaterial;

	[UnityEngine.SerializeField]
	public UnityEngine.Material playMaterial;

	[System.NonSerialized]
	public float prevMovSpeed;

	[System.NonSerialized]
	public float prevH;

	public void Awake()
	{
		GameControl.OnEnd.AddListener(Die);
		GameControl.OnPlay.AddListener(Spawn);
		GameControl.OnMain.AddListener(OnMain);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(instance);
		}
		UnityEngine.Debug.Log("PlayerPrefs.GetInt(TutorialJump) " + UnityEngine.PlayerPrefs.GetInt("TutorialJump"));
	}

	public void OnMain()
	{
		UnityEngine.Object.Destroy(physicsSledge);
		Spawn();
		UnityEngine.GameObject.FindGameObjectWithTag("SledModel").GetComponent<UnityEngine.Renderer>().material = menuMaterial;
	}

	public void Update()
	{
		if (GameControl.gameMode == GameMode.play)
		{
			if (transform.position.z >= 0.5f)
			{
				ControlInput();
			}
			Rotation();
			Position();
			SledgePoints();
			Collision();
			Jump();
			if (collisionPoint.transform.position.y < -10f || collisionPoint.transform.position.y > 10f)
			{
				GameControl.OnEnd.Invoke();
			}
		}
		ModelTransform();
	}

	public void Jump()
	{
		bool wasGrounded = isGrounded;
		if (!collisionPoint.isGrounded)
		{
			if (wasGrounded)
			{
				jumpStartTime = UnityEngine.Time.time;
			}
			isGrounded = false;
		}
		else
		{
			if (!wasGrounded)
			{
				if (UnityEngine.Time.time - jumpStartTime > 1f)
				{
					GameControl.OnLand.Invoke();
					rearParticles.Play();
					SoundControl.instance.PlaySledLand();
				}
			}
			isGrounded = true;
		}
	}

	public void Spawn()
	{
		sledgeModel.Find("BodyModel").gameObject.SetActive(true);
		currMoveSpeed = 0f;
		vRot = 0f;
		hRot = 0f;
		hRot = transform.rotation.eulerAngles.y;
		if (scoreCanvas != null)
		{
			UnityEngine.Object.Destroy(scoreCanvas);
			scoreCanvas = null;
		}
		scoreCanvas = UnityEngine.Object.Instantiate(scoreCanvasPrefab);
		scoreCanvas.transform.parent = UnityEngine.GameObject.FindGameObjectWithTag("SledCamPoint").transform;
		scoreCanvas.transform.localPosition = UnityEngine.Vector3.zero;
		scoreCanvas.transform.localRotation = UnityEngine.Quaternion.identity;
		UnityEngine.GameObject.FindGameObjectWithTag("SledModel").GetComponent<UnityEngine.Renderer>().material = playMaterial;
	}

	public void ControlInput()
	{
		if (GameControl.controlMode != ControlMode.sided)
		{
			return;
		}
		if (GameInput.LeftSteerHeld)
		{
			if (!touchLeftPressed)
			{
				touchLeftPressed = true;
				touchLeftTime = UnityEngine.Time.time;
			}
			touchLeftThisFrame = true;
		}
		if (GameInput.RightSteerHeld)
		{
			if (!touchRightPressed)
			{
				touchRightPressed = true;
				touchRightTime = UnityEngine.Time.time;
			}
			touchRightThisFrame = true;
		}
		if (!touchLeftThisFrame)
		{
			touchLeftPressed = false;
		}
		if (!touchRightThisFrame)
		{
			touchRightPressed = false;
		}
		touchRightThisFrame = false;
		touchLeftThisFrame = false;
		if (touchLeftPressed && !touchRightPressed)
		{
			hRot -= data.rotationSpeed * UnityEngine.Time.deltaTime * rotLimit(hRot, -1f);
		}
		else if (touchRightPressed && !touchLeftPressed)
		{
			hRot += data.rotationSpeed * UnityEngine.Time.deltaTime * rotLimit(hRot, 1f);
		}
		bool jumpHeld = GameInput.JumpHeld;
		if (!jumpHeld || !isGrounded)
		{
			alreadyJumped = false;
		}
		if (jumpHeld && !alreadyJumped && isGrounded)
		{
			alreadyJumped = true;
			collisionPoint.Jump();
			jumpStartTime = UnityEngine.Time.time;
		}
	}

	public void Rotation()
	{
		transform.rotation = UnityEngine.Quaternion.Lerp(
			transform.rotation,
			UnityEngine.Quaternion.Euler(0f, hRot, 0f),
			FrameRateControl.Blend(1f - data.rotationSmoothness));
	}

	public float Smoothstep(float edge0, float edge1, float x)
	{
		float value = UnityEngine.Mathf.Clamp01((x - edge0) / (edge1 - edge0));
		return value * value * (3f - 2f * value);
	}

	public void Position()
	{
		moveSpeed = data.baseMoveSpeed + (float)System.Math.Pow(
			(float)GameControl.score / data.speedAcceleration * data.speedAccelerationAmplitude,
			0.55);
		currMoveSpeed = UnityEngine.Mathf.Lerp(currMoveSpeed, moveSpeed, FrameRateControl.Blend(0.01f));
		UnityEngine.Vector3 direction = UnityEngine.Quaternion.Euler(0f, hRot, 0f) * UnityEngine.Vector3.forward;
		direction *= currMoveSpeed;
		direction *= UnityEngine.Mathf.Cos(vRot * 0.017453f);
		transform.position += direction * UnityEngine.Time.deltaTime;
	}

	public void ModelTransform()
	{
		float pointHeight = collisionPoint.transform.position.y;
		sledgeModel.localPosition = UnityEngine.Vector3.up * (pointHeight - transform.position.y);
		if (moveSpeed != 0f)
		{
			vRot = -UnityEngine.Mathf.Atan((pointHeight - prevH) / (moveSpeed * collisionPoint.lastVerticalElapsedTime)) * 57.29578f;
		}
		sledgeModel.localRotation = UnityEngine.Quaternion.Lerp(
			sledgeModel.localRotation,
			UnityEngine.Quaternion.Euler(vRot, 0f, 0f),
			FrameRateControl.Blend((1f - data.modelRotationSmoothness) * UnityEngine.Time.timeScale));
		prevMovSpeed = moveSpeed;
		prevH = pointHeight;
	}

	public void Collision()
	{
		if (CheckCollisionRays())
		{
			GameControl.OnEnd.Invoke();
		}
	}

	public void Die()
	{
		physicsSledgePrefab = skinData.skins[GameControl.currentSkin].physicsModel;
		sledgeModel.Find("BodyModel").gameObject.SetActive(false);
		physicsSledge = new UnityEngine.GameObject("Physics Sledge");
		for (int i = 0; i < physicsSledgePrefab.obj.Count; i++)
		{
			ChunkObj chunk = physicsSledgePrefab.obj[i];
			UnityEngine.GameObject piece = UnityEngine.Object.Instantiate(
				chunk.prefab,
				sledgeModel.position + chunk.pos,
				sledgeModel.rotation * chunk.rot);
			piece.transform.parent = physicsSledge.transform;
			UnityEngine.Rigidbody body = piece.GetComponent<UnityEngine.Rigidbody>();
			if (body != null)
			{
				body.AddForce(piece.transform.forward * 100f);
			}
			int particlesLayer = UnityEngine.LayerMask.NameToLayer("Particles");
			piece.layer = particlesLayer >= 0 ? particlesLayer : 12;
		}
		SoundControl.instance.PlaySledCrash();
	}

	public float rotLimit(float currRot, float dir)
	{
		if ((currRot < 0f && dir == -1f) || (currRot > 0f && dir == 1f))
		{
			return 1f - UnityEngine.Mathf.Pow(UnityEngine.Mathf.Abs(hRot) / 120f, 3f);
		}
		return 1f;
	}

	public bool CheckCollisionRays()
	{
		foreach (CollisionRay collisionRay in collisionRays)
		{
			if (collisionRay.Hit())
			{
				return true;
			}
		}
		return false;
	}

	public void SledgePoints()
	{
		collisionPoint.U();
	}

	public PlayerControl()
	{
		maxTouchJumpDelta = 0.1f;
	}
}
