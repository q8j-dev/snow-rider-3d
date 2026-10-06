public class FloatsEndMod : GenModifier
{
	public StructData nextStructure;

	public override void BeforeApply()
	{
		base.BeforeApply();
	}

	public override void AfterApply()
	{
		GGen.instance.currentStruct = nextStructure;
	}

}
