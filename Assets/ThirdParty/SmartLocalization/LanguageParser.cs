using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace SmartLocalization
{




public static class LanguageParser
{
	private static string xmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<root>";

	
	
	
	public static SortedDictionary<string, LocalizedObject> LoadLanguage(string languageDataInResX)
	{
		if (languageDataInResX == null || languageDataInResX == string.Empty)
		{
			Debug.LogError((object)"Cannot load language file - languageDataInResX is null!");
			return null;
		}
		SortedDictionary<string, LocalizedObject> sortedDictionary = new SortedDictionary<string, LocalizedObject>();
		int num = languageDataInResX.IndexOf("</xsd:schema>");
		num += 13;
		string text = languageDataInResX.Substring(num);
		text = xmlHeader + text;
		using (StringReader stringReader = new StringReader(text))
		using (XmlReader reader = XmlReader.Create(stringReader))
		{
			ReadElements(reader, sortedDictionary);
		}
		return sortedDictionary;
	}

	private static void ReadElements(XmlReader reader, SortedDictionary<string, LocalizedObject> loadedLanguageDictionary)
	{
		while (reader.Read())
		{
			if (reader.NodeType == XmlNodeType.Element && reader.Name == "data")
			{
				ReadData(reader, loadedLanguageDictionary);
			}
		}
	}

	private static void ReadData(XmlReader reader, SortedDictionary<string, LocalizedObject> loadedLanguageDictionary)
	{
		string key = string.Empty;
		string textValue = string.Empty;
		if (reader.HasAttributes)
		{
			while (reader.MoveToNextAttribute())
			{
				if (reader.Name == "name")
				{
					key = reader.Value;
				}
			}
		}
		reader.MoveToElement();
		if (reader.ReadToDescendant("value"))
		{
			do
			{
				textValue = reader.ReadElementContentAsString();
			}
			while (reader.ReadToNextSibling("value"));
		}
		LocalizedObject localizedObject = new LocalizedObject();
		localizedObject.ObjectType = LocalizedObject.GetLocalizedObjectType(key);
		localizedObject.TextValue = textValue;
		if (localizedObject.ObjectType != LocalizedObjectType.STRING && localizedObject.TextValue != null && localizedObject.TextValue.StartsWith("override="))
		{
			localizedObject.OverrideLocalizedObject = true;
			localizedObject.OverrideObjectLanguageCode = localizedObject.TextValue.Substring("override=".Length);
		}
		loadedLanguageDictionary.Add(LocalizedObject.GetCleanKey(key, localizedObject.ObjectType), localizedObject);
	}
}

}
