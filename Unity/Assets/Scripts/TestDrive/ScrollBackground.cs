using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour {

	public GameObject [] BG;

	Vector3 leftLimit;
	Vector3 offset;
	float speed = 5f;
	Vector3 move;



	public Sprite[] spritesTestDriveBG;
	public GameObject Bushes;

	void Awake()
	{
		int bgIndex = Random.Range(0,3);
		if(bgIndex == 2) Bushes.SetActive(true);
		else Bushes.SetActive(false);
		for(int i=0;i<4;i++)
		{
			BG[i].transform.GetComponent<Image>().sprite =  spritesTestDriveBG[bgIndex];
		}
	}

	// Use this for initialization
	void Start () {


		leftLimit = BG[0].transform.position;
		offset =(BG[2].transform.position - BG[0].transform.position)*2 ;

		//speed = 1+ Random.Range(0,10)/10f;
		move = new Vector3(speed,0,0);
	}
	
	// Update is called once per frame
	void Update () {
 
		for(int i=0;i<4;i++)
		{
		 	BG[i].transform.position -= move*Time.deltaTime; 
			if(BG[i].transform.position.x<leftLimit.x) BG[i].transform.position += offset;
		}
	}
}
