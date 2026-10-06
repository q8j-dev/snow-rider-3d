public class EndPlaceGUI : UnityEngine.MonoBehaviour
{
	public void OnEnable()
	{
		GetComponent<UnityEngine.UI.Text>().text =
			(OnlineControl.worldPlace + 1) + "   " + LocalizedText.SetText("Of") + "   " + OnlineControl.worldAllPlaces;
	}

}
