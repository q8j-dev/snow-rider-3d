public class GenModifier : UnityEngine.MonoBehaviour
{
	public virtual void BeforeApply()
	{
		GGen.instance.moded = true;
	}

	public virtual void AfterApply()
	{
		return;
	}

	public virtual void EndApply()
	{
		return;
	}

}
