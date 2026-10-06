public class FacebookConnect : UnityEngine.MonoBehaviour
{
	public void OnClick()
	{
		StartCoroutine(TransitionControl.instance.EndTransition());
		FacebookManager.instance.Login();
	}

}
