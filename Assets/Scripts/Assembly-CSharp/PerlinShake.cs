public class PerlinShake : UnityEngine.MonoBehaviour
{
	public static PerlinShake instance;

	public float duration;

	public float speed;

	public float magnitude;

	public CameraControlC cam;

	public void Awake()
	{
		instance = this;
		GameControl.OnEnd.AddListener(PlayShake);
		GameControl.OnLand.AddListener(PlayShake);
		cam = UnityEngine.GameObject.FindGameObjectWithTag("Cam").GetComponent<CameraControlC>();
	}

	public void PlayShake()
	{
		StartCoroutine(Shake(0.5f, 3f, 0.1f));
	}

	public void PlayShakeBig()
	{
		StartCoroutine(Shake(0.5f, 6f, 0.1f));
	}
	public System.Collections.IEnumerator Shake(float duration, float speed, float magnitude)
	{
		StopCoroutine("Shake");
		float elapsed = 0f;
		UnityEngine.Vector3 originalCameraPosition = UnityEngine.Camera.main.transform.localPosition;
		float randomStart = UnityEngine.Random.Range(-1000f, 1000f);
		while (elapsed < duration)
		{
			elapsed += UnityEngine.Time.deltaTime / UnityEngine.Time.timeScale;
			float progress = elapsed / duration;
			float fade = UnityEngine.Mathf.Clamp01(progress * 2f - 1f);
			double noiseCoordinate = randomStart + speed * progress;
			float x = (Util.Noise.GetNoise(noiseCoordinate, 0.0, 0.0) * 2f - 1f) * magnitude * (1f - fade);
			float y = (Util.Noise.GetNoise(0.0, noiseCoordinate, 0.0) * 2f - 1f) * magnitude * (1f - fade);
			cam.camOffsetPos = new UnityEngine.Vector3(x, y, 0f);
			yield return null;
		}
		cam.camOffsetPos = UnityEngine.Vector3.zero;
	}

	public PerlinShake()
	{
		duration = 0.5f;
		speed = 3f;
		magnitude = 0.1f;
	}
}
