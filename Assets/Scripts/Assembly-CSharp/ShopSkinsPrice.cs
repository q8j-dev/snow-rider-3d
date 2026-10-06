public class ShopSkinsPrice : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.UI.Text t;

	[System.NonSerialized]
	public ShopSleds s;

	public void Awake()
	{
		t = GetComponent<UnityEngine.UI.Text>();
		s = ShopSleds.inst;
		ShopSleds.OnChangeItem.AddListener(OnChangeItem);
	}

	public void OnChangeItem()
	{
		if (s.currentModelIndex < s.skinData.skins.Count)
		{
			t.text = s.skinData.skins[s.currentModelIndex].price.ToString();
		}
	}

}
