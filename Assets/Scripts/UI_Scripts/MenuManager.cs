﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
public class MenuManager : MonoBehaviour {

	public static string envNo = "US";

	public GameObject levelPanel;

	public GameObject fadePanel;


	public Image fadePanelImage;
	
	public PopupPanel popupPanel; 
	public PopupPanel2 popupPanel2 ;
	public static MenuManager _instance;

	public static int golds;
	
	public static int totalscore;

	public static int cupcakeNo;

	public static int handcuffNo;

	public int Level_loaded ;

	public GameObject lastPanel;
	public string lastPanelName;

	public SoundManager menuSoundButton;

	public GameObject loader; 

	public GameObject loader2 ;
	public GameObject achievement_bttn;
	public Animator achievment;

	public static bool onceRequestBanner;
	public GameObject circular_loader ;
	// Use this for initialization
	void Start () {

//		Debug.LogError("Onee"+PlayerPrefs.GetInt("China/a-1"));
//		Debug.LogError("tree"+PlayerPrefs.GetInt("China/a-3"));
//		Debug.LogError("four"+PlayerPrefs.GetInt("China/a-4"));
//
//
//
//
//		Debug.Log("impp"+PlayerPrefs.GetString("GrillsUpgrade"));

		_instance = this;
		#if UNITY_IOS || UNITY_IPHONE
		Chartboost.cacheInterstitial (CBLocation.Default);
		Debug.Log("Chartboostcaching @Menumanager");

#elif UNITY_ANDROID

#endif
		Application.targetFrameRate = 120;
		AchievementChild.check_claim = (PlayerPrefs.GetInt("claimvalue"));
		//PlayerPrefs.DeleteAll ();

		if(!PlayerPrefs.HasKey ("PlateUpgrade"))
		{
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt ("1"));
			PlayerPrefs.SetString ("TotalScore",EncryptionHandler64.Encrypt ("100"));

			PlayerPrefs.SetString ("Cupcake",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("Handcuff",EncryptionHandler64.Encrypt ("0"));


			PlayerPrefs.SetString ("GrillsUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("USCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("PlateUpgrade",EncryptionHandler64.Encrypt ("0"));


			PlayerPrefs.SetString ("USStars",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("USLevels",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("US_TableCover","US/base-flat-1");
			PlayerPrefs.SetString ("US_TableTop","US/top-floor-1");

			PlayerPrefs.SetInt ("US/base-flat-1" , 1);
			PlayerPrefs.SetInt ("US/top-floor-1" , 1);

			PlayerPrefs.SetString ("AusLevels",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ItalyLevels",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ChinaLevels",EncryptionHandler64.Encrypt ("0"));

			PlayerPrefs.SetString ("ItalyStars",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("Italy_TableCover","Italy/1");
			PlayerPrefs.SetString ("Italy_TableTop","Italy/top-strip-1");

			PlayerPrefs.SetString ("ItalyPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ItalyCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("OvenUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetInt ("Italy/1" , 1);
			PlayerPrefs.SetInt ("Italy/top-strip-1" , 1);


			PlayerPrefs.SetString ("ChinaStars",EncryptionHandler64.Encrypt ("0"));

			PlayerPrefs.SetString ("China_TableCover","China/1");
			PlayerPrefs.SetString ("China_TableTop","China/a-1");

			PlayerPrefs.SetString ("ChinaPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ChinaBowlsUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ChinaPansUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("ChinaSoupContainerUpgrade",EncryptionHandler64.Encrypt ("0"));

			PlayerPrefs.SetInt ("China/1" , 1);
			PlayerPrefs.SetInt ("China/a-1" , 1);
				
			PlayerPrefs.SetString ("AusStars",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("Aus_TableCover","Aus/1");
			PlayerPrefs.SetString ("Aus_TableTop","Aus/top-shed-1");

			PlayerPrefs.SetString ("AusPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("FriesUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("AusCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
			PlayerPrefs.SetString ("AusGrillsUpgrade",EncryptionHandler64.Encrypt ("0"));

			PlayerPrefs.SetInt ("Aus/1" , 1);
			PlayerPrefs.SetInt ("Aus/top-shed-1" , 3);
			PlayerPrefs.SetInt ("Upgrade2", 1);
		}

	
//		PlayerPrefs.SetInt ("ItalyOpen", 1);
//		PlayerPrefs.SetInt ("ChinaOpen", 1);
	
		//	PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt ("100"));
//			PlayerPrefs.SetString ("ChinaSoupContainerUpgrade",EncryptionHandler64.Encrypt ("0"));
	

		golds = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Golds"));
		totalscore = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("TotalScore"));
		cupcakeNo = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Cupcake"));
		handcuffNo = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Handcuff"));



	}

	public void EnableFB()
	{
		FacebookHandler._instance.EnableFB ();
	}
	public void FB_invite()
	{
		FacebookHandler._instance.FBAppinvite ();
	}
	public void Fbinit()
	{
		FacebookHandler._instance.FbLogin ();
	}

	void OnEnable()
	{
//		Debug.Log("cliam"+PlayerPrefs.GetInt("claimvalue"));

		if(UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad1Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad2Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad3Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad4Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad5Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadAir2 || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen || 
		   UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadUnknown )
		{
			this.transform.FindChild("MainScreenPanel").FindChild("Man").GetComponent<RectTransform>().localScale = new Vector3(13,6f,6.548484f);

			Debug.Log("____iPad_______");

		
		}
		
		else
		{
			this.transform.FindChild("MainScreenPanel").FindChild("Man").GetComponent<RectTransform>().localScale = new Vector3(16,6.145341f,6.548484f);

			Debug.Log("____iPhone_______");

		}
	}
	// Update is called once per frame
	void Update () {

		if (PlayerPrefs.GetInt("claimvalue") > 0) {
			achievment.enabled = true;
		} else 
		{

			achievment.enabled = false;
		}
	//	if(Time.deltaTime > 0)
	//		Debug.Log ("fps =  "+1/Time.deltaTime);
	}


	public void Decorationpanel()
	{

	}
	public void USLevel()
	{

#if UNITY_ANDROID


		#elif UNITY_IOS || UNITY_IPHONE
		if (Chartboost.hasInterstitial (CBLocation.Default))
		{
			Chartboost.showInterstitial (CBLocation.Default);
			Debug.Log("Chartboost @Menumanager");
		}else{
			Chartboost.cacheInterstitial (CBLocation.Default);
			Debug.Log("Chartboostcaching @Menumanager");

		}
#endif
//		Application.LoadLevel (1);
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("EnvPanel"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		EnableFadePanel();
	}

	public void Upgrades()
	{

		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		EnableFadePanel();
	}
	public void loader2f()
	{
		loader2.SetActive (true);
	}

	public void circular_Loader()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Cicular_loader"));
		upgradePanel.transform.SetParent(transform,false);
		//upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;

	}
	public void Achievments()
	{

		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("AchievementsPanel"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		loader2.SetActive (false);
		EnableFadePanel();

	}

	public void LevelPanelCross()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("EnvPanel"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		levelPanel.SetActive (false);
		EnableFadePanel();
	}
	public void Exitpanel()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("ExitPanel"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
	
		EnableFadePanel();
	}

	public void FadeInOff()
	{
		fadePanel.SetActive (false);
	}

	public void EnableFadePanel()
	{
		fadePanel.transform.SetAsLastSibling ();
		fadePanel.SetActive (true);
		StartCoroutine (FadeIn());
	}

	IEnumerator FadeIn()
	{
		float alphaVal = 1;
		fadePanelImage.color = new Color(1,1,1,1);
		while(alphaVal > 0.1f)
		{
			alphaVal-=Time.deltaTime;
			fadePanelImage.color = new Color(1,1,1,alphaVal);
			yield return 0;
		}
		FadeInOff();
	}
	public void Insufficinetcoin()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("InsufficinetCoin"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}
	public void Insufficinetgold()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("InsufficinetGold"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}
	public void Alreadypurchase()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Already"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}

	public void Radiopurchase()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("AlreadyRadiol"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}
	public void Bellpurchase()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Alreadybell"));
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}
	public void Whistlepurchase()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("AlreadyWhistle"));
		    
		upgradePanel.transform.SetParent(transform,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		
		EnableFadePanel();
	}


	public void GoldPanel()
	{
		GameObject specialPanel = ( GameObject )Instantiate(Resources.Load ("GoldPanel"));
		specialPanel.transform.SetParent(transform.parent,false);
		specialPanel.transform.localScale = Vector3.one;
		specialPanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);
			

	}
	public GameObject GeneratePopupPanel()
	{

		GameObject popupPanel2 = ( GameObject )Instantiate(Resources.Load ("EqPopupPanel"));
		popupPanel2.transform.SetParent(transform.parent,false);
		//popupPanel.transform.localScale = Vector3.one;
		popupPanel2.transform.localPosition = Vector3.zero;
		return popupPanel2;
	}



}
