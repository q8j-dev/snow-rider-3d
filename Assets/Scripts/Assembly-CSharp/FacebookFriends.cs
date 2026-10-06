public class FacebookFriends : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject friendPrefab;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject inviteButtonPrefab;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject scrollList;

	public void GetFacebookScores()
	{
		ShowFriends();
	}

	public void ShowFriends()
	{
		FriendsData data = FriendsData.instance;
		foreach (UnityEngine.Transform child in scrollList.transform)
		{
			UnityEngine.Object.Destroy(child.gameObject);
		}
		for (int i = 0; i < data.friends.Count; i++)
		{
			UnityEngine.GameObject item = UnityEngine.Object.Instantiate(friendPrefab);
			item.transform.parent = scrollList.transform;
			item.transform.localRotation = UnityEngine.Quaternion.identity;
			item.transform.localScale = UnityEngine.Vector3.one;
			UnityEngine.Transform image = item.transform.Find("FriendImageBorder").Find("FriendImageMask").Find("FriendImage");
			item.transform.Find("FriendScore").GetComponent<UnityEngine.UI.Text>().text = data.friends[i].score.ToString();
			image.GetComponent<UnityEngine.UI.Image>().sprite = data.friends[i].sprite;
		}
		if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork || UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork)
		{
			UnityEngine.GameObject invite = UnityEngine.Object.Instantiate(inviteButtonPrefab);
			invite.transform.parent = scrollList.transform;
			invite.transform.localRotation = UnityEngine.Quaternion.identity;
			invite.transform.localScale = UnityEngine.Vector3.one;
		}
	}

}
