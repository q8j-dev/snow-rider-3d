public class SledgePoint : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.Vector3 startPosition;

	[System.NonSerialized]
	public UnityEngine.Ray upRay;

	[System.NonSerialized]
	public UnityEngine.Ray downRay;

	[System.NonSerialized]
	public UnityEngine.RaycastHit hit;

	public float vSpeed;

	public float gravityAcc;

	public bool isGrounded;

	public SledgeData data;

	[System.NonSerialized]
	public float rayLength;

	[System.NonSerialized]
	public UnityEngine.LayerMask mask;

	[System.NonSerialized]
	public float lastVerticalElapsedTime;

	public void Awake()
	{
		int surfaceLayer = UnityEngine.LayerMask.NameToLayer("Surface");
		mask = 1 << (surfaceLayer >= 0 ? surfaceLayer : 8);
		startPosition = transform.localPosition;
		GameControl.OnMain.AddListener(Spawn);
		GameControl.OnPlay.AddListener(Spawn);
	}

	public void Spawn()
	{
		vSpeed = 0f;
		transform.localPosition = startPosition;
	}

	public void U()
	{
		float elapsedTime = UnityEngine.Time.deltaTime;
		lastVerticalElapsedTime = elapsedTime;
		isGrounded = false;
		upRay = new UnityEngine.Ray(transform.position + UnityEngine.Vector3.up * rayLength, UnityEngine.Vector3.down);
		float verticalDistance;
		if (UnityEngine.Physics.Raycast(upRay, out hit, rayLength, mask))
		{
			isGrounded = true;
			vSpeed = UnityEngine.Mathf.Max((transform.position.y - hit.point.y) / elapsedTime, vSpeed);
			transform.position = hit.point;
			verticalDistance = vSpeed * elapsedTime;
		}
		else
		{
			float initialSpeed = vSpeed;
			vSpeed -= gravityAcc * elapsedTime;
			verticalDistance = initialSpeed * elapsedTime - gravityAcc * elapsedTime * elapsedTime * 0.5f - gravityAcc * FrameRateControl.ReferenceFrameDuration * elapsedTime * 0.5f;
		}
		downRay = new UnityEngine.Ray(transform.position, UnityEngine.Vector3.down);
		if (UnityEngine.Physics.Raycast(downRay, out hit, UnityEngine.Mathf.Max(0f, -verticalDistance), mask))
		{
			isGrounded = true;
			vSpeed = 0f;
			verticalDistance = 0f;
		}
		if (UnityEngine.Physics.Raycast(downRay, out hit, 0.1f, mask))
		{
			isGrounded = true;
		}
		transform.localPosition += UnityEngine.Vector3.up * verticalDistance;
	}

	public void OnDrawGizmos()
	{
		UnityEngine.Gizmos.color = UnityEngine.Color.blue;
		UnityEngine.Gizmos.DrawSphere(transform.position, 0.1f);
	}

	public void Jump()
	{
		if (isGrounded && vSpeed < 0f)
		{
			vSpeed = 0f;
		}
		vSpeed += data.jumpSpeed;
		transform.position = transform.position + UnityEngine.Vector3.up * vSpeed * FrameRateControl.ReferenceFrameDuration * 2f;
		lastVerticalElapsedTime = FrameRateControl.ReferenceFrameDuration;
	}

	public SledgePoint()
	{
		gravityAcc = 2f;
		isGrounded = true;
		rayLength = 0.3f;
		lastVerticalElapsedTime = FrameRateControl.ReferenceFrameDuration;
	}
}
