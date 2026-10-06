public class SledSkinControl : UnityEngine.MonoBehaviour
{
	[System.Serializable]
	public class SledSkin
	{
		public string codeName;

		public bool isOwned;

	}

	public static string savePath;

	public static System.Collections.Generic.List<SledSkinControl.SledSkin> skins = new System.Collections.Generic.List<SledSkinControl.SledSkin>();

	public void Awake()
	{
		GameControl.OnIntro.AddListener(OnIntro);
		savePath = UnityEngine.Application.persistentDataPath + "/SledSkins.dat";
	}

	public static void Read()
	{
		BinarySerializationUtil.Read(savePath, ref skins);
		UnityEngine.Debug.Log("Sled Skin Count : " + skins.Count);
	}

	public static void Write()
	{
		BinarySerializationUtil.Write(savePath, ref skins);
	}

	public void OnIntro()
	{
		Read();
	}

}
