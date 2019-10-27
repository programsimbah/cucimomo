using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour {

	public static int TotalStars =0;
	public static int CollectedStars = 0;

	public static bool bWatchVideoReady = false;
	public static bool bWatchVideoStart = false;
 
	public static bool TestTutorial = false;
	public static string UnlockedVehicles = "dafE1A";
	public static int TutorialShown = 0;
	public static List<VehicleData> VehicleDataList = new List<VehicleData>();
	
 
	public static void UnlockVehicle( int vehicleNo)
	{
		if(!UnlockedVehicles.Contains(  VehicleDataList[  vehicleNo ] .code))
	   {
			UnlockedVehicles +=VehicleDataList[  vehicleNo ] .code;
			PlayerPrefs.SetString("Data2",UnlockedVehicles);
		}
		SetUnlocekedVehicles();
 
//			WebelinxCMS.Instance.FlurryEvent("LevelEvents", "LevelReached", LastLevel.ToString());
 
	}

	 
	 
	public static void TutorialOver()
	{
		TutorialShown = 1;
		PlayerPrefs.SetInt("TUTORIAL",1);
	}

	public static void Init()
	{
		if(VehicleDataList.Count>0) return;

		TutorialShown = PlayerPrefs.GetInt("TUTORIAL",0);
		 

		GetStarsFromPP();

		GetPurchasedItems();

		VehicleDataList.Add(new VehicleData(  "BEATLE", 0, "da" ));
		VehicleDataList.Add(new VehicleData(  "POLICE CAR", 20, "fE" ));
		VehicleDataList.Add(new VehicleData(  "TRACTOR", 50, "1A" ));


		VehicleDataList.Add(new VehicleData(  "FIRE TRUCK", 60, "g2" ));
		VehicleDataList.Add(new VehicleData(  "JEEP", 90, "tI" ));
		VehicleDataList.Add(new VehicleData(  "FAMILY VAN", 120, "m0" ));
		VehicleDataList.Add(new VehicleData(  "BULLDOZER", 180, "Ke" ));
		VehicleDataList.Add(new VehicleData(  "SPORTS CAR", 240, "6A" ));
		VehicleDataList.Add(new VehicleData(  "SAND TRUCK", 330, "bO" ));
		VehicleDataList.Add(new VehicleData(  "ICECREAM TRUCK", 420, "p4" ));
		VehicleDataList.Add(new VehicleData(  "CRANE TRUCK", 540, "RR" ));
		VehicleDataList.Add(new VehicleData(  "CABLE TRACK", 660, "l6" ));
		VehicleDataList.Add(new VehicleData(  "SCHOOL BUS", 810, "vy" ));
		VehicleDataList.Add(new VehicleData(  "CIRCUS CAR", 960, "te" ));
		VehicleDataList.Add(new VehicleData(  "FORMULA 1", 1140, "j3" ));
		VehicleDataList.Add(new VehicleData(  "TANK", 1320, "Hu" ));
		VehicleDataList.Add(new VehicleData(  "MONSTER TRUCK", 1530, "1a" ));

		VehicleDataList.Add(new VehicleData(  "PRESIDENT CAR", 1740 , "X2" ));
		VehicleDataList.Add(new VehicleData(  "AMBULANCE", 1980, "wO" ));
		VehicleDataList.Add(new VehicleData(  "MILK TRUCK ??", 2220, "0o" ));
		VehicleDataList.Add(new VehicleData(  "BIPLANE", 2490, "55" ));
		VehicleDataList.Add(new VehicleData(  "UFO", 2760, "88" ));

		UnlockedVehicles = 	PlayerPrefs.GetString("Data2","dafE1A");
 
		if(  Shop.UnlockFirst10 !=2 && TotalStars>= VehicleDataList[9].stars)   Shop.UnlockFirst10 = 2;
		if(  Shop.UnlockFirst16 !=2 && TotalStars>= VehicleDataList[15].stars)   Shop.UnlockFirst16 = 2; 
		

		SetUnlocekedVehicles();
	}

	public static void SetUnlocekedVehicles()
	{


		for(int i =0; i <VehicleDataList.Count;i++)
		{
			VehicleDataList[i].unlocked = (    UnlockedVehicles.Contains( VehicleDataList[i].code ) || Shop.UnlockAll == 2 || (Shop.UnlockFirst10==2 && i<10) ||  (Shop.UnlockFirst16==2 && i<16) || TotalStars>=	VehicleDataList[i].stars);
 
		}
	}



	public static void GetStarsFromPP()
	{
		string tmp = PlayerPrefs.GetString("Data1","9383");
		tmp= tmp.Replace("_","9");
		tmp= tmp.Replace("76q","8");
		tmp= tmp.Replace("nmFs","7");
		tmp= tmp.Replace("Tr;","6");
		tmp= tmp.Replace("^3","5");
		tmp= tmp.Replace("D","4");
		tmp= tmp.Replace("EE","3");
		tmp= tmp.Replace("g$","2");
		tmp= tmp.Replace("=0","1");
		tmp= tmp.Replace("Ase","0");
 
		int tmpStars = int.Parse(tmp);
		TotalStars = tmpStars - 9383;
		//Debug.Log(tmpStars + "  " + TotalStars + "  " + tmp); 

		 
	}

	public  static void SetStarsToPP()
	{
		string tmp = (TotalStars+9383).ToString();

		tmp= tmp.Replace("0","Ase");
		tmp= tmp.Replace("1","=0");
		tmp= tmp.Replace("2","g$");
		tmp= tmp.Replace("3","EE");
		tmp= tmp.Replace("4","D");
		tmp= tmp.Replace("5","^3");
		tmp= tmp.Replace("6","Tr;");
		tmp= tmp.Replace("7","nmFs");
		tmp= tmp.Replace("8","76q");
		tmp= tmp.Replace("9","_");

		//Debug.Log(TotalStars + "  " + (TotalStars+9383).ToString() + "  " + tmp);
		PlayerPrefs.SetString("Data1", tmp);

		if(  Shop.UnlockFirst10 !=2 && TotalStars>= VehicleDataList[9].stars)   Shop.UnlockFirst10 = 2;
		if(  Shop.UnlockFirst16 !=2 && TotalStars>= VehicleDataList[15].stars)   Shop.UnlockFirst16 = 2; 

		SetUnlocekedVehicles();
	}


	public static void GetPurchasedItems()
	{
		string tmp = PlayerPrefs.GetString("Data3","20153");
		tmp= tmp.Replace("<","9");
		tmp= tmp.Replace("7>q","8");
		tmp= tmp.Replace("nmFs","7");
		tmp= tmp.Replace("Vy;","6");
		tmp= tmp.Replace("*2","5");
		tmp= tmp.Replace("H","4");
		tmp= tmp.Replace("JE","3");
		tmp= tmp.Replace("B#","2");
		tmp= tmp.Replace("+0","1");
		tmp= tmp.Replace("Kce","0");
		
		int tmpPurchased = int.Parse(tmp);
		int purchased = tmpPurchased - 20153;

 
		Shop.UnlockFirst16 = Mathf.FloorToInt(purchased/10000);
		purchased = purchased -  Shop.UnlockFirst16*10000;

		Shop.UnlockFirst10 = Mathf.FloorToInt(purchased/1000);
		purchased = purchased -  Shop.UnlockFirst10*1000;

		Shop.SpecialOffer = Mathf.FloorToInt(purchased/100);
		purchased = purchased -  Shop.SpecialOffer*1000;

		Shop.RemoveAds = purchased - Shop.UnlockAll*10;

		Shop.UnlockAll = Mathf.FloorToInt(purchased/10);
		Shop.RemoveAds = purchased - Shop.UnlockAll*10;


		if( Shop.SpecialOffer == 2) Shop.UnlockAll_RemoveAds  = 2;
		if(Shop.UnlockAll_RemoveAds == 2) { Shop.UnlockAll =2; Shop.RemoveAds = 2; }
		else if(Shop.UnlockAll ==2 && Shop.RemoveAds == 2) Shop.UnlockAll_RemoveAds =2;
		if(Shop.UnlockAll==2) Shop.UnlockFirst16 = 2;  
		if(Shop.UnlockFirst16==2)  Shop.UnlockFirst10 =2;   

//		Debug.Log ( Shop.UnlockAll + "  -  ra  " + Shop.RemoveAds);
		//Shop.UnlockAll = 2;
	}

	public static void SetPurchasedItems()
	{
		if(  Shop.UnlockFirst10 !=2 && TotalStars>= VehicleDataList[9].stars)   Shop.UnlockFirst10 = 2;
		if(  Shop.UnlockFirst16 !=2 && TotalStars>= VehicleDataList[15].stars)   Shop.UnlockFirst16 = 2; 
		
		if( Shop.SpecialOffer == 2) Shop.UnlockAll_RemoveAds  = 2;
		if(Shop.UnlockAll_RemoveAds == 2) { Shop.UnlockAll =2; Shop.RemoveAds = 2; }
		else if(Shop.UnlockAll ==2 && Shop.RemoveAds == 2) Shop.UnlockAll_RemoveAds =2;
		if(Shop.UnlockAll==2) Shop.UnlockFirst16 = 2;  
		if(Shop.UnlockFirst16==2)  Shop.UnlockFirst10 =2;   

		int purchased = Shop.UnlockFirst16*10000 +  Shop.UnlockFirst10*1000+ Shop.SpecialOffer*100 +   Shop.UnlockAll*10+Shop.RemoveAds ;
 
		string tmp = (purchased+20153).ToString();
		
		tmp= tmp.Replace("0","Kce");
		tmp= tmp.Replace("1","+0");
		tmp= tmp.Replace("2","B#");
		tmp= tmp.Replace("3","JE");
		tmp= tmp.Replace("4","H");
		tmp= tmp.Replace("5","*2");
		tmp= tmp.Replace("6","Vy;");
		tmp= tmp.Replace("7","nmFs");
		tmp= tmp.Replace("8","7>q");
		tmp= tmp.Replace("9","<");
		
		 
		PlayerPrefs.SetString("Data3", tmp);
		PlayerPrefs.Save();
	}

 
	 
	//BROJANJE KLIKOVA ZA PRIKAZ REKLAMA
 
	public static void IncrementScrollCarCount()
	{
		Debug.Log("IncrementScrollCarCount");
		 
//		if (Shop.RemoveAds !=2  ) 
//		{
//			WebelinxCMS.Instance.ShowInterstitial(WebelinxCMS.INTERSTITIAL_SCROLL_CAR);
//		}
	}
	
	public static void IncrementCarCompletedCount()
	{
		Debug.Log("IncrementCarCompletedCount"); 
		if (Shop.RemoveAds !=2    )
		{
		//	AdsManager.Instance.ShowInterstitial();
		}
	}
	 
	public static void IncrementHomeButtonClickedCount()
	{
		Debug.Log("IncrementHomeButtonClickedCount"); 
		if (Shop.RemoveAds !=2    )
		{
//			AdsManager.Instance.ShowInterstitial();
		}
	}
 
}

public class VehicleData 
{
	public string name = "";
	public int stars = 0;
	public bool unlocked = false;
	public string code = "";

	public VehicleData( string name, int stars, string code)
	{
		this.name = name;
		this.stars = stars;
		this.code = code;
	}
	
} 

