public class SnowParticleControl : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.Vector3 startPos;

	public void Awake()
	{
		startPos = transform.localPosition;
		GameControl.OnIntro.AddListener(OnIntro);
		GameControl.OnPlay.AddListener(OnPlay);
	}

	public void OnIntro()
	{
		transform.localPosition = new UnityEngine.Vector3(7f, 8f, 18f);
		var main = GetComponent<UnityEngine.ParticleSystem>().main;
		main.startLifetime = 5f;
	}

	public void OnPlay()
	{
		transform.localPosition = startPos;
		var main = GetComponent<UnityEngine.ParticleSystem>().main;
		main.startLifetime = 2f;
	}

}
