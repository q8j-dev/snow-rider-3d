using System.Collections.Generic;
using UnityEngine;

namespace SmartLocalization
{

internal class LanguageDataHandler
{
	private SortedDictionary<string, LocalizedObject> loadedValuesDictionary;

	private bool verboseLogging;

	private SmartCultureInfo loadedCultureInfo;

	private ILocalizedAssetLoader assetLoader;

	internal SmartCultureInfo LoadedCulture
	{
		get
		{
			return loadedCultureInfo;
		}
		set
		{
			loadedCultureInfo = value;
		}
	}

	internal bool VerboseLogging
	{
		get
		{
			return verboseLogging;
		}
		set
		{
			verboseLogging = value;
		}
	}

	internal SortedDictionary<string, LocalizedObject> LoadedValuesDictionary
	{
		get
		{
			return loadedValuesDictionary;
		}
		set
		{
			loadedValuesDictionary = value;
		}
	}

	internal ILocalizedAssetLoader AssetLoader
	{
		get
		{
			if (assetLoader == null)
			{
				assetLoader = new RuntimeLocalizedAssetLoader();
			}
			return assetLoader;
		}
	}

	internal LanguageDataHandler()
	{
		loadedValuesDictionary = new SortedDictionary<string, LocalizedObject>();
	}

	internal bool Load(string resxData)
	{
		SortedDictionary<string, LocalizedObject> sortedDictionary = LoadLanguageDictionary(resxData);
		if (sortedDictionary != null && sortedDictionary.Count > 0)
		{
			loadedValuesDictionary = sortedDictionary;
			if (verboseLogging)
			{
				Debug.Log((object)"Successfully loaded language");
			}
			return true;
		}
		return false;
	}

	internal bool Append(string resxData)
	{
		SortedDictionary<string, LocalizedObject> sortedDictionary = LoadLanguageDictionary(resxData);
		if (sortedDictionary != null && sortedDictionary.Count > 0)
		{
			foreach (KeyValuePair<string, LocalizedObject> item in sortedDictionary)
			{
				if (loadedValuesDictionary.ContainsKey(item.Key))
				{
					loadedValuesDictionary[item.Key] = item.Value;
				}
				else
				{
					loadedValuesDictionary.Add(item.Key, item.Value);
				}
			}
			if (verboseLogging)
			{
				Debug.Log((object)("Successfully appended language with " + sortedDictionary.Count + " values"));
			}
			return true;
		}
		return false;
	}

	internal List<string> GetKeysWithinCategory(string category)
	{
		if (string.IsNullOrEmpty(category) || loadedValuesDictionary == null)
		{
			return new List<string>();
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, LocalizedObject> item in loadedValuesDictionary)
		{
			if (item.Key.StartsWith(category))
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	internal List<string> GetAllKeys()
	{
		return new List<string>(loadedValuesDictionary.Keys);
	}

	internal LocalizedObject GetLocalizedObject(string key)
	{
		loadedValuesDictionary.TryGetValue(key, out var value);
		return value;
	}

	internal string GetTextValue(string key)
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return localizedObject.TextValue;
		}
		if (VerboseLogging)
		{
			Debug.LogError((object)("LanguageManager.cs: Invalid Key:" + key + ". Could not get language value."));
		}
		return null;
	}

	internal T GetAsset<T>(string key) where T : Object
	{
		LocalizedObject localizedObject = GetLocalizedObject(key);
		if (localizedObject != null)
		{
			return AssetLoader.LoadAsset<T>(key, CheckLanguageOverrideCode(localizedObject));
		}
		if (VerboseLogging)
		{
			Debug.LogError((object)("Could not get asset with key: " + key + " as asset type:" + typeof(T).ToString()));
		}
		return null;
	}

	internal bool HasKey(string key)
	{
		return GetLocalizedObject(key) != null;
	}

	private SortedDictionary<string, LocalizedObject> LoadLanguageDictionary(string resxData)
	{
		if (string.IsNullOrEmpty(resxData))
		{
			return null;
		}
		return LanguageParser.LoadLanguage(resxData);
	}

	
	
	
	
	private string CheckLanguageOverrideCode(LocalizedObject localizedObject)
	{
		if (localizedObject == null)
		{
			return loadedCultureInfo.languageCode;
		}
		string text = ((!localizedObject.OverrideLocalizedObject) ? loadedCultureInfo.languageCode : localizedObject.OverrideObjectLanguageCode);
		if (string.IsNullOrEmpty(text))
		{
			text = loadedCultureInfo.languageCode;
		}
		return text;
	}
}

}

