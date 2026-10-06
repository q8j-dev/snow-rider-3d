public class DataText : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public string variableName;

	[System.NonSerialized]
	public UnityEngine.UI.Text text;

	public void Awake()
	{
		text = GetComponent<UnityEngine.UI.Text>();
	}

	public void OnEnable()
	{
		text.text = UnityEngine.PlayerPrefs.GetInt(variableName).ToString();
	}

}
