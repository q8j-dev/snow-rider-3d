public class LocalizationControl : UnityEngine.MonoBehaviour
{
	public void Awake()
	{
		UnityEngine.SystemLanguage systemLanguage = UnityEngine.Application.systemLanguage;
		if (systemLanguage == UnityEngine.SystemLanguage.German)
		{
			SmartLocalization.LanguageManager.Instance.ChangeLanguage("de");
		}
		else if (systemLanguage != UnityEngine.SystemLanguage.Korean)
		{
			if (systemLanguage == UnityEngine.SystemLanguage.Polish)
			{
				SmartLocalization.LanguageManager.Instance.ChangeLanguage("pl");
			}
			else if (systemLanguage == UnityEngine.SystemLanguage.Portuguese)
			{
				SmartLocalization.LanguageManager.Instance.ChangeLanguage("pt");
			}
			else if (systemLanguage != UnityEngine.SystemLanguage.Russian)
			{
				SmartLocalization.LanguageManager.Instance.ChangeLanguage("en");
			}
			else
			{
				SmartLocalization.LanguageManager.Instance.ChangeLanguage("ru");
			}
		}
		else
		{
			SmartLocalization.LanguageManager.Instance.ChangeLanguage("ko");
		}
	}

}
