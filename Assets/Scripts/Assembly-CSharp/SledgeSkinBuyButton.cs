public class SledgeSkinBuyButton : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public ShopSleds s;

	public void OnEnable()
	{
		s = ShopSleds.inst;
	}

	public void OnClick()
	{
		Skin skin = s.skinData.skins[s.currentModelIndex];
		if (skin.price <= GameControl.gifts)
		{
			GameControl.gifts -= skin.price;
			UnityEngine.PlayerPrefs.SetInt("SledBought" + s.currentModelIndex, 1);
			s.ApplySkin(s.currentModelIndex);
		}
	}

}
