using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SmartLocalization
{

public class LocalizedAudioSource : MonoBehaviour
{
	public string localizedKey = "INSERT_KEY_HERE";

	public AudioClip audioClip;

	private AudioSource audioSource;

	private void Start()
	{
		LanguageManager instance = LanguageManager.Instance;
		instance.OnChangeLanguage += OnChangeLanguage;
		audioSource = GetComponent<AudioSource>();
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
		audioClip = languageManager.GetAudioClip(localizedKey);
		if (audioSource != null)
		{
			audioSource.clip = audioClip;
		}
	}
}

}

