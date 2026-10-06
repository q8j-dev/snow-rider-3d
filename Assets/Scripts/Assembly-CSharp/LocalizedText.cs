public class LocalizedText : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public string stringName;

	public void Start()
	{
		if (stringName != null)
		{
			GetComponent<UnityEngine.UI.Text>().text = SmartLocalization.LanguageManager.Instance.GetTextValue(stringName);
		}
	}

	public static string SetText(string textName)
	{
		return SmartLocalization.LanguageManager.Instance.GetTextValue(textName);
	}

}
