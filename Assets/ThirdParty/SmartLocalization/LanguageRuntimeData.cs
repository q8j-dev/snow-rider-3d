namespace SmartLocalization
{




public static class LanguageRuntimeData
{
	private static string AvailableCulturesFileName = "AvailableCultures";

	private static string rootLanguageName = "Language";

	private static string AudioFileFolderName = "AudioFiles";

	private static string TexturesFolderName = "Textures";

	private static string PrefabsFolderName = "Prefabs";

	
	
	
	
	
	public static string LanguageFilePath(string languageCode)
	{
		return rootLanguageName + "." + languageCode;
	}

	
	
	
	
	public static string AvailableCulturesFilePath()
	{
		return AvailableCulturesFileName;
	}

	
	
	
	public static string AudioFilesFolderPath(string languageCode)
	{
		return languageCode + "/" + AudioFileFolderName;
	}

	
	
	
	public static string TexturesFolderPath(string languageCode)
	{
		return languageCode + "/" + TexturesFolderName;
	}

	public static string PrefabsFolderPath(string languageCode)
	{
		return languageCode + "/" + PrefabsFolderName;
	}
}

}

