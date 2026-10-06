public class CloudControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject cloudPrefab;

	[UnityEngine.SerializeField]
	public float distanceBetween;

	[UnityEngine.SerializeField]
	public float randomness;

	[UnityEngine.SerializeField]
	public float cloudSize;

	[UnityEngine.SerializeField]
	public int cloudCountX;

	[UnityEngine.SerializeField]
	public int cloudCountY;

	public void Awake()
	{
		GameControl.OnMain.AddListener(OnMain);
	}

	public UnityEngine.GameObject SpawnClouds()
	{
		UnityEngine.GameObject cloudParent = new UnityEngine.GameObject("CloudParent");
		cloudParent.transform.parent = transform;
		for (int i = 0; i < cloudCountX; i++)
		{
			UnityEngine.Transform cloud = UnityEngine.Object.Instantiate(cloudPrefab).transform;
			cloud.parent = cloudParent.transform;
			cloud.localPosition = new UnityEngine.Vector3(i * distanceBetween, 0f, 0f) + UnityEngine.Random.insideUnitSphere * randomness;
			cloud.localRotation = UnityEngine.Quaternion.Euler(0f, UnityEngine.Random.Range(0, 360), 0f);
			cloud.localScale = UnityEngine.Vector3.one * cloudSize * (1f + UnityEngine.Random.Range(randomness, randomness));
		}
		return cloudParent;
	}

	public void OnMain()
	{
		for (int i = 0; i < cloudCountY; i++)
		{
			UnityEngine.Transform cloud = SpawnClouds().transform;
			cloud.SetParent(transform);
			cloud.localPosition = new UnityEngine.Vector3(0f, 0f, i * distanceBetween);
		}
	}

}
