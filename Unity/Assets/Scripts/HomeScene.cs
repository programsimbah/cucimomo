using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HomeScene : MonoBehaviour {

	public Sprite SoundOn;
	public Sprite SoundOff;
	public Image BtnSoundIcon;

 
	public GameObject PopUpSecialOffer;
	public GameObject PopUpSettings;
	 

	public MenuManager menuManager;
	CanvasGroup BlockAll;
 
	void Awake()
	{
		Shop.Instance.txtDispalyStars =  null;
		//WebelinxCMS.IsInterstitialAvailable(WebelinxCMS.INTERSTITIAL_APPSTART_ID);
	}

	void Start () {

		Input.multiTouchEnabled = false;
 
		BlockAll = GameObject.Find("Canvas/BlockAll").transform.GetComponent<CanvasGroup>();
		BlockAll.blocksRaycasts = false;

	/*	if(SoundManager.soundOn == 0) 
			BtnSoundIcon.sprite = SoundOff;
		else  
			BtnSoundIcon.sprite = SoundOn;
 
*/

	}

	
	public void ExitGame () {
		if (Shop.RemoveAds !=2 )
		{
			Application.Quit();
		}
	}

 
	public void btnPlayClick()
	{
		SoundManager.Instance.Play_ButtonClick();
		StartCoroutine(LoadMap());
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(2f,false));
	}
	
	IEnumerator LoadMap()
	{
		yield return new WaitForSeconds(.3f);
		Application.LoadLevel("SelectCar");
		
	}

 

//	public void btnHelpClick()
//	{
//		SoundManager.Instance.Play_ButtonClick();
//		menuManager.ShowPopUpMenu(PopUpHelp);
//	}
//
//	public void btnCloseHelpClick()
//	{
//		SoundManager.Instance.Play_ButtonClick();
//		menuManager.ClosePopUpMenu(PopUpHelp);
//	}

	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		BlockAll.alpha = blockRays?1:0;
	}


	public void ClickSound()
	{
		SoundManager.Instance.Play_ButtonClick();
	}

	public void btnSettingsClicked()
	{
		SoundManager.Instance.Play_ButtonClick();
		menuManager.ShowPopUpMenu(PopUpSettings);
//		BlockAll.blocksRaycasts = true;
//		StartCoroutine(SetBlockAll(.5f,false));
	}

	
	public void btnBuySpecialOfferClick(string btnID)
	{
		
		BlockAll.blocksRaycasts = true;
		if(btnID == "SpecialOffer" &&  Shop.SpecialOffer == 0)  Shop.Instance.SendShopRequest(btnID);
		
		
		StartCoroutine(SetBlockAll(1f,false));
		SoundManager.Instance.Play_ButtonClick();
		StartCoroutine("CloseSpecialOffer");
	}
	
	IEnumerator CloseSpecialOffer()
	{
		yield return new WaitForSeconds(1);
		menuManager.ClosePopUpMenu(PopUpSecialOffer );
		
	}

 
}
