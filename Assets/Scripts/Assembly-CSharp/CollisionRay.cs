public class CollisionRay : UnityEngine.MonoBehaviour
{
	public float rayLenght;

	[System.NonSerialized]
	public UnityEngine.Ray ray;

	[System.NonSerialized]
	public UnityEngine.RaycastHit hit;

	[System.NonSerialized]
	public UnityEngine.LayerMask mask;

	public void Awake()
	{
		int obstacleLayer = UnityEngine.LayerMask.NameToLayer("Obstacle");
		mask = 1 << (obstacleLayer >= 0 ? obstacleLayer : 9);
	}

	public bool Hit()
	{
		float sweepLength = rayLenght;
		if (PlayerControl.instance != null)
		{
			sweepLength = UnityEngine.Mathf.Max(sweepLength, PlayerControl.instance.currMoveSpeed * UnityEngine.Time.deltaTime);
		}
		ray = new UnityEngine.Ray(transform.position - transform.forward * sweepLength, transform.forward);
		return UnityEngine.Physics.Raycast(ray, out hit, sweepLength, mask);
	}

	public void OnDrawGizmos()
	{
		UnityEngine.Gizmos.color = UnityEngine.Color.red;
		UnityEngine.Gizmos.DrawRay(ray);
	}

	public CollisionRay()
	{
		rayLenght = 1f;
	}
}
