public static class FrameRateControl
{
	public const float ReferenceFrameRate = 60f;

	public const float ReferenceFrameDuration = 1f / ReferenceFrameRate;

	public static float Blend(float referenceFrameAmount)
	{
		return Blend(referenceFrameAmount, UnityEngine.Time.unscaledDeltaTime);
	}

	public static float Blend(float referenceFrameAmount, float elapsedRealTime)
	{
		float amount = UnityEngine.Mathf.Clamp01(referenceFrameAmount);
		if (amount <= 0f || elapsedRealTime <= 0f)
		{
			return 0f;
		}
		if (amount >= 1f)
		{
			return 1f;
		}
		return 1f - UnityEngine.Mathf.Pow(1f - amount, elapsedRealTime * ReferenceFrameRate);
	}

	public static float ReferenceFrameScale
	{
		get
		{
			return UnityEngine.Time.unscaledDeltaTime * ReferenceFrameRate;
		}
	}
}
