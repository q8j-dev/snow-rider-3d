[System.Serializable]
public struct ChunkObj
{
	public UnityEngine.GameObject prefab;

	public UnityEngine.Vector3 pos;

	public UnityEngine.Quaternion rot;

	public UnityEngine.Vector3 scale;

	public ChunkObj(UnityEngine.GameObject prefab, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scale)
	{
		this.prefab = prefab;
		this.pos = pos;
		this.rot = rot;
		this.scale = scale;
	}
}
