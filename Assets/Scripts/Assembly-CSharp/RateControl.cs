public class RateControl : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public UnityEngine.UI.Text titleText;

	public void OnClickLikeYes()
	{
		GUIControl.instance.ChangeCanvas(GUIControl.instance.likeCanvasPrefab);
	}

	public void OnClickLikeNo()
	{
		GUIControl.instance.ChangeCanvas(GUIControl.instance.endCanvasPrefab);
		UnityEngine.PlayerPrefs.SetInt("Rated", 1);
	}

	public void OnClickRateYes()
	{
		UnityEngine.PlayerPrefs.SetInt("Rated", 1);
		GUIControl.instance.ChangeCanvas(GUIControl.instance.endCanvasPrefab);
	}

	public void OnClickRateNo()
	{
		GUIControl.instance.ChangeCanvas(GUIControl.instance.endCanvasPrefab);
		UnityEngine.PlayerPrefs.SetInt("Rated", 1);
	}

	public void OnClickRateLater()
	{
		GameControl.playsToRate += 10;
		GUIControl.instance.ChangeCanvas(GUIControl.instance.sledsCanvasPrefab);
	}

}
