using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class GlobalVariables : MonoBehaviour {

	public static bool removeAdsOwned = false;
	public static string applicationID;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
		#if UNITY_ANDROID || UNITY_EDITOR_WIN
		applicationID = "com.Car.Cleaning.Spa.Games";
		#elif UNITY_IOS
		applicationID = ""; // "bundle.ID";
		#endif
	}
}
