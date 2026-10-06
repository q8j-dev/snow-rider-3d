using UnityEngine;
using System.Collections;

public class ReporterMessageReceiver : MonoBehaviour
{
	Reporter reporter;
	void Start()
	{
		reporter = gameObject.GetComponent<Reporter>();
	}

	void OnPreStart()
	{
		
		
		if (reporter == null)
			reporter = gameObject.GetComponent<Reporter>();

		if (Screen.width < 1000)
			reporter.size = new Vector2(32, 32);
		else
			reporter.size = new Vector2(48, 48);

		reporter.UserData = "Put user date here like his account to know which user is playing on this device";
	}

	void OnHideReporter()
	{
		
	}

	void OnShowReporter()
	{
		
	}

	void OnLog(Reporter.Log log)
	{
		
	}

}
