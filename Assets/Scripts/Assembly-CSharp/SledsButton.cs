public class SledsButton : UnityEngine.MonoBehaviour
{
	public void OnClick()
	{
		GameControl.OnSleds.Invoke();
	}

}
