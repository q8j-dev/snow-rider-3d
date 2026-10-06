public class FacebookTarget : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	public class FBUser
	{
		public string facebook_id;

		public int score;

		public UnityEngine.Sprite sprite;

	}
	[UnityEngine.SerializeField]
	public UnityEngine.UI.Image targetImage;

	[UnityEngine.SerializeField]
	public UnityEngine.UI.Text targetText;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject targetObject;

	[UnityEngine.SerializeField]
	public Friend currTarget;

	[UnityEngine.SerializeField]
	public Friend nextTarget;

	[System.NonSerialized]
	public bool foundTarget;

	[System.NonSerialized]
	public bool canGetTarget;

	[System.NonSerialized]
	public bool thereAreMoreTargets;

	public void Awake()
	{
		gameObject.SetActive(false);
	}

	public void OnStart()
	{
		GameControl.OnGetScore.AddListener(OnGetScore);
		StartCoroutine(StartGetScoreRoutine());
	}
	public System.Collections.IEnumerator StartGetScoreRoutine()
	{
		yield return StartCoroutine(OnGetScoreRoutine());
		yield return StartCoroutine(OnGetScoreRoutine());
	}

	public void OnGetScore()
	{
		StartCoroutine(OnGetScoreRoutine());
	}
	public System.Collections.IEnumerator OnGetScoreRoutine()
	{
		if (GameControl.score <= currTarget.score && GameControl.score != 0)
		{
			SetTargetText();
			yield break;
		}
		if (!thereAreMoreTargets)
		{
			HideTarget();
			yield break;
		}
		ShowNextTarget();
		yield return StartCoroutine(GetNextTargetRoutine(currTarget.score));
	}

	public void ShowNextTarget()
	{
		if (nextTarget.score != 0)
		{
			currTarget = nextTarget;
			nextTarget = null;
			SetTargetImage();
			SetTargetText();
		}
	}

	public void GetNextTarget()
	{
		StartCoroutine(GetNextTargetRoutine(currTarget.score));
	}
	public System.Collections.IEnumerator GetNextTargetRoutine(int score)
	{
		foundTarget = false;
		if (!foundTarget)
		{
			yield return StartCoroutine(GetFriendTarget(score));
		}
		if (!foundTarget)
		{
			yield return StartCoroutine(GetOnlineTarget(score));
		}
		if (!foundTarget)
		{
			yield return StartCoroutine(GetLocalFriendTarget(score));
		}
		if (!foundTarget)
		{
			thereAreMoreTargets = false;
		}
	}
	public System.Collections.IEnumerator GetFriendTarget(int score)
	{
		yield break;
	}
	public System.Collections.IEnumerator GetLocalFriendTarget(int score)
	{
		yield break;
	}
	public System.Collections.IEnumerator GetOnlineTarget(int score)
	{
		yield break;
	}

	public void UpdateTarget(Friend target)
	{
		nextTarget = target;
		foundTarget = true;
	}

	public void SetTargetImage()
	{
		targetImage.sprite = currTarget.sprite;
		targetObject.GetComponent<MenuElement>().ShowElement();
	}

	public void SetTargetText()
	{
		targetText.text = (currTarget.score - GameControl.score).ToString();
		targetText.GetComponent<MenuElement>().ShowElement();
	}

	public void HideTarget()
	{
		if (targetObject.GetComponent<MenuElement>().isVisible)
		{
			targetObject.GetComponent<MenuElement>().HideElement();
			targetText.GetComponent<MenuElement>().HideElement();
		}
	}

	public FacebookTarget()
	{
		canGetTarget = true;
		thereAreMoreTargets = true;
	}
}
