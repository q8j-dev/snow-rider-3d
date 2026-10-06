public class SnowBallSpawner : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject snowballBigPrefab;

	[UnityEngine.SerializeField]
	public float interval;

	[UnityEngine.SerializeField]
	public int ratio;

	[UnityEngine.SerializeField]
	public float speed;

	[UnityEngine.SerializeField]
	public float randomness;

	public int dir;

	[System.NonSerialized]
	public int ballCount;

	public void Start()
	{
		SpawnStartBalls(null);
		StartCoroutine(SpawnRoutine());
	}
	public System.Collections.IEnumerator SpawnRoutine()
	{
		while (true)
		{
			yield return new UnityEngine.WaitForSeconds(interval * UnityEngine.Random.Range(1f - randomness, 1f + randomness));
			SpawnBall(snowballBigPrefab, UnityEngine.Vector3.zero, dir);
		}
	}

	public void SpawnBall(UnityEngine.GameObject prefab, UnityEngine.Vector3 pos, int dir)
	{
		UnityEngine.GameObject ball = UnityEngine.Object.Instantiate(prefab);
		ball.transform.parent = transform;
		ball.transform.localPosition = pos;
		SnowBall snowBall = ball.transform.GetComponent<SnowBall>();
		snowBall.direction = dir;
		ballCount++;
	}

	public void SpawnStartBalls(UnityEngine.GameObject prefab)
	{
		float distance = 0f;
		while (distance < GGen.instance.genData.structWidth * GGen.instance.genData.chunkSize.x)
		{
			SpawnBall(snowballBigPrefab, UnityEngine.Vector3.right * distance * dir, dir);
			distance += speed * interval;
		}
	}

}
