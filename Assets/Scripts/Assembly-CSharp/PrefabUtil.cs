public class PrefabUtil : UnityEngine.MonoBehaviour
{
	public static UnityEngine.GameObject Instantiate(ScriptableObj prefab, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
	{
		UnityEngine.GameObject gameObject = new UnityEngine.GameObject(prefab.name);
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;
		foreach (ChunkObj item in prefab.obj)
		{
			UnityEngine.Transform transform = UnityEngine.Object.Instantiate(item.prefab).transform;
			transform.SetParent(gameObject.transform);
			transform.localPosition = item.pos;
			transform.localRotation = item.rot;
			transform.localScale = item.scale;
		}
		return gameObject.transform.gameObject;
	}

}
