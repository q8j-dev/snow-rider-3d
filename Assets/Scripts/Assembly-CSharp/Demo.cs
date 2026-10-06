public class Demo : UnityEngine.MonoBehaviour
{
	public UnityEngine.Vector3[] vectorArray;

	public System.Collections.Generic.List<UnityEngine.Color> colorList;

	public UnityEngine.Texture2D[] textureArray;

	
	public int[] intArray;

	
	[UnityEngine.NotReorderable]
	public float[] classicFloatArray;

	[UnityEngine.NotReorderable]
	public UnityEngine.Texture2D[] classicTextureArray;

	[UnityEngine.NotReorderable]
	public UnityEngine.Vector3[] classicVector3Array;

	public Player[] playerArray;

	[UnityEngine.NotReorderable]
	public System.Collections.Generic.List<Player> playerList;

	public System.Collections.Generic.List<MagicSpell> customDrawers;

}
