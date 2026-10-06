public class ShopSleds : UnityEngine.MonoBehaviour
{
	public static UnityEngine.Events.UnityEvent OnChangeItem = new UnityEngine.Events.UnityEvent();

	[UnityEngine.SerializeField]
	public SkinData skinData;

	[UnityEngine.SerializeField]
	public MenuBlock navigationBlock;

	[UnityEngine.SerializeField]
	public MenuBlock itemInfoBlock;

	[UnityEngine.SerializeField]
	public MenuBlock buyBlock;

	[UnityEngine.SerializeField]
	public MenuBlock ownedBlock;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject buyObject;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject comingSoonPrefab;

	[UnityEngine.HideInInspector]
	public int currentModelIndex;

	[UnityEngine.HideInInspector]
	[System.NonSerialized]
	public UnityEngine.GameObject sledsModel;

	public static ShopSleds inst;

	public void Awake()
	{
		GameControl.OnSleds.AddListener(OnSleds);
		currentModelIndex = GameControl.currentSkin;
		ApplySkin(currentModelIndex);
		if (inst == null)
		{
			inst = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public void OnSleds()
	{
		UnityEngine.Object.Destroy(sledsModel);
	}

	public void OnClickLeft()
	{
		currentModelIndex--;
		if (currentModelIndex < 0)
		{
			currentModelIndex = skinData.skins.Count;
		}
		ApplySkin(currentModelIndex);
	}

	public void OnClickRight()
	{
		currentModelIndex++;
		if (skinData.skins.Count < currentModelIndex)
		{
			currentModelIndex = 0;
		}
		ApplySkin(currentModelIndex);
	}

	public void ApplySkin(int index)
	{
		OnChangeItem.Invoke();
		UnityEngine.GameObject shopObject = UnityEngine.GameObject.FindGameObjectWithTag("Shop");
		Rotation shopRotation = shopObject.GetComponentInParent<Rotation>();
		Popup shopPopup = shopObject.GetComponent<Popup>();
		if (shopPopup != null)
		{
			
			
			
			
			shopObject.transform.localScale = shopPopup.startScale;
			UnityEngine.Object.Destroy(shopPopup);
		}
		if (sledsModel != null)
		{
			UnityEngine.Object.Destroy(sledsModel);
		}

		if (skinData.skins.Count == index)
		{
			ownedBlock.gameObject.SetActive(false);
			buyBlock.gameObject.SetActive(false);
			itemInfoBlock.gameObject.SetActive(false);
			sledsModel = UnityEngine.Object.Instantiate(comingSoonPrefab);
			sledsModel.transform.parent = shopObject.transform;
			sledsModel.transform.localPosition = UnityEngine.Vector3.zero;
			sledsModel.transform.localRotation = UnityEngine.Quaternion.identity;
			shopRotation.enabled = false;
			
			
			
			
			shopObject.transform.rotation = UnityEngine.Quaternion.Euler(0f, 80f, 0f);
			sledsModel.transform.localRotation = UnityEngine.Quaternion.identity;
			return;
		}

		shopObject.transform.rotation = UnityEngine.Quaternion.Euler(-30f, 0f, 0f);
		shopRotation.enabled = true;
		Skin skin = skinData.skins[index];
		sledsModel = UnityEngine.Object.Instantiate(skin.model);
		sledsModel.transform.parent = shopObject.transform;
		sledsModel.transform.localPosition = UnityEngine.Vector3.zero;
		sledsModel.transform.localRotation = UnityEngine.Quaternion.identity;
		if (sledsModel.transform.localEulerAngles == UnityEngine.Vector3.zero)
		{
			sledsModel.transform.localScale = UnityEngine.Vector3.one;
		}
		itemInfoBlock.gameObject.SetActive(true);
		if (UnityEngine.PlayerPrefs.GetInt("SledBought" + index) == 0 && skin.price >= 1)
		{
			ownedBlock.gameObject.SetActive(false);
			buyBlock.gameObject.SetActive(true);
			return;
		}
		ownedBlock.gameObject.SetActive(true);
		buyBlock.gameObject.SetActive(false);
		GameControl.currentSkin = index;
		PlayerControl.instance.GetComponent<SledSkinSelector>().ChangeSkin(index);
	}

}
