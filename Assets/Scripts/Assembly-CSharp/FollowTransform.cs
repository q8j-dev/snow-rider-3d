public class FollowTransform : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public bool onlyZ;

	[UnityEngine.SerializeField]
	public UnityEngine.Vector3 pivotPosition;

	public UnityEngine.Transform target;

	[System.NonSerialized]
	public float positionOffset;

	public void Awake()
	{
		GameControl.OnIntro.AddListener(OnIntro);
		GameControl.OnPlay.AddListener(OnPlay);
	}

	public void OnIntro()
	{
		target = UnityEngine.GameObject.FindGameObjectWithTag("Cam").transform;
	}

	public void OnPlay()
	{
		target = UnityEngine.GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void OnMain()
	{
		target = UnityEngine.GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void Update()
	{
		if (onlyZ)
		{
			transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y, target.position.z);
		}
		else
		{
			transform.position = target.position;
		}
	}

	public FollowTransform()
	{
		pivotPosition = new UnityEngine.Vector3(7f, 5f, 15f);
	}
}
