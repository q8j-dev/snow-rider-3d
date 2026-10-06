namespace SmartLocalization
{
	public class LoadAllLanguages : UnityEngine.MonoBehaviour
	{
		[System.NonSerialized]
		public System.Collections.Generic.List<string> currentLanguageKeys;

		[System.NonSerialized]
		public System.Collections.Generic.List<SmartLocalization.SmartCultureInfo> availableLanguages;

		[System.NonSerialized]
		public SmartLocalization.LanguageManager languageManager;

		[System.NonSerialized]
		public UnityEngine.Vector2 valuesScrollPosition;

		[System.NonSerialized]
		public UnityEngine.Vector2 languagesScrollPosition;

		public void Start()
		{
			languageManager = SmartLocalization.LanguageManager.Instance;
			SmartLocalization.SmartCultureInfo deviceCulture = languageManager.GetDeviceCultureIfSupported();
			if (deviceCulture == null)
			{
				UnityEngine.Debug.Log("The device language is not available in the current application. Loading default.");
			}
			else
			{
				languageManager.ChangeLanguage(deviceCulture);
			}
			if (languageManager.NumberOfSupportedLanguages < 1)
			{
				UnityEngine.Debug.LogError("No languages are created!, Open the Smart Localization plugin at Window->Smart Localization and create your language!");
			}
			else
			{
				currentLanguageKeys = languageManager.GetAllKeys();
				availableLanguages = languageManager.GetSupportedLanguages();
			}
			SmartLocalization.LanguageManager.Instance.OnChangeLanguage += OnLanguageChanged;
		}

		public void OnDestroy()
		{
			if (SmartLocalization.LanguageManager.HasInstance)
			{
				SmartLocalization.LanguageManager.Instance.OnChangeLanguage -= OnLanguageChanged;
			}
		}

		public void OnLanguageChanged(SmartLocalization.LanguageManager languageManager)
		{
			currentLanguageKeys = languageManager.GetAllKeys();
		}

		public void OnGUI()
		{
			if (languageManager.NumberOfSupportedLanguages > 0)
			{
				if (languageManager.CurrentlyLoadedCulture.nativeName != null)
				{
					UnityEngine.GUILayout.Label("Current Language:" + languageManager.CurrentlyLoadedCulture.nativeName);
				}
				UnityEngine.GUILayout.BeginHorizontal();
				UnityEngine.GUILayout.Label("Keys:", UnityEngine.GUILayout.Width(460f));
				UnityEngine.GUILayout.Label("Values:", UnityEngine.GUILayout.Width(460f));
				UnityEngine.GUILayout.EndHorizontal();
				valuesScrollPosition = UnityEngine.GUILayout.BeginScrollView(valuesScrollPosition);
				foreach (string key in currentLanguageKeys)
				{
					UnityEngine.GUILayout.BeginHorizontal();
					UnityEngine.GUILayout.Label(key, UnityEngine.GUILayout.Width(460f));
					UnityEngine.GUILayout.Label(languageManager.GetTextValue(key), UnityEngine.GUILayout.Width(460f));
					UnityEngine.GUILayout.EndHorizontal();
				}
				UnityEngine.GUILayout.EndScrollView();
				languagesScrollPosition = UnityEngine.GUILayout.BeginScrollView(languagesScrollPosition);
				foreach (SmartLocalization.SmartCultureInfo language in availableLanguages)
				{
					if (UnityEngine.GUILayout.Button(language.nativeName, UnityEngine.GUILayout.Width(960f)))
					{
						languageManager.ChangeLanguage(language);
					}
				}
				UnityEngine.GUILayout.EndScrollView();
			}
		}

		public LoadAllLanguages()
		{
			valuesScrollPosition = UnityEngine.Vector2.zero;
			languagesScrollPosition = UnityEngine.Vector2.zero;
		}
	}
}
