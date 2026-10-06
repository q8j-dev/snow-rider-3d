public class Swing : UnityEngine.MonoBehaviour
{
	public UnityEngine.Vector3 maxAngle;

	public UnityEngine.Vector3 interval;

	public bool timeScaleEnabled;

	[System.NonSerialized]
	public UnityEngine.Quaternion startRot;

	[System.NonSerialized]
	public float time;

	public void Start()
	{
		startRot = transform.rotation;
	}

	public void Update()
	{
		time = timeScaleEnabled ? UnityEngine.Time.time : UnityEngine.Time.realtimeSinceStartup;
		transform.rotation = startRot * UnityEngine.Quaternion.Euler(
			UnityEngine.Mathf.Sin(time * interval.x * UnityEngine.Mathf.PI * 2f) * maxAngle.x,
			UnityEngine.Mathf.Sin(time * interval.y * UnityEngine.Mathf.PI * 2f) * maxAngle.y,
			UnityEngine.Mathf.Sin(time * interval.z * UnityEngine.Mathf.PI * 2f) * maxAngle.z);
	}

}
