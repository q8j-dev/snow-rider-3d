public class TextScore : UnityEngine.MonoBehaviour
{
	public void OnEnable()
	{
		GetComponent<UnityEngine.UI.Text>().text = GameControl.score.ToString();
	}

}
