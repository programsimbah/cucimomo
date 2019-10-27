using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectCar : MonoBehaviour {

	//AUTOMOBILI SE POMERAJU IZ SKRIPTE ItemsSlider.cs
	public GameObject PopUpSettings;
	public GameObject PopUpCongrats;
	public Text txtCollectedStars;

	MenuManager menuManager;
	public Text[] txtDispalyStars;
	CanvasGroup BlockAll;

	void Start () {
		menuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();


		Tutorial.bPause = false;

		Shop.Instance.txtDispalyStars =  txtDispalyStars;


		BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(1f,false));

		if( GameData.CollectedStars > 0) 
		{
			StartCoroutine("ShowCongratsPopup");
			for(int i = 0; i<txtDispalyStars.Length;i++)
			{
				txtDispalyStars[i].text =   (GameData.TotalStars - GameData.CollectedStars).ToString();
			}
		
		}
		else
		{

			for(int i = 0; i<txtDispalyStars.Length;i++)
			{
				txtDispalyStars[i].text =   GameData.TotalStars.ToString();
			}
		}

	}

	IEnumerator ShowCongratsPopup( )
	{
		yield return new WaitForSeconds(1);
		menuManager.ShowPopUpMenu(PopUpCongrats);
		txtCollectedStars.text = "YOU HAVE WON\n" + GameData.CollectedStars.ToString() + " STARS!";

		yield return new WaitForSeconds(1.5f);
		Shop.Instance.txtFields = new Text[1] {  txtDispalyStars[0]};
		Shop.Instance. AnimiranjeDodavanjaVrednosti( GameData.TotalStars - GameData.CollectedStars, GameData.CollectedStars, "" );
		GameData.CollectedStars = 0;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void btnBackClick()
	{
		SoundManager.Instance.Play_ButtonClick();
		Application.LoadLevel("MainScene");
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(2f,false));
		GameData.IncrementHomeButtonClickedCount();
	}

	public void btnSettingsClicked()
	{
		SoundManager.Instance.Play_ButtonClick();
		menuManager.ShowPopUpMenu(PopUpSettings);
	}

	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		
	}
}
