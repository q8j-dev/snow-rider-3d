using System;
using UnityEngine;

namespace SmartLocalization
{




[Serializable]
public class LocalizedObject
{
	public static readonly string keyTypeIdentifier = "[type=";

	private static readonly string endBracket = "]";

	
	[SerializeField]
	private LocalizedObjectType objectType;

	
	
	
	
	[SerializeField]
	private string textValue;

	
	
	
	[SerializeField]
	private GameObject thisGameObject;

	
	
	
	[SerializeField]
	private AudioClip thisAudioClip;

	
	
	
	[SerializeField]
	private Texture thisTexture;

	
	
	
	
	
	
	[SerializeField]
	private bool overrideLocalizedObject;

	
	
	
	[SerializeField]
	private string overrideObjectLanguageCode;

	
	public LocalizedObjectType ObjectType
	{
		get
		{
			return objectType;
		}
		set
		{
			objectType = value;
		}
	}

	
	
	
	
	
	
	
	public string TextValue
	{
		get
		{
			return textValue;
		}
		set
		{
			textValue = value;
		}
	}

	
	
	
	
	
	
	public GameObject ThisGameObject
	{
		set
		{
			thisGameObject = value;
		}
	}

	
	
	
	
	
	
	public AudioClip ThisAudioClip
	{
		set
		{
			thisAudioClip = value;
		}
	}

	
	
	
	
	public Texture ThisTexture
	{
		set
		{
			thisTexture = value;
		}
	}

	
	
	
	public bool OverrideLocalizedObject
	{
		get
		{
			return overrideLocalizedObject;
		}
		set
		{
			overrideLocalizedObject = value;
			if (!overrideLocalizedObject)
			{
				overrideObjectLanguageCode = null;
				return;
			}
			ThisAudioClip = null;
			ThisTexture = null;
			ThisGameObject = null;
			ThisTexture = null;
		}
	}

	
	
	
	public string OverrideObjectLanguageCode
	{
		get
		{
			return overrideObjectLanguageCode;
		}
		set
		{
			overrideObjectLanguageCode = value;
			if (overrideLocalizedObject)
			{
				textValue = "override=" + overrideObjectLanguageCode;
			}
		}
	}

	
	
	
	public LocalizedObject()
	{
	}

	
	
	
	
	
	
	
	
	
	public static LocalizedObjectType GetLocalizedObjectType(string key)
	{
		if (key.StartsWith(keyTypeIdentifier))
		{
			if (key.StartsWith(keyTypeIdentifier + "AUDIO" + endBracket))
			{
				return LocalizedObjectType.AUDIO;
			}
			if (key.StartsWith(keyTypeIdentifier + "GAME_OBJECT" + endBracket))
			{
				return LocalizedObjectType.GAME_OBJECT;
			}
			if (key.StartsWith(keyTypeIdentifier + "TEXTURE" + endBracket))
			{
				return LocalizedObjectType.TEXTURE;
			}
			Debug.LogError((object)("LocalizedObject.cs: ERROR IN SYNTAX of key:" + key + ", setting object type to STRING"));
			return LocalizedObjectType.STRING;
		}
		return LocalizedObjectType.STRING;
	}

	public static string GetCleanKey(string key, LocalizedObjectType objectType)
	{
		int length = (keyTypeIdentifier + GetLocalizedObjectTypeStringValue(objectType) + endBracket).Length;
		switch (objectType)
		{
		case LocalizedObjectType.STRING:
			return key;
		case LocalizedObjectType.GAME_OBJECT:
		case LocalizedObjectType.AUDIO:
		case LocalizedObjectType.TEXTURE:
			return key.Substring(length);
		default:
			Debug.LogError((object)("LocalizedObject.GetCleanKey(key) error!, object type is unknown! objectType:" + (int)objectType));
			return key;
		}
	}

	
	
	
	
	
	
	public static string GetLocalizedObjectTypeStringValue(LocalizedObjectType objectType)
	{
		switch (objectType)
		{
			case LocalizedObjectType.AUDIO: return "AUDIO";
			case LocalizedObjectType.GAME_OBJECT: return "GAME_OBJECT";
			case LocalizedObjectType.STRING: return "STRING";
			case LocalizedObjectType.TEXTURE: return "TEXTURE";
			default: return "STRING";
		}
	}
}

}
