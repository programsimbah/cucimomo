using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelTransition : MonoBehaviour {

/*	Animator anim;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad();
		anim = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadNextLevel(string level)
	{
		//BLOCK ALL
		anim.SetTrigger("tClose");
		StartCoroutine("WaitLoadNextLevel", level);
	}

	IEnumerator WaitLoadNextLevel(string level)
	{
		yield return new WaitForSeconds(1.2f);
		Application.LoadLevel(level);
	}


}
*/
//**************************************************



	Animator anim;
	//TransitionDepartingStart
	public static bool bFirstStart = true;
	static LevelTransition instance;

 
	bool bLoadScene = false;
	public static LevelTransition Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(LevelTransition)) as LevelTransition;
			}
			return instance;
		}
	}


	void Start () 
	{
		DontDestroyOnLoad(this.gameObject);
		anim = transform.GetComponent<Animator>();
	}

	void Awake()
	{
		if(instance !=null &&  instance != this ) GameObject.Destroy(gameObject);
 
	}



	public void HideScene(string levelName)
	{
		if(bLoadScene) return;
		bLoadScene = true;
		StopAllCoroutines();
		StartCoroutine(SetBlockAll(0,true));

		StartCoroutine("LoadScene" , levelName);
		anim.SetTrigger("tClose");
	}

	public void ShowScene()
	{
		StartCoroutine(SetBlockAll(0,true));
		anim.SetTrigger("tOpen");


		if(Application.loadedLevelName == "MainScene") { StartCoroutine(SetBlockAll(6f,false));}
		else 
			StartCoroutine(SetBlockAll(1.0f,false)); 
	}

	CanvasGroup BlockAll;
 
	IEnumerator SetBlockAll(float time, bool blockRays)
	{
 
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();

		yield return new WaitForSeconds(time);

		BlockAll.blocksRaycasts = blockRays;
		
	}

	IEnumerator LoadScene (string levelName)
	{
//		try {
//			if (Shop.RemoveAds !=2 && DataManager.Instance.Tutorial >= 4 && Application.loadedLevelName != "Gameplay4" ) WebelinxCMS.Instance.ShowInterstitial(7);
//		}
//		catch{ Debug.Log("Show Interstitial Error"); } 
		yield return new WaitForSeconds(1.0f);
		bLoadScene = false;
		Application.LoadLevel (levelName);
		
	}

	public void HideAndShowScene( )
	{
		StopAllCoroutines();
		StartCoroutine(SetBlockAll(0,true));
		anim.ResetTrigger("tOpen");
		anim.SetTrigger("tClose");
		StartCoroutine("WaitHideAndShowScene");

	}

	IEnumerator WaitHideAndShowScene ( )
	{
		yield return new WaitForSeconds(2.0f);
		anim.SetTrigger("tOpen");
		StartCoroutine(SetBlockAll(1,false));
	}
	
}

