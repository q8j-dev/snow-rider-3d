public class WorldBender : UnityEngine.MonoBehaviour
{
	public float extraCullHeight;

	public UnityEngine.GameObject hero;

	public UnityEngine.Camera _camera;

	private UnityEngine.Light _skyLight;

	public float attenuation;

	public float horizonOffset;

	public float spread;

	public float Horizon
	{
		get
		{
			return hero != null ? hero.transform.position.z + horizonOffset : 0f;
		}
	}

	public void Update()
	{
		if (hero != null)
		{
			UnityEngine.Shader.SetGlobalFloat("_HORIZON", hero.transform.position.z + horizonOffset);
			UnityEngine.Shader.SetGlobalFloat("_SPREAD", spread);
			UnityEngine.Shader.SetGlobalFloat("_ATTENUATE", attenuation);
		}
	}

	public void OnDestroy()
	{
		UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
		UnityEngine.Rendering.RenderPipelineManager.endCameraRendering -= EndCameraRendering;
		if (_camera != null)
		{
			_camera.ResetCullingMatrix();
		}
		UnityEngine.Shader.SetGlobalFloat("_ATTENUATE", 0f);
		UnityEngine.Shader.SetGlobalFloat("_SPREAD", 0f);
		UnityEngine.Shader.SetGlobalFloat("_HORIZON", 0f);
	}

	public void OnApplicationQuit()
	{
		UnityEngine.Shader.SetGlobalFloat("_ATTENUATE", 0f);
		UnityEngine.Shader.SetGlobalFloat("_SPREAD", 0f);
		UnityEngine.Shader.SetGlobalFloat("_HORIZON", 0f);
	}

	public void Start()
	{
		if (_camera == null)
		{
			_camera = GetComponent<UnityEngine.Camera>();
		}
	}

	public void OnEnable()
	{
		UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
		UnityEngine.Rendering.RenderPipelineManager.endCameraRendering -= EndCameraRendering;
		UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
		UnityEngine.Rendering.RenderPipelineManager.endCameraRendering += EndCameraRendering;
	}

	public void OnDisable()
	{
		UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
		UnityEngine.Rendering.RenderPipelineManager.endCameraRendering -= EndCameraRendering;
		if (_camera != null)
		{
			_camera.ResetCullingMatrix();
		}
	}

	private void BeginCameraRendering(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera)
	{
		if (_camera == null)
		{
			_camera = GetComponent<UnityEngine.Camera>();
		}
		if (camera != _camera)
		{
			return;
		}
		UnityEngine.Shader.SetGlobalMatrix("_Camera2World", camera.cameraToWorldMatrix);
		UnityEngine.Shader.SetGlobalMatrix("_World2Camera", camera.worldToCameraMatrix);
		if (_skyLight == null)
		{
			_skyLight = UnityEngine.RenderSettings.sun;
			if (_skyLight == null)
			{
				UnityEngine.Light[] lights = UnityEngine.Object.FindObjectsByType<UnityEngine.Light>();
				for (int i = 0; i < lights.Length; i++)
				{
					if (lights[i].isActiveAndEnabled && lights[i].type == UnityEngine.LightType.Directional && (_skyLight == null || lights[i].intensity > _skyLight.intensity))
					{
						_skyLight = lights[i];
					}
				}
			}
		}
		if (_skyLight != null)
		{
			UnityEngine.Vector3 lightDirection = -_skyLight.transform.forward;
			UnityEngine.Shader.SetGlobalVector("_WorldSpaceLightPos0", new UnityEngine.Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0f));
		}
		float aspect = _camera.aspect;
		float fieldOfView = _camera.fieldOfView;
		float originalHalfHeight = UnityEngine.Mathf.Tan(fieldOfView * UnityEngine.Mathf.Deg2Rad * 0.5f);
		float expandedFieldOfView = fieldOfView * (extraCullHeight + 1f);
		float expandedHalfHeight = UnityEngine.Mathf.Tan(expandedFieldOfView * UnityEngine.Mathf.Deg2Rad * 0.5f);
		UnityEngine.Matrix4x4 cullingProjection = UnityEngine.Matrix4x4.Perspective(expandedFieldOfView, originalHalfHeight * aspect / expandedHalfHeight, _camera.nearClipPlane, _camera.farClipPlane);
		_camera.cullingMatrix = cullingProjection * _camera.worldToCameraMatrix;
	}

	private void EndCameraRendering(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera)
	{
		if (camera == _camera)
		{
			_camera.ResetCullingMatrix();
		}
	}

}
