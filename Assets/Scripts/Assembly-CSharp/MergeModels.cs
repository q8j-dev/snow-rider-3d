
public class MergeModels : UnityEngine.MonoBehaviour
{
	public void Start()
	{
		UnityEngine.Vector3 position = transform.position;
		transform.position = UnityEngine.Vector3.zero;
		UnityEngine.MeshFilter[] meshFilters = GetComponentsInChildren<UnityEngine.MeshFilter>();
		UnityEngine.CombineInstance[] combine = new UnityEngine.CombineInstance[meshFilters.Length];
		for (int i = 0; i < meshFilters.Length; i++)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.SetActive(false);
		}
		transform.GetComponent<UnityEngine.MeshFilter>().mesh = new UnityEngine.Mesh();
		transform.GetComponent<UnityEngine.MeshFilter>().mesh.CombineMeshes(combine, true, true);
		transform.gameObject.SetActive(true);
		transform.position = position;
	}

}
