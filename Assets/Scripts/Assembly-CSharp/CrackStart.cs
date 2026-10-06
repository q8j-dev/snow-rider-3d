public class CrackStart : GenModifier
{
	public StructData nextStructure;

	public ScriptableObj rampPrefab;

	public ScriptableObj sideRampprefab;

	public override void BeforeApply()
	{
		base.BeforeApply();
		int index = UnityEngine.Random.Range(0, GGen.instance.genData.structWidth);
		GGen.instance.currentChunksData[index] =
			(index < 1 || index >= GGen.instance.genData.structWidth - 1) ? sideRampprefab : rampPrefab;
	}

	public override void AfterApply()
	{
		GGen.instance.currentStruct = nextStructure;
	}

}
