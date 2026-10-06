using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SmartLocalization
{




public class LanguageManager : MonoBehaviour, ISerializationCallbackReceiver
{
	private static LanguageManager instance;

	private static bool IsQuitting;

	private static bool DontDestroyOnLoadToggle;

	private static bool DidSetDontDestroyOnLoad;

	[SerializeField]
	[HideInInspector]
	private List<string> serializedKeys;

	[SerializeField]
	[HideInInspector]
	private List<LocalizedObject> serializedValues;

	[SerializeField]
	[HideInInspector]
	private SmartCultureInfo serializedCulture;

	
	
	
	
	public ChangeLanguageEventHandler OnChangeLanguage;

	public string defaultLanguage = "en";

	[SerializeField]
	private SmartCultureInfoCollection availableLanguages;

	private LanguageDataHandler languageDataHandler = new LanguageDataHandler();

	
	
	
	
	public static LanguageManager Instance
	{
		get
		{
			if ((Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor) && instance == null)
			{
				instance = Object.FindAnyObjectByType<LanguageManager>();
			}
			if (instance == null && !IsQuitting)
			{
				GameObject managerObject = new GameObject("LanguageManager");
				instance = managerObject.AddComponent<LanguageManager>();
				if (!DidSetDontDestroyOnLoad && DontDestroyOnLoadToggle)
				{
					Object.DontDestroyOnLoad(instance);
					DidSetDontDestroyOnLoad = true;
				}
			}
			return instance;
		}
	}

	
	
	
	public static bool HasInstance => instance != null;

	[Obsolete("This will be removed in the future. Use LanguageManager.GetTextValue and LanguageManager.GetAllKeys")]
	public SortedDictionary<string, LocalizedObject> LanguageDatabase => languageDataHandler.LoadedValuesDictionary;

	
	[Obsolete("This will be removed in the future. Use LanguageManager.GetTextValue and LanguageManager.GetAllKeys")]
	public Dictionary<string, string> RawTextDatabase
	{
		get
		{
			if (languageDataHandler.LoadedValuesDictionary == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, LocalizedObject> item in languageDataHandler.LoadedValuesDictionary)
			{
				dictionary.Add(item.Key, item.Value.TextValue);
			}
			return dictionary;
		}
	}

	
	
	
	public int NumberOfSupportedLanguages
	{
		get
		{
			if (availableLanguages == null)
			{
				return 0;
			}
			return availableLanguages.cultureInfos.Count;
		}
	}

	[Obsolete("Use CurrentlyLoadedCulture")]
	public string LoadedLanguage => CurrentlyLoadedCulture.languageCode;

	
	
	
	
	public SmartCultureInfo CurrentlyLoadedCulture => languageDataHandler.LoadedCulture;

	
	
	
	public bool VerboseLogging
	{
		get
		{
			return languageDataHandler.VerboseLogging;
		}
		set
		{
			languageDataHandler.VerboseLogging = value;
		}
	}

	
	
	
	public static void SetDontDestroyOnLoad()
	{
		DontDestroyOnLoadToggle = true;
		if (instance != null && !DidSetDontDestroyOnLoad)
		{
			Object.DontDestroyOnLoad(instance);
			DidSetDontDestroyOnLoad = true;
		}
	}

	public void OnAfterDeserialize()
	{
		if (serializedKeys != null)
		{
			languageDataHandler.LoadedValuesDictionary = new SortedDictionary<string, LocalizedObject>();
			for (int i = 0; i < serializedKeys.Count; i++)
			{
				languageDataHandler.LoadedValuesDictionary.Add(serializedKeys[i], serializedValues[i]);
			}
			languageDataHandler.LoadedCulture = serializedCulture;
			serializedKeys.Clear();
			serializedValues.Clear();
			serializedCulture = null;
		}
	}

	public void OnBeforeSerialize()
	{
		if (serializedKeys == null)
		{
			serializedKeys = new List<string>();
		}
		if (serializedValues == null)
		{
			serializedValues = new List<LocalizedObject>();
		}
		serializedKeys.Clear();
		serializedValues.Clear();
		if (languageDataHandler.LoadedValuesDictionary == null)
		{
			return;
		}
		foreach (KeyValuePair<string, LocalizedObject> item in languageDataHandler.LoadedValuesDictionary)
		{
			serializedKeys.Add(item.Key);
			serializedValues.Add(item.Value);
		}
		serializedCulture = CurrentlyLoadedCulture;
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			if (VerboseLogging)
			{
				Debug.LogError((object)"Found duplicate LanguageManagers! Removing one of them");
			}
			Object.Destroy(this);
			return;
		}
		if (LoadAvailableCultures())
		{
			if (VerboseLogging)
			{
				Debug.Log((object)"LanguageManager.cs: Waking up");
			}
			if (availableLanguages.cultureInfos.Count > 0)
			{
				SmartCultureInfo smartCultureInfo = availableLanguages.cultureInfos.Find((SmartCultureInfo info) => info.languageCode == defaultLanguage);
				if (smartCultureInfo != null)
				{
					ChangeLanguage(smartCultureInfo);
					return;
				}
				ChangeLanguage(availableLanguages.cultureInfos[0]);
				defaultLanguage = availableLanguages.cultureInfos[0].languageCode;
			}
			else
			{
				Debug.LogError((object)"LanguageManager.cs: No language is available! Use Window->Smart Localization tool to create a language");
			}
		}
		else
		{
			Debug.LogError((object)"LanguageManager.cs: No localization workspace is created! Use Window->Smart Localization tool to create one");
		}
	}

	private void OnDestroy()
	{
		OnChangeLanguage = null;
	}

	private void OnApplicationQuit()
	{
		IsQuitting = true;
	}

	private bool LoadAvailableCultures()
	{
		TextAsset languageList = Resources.Load<TextAsset>(LanguageRuntimeData.AvailableCulturesFilePath());
		if (languageList == null)
		{
			Debug.LogError((object)"Could not load available languages! No such file!");
			return false;
		}
		availableLanguages = SmartCultureInfoCollection.Deserialize(languageList);
		return true;
	}

	public List<string> GetAllKeys()
	{
		return languageDataHandler.GetAllKeys();
	}

	
	
	
	
	public void ChangeLanguage(SmartCultureInfo cultureInfo)
	{
		ChangeLanguage(cultureInfo.languageCode);
	}

	
	
	
	
	public void ChangeLanguage(string languageCode)
	{
		TextAsset languageFile = Resources.Load<TextAsset>(LanguageRuntimeData.LanguageFilePath(languageCode));
		if (languageFile == null)
		{
			Debug.LogError((object)("Failed to load language: " + languageCode));
			return;
		}
		LoadLanguage(languageFile.text, languageCode);
		if (OnChangeLanguage != null)
		{
			OnChangeLanguage(this);
		}
	}

	
	
	
	
	
	
	public void ChangeLanguageWithData(string languageDataInResX, string languageCode)
	{
		if (LoadLanguage(languageDataInResX, languageCode) && OnChangeLanguage != null)
		{
			OnChangeLanguage(this);
		}
	}

	
	
	
	
	
	
	public bool AppendLanguageWithTextData(string languageDataInResX)
	{
		return languageDataHandler.Append(languageDataInResX);
	}

	private bool LoadLanguage(string languageData, string languageCode)
	{
		if (string.IsNullOrEmpty(languageData))
		{
			Debug.LogError((object)"Failed to load language with ISO-639 code. Data was null or empty");
			return false;
		}
		SmartCultureInfo cultureInfo = GetCultureInfo(languageCode);
		if (cultureInfo == null)
		{
			Debug.LogError((object)("Failed to load language with ISO-639 code: " + languageCode + ". Unable to find a corresponding SmartCultureInfo"));
			return false;
		}
		if (languageDataHandler.Load(languageData))
		{
			languageDataHandler.LoadedCulture = cultureInfo;
			return true;
		}
		return false;
	}

	[Obsolete("Use IsCultureSupported")]
	public bool IsLanguageSupported(string languageCode)
	{
		return IsCultureSupported(languageCode);
	}

	[Obsolete("Use IsCultureSupported")]
	public bool IsLanguageSupported(SmartCultureInfo cultureInfo)
	{
		return IsLanguageSupported(cultureInfo.languageCode);
	}

	[Obsolete("Use GetDeviceCultureIfSupported")]
	public SmartCultureInfo GetSupportedSystemLanguage()
	{
		return GetDeviceCultureIfSupported();
	}

	[Obsolete]
	public string GetSupportedSystemLanguageCode()
	{
		SmartCultureInfo deviceCultureIfSupported = GetDeviceCultureIfSupported();
		return (deviceCultureIfSupported == null) ? string.Empty : deviceCultureIfSupported.languageCode;
	}

	
	
	
	public SmartCultureInfo GetDeviceCultureIfSupported()
	{
		if (availableLanguages == null)
		{
			return null;
		}
		string englishName = GetSystemLanguageEnglishName();
		return availableLanguages.cultureInfos.Find((SmartCultureInfo info) => info.englishName.ToLower() == englishName.ToLower());
	}

	
	
	
	public bool IsCultureSupported(string languageCode)
	{
		if (availableLanguages == null)
		{
			Debug.LogError((object)"LanguageManager is not initialized properly!");
			return false;
		}
		return availableLanguages.cultureInfos.Find((SmartCultureInfo info) => info.languageCode == languageCode) != null;
	}

	
	
	
	
	
	public bool IsCultureSupported(SmartCultureInfo cultureInfo)
	{
		return IsCultureSupported(cultureInfo.languageCode);
	}

	
	
	
	
	
	public bool IsLanguageSupportedEnglishName(string englishName)
	{
		if (availableLanguages == null)
		{
			Debug.LogError((object)"LanguageManager is not initialized properly!");
			return false;
		}
		return availableLanguages.cultureInfos.Find((SmartCultureInfo info) => info.englishName.ToLower() == englishName.ToLower()) != null;
	}

	
	
	
	public SmartCultureInfo GetCultureInfo(string languageCode)
	{
		return availableLanguages.cultureInfos.Find((SmartCultureInfo info) => info.languageCode == languageCode);
	}

	
	
	
	
	public string GetSystemLanguageEnglishName()
	{
		return ApplicationExtensions.GetSystemLanguage();
	}

	
	public List<SmartCultureInfo> GetSupportedLanguages()
	{
		if (availableLanguages == null)
		{
			Debug.LogError((object)"LanguageManager is not initialized properly! Cannot find available languages!");
			return null;
		}
		return availableLanguages.cultureInfos;
	}

	
	
	
	
	
	
	public List<string> GetKeysWithinCategory(string category)
	{
		return languageDataHandler.GetKeysWithinCategory(category);
	}

	
	
	
	public string GetTextValue(string key)
	{
		return languageDataHandler.GetTextValue(key);
	}

	
	
	
	public AudioClip GetAudioClip(string key)
	{
		return languageDataHandler.GetAsset<AudioClip>(key);
	}

	
	
	
	public GameObject GetPrefab(string key)
	{
		return languageDataHandler.GetAsset<GameObject>(key);
	}

	
	
	
	public Texture GetTexture(string key)
	{
		return languageDataHandler.GetAsset<Texture>(key);
	}

	
	
	
	public bool HasKey(string key)
	{
		return languageDataHandler.HasKey(key);
	}

	
	
	
	private LocalizedObject GetLocalizedObject(string key)
	{
		return languageDataHandler.GetLocalizedObject(key);
	}
}

}
