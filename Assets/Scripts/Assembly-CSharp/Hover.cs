public class Hover : UnityEngine.MonoBehaviour
{
	public UnityEngine.Vector3 speed;

	public UnityEngine.Vector3 amplitude;

	public bool timeScaleEnabled;

	[System.NonSerialized]
	public UnityEngine.Vector3 startPos;

	[System.NonSerialized]
	public float time;

	public void Start()
	{
		startPos = transform.localPosition;
	}

	public void Update()
	{
		time = timeScaleEnabled ? UnityEngine.Time.time : UnityEngine.Time.realtimeSinceStartup;
		transform.localPosition = startPos + new UnityEngine.Vector3(
			UnityEngine.Mathf.Sin(time * speed.x * UnityEngine.Mathf.PI * 2f) * amplitude.x,
			UnityEngine.Mathf.Sin(time * speed.y * UnityEngine.Mathf.PI * 2f) * amplitude.y,
			UnityEngine.Mathf.Sin(time * speed.z * UnityEngine.Mathf.PI * 2f) * amplitude.z);
	}

}
