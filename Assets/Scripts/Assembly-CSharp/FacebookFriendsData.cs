public class FacebookFriendsData : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	public struct Friend
	{
		public string name;

		public int score;

		public UnityEngine.Sprite sprite;
	}

	[System.Serializable]
	public class FacebookScores
	{
		public System.Collections.Generic.List<FacebookFriendsData.FacebookData> data;

	}

	[System.Serializable]
	public class FacebookData
	{
		public int score;

		public FacebookFriendsData.FacebookUser user;

	}

	[System.Serializable]
	public class FacebookUser
	{
		public string name;

		public string id;

	}

	[System.Serializable]
	public struct StoredData
	{
		public string name;

		public string id;

		public int score;

		public byte[] sprite;
	}
	public System.Collections.Generic.List<FacebookFriendsData.Friend> friends;

	[UnityEngine.SerializeField]
	public static System.Collections.Generic.List<FacebookFriendsData.StoredData> storedData = new System.Collections.Generic.List<FacebookFriendsData.StoredData>();

	[System.NonSerialized]
	public byte[] spriteResult;

	public void FullUpdate()
	{
		friends.Clear();
	}

	public void PartialUpdate()
	{
		return;
	}

	public void WriteData()
	{
		BinarySerializationUtil.Write(
			UnityEngine.Application.persistentDataPath + "/FacebookFriends.dat", ref storedData);
	}

	public void GetFacebookData()
	{
		return;
	}
	public System.Collections.IEnumerator PrepareStoredDataRoutine(string JSONResult)
	{
		FacebookScores scores = UnityEngine.JsonUtility.FromJson<FacebookScores>(JSONResult);
		foreach (FacebookData data in scores.data)
		{
			StoredData stored = new StoredData
			{
				name = data.user.name,
				id = data.user.id,
				score = data.score,
				sprite = null
			};
			yield return StartCoroutine(GetImageRoutine(stored.id));
			stored.sprite = spriteResult;
			storedData.Add(stored);
		}
		WriteData();
	}
	public System.Collections.IEnumerator GetImageRoutine(string facebook_id)
	{
		yield break;
	}

	public FacebookFriendsData()
	{
		friends = new System.Collections.Generic.List<FacebookFriendsData.Friend>();
	}
}
