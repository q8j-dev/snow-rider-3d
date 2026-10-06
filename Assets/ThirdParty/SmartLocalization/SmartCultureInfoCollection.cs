using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace SmartLocalization
{




[Serializable]
[XmlRoot("SmartCultureCollections")]
public class SmartCultureInfoCollection
{
	public const int LatestVersion = 4;

	[XmlElement(ElementName = "version")]
	public int version;

	[XmlArray("CultureInfos")]
	[XmlArrayItem("CultureInfo")]
	public List<SmartCultureInfo> cultureInfos = new List<SmartCultureInfo>();

	
	
	
	
	public void AddCultureInfo(SmartCultureInfo cultureInfo)
	{
		if (cultureInfo == null)
		{
			Debug.LogError((object)"Cannot add a SmartCultureInfo that's null!");
		}
		else
		{
			cultureInfos.Add(cultureInfo);
		}
	}

	
	
	
	
	
	public static SmartCultureInfoCollection Deserialize(TextAsset xmlFile)
	{
		return SmartCultureInfoCollectionDeserializer.Deserialize(xmlFile);
	}

}

}

