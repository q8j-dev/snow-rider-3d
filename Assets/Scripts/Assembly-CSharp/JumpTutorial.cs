public class JumpTutorial : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public float s;

	public void Start()
	{
		if (UnityEngine.PlayerPrefs.GetInt("TutorialJump") != 0)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
	}

	public void Update()
	{
		if (UnityEngine.PlayerPrefs.GetInt("TutorialJump") != 0)
		{
			s -= 3f * UnityEngine.Time.deltaTime;
			transform.localPosition += UnityEngine.Vector3.one * s * FrameRateControl.ReferenceFrameScale;
			if (s < 0f)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public JumpTutorial()
	{
		s = 1f;
	}
}
