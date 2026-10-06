public class testt : UnityEngine.MonoBehaviour
{
	public void Update()
	{
		if (GameInput.QPressedThisFrame)
		{
			GetComponent<MenuControler>().MenuShow();
		}
		if (GameInput.EPressedThisFrame)
		{
			GetComponent<MenuControler>().MenuHide();
		}
	}

}
