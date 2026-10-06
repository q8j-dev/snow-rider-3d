using UnityEngine;

namespace SmartLocalization
{

internal interface ILocalizedAssetLoader
{
	T LoadAsset<T>(string assetKey, string languageCode) where T : Object;
}

}

