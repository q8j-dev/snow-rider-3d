public class Tunnel : GenModifier
{
	[UnityEngine.SerializeField]
	public System.Collections.Generic.List<ScriptableObj> tunnelPrefab;

	[UnityEngine.SerializeField]
	public StructData nextStruct;

	public override void BeforeApply()
	{
		base.BeforeApply();
		if (GameControl.score < 31)
		{
			GGen.instance.currentChunksData[1] = tunnelPrefab[UnityEngine.Random.Range(0, tunnelPrefab.Count)];
			GGen.instance.currentChunksData[2] = tunnelPrefab[UnityEngine.Random.Range(0, tunnelPrefab.Count)];
		}
		else
		{
			GGen.instance.currentChunksData[UnityEngine.Random.Range(1, 3)] = tunnelPrefab[UnityEngine.Random.Range(0, tunnelPrefab.Count)];
		}
	}

	public override void AfterApply()
	{
		GGen.instance.currentStruct = nextStruct;
	}

}
