public class Rotation : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.Vector3 speed;

	public void Update()
	{
		UnityEngine.Vector3 euler = transform.rotation.eulerAngles;
		transform.rotation = UnityEngine.Quaternion.Euler(0f, euler.y + speed.y * UnityEngine.Time.deltaTime, 0f);
	}

}
