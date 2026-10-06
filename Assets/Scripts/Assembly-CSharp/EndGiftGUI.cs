public class EndGiftGUI : UnityEngine.MonoBehaviour
{
	public void OnEnable()
	{
		GetComponent<UnityEngine.UI.Text>().text = "+" + GameControl.giftsThisGame;
	}

}
