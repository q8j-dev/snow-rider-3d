public class alala : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	public class FBUser
	{
		public string facebook_id;

		public int score;

		public UnityEngine.Sprite sprite;

	}
	[UnityEngine.SerializeField]
	public UnityEngine.UI.Image image;

	[System.NonSerialized]
	public string facebook_id;

	[System.NonSerialized]
	public alala.FBUser currUser;

	[System.NonSerialized]
	public alala.FBUser nextUser;

	public void Awake()
	{
		return;
	}
	public System.Collections.IEnumerator SetStartNext()
	{
		yield break;
	}
	public System.Collections.IEnumerator GetNext(int score)
	{
		yield break;
	}
	public System.Collections.IEnumerator GetImage()
	{
		yield break;
	}

	public alala()
	{
		facebook_id = "100001017571898";
	}
}
