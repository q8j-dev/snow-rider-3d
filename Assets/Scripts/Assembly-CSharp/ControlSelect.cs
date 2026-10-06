public class ControlSelect : UnityEngine.MonoBehaviour
{
	public void SelectTap()
	{
		GameControl.controlMode = ControlMode.sided;
		OnClick();
	}

	public void SelectTilt()
	{
		GameControl.controlMode = ControlMode.tilt;
		OnClick();
	}

	public void OnClick()
	{
		if (GameControl.gameMode == GameMode.intro)
		{
			GUIControl.instance.OnSleds();
		}
		else if (!GameControl.tutorialCompleted)
		{
			GUIControl.instance.OnTutorial();
		}
	}

}
