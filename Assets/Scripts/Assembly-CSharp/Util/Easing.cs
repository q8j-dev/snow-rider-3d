namespace Util
{
	public static class Easing
	{
		public static class Sine
		{
			public static float EaseIn(double s)
			{
				return (float)System.Math.Sin(s * MathHelper.HalfPi - MathHelper.HalfPi) + 1f;
			}

			public static float EaseOut(double s)
			{
				return (float)System.Math.Sin(s * MathHelper.HalfPi);
			}

			public static float EaseInOut(double s)
			{
				return (float)(System.Math.Sin(s * MathHelper.Pi - MathHelper.HalfPi) + 1.0) / 2f;
			}
		}

		public static class Power
		{
			public static float EaseIn(double s, int power)
			{
				return (float)System.Math.Pow(s, power);
			}

			public static float EaseOut(double s, int power)
			{
				int sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign * (System.Math.Pow(s - 1.0, power) + sign));
			}

			public static float EaseInOut(double s, int power)
			{
				s *= 2.0;
				if (s < 1.0)
				{
					return EaseIn(s, power) / 2f;
				}

				int sign = power % 2 == 0 ? -1 : 1;
				return (float)(sign / 2.0 * (System.Math.Pow(s - 2.0, power) + sign * 2));
			}
		}

		public static float Ease(double linearStep, float acceleration, Util.EasingType type)
		{
			float easedStep = acceleration > 0f
				? EaseIn(linearStep, type)
				: acceleration < 0f
					? EaseOut(linearStep, type)
					: (float)linearStep;
			return MathHelper.Lerp(linearStep, easedStep, System.Math.Abs(acceleration));
		}

		public static float EaseIn(double linearStep, Util.EasingType type)
		{
			switch (type)
			{
				case EasingType.Step: return linearStep < 0.5 ? 0f : 1f;
				case EasingType.Linear: return (float)linearStep;
				case EasingType.Sine: return Sine.EaseIn(linearStep);
				case EasingType.Quadratic: return Power.EaseIn(linearStep, 2);
				case EasingType.Cubic: return Power.EaseIn(linearStep, 3);
				case EasingType.Quartic: return Power.EaseIn(linearStep, 4);
				case EasingType.Quintic: return Power.EaseIn(linearStep, 5);
				default: throw new System.NotImplementedException();
			}
		}

		public static float EaseOut(double linearStep, Util.EasingType type)
		{
			switch (type)
			{
				case EasingType.Step: return linearStep < 0.5 ? 0f : 1f;
				case EasingType.Linear: return (float)linearStep;
				case EasingType.Sine: return Sine.EaseOut(linearStep);
				case EasingType.Quadratic: return Power.EaseOut(linearStep, 2);
				case EasingType.Cubic: return Power.EaseOut(linearStep, 3);
				case EasingType.Quartic: return Power.EaseOut(linearStep, 4);
				case EasingType.Quintic: return Power.EaseOut(linearStep, 5);
				default: throw new System.NotImplementedException();
			}
		}

		public static float EaseInOut(double linearStep, Util.EasingType easeInType, Util.EasingType easeOutType)
		{
			return linearStep < 0.5
				? EaseInOut(linearStep, easeInType)
				: EaseInOut(linearStep, easeOutType);
		}

		public static float EaseInOut(double linearStep, Util.EasingType type)
		{
			switch (type)
			{
				case EasingType.Step: return linearStep < 0.5 ? 0f : 1f;
				case EasingType.Linear: return (float)linearStep;
				case EasingType.Sine: return Sine.EaseInOut(linearStep);
				case EasingType.Quadratic: return Power.EaseInOut(linearStep, 2);
				case EasingType.Cubic: return Power.EaseInOut(linearStep, 3);
				case EasingType.Quartic: return Power.EaseInOut(linearStep, 4);
				case EasingType.Quintic: return Power.EaseInOut(linearStep, 5);
				default: throw new System.NotImplementedException();
			}
		}
	}
}
