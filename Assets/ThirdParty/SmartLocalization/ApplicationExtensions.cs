using UnityEngine;

namespace SmartLocalization
{

internal static class ApplicationExtensions
{
	internal static string GetSystemLanguage()
	{
		return GetStringValueOfSystemLanguage(Application.systemLanguage);
	}

	internal static string GetStringValueOfSystemLanguage(SystemLanguage systemLanguage)
	{
		switch ((int)systemLanguage)
		{
			case 0: return "Afrikaans";
			case 1: return "Arabic";
			case 2: return "Basque";
			case 3: return "Belarusian";
			case 4: return "Bulgarian";
			case 5: return "Catalan";
			case 6: return "Chinese";
			case 7: return "Czech";
			case 8: return "Danish";
			case 9: return "Dutch";
			case 10: return "English";
			case 11: return "Estonian";
			case 12: return "Faroese";
			case 13: return "Finnish";
			case 14: return "French";
			case 15: return "German";
			case 16: return "Greek";
			case 17: return "Hebrew";
			case 18: return "Hungarian";
			case 19: return "Icelandic";
			case 20: return "Indonesian";
			case 21: return "Italian";
			case 22: return "Japanese";
			case 23: return "Korean";
			case 24: return "Latvian";
			case 25: return "Lithuanian";
			case 26: return "Norwegian";
			case 27: return "Polish";
			case 28: return "Portuguese";
			case 29: return "Romanian";
			case 30: return "Russian";
			case 31: return "SerboCroatian";
			case 32: return "Slovak";
			case 33: return "Slovenian";
			case 34: return "Spanish";
			case 35: return "Swedish";
			case 36: return "Thai";
			case 37: return "Turkish";
			case 38: return "Ukrainian";
			case 39: return "Vietnamese";
			case 42: return "Unknown";
			default: return "Unknown";
		}
	}
}

}
