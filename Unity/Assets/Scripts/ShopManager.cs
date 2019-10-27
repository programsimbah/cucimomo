using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	MenuManager menuManager;
	 
	 
	 
	 
	public Text txtPrice_RemoveAds;
	public Text txtPrice_First10;
	public Text txtPrice_First16;
	public Text txtPrice_UnlockAll_RemoveAds;
	public Text txtPrice_UnlockAll;
	public Text txtPrice_SpecialOffer;
 
	CanvasGroup BlockAll;

	public static bool bShowShop = false;

	void Awake () {
		InitPrices();
	}

	void Start () {



		bShowShop = false;
		menuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();
		 
		BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();

		InitPrices();
		SetShopItems();

		if(Application.loadedLevelName == "CustomerScene")  transform.localScale =  (0.82f-( 1.7777f -(float) Screen.width/ (float) Screen.height )*0.45f) *Vector3.one;
		if(Application.loadedLevelName == "MapScene")  transform.localScale =  (0.80f-( 1.7777f -(float) Screen.width/ (float) Screen.height )*0.19f) *Vector3.one;
//		if(Shop.RemoveAds !=2 && DataManager.Instance.Tutorial>=4 ) GameObject.Find("PopUps").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-30);

		#if UNITY_ANDROID
		 
//		GameObject restoreP= GameObject.Find("TabButtonRestorePurchase"); 
//		restoreP.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2 (600, 400);
//		GameObject.Destroy(restoreP);
		
		#endif
	}


	void InitPrices()
	{
//		if(txtPrice_RemoveAds !=null) txtPrice_RemoveAds .text = WebelinxAndroidInApps.removeAdsPrice; 
//		if(txtPrice_First10 !=null) txtPrice_First10.text =   WebelinxAndroidInApps.unlockFirst10Price;	 
//		if(txtPrice_First16 !=null) txtPrice_First16.text = WebelinxAndroidInApps.unlockFirst16Price;	 
//		if(txtPrice_UnlockAll !=null) txtPrice_UnlockAll.text = WebelinxAndroidInApps.unlockAllPrice;	 
//		if(txtPrice_UnlockAll_RemoveAds !=null) txtPrice_UnlockAll_RemoveAds.text = WebelinxAndroidInApps.unlockAllAndRemoveAdsPrice;	 
//		if(txtPrice_SpecialOffer !=null) txtPrice_SpecialOffer.text = WebelinxAndroidInApps.specialOfferPrice;	 

		//*******************************************************
		//Color col = new Color(0,0.1f,1,1);
		
		 
		if(  Shop.RemoveAds == 2 )
		{
//			txtPrice_RemoveAds.transform.parent.GetComponent<Image>().color = col;
			txtPrice_RemoveAds.text = "bought"; 
			txtPrice_RemoveAds.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_RemoveAds.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_RemoveAds.transform.parent.GetComponent<Image>().enabled = false;//  .color = col;
		}

		if(  Shop.UnlockAll == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_UnlockAll.text = "bought"; 
			txtPrice_UnlockAll.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll.transform.parent.GetComponent<Image>().enabled = false;
			
		}

		if(  Shop.UnlockFirst10 == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_First10.text = "bought"; 
			txtPrice_First10.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_First10.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_First10.transform.parent.GetComponent<Image>().enabled = false;
			
		}

		if(  Shop.UnlockFirst16 == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_First16.text = "bought"; 
			txtPrice_First16.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_First16.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_First16.transform.parent.GetComponent<Image>().enabled = false;
			
		}

		if(  Shop.UnlockAll_RemoveAds == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_UnlockAll_RemoveAds.text = "bought"; 
			txtPrice_UnlockAll_RemoveAds.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll_RemoveAds.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll_RemoveAds.transform.parent.GetComponent<Image>().enabled = false;
			
		}
	 
		
 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void btnBuyClick(string btnID)
	{
	
		BlockAll.blocksRaycasts = true;
		//RemoveAds, Coins500,  Coins1000, Coins5000
		//Spoon, , Supplies
		//SpecialOffer
	
		if(btnID == "RemoveAds" &&  Shop.RemoveAds == 0)  Shop.Instance.SendShopRequest(btnID);
		if(btnID == "UnlockAll" &&  Shop.UnlockAll == 0)  Shop.Instance.SendShopRequest(btnID);

		if(btnID == "UnlockFirst10" &&  Shop.UnlockFirst10 == 0)  Shop.Instance.SendShopRequest(btnID);
		if(btnID == "UnlockFirst16" &&  Shop.UnlockFirst16 == 0)  Shop.Instance.SendShopRequest(btnID);

		if(btnID == "UnlockAll_RemoveAds" &&  Shop.UnlockAll_RemoveAds == 0)  Shop.Instance.SendShopRequest(btnID);
		if(btnID == "SpecialOffer" &&  Shop.SpecialOffer == 0)  Shop.Instance.SendShopRequest(btnID);

	 
 
		StartCoroutine(SetBlockAll(1f,false));
 		SoundManager.Instance.Play_ButtonClick();

		// menuManager.ShowPopUpMessage("TEST", "Transacition succesful");
	 
	}


	public void SetShopItems()
	{
		//Color col = new Color(0,0.1f,1,1);
  
		if(  Shop.RemoveAds == 2 )
		{
			//txtPrice_RemoveAds.transform.parent.GetComponent<Image>().color = col;
			txtPrice_RemoveAds.text = "bought"; 
			txtPrice_RemoveAds.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_RemoveAds.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_RemoveAds.transform.parent.GetComponent<Image>().enabled = false;//  .color = col;
			StartCoroutine(SetBlockAll(1f,false));
		}

		if(  Shop.UnlockAll == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_UnlockAll.text = "bought"; 
			txtPrice_UnlockAll.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll.transform.parent.GetComponent<Image>().enabled = false;
			StartCoroutine(SetBlockAll(1f,false));
		}

		if(  Shop.UnlockFirst10 == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_First10.text = "bought"; 
			txtPrice_First10.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_First10.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_First10.transform.parent.GetComponent<Image>().enabled = false;
			
		}
		
		if(  Shop.UnlockFirst16 == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_First16.text = "bought"; 
			txtPrice_First16.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_First16.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_First16.transform.parent.GetComponent<Image>().enabled = false;
			
		}
		
		if(  Shop.UnlockAll_RemoveAds == 2 )
		{
			//txtPrice_UnlockAll.transform.parent.GetComponent<Image>().color = col;
			txtPrice_UnlockAll_RemoveAds.text = "bought"; 
			txtPrice_UnlockAll_RemoveAds.transform.parent.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll_RemoveAds.transform.parent.GetComponent<Button>().enabled = false;
			txtPrice_UnlockAll_RemoveAds.transform.parent.GetComponent<Image>().enabled = false;
			
		}

	}
 
	public void ShowPopUpShop()
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		BlockAll.blocksRaycasts = true;
		InitPrices();
		if(menuManager == null) menuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();
 
		menuManager.ShowPopUpMenu (gameObject);
		StartCoroutine(SetBlockAll(1f,false));

		Camera.main.SendMessage("SetPauseOn",SendMessageOptions.DontRequireReceiver);
		/* VRATI
		menuManager.EscapeButonFunctionStack.Push("ClosePopUpShop");
		*/
		bShowShop = true;
	}

	 
 
	//**********************************************************
	 

	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		 
	}


	//ovo je funkcija koja sluzi samo da pusti zvuk
	public void btnClicked_PlaySound()
	{
		SoundManager.Instance.Play_ButtonClick();
	}

	public void btnRestorePurchaseClick()
	{
		//brisi
		//PurchaseRestored();

	}
	
	void PurchaseRestored( )
	{
		
		CanvasGroup canvasGroup_ItemsRestored = GameObject.Find("Message_ItemsRestored").GetComponent<CanvasGroup>();
		canvasGroup_ItemsRestored.interactable = true;
		canvasGroup_ItemsRestored.blocksRaycasts = true;
		StartCoroutine(ShowMsgItemsRestored(canvasGroup_ItemsRestored));
		 
	}
	
	IEnumerator ShowMsgItemsRestored(CanvasGroup canvasGroup_ItemsRestored)
	{
		for(float i = 0;i<=1;i+=0.1f)
		{
			canvasGroup_ItemsRestored.alpha = i;
			yield return new WaitForSeconds(0.03f);
		}
		canvasGroup_ItemsRestored.alpha = 1;
		StartCoroutine(CloseMsgItemsRestored(canvasGroup_ItemsRestored));
	}
	
	IEnumerator CloseMsgItemsRestored(CanvasGroup canvasGroup_ItemsRestored)
	{
		yield return new WaitForSeconds(2);
		for(float i = 1;i>=0;i-=0.1f)
		{
			canvasGroup_ItemsRestored.alpha = i;
			yield return new WaitForSeconds(0.03f);
		}
		canvasGroup_ItemsRestored.interactable = false;
		canvasGroup_ItemsRestored.blocksRaycasts = false;
		canvasGroup_ItemsRestored.alpha = 0;
	} 
}
