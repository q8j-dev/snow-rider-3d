public class GGen : UnityEngine.MonoBehaviour
{
	private static readonly System.Collections.Generic.Dictionary<string, UnityEngine.Mesh> mirroredBoxMeshes = new System.Collections.Generic.Dictionary<string, UnityEngine.Mesh>();

	public int giftChunk;

	public UnityEngine.Material mat;

	public UnityEngine.Material shadowMat;

	public GenData genData;

	public RandData randData;

	[System.NonSerialized]
	public UnityEngine.GameObject camp;

	[System.NonSerialized]
	public BiomeData currentBiome;

	public int currentBiomeLength;

	public StructData currentStruct;

	public int structIndex;

	public int currentStructLength;

	public UnityEngine.Transform structParent;

	public System.Collections.Generic.List<UnityEngine.GameObject> structs;

	[System.NonSerialized]
	public int chunkIndex;

	public UnityEngine.Transform target;

	public float generateDistance;

	public System.Collections.Generic.List<ScriptableObj> currentChunksData;

	public static GGen instance;

	public bool moded;

	public void Awake()
	{
		giftChunk = UnityEngine.Random.Range(0, genData.structWidth - 1);
		GameControl.OnPlay.AddListener(OnPlay);
		GameControl.OnIntro.AddListener(OnIntro);
		GameControl.OnMain.AddListener(OnMain);
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public void OnMain()
	{
		DestroyLevel();
	}

	public void OnPlay()
	{
		GenerateStartLevel();
		target = PlayerControl.instance.transform;
	}

	public void OnIntro()
	{
		GenerateStartLevel();
		target = UnityEngine.GameObject.FindGameObjectWithTag("Cam").transform;
	}

	public void Update()
	{
		while ((GameControl.gameMode == GameMode.play || GameControl.gameMode == GameMode.intro) && target.position.z > generateDistance)
		{
			GameControl.OnGenChunk.Invoke();
			GenerateStruct();
		}
	}

	public void GenerateStartLevel()
	{
		DestroyLevel();
		chunkIndex = 0;
		structIndex = 0;
		generateDistance = 10f;
		currentBiome = genData.WarmupBiome;
		currentStruct = currentBiome.structs[0];
		currentBiomeLength = genData.warmupLength;
		for (int i = 0; i < genData.visibleStructCount; i++)
		{
			GenerateStruct();
		}
		generateDistance -= genData.visibleStructCount * genData.chunkSize.y;
	}

	public void DestroyLevel()
	{
		for (int i = 0; i < structs.Count; i++)
		{
			UnityEngine.Object.Destroy(structs[i]);
		}
		structs.Clear();
	}

	public void GenerateStruct()
	{
		ChooseBiome();
		UnityEngine.GameObject structure = new UnityEngine.GameObject("Structure");
		structParent = structure.transform;
		structParent.parent = transform;
		structs.Add(structParent.gameObject);
		chunkIndex = 0;
		ChooseStruct();
		currentChunksData.Clear();
		for (int i = 0; i < genData.structWidth; i++)
		{
			ChooseChunk(i);
		}
		if (currentStruct.mod != null)
		{
			currentStruct.mod.BeforeApply();
		}
		for (int i = 0; i < genData.structWidth; i++)
		{
			GenerateChunk(i >= genData.structWidth - 1, i);
			chunkIndex++;
		}
		if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
		{
			if (giftChunk < genData.structWidth - 1)
			{
				giftChunk++;
			}
			else if (UnityEngine.Random.Range(0f, 1f) > 0.75f)
			{
				giftChunk--;
			}
		}
		else if (giftChunk >= 1)
		{
			giftChunk--;
		}
		else if (UnityEngine.Random.Range(0f, 1f) > 0.75f)
		{
			giftChunk++;
		}
		structIndex++;
		generateDistance += genData.chunkSize.y;
		structParent.gameObject.SetActive(true);
		structParent.position = UnityEngine.Vector3.forward * (structIndex - 1) * genData.chunkSize.y;
		if (currentStruct.mod != null && currentStructLength < 1)
		{
			currentStruct.mod.AfterApply();
			currentStructLength = UnityEngine.Random.Range(currentStruct.minLenght, currentStruct.maxLength);
			currentBiomeLength += currentStructLength;
		}
	}

	public void DestroyLastStruct()
	{
		if (genData.visibleStructCount < structs.Count)
		{
			UnityEngine.Object.Destroy(structs[0]);
			structs.RemoveAt(0);
		}
	}

	public void GenerateChunk(bool isMirrored, int j)
	{
		UnityEngine.GameObject chunkObject = new UnityEngine.GameObject("Chunk " + chunkIndex);
		UnityEngine.Transform chunkTransform = chunkObject.transform;
		chunkTransform.parent = structParent;
		if (isMirrored)
		{
			chunkTransform.localPosition = UnityEngine.Vector3.right * chunkIndex * genData.chunkSize.x + UnityEngine.Vector3.right * 5f;
		}
		else
		{
			chunkTransform.localPosition = UnityEngine.Vector3.right * chunkIndex * genData.chunkSize.x;
		}

		foreach (ChunkObj chunkObj in currentChunksData[j].obj)
		{
			UnityEngine.GameObject pre = chunkObj.prefab;
			if (chunkIndex != giftChunk && pre.tag == "Gift")
			{
				continue;
			}
			if (pre.tag == "Gift" && structIndex < 5)
			{
				continue;
			}

			Obj randomization = randData.objects.Find(objRand => objRand.obj == pre);
			UnityEngine.Quaternion randomRotation = UnityEngine.Quaternion.identity;
			UnityEngine.Transform spawnedTransform = null;
			if (randomization.obj != null)
			{
				UnityEngine.Vector3 euler = new UnityEngine.Vector3(
					UnityEngine.Random.Range(randomization.minRotOffset.x, randomization.maxRotOffset.x),
					UnityEngine.Random.Range(randomization.minRotOffset.y, randomization.maxRotOffset.y),
					UnityEngine.Random.Range(randomization.minRotOffset.z, randomization.maxRotOffset.z));
				randomRotation = UnityEngine.Quaternion.Euler(euler);
				_ = UnityEngine.Random.Range(randomization.scaleOffset.x, randomization.scaleOffset.y);
				UnityEngine.GameObject selectedPrefab = randomization.prefab.Count < 1
					? randomization.obj
					: randomization.prefab[UnityEngine.Random.Range(0, randomization.prefab.Count)];
				spawnedTransform = UnityEngine.Object.Instantiate(selectedPrefab).transform;
			}
			if (spawnedTransform == null)
			{
				spawnedTransform = UnityEngine.Object.Instantiate(pre).transform;
			}
			spawnedTransform.SetParent(chunkTransform);
			spawnedTransform.localPosition = chunkObj.pos;
			spawnedTransform.localRotation = chunkObj.rot * randomRotation;
			spawnedTransform.localScale = chunkObj.scale * 1f;
		}
		if (isMirrored)
		{
			ReplaceMirroredBoxColliders(chunkTransform);
			chunkTransform.localScale = new UnityEngine.Vector3(-1f, 1f, 1f);
		}
	}

	private static void ReplaceMirroredBoxColliders(UnityEngine.Transform chunkTransform)
	{
		UnityEngine.BoxCollider[] boxes = chunkTransform.GetComponentsInChildren<UnityEngine.BoxCollider>(true);
		for (int i = 0; i < boxes.Length; i++)
		{
			UnityEngine.BoxCollider box = boxes[i];
			UnityEngine.MeshCollider replacement = box.gameObject.AddComponent<UnityEngine.MeshCollider>();
			replacement.sharedMesh = GetBoxMesh(box.center, box.size);
			replacement.convex = true;
			replacement.isTrigger = box.isTrigger;
			replacement.sharedMaterial = box.sharedMaterial;
			box.enabled = false;
			UnityEngine.Object.Destroy(box);
		}
	}

	private static UnityEngine.Mesh GetBoxMesh(UnityEngine.Vector3 center, UnityEngine.Vector3 size)
	{
		string key = center.x + ":" + center.y + ":" + center.z + ":" + size.x + ":" + size.y + ":" + size.z;
		if (mirroredBoxMeshes.TryGetValue(key, out UnityEngine.Mesh cached))
		{
			return cached;
		}
		UnityEngine.Vector3 half = size * 0.5f;
		UnityEngine.Mesh mesh = new UnityEngine.Mesh
		{
			name = "MirroredBoxCollider",
			vertices = new[]
			{
				center + new UnityEngine.Vector3(-half.x, -half.y, -half.z),
				center + new UnityEngine.Vector3(half.x, -half.y, -half.z),
				center + new UnityEngine.Vector3(half.x, half.y, -half.z),
				center + new UnityEngine.Vector3(-half.x, half.y, -half.z),
				center + new UnityEngine.Vector3(-half.x, -half.y, half.z),
				center + new UnityEngine.Vector3(half.x, -half.y, half.z),
				center + new UnityEngine.Vector3(half.x, half.y, half.z),
				center + new UnityEngine.Vector3(-half.x, half.y, half.z)
			},
			triangles = new[]
			{
				0, 2, 1, 0, 3, 2,
				4, 5, 6, 4, 6, 7,
				0, 1, 5, 0, 5, 4,
				3, 7, 6, 3, 6, 2,
				0, 4, 7, 0, 7, 3,
				1, 2, 6, 1, 6, 5
			}
		};
		mesh.RecalculateBounds();
		mirroredBoxMeshes.Add(key, mesh);
		return mesh;
	}

	public void ChooseBiome()
	{
		if (currentBiomeLength < 1)
		{
			BiomeData biome;
			do
			{
				biome = genData.biomes[UnityEngine.Random.Range(0, genData.biomes.Count)];
			}
			while (biome == currentBiome && genData.biomes.Count > 1);
			currentBiome = biome;
			currentBiomeLength = UnityEngine.Random.Range(currentBiome.minLength, currentBiome.maxLength);
			currentStructLength = 0;
		}
		currentBiomeLength--;
	}

	public void ChooseStruct()
	{
		if (currentStructLength < 1)
		{
			var eligible = new System.Collections.Generic.List<StructData>();
			float probabilityTotal = 0f;
			foreach (StructData candidate in currentBiome.structs)
			{
				if (candidate.minScoreToSpawn <= structIndex && structIndex < candidate.maxScoreToSpawn)
				{
					eligible.Add(candidate);
					if (candidate != currentStruct)
					{
						probabilityTotal += candidate.probability;
					}
				}
			}
			float choice = UnityEngine.Random.Range(0f, probabilityTotal);
			for (int i = 0; i < eligible.Count; i++)
			{
				if (eligible[i] != currentStruct)
				{
					choice -= eligible[i].probability;
				}
				if (choice <= 0f)
				{
					currentStruct = eligible[i];
					currentStructLength = UnityEngine.Random.Range(currentStruct.minLenght, currentStruct.maxLength);
					break;
				}
			}
		}
		currentStructLength--;
	}

	public void ChooseChunk(int i)
	{
		System.Collections.Generic.List<Chunk> chunks =
			(i == 0 || i >= genData.structWidth - 1) ? currentStruct.sideChunks : currentStruct.midChunks;
		currentChunksData.Add(chunks[UnityEngine.Random.Range(0, chunks.Count)].data);
	}

	public GGen()
	{
		currentChunksData = new System.Collections.Generic.List<ScriptableObj>();
	}
}
