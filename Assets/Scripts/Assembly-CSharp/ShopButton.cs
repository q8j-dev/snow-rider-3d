public class ShopButton : UnityEngine.MonoBehaviour
{
	public void OnClick()
	{
		GameControl.OnShop.Invoke();
	}

}
