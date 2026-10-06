public class DisableAfter : UnityEngine.MonoBehaviour
{
	public float disableItAfter;

	public void OnEnable()
	{
		CancelInvoke();
		Invoke("DisableAfterFun", disableItAfter);
	}

	public void OnDisable()
	{
		CancelInvoke();
	}

	public void DisableAfterFun()
	{
		gameObject.SetActive(false);
	}

	public DisableAfter()
	{
		disableItAfter = 1.3f;
	}
}
