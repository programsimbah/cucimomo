using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public static Shop Instance = null;


	 

	public int StarsToAdd  = 0;
	public int StarsToAddStart = 0;


	 
	public static int UnlockAll = 0;
	public static int RemoveAds = 0;
	public static int UnlockFirst10 = 0;
	public static int UnlockFirst16 = 0;
	public static int UnlockAll_RemoveAds = 0;
	public static int SpecialOffer = 0;

	public bool bShopWatchVideo = false;

	public string ShopItemID = "";

	public Text[] txtDispalyStars; //SVA POLJA NA SCENI KOJA TREBA DA SE AZURIRAJU PRILIKOM DODAVANJA ILI ODUZIMANJA ZZVEZDICA

	 
	/*
	public static void InitShop()
	{
		GameObject container;
		if(Instance == null) 
		{ 	
			if(GameObject.Find("DATA_MANAGER") == null)
			{
				container = new GameObject();
				container.name = "DATA_MANAGER";
			}
			else 	container = GameObject.Find("DATA_MANAGER");
			
			
			if(container.GetComponent<Shop>() == null) 	
				Instance = container.AddComponent<Shop>(); 
			else 
				Instance = container.GetComponent<Shop>();
			
			DontDestroyOnLoad(container);
		}
		 
	}
*/
	void Awake()
	{
		if(Instance !=null &&  Instance != this ) GameObject.Destroy(gameObject);
		else {  Instance = this; DontDestroyOnLoad(this.gameObject); }

		//Shop.InitShop();
		GameData.Init();
		 
	}


 
	//***************************WATCH VIDEO******************
	public void WatchVideo( )
	{
 		//zahtev da se prikaze video
		Debug.Log(  "WATCH VIDEO");
		/* VRATI
 		WebelinxCMS.Instance.bPlayVideoReward = true;
 		WebelinxCMS.Instance.IsVideoRewardAvailable(5);
		 */
 
		//odgovor - brisi
		 // FinishWatchingVideo(true);
	}
	
	public void FinishWatchingVideo(bool bVideoOdgledan)
	{
		 // potvrda da je odgledan video...
		//poziva se iz native...
		 
		if(bShopWatchVideo)
		{
			bShopWatchVideo = false;
			AnimiranjeDodavanjaZvezdica( 50 ,null,""); //KADA SE ODGLEDA VIDEO DODAJU SE 50 NIVCICA 
		}
		else
			Camera.main.SendMessage("EndWatchingVideo", bVideoOdgledan ,SendMessageOptions.DontRequireReceiver);
		 
		SoundManager.Instance.Coins.Stop();
		SoundManager.Instance.Play_Sound(SoundManager.Instance.Coins);
	 
	}

	//***************************************************************
	//ODBROJAVANJE NOVCICA
	 
	public void AnimiranjeDodavanjaZvezdica(int _StarsToAdd,  Text txtStars  = null , string message = "STARS: " )
	{
		 
		 
		SoundManager.Instance.Coins.Stop();
		SoundManager.Instance.Play_Sound(SoundManager.Instance.Coins);
		 

		StarsToAddStart =  GameData.TotalStars;
		GameData.TotalStars +=_StarsToAdd;
		StarsToAdd = _StarsToAdd;
		 GameData.SetStarsToPP();

//		Debug.Log(Coins);

		if(txtStars !=null)
		{

			StartCoroutine(animShopCoins(txtStars, message ));
		}
		else 
			StartCoroutine(animShopStarsAllTextFilds( ));
	}

	IEnumerator animShopCoins( Text txtStars , string message  )
	{
		//AUDIO.PlaySound(  "shop_coin");
		int  StarsToAddProg=0;

		int addC = 0;
		int stepUL = Mathf.FloorToInt(StarsToAdd*0.175f);
		int stepLL = Mathf.FloorToInt(StarsToAdd*0.19f);
 
		while( (Mathf.Abs(StarsToAddProg) + Mathf.Abs(addC)) < Mathf.Abs(StarsToAdd) )
		{
			StarsToAddProg+=addC;
			txtStars.text = message+  (StarsToAddStart + StarsToAddProg).ToString();
			//Debug.Log(CoinsToAddStart + CoinsToAddProg);
			yield return new WaitForSeconds (0.05f);
			addC = Mathf.FloorToInt(UnityEngine.Random.Range(stepLL, stepUL));
		}
		
		StarsToAddProg = StarsToAdd;
		txtStars.text = message + GameData.TotalStars.ToString();

		//DataManager.Instance.SaveLastLevelData();
	}

	IEnumerator animShopStarsAllTextFilds(     )
	{
		//AUDIO.PlaySound(  "shop_coin");
		int  StarsToAddProg=0;
		
		int addC = 0;
		int stepUL = Mathf.FloorToInt(StarsToAdd*0.175f);
		int stepLL = Mathf.FloorToInt(StarsToAdd*0.22f);
		if(txtDispalyStars!=null)
		{
			while( (Mathf.Abs(StarsToAddProg) + Mathf.Abs(addC)) < Mathf.Abs(StarsToAdd) )
			{
				StarsToAddProg+=addC;
				for(int i = 0; i<txtDispalyStars.Length;i++)
				{
					if(txtDispalyStars[i].text == "") continue;
					if(txtDispalyStars[i].text.Contains("/"))
					{
						string[] split = txtDispalyStars[i].text.Split('/');
						txtDispalyStars[i].text =   (StarsToAddStart + StarsToAddProg).ToString() + "/" + split[1];
					}
					else
						txtDispalyStars[i].text =   (StarsToAddStart + StarsToAddProg).ToString();
				}
				//Debug.Log(CoinsToAddStart + CoinsToAddProg);
				yield return new WaitForSeconds (0.05f);
				addC = Mathf.FloorToInt(UnityEngine.Random.Range(stepLL, stepUL));
			}
			
			StarsToAddProg = StarsToAdd;

			for(int i = 0; i<txtDispalyStars.Length;i++)
			{
				if(txtDispalyStars[i].text == "") continue;
				if(txtDispalyStars[i].text.Contains("/"))
				{
					string[] split = txtDispalyStars[i].text.Split('/');
					txtDispalyStars[i].text =   (StarsToAddStart + StarsToAddProg).ToString() + "/" + split[1];
				}
				else
					txtDispalyStars[i].text =   GameData.TotalStars.ToString();
			}


		}
//		DataManager.Instance.SaveLastLevelData();
//		Debug.Log(" ** " + Coins);
	}


	
	//********************************************************










	//***************************************************************
	//ODBROJAVANJE DODAVANJA VREDNOSTI
	public Text[] txtFields;
	int StartVal = 0;
	int ValToAdd = 0;

	public void AnimiranjeDodavanjaVrednosti ( int _Start,  int _Add,   string message = "" )
	{
		 
		SoundManager.Instance.Coins.Stop();
		SoundManager.Instance.Play_Sound(SoundManager.Instance.Coins);
		 
		StartVal =  _Start;
 
		ValToAdd = _Add;
		//StopAllCoroutines();
		if(txtFields !=null)
			StartCoroutine(animValue(  message ));
		 
	}
	
	 
	
	IEnumerator animValue(   string message = ""  )
	{
		//AUDIO.PlaySound(  "shop_coin");
		int  ValToAddProg=0;
		
		int addC = 0;
		int stepUL = Mathf.FloorToInt(ValToAdd*0.175f);
		int stepLL = Mathf.FloorToInt(ValToAdd*0.22f);
		if(stepLL == 0 ) stepLL =1;
		if(stepUL ==0 ) stepUL =1;
		if(txtFields!=null)
		{
			while( (Mathf.Abs(ValToAddProg) + Mathf.Abs(addC)) < Mathf.Abs(ValToAdd) )
			{
				ValToAddProg+=addC;
				for(int i = 0; i<txtFields.Length;i++)
				{
					txtFields[i].text = message+  (StartVal + ValToAddProg).ToString();
				}
 
				yield return new WaitForSeconds (0.05f);
				addC = Mathf.FloorToInt(UnityEngine.Random.Range(stepLL, stepUL));
			}
			
			ValToAddProg = ValToAdd;
//			Debug.Log(StartVal + ValToAddProg);
			for(int i = 0; i<txtFields.Length;i++)
			{
				txtFields[i].text = message + (StartVal +ValToAdd).ToString();
			}
		}
	}
	
	
	
	//***********************KUPOVINA *********************************

	 

	public void SendShopRequest(string _shopItemId)
	{
		//RemoveAds, Coins500,  Coins1000, Coins5000
		//Spoon, Bowl , Supplies
		//SpecialOffer

		ShopItemID = _shopItemId;

	 


		string __shopItemId = "";
		switch(_shopItemId)
		{
		case "RemoveAds":
			__shopItemId = "remove_ads";
			break;
		case "UnlockFirst10":
			__shopItemId = "unlock_vehicles4to10";
			break;
		case "UnlockFirst16":
			__shopItemId = "unlock_vehicles4to16";
			break;
		case "UnlockAll":
			__shopItemId = "unlock_all_vehicles";
			break;
		case "SpecialOffer":
			__shopItemId = "special_offer";
			break;
		case "UnlockAll_RemoveAds":
			__shopItemId = "unlock_all_remove_ads";
			break;
	 
 
		}
		 
		 //POZIV U NATIVE ZA KUPOVINU
		 
		//brisi TEST
		//  Debug.Log("BRISI TRANSACTION CONFIRMED");
		//    StartCoroutine (CONFIRM(_shopItemId));


	}

	IEnumerator CONFIRM(string _shopItemId)
	{
		yield return new WaitForSeconds(1);
		//test failed 
		//GameObject.Find("Canvas").GetComponent<MenuManager>().ShowPopUpDialogTitleText("InApp Currently Not Available");
		//GameObject.Find("Canvas").GetComponent<MenuManager>().ShowPopUpDialogCustomMessageText("This InApp purchase is not available at this moment. Thank you for understanding.");

		//test CONFIRMED
		ShopTransactionConfirmed(  _shopItemId);
	}


	public void ShopTransactionConfirmed(string _shopItemId)
	{
 
		ShopManager shopManager=null;
		if( GameObject.Find ("PopUpShop")!=null) shopManager = GameObject.Find ("PopUpShop").GetComponent<ShopManager>();
		ShopItemID = _shopItemId;
		Debug.Log( _shopItemId);

 
		GameObject go =  GameObject.Find("CanvasBG/MainMenu/ButtonsHolder/ItemsSlider");
		switch(ShopItemID )
		{

		case "RemoveAds":
			RemoveAds = 2;
			GameData.SetPurchasedItems();

//			WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "RemoveAds");
			break;

 

		case "UnlockFirst10":
			//AnimiranjeDodavanjaZvezdica(500,null,"");
			UnlockFirst10 = 2;
			GameData.SetPurchasedItems();
			GameData.SetUnlocekedVehicles();
			if(go!=null) go.GetComponent<ItemsSlider>().SetCarPanels();
	//		WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "SmallCoinPack");

			break;
		case "UnlockFirst16":
			UnlockFirst10 = 2;
			UnlockFirst16 = 2;
			GameData.SetPurchasedItems();
			GameData.SetUnlocekedVehicles();
			if(go!=null) go.GetComponent<ItemsSlider>().SetCarPanels();
		 
			 
		//	WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "MediumCoinPack");
			break;
		case "UnlockAll":
			UnlockFirst10 = 2;
			UnlockFirst16 = 2;
			UnlockAll = 2;
			GameData.SetPurchasedItems();
			GameData.SetUnlocekedVehicles();
			if(go!=null) go.GetComponent<ItemsSlider>().SetCarPanels();
 
			//		WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "SmallCoinPack");
			break;

		case "UnlockAll_RemoveAds":
			UnlockFirst10 = 2;
			UnlockFirst16 = 2;
			UnlockAll = 2;
			RemoveAds = 2;
			UnlockAll_RemoveAds = 2;
			GameData.SetPurchasedItems();
			GameData.SetUnlocekedVehicles();


		


			 

			if(go!=null) go.GetComponent<ItemsSlider>().SetCarPanels();
//			WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "LargeCoinPack");
			break;

		case "SpecialOffer":
			Debug.Log("Special offer bought");
			UnlockFirst10 = 2;
			UnlockFirst16 = 2;
			UnlockAll = 2;
			RemoveAds = 2;
			UnlockAll_RemoveAds = 2;
			GameData.SetPurchasedItems();
			GameData.SetUnlocekedVehicles();




 
			if(go!=null) go.GetComponent<ItemsSlider>().SetCarPanels();
			//	 WebelinxCMS.Instance.FlurryEvent("TotalInAppsBought", "InappBought", "LargeCoinPack");
			break;
 
		}
		ShopItemID = "";

 
		 
		 
		if(shopManager!=null)  shopManager.SetShopItems();
	}
  

	public static bool TestSpecialOffer()
	{
		string lastOfferTimeString = "";
		DateTime lastOfferTime = DateTime.Today ;
		if(PlayerPrefs.HasKey("VremePonude"))
		{
			lastOfferTimeString=PlayerPrefs.GetString("VremePonude");
			DateTime dt = DateTime.Parse(lastOfferTimeString) ;
			lastOfferTime = new DateTime(dt.Year,dt.Month,dt.Day) ;
		 
		}
		else
		{
			lastOfferTimeString = DateTime.Today.ToString();
			PlayerPrefs.SetString("VremePonude", lastOfferTimeString);
		}

	//	Debug.Log(lastOfferTime + " " + lastOfferTime.DayOfWeek);
		if( ( Shop.UnlockAll==0 && Shop.RemoveAds ==0 ) &&   lastOfferTime != DateTime.Today   &&   DateTime.Today.DayOfWeek ==  DayOfWeek.Saturday)
		{
			PlayerPrefs.SetString("VremePonude", DateTime.Today.ToString());
			return true;
		}


		return false;
	}

	public void ReturnRestoreData(string shopItemsData)
	{
		
		
	}

	/*
	 * 
	 * 
	 * InApps
Remove Ads - $1.99 - remove_ads
Special Offer - $2.99 - special_offer
Unlock All + Remove Ads - $4.99 - unlock_all_remove_ads
Unlock All Vehicles - $3.99 - unlock_all_vehicles
Unlock Vehicles 4 to 10 - $1.99 - unlock_vehicles4to10
Unlock Vehicles 4 to 16 - $2.99 - unlock_vehicles4to16

 



*/

 

  
}
