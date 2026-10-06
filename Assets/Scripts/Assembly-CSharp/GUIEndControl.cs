public class GUIEndControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.GameObject giftsBlock;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject rankBlock;

	public void ChooseEnd()
	{
		bool hasInternet = UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork
			|| UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaCarrierDataNetwork;
		if (hasInternet && OnlineControl.gotScore)
		{
			giftsBlock.SetActive(false);
			rankBlock.SetActive(true);
		}
		else
		{
			rankBlock.SetActive(false);
			giftsBlock.SetActive(true);
		}
	}

}
