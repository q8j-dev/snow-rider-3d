public class SettingsButton : UnityEngine.MonoBehaviour
{
	public void OnClick()
	{
		GameControl.OnSettings.Invoke();
	}

}
