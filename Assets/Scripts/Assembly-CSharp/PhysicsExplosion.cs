public class PhysicsExplosion : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public float force;

	[System.NonSerialized]
	public float radius;

	public void Start()
	{
		UnityEngine.Collider[] colliders = UnityEngine.Physics.OverlapSphere(transform.position, radius);
		for (int i = 0; i < colliders.Length; i++)
		{
			UnityEngine.Rigidbody body = colliders[i].GetComponent<UnityEngine.Rigidbody>();
			if (body != null)
			{
				body.AddExplosionForce(force, transform.position, radius, 0f, UnityEngine.ForceMode.Impulse);
			}
		}
	}

	public PhysicsExplosion()
	{
		force = 5f;
		radius = 5f;
	}
}
