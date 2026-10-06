public class CampControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public ScriptableObj campPrefab;

	public static CampControl instance;

	[System.NonSerialized]
	public UnityEngine.GameObject camp;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public void OnMain()
	{
		if (camp != null)
		{
			if (camp.activeSelf)
			{
				camp.SetActive(false);
			}
			camp.SetActive(true);
		}
		else
		{
			camp = PrefabUtil.Instantiate(campPrefab, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
			camp.transform.parent = transform;
		}
		PlayerControl.instance.transform.position = UnityEngine.GameObject.FindGameObjectWithTag("SledsPoint").transform.position;
		PlayerControl.instance.transform.rotation = UnityEngine.GameObject.FindGameObjectWithTag("SledsPoint").transform.rotation;
	}

	public void Update()
	{
		if (GameControl.gameMode == GameMode.play && PlayerControl.instance.transform.position.z > 10f)
		{
			camp.SetActive(false);
		}
	}

}
