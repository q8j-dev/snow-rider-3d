using System;

namespace SmartLocalization
{




[Serializable]
public class SmartCultureInfo
{
	
	public string languageCode;

	
	public string englishName;

	
	public string nativeName;

	
	public bool isRightToLeft;

	
	public SmartCultureInfo()
	{
	}

	
	
	
	
	
	
	public SmartCultureInfo(string languageCode, string englishName, string nativeName, bool isRightToLeft)
	{
		this.languageCode = languageCode;
		this.englishName = englishName;
		this.nativeName = nativeName;
		this.isRightToLeft = isRightToLeft;
	}

	public override string ToString()
	{
		return string.Format("[SmartCultureInfo LanguageCode=\"{0}\" EnglishName={1} NativeName={2} IsRightToLeft={3}]", new object[4]
		{
			languageCode,
			englishName,
			nativeName,
			isRightToLeft.ToString()
		});
	}
}

}

