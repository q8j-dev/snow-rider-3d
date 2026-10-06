public class FriendsData : UnityEngine.MonoBehaviour
{
	public enum UpdateMode
	{
		Full = 0,
		Partial = 1,
		Self = 2,
		Local = 3,
		None = 4
	}

	[System.Serializable]
	public class AdapterData
	{
		public string name;

		public string id;

		public int score;

		public byte[] sprite;

		public Friend Export()
		{
			var friend = new Friend
			{
				name = name,
				id = id,
				score = score
			};
			var texture = new UnityEngine.Texture2D(2, 2);
			UnityEngine.ImageConversion.LoadImage(texture, sprite);
			friend.sprite = UnityEngine.Sprite.Create(
				texture,
				new UnityEngine.Rect(0f, 0f, texture.width, texture.height),
				UnityEngine.Vector2.zero,
				1f);
			return friend;
		}

	}

	[System.Serializable]
	public class FacebookScores
	{
		public System.Collections.Generic.List<FriendsData.FacebookData> data;

	}

	[System.Serializable]
	public class FacebookData
	{
		public int score;

		public FriendsData.FacebookUser user;

	}

	[System.Serializable]
	public class FacebookUser
	{
		public string name;

		public string id;

	}

	public System.Collections.Generic.List<Friend> friends;

	[System.NonSerialized]
	public byte[] currentImage;

	[System.NonSerialized]
	public System.Collections.Generic.List<FriendsData.AdapterData> adapterData;

	[System.NonSerialized]
	public string savePath;

	[System.NonSerialized]
	public FriendsData.UpdateMode updateMode;

	public static FriendsData instance;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
		savePath = UnityEngine.Application.persistentDataPath + "/FbFriends.dat";
		GameControl.OnIntro.AddListener(UpdateLocal);
		GameControl.OnEnd.AddListener(A);
	}

	public void FullUpdate()
	{
		_ = OnlineControl.isOnline;
	}

	public void PartialUpdate()
	{
		_ = OnlineControl.isOnline;
	}

	public void A()
	{
		UpdateSelf();
	}

	public void UpdateSelf()
	{
		print("Self Update");
		updateMode = FriendsData.UpdateMode.Self;
		ReadData();
		print("Updating  self fbid = " + FacebookManager.currentFacebookId);
		for (int i = 0; i < adapterData.Count; i++)
		{
			if (adapterData[i].id == FacebookManager.currentFacebookId)
			{
				print("found self" + adapterData[i].name);
				if (adapterData[i].score < GameControl.score)
				{
					adapterData[i].score = GameControl.score;
					print(adapterData[i].score);
				}
				break;
			}
		}
		WriteData();
		OutputData();
	}

	public void UpdateLocal()
	{
		print("Update Local");
		updateMode = FriendsData.UpdateMode.Local;
		ReadData();
		OutputData();
	}

	public void WriteData()
	{
		BinarySerializationUtil.Write(savePath, ref adapterData);
		if (updateMode == FriendsData.UpdateMode.Full)
		{
			ReadData();
		}
	}

	public void ReadData()
	{
		BinarySerializationUtil.Read(savePath, ref adapterData);
		if (updateMode == FriendsData.UpdateMode.Full)
		{
			OutputData();
		}
	}

	public void OutputData()
	{
		friends.Clear();
		while (adapterData.Count > 0)
		{
			FriendsData.AdapterData lowest = adapterData[0];
			for (int i = 0; i < adapterData.Count; i++)
			{
				if (adapterData[i].score < lowest.score)
				{
					lowest = adapterData[i];
				}
			}
			friends.Add(lowest.Export());
			adapterData.Remove(lowest);
		}
	}

	public void SetFacebookScore()
	{
		return;
	}

	public FriendsData()
	{
		friends = new System.Collections.Generic.List<Friend>();
		adapterData = new System.Collections.Generic.List<FriendsData.AdapterData>();
	}
}
