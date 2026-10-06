public class Gift : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public float DieSpeed;

	public void OnDie()
	{
		StartCoroutine(Die());
	}
	public System.Collections.IEnumerator Die()
	{
		gameObject.layer = 0;
		float scale = transform.localScale.x;
		while (scale > 0f)
		{
			transform.localScale = UnityEngine.Vector3.one * scale;
			yield return new UnityEngine.WaitForEndOfFrame();
			scale -= UnityEngine.Time.deltaTime / DieSpeed;
		}
		UnityEngine.Object.Destroy(gameObject);
		yield return null;
	}

	public Gift()
	{
		DieSpeed = 0.1f;
	}
}
