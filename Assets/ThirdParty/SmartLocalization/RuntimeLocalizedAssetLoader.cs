using System;
using UnityEngine;

namespace SmartLocalization
{

internal class RuntimeLocalizedAssetLoader : ILocalizedAssetLoader
{
	private static readonly Type GameObjectType = typeof(GameObject);

	private static readonly Type AudioClipType = typeof(AudioClip);

	private static readonly Type TextureType = typeof(Texture);

	public T LoadAsset<T>(string assetKey, string languageCode) where T : UnityEngine.Object
	{
		return Resources.Load(GetAssetFolderPath(typeof(T), languageCode) + "/" + assetKey) as T;
	}

	private string GetAssetFolderPath(Type assetType, string languageCode)
	{
		if ((object)assetType == GameObjectType)
		{
			return LanguageRuntimeData.PrefabsFolderPath(languageCode);
		}
		if ((object)assetType == AudioClipType)
		{
			return LanguageRuntimeData.AudioFilesFolderPath(languageCode);
		}
		if ((object)assetType == TextureType)
		{
			return LanguageRuntimeData.TexturesFolderPath(languageCode);
		}
		return string.Empty;
	}
}

}

