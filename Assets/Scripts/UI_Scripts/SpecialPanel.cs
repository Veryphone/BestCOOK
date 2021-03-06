﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpecialPanel : MonoBehaviour {
	public int bellValue;
	public int whistleValue;
	public int handcuffValue;
	public int cupcakeValue;
	public int radioValue;

	public Text totalCoinsText;
	
	public Text totalGoldText;

	public string []itemUsage;


	public GameObject myParent;

	public GameObject bellpanel ;
	public GameObject radiopanel ;
	public GameObject whistlepanel ;

	public GameObject bell_tex;
	public GameObject bell_gold;
	public GameObject bell_tick ;

	public GameObject radio_tex;
	public GameObject radio_gold;
	public GameObject radio_tick ;

	public GameObject whistle_tex;
	public GameObject whistle_gold;
	public GameObject whistle_tick ;
	//public Text radio_text ;
	//public Text bell_text ;
	//public Text whistle_text ;
	//public Text handcuff_text ;
	//public Text Cupcake_text ;

	// Use this for initialization
	void Start () {

		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
	}
	void OnEnable()
	{
		if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad1Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad2Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad3Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad4Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPad5Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadAir2 || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini1Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini2Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadMini3Gen || 
		    UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPadUnknown) 
		{
			this.transform.FindChild ("totalCoins").FindChild ("Image (2)").transform.localPosition = new Vector3 (-67, 10f, 0);
			
			Debug.Log("envPanelEnable()____iPad____");
			
		} else 
		{
			this.transform.FindChild ("totalCoins").FindChild ("Image (2)").transform.localPosition = new Vector3 (-90, 10f, 0);
			
			Debug.Log("envPanelEnable()____iPhone____");
			
		}
		if(PlayerPrefs.HasKey("Radio"))
		   {
			radio_tex.SetActive(false);
			radio_tick.SetActive(true);
			radio_gold.SetActive(false);

		}
		if(PlayerPrefs.HasKey("Bell"))
		{
			bell_tex.SetActive(false);
			bell_tick.SetActive(true);
			bell_gold.SetActive(false);
		}
		if(PlayerPrefs.HasKey("Whistle"))
		{
			whistle_tex.SetActive(false);
			whistle_tick.SetActive(true);
			whistle_gold.SetActive(false);	
		}
	}

	void Update()
	{
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		 
	}
	void MenuPopup(string messagePopup)
	{
		if(MenuManager._instance.popupPanel != null)
		{
			MenuManager._instance.popupPanel.gameObject.SetActive (true);
			MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
		}
		else
		{
			GameObject popupPanel = GeneratePopupPanel();
			MenuManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
			MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
		}


//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
	}

	GameObject GeneratePopupPanel()
	{
		GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("PopupPanel"));
		popupPanel.transform.SetParent(transform.parent,false);
		popupPanel.transform.localScale = Vector3.one;
		popupPanel.transform.localPosition = Vector3.zero;
		return popupPanel;
	}

	void GamePopup(string messagePopup)
	{
		if(UIManager._instance.popupPanel != null)
		{
			UIManager._instance.popupPanel.gameObject.SetActive (true);
			UIManager._instance.popupPanel.EnablePopup (messagePopup,false);
		}
		else
		{
			GameObject popupPanel = GeneratePopupPanel();
			UIManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
			UIManager._instance.popupPanel.EnablePopup (messagePopup,false);
		}
	}
	
	public void ClickedHelp(int itemNo)
	{
		if(MenuManager._instance != null)
		{
			MenuPopup(itemUsage[itemNo]);
		}
		else
		{
			GamePopup(itemUsage[itemNo]);
		}
	}

	public void Bell()
	{
		if(!PlayerPrefs.HasKey ("Bell"))
		{

			if(MenuManager.golds >= bellValue)
			{

				if (!PlayerPrefs.HasKey ("BellsTut"))
				{
					PlayerPrefs.SetInt ("BellsTut",1);
				}

				MenuManager.golds-=bellValue;
//				MenuManager.totalscore-=bellValue;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Bell",1);
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("Bell","1"));
				FacebookHandler._instance.send_single_key("Bell","1");
				CallDecrementCoin();
				bell_tex.SetActive(false);
				bell_tick.SetActive(true);
				bell_gold.SetActive(false);
			}
			else
			{
				MenuManager._instance.lastPanel = gameObject;
				MenuManager._instance.lastPanelName = "SpecialPanel";
				MenuManager._instance.Insufficinetgold();
				//Debug.Log("insufficient coins");
			}
		}
		else
		{
			MenuManager._instance.Bellpurchase();
			//Debug.Log("Already Purchased");
		}
	}

	public void Whistle()
	{
		if(!PlayerPrefs.HasKey ("Whistle"))
		{
			if(MenuManager.golds >= whistleValue)
			{
				if (!PlayerPrefs.HasKey ("WhistlesTut"))
				{
					PlayerPrefs.SetInt ("WhistlesTut",1);
				}
				MenuManager.golds-=whistleValue;
//				MenuManager.totalscore-=whistleValue;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Whistle" , 1);
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("Special","1"));
				FacebookHandler._instance.send_single_key("Special","1");
				CallDecrementCoin();
				whistle_tex.SetActive(false);
				whistle_tick.SetActive(true);
				whistle_gold.SetActive(false);
			}
			else
			{
				MenuManager._instance.lastPanel = gameObject;
				MenuManager._instance.lastPanelName = "SpecialPanel";
				MenuManager._instance.Insufficinetgold();
				//Debug.Log("insufficient coins");
			}
		}
		else
		{
			MenuManager._instance.Whistlepurchase();
			//Debug.Log("Already Purchased");
		}
	}

	public void Radio()
	{
		if(!PlayerPrefs.HasKey ("Radio"))
		{

			if(MenuManager.golds >= radioValue)
			{
				if (!PlayerPrefs.HasKey ("radioTut"))
				{
					PlayerPrefs.SetInt ("radioTut",1);
				}
				MenuManager.golds-=radioValue;
//				MenuManager.totalscore-=radioValue;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Radio" , 1);
				//StartCoroutine(FacebookHandler._instance.SendSingleKey("Radio","1"));
				FacebookHandler._instance.send_single_key("Radio","1");
				CallDecrementCoin();
				radio_tex.SetActive(false);
				radio_tick.SetActive(true);
				radio_gold.SetActive(false);
			}
			else
			{
				MenuManager._instance.lastPanel = gameObject;
				MenuManager._instance.lastPanelName = "SpecialPanel";
				MenuManager._instance.Insufficinetgold();
				//Debug.Log("insufficient coins");
			}
		}
		else
		{
			MenuManager._instance.Radiopurchase();
			//Debug.Log("Already Purchased");
		}
	}

	public void HandCuffs()
	{
		if(MenuManager.totalscore >= handcuffValue)
		{
			if (!PlayerPrefs.HasKey ("HandCuffTut"))
			{
				PlayerPrefs.SetInt ("HandCuffTut",1);
			}
//			golds-=goldToUpgradeLevel[upgradeValue];
			MenuManager.totalscore-=handcuffValue;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
//			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			MenuManager.handcuffNo++;
			PlayerPrefs.SetString ("Handcuff",EncryptionHandler64.Encrypt (MenuManager.handcuffNo.ToString ()));
			
			CallDecrementCoin();
		}
		else
		{
			MenuManager._instance.lastPanel = gameObject;
			MenuManager._instance.lastPanelName = "SpecialPanel";
			MenuManager._instance.Insufficinetcoin();
			//Debug.Log("insufficient coins");
		}
	}

	public void Cupcake()
	{

		if(MenuManager.totalscore >= cupcakeValue)
		{
			if (!PlayerPrefs.HasKey ("cupcakeTut"))
			{
				PlayerPrefs.SetInt ("cupcakeTut",1);
			}
//			golds-=goldToUpgradeLevel[upgradeValue];
			MenuManager.totalscore-=cupcakeValue;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
//			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			MenuManager.cupcakeNo = 3;
			PlayerPrefs.SetString ("Cupcake",EncryptionHandler64.Encrypt (MenuManager.cupcakeNo.ToString ()));
			
			CallDecrementCoin();
		}
		else
		{
			MenuManager._instance.Insufficinetcoin();
			//Debug.Log("insufficient coins");
		}
	}

	public void Cross()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
		upgradePanel.transform.SetParent(transform.parent,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}

	public void CallDecrementCoin()
	{
		StopCoroutine ("DecrementCoins");
		StopCoroutine ("DecrementGold");
		StartCoroutine ("DecrementCoins");
		StartCoroutine ("DecrementGold");
	}

	IEnumerator DecrementCoins()
	{
		int textCoins = int.Parse(totalCoinsText.text);

		while (textCoins > MenuManager.totalscore)
		{
			textCoins-=20;
			totalCoinsText.text = textCoins.ToString ();
			yield return 0;
		}
		totalCoinsText.text = MenuManager.totalscore.ToString ();

	}

	IEnumerator DecrementGold()
	{
		int textCoins = int.Parse(totalGoldText.text);
		while (textCoins > MenuManager.golds)
		{
			textCoins-=1;
			totalGoldText.text = textCoins.ToString ();
			yield return 0;
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		
	}

}
