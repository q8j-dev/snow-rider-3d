public class DestructibleObj : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public ScriptableObj prefab;

	public void OnDestruct()
	{
		UnityEngine.GameObject gameObject = PrefabUtil.Instantiate(prefab, transform.position, transform.rotation);
		gameObject.transform.parent = transform.parent;
		UnityEngine.Object.Destroy(this.gameObject);
	}

}
