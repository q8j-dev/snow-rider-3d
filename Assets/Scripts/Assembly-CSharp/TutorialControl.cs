public class TutorialControl : UnityEngine.MonoBehaviour
{
	[System.NonSerialized]
	public bool enable;

	public void OnEnable()
	{
		StartCoroutine(EnableRoutine());
	}

	public void Update()
	{
		if (GameInput.PrimaryPressedThisFrame && enable)
		{
			GUIControl.instance.ChangeCanvas(GUIControl.instance.playCanvasPrefab);
			SlowMotion.instance.Apply(1f, 1f);
		}
	}
	public System.Collections.IEnumerator EnableRoutine()
	{
		yield return new UnityEngine.WaitForSecondsRealtime(3f);
		enable = true;
	}

}
