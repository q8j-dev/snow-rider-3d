public class SpriteChange : UnityEngine.MonoBehaviour
{
	[UnityEngine.SerializeField]
	public float interval;

	[UnityEngine.SerializeField]
	public UnityEngine.Sprite spr1;

	[UnityEngine.SerializeField]
	public UnityEngine.Sprite spr2;

	[UnityEngine.SerializeField]
	public UnityEngine.UI.Image image1;

	[UnityEngine.SerializeField]
	public UnityEngine.UI.Image image2;

	[System.NonSerialized]
	public bool isFirst;

	public void OnStart()
	{
		StartCoroutine(ChangeRoutine());
	}

	public void Change()
	{
		isFirst = !isFirst;
		UnityEngine.Sprite sprite = isFirst ? spr1 : spr2;
		image2.sprite = sprite;
		image1.sprite = sprite;
	}
	public System.Collections.IEnumerator ChangeRoutine()
	{
		yield return new UnityEngine.WaitForEndOfFrame();
		while (true)
		{
			yield return new UnityEngine.WaitForSecondsRealtime(interval);
			Change();
		}
	}

}
