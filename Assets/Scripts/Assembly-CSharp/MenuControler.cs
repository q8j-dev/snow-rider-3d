public class MenuControler : UnityEngine.MonoBehaviour
{
	public BackgroundRoll back;

	[System.NonSerialized]
	public bool routineActive;

	public UnityEngine.GameObject Controls;

	public static int ControlsShowCount;

	public static bool hasTutAppeared;

	public void MenuShow()
	{
		StartCoroutine(MenuShowRoutine());
	}
	public System.Collections.IEnumerator MenuShowRoutine()
	{
		BackgroundRoll background = GetComponentInChildren<BackgroundRoll>();
		if (background != null)
		{
			back = GetComponentInChildren<BackgroundRoll>();
			yield return new UnityEngine.WaitForSecondsRealtime(back.delay);
			yield return StartCoroutine(back.BackgroundShowRoutine());
		}
		foreach (MenuBlock menuBlock in transform.GetComponentsInChildren<MenuBlock>())
		{
			if (menuBlock.gameObject.activeInHierarchy)
			{
				menuBlock.gameObject.SetActive(false);
				menuBlock.gameObject.SetActive(true);
				yield return StartCoroutine(menuBlock.ShowElementsRoutine());
			}
		}
		if (name.Contains("Tutorial Canvas"))
		{
			hasTutAppeared = true;
		}
		if (name.Contains("Play Canvas"))
		{
			UnityEngine.Debug.Log("GameControl.completedTutorial " + GameControl.completedTutorial);
			if (GameControl.completedTutorial == 1)
			{
				ControlsShowCount++;
				if (ControlsShowCount < 3)
				{
					if (!hasTutAppeared)
					{
						UnityEngine.Debug.Log("3.3");
						Invoke("DisableControls", 3.3f);
					}
					else
					{
						UnityEngine.Debug.Log("2.0");
						Invoke("DisableControls", 1f);
					}
				}
				else
				{
					DisableControls();
				}
			}
		}
	}

	public void DisableControls()
	{
		if (GameControl.completedTutorial == 1)
		{
			hasTutAppeared = false;
			if (name.Contains("Play Canvas"))
			{
				Controls.SetActive(false);
			}
		}
	}

	public void MenuHide()
	{
		StartCoroutine(MenuHideRoutine());
	}
	public System.Collections.IEnumerator MenuHideRoutine()
	{
		foreach (MenuBlock menuBlock in transform.GetComponentsInChildren<MenuBlock>())
		{
			if (menuBlock.gameObject.activeInHierarchy)
			{
				yield return StartCoroutine(menuBlock.HideElementsRoutine());
			}
		}
		if (back != null)
		{
			yield return StartCoroutine(back.BackgroundHideRoutine());
		}
	}

}
