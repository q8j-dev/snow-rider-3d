public class Slowdown : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public bool onAwake;

	[UnityEngine.SerializeField]
	public float amount;

	[UnityEngine.SerializeField]
	public float duration;

	public void Awake()
	{
		if (onAwake)
		{
			Apply();
		}
	}

	public void Apply()
	{
		SlowMotion.instance.Apply(duration, amount);
	}

	public Slowdown()
	{
		amount = 0.05f;
		duration = 1f;
	}
}
