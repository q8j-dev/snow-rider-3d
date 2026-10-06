#if UNITY_EDITOR







internal static class Unity6WebGLFramePacing
{
	[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Apply()
	{
		UnityEngine.QualitySettings.vSyncCount = 0;
		UnityEngine.Application.targetFrameRate = 60;
	}
}
#endif
