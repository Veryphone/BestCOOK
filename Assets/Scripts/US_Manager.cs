﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class US_Manager : MonoBehaviour {

//	public enum Orders{NONE = 0,HOTDOG = 1,HOTDOG_YELLOW = 2,HOTDOG_RED = 3,COKE =  4};

	public GameObject TheifPanel;
	public int cokePrice;

	public int lessBakedHotdog;

	public int perfectHotDog;
	
	public Sprite []hotDogTikkiVariations;   //3

	public Sprite []hotDogOrderVariations;   //3

	public Sprite []hotDogVariations;  //2 -- 0 - empty , 1 - filled
	public Sprite []hotDogSauces; //2  0 - red , 1 - yellow
	public SpriteRenderer []hotDogSaucesOnPlates; //6
	public SpriteRenderer []hotdogPlates; //6
	public SpriteRenderer []hotdogOnPlates; //6

	public GameObject []grills; //3
	public SpriteRenderer []grillTikkis;  //6
	public Availability []grillPlaces; //6

	public SpriteRenderer []cokeBottles; //9
	public Availability []cokePlaces;  //9

	public int grillsFilledCount;

	public int totalGrillsAvailable;

	public int platesFilledCount;

	public int totalPlatesAvailable;

	public int cokesFilled;

	public int totalCokesAvailable;

	public static US_Manager _instance;

	public bool clickedHotDog , clickedTikki , clickedRedSauce , clickedYellowSauce , clickedCoke;

	public MakeTikki clickedTikkiDestinationFunction;

	public ObjectMotion clickedItemDestinationFunction;

	public HotDog clickedHotDogDestinationFunction;

	public GameObject dustbin;

	public ObjectMotion redSauce , yellowSauce , cupCake;

	public SpriteRenderer cokeAdd , yellowSauceAdd , redSauceAdd;

	public static int noOfPerfects;

	public HotDog firstHotDog;

	public MakeTikki firstTikki;

	public Coins firstCoins;

	public Customer firstCustomer;

	public bool clickfirstBun , clickFirstTikki;

	public SpriteRenderer tableTop, tableCover;
	
	public static bool tutorialEnd;

	public GameObject Radio ;
	public GameObject whistle ;
	public GameObject Bell ;
	public GameObject handcuff ;
	public GameObject starting_text ;

	public GameObject upgrade_bttn ;
	// Use this for initialization
	void Awake () {
		_instance = this;
		//PlayerPrefs.DeleteAll ();
	}

	void OnEnable()
	{
		if (LevelManager.levelNo == 1) {
			starting_text.SetActive(true);
		}
		if(PlayerPrefs.HasKey ("Radio"))
		{

			Radio.SetActive(true);

			
		}
		if(PlayerPrefs.HasKey ("Whistle"))
		{

		whistle.SetActive(true);
			
			
		}
		if (PlayerPrefs.HasKey ("Bell"))
		{

		Bell.SetActive(true);
			
			
		}
		if(MenuManager.handcuffNo > 0)
		{
			handcuff.SetActive(true);
		}
		
	}
	public void TikkiReached()
	{
		clickedTikkiDestinationFunction.ClickedDestination ();
	}

	public void HotDogReached()
	{
		clickedHotDogDestinationFunction.ClickedDestination ();
	}

	public void ObjectReached()
	{
		clickedItemDestinationFunction.ClickedDestination ();
	}

	void Start()
	{
		US_Manager.tutorialEnd = false;
		//Debug.Log("LevelManager.levelNo = "+LevelManager.levelNo);
		if(LevelManager.levelNo == 1)
		{
		starting_text.SetActive(true);
		
			cokeAdd.gameObject.SetActive (false);
			
			cokeAdd.color = new Color(1,1,1,0.5f); 
			cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;

			yellowSauceAdd.gameObject.SetActive (false);
			yellowSauceAdd.color = new Color(1,1,1,0.5f); 
			yellowSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;

			redSauceAdd.gameObject.SetActive (false);
			redSauceAdd.color = new Color(1,1,1,0.5f); 
			redSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;

		}
		else if(LevelManager.levelNo == 2)
		{
//			tutorialEnd = true;
			cokeAdd.gameObject.SetActive (false);
			cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;
			cokeAdd.color = new Color(1,1,1,0.5f); 
		}
		else if(LevelManager.levelNo > 3)
		{
//			tutorialEnd = true;
		}

		int platesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("PlateUpgrade")); 
		//Debug.Log("platesUpgradeValue = "+platesUpgradeValue);
		
		totalPlatesAvailable = 2+(platesUpgradeValue*2);
		for(int i = 0; i < totalPlatesAvailable ; i++)
		{
			hotdogPlates[i].color = new Color(1,1,1,1);
		}

		int cokeUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("USCokeUpgrade")); 
		//Debug.Log("cokeUpgradeValue = "+cokeUpgradeValue);
		
		totalCokesAvailable = 3+(cokeUpgradeValue*3);

		int grillsTpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("GrillsUpgrade")); 
		//Debug.Log("grillsTpgrade = "+grillsTpgrade);
		totalGrillsAvailable = 2+(grillsTpgrade*2);

		int grillVal = (int)totalGrillsAvailable/2;
		//Debug.Log("grillVal = "+grillVal);
		for(int i = 0; i < grillVal ; i++)
		{
			grills[i].SetActive (true);
		}
		//Debug.Log((PlayerPrefs.GetString("US_TableCover")));

		char []coverVal = PlayerPrefs.GetString ("US_TableCover").ToCharArray ();

		UIManager._instance._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());

	

		UIManager._instance.ForCoinAdd ();
	


		//Debug.Log( "val = "+US_last);

//		tableCover.sprite = Resources.Load ("base-flat-1") as Sprite;
		tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableCover"));


		tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableTop")) as Sprite;
		if(MenuManager.cupcakeNo <= 0)
		{
			cupCake.gameObject.SetActive (false);
		}
	}
	void Update()
	{

	}

	public void AddMoreTikki()
	{
		if(clickFirstTikki || (tutorialEnd && !TutorialPanel.popupPanelActive))
		{
			AllClickedBoolsReset();
			if(grillsFilledCount < totalGrillsAvailable)
			{
				for(int i = 0 ; i < totalGrillsAvailable ; i++)
				{
					if(grillPlaces[i].available)
					{
						grillTikkis[i].gameObject.SetActive (true);
						grillTikkis[i].sprite = hotDogTikkiVariations[0];
						grillPlaces[i].available = false;
						grillsFilledCount++;
						break;
					}
				}
			}
			if(clickFirstTikki)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("WAIT FOR THE SAUSAGE \n TO BAKE",false,false , 2);
				firstTikki.tutorialOn = true;
			}
			clickFirstTikki = false;
		}
	}

	public void DeactivateTikkiSelection()
	{
		for(int i = 0 ; i < totalGrillsAvailable ; i++)
		{
			if(!grillPlaces[i].available)
			{
				grillTikkis[i].transform.GetComponent<MakeTikki>().iAmSelected = false;
				grillTikkis[i].transform.GetComponent<MakeTikki>().startAnimating = false;
				grillTikkis[i].transform.GetComponent<MakeTikki>().mySelection.SetActive (false);
				grillTikkis[i].transform.localScale = Vector3.one;
			}
		}
	}


	public void AddHotDogBuns()
	{
		if(clickfirstBun || (tutorialEnd && !TutorialPanel.popupPanelActive))
		{
			AllClickedBoolsReset();
			if(platesFilledCount < totalPlatesAvailable)
			{
				for(int i = 0 ; i < totalPlatesAvailable ; i++)
				{
					if(hotdogPlates[i].gameObject.GetComponent<Availability>().available)
					{
						hotdogOnPlates[i].gameObject.SetActive (true);
						hotdogOnPlates[i].sprite = hotDogVariations[0];
						platesFilledCount++;
						hotdogPlates[i].gameObject.GetComponent<Availability>().available = false;
						hotdogOnPlates[i].transform.GetComponent<HotDog>().perfect = false;
						break;
					}
				}
			}
			if(clickfirstBun)
			{
				clickFirstTikki = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("TAP SAUSAGE TO PUT \n ON THE GRILLS",false,false , 1);
			}
			clickfirstBun = false;
		}
	}

	public void DeactivateBunSelection()
	{
		for(int i = 0 ; i < totalPlatesAvailable ; i++)
		{
			if(!hotdogPlates[i].gameObject.GetComponent<Availability>().available)
			{
				HotDog myHotDog = hotdogOnPlates[i].transform.GetComponent<HotDog>();
				myHotDog.iAmSelected = false;
				myHotDog.startAnimating = false;
				myHotDog.mySelection.SetActive (false);
				hotdogOnPlates[i].transform.localScale = myHotDog.myLocalScale;
			}
		}
	}


	public void AddCokeBottles()
	{
		if(tutorialEnd && !TutorialPanel.popupPanelActive)
		{
			AllClickedBoolsReset();
			if(cokesFilled < totalCokesAvailable)
			{
				for(int i = 0 ; i < totalCokesAvailable ; i++)
				{
					if(cokePlaces[i].available)
					{
						LevelSoundManager._instance.bottle_click.Play();
						cokeBottles[i].gameObject.SetActive (true);
						cokeBottles[i].color = new Color(1,1,1,1);
						cokesFilled++;
						cokePlaces[i].available = false;
						break;
					}
				}
			}
		}
	}

	public void DeactivateAllBottlesSelection()
	{
		for(int i = 0 ; i < totalCokesAvailable ; i++)
		{
			if(cokeBottles[i].gameObject.activeInHierarchy)
			{
				if(cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected)
				{
					cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
					cokeBottles[i].GetComponent<ObjectMotion>().startAnimating = false;
					cokeBottles[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
					cokeBottles[i].gameObject.transform.localScale = Vector3.one;
				}
			}
		}
	}


	

	public void OnClickDustbn()
	{
		if(tutorialEnd)
		{
			if(clickedHotDog)
			{
				clickedHotDogDestinationFunction.otherObject = dustbin;
				HotDogReached();
			}
			else if(clickedTikki)
			{
				if(clickedTikkiDestinationFunction.isBurnt)
				{
					TikkiReached ();
				}
			}

			AllClickedBoolsReset();
		}
	}

	public void AllClickedBoolsReset()
	{
		DeactivateTikkiSelection();
		DeactivateAllBottlesSelection();
		yellowSauce.startAnimating = false;
		redSauce.startAnimating = false;
		yellowSauce.iAmSelected = false;
		redSauce.iAmSelected = false;
		yellowSauce.transform.localScale = yellowSauce.myLocalScale;
		redSauce.transform.localScale = redSauce.myLocalScale;
		redSauce.mySelection.SetActive (false);
		yellowSauce.mySelection.SetActive (false);
		cupCake.iAmSelected = false;
		cupCake.mySelection.gameObject.SetActive (false);
		DeactivateBunSelection();
		clickedHotDog = false;
		clickedTikki = false;
		clickedRedSauce = false;
		clickedYellowSauce = false;
		clickedCoke = false;
	}
}
