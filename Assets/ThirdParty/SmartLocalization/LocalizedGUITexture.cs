using System;
using UnityEngine;

namespace SmartLocalization
{

public class LocalizedGUITexture : MonoBehaviour
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
		GetComponent<UnityEngine.UI.RawImage>().texture = LanguageManager.Instance.GetTexture(localizedKey);
	}
}

}

