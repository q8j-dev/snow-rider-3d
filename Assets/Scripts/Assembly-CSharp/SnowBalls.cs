public class SnowBalls : GenModifier
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject spawner;

	[UnityEngine.SerializeField]
	public ScriptableObj startChunk;

	[UnityEngine.SerializeField]
	public ScriptableObj endChunk;

	[System.NonSerialized]
	public int dir;

	public override void BeforeApply()
	{
		base.BeforeApply();
		if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
		{
			GGen.instance.currentChunksData[0] = startChunk;
			GGen.instance.currentChunksData[GGen.instance.genData.structWidth - 1] = endChunk;
			dir = 1;
		}
		else
		{
			GGen.instance.currentChunksData[0] = endChunk;
			GGen.instance.currentChunksData[GGen.instance.genData.structWidth - 1] = startChunk;
			dir = -1;
		}
	}

	public override void AfterApply()
	{
		UnityEngine.Vector3 position = UnityEngine.Vector3.zero;
		UnityEngine.Transform[] componentsInChildren = GGen.instance.structParent.gameObject.GetComponentsInChildren<UnityEngine.Transform>();
		foreach (UnityEngine.Transform transform in componentsInChildren)
		{
			if (transform.tag == "Spawner")
			{
				position = transform.position;
				break;
			}
		}
		UnityEngine.GameObject gameObject = UnityEngine.Object.Instantiate(spawner);
		gameObject.transform.parent = GGen.instance.structParent;
		gameObject.transform.position = position;
		gameObject.transform.GetComponent<SnowBallSpawner>().dir = dir;
	}

}
