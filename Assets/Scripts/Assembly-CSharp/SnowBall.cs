public class SnowBall : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.Rigidbody rb;

	[UnityEngine.SerializeField]
	public float force;

	public float direction;

	public float minSize;

	public float maxSize;

	public void Start()
	{
		transform.localScale = UnityEngine.Vector3.one * UnityEngine.Random.Range(minSize, maxSize);
		transform.localRotation = UnityEngine.Quaternion.Euler(UnityEngine.Vector3.zero);
		rb = GetComponent<UnityEngine.Rigidbody>();
		rb.AddForce(transform.forward * force * rb.mass);
	}

	public void Update()
	{
		if (rb.linearVelocity.magnitude < 5f)
		{
			rb.AddForce(UnityEngine.Vector3.right * force * rb.mass * UnityEngine.Time.deltaTime * direction);
		}
	}

	public void OnCollisionEnter(UnityEngine.Collision other)
	{
		if (other.gameObject.tag == "SnowballDestroy")
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public SnowBall()
	{
		minSize = 0.8f;
		maxSize = 1.1f;
	}
}
