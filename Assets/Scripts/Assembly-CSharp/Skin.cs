[System.Serializable]
public struct Skin
{
	[UnityEngine.SerializeField]
	public string title;

	public string description;

	public UnityEngine.GameObject model;

	public ScriptableObj physicsModel;

	public int price;
}
