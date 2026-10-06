public class SledgeData : UnityEngine.ScriptableObject
{
	public float baseMoveSpeed;

	public float speedAcceleration;

	public float speedAccelerationAmplitude;

	public float rotationSpeed;

	public float rotationSmoothness;

	public float modelRotationSmoothness;

	public float pointJumpDelay;

	public float jumpSpeed;

	public SledgeData()
	{
		baseMoveSpeed = 5f;
		speedAcceleration = 5f;
		speedAccelerationAmplitude = 1f;
		rotationSpeed = 90f;
		rotationSmoothness = 0.08f;
		modelRotationSmoothness = 0.2f;
		pointJumpDelay = 0.1f;
		jumpSpeed = 3f;
	}
}
