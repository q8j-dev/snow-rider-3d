public class SledSkinSelector : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public SkinData skinData;

	[UnityEngine.SerializeField]
	public UnityEngine.GameObject currModel;

	[UnityEngine.SerializeField]
	public UnityEngine.Transform skinParent;

	public void OnEnable()
	{
		ChangeSkin(GameControl.currentSkin);
	}

	public void ChangeSkin(int index)
	{
		GameControl.currentSkin = index;
		if (currModel != null)
		{
			UnityEngine.Object.Destroy(currModel);
			currModel = null;
		}
		currModel = UnityEngine.Object.Instantiate(skinData.skins[index].model);
		currModel.transform.parent = skinParent;
		currModel.transform.localPosition = UnityEngine.Vector3.zero;
		currModel.transform.localRotation = UnityEngine.Quaternion.identity;
	}

}
