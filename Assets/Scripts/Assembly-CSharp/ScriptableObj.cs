[System.Serializable]
public class ScriptableObj : UnityEngine.ScriptableObject
{
	public System.Collections.Generic.List<ChunkObj> obj;

	public void Add(UnityEngine.GameObject prefab, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scale)
	{
		obj.Add(new ChunkObj(prefab, pos, rot, scale));
	}

	public ScriptableObj()
	{
		obj = new System.Collections.Generic.List<ChunkObj>();
	}
}
