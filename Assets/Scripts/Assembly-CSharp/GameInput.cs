public static class GameInput
{
	public static bool LeftSteerHeld
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed);
		}
	}

	public static bool RightSteerHeld
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed);
		}
	}

	public static bool JumpHeld
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && (keyboard.spaceKey.isPressed || keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed);
		}
	}

	public static bool EscapePressedThisFrame
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && keyboard.escapeKey.wasPressedThisFrame;
		}
	}

	public static bool QPressedThisFrame
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && keyboard.qKey.wasPressedThisFrame;
		}
	}

	public static bool EPressedThisFrame
	{
		get
		{
			UnityEngine.InputSystem.Keyboard keyboard = UnityEngine.InputSystem.Keyboard.current;
			return keyboard != null && keyboard.eKey.wasPressedThisFrame;
		}
	}

	public static bool PrimaryPressedThisFrame
	{
		get
		{
			UnityEngine.InputSystem.Mouse mouse = UnityEngine.InputSystem.Mouse.current;
			UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
			return (mouse != null && mouse.leftButton.wasPressedThisFrame) ||
				(touchscreen != null && touchscreen.primaryTouch.press.wasPressedThisFrame);
		}
	}

	public static bool PrimaryReleasedThisFrame
	{
		get
		{
			UnityEngine.InputSystem.Mouse mouse = UnityEngine.InputSystem.Mouse.current;
			UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
			return (mouse != null && mouse.leftButton.wasReleasedThisFrame) ||
				(touchscreen != null && touchscreen.primaryTouch.press.wasReleasedThisFrame);
		}
	}

	public static bool PrimaryHeld
	{
		get
		{
			UnityEngine.InputSystem.Mouse mouse = UnityEngine.InputSystem.Mouse.current;
			UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
			return (mouse != null && mouse.leftButton.isPressed) ||
				(touchscreen != null && touchscreen.primaryTouch.press.isPressed);
		}
	}

	public static UnityEngine.Vector2 PointerPosition
	{
		get
		{
			UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
			if (touchscreen != null && touchscreen.primaryTouch.press.isPressed)
			{
				return touchscreen.primaryTouch.position.ReadValue();
			}
			UnityEngine.InputSystem.Mouse mouse = UnityEngine.InputSystem.Mouse.current;
			return mouse != null ? mouse.position.ReadValue() : UnityEngine.Vector2.zero;
		}
	}

	public static int TouchCount
	{
		get
		{
			UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
			if (touchscreen == null)
			{
				return 0;
			}
			int count = 0;
			for (int i = 0; i < touchscreen.touches.Count; i++)
			{
				if (IsCurrentTouch(touchscreen.touches[i]))
				{
					count++;
				}
			}
			return count;
		}
	}

	public static bool TouchBegan(int index)
	{
		UnityEngine.InputSystem.Controls.TouchControl touch = GetTouch(index);
		return touch != null && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began;
	}

	public static bool TouchMoved(int index)
	{
		UnityEngine.InputSystem.Controls.TouchControl touch = GetTouch(index);
		return touch != null && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved;
	}

	public static bool TouchCanceledOrEnded(int index)
	{
		UnityEngine.InputSystem.Controls.TouchControl touch = GetTouch(index);
		if (touch == null)
		{
			return false;
		}
		UnityEngine.InputSystem.TouchPhase phase = touch.phase.ReadValue();
		return phase == UnityEngine.InputSystem.TouchPhase.Canceled || phase == UnityEngine.InputSystem.TouchPhase.Ended;
	}

	public static UnityEngine.Vector2 TouchPosition(int index)
	{
		UnityEngine.InputSystem.Controls.TouchControl touch = GetTouch(index);
		return touch != null ? touch.position.ReadValue() : UnityEngine.Vector2.zero;
	}

	private static UnityEngine.InputSystem.Controls.TouchControl GetTouch(int index)
	{
		UnityEngine.InputSystem.Touchscreen touchscreen = UnityEngine.InputSystem.Touchscreen.current;
		if (touchscreen == null || index < 0)
		{
			return null;
		}
		int activeIndex = 0;
		for (int i = 0; i < touchscreen.touches.Count; i++)
		{
			UnityEngine.InputSystem.Controls.TouchControl touch = touchscreen.touches[i];
			if (!IsCurrentTouch(touch))
			{
				continue;
			}
			if (activeIndex == index)
			{
				return touch;
			}
			activeIndex++;
		}
		return null;
	}

	private static bool IsCurrentTouch(UnityEngine.InputSystem.Controls.TouchControl touch)
	{
		UnityEngine.InputSystem.TouchPhase phase = touch.phase.ReadValue();
		return phase == UnityEngine.InputSystem.TouchPhase.Began ||
			phase == UnityEngine.InputSystem.TouchPhase.Moved ||
			phase == UnityEngine.InputSystem.TouchPhase.Stationary ||
			touch.press.wasReleasedThisFrame;
	}
}
