public class ShopSledTitle : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public UnityEngine.UI.Text t;

	[System.NonSerialized]
	public ShopSleds s;

	public void OnEnable()
	{
		t = GetComponent<UnityEngine.UI.Text>();
		s = ShopSleds.inst;
		ShopSleds.OnChangeItem.AddListener(OnChangeItem);
	}

	public void OnChangeItem()
	{
		if (s.currentModelIndex < s.skinData.skins.Count)
		{
			t.text = LocalizedText.SetText(s.skinData.skins[s.currentModelIndex].title);
		}
	}

}
