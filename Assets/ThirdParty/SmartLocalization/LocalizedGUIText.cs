using System;
using UnityEngine;

namespace SmartLocalization
{

public class LocalizedGUIText : MonoBehaviour
{
	public string localizedKey = "INSERT_KEY_HERE";

	private void Start()
	{
		LanguageManager instance = LanguageManager.Instance;
		instance.OnChangeLanguage += OnChangeLanguage;
		OnChangeLanguage(instance);
	}

	private void OnDestroy()
	{
		if (LanguageManager.HasInstance)
		{
			LanguageManager instance = LanguageManager.Instance;
			instance.OnChangeLanguage -= OnChangeLanguage;
		}
	}

	private void OnChangeLanguage(LanguageManager languageManager)
	{
		GetComponent<UnityEngine.UI.Text>().text = LanguageManager.Instance.GetTextValue(localizedKey);
	}
}

}

