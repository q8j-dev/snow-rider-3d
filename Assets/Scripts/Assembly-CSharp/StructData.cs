public class StructData : UnityEngine.ScriptableObject
{
	public new string name;

	public int minScoreToSpawn;

	public int maxScoreToSpawn;

	public float probability;

	public int minLenght;

	public int maxLength;

	public GenModifier mod;

	public System.Collections.Generic.List<Chunk> midChunks;

	public System.Collections.Generic.List<Chunk> sideChunks;

	public StructData()
	{
		maxScoreToSpawn = int.MaxValue;
	}
}
