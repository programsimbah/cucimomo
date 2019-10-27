using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsSlider : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	CanvasGroup BlockAll;
	private const float inchToCm = 2.54f;
	private EventSystem eventSystem = null;
	private float dragThresholdCM =  0.5f; //vrednost u cm
 
	public GameObject[] ItemPanels;

	public static int ActiveItemNo = 1;
	public Transform[] ItemPrefabs;
	public ShopManager shopManager;
 
	bool bEnableDrag = true;
	bool bDrag = false;
	bool bInertia = false;
	 
	float x;
	float y;
	float speedX = 0;
	float speedLimit = 2;
	float prevX=0;
	Vector3 diffPos = new Vector3(0,0,0);
	Vector3 startPos = new Vector3(0,0,0);

	Vector3 dist1;
	 
	Vector3 dist3;

	float nextItemTresholdX = 1;

	Vector3 ActiveItemPosition;
	float itemDistanceX; 

	public Button Next;
	public Button Prev;

	void Start () {
		 

		Time.timeScale = 1;
		if(!LevelTransition.bFirstStart)  LevelTransition.Instance.ShowScene();
		BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();

//		Debug.Log(transform.position);
		if (eventSystem == null)
		{
			eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		}
		SetDragThreshold();
// 		ItemSprites = (Sprite[]) Resources.LoadAll<Sprite>("cars/cars");
		 
		ActiveItemPosition =   transform.position;

		itemDistanceX = ItemPanels[1].transform.position.x - ItemPanels[0].transform.position.x;

		dist1 = new Vector3(   itemDistanceX  , 0, 0);
		 
		dist3 = new Vector3(   itemDistanceX*3  , 0, 0);
		startPos   = ItemPanels[1].transform.position;
 


		SetCarPanels();
	}

	public void SetCarPanels()
	{
		if( ItemPanels[0].transform.Find("Item").childCount >0)  GameObject.DestroyImmediate( ItemPanels[0].transform.Find("Item").GetChild(0).gameObject);
		if( ItemPanels[1].transform.Find("Item").childCount >0) GameObject.DestroyImmediate( ItemPanels[1].transform.Find("Item").GetChild(0).gameObject);
		if( ItemPanels[2].transform.Find("Item").childCount >0) GameObject.DestroyImmediate( ItemPanels[2].transform.Find("Item").GetChild(0).gameObject);
		//prethodni panel
		if(ActiveItemNo>1)
		{
			Transform c0 = (Transform) GameObject.Instantiate( ItemPrefabs[ActiveItemNo-2]);
			c0.SetParent(ItemPanels[0].transform.Find("Item"));
			c0.localPosition = Vector3.zero;
			c0.localScale = Vector3.one*0.6f;

			if(GameData.VehicleDataList[ActiveItemNo-2].unlocked)
			{
				ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
			}
			else
			{
				ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text =  GameData.TotalStars.ToString()+"/"+ GameData.VehicleDataList[ActiveItemNo-2].stars.ToString();//            "CAR "+(ActiveItemNo-1).ToString();
			}
			ItemPanels[0].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-2].unlocked;
			ItemPanels[0].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-2].unlocked;
		}
		else
		{
			ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
			Prev.interactable = false;
		}

		//trenutni panel
		Transform c1 = (Transform) GameObject.Instantiate( ItemPrefabs[ActiveItemNo-1]);
		c1.SetParent(ItemPanels[1].transform.Find("Item"));
		c1.localPosition = Vector3.zero;
		c1.localScale = Vector3.one*0.6f;

		if(GameData.VehicleDataList[ActiveItemNo-1].unlocked)
		{
			ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = "";
			Tutorial.bPause = false;
			Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
		}
		else
		{
			ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = GameData.TotalStars.ToString()+"/"+ GameData.VehicleDataList[ActiveItemNo-1].stars.ToString();// "CAR "+(ActiveItemNo).ToString();
			Tutorial.bPause = true;
			Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
		}
		ItemPanels[1].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-1].unlocked;
		ItemPanels[1].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-1].unlocked;

		//sledeci panel
		if(ActiveItemNo< ItemPrefabs.Length)
		{
			Transform c2 = (Transform) GameObject.Instantiate( ItemPrefabs[ActiveItemNo]);
			c2.SetParent(ItemPanels[2].transform.Find("Item"));
			c2.localPosition = Vector3.zero;
			c2.localScale = Vector3.one*0.6f;

			if(GameData.VehicleDataList[ActiveItemNo].unlocked)
			{
				ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				 
			}
			else
			{
				ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.VehicleDataList[ActiveItemNo].stars.ToString();// "CAR "+(ActiveItemNo+1).ToString();
				 
			}
			ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo].unlocked;
			ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo].unlocked;
		}
		else
		{
			//ItemPanels[2].transform.FindChild("Item"). GetComponent<Image>().enabled = false;
			//ItemPanels[2].transform.FindChild("Text"). GetComponent<Text>().text = "";
			Next.interactable = false;
		}

	}

	 
	private void SetDragThreshold()
	{
		if (eventSystem != null)
		{
			eventSystem.pixelDragThreshold = (int)( dragThresholdCM * Screen.dpi / inchToCm);

		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		 
 
		if(bInertia)
		{
			speedX *=.80f;
			if(speedX <0.05f && speedX > -0.05f)
			{
				speedX = 0;
				bInertia = false;
				//MoveBack
			}


			ItemPanels[1].transform.position  += new Vector3(  speedX   , 0, 0);
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;



			if(ItemPanels[1].transform.position.x < -nextItemTresholdX || ItemPanels[1].transform.position.x >nextItemTresholdX)
			{
				bInertia = false;
				ChangeCar();
			}
			else if (!bInertia )  StartCoroutine ("SnapToPosition");

		}


		  

	}

	void ChangeCar()
	{
	 	if(ItemPanels[1].transform.position.x < -nextItemTresholdX ) //pomeranje u levo
		{

			if(ActiveItemNo < ItemPrefabs.Length ) 
			{
				ChangeNextCar();
			}
			else  StartCoroutine ("SnapToPosition");
		}
		else
		{
			if(ActiveItemNo > 1 ) 
			{
				ChangePreviousCar();
			}
			else  StartCoroutine ("SnapToPosition");

		}
		SwitchPlace(); 

	
	}

	void ChangeNextCar()
	{
 		if(ActiveItemNo < ItemPrefabs.Length ) 
		{
			SoundManager.Instance.Play_PopUpHide(0f);
			ActiveItemNo++;
			Prev.interactable = true;
			if( ItemPanels[0].transform.Find("Item").childCount>0)  
				GameObject.DestroyImmediate( ItemPanels[0].transform.Find("Item").GetChild(0).gameObject);

			if(ActiveItemNo< ItemPrefabs.Length )
			{

				Transform c2 = (Transform) GameObject.Instantiate( ItemPrefabs[ActiveItemNo]);
				c2.SetParent(ItemPanels[0].transform.Find("Item"));
				c2.localPosition = Vector3.zero;
				c2.localScale = Vector3.one*0.6f;

				if(GameData.VehicleDataList[ActiveItemNo].unlocked)
				{
					ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";

				}
				else
				{
					ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = GameData.TotalStars.ToString()+"/"+ GameData.VehicleDataList[ActiveItemNo].stars.ToString();// "CAR "+(ActiveItemNo+1).ToString();
					 
				}
				ItemPanels[0].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo].unlocked;
				ItemPanels[0].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo].unlocked;
			}
			else
			{
				ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
				Next.interactable = false;
			}

			GameObject  ItemPanelTmp = ItemPanels[0];
			
			ItemPanels[0] = ItemPanels[1];
			ItemPanels[1] = ItemPanels[2];
			ItemPanels[2] = ItemPanelTmp;
			
			
			StartCoroutine ("SnapToPosition");
			GameData.IncrementScrollCarCount();
 
			if(GameData.VehicleDataList[ActiveItemNo-1].unlocked)
			{
				Tutorial.bPause = false;
				Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				Tutorial.bPause = true;
				Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}

		} 


	}

	//*******************
	void ChangePreviousCar()
	{
 		if(ActiveItemNo > 1 ) 
		{
			SoundManager.Instance.Play_PopUpHide(0f);
			ActiveItemNo--;
			Next.interactable = true;
			if(ItemPanels[2].transform.Find("Item").childCount>0) 
				GameObject.DestroyImmediate( ItemPanels[2].transform.Find("Item").GetChild(0).gameObject);

			if(ActiveItemNo>1 )
			{

				Transform c0 = (Transform) GameObject.Instantiate( ItemPrefabs[ActiveItemNo-2]);
				c0.SetParent(ItemPanels[2].transform.Find("Item"));
				c0.localPosition = Vector3.zero;
				c0.localScale = Vector3.one*0.6f;

				if(GameData.VehicleDataList[ActiveItemNo-2].unlocked)
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				}
				else
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.VehicleDataList[ActiveItemNo-2].stars.ToString();// "CAR "+(ActiveItemNo-1).ToString();
				}
				ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-2].unlocked;
				ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-2].unlocked;
			}
			else
			{
				ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				Prev.interactable = false;
			}

			GameObject  ItemPanelTmp = ItemPanels[2];

			ItemPanels[2] = ItemPanels[1];
			ItemPanels[1] = ItemPanels[0];
			ItemPanels[0] = ItemPanelTmp;

			 
			StartCoroutine ("SnapToPosition");
			GameData.IncrementScrollCarCount();

			if(GameData.VehicleDataList[ActiveItemNo-1].unlocked)
			{
				Tutorial.bPause = false;
				Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				Tutorial.bPause = true;
				Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
		} 
	}

	IEnumerator SnapToPosition()
	{
		bEnableDrag = false;
		//speedX
		float i =0;
		while(   i<1.1f)
		{
			i+=0.06f;
			yield return new WaitForFixedUpdate();

			ItemPanels[1].transform.position   =   Vector3.Lerp( ItemPanels[1].transform.position, startPos  , i);
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;
		}
		bEnableDrag = true;
	}



	void SwitchPlace()
	{
		if(ItemPanels[0].transform.position.x < itemDistanceX) ItemPanels[0].transform.position  += dist3;
		if(ItemPanels[1].transform.position.x < itemDistanceX) ItemPanels[1].transform.position  += dist3;
		if(ItemPanels[2].transform.position.x < itemDistanceX) ItemPanels[2].transform.position  += dist3;

		if(ItemPanels[0].transform.position.x > itemDistanceX) ItemPanels[0].transform.position  -= dist3;
		if(ItemPanels[1].transform.position.x > itemDistanceX) ItemPanels[1].transform.position  -= dist3;
		if(ItemPanels[2].transform.position.x > itemDistanceX) ItemPanels[2].transform.position  -= dist3;
		
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if(MenuManager.activeMenu != "") return;
		if(!bEnableDrag) return;

		bDrag = true;
	 
		diffPos = ItemPanels[1].transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		prevX = ItemPanels[1].transform.position.x;
			
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if(MenuManager.activeMenu != "") return;
		if( bEnableDrag &&  bDrag )
		{
			 
			prevX = ItemPanels[1].transform.position.x;
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
		 
			ItemPanels[1].transform.position = new Vector3(  (Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos).x   ,  ActiveItemPosition.y,  ActiveItemPosition.z);
			 

			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;

		if( (ActiveItemNo ==1 && ItemPanels[1].transform.position.x> 1) ||  (ActiveItemNo== ItemPrefabs.Length &&  ItemPanels[1].transform.position.x < -1 ))
			{
				bDrag = false;
				bInertia = true;
			}
		}

	 
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
 	 
			ItemPanels[1].transform.position = new Vector3(  (Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos).x   ,  ActiveItemPosition.y,  ActiveItemPosition.z);
			
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;
 	 
			speedX =  ItemPanels[1].transform.position.x  - prevX;
			if(speedX <-speedLimit) speedX = -speedLimit;
			else if(speedX > speedLimit) speedX = speedLimit;
		 
			bDrag = false;
			bInertia = true;
 
		}
	}

	public void OnPointerClick( PointerEventData eventData)
	{
		Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
		if(MenuManager.activeMenu != "") return;
		if(BlockAll.blocksRaycasts) return;
		if(!eventData.dragging) 
		{
			if(GameData.VehicleDataList[ActiveItemNo-1].unlocked)
			{
				SoundManager.Instance.Play_ButtonClick();
			 
				LevelTransition.Instance.HideScene("Gameplay");
				BlockAll.blocksRaycasts = true;

			}
			else
			{
//				if( GameData.TotalStars  >=  GameData.VehicleDataList[ActiveItemNo-1].stars  )
//				{
//					GameData.UnlockVehicle( ActiveItemNo-1);
//					ItemPanels[1].transform.FindChild("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-1].unlocked;
//					Shop.Instance.AnimiranjeDodavanjaZvezdica( -GameData.VehicleDataList[ActiveItemNo-1].stars,null, ""); 
//					BlockAll.blocksRaycasts = true;
//					StartCoroutine(SetBlockAll(1f,false));
//				}
//				else
//				{
//					Debug.Log("NEMA DOVOLJNO ZVEZDICA");
					shopManager.ShowPopUpShop();
				 	SoundManager.Instance.Play_Error();
				//	BlockAll.blocksRaycasts = true;
				//	StartCoroutine(SetBlockAll(1f,false));
//				}
			}
		}
	}

	public void btnNextCar(  )
	{
		ChangeNextCar();
		//SoundManager.Instance.Play_PopUpHide(0f);	
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(.4f,false));
		Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
//		GameObject.Find("Tutorial").SendMessage("HidePointer");
		 
	}
	
	public void btnPreviousCar(  )
	{
		ChangePreviousCar();
		//SoundManager.Instance.Play_PopUpHide(0f);
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(.4f,false));
		Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
	//	GameObject.Find("Tutorial").SendMessage("HidePointer");
		 
	}

	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		
	}




}